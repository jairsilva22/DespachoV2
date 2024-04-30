using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class filtroEstadoResultados : System.Web.UI.Page
    {
        cSucursales cSuc = new cSucursales();
        cTipoProducto cTP = new cTipoProducto();
        cOrdenesDosificacion cOD = new cOrdenesDosificacion();
        cClientes cCl = new cClientes();
        cSolicitudes cSol = new cSolicitudes();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillSucursales();
                fillAnios();
            }
        }

        protected void fillSucursales()
        {
            ddlSucursal.DataSource = cSuc.obtenerSucursales();
            ddlSucursal.DataValueField = "id";
            ddlSucursal.DataTextField = "nombre";
            ddlSucursal.DataBind();
            ddlSucursal.Items.Insert(0, new ListItem("Todas las sucursales", "0"));
            ddlSucursal.SelectedValue = Request.Cookies["ksroc"]["idSucursal"];
        }

        protected void fillAnios()
        {
            cbxAnio.Items.Clear();
            DataTable dt = new DataTable();
            dt.Columns.Add("valor");
            dt.Columns.Add("anio");

            //dt.Rows.Add("0", "Selecciona un año");

            int lastYear = int.Parse(DateTime.Now.Year.ToString());

            int firstYear = cSol.obtenerAnioPrimerRegistro(int.Parse(ddlSucursal.SelectedValue));

            for (int i = lastYear; i >= firstYear; i--)
            {
                dt.Rows.Add(i.ToString(), i.ToString());
            }

            cbxAnio.DataSource = dt;
            cbxAnio.DataValueField = "valor";
            cbxAnio.DataTextField = "anio";
            cbxAnio.DataBind();
            cbxAnio.SelectedIndex = 0;
        }
        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            string url = "reporteEstadoResultados.aspx?idSucursal=" + ddlSucursal.SelectedValue + "&anio=" + cbxAnio.Text;
            string s = "window.open('" + url + "', '_blank');";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", s, true);
            //Response.Write("<script>window.open('reporteEstadoResultados.aspx?idSucursal=" + ddlSucursal.SelectedValue + "&anio=" + cbxAnio.Text + "' ,'_blank');</script>");
        }
        protected void btnExcel_Click(object sender, EventArgs e)
        {
            string url = "reporteEstadoResultados.aspx?idSucursal=" + ddlSucursal.SelectedValue + "&anio=" + cbxAnio.Text + "&Excel=1";
            string s = "window.open('" + url + "', '_blank');";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", s, true);
            //Response.Write("<script>window.open('reporteEstadoResultados.aspx?idSucursal=" + ddlSucursal.SelectedValue + "&anio=" + cbxAnio.Text + "&Excel=1" + "' ,'_blank');</script>");
        }

        protected void ddlSucursal_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillAnios();
        }
    }
}