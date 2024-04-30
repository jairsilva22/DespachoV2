using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class validarSolicitud : System.Web.UI.Page
    {
        cUtilidades cUtl = new cUtilidades();
        cOrdenes cOrd = new cOrdenes();
        cSolicitudes cSol = new cSolicitudes();
        cUsuarios cUsr = new cUsuarios();
        cDetallesSolicitud cDS = new cDetallesSolicitud();
        Folio cFol = new Folio();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                cUtl.idUsuarioActivo = int.Parse(Request.Cookies["ksroc"]["id"]);
                if (!IsPostBack)
                {
                    llenarVendedores();
                    cargarDatos();
                }
            }
            catch (Exception)
            {

            }
        }

        private void cargarDatos()
        {
            hfIdSolicitud.Value = Request.QueryString["id"]; 
            cSol.obtenerSolicitudByID(int.Parse(hfIdSolicitud.Value));
            lblClienteNombre.Text = cSol.nombreCliente;
            lblFolio.Text = cSol.folio.ToString();
            txtFecha.Text = cSol.fecha.ToString().Substring(0,10);
            lblHora.Text = cSol.hora;
            llenarDetalleSolicitud(int.Parse(hfIdSolicitud.Value));
        }

        //private void fillHora(string sHora)
        //{
        //    cbxHora.SelectedValue = sHora.Substring(0, 2);
        //    cbxMinutos.SelectedValue = sHora.Substring(3, 2);
        //}
        private void llenarDetalleSolicitud(int idS)
        {
            lvDetalles.DataSource = cDS.obtenerDetallesSolicitud(idS);
            lvDetalles.DataBind();
        }
        protected void llenarVendedores()
        {
            ddlV2.Items.Clear();
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("nombre");

            dt.Rows.Add("0", "Selecciona un Vendedor");

            DataTable dt1 = cUsr.obtenerUsuariosActivosByPefilAndSucursal(2, int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
            foreach (DataRow dr in dt1.Rows)
            {
                dt.Rows.Add(dr["id"].ToString(), dr["nombre"].ToString());
            }

            ddlV2.DataSource = dt;
            ddlV2.DataValueField = "id";
            ddlV2.DataTextField = "nombre";
            ddlV2.DataBind();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            //buscamos si requiere factura la solicitud para ingresarla en la orden 
           
            cSol.obtenerSolicitudByID(int.Parse(hfIdSolicitud.Value));
            cOrd.reqFac = cSol.reqFac;
            cOrd.idSolicitud = int.Parse(hfIdSolicitud.Value);
            if (ddlV2.SelectedValue.Equals("0") || ddlV2.SelectedIndex.Equals(0))
                return; //Pedir que seleccione un vendedor
            cOrd.idVendedor = int.Parse(ddlV2.SelectedValue);
            cOrd.fecha = DateTime.Parse(txtFecha.Text);
            cOrd.hora = lblHora.Text;
            cOrd.comentarios = txtComentarios.Text;
            cOrd.ubicacion = txtComentariosUbicacion.Text;
            string[] arrFolio = cFol.obtenerFolio("ordenes", int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
            cOrd.folio = arrFolio[0] + arrFolio[2];
            
            cOrd.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
            cFol.actualizarFolio("ordenes", int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
            cSol.cambiarAOrden(cOrd.idSolicitud, int.Parse(Request.Cookies["ksroc"]["id"]));
            Response.Redirect("solicitudes.aspx");
        }
        //private string getHora()
        //{
        //    return cbxHora.Text + ":" + cbxMinutos.Text;
        //}

    }
}
