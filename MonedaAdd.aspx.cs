using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class MonedaAdd : System.Web.UI.Page
    {
        cMoneda cMd = new cMoneda();
        cSucursales cSuc = new cSucursales();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    lblSuc.Text = cSuc.obtenerNombreSucursalByID(int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
                }
            }
            catch (Exception)
            {

            }
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                cMd.cMon = txtClave.Text;
                cMd.descripcion = txtDescripcion.Text;
                cMd.tCambio = double.Parse(txtCambio.Text);
                cMd.idSuc = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);

                if (cMd.existe(cMd.descripcion, cMd.cMon, cMd.idSuc))
                {
                    lblError.Text = "Ya existe la moneda, favor de revisar si es otro tipo de moneda";
                }
                else
                {
                    cMd.insertar();
                    Response.Redirect("monedas.aspx");
                }
            }
            catch (Exception ex){
                if (ex.Message == "ok")
                {

                    //script que te abre el modal 
                    string errorMessage = "Producto agregado correctamente ";
                    ScriptManager.RegisterStartupScript(this, GetType(), "showErrorModal", $"$('#errorMessage').text('{errorMessage}'); $('#errorModal').modal('show');", true);


                }
                else
                {
                    string errorMessage = "Verifica que la informacón ingresada sea correcta  Ejemplo: Codigo: MX.  Moneda : Pesos. Cambio : 1  " ;
                    ScriptManager.RegisterStartupScript(this, GetType(), "showErrorModal", $"$('#errorMessage').text('{errorMessage}'); $('#errorModal').modal('show');", true);
                }

            }

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("monedas.aspx");
        }
    }
}