using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Windows;
using System.Text;

namespace despacho
{
    public partial class rProgra : System.Web.UI.Page
    {
        cUtilidades cUtl = new cUtilidades();
        cOrdenesDosificacion cOD = new cOrdenesDosificacion();
        cAlertaP cAP = new cAlertaP();
        DataTable dt = new DataTable();
        DataTable dtAux1 = new DataTable();
        DataTable dtAux2 = new DataTable();
        DataTable dtAux3 = new DataTable();
        DataTable dtAux4 = new DataTable();
        DataTable dtAux5 = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                cUtl.idUsuarioActivo = int.Parse(Request.Cookies["ksroc"]["id"]);
                cOD.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                if (!IsPostBack)
                {
                    filtrar();
                }
            }
            catch (Exception)
            {

            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        { //base.VerifyRenderingInServerForm(control); 
        }

        protected void llenarLVOD(System.Web.UI.WebControls.ListView lv, DataTable dt)
        {
            lv.DataSource = dt;
            lv.DataBind();
        }
        protected void llenarGV(System.Web.UI.WebControls.GridView gv, DataTable dt)
        {
            gv.DataSource = dt;
            gv.DataBind();
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

        protected void ddlFFiltros_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtrar();
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
                    dt = cOD.obtenerODFilteredSinDuplicados(cOD.obtenerODFiltered(sCliente, sVendedor, sCodigo, sUnidad, sChofer, 1, true, yesterday));
                    break;
                case "1":
                    dt = cOD.obtenerODFilteredSinDuplicados(cOD.obtenerODFiltered(sCliente, sVendedor, sCodigo, sUnidad, sChofer, 1, true, today));
                    break;
                case "2":
                    dt = cOD.obtenerODFilteredSinDuplicados(cOD.obtenerODFiltered(sCliente, sVendedor, sCodigo, sUnidad, sChofer, 2, true, tomorrow));
                    break;
                case "5":
                    dt = cOD.obtenerODFilteredSinDuplicados(cOD.obtenerODFiltered(sCliente, sVendedor, sCodigo, sUnidad, sChofer, 5, true, txtFechaI.Text, txtFechaF.Text));
                    break;
            }
            //llenarLVOD(lvOD, dt);
            llenarGV(gvOD, dt);
            llenarGV(gvOD, dt);
            //if (!dt.Rows.Count.Equals(0))
            //{
            //    btnExportar.Visible = true;
            //    btnVerPDF.Visible = true;
            //}
            //else
            //{
            //    btnExportar.Visible = false;
            //    btnVerPDF.Visible = false;
            //}
            if (isCbx.Equals(0))
            {
                dtAux1 = dt.Copy();
                dtAux2 = dt.Copy();
                dtAux3 = dt.Copy();
                dtAux4 = dt.Copy();
                dtAux5 = dt.Copy();
                fillDDL(dtAux1, cbxClientes, "cliente", "clave", "Filtrar por cliente...", 14);
                fillDDL(dtAux2, cbxVendedores, "vendedor", "idV", "Filtrar por Vendedor...", 22);
                fillDDL(dtAux3, cbxProductos, "codigo", "codigo", "Filtrar por Producto...", 9);
                fillDDL(dtAux4, cbxUnidad, "uNombre", "uNombre", "Filtrar por Unidad...", 19);
                fillDDL(dtAux5, cbxChoferes, "chofer", "idCh", "Filtrar por Chofer...", 20);
            }
            return dt;
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            filtrar();
        }
        protected void chbxTerminados_CheckedChanged(object sender, EventArgs e)
        {
        }
        protected void btnExportar_Click(object sender, EventArgs e)
        {
            DataTable dtEx = filtrar();

            GridView gv = new GridView();
            string nombreDoc = "rProgra.pdf";
            string path = Server.MapPath(".");
            //varificar si existe la carptea si no crearla
            if (!Directory.Exists(path + "\\RepProgramacion"))
            {
                Directory.CreateDirectory(path + "\\RepProgramacion");
            }

            //verificamos si existe el archivo
            if (File.Exists(path + "\\RepProgramacion\\" + nombreDoc))
                File.Delete(nombreDoc);

            //indicamos el tamaño de la hoja del pdf
            Document document = new Document(PageSize.LEGAL.Rotate());
            PdfWriter pdf;

            pdf = PdfWriter.GetInstance(document, new FileStream(path + "\\RepProgramacion\\" + nombreDoc, FileMode.Create, FileAccess.Write, FileShare.None));

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + nombreDoc);
            Response.Charset = "UTF-8";
            Response.ContentType = "application/pdf";

