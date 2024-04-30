using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class documentosAdd : System.Web.UI.Page
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
                    lblSuc.Text = cSuc.obtenerNombreSucursalByID(int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
                }
            }
            catch (Exception)
            {

            }
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

            if (cDoc.existe(cDoc.descripcion, cDoc.tipo))
            {
                lblError.Text = "Ya existe el documento, favor de revisar si es otro tipo de documento";
            }
            else
            {
                cDoc.insertar();
                Response.Redirect("Documentos.aspx");
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Documentos.aspx");
        }
    }
}