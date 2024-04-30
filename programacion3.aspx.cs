using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class programacion3 : System.Web.UI.Page
    {
        cUtilidades cUtl = new cUtilidades();
        cOrdenes cOrd = new cOrdenes();
        cOrdenesDosificacion cOD = new cOrdenesDosificacion();
        cUTransporte cUT = new cUTransporte();
        cAlertaP cAP = new cAlertaP();
        cDetallesSolicitud cDS = new cDetallesSolicitud();
        cTipoUT cTUT = new cTipoUT();
        DataTable dt = new DataTable();
        cRemisionPDF cRem = new cRemisionPDF();
        string Documento;
        string Ruta = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            cUtl.idUsuarioActivo = int.Parse(Request.Cookies["login"]["id"]);
            cOD.idSucursal = int.Parse(Request.Cookies["login"]["idSucursal"]);
            cOrd.idSucursal = cOD.idSucursal;
            if (!IsPostBack)
            {
                dt = cOrd.obtenerODSinDuplicados(cOrd.obtenerOrdenesProgramaciónFiltro("", 0, 1, chbxTerminados.Checked, DateTime.Now.ToString("yyyy-MM-dd")));
                llenarLVO(lvO, dt);
            }
        }

        protected void llenarLVO(System.Web.UI.WebControls.ListView lv, DataTable dt)
        {
            lv.DataSource = dt;
            lv.DataBind();
        }

        protected void fillDDL(DataTable dt, DropDownList ddl, string text, string value, string fText)
        {
            ddl.Items.Clear();
            int i = 0;
            ListItem li1 = new ListItem();
            if (i.Equals(0))
            {
                li1.Text = fText;
                li1.Value = "0";
                ddl.Items.Add(li1);
                i++;
            }
            foreach (DataRow dr in dt.Rows)
            {
                try
                {
                    if (dr[0].ToString().Equals(null) || dr[0].ToString().Equals(""))
                        return;
                    ListItem li = new ListItem();
                    li.Text = dr[text].ToString();
                    li.Value = dr[value].ToString();
                    ddl.Items.Add(li);
                }
                catch (Exception)
                {

                }
            }
        }

        private void getAndFillListView(string lvName, string lblIdOrden, ListViewItemEventArgs e, string fIni = null, string fFin = null)
        {
            //find the nested listview
            System.Web.UI.WebControls.ListView listView = e.Item.FindControl(lvName) as System.Web.UI.WebControls.ListView;

            System.Web.UI.WebControls.Label eLbl = (System.Web.UI.WebControls.Label)e.Item.FindControl(lblIdOrden);
            // find the name of current marca

            //here I use linq , you could use other ways only if you could get the perfumes having the current Order
            //DataTable dt1 = cOD.obtenerODDashboard(int.Parse(eLbl.Text));            

            DataTable dtX = cOD.obtenerODDFiltered(int.Parse(eLbl.Text));
            //filtrarDataBound()
            string yesterday = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            string today = DateTime.Now.ToString("yyyy-MM-dd");
            string tomorrow = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            string sCodigo = "";
            int idDdlFOrdenes = 0;
            
            switch (ddlFFiltros.SelectedValue)
            {
                case "0":
                    dtX = cOD.obtenerODDFiltered(int.Parse(eLbl.Text), sCodigo, idDdlFOrdenes, 0, chbxTerminados.Checked, yesterday);
                    break;
                case "1":
                    dtX = cOD.obtenerODDFiltered(int.Parse(eLbl.Text), sCodigo, idDdlFOrdenes, 1, chbxTerminados.Checked, today);
                    break;
                case "2":
                    dtX = cOD.obtenerODDFiltered(int.Parse(eLbl.Text), sCodigo, idDdlFOrdenes, 2, chbxTerminados.Checked, tomorrow);
                    break;
                case "3":
                    dtX = cOD.obtenerODDFiltered(int.Parse(eLbl.Text), sCodigo, idDdlFOrdenes, 3, chbxTerminados.Checked);
                    break;
                case "4":
                    dtX = cOD.obtenerODDFiltered(int.Parse(eLbl.Text), sCodigo, idDdlFOrdenes, 4, chbxTerminados.Checked);
                    break;
                case "5":
                    dtX = cOD.obtenerODDFiltered(int.Parse(eLbl.Text), sCodigo, idDdlFOrdenes, 5, chbxTerminados.Checked, txtFechaI.Text, txtFechaF.Text);
                    break;

            }

            DataTable dt1 = quitarDuplicadosOD(dtX);

            //Agregar columnas
            //dt1.Columns.Add("colorBell");
            dt1.Columns.Add("antIdEstado");
            dt1.Columns.Add("antEstado");
            dt1.Columns.Add("sigIdEstado");
            dt1.Columns.Add("sigEstado");
            int idAntE = 0;
            int idSigE = 0;

            DataTable dtA = cAP.obtenerAlertasByIdSucursal(int.Parse(Request.Cookies["login"]["idSucursal"]));
            foreach (DataRow dr in dt1.Rows)
            {
                try
                {
                    if (dr["colorBell"].ToString().Equals("") || dr["colorBell"].ToString().Equals(null))
                        dr["colorBell"] = cOD.getDiffTime(DateTime.Parse(dr["fecha"].ToString().Substring(0, 11) + dr["hora"].ToString().Substring(0, 5) + ":00"), dtA);

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
                            dr["antEstado"] = cOD.getEstadoDosificacion(idAntE);
                            dr["sigEstado"] = cOD.getEstadoDosificacion(idSigE);
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

                }
            }
            listView.DataSource = dt1;
            listView.DataBind();

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

        protected void lvO_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            getAndFillListView("lvOD", "lblIDOrden", e);
            //System.Web.UI.WebControls.Label eLbl = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblIDOrden");
            //fillLVOD2(cOD.obtenerODDashboard(int.Parse(eLbl.Text)));
        }

        protected void lvO_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            //find the nested listview
            System.Web.UI.WebControls.ListView listView = e.Item.FindControl("lvOD") as System.Web.UI.WebControls.ListView;

        }

        protected void ddlFFiltros_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void filtrar(int callControl = 0)
        {
            string yesterday = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            string today = DateTime.Now.ToString("yyyy-MM-dd");
            string tomorrow = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            lblFechaI.Visible = false;
            txtFechaI.Visible = false;
            lblFechaF.Visible = false;
            txtFechaF.Visible = false;
            btnFiltrar.Visible = false;
            dt.Clear();
            string sCodigo = "";
            int idDdlFOrdenes = 0;
            
            switch (ddlFFiltros.SelectedValue)
            {
                case "0":
                    dt = cOrd.obtenerODSinDuplicados(cOrd.obtenerOrdenesProgramaciónFiltro(sCodigo, idDdlFOrdenes, 0, chbxTerminados.Checked, yesterday));
                    break;
                case "1":
                    dt = cOrd.obtenerODSinDuplicados(cOrd.obtenerOrdenesProgramaciónFiltro(sCodigo, idDdlFOrdenes, 1, chbxTerminados.Checked, today));
                    break;
                case "2":
                    dt = cOrd.obtenerODSinDuplicados(cOrd.obtenerOrdenesProgramaciónFiltro(sCodigo, idDdlFOrdenes, 2, chbxTerminados.Checked, tomorrow));
                    break;
                case "3":
                    dt = cOrd.obtenerODSinDuplicados(cOrd.obtenerOrdenesProgramaciónFiltro(sCodigo, idDdlFOrdenes, 3, chbxTerminados.Checked));
                    break;
                case "4":
                    dt = cOrd.obtenerODSinDuplicados(cOrd.obtenerOrdenesProgramaciónFiltro(sCodigo, idDdlFOrdenes, 4, chbxTerminados.Checked));
                    break;
                case "5":
                    lblFechaI.Visible = true;
                    txtFechaI.Visible = true;
                    lblFechaF.Visible = true;
                    txtFechaF.Visible = true;
                    btnFiltrar.Visible = true;
                    return;
            }
            llenarLVO(lvO, dt);
        }

        protected void ddlFClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtrar(1);
        }

        protected void ddlFOrdenes_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtrar(2);
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            dt.Clear();
            string sCodigo = "";
            int idDdlFOrdenes = 0;
            
            dt = cOrd.obtenerODSinDuplicados(cOrd.obtenerOrdenesProgramaciónFiltro(sCodigo, idDdlFOrdenes, 5, chbxTerminados.Checked, txtFechaI.Text, txtFechaF.Text));

            llenarLVO(lvO, dt);
        }

        protected void lbtnAsignarU_Command(object sender, CommandEventArgs e)
        {
            try
            {
                string[] arr;
                arr = e.CommandArgument.ToString().Split('ˇ');
                if (arr[1].Equals("Asignar"))
                {
                    //lblIdOD.Text = arr[0];
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ShowPopupAsigU();", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PopUP", "openPAU(" + arr[0].ToString() + ")", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PopUP", "openPMAP(" + arr[0].ToString() + ")", true);
                }
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Modal", "opnMdl('#asignarUnidadModal')", true);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string protectedfillLVOD2(DataTable dt)
        {
            string tabla = "";

            foreach (DataRow dr in dt.Rows)
            {
                tabla = "<table style='width: 100%;'>";
                tabla += "<tr>";
                //Primer TD
                tabla += "<td style='width: 70%;'>";
                tabla += "<asp:Label ID='Label3' runat='server' Text='Remisión: " + dr["id"].ToString();
                tabla += " - Hora: " + dr["hora"].ToString() + " - Chofer: " + dr["chofer"].ToString() + " Estado: " + dr["estado"].ToString();
                tabla += " Progreso: " + dr["porcentaje"].ToString() + "% Completo'>";
                tabla += "</asp:Label></td>";
                //SegundoTD
                tabla += "<td style='text-align: center; width: 10%;'>";
                tabla += "<asp:LinkButton ID='lbtnAsignarU' runat='server' CommandName='xAssignUnit' CommandArgument='" + dr["id"].ToString() + "' OnCommand='lbtnAsignarU_Command'>";
                tabla += "<i class='icon-truck2'>" + dr["uTransporte"].ToString() + "</i></asp:LinkButton>";
                tabla += "</td>";
                //TercerTD
                tabla += "<td style='text-align: center; width: 10%;'>";
                tabla += "<asp:LinkButton ID='lbtnBitacora' runat='server'><i class='icon-file-text2'></i>Bitácora</asp:LinkButton>";
                tabla += "</td>";
                //CuartoTD
                tabla += "<td style='text-align: center; width: 10%;'>";
                tabla += "<asp:LinkButton ID='lbtnCalidad' runat='server'><i class='icon-clipboard'></i>Calidad</asp:LinkButton>";
                tabla += "</td>";
                //QuintoTD
                tabla += "<td style='text-align: center; width: 10%;'>";
                tabla += "<asp:LinkButton ID='lbtnCalidad' runat='server'><i class='icon-clipboard'></i>Calidad</asp:LinkButton>";
                tabla += "</td>";

                //TR ProgressBar
                tabla += "<tr>";
                tabla += "<td colspan='4'>";
                tabla += "<div style='background-color: lightgrey; height: 10px; width: 100%'>";
                tabla += "<div class='progress-bar' role='progressbar' aria-valuenow='" + dr["porcentaje"].ToString() + "' aria-valuemin='0' aria-valuemax='100' style='width: " + dr["porcentaje"].ToString();
                tabla += "%; background-color: " + dr["color"].ToString() + ">";
                tabla += "</div>";
                tabla += "</div>";
                tabla += "</td>";

                tabla += "</tr>";
                tabla += "</table>";
                return tabla;
            }
            return "";
        }

        protected void lbtnBitacora_Command(object sender, CommandEventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PopUP", "openPBIT(" + e.CommandArgument.ToString() + ")", true);
        }

        protected void lbtnCalidad_Command(object sender, CommandEventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PopUP", "openPCALIDAD(" + e.CommandArgument.ToString() + ")", true);
        }

        protected void lbtnDosificar_Command(object sender, CommandEventArgs e)
        {
            string[] arr;
            arr = e.CommandArgument.ToString().Split('ˇ');
            if (!arr[3].Equals(""))
            {
                cOD.idEstadoDosificacion = int.Parse(arr[1].ToString());
                cOD.actualizarEstadoDosificacion(int.Parse(arr[0].ToString()), int.Parse(Request.Cookies["login"]["id"]));
                if (cOD.getEstadoDosificacion(cOD.idEstadoDosificacion).Equals("Terminado"))
                {
                    cUT.idEstadoUnidad = 1;
                    cUT.actualizarEstado(int.Parse(arr[2].ToString()), int.Parse(Request.Cookies["login"]["id"]));
                    cDS.setCantidadEntregada(int.Parse(arr[5]), float.Parse(arr[6]), int.Parse(Request.Cookies["login"]["id"]));
                }
                cOD.colorBell = arr[4].ToString();
                if (cOD.idEstadoDosificacion > 4)
                    cOD.asignarColorBell(int.Parse(arr[0]), int.Parse(Request.Cookies["login"]["id"]));
                filtrar();
            }
        }

        protected void lbtnBackDosificar_Command(object sender, CommandEventArgs e)
        {
            string[] arr;
            arr = e.CommandArgument.ToString().Split('ˇ');
            if (!arr[3].Equals(""))
            {
                cOD.idEstadoDosificacion = int.Parse(arr[1].ToString());
                if (cOD.getEstadoDosificacion(cOD.idEstadoDosificacion).Equals("Dosificar"))
                {
                    cOD.liberarUnidadT(int.Parse(arr[0].ToString()), int.Parse(Request.Cookies["login"]["id"]));
                    cUT.idEstadoUnidad = 1;
                    cUT.actualizarEstado(int.Parse(arr[2].ToString()), int.Parse(Request.Cookies["login"]["id"]));
                }
                cOD.actualizarEstadoDosificacion(int.Parse(arr[0].ToString()), int.Parse(Request.Cookies["login"]["id"]));
                filtrar();
                //Response.Redirect("programacion.aspx");
            }
        }

        protected void mlbtnCloseMdlAjuste_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopupAjuste();", true);
        }

        protected void mbtnCloseAjuste_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopupAjuste();", true);
        }

        protected void mbtnAceptarmbtnCloseAjuste_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopupAjuste();", true);
        }
        protected void mbtAceptarMdl_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopup();", true);
        }

        protected void mbtnCloseMdl_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopup();", true);
        }

        protected void mlbtnCloseMdl_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopup();", true);
        }


        protected void lBtnAsignar_Command(object sender, CommandEventArgs e)
        {
            //asignarU(int.Parse(lblIdOD.Text), int.Parse(e.CommandArgument.ToString()));
        }

        private bool asignarU(int idOD, int idU)
        {
            int idUA = int.Parse(Request.Cookies["login"]["id"]);
            try
            {
                cUT.idEstadoUnidad = 2;
                cUT.actualizarEstado(idU, idUA);
                cOD.idUnidadTransporte = idU;
                cOD.asignarUnidadTransporte(idOD, idUA);
                cOD.idEstadoDosificacion = 4;
                cOD.actualizarEstadoDosificacion(idOD, idUA);
                //lblMensaje.Text = "Se asignó la unidad Correctamente, favor de cerrar ésta ventana y actualizar Programación";
                return true;
            }
            catch (Exception)
            {
                //lblMensaje.Text = "No se pudo asignar la unidad Correctamente, favor de cerrar ésta ventana y actualizar Programación";
                return false;
                throw;
            }
        }

        protected void chbxProgramadas_CheckedChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        protected void mbtnAjuste_Click(object sender, EventArgs e)
        {

        }

        protected void btnCrearOD_Click(object sender, EventArgs e)
        {

        }

        protected void btnModOD_Click(object sender, EventArgs e)
        {

        }

        protected void llenarDdlTU(int idTP)
        {
            //ddlTU.Items.Clear();
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("tipo");

            dt.Rows.Add("0", "Selecciona un tipo de unidad");

            DataTable dt1 = cTUT.obtenerUTByIdTipoProductoAndIsCarga(idTP);
            foreach (DataRow dr in dt1.Rows)
            {
                dt.Rows.Add(dr["id"].ToString(), (dr["tipo"].ToString() + " - Límite: " + dr["capacidad"].ToString() + " - " + dr["unidad"].ToString()));
            }

            //ddlTU.DataSource = dt;
            //ddlTU.DataValueField = "id";
            //ddlTU.DataTextField = "tipo";
            //ddlTU.DataBind();
        }

        protected void lbtnAjuste_Command(object sender, CommandEventArgs e)
        {
            //string[] arr;
            //arr = e.CommandArgument.ToString().Split('ˇ');
            //mlblTitleAjuste.Text = "AJUSTE";
            //txtCantSolAjuste.Text = arr[6];

            //txtFechaDosificacion.Text = arr[2].ToString().Substring(0,10);
            //fillHoraOE(arr[3]);
            //mlblCodigoP.Text = arr[4];
            //mlblDescP.Text = arr[5];
            //mlblCantOrd.Text = arr[6];
            //lblUDM1Dosificacion.Text = arr[7];
            //mlblRevP.Text = arr[8];
            //if (mlblRevP.Text.Equals("") || mlblRevP.Text.Equals("0"))
            //{
            //    mlblRevLbl.Visible = false;
            //    mlblRevP.Visible = false;
            //}
            //else
            //{
            //    mlblRevLbl.Visible = true;
            //    mlblRevP.Visible = true;
            //}

            //mlblIdOrdenAjuste.Text = arr[0];
            //mlblIdProductoAjuste.Text = arr[10];
            //hfIdS.Text = arr[11];
            //hfIdDS.Text= arr[12];

            //llenarDdlTU(int.Parse(arr[1]));
            //llenarLVOD(int.Parse(arr[0]));
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ShowPopupAjuste();", true);
        }
        private void fillHoraOE(string sHora)
        {
            //cbxHoraD.SelectedValue = sHora.Substring(0, 2);
            //cbxMinutosD.SelectedValue = sHora.Substring(3, 2);
        }

        protected void lvOD_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                string[] arr;
                arr = e.CommandArgument.ToString().Split('ˇ');

                //if (e.CommandName.Equals("modificarOD"))
                //{
                //    txtFechaDosificacion.Text = arr[1].Substring(0, 10);
                //    fillHoraOE(arr[2]);
                //    mlblCodigoP.Text = arr[5];
                //    mlblDescP.Text = arr[6];
                //    txtCantidadDosificacion.Text = arr[7];
                //    if (arr[9].Equals("") || arr[9].Equals("0"))
                //    {
                //        mlblRevLbl.Visible = false;
                //        mlblRevP.Visible = false;
                //    }
                //    else
                //    {
                //        mlblRevLbl.Visible = true;
                //        mlblRevP.Visible = true;
                //        mlblRevP.Text = arr[9];
                //    }
                //    ddlTU.SelectedValue = arr[10];
                //    hfCapMaxUnidad.Text = cTUT.obtenerCargaMax(int.Parse(ddlTU.SelectedValue));
                //    btnCrearOD.Visible = false;
                //    btnModOD.Visible = true;
                //    hfEnableMod.Text = "1";

                //}
                //if (e.CommandName.Equals("eliminarOD"))
                //{
                //    this.mlblTitle.Text = "¡¡¡CONFIRMACIÓN!!!";
                //    if (cOD.getidEDByIdOD(int.Parse(arr[0].ToString())) < 5)
                //    {
                //        lblAux.Text = "";
                //        this.mlblMessage.Text = "¿Estás seguro que deseas eliminar la solicitud con folio: " + arr[1] + " del sistema?";
                //    }
                //    else
                //    {
                //        lblAux.Text = "1";
                //        this.mlblMessage.Text = "La orden de Entrega NO se eliminó debido a que ya está en proceso de dosificación";
                //    }
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ShowPopup();", true);
                //}
            }
            catch (Exception)
            {

                throw;
            }
        }

        private bool llenarLVOD(int idO)
        {
            try
            {
                DataTable dt = cOD.obtenerODByIdOrden(idO);
                //lvOD.DataSource = dt;
                //lvOD.DataBind();

                if (dt.Rows.Count > 0)
                {
                    //cOrd.setProgramada(idO, true, int.Parse(Request.Cookies["login"]["id"]));
                    return true;
                }
                else
                {
                    //cOrd.setProgramada(idO, false, int.Parse(Request.Cookies["login"]["id"]));
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        private bool restoDosificar(int idO, int idP, float tMax, float qty = 0)
        {
            float qtyDB = cOD.obtenerQtyByOrdenAndIdProducto(idO, idP);
            float total = qtyDB + qty;
            float resto = tMax - total;
            //hfRestan.Text = resto.ToString();
            //if (hfEnableMod.Text.Equals("1"))
            //{
            //    hfEnableMod.Text = "";
            //    return true;
            //}
            //if (resto == 0)
            //{
            //    cDS.setProgramada(int.Parse(hfIdDS.Text), int.Parse(Request.Cookies["login"]["id"]));
            //    checkProgramada();
            //    lblMensaje.Text = "Ya se generaron las Ordenes de entrega necesarias para éste producto";
            //    return true;
            //}
            //if (total <= tMax)
            //{
            //    return true;
            //}
            //if (resto <= float.Parse(hfCapMaxUnidad.Text))
            //{
            //    txtCantidadDosificacion.Text = (tMax - qtyDB).ToString();
            //    lblMensaje.Text = "El restante para igualar la Cantidad Ordenada es por " + (tMax - qtyDB) + " " + lblUDM1Dosificacion.Text;
            //    return false;
            //}
            //if (total > tMax)
            //{
            //    txtCantidadDosificacion.Text = hfCapMaxUnidad.Text;
            //    lblMensaje.Text = "La cantidad ingresada sobrepasa la Cantidad Ordenada por: " + resto + " " + lblUDM1Dosificacion.Text;
            //    return false;
            //}
            //if ((qty <= resto) && (!qty.Equals(0)))
            //{
            //    return true;
            //}
            return false;
        }

        private bool checkProgramada()
        {
            //DataTable dt = cDS.obtenerDetallesSolicitud(int.Parse(hfIdS.Text));
            //if (!cOrd.esProgramada(int.Parse(mlblIdOrdenAjuste.Text)))
            //{
            //    int vcs = 0;
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        if (dr["programada"].Equals(true))
            //            vcs++;
            //    }
            //    if (dt.Rows.Count.Equals(vcs))
            //    {
            //        if (vcs > 0)
            //        {
            //            return true;
            //        }
            //        return false;
            //    }
            //    return false;
            //}
            return true;
        }

        protected void mbtnAceptarRemPDF_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopupARemPDF();", true);
        }

        protected void mbtnCloseRemPDF_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopupARemPDF();", true);
        }

        protected void mBtnGenerarRemPDF_Click(object sender, EventArgs e)
        {
            cRem.nombreDoc = "Remision";
            cRem.extencion = ".pdf";
            cRem.path = Server.MapPath(".");
            cRem.idOD = int.Parse(mlblIdODRem.Text);
            mlblMensajeRemPDF.Text = cRem.generarPdf();
            Documento = cRem.nombreDoc + cRem.folio.ToString() + cRem.extencion;

            mhfDocumento.Value = Documento;

            Ruta = Server.MapPath(@"Remisiones");
            mhfRuta.Value = Ruta + "\\";
            //lblMensaje.Text += "Cotizacion/" + Documento;
            string htm = "<iframe src ='Remisiones/" + Documento + "' width='100%' height='600px' ></iframe>";
            mlPDF.Text = htm;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ShowPopupRemPDF();", true);
        }

        protected void lbtnImprimirRemision_Command(object sender, CommandEventArgs e)
        {
            mlblIdODRem.Text = e.CommandArgument.ToString();
            mlPDF.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ShowPopupRemPDF();", true);
        }

        protected void mBtnTittleCloseRemPDF_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopupARemPDF();", true);
        }

        protected void mBtnTittleCloseAsigU_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopupAsigU();", true);
        }

        protected void mbtnCloseAsigU_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopupAsigU();", true);
        }

        protected void mbtnAceptarAsigU_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopupAsigU();", true);
        }

        protected void chbxTerminados_CheckedChanged(object sender, EventArgs e)
        {
            filtrar();
        }
    }
}