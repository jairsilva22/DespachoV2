using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class Departamento : System.Web.UI.Page
    {
        cDepartamento dpto = new cDepartamento();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                llenar();
            }
        }

        private void llenar()
        {
            dpto.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
            listView.DataSource = dpto.obtenerDptos();
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
                    this.mlblMessage.Text = "¿Estás seguro que deseas eliminar el Departamento " + arr[1] + " del sistema?";
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
            dpto.eliminar(int.Parse(hfId.Value), int.Parse(Request.Cookies["ksroc"]["id"]));
            llenar();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopup();", true);
        }

        protected void mbtnClose_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopup();", true);
        }
        public string buscarEdoR(string idEdoR)
        {
            string param = "";
            if(idEdoR != "")
            {
                dpto.idEdoR = int.Parse(idEdoR);
                dpto.buscarEdoR();
                param = dpto.nombreER;
            }
            return param;
        }
    }    
}