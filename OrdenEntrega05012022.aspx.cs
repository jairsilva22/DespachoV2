using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class OrdenEntrega05012022 : System.Web.UI.Page
    {
        cUtilidades cUtl = new cUtilidades();
        cDosificacion cDOS = new cDosificacion();
        cProductos cProd = new cProductos();
        cFormulacion cFor = new cFormulacion();
        cUsuarios cUsr = new cUsuarios();
        cProyectos cPro = new cProyectos();
        cCodigosPostales cCP = new cCodigosPostales();
        cSolicitudes cSol = new cSolicitudes();
        cDetallesSolicitud cDS = new cDetallesSolicitud();
        cOrdenes cOrd = new cOrdenes();
        cOrdenesDosificacion cOD = new cOrdenesDosificacion();
        cClientes cCl = new cClientes();
        cFormasPago cFP = new cFormasPago();
        protected void Page_Load(object sender, EventArgs e)
        {
            txtClaveCliente_AutoCompleteExtender.ContextKey = Request.Cookies["ksroc"]["idSucursal"];
            txtNombreCliente_AutoCompleteExtender.ContextKey = Request.Cookies["ksroc"]["idSucursal"];

            hfIdOrden.Value = Request.QueryString["idO"];
            try
            {
                hfidOD.Value = Request.QueryString["idOD"];
            }
            catch (Exception)
            {
                hfidOD.Value = "";
            }
            hfIdSolicitud.Value = Request.QueryString["idS"];
            hfIdDS.Value = Request.QueryString["idDS"];
            if (!IsPostBack)
            {
                cargarControles();
                fillData(int.Parse(hfIdSolicitud.Value));
            }
        }

        protected void cargarControles()
        {
            llenarDdlVendedores();
            llenarFormasDePago();
            //llenarTP();
            llenarEstados();
        }
        private void fillData(int idSolicitud)
        {
            hfIdSolicitud.Value = idSolicitud.ToString();
            DataTable dtS = cDOS.obtenerSolicitudByID(idSolicitud);
            //lblFolio.Text = dtS.Rows[0]["folioS"].ToString();
            ddlReqFac.SelectedValue = dtS.Rows[0]["reqFac"].ToString();
            txtFecha.Text = dtS.Rows[0]["fecha"].ToString().Substring(0, 10);
            fillHora(dtS.Rows[0]["hora"].ToString());
            ddlEstadoSolicitud.SelectedValue = dtS.Rows[0]["idEstadoSolicitud"].ToString();
            txtClaveCliente.Text = dtS.Rows[0]["clave"].ToString();
            txtNombreCliente.Text = dtS.Rows[0]["nombre"].ToString();
            ddlVendedores.SelectedValue = dtS.Rows[0]["idVendedor"].ToString();
            hfIdVendedor.Value = ddlVendedores.SelectedValue;
            ddlVendedorOrden.SelectedValue = dtS.Rows[0]["oIdVendedor"].ToString();
            hfOIdVendedor.Value = ddlVendedorOrden.SelectedValue;
            hfIdFP.Value = dtS.Rows[0]["idFormaPago"].ToString().Trim();
            ddlFP.SelectedValue = hfIdFP.Value;
            llenarProyectos(txtClaveCliente.Text);
            ddlProyectos.SelectedValue = dtS.Rows[0]["idProyecto"].ToString();
            fillProyecto(int.Parse(dtS.Rows[0]["idProyecto"].ToString()));
            hfIdProyecto.Value = dtS.Rows[0]["idProyecto"].ToString();
            txtObservaciones.Text = dtS.Rows[0]["comentarios"].ToString();
            llenarDetalleSolicitud(int.Parse(hfIdDS.Value));
            try
            {
                if (Request.QueryString["retro"].Equals("cfinanzas"))
                {
                    txtCantOrdenada.Enabled = false;
                    txtRevenimiento.Enabled = false;
                    ddlElemento.Enabled = false;
                }
            }
            catch (Exception)
            {
            }
        }
        private void fillHora(string sHora)
        {
            cbxHora.SelectedValue = sHora.Substring(0, 2);
            cbxMinutos.SelectedValue = sHora.Substring(3, 2);
        }

        private void llenarDetalleSolicitud(int idDS)
        {
            DataTable dtDS = cDOS.obtenerDetallesSolicitud(idDS, int.Parse(Request.Cookies["ksroc"]["idSucursal"]));

            //ddlTipoProducto.SelectedValue = dtDS.Rows[0]["idTipoProducto"].ToString();
            lblTipoProducto.Text = dtDS.Rows[0]["tipo"].ToString();
            if (lblTipoProducto.Text.ToLower().Contains("concre"))
            {
                lblElementos.Visible = true;
                ddlElemento.Visible = true;
                lblRevenimiento.Visible = true;
                txtRevenimiento.Visible = true;
            }
            else
            {
                lblElementos.Visible = false;
                ddlElemento.Visible = false;
                lblRevenimiento.Visible = false;
                txtRevenimiento.Visible = false;
            }
            hfIdTipoProducto.Value = dtDS.Rows[0]["idTipoProducto"].ToString();
            //txtCodigoProducto_AutoCompleteExtender.ContextKey = ddlTipoProducto.SelectedValue;
            if (!dtDS.Rows[0]["idElemento"].ToString().Equals("") || !dtDS.Rows[0]["idElemento"].ToString().Equals(null))
            {
                try
                {
                    llenarElementos(int.Parse(dtDS.Rows[0]["idTipoProducto"].ToString()));
                    ddlElemento.SelectedValue = dtDS.Rows[0]["idElemento"].ToString();
                    lblElementos.Visible = true;
                    ddlElemento.Visible = true;
                }
                catch (Exception)
                {

                }
                hfIdElemento.Value = dtDS.Rows[0]["idElemento"].ToString();
            }
            hfIdProducto.Value = dtDS.Rows[0]["idProducto"].ToString();
            //txtCodigoProducto.Text = dtDS.Rows[0]["codigo"].ToString();
            lblCodigoProducto.Text = dtDS.Rows[0]["codigo"].ToString();
            //lblCodigoProducto.Text = txtCodigoProducto.Text;
            lblUDM.Text = dtDS.Rows[0]["unidad"].ToString().Trim();
            txtPrecioF.Text = dtDS.Rows[0]["precioF"].ToString();
            txtRevenimiento.Text = dtDS.Rows[0]["revenimiento"].ToString();
            hfIdFactor.Value = dtDS.Rows[0]["idFactor"].ToString();
            txtCantOrdenada.Text = dtDS.Rows[0]["cantidad"].ToString();
            hfQtyOrdenadaEnBD.Value = txtCantOrdenada.Text;
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> getDataNombreCliente(string prefixText, int count, string contextKey)
        {
            try
            {
                List<string> equis = new List<string>();
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnx"].ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT clave, nombre FROM clientes WHERE(idSucursal=" + contextKey + ") AND  ((LOWER(nombre) LIKE '%" + prefixText +
                        "%') OR (UPPER(nombre) LIKE '%" + prefixText + "%'))", conn))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            if (!dt.Rows.Count.Equals(0))
                            {
                                List<String> list = new List<String>();
                                foreach (DataRow dr in dt.Rows)
                                {
                                    list.Add(dr["nombre"].ToString());
                                }
                                return list;
                            }
                            else
                            {
                                equis.Add("No existe el cliente que coincida con lo introducido, favor de registrarlo");
                                return equis;
                            }
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> getDataClaveCliente(string prefixText, int count, string contextKey)
        {
            try
            {
                List<string> equis = new List<string>();
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnx"].ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT clave FROM clientes WHERE (idSucursal=" + contextKey + ") AND  ((LOWER(clave) LIKE '%" + prefixText +
                        "%') OR (UPPER(clave) LIKE '%" + prefixText + "%'))", conn))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            if (!dt.Rows.Count.Equals(0))
                            {
                                List<String> list = new List<String>();
                                foreach (DataRow dr in dt.Rows)
                                {
                                    list.Add(dr["clave"].ToString());
                                }
                                return list;
                            }
                            else
                            {
                                equis.Add("No existen claves que coincidan con lo introducido");
                                return equis;
                            }
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> getCodigoProductos(string prefixText, int count, string contextKey)
        {
            try
            {
                List<string> equis = new List<string>();
                if (contextKey.Equals(""))
                {
                    equis.Add("Favor de seleccionar un Tipo de Producto");
                    return equis;
                }
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnx"].ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT codigo FROM productos WHERE (eliminado IS NULL) AND (idTipoProducto=" + contextKey + ") AND ((LOWER(codigo) LIKE '%" + prefixText + "%') OR " +
                         "(UPPER(codigo) LIKE '%" + prefixText + "%'))", conn))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            if (!dt.Rows.Count.Equals(0))
                            {
                                List<String> list = new List<String>();
                                foreach (DataRow dr in dt.Rows)
                                {
                                    list.Add(dr["codigo"].ToString());
                                }
                                return list;
                            }
                            else
                            {
                                equis.Add("No existen códigos que coincidan con lo introducido");
                                return equis;
                            }
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        protected void txtClaveCliente_TextChanged(object sender, EventArgs e)
        {
            txtNombreCliente.Text = cCl.obtenerNombreByClave(txtClaveCliente.Text);

            if (txtNombreCliente.Text.Equals(""))
                return;
            DataTable dt = cCl.obtenerDatosByClaveCliente(txtClaveCliente.Text);
            ddlFP.SelectedValue = dt.Rows[0]["idFormaPago"].ToString();
            ddlVendedores.SelectedValue = dt.Rows[0]["idVendedor"].ToString();
            llenarProyectos(txtClaveCliente.Text);
            cleanInfoProyectos();
        }

        protected void txtNombreCliente_TextChanged(object sender, EventArgs e)
        {
            cCl.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
            txtClaveCliente.Text = cCl.obtenerClaveByNombre(txtNombreCliente.Text);

            if (txtClaveCliente.Text.Equals(""))
                return;
            DataTable dt = cCl.obtenerDatosByClaveCliente(txtClaveCliente.Text);
            ddlFP.SelectedValue = dt.Rows[0]["idFormaPago"].ToString();
            ddlVendedores.SelectedValue = dt.Rows[0]["idVendedor"].ToString();

            llenarProyectos(txtClaveCliente.Text);
        }
        private void llenarProyectos(string claveClt)
        {
            //ddlProyectos.SelectedIndex = 1;
            ddlProyectos.Items.Clear();
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("nombre");

            dt.Rows.Add("0", "Selecciona un Proyecto");
            DataTable dt1 = cPro.obtenerProyectos(cCl.obtenerIdByClave(claveClt));

            foreach (DataRow dr in dt1.Rows)
            {
                dt.Rows.Add(dr["id"].ToString(), dr["nombre"].ToString());
            }

            ddlProyectos.DataSource = dt;
            ddlProyectos.DataValueField = "id";
            ddlProyectos.DataTextField = "nombre";
            ddlProyectos.DataBind();

            if (ddlProyectos.Items.Count.Equals(1))
            {
                cleanInfoProyectos();
            }
        }
        private void cleanInfoProyectos()
        {
            txtCP.Text = "";
            cbxColonias.Items.Clear();
            txtCalle.Text = "";
            txtNumero.Text = "";
            txtInterior.Text = "";
            ddlProyectos.Focus();
        }

        private void llenarFormasDePago()
        {
            ddlFP.Items.Clear();
            DataTable dt = cFP.obtenerFormasPago();

            ddlFP.DataSource = dt;
            ddlFP.DataValueField = "id";
            ddlFP.DataTextField = "nombre";
            ddlFP.DataBind();
        }
        protected void ddlTipoProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            //hfIdTipoProducto.Value = ddlTipoProducto.SelectedValue;
            //llenarElementos(int.Parse(ddlTipoProducto.SelectedValue));
            //txtCodigoProducto_AutoCompleteExtender.ContextKey = ddlTipoProducto.SelectedValue;
            //txtCodigoProducto.Text = "";
            //lblCodigoProducto.Text = "";
            //lblUDM.Text = "";
            //txtRevenimiento.Text = "";
            //if (ddlTipoProducto.SelectedItem.Text.ToLower().Contains("concre"))
            //{
            //    lblRevenimiento.Visible = true;
            //    txtRevenimiento.Visible = true;
            //}
            //else
            //{
            //    lblRevenimiento.Visible = false;
            //    txtRevenimiento.Visible = false;
            //}
            ////txtCantOrdenada.Text = "0";
            //txtCodigoProducto.Focus();
        }

        protected void llenarElementos(int idTP)
        {
            ddlElemento.Items.Clear();
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("elemento");

            dt.Rows.Add("0", "Selecciona un Elemento");

            cDOS.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
            DataTable dt1 = cDOS.obtenerByTipoProductoAndIdSucursal(idTP);
            if (dt1.Rows.Count >= 1)
            {
                lblElementos.Visible = true;
                ddlElemento.Visible = true;
            }
            else
            {
                lblElementos.Visible = false;
                ddlElemento.Visible = false;
            }
            foreach (DataRow dr in dt1.Rows)
            {
                dt.Rows.Add(dr["id"].ToString(), dr["elemento"].ToString());
            }

            ddlElemento.DataSource = dt;
            ddlElemento.DataValueField = "id";
            ddlElemento.DataTextField = "elemento";
            ddlElemento.DataBind();
        }

        protected void ddlElemento_SelectedIndexChanged(object sender, EventArgs e)
        {
            hfIdElemento.Value = ddlElemento.SelectedValue;
        }

        protected void txtCodigoProducto_TextChanged(object sender, EventArgs e)
        {
            //if (txtCodigoProducto.Text.Equals("Favor de seleccionar un Tipo de Producto"))
            //{
            //    txtCodigoProducto.Text = "";
            //    lblCodigoProducto.Text = "";
            //    ddlTipoProducto.Focus();
            //    return;
            //}
            //if (txtCodigoProducto.Text.Length < 3)
            //    return;

            //cProd.codigo = txtCodigoProducto.Text;
            //cProd.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
            //DataTable dt = cProd.obtenerProductoByCodigo(cProd.codigo);
            //hfIdProducto.Value = dt.Rows[0]["id"].ToString();
            //hfPrecioU.Value = dt.Rows[0]["precio"].ToString();
            //hfIVA.Value = dt.Rows[0]["iva"].ToString();
            //lblUDM.Text = dt.Rows[0]["unidad"].ToString().Trim();
            //lblUDMCamion.Text = dt.Rows[0]["unidad"].ToString().Trim();

            //if (txtCodigoProducto.Text.ToLower().Contains("bba"))
            //{
            //    lblBombaCamion.Visible = true;
            //    ddlBombaCamion.Visible = true;
            //}
            //else
            //{
            //    lblBombaCamion.Visible = false;
            //    ddlBombaCamion.Visible = false;
            //}

            //if (dt.Rows[0]["revenimiento"].ToString().Equals("True"))
            //{
            //    lblRevenimiento.Visible = true;
            //    txtRevenimiento.Visible = true;
            //    txtRevenimiento.Focus();
            //}
            //else
            //{
            //    lblRevenimiento.Visible = false;
            //    txtRevenimiento.Visible = false;
            //    txtCantOrdenada.Focus();
            //}
        }
        private string getHora()
        {
            return cbxHora.Text + ":" + cbxMinutos.Text;
        }

        private bool modSolicitud()
        {
            try
            {
                cSol.idCliente = cCl.obtenerIdByClave(txtClaveCliente.Text);
                if (cSol.idCliente.Equals(0))
                {
                    lblError.Text = "Favor de volver a ingresar el código o nombre del cliente";
                    return false;
                }
                if (cSol.idVendedor.Equals(null))
                {
                    hfIdVendedor.Value = "0";
                }
                cSol.fecha = DateTime.Parse(txtFecha.Text);
                cSol.hora = getHora();
                cSol.idVendedor = int.Parse(hfIdVendedor.Value);
                if (ddlEstadoSolicitud.SelectedValue.Equals(null))
                    ddlEstadoSolicitud.SelectedValue = "0";
                cSol.idEstadoSolicitud = int.Parse(ddlEstadoSolicitud.SelectedValue);
                if (ddlFP.SelectedValue.Equals(null))
                    ddlFP.SelectedValue = "1";
                cSol.idFormaPago = int.Parse(ddlFP.SelectedValue);

                cPro.nombre = txtCalle.Text + " " + txtNumero.Text;

                if (!txtCP.Text.Equals(""))
                    cPro.cp = int.Parse(txtCP.Text);
                if (hfIdEstado.Value.Equals(""))
                {
                    lblError.Text = "Favor de seleccionar el Estado del país al que pertenece el proyecto";
                    return false;
                }
                cPro.idEstado = int.Parse(hfIdEstado.Value);
                if (hfIdCiudad.Value.Equals(""))
                {
                    lblError.Text = "Favor de seleccionar la ciudad al que pertenece el proyecto";
                    return false;
                }
                cPro.idCiudad = int.Parse(hfIdCiudad.Value);
                if (cbxColonias.Text.Equals(""))
                {
                    lblError.Text = "Favor de seleccionar una colonia o ingresar el nombre de la colonia para agregarla al sistema";
                    return false;
                }
                cCP.asenta = cbxColonias.SelectedItem.Text;
                if (!cCP.existe() && !cCP.cp.Equals(0))
                {
                    cCP.cp = int.Parse(txtCP.Text);
                    cCP.idEstado = int.Parse(hfIdEstado.Value);
                    cCP.idCiudad = int.Parse(hfIdCiudad.Value);
                    cCP.insertar();
                }
                cPro.colonia = cbxColonias.SelectedItem.Text;
                if (txtCalle.Text.Equals(""))
                {
                    lblError.Text = "Favor de introducir la calle del proyecto";
                    return false;
                }
                cPro.calle = txtCalle.Text;
                if (txtNumero.Text.Equals(""))
                {
                    lblError.Text = "Favor de introducir el número exterior del proyecto";
                    return false;
                }
                if(ddlReqFac.SelectedValue == "")
                {
                    lblError.Text = "Favor de Seleccionar si Requiere Factura";
                    return false;
                }
                cPro.numero = int.Parse(txtNumero.Text);
                cPro.interior = txtInterior.Text;

                cPro.idCliente = cSol.idCliente;

                cPro.id = int.Parse(ddlProyectos.SelectedValue);
                if (cPro.existe())
                {
                    cPro.actualizar(cUtl.idUsuarioActivo);
                    cSol.idProyecto = cPro.id;
                }
                else
                {
                    cPro.insertar(cUtl.idUsuarioActivo);
                    cSol.idProyecto = cPro.obtenerProyectoByCPCalleNumeroLIKE(cPro.cp, cPro.calle, cPro.numero, cSol.idCliente);
                    if (cSol.idProyecto.Equals(0))
                        cSol.idProyecto = cPro.obtenerProyectoByCPCalleNumeroEQUAL(cPro.cp, cPro.calle, cPro.numero, cSol.idCliente);
                }
                try
                {
                    cSol.reqFac = ddlReqFac.SelectedValue;
                    cSol.actualizarDesdeDosificacion(int.Parse(Request.Cookies["ksroc"]["id"]), int.Parse(hfIdSolicitud.Value));
                    //modifica el requiere factura en la orden 
                    cOrd.reqFac = ddlReqFac.SelectedValue;
                    cOrd.actualizarReqFac(int.Parse(Request.QueryString["idO"]), int.Parse(Request.Cookies["ksroc"]["id"]));
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool modDS()
        {
            try
            {
                cDS.id = int.Parse(Request.QueryString["idDS"]);
                cDS.obtenerDetalleSolicitudByID();
                cProd.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                if (ddlElemento.Visible)
                {
                    cDS.idElemento = int.Parse(hfIdElemento.Value);
                }
                //if (txtCodigoProducto.Text.Equals(""))
                if (lblCodigoProducto.Text.Equals(""))
                    return false;

                //cProd.codigo = txtCodigoProducto.Text;
                cProd.codigo = lblCodigoProducto.Text;
                cProd.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                if (hfIdProducto.Value.Equals(""))
                {
                    DataTable dt = cProd.obtenerProductoByCodigo(cProd.codigo);
                    hfIdProducto.Value = dt.Rows[0]["id"].ToString();
                }
                cDS.idProducto = int.Parse(hfIdProducto.Value);

                if (txtCantOrdenada.Text.Equals(""))
                    return false;
                cDS.cantidad = txtCantOrdenada.Text;
                if (txtRevenimiento.Text.Equals(""))
                    txtRevenimiento.Text = "0";
                cDS.revenimiento = txtRevenimiento.Text;

                //DataTable dt = cProd.getProductoByID(int.Parse(hfIdProducto.Value));
                hfIVA.Value = cDS.iva;
                if (string.IsNullOrEmpty(cDS.iva))
                    hfIVA.Value = "0";

                decimal precioF, subtotal, iva, total;
                precioF = decimal.Parse(txtPrecioF.Text);
                subtotal = decimal.Parse(txtCantOrdenada.Text) * decimal.Parse(precioF.ToString());
                iva = decimal.Parse(hfIVA.Value) * subtotal;
                total = subtotal + iva;

                cDS.precioF = precioF.ToString("0.00");
                cDS.subtotal = subtotal.ToString("0.00");
                cDS.total = total.ToString("0.00");
                cDS.actualizarCantSolicitada(int.Parse(hfIdDS.Value), int.Parse(Request.Cookies["ksroc"]["id"]));
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool modO()
        {
            try
            {
                cDS.id = int.Parse(Request.QueryString["idDS"]);
                cDS.obtenerDetalleSolicitudByID();
                cProd.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                if (ddlElemento.Visible)
                {
                    cDS.idElemento = int.Parse(hfIdElemento.Value);
                }
                //if (txtCodigoProducto.Text.Equals(""))
                if (lblCodigoProducto.Text.Equals(""))
                    return false;

                //cProd.codigo = txtCodigoProducto.Text;
                cProd.codigo = lblCodigoProducto.Text;
                cProd.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                if (hfIdProducto.Value.Equals(""))
                {
                    DataTable dt = cProd.obtenerProductoByCodigo(cProd.codigo);
                    hfIdProducto.Value = dt.Rows[0]["id"].ToString();
                }
                cDS.idProducto = int.Parse(hfIdProducto.Value);

                if (txtCantOrdenada.Text.Equals(""))
                    return false;
                cDS.cantidad = txtCantOrdenada.Text;
                if (txtRevenimiento.Text.Equals(""))
                    txtRevenimiento.Text = "0";
                cDS.revenimiento = txtRevenimiento.Text;

                //DataTable dt = cProd.getProductoByID(int.Parse(hfIdProducto.Value));
                hfIVA.Value = cDS.iva;
                if (string.IsNullOrEmpty(cDS.iva))
                    hfIVA.Value = "0";

                decimal precioF, subtotal, iva, total;
                precioF = decimal.Parse(txtPrecioF.Text);
                subtotal = decimal.Parse(txtCantOrdenada.Text) * decimal.Parse(precioF.ToString());
                iva = decimal.Parse(hfIVA.Value) * subtotal;
                total = subtotal + iva;

                cDS.precioF = precioF.ToString("0.00");
                cDS.subtotal = subtotal.ToString("0.00");
                cDS.total = total.ToString("0.00");
                cDS.actualizarCantSolicitada(int.Parse(hfIdDS.Value), int.Parse(Request.Cookies["ksroc"]["id"]));
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //protected void llenarTP()
        //{
        //    ddlTipoProducto.Items.Clear();
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("id");
        //    dt.Columns.Add("tipo");

        //    dt.Rows.Add("0", "Selecciona un Tipo de Producto");

        //    cDOS.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
        //    DataTable dt1 = cDOS.obtenerTPByIdSucursal();
        //    foreach (DataRow dr in dt1.Rows)
        //    {
        //        dt.Rows.Add(dr["id"].ToString(), dr["tipo"].ToString());
        //    }

        //    ddlTipoProducto.DataSource = dt;
        //    ddlTipoProducto.DataValueField = "id";
        //    ddlTipoProducto.DataTextField = "tipo";
        //    ddlTipoProducto.DataBind();
        //}

        protected void ddlProyectos_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillProyecto(int.Parse(ddlProyectos.SelectedValue));
        }

        private void fillProyecto(int idP = 0)
        {
            if (!idP.Equals(0))
                hfIdProyecto.Value = idP.ToString();
            if (ddlProyectos.SelectedValue.Equals("") || ddlProyectos.SelectedValue.Equals("0"))
            {
                //Response.Cookies["Solicitud"]["idProyecto"] = "0";
                hfIdProyecto.Value = "0";
                cleanInfoProyectos();
                txtCP.Focus();
            }
            else
            {
                DataTable dt = cPro.obtenerProyectosByID(int.Parse(hfIdProyecto.Value));
                txtCP.Text = dt.Rows[0]["cp"].ToString();
                if (txtCP.Text.Equals("") || txtCP.Text.Equals("0"))
                {
                    hfIdEstado.Value = dt.Rows[0]["idEstado"].ToString();
                    ddlEstados.SelectedValue = hfIdEstado.Value;
                    llenarCiudades(int.Parse(ddlEstados.SelectedValue));
                    hfIdCiudad.Value = dt.Rows[0]["idCiudad"].ToString();
                    ddlCiudades.SelectedValue = hfIdCiudad.Value;
                    hfSearchBy.Value = "cd";
                    cbxColonias.Items.Clear();
                    ListItem li = new ListItem();
                    li.Value = dt.Rows[0]["colonia"].ToString();
                    li.Text = dt.Rows[0]["colonia"].ToString();
                    cbxColonias.Items.Add(li);

                }
                else
                {
                    fillColonias(int.Parse(txtCP.Text), true);
                }
                try
                {
                    cbxColonias.SelectedValue = dt.Rows[0]["colonia"].ToString();
                }
                catch (Exception)
                {
                    ListItem li = new ListItem();
                    li.Text = dt.Rows[0]["colonia"].ToString();
                    li.Value = dt.Rows[0]["colonia"].ToString();
                    cbxColonias.Items.Add(li);
                    cbxColonias.SelectedValue = dt.Rows[0]["colonia"].ToString();
                }
                txtCalle.Text = dt.Rows[0]["calle"].ToString();
                txtNumero.Text = dt.Rows[0]["numero"].ToString();
                txtInterior.Text = dt.Rows[0]["interior"].ToString();
            }
        }
        private void fillColonias(int value, bool hasCP)
        {
            cbxColonias.Items.Clear();
            DataTable dt = new DataTable();
            if (hasCP)
                dt = cDOS.getColoniasByCP(value);
            else
                dt = cDOS.getColoniasByIdC(value);

            cbxColonias.DataSource = dt;
            cbxColonias.DataValueField = "asenta";
            cbxColonias.DataTextField = "asenta";
            cbxColonias.DataBind();


            if (hasCP)
            {
                hfIdEstado.Value = dt.Rows[0]["idEstado"].ToString();
                ddlEstados.SelectedValue = hfIdEstado.Value;
                llenarCiudades(int.Parse(ddlEstados.SelectedValue));
                hfIdCiudad.Value = dt.Rows[0]["idCiudad"].ToString();
                ddlCiudades.SelectedValue = hfIdCiudad.Value;
            }
            cbxColonias.Focus();
        }
        protected void llenarEstados()
        {
            ddlEstados.Items.Clear();
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("estado");

            dt.Rows.Add("0", "Selecciona un Estado");

            DataTable dt1 = cDOS.obtenerEstados();
            foreach (DataRow dr in dt1.Rows)
            {
                dt.Rows.Add(dr["id"].ToString(), dr["estado"].ToString());
            }

            ddlEstados.DataSource = dt;
            ddlEstados.DataValueField = "id";
            ddlEstados.DataTextField = "estado";
            ddlEstados.DataBind();
        }
        protected void llenarCiudades(int idEst)
        {
            ddlCiudades.Items.Clear();
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("ciudad");

            DataTable dt1 = cDOS.obtenerCiudadesByIdEstado(idEst);
            foreach (DataRow dr in dt1.Rows)
            {
                dt.Rows.Add(dr["id"].ToString(), dr["ciudad"].ToString());
            }

            ddlCiudades.DataSource = dt;
            ddlCiudades.DataValueField = "id";
            ddlCiudades.DataTextField = "ciudad";
            ddlCiudades.DataBind();
        }

        protected void ddlFP_SelectedIndexChanged(object sender, EventArgs e)
        {
            hfIdFP.Value = ddlFP.SelectedValue;
        }

        protected void ddlVendedores_SelectedIndexChanged(object sender, EventArgs e)
        {
            hfIdVendedor.Value = ddlVendedores.SelectedValue;
        }

        protected void txtCP_TextChanged(object sender, EventArgs e)
        {
            if (hfSearchBy.Value.Equals("cd"))
                return;
            hfSearchBy.Value = "cp";
            if (txtCP.Text.Length == 5)
            {
                fillColonias(int.Parse(txtCP.Text), true);
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
            fillColonias(int.Parse(hfIdCiudad.Value), false);
        }

        protected void cbxColonias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (hfSearchBy.Value.Equals("cd"))
            {
                txtCP.Text = cDOS.obtenerCPByAsentaAndIdC(cbxColonias.SelectedItem.Text, int.Parse(hfIdCiudad.Value));
                txtCalle.Focus();
            }
        }
        protected void llenarDdlVendedores()
        {
            ddlVendedores.Items.Clear();
            ddlVendedorOrden.Items.Clear();
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("nombre");

            dt.Rows.Add("0", "Selecciona un Vendedor");

            //DataTable dt1 = cUsr.obtenerUsuariosActivosByPefilAndSucursal(2, int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
            DataTable dt1 = cUsr.obtenerUsuariosBySucursal(int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
            foreach (DataRow dr in dt1.Rows)
            {
                dt.Rows.Add(dr["id"].ToString(), dr["nombre"].ToString());
            }

            ddlVendedores.DataSource = dt;
            ddlVendedores.DataValueField = "id";
            ddlVendedores.DataTextField = "nombre";
            ddlVendedores.DataBind();

            ddlVendedorOrden.DataSource = dt;
            ddlVendedorOrden.DataValueField = "id";
            ddlVendedorOrden.DataTextField = "nombre";
            ddlVendedorOrden.DataBind();
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            float qtyDB = cDOS.obtenerQtyByOrdenAndIdProductoDosificada(int.Parse(hfIdOrden.Value), int.Parse(hfIdProducto.Value));
            if (float.Parse(txtCantOrdenada.Text) < qtyDB)
            {
                mlblTitle.Text = "¡¡¡Atención!!!";
                mlblMessage.Text = "La cantidad es menor a la cantidad qye ya se ha entregado de la Orden";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ShowPopup();", true);
                return;
            }
            if (!modSolicitud())
                return;
            if (!modDS())
                return;

            if (!string.IsNullOrEmpty(hfidOD.Value))
            {
                cOD.fecha = DateTime.Parse(txtFecha.Text);
                cOD.hora = getHora();
                cOD.actualizarODFH(int.Parse(hfidOD.Value), int.Parse(Request.Cookies["ksroc"]["id"]));
            }

            cOrd.idVendedor = int.Parse(ddlVendedorOrden.SelectedValue);
            cOrd.fecha = DateTime.Parse(txtFecha.Text);
            cOrd.hora = getHora();
            cOrd.comentarios = txtObservaciones.Text;
            cOrd.actualizarOFHC(int.Parse(hfIdOrden.Value), int.Parse(Request.Cookies["ksroc"]["id"]));

            cUtl.idUsuarioActivo = 0;
            cUtl.idSucursalActiva = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);

            //Generar alerta random
            cUtl.motivo = "Se ha actualizado información de una orden desde dosificación ¿Deseas actualizar?";
            cUtl.generarAlerta();

            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "window", "window.opener.parent.location.reload();", true);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "window", "window.opener.location.href='programacion.aspx';", true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "cerrar", "cerrar();", true);
        }


        protected void bntCancelar_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "window", "javascript:window.close();", true);
        }

        protected void mbtnAceptar_Click(object sender, EventArgs e)
        {

            mbtnClose.Visible = true;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopup();", true);
        }

        protected void mbtnClose_Click(object sender, EventArgs e)
        {
            //txtCargaCamion.Text = (float.Parse(txtCargaCamion.Text) - float.Parse(hfSupera.Value)).ToString();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopup();", true);
        }

    }
}