            try
            {
                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                    {
                        hw.AddAttribute("font-size", "7pt");
                        gvOD.RenderControl(hw);
                        StringReader sr = new StringReader(sw.ToString());
                        document.Open();
                        XMLWorkerHelper.GetInstance().ParseXHtml(pdf, document, sr);
                        document.Close();
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.Write(document);
                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                Response.End();
            }



            //Response.Clear();
            //Response.Buffer = true;
            //Response.AddHeader("content-disposition", "attachment;filename=" + nombreDoc);
            //Response.Charset = "";
            //Response.ContentType = "application/vnd.ms-excel";
            //using (StringWriter sw = new StringWriter())
            //{
            //    HtmlTextWriter hw = new HtmlTextWriter(sw);

            //    //To Export all pages.
            //    gv.AllowPaging = false;

            //    gv.HeaderRow.BackColor = Color.White;
            //    foreach (TableCell cell in gv.HeaderRow.Cells)
            //    {
            //        cell.BackColor = gv.HeaderStyle.BackColor;
            //    }
            //    foreach (GridViewRow row in gv.Rows)
            //    {
            //        row.BackColor = Color.White;
            //        foreach (TableCell cell in row.Cells)
            //        {
            //            if (row.RowIndex % 2 == 0)
            //            {
            //                cell.BackColor = gv.AlternatingRowStyle.BackColor;
            //            }
            //            else
            //            {
            //                cell.BackColor = gv.RowStyle.BackColor;
            //            }
            //            cell.CssClass = "textmode";
            //        }
            //    }

            //    gv.RenderControl(hw);

            //    //Style to format numbers to string.
            //    string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            //    Response.Write(style);
            //    Response.Output.Write(sw.ToString());
            //    Response.Flush();
            //    Response.End();
            //}


            //try
            //{
            //    //añadimos las cabeceras para la generacion del archivo
            //    Response.Clear();
            //    Response.AddHeader("content-disposition", "attachment;filename=" + DateTime.Now.ToString("dd MMMM yyyy") + ".xls");
            //    Response.Charset = "";
            //    Response.ContentType = "application/vnd.ms-excel";
            //    Response.Cache.SetCacheability(HttpCacheability.NoCache);

            //    //instanciamos un objeto  stringWriter
            //    using (StringWriter sw = new StringWriter())
            //    {
            //        //instanciamos un objeto htmlTextWriter
            //        HtmlTextWriter hw = new HtmlTextWriter(sw);
            //        HtmlForm frm = new HtmlForm();

            //        sw.WriteLine();

            //        lvOD.Parent.Controls.Add(frm);
            //        frm.Controls.Add(lvOD);
            //        frm.RenderControl(hw);
            //        Response.Write(sw.ToString());

            //        Response.Write(sw.ToString());
            //    }


            //}
            //catch (Exception ex)
            //{
            //    throw (ex);
            //}
            //finally
            //{
            //    Response.End();
            //}


            //if (dtEx.Rows.Count > 0)
            //{
            //    StringBuilder sb = new StringBuilder();
            //    StringWriter sw = new StringWriter(sb);
            //    HtmlTextWriter htw = new HtmlTextWriter(sw);
            //    Page pagina = new Page();
            //    HtmlForm form = new HtmlForm();
            //    GridView dg = new GridView();
            //    dg.EnableViewState = false;
            //    dg.DataSource = dtEx;
            //    dg.DataBind();
            //    pagina.EnableEventValidation = false;
            //    pagina.DesignerInitialize();
            //    pagina.Controls.Add(form);
            //    form.Controls.Add(dg);
            //    pagina.RenderControl(htw);
            //    Response.Clear();
            //    Response.Buffer = true;
            //    Response.ContentType = "application/vnd.ms-excel";
            //    Response.AddHeader("Content-Disposition", "attachment;filename=reporteProgramacion.xlsx");
            //    Response.Charset = "UTF-8";
            //    Response.ContentEncoding = Encoding.Default;
            //    Response.Write(sb.ToString());
            //    Response.End();
            //    lblMensaje.Text = "Mensaje: Se terminó el proceso";
            //}

            //string File = "PDFDetails.pdf";
            //ExportListToPDF(dtEx, File);
        }
        protected void btnExportarExcel_Click(object sender, EventArgs e)
        {
            //añadimos las cabeceras para la generacion del archivo
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=ReporteProgramacion" + DateTime.Today.ToShortDateString() + ".xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = System.Text.Encoding.Default;
            Response.ContentType = "application/vnd.ms-excel";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);


            //instanciamos un objeto  stringWriter
            using (StringWriter sw = new StringWriter())
            {
                //instanciamos un objeto htmlTextWriter
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                HtmlForm frm = new HtmlForm();

                sw.WriteLine();

                gvOD.Parent.Controls.Add(frm);
                frm.Controls.Add(gvOD);
                frm.RenderControl(hw);

                Response.Write(sw.ToString());

                Response.End();
            }
        }

        protected void lbtnCloseModalCot_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopupCot();", true);
        }

        protected void btnVerPDF_Click(object sender, EventArgs e)
        {
            string nombreDoc = "rProgra.pdf";
            string path = Server.MapPath(".");

            string htm = "<iframe src ='RepProgramacion/" + nombreDoc + "' width='100%' height='600px' ></iframe>";
            lPDF.Text = htm.ToString();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ShowPopupCot();", true);
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
    }
}