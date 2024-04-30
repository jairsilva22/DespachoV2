using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class perfilesMod : System.Web.UI.Page
    {
        cPerfiles cPerf = new cPerfiles();
        cUtilidades cUtl = new cUtilidades();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                cUtl.idUsuarioActivo = int.Parse(Request.Cookies["ksroc"]["id"]);
                cPerf.id = int.Parse(Request.QueryString["id"]);
                if (!IsPostBack)
                {
                    getPerfil();
                }
            }
            catch (Exception)
            {

            }
        }
        public void getPerfil()
        {
            cPerf.obtenerPerfilByID();
            txtDescripcion.Text = cPerf.descripcion;
            chbxActivo.Checked = cPerf.activo;
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            cPerf.descripcion = txtDescripcion.Text;
            cPerf.activo = chbxActivo.Checked;
            cPerf.actualizar(cUtl.idUsuarioActivo);
            Response.Redirect("perfiles.aspx");
        }
    }
}