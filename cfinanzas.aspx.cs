using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using OfficeOpenXml;

namespace despacho
{
    public partial class cfinanzas : System.Web.UI.Page
    {
        cSolicitudes cSol;
        cPerfiles cPerfil;
        cFactura cFactura;
        cSucursales sucursal;
        cClientes cte;
        cUsuarios vendedor;
        string carpeta = "";
        float totalS = 0;
        float totalP = 0;
        float totalSaldo = 0;
        //Para visualizar folios de pagos, agregado por Enrique el 14-11-2022
        cPagos cpago;

        //Para visualizar folios de remisiones, agregado por Enrique el 16-10-2023
        cRemision crem;

        protected void Page_Load(object sender, EventArgs e)
        {
            cSol = new cSolicitudes();
            cPerfil = new cPerfiles();
            cFactura = new cFactura();
            sucursal = new cSucursales();
            cte = new cClientes();
            vendedor = new cUsuarios();
            if (!IsPostBack)
            {
                if (Request.Cookies["ksroc"] == null)
                {
                    Response.Redirect("login.aspx");
                }
                else
                {
                    //validamos si hay datos en cookies de filtro 
                   
                        if (Request.QueryString["fechaIF"] != "" && Request.QueryString["fechaIF"] != null)
                        {
                            txtFechaI.Text = Request.QueryString["fechaIF"];
                        }
                        else
                        {
                            txtFechaI.Text = DateTime.Now.AddDays(-1).ToShortDateString();
                        }
                        if (Request.QueryString["fechaFF"] != "" && Request.QueryString["fechaFF"] != null)
                        {
                            txtFechaF.Text = Request.QueryString["fechaFF"];
                        }
                        else
                        {
                            txtFechaF.Text = DateTime.Now.AddDays(-1).ToShortDateString();
                        }
                        if (Request.QueryString["ordenF"] != "" && Request.QueryString["ordenF"] != null)
                        {
                            txtOrden.Text = Request.QueryString["ordenF"];
                            hdfParametros.Value += " AND o.folio = '" + Request.QueryString["ordenF"] + "'";
                        }
                        if (Request.QueryString["remF"] != "" && Request.QueryString["remF"] != null)
                        {
                            txtRem.Text = Request.QueryString["remF"];
                            hdfParametros.Value += " AND od.folioR = '" + Request.QueryString["remF"] + "'";
                        }
                        if (Request.QueryString["cteF"] != "" && Request.QueryString["cteF"] != null)
                        {
                            ddlCte.SelectedValue = Request.QueryString["cteF"];
                            hdfParametros.Value += "  AND s.idCliente = " + Request.QueryString["cteF"];
                        }
                        if (Request.QueryString["vendedorF"] != "" && Request.QueryString["vendedorF"] != null)
                        {
                            ddlVendedor.SelectedValue = Request.QueryString["vendedorF"];
                            hdfParametros.Value += " AND s.idVendedor =" + Request.QueryString["vendedorF"];
                        }
                        if (Request.QueryString["estatusF"] != "" && Request.QueryString["estatusF"] != null)
                        {
                            ddlEstatus.SelectedValue = Request.QueryString["estatusF"];
                            hdfParametros.Value = " AND s.estatusPago = '" + Request.QueryString["estatusF"] + "'";
                        }
                    
                    //buscamos la crpeta 
                    sucursal.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                    sucursal.buscarCarpetaTimbre();
                    carpeta = sucursal.carpetaTimbre;

                    
                    
                    

                    cte.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                    ddlCte.DataSource = cte.obtenerClientesCF();
                    ddlCte.DataTextField = "nombre";
                    ddlCte.DataValueField = "id";
                    ddlCte.DataBind();
                    ddlCte.Items.Insert(0, new ListItem("Filtrar por Cliente", ""));
                    
                    ddlVendedor.DataSource = vendedor.obtenerVendedoresActivosBySuc(int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
                    ddlVendedor.DataTextField = "nombre";
                    ddlVendedor.DataValueField = "id";
                    ddlVendedor.DataBind();
                    ddlVendedor.Items.Insert(0, new ListItem("Filtrar por Vendedor", ""));
                    lblTotalCobrar.Text = "<strong>Total: </strong> $ " + totalS.ToString("#,##0.00");
                    lblCobrado.Text = "<strong>Pagado: </strong> $ " + totalP.ToString("#,##0.00");
                    lblSaldo.Text = "<strong>Saldo: </strong> $ " + totalSaldo.ToString("#,##0.00");
                    mostrarSolicitudes();
                }
            }
        }

        private void mostrarSolicitudes()
        {
            totalS = 0;
            cPerfil.id = int.Parse(Request.Cookies["ksroc"]["idPerfil"]);
            cPerfil.obtenerPerfilByID();
            string param = hdfParametros.Value + " AND (s.reqFac = 'NO' OR s.reqFac is null)";
            if(cPerfil.descripcion != "VENDEDOR" && cPerfil.descripcion != "Vendedor")
            {
                if(txtFechaI.Text != "" && txtFechaF.Text != "")
                {
                    DateTime fechaI = DateTime.Parse(txtFechaI.Text);   
                    DateTime fechaF = DateTime.Parse(txtFechaF.Text);
                    param += " AND (o.fecha BETWEEN '" + fechaI.ToString("yyyy-MM-dd") + "' AND '" + fechaF.ToString("yyyy-MM-dd") +"')";
                }
                if (txtFoliosPagos.Text != "") {
                    cpago = new cPagos();
                    int idSoli = cpago.obtenerIDSolicitud(int.Parse(txtFoliosPagos.Text));
                    param += " AND (s.id = "+idSoli+") ";
                }
                lvSolicitudes.DataSource = cSol.mostrarSolicitudesF(int.Parse(Request.Cookies["ksroc"]["idSucursal"]), param);
                lvSolicitudes.DataBind();
            }
            else
            {
                lvSolicitudes.DataSource = "";
                lvSolicitudes.DataBind();
            }
        }

        protected void btnPago_ItemCommand(object sender, CommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("pagos"))
                {
                    string[] arr;
                    arr = e.CommandArgument.ToString().Split('ˇ');
                    hfId.Value = arr[0];
                    Response.Redirect("PagosSolicitudF.aspx?idSolicitud=" + arr[0] + "&Vendedor=" + arr[1] + "&estatus=" + arr[2] + "&factura=no&fechaIF=" + txtFechaI.Text + "&fechaFF=" + txtFechaF.Text + "&ordenF=" + txtOrden.Text + "&remF=" + txtRem.Text + "&cteF=" + ddlCte.SelectedValue + "&vendedorF=" + ddlVendedor.SelectedValue + "&estatusF=" + ddlEstatus.SelectedValue);


                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        protected void btnFacturar_ItemCommand(object sender, CommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("facturar"))
                {
                    string[] arr;
                    arr = e.CommandArgument.ToString().Split('ˇ');
                    if (arr[2] == null || arr[2] == "" || arr[2] == "0")
                    {
                        hfId.Value = arr[0];
                        Response.Redirect("facturarSolicitud.aspx?idSolicitud=" + arr[0] + "&idCliente=" + arr[1] + "&factura=no&fechaIF=" + txtFechaI.Text + "&fechaFF=" + txtFechaF.Text + "&ordenF=" + txtOrden.Text + "&remF=" + txtRem.Text + "&cteF=" + ddlCte.SelectedValue + "&vendedorF=" + ddlVendedor.SelectedValue + "&estatusF=" + ddlEstatus.SelectedValue);
                    }
                    
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        protected void btnVistaPrevia_ItemCommand(object sender, CommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("vistaprevia"))
                {
                    string[] arr;
                    arr = e.CommandArgument.ToString().Split('ˇ');
                    hfId.Value = arr[0];
                    if(arr[0] == null || arr[0] == "")
                    {
                        //Response.Redirect("impfactura.asp?idfactura=" + arr[1] + "&idCliente=" + arr[2]);
                        Response.Write("<script>window.open('impfacturasol.asp?idfactura=" + arr[1] + "&idCliente=" + arr[2] + "' ,'_blank');</script>");
                    }
                    else
                    {
                        int idcliente = cFactura.obtenerIdClienteFacturaByID(int.Parse(arr[0]));
                        //Response.Redirect("impfactura.asp?idfactura=" + arr[0] + "&idCliente=" + arr[2]);
                        Response.Write("<script>window.open('impfactura.asp?idfactura=" + arr[0] + "&idCliente=" + idcliente + "' ,'_blank');</script>");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string montoSolicitud(int idSolicitud, string estatusP, float cTotal, float cantE, bool eliminadoOD)
        {
            float montoSolicitud = float.Parse(cSol.obtenerMontoSolicitud(idSolicitud));
            float pago = float.Parse(pagadoTotal(idSolicitud));
            string status = estatusOrd(cTotal, cantE, eliminadoOD);
            if(status != "Cancelado")
            {
                totalS = totalS + montoSolicitud;
                totalP = totalP + pago;
                totalSaldo = totalS - totalP;
                lblTotalCobrar.Text = "<strong>Total:</strong> $" + totalS.ToString("#,##0.00");
                lblCobrado.Text = "<strong>Pagado:</strong> $" + totalP.ToString("#,##0.00");
                lblSaldo.Text = "<strong>Saldo:</strong> $" + totalSaldo.ToString("#,##0.00");
            }
            
            if(estatusP == "" || estatusP == null)
            {
                ActualizarEstatusPago(idSolicitud, montoSolicitud);
            }
            
            return montoSolicitud.ToString("#,##0.00");
        }

        private void ActualizarEstatusPago(int idS, float monto)
        {
            string pagadoT = pagadoTotal(idS);
            float pagoT = float.Parse(pagadoT);
            string estatus = "";
            if(monto > 0)
            {
                if (pagoT == monto) //pagada
                {
                    estatus = "Pagado";
                }
                else if (pagoT > 0 && pagoT != monto)
                {
                    //parcial
                    estatus = "Parcial";
                }
                else
                {
                    //no pagada
                    estatus = "No Pagado";
                }
            }
            else
            {
                estatus = "No Pagado";
            }
            
            cSol.actualizarEstPago(int.Parse(Request.Cookies["ksroc"]["id"]), idS, estatus);
        }

        public string pagadoTotal(int idSolicitud)
        {
            
            if(cSol.existePagoSolicitud(idSolicitud))
            {
                float pagadoTotal = float.Parse(cSol.obtenerPagadoSolicitud(idSolicitud));
                return pagadoTotal.ToString("#,##0.00");
            }
            else
            {
                return "00.00";
            }
        }
        public string saldo(int idSolicitud)
        {
            float montoSolicitud = float.Parse(cSol.obtenerMontoSolicitud(idSolicitud));
            
            if (cSol.existePagoSolicitud(idSolicitud))
            {
                float pagadoTotal = float.Parse(cSol.obtenerPagadoSolicitud(idSolicitud));
                float saldo = montoSolicitud - pagadoTotal;
                return saldo.ToString("#,##0.00");
            }
            else
            {
                return montoSolicitud.ToString("#,##0.00");
            }
        }

        //se crea método publico para saber si mstrar el icono de facturación y la vista previa 
        public string mostrarFactura(int idSol, int idCliente, string idFactura)
       {
            string param = "";
            if(idFactura == "") //si el idfactura es vacio aún no se agrega factura
            {
                param = "<a href='facturarSolicitud.aspx?idSolicitud=" + idSol+"&idCliente="+idCliente+ "&factura=no&fechaIF=" + txtFechaI.Text + "&fechaFF=" + txtFechaF.Text + "&ordenF=" + txtOrden.Text + "&remF=" + txtRem.Text + "&cteF=" + ddlCte.SelectedValue + "&vendedorF=" + ddlVendedor.SelectedValue + "&estatusF=" + ddlEstatus.SelectedValue+"'><span class='icon-documents'></span></a>";
            }
            else //si no muestra solo la vista previa y muestra el pdf y xml y estatus
            {
                //buscamos la factura 
                cFactura.idfactura = long.Parse(idFactura);
                cFactura.buscarFactura();
                if(cFactura.estatus != "Pendiente" && cFactura.estatus != "Cancelada")
                {
                    param = "<iframe src='carga2.asp?id="+idFactura+"' width='150px' height='70px' frameborder='0'></iframe>";
                }
                else
                {
                    param = cFactura.estatus + "<br>";
                }
                //param += "<a href='impfactura.asp?idfactura=" + idFactura + "&idCliente=" + idCliente + "' target='_blank'><span>Vista Previa</span></a>";
            }
            return param;
       }

        public string mostrarXML(string tipo, int idSol, int idCliente, string idFactura)
        {
            string para = "";
            if(idFactura != "")
            {
                cFactura.idfactura = long.Parse(idFactura);
                cFactura.buscarFactura();
                if (cFactura.buscarArchivos())
                {
                    if (tipo == "XML")
                    {
                        string ruta = carpeta + @"\" + cFactura.XML;
                        if (File.Exists(ruta)) //validamos que exista el archivo
                        {
                            para = "<a href='" + ruta + "' target='_blank'>" + cFactura.XML + "</a>";
                        }
                    }
                    else
                    {
                        string ruta = carpeta + @"\" + cFactura.PDF;
                        if (File.Exists(ruta)) //validamos que exista el archivo
                        {
                            para = "<a href='" + ruta + "' target='_blank'>" + cFactura.PDF + "</a><br>";
                        }
                        
                    }
                }
                if(tipo == "PDF")
                {
                    para += "<a href='impfactura.asp?idfactura=" + idFactura + "&idCliente=" + cFactura.idcliente + "' target='_blank'><span>Vista Previa</span></a>";
                }
            }
            return para;
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            mostrarSolicitudes();
        }

        public string estatusOrd(float cTotal, float cantE, bool eliminadoOD)
        {
            string param = "";
            if(cTotal > 0)
            {
                if (cantE == cTotal)
                {
                    param = "Terminada";
                }
                else if (cTotal > 0 && cantE < cTotal)
                {
                    param = "En Proceso";
                }
            }
            if (eliminadoOD)
            {
                param = "Cancelado";
            }
            
            return param;
        }

        protected void lbtnOrdenEntrega_Command(object sender, CommandEventArgs e)
        {
            string[] arr;
            arr = e.CommandArgument.ToString().Split('ˇ');

            string url = "OrdenEntrega.aspx?idO=" + arr[0].ToString() + "&idS=" + arr[1].ToString() + "&idDS=" + arr[2].ToString() + "&retro=cfinanzas";
            string s = "window.open('" + url + "', 'popup_window', 'width=1000,height=700,left=100,top=100,resizable=yes');";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", s, true);
        }

        protected void txtFechaI_TextChanged(object sender, EventArgs e)
        {
            //if(txtFechaI.Text != "")
            //{
            //    Request.Cookies["filtro"]["fechaIF"] = txtFechaI.Text.ToString();
            //}
            //if (txtFechaF.Text != "")
            //{
            //    Request.Cookies["filtro"]["fechaFF"] = txtFechaF.Text.ToString();
            //}
            //if (txtOrden.Text != "")
            //{
            //    Request.Cookies["filtro"]["ordenF"] = txtOrden.Text;
            //}
            //if (txtRem.Text != "")
            //{
            //    Request.Cookies["filtro"]["remF"] = txtRem.Text;
            //}
            //if (ddlCte.SelectedValue != "")
            //{
            //    Request.Cookies["filtro"]["cteF"] = ddlCte.SelectedValue;
            //}
            //if (ddlVendedor.SelectedValue != "")
            //{
            //    Request.Cookies["filtro"]["vendedorF"] = ddlVendedor.SelectedValue;
            //}
            //if (ddlEstatus.SelectedValue != "")
            //{
            //    Request.Cookies["filtro"]["estatusF"] = ddlEstatus.SelectedValue;
            //}
        }

        //Método para obtener los folios de pagos a través del idSolicitud
        public string obtenerFoliosPagos(int idSolicitud) {
            cpago = new cPagos();
            return cpago.obtenerFolioByIDSolicitud(idSolicitud);
        }

        //Método para obtener los folios de pagos a través del idSolicitud
        public string obtenerFoliosRemisiones(int idO) {
            crem = new cRemision();
            
            return crem.obtenerFolioByIDOrden(idO);
        }

        protected void ExportToExcel() {
            DataTable dt;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            totalS = 0;
            cPerfil.id = int.Parse(Request.Cookies["ksroc"]["idPerfil"]);
            cPerfil.obtenerPerfilByID();
            string param = hdfParametros.Value + " AND (s.reqFac = 'NO' OR s.reqFac is null)";
            if (cPerfil.descripcion != "VENDEDOR" && cPerfil.descripcion != "Vendedor") {
                if (txtFechaI.Text != "" && txtFechaF.Text != "") {
                    DateTime fechaI = DateTime.Parse(txtFechaI.Text);
                    DateTime fechaF = DateTime.Parse(txtFechaF.Text);
                    param += " AND (o.fecha BETWEEN '" + fechaI.ToString("yyyy-MM-dd") + "' AND '" + fechaF.ToString("yyyy-MM-dd") + "')";
                }
                if (txtFoliosPagos.Text != "") {
                    cpago = new cPagos();
                    int idSoli = cpago.obtenerIDSolicitud(int.Parse(txtFoliosPagos.Text));
                    param += " AND (s.id = " + idSoli + ") ";
                }
                dt = cSol.mostrarSolicitudesFExcel(int.Parse(Request.Cookies["ksroc"]["idSucursal"]), param);
            }
            else {
                dt = null;
            }
            if (dt != null) {
                // Crear un nuevo archivo Excel
                using (ExcelPackage package = new ExcelPackage()) {
                    // Agregar una hoja de trabajo al archivo Excel
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Reporte Finanzas");

                    // Llenar la hoja de trabajo con los datos de la consulta SQL
                    worksheet.Cells["A1"].LoadFromDataTable(dt, true);

                    // Guardar el archivo Excel en el servidor o descargarlo
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;  filename=reporte_finanzas.xlsx");
                    Response.BinaryWrite(package.GetAsByteArray());
                    Response.End();
                }
            }
            
        }

        protected void btnExcel_Click(object sender, EventArgs e) {
            ExportToExcel();
        }
    }
}