using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace despacho
{
    public partial class percepcionesTipoMod : System.Web.UI.Page
    {
        cUtilidades cUtl = new cUtilidades();
        cTipoPercepcion cTipoP = new cTipoPercepcion();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                cUtl.idUsuarioActivo = int.Parse(Request.Cookies["ksroc"]["id"]);
                if (!IsPostBack)
                {
                    cargarTipoProducto();
                }
            }
            catch (Exception)
            {

            }
        }
        private void cargarTipoProducto()
        {
            DataTable dt = cTipoP.obtenerTipoByID(int.Parse(Request.QueryString["id"]));
            txtTipo.Text = dt.Rows[0]["percepcion"].ToString();
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtTipo.Text.Equals(""))
            {
                lblError.Text = "Favor de ingresar el tipo de Percepción";
                return;
            }
            cTipoP.percepcion = txtTipo.Text;

            cTipoP.actualizar(int.Parse(Request.QueryString["id"]), int.Parse(Request.Cookies["ksroc"]["id"]));
            Response.Redirect("percepcionesTipo.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("percepcionesTipo.aspx");
        }
    }
}
