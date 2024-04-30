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
using System.Globalization;

namespace despacho
{
    public partial class reporteEstadoResultados2 : System.Web.UI.Page
    {
        cSucursales cSuc = new cSucursales();
        cReporteResultados cRR = new cReporteResultados();
        cDosificacion cDOS = new cDosificacion();
        cMateriales cMat = new cMateriales();
        int idSucursal = 0;
        DateTime fechaAIni = DateTime.Now;
        DateTime fechaAFin = DateTime.Now;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblAnio.Text += Request.QueryString["anio"];
                hfAnio.Value = Request.QueryString["anio"];
                idSucursal = int.Parse(Request.QueryString["idSucursal"].ToString());

                hfFechaAIni.Value = hfAnio.Value + "/01/01";
                hfFechaAFin.Value = hfAnio.Value + "/12/31";

                fechaAIni = DateTime.Parse(hfFechaAIni.Value);
                fechaAFin = DateTime.Parse(hfFechaAFin.Value);

                imagen.InnerHtml = "<img src='img/pepi_logo.png' width='100' height='78'/>&nbsp;&nbsp;" + cSuc.nombre;

                //llenarLV();
                llenarLVIngresos();

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

        protected void exportarExcel()
        {
            //if (lvIngresos.Rows.Count == 0)
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Reporte", "datos()", true);
            //}
            //else
            //{
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

                    lvIngresos.Parent.Controls.Add(frm);
                    frm.Controls.Add(lvIngresos);
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
            //}
        }

        //protected void llenarLV()
        //{
        //    DataTable dt = new DataTable();
        //    DataTable dtM = cMat.obtenerMateriales();

        //    dt.Columns.Add("Fecha");
        //    foreach (DataRow drM in dtM.Rows)
        //    {
        //        string dtCol = drM["material"].ToString().ToUpper();
        //        dtCol = dtCol.Replace("\r\n", string.Empty);
        //        dt.Columns.Add(dtCol);
        //    }

        //    float[] totales = new float[dt.Columns.Count];

        //    TimeSpan dias = DateTime.Parse(Request.QueryString["FechaFin"]) - DateTime.Parse(Request.QueryString["FechaInicio"]);
        //    int days = dias.Days;
        //    DateTime fecha = DateTime.Parse(Request.QueryString["FechaInicio"]).AddDays(-1);
        //    string col = "";
        //    float qty = 0;

        //    for (int i = 0; i <= days; i++)
        //    {
        //        fecha = fecha.AddDays(1);
        //        totales[0]++;
        //        DataRow rw = dt.NewRow();

        //        rw["Fecha"] = fecha.ToString().Substring(0, 10);

        //        for (int y = 1; y <= dt.Columns.Count -1; y++)
        //        {
        //            col = dt.Columns[y].ToString();
        //            col = col.Replace("\r\n", string.Empty);
        //            qty = cMat.getCantidadByIdMaterialANDFecha(col, int.Parse(Request.QueryString["idSucursal"]), fecha);
        //            rw[col] = qty;
        //            totales[y] += qty;
        //        }
        //        dt.Rows.Add(rw);
        //    }

        //    DataRow lrwArr = dt.NewRow();
        //    for (int i = 0; i < totales.Length; i++)
        //    {
        //        lrwArr[i] = "Total";
        //    }
        //    dt.Rows.Add(lrwArr);

        //    DataRow rwArr = dt.NewRow();
        //    for (int i = 0; i < totales.Length; i++)
        //    {
        //        rwArr[i] = totales[i];
        //    }
        //    dt.Rows.Add(rwArr);

        //    //gv.DataSource = dt;
        //    //gv.DataBind();
        //}

        private void llenarLVIngresos()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ingD0");
            dt.Columns.Add("ingD1");
            dt.Columns.Add("ingDP1");
            dt.Columns.Add("ingD2");
            dt.Columns.Add("ingDP2");
            dt.Columns.Add("ingD3");
            dt.Columns.Add("ingDP3");
            dt.Columns.Add("ingD4");
            dt.Columns.Add("ingDP4");
            dt.Columns.Add("ingD5");
            dt.Columns.Add("ingDP5");
            dt.Columns.Add("ingD6");
            dt.Columns.Add("ingDP6");
            dt.Columns.Add("ingD7");
            dt.Columns.Add("ingDP7");
            dt.Columns.Add("ingD8");
            dt.Columns.Add("ingDP8");
            dt.Columns.Add("ingD9");
            dt.Columns.Add("ingDP9");
            dt.Columns.Add("ingD10");
            dt.Columns.Add("ingDP10");
            dt.Columns.Add("ingD11");
            dt.Columns.Add("ingDP11");
            dt.Columns.Add("ingD12");
            dt.Columns.Add("ingDP12");
            dt.Columns.Add("ingDAcu");
            dt.Columns.Add("ingDPAcu");

