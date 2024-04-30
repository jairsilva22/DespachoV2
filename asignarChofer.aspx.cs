using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class asignarChofer : System.Web.UI.Page
    {
        cUtilidades cUtl = new cUtilidades();
        cUsuarios cUsr = new cUsuarios();
        cUTransporte cUt = new cUTransporte();
        cUnidadTChofer cUC = new cUnidadTChofer();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                cUtl.idUsuarioActivo = int.Parse(Request.Cookies["ksroc"]["id"]);
                if (!IsPostBack)
                {
                    cargarControles();
                    cargarDatos();
                    hfDelete.Value = "0";
                    hfReasignar.Value = "0";
                }
            }
            catch (Exception)
            {
            }
        }

        private void cargarDatos()
        {
            cUt.id = int.Parse(Request.QueryString["id"]);
            cUt.obtenerByID(cUt.id);
            lblNombre.Text = cUt.nombre;
            hfIdUnidad.Value = cUt.id.ToString();
            llenarGrid(cUt.id);
        }

        protected void cargarControles()
        {
            llenarChoferes();
        }

        protected void llenarChoferes()
        {
            ddlChofer.Items.Clear();
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("nombre");

            dt.Rows.Add("0", "Selecciona un Chofer");

            DataTable dt1 = cUsr.obtenerUsuariosActivosByPefilAndSucursal(1003, int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
            foreach (DataRow dr in dt1.Rows)
            {
                dt.Rows.Add(dr["id"].ToString(), dr["nombre"].ToString());
            }

            ddlChofer.DataSource = dt;
            ddlChofer.DataValueField = "id";
            ddlChofer.DataTextField = "nombre";
            ddlChofer.DataBind();
        }
        protected void llenarGrid(int idU)
        {
            lvDetalles.DataSource = cUC.obtenerUnidadesAsignadasByIDUnidadANDIDSucursal(idU, int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
            lvDetalles.DataBind();
        }
        public string mostrar(bool activo)
        {
            string pram = "";
            if (activo)
            {
                pram = "<i class='icon-checkmark' style='color: #32AB52'></i>";
            }
            else
            {
                pram = "<i class='icon-cancel' style='color: gray'></i>";
            }
            return pram;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("unidadesTransporte.aspx");
        }

        protected void lvDetalles_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("delete"))
                {
                    string[] arr;
                    arr = e.CommandArgument.ToString().Split('ˇ');
                    hfIdUC.Value = arr[0];
                    hfDelete.Value = "1";
                    lblDelete.Text = "1";
                    this.mlblTitle.Text = "¡¡¡CONFIRMACIÓN!!!";
                    this.mlblMessage.Text = "¿Estás seguro que deseas eliminar a " + arr[1] + " como chofer de ésta unidad?";
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

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            cUC.idUnidad = int.Parse(hfIdUnidad.Value);
            if (hfIdChofer.Value.Equals(""))
            {
                if (ddlChofer.SelectedIndex == 0)
                {
                    lblError.Text = "Favor de seleccionar un chofer";
                    return;
                }
                hfIdChofer.Value = ddlChofer.SelectedValue;
            }
            cUC.idChofer = int.Parse(hfIdChofer.Value);
            cUC.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);

            int idAux = cUC.existeChoferEnUnidad(cUC.idChofer);
            //int idAux = cUC.existeChofer(cUC.idChofer);
            //if (idAux.Equals(0))
            //{
            cUC.desactivarChoferes(cUC.idUnidad, int.Parse(Request.Cookies["ksroc"]["id"]));
            if (idAux.Equals(0))
                cUC.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
            else
                cUC.actualizar(idAux, int.Parse(Request.Cookies["ksroc"]["id"]));
            llenarGrid(cUC.idUnidad);
            lblError.Text = "Se realizó la asignación a la unidad correctamente";
            //}
            //else
            //{
            //    hfReasignar.Value = "1";
            //    lblReasignar.Text = "1";
            //    this.mlblTitle.Text = "¡¡¡CONFIRMACIÓN!!!";
            //    this.mlblMessage.Text = "¿Estás seguro que deseas reasignar a " + ddlChofer.SelectedItem.Text + " a ésta unidad?";
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ShowPopup();", true);
            //}
        }

        protected void mbtnClose_Click(object sender, EventArgs e)
        {
            if (!hfReasignar.Value.Equals("0"))
            {
                hfReasignar.Value = "0";
                lblReasignar.Text = "0";
                lblError.Text = "No se realizó la asignación de chofer a la unidad";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopup();", true);
            }
            if (!hfDelete.Value.Equals("0"))
            {
                hfDelete.Value = "0";
                lblDelete.Text = "0";
                lblError.Text = "No se realizó la eliminación de chofer de la unidad";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopup();", true);
            }
        }

        protected void mbtnAceptar_Click(object sender, EventArgs e)
        {
            if (!hfReasignar.Value.Equals("0") || !lblReasignar.Text.Equals("0"))
            {
                cUC.idUnidad = int.Parse(Request.QueryString["id"]);
                cUC.idChofer = int.Parse(ddlChofer.SelectedValue);
                cUC.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                cUC.desactivarChoferes(cUC.idUnidad, int.Parse(Request.Cookies["ksroc"]["id"]));
                cUC.actualizar(cUC.existeChofer(cUC.idChofer), int.Parse(Request.Cookies["ksroc"]["id"]));
                lblError.Text = "Se realizó la asignación a la unidad correctamente";
                llenarGrid(cUC.idUnidad);
                hfReasignar.Value = "0";
                lblReasignar.Text = "0";
            }
            if (!hfDelete.Value.Equals("0") || !lblDelete.Text.Equals("0"))
            {
                cUC.desactivarChofer(int.Parse(hfIdUC.Value), int.Parse(Request.Cookies["ksroc"]["id"]));
                cUC.eliminar(int.Parse(hfIdUC.Value), int.Parse(Request.Cookies["ksroc"]["id"]));
                cUt.id = int.Parse(Request.QueryString["id"]);
                lblError.Text = "Se eliminó el chofer de la unidad correctamente";
                llenarGrid(cUt.id);
                hfDelete.Value = "0";
                lblDelete.Text = "0";
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopup();", true);
        }
    }
}
