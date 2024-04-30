using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class percepcionesTipoAdd : System.Web.UI.Page
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
                    cargarControles();
                }
            }
            catch (Exception)
            {

            }
        }
        protected void cargarControles()
        {
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtTipo.Text.Equals(""))
            {
                lblError.Text = "Favor de ingresar el tipo de percepción";
                return;
            }
            cTipoP.percepcion = txtTipo.Text;

            if (cTipoP.existe())
            {
                lblError.Text = "Ya existe el Tipo de Percepción, favor de revisar";
            }
            else
            {
                cTipoP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                Response.Redirect("percepcionesTipo.aspx");
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("percepcionesTipo.aspx");
        }
    }
}
