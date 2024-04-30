using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class clientesAdd : System.Web.UI.Page
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
                txtNombreCliente_AutoCompleteExtender.ContextKey = Request.Cookies["ksroc"]["idSucursal"];
                if (!IsPostBack)
                {
                    cargarControles();
                    //crearCookies();
                }
            }
            catch (Exception)
            {

            }
        }

        protected void cargarControles()
        {
            llenarEstados();
            llenarFormasDePago();
            llenarVendedores();
        }

        protected void llenarEstados()
        {
            ddlEstados.Items.Clear();
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("estado");

            dt.Rows.Add("0", "Selecciona un Estado");

            DataTable dt1 = cEst.obtenerEstados();
            foreach (DataRow dr in dt1.Rows)
            {
                dt.Rows.Add(dr["id"].ToString(), dr["estado"].ToString());
            }

            ddlEstados.DataSource = dt;
            ddlEstados.DataValueField = "id";
            ddlEstados.DataTextField = "estado";
            ddlEstados.DataBind();

            ddlCiudades.Items.Add("Selecciona un Estado");
        }
        protected void llenarCiudades(int idEst)
        {
            ddlCiudades.Items.Clear();
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("ciudad");

            dt.Rows.Add("0", "Selecciona una Ciudad");

            DataTable dt1 = cCd.obtenerCiudadesByIdEstado(idEst);
            foreach (DataRow dr in dt1.Rows)
            {
                dt.Rows.Add(dr["id"].ToString(), dr["ciudad"].ToString());
            }

            ddlCiudades.DataSource = dt;
            ddlCiudades.DataValueField = "id";
            ddlCiudades.DataTextField = "ciudad";
            ddlCiudades.DataBind();
        }
        protected void llenarVendedores()
        {
            ddlVendedores.Items.Clear();
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("nombre");

            dt.Rows.Add("0", "Selecciona un Vendedor");

            DataTable dt1 = cUsr.obtenerUsuariosBySucursal(int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
            //DataTable dt1 = cUsr.obtenerUsuariosActivosByPefilAndSucursal(2, int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
            foreach (DataRow dr in dt1.Rows)
            {
                dt.Rows.Add(dr["id"].ToString(), dr["nombre"].ToString());
            }

            ddlVendedores.DataSource = dt;
            ddlVendedores.DataValueField = "id";
            ddlVendedores.DataTextField = "nombre";
            ddlVendedores.DataBind();
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> getDataNombreCliente(string prefixText, int count, string contextKey)
        {
            try
            {
                int cKey = int.Parse(contextKey);
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnx"].ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT clave, nombre FROM clientes WHERE ((LOWER(nombre) LIKE '%" + prefixText + "%') OR (UPPER(nombre) LIKE '%" + prefixText + "%')) " +
                        "AND (idSucursal = " + cKey + ")", conn))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            List<String> list = new List<String>();
                            foreach (DataRow dr in dt.Rows)
                            {
                                list.Add(dr["nombre"].ToString());
                            }
                            return list;
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        protected void txtNombreCliente_TextChanged(object sender, EventArgs e)
        {
            cClt.nombre = txtNombreCliente.Text;
        }
        private void llenarFormasDePago()
        {
            ddlFP.Items.Clear();
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("nombre");

            dt.Rows.Add("0", "Selecciona una Forma de Pago");
            dt = cFP.obtenerFormasPago();

            ddlFP.DataSource = dt;
            ddlFP.DataValueField = "id";
            ddlFP.DataTextField = "nombre";
            ddlFP.DataBind();

            ddlFP.SelectedValue = "2";
        }

        protected void txtCP_TextChanged(object sender, EventArgs e)
        {
            if (hfSearchBy.Value.Equals("cd"))
                return;
            hfSearchBy.Value = "cp";
            if (txtCP.Text.Length == 5)
            {
                fillColonias(int.Parse(txtCP.Text), true, 0);
            }
        }

        private void fillColonias(int value, bool hasCP, int idE)
        {
            cbxColonias.Items.Clear();
            if (hasCP)
            { 
                DataTable dt = cCP.getColoniasByCP(value);
                cbxColonias.DataSource = dt;
                cbxColonias.DataValueField = "asenta";
                cbxColonias.DataTextField = "asenta";
                cbxColonias.DataBind();

                hfIdEstadoCP.Value = dt.Rows[0]["idEstado"].ToString();
                ddlEstados.SelectedValue = hfIdEstadoCP.Value;
                llenarCiudades(int.Parse(ddlEstados.SelectedValue));
                hfIdCiudadCP.Value = dt.Rows[0]["idCiudad"].ToString();
                ddlCiudades.SelectedValue = hfIdCiudadCP.Value;
                cbxColonias.Focus();
            }
            else
            {
                DataTable dt = cCP.getColoniasByIdC(value, idE);
                cbxColonias.DataSource = dt;
                cbxColonias.DataValueField = "asenta";
                cbxColonias.DataTextField = "asenta";
                cbxColonias.DataBind();
                cbxColonias.Focus();
            }
        }
        protected void ddlVendedores_SelectedIndexChanged(object sender, EventArgs e)
        {
            hfIdVendedor.Value = ddlVendedores.SelectedValue;
        }
        protected void ddlFP_SelectedIndexChanged(object sender, EventArgs e)
        {
            hfIdFP.Value = ddlFP.SelectedValue;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            cClt.nombre = txtNombreCliente.Text;
            if (!txtCP.Text.Equals(""))
                cClt.cp = int.Parse(txtCP.Text);
            cClt.calle = txtCalle.Text;
            if (txtNumero.Text.Equals(""))
            {
                txtNumero.Text = "0";
            }
            cClt.numero = int.Parse(txtNumero.Text);
            cClt.interior = txtInterior.Text;
            if (cbxColonias.Text.Equals(""))
            {
                lblError.Text = "Favor de seleccionar una colonia o ingresar el nombre de la colonia para agregarla al sistema";
                return;
            }
            cCP.asenta = cbxColonias.SelectedItem.Text;
            if (!cCP.existe() && !cCP.cp.Equals(0))
            {
                cCP.cp = cClt.cp;
                cCP.idEstado = int.Parse(hfIdEstadoCP.Value);
                cCP.idCiudad = int.Parse(hfIdCiudadCP.Value);
                cCP.insertar();
            }
            cClt.colonia = cbxColonias.SelectedItem.Text;
            cClt.estado = ddlEstados.SelectedItem.Text;
            if (ddlEstados.SelectedValue.Equals(null))
            {
                ddlEstados.SelectedIndex = 0;
                hfIdEstado.Value = ddlEstados.SelectedValue;
            }
            else
            {
                hfIdEstado.Value = ddlEstados.SelectedValue;
            }
            cClt.idEstado = int.Parse(hfIdEstado.Value);
            cClt.ciudad = ddlCiudades.SelectedItem.Text; 
            if (ddlCiudades.SelectedValue.Equals(null))
            {
                ddlCiudades.SelectedIndex = 0;
                hfIdCiudad.Value = ddlCiudades.SelectedValue;
            }
            else
            {
                hfIdCiudad.Value = ddlCiudades.SelectedValue;
            }
            cClt.idCiudad = cCd.obtenerIdByIdCiudadANDIdEstado(cClt.idEstado,  int.Parse(hfIdCiudad.Value));
            cClt.email = txtEmail.Text;
            cClt.telefono = txtTelefono.Text;
            cClt.celular = txtCelular.Text;
            if (cClt.idVendedor.Equals(null))
            {
                hfIdVendedor.Value = "0";
            }
            cClt.idVendedor = int.Parse(hfIdVendedor.Value);
            if (ddlFP.SelectedValue.Equals(null))
                ddlFP.SelectedValue = "1";
            cClt.idFormaPago = int.Parse(ddlFP.SelectedValue);
            cClt.directo = chbxDirecto.Checked;
            cClt.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
            try
            {
                if (!cClt.existe())
                {
                    cClt.clave = cClt.generarClave();
                    cClt.insertar(cUtl.idUsuarioActivo);
                    Response.Redirect("clientes.aspx");
                }
                else
                    lblError.Text = "Ya existe un cliente registrado con el mismo nombre";
            }
            catch (Exception)
            {

            }
        }

        protected void ddlEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            hfIdEstado.Value = ddlEstados.SelectedValue;
            llenarCiudades(int.Parse(hfIdEstado.Value));
        }

        protected void ddlCiudades_SelectedIndexChanged(object sender, EventArgs e)
        {
            hfIdCiudad.Value = ddlCiudades.SelectedValue;
            hfSearchBy.Value = "cd";
            fillColonias(int.Parse(hfIdCiudad.Value), false, int.Parse(hfIdEstado.Value));
        }

        protected void cbxColonias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (hfSearchBy.Value.Equals("cd"))
            {
                cCP.idEstado = int.Parse(hfIdEstado.Value);
                cCP.idCiudad = int.Parse(hfIdCiudad.Value);
                cCP.asenta = cbxColonias.SelectedItem.Text;
                txtCP.Text = cCP.obtenerCPByAsentaAndIdC();
            }
        }
    }
}