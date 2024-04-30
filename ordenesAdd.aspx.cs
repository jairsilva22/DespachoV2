using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class ordenesAdd : System.Web.UI.Page
    {
        cSolicitudes cSol = new cSolicitudes();
        cSucursales cSuc = new cSucursales();
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
        Folio cFol = new Folio();
        cCodigosPostales cCP = new cCodigosPostales();
        cEstados cEst = new cEstados();
        cCiudades cCd = new cCiudades();
        cOrdenes cOrd = new cOrdenes();
        cOrdenesDosificacion cOD = new cOrdenesDosificacion();
        public string param = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                cUtl.idUsuarioActivo = int.Parse(Request.Cookies["ksroc"]["id"]);
                txtClaveCliente_AutoCompleteExtender.ContextKey = Request.Cookies["ksroc"]["idSucursal"];
                txtNombreCliente_AutoCompleteExtender.ContextKey = Request.Cookies["ksroc"]["idSucursal"];


                //if (!hfIdProyecto.Value.Equals(""))
                //    ddlProyectos.SelectedValue = hfIdProyecto.Value;
                //validmaos si hay post
                //buscarClientesAGGrid();

                if (!IsPostBack)
                {
                    
                  
                    cargarControles();
                    //buscamos clientes para el aggrid agregado el 17-08-2022 por Mildred 

                 
                    //crearCookies();
                }
            }
            catch (Exception)
            {

            }
        }

        [System.Web.Services.WebMethod]
        public static string llenarAgGrid(int idSucursal) {
            
            cClientes cCliente = new cClientes();
            cCliente.idSucursal = idSucursal;
            DataTable dt = cCliente.buscarClientes();
            var Clientes = new List<cClientes>();
            Clientes.Add(new cClientes() { id = 0, clave = "", nombre = "", fp = "", mod = 0, select = 0 });


            if (dt.Rows.Count > 0) {
                for (int i = 0; i < dt.Rows.Count; i++) {

                    Clientes.Add(new cClientes() { id = int.Parse(dt.Rows[i]["id"].ToString()), clave = dt.Rows[i]["Clave"].ToString(), nombre = dt.Rows[i]["Nombre"].ToString(), fp = dt.Rows[i]["formaPago"].ToString(), mod = int.Parse(dt.Rows[i]["id"].ToString()), select = int.Parse(dt.Rows[i]["id"].ToString()) });
                }
            }

            var serializer = new JavaScriptSerializer();
            var serializedResult = serializer.Serialize(Clientes);

            return serializedResult;
            
        }

        private void buscarClientesAGGrid()
        {
            cClt.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
            DataTable dt = cClt.buscarClientes();
            param = "[ {fp:\"\", nombre: \"\", clave: \"\", mod: 0, select: 0}";
            //param = '[ {fp:"", nombre: "", clave: "", mod: 0}';
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        param += ",  ";
                    }
                    //int cont = i + 1;

                    if (i == dt.Rows.Count - 1)
                    {
                        param += "{fp: \"" + dt.Rows[i]["formaPago"].ToString() + "\", nombre: \"" + dt.Rows[i]["nombre"].ToString() + "\", clave: \"" + dt.Rows[i]["clave"].ToString() + "\", mod: " + dt.Rows[i]["id"].ToString() + ", select: " + dt.Rows[i]["id"] + "} ";
                        //param += "{fp: '" + dt.Rows[i]["formaPago"].ToString() + "', nombre: '" + dt.Rows[i]["nombre"].ToString() + "', clave: '" + dt.Rows[i]["clave"].ToString() + "', mod: " + dt.Rows[i]["id"].ToString() + "}";
                    }
                    else
                    {
                        param += "{fp: \"" + dt.Rows[i]["formaPago"].ToString() + "\", nombre: \"" + dt.Rows[i]["nombre"].ToString() + "\", clave: \"" + dt.Rows[i]["clave"].ToString() + "\", mod: " + dt.Rows[i]["id"].ToString() + ", select:" + dt.Rows[i]["id"] + "}, ";
                    }
                }
            }
            param += "]";
        }
        
        [System.Web.Services.WebMethod]
        public static string[] clientesAGGrid(int idSucursal)
        {
            cClientes cClt = new cClientes();
            
            cClt.idSucursal = idSucursal;
            DataTable dt = cClt.buscarClientes();
            string[] param = new string[dt.Rows.Count+1];
            
            //param = '[ {fp:"", nombre: "", clave: "", mod: 0}';
            if (dt.Rows.Count > 0)
            {
               // param[0] = "[{fp:\"\", nombre: \"\", clave: \"\", mod: 0, select: 0},";
                param[0] = "[{fp: '', nombre: '', clave: '', mod: 0, select: 0},";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    
                    //int cont = i + 1;

                    if (i == dt.Rows.Count - 1)
                    {
                       //param[i+1] = "{fp: \"" + dt.Rows[i]["formaPago"].ToString() + "\", nombre: \"" + dt.Rows[i]["nombre"].ToString() + "\", clave: \"" + dt.Rows[i]["clave"].ToString() + "\", mod: " + dt.Rows[i]["id"].ToString() + ", select: " + dt.Rows[i]["id"].ToString() + "}]";
                       param[i+1] = "{fp: '" + dt.Rows[i]["formaPago"].ToString() + "', nombre: '" + dt.Rows[i]["nombre"].ToString() + "', clave: '" + dt.Rows[i]["clave"].ToString() + "', mod: " + dt.Rows[i]["id"].ToString() + ", select: " + dt.Rows[i]["id"].ToString() + "}]";
                       //param[i+1] += '{fp: "' + dt.Rows[i]["formaPago"].ToString() + '", nombre: "' + dt.Rows[i]["nombre"].ToString() + '", clave: '" + dt.Rows[i]["clave"].ToString() + '", mod: ' + dt.Rows[i]["id"].ToString() + ' select: ' + dt.Rows[i]["id"].ToString() + '}]';
                       //param += "{fp: '" + dt.Rows[i]["formaPago"].ToString() + "', nombre: '" + dt.Rows[i]["nombre"].ToString() + "', clave: '" + dt.Rows[i]["clave"].ToString() + "', mod: " + dt.Rows[i]["id"].ToString() + "}";
                    }
                    else
                    {
                        //param[i+1] = "{fp: \"" + dt.Rows[i]["formaPago"].ToString() + "\", nombre: \"" + dt.Rows[i]["nombre"].ToString() + "\", clave: \"" + dt.Rows[i]["clave"].ToString() + "\", mod: " + dt.Rows[i]["id"].ToString() + ", select: " + dt.Rows[i]["id"].ToString() +"}, ";
                        param[i+1] = "{fp: '" + dt.Rows[i]["formaPago"].ToString() + "', nombre: '" + dt.Rows[i]["nombre"].ToString() + "', clave: '" + dt.Rows[i]["clave"].ToString() + "', mod: " + dt.Rows[i]["id"].ToString() + ", select: " + dt.Rows[i]["id"].ToString() +"}, ";
                       // param[i+1] += '{fp: \"" + dt.Rows[i]["formaPago"].ToString() + "\", nombre: \"" + dt.Rows[i]["nombre"].ToString() + "\", clave: \"" + dt.Rows[i]["clave"].ToString() + "\", mod: " + dt.Rows[i]["id"].ToString() + ", select: " + dt.Rows[i]["id"].ToString() +"}, ";
                    }
                }
            }
            else
            {
                param[0] = "[{fp:'', nombre: '', clave: '', mod: 0, select: 0}]";
            }
           // param += "]";
            return param;
        }

        public string buscarClientes()
        {
            buscarClientesAGGrid();
            return param;
        }
        protected void cargarControles()
        {
            llenarDdlVendedores();
            llenarFormasDePago();
            llenarTP();
            llenarEstados();
            llenarV2();
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
                    using (SqlCommand cmd = new SqlCommand("SELECT clave, nombre FROM clientes WHERE (idSucursal=" + contextKey + ") AND  ((LOWER(nombre) LIKE '%" + prefixText +
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

        //protected void txtNombreCliente_TextChanged(object sender, EventArgs e)
        //{
        //    bool modal = false;
        //    cClt.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
        //    cClt.nombre = txtNombreCliente.Text;
        //    txtClaveCliente.Text = cClt.obtenerClaveByNombre(cClt.nombre);

        //    cleanInfoProyectos();

        //    if (txtClaveCliente.Text.Equals(""))
        //        return;
        //    DataTable dt = cFP.obtenerDatosByClaveCliente(txtClaveCliente.Text);
        //    ddlFP.SelectedValue = dt.Rows[0]["idFormaPago"].ToString();
        //    hfIdFP.Value = ddlFP.SelectedValue;
        //    try
        //    {
        //        ddlVendedores.SelectedValue = dt.Rows[0]["idVendedor"].ToString();
        //        hfIdVendedor.Value = ddlVendedores.SelectedValue;
        //    }
        //    catch (Exception)
        //    {
        //        modal = true;
        //        ddlVendedores.SelectedIndex = 0;
        //        this.mlblTitle.Text = "¡¡¡ATENCIÓN!!!";
        //        this.mlblMessage.Text = "El vendedor que tiene asignado el cliente ya no está activo en el sistema, favor de asignar un nuevo vendedor";
        //    }
        //    llenarProyectos(txtClaveCliente.Text);
            
        //    if (modal)
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ShowPopup();", true);
        //}

        public void txtNombreCliente_TextChanged(object sender, EventArgs e)
        {
            bool modal = false;
            cClt.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
            cClt.nombre = txtNombreCliente.Text;
            txtClaveCliente.Text = cClt.obtenerClaveByNombre(cClt.nombre);
            hfIdCliente.Value = cClt.obtenerIdByClave(txtClaveCliente.Text).ToString();
            string idClienteProy = hfIdCliente.Value;
            cClt.id = cClt.obtenerIdByClave(txtClaveCliente.Text);

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

            DataTable dtProy = cPro.obtenerProyectosByIdClienteModal(int.Parse(idClienteProy));
            if (dtProy == null)
            {

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alerta", "AlertaProyectos();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Modal", "abrirpopupProyectos();", true);
                //this.mlblTitle.Text = "¡¡¡ATENCIÓN!!!";
                //this.mlblMessage.Text = "El cliente no cuenta con un proyecto, favor de asignarle uno";
            }
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
            ddlEstados.SelectedIndex = 0;
            ddlCiudades.Items.Clear();
            cbxColonias.Items.Clear();
            txtCalle.Text = "";
            txtNumero.Text = "";
            txtInterior.Text = "";
            ddlProyectos.Focus();
        }

        protected void ddlProyectos_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string SelectedVal = ddlProyectos.SelectedValue;
            if (ddlProyectos.SelectedIndex.Equals(0) || ddlProyectos.SelectedValue.Equals("0"))
            {
                //Response.Cookies["Solicitud"]["idProyecto"] = "0";
                hfIdProyecto.Value = "0";
                cleanInfoProyectos();
                txtCP.Focus();
            }
            else
            {
                DataTable dt = cPro.obtenerProyectosByID(int.Parse(ddlProyectos.SelectedValue));
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
                //Response.Cookies["Solicitud"]["idProyecto"] = ddlProyectos.SelectedValue;
                hfIdProyecto.Value = ddlProyectos.SelectedValue;
            }
        }

        protected void txtClaveCliente_TextChanged(object sender, EventArgs e)
        {
            bool modal = false;
            cClt.clave = txtClaveCliente.Text;
            txtNombreCliente.Text = cClt.obtenerNombreByClave(txtClaveCliente.Text);

            cleanInfoProyectos();
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

            hfIdProducto.Value = dt.Rows[0]["id"].ToString();
            txtDescProducto.Text = dt.Rows[0]["descripcion"].ToString();
            lblUDM.Text = dt.Rows[0]["unidad"].ToString();
            hfIdUDM.Value = dt.Rows[0]["idUDM"].ToString();
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
            txtprecioFIVA.Text = string.Format("{0:C}", decimal.Parse(dt.Rows[0]["precio"].ToString()) * (1 + decimal.Parse(dt.Rows[0]["iva"].ToString())));
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
            addSolicitud();
            cProd.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
            if (hfIdSolicitud.Equals(0) || hfIdSolicitud.Equals(null) || hfIdSolicitud.Equals("") || hfIdSolicitud.Equals("0"))
            {
                return;
            }

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
            if (cDS.idProducto.Equals(0))
            {
                lblError.Text = "Favor de introducir un código de producto existente";
                return;
            }
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

            if (txtprecioFIVA.Text.Equals("")) {
                cDS.precioFIVA = "0";
            }
            else {
                cDS.precioFIVA = txtprecioFIVA.Text.Replace("$", "").Replace(",", "");
            }

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
            txtprecioFIVA.Text = "";
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

        private void addSolicitud()
        {
            cSuc.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
            cSol.idSucursal = cSuc.idSucursal;
            cSol.anio = int.Parse(txtFecha.Text.Substring(0, 4));
            cSol.fecha = DateTime.Parse(txtFecha.Text);
            cSol.hora = getHora();
            hfIdCliente.Value = cClt.obtenerIdByClave(txtClaveCliente.Text).ToString();
            if (hfIdCliente.Value.Equals("0"))
            {
                lblError.Text = "Favor de volver a ingresar el código o nombre del cliente";
                return;
            }
            cSol.idCliente = int.Parse(hfIdCliente.Value);
            if (cSol.idVendedor.Equals(null))
            {
                hfIdVendedor.Value = "0";
            }
            if (hfIdVendedor.Value.Equals("0"))
            {
                lblError.Text = "Favor de seleccionar un vendedor";
                hfIdSolicitud.Value = "0";
                return;
            }
            else
            {
                lblError.Text = "";

            }
            cSol.idVendedor = int.Parse(hfIdVendedor.Value);
            if (ddlEstadoSolicitud.SelectedValue.Equals(null))
                ddlEstadoSolicitud.SelectedValue = "0";
            cSol.idEstadoSolicitud = int.Parse(ddlEstadoSolicitud.SelectedValue);
            if (ddlFP.SelectedValue.Equals(null))
                ddlFP.SelectedValue = "1";
            cSol.idFormaPago = int.Parse(ddlFP.SelectedValue);

            if (!txtCP.Text.Equals(""))
                cPro.cp = int.Parse(txtCP.Text);
            else
            {
                lblError.Text = "Favor de ingresar el Código Postal al que pertenece el proyecto";
                return;
            }
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
                cCP.cp = int.Parse(txtCP.Text);
                cCP.idEstado = int.Parse(hfIdEstado.Value);
                cCP.idCiudad = int.Parse(hfIdCiudad.Value);
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

            cPro.id = int.Parse(ddlProyectos.SelectedValue);
            if (cPro.existe())
            {
                cPro.actualizar(cUtl.idUsuarioActivo);
                cSol.idProyecto = cPro.id;
            }
            else
            {
                cPro.nombre = txtCalle.Text + " " + txtNumero.Text;
                cPro.insertar(cUtl.idUsuarioActivo);
                cSol.idProyecto = cPro.obtenerProyectoByCPCalleNumeroLIKE(cPro.cp, cPro.calle, cPro.numero, cSol.idCliente);
                if (cSol.idProyecto.Equals(0))
                    cSol.idProyecto = cPro.obtenerProyectoByCPCalleNumeroEQUAL(cPro.cp, cPro.calle, cPro.numero, cSol.idCliente);
                llenarProyectos(txtClaveCliente.Text);
                ddlProyectos.SelectedValue = cSol.idProyecto.ToString();
            }
            cSol.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"].ToString());
            if (ddlReqFac.SelectedValue == "")
            {
                lblError.Text = "Favor de Seleccionar si Requiere Factura";
                return;
            }
            cSol.reqFac = ddlReqFac.SelectedValue;
            try
            {
                if (string.IsNullOrEmpty(lblFolio.Text))
                {
                    string[] arrFolio = cFol.obtenerFolio("solicitudes", int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
                    cSol.folio = arrFolio[0] + arrFolio[1] + arrFolio[2];

                    cSol.insertar(cUtl.idUsuarioActivo);
                    cFol.actualizarFolio("solicitudes", int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
                    lblFolio.Text = cSol.folio.ToString();
                    lblFoli.Visible = true;
                    lblFolio.Visible = true;

                    hfIdSolicitud.Value = cSol.obtenerIDSolicitudByFolio(lblFolio.Text).ToString();
                    llenarDetalleSolicitud(int.Parse(hfIdSolicitud.Value));


                }
                else
                {
                    hfIdSolicitud.Value = cSol.obtenerIDSolicitudByFolio(lblFolio.Text).ToString();
                    cSol.actualizar(cUtl.idUsuarioActivo, int.Parse(hfIdSolicitud.Value));
                    llenarDetalleSolicitud(int.Parse(hfIdSolicitud.Value));
                }
            }
            catch (Exception)
            {

            }
        }

        private void addOrden()
        {
            try
            {
                cOrd.idSolicitud = int.Parse(hfIdSolicitud.Value);
                hfIdOrden.Value = cOrd.obtenerIdByIdSol().ToString();

                if (ddlV2.SelectedValue.Equals("0") || ddlV2.SelectedIndex.Equals(0))
                {
                    lblError.Text = "Favor de seleccionar un vendedor para aprobar";
                    return; //Pedir que seleccione un vendedor
                }
                 
                cOrd.idVendedor = int.Parse(ddlV2.SelectedValue);
                cOrd.fecha = DateTime.Parse(txtFecha.Text);
                cOrd.hora = getHora();
                cOrd.comentarios = txtComentarios.Text;
                cOrd.ubicacion = txtComentariosUbicacion.Text;
                cOrd.reqFac = ddlReqFac.SelectedValue;
                if (hfIdOrden.Value.Equals("0"))
                {
                    string[] arrFolio = cFol.obtenerFolio("ordenes", int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
                    cOrd.folio = arrFolio[0] + arrFolio[2];
                    cOrd.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                    cFol.actualizarFolio("ordenes", int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
                    cSol.cambiarAOrden(cOrd.idSolicitud, int.Parse(Request.Cookies["ksroc"]["id"]));
                    hfIdOrden.Value = cOrd.obtenerIdByIdSol().ToString();
                }
                else
                {
                    cOrd.actualizar(int.Parse(hfIdOrden.Value), int.Parse(Request.Cookies["ksroc"]["id"]));
                }
                crearOD();
                cOrd.setProgramada(int.Parse(hfIdOrden.Value), true, int.Parse(Request.Cookies["ksroc"]["id"]));
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void crearOD()
        {
            DataTable dtDS = cDS.obtenerDetallesSolicitud(int.Parse(hfIdSolicitud.Value));

            foreach (DataRow drDS in dtDS.Rows)
            {
                cOD.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                cOD.fecha = DateTime.Parse(txtFecha.Text);
                cOD.hora = getHora();
                cOD.idOrden = int.Parse(hfIdOrden.Value);
                cOD.idProducto = int.Parse(drDS["idProducto"].ToString());
                cOD.cantidad = 0;
                cOD.idUDM = int.Parse(drDS["idUDM"].ToString());
                cOD.idDetalleSolicitud = int.Parse(drDS["id"].ToString());
                cOD.idEstadoDosificacion = 1;
                cOD.idUnidadTransporte = 1;

                cOD.folio = cOD.generarFolio();
                cOD.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));


                cUtl.idUsuarioActivo = int.Parse(Request.Cookies["ksroc"]["id"]);
                cUtl.idSucursalActiva = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);

                //Generar alerta random
                cUtl.motivo = "Se ha generado una nueva Orden desde Despacho ¿Deseas actualizar?";
                cUtl.generarAlerta();
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
            addSolicitud();
            cProd.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
            if (ddlElemento.Visible)
            {
                cDS.idElemento = int.Parse(hfIdElemento.Value);
            }
            if (txtCodigoProducto.Text.Equals(""))
                return;
            cDS.idProducto = cProd.obtenerIDProductoByCodigo(txtCodigoProducto.Text);
            if (cDS.idProducto.Equals(0))
            {
                lblError.Text = "Favor de introducir un código de producto existente";
                return;
            }
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
            txtprecioFIVA.Text = string.Format("{0:C}", decimal.Parse(txtPrecioFactor2.Text )+ (decimal.Parse(txtPrecioFactor2.Text) * decimal.Parse(txtIVA.Text)));
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
            txtprecioFIVA.Text = string.Format("{0:C}", sF + (sF * decimal.Parse(txtIVA.Text)));
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

        protected void mbtnCloseModalP_Click(object sender, EventArgs e)
        {
            string idClienteProy = hfIdCliente.Value;
            DataTable dtProy = cPro.obtenerProyectosByIdClienteModal(int.Parse(idClienteProy));
            if (dtProy == null)
            {

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CerrarModalF", "cerrarmodalFrame();", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CerrarModalP", "cerrarmodalProyectos();", true);

                llenarProyectos(txtClaveCliente.Text);
            }

        }

        protected void txtIVA_TextChanged(object sender, EventArgs e)
        {
            txtprecioFIVA.Text = string.Format("{0:C}", decimal.Parse(txtPrecioFactor2.Text) * (1 + decimal.Parse(txtIVA.Text)));
            txtprecioFIVA_TextChanged(sender, e);
            //calcTotal();
        }

        protected void btnFinalizar_Click(object sender, EventArgs e)
        {
            if (cDS.obtenerDetallesSolicitud(int.Parse(hfIdSolicitud.Value)).Rows.Count.Equals(0))
            {
                lblError.Text = "Favor de dar de alta un producto para la orden";
                return;
            }
            addSolicitud();
            if (hfIdSolicitud.Equals(0) || hfIdSolicitud.Equals(null) || hfIdSolicitud.Equals("") || hfIdSolicitud.Equals("0"))
            {
                return;
            }
            addOrden();
            if (hfIdOrden.Value.Equals("0"))
            {
                return;
            }
            Response.Redirect("ordenes.aspx");
        }

        protected void llenarV2()
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
       
        public string modificarCliente()
        {
            string idCliente = (Context.Request.Form["idCliente"]);
            return idCliente.ToString();
        }

        protected void btnmodalAceptar_Click(object sender, EventArgs e)
        {
            buscarClientesAGGrid();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cerrarmodal", "cerrarModal()", true);
        }

        private void encontrarClienteByNombre() //agegamos método para buscar cliente cuando lo selecciona desde ag-grid
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

        private void encontrarClienteByClave()
        {
            bool modal = false;
            cClt.clave = txtClaveCliente.Text;
            txtNombreCliente.Text = cClt.obtenerNombreByClave(txtClaveCliente.Text);
            hfIdCliente.Value = cClt.obtenerIdByClave(txtClaveCliente.Text).ToString();
            string idClienteProy = hfIdCliente.Value;
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
            if (modal)
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ShowPopup();", true);
            DataTable dtProy = cPro.obtenerProyectosByIdClienteModal(int.Parse(idClienteProy));
            if (dtProy == null) {

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alerta", "AlertaProyectos();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Modal", "abrirpopupProyectos();", true);
                //this.mlblTitle.Text = "¡¡¡ATENCIÓN!!!";
                //this.mlblMessage.Text = "El cliente no cuenta con un proyecto, favor de asignarle uno";
            }
        }

        protected void btnBuscar_Click(object sender,EventArgs e)
        {
            // txtClaveCliente.Text = hdClave.Value;
            
            encontrarClienteByClave();
        }

        protected void btnMostrarCte_Click(object sender, EventArgs e)
        {
            buscarClientesAGGrid();
            hdClienteAG.Value = param;
        }

        protected void txtprecioFIVA_TextChanged(object sender, EventArgs e) {
            double sF = 0;
            double sFsinIva = 0;
            if (txtprecioFIVA.Text.Substring(0, 1).Equals("$")) {
                sF = double.Parse(txtprecioFIVA.Text.Substring(1, txtprecioFIVA.Text.Length - 1));
                sFsinIva = sF / (1 + double.Parse(txtIVA.Text));
            }
            else {
                sF = double.Parse(txtprecioFIVA.Text);
                txtprecioFIVA.Text = "$" + sF.ToString();
                sFsinIva = sF / (1 + double.Parse(txtIVA.Text));
            }
            txtPrecioFactor.Text = "$" + sFsinIva.ToString("0.000");
            txtPrecioFactor_TextChanged(sender, e);
        }
    }
}