            //Crear Array de IngresosBrutoM (Mensual), IngresosDescuentoM y IngresosDescuentoDifM
            //Para sacar la diferencia y obtener IngresosDescuentoDifM = IngresosBrutoM - IngresosDescuentoM mes a mes y IngresosDescuentosDifA = IngresosDescuentosDifA +  IngresosDescuentoDifM

            //Obtenemos primero el dato anual para poder obtener el porcentaje mensual
            DataTable dtIngD = cRR.getIngresos(fechaAIni.ToString("yyyy-MM-dd"), fechaAFin.ToString("yyyy-MM-dd"), idSucursal);
            float ingresoBrutoA = 0;
            float ingresoDescuentosA = 0;
            float ingresoDescuentosADif = 0;
            for (int i = 1; i <= 3; i++)
            {
                DateTime fechaM = fechaAIni;
                DataRow rw = dt.NewRow();

                float cant = 0;
                //Obtenemos mes a mes la cantidad y así poder obtener el porcentaje mensual
                for (int j = 1; j <= 12; j++)
                {
                    if (!j.Equals(1))
                        fechaM = fechaM.AddMonths(1);


                    var firstDayOfMonth = new DateTime(fechaM.Year, fechaM.Month, 1);
                    var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                    DataTable dtIngDM = cRR.getIngresos(firstDayOfMonth.ToString("yyyy-MM-dd"), lastDayOfMonth.ToString("yyyy-MM-dd"), idSucursal);

                    if (i.Equals(1))
                    {
                        cant = float.Parse(dtIngDM.Rows[0]["cantidadEntregada"].ToString()) * float.Parse(dtIngDM.Rows[0]["precioUnitario"].ToString());
                        rw["ingD" + j] = cant.ToString("N1", CultureInfo.InvariantCulture);
                        ingresoBrutoA = ingresoBrutoA + cant;
                    }
                    if (i.Equals(2))
                    {
                        //Como se obtienen las devoluciones
                        rw["ingD" + j] = "S/D";
                        rw["ingDP" + j] = "0";
                    }
                    if (i.Equals(3))
                    {
                        //Tomar solo precioFactor que sean menorres al precio unitario???
                        cant = float.Parse(dtIngDM.Rows[0]["cantidadEntregada"].ToString()) * float.Parse(dtIngDM.Rows[0]["precioFactor"].ToString());
                        rw["ingD" + j] = cant.ToString("N1", CultureInfo.InvariantCulture);
                        ingresoDescuentosA = ingresoDescuentosA + cant;
                    }
                }

                if (i.Equals(1))
                {
                    rw["ingD0"] = "Ventas Brutas";
                    rw["ingDAcu"] = ingresoBrutoA.ToString("N1", CultureInfo.InvariantCulture);
                    rw["ingDPAcu"] = "100%";
                }
                if (i.Equals(2))
                {
                    rw["ingD0"] = "Devoluciones";
                    rw["ingDAcu"] = "S/D";
                    rw["ingDPAcu"] = "100%";
                }
                if (i.Equals(3))
                {
                    ingresoDescuentosADif = ingresoBrutoA - ingresoDescuentosA;
                    rw["ingD0"] = "Descuentos";
                    //rw["ingDAcu"] = ingresoDescuentosADif.ToString("N1", CultureInfo.InvariantCulture);
                    rw["ingDAcu"] = ingresoDescuentosA.ToString("N1", CultureInfo.InvariantCulture);
                    rw["ingDPAcu"] = "100%";
                }

                for (int j = 1; j <= 12; j++)
                {
                    if (i.Equals(1))
                    {
                        rw["ingDP" + j] = getPorcentaje(float.Parse(rw["ingD" + j].ToString()), ingresoBrutoA);
                    }
                    if (i.Equals(2))
                    {
                        //Como se obtienen las devoluciones
                        rw["ingDP" + j] = "0";
                    }
                    if (i.Equals(3))
                    {
                        //Tomar solo precioFactor que sean menorres al precio unitario???
                        rw["ingDP" + j] = getPorcentaje(float.Parse(rw["ingD" + j].ToString()), ingresoDescuentosA);
                    }
                }

                dt.Rows.Add(rw);
            }
            lvIngresos.DataSource = dt;
            lvIngresos.DataBind();
        }

        private string getPorcentaje(float cant, float total)
        {
            float sub = cant / total;
            float porcentaje = sub * 100;
            return porcentaje.ToString("N1", CultureInfo.InvariantCulture) + "%";
        }
    }
}