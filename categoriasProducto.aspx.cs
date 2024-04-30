using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class categoriasProducto : System.Web.UI.Page
    {
        cUtilidades cUtl = new cUtilidades();
        cCategorias cCat = new cCategorias();
        cTipoProducto cTP = new cTipoProducto();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                cUtl.idUsuarioActivo = int.Parse(Request.Cookies["ksroc"]["id"]);
                if (!IsPostBack)
                {
                    cargarDatos();
                }
            }
            catch (Exception)
            {

            }
        }

        private void cargarDatos()
        {
            cTP.id = int.Parse(Request.QueryString["id"]);
            DataTable dt = cTP.obtenerTiposProductoByID(cTP.id);
            lblTipoProducto.Text = dt.Rows[0]["tipo"].ToString();
            llenarGrid(cTP.id);

        }

        protected void llenarGrid(int id)
        {
            cCat.idTipoProducto = int.Parse(Request.QueryString["id"]);
            lvDetalles.DataSource = cCat.obtenerByIDTP(); 
            lvDetalles.DataBind();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("productosTipo.aspx");
        }

        protected void lvDetalles_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("update"))
                {
                    string[] arr;
                    arr = e.CommandArgument.ToString().Split('ˇ');
                    hfIdCat.Value = arr[0];
                    txtNombre.Text = arr[1];
                }
                if (e.CommandName.Equals("delete"))
                {
                    string[] arr;
                    arr = e.CommandArgument.ToString().Split('ˇ');
                    hfIdCat.Value = arr[0];
                    this.mlblTitle.Text = "¡¡¡CONFIRMACIÓN!!!";
                    this.mlblMessage.Text = "¿Estás seguro que deseas eliminar la categoría " + arr[1] + "?";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ShowPopup();", true);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void lvDetalles_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {

        }

        protected void mbtnClose_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopup();", true);
           
        }

        protected void mbtnAceptar_Click(object sender, EventArgs e)
        {
            cCat.id = int.Parse(hfIdCat.Value);
            cCat.eliminar(int.Parse(Request.Cookies["ksroc"]["id"]));
            llenarGrid(int.Parse(Request.QueryString["id"]));
            cleanInfo();
           
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopup();", true);
             
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            cCat.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
            cCat.idTipoProducto = int.Parse(Request.QueryString["id"]);
            if (txtNombre.Text.Equals(""))
            {
                lblError.Text = "Favor de introducir el nombre de la categoría";
                return;
            }
            cCat.nombre = txtNombre.Text;

            try
            {
                cCat.id = int.Parse(hfIdCat.Value);
            }
            catch (Exception)
            {

            }
            if (cCat.existeEnSucursal())
            {
                cCat.actualizar(int.Parse(Request.Cookies["ksroc"]["id"]));
            }
            else
            {
                if (hfIdCat.Value.Equals("") || hfIdCat.Value.Equals("0"))
                {
                    cCat.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                }
                else
                {
                    cCat.actualizar(int.Parse(Request.Cookies["ksroc"]["id"]));
                }
            }
            llenarGrid(cCat.idTipoProducto);
            cleanInfo();
        }

        private void cleanInfo()
        {
            hfIdCat.Value = "";
            txtNombre.Text = "";
        }

        protected void lvDetalles_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {

        }
    }
}
