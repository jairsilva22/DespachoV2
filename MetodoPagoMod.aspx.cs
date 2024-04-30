using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class MetodoPagoMod : System.Web.UI.Page
    {
        MetodoPagos metodo;
        cUtilidades cUtl = new cUtilidades();

        protected void Page_Load(object sender, EventArgs e)
        {
            metodo = new MetodoPagos();


            try
            {
                cUtl.idUsuarioActivo = int.Parse(Request.Cookies["ksroc"]["id"]);
                if (!IsPostBack)
                {
                    metodo.idPago = int.Parse(Request.QueryString["id"]);
                    metodo.obtenerMetodo();

                    txtClave.Text = metodo.forma_pago;
                    txtDescripcion.Text = metodo.descripcion;

                }
            }
            catch (Exception)
            {
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            metodo.forma_pago = txtClave.Text;
            metodo.descripcion = txtDescripcion.Text;
            metodo.idPago = int.Parse(Request.QueryString["id"]);
            //if (metodo.existeMetodo())
            //{
            //    lblError.Text = "El Método de Pago ya existe.";
            //}
            //else
            //{
                metodo.idUsuario = int.Parse(Request.Cookies["ksroc"]["id"]);
                metodo.modificar();

                Response.Redirect("MetodoPago.aspx");
            //}
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("MetodoPago.aspx");
        }
    }
}