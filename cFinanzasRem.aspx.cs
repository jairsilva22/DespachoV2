
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class cFinanzasRem : System.Web.UI.Page
    {
        cSolicitudes cSol;
        cPerfiles cPerfil;
        cFactura cFactura;
        cSucursales sucursal;
        cClientes cte;
        cUsuarios vendedor;
        cContpaq contpaq;
        string carpeta = "";
        float totalS = 0;
        float totalP = 0;
        float totalSaldo = 0;
        int ultimo = 0;
        cPagos cpago;
        protected void Page_Load(object sender, EventArgs e)
        {
            cSol = new cSolicitudes();
            cPerfil = new cPerfiles();
            cFactura = new cFactura();
            sucursal = new cSucursales();
            cte = new cClientes();
            vendedor = new cUsuarios();
            contpaq = new cContpaq();

            //Actualizar letrero de ultima actualización
            DataTable dt2 = contpaq.obtenerActualizacion();
            if (dt2.Rows.Count > 0)
            {
              //  LabelActualizaciones.Text = ("Ultima Actualización: " + dt2.Rows[0]["ultimaConsulta"].ToString());
            }

            if (!IsPostBack)
            {
                if (Request.Cookies["ksroc"] == null)
                {
                    Response.Redirect("login.aspx");
                }
                else
                {
                    //Buscmaos si hay datos de filtro 
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
                    lblTotalCobrar.Text = "<strong>Total: </strong> " + totalS.ToString("#,##0.00");
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
            string param = hdfParametros.Value + " AND s.reqFac = 'SI'";
            if (cPerfil.descripcion != "VENDEDOR" && cPerfil.descripcion != "Vendedor")
            {
                if (txtFechaI.Text != "" && txtFechaF.Text != "")
                {
                    DateTime fechaI = DateTime.Parse(txtFechaI.Text);
                    DateTime fechaF = DateTime.Parse(txtFechaF.Text);
                    param += " AND (o.fecha BETWEEN '" + fechaI.ToString("yyyy-MM-dd") + "' AND '" + fechaF.ToString("yyyy-MM-dd") + "')";
                }
                if (txtFoliosPagos.Text != "") {
                    cpago = new cPagos();
                    int idSoli = cpago.obtenerIDSolicitud(int.Parse(txtFoliosPagos.Text));
                    param += " AND (s.id = " + idSoli + ") ";
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
                    Response.Redirect("PagosSolicitudF.aspx?idSolicitud=" + arr[0] + "&Vendedor=" + arr[1] + "&estatus=" + arr[2] + "&factura=si&fechaIF=" + txtFechaI.Text + "&fechaFF=" + txtFechaF.Text + "&ordenF=" + txtOrden.Text + "&remF=" + txtRem.Text + "&cteF=" + ddlCte.SelectedValue + "&vendedorF=" + ddlVendedor.SelectedValue + "&estatusF=" + ddlEstatus.SelectedValue);
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
                        Response.Redirect("facturarSolicitud.aspx?idSolicitud=" + arr[0] + "&idCliente=" + arr[1] + "&factura=si&fechaIF=" + txtFechaI.Text + "&fechaFF=" + txtFechaF.Text + "&ordenF=" + txtOrden.Text + "&remF=" + txtRem.Text + "&cteF=" + ddlCte.SelectedValue + "&vendedorF=" + ddlVendedor.SelectedValue + "&estatusF=" + ddlEstatus.SelectedValue);
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
                    if (arr[0] == null || arr[0] == "")
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
            if (status != "Cancelado")
            {
                totalS = totalS + montoSolicitud;
                totalP = totalP + pago;
                totalSaldo = totalS - totalP;
                lblTotalCobrar.Text = "<strong>Total:</strong> $" + totalS.ToString("#,##0.00");
                lblCobrado.Text = "<strong>Pagado:</strong> $" + totalP.ToString("#,##0.00");
                lblSaldo.Text = "<strong>Saldo:</strong> $" + totalSaldo.ToString("#,##0.00");
            }

            if (estatusP == "" || estatusP == null)
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
            if (monto > 0)
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
            if (cSol.existePagoSolicitud(idSolicitud))
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
            if (idFactura == "") //si el idfactura es vacio aún no se agrega factura
            {
                param = "<a href='facturarSolicitud.aspx?idSolicitud=" + idSol + "&idCliente=" + idCliente + "&factura=si&fechaIF=" + txtFechaI.Text + "&fechaFF=" + txtFechaF.Text + "&ordenF=" + txtOrden.Text + "&remF=" + txtRem.Text + "&cteF=" + ddlCte.SelectedValue + "&vendedorF=" + ddlVendedor.SelectedValue + "&estatusF=" + ddlEstatus.SelectedValue + "'><span class='icon-documents'></span></a>";
            }
            else //si no muestra solo la vista previa y muestra el pdf y xml y estatus
            {
                //buscamos la factura 
                cFactura.idfactura = long.Parse(idFactura);
                cFactura.buscarFactura();
                if (cFactura.estatus != "Pendiente" && cFactura.estatus != "Cancelada")
                {
                    param = "<iframe src='carga2.asp?id=" + idFactura + "' width='150px' height='70px' frameborder='0'></iframe>";
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
            if (idFactura != "")
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
                if (tipo == "PDF")
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
            if (cTotal > 0)
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


        //actualizar contpaq
        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            /*Antes de comezar la consulta, inicializar el ID inicial para la consulta*/
            DataTable dt =  contpaq.obtenerDatos();
            if (dt.Rows.Count > 0)
            {
                
                //ciclo pra comenzar el llenado de la tabla de Pagos /*Revisar lleado, el fo deberia llenar al reves*/
                contpaq.IdInicial = int.Parse(contpaq.obtenerUltimoId());
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    contpaq.folio= dt.Rows[i]["CFOLIO"].ToString();
                    contpaq.pago = dt.Rows[i]["CTOTAL"].ToString();
                    contpaq.fecha = DateTime.Parse(dt.Rows[i]["CFECHA"].ToString());
                    contpaq.observaciones = dt.Rows[i]["COBSERVACIONES"].ToString();
                    contpaq.idContpaq = int.Parse(dt.Rows[i]["CIDDOCUMENTO"].ToString());
                    contpaq.folioAux = contpaq.folio.ToString();
                    contpaq.idSolicitud = int.Parse(contpaq.obtenerSolicitudPorRemision());
                    //insertar
                    //validar que no exista el pago que queremos insertar
                    if (contpaq.existePago() == false)
                    {
                        contpaq.folioPago = int.Parse(contpaq.obtenerUltimoFolio()) + 1;
                        contpaq.insertarPagos();
                    }

                    //insertamos el ultimo Id de nuestra consulta
                    if (i == dt.Rows.Count - 1)
                    //if(i == 0)
                    {
                        contpaq.ultimoId = Convert.ToInt32(dt.Rows[i]["CIDDOCUMENTO"]);
                        contpaq.insertarUltimoId();
                        contpaq.nombreUsr = (Request.Cookies["ksroc"]["usuario"]);
                        contpaq.insertarLogContpaq();
                    }
                    //Avisamos que la extracción concluyo satisfactoriamente
                    if (i == dt.Rows.Count - 1)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Contpaq", "$('#myModalContpaq').modal('show');", true);
                        LabelTerminaContpaq.Text = "Los Pagos han sido extraídos con éxito";
                    }
                }
            }
            //Si no encontramos datos mandamos una alerta
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Contpaq", "$('#myModalContpaq').modal('show');", true);
                LabelTerminaContpaq.Text = "No se encontraron Pagos en Contpaq";
            }
            //Actualizar letrero de ultima actualización
            DataTable dt2 = contpaq.obtenerActualizacion();
            if (dt2.Rows.Count > 0)
            {
              //  LabelActualizaciones.Text = ("Ultima Actualización: " + dt2.Rows[0]["ultimaConsulta"].ToString());
            }
        }

        protected void btnImportaDatosCompaq_Click(object sender, EventArgs e)
        {

        }

        //Método para obtener los folios de pagos a través del idSolicitud
        public string obtenerFoliosPagos(int idSolicitud) {
            cpago = new cPagos();
            return cpago.obtenerFolioByIDSolicitud(idSolicitud);
        }


        public string saldoContpaq(int idOrden, int idSolicitud)
        {
            float montoContpaq = 0;
            montoContpaq = float.Parse(cSol.obtenerMontoOrdenDosificacionContpaq(idOrden));
            float montoSolicitud = float.Parse(cSol.obtenerMontoSolicitud(idSolicitud));
            if (montoContpaq <= 0)
            {
                return montoContpaq.ToString("#,##0.00");

            }

            float difereciaMontos = montoSolicitud - montoContpaq;
            return difereciaMontos.ToString("#,##0.00");
            /*if (cSol.existePagoSolicitud(idSolicitud))
            {
                float pagadoTotal = float.Parse(cSol.obtenerPagadoSolicitud(idSolicitud));
                float saldoContpaq = montoContpaq - pagadoTotal;
                return saldoContpaq.ToString("#,##0.00");
            }
            else
            {
                return montoContpaq.ToString("#,##0.00");
            }*/
        }

        public string saldoContpaqEstilo(int idOrden, int idSolicitud)
        {
            float montoContpaq = 0;
            montoContpaq = float.Parse(cSol.obtenerMontoOrdenDosificacionContpaq(idOrden));
            float montoSolicitud = float.Parse(cSol.obtenerMontoSolicitud(idSolicitud));
            if (montoContpaq <= 0)
            {
                return "";

            }

            float diferenciaMontos = montoSolicitud - montoContpaq;
            if (diferenciaMontos >= 1 || diferenciaMontos <= -1)
            {
                return "border: 1px solid red;";

            }
            return "";

        }
        protected void ExportToExcel() {
            DataTable dt;
            totalS = 0;
            cPerfil.id = int.Parse(Request.Cookies["ksroc"]["idPerfil"]);
            cPerfil.obtenerPerfilByID();
            string param = hdfParametros.Value + " AND s.reqFac = 'SI'";
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
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // or LicenseContext.Commercial, depending on your license

                using (ExcelPackage package = new ExcelPackage()) {
                    // Agregar una hoja de trabajo al archivo Excel
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Reporte Finanzas");

                    // Llenar la hoja de trabajo con los datos de la consulta SQL
                    worksheet.Cells["A1"].LoadFromDataTable(dt, true);

                    // Guardar el archivo Excel en el servidor o descargarlo
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;  filename=reporte_finanzas_facturables.xlsx");
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