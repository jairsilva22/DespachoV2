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
    public partial class reporteEstadoResultados28122021 : System.Web.UI.Page
    {
        cSucursales cSuc = new cSucursales();
        cReporteResultados cRR = new cReporteResultados();
        cDosificacion cDOS = new cDosificacion();
        cMateriales cMat = new cMateriales();
        int idSucursal = 0, idSucursalCompras = 0;
        bool continuar = true;
        DateTime fechaAIni = DateTime.Now;
        DateTime fechaAFin = DateTime.Now;
        float[] arrVN = new float[13];
        float[] arrCV = new float[13];
        float[] arrUB = new float[13];
        float[] arrGV = new float[13];
        float[] arrGA = new float[13];
        float[] arrUO = new float[13];


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                for (int i = 0; i < 13; i++)
                {
                    arrVN[i] = 0;
                    arrCV[i] = 0;
                    arrUB[i] = 0;
                    arrGV[i] = 0;
                    arrGA[i] = 0;
                    arrUO[i] = 0;
                }
                lblAnio.Text += Request.QueryString["anio"];
                hfAnio.Value = Request.QueryString["anio"];
                idSucursal = int.Parse(Request.QueryString["idSucursal"].ToString());
                idSucursalCompras = cSuc.obtenerComprasID(idSucursal);

                hfFechaAIni.Value = hfAnio.Value + "/01/01";
                hfFechaAFin.Value = hfAnio.Value + "/12/31";

                fechaAIni = DateTime.Parse(hfFechaAIni.Value);
                fechaAFin = DateTime.Parse(hfFechaAFin.Value);

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

                    lv.Parent.Controls.Add(frm);
                    frm.Controls.Add(lv);
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

        private void llenarLV()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("celD0");
            dt.Columns.Add("celD1");
            dt.Columns.Add("celDP1");
            dt.Columns.Add("celD2");
            dt.Columns.Add("celDP2");
            dt.Columns.Add("celD3");
            dt.Columns.Add("celDP3");
            dt.Columns.Add("celD4");
            dt.Columns.Add("celDP4");
            dt.Columns.Add("celD5");
            dt.Columns.Add("celDP5");
            dt.Columns.Add("celD6");
            dt.Columns.Add("celDP6");
            dt.Columns.Add("celD7");
            dt.Columns.Add("celDP7");
            dt.Columns.Add("celD8");
            dt.Columns.Add("celDP8");
            dt.Columns.Add("celD9");
            dt.Columns.Add("celDP9");
            dt.Columns.Add("celD10");
            dt.Columns.Add("celDP10");
            dt.Columns.Add("celD11");
            dt.Columns.Add("celDP11");
            dt.Columns.Add("celD12");
            dt.Columns.Add("celDP12");
            dt.Columns.Add("celDAcu");
            //dt.Columns.Add("celDPAcu");

            if (continuar)
            {
                //Obtenemos primero el dato anual para poder obtener el porcentaje mensual
                //DataTable dtIngD = cRR.getIngresos(fechaAIni.ToString("yyyy-MM-dd"), fechaAFin.ToString("yyyy-MM-dd"), idSucursal);
                float ingresoBrutoM = 0;
                float ingresoDescuentosM = 0;
                float ingresoBrutoA = 0;
                float ingresoDescuentosA = 0;
                float ingresoDescuentosMDif = 0;
                float ingresoDescuentosADif = 0;
                int idP = 0;
                DataTable dtProductosM = new DataTable();
                DataTable dtIngDM = new DataTable();
                for (int i = 1; i <= 4; i++)
                {
                    DateTime fechaM = fechaAIni;
                    DataRow rw = dt.NewRow();

                    float cant = 0;
                    //Obtenemos mes a mes la cantidad y así poder obtener el porcentaje mensual
                    for (int j = 1; j <= 12; j++)
                    {
                        ingresoBrutoM = 0;
                        ingresoDescuentosM = 0;
                        ingresoDescuentosMDif = 0;
                        if (!j.Equals(1))
                            fechaM = fechaM.AddMonths(1);

                        var firstDayOfMonth = new DateTime(fechaM.Year, fechaM.Month, 1);
                        var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                        dtProductosM = cRR.getProductos(firstDayOfMonth.ToString("yyyy-MM-dd"), lastDayOfMonth.ToString("yyyy-MM-dd"), idSucursal);

                        foreach (DataRow drP in dtProductosM.Rows)
                        {
                            idP = int.Parse(drP["idProducto"].ToString());
                            //otro foreach para sumar la cantidad bruta y descuentos de el producto
                            dtIngDM = cRR.getIngresosM(firstDayOfMonth.ToString("yyyy-MM-dd"), lastDayOfMonth.ToString("yyyy-MM-dd"), idP, idSucursal);
                            foreach (DataRow drIM in dtIngDM.Rows)
                            {
                                ingresoBrutoM += float.Parse(drIM["Bruto"].ToString());
                                ingresoDescuentosM += float.Parse(drIM["Descuento"].ToString());
                                ingresoDescuentosMDif = ingresoBrutoM - ingresoDescuentosM;
                            }
                        }
                        //DataTable dtIngDM = cRR.getIngresos(firstDayOfMonth.ToString("yyyy-MM-dd"), lastDayOfMonth.ToString("yyyy-MM-dd"), idSucursal);

                        if (i.Equals(1))
                        {
                            //cant = float.Parse(dtIngDM.Rows[0]["cantidadEntregada"].ToString()) * float.Parse(dtIngDM.Rows[0]["precioUnitario"].ToString());
                            rw["celD" + j] = ingresoBrutoM.ToString("N1", CultureInfo.InvariantCulture);
                            ingresoBrutoA += ingresoBrutoM;
                        }
                        if (i.Equals(2))
                        {
                            //Como se obtienen las devoluciones
                            rw["celD" + j] = "0";
                            rw["celDP" + j] = "";
                        }
                        if (i.Equals(3))
                        {
                            ingresoDescuentosMDif = ingresoBrutoM - ingresoDescuentosM;
                            ingresoDescuentosADif += ingresoDescuentosMDif;
                            //Tomar solo precioFactor que sean menorres al precio unitario???
                            //cant = float.Parse(dtIngDM.Rows[0]["cantidadEntregada"].ToString()) * float.Parse(dtIngDM.Rows[0]["precioFactor"].ToString());
                            rw["celD" + j] = ingresoDescuentosMDif.ToString("N1", CultureInfo.InvariantCulture);
                            //ingresoDescuentosA = ingresoDescuentosA + ingresoDescuentosM;
                        }
                        if (i.Equals(4))
                        {
                            rw["celD" + j] = (ingresoBrutoM - ingresoDescuentosMDif).ToString("N1", CultureInfo.InvariantCulture);
                            arrVN[j - 1] = ingresoBrutoM - ingresoDescuentosMDif;
                        }
                    }

                    if (i.Equals(1))
                    {
                        rw["celD0"] = "Ventas Brutas";
                        rw["celDAcu"] = ingresoBrutoA.ToString("N1", CultureInfo.InvariantCulture);
                        //rw["celDPAcu"] = "100%";
                    }
                    if (i.Equals(2))
                    {
                        rw["celD0"] = "Devoluciones";
                        rw["celDAcu"] = "0";
                        //rw["celDPAcu"] = "100%";
                    }
                    if (i.Equals(3))
                    {
                        //ingresoDescuentosADif = ingresoBrutoA - ingresoDescuentosA;
                        rw["celD0"] = "Descuentos";
                        //rw["celDAcu"] = ingresoDescuentosADif.ToString("N1", CultureInfo.InvariantCulture);
                        rw["celDAcu"] = ingresoDescuentosADif.ToString("N1", CultureInfo.InvariantCulture);
                        //rw["celDPAcu"] = "100%";
                    }
                    if (i.Equals(4))
                    {
                        rw["celD0"] = "Ventas Netas";
                        rw["celDAcu"] = (ingresoBrutoA - ingresoDescuentosADif).ToString("N1", CultureInfo.InvariantCulture);
                        arrVN[12] = ingresoBrutoA - ingresoDescuentosADif;
                        //rw["celDPAcu"] = "100%";
                    }

                    for (int j = 1; j <= 12; j++)
                    {
                        if (i.Equals(1))
                        {
                            //rw["celDP" + j] = getPorcentaje(float.Parse(rw["celD" + j].ToString()), ingresoBrutoA);
                        }
                        if (i.Equals(2))
                        {
                            //Como se obtienen las devoluciones
                            //rw["celDP" + j] = "0";
                        }
                        if (i.Equals(3))
                        {
                            //Tomar solo precioFactor que sean menorres al precio unitario???
                            //rw["celDP" + j] = getPorcentaje(float.Parse(rw["celD" + j].ToString()), ingresoDescuentosADif);
                        }
                        if (i.Equals(4))
                        {
                            //rw["celDP" + j] = getPorcentaje(float.Parse(rw["celD" + j].ToString()), (ingresoBrutoA + ingresoDescuentosADif));
                        }
                    }

                    dt.Rows.Add(rw);
                }
            }

            if (continuar)
            {
                //Costo de VENTA
                DataTable dtBitacora = new DataTable();
                DataTable dtCostoProduccion = new DataTable();
                DataTable dtNomina = new DataTable();

                DateTime fechaMCostoVenta = fechaAIni;
                DataRow rwCM = dt.NewRow();
                DataRow rwCP = dt.NewRow();
                DataRow rwNomina = dt.NewRow();
                DataRow rwCostoVenta = dt.NewRow();

                rwCM[0] = "Costo de Materiales";
                rwCP[0] = "Costo de Producción";
                rwNomina[0] = "Nomina Producción";
                rwCostoVenta[0] = "Costo de Venta";

                float cantCM = 0;
                float cantM = 0;
                float cantA = 0;
                float cantCP = 0;
                float cantCPM = 0;
                float cantCPA = 0;
                float cantNomina = 0;
                float cantNominaM = 0;
                float cantNominaA = 0;
                float cantCostoVenta = 0;
                float cantCostoVentaM = 0;
                float cantCostoVentaA = 0;
                //Obtenemos mes a mes la cantidad y así poder obtener el porcentaje mensual
                for (int j = 1; j <= 12; j++)
                {
                    if (!j.Equals(1))
                        fechaMCostoVenta = fechaMCostoVenta.AddMonths(1);

                    var firstDayOfMonth = new DateTime(fechaMCostoVenta.Year, fechaMCostoVenta.Month, 1);
                    var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                    //Obtenemos Bitacora Salida Materiales se deberá quitar cuando ya se estén generando las salidas
                    dtBitacora = cRR.getBitacora(firstDayOfMonth.ToString("yyyy-MM-dd"), lastDayOfMonth.ToString("yyyy-MM-dd"), idSucursal);
                    //Obtenemos todas las salidas de compras hacia el estado de resultados de producción
                    dtCostoProduccion = cRR.getSalidasCompras(firstDayOfMonth.ToString("yyyy-MM-dd"), lastDayOfMonth.ToString("yyyy-MM-dd"), 1, idSucursalCompras);
                    //Obtenemos Nomina
                    dtNomina = cRR.getNomina(1, fechaMCostoVenta.Month, idSucursal);

                    cantM = 0;
                    cantCP = 0;
                    cantNominaM = 0;
                    cantCostoVentaM = 0;
                    foreach (DataRow drB in dtBitacora.Rows)
                    {
                        cantCM = 0;
                        cantCM = float.Parse(drB["cantidad"].ToString()) * float.Parse(drB["costoKG"].ToString());
                        cantM = cantM + cantCM;

                    }
                    foreach (DataRow drC in dtCostoProduccion.Rows)
                    {
                        cantCP = 0;
                        cantCP = float.Parse(drC["total"].ToString());
                        cantCPM = cantCPM + cantCP;

                    }
                    cantNominaM = 0;
                    foreach (DataRow drN in dtNomina.Rows)
                    {
                        cantNomina = 0;
                        cantNomina = float.Parse(drN["monto"].ToString());
                        cantNominaM = cantNominaM + cantNomina;
                    }
                    cantCostoVentaM = cantM + cantNominaM + cantCPM;
                    arrCV[j - 1] = cantCostoVentaM;
                    if (j.Equals(1))
                    {
                        rwCM[1] = cantM.ToString("N1", CultureInfo.InvariantCulture);
                        rwCM[2] = getPorcentaje(cantM, arrVN[0]);
                        rwCP[1] = cantCPM.ToString("N1", CultureInfo.InvariantCulture);
                        rwCP[2] = getPorcentaje(cantCPM, arrVN[0]);
                        rwNomina[1] = cantNominaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwNomina[2] = getPorcentaje(cantNominaM, arrVN[0]);
                        rwCostoVenta[1] = cantCostoVentaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwCostoVenta[2] = getPorcentaje(cantCostoVentaM, arrVN[0]);
                    }
                    else if (j.Equals(2))
                    {
                        rwCM[3] = cantM.ToString("N1", CultureInfo.InvariantCulture);
                        rwCM[4] = getPorcentaje(cantM, arrVN[1]);
                        rwCP[3] = cantCPM.ToString("N1", CultureInfo.InvariantCulture);
                        rwCP[4] = getPorcentaje(cantCPM, arrVN[1]);
                        rwNomina[3] = cantNominaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwNomina[4] = getPorcentaje(cantNominaM, arrVN[1]);
                        rwCostoVenta[3] = cantCostoVentaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwCostoVenta[4] = getPorcentaje(cantCostoVentaM, arrVN[1]);
                    }
                    else if (j.Equals(3))
                    {
                        rwCM[5] = cantM.ToString("N1", CultureInfo.InvariantCulture);
                        rwCM[6] = getPorcentaje(cantM, arrVN[2]);
                        rwCP[5] = cantCPM.ToString("N1", CultureInfo.InvariantCulture);
                        rwCP[6] = getPorcentaje(cantCPM, arrVN[2]);
                        rwNomina[5] = cantNominaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwNomina[6] = getPorcentaje(cantNominaM, arrVN[2]);
                        rwCostoVenta[5] = cantCostoVentaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwCostoVenta[6] = getPorcentaje(cantCostoVentaM, arrVN[2]);
                    }
                    else if (j.Equals(4))
                    {
                        rwCM[7] = cantM.ToString("N1", CultureInfo.InvariantCulture);
                        rwCM[8] = getPorcentaje(cantM, arrVN[3]);
                        rwCP[7] = cantCPM.ToString("N1", CultureInfo.InvariantCulture);
                        rwCP[8] = getPorcentaje(cantCPM, arrVN[3]);
                        rwNomina[7] = cantNominaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwNomina[8] = getPorcentaje(cantNominaM, arrVN[3]);
                        rwCostoVenta[7] = cantCostoVentaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwCostoVenta[8] = getPorcentaje(cantCostoVentaM, arrVN[3]);
                    }
                    else if (j.Equals(5))
                    {
                        rwCM[9] = cantM.ToString("N1", CultureInfo.InvariantCulture);
                        rwCM[10] = getPorcentaje(cantM, arrVN[4]);
                        rwCP[9] = cantCPM.ToString("N1", CultureInfo.InvariantCulture);
                        rwCP[10] = getPorcentaje(cantCPM, arrVN[4]);
                        rwNomina[9] = cantNominaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwNomina[10] = getPorcentaje(cantNominaM, arrVN[4]);
                        rwCostoVenta[9] = cantCostoVentaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwCostoVenta[10] = getPorcentaje(cantCostoVentaM, arrVN[4]);
                    }
                    else if (j.Equals(6))
                    {
                        rwCM[11] = cantM.ToString("N1", CultureInfo.InvariantCulture);
                        rwCM[12] = getPorcentaje(cantM, arrVN[5]);
                        rwCP[11] = cantCPM.ToString("N1", CultureInfo.InvariantCulture);
                        rwCP[12] = getPorcentaje(cantCPM, arrVN[5]);
                        rwNomina[11] = cantNominaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwNomina[12] = getPorcentaje(cantNominaM, arrVN[5]);
                        rwCostoVenta[11] = cantCostoVentaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwCostoVenta[12] = getPorcentaje(cantCostoVentaM, arrVN[5]);
                    }
                    else if (j.Equals(7))
                    {
                        rwCM[13] = cantM.ToString("N1", CultureInfo.InvariantCulture);
                        rwCM[14] = getPorcentaje(cantM, arrVN[6]);
                        rwCP[13] = cantCPM.ToString("N1", CultureInfo.InvariantCulture);
                        rwCP[14] = getPorcentaje(cantCPM, arrVN[6]);
                        rwNomina[13] = cantNominaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwNomina[14] = getPorcentaje(cantNominaM, arrVN[6]);
                        rwCostoVenta[13] = cantCostoVentaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwCostoVenta[14] = getPorcentaje(cantCostoVentaM, arrVN[6]);
                    }
                    else if (j.Equals(8))
                    {
                        rwCM[15] = cantM.ToString("N1", CultureInfo.InvariantCulture);
                        rwCM[16] = getPorcentaje(cantM, arrVN[7]);
                        rwCP[15] = cantCPM.ToString("N1", CultureInfo.InvariantCulture);
                        rwCP[16] = getPorcentaje(cantCPM, arrVN[7]);
                        rwNomina[15] = cantNominaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwNomina[16] = getPorcentaje(cantNominaM, arrVN[7]);
                        rwCostoVenta[15] = cantCostoVentaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwCostoVenta[16] = getPorcentaje(cantCostoVentaM, arrVN[7]);
                    }
                    else if (j.Equals(9))
                    {
                        rwCM[17] = cantM.ToString("N1", CultureInfo.InvariantCulture);
                        rwCM[18] = getPorcentaje(cantM, arrVN[8]);
                        rwCP[17] = cantCPM.ToString("N1", CultureInfo.InvariantCulture);
                        rwCP[18] = getPorcentaje(cantCPM, arrVN[8]);
                        rwNomina[17] = cantNominaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwNomina[18] = getPorcentaje(cantNominaM, arrVN[8]);
                        rwCostoVenta[17] = cantCostoVentaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwCostoVenta[18] = getPorcentaje(cantCostoVentaM, arrVN[8]);
                    }
                    else if (j.Equals(10))
                    {
                        rwCM[19] = cantM.ToString("N1", CultureInfo.InvariantCulture);
                        rwCM[20] = getPorcentaje(cantM, arrVN[9]);
                        rwCP[19] = cantCPM.ToString("N1", CultureInfo.InvariantCulture);
                        rwCP[20] = getPorcentaje(cantCPM, arrVN[9]);
                        rwNomina[19] = cantNominaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwNomina[20] = getPorcentaje(cantNominaM, arrVN[9]);
                        rwCostoVenta[19] = cantCostoVentaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwCostoVenta[20] = getPorcentaje(cantCostoVentaM, arrVN[9]);
                    }
                    else if (j.Equals(11))
                    {
                        rwCM[21] = cantM.ToString("N1", CultureInfo.InvariantCulture);
                        rwCM[22] = getPorcentaje(cantM, arrVN[10]);
                        rwCP[21] = cantCPM.ToString("N1", CultureInfo.InvariantCulture);
                        rwCP[22] = getPorcentaje(cantCPM, arrVN[10]);
                        rwNomina[21] = cantNominaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwNomina[22] = getPorcentaje(cantNominaM, arrVN[10]);
                        rwCostoVenta[21] = cantCostoVentaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwCostoVenta[22] = getPorcentaje(cantCostoVentaM, arrVN[10]);
                    }
                    else if (j.Equals(12))
                    {
                        rwCM[23] = cantM.ToString("N1", CultureInfo.InvariantCulture);
                        rwCM[24] = getPorcentaje(cantM, arrVN[11]);
                        rwCP[23] = cantCPM.ToString("N1", CultureInfo.InvariantCulture);
                        rwCP[24] = getPorcentaje(cantCPM, arrVN[11]);
                        rwNomina[23] = cantNominaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwNomina[24] = getPorcentaje(cantNominaM, arrVN[11]);
                        rwCostoVenta[23] = cantCostoVentaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwCostoVenta[24] = getPorcentaje(cantCostoVentaM, arrVN[11]);
                    }

                    cantA = cantA + cantM;
                    cantCPA = cantCPA + cantCPM;
                    cantNominaA = cantNominaA + cantNominaM;
                    cantCostoVentaA = cantCostoVentaA + cantCostoVentaM;
                }

                arrCV[12] = cantCostoVentaA;
                rwCM[25] = cantA.ToString("N1", CultureInfo.InvariantCulture);
                //rwCM[26] = "100%";
                rwCP[25] = cantCPA.ToString("N1", CultureInfo.InvariantCulture);
                //rwCP[26] = "100%";
                rwNomina[25] = cantNominaA.ToString("N1", CultureInfo.InvariantCulture);
                //rwNomina[26] = "100%";
                rwCostoVenta[25] = cantCostoVentaA.ToString("N1", CultureInfo.InvariantCulture);
                //rwCostoVenta[26] = "100%";

                dt.Rows.Add(rwCM);
                dt.Rows.Add(rwCP);
                dt.Rows.Add(rwNomina);
                dt.Rows.Add(rwCostoVenta);
            }

            if (continuar)
            {
                DataRow rwUB = dt.NewRow();

                int columasOnDT = dt.Columns.Count;

                rwUB[0] = "Utilidad Bruta";

                for (int i = 1; i < columasOnDT; i++)
                {
                    if (i < 13)
                    {
                        int y = i - 1;
                        arrUB[y] = arrVN[y] - arrCV[y];
                        //rwUB[i] = arrUB[y];
                    }

                    if (i.Equals(1))
                    {
                        rwUB[i] = arrUB[0].ToString("N1", CultureInfo.InvariantCulture);
                        rwUB[i + 1] = getPorcentaje(arrUB[0], arrVN[0]);
                    }
                    else if (i.Equals(3)) { 
                        rwUB[i] = arrUB[1].ToString("N1", CultureInfo.InvariantCulture);
                        rwUB[i + 1] = getPorcentaje(arrUB[1], arrVN[1]);
                    }
                    else if (i.Equals(5)) { 
                        rwUB[i] = arrUB[2].ToString("N1", CultureInfo.InvariantCulture);
                        rwUB[i + 1] = getPorcentaje(arrUB[2], arrVN[2]);
                    }
                    else if (i.Equals(7)) { 
                        rwUB[i] = arrUB[3].ToString("N1", CultureInfo.InvariantCulture);
                        rwUB[i + 1] = getPorcentaje(arrUB[3], arrVN[3]);
                    }
                    else if (i.Equals(9)) { 
                        rwUB[i] = arrUB[4].ToString("N1", CultureInfo.InvariantCulture);
                        rwUB[i + 1] = getPorcentaje(arrUB[4], arrVN[4]);
                    }
                    else if (i.Equals(11)) { 
                        rwUB[i] = arrUB[5].ToString("N1", CultureInfo.InvariantCulture);
                        rwUB[i + 1] = getPorcentaje(arrUB[5], arrVN[5]);
                    }
                    else if (i.Equals(13)) { 
                        rwUB[i] = arrUB[6].ToString("N1", CultureInfo.InvariantCulture);
                        rwUB[i + 1] = getPorcentaje(arrUB[6], arrVN[6]);
                    }
                    else if (i.Equals(15)) { 
                        rwUB[i] = arrUB[7].ToString("N1", CultureInfo.InvariantCulture);
                        rwUB[i + 1] = getPorcentaje(arrUB[7], arrVN[7]);
                    }
                    else if (i.Equals(17)) { 
                        rwUB[i] = arrUB[8].ToString("N1", CultureInfo.InvariantCulture);
                        rwUB[i + 1] = getPorcentaje(arrUB[8], arrVN[8]);
                    }
                    else if (i.Equals(19)) { 
                        rwUB[i] = arrUB[9].ToString("N1", CultureInfo.InvariantCulture);
                        rwUB[i + 1] = getPorcentaje(arrUB[9], arrVN[9]);
                    }
                    else if (i.Equals(21)) { 
                        rwUB[i] = arrUB[10].ToString("N1", CultureInfo.InvariantCulture);
                        rwUB[i + 1] = getPorcentaje(arrUB[10], arrVN[10]);
                    }
                    else if (i.Equals(23)) { 
                        rwUB[i] = arrUB[11].ToString("N1", CultureInfo.InvariantCulture);
                        rwUB[i + 1] = getPorcentaje(arrUB[11], arrVN[11]);
                    }
                    else if (i.Equals(25)) { 
                        rwUB[i] = arrUB[12].ToString("N1", CultureInfo.InvariantCulture);
                        //rwUB[i + 1] = getPorcentaje(arrUB[12], arrVN[0]);
                    }
                    //else
                    //    rwUB[i] = "0";
                }

                dt.Rows.Add(rwUB);
            }

            if (continuar)
            {
                //DataTable dtCompras = new DataTable();
                DataTable dtNomina = new DataTable();

                DateTime fechaM = fechaAIni;
                DataRow rw = dt.NewRow();
                DataRow rwNomina = dt.NewRow();
                DataRow rwGastoVenta = dt.NewRow();

                rw[0] = "Gastos de ventas";
                rwNomina[0] = "Nomina de Ventas";
                rwGastoVenta[0] = "Gasto de Venta";

                float cant = 0;
                float cantM = 0;
                float cantA = 0;
                float cantNomina = 0;
                float cantNominaM = 0;
                float cantNominaA = 0;
                float cantGastoVenta = 0;
                float cantGastoVentaM = 0;
                float cantGastoVentaA = 0;
                //Obtenemos mes a mes la cantidad y así poder obtener el porcentaje mensual
                for (int j = 1; j <= 12; j++)
                {
                    if (!j.Equals(1))
                        fechaM = fechaM.AddMonths(1);

                    var firstDayOfMonth = new DateTime(fechaM.Year, fechaM.Month, 1);
                    var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                    //Obtenemos Bitacora
                    //dtCompras = cRR.getBitacora(firstDayOfMonth.ToString("yyyy-MM-dd"), lastDayOfMonth.ToString("yyyy-MM-dd"), idSucursal);
                    //Obtenemos Nomina
                    dtNomina = cRR.getNomina(3, fechaM.Month, idSucursal);

                    cantM = 0;
                    cantNominaM = 0;
                    cantGastoVentaM = 0;
                    //foreach (DataRow drC in dtCompras.Rows)
                    //{
                    cant = 0;
                    //cant = float.Parse(drC["cantidad"].ToString()) * float.Parse(drC["GastoKG"].ToString());
                    cantM = cantM + cant;

                    //}
                    cantNominaM = 0;
                    foreach (DataRow drN in dtNomina.Rows)
                    {
                        cantNomina = 0;
                        cantNomina = float.Parse(drN["monto"].ToString());
                        cantNominaM = cantNominaM + cantNomina;
                    }
                    cantGastoVentaM = cantM + cantNominaM;
                    arrGV[j - 1] = cantGastoVentaM;

                    if (j.Equals(1))
                    {
                        rw[1] = cantM.ToString("N1", CultureInfo.InvariantCulture);
                        rw[2] = getPorcentaje(cantM, arrVN[0]);
                        rwNomina[1] = cantNominaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwNomina[2] = getPorcentaje(cantNominaM, arrVN[0]);
                        rwGastoVenta[1] = cantGastoVentaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwGastoVenta[2] = getPorcentaje(cantGastoVentaM, arrVN[0]);
                    }
                    else if (j.Equals(2))
                    {
                        rw[3] = cantM.ToString("N1", CultureInfo.InvariantCulture);
                        rw[4] = getPorcentaje(cantM, arrVN[1]);
                        rwNomina[3] = cantNominaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwNomina[4] = getPorcentaje(cantNominaM, arrVN[1]);
                        rwGastoVenta[3] = cantGastoVentaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwGastoVenta[4] = getPorcentaje(cantGastoVentaM, arrVN[1]);
                    }
                    else if (j.Equals(3))
                    {
                        rw[5] = cantM.ToString("N1", CultureInfo.InvariantCulture);
                        rw[6] = getPorcentaje(cantM, arrVN[2]);
                        rwNomina[5] = cantNominaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwNomina[6] = getPorcentaje(cantNominaM, arrVN[2]);
                        rwGastoVenta[5] = cantGastoVentaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwGastoVenta[6] = getPorcentaje(cantGastoVentaM, arrVN[2]);
                    }
                    else if (j.Equals(4))
                    {
                        rw[7] = cantM.ToString("N1", CultureInfo.InvariantCulture);
                        rw[8] = getPorcentaje(cantM, arrVN[3]);
                        rwNomina[7] = cantNominaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwNomina[8] = getPorcentaje(cantNominaM, arrVN[3]);
                        rwGastoVenta[7] = cantGastoVentaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwGastoVenta[8] = getPorcentaje(cantGastoVentaM, arrVN[3]);
                    }
                    else if (j.Equals(5))
                    {
                        rw[9] = cantM.ToString("N1", CultureInfo.InvariantCulture);
                        rw[10] = getPorcentaje(cantM, arrVN[4]);
                        rwNomina[9] = cantNominaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwNomina[10] = getPorcentaje(cantNominaM, arrVN[4]);
                        rwGastoVenta[9] = cantGastoVentaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwGastoVenta[10] = getPorcentaje(cantGastoVentaM, arrVN[4]);
                    }
                    else if (j.Equals(6))
                    {
                        rw[11] = cantM.ToString("N1", CultureInfo.InvariantCulture);
                        rw[12] = getPorcentaje(cantM, arrVN[5]);
                        rwNomina[11] = cantNominaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwNomina[12] = getPorcentaje(cantNominaM, arrVN[5]);
                        rwGastoVenta[11] = cantGastoVentaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwGastoVenta[12] = getPorcentaje(cantGastoVentaM, arrVN[5]);
                    }
                    else if (j.Equals(7))
                    {
                        rw[13] = cantM.ToString("N1", CultureInfo.InvariantCulture);
                        rw[14] = getPorcentaje(cantM, arrVN[6]);
                        rwNomina[13] = cantNominaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwNomina[14] = getPorcentaje(cantNominaM, arrVN[6]);
                        rwGastoVenta[13] = cantGastoVentaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwGastoVenta[14] = getPorcentaje(cantGastoVentaM, arrVN[6]);
                    }
                    else if (j.Equals(8))
                    {
                        rw[15] = cantM.ToString("N1", CultureInfo.InvariantCulture);
                        rw[16] = getPorcentaje(cantM, arrVN[7]);
                        rwNomina[15] = cantNominaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwNomina[16] = getPorcentaje(cantNominaM, arrVN[7]);
                        rwGastoVenta[15] = cantGastoVentaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwGastoVenta[16] = getPorcentaje(cantGastoVentaM, arrVN[7]);
                    }
                    else if (j.Equals(9))
                    {
                        rw[17] = cantM.ToString("N1", CultureInfo.InvariantCulture);
                        rw[18] = getPorcentaje(cantM, arrVN[8]);
                        rwNomina[17] = cantNominaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwNomina[18] = getPorcentaje(cantNominaM, arrVN[8]);
                        rwGastoVenta[17] = cantGastoVentaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwGastoVenta[18] = getPorcentaje(cantGastoVentaM, arrVN[8]);
                    }
                    else if (j.Equals(10))
                    {
                        rw[19] = cantM.ToString("N1", CultureInfo.InvariantCulture);
                        rw[20] = getPorcentaje(cantM, arrVN[9]);
                        rwNomina[19] = cantNominaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwNomina[20] = getPorcentaje(cantNominaM, arrVN[9]);
                        rwGastoVenta[19] = cantGastoVentaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwGastoVenta[20] = getPorcentaje(cantGastoVentaM, arrVN[9]);
                    }
                    else if (j.Equals(11))
                    {
                        rw[21] = cantM.ToString("N1", CultureInfo.InvariantCulture);
                        rw[22] = getPorcentaje(cantM, arrVN[10]);
                        rwNomina[21] = cantNominaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwNomina[22] = getPorcentaje(cantNominaM, arrVN[10]);
                        rwGastoVenta[21] = cantGastoVentaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwGastoVenta[22] = getPorcentaje(cantGastoVentaM, arrVN[10]);
                    }
                    else if (j.Equals(12))
                    {
                        rw[23] = cantM.ToString("N1", CultureInfo.InvariantCulture);
                        rw[24] = getPorcentaje(cantM, arrVN[11]);
                        rwNomina[23] = cantNominaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwNomina[24] = getPorcentaje(cantNominaM, arrVN[11]);
                        rwGastoVenta[23] = cantGastoVentaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwGastoVenta[24] = getPorcentaje(cantGastoVentaM, arrVN[11]);
                    }

                    cantA = cantA + cantM;
                    cantNominaA = cantNominaA + cantNominaM;
                    cantGastoVentaA = cantGastoVentaA + cantGastoVentaM;
                }
                arrGV[12] = cantGastoVentaA;

                rw[25] = cantA.ToString("N1", CultureInfo.InvariantCulture);
                //rw[26] = getPorcentaje(cantA, arrVN[12]);
                rwNomina[25] = cantNominaA.ToString("N1", CultureInfo.InvariantCulture);
                //rwNomina[26] = getPorcentaje(cantNominaA, arrVN[12]);
                rwGastoVenta[25] = cantGastoVentaA.ToString("N1", CultureInfo.InvariantCulture);
                //rwGastoVenta[26] = getPorcentaje(cantGastoVentaA, arrVN[12]);

                dt.Rows.Add(rw);
                dt.Rows.Add(rwNomina);
                dt.Rows.Add(rwGastoVenta);
            }
            if (continuar)
            {
                //DataTable dtCompras = new DataTable();
                DataTable dtNomina = new DataTable();

                DateTime fechaM = fechaAIni;
                DataRow rw = dt.NewRow();
                DataRow rwNomina = dt.NewRow();
                DataRow rwGastoADMON = dt.NewRow();

                rw[0] = "Gasto de ADMON";
                rwNomina[0] = "Nomina ADMON";
                rwGastoADMON[0] = "Gasto de ADMON";

                float cant = 0;
                float cantM = 0;
                float cantA = 0;
                float cantNomina = 0;
                float cantNominaM = 0;
                float cantNominaA = 0;
                float cantGastoADMON = 0;
                float cantGastoADMONM = 0;
                float cantGastoADMONA = 0;
                //Obtenemos mes a mes la cantidad y así poder obtener el porcentaje mensual
                for (int j = 1; j <= 12; j++)
                {
                    if (!j.Equals(1))
                        fechaM = fechaM.AddMonths(1);

                    var firstDayOfMonth = new DateTime(fechaM.Year, fechaM.Month, 1);
                    var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                    //Obtenemos Bitacora
                    //dtCompras = cRR.getBitacora(firstDayOfMonth.ToString("yyyy-MM-dd"), lastDayOfMonth.ToString("yyyy-MM-dd"), idSucursal);
                    //Obtenemos Nomina
                    dtNomina = cRR.getNomina(2, fechaM.Month, idSucursal);

                    cantM = 0;
                    cantNominaM = 0;
                    cantGastoADMONM = 0;
                    //foreach (DataRow drC in dtCompras.Rows)
                    //{
                    cant = 0;
                    //cant = float.Parse(drC["cantidad"].ToString()) * float.Parse(drC["GastoKG"].ToString());
                    cantM = cantM + cant;

                    //}
                    cantNominaM = 0;
                    foreach (DataRow drN in dtNomina.Rows)
                    {
                        cantNomina = 0;
                        cantNomina = float.Parse(drN["monto"].ToString());
                        cantNominaM = cantNominaM + cantNomina;
                    }
                    cantGastoADMONM = cantM + cantNominaM;
                    arrGA[j - 1] = cantGastoADMONM;

                    if (j.Equals(1))
                    {
                        rw[1] = cantM.ToString("N1", CultureInfo.InvariantCulture);
                        rw[2] = getPorcentaje(cantM, arrVN[0]);
                        rwNomina[1] = cantNominaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwNomina[2] = getPorcentaje(cantNominaM, arrVN[0]);
                        rwGastoADMON[1] = cantGastoADMONM.ToString("N1", CultureInfo.InvariantCulture);
                        rwGastoADMON[2] = getPorcentaje(cantGastoADMONM, arrVN[0]);
                    }
                    else if (j.Equals(2))
                    {
                        rw[3] = cantM.ToString("N1", CultureInfo.InvariantCulture);
                        rw[4] = getPorcentaje(cantM, arrVN[1]);
                        rwNomina[3] = cantNominaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwNomina[4] = getPorcentaje(cantNominaM, arrVN[1]);
                        rwGastoADMON[3] = cantGastoADMONM.ToString("N1", CultureInfo.InvariantCulture);
                        rwGastoADMON[4] = getPorcentaje(cantGastoADMONM, arrVN[1]);
                    }
                    else if (j.Equals(3))
                    {
                        rw[5] = cantM.ToString("N1", CultureInfo.InvariantCulture);
                        rw[6] = getPorcentaje(cantM, arrVN[2]);
                        rwNomina[5] = cantNominaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwNomina[6] = getPorcentaje(cantNominaM, arrVN[2]);
                        rwGastoADMON[5] = cantGastoADMONM.ToString("N1", CultureInfo.InvariantCulture);
                        rwGastoADMON[6] = getPorcentaje(cantGastoADMONM, arrVN[2]);
                    }
                    else if (j.Equals(4))
                    {
                        rw[7] = cantM.ToString("N1", CultureInfo.InvariantCulture);
                        rw[8] = getPorcentaje(cantM, arrVN[3]);
                        rwNomina[7] = cantNominaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwNomina[8] = getPorcentaje(cantNominaM, arrVN[3]);
                        rwGastoADMON[7] = cantGastoADMONM.ToString("N1", CultureInfo.InvariantCulture);
                        rwGastoADMON[8] = getPorcentaje(cantGastoADMONM, arrVN[3]);
                    }
                    else if (j.Equals(5))
                    {
                        rw[9] = cantM.ToString("N1", CultureInfo.InvariantCulture);
                        rw[10] = getPorcentaje(cantM, arrVN[4]);
                        rwNomina[9] = cantNominaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwNomina[10] = getPorcentaje(cantNominaM, arrVN[4]);
                        rwGastoADMON[9] = cantGastoADMONM.ToString("N1", CultureInfo.InvariantCulture);
                        rwGastoADMON[10] = getPorcentaje(cantGastoADMONM, arrVN[4]);
                    }
                    else if (j.Equals(6))
                    {
                        rw[11] = cantM.ToString("N1", CultureInfo.InvariantCulture);
                        rw[12] = getPorcentaje(cantM, arrVN[5]);
                        rwNomina[11] = cantNominaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwNomina[12] = getPorcentaje(cantNominaM, arrVN[5]);
                        rwGastoADMON[11] = cantGastoADMONM.ToString("N1", CultureInfo.InvariantCulture);
                        rwGastoADMON[12] = getPorcentaje(cantGastoADMONM, arrVN[5]);
                    }
                    else if (j.Equals(7))
                    {
                        rw[13] = cantM.ToString("N1", CultureInfo.InvariantCulture);
                        rw[14] = getPorcentaje(cantM, arrVN[6]);
                        rwNomina[13] = cantNominaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwNomina[14] = getPorcentaje(cantNominaM, arrVN[6]);
                        rwGastoADMON[13] = cantGastoADMONM.ToString("N1", CultureInfo.InvariantCulture);
                        rwGastoADMON[14] = getPorcentaje(cantGastoADMONM, arrVN[6]);
                    }
                    else if (j.Equals(8))
                    {
                        rw[15] = cantM.ToString("N1", CultureInfo.InvariantCulture);
                        rw[16] = getPorcentaje(cantM, arrVN[7]);
                        rwNomina[15] = cantNominaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwNomina[16] = getPorcentaje(cantNominaM, arrVN[7]);
                        rwGastoADMON[15] = cantGastoADMONM.ToString("N1", CultureInfo.InvariantCulture);
                        rwGastoADMON[16] = getPorcentaje(cantGastoADMONM, arrVN[7]);
                    }
                    else if (j.Equals(9))
                    {
                        rw[17] = cantM.ToString("N1", CultureInfo.InvariantCulture);
                        rw[18] = getPorcentaje(cantM, arrVN[8]);
                        rwNomina[17] = cantNominaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwNomina[18] = getPorcentaje(cantNominaM, arrVN[8]);
                        rwGastoADMON[17] = cantGastoADMONM.ToString("N1", CultureInfo.InvariantCulture);
                        rwGastoADMON[18] = getPorcentaje(cantGastoADMONM, arrVN[8]);
                    }
                    else if (j.Equals(10))
                    {
                        rw[19] = cantM.ToString("N1", CultureInfo.InvariantCulture);
                        rw[20] = getPorcentaje(cantM, arrVN[9]);
                        rwNomina[19] = cantNominaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwNomina[20] = getPorcentaje(cantNominaM, arrVN[9]);
                        rwGastoADMON[19] = cantGastoADMONM.ToString("N1", CultureInfo.InvariantCulture);
                        rwGastoADMON[20] = getPorcentaje(cantGastoADMONM, arrVN[9]);
                    }
                    else if (j.Equals(11))
                    {
                        rw[21] = cantM.ToString("N1", CultureInfo.InvariantCulture);
                        rw[22] = getPorcentaje(cantM, arrVN[10]);
                        rwNomina[21] = cantNominaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwNomina[22] = getPorcentaje(cantNominaM, arrVN[10]);
                        rwGastoADMON[21] = cantGastoADMONM.ToString("N1", CultureInfo.InvariantCulture);
                        rwGastoADMON[22] = getPorcentaje(cantGastoADMONM, arrVN[10]);
                    }
                    else if (j.Equals(12))
                    {
                        rw[23] = cantM.ToString("N1", CultureInfo.InvariantCulture);
                        rw[24] = getPorcentaje(cantM, arrVN[11]);
                        rwNomina[23] = cantNominaM.ToString("N1", CultureInfo.InvariantCulture);
                        rwNomina[24] = getPorcentaje(cantNominaM, arrVN[11]);
                        rwGastoADMON[23] = cantGastoADMONM.ToString("N1", CultureInfo.InvariantCulture);
                        rwGastoADMON[24] = getPorcentaje(cantGastoADMONM, arrVN[11]);
                    }

                    cantA = cantA + cantM;
                    cantNominaA = cantNominaA + cantNominaM;
                    cantGastoADMONA = cantGastoADMONA + cantGastoADMONM;
                }

                arrGA[12] = cantGastoADMONA;
                rw[25] = cantA.ToString("N1", CultureInfo.InvariantCulture);
                //rw[26] = "100%";
                rwNomina[25] = cantNominaA.ToString("N1", CultureInfo.InvariantCulture);
                //rwNomina[26] = "100%";
                rwGastoADMON[25] = cantGastoADMONA.ToString("N1", CultureInfo.InvariantCulture);
                //rwGastoADMON[26] = "100%";

                dt.Rows.Add(rw);
                dt.Rows.Add(rwNomina);
                dt.Rows.Add(rwGastoADMON);
            }

            if (continuar)
            {
                DataRow rw = dt.NewRow();

                int columasOnDT = dt.Columns.Count;

                rw[0] = "Utilidad de Operación";

                for (int i = 1; i < columasOnDT; i++)
                {
                    if (i < 13)
                    {
                        int y = i - 1;
                        arrUO[y] = arrUB[y] - (arrGV[y] + arrGA[y]);
                        //rw[i] = arrUO[y];
                    }

                    if (i.Equals(1)) { 
                        rw[i] = arrUO[0].ToString("N1", CultureInfo.InvariantCulture);
                        rw[i + 1] = getPorcentaje(arrUO[0], arrVN[0]);
                    }
                    else if (i.Equals(3)){ 
                        rw[i] = arrUO[1].ToString("N1", CultureInfo.InvariantCulture);
                        rw[i + 1] = getPorcentaje(arrUO[1], arrVN[1]);
                    }
                    else if (i.Equals(5)) { 
                        rw[i] = arrUO[2].ToString("N1", CultureInfo.InvariantCulture);
                        rw[i + 1] = getPorcentaje(arrUO[2], arrVN[2]);
                    }
                    else if (i.Equals(7)) { 
                        rw[i] = arrUO[3].ToString("N1", CultureInfo.InvariantCulture);
                        rw[i + 1] = getPorcentaje(arrUO[3], arrVN[3]);
                    }
                    else if (i.Equals(9)) { 
                        rw[i] = arrUO[4].ToString("N1", CultureInfo.InvariantCulture);
                        rw[i + 1] = getPorcentaje(arrUO[4], arrVN[4]);
                    }
                    else if (i.Equals(11)) { 
                        rw[i] = arrUO[5].ToString("N1", CultureInfo.InvariantCulture);
                        rw[i + 1] = getPorcentaje(arrUO[5], arrVN[5]);
                    }
                    else if (i.Equals(13)) { 
                        rw[i] = arrUO[6].ToString("N1", CultureInfo.InvariantCulture);
                        rw[i + 1] = getPorcentaje(arrUO[6], arrVN[6]);
                    }
                    else if (i.Equals(15)) { 
                        rw[i] = arrUO[7].ToString("N1", CultureInfo.InvariantCulture);
                        rw[i + 1] = getPorcentaje(arrUO[7], arrVN[7]);
                    }
                    else if (i.Equals(17)) { 
                        rw[i] = arrUO[8].ToString("N1", CultureInfo.InvariantCulture);
                        rw[i + 1] = getPorcentaje(arrUO[8], arrVN[8]);
                    }
                    else if (i.Equals(19)) { 
                        rw[i] = arrUO[9].ToString("N1", CultureInfo.InvariantCulture);
                        rw[i + 1] = getPorcentaje(arrUO[9], arrVN[9]);
                    }
                    else if (i.Equals(21)) { 
                        rw[i] = arrUO[10].ToString("N1", CultureInfo.InvariantCulture);
                        rw[i + 1] = getPorcentaje(arrUO[10], arrVN[10]);
                    }
                    else if (i.Equals(23)) { 
                        rw[i] = arrUO[11].ToString("N1", CultureInfo.InvariantCulture);
                        rw[i + 1] = getPorcentaje(arrUO[11], arrVN[11]);
                    }
                    else if (i.Equals(25)) { 
                        rw[i] = arrUO[12].ToString("N1", CultureInfo.InvariantCulture);
                        //rw[i + 1] = getPorcentaje(arrUB[12], arrVN[12]);
                    }
                    //else
                    //    rw[i] = "0";
                }

                dt.Rows.Add(rw);
            }

            if (continuar)
            {
                DataRow rw = dt.NewRow();

                int columasOnDT = dt.Columns.Count;

                rw[0] = "Gastos Financieros";

                for (int i = 1; i < columasOnDT; i++)
                {
                    rw[i] = "0";
                }

                dt.Rows.Add(rw);

            }

            if (continuar)
            {
                DataRow rw = dt.NewRow();

                int columasOnDT = dt.Columns.Count;

                rw[0] = "Utilidad Antes de Impuestos";

                for (int i = 1; i < columasOnDT; i++)
                {
                    rw[i] = "0";
                }

                dt.Rows.Add(rw);
            }

            if (continuar)
            {
            }

            lv.DataSource = dt;
            lv.DataBind();
        }

        private string getPorcentaje(float cant, float total)
        {
            if (cant.Equals(0) || total.Equals(0))
                return "0%";
            else
            {
                float sub = cant / total;
                float porcentaje = sub * 100;
                return porcentaje.ToString("N1", CultureInfo.InvariantCulture) + "%";
            }
        }
    }
}