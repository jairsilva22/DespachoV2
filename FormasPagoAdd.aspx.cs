using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class FormasPagoAdd : System.Web.UI.Page
    {
        cFormasPagoSAT cFPS = new cFormasPagoSAT();
        cUtilidades cUtl = new cUtilidades();
        protected void Page_Load(object sender, EventArgs e)
        {
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
        public void limpiar()
        {
            txtCodigo.Text = "";
            txtDescripcion.Text = "";
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {


                cFPS.codigo = int.Parse(txtCodigo.Text);
                cFPS.descripcion = txtDescripcion.Text;

                if (cFPS.existe(cFPS.codigo))
                {
                    cFPS.actualizarEliminacion();
                    lblError.Text = "Ya existia una forma de pago con ese nombre se acaba de agregar nuevamente al sistema verifica la informacion";
                    limpiar();
                }
                else
                {
                    cFPS.insertar();
                    Response.Redirect("formaspago.aspx");
                }
            }catch (Exception ex) {

                if (ex.Message == "ok")
                {

                    //script que te abre el modal 
                    string errorMessage = "Metodo agregado correctamente ";
                    ScriptManager.RegisterStartupScript(this, GetType(), "showErrorModal", $"$('#errorMessage').text('{errorMessage}'); $('#errorModal').modal('show');", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "redirectScript", "setTimeout(function() { window.location.href = 'FormasPago.aspx'; }, 2000);", true);

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
            Response.Redirect("formaspago.aspx");
        }
    }
}
