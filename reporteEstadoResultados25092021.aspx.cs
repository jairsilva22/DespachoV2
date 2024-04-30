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
    public partial class reporteEstadoResultados25092021 : System.Web.UI.Page
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
                llenarLVCostoVentas(); 
                llenarLVUtilidadBruta();
                llenarLVGastosDeVenta();
                llenarLVGastosAdministrativos();
                llenarLVUtilidadOperativa();
                llenarLVGastosFinancieros();
                llenarLVUtilidadAntesImpuestos();

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
            //dt.Columns.Add("vn0");
            //dt.Columns.Add("vn1");
            //dt.Columns.Add("vnP1");
            //dt.Columns.Add("vn2");
            //dt.Columns.Add("vnP2");
            //dt.Columns.Add("vn3");
            //dt.Columns.Add("vnP3");
            //dt.Columns.Add("vn4");
            //dt.Columns.Add("vnP4");
            //dt.Columns.Add("vn5");
            //dt.Columns.Add("vnP5");
            //dt.Columns.Add("vn6");
            //dt.Columns.Add("vnP6");
            //dt.Columns.Add("vn7");
            //dt.Columns.Add("vnP7");
            //dt.Columns.Add("vn8");
            //dt.Columns.Add("vnP8");
            //dt.Columns.Add("vn9");
            //dt.Columns.Add("vnP9");
            //dt.Columns.Add("vn10");
            //dt.Columns.Add("vnP10");
            //dt.Columns.Add("vn11");
            //dt.Columns.Add("vnP11");
            //dt.Columns.Add("vn12");
            //dt.Columns.Add("vnP12");
            //dt.Columns.Add("vnAcu");
            //dt.Columns.Add("vnPAcu");

            //Crear Array de IngresosBrutoM (Mensual), IngresosDescuentoM y IngresosDescuentoDifM
            //Para sacar la diferencia y obtener IngresosDescuentoDifM = IngresosBrutoM - IngresosDescuentoM mes a mes y IngresosDescuentosDifA = IngresosDescuentosDifA +  IngresosDescuentoDifM

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
                        rw["ingD" + j] = ingresoBrutoM.ToString("N1", CultureInfo.InvariantCulture);
                        ingresoBrutoA += ingresoBrutoM;
                    }
                    if (i.Equals(2))
                    {
                        //Como se obtienen las devoluciones
                        rw["ingD" + j] = "0";
                        rw["ingDP" + j] = "0";
                    }
                    if (i.Equals(3))
                    {
                        ingresoDescuentosMDif = ingresoBrutoM - ingresoDescuentosM;
                        ingresoDescuentosADif += ingresoDescuentosMDif;
                        //Tomar solo precioFactor que sean menorres al precio unitario???
                        //cant = float.Parse(dtIngDM.Rows[0]["cantidadEntregada"].ToString()) * float.Parse(dtIngDM.Rows[0]["precioFactor"].ToString());
                        rw["ingD" + j] = ingresoDescuentosMDif.ToString("N1", CultureInfo.InvariantCulture);
                        //ingresoDescuentosA = ingresoDescuentosA + ingresoDescuentosM;
                    }
                    if (i.Equals(4))
                    {
                        rw["ingD" + j] = (ingresoBrutoM - ingresoDescuentosMDif).ToString("N1", CultureInfo.InvariantCulture);
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
                    rw["ingDAcu"] = "0";
                    rw["ingDPAcu"] = "100%";
                }
                if (i.Equals(3))
                {
                    //ingresoDescuentosADif = ingresoBrutoA - ingresoDescuentosA;
                    rw["ingD0"] = "Descuentos";
                    //rw["ingDAcu"] = ingresoDescuentosADif.ToString("N1", CultureInfo.InvariantCulture);
                    rw["ingDAcu"] = ingresoDescuentosADif.ToString("N1", CultureInfo.InvariantCulture);
                    rw["ingDPAcu"] = "100%";
                }
                if (i.Equals(4))
                {
                    rw["ingD0"] = "Ventas Netas";
                    rw["ingDAcu"] = (ingresoBrutoA - ingresoDescuentosADif).ToString("N1", CultureInfo.InvariantCulture);
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
                        rw["ingDP" + j] = getPorcentaje(float.Parse(rw["ingD" + j].ToString()), ingresoDescuentosADif);
                    }
                    if (i.Equals(4))
                    {
                        rw["ingDP" + j] = getPorcentaje(float.Parse(rw["ingD" + j].ToString()), (ingresoBrutoA + ingresoDescuentosADif));
                    }
                }

                dt.Rows.Add(rw);
            }
            lvIngresos.DataSource = dt;
            lvIngresos.DataBind();
        }

        private void llenarLVCostoVentas()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("cvD0");
            dt.Columns.Add("cvD1");
            dt.Columns.Add("cvDP1");
            dt.Columns.Add("cvD2");
            dt.Columns.Add("cvDP2");
            dt.Columns.Add("cvD3");
            dt.Columns.Add("cvDP3");
            dt.Columns.Add("cvD4");
            dt.Columns.Add("cvDP4");
            dt.Columns.Add("cvD5");
            dt.Columns.Add("cvDP5");
            dt.Columns.Add("cvD6");
            dt.Columns.Add("cvDP6");
            dt.Columns.Add("cvD7");
            dt.Columns.Add("cvDP7");
            dt.Columns.Add("cvD8");
            dt.Columns.Add("cvDP8");
            dt.Columns.Add("cvD9");
            dt.Columns.Add("cvDP9");
            dt.Columns.Add("cvD10");
            dt.Columns.Add("cvDP10");
            dt.Columns.Add("cvD11");
            dt.Columns.Add("cvDP11");
            dt.Columns.Add("cvD12");
            dt.Columns.Add("cvDP12");
            dt.Columns.Add("cvDAcu");
            dt.Columns.Add("cvDPAcu");


            DataRow rw = dt.NewRow();

            int columasOnDT = dt.Columns.Count;

            rw[0] = "Costo de Venta";

            for (int i = 1; i < columasOnDT; i++)
            {
                rw[i] = "0";
            }

            dt.Rows.Add(rw);


            lvCostoVentas.DataSource = dt;
            lvCostoVentas.DataBind();
        }

        private void llenarLVUtilidadBruta()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ubD0");
            dt.Columns.Add("ubD1");
            dt.Columns.Add("ubDP1");
            dt.Columns.Add("ubD2");
            dt.Columns.Add("ubDP2");
            dt.Columns.Add("ubD3");
            dt.Columns.Add("ubDP3");
            dt.Columns.Add("ubD4");
            dt.Columns.Add("ubDP4");
            dt.Columns.Add("ubD5");
            dt.Columns.Add("ubDP5");
            dt.Columns.Add("ubD6");
            dt.Columns.Add("ubDP6");
            dt.Columns.Add("ubD7");
            dt.Columns.Add("ubDP7");
            dt.Columns.Add("ubD8");
            dt.Columns.Add("ubDP8");
            dt.Columns.Add("ubD9");
            dt.Columns.Add("ubDP9");
            dt.Columns.Add("ubD10");
            dt.Columns.Add("ubDP10");
            dt.Columns.Add("ubD11");
            dt.Columns.Add("ubDP11");
            dt.Columns.Add("ubD12");
            dt.Columns.Add("ubDP12");
            dt.Columns.Add("ubDAcu");
            dt.Columns.Add("ubDPAcu");


            DataRow rw = dt.NewRow();

            int columasOnDT = dt.Columns.Count;

            rw[0] = "Utilidad Bruta";

            for (int i = 1; i < columasOnDT; i++)
            {
                rw[i] = "0";
            }

            dt.Rows.Add(rw);


            lvUtilidadBruta.DataSource = dt;
            lvUtilidadBruta.DataBind();
        }

        private void llenarLVGastosDeVenta()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("gvD0");
            dt.Columns.Add("gvD1");
            dt.Columns.Add("gvDP1");
            dt.Columns.Add("gvD2");
            dt.Columns.Add("gvDP2");
            dt.Columns.Add("gvD3");
            dt.Columns.Add("gvDP3");
            dt.Columns.Add("gvD4");
            dt.Columns.Add("gvDP4");
            dt.Columns.Add("gvD5");
            dt.Columns.Add("gvDP5");
            dt.Columns.Add("gvD6");
            dt.Columns.Add("gvDP6");
            dt.Columns.Add("gvD7");
            dt.Columns.Add("gvDP7");
            dt.Columns.Add("gvD8");
            dt.Columns.Add("gvDP8");
            dt.Columns.Add("gvD9");
            dt.Columns.Add("gvDP9");
            dt.Columns.Add("gvD10");
            dt.Columns.Add("gvDP10");
            dt.Columns.Add("gvD11");
            dt.Columns.Add("gvDP11");
            dt.Columns.Add("gvD12");
            dt.Columns.Add("gvDP12");
            dt.Columns.Add("gvDAcu");
            dt.Columns.Add("gvDPAcu");


            DataRow rw = dt.NewRow();

            int columasOnDT = dt.Columns.Count;

            rw[0] = "Gastos de Venta";

            for (int i = 1; i < columasOnDT; i++)
            {
                rw[i] = "0";
            }

            dt.Rows.Add(rw);


            lvGastosVenta.DataSource = dt;
            lvGastosVenta.DataBind();
        }

        private void llenarLVGastosAdministrativos()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("gaD0");
            dt.Columns.Add("gaD1");
            dt.Columns.Add("gaDP1");
            dt.Columns.Add("gaD2");
            dt.Columns.Add("gaDP2");
            dt.Columns.Add("gaD3");
            dt.Columns.Add("gaDP3");
            dt.Columns.Add("gaD4");
            dt.Columns.Add("gaDP4");
            dt.Columns.Add("gaD5");
            dt.Columns.Add("gaDP5");
            dt.Columns.Add("gaD6");
            dt.Columns.Add("gaDP6");
            dt.Columns.Add("gaD7");
            dt.Columns.Add("gaDP7");
            dt.Columns.Add("gaD8");
            dt.Columns.Add("gaDP8");
            dt.Columns.Add("gaD9");
            dt.Columns.Add("gaDP9");
            dt.Columns.Add("gaD10");
            dt.Columns.Add("gaDP10");
            dt.Columns.Add("gaD11");
            dt.Columns.Add("gaDP11");
            dt.Columns.Add("gaD12");
            dt.Columns.Add("gaDP12");
            dt.Columns.Add("gaDAcu");
            dt.Columns.Add("gaDPAcu");


            DataRow rw = dt.NewRow();

            int columasOnDT = dt.Columns.Count;

            rw[0] = "Gastos de ADMON";

            for (int i = 1; i < columasOnDT; i++)
            {
                rw[i] = "0";
            }

            dt.Rows.Add(rw);


            lvGastosAdministrativos.DataSource = dt;
            lvGastosAdministrativos.DataBind();
        }

        private void llenarLVUtilidadOperativa()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("uoD0");
            dt.Columns.Add("uoD1");
            dt.Columns.Add("uoDP1");
            dt.Columns.Add("uoD2");
            dt.Columns.Add("uoDP2");
            dt.Columns.Add("uoD3");
            dt.Columns.Add("uoDP3");
            dt.Columns.Add("uoD4");
            dt.Columns.Add("uoDP4");
            dt.Columns.Add("uoD5");
            dt.Columns.Add("uoDP5");
            dt.Columns.Add("uoD6");
            dt.Columns.Add("uoDP6");
            dt.Columns.Add("uoD7");
            dt.Columns.Add("uoDP7");
            dt.Columns.Add("uoD8");
            dt.Columns.Add("uoDP8");
            dt.Columns.Add("uoD9");
            dt.Columns.Add("uoDP9");
            dt.Columns.Add("uoD10");
            dt.Columns.Add("uoDP10");
            dt.Columns.Add("uoD11");
            dt.Columns.Add("uoDP11");
            dt.Columns.Add("uoD12");
            dt.Columns.Add("uoDP12");
            dt.Columns.Add("uoDAcu");
            dt.Columns.Add("uoDPAcu");


            DataRow rw = dt.NewRow();

            int columasOnDT = dt.Columns.Count;

            rw[0] = "Utilidad de Operación";

            for (int i = 1; i < columasOnDT; i++)
            {
                rw[i] = "0";
            }

            dt.Rows.Add(rw);


            lvUtilidadOperativa.DataSource = dt;
            lvUtilidadOperativa.DataBind();
        }

        private void llenarLVGastosFinancieros()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("gfD0");
            dt.Columns.Add("gfD1");
            dt.Columns.Add("gfDP1");
            dt.Columns.Add("gfD2");
            dt.Columns.Add("gfDP2");
            dt.Columns.Add("gfD3");
            dt.Columns.Add("gfDP3");
            dt.Columns.Add("gfD4");
            dt.Columns.Add("gfDP4");
            dt.Columns.Add("gfD5");
            dt.Columns.Add("gfDP5");
            dt.Columns.Add("gfD6");
            dt.Columns.Add("gfDP6");
            dt.Columns.Add("gfD7");
            dt.Columns.Add("gfDP7");
            dt.Columns.Add("gfD8");
            dt.Columns.Add("gfDP8");
            dt.Columns.Add("gfD9");
            dt.Columns.Add("gfDP9");
            dt.Columns.Add("gfD10");
            dt.Columns.Add("gfDP10");
            dt.Columns.Add("gfD11");
            dt.Columns.Add("gfDP11");
            dt.Columns.Add("gfD12");
            dt.Columns.Add("gfDP12");
            dt.Columns.Add("gfDAcu");
            dt.Columns.Add("gfDPAcu");


            DataRow rw = dt.NewRow();

            int columasOnDT = dt.Columns.Count;

            rw[0] = "Gastos Financieros";

            for (int i = 1; i < columasOnDT; i++)
            {
                rw[i] = "0";
            }

            dt.Rows.Add(rw);


            lvGastosFinancieros.DataSource = dt;
            lvGastosFinancieros.DataBind();
        }

        private void llenarLVUtilidadAntesImpuestos()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("uaiD0");
            dt.Columns.Add("uaiD1");
            dt.Columns.Add("uaiDP1");
            dt.Columns.Add("uaiD2");
            dt.Columns.Add("uaiDP2");
            dt.Columns.Add("uaiD3");
            dt.Columns.Add("uaiDP3");
            dt.Columns.Add("uaiD4");
            dt.Columns.Add("uaiDP4");
            dt.Columns.Add("uaiD5");
            dt.Columns.Add("uaiDP5");
            dt.Columns.Add("uaiD6");
            dt.Columns.Add("uaiDP6");
            dt.Columns.Add("uaiD7");
            dt.Columns.Add("uaiDP7");
            dt.Columns.Add("uaiD8");
            dt.Columns.Add("uaiDP8");
            dt.Columns.Add("uaiD9");
            dt.Columns.Add("uaiDP9");
            dt.Columns.Add("uaiD10");
            dt.Columns.Add("uaiDP10");
            dt.Columns.Add("uaiD11");
            dt.Columns.Add("uaiDP11");
            dt.Columns.Add("uaiD12");
            dt.Columns.Add("uaiDP12");
            dt.Columns.Add("uaiDAcu");
            dt.Columns.Add("uaiDPAcu");


            DataRow rw = dt.NewRow();

            int columasOnDT = dt.Columns.Count;

            rw[0] = "Utilidad Antes de Impuestos";

            for (int i = 1; i < columasOnDT; i++)
            {
                rw[i] = "0";
            }

            dt.Rows.Add(rw);


            lvUtilidadAntesImpuestos.DataSource = dt;
            lvUtilidadAntesImpuestos.DataBind();
        }

        private string getPorcentaje(float cant, float total)
        {
            float sub = cant / total;
            float porcentaje = sub * 100;
            return porcentaje.ToString("N1", CultureInfo.InvariantCulture) + "%";
        }
    }
}