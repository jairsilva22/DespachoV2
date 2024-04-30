using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class pagosSolicitudFAdd : System.Web.UI.Page
    {
        cPagos cPag = new cPagos();
        cSolicitudes cSol = new cSolicitudes();
        Folio cFol = new Folio();
        cFormasPagoSAT cFP = new cFormasPagoSAT();
        MetodoPagos metodo = new MetodoPagos();
        cPDFpago pdf = new cPDFpago();
        cBancos bco = new cBancos();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    txtPago.Focus();
                    pago.Visible = false;
                    recibo.Visible = false;
                    string monto = cSol.obtenerMontoSolicitud(int.Parse(Request.QueryString["idSolicitud"]));
                    string saldo;
                    if (cPag.consultaSaldoFinanzas(int.Parse(Request.QueryString["idSolicitud"])))
                    {
                        saldo = cPag.obtenerSaldoFinanzas(int.Parse(Request.QueryString["idSolicitud"]));
                    }
                    else
                    {
                        saldo = monto;
                    }
                    txtPago.Text = Convert.ToDecimal(saldo).ToString("#,##0.00");
                    txtMonto.Text = Convert.ToDecimal(monto).ToString("#,##0.00");
                    txtSaldo.Text = Convert.ToDecimal(saldo).ToString("#,##0.00");
                    lblFolio.Text = cFol.obtenerFolioPagoFinanzas(int.Parse(Request.Cookies["ksroc"]["idSucursal"]));

                    ddlFP.DataSource = cFP.obtenerFormasPagoSAT();
                    ddlFP.DataValueField = "codigo";
                    ddlFP.DataTextField = "descripcion";
                    ddlFP.DataBind();
                    //ddlFP.Items.Insert(0, new ListItem("Seleccionar", ""));

                    ddlMP.DataSource = metodo.metodos();
                    ddlMP.DataTextField = "descripcion";
                    ddlMP.DataValueField = "idpago";
                    ddlMP.DataBind();
                    //ddlMP.Items.Insert(0, new ListItem("Seleccionar", ""));

                    ddlBancoP.DataSource = bco.obtenerBancos();
                    ddlBancoP.DataValueField = "idbanco";
                    ddlBancoP.DataTextField = "nombrebanco";
                    ddlBancoP.DataBind();
                    ddlBancoP.Items.Insert(0, new ListItem("Seleccionar", "0"));

                    ddlBancoR.DataSource = bco.obtenerBancosR();
                    ddlBancoR.DataValueField = "idbanco";
                    ddlBancoR.DataTextField = "nombrebanco";
                    ddlBancoR.DataBind();
                    ddlBancoR.Items.Insert(0, new ListItem("Seleccionar", "0"));


                }
            }
            catch (Exception)
            {

            }

        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            float totalSolicitud = float.Parse(cSol.obtenerMontoSolicitud(int.Parse(Request.QueryString["idSolicitud"])));
            bool banExistePago = cSol.existePagoSolicitud(int.Parse(Request.QueryString["idSolicitud"]));
            //if (float.Parse(txtPago.Text) > 0)
            if ((float.Parse(txtPago.Text) > 0) || (float.Parse(txtPago.Text) == 0 && totalSolicitud == 0))
            {
                //if (float.Parse(txtSaldo.Text) > 0)
                if ((float.Parse(txtSaldo.Text) > 0) || ((float.Parse(txtSaldo.Text) == 0) && totalSolicitud == 0 && banExistePago == false))
                {
                    if (float.Parse(txtPago.Text) <= float.Parse(txtSaldo.Text))
                    {
                        int continuar = 0;
                        if (ddlFP.SelectedValue != "01")
                        {
                            if(ddlBancoP.SelectedValue == "0" || ddlBancoR.SelectedValue == "0")
                            {
                                continuar = 0;
                                lblError.Text = "Favor de Seleccionar el Banco de Pago y/o el Banco de Recibo";
                            }
                            else
                            {
                                continuar = 1;
                            }
                        }
                        else
                        {
                            continuar = 1;
                        }

                        if(continuar == 1)
                        {
                            float nuevoSaldo;
                            if (cPag.consultaSaldoFinanzas(int.Parse(Request.QueryString["idSolicitud"])))
                            {
                                nuevoSaldo = float.Parse(cPag.obtenerSaldoFinanzas(int.Parse(Request.QueryString["idSolicitud"]))) - float.Parse(txtPago.Text);
                            }
                            else
                            {
                                nuevoSaldo = float.Parse(cSol.obtenerMontoSolicitud(int.Parse(Request.QueryString["idSolicitud"]))) - float.Parse(txtPago.Text);
                            }
                            cPag.idBP = int.Parse(ddlBancoP.SelectedValue);
                            cPag.idBR = int.Parse(ddlBancoR.SelectedValue);
                            cPag.monto = float.Parse(cSol.obtenerMontoSolicitud(int.Parse(Request.QueryString["idSolicitud"])));
                            cPag.saldo = nuevoSaldo;
                            cPag.pago = float.Parse(txtPago.Text);
                            cPag.idUsuarioActivo = int.Parse(Request.Cookies["ksroc"]["id"]);
                            cPag.folio = int.Parse(lblFolio.Text);
                            cPag.estatus = "Pagado";
                            cPag.metodoPago = ddlMP.SelectedValue;
                            cPag.formaPago = ddlFP.SelectedValue;
                            cPag.observaciones = txtObservaciones.Text;
                            //creamos PDF 
                            pdf.idSolicitud = int.Parse(Request.QueryString["idSolicitud"]);
                            pdf.path = Server.MapPath(".");
                            pdf.nombreDoc = "pago" + lblFolio.Text + "_" + Request.QueryString["idSolicitud"] + ".pdf";

                            cPag.pdf = pdf.nombreDoc;
                            cPag.insertarPagosFinanzas(int.Parse(Request.QueryString["idSolicitud"]));
                            cPag.idSolicitud = int.Parse(Request.QueryString["idSolicitud"]);
                            cPag.obtenerUltimoID();
                            pdf.idPago = cPag.id;
                            cFol.actualizarFolioPagosFinanzas(int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
                            string mensaje = pdf.generarPDF();
                            if(mensaje == "Creado")
                            {
                                Response.Redirect("pagosSolicitudF.aspx?idSolicitud=" + Request.QueryString["idSolicitud"] + "&Vendedor=" + Request.QueryString["Vendedor"]+ "&estatus="+ Request.QueryString["estatus"]);
                            }
                            else
                            {
                                lblErrorPDF.Text = mensaje;
                                ScriptManager.RegisterStartupScript(Page, GetType(), "errorpdf", "$('#myModalPDF').modal('show');", true);
                            }
                            
                        }
                    }
                    else
                    {
                        lblError.Text = "No puede realizar un pago mayor al saldo";
                    }
                }
                else
                {
                    lblError.Text = "La Solicitud ha sido pagada.";
                }
            }
            else
            {
                lblError.Text = "El Pago debe ser mayor que cero";
            }

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("pagosSolicitudF.aspx?idSolicitud=" + Request.QueryString["idSolicitud"] + "&Vendedor=" + Request.QueryString["Vendedor"] + "&estatus=" + Request.QueryString["estatus"]);
        }

        protected void ddlFP_SelectedIndexChanged(object sender, EventArgs e)
        {
            //buscamos forma pago efectivo 
            if(ddlFP.SelectedValue != "")
            {
                if (int.Parse(ddlFP.SelectedValue) != cFP.existeCod("01"))
                {
                    pago.Visible = true;
                    recibo.Visible = true;
                    ddlFP.Focus();
                    upForm.Update();
                    
                }
                else
                {
                    pago.Visible = false;
                    recibo.Visible = false;
                    ddlFP.Focus();
                    upForm.Update();
                }
            }
            else
            {
                pago.Visible = false;
                recibo.Visible = false;
                ddlFP.Focus();
                upForm.Update();
            }
        }
    }
}