using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;


namespace despacho
{
    public partial class docMod : System.Web.UI.Page
    {
        cDocumento cDoc = new cDocumento();
        cSucursales cSuc = new cSucursales();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                cDoc.idUsuarioActivo = int.Parse(Request.Cookies["ksroc"]["id"]);
                if (!IsPostBack)
                {
                    cargarDoc();
                }
            }
            catch (Exception)
            {

            }
        }
        private void cargarDoc()
        {
            DataTable dt = cDoc.obtenerDocumentoByID(int.Parse(Request.QueryString["id"]));
            txtDescripcion.Text = dt.Rows[0]["descripcion"].ToString();
            txtTipo.Text = dt.Rows[0]["tipo"].ToString();
            txtAbrebiatura.Text = dt.Rows[0]["abrebiatura"].ToString();
            txtSerie.Text = dt.Rows[0]["serie"].ToString();
            txtEfecto.Text = dt.Rows[0]["efecto"].ToString();
            txtFormato.Text = dt.Rows[0]["formato"].ToString();
            lblSuc.Text = cSuc.obtenerNombreSucursalByID(int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            cDoc.descripcion = txtDescripcion.Text;
            cDoc.tipo = txtTipo.Text;
            cDoc.abrebiatura = txtAbrebiatura.Text;
            cDoc.serie = txtSerie.Text;
            cDoc.efecto = txtEfecto.Text;
            cDoc.formato = txtFormato.Text;
            cDoc.idUsuarioActivo = int.Parse(Request.Cookies["ksroc"]["id"]);
            cDoc.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);

            cDoc.actualizar(int.Parse(Request.QueryString["id"]), int.Parse(Request.Cookies["ksroc"]["id"]));
            Response.Redirect("documentos.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("documentos.aspx");
        }
    }
}