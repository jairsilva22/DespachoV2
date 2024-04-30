using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class programacion : System.Web.UI.Page
    {
        cUtilidades cUtl = new cUtilidades();
        cDosificacion cDOS = new cDosificacion();
        cRemisionBlock cRemB = new cRemisionBlock();
        cRemisionConcreto cRemC = new cRemisionConcreto();
        cBitacora cBit = new cBitacora();
        cFormulacion cFor = new cFormulacion();
        cProductos cProd = new cProductos();
        cPasosReceta cPR = new cPasosReceta();
        cDetallesSolicitud cDS = new cDetallesSolicitud();
        cOrdenesDosificacion cOD = new cOrdenesDosificacion();
        DataTable dt = new DataTable();
        DataTable dtAux1 = new DataTable();
        DataTable dtAux2 = new DataTable();
        DataTable dtAux3 = new DataTable();
        DataTable dtAux4 = new DataTable();
        DataTable dtAux5 = new DataTable();
        //Para insertar en logCantidadEntregada - Agregado por Enrique Sandoval 10-11-2022
        cLogCantidadEntregada cLCE = new cLogCantidadEntregada();
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
            catch (Exception)
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
                    dtX = cDOS.obtenerODDFiltered(int.Parse(eLbl.Text), 0, chbxTerminados.Checked, yesterday);
                    break;
                case "1":
                    dtX = cDOS.obtenerODDFiltered(int.Parse(eLbl.Text), 1, chbxTerminados.Checked, today);
                    break;
                case "2":
                    dtX = cDOS.obtenerODDFiltered(int.Parse(eLbl.Text), 2, chbxTerminados.Checked, tomorrow);
                    break;
                case "5":
                    dtX = cDOS.obtenerODDFiltered(int.Parse(eLbl.Text), 5, chbxTerminados.Checked, txtFechaI.Text, txtFechaF.Text);
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
                    if (dr["cantidad"].ToString().Equals("0"))
                    {
                        dr.Delete();
                        //No mostrar linkbutton cancelar unidad
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
        public string getHora1(string idOD)
        {
            string equis = cBit.getHoraBitByStatus(int.Parse(idOD), "Estado de dosificacion a = 7");
            if (!string.IsNullOrEmpty(equis))
                return "- Salió de planta:" + equis.Substring(11, equis.Length - 11);
            else
                return "";
        }

        public string getHora2(string idOD)
        {
            string equis = cBit.getHoraBitByStatus(int.Parse(idOD), "Estado de dosificacion a = 9");
            if (!string.IsNullOrEmpty(equis))
                return "- LLegó a obra:" + equis.Substring(11, equis.Length - 11);
            else
                return "";
        }

        public string getHora3(string idOD)
        {
            string equis = cBit.getHoraBitByStatus(int.Parse(idOD), "Estado de dosificacion a = 10");
            if (!string.IsNullOrEmpty(equis))
                return "- Salió de obra:" + equis.Substring(11, equis.Length - 11);
            else
                return "";
        }

        public string getHora4(string idOD)
        {
            string equis = cBit.getHoraBitByStatus(int.Parse(idOD), "Estado de dosificacion a = 11");
            if (!string.IsNullOrEmpty(equis))
                return "- Regresó a planta:" + equis.Substring(11, equis.Length - 11);
            else
                return "";
        }
        private void cleanHFTotales()
        {
            hfTDTotal.Value = "0";
            hfBBATotal.Value = "0";
            hfBlockTotal.Value = "0";
            hfTDDone.Value = "0";
            hfBBADone.Value = "0";
            hfBlockDone.Value = "0";
            hfTDPte.Value = "0";
            hfBBAPte.Value = "0";
            hfBlockPte.Value = "0";
            hfCTotal.Value = "0";
            hfCDone.Value = "0";
            hfCPte.Value = "0";
        }
        private void proyeccion(int opc = 1, string fFechaI = "", string fFechaF = "")
        {
            DataTable dtPT = cDOS.proyeccion(opc, fFechaI, fFechaF);
            DataTable dtPTD = cDOS.proyeccion(opc, fFechaI, fFechaF, "conc", "td");
            DataTable dtPTBBA = cDOS.proyeccion(opc, fFechaI, fFechaF, "conc", "bba");
            DataTable dtPOtros = cDOS.proyeccion(opc, fFechaI, fFechaF, "conc", "otro");
            DataTable dtPTBlock = cDOS.proyeccion(opc, fFechaI, fFechaF, "block");

            lblBBA.Text = "BBA Programados: " + dtPTBBA.Rows[0]["cantidadSolicitada"] + " - Entregados: " + dtPTBBA.Rows[0]["cantidadEntregada"] + " = " + (float.Parse(dtPTBBA.Rows[0]["cantidadSolicitada"].ToString()) - float.Parse(dtPTBBA.Rows[0]["cantidadEntregada"].ToString())) + " Pendientes";
            lblTD.Text = "TD Programados: " + dtPTD.Rows[0]["cantidadSolicitada"] + " - Entregados: " + dtPTD.Rows[0]["cantidadEntregada"] + " = " + (float.Parse(dtPTD.Rows[0]["cantidadSolicitada"].ToString()) - float.Parse(dtPTD.Rows[0]["cantidadEntregada"].ToString())) + " Pendientes";
            lblOtro.Text = "Otros Programados: " + dtPOtros.Rows[0]["cantidadSolicitada"] + " - Entregados: " + dtPOtros.Rows[0]["cantidadEntregada"] + " = " + (float.Parse(dtPOtros.Rows[0]["cantidadSolicitada"].ToString()) - float.Parse(dtPOtros.Rows[0]["cantidadEntregada"].ToString())) + " Pendientes";
            lblC.Text = "Concreto Programado: " + dtPT.Rows[0]["cantidadSolicitada"] + " - Entregado: " + dtPT.Rows[0]["cantidadEntregada"] + " = " + (float.Parse(dtPT.Rows[0]["cantidadSolicitada"].ToString()) - float.Parse(dtPT.Rows[0]["cantidadEntregada"].ToString())) + " Pendientes";
            lblBlock.Text = "Block Programados: " + dtPTBlock.Rows[0]["cantidadSolicitada"] + " - Entregados: " + dtPTBlock.Rows[0]["cantidadEntregada"] + " = " + (float.Parse(dtPTBlock.Rows[0]["cantidadSolicitada"].ToString()) - float.Parse(dtPTBlock.Rows[0]["cantidadEntregada"].ToString())) + " Pendientes";

            if (dtPTBBA.Rows[0]["cantidadSolicitada"].ToString().Equals("0"))
                lblBBA.Visible = false;
            else
                lblBBA.Visible = true;

            if (dtPTD.Rows[0]["cantidadSolicitada"].ToString().Equals("0"))
                lblTD.Visible = false;
            else
                lblTD.Visible = true;

            if (dtPOtros.Rows[0]["cantidadSolicitada"].ToString().Equals("0"))
                lblOtro.Visible = false;
            else
                lblOtro.Visible = true;

            if (dtPT.Rows[0]["cantidadSolicitada"].ToString().Equals("0"))
                lblC.Visible = false;
            else
                lblC.Visible = true;

            if (dtPTBlock.Rows[0]["cantidadSolicitada"].ToString().Equals("0"))
                lblBlock.Visible = false;
            else
                lblBlock.Visible = true;
        }

        private void proyeccionOLD(int opc = 1, string fFechaI = "", string fFechaF = "")
        {

            DataTable dtY = cDOS.proyeccion(opc, fFechaI, fFechaF);

            foreach (DataRow dr in dtY.Rows)
            {
                try
                {
                    if (dr["tipo"].ToString().ToLower().Contains("concreto"))
                    {
                        if (!int.Parse(dr["idEstadoDosificacion"].ToString()).Equals(15))
                        {
                            hfCTotal.Value = (float.Parse(hfCTotal.Value) + float.Parse(dr["cantidad"].ToString())).ToString();
                            if (dr["codigo"].ToString().ToLower().Contains("bba"))
                            {
                                hfBBATotal.Value = (float.Parse(hfBBATotal.Value) + float.Parse(dr["cantidad"].ToString())).ToString();
                                if (int.Parse(dr["idEstadoDosificacion"].ToString()) >= 6)
                                {
                                    hfBBADone.Value = (float.Parse(hfBBADone.Value) + float.Parse(dr["cantidad"].ToString())).ToString();
                                    hfCDone.Value = (float.Parse(hfCDone.Value) + float.Parse(dr["cantidad"].ToString())).ToString();
                                }
                                else
                                {
                                    hfBBAPte.Value = (float.Parse(hfBBAPte.Value) + float.Parse(dr["cantidad"].ToString())).ToString();
                                    hfCPte.Value = (float.Parse(hfCPte.Value) + float.Parse(dr["cantidad"].ToString())).ToString();
                                }
                            }
                            else
                            {
                                if (dr["codigo"].ToString().ToLower().Contains("td"))
                                {
                                    hfTDTotal.Value = (float.Parse(hfTDTotal.Value) + float.Parse(dr["cantidad"].ToString())).ToString();
                                    if (int.Parse(dr["idEstadoDosificacion"].ToString()) >= 6)
                                    {
                                        hfTDDone.Value = (float.Parse(hfTDDone.Value) + float.Parse(dr["cantidad"].ToString())).ToString();
                                        hfCDone.Value = (float.Parse(hfCDone.Value) + float.Parse(dr["cantidad"].ToString())).ToString();
                                    }
                                    else
                                    {
                                        hfTDPte.Value = (float.Parse(hfTDPte.Value) + float.Parse(dr["cantidad"].ToString())).ToString();
                                        hfCPte.Value = (float.Parse(hfCPte.Value) + float.Parse(dr["cantidad"].ToString())).ToString();
                                    }
                                }
                                else
                                {
                                    if (int.Parse(dr["idEstadoDosificacion"].ToString()) >= 6)
                                    {
                                        hfCDone.Value = (float.Parse(hfCDone.Value) + float.Parse(dr["cantidad"].ToString())).ToString();
                                    }
                                    else
                                    {
                                        hfCPte.Value = (float.Parse(hfCPte.Value) + float.Parse(dr["cantidad"].ToString())).ToString();
                                    }
                                }
                            }
                        }
                    }
                    if (dr["tipo"].ToString().ToLower().Contains("block"))
                    {
                        if (!int.Parse(dr["idEstadoDosificacion"].ToString()).Equals(15))
                        {
                            hfBlockTotal.Value = (float.Parse(hfBlockTotal.Value) + float.Parse(dr["cantidad"].ToString())).ToString();
                            if (int.Parse(dr["idEstadoDosificacion"].ToString()) >= 6)
                            {
                                hfBlockDone.Value = (float.Parse(hfBlockDone.Value) + float.Parse(dr["cantidad"].ToString())).ToString();
                            }
                            else
                            {
                                hfBlockPte.Value = (float.Parse(hfBlockPte.Value) + float.Parse(dr["cantidad"].ToString())).ToString();
                            }
                        }
                    }
                }
                catch (Exception)
                {

                }
            }

            lblBBA.Text = "BBA Programados: " + hfBBATotal.Value + " - Entregados: " + hfBBADone.Value + " = " + hfBBAPte.Value + " Pendientes";
            lblTD.Text = "TD Programados: " + hfTDTotal.Value + " - Entregados: " + hfTDDone.Value + " = " + hfTDPte.Value + " Pendientes";
            lblC.Text = "Concreto Programado: " + hfCTotal.Value + " - Entregado: " + hfCDone.Value + " = " + hfCPte.Value + " Pendiente";
            lblBlock.Text = "Block Programados: " + hfBlockTotal.Value + " - Entregados: " + hfBlockDone.Value + " = " + hfBlockPte.Value + " Pendientes";

            if (hfBBATotal.Value.Equals("0"))
                lblBBA.Visible = false;
            else
                lblBBA.Visible = true;

            if (hfTDTotal.Value.Equals("0"))
                lblTD.Visible = false;
            else
                lblTD.Visible = true;

            if (hfCTotal.Value.Equals("0"))
                lblC.Visible = false;
            else
                lblC.Visible = true;

            if (hfBlockTotal.Value.Equals("0"))
                lblBlock.Visible = false;
            else
                lblBlock.Visible = true;
        }
        private DataTable filtrar(int isCbx = 0)
        {
            cleanHFTotales();
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

            switch (ddlFFiltros.SelectedValue)
            {
                case "0":
                    dt = cDOS.obtenerODSinDuplicados(cDOS.obtenerOPFiltro(sCliente, sVendedor, sCodigo, sUnidad, sChofer, 0, chbxTerminados.Checked, yesterday));
                    proyeccion(0, yesterday);
                    break;
                case "1":
                    dt = cDOS.obtenerODSinDuplicados(cDOS.obtenerOPFiltro(sCliente, sVendedor, sCodigo, sUnidad, sChofer, 1, chbxTerminados.Checked, today));
                    proyeccion(1, today);
                    break;
                case "2":
                    dt = cDOS.obtenerODSinDuplicados(cDOS.obtenerOPFiltro(sCliente, sVendedor, sCodigo, sUnidad, sChofer, 2, chbxTerminados.Checked, tomorrow));
                    proyeccion(2, tomorrow);
                    break;
                case "5":
                    dt = cDOS.obtenerODSinDuplicados(cDOS.obtenerOPFiltro(sCliente, sVendedor, sCodigo, sUnidad, sChofer, 5, chbxTerminados.Checked, txtFechaI.Text, txtFechaF.Text));
                    proyeccion(5, txtFechaI.Text, txtFechaF.Text);
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
            fillDDL(dtAux1, cbxClientes, "cliente", "clave", "Filtrar por cliente...", 0);
            fillDDL(dtAux2, cbxVendedores, "vendedor", "idV", "Filtrar por Vendedor...", 1);
            fillDDL(dtAux3, cbxProductos, "codigo", "codigo", "Filtrar por Producto...", 2);
            fillDDL(dtAux4, cbxUnidad, "uNombre", "uNombre", "Filtrar por Unidad...", 3);
            fillDDL(dtAux5, cbxChoferes, "chofer", "idCh", "Filtrar por Chofer...", 4);
            //}
            return dt;
        }
        protected void ddlFFiltros_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtrar();
        }


        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", "showLoader();", true);
            filtrar();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hideLoader", "hideLoader();", true);
        }

        protected void mbtnAceptar_Click(object sender, EventArgs e)
        {
            tmrCheckChanges.Enabled = true;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopup();", true);
        }

        protected void mbtnClose_Click(object sender, EventArgs e)
        {
            tmrCheckChanges.Enabled = true;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopup();", true);
        }

        protected void lbtnBackDosificar_Command(object sender, CommandEventArgs e)
        {

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
            tmrCheckChanges.Enabled = false;
            string[] arr;
            arr = e.CommandArgument.ToString().Split('ˇ');
            mlblIdODRem.Text = arr[0];
            int idorden = int.Parse(arr[4]);
            mlPDF.Text = "";
            bool hasFolioR = false;
            if (cDOS.isTravelGroup(int.Parse(mlblIdODRem.Text))) {
                int grupoViaje = cDOS.getTravelGroup(int.Parse(arr[0]));
                DataTable productsViaje = cOD.obtenerRemisionesGrupoViaje(idorden, grupoViaje);
                string folioRemision = cDOS.obtenerFolioOD(int.Parse(arr[0]));
                foreach (DataRow dr in productsViaje.Rows) {
                    mlblIdODRem.Text = dr["id"].ToString();

                    if (int.Parse(dr["idEstadoDosificacion"].ToString().ToString()) >= 6) {
                        //Validar que la cantidadEntregada de detalles solicitud sea la correcta
                        validarCantidadEntregada(idorden);

                        if (!dr["folioR"].ToString().Equals("")) {
                            string Documento = "";
                            if (dr["tp"].ToString().ToString().ToLower().Contains("conc")) {
                                cRemC.nombreDoc = "Remision";
                                cRemC.extencion = ".pdf";
                                cRemC.path = Server.MapPath(".");
                                cRemC.idOD = int.Parse(mlblIdODRem.Text);
                                cRemC.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                                mlblMensajeRemPDF.Text = cRemC.generarPdf();
                                Documento = cRemC.nombreDoc + cRemC.folio.ToString() + cRemC.extencion;
                            }
                            else {
                                //Block

                                cRemB.nombreDoc = "Remision";
                                cRemB.extencion = ".pdf";
                                cRemB.path = Server.MapPath(".");
                                cRemB.idOD = int.Parse(mlblIdODRem.Text);
                                cRemB.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                                cRemB.idOrden = idorden;
                                
                                mlblMensajeRemPDF.Text = cRemB.generarPdf();
                                Documento = cRemB.nombreDoc + cRemB.folio.ToString() + cRemB.extencion;
                            }

                            mhfDocumento.Value = Documento;

                            string Ruta = Server.MapPath(@"Remisiones");
                            mhfRuta.Value = Ruta + "\\";
                            //lblMensaje.Text += "Cotizacion/" + Documento;
                            string htm = "<iframe src ='Remisiones/" + int.Parse(Request.Cookies["ksroc"]["idSucursal"]) + "/" + Documento + "' width='100%' height='600px' ></iframe>";
                            mlPDF.Text = htm;
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ShowPopupRemPDF();", true);
                        }
                        else {
                            mlblTitle.Text = "!!!Atención!!!";
                            mlblMessage.Text = "No es posible generar la remisión debido que aún no se ha generado Folio en Dosificación";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ShowPopup();", true);
                        }
                    }
                    else {
                        mlblTitle.Text = "!!!Atención!!!";
                        mlblMessage.Text = "No es posible generar la remisión debido que aún no se ha dosificado/cargado la unidad";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ShowPopup();", true);
                    }
                }
            }
            else {
                if (int.Parse(arr[1].ToString()) >= 6) {
                    //Validar que la cantidadEntregada de detalles solicitud sea la correcta
                    validarCantidadEntregada(idorden);

                    if (!arr[3].ToString().Equals("")) {
                        string Documento = "";
                        if (arr[2].ToString().ToLower().Contains("conc")) {
                            cRemC.nombreDoc = "Remision";
                            cRemC.extencion = ".pdf";
                            cRemC.path = Server.MapPath(".");
                            cRemC.idOD = int.Parse(mlblIdODRem.Text);
                            cRemC.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                            mlblMensajeRemPDF.Text = cRemC.generarPdf();
                            Documento = cRemC.nombreDoc + cRemC.folio.ToString() + cRemC.extencion;
                        }
                        else {
                            cRemB.nombreDoc = "Remision";
                            cRemB.extencion = ".pdf";
                            cRemB.path = Server.MapPath(".");
                            cRemB.idOD = int.Parse(mlblIdODRem.Text);
                            cRemB.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                            cRemB.idOrden = idorden;
                            mlblMensajeRemPDF.Text = cRemB.generarPdf();
                            Documento = cRemB.nombreDoc + cRemB.folio.ToString() + cRemB.extencion;
                        }

                        mhfDocumento.Value = Documento;

                        string Ruta = Server.MapPath(@"Remisiones");
                        mhfRuta.Value = Ruta + "\\";
                        //lblMensaje.Text += "Cotizacion/" + Documento;
                        string htm = "<iframe src ='Remisiones/" + int.Parse(Request.Cookies["ksroc"]["idSucursal"]) + "/" + Documento + "' width='100%' height='600px' ></iframe>";
                        mlPDF.Text = htm;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ShowPopupRemPDF();", true);
                    }
                    else {
                        mlblTitle.Text = "!!!Atención!!!";
                        mlblMessage.Text = "No es posible generar la remisión debido que aún no se ha generado Folio en Dosificación";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ShowPopup();", true);
                    }
                }
                else {
                    mlblTitle.Text = "!!!Atención!!!";
                    mlblMessage.Text = "No es posible generar la remisión debido que aún no se ha dosificado/cargado la unidad";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ShowPopup();", true);
                }
            }
            
        }

        protected void lbtnBitacora_Command(object sender, CommandEventArgs e)
        {
            tmrCheckChanges.Enabled = false;
            mlblTitleBit.Text = "Bitácora";
            mlblMessageBit.Text = "";
            lblOD.Text = e.CommandArgument.ToString();
            llenarLV(int.Parse(lblOD.Text));
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ShowPopupBit();", true);
        }

        protected void mbtnAceptarRemPDF_Click(object sender, EventArgs e)
        {
            tmrCheckChanges.Enabled = true;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopupRemPDF();", true);
        }

        protected void mBtnTittleCloseRemPDF_Click(object sender, EventArgs e)
        {
            tmrCheckChanges.Enabled = true;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopupRemPDF();", true);
        }

        protected void mlbtnCloseBit_Click(object sender, EventArgs e)
        {
            tmrCheckChanges.Enabled = true;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopupBit();", true);
        }
        protected void llenarLV(int idOD)
        {
            lvBit.DataSource = cBit.obtenerBitacoraDT("ordenDosificacionIncidencias", idOD);
            lvBit.DataBind();
        }

        protected void mbtnAgregarBit_Click(object sender, EventArgs e)
        {
            tmrCheckChanges.Enabled = false;
            cBit.idMaster = int.Parse(lblOD.Text);
            cBit.insertar(cBit.idMaster, "dosificacion.aspx", int.Parse(Request.Cookies["ksroc"]["id"]), "ordenDosificacionIncidencias", txtMotivo.Text);
            txtMotivo.Text = "";
            llenarLV(cBit.idMaster);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ShowPopupBit();", true);
        }

        protected void cbxClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        protected void cbxVendedores_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        protected void cbxProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        protected void cbxUnidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        protected void cbxChoferes_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        protected void lbtnCalidad_Command(object sender, CommandEventArgs e)
        {
            string url = "pCalidad.aspx?idOD=" + e.CommandArgument.ToString();
            string s = "window.open('" + url + "', 'popup_window', 'width=1000,height=800,left=100,top=100,resizable=yes');";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", s, true);
        }

        protected void mBtnCheckCerrar_Click(object sender, EventArgs e)
        {
            cUtl.idUsuarioActivo = int.Parse(Request.Cookies["ksroc"]["id"]);
            cUtl.idSucursalActiva = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
            cUtl.generarBiAlertaBitacora(int.Parse(hfCheckID.Value), "Descartar");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopupCheck();", true);
            tmrCheckChanges.Enabled = true;
        }

        protected void mBtnCheckAceptar_Click(object sender, EventArgs e)
        {
            cUtl.idUsuarioActivo = int.Parse(Request.Cookies["ksroc"]["id"]);
            cUtl.idSucursalActiva = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
            cUtl.generarBiAlertaBitacora(int.Parse(hfCheckID.Value), "Aceptar");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopupCheck();", true);
            filtrar();
            tmrCheckChanges.Enabled = true;
        }

        protected void tmrCheckChanges_Tick(object sender, EventArgs e)
        {
            cUtl.idUsuarioActivo = int.Parse(Request.Cookies["ksroc"]["id"]);
            cUtl.idSucursalActiva = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);

            DataTable dtAlerta = cUtl.consultarAlerta();

            if (!dtAlerta.Rows.Count.Equals(0))
            {
                //if (!int.Parse(dtAlerta.Rows[0]["idUsuario"].ToString()).Equals(int.Parse(Request.Cookies["ksroc"]["id"])))
                //{
                hfCheckID.Value = dtAlerta.Rows[0]["id"].ToString();
                if (!cUtl.alertaVista(int.Parse(hfCheckID.Value)))
                {
                    //tmrCheckChanges.Enabled = false;
                    //mlblCheckTittle.Text = "Actualización";
                    //mlblCheckMessage.Text = dtAlerta.Rows[0]["motivo"].ToString();
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ShowPopupCheck();", true);

                    cUtl.generarBiAlertaBitacora(int.Parse(hfCheckID.Value), "Aceptar");

                    filtrar();
                }
                //}
            }
        }

        protected void mlbtnClose_Click(object sender, EventArgs e)
        {
            tmrCheckChanges.Enabled = true;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopup();", true);
        }

        protected void mlbtnCheckClose_Click(object sender, EventArgs e)
        {
            cUtl.idUsuarioActivo = int.Parse(Request.Cookies["ksroc"]["id"]);
            cUtl.idSucursalActiva = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
            cUtl.generarBiAlertaBitacora(int.Parse(hfCheckID.Value), "Descartar");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopupCheck();", true);
            tmrCheckChanges.Enabled = true;
        }


        protected void mlbtnEdoSolClose_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopupEdoSol();", true);
            tmrCheckChanges.Enabled = true;
        }

        protected void mlbtnEdoSolAceptar_Click(object sender, EventArgs e)
        {
            cDOS.actualizarEstadoSolicitud(int.Parse(hfEdoSolIDS.Value), int.Parse(ddlEstadoSolicitud.SelectedValue), int.Parse(Request.Cookies["ksroc"]["id"]));
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopupEdoSol();", true);
            filtrar();
            tmrCheckChanges.Enabled = true;
        }

        protected void lBtnEdoSolicitud_Command(object sender, CommandEventArgs e)
        {
            tmrCheckChanges.Enabled = false;
            hfEdoSolIDS.Value = e.CommandArgument.ToString();
            ddlEstadoSolicitud.SelectedValue = cDOS.getEstadoSolicitud(int.Parse(hfEdoSolIDS.Value));
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ShowPopupEdoSol();", true);
        }

        protected void lBtnOrden_Command(object sender, CommandEventArgs e)
        {
            string[] arr;
            arr = e.CommandArgument.ToString().Split('ˇ');

            string url = "OrdenEntrega.aspx?idO=" + arr[0].ToString() + "&idS=" + arr[1].ToString() + "&idDS=" + arr[2].ToString() + "&idOD=" + arr[3].ToString();
            string s = "window.open('" + url + "', 'popup_window', 'width=1000,height=700,left=100,top=100,resizable=yes');";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", s, true);
        }

        protected void lbtnTerminarOrden_Command(object sender, CommandEventArgs e)
        {
            string[] arr;
            arr = e.CommandArgument.ToString().Split('ˇ');
            hfIdOrden.Value = arr[0];
            hfIdDS.Value = arr[1];
            hfCantOrd.Value = arr[2];
            hfCantEnt.Value = arr[3];
            lblAuxTerminarOrden.Text = "1";
            mlblTitleTC.Text = "!!!Atención!!!";
            mlblMessageTC.Text = "¿Estás seguro que deseas dar por terminada la orden?";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ShowPopupTC();", true);
        }

        protected void mlbtnCloseTC_Click(object sender, EventArgs e)
        {
            tmrCheckChanges.Enabled = true;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopupTC();", true);
        }

        protected void mbtnCancelarTC_Click(object sender, EventArgs e)
        {
            tmrCheckChanges.Enabled = true;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopupTC();", true);
        }

        //Método para ver la cantidad correcta entregada. Agregado por Enrique 10-11-22
        public string calcularCantE(int idO) {
            string dato = "";
            dato = cDOS.obtenerCantE(idO);
            return dato;
        }

        //Agregado - Validar cantidad Entregada - Enrique Sandoval 10-11-2022
        public void validarCantidadEntregada(int idOrden) {
            //Tabla detalles solicitud
            float cantidadE = cDS.getCantidadEntregadaByIdOrden(idOrden);

            //Tabla ordenDosificacion
            float cantidadEOD = float.Parse(calcularCantE(idOrden));

            //Validar que cantidadE no sea mayor a cantidadEOD
            if (cantidadE > cantidadEOD) {
                cDOS.setCantidadEntregadaByOrden(idOrden, cantidadEOD.ToString(), int.Parse(Request.Cookies["ksroc"]["id"]));
                //Insertar en el logCantidadEntregada
                cLCE.obtenerIdDetalleSolicitud(idOrden);
                cLCE.cantidadAnterior = cantidadE;
                cLCE.cantidadActualizada = cantidadEOD;
                cLCE.idUsuario = int.Parse(Request.Cookies["ksroc"]["id"]);
                cLCE.insertar();
            }

        }

        protected void mbtnAceptarTC_Click(object sender, EventArgs e)
        {
            //int idUA = int.Parse(Request.Cookies["ksroc"]["id"]);
            //if (lblAuxTerminarOrden.Text.Equals("1"))
            //{
            //    //Aqui eliminaría las OD pendientes por dosificar y actualizaría la cantidad ordenada por la cantidad que se ha dosificado vdd???
            //    //Set cantidad = cantidadOrdenada en DS

            //    cDS.id = int.Parse(hfIdDS.Value);
            //    cDS.obtenerDetalleSolicitudByID();

            //    decimal precioF = decimal.Parse(cDS.precioF);
            //    decimal iva = decimal.Parse(cDS.iva);
            //    decimal subtotal, total;

            //    subtotal = decimal.Parse(hfCantEnt.Value) * decimal.Parse(precioF.ToString());
            //    iva = iva * subtotal;
            //    total = subtotal + iva;

            //    cOD.idOrden = int.Parse(hfIdOrden.Value);
            //    cDS.cantidad = hfCantEnt.Value;
            //    cDS.subtotal = subtotal.ToString("0.00");
            //    cDS.total = total.ToString("0.00");
            //    cDS.setCantidadByOrdenTerminada(idUA);
            //    //For each consulta ODs por idOrden y set to eliminada todas las que su idEstadoDosificacion < 4
            //    DataTable dtPtes = cOD.getPendientes(cOD.idOrden);
            //    foreach (DataRow dr in dtPtes.Rows)
            //    {
            //        cOD.id = int.Parse(dr[0].ToString());
            //        cOD.eliminarPendiente(idUA);
            //    }
            //    lblAuxTerminarOrden.Text = "";
            //}
            //if (lblAuxCancelarOrden.Text.Equals("1"))
            //{
            //    DataTable dtOds = cOD.obtenerOEByIDOrden(int.Parse(hfIdOrden.Value));

            //    foreach (DataRow drOD in dtOds.Rows)
            //    {
            //        cOD.cancelarODs(int.Parse(drOD["id"].ToString()), idUA);
            //        if (!drOD["idUnidadTransporte"].ToString().Equals("1"))
            //            desasignarU(int.Parse(drOD["id"].ToString()), int.Parse(drOD["idUnidadTransporte"].ToString()));
            //    }
            //    lblAuxCancelarOrden.Text = "";
            //}
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopupTC();", true);
            //filtrar();
        }
    }
}