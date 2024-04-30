using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class clientesProyectos : System.Web.UI.Page
    {
        cClientes cClt = new cClientes();
        cUtilidades cUtl = new cUtilidades();
        cUsuarios cUsr = new cUsuarios();
        cEstados cEst = new cEstados();
        cCiudades cCd = new cCiudades();
        cFormasPago cFP = new cFormasPago();
        cCodigosPostales cCP = new cCodigosPostales();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                cUtl.idUsuarioActivo = int.Parse(Request.Cookies["ksroc"]["id"]);
                if (!IsPostBack)
                {
                }
            }
            catch (Exception)
            {

            }
        }

        protected void cargarControles()
        {
        }

        private void fillColonias(int cp)
        {
            cbxColoniaProyecto.Items.Clear();
            DataTable dt = cCP.getColoniasByCP(cp);
            cbxColoniaProyecto.DataSource = dt;
            cbxColoniaProyecto.DataValueField = "asenta";
            cbxColoniaProyecto.DataTextField = "asenta";
            cbxColoniaProyecto.DataBind();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            //cClt.nombre = txtNombreCliente.Text;
            //cClt.cp = int.Parse(txtCP.Text);
            //cClt.calle = txtCalle.Text;
            //if (txtNumero.Text.Equals(""))
            //{
            //    txtNumero.Text = "0";
            //}
            //cClt.numero = int.Parse(txtNumero.Text);
            //if (txtInterior.Text.Equals(""))
            //{
            //    txtInterior.Text = "0";
            //}
            //cClt.interior = int.Parse(txtInterior.Text);
            //cClt.colonia = ddlColonias.SelectedItem.Text;
            //cClt.estado = ddlEstados.SelectedItem.Text;
            //cClt.idEstado = int.Parse(hfIdEstado.Value);
            //cClt.ciudad = ddlCiudades.SelectedItem.Text; 
            //if (ddlCiudades.SelectedValue.Equals(null))
            //{
            //    ddlCiudades.SelectedIndex = 0;
            //    hfIdCiudad.Value = ddlCiudades.SelectedValue;
            //}
            //else
            //{
            //    hfIdCiudad.Value = ddlCiudades.SelectedValue;
            //}
            //cClt.idCiudad = int.Parse(hfIdCiudad.Value);
            //cClt.email = txtEmail.Text;
            //cClt.telefono = txtTelefono.Text;
            //cClt.celular = txtCelular.Text;
            //if (cClt.idVendedor.Equals(null))
            //{
            //    hfIdVendedor.Value = "0";
            //}
            //cClt.idVendedor = int.Parse(hfIdVendedor.Value);
            //if (ddlFP.SelectedValue.Equals(null))
            //    ddlFP.SelectedValue = "1";
            //cClt.idFormaPago = int.Parse(ddlFP.SelectedValue);
            //cClt.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
            //try
            //{
            //    if (!cClt.existe())
            //    {
            //        cClt.clave = cClt.generarClave();
            //        cClt.insertar(cUtl.idUsuarioActivo);
            //        Response.Redirect("clientes.aspx");
            //    }
            //    else
            //        lblError.Text = "Ya existe un cliente registrado con el mismo nombre";
            //}
            //catch (Exception ex)
            //{

            //}
        }
        protected void txtCP_TextChanged(object sender, EventArgs e)
        {
            if (txtCP.Text.Length == 5)
            {
                fillColonias(int.Parse(txtCP.Text));
            }
        }
    }
}