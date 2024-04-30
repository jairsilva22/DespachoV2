using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class solicitudesMod : System.Web.UI.Page
    {
        cSolicitudes cSol = new cSolicitudes();
        cClientes cClt = new cClientes();
        cUtilidades cUtl = new cUtilidades();
        cProyectos cPro = new cProyectos();
        cProductos cProd = new cProductos();
        cTipoProducto cTP = new cTipoProducto();
        cProductosFactor cPF = new cProductosFactor();
        cUsuarios cUsr = new cUsuarios();
        cDetallesSolicitud cDS = new cDetallesSolicitud();
        cFormasPago cFP = new cFormasPago();
        cElementos cEl = new cElementos();
        cCodigosPostales cCP = new cCodigosPostales();
        cEstados cEst = new cEstados();
        cCiudades cCd = new cCiudades();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                cUtl.idUsuarioActivo = int.Parse(Request.Cookies["ksroc"]["id"]);
                txtClaveCliente_AutoCompleteExtender.ContextKey = Request.Cookies["ksroc"]["idSucursal"];
                txtNombreCliente_AutoCompleteExtender.ContextKey = Request.Cookies["ksroc"]["idSucursal"];
                hfIdSolicitud.Value = Request.QueryString["id"];
                //if (!lblProyecto.Text.Equals(""))
                //    ddlProyectos.SelectedValue = lblProyecto.Text;
                if (!IsPostBack)
                {
                    cargarControles();
                    getSolicitud();
                }
            }
            catch (Exception)
            {

            }
        }
        private void getSolicitud()
        {
            cSol.obtenerSolicitudByID(int.Parse(hfIdSolicitud.Value));
            ddlReqFac.SelectedValue = cSol.reqFac;
            txtFecha.Text = cSol.fecha.ToString().Substring(0, 10);
            fillHora(cSol.hora);
            lblFolio.Text = cSol.folio.ToString();
            hfIdVendedor.Value = cSol.idVendedor.ToString();
            ddlVendedores.SelectedValue = hfIdVendedor.Value;
            ddlEstadoSolicitud.SelectedValue = cSol.idEstadoSolicitud.ToString();
            txtClaveCliente.Text = cClt.obtenerClaveByID(cSol.idCliente);
            txtNombreCliente.Text = cClt.obtenerNombreByClave(txtClaveCliente.Text);
            hfIdFP.Value = cSol.idFormaPago.ToString();
            ddlFP.SelectedValue = hfIdFP.Value;
            llenarProyectos(txtClaveCliente.Text);
            fillProyecto(cSol.idProyecto);
            ddlProyectos.SelectedValue = cSol.idProyecto.ToString();
            lblProyecto.Text = cSol.idProyecto.ToString();
            hfIdVendedor.Value = cSol.idVendedor.ToString();
            ddlVendedores.SelectedValue = hfIdVendedor.Value;
            pnlDetalleProductos.Visible = true;
            llenarDetalleSolicitud(int.Parse(hfIdSolicitud.Value));
        }
        private void fillHora(string sHora)
        {
            cbxHora.SelectedValue = sHora.Substring(0, 2);
            cbxMinutos.SelectedValue = sHora.Substring(3, 2);
        }

        protected void cargarControles()
        {
            llenarDdlVendedores();
            llenarFormasDePago();
            llenarTP();
            llenarEstados();
        }
        private void fillProyecto(int idP = 0)
        {
            if (!idP.Equals(0))
                hfIdProyecto.Value = idP.ToString();
            if (hfIdProyecto.Value.Equals("") || hfIdProyecto.Value.Equals("0"))
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
                    fillColonias(int.Parse(txtCP.Text), true, 0);
                }
                try
                {
                    cbxColonias.SelectedValue = dt.Rows[0]["colonia"].ToString();
                }
                catch (Exception ex)
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

        protected void llenarDdlVendedores()
        {
            ddlVendedores.Items.Clear();
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
        }
        protected void llenarTP()
        {
            ddlTipoProducto.Items.Clear();
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("tipo");

            dt.Rows.Add("0", "Selecciona un Tipo de Producto");

            cTP.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
            DataTable dt1 = cTP.obtenerByIdSucursal();
            foreach (DataRow dr in dt1.Rows)
            {
                dt.Rows.Add(dr["id"].ToString(), dr["tipo"].ToString());
            }

            ddlTipoProducto.DataSource = dt;
            ddlTipoProducto.DataValueField = "id";
            ddlTipoProducto.DataTextField = "tipo";
            ddlTipoProducto.DataBind();
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
        }
        protected void llenarCiudades(int idEst)
        {
            ddlCiudades.Items.Clear();
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("ciudad");

            //dt.Rows.Add("0", "Selecciona un Estado");

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

        protected void txtNombreCliente_TextChanged(object sender, EventArgs e)
        {
            bool modal = false;
            cClt.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
            cClt.nombre = txtNombreCliente.Text;
            txtClaveCliente.Text = cClt.obtenerClaveByNombre(cClt.nombre);

            if (txtClaveCliente.Text.Equals(""))
                return;
            DataTable dt = cFP.obtenerDatosByClaveCliente(txtClaveCliente.Text);
            ddlFP.SelectedValue = dt.Rows[0]["idFormaPago"].ToString();
            hfIdFP.Value = ddlFP.SelectedValue;
            try
            {
                ddlVendedores.SelectedValue = dt.Rows[0]["idVendedor"].ToString();
                hfIdVendedor.Value = ddlVendedores.SelectedValue;
            }
            catch (Exception)
            {
                modal = true;
                ddlVendedores.SelectedIndex = 0;
                this.mlblTitle.Text = "¡¡¡ATENCIÓN!!!";
                this.mlblMessage.Text = "El vendedor que tiene asignado el cliente ya no está activo en el sistema, favor de asignar un nuevo vendedor";
            }
            llenarProyectos(txtClaveCliente.Text);
            if (modal)
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ShowPopup();", true);
        }

        private void crearCookies()
        {
            //Escribimos la variables de sesion
            Response.Cookies["Solicitud"]["idCliente"] = "";
            Response.Cookies["Solicitud"]["idVendedor"] = "";
            Response.Cookies["Solicitud"]["idProyecto"] = "";
            Response.Cookies["Solicitud"].Expires = DateTime.Now.AddMinutes(120);
        }
        private void llenarProyectos(string claveClt)
        {
            //ddlProyectos.SelectedIndex = 1;
            ddlProyectos.Items.Clear();
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("nombre");

            dt.Rows.Add("0", "Selecciona un Proyecto");
            DataTable dt1 = cPro.obtenerProyectos(cClt.obtenerIdByClave(claveClt));

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
        private void llenarFormasDePago()
        {
            ddlFP.Items.Clear();
            DataTable dt = cFP.obtenerFormasPago();

            ddlFP.DataSource = dt;
            ddlFP.DataValueField = "id";
            ddlFP.DataTextField = "nombre";
            ddlFP.DataBind();
        }
        protected void llenarElementos(int idTP)
        {
            ddlElemento.Items.Clear();
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("elemento");

            dt.Rows.Add("0", "Selecciona un Elemento");

            cEl.idTipoProducto = idTP;
            cEl.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
            DataTable dt1 = cEl.obtenerByTipoProductoAndIdSucursal();
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
        protected void llenarFactores(int idTP)
        {
            ddlFactor.Items.Clear();
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("factor");

            dt.Rows.Add("0", "Selecciona un Factor");

            cPF.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
            DataTable dt1 = cPF.obtenerByTP(idTP);
            foreach (DataRow dr in dt1.Rows)
            {
                dt.Rows.Add(dr["id"].ToString(), dr["factor"].ToString() + " - " + dr["porcentaje"].ToString());
            }

            ddlFactor.DataSource = dt;
            ddlFactor.DataValueField = "id";
            ddlFactor.DataTextField = "factor";
            ddlFactor.DataBind();
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

        protected void ddlProyectos_SelectedIndexChanged(object sender, EventArgs e)
        {
            hfIdProyecto.Value = ddlProyectos.SelectedValue;
            fillProyecto();
        }

        protected void txtClaveCliente_TextChanged(object sender, EventArgs e)
        {
            bool modal = false;
            cClt.clave = txtClaveCliente.Text;
            txtNombreCliente.Text = cClt.obtenerNombreByClave(txtClaveCliente.Text);

            if (txtNombreCliente.Text.Equals(""))
                return;
            DataTable dt = cFP.obtenerDatosByClaveCliente(txtClaveCliente.Text);
            ddlFP.SelectedValue = dt.Rows[0]["idFormaPago"].ToString();
            hfIdFP.Value = ddlFP.SelectedValue;
            try
            {
                ddlVendedores.SelectedValue = dt.Rows[0]["idVendedor"].ToString();
                hfIdVendedor.Value = ddlVendedores.SelectedValue;
            }
            catch (Exception)
            {
                modal = true;
                ddlVendedores.SelectedIndex = 0;
                this.mlblTitle.Text = "¡¡¡ATENCIÓN!!!";
                this.mlblMessage.Text = "El vendedor que tiene asignado el cliente ya no está activo en el sistema, favor de asignar un nuevo vendedor";
            }
            llenarProyectos(cClt.clave);
            cleanInfoProyectos();
            if (modal)
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ShowPopup();", true);
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
            DataTable dt = new DataTable();
            if (hasCP)
                dt = cCP.getColoniasByCP(value);
            else
                dt = cCP.getColoniasByIdC(value, idE);

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

        protected void txtCodigoProducto_TextChanged(object sender, EventArgs e)
        {
            if (txtCodigoProducto.Text.Equals("Favor de seleccionar un Tipo de Producto"))
            {
                txtCodigoProducto.Text = "";
                ddlTipoProducto.Focus();
                return;
            }
            if (txtCodigoProducto.Text.Length < 3)
                return;

            cProd.codigo = txtCodigoProducto.Text;
            cProd.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
            DataTable dt = cProd.obtenerProductoByCodigo(cProd.codigo);

            txtDescProducto.Text = dt.Rows[0]["descripcion"].ToString();
            lblUDM.Text = dt.Rows[0]["unidad"].ToString();
            lblPrecioU2.Text = dt.Rows[0]["precio"].ToString();
            lblPrecioU.Text = string.Format("{0:C}", decimal.Parse(lblPrecioU2.Text));
            txtPrecioFactor2.Text = dt.Rows[0]["precio"].ToString();
            txtPrecioFactor.Text = string.Format("{0:C}", decimal.Parse(txtPrecioFactor2.Text));
            txtCantOrdenada.Text = "0";
            txtIVA.Text = dt.Rows[0]["iva"].ToString();
            lblSubTotal2.Text = "0";
            lblSubTotal.Text = string.Format("{0:C}", decimal.Parse(lblSubTotal2.Text));
            lblTotal2.Text = "0";
            lblTotal.Text = string.Format("{0:C}", decimal.Parse(lblTotal2.Text));

            if (dt.Rows[0]["revenimiento"].ToString().Equals("True"))
            {
                lblRevenimiento.Visible = true;
                txtRevenimiento.Visible = true;
                txtRevenimiento.Focus();
            }
            else
            {
                lblRevenimiento.Visible = false;
                txtRevenimiento.Visible = false;
                txtCantOrdenada.Focus();
            }
        }

        protected void lvDetalles_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                string[] arr;
                arr = e.CommandArgument.ToString().Split('ˇ');
                hfIdDS.Value = arr[0];

                if (e.CommandName.Equals("modificar"))
                {
                    btnInsertarProducto.Visible = false;
                    btnModProducto.Visible = true;

                    ddlTipoProducto.SelectedValue = arr[1];
                    if (!arr[2].Equals("") || !arr[2].Equals(null))
                    {
                        try
                        {
                            llenarElementos(int.Parse(arr[1]));
                            ddlElemento.SelectedValue = arr[2];
                            lblElementos.Visible = true;
                            ddlElemento.Visible = true;
                        }
                        catch (Exception)
                        {

                        }
                        hfIdElemento.Value = arr[2];
                    }
                    txtCodigoProducto.Text = arr[3];
                    txtDescProducto.Text = arr[4];
                    lblUDM.Text = arr[5];
                    txtRevenimiento.Text = arr[6];
                    if (txtRevenimiento.Text.Equals("") || txtRevenimiento.Text.Equals("0"))
                    {
                        lblRevenimiento.Visible = false;
                        txtRevenimiento.Visible = false;
                    }
                    else
                    {
                        lblRevenimiento.Visible = true;
                        txtRevenimiento.Visible = true;
                    }
                    lblPrecioU2.Text = arr[7];
                    lblPrecioU.Text = string.Format("{0:C}", decimal.Parse(lblPrecioU2.Text));
                    llenarFactores(int.Parse(arr[1]));
                    if (!arr[8].Equals("1"))
                    {
                        ddlFactor.SelectedValue = arr[8];
                    }
                    else
                        ddlFactor.SelectedIndex = 0;
                    txtPrecioFactor2.Text = arr[9];
                    txtPrecioFactor.Text = string.Format("{0:C}", decimal.Parse(txtPrecioFactor2.Text));
                    txtCantOrdenada.Text = arr[10];
                    lblSubTotal2.Text = arr[11];
                    lblSubTotal.Text = string.Format("{0:C}", decimal.Parse(lblSubTotal2.Text));
                    txtIVA.Text = arr[12];
                    lblTotal2.Text = arr[13];
                    lblTotal.Text = string.Format("{0:C}", decimal.Parse(lblTotal2.Text));
                }
                if (e.CommandName.Equals("eliminar"))
                {
                    cDS.eliminar(int.Parse(hfIdDS.Value), int.Parse(Request.Cookies["ksroc"]["id"]));
                    llenarDetalleSolicitud(int.Parse(hfIdSolicitud.Value));
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private decimal calcular()
        {
            decimal porcentaje = decimal.Parse(getPorcentaje());
            decimal precioUnitario = decimal.Parse(lblPrecioU2.Text);

            if (porcentaje < 1)
                return precioUnitario - (precioUnitario * porcentaje);
            else
                return precioUnitario * porcentaje;
        }

        private string getPorcentaje()
        {
            string[] arr;
            if (ddlFactor.SelectedValue.Equals("0") || ddlFactor.SelectedIndex.Equals(0))
            {
                return "0";
            }
            arr = ddlFactor.SelectedItem.Text.Split('-');
            return arr[1].Substring(1, arr[1].Length - 1).ToString();
        }
        protected void btnInsertarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                cProd.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                cDS.idSolicitud = int.Parse(hfIdSolicitud.Value);
                if (ddlTipoProducto.SelectedValue.Equals("") || ddlTipoProducto.SelectedIndex.Equals(0))
                {
                    lblError.Text = "Favor de seleccionar un Tipo de producto";
                    return;
                }
                if (ddlElemento.Items.Count > 1)
                {
                    if (ddlElemento.SelectedValue.Equals("") || ddlElemento.SelectedIndex.Equals(0))
                    {
                        lblError.Text = "Favor de seleccionar un Elemento";
                        return;
                    }
                    cDS.idElemento = int.Parse(hfIdElemento.Value);
                }
                else
                {
                    cDS.idElemento = 1;
                }
                if (txtCodigoProducto.Text.Equals(""))
                {
                    lblError.Text = "Favor de introducir un código de producto";
                    return;
                }
                cDS.idProducto = cProd.obtenerIDProductoByCodigo(txtCodigoProducto.Text);
                if (txtCantOrdenada.Text.Equals(""))
                {
                    lblError.Text = "Favor de introducir una cantidad de producto";
                    return;
                }
                cDS.cantidad = txtCantOrdenada.Text;
                cDS.tamano = "0";
                cDS.cantidadEntregada = "0";
                if (txtRevenimiento.Text.Equals(""))
                    txtRevenimiento.Text = "0";
                cDS.revenimiento = txtRevenimiento.Text;
                if (lblPrecioU2.Text.Equals(""))
                    return;
                cDS.precioU = lblPrecioU2.Text;
                if (ddlFactor.SelectedIndex.Equals(0))
                    cDS.idFactor = 1;
                else
                    cDS.idFactor = int.Parse(ddlFactor.SelectedValue);
                cDS.precioF = txtPrecioFactor2.Text;
                cDS.subtotal = lblSubTotal2.Text;
                if (txtIVA.Text.Equals(""))
                    cDS.iva = "0";
                else
                    cDS.iva = txtIVA.Text;
                cDS.total = lblTotal2.Text;

                if (!cDS.existe())
                    cDS.insertar(cUtl.idUsuarioActivo);
                else
                {
                    lblError.Text = "Ya existe éste producto en el detalle de la solicitud";
                    return;
                }

                cleanDataDetalle();
                llenarDetalleSolicitud(cDS.idSolicitud);
            }
            catch (Exception ex)
            {
                if (ex.Message == "ok")
                {

                    //script que te abre el modal 
                    string errorMessage = "Producto agregado correctamente ";
                    ScriptManager.RegisterStartupScript(this, GetType(), "showErrorModal", $"$('#errorMessage').text('{errorMessage}'); $('#errorModal').modal('show');", true);
                    cleanDataDetalle();
                    getSolicitud();

                }
                else
                {
                    string errorMessage = "Error: " + ex.Message;
                    ScriptManager.RegisterStartupScript(this, GetType(), "showErrorModal", $"$('#errorMessage').text('{errorMessage}'); $('#errorModal').modal('show');", true);
                }
            }
        }

        private void cleanDataDetalle()
        {
            ddlTipoProducto.SelectedIndex = 0;
            lblElementos.Visible = false;
            ddlElemento.Visible = false;
            txtCodigoProducto.Text = "";
            txtDescProducto.Text = "";
            lblUDM.Text = "";
            txtRevenimiento.Text = "";
            lblRevenimiento.Visible = false;
            txtRevenimiento.Visible = false;
            lblPrecioU2.Text = "0";
            lblPrecioU.Text = string.Format("{0:C}", decimal.Parse(lblPrecioU2.Text));
            ddlFactor.Items.Clear();
            txtPrecioFactor2.Text = "0";
            txtPrecioFactor.Text = string.Format("{0:C}", decimal.Parse(txtPrecioFactor2.Text));
            txtCantOrdenada.Text = "0";
            lblSubTotal2.Text = "0";
            lblSubTotal.Text = string.Format("{0:C}", decimal.Parse(lblSubTotal2.Text));
            txtIVA.Text = "0";
            lblTotal2.Text = "0";
            lblTotal.Text = string.Format("{0:C}", decimal.Parse(lblTotal2.Text));
            txtCodigoProducto_AutoCompleteExtender.ContextKey = "";
            btnInsertarProducto.Visible = true;
            btnModProducto.Visible = false;
            txtCodigoProducto.Focus();
        }

        private void llenarDetalleSolicitud(int idS)
        {
            lvDetalles.DataSource = cDS.obtenerDetallesSolicitud(idS);
            lvDetalles.DataBind();
        }

        protected void ddlVendedores_SelectedIndexChanged(object sender, EventArgs e)
        {
            hfIdVendedor.Value = ddlVendedores.SelectedValue;
        }

        protected void btnAgregarDetalle_Click(object sender, EventArgs e)
        {
            cSol.fecha = DateTime.Parse(txtFecha.Text);
            cSol.hora = getHora();
            hfIdCliente.Value = cClt.obtenerIdByClave(txtClaveCliente.Text).ToString();
            cSol.idCliente = int.Parse(hfIdCliente.Value);
            if (cSol.idVendedor.Equals(null))
            {
                hfIdVendedor.Value = "0";
            }
            cSol.idVendedor = int.Parse(hfIdVendedor.Value);
            if (ddlEstadoSolicitud.SelectedValue.Equals(null))
                ddlEstadoSolicitud.SelectedValue = "0";
            cSol.idEstadoSolicitud = int.Parse(ddlEstadoSolicitud.SelectedValue);
            if (ddlFP.SelectedValue.Equals(null))
                ddlFP.SelectedValue = "1";
            cSol.idFormaPago = int.Parse(ddlFP.SelectedValue);
            if (hfIdProyecto.Value.Equals(null) || hfIdProyecto.Value.Equals("") || hfIdProyecto.Value.Equals("0"))
            {
                cPro.nombre = txtCalle.Text + " " + txtNumero.Text;

                if (!txtCP.Text.Equals(""))
                    cPro.cp = int.Parse(txtCP.Text);
                if (hfIdEstado.Value.Equals(""))
                {
                    lblError.Text = "Favor de seleccionar el Estado del país al que pertenece el proyecto";
                    return;
                }
                cPro.idEstado = int.Parse(hfIdEstado.Value);
                if (hfIdCiudad.Value.Equals(""))
                {
                    lblError.Text = "Favor de seleccionar la ciudad al que pertenece el proyecto";
                    return;
                }
                cPro.idCiudad = int.Parse(hfIdCiudad.Value);
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
                cPro.colonia = cbxColonias.SelectedItem.Text;
                if (txtCalle.Text.Equals(""))
                {
                    lblError.Text = "Favor de introducir la calle del proyecto";
                    return;
                }
                cPro.calle = txtCalle.Text;
                if (txtNumero.Text.Equals(""))
                {
                    lblError.Text = "Favor de introducir el número exterior del proyecto";
                    return;
                }
                cPro.numero = int.Parse(txtNumero.Text);
                cPro.interior = txtInterior.Text;

                cPro.idCliente = cSol.idCliente;
                cPro.insertar(cUtl.idUsuarioActivo);
                cSol.idProyecto = cPro.obtenerProyectoByCPCalleNumeroLIKE(cPro.cp, cPro.calle, cPro.numero, cSol.idCliente);
                if (cSol.idProyecto.Equals(0))
                    cSol.idProyecto = cPro.obtenerProyectoByCPCalleNumeroEQUAL(cPro.cp, cPro.calle, cPro.numero, cSol.idCliente);
            }
            else
            {
                cSol.idProyecto = int.Parse(hfIdProyecto.Value);
            }
            if(ddlReqFac.SelectedValue == "")
            {
                lblError.Text = "Favor de Seleccionar si Requiere Factura";
                return;
            }
            cSol.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"].ToString());
            try
            {
                cSol.reqFac = ddlReqFac.SelectedValue;
                cSol.actualizar(cUtl.idUsuarioActivo, int.Parse(Request.QueryString["id"]));
                lblError.Text = "Los datos de la solicitud se actualizaron...";
            }
            catch (Exception)
            {

            }
        }

        private string getHora()
        {
            return cbxHora.Text + ":" + cbxMinutos.Text;
        }

        protected void ddlFP_SelectedIndexChanged(object sender, EventArgs e)
        {
            hfIdFP.Value = ddlFP.SelectedValue;
        }

        protected void btnModProducto_Click(object sender, EventArgs e)
        {
            cProd.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
            if (ddlElemento.Visible)
            {
                cDS.idElemento = int.Parse(hfIdElemento.Value);
            }
            if (txtCodigoProducto.Text.Equals(""))
                return;
            cDS.idProducto = cProd.obtenerIDProductoByCodigo(txtCodigoProducto.Text);
            if (txtCantOrdenada.Text.Equals(""))
                return;
            cDS.cantidad = txtCantOrdenada.Text;
            if (txtRevenimiento.Text.Equals(""))
                txtRevenimiento.Text = "0";
            cDS.revenimiento = txtRevenimiento.Text;
            if (lblPrecioU2.Text.Equals(""))
                return;
            cDS.precioU = lblPrecioU2.Text;
            if (ddlFactor.SelectedIndex.Equals(0))
                cDS.idFactor = 1;
            else
                cDS.idFactor = int.Parse(ddlFactor.SelectedValue);
            cDS.precioF = txtPrecioFactor2.Text;
            cDS.subtotal = lblSubTotal2.Text;
            if (txtIVA.Text.Equals(""))
                cDS.iva = "0";
            else
                cDS.iva = txtIVA.Text;
            cDS.total = lblTotal2.Text;
            cDS.actualizar(int.Parse(hfIdDS.Value), cUtl.idUsuarioActivo);

            llenarDetalleSolicitud(int.Parse(hfIdSolicitud.Value));

            cleanDataDetalle();
        }

        protected void lvDetalles_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {

        }
        protected void ddlFactor_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtPrecioFactor2.Text = calcular().ToString();
            txtPrecioFactor.Text = string.Format("{0:C}", calcular());
            calcTotal();
        }
        protected void ddlTipoProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblRevenimiento.Visible = false;
            txtRevenimiento.Visible = false;
            llenarElementos(int.Parse(ddlTipoProducto.SelectedValue));
            llenarFactores(int.Parse(ddlTipoProducto.SelectedValue));
            txtCodigoProducto_AutoCompleteExtender.ContextKey = ddlTipoProducto.SelectedValue;
            txtCodigoProducto.Text = "";
            txtDescProducto.Text = "";
            lblUDM.Text = "";
            txtRevenimiento.Text = "";
            lblRevenimiento.Visible = false;
            txtRevenimiento.Visible = false;
            lblPrecioU2.Text = "0";
            lblPrecioU.Text = string.Format("{0:C}", decimal.Parse(lblPrecioU2.Text));
            txtPrecioFactor2.Text = "0";
            txtPrecioFactor.Text = string.Format("{0:C}", decimal.Parse(txtPrecioFactor2.Text));
            txtCantOrdenada.Text = "0";
            lblSubTotal2.Text = "0";
            lblSubTotal.Text = string.Format("{0:C}", decimal.Parse(lblSubTotal2.Text));
            txtIVA.Text = "0";
            lblTotal2.Text = "0";
            lblTotal.Text = string.Format("{0:C}", decimal.Parse(lblTotal2.Text));
            txtCodigoProducto.Focus();
        }

        protected void txtCantOrdenada_TextChanged(object sender, EventArgs e)
        {
            calcTotal();
        }
        private void calcTotal()
        {
            decimal subtotal = decimal.Parse(txtCantOrdenada.Text) * decimal.Parse(txtPrecioFactor2.Text);
            if (txtIVA.Text.Equals(""))
                txtIVA.Text = "0";
            decimal iva = decimal.Parse(txtIVA.Text) * subtotal;
            decimal total = subtotal + iva;
            lblSubTotal2.Text = subtotal.ToString();
            lblTotal2.Text = total.ToString();
            lblSubTotal.Text = string.Format("{0:C}", subtotal);
            lblTotal.Text = string.Format("{0:C}", total);
        }

        protected void txtPrecioFactor_TextChanged(object sender, EventArgs e)
        {
            decimal sF = 0;
            if (txtPrecioFactor.Text.Substring(0, 1).Equals("$"))
            {
                sF = decimal.Parse(txtPrecioFactor.Text.Substring(1, txtPrecioFactor.Text.Length - 1));
            }
            else
            {
                sF = decimal.Parse(txtPrecioFactor.Text);
                txtPrecioFactor.Text = "$" + sF.ToString();
            }
            txtPrecioFactor2.Text = sF.ToString();
            calcTotal();
        }

        protected void ddlElemento_SelectedIndexChanged(object sender, EventArgs e)
        {
            hfIdElemento.Value = ddlElemento.SelectedValue;
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
                txtCalle.Focus();
            }
        }

        protected void mbtnAceptar_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopup();", true);
        }

        protected void mbtnClose_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopup();", true);
        }

        protected void txtIVA_TextChanged(object sender, EventArgs e)
        {
            calcTotal();
        }
    }
}