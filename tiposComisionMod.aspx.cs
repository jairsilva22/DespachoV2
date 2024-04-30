using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace despacho
{
    public partial class tiposComisionMod : System.Web.UI.Page
    {
        cUtilidades cUtl = new cUtilidades();
        cTipoComision cTipoC = new cTipoComision();
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
            DataTable dt = cTipoC.obtenerTipoByID(int.Parse(Request.QueryString["id"]));
            txtTipo.Text = dt.Rows[0]["percepcion"].ToString();
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtTipo.Text.Equals(""))
            {
                lblError.Text = "Favor de ingresar el tipo de Percepción";
                return;
            }
            cTipoC.comision = txtTipo.Text;

            cTipoC.actualizar(int.Parse(Request.QueryString["id"]), int.Parse(Request.Cookies["ksroc"]["id"]));
            Response.Redirect("tiposComision.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("tiposComision.aspx");
        }
    }
}
