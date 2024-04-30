using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class DepartamentoAdd : System.Web.UI.Page
    {
        cDepartamento dpto = new cDepartamento();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                buscarEstadoR();
            }
        }

        private void buscarEstadoR()
        {
            ddlEstadoR.DataSource = dpto.obtenerEstadoR();
            ddlEstadoR.DataValueField = "id";
            ddlEstadoR.DataTextField = "nombre";
            ddlEstadoR.DataBind();
            ddlEstadoR.Items.Insert(0, new ListItem("Seleccionar", ""));
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            if(txtDpto.Text == "")
            {
                lblError.Text = "Favor de Ingresar el Nombre del Departamento.";
            }else if (ddlEstadoR.SelectedValue == "")
            {
                lblError.Text = "Favor de Seleccionar el Estado de Resultados.";
            }
            else
            {
                dpto.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                dpto.departamento = txtDpto.Text;
                if (dpto.existeDpto())
                {
                    lblError.Text = "El Departamento ya existe, favor de ingresar otro.";
                }
                else
                {
                    dpto.idUsuario = int.Parse(Request.Cookies["ksroc"]["id"]);
                    dpto.idEdoR = int.Parse(ddlEstadoR.SelectedValue);
                    dpto.insertarDpto();
                    Response.Redirect("Departamento.aspx");
                }
            }
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Departamento.aspx");
        }
    }
}