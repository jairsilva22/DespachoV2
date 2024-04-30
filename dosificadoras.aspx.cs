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
    public partial class dosificacdoras : System.Web.UI.Page
    {
        cUtilidades cUtl = new cUtilidades();
        cDosificadora cDos = new cDosificadora();
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
            cDos.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
            listView.DataSource = cDos.obtenerView();
            listView.DataBind();
        }

        protected void listView_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("delete"))
                {
                    string[] arr;
                    arr = e.CommandArgument.ToString().Split('ˇ');
                    hfId.Value = arr[0];
                    this.mlblTitle.Text = "¡¡¡CONFIRMACIÓN!!!";
                    this.mlblMessage.Text = "¿Estás seguro que deseas eliminar la dosificadora " + arr[1] + " del sistema?";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ShowPopup();", true);
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

        protected void mbtnAceptar_Click(object sender, EventArgs e)
        {
            cDos.id = int.Parse(hfId.Value);
            cDos.eliminar(int.Parse(Request.Cookies["ksroc"]["id"]));
            llenarGrid();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopup();", true);
        }

        protected void mbtnClose_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopup();", true);
        }
    }
}