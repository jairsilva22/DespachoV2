using System;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class cnfAlertaP : System.Web.UI.Page
    {
        cUtilidades cUtl = new cUtilidades();
        cAlertaP cAP = new cAlertaP();
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

                throw;
            }
        }

        protected void llenarGrid()
        {
            cAP.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
            listView.DataSource = cAP.obtenerView();
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
                    //oDlgRes = MessageBox.Show("¿Estás seguro que deseas eliminar ésta alerta?", "¡¡¡CONFIRMACIÓN!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    //if (oDlgRes == DialogResult.Yes)
                    //{
                        cAP.eliminar(id, int.Parse(Request.Cookies["ksroc"]["id"]));
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

        protected void listView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}