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
    public partial class elementos : System.Web.UI.Page
    {
        cUtilidades cUtl = new cUtilidades();
        cElementos cEl = new cElementos();
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
            cEl.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
            listView.DataSource = cEl.obtenerView();
            listView.DataBind();
        }

        protected void listView_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("delete"))
                {
                    int id = int.Parse(e.CommandArgument.ToString());
                    //DialogResult oDlgRes;
                    //oDlgRes = MessageBox.Show("¿Estás seguro que deseas eliminar este tipo de producto?", "¡¡¡CONFIRMACIÓN!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    //if (oDlgRes == DialogResult.Yes)
                    //{
                    cEl.eliminar(id, int.Parse(Request.Cookies["ksroc"]["id"]));
                    llenarGrid();
                    //}
                }
            }
            catch (Exception)
            {

                throw;
            }
                
            
        }

        protected void listView_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}