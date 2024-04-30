using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class filtroDispatchPlan : System.Web.UI.Page
    {
        cSucursales cSuc = new cSucursales();
        cTipoProducto cTP = new cTipoProducto();
        cUTransporte cUT = new cUTransporte();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            if (txtFecha.Text.Equals(""))
            {
                lblError.Text = "Favor de seleccionar fecha";
                return;
            }
            else
            {
                Response.Write("<script>window.open('ReporteDispatchPlan.aspx?idSucursal=" + int.Parse(Request.Cookies["ksroc"]["idSucursal"]) + "&Fecha=" + txtFecha.Text + "' ,'_blank');</script>");
            }
        }
        protected void btnExcel_Click(object sender, EventArgs e)
        {
            if (txtFecha.Text.Equals(""))
            {
                lblError.Text = "Favor de seleccionar fecha de inicio";
                return;
            }
            else
            {
                Response.Write("<script>window.open('ReporteDispatchPlan.aspx?idSucursal=" + int.Parse(Request.Cookies["ksroc"]["idSucursal"]) + "&Fecha=" + txtFecha.Text + "&Excel=1" + "' ,'_blank');</script>");
            }
        }
    }
}