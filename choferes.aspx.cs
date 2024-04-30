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
    public partial class choferes : System.Web.UI.Page
    {
        cUtilidades cUtl = new cUtilidades();
        cUsuarios cUsr = new cUsuarios();
        cUnidadTChofer cUA = new cUnidadTChofer();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                cUtl.idUsuarioActivo = int.Parse(Request.Cookies["ksroc"]["id"]);
                if (!IsPostBack)
                {
                    llenarGrid();
                }
            }
            catch (Exception)
            {

            }
        }

        protected void llenarGrid()
        {
            DataTable dt = cUsr.obtenerChoferesOfUTC(int.Parse(Request.Cookies["ksroc"]["idSucursal"]));

            //dt.Columns.Add("unidad", Type.GetType("System.String"));


            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    dt.Rows[i]["unidad"] = cUA.obtenerUnidadByIdChofer(int.Parse(dt.Rows[i]["id"].ToString()));
            //}
            listView.DataSource = dt;
            listView.DataBind();
        }
    }
}