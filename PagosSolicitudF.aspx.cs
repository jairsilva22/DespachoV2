using despacho.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho {
    public partial class PagosSolicitud : System.Web.UI.Page {
        cPagos cPag = new cPagos();
        cPagos cPagVendedor = new cPagos();
        cSolicitudes cSol = new cSolicitudes();
        cPDFpago pdf = new cPDFpago();
        cOrdenesDosificacion od = new cOrdenesDosificacion();
        cContpaq contpaq;

        protected void Page_Load(object sender, EventArgs e) {
            contpaq = new cContpaq();
            try {
                if (!IsPostBack) {
                    Inicio();
                }

            }
            catch (Exception) {

            }
        }

        private void Inicio() {
            if (Request.QueryString["factura"] == "si") {
                //regresar.HRef = "cFinanzasRem.aspx?fechaIR=" + Request.QueryString["fechaIR"] + "&fechaFR=" + Request.QueryString["fechaFR"] + "&ordenR=" + Request.QueryString["ordenR"] + "&remR=" + Request.QueryString["remR"] + "&cteR=" + Request.QueryString["cteR"] + "&vendedorR=" + Request.QueryString["vendedorR"] + "&estatusR=" + Request.QueryString["estatusR"];
                regresar.HRef = "cFinanzasRem.aspx?fechaIF=" + Request.QueryString["fechaIF"] + "&fechaFF=" + Request.QueryString["fechaFF"] + "&ordenF=" + Request.QueryString["ordenF"] + "&remF=" + Request.QueryString["remF"] + "&cteF=" + Request.QueryString["cteF"] + "&vendedorF=" + Request.QueryString["vendedorF"] + "&estatusF=" + Request.QueryString["estatusF"];
                btnActualizar.Visible = true;
            }
            else {
                regresar.HRef = "cFinanzas.aspx?fechaIF=" + Request.QueryString["fechaIF"] + "&fechaFF=" + Request.QueryString["fechaFF"] + "&ordenF=" + Request.QueryString["ordenF"] + "&remF=" + Request.QueryString["remF"] + "&cteF=" + Request.QueryString["cteF"] + "&vendedorF=" + Request.QueryString["vendedorF"] + "&estatusF=" + Request.QueryString["estatusF"];
                btnActualizar.Visible = false;
            }
            lblFolio.Text = "Folio de la solicitud: " + cSol.obtenerFolioSolicitudByID(Request.QueryString["idSolicitud"]).ToString();
            cPag.idSolicitud = int.Parse(Request.QueryString["idSolicitud"]);
            DataTable datos = cPag.obtenerPagosSolicitud();
            DataTable resumenPago = cPag.obtenerResumenPagos();
            DataTable totalSolicitud = cPag.obtenerTotalSol();
            if (datos.Rows.Count > 0) {
                //lblSaldo.Text = "Saldo: $" + datos.Rows[0]["saldo"].ToString();
                lblSaldo.Text = "Saldo: $" + (float.Parse(datos.Rows[0]["monto"].ToString()) - float.Parse(resumenPago.Rows[0]["totalPagos"].ToString())).ToString("#,##0.00");
                //lblPago.Text = "Pagos Realizados: $" + datos.Rows[0]["pago"].ToString();
                lblPago.Text = "Pagos Realizados: $" + float.Parse(resumenPago.Rows[0]["totalPagos"].ToString()).ToString("#,##0.00");
                //lblStatus.Text = "Status :" + datos.Rows[0]["estatus"].ToString();
                float estatusAux = (float.Parse(datos.Rows[0]["monto"].ToString()) - float.Parse(resumenPago.Rows[0]["totalPagos"].ToString()));
                if (lblSaldo.Text == "Saldo: $0.00") {
                    lblStatus.Text = "Status : Pagado";
                }
                else {
                    lblStatus.Text = "Status : Pendiente";
                }
            }
            else {
                lblSaldo.Text = "Saldo: $" + (float.Parse(totalSolicitud.Rows[0]["total"].ToString()).ToString("#,##0.00"));
            }
            lvPagos.DataSource = datos;
            lvPagos.DataBind();

            /*lblVendedor.Text = "<strong>Vendedor: </strong>" + Request.QueryString["vendedor"];
            cPag.idSolicitud = int.Parse(Request.QueryString["idSolicitud"]);
            LvPagosVendedor.DataSource = cPag.obtenerPagosSolicitudVendedor();
            LvPagosVendedor.DataBind();*/

            //obtener orden
            //remisiones
            lvRemision.DataSource = od.obtenerRemisiones(int.Parse(Request.QueryString["idSolicitud"]));
            lvRemision.DataBind();
        }

        protected void lkCancelar_ItemCommand(object sender, CommandEventArgs e) {
            if (e.CommandName == "pdf") {
                string[] arr = e.CommandArgument.ToString().Split(';');
                string url = arr[1];
                if (File.Exists(Server.MapPath("/Pagos/" + arr[1]))) {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "abrirPDF", "abrirPDF('" + url + "');", true);
                }
                else {
                    pdf.path = Server.MapPath(".");
                    pdf.idSolicitud = int.Parse(Request.QueryString["idSolicitud"]);
                    pdf.idPago = int.Parse(arr[0]);
                    pdf.nombreDoc = arr[1];
                    string mensaje = pdf.generarPDF();
                    if (mensaje != "Creado") {
                        lblErrorPDF.Text = mensaje;
                        ScriptManager.RegisterStartupScript(Page, GetType(), "errorpdf", "$('#myModalPDF').modal('show');", true);
                    }
                    else {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "abrirPDF", "abrirPDF('" + url + "');", true);
                        //Response.Redirect("PagosSolicitudF.aspx?idSolicitud=" + Request.QueryString["idSolicitud"] + "&Vendedor=" + Request.QueryString["Vendedor"] + "&estatus=" + Request.QueryString["estatus"]);
                    }
                }

            }
            else {
                hdIdPago.Value = e.CommandArgument.ToString();
                ScriptManager.RegisterStartupScript(Page, GetType(), "cancelarpago", "$('#myModal').modal('show');", true);
            }
        }

        protected void lkRemision_ItemCommand(object sender, CommandEventArgs e) {
            string[] arr = e.CommandArgument.ToString().Split(';');
            string url = "/Remisiones/" + arr[1] + "/Remision" + arr[0];
            if (File.Exists(Server.MapPath(url))) {
                ScriptManager.RegisterStartupScript(Page, GetType(), "abrirPDF", "abrirPDFRem('" + url + "');", true);
            }
        }

        public string mostrarPDF(string folio, int idS) {
            string param = "";
            string url = @"C:\inetpub\wwwroot\Pepi\Dosificacion\Remisiones\" + idS + @"\Remision" + folio + ".pdf";
            if (File.Exists(url)) {
                param = "style='display:block'";
            }
            else {
                param = "style='display:none'";
            }
            return param;
        }

        public string obtenerEstatus(string eliminado) {
            string param = "";
            if (eliminado != "") {
                bool elim = bool.Parse(eliminado);
                if (elim) {
                    param = "Cancelado";
                }
            }
            else {
                param = Request.QueryString["estatus"];
            }
            return param;
        }
        protected void mbtnAceptar_Click(object sender, EventArgs e) {
            cPag.id = int.Parse(hdIdPago.Value);
            cPag.idUsuarioActivo = int.Parse(Request.Cookies["ksroc"]["id"]);
            cPag.modificarEstatusF();
            Inicio();
            //Response.Redirect("PagosSolicitudF.aspx?idSolicitud=" + Request.QueryString["idSolicitud"] + "&Vendedor=" + Request.QueryString["Vendedor"] + "&estatus=" + Request.QueryString["estatus"], false);
        }

        public string mostrarCancelar(string estatus) {
            string param = "";

            if (estatus == "Cancelado") {
                param = "style='display:none'";
            }

            return param;
        }

        public string convertir(string total) {
            string param = "";
            param = Convert.ToDecimal(total).ToString("#,##0.00");

            return param;
        }
        //actualizar contpaq - Luis Moctezuma
        cEmpleadosPercepciones per = new cEmpleadosPercepciones();

        //Extraccion de pagos - Luis Sandoval 06/01/2023
        protected void ExtraerPago(object sender, EventArgs e) {
            LogPagosContpaq log;
            contpaq = new cContpaq();
            contpaq.idSolicitud = int.Parse(Request.QueryString["idSolicitud"]);
            contpaq.cadena = contpaq.cadenaContpaq(int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
            DataTable dtRemisiones = od.obtenerRemisiones(int.Parse(Request.QueryString["idSolicitud"]));
            int pagosExtraidos = 0;
            bool bandera = false;

            if (dtRemisiones.Rows.Count > 0) {
                foreach (DataRow dataRow in dtRemisiones.Rows) {
                    string folio = dataRow["folioR"].ToString();
                    if (folio == null) {
                        lblInfo.Text = " folios disponibles";
                    }
                    else {
                        string idRemision = contpaq.idRemision(folio);
                        if (idRemision == null) {
                            lblInfo.Text = " remisiones disponibles";
                        }
                        else {
                            string idCargo = contpaq.idCargo(idRemision);
                            if (idCargo == null) {
                                lblInfo.Text = " facturas disponibles";
                            }
                            else {
                                DataTable pagos = contpaq.pagoContpaq(idCargo);
                                if (pagos == null ) {
                                    lblInfo.Text = " pagos disponibles";
                                }
                                else {
                                    //Monto a pagar obtenido del primer registro del datatable
                                    double montoAPagar = double.Parse(cSol.obtenerMontoSolicitud(int.Parse(Request.QueryString["idSolicitud"])));
                                    lblInfo.Text = "Pagos encontrados: ";
                                    //Recorrido de pagos
                                    foreach (DataRow row in pagos.Rows) {
                                        string idPagoContpaq = row["CIDDOCUMENTO"].ToString();

                                        //Descartar que el id sea el id del cargo
                                        if (idPagoContpaq != idCargo) {
                                            log = new LogPagosContpaq {
                                                idPagoContpaq = int.Parse(idPagoContpaq),
                                                idCargoContpaq = int.Parse(idCargo)
                                            };
                                            //Verificar que no exista en pagosLog
                                            if (!log.exists()) {
                                                //Insertar pago en el log
                                                log.idUsuario = int.Parse(Request.Cookies["ksroc"]["id"]);
                                                log.monto = double.Parse(row["CIMPORTEABONO"].ToString());
                                                log.metodoPago = row["CMETODOPAG"].ToString();

                                                //Insertar en pagos finanzas
                                                cPagos cpago = new cPagos();
                                                cpago.folio = int.Parse(row["CFOLIO"].ToString());
                                                cpago.monto = (float)montoAPagar;
                                                //Buscar saldo pendiente
                                                string saldoPendiente = cpago.obtenerSaldoFinanzas(int.Parse(Request.QueryString["idSolicitud"]));
                                                cpago.saldo = float.Parse(saldoPendiente) - float.Parse(row["CIMPORTEABONO"].ToString());
                                                cpago.pago = float.Parse(row["CIMPORTEABONO"].ToString());
                                                cpago.estatus = "Pagado";
                                                cpago.idUsuarioActivo = log.idUsuario;
                                                cpago.metodoPago = row["CMETODOPAG"].ToString();
                                                cpago.formaPago = "";
                                                cpago.observaciones = "Extraído de Contpaq";
                                                //bancos pendiente
                                                cpago.idBP = 0;
                                                cpago.idBR = 0;
                                                //Crear PDF
                                                cPDFpago pDFpago = new cPDFpago();

                                                pDFpago.idSolicitud = int.Parse(Request.QueryString["idSolicitud"]);
                                                pDFpago.folio = row["CFOLIO"].ToString();
                                                pDFpago.path = Server.MapPath(".");
                                                pDFpago.nombreDoc = "pago" + pDFpago.folio + "_" + pDFpago.idSolicitud + ".pdf";

                                                cpago.pdf = pDFpago.nombreDoc;
                                                int idPago = cpago.insertarPagosFinanzasAlgoritmo(int.Parse(Request.QueryString["idSolicitud"]), log.idPagoContpaq, int.Parse(Request.Cookies["ksroc"]["idSucursal"]));

                                                pDFpago.idPago = idPago;
                                                pDFpago.generarPDF();

                                                log.idPagoFinanzas = idPago;
                                                log.insertar();

                                                pagosExtraidos++;
                                                bandera = true;
                                            }
                                            
                                        }
                                    }
                                    if (bandera) {
                                        lblInfo.Text = "Pagos encontrados: " + pagosExtraidos;
                                    }
                                    else {
                                        lblInfo.Text = " pagos sin registrar en el sistema";
                                    }
                                    
                                }
                            }
                        }
                    }
                }
            }
            else {
                lblInfo.Text = " folios disponibles";
            }
            if (pagosExtraidos > 0) {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Contpaq", "$('#myModalContpaq').modal('show');", true);
                LabelTerminaContpaq.Text = "Pagos encontrados: " + pagosExtraidos;
                Inicio();
            }
            else {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Contpaq", "$('#myModalContpaq').modal('show');", true);
                LabelTerminaContpaq.Text = "No se encontraron" + lblInfo.Text;
                Inicio();
            }


        }

        protected void btnActualizar_Click(object sender, EventArgs e) {
            //Obtenemos las remisiones de la solicitud
            contpaq.idSolicitud = int.Parse(Request.QueryString["idSolicitud"]);
            DataTable dtRemisiones = od.obtenerRemisiones(int.Parse(Request.QueryString["idSolicitud"]));
            if (dtRemisiones.Rows.Count > 0) {
                for (int i = 0; i < dtRemisiones.Rows.Count; i++) {
                    //asignamos el folio a buscar
                    contpaq.folioAux = dtRemisiones.Rows[i]["folioR"].ToString();
                    int anomenos = int.Parse(DateTime.Now.Year.ToString());
                    anomenos = anomenos - 1;
                    DataTable datosadmdocumentos = new DataTable();
                    DataTable datosbanco = new DataTable();
                    DataTable datosingreso = new DataTable();
                    DataTable TotalPagado = new DataTable();
                    DataTable dtPagosFolio = new DataTable();
                    int sucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"].ToString());
                    if (sucursal == 1) {
                        dtPagosFolio = contpaq.obtenerDatosPorFolioSaltilloConcretos();

                        datosadmdocumentos = per.ObtenerDatosSaltilloConcretosRemisiones("select CFOLIO, CTOTAL, CPENDIENTE, CRAZONSOCIAL from admDocumentos where COBSERVACIONES like '%" + contpaq.folioAux + "%' and(DATEPART(yy, cfecha) = " + DateTime.Now.Year + " or DATEPART(yy, cfecha) = " + anomenos + ") ");

                    }
                    else if (sucursal == 2) {
                        dtPagosFolio = contpaq.ObtenerDatosPorFolioIrapuatoConcreto("SELECT CIDDOCUMENTO, CTOTAL, CFECHA, COBSERVACIONES, CFOLIO, CPENDIENTE FROM dbo.admDocumentos WHERE (CFOLIO = " + contpaq.folioAux + " AND CIDDOCUMENTODE = 3)");

                        datosadmdocumentos = per.ObtenerDatosIrapuatoConcretosRemisiones("select CFOLIO, CTOTAL, CPENDIENTE, CRAZONSOCIAL from admDocumentos where COBSERVACIONES like '%" + contpaq.folioAux + "%' and(DATEPART(yy, cfecha) = " + DateTime.Now.Year + " or DATEPART(yy, cfecha) = " + anomenos + ") ");
                    }
                    else if (sucursal == 3) {
                        dtPagosFolio = contpaq.ObtenerDatosPorFolioSaltilloBlock("SELECT CIDDOCUMENTO, CTOTAL, CFECHA, COBSERVACIONES, CFOLIO, CPENDIENTE FROM dbo.admDocumentos WHERE (CFOLIO = " + contpaq.folioAux + " AND CIDDOCUMENTODE = 3)");

                        datosadmdocumentos = per.ObtenerDatosSaltilloBlockRemisiones("select CFOLIO, CTOTAL, CPENDIENTE, CRAZONSOCIAL from admDocumentos where COBSERVACIONES like '%" + contpaq.folioAux + "%' and(DATEPART(yy, cfecha) = " + DateTime.Now.Year + " or DATEPART(yy, cfecha) = " + anomenos + ") ");

                    }
                    else if (sucursal == 1006) {
                        dtPagosFolio = contpaq.ObtenerDatosPorFolioIrapuatoBlock("SELECT CIDDOCUMENTO, CTOTAL, CFECHA, COBSERVACIONES, CFOLIO, CPENDIENTE FROM dbo.admDocumentos WHERE (CFOLIO = " + contpaq.folioAux + " AND CIDDOCUMENTODE = 3)");

                        datosadmdocumentos = per.ObtenerDatosIrapuatoBlockRemisiones("select CFOLIO, CTOTAL, CPENDIENTE, CRAZONSOCIAL from admDocumentos where COBSERVACIONES like '%" + contpaq.folioAux + "%' and(DATEPART(yy, cfecha) = " + DateTime.Now.Year + " or DATEPART(yy, cfecha) = " + anomenos + ") ");
                    }
                    if (datosadmdocumentos.Rows.Count == 0) {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Contpaq", "$('#myModalContpaq').modal('show');", true);
                        LabelTerminaContpaq.Text = "No se encontraron Remisiones.";
                        Inicio();
                        return;
                    }
                    datosbanco = per.ObtenerDatosSaltilloConcretoBanco("select ImportePago, IdIngreso from AsocCargosAbonos where (ClienteProveedor LIKE N'%" + datosadmdocumentos.Rows[0]["CRAZONSOCIAL"].ToString() + "%')");
                    foreach (DataRow bancos in datosbanco.Rows) {
                        if (datosbanco.Rows.Count > 0) {
                            datosingreso = per.ObtenerDatosSaltilloConcretoBanco("select Concepto from Ingresos where Id = " + bancos["IdIngreso"].ToString() + "");
                        }
                        else {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Contpaq", "$('#myModalContpaq').modal('show');", true);
                            LabelTerminaContpaq.Text = "Cliente no encontrado.";
                            Inicio();
                            return;
                        }
                        string[] facturas_a = datosingreso.Rows[0]["Concepto"].ToString().Split(' ');
                        //Verificamos si encontramos datos en contpaq para esta remisión
                        if (dtPagosFolio.Rows.Count > 0) {
                            /*Aqui llenamos los pagos con la consulta que se hizo*/
                            for (int j = 0; j < dtPagosFolio.Rows.Count; j++) {
                                if (datosadmdocumentos.Rows.Count > 0) {
                                    contpaq.folio = dtPagosFolio.Rows[j]["CFOLIO"].ToString();
                                    bool bandera = false;
                                    for (int f = 0; f < facturas_a.Length; f++) {
                                        int n;
                                        bool esnummero = int.TryParse(facturas_a[f], out n);
                                        if (esnummero) {
                                            if (int.Parse(facturas_a[f].ToString()) == int.Parse(datosadmdocumentos.Rows[0]["CFOLIO"].ToString())) {
                                                contpaq.pago = datosbanco.Rows[0]["importepago"].ToString();
                                                bandera = true;
                                                break;
                                            }
                                            else {
                                                contpaq.pago = "0";
                                            }
                                        }
                                    }
                                    if (bandera == false) {
                                        break;
                                    }


                                    contpaq.fecha = DateTime.Parse(dtPagosFolio.Rows[j]["CFECHA"].ToString());
                                    contpaq.observaciones = dtPagosFolio.Rows[j]["COBSERVACIONES"].ToString();
                                    contpaq.idContpaq = int.Parse(dtPagosFolio.Rows[j]["CIDDOCUMENTO"].ToString());
                                }
                                else {
                                    ScriptManager.RegisterStartupScript(Page, GetType(), "Contpaq", "$('#myModalContpaq').modal('show');", true);
                                    LabelTerminaContpaq.Text = "Factura sin folio en observacion";
                                    contpaq.folio = dtPagosFolio.Rows[j]["CFOLIO"].ToString();
                                    contpaq.pago = Math.Abs(decimal.Parse(dtPagosFolio.Rows[j]["CPENDIENTE"].ToString()) - decimal.Parse(dtPagosFolio.Rows[j]["CTOTAL"].ToString())).ToString(); ;
                                    contpaq.fecha = DateTime.Parse(dtPagosFolio.Rows[j]["CFECHA"].ToString());
                                    contpaq.observaciones = dtPagosFolio.Rows[j]["COBSERVACIONES"].ToString();
                                    contpaq.idContpaq = int.Parse(dtPagosFolio.Rows[j]["CIDDOCUMENTO"].ToString());
                                }

                                //validar que no exista el pago que queremos insertar
                                if (contpaq.existePago() == false) {
                                    contpaq.folioPago = int.Parse(contpaq.obtenerUltimoFolio()) + 1;
                                    DataTable dt = contpaq.ObtenerMovimientos(contpaq.idContpaq.ToString());
                                    if (dt.Rows.Count == 0) {
                                        ScriptManager.RegisterStartupScript(Page, GetType(), "Contpaq", "$('#myModalContpaq').modal('show');", true);
                                        LabelTerminaContpaq.Text = "No se encontraron Movmientos para estas remisiones.";
                                        Inicio();
                                        break;
                                    }
                                    //  string [] Remisiones
                                    if (datosadmdocumentos.Rows.Count > 0) {
                                        contpaq.Monto = dt.Rows[0]["CTOTAL"].ToString();
                                        contpaq.Saldo = decimal.Parse(datosadmdocumentos.Rows[0]["CPENDIENTE"].ToString());
                                        lblSaldo.Text = "Saldo: " + contpaq.Saldo.ToString();
                                        lblPago.Text = "Pagos Realizados: " + contpaq.pago;

                                        if (contpaq.Saldo == 0) {
                                            contpaq.Status = "Pagado";
                                        }
                                        else {
                                            contpaq.Status = "Pendiente";

                                        }
                                        lblStatus.Text = contpaq.Status;
                                    }
                                    else {
                                        contpaq.Monto = dt.Rows[0]["CTOTAL"].ToString();
                                        contpaq.Saldo = decimal.Parse(dtPagosFolio.Rows[j]["CPENDIENTE"].ToString());
                                        contpaq.Status = "Pendiente";

                                    }

                                    contpaq.insertarPagos();
                                    ScriptManager.RegisterStartupScript(Page, GetType(), "Contpaq", "$('#myModalContpaq').modal('show');", true);
                                    LabelTerminaContpaq.Text = "Los Pagos han sido extraídos con éxito";
                                    Inicio();
                                    return;
                                }

                            }
                            if (i == dtRemisiones.Rows.Count - 1) {
                                ScriptManager.RegisterStartupScript(Page, GetType(), "Contpaq", "$('#myModalContpaq').modal('show');", true);
                                LabelTerminaContpaq.Text = "Los Pagos han sido extraídos con éxito";
                                Inicio();
                                return;
                            }
                        }
                        //Si no encontramos datos mandamos una alerta
                        else {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Contpaq", "$('#myModalContpaq').modal('show');", true);
                            LabelTerminaContpaq.Text = "No se encontraron Pagos en Contpaq para esta remisión";
                            Inicio();
                            return;
                        }
                    }






                    //string[] facturas_ = datosingreso.Rows[0]["Concepto"].ToString().Split(' ');
                    ////Verificamos si encontramos datos en contpaq para esta remisión
                    //if (dtPagosFolio.Rows.Count > 0)
                    //{
                    //    /*Aqui llenamos los pagos con la consulta que se hizo*/
                    //    for (int j = 0; j < dtPagosFolio.Rows.Count; j++)
                    //    {
                    //        if (datosadmdocumentos.Rows.Count > 0)
                    //        {
                    //            contpaq.folio = dtPagosFolio.Rows[j]["CFOLIO"].ToString();
                    //            for (int f = 0; f < facturas_.Length; f++)
                    //            {
                    //                int n;
                    //                bool esnummero = int.TryParse(facturas_[f], out n);
                    //                if (esnummero)
                    //                {
                    //                    if (int.Parse(facturas_[f].ToString()) == int.Parse(datosadmdocumentos.Rows[0]["CFOLIO"].ToString()))
                    //                    {
                    //                        contpaq.pago = datosbanco.Rows[0]["importepago"].ToString();
                    //                        break;
                    //                    }
                    //                    else
                    //                    {
                    //                        contpaq.pago = "0";
                    //                    }
                    //                }
                    //            }


                    //            contpaq.fecha = DateTime.Parse(dtPagosFolio.Rows[j]["CFECHA"].ToString());
                    //            contpaq.observaciones = dtPagosFolio.Rows[j]["COBSERVACIONES"].ToString();
                    //            contpaq.idContpaq = int.Parse(dtPagosFolio.Rows[j]["CIDDOCUMENTO"].ToString());
                    //        }
                    //        else
                    //        {
                    //            ScriptManager.RegisterStartupScript(Page, GetType(), "Contpaq", "$('#myModalContpaq').modal('show');", true);
                    //            LabelTerminaContpaq.Text = "Factura sin folio en observacion";
                    //            contpaq.folio = dtPagosFolio.Rows[j]["CFOLIO"].ToString();
                    //            contpaq.pago = Math.Abs(decimal.Parse(dtPagosFolio.Rows[j]["CPENDIENTE"].ToString()) - decimal.Parse(dtPagosFolio.Rows[j]["CTOTAL"].ToString())).ToString(); ;
                    //            contpaq.fecha = DateTime.Parse(dtPagosFolio.Rows[j]["CFECHA"].ToString());
                    //            contpaq.observaciones = dtPagosFolio.Rows[j]["COBSERVACIONES"].ToString();
                    //            contpaq.idContpaq = int.Parse(dtPagosFolio.Rows[j]["CIDDOCUMENTO"].ToString());
                    //        }

                    //        //validar que no exista el pago que queremos insertar
                    //        if (contpaq.existePago() == false)
                    //        {
                    //            contpaq.folioPago = int.Parse(contpaq.obtenerUltimoFolio()) + 1;
                    //            DataTable dt = contpaq.ObtenerMovimientos(contpaq.idContpaq.ToString());
                    //            if (dt.Rows.Count == 0)
                    //            {
                    //                ScriptManager.RegisterStartupScript(Page, GetType(), "Contpaq", "$('#myModalContpaq').modal('show');", true);
                    //                LabelTerminaContpaq.Text = "No se encontraron Movmientos para estas remisiones.";
                    //                Inicio();
                    //                break;
                    //            }
                    //            //  string [] Remisiones
                    //            if (datosadmdocumentos.Rows.Count > 0)
                    //            {
                    //                contpaq.Monto = dt.Rows[0]["CTOTAL"].ToString();
                    //                contpaq.Saldo = decimal.Parse(datosadmdocumentos.Rows[0]["CPENDIENTE"].ToString());
                    //                lblSaldo.Text = "Saldo: " + contpaq.Saldo.ToString();
                    //                lblPago.Text = "Pagos Realizados: " + contpaq.pago;

                    //                if (contpaq.Saldo == 0)
                    //                {
                    //                    contpaq.Status = "Pagado";
                    //                }
                    //                else
                    //                {
                    //                    contpaq.Status = "Pendiente";

                    //                }
                    //                lblStatus.Text = contpaq.Status;
                    //            }
                    //            else
                    //            {
                    //                contpaq.Monto = dt.Rows[0]["CTOTAL"].ToString();
                    //                contpaq.Saldo = decimal.Parse(dtPagosFolio.Rows[j]["CPENDIENTE"].ToString());
                    //                contpaq.Status = "Pendiente";

                    //            }

                    //            contpaq.insertarPagos();
                    //        }

                    //    }
                    //    if (i == dtRemisiones.Rows.Count - 1)
                    //    {
                    //        ScriptManager.RegisterStartupScript(Page, GetType(), "Contpaq", "$('#myModalContpaq').modal('show');", true);
                    //        LabelTerminaContpaq.Text = "Los Pagos han sido extraídos con éxito";
                    //        Inicio();
                    //    }
                    //}
                    ////Si no encontramos datos mandamos una alerta
                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(Page, GetType(), "Contpaq", "$('#myModalContpaq').modal('show');", true);
                    //    LabelTerminaContpaq.Text = "No se encontraron Pagos en Contpaq para esta remisión";
                    //    Inicio();
                    //}
                }

            }
        }
    }
}