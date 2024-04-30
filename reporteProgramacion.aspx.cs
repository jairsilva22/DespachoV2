using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class reporteProgramacion : System.Web.UI.Page
    {
        cUtilidades cUtl = new cUtilidades();
        cDosificacion cDOS = new cDosificacion();
        cRemisionBlock cRemB = new cRemisionBlock();
        cRemisionConcreto cRemC = new cRemisionConcreto();
        cBitacora cBit = new cBitacora();
        cFormulacion cFor = new cFormulacion();
        cProductos cProd = new cProductos();
        cPasosReceta cPR = new cPasosReceta();
        DataTable dt = new DataTable();
        DataTable dtAux1 = new DataTable();
        DataTable dtAux2 = new DataTable();
        DataTable dtAux3 = new DataTable();
        DataTable dtAux4 = new DataTable();
        DataTable dtAux5 = new DataTable();
        DataTable dtAux6 = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                cUtl.idUsuarioActivo = int.Parse(Request.Cookies["ksroc"]["id"]);
                cDOS.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                if (!IsPostBack)
                {
                    filtrar();
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void llenarLVO(System.Web.UI.WebControls.ListView lv, DataTable dt)
        {
            lv.DataSource = dt;
            lv.DataBind();
        }
        protected void fillDDL(DataTable dtAux, AjaxControlToolkit.ComboBox ddl, string text, string value, string fText, int position)
        {
            DataTable dtX = ddlSinDuplicados(dtAux, position);

            ddl.Items.Clear();
            int i = 0;
            System.Web.UI.WebControls.ListItem li1 = new System.Web.UI.WebControls.ListItem();
            if (i.Equals(0))
            {
                li1.Text = fText;
                li1.Value = "0";
                ddl.Items.Add(li1);
                i++;
            }
            foreach (DataRow dr in dtX.Rows)
            {
                try
                {
                    if (dr[0].ToString().Equals(null) || dr[0].ToString().Equals(""))
                        return;
                    System.Web.UI.WebControls.ListItem li = new System.Web.UI.WebControls.ListItem();
                    li.Text = dr[text].ToString();
                    li.Value = dr[value].ToString();
                    ddl.Items.Add(li);
                }
                catch (Exception)
                {

                }
            }
            ddl.SelectedIndex = 0;
        }
        public DataTable ddlSinDuplicados(DataTable dtO, int position)
        {
            string idO = "", aux = "0";
            DataTable dtX = new DataTable();
            dtX = dtO.Copy();
            dtX.Clear();
            foreach (DataRow r in dtX.Rows)
            {
                r.Delete();
            }
            for (int i = 0; i < dtO.Rows.Count; i++)
            {
                try
                {
                    idO = dtO.Rows[i][position].ToString();
                    //if (i.Equals(0))
                    //    dtX.ImportRow(dtO.Rows[i]);
                    for (int j = 0; j < dtO.Rows.Count; j++)
                    {
                        try
                        {
                            if (dtO.Rows[j][position].ToString().Equals(idO))
                            {
                                if (aux.Equals("0"))
                                    dtX.ImportRow(dtO.Rows[j]);
                                aux = "1";
                                dtO.Rows[j].Delete();
                            }
                        }
                        catch (Exception)
                        {

                        }
                    }
                    aux = "0";
                }
                catch (Exception)
                {
                }
            }
            return dtX;
        }
        private void getAndFillListView(string lvName, string lblIdOrden, ListViewItemEventArgs e, string fIni = null, string fFin = null)
        {
            //find the nested listview
            System.Web.UI.WebControls.ListView listViewOD = e.Item.FindControl(lvName) as System.Web.UI.WebControls.ListView;

            System.Web.UI.WebControls.Label eLbl = (System.Web.UI.WebControls.Label)e.Item.FindControl(lblIdOrden);
            // find the name of current marca

            //here I use linq , you could use other ways only if you could get the perfumes having the current Order
            string yesterday = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            string today = DateTime.Now.ToString("yyyy-MM-dd");
            string tomorrow = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            DataTable dtX = new DataTable();

            switch (ddlFFiltros.SelectedValue)
            {
                case "0":
                    dtX = cDOS.obtenerRepODDFiltered(int.Parse(eLbl.Text), 0, chbxTerminados.Checked, today);
                    break;
                case "1":
                    dtX = cDOS.obtenerRepODDFiltered(int.Parse(eLbl.Text), 1, chbxTerminados.Checked, today);
                    break;
                case "2":
                    dtX = cDOS.obtenerRepODDFiltered(int.Parse(eLbl.Text), 2, chbxTerminados.Checked, tomorrow);
                    break;
                case "5":
                    dtX = cDOS.obtenerRepODDFiltered(int.Parse(eLbl.Text), 5, chbxTerminados.Checked, txtFechaI.Text, txtFechaF.Text);
                    break;
            }



            DataTable dt1 = quitarDuplicadosOD(dtX);

            //Agregar columnas
            //dt1.Columns.Add("colorBell");
            dt1.Columns.Add("estadoD");
            dt1.Columns.Add("antIdEstado");
            dt1.Columns.Add("antEstado");
            dt1.Columns.Add("sigIdEstado");
            dt1.Columns.Add("sigEstado");
            int idAntE = 0;
            int idSigE = 0;

            DataTable dtA = cDOS.obtenerAlertasByIdSucursal(int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
            foreach (DataRow dr in dt1.Rows)
            {
                try
                {

                    if (dr["idUTransporte"].ToString().Equals("1"))
                    {
                        //No mostrar linkbutton cancelar unidad
                    }

                    //if (dr["estado"].ToString().Contains("ado"))
                    //{
                    //find the nested linkButton
                    //LinkButton linkBtn = (LinkButton)listViewOD.FindControl("lbtnImprimirRemision");
                    //linkBtn.Visible = false;
                    //}

                    int idED = int.Parse(dr["idEstadoDosificacion"].ToString());
                    if (idED.Equals(4) || idED.Equals(5))
                    {
                        if (dr["tp"].ToString().ToLower().Contains("conc"))
                            dr["estadoD"] = cDOS.getEstadoDosificacion(idED, "concreto");
                        else
                            dr["estadoD"] = cDOS.getEstadoDosificacion(idED, "block");
                    }
                    else
                        dr["estadoD"] = cDOS.getEstadoDosificacion(idED);

                    if (dr["colorBell"].ToString().Equals("") || dr["colorBell"].ToString().Equals(null))
                        dr["colorBell"] = cDOS.getDiffTime(DateTime.Parse(dr["fecha"].ToString() + " " + dr["hora"].ToString().Substring(0, 5) + ":00"), dtA);

                    if (!dr["uTransporte"].ToString().Equals("Asignar"))
                    {
                        if (dr["estado"].ToString().Equals("Terminado"))
                        {
                            idAntE = 4;
                            dr["antIdEstado"] = idSigE.ToString();
                            dr["antEstado"] = "";
                        }
                        else
                        {
                            idAntE = int.Parse(dr["idEstadoDosificacion"].ToString()) - 1;
                            idSigE = int.Parse(dr["idEstadoDosificacion"].ToString()) + 1;
                            dr["antIdEstado"] = idAntE.ToString();
                            dr["sigIdEstado"] = idSigE.ToString();
                            dr["antEstado"] = cDOS.getEstadoDosificacion(idAntE);
                            dr["sigEstado"] = cDOS.getEstadoDosificacion(idSigE);
                        }
                    }
                    else
                    {
                        if (dr["estado"].ToString().Equals("Cancelar"))
                        {
                            idAntE = 4;
                            dr["antIdEstado"] = idSigE.ToString();
                            dr["antEstado"] = "";
                            dr["estado"] = "";
                            break;
                        }
                        idAntE = 4;
                        dr["antIdEstado"] = idSigE.ToString();
                        dr["antEstado"] = "";
                        idSigE = 5;
                        dr["sigIdEstado"] = idSigE.ToString();
                        dr["sigEstado"] = "";
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            listViewOD.DataSource = dt1;
            listViewOD.DataBind();

        }
        private DataTable quitarDuplicadosOD(DataTable dtO)
        {
            int idO = 0, aux = 0;
            DataTable dtX = new DataTable();
            dtX = dtO.Copy();
            dtX.Clear();
            foreach (DataRow r in dtX.Rows)
            {
                r.Delete();
            }
            for (int i = 0; i < dtO.Rows.Count; i++)
            {
                try
                {
                    idO = int.Parse(dtO.Rows[i]["id"].ToString());
                    //if (i.Equals(0))
                    //    dtX.ImportRow(dtO.Rows[i]);
                    for (int j = 0; j < dtO.Rows.Count; j++)
                    {
                        try
                        {
                            if (int.Parse(dtO.Rows[j]["id"].ToString()).Equals(idO))
                            {
                                if (aux.Equals(0))
                                    dtX.ImportRow(dtO.Rows[j]);
                                aux = 1;
                                dtO.Rows[j].Delete();
                            }
                        }
                        catch (Exception)
                        {

                        }
                    }
                    aux = 0;
                }
                catch (Exception)
                {
                }
            }
            return dtX;
        }

        private DataTable filtrar(int isCbx = 0)
        {
            if (ddlFFiltros.SelectedValue.Equals("5"))
            {
                lblFechaI.Visible = true;
                txtFechaI.Visible = true;
                lblFechaF.Visible = true;
                txtFechaF.Visible = true;
                if (txtFechaI.Text.Equals("") || txtFechaF.Text.Equals(""))
                    return null;
            }
            else
            {
                lblFechaI.Visible = false;
                txtFechaI.Visible = false;
                lblFechaF.Visible = false;
                txtFechaF.Visible = false;
            }

            string yesterday = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            string today = DateTime.Now.ToString("yyyy-MM-dd");
            string tomorrow = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");

            dt.Clear();

            string sCliente = "";
            string sVendedor = "";
            string sCodigo = "";
            string sUnidad = "";
            string sChofer = "";
            string sSucursal = "";

            if (!cbxClientes.SelectedIndex.Equals(0))
                sCliente = cbxClientes.SelectedValue;
            if (!cbxVendedores.SelectedIndex.Equals(0))
                sVendedor = cbxVendedores.SelectedValue;
            if (!cbxProductos.SelectedIndex.Equals(0))
                sCodigo = cbxProductos.SelectedValue;
            if (!cbxUnidad.SelectedIndex.Equals(0))
                sUnidad = cbxUnidad.SelectedValue;
            if (!cbxChoferes.SelectedIndex.Equals(0))
                sChofer = cbxChoferes.SelectedValue;
            if (!cbxSucursales.SelectedIndex.Equals(0))
                sSucursal = cbxSucursales.SelectedValue;

            switch (ddlFFiltros.SelectedValue)
            {
                case "0":
                    dt = cDOS.obtenerODSinDuplicados(cDOS.obtenerRepOPFiltro(sCliente, sVendedor, sCodigo, sUnidad, sChofer, sSucursal, 0, chbxTerminados.Checked));
                    break;
                case "1":
                    dt = cDOS.obtenerODSinDuplicados(cDOS.obtenerRepOPFiltro(sCliente, sVendedor, sCodigo, sUnidad, sChofer, sSucursal, 1, chbxTerminados.Checked, today));
                    break;
                case "2":
                    dt = cDOS.obtenerODSinDuplicados(cDOS.obtenerRepOPFiltro(sCliente, sVendedor, sCodigo, sUnidad, sChofer, sSucursal, 2, chbxTerminados.Checked, tomorrow));
                    break;
                case "5":
                    dt = cDOS.obtenerODSinDuplicados(cDOS.obtenerRepOPFiltro(sCliente, sVendedor, sCodigo, sUnidad, sChofer, sSucursal, 5, chbxTerminados.Checked, txtFechaI.Text, txtFechaF.Text));
                    break;
            }

            llenarLVO(lvO, dt);
            //if (isCbx.Equals(0))
            //{
                dtAux1 = dt.Copy();
                dtAux2 = dt.Copy();
                dtAux3 = dt.Copy();
                dtAux4 = dt.Copy();
                dtAux5 = dt.Copy();
                dtAux6 = dt.Copy();
                fillDDL(dtAux1, cbxClientes, "cliente", "clave", "Filtrar por cliente...", 0);
                fillDDL(dtAux2, cbxVendedores, "vendedor", "idV", "Filtrar por Vendedor...", 1);
                fillDDL(dtAux3, cbxProductos, "codigo", "codigo", "Filtrar por Producto...", 2);
                fillDDL(dtAux4, cbxUnidad, "uNombre", "uNombre", "Filtrar por Unidad...", 3);
                fillDDL(dtAux5, cbxChoferes, "chofer", "idCh", "Filtrar por Chofer...", 4);
                fillDDL(dtAux6, cbxSucursales, "sucursal", "idSuc", "Filtrar por Sucursal...", 5);
            //}
            return dt;
        }
        protected void ddlFFiltros_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtrar(1);
        }


        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            filtrar(1);
        }

        protected void mbtnAceptar_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopup();", true);
        }

        protected void mbtnClose_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopup();", true);
        }

        protected void lbtnBackDosificar_Command(object sender, CommandEventArgs e)
        {

        }

        protected void lbtnDosificar_Command(object sender, CommandEventArgs e)
        {
            string[] arr;
            arr = e.CommandArgument.ToString().Split('ˇ');
            if (!arr[3].Equals(""))
            {
                string sigEstadoDosificacion = cDOS.getEstadoDosificacion(int.Parse(arr[1]));
                if (sigEstadoDosificacion.Equals("Dosificado"))
                {
                    if (arr[10].ToLower().Contains("conc"))
                    {
                        mlblTittleDosi.Text = "Dosificación de la Remisión: " + arr[8] + " y Fórmula: " + arr[9];
                        mlblDosiIdOD.Text = arr[0];
                        mlblDosiIdFormula.Text = arr[7];
                        mlblDosiCantidad.Text = arr[6];
                        mlblDosiEdpDosi.Text = arr[1];
                        mlblDosiBell.Text = arr[4];
                        mlblTP.Text = arr[10];
                        llenarGVFormBase(int.Parse(arr[7]), float.Parse(arr[6]));
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ShowPopupDosi();", true);
                        return;
                    }
                }
                if (sigEstadoDosificacion.Equals("Terminado"))
                {
                    if (arr[10].ToLower().Contains("concreto"))
                    {
                        cDOS.actualizarEstado(int.Parse(arr[2].ToString()), 1, int.Parse(Request.Cookies["ksroc"]["id"]));
                    }
                    cDOS.actualizarEstadoDosificacion(int.Parse(arr[0].ToString()), int.Parse(arr[1].ToString()), int.Parse(Request.Cookies["ksroc"]["id"]));
                    cDOS.setCantidadEntregada(int.Parse(arr[5]), float.Parse(arr[6]), int.Parse(Request.Cookies["ksroc"]["id"]));
                    filtrar(1);
                }
                else
                {
                    cDOS.actualizarEstadoDosificacion(int.Parse(arr[0].ToString()), int.Parse(arr[1].ToString()), int.Parse(Request.Cookies["ksroc"]["id"]));
                    filtrar(1);
                }
            }
        }
        protected void llenarGVFormBase(int idP, float cantidad)
        {
            DataTable dtGV = cFor.obtenerFormulacionByIdProducto(idP);
            DataTable dt = new DataTable();
            dt.Columns.Add("Material");
            dt.Columns.Add("Cantidad");
            dt.Columns.Add("Unidad");
            dt.Columns.Add("Adicional");

            foreach (DataRow row in dtGV.Rows)
            {
                DataRow rw = dt.NewRow();
                rw["Material"] = row["material"];
                rw["Cantidad"] = row["cantidad"];
                rw["Unidad"] = row["unidad"].ToString().Trim();
                rw["Adicional"] = row["adicional"];
                dt.Rows.Add(rw);
            }
            gvFormulaBase.DataSource = dt;
            gvFormulaBase.DataBind();
        }

        protected void chbxTerminados_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void lvO_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            getAndFillListView("lvOD", "lblIDOrden", e);
        }

        protected void lvO_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

        }

        protected void lbtnImprimirRemision_Command(object sender, CommandEventArgs e)
        {
            string[] arr;
            arr = e.CommandArgument.ToString().Split('ˇ');
            mlblIdODRem.Text = arr[0];
            mlPDF.Text = "";

            if (arr[4].ToString().Equals(""))
            {
                mlblTitle.Text = "!!!Atención!!!";
                mlblMessage.Text = "No es posible visualizar la remisión debido que aún no se ha asignado Folio a la Remisión por Dosificación/Carga";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ShowPopup();", true);
            }
            else
            {
                if (int.Parse(arr[1].ToString()) >= 6)
                {
                    string Documento = "";
                    if (arr[2].ToString().Equals("") || arr[2].ToString().Equals("0"))
                    {
                        cRemB.nombreDoc = "Remision";
                        cRemB.extencion = ".pdf";
                        cRemB.path = Server.MapPath(".");
                        cRemB.idOD = int.Parse(mlblIdODRem.Text);
                        cRemB.idSucursal = int.Parse(arr[3].ToString());
                        mlblMensajeRemPDF.Text = cRemB.generarPdf();
                        Documento = cRemB.nombreDoc + cRemB.folio.ToString() + cRemB.extencion;
                    }
                    else
                    {
                        cRemC.nombreDoc = "Remision";
                        cRemC.extencion = ".pdf";
                        cRemC.path = Server.MapPath(".");
                        cRemC.idOD = int.Parse(mlblIdODRem.Text);
                        cRemC.idSucursal = int.Parse(arr[3].ToString());
                        mlblMensajeRemPDF.Text = cRemC.generarPdf();
                        Documento = cRemC.nombreDoc + cRemC.folio.ToString() + cRemC.extencion;
                        //Dosificación aún no genera folio para ésta remisión, intentelo más tarde :)
                    }
                    mhfDocumento.Value = Documento;

                    string Ruta = Server.MapPath(@"Remisiones");
                    mhfRuta.Value = Ruta + "\\";
                    //lblMensaje.Text += "Cotizacion/" + Documento;
                    string htm = "<iframe src ='Remisiones/" + int.Parse(Request.Cookies["ksroc"]["idSucursal"]) + "/" + Documento + "' width='100%' height='600px' ></iframe>";
                    mlPDF.Text = htm;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ShowPopupRemPDF();", true);
                }
                else
                {
                    mlblTitle.Text = "!!!Atención!!!";
                    mlblMessage.Text = "No es posible visualizar la remisión debido que aún no se ha dosificado/cargado la unidad";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ShowPopup();", true);
                }

            }
        }

        protected void lbtnBitacora_Command(object sender, CommandEventArgs e)
        {
            mlblTitleBit.Text = "Bitácora";
            mlblMessageBit.Text = "";
            lblOD.Text = e.CommandArgument.ToString();
            llenarLV(int.Parse(lblOD.Text));
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ShowPopupBit();", true);
        }

        protected void mbtnAceptarRemPDF_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopupRemPDF();", true);
        }

        protected void mBtnTittleCloseRemPDF_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopupRemPDF();", true);
        }

        protected void mBtnGenerarRemPDF_Click(object sender, EventArgs e)
        {
            //cRem.nombreDoc = "Remision";
            //cRem.extencion = ".pdf";
            //cRem.path = Server.MapPath(".");
            //cRem.idOD = int.Parse(mlblIdODRem.Text);
            //mlblMensajeRemPDF.Text = cRem.generarPdf();
            //string Documento = cRem.nombreDoc + cRem.folio.ToString() + cRem.extencion;

            //mhfDocumento.Value = Documento;

            //string Ruta = Server.MapPath(@"Remisiones");
            //mhfRuta.Value = Ruta + "\\";
            ////lblMensaje.Text += "Cotizacion/" + Documento;
            //string htm = "<iframe src ='Remisiones/" + Documento + "' width='100%' height='600px' ></iframe>";
            //mlPDF.Text = htm;
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ShowPopupRemPDF();", true);
        }

        protected void mlbtnSiguienteDosi_Click(object sender, EventArgs e)
        {
            cDOS.actualizarEstadoDosificacion(int.Parse(mlblDosiIdOD.Text), int.Parse(mlblDosiEdpDosi.Text), int.Parse(Request.Cookies["ksroc"]["id"]));
            cDOS.asignarColorBell(int.Parse(mlblDosiIdOD.Text), mlblDosiBell.Text, int.Parse(Request.Cookies["ksroc"]["id"]));
            mpnlDosi1.Visible = true;
            mpnlDosi2.Visible = false;
            mpnlDosi3.Visible = false;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopupDosi();", true);
            filtrar(1);
        }

        protected void mlbtnCloseDosi_Click(object sender, EventArgs e)
        {
            mpnlDosi1.Visible = true;
            mpnlDosi2.Visible = false;
            mpnlDosi3.Visible = false;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopupDosi();", true);
        }

        protected void mlbtnCancelarDosi2_Click(object sender, EventArgs e)
        {
            mpnlDosi1.Visible = true;
            mpnlDosi2.Visible = false;
            mpnlDosi3.Visible = false;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopupDosi();", true);
        }

        protected void mlbtnCloseBit_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopupBit();", true);
        }
        protected void llenarLV(int idOD)
        {
            lvBit.DataSource = cBit.obtenerBitacoraDT("ordenDosificacionIncidencias", idOD);
            lvBit.DataBind();
        }

        protected void mbtnAgregarBit_Click(object sender, EventArgs e)
        {
            cBit.idMaster = int.Parse(lblOD.Text);
            cBit.insertar(cBit.idMaster, "dosificacion.aspx", int.Parse(Request.Cookies["ksroc"]["id"]), "ordenDosificacionIncidencias", txtMotivo.Text);
            txtMotivo.Text = "";
            llenarLV(cBit.idMaster);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ShowPopupBit();", true);
        }

        protected void btnCalcularMaterial_Click(object sender, EventArgs e)
        {
            if (txtMezclaEnCamion.Text.Equals(""))
                txtMezclaEnCamion.Text = "0";
            if (txtAguaEnCamion.Text.Equals(""))
                txtAguaEnCamion.Text = "0";

            int idOD = int.Parse(mlblDosiIdOD.Text);
            int idFormula = int.Parse(mlblDosiIdFormula.Text);
            float cantidad = float.Parse(mlblDosiCantidad.Text);
            if (cDOS.existeMTD(idOD))
            {
                cDOS.executeSQL("Delete DosificacionTotalMaterial WHERE idOD = " + idOD.ToString());
            }
            DataTable dt = new DataTable();
            dt.Columns.Add("Material");
            dt.Columns.Add("Cantidad");
            dt.Columns.Add("Unidad");


            DataTable dtA = new DataTable();
            dtA.Columns.Add("Material");
            dtA.Columns.Add("Cantidad");
            dtA.Columns.Add("Unidad");
            foreach (GridViewRow row in gvFormulaBase.Rows)
            {
                if (!row.Cells[3].Text.Equals("True"))
                {
                    DataRow rw = dt.NewRow();
                    rw["Material"] = row.Cells[0].Text;
                    if (!row.Cells[0].Text.Equals("AGUA"))
                        rw["Cantidad"] = (float.Parse(row.Cells[1].Text) * (cantidad - float.Parse(txtMezclaEnCamion.Text))).ToString();
                    else
                    {
                        rw["Cantidad"] = (((float.Parse(row.Cells[1].Text) * cantidad) - float.Parse(txtMezclaEnCamion.Text)) - float.Parse(txtAguaEnCamion.Text)).ToString();
                    }
                    rw["Unidad"] = row.Cells[2].Text;
                    dt.Rows.Add(rw);
                }
                else
                {
                    DataRow rw = dtA.NewRow();
                    rw["Material"] = row.Cells[0].Text;
                    if (!row.Cells[0].Text.Equals("AGUA"))
                        rw["Cantidad"] = (float.Parse(row.Cells[1].Text) * (cantidad - float.Parse(txtMezclaEnCamion.Text))).ToString();
                    else
                    {
                        rw["Cantidad"] = (((float.Parse(row.Cells[1].Text) * cantidad) - float.Parse(txtMezclaEnCamion.Text)) - float.Parse(txtAguaEnCamion.Text)).ToString();
                    }
                    rw["Unidad"] = row.Cells[2].Text;
                    dtA.Rows.Add(rw);
                }
            }
            gvTotalMaterial.DataSource = dt;
            gvTotalMaterial.DataBind();
            gvAdicionales.DataSource = dtA;
            gvAdicionales.DataBind();
            mpnlDosi2.Visible = true;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ShowPopupDosi();", true);
        }

        protected void mbtnDosiDosificar_Click(object sender, EventArgs e)
        {
            mpnlDosi1.Visible = false;
            mpnlDosi3.Visible = true;

            int idOD = int.Parse(mlblDosiIdOD.Text);
            int idFormula = int.Parse(mlblDosiIdFormula.Text);
            float cantidad = float.Parse(mlblDosiCantidad.Text);
            if (cDOS.existeBitacora(idOD))
            {
                cDOS.executeSQL("Delete bitacoraDosificacion WHERE idOD = " + idOD.ToString());
            }
            addDosificacionTotal(idOD);
            generarBitacora(idOD, idFormula);
            //gvBitacora.DataSource = getBitacoraDosificacion(idOD);
            //gvBitacora.DataBind();
            lvBitacora.DataSource = cDOS.getBitacoraDosificacion(idOD);
            lvBitacora.DataBind();
            mlbtnSiguienteDosi.Visible = true;

            //dt = cDOS.obtenerODSinDuplicados(cDOS.obtenerOrdenesProgramaciónFiltro("", 0, 1, false, DateTime.Now.ToString("yyyy-MM-dd")));
            //llenarLVO(lvO, dt);
            filtrar(1);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ShowPopupDosi();", true);
        }
        public void addDosificacionTotal(int idOD)
        {
            foreach (GridViewRow row in gvTotalMaterial.Rows)
            {
                cDOS.saveDosificacionTotal(idOD, row.Cells[0].Text, float.Parse(row.Cells[1].Text), row.Cells[2].Text);
            }
        }
        public void generarBitacora(int idOD, int idP)
        {
            float cantidad = 0;
            int idReceta = cProd.obtenerIdRecetaByID(idP);
            string porcentaje = "", progreso = "";
            DataTable dtDTM = cDOS.getDosificacionTotalDeMaterial(idOD);
            int totMateriales = dtDTM.Rows.Count;
            DataTable dtReceta = cPR.getPasosByIdReceta(idReceta);
            decimal pTotal = decimal.Parse((totMateriales * 100).ToString()), totProgreso = 0, prog = 0, per = 0;

            try
            {
                for (int i = 0; i < dtReceta.Rows.Count; i++)
                {
                    //inserts = getCountPasos(idReceta, i);
                    for (int j = 0; j < dtDTM.Rows.Count; j++)
                    {
                        if (dtReceta.Rows[i]["material"].ToString().Trim().Contains(dtDTM.Rows[j]["material"].ToString().Trim()))
                        {
                            cantidad = float.Parse(dtDTM.Rows[j]["cantidad"].ToString()) * float.Parse(dtReceta.Rows[i]["porcentaje"].ToString());

                            decimal p = decimal.Parse(dtReceta.Rows[i]["porcentaje"].ToString());
                            porcentaje = cDOS.getPorcentaje(p);

                            per = decimal.Parse(porcentaje.ToString().Substring(0, (porcentaje.ToString().Length - 1)));
                            prog = decimal.Parse(per.ToString()) / pTotal;
                            progreso = cDOS.getPorcentaje(prog);

                            totProgreso += prog;
                            progreso = progreso + " - " + cDOS.getPorcentaje(totProgreso);

                            cDOS.addOnBitacora(idOD, dtReceta.Rows[i]["material"].ToString(), cantidad, dtDTM.Rows[j]["unidad"].ToString(), porcentaje, progreso);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void cbxClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtrar(1);
        }

        protected void cbxVendedores_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtrar(1);
        }

        protected void cbxProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtrar(1);
        }

        protected void cbxUnidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtrar(1);
        }

        protected void cbxChoferes_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtrar(1);
        }

        protected void lbtnAsignarU_Command(object sender, CommandEventArgs e)
        {
            string[] arr;
            arr = e.CommandArgument.ToString().Split('ˇ');
            mhfIdODAU.Value = arr[0];
            mhfNombreUTAU.Value = arr[1];
            mhfTPAU.Value = arr[2];
            mhfIdUTAU.Value = arr[3];
            mhfIdEstadoDAU.Value = arr[4];
            if (!arr[3].ToString().Equals("1"))
            {
                mpnlAUAsignarUnidad.Visible = false;
                mlblAUNombreUnidad.Text = mhfNombreUTAU.Value;
                mpnlAUDesasignar.Visible = true;
                mlbtnAUDAceptar.Visible = true;
                mlbtnAUDCancelar.Visible = true;
            }
            else
            {
                mpnlAUAsignarUnidad.Visible = true;
                mlblAUNombreUnidad.Text = mhfNombreUTAU.Value;
                mpnlAUDesasignar.Visible = false;
                mlbtnAUDAceptar.Visible = false;
                mlbtnAUDCancelar.Visible = false;
                llenarLVUT();
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ShowPopupAU();", true);
        }

        protected void mlbtnAceptarAU_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopupAU();", true);
            filtrar(1);
        }

        protected void mlbtnCancelar_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopupAU();", true);
        }

        protected void mlbtnCloseMyModalAU_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopupAU();", true);
        }

        protected void lbtnAsignarAU_Command(object sender, CommandEventArgs e)
        {
            asignarU(int.Parse(mhfIdODAU.Value), int.Parse(e.CommandArgument.ToString()));
        }

        private bool asignarU(int idOD, int idU)
        {
            int idUA = int.Parse(Request.Cookies["ksroc"]["id"]);
            try
            {
                if (mhfTPAU.Value.ToLower().Contains("concreto"))
                    cDOS.actualizarEstadoUnidadTransporte(idU, 2, idUA);
                cDOS.asignarUnidadTransporteAOD(idOD, idU, idUA);
                if (int.Parse(mhfIdEstadoDAU.Value) < 4)
                    cDOS.actualizarEstadoDosificacion(idOD, 4, idUA);
                else
                    cDOS.actualizarEstadoDosificacion(idOD, int.Parse(mhfIdEstadoDAU.Value), idUA);
                //lblMensaje.Text = "Se asignó la unidad Correctamente, favor de cerrar ésta ventana y actualizar Programación";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopupAU();", true);
                filtrar(1);
                return true;
            }
            catch (Exception)
            {
                //lblMensaje.Text = "No se pudo asignar la unidad Correctamente, favor de cerrar ésta ventana y actualizar Programación";
                return false;
            }
        }
        private bool desasignarU(int idOD, int idU)
        {
            int idUA = int.Parse(Request.Cookies["ksroc"]["id"]);
            try
            {
                if (mhfTPAU.Value.ToLower().Contains("concreto"))
                    cDOS.actualizarEstadoUnidadTransporte(idU, 1, idUA);
                cDOS.asignarUnidadTransporteAOD(idOD, 1, idUA);
                //cDOS.actualizarEstadoDosificacion(idOD, 4, idUA);
                //lblMensaje.Text = "Se desasignó la unidad Correctamente, favor de cerrar ésta ventana y actualizar Programación";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopupAU();", true);
                filtrar(1);
                return true;
            }
            catch (Exception)
            {
                //lblMensaje.Text = "No se pudo asignar la unidad Correctamente, favor de cerrar ésta ventana y actualizar Programación";
                return false;
            }
        }

        protected void llenarLVUT()
        {
            DataTable dt = cDOS.obtenerUDByIDOD(int.Parse(mhfIdODAU.Value));
            foreach (DataRow dr in dt.Rows)
            {
                try
                {
                    float cantOD = float.Parse(dr[3].ToString());
                    float cantUT = float.Parse(dr[12].ToString());
                    float cantMaxUT = float.Parse(dr[13].ToString());
                    if (dr[6].ToString().ToLower().Contains("conc"))
                    {
                        if (cantOD > cantMaxUT)
                            dr.Delete();
                    }
                    else
                    {
                        float cantPorTarima = float.Parse(dr[4].ToString());
                        float cantTarimas = cantOD / cantPorTarima;
                        if (cantTarimas > cantUT)
                            dr.Delete();
                    }
                }
                catch (Exception)
                {

                }
            }
            lvMdlAU.DataSource = dt;
            lvMdlAU.DataBind();
        }

        protected void mlbtnAUDAceptar_Click(object sender, EventArgs e)
        {
            desasignarU(int.Parse(mhfIdODAU.Value), int.Parse(mhfIdUTAU.Value));
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopupAU();", true);
        }

        protected void mlbtnAUDCancelar_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopupAU();", true);
        }

        protected void cbxSucursales_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtrar(1);
        }
    }
}