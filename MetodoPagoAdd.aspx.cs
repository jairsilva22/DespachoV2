using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class MetodoPagoAdd : System.Web.UI.Page
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

                }
            }
            catch (Exception)
            {

            }
        }

        public void borrarDatos()
        {
           txtClave.Text= "";
           txtDescripcion.Text = "";
        }
       
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {

                metodo.forma_pago = txtClave.Text;
                metodo.descripcion = txtDescripcion.Text;

                if (metodo.existeMetodo())
                {
                    metodo.actualizarEliminacion();
                    lblError.Text = "El Método de Pago y existia en el sistema pero fue eliminado se acaba de agregar nuevamente verifica el codigo";

                }
                else
                {
                    metodo.idUsuario = int.Parse(Request.Cookies["ksroc"]["id"]);
                    metodo.insertar();

                    Response.Redirect("MetodoPago.aspx");
                }
            }
            catch (Exception ex){

                if (ex.Message == "ok")
                {

                    //script que te abre el modal 
                    string errorMessage = "Metodo agregado correctamente ";
                    ScriptManager.RegisterStartupScript(this, GetType(), "showErrorModal", $"$('#errorMessage').text('{errorMessage}'); $('#errorModal').modal('show');", true);


                }
                else
                {
                    string errorMessage = "Verifica que la informacón ingresada sea correcta recuerda que el codigo solo acepta numeros  ";
                    ScriptManager.RegisterStartupScript(this, GetType(), "showErrorModal", $"$('#errorMessage').text('{errorMessage}'); $('#errorModal').modal('show');", true);
                }
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("MetodoPago.aspx");
        }
    }
}