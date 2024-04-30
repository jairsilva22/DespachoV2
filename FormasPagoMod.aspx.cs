using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class FormasPagoMod : System.Web.UI.Page
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
                    cargarFPS();
                }
            }
            catch (Exception)
            {

            }
        }
        private void cargarFPS()
        {
            DataTable dt = cFPS.obtenerFormasPagoByID(int.Parse(Request.QueryString["id"]));
            txtCodigo.Text = dt.Rows[0]["codigo"].ToString();
            txtDescripcion.Text = dt.Rows[0]["descripcion"].ToString();
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            cFPS.codigo = int.Parse(txtCodigo.Text);
            cFPS.descripcion = txtDescripcion.Text;

            cFPS.actualizar(int.Parse(Request.QueryString["id"]));
            Response.Redirect("formaspago.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("formaspago.aspx");
        }
    }
}