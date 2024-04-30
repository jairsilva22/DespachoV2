using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class perfilesAdd : System.Web.UI.Page
    {
        cPerfiles cPerf = new cPerfiles();
        cUtilidades cUtl = new cUtilidades();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                cUtl.idUsuarioActivo = int.Parse(Request.Cookies["ksroc"]["id"]);
            }
            catch (Exception)
            {

            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            cPerf.descripcion = txtDescripcion.Text;
            cPerf.activo = chbxActivo.Checked;
            cPerf.insertar(cUtl.idUsuarioActivo);
            Response.Redirect("perfiles.aspx");
        }
    }
}