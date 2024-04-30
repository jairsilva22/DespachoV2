using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;


namespace despacho
{
    public partial class MonedaMod : System.Web.UI.Page
    {
        cMoneda cMD = new cMoneda();
        cSucursales cSuc = new cSucursales();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    lblSuc.Text = cSuc.obtenerNombreSucursalByID(int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
                    cargarMD();
                }
            }
            catch (Exception)
            {

            }
        }
        private void cargarMD()
        {
            DataTable dt = cMD.obtenerMonedaByID(int.Parse(Request.QueryString["id"]));
            txtClave.Text = dt.Rows[0]["cmoneda"].ToString();
            txtDescripcion.Text = dt.Rows[0]["descripcion"].ToString();
            txtCambio.Text = dt.Rows[0]["tcambio"].ToString();
            lblSuc.Text = cSuc.obtenerNombreSucursalByID(int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            cMD.cMon = txtClave.Text;
            cMD.descripcion = txtDescripcion.Text;
            cMD.tCambio = double.Parse(txtCambio.Text);
            cMD.idSuc = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);

            cMD.actualizar(int.Parse(Request.QueryString["id"]));
            Response.Redirect("monedas.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("monedas.aspx");
        }
    }
}