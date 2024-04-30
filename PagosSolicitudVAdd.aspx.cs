using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class PagosSolicitudVAdd : System.Web.UI.Page
    {
        cPagos cPag = new cPagos();
        cSolicitudes cSol = new cSolicitudes();
        Folio cFol = new Folio();
        cFormasPagoSAT cFP = new cFormasPagoSAT();
        MetodoPagos metodo = new MetodoPagos();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    string monto = cSol.obtenerMontoSolicitud(int.Parse(Request.QueryString["idSolicitud"]));
                    string saldo;
                    if (cPag.consultaSaldoVendedor(int.Parse(Request.QueryString["idSolicitud"])))
                    {
                        saldo = cPag.obtenerSaldoVendedor(int.Parse(Request.QueryString["idSolicitud"]));
                    }
                    else
                    {
                        saldo = monto;
                    }

                    txtMonto.Text = Convert.ToDecimal(monto).ToString("#,##0.00");
                    txtSaldo.Text = Convert.ToDecimal(saldo).ToString("#,##0.00");
                    lblFolio.Text = cFol.obtenerFolioPagoVendedor(int.Parse(Request.Cookies["ksroc"]["idSucursal"]));

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
                }
            }
            catch (Exception)
            {

            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if(float.Parse(txtPago.Text) > 0)
            {
                if (float.Parse(txtSaldo.Text) > 0)
                {
                    if (float.Parse(txtPago.Text) <= float.Parse(txtSaldo.Text))
                    {
                        float nuevoSaldo;
                        if (cPag.consultaSaldoVendedor(int.Parse(Request.QueryString["idSolicitud"])))
                        {
                            nuevoSaldo = float.Parse(cPag.obtenerSaldoVendedor(int.Parse(Request.QueryString["idSolicitud"]))) - float.Parse(txtPago.Text);
                        }
                        else
                        {
                            nuevoSaldo = float.Parse(cSol.obtenerMontoSolicitud(int.Parse(Request.QueryString["idSolicitud"]))) - float.Parse(txtPago.Text);
                        }

                        cPag.monto = float.Parse(cSol.obtenerMontoSolicitud(int.Parse(Request.QueryString["idSolicitud"])));
                        cPag.saldo = nuevoSaldo;
                        cPag.pago = float.Parse(txtPago.Text);
                        cPag.idUsuarioActivo = int.Parse(Request.Cookies["ksroc"]["id"]);
                        cPag.folio = int.Parse(lblFolio.Text);
                        cPag.estatus = "Pagado";
                        cPag.metodoPago = ddlMP.SelectedValue;
                        cPag.formaPago = ddlFP.SelectedValue;

                        cPag.insertarPagosVendedor(int.Parse(Request.QueryString["idSolicitud"]));
                        cFol.actualizarFolioPagosVendedor(int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
                        Response.Redirect("pagosSolicitudV.aspx?idSolicitud=" + Request.QueryString["idSolicitud"] + "&Vendedor=" + Request.QueryString["Vendedor"]);
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
            Response.Redirect("pagosSolicitudV.aspx?idSolicitud=" + Request.QueryString["idSolicitud"] + "&Vendedor=" + Request.QueryString["Vendedor"]);
        }


    }
}