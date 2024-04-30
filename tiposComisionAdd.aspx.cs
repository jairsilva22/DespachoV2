using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class tiposComisionAdd : System.Web.UI.Page
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
            cTipoC.comision = txtTipo.Text;

            if (cTipoC.existe())
            {
                lblError.Text = "Ya existe el Tipo de Percepción, favor de revisar";
            }
            else
            {
                cTipoC.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                Response.Redirect("tiposComision.aspx");
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("tiposComision.aspx");
        }
    }
}
