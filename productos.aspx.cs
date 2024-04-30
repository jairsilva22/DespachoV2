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
    public partial class productos : System.Web.UI.Page
    {
        cUtilidades cUtl = new cUtilidades();
        cProductos cProd = new cProductos();
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
            cProd.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
            listView.DataSource = cProd.obtenerProductos();
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
                    //this.mlblTitle.Text = "¡¡¡CONFIRMACIÓN!!!";
                    //this.mlblMessage.Text = "¿Estás seguro que deseas eliminar el producto " + arr[1] + " del sistema?";
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ShowPopup();", true);
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
                
                cProd.eliminar(int.Parse(hfId.Value), int.Parse(Request.Cookies["ksroc"]["id"]));
                cProd.id = int.Parse(hfId.Value);
                cProd.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                cProd.idUsuario = int.Parse(Request.Cookies["ksroc"]["id"]);
                if (cProd.eliminarContpaq())
                {
                    Response.Redirect("productos.aspx?error=0");
                }
             
            }
            catch (Exception ex)
            {
                if (ex.Message == "ok")
                {

                    //script que te abre el modal 
                    string errorMessage = "Producto eliminado correctamente ";
                    ScriptManager.RegisterStartupScript(this, GetType(), "showErrorModal", $"$('#errorMessage').text('{errorMessage}'); $('#errorModal .modal-header').addClass('bg-primary bg-gradient'); $('#errorModal').modal('show');", true);

                    ScriptManager.RegisterStartupScript(this, GetType(), "redirectScript", "setTimeout(function() { window.location.href = 'productos.aspx'; }, 2000);", true);

                }
                else
                {
                    string errorMessage = "Error: " + ex.Message;
                    ScriptManager.RegisterStartupScript(this, GetType(), "showErrorModal", $"$('#errorMessage').text('{errorMessage}'); $('#errorModal .modal-header').addClass('bg-danger bg-gradient'); $('#errorModal').modal('show');", true);

                }
            }
            
        }

        protected void mbtnAceptar_Click(object sender, EventArgs e)
        {

          
            //cProd.id = int.Parse(hfId.Value);
            //cProd.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
            //cProd.idUsuario = int.Parse(Request.Cookies["ksroc"]["id"]);
            //if (cProd.eliminarContpaq())
            //{
            //    Response.Redirect("productos.aspx?error=0");
            //}
            //cProd.eliminar(int.Parse(hfId.Value), int.Parse(Request.Cookies["ksroc"]["id"]));
            
            ////ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopup();", true);
        }


        protected void mbtnClose_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopup();", true);
        }

        protected void btnHomologar_Click(object sender, EventArgs e) {
            foreach (ListViewItem item in listView.Items) {
                CheckBox chkSeleccionado = (CheckBox)item.FindControl("chkHomologar");
                if (chkSeleccionado.Checked) {
                    // Obtener el ID de la fila seleccionada
                    int selectedId = (int)listView.DataKeys[item.DataItemIndex]["id"];
                    //lblResultados.Text += selectedId.ToString() + ", ";
                    cProd.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                    cProd.idUsuario = int.Parse(Request.Cookies["ksroc"]["id"]);
                    if(cProd.homologar(selectedId, cProd.idSucursal, cProd.idUsuario)) {
                        mlblTitle.Text = "Homologación de productos";
                        mlblMessage.Text = "Homologación exitosa de los productos.";
                        mbtnAceptar.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", "$('#myModal').modal('show');", true);
                    }
                }
            }
        }
    }
}