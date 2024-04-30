using System;
using System.Data;
using System.Web.UI;

namespace despacho
{
    public partial class generarOD2 : System.Web.UI.Page
    {
        cUtilidades cUtl = new cUtilidades();
        cOrdenes cOrd = new cOrdenes();
        cSolicitudes cSol = new cSolicitudes();
        cUsuarios cUsr = new cUsuarios();
        cDetallesSolicitud cDS = new cDetallesSolicitud();
        cOrdenesDosificacion cOD = new cOrdenesDosificacion();
        cSucursales cSuc = new cSucursales();
        cTipoUT cTUT = new cTipoUT();
        cProductos cPr = new cProductos();
        Folio cFol = new Folio();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                cUtl.idUsuarioActivo = int.Parse(Request.Cookies["ksroc"]["id"]);
                if (!IsPostBack)
                {
                    cargarDatos();
                }
            }
            catch (Exception)
            {
            }
        }

        private void cargarDatos()
        {
            hfIdOrden.Value = Request.QueryString["id"];
            cOrd.obtenerOrdenByID(int.Parse(hfIdOrden.Value));
            hfIdSucursal.Value = cOrd.idSucursal.ToString();
            lblSucursal.Text = cSuc.obtenerNombreSucursalByID(cOrd.idSucursal);
            lblClienteNombre.Text = cOrd.nombreC;
            hfIdSolicitud.Value = cOrd.idSolicitud.ToString();
            lblFolio.Text = cOrd.folio.ToString();
            lblFecha.Text = cOrd.fecha.ToString().Substring(0, 10);
            lblHora.Text = cOrd.hora;
            //fillHora(cOrd.hora);
            txtComentarios.Text = cOrd.comentarios;
            llenarDetalleSolicitud(int.Parse(hfIdSolicitud.Value));
        }
        private void fillHora(string sHora)
        {
            //cbxHora.SelectedValue = sHora.Substring(0, 2);
            //cbxMinutos.SelectedValue = sHora.Substring(3, 2);
        }
        private void llenarDetalleSolicitud(int idS)
        {
            DataTable dt = cDS.obtenerDetallesSolicitud(idS);
            checkProgramada(dt);
            lvDetalles.DataSource = dt;
            lvDetalles.DataBind();
        }

        //protected void llenarDdlTU(int idTP)
        //{
        //    ddlTU.Items.Clear();
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("id");
        //    dt.Columns.Add("tipo");

        //    dt.Rows.Add("0", "Selecciona un tipo de unidad");
        //    cTUT.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
        //    DataTable dt1 = cTUT.obtenerUTByIdTipoProductoAndIsCarga(idTP);
        //    foreach (DataRow dr in dt1.Rows)
        //    {
        //        dt.Rows.Add(dr["id"].ToString(), (dr["tipo"].ToString() + " - Límite: " + dr["capacidad"].ToString() + " - " + dr["unidad"].ToString()));
        //    }

        //    ddlTU.DataSource = dt;
        //    ddlTU.DataValueField = "id";
        //    ddlTU.DataTextField = "tipo";
        //    ddlTU.DataBind();
        //}

        protected void lvDetalles_ItemCommand(object sender, System.Web.UI.WebControls.ListViewCommandEventArgs e)
        {
            try
            {
                string[] arr;
                arr = e.CommandArgument.ToString().Split('ˇ');
                hfIdDS.Value = arr[0];
                hfIdProducto.Value = arr[1];

                if (e.CommandName.Equals("programar"))
                {
                    lblCodigoProducto0.Text = arr[2];
                    lblDescProducto0.Text = arr[3];
                    lblCantProducto0.Text = arr[4];
                    lblUDM2.Text = arr[5];
                    hfIdUDM.Value = arr[6];
                    if (arr[7].ToString().Equals("") || arr[7].ToString().Equals("0"))
                    {
                        lblRevenimiento.Visible = false;
                        lblRevenimiento3.Visible = false;
                    }
                    else
                    {
                        lblRevenimiento.Visible = true;
                        lblRevenimiento3.Visible = true;
                        lblRevenimiento3.Text = arr[7].ToString();
                    }
                    hfCapMaxUnidad.Value = getCapMaxCarga(int.Parse(arr[1]), int.Parse(arr[8]), arr[9].ToString()).ToString();
                    lblCantMaxCarga.Text = hfCapMaxUnidad.Value + " " + lblUDM2.Text;
                    if (llenarLVOD(int.Parse(hfIdOrden.Value)) == true)
                    {
                        cargarDatosODManual();
                        txtCantidadDosificacion.Focus();
                    }
                    pnlDS.Visible = true;
                    restoDosificar(int.Parse(Request.QueryString["id"]), int.Parse(hfIdProducto.Value), float.Parse(lblCantProducto0.Text), float.Parse(hfCapMaxUnidad.Value), 0);
                    txtCantidadDosificacion.Text = hfCantRecomendada.Value;
                    generarODMnl();
                    if (float.Parse(hfRestan.Value) <= 0)
                        hideControlsOE();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private float getCapMaxCarga(int idP, int idTP, string tipo)
        {
            cPr.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
            cPr.id = idP;
            float cargaP = cPr.obtenerCarga();
            cTUT.idTipoProducto = idTP;
            float cargaTU = cTUT.obtenerCargaMax();
            if (tipo.ToLower().Contains("concr"))
            {
                return cargaTU;
            }
            else
            {
                return cargaP * cargaTU;
            }
        }
        protected void btnGenerarODManual_Click(object sender, EventArgs e)
        {
        }

        private void generarODMnl()
        {
            cargarDatosODManual();
            restoDosificar(int.Parse(Request.QueryString["id"]), int.Parse(hfIdProducto.Value), float.Parse(lblCantProducto0.Text), float.Parse(hfCapMaxUnidad.Value), 0);
            txtCantidadDosificacion.Focus();
        }

        private void cargarDatosODManual()
        {
            PanelDatosOD.Visible = true;
            txtFechaDosificacion.Text = lblFecha.Text;
            cbxHoraD.SelectedValue = lblHora.Text.Substring(0, 2);
            cbxMinutosD.SelectedValue = lblHora.Text.Substring(3, 2);
            lblCodigoProductoDosificacion.Text = lblCodigoProducto0.Text;
            lblDescProductoDosificacion.Text = lblDescProducto0.Text;
            lblUDM1Dosificacion.Text = lblUDM2.Text;

        }

        protected void btnGenerarOD_Click(object sender, EventArgs e)
        {
            PanelDatosOD.Visible = false;
        }

        protected void btnCrearOD_Click(object sender, EventArgs e)
        {
            if (insertarOD(float.Parse(txtCantidadDosificacion.Text), getHoraD()))
            {
                if (float.Parse(hfRestan.Value) <= 0)
                    hideControlsOE();
                txtCantidadDosificacion.Text = hfCantRecomendada.Value;
                llenarLVOD(cOD.idOrden);
                if (lblMensaje.Text.Contains("Ya se generaron las Ordenes de entrega necesarias para TODOS los productos Ordenados"))
                {
                    lblMensaje.Text = "";
                    lblMensaje.Text = "Se generó con éxito la orden de entrega";
                    lblMensaje.Text += " || Ya se generaron las Ordenes de entrega necesarias para TODOS los productos Ordenados";
                }
                else
                    lblMensaje.Text += "Se generó con éxito la orden de entrega";
            }
            setOrdenToProgramada();
        }
        private string getHoraD()
        {
            return cbxHoraD.Text + ":" + cbxMinutosD.Text;
        }
        private void hideControlsOE()
        {
            pnlDS.Visible = false;
            PanelDatosOD.Visible = false;
        }

        private bool insertarOD(float qty, string sHora)
        {
            if (float.Parse(txtCantidadDosificacion.Text) > float.Parse(hfCapMaxUnidad.Value))
            {
                lblMensaje.Text = "ATENCIÓN!!! La cantidad ingresada supera la capacidad máxima de transportación del tipo de unidad, ingrese una cantidad menor";
                return false;
            }
            else
            {
                cOD.idSucursal = int.Parse(hfIdSucursal.Value);
                cOD.fecha = DateTime.Parse(txtFechaDosificacion.Text);
                cOD.hora = sHora;
                cOD.idOrden = int.Parse(hfIdOrden.Value);
                cOD.idProducto = int.Parse(hfIdProducto.Value);
                cOD.cantidad = qty;
                cOD.idUDM = int.Parse(hfIdUDM.Value);
                cOD.idDetalleSolicitud = int.Parse(hfIdDS.Value);
                cOD.idEstadoDosificacion = 1;
                cOD.idUnidadTransporte = 1;

                if (restoDosificar(int.Parse(Request.QueryString["id"]), cOD.idProducto, float.Parse(lblCantProducto0.Text), float.Parse(hfCapMaxUnidad.Value), 0, qty))
                {
                    //string[] arrFolio = cFol.obtenerFolio("remisiones", int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
                    //cOD.folio = arrFolio[0] + arrFolio[2];
                    cOD.folio = cOD.generarFolio();
                    cOD.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                    //cFol.actualizarFolio("remisiones", int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
                    return true;
                }
                else
                    return false;
            }
        }
        private bool modificarOD(float qty, string sHora)
        {
                if (float.Parse(txtCantidadDosificacion.Text) > float.Parse(hfCapMaxUnidad.Value))
                {
                    lblMensaje.Text = "ATENCIÓN!!! La cantidad ingresada supera la capacidad máxima de transportación del tipo de unidad, ingrese una cantidad menor";
                    return false;
                }
                else
                {
                    cOD.idSucursal = int.Parse(hfIdSucursal.Value);
                    cOD.fecha = DateTime.Parse(txtFechaDosificacion.Text);
                    cOD.hora = sHora;
                    cOD.idOrden = int.Parse(hfIdOrden.Value);
                    cOD.idProducto = int.Parse(hfIdProducto.Value);
                    cOD.cantidad = qty;
                    cOD.idUDM = int.Parse(hfIdUDM.Value);
                    cOD.idDetalleSolicitud = int.Parse(hfIdDS.Value);
                    cOD.idEstadoDosificacion = 1;
                    cOD.idUnidadTransporte = 1;

                    if (restoDosificar(int.Parse(Request.QueryString["id"]), cOD.idProducto, float.Parse(lblCantProducto0.Text), float.Parse(hfCapMaxUnidad.Value), float.Parse(hfCantMod.Value), qty))
                    {
                        cOD.actualizar(int.Parse(hfIdOD.Value), int.Parse(Request.Cookies["ksroc"]["id"]));
                        llenarLVOD(cOD.idOrden);
                        if (lblMensaje.Text.Contains("Se modificó con éxito la orden de entrega"))
                            return true;
                        lblMensaje.Text += " || Se modificó con éxito la orden de entrega";
                        return true;
                    }
                    else
                        return false;
                }
        }

        private bool checkProgramada(DataTable dt)
        {
            try
            {
                if (!cOrd.esProgramada(int.Parse(hfIdOrden.Value)))
                {
                    int vcs = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["programada"].Equals(true))
                            vcs++;
                    }
                    if (dt.Rows.Count.Equals(vcs))
                    {
                        if (vcs > 0)
                        {
                            lblMensaje.Text += "Ya se generaron las Ordenes de entrega necesarias para TODOS los productos Ordenados";
                            return true;
                        }
                        return false;
                    }
                    return false;
                }
                lblMensaje.Text += "Ya se generaron las Ordenes de entrega necesarias para TODOS los productos Ordenados";
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool restoDosificar(int idO, int idP, float tMax, float limiteUnidad, float mod, float qty = 0)
        {
            lblMensaje.Text = "";
            float qtyDB = cOD.obtenerQtyByOrdenAndIdProducto(idO, idP);
            if (!mod.Equals(0))
                qtyDB = qtyDB - mod;
            float total = qtyDB + qty;
            float resto = tMax - total;
            hfRestan.Value = resto.ToString();
            lblRestante.Text = hfRestan.Value;
            hfCantRecomendada.Value = limiteUnidad.ToString();
            if (qty > limiteUnidad)
            {
                lblMensaje.Text = "La cantidad ingresada sobrepasa el límite de la capacidad de la unidad por: " + (total - limiteUnidad).ToString() + " " + lblUDM1Dosificacion.Text;
                hfCantRecomendada.Value = limiteUnidad.ToString();
                txtCantidadDosificacion.Text = hfCantRecomendada.Value;
                return false;
            }
            if (hfEnableMod.Value.Equals("1"))
            {
                hfEnableMod.Value = "";
                return true;
            }
            if (resto == 0)
            {
                cDS.setProgramada(int.Parse(hfIdDS.Value), true, int.Parse(Request.Cookies["ksroc"]["id"]));
                DataTable dt = cDS.obtenerDetallesSolicitud(int.Parse(hfIdSolicitud.Value));
                checkProgramada(dt);
                lblMensaje.Text = "Ya se generaron las Ordenes de entrega necesarias para éste producto ";
                setOrdenToProgramada();

                cUtl.idUsuarioActivo = int.Parse(Request.Cookies["ksroc"]["id"]);
                cUtl.idSucursalActiva = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);

                //Generar alerta random
                cUtl.motivo = "Se ha generado una nueva Orden desde Despacho ¿Deseas actualizar?";
                cUtl.generarAlerta();
                return true;
            }
            if (total <= tMax)
            {
                //if (total < resto)
                //{
                //if (total.Equals(0))
                //{
                if (resto <= limiteUnidad)
                {
                    hfCantRecomendada.Value = resto.ToString();
                }
                else
                {
                    hfCantRecomendada.Value = limiteUnidad.ToString();
                }
                //}
                //else
                //{
                //    if (resto <= limiteUnidad)
                //    {
                //        hfCantRecomendada.Value = resto.ToString();
                //    }
                //    else
                //    {
                //        hfCantRecomendada.Value = limiteUnidad.ToString();
                //    }
                //}
                //}
                //else
                //{
                //    hfCantRecomendada.Value = resto.ToString();
                //}
                return true;
            }
            if (resto <= float.Parse(hfCapMaxUnidad.Value))
            {
                txtCantidadDosificacion.Text = (tMax - qtyDB).ToString();
                lblMensaje.Text = "El restante para igualar la Cantidad Ordenada es por " + (tMax - qtyDB) + " " + lblUDM1Dosificacion.Text;
                return false;
            }
            if (total > tMax)
            {
                txtCantidadDosificacion.Text = hfCapMaxUnidad.Value;
                lblMensaje.Text = "La cantidad ingresada sobrepasa la Cantidad Ordenada por: " + resto + " " + lblUDM1Dosificacion.Text;
                return false;
            }
            if ((qty <= resto) && (!qty.Equals(0)))
            {
                return true;
            }
            return false;
        }

        private bool llenarLVOD(int idO)
        {
            try
            {
                DataTable dt = cOD.obtenerODByIdOrden(int.Parse(hfIdOrden.Value));
                lvOD.DataSource = dt;
                lvOD.DataBind();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool setOrdenToProgramada()
        {
            DataTable dtDS = cDS.obtenerDetallesSolicitud(int.Parse(hfIdSolicitud.Value));
            int vcs = 0;
            foreach (DataRow drDS in dtDS.Rows)
            {
                if (float.Parse(drDS["cantidad"].ToString()).Equals(cOD.obtenerQtyByOrdenAndIdProducto(int.Parse(hfIdOrden.Value), int.Parse(drDS["idProducto"].ToString()))))
                    vcs++;
            }
            if (dtDS.Rows.Count.Equals(vcs))
            {
                cOrd.setProgramada(int.Parse(hfIdOrden.Value), true, int.Parse(Request.Cookies["ksroc"]["id"]));
                lblMensaje.Text += "| Ya se generaron las Ordenes de entrega necesarias para TODOS los productos Ordenados";
                return true;
            }
            else
            {
                cOrd.setProgramada(int.Parse(hfIdOrden.Value), false, int.Parse(Request.Cookies["ksroc"]["id"]));
                return false;
            }
        }

        protected void btnModOD_Click(object sender, EventArgs e)
        {
            if (modificarOD(float.Parse(txtCantidadDosificacion.Text), getHoraD()))
            {
                llenarLVOD(cOD.idOrden);
                float xMod = float.Parse(txtCantidadDosificacion.Text);
                if (int.Parse(hfRestan.Value) <= 0)
                    hideControlsOE();
                btnCrearOD.Visible = true;
                btnModOD.Visible = false;
                txtCantidadDosificacion.Text = hfRestan.Value;
            }
            setOrdenToProgramada();
        }

        protected void lvOD_ItemCommand(object sender, System.Web.UI.WebControls.ListViewCommandEventArgs e)
        {
            try
            {
                string[] arr;
                arr = e.CommandArgument.ToString().Split('ˇ');
                hfIdOD.Value = arr[0];

                if (e.CommandName.Equals("modificarOD"))
                {
                    txtFechaDosificacion.Text = arr[1].Substring(0, 10);
                    fillHoraOE(arr[2]);
                    lblCodigoProductoDosificacion.Text = arr[5];
                    lblDescProductoDosificacion.Text = arr[6];
                    txtCantidadDosificacion.Text = arr[7];
                    hfCantMod.Value = arr[7];
                    hfCapMaxUnidad.Value = getCapMaxCarga(int.Parse(arr[9]), int.Parse(arr[10]), arr[11].ToString()).ToString();
                    lblCantMaxCarga.Text = hfCapMaxUnidad.Value + " " + lblUDM2.Text;
                    btnCrearOD.Visible = false;
                    btnModOD.Visible = true;
                    pnlDS.Visible = true;
                    PanelDatosOD.Visible = true;
                    hfEnableMod.Value = "1";

                }
                if (e.CommandName.Equals("eliminarOD"))
                {
                    this.mlblTitle.Text = "¡¡¡CONFIRMACIÓN!!!";
                    if (cOD.getidEDByIdOD(int.Parse(arr[0].ToString())) < 5)
                    {
                        lblAux.Text = "";
                        this.mlblMessage.Text = "¿Estás seguro que deseas eliminar la Orden de entrega?";
                    }
                    else
                    {
                        lblAux.Text = "1";
                        this.mlblMessage.Text = "La orden de Entrega NO se eliminará debido a que ya está cargada/dosificada en la unidad";
                    }
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ShowPopup();", true);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void fillHoraOE(string sHora)
        {
            cbxHoraD.SelectedValue = sHora.Substring(0, 2);
            cbxMinutosD.SelectedValue = sHora.Substring(3, 2);
        }

        protected void mbtnAceptar_Click(object sender, EventArgs e)
        {
            if (lblAux.Text.Equals(""))
            {
                cOD.eliminar(int.Parse(hfIdOD.Value), int.Parse(Request.Cookies["ksroc"]["id"]));
                llenarLVOD(int.Parse(hfIdOD.Value));
                restoDosificar(int.Parse(Request.QueryString["id"]), int.Parse(hfIdProducto.Value), float.Parse(lblCantProducto0.Text), float.Parse(hfCapMaxUnidad.Value), 0);
                txtCantidadDosificacion.Text = hfCantRecomendada.Value;
                lblMensaje.Text = "La orden de Entrega se eliminó satisfactoriamente";

                cDS.setProgramada(int.Parse(hfIdDS.Value), false, int.Parse(Request.Cookies["ksroc"]["id"]));
                cOrd.setProgramada(int.Parse(hfIdOrden.Value), false, int.Parse(Request.Cookies["ksroc"]["id"]));
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopup();", true);
        }

        protected void mbtnClose_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopup();", true);
        }

        protected void btnTerminar_Click(object sender, EventArgs e)
        {
            setOrdenToProgramada();
            Response.Redirect("solicitudes.aspx");
        }
    }
}
