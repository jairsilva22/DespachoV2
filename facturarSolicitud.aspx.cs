using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class facturarSolicitud : System.Web.UI.Page
    {
        cClientes cClt = new cClientes();
        cUtilidades cUtl = new cUtilidades();
        cUsuarios cUsr = new cUsuarios();
        cEstados cEst = new cEstados();
        cCiudades cCd = new cCiudades();
        cFormasPagoSAT cFP = new cFormasPagoSAT();
        cCodigosPostales cCP = new cCodigosPostales();
        cUsoCFDI cUsoCFDI = new cUsoCFDI();
        cFactura cFac = new cFactura();
        cFactura cFacturar = new cFactura();
        cDetallesFactura cFacDet = new cDetallesFactura();
        cSolicitudes cSol = new cSolicitudes();
        cSolicitudes cSolFac = new cSolicitudes();
        ClientesFacturacion clientes = new ClientesFacturacion();
        MetodoPagos metodo = new MetodoPagos();
        cMoneda cMon = new cMoneda();
        cDocumento cDoc = new cDocumento();
        Folio cFol = new Folio();

        protected void Page_Load(object sender, EventArgs e)
        {
            cUtl.idUsuarioActivo = int.Parse(Request.Cookies["ksroc"]["id"]);
            txtNombreCliente_AutoCompleteExtender.ContextKey = Request.Cookies["ksroc"]["idSucursal"];
            if (!IsPostBack)
            {
                cargarControles();
                cargarInfo(int.Parse(Request.QueryString["idCliente"]));
                //crearCookies();
            }
        }

        protected void cargarControles()
        {
            llenarEstados();
            llenarFormasDePago();
            llenarUsoCFDI();
            llenarMetodoPago();
            llenarMoneda();
            llenarDocumentos();
        }

        private void llenarDocumentos()
        {
            ddlDocumento.DataSource = cDoc.obtenerDocumentosbySuc2(int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
            ddlDocumento.DataTextField = "descripcion";
            ddlDocumento.DataValueField = "idDocumento";
            ddlDocumento.DataBind();

            /*ddlDocumento1.DataSource = cDoc.obtenerDocumentosbySuc2(int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
            ddlDocumento1.DataTextField = "descripcion";
            ddlDocumento1.DataValueField = "idDocumento";
            ddlDocumento1.DataBind();*/
        }

        private void llenarMetodoPago()
        {
            ddlMetodoPago.DataSource = metodo.metodos();
            ddlMetodoPago.DataTextField = "descripcion";
            ddlMetodoPago.DataValueField = "idpago";
            ddlMetodoPago.DataBind();
        }

        private void llenarMoneda()
        {
            ddlMoneda.DataSource = cMon.obtenerMonedabySuc(int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
            ddlMoneda.DataTextField = "descripcion";
            ddlMoneda.DataValueField = "idmd";
            ddlMoneda.DataBind();

            /*ddlMoneda1.DataSource = cMon.obtenerMonedabySuc(int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
            ddlMoneda1.DataTextField = "descripcion";
            ddlMoneda1.DataValueField = "idmd";
            ddlMoneda1.DataBind();*/
        }

        public void cargarInfo(int idC)
        {
            cClt.obtenerClienteByID(idC);
            hdClave.Value = cClt.clave;
            txtClave.Text = cClt.clave;
            txtNombreCliente.Text = cClt.nombre;
            txtNombreEmpresa.Text = cClt.nombre;
            hfIdFP.Value = cClt.idFormaPago.ToString();
            ddlFP.SelectedValue = hfIdFP.Value;
            hfIdVendedor.Value = cClt.idVendedor.ToString();
            txtCP.Text = cClt.cp.ToString();
            fillColonias(int.Parse(txtCP.Text));
            cbxColonias.SelectedValue = cClt.colonia.ToString();
            txtCalle.Text = cClt.calle;
            txtNumero.Text = cClt.numero.ToString();
            txtInterior.Text = cClt.interior.ToString();
            txtEmail.Text = cClt.email;
            txtTelefono.Text = cClt.telefono;
            txtCelular.Text = cClt.celular;
            //chbxDirecto.Checked = cClt.directo;

            //buscamos el IVA de la solicitud 
            string iva = cSol.obtenerIVASolicitud(int.Parse(Request.QueryString["idSolicitud"]));
            txtIva.Text = (float.Parse(iva) * 100).ToString();
            //txtIva1.Text = (float.Parse(iva) * 100).ToString();
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
            dt.Columns.Add("codigo");
            dt.Columns.Add("descripcion");

            dt.Rows.Add("0", "Selecciona una Forma de Pago");
            dt = cFP.obtenerFormasPagoSAT();

            ddlFP.DataSource = dt;
            ddlFP.DataValueField = "codigo";
            ddlFP.DataTextField = "descripcion";
            ddlFP.DataBind();
            ddlFP.Items.Insert(0, new ListItem("Seleccionar", ""));

           /* ddlFP1.DataSource = dt;
            ddlFP1.DataValueField = "codigo";
            ddlFP1.DataTextField = "descripcion";
            ddlFP1.DataBind();
            ddlFP1.Items.Insert(0, new ListItem("Seleccionar", ""));*/
        }

        private void llenarUsoCFDI()
        {
            ddlUsoCFDI.Items.Clear();
            DataTable dt = new DataTable();
            dt.Columns.Add("claveSat");
            dt.Columns.Add("descripcion");

            dt.Rows.Add("0", "Selecciona un uso CFDI");

            dt = cUsoCFDI.obtenerUsoCFDI();

            ddlUsoCFDI.DataSource = dt;
            ddlUsoCFDI.DataValueField = "claveSat";
            ddlUsoCFDI.DataTextField = "descripcion";
            ddlUsoCFDI.DataBind();
            ddlUsoCFDI.Items.Insert(0, new ListItem("Seleccionar", ""));
        }

        protected void txtCP_TextChanged(object sender, EventArgs e)
        {
            if (txtCP.Text.Length == 5)
            {
                fillColonias(int.Parse(txtCP.Text));
            }
        }

        private void fillColonias(int cp)
        {
            cbxColonias.Items.Clear();
            DataTable dt = cCP.getColoniasByCP(cp);
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
        protected void ddlFP_SelectedIndexChanged(object sender, EventArgs e)
        {
            hfIdFP.Value = ddlFP.SelectedValue;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            clientes.nombreCliente = txtNombreCliente.Text;
            if (txtRFC.Text.Equals(""))
            {
                lblError.Text = "Favor de introducir el RFC";
                return;
            }
            else
            {
                //se asigna a clientes Facturacion
                clientes.rfcCliente = txtRFC.Text;
            }
            if (ddlFP.SelectedValue.Equals(""))
            {
                lblError.Text = "Favor de Seleccionar la Forma de Pago";
                return;
            }
            else
            {
                clientes.idFormaPago = int.Parse(ddlFP.SelectedValue);
            }
            if (ddlMetodoPago.SelectedValue.Equals(""))
            {
                lblError.Text = "Favor de Seleccionar el metodo de Pago";
                return;
            }
            else
            {
                clientes.metodoPago = ddlMetodoPago.SelectedValue;
            }
            if (ddlUsoCFDI.SelectedValue.Equals(""))
            {
                lblError.Text = "Favor de Seleccionar el Uso CFDI";
                return;
            }
            else
            {
                clientes.usoCFDI = ddlUsoCFDI.SelectedValue;
            }
            if (txtNombreEmpresa.Text.Equals(""))
            {
                lblError.Text = "Favor de introducir el Nombre de la Empresa";
                return;
            }
            else
            {
                clientes.nombreEmpresa = txtNombreEmpresa.Text;
            }
            if (txtIva.Text.Equals(""))
            {
                lblError.Text = "Favor de introducir el IVA";
                return;
            }
            else
            {
                cFac.tasa = (txtIva.Text);
            }
            if (txtCP.Text.Equals("") || txtCP.Text.Length < 5)
            {
                lblError.Text = "Favor de introducir un código postal válido";
                return;
            }
            clientes.codigopostalCliente = (txtCP.Text);
            clientes.calleCliente = txtCalle.Text;
            if (txtNumero.Text.Equals(""))
            {
                txtNumero.Text = "0";
            }
            clientes.noExterior = (txtNumero.Text);
            clientes.noInterior = txtInterior.Text;
            if (cbxColonias.Text.Equals(""))
            {
                lblError.Text = "Favor de seleccionar una colonia o ingresar el nombre de la colonia para agregarla al sistema";
                return;
            }
            cCP.asenta = cbxColonias.SelectedItem.Text;
            if (!cCP.existe())
            {
                cCP.cp = cClt.cp;
                cCP.idEstado = int.Parse(hfIdEstadoCP.Value);
                cCP.idCiudad = int.Parse(hfIdCiudadCP.Value);
                cCP.insertar();
            }
            clientes.colonia = cbxColonias.SelectedItem.Text;
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
            clientes.estadoCliente = int.Parse(hfIdEstado.Value);
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
            if (ddlDocumento.SelectedValue.Equals(""))
            {
                lblError.Text = "Favor de Seleccionar el tipo de Documento";
                return;
            }
            clientes.clave = txtClave.Text;
            clientes.paisCliente = 1;
            clientes.ciudadCliente = int.Parse(hfIdCiudad.Value);
            clientes.correo = txtEmail.Text;
            clientes.telefonoCliente = txtTelefono.Text;
            cClt.celular = txtCelular.Text;
            clientes.idEmpresa = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
            if (cClt.idVendedor.Equals(null))
            {
                hfIdVendedor.Value = "0";
            }
            cClt.idVendedor = int.Parse(hfIdVendedor.Value);
            if (ddlFP.SelectedValue.Equals(null))
                ddlFP.SelectedValue = "1";
            cClt.idFormaPago = int.Parse(ddlFP.SelectedValue);
            //if (ddlUsoCFDI.SelectedValue.Equals(null))
            //cClt.directo = chbxDirecto.Checked;
            if (clientes.existeClienteEnFacturacion())
            {
                DataTable dt = clientes.consultarClienteFacturacion(clientes.nombreCliente, clientes.rfcCliente);
                string vendedor = cUsr.obtenerNombreUsuario(int.Parse(hfIdVendedor.Value));
                cFac.idcliente = int.Parse(dt.Rows[0]["idCliente"].ToString());
                cFac.estatus = "Pendiente";
                cFac.vendedor = vendedor;
                cFac.idSolicitud = int.Parse(Request.QueryString["idSolicitud"]);
                cFac.tasa = txtIva.Text;
                cFac.total = float.Parse(cSol.obtenerMontoSolicitud(int.Parse(Request.QueryString["idSolicitud"])));
                cFac.subtotal = float.Parse(cSol.obtenerSubtotalSolicitud(int.Parse(Request.QueryString["idSolicitud"])));
                cFac.iva = cFac.subtotal * float.Parse(cSol.obtenerIVASolicitud(int.Parse(Request.QueryString["idSolicitud"])));
                cFac.forma_pago = ddlMetodoPago.SelectedValue;
                cFac.metodoPago = ddlFP.SelectedValue;
                cFac.moneda = int.Parse(ddlMoneda.SelectedValue);
                cFac.cambio = float.Parse(cMon.obtenerTCambioByID(int.Parse(ddlMoneda.SelectedValue)).ToString());
                cFac.retencion = "0";
                cFac.asn = "";
                cFac.ordenCompra = "";
                cFac.va = "";
                cFac.estCobranza = "Pendiente";
                cFac.estatusCobranza = "Pendiente";
                DataTable dtComprobante = cDoc.obtenerDocumentoByID(int.Parse(ddlDocumento.SelectedValue));
                cFac.tipo_comprobante = int.Parse(ddlDocumento.SelectedValue);
                cFac.abreviatura = dtComprobante.Rows[0]["abrebiatura"].ToString();
                cFac.obsCliente = "Vendedor: " + vendedor;
                cFac.idusuario = int.Parse(Request.Cookies["ksroc"]["id"]);
                cFac.idempresa = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                cFac.serie = cFol.obtenerSerie(int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
                cFac.idSolicitud = int.Parse(Request.QueryString["idSolicitud"]);
                cFac.insertarFactura();

                cSolFac.agregarIDFactura(int.Parse(Request.QueryString["idSolicitud"]), int.Parse(cFac.idfactura.ToString()));

                DataTable dt2 = cSol.obtenerDetallesSolicitud(int.Parse(Request.QueryString["idSolicitud"]));
                int cantidadDetalles = cSol.obtenerCantidadDetallesSolicitud(int.Parse(Request.QueryString["idSolicitud"]));
                for (int i = 0; i < cantidadDetalles; i++)
                {
                    cFacDet.cantidad = float.Parse(dt2.Rows[i]["cantidad"].ToString());
                    cFacDet.descripcion = dt2.Rows[i]["prodDesc"].ToString();
                    cFacDet.total = float.Parse(dt2.Rows[i]["subtotal"].ToString());
                    cFacDet.precio_unitario = float.Parse(dt2.Rows[i]["precioF"].ToString());
                    cFacDet.id_factura = int.Parse(cFac.idfactura.ToString());
                    cFacDet.iva = txtIva.Text;
                    cFacDet.impuesto = float.Parse(dt2.Rows[i]["subtotal"].ToString()) * float.Parse(dt2.Rows[i]["iva"].ToString());
                    cFacDet.unidad = dt2.Rows[i]["udmDesc"].ToString();
                    cFacDet.nparte = dt2.Rows[i]["codigo"].ToString();
                    cFacDet.claveUnidad = dt2.Rows[i]["unidadSAT"].ToString();
                    cFacDet.claveProdServ = dt2.Rows[i]["codigoSAT"].ToString();
                    cFacDet.idUsuario = int.Parse(Request.Cookies["ksroc"]["id"]);

                    cFacDet.insertarDetFactura();
                }

                int cantidadDetFactura = cFac.obtenerCantidadDetallesFactura(int.Parse(cFac.idfactura.ToString()));
                    cFacturar.idfactura = int.Parse(cFac.idfactura.ToString());
                    cFacturar.estatus = "ProcesoCO";
                    cFacturar.modificarEstatus();

                Response.Redirect("cfinanzas.aspx");
                //if(cantidadDetalles == cantidadDetFactura)
                //{

                //}


            }
            else
            {
                clientes.insertar();
                DataTable dt = clientes.consultarClienteFacturacion(clientes.nombreCliente, clientes.rfcCliente);
                string vendedor = cUsr.obtenerNombreUsuario(int.Parse(hfIdVendedor.Value));
                cFac.idcliente = int.Parse(dt.Rows[0]["idCliente"].ToString());
                cFac.estatus = "Pendiente";
                cFac.vendedor = vendedor;
                cFac.idSolicitud = int.Parse(Request.QueryString["idSolicitud"]);
                cFac.tasa = txtIva.Text;
                cFac.total = float.Parse(cSol.obtenerMontoSolicitud(int.Parse(Request.QueryString["idSolicitud"])));
                cFac.subtotal = float.Parse(cSol.obtenerSubtotalSolicitud(int.Parse(Request.QueryString["idSolicitud"])));
                cFac.iva = cFac.subtotal * float.Parse(cSol.obtenerIVASolicitud(int.Parse(Request.QueryString["idSolicitud"])));
                cFac.forma_pago = ddlMetodoPago.SelectedValue;
                cFac.metodoPago = ddlFP.SelectedValue;
                cFac.moneda = int.Parse(ddlMoneda.SelectedValue);
                cFac.cambio = float.Parse(cMon.obtenerTCambioByID(int.Parse(ddlMoneda.SelectedValue)).ToString());
                cFac.retencion = "0";
                cFac.asn = "";
                cFac.ordenCompra = "";
                cFac.va = "";
                cFac.estCobranza = "Pendiente";
                cFac.estatusCobranza = "Pendiente";
                DataTable dtComprobante = cDoc.obtenerDocumentosbySuc(int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
                cFac.tipo_comprobante = int.Parse(dtComprobante.Rows[0]["idDocumento"].ToString());
                cFac.abreviatura = dtComprobante.Rows[0]["abrebiatura"].ToString();
                cFac.obsCliente = "Vendedor: " + vendedor;
                cFac.idusuario = int.Parse(Request.Cookies["ksroc"]["id"]);
                cFac.idempresa = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                cFac.serie = cFol.obtenerSerie(int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
                cFac.insertarFactura();

                cSolFac.agregarIDFactura(int.Parse(Request.QueryString["idSolicitud"]), int.Parse(cFac.idfactura.ToString()));

                DataTable dt2 = cSol.obtenerDetallesSolicitud(int.Parse(Request.QueryString["idSolicitud"]));
                int cantidadDetalles = cSol.obtenerCantidadDetallesSolicitud(int.Parse(Request.QueryString["idSolicitud"]));
                for (int i = 0; i < cantidadDetalles; i++)
                {
                    cFacDet.cantidad = float.Parse(dt2.Rows[i]["cantidad"].ToString());
                    cFacDet.descripcion = dt2.Rows[i]["prodDesc"].ToString();
                    cFacDet.total = float.Parse(dt2.Rows[i]["subtotal"].ToString());
                    cFacDet.precio_unitario = float.Parse(dt2.Rows[i]["precioF"].ToString());
                    cFacDet.id_factura = int.Parse(cFac.idfactura.ToString());
                    cFacDet.iva = txtIva.Text;
                    cFacDet.impuesto = float.Parse(dt2.Rows[i]["subtotal"].ToString()) * float.Parse(dt2.Rows[i]["iva"].ToString());
                    cFacDet.unidad = dt2.Rows[i]["udmDesc"].ToString();
                    cFacDet.nparte = dt2.Rows[i]["codigo"].ToString();
                    cFacDet.claveUnidad = dt2.Rows[i]["unidadSAT"].ToString();
                    cFacDet.claveProdServ = dt2.Rows[i]["codigoSAT"].ToString();
                    cFacDet.idUsuario = int.Parse(Request.Cookies["ksroc"]["id"]);

                    cFacDet.insertarDetFactura();
                }

                int cantidadDetFactura = cFac.obtenerCantidadDetallesFactura(int.Parse(cFac.idfactura.ToString()));
                cFacturar.idfactura = int.Parse(cFac.idfactura.ToString());
                cFacturar.estatus = "ProcesoCO";
                cFacturar.modificarEstatus();

                Response.Redirect("cfinanzas.aspx");
            }
            
            //cClt.actualizar(int.Parse(Request.QueryString["id"]), cUtl.idUsuarioActivo);
            //Response.Redirect("clientes.aspx");

        }

        protected void btnGuardarN_Click(object sender, EventArgs e)
        {
            //busca cliente 
            DataTable dt = clientes.consultarClienteFacturacionByID(int.Parse(clientes1.Value));
            clientee.InnerHtml = "<strong>" + dt.Rows[0]["nombreCliente"].ToString() + "</strong>&nbsp;<a href='javascript:borrar()'><img src='imagenes/tacha.gif' title='Borrar' width='20' height='20' border='0' /></a>";
            hdClave.Value = dt.Rows[0]["clave"].ToString();
            txtClave.Text = dt.Rows[0]["clave"].ToString();
            txtNombreCliente.Text = dt.Rows[0]["nombreCliente"].ToString();
            txtNombreEmpresa.Text = dt.Rows[0]["nombreEmpresa"].ToString();
            txtRFC.Text = dt.Rows[0]["rfcCliente"].ToString();
            //hfIdVendedor.Value = cClt.idVendedor.ToString();
            txtCP.Text = dt.Rows[0]["codigopostalCliente"].ToString();
            fillColonias(int.Parse(dt.Rows[0]["codigopostalCliente"].ToString()));
            cbxColonias.SelectedValue = dt.Rows[0]["colonia"].ToString();
            txtCalle.Text = dt.Rows[0]["calleCliente"].ToString();
            txtNumero.Text = dt.Rows[0]["noExterior"].ToString();
            txtInterior.Text = dt.Rows[0]["noInterior"].ToString();
            txtEmail.Text = dt.Rows[0]["correo"].ToString();
            txtTelefono.Text = dt.Rows[0]["telefonoCliente"].ToString();
            txtCelular.Text = dt.Rows[0]["telefonoCliente"].ToString();

            ddlUsoCFDI.SelectedValue = dt.Rows[0]["usoCFDI"].ToString();
            ddlMetodoPago.SelectedValue = dt.Rows[0]["metodoPago"].ToString();
            ddlEstados.SelectedValue = dt.Rows[0]["estadoCliente"].ToString();
            ddlCiudades.SelectedValue = dt.Rows[0]["ciudadCliente"].ToString();
        }

        protected void ddlEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            hfIdEstado.Value = ddlEstados.SelectedValue;
            llenarCiudades(int.Parse(hfIdEstado.Value));
        }

        protected void ddlCiudades_SelectedIndexChanged(object sender, EventArgs e)
        {
            hfIdCiudad.Value = ddlCiudades.SelectedValue;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            if(Request.QueryString["factura"] == "si")
            {
                Response.Redirect("cfinanzasRem.aspx?fechaIF=" + Request.QueryString["fechaIF"] + "&fechaFF=" + Request.QueryString["fechaFF"] + "&ordenF=" + Request.QueryString["ordenF"] + "&remF=" + Request.QueryString["remF"] + "&cteF=" + Request.QueryString["cteF"] + "&vendedorF=" + Request.QueryString["vendedorF"] + "&estatusF=" + Request.QueryString["estatusF"]);
            }
            else
            {
                Response.Redirect("cfinanzas.aspx?fechaIF=" + Request.QueryString["fechaIF"] + "&fechaFF=" + Request.QueryString["fechaFF"] + "&ordenF=" + Request.QueryString["ordenF"] + "&remF=" + Request.QueryString["remF"] + "&cteF=" + Request.QueryString["cteF"] + "&vendedorF=" + Request.QueryString["vendedorF"] + "&estatusF=" + Request.QueryString["estatusF"]);
            }
        }
    }
}