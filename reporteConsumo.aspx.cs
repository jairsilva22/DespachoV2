using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace despacho
{
    public partial class reporteConsumo : System.Web.UI.Page
    {
        cSucursales cSuc = new cSucursales();
        cMateriales cMat = new cMateriales();
        int idSucursal = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblFechaInicio.Text += Request.QueryString["FechaInicio"];
                lblFechaFin.Text += Request.QueryString["FechaFin"];

                imagen.InnerHtml = "<img src='img/pepi_logo.png' width='100' height='78'/>&nbsp;&nbsp;" + cSuc.nombre;

                llenarLV();

                if (Request.QueryString["Excel"] != "" && Request.QueryString["Excel"] != null)
                {
                    exportarExcel();

                    //if (lvBacheos.Items.Count == 0)
                    //{
                    //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No se puede generar Excel sin datos')", true);
                    //}
                    Response.Write("<script> window.close(); </script>");
                }
            }
        }
        protected void llenarLV()
        {
            DataTable dt = new DataTable();
            DataTable dtM = cMat.obtenerMateriales();

            dt.Columns.Add("Fecha");
            foreach (DataRow drM in dtM.Rows)
            {
                string dtCol = drM["material"].ToString().ToUpper();
                dtCol = dtCol.Replace("\r\n", string.Empty);
                dt.Columns.Add(dtCol);
            }

            float[] totales = new float[dt.Columns.Count];

            TimeSpan dias = DateTime.Parse(Request.QueryString["FechaFin"]) - DateTime.Parse(Request.QueryString["FechaInicio"]);
            int days = dias.Days;
            DateTime fecha = DateTime.Parse(Request.QueryString["FechaInicio"]).AddDays(-1);
            string col = "";
            float qty = 0;

            for (int i = 0; i <= days; i++)
            {
                fecha = fecha.AddDays(1);
                totales[0]++;
                DataRow rw = dt.NewRow();

                rw["Fecha"] = fecha.ToString().Substring(0, 10);
                
                for (int y = 1; y <= dt.Columns.Count -1; y++)
                {
                    col = dt.Columns[y].ToString();
                    col = col.Replace("\r\n", string.Empty);
                    qty = cMat.getCantidadByIdMaterialANDFecha(col, int.Parse(Request.QueryString["idSucursal"]), fecha);
                    rw[col] = qty;
                    totales[y] += qty;
                }
                dt.Rows.Add(rw);
            }

            DataRow lrwArr = dt.NewRow();
            for (int i = 0; i < totales.Length; i++)
            {
                lrwArr[i] = "Total";
            }
            dt.Rows.Add(lrwArr);

            DataRow rwArr = dt.NewRow();
            for (int i = 0; i < totales.Length; i++)
            {
                rwArr[i] = totales[i];
            }
            dt.Rows.Add(rwArr);

            gv.DataSource = dt;
            gv.DataBind();
        }

        protected void exportarExcel()
        {
            if (gv.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Reporte", "datos()", true);
            }
            else
            {
                try
                {
                    //añadimos las cabeceras para la generacion del archivo
                    Response.Clear();
                    Response.AddHeader("content-disposition", "attachment;filename=ReporteConsumo" + DateTime.Today.ToShortDateString() + ".xls");
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

                        gv.Parent.Controls.Add(frm);
                        frm.Controls.Add(gv);
                        frm.RenderControl(hw);

                        Response.Write(sw.ToString());
                    }
                }
                catch (Exception ex)
                {

                    throw (ex);
                }
                finally
                {
                    Response.End();
                }
            }
        }
    }
}