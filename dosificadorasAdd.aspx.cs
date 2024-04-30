using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class dosificadorasAdd : System.Web.UI.Page
    {
        cUtilidades cUtl = new cUtilidades();
        cDosificadora cDos = new cDosificadora();
        cUDM cUdm = new cUDM();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                cUtl.idUsuarioActivo = int.Parse(Request.Cookies["ksroc"]["id"]);
                if (!IsPostBack)
                {
                    cargarControles();
                }
            }
            catch (Exception)
            {

            }
        }
        protected void cargarControles()
        {
            llenarUDM();
        }
        protected void llenarUDM()
        {
            ddlUDM.Items.Clear();
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("unidad");

            dt.Rows.Add("0", "Selecciona una Unidad de Medida");

            //cUdm.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
            DataTable dt1 = cUdm.obtenerUDMActivas();
            foreach (DataRow dr in dt1.Rows)
            {
                dt.Rows.Add(dr["id"].ToString(), dr["unidad"].ToString());
            }

            ddlUDM.DataSource = dt;
            ddlUDM.DataValueField = "id";
            ddlUDM.DataTextField = "unidad";
            ddlUDM.DataBind();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text.Equals(""))
            {
                this.mlblTitle.Text = "¡¡¡ATENCIÓN!!!";
                this.mlblMessage.Text = "Favor de introducir el nombre de la dosificadora";
                mbtnClose.Visible = false;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ShowPopup();", true);
            }
            cDos.nombre = txtNombre.Text;
            if (txtCapacidad.Text.Equals(""))
            {
                this.mlblTitle.Text = "¡¡¡ATENCIÓN!!!";
                this.mlblMessage.Text = "Favor de introducir la capacidad de la dosificadora";
                mbtnClose.Visible = false;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ShowPopup();", true);
            }
            cDos.capacidad = txtCapacidad.Text;
            if (txtLimite.Text.Equals(""))
            {
                this.mlblTitle.Text = "¡¡¡ATENCIÓN!!!";
                this.mlblMessage.Text = "Favor de introducir el límite de la dosificadora";
                mbtnClose.Visible = false;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ShowPopup();", true);
            }
            cDos.limite = txtLimite.Text;
            if (ddlUDM.SelectedValue.Equals("") || ddlUDM.SelectedIndex.Equals(0) || ddlUDM.SelectedValue.Equals("0"))
            {
                this.mlblTitle.Text = "¡¡¡ATENCIÓN!!!";
                this.mlblMessage.Text = "Favor de seleccionar la unidad de medida de la dosificadora";
                mbtnClose.Visible = false;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ShowPopup();", true);
            }
            cDos.idUDM = int.Parse(ddlUDM.SelectedValue);
            cDos.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
            if (cDos.existe())
            {
                lblError.Text = "Ya existe registro con el mismo nombre en la Sucursal, favor de revisar";
            }
            else
            {
                cDos.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                Response.Redirect("dosificadoras.aspx");
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("dosificadoras.aspx");
        }

        protected void mbtnAceptar_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopup();", true);
        }

        protected void mbtnClose_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopup();", true);
        }

    }
}
