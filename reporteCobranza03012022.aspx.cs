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
    public partial class reporteCobranza03012022 : System.Web.UI.Page
    {
        cUtilidades cUtl = new cUtilidades();
        cSucursales cSuc = new cSucursales();
        cReporteResultados cRR = new cReporteResultados();
        cDosificacion cDOS = new cDosificacion();
        cMateriales cMat = new cMateriales();
        int idSucursal = 0, idSucursalCompras = 0, idVendedor = 0;
        DateTime fechaIni = DateTime.Now;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //lblAnio.Text += Request.QueryString["anio"];
                //hfAnio.Value = Request.QueryString["anio"];
                idSucursal = int.Parse(Request.QueryString["idSucursal"].ToString());
                idVendedor = int.Parse(Request.QueryString["idVendedor"].ToString());
                idSucursalCompras = cSuc.obtenerComprasID(idSucursal);

                //fechaIni = DateTime.Parse(cUtl.getDateTimeFromDB());
                fechaIni = DateTime.Parse("01/12/2021");

                lblFecha.Text = fechaIni.ToString().Substring(0, 10);

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
                Response.AddHeader("content-disposition", "attachment;filename=ReporteCobranza-" + DateTime.Today.ToShortDateString() + ".xls");
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
                    frm.Controls.Add(lvTotales);
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
        //public DataTable getMontosVencidos(string fFechaI, string fFechaF, int idCliente, int idFormaPago, int idVendedor = 0)
        //{
        //    try
        //    {
        //        DataTable dt = new DataTable();
        //        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnx"].ConnectionString))
        //        {
        //            conn.Open();
        //            string selectComand = "SELECT o.folio, s.reqFac AS factura, ISNULL(ds.total,0) AS total, ISNULL " +
        //                "((SELECT SUM(ISNULL(monto, 0)) AS Expr1 FROM pagosFinanzas AS pf WHERE(idSolicitud = s.id)), 0) AS pagado, o.fecha ";
        //            string fromComand = "FROM ordenes AS o INNER JOIN " +
        //                 "solicitudes AS s ON o.idSolicitud = s.id INNER JOIN " +
        //                 "clientes AS cl ON s.idCliente = cl.id INNER JOIN " +
        //                 "usuarios AS v ON s.idVendedor = v.id INNER JOIN " +
        //                 "detallesSolicitud AS ds ON s.id = ds.idSolicitud ";
        //            string whereComand = "WHERE (s.idCliente= @idCliente) AND (s.idFormaPago = @idFormaPago) AND (s.eliminada IS NULL) AND (o.eliminado IS NULL) AND (ds.eliminado IS NULL) ";

        //            if (!idSucursal.Equals(0))
        //                whereComand += "AND (s.idSucursal = @idSucursal) ";
        //            else
        //                whereComand += "AND (s.idSucursal NOT LIKE '5') ";

        //            if (!idVendedor.Equals(0))
        //                whereComand += "AND (s.idVendedor = @idVendedor) ";

        //            whereComand += " AND (s.fecha BETWEEN '" + fFechaI + "' AND '" + fFechaF + "')";

        //            using (SqlCommand cmd = new SqlCommand(selectComand + fromComand + whereComand, conn))
        //            {
        //                if (!idSucursal.Equals(0))
        //                    cmd.Parameters.Add(new SqlParameter("@idCliente", idCliente));
        //                if (!idVendedor.Equals(0))
        //                    cmd.Parameters.Add(new SqlParameter("@idVendedor", idVendedor));
        //                cmd.Parameters.Add(new SqlParameter("@idFormaPago", idFormaPago));
        //                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
        //                {
        //                    sda.Fill(dt);
        //                    return dt;
        //                }
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //    }
        //}
        //public DataTable getMontos(string fFechaI, string fFechaF, int idVendedor = 0, int idSucursal = 0)
        //{
        //    try
        //    {
        //        DataTable dt = new DataTable();
        //        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnx"].ConnectionString))
        //        {
        //            conn.Open();
        //            string selectComand = "cl.clave, cl.nombre AS cliente, v.id AS claveV, v.nombre AS vendedor, o.folio, s.reqFac AS factura, ISNULL(ds.total,0) AS total, ISNULL " +
        //                "((SELECT SUM(ISNULL(monto, 0)) AS Expr1 FROM pagosFinanzas AS pf WHERE(idSolicitud = s.id)), 0) AS pagado, o.fecha ";
        //            string fromComand = "FROM ordenes AS o INNER JOIN " +
        //                 "solicitudes AS s ON o.idSolicitud = s.id INNER JOIN " +
        //                 "clientes AS cl ON s.idCliente = cl.id INNER JOIN " +
        //                 "usuarios AS v ON s.idVendedor = v.id INNER JOIN " +
        //                 "detallesSolicitud AS ds ON s.id = ds.idSolicitud ";
        //            string whereComand = "WHERE (s.eliminada IS NULL) AND (o.eliminado IS NULL) AND (ds.eliminado IS NULL) ";

        //            if (!idSucursal.Equals(0))
        //                whereComand += "AND (s.idSucursal = @idSucursal) ";
        //            else
        //                whereComand += "AND (s.idSucursal NOT LIKE '5') ";

        //            if (!idVendedor.Equals(0))
        //                whereComand += "AND (s.idVendedor = @idVendedor) ";

        //            whereComand += " AND (o.fecha BETWEEN '" + fFechaI + "' AND '" + fFechaF + "')";

        //            using (SqlCommand cmd = new SqlCommand(selectComand + fromComand + whereComand, conn))
        //            {
        //                if (!idSucursal.Equals(0))
        //                    cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
        //                if (!idVendedor.Equals(0))
        //                    cmd.Parameters.Add(new SqlParameter("@idVendedor", idVendedor));
        //                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
        //                {
        //                    sda.Fill(dt);
        //                    return dt;
        //                }
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //    }
        //}

        public DataTable getClientes(int idVendedor = 0, int idSucursal = 0)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnx"].ConnectionString))
                {
                    conn.Open();
                    string selectComand = "SELECT  c.id, c.clave, c.nombre AS cliente, v.nombre AS vendedor ";
                    string fromComand = "FROM clientes AS c INNER JOIN usuarios AS v ON c.idVendedor = v.id ";
                    string whereComand = "WHERE (c.eliminado IS NULL) ";

                    if (!idSucursal.Equals(0))
                        whereComand += "AND (c.idSucursal = @idSucursal) ";
                    else
                        whereComand += "AND (c.idSucursal NOT LIKE '5') ";

                    if (!idVendedor.Equals(0))
                        whereComand += "AND (c.idVendedor = @idVendedor) ";

                    using (SqlCommand cmd = new SqlCommand(selectComand + fromComand + whereComand, conn))
                    {
                        if (!idSucursal.Equals(0))
                            cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
                        if (!idVendedor.Equals(0))
                            cmd.Parameters.Add(new SqlParameter("@idVendedor", idVendedor));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public DataTable getSolicitudesByCliente(int idCliente)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnx"].ConnectionString))
                {
                    conn.Open();
                    string selectComand = "SELECT s.id, s.idFormaPago, s.fecha, ";
                    string selectComand1 = "(SELECT SUM(ISNULL(total, 0)) AS Expr1 FROM detallesSolicitud AS ds WHERE(ds.idSolicitud = s.id) AND (ds.eliminado IS NULL)) AS debe, ";
                    string selectComand2 = "(SELECT ISNULL(SUM(ISNULL(monto, 0)), 0) AS Expr1 FROM pagosFinanzas AS pf WHERE(idSolicitud = s.id) AND (pf.estatus = 'Pagado')) AS pagado ";
                    string fromComand = "FROM solicitudes AS s ";
                    string whereComand = "WHERE (s.eliminada IS NULL) AND (s.idCliente = @idCliente) ";

                    using (SqlCommand cmd = new SqlCommand(selectComand + selectComand1 + selectComand2 + fromComand + whereComand, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idCliente", idCliente));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        private void llenarLV()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("clave");
            dt.Columns.Add("cliente");
            dt.Columns.Add("vencido");
            decimal vencido = 0, tVencido = 0; 
            dt.Columns.Add("07");
            decimal d07 = 0, tD07 = 0;
            dt.Columns.Add("814");
            decimal d814 = 0, tD814 = 0;
            dt.Columns.Add("1521");
            decimal d1521 = 0, tD1521 = 0;
            dt.Columns.Add("22");
            decimal d22 = 0, tD22 = 0;
            dt.Columns.Add("vencer");
            decimal vencer = 0, tVencer=0;
            dt.Columns.Add("total");
            decimal total = 0, tTotal = 0;
            dt.Columns.Add("vendedor");
            dt.Columns.Add("vacio");

            DataTable dtT = dt.Clone();

            DataTable dtC = getClientes(idVendedor, idSucursal);
            DataTable dtS = new DataTable();

            DateTime fechaHOY = DateTime.Parse(cUtl.getDateTimeFromDB());
            DateTime fecha = fechaHOY;

            decimal dVen = 0, dPag = 0, diff = 0;

            foreach (DataRow drC in dtC.Rows)
            {
                dVen = 0; 
                dPag = 0;
                DataRow dr = dt.NewRow();
                dr["clave"] = drC["clave"];
                dr["cliente"] = drC["cliente"];

                //TODO
                dtS = getSolicitudesByCliente(int.Parse(drC["id"].ToString()));

                if (dtS.Rows.Count>0)
                {
                    foreach (DataRow drS in dtS.Rows)
                    {
                        if (drS["idFormaPago"].Equals(1))
                            //Se suman 30 días
                            fecha = DateTime.Parse(drS["fecha"].ToString()).AddDays(30);
                        else
                            //se toma fecha tal cual
                            fecha = DateTime.Parse(drS["fecha"].ToString()).AddDays(30);

                        //asignar vencido y pagardo
                        try
                        {
                            dVen = decimal.Parse(drS["debe"].ToString());
                        }
                        catch (Exception)
                        {
                            dVen = 0;
                        }
                        try
                        {
                            dPag = decimal.Parse(drS["pagado"].ToString());
                        }
                        catch (Exception)
                        {
                            dPag = 0;
                        }
                        diff = dVen - dPag;

                        if (fecha < fechaHOY)
                        {
                            //TOTAL VENCIDO
                            vencido += diff;
                        }
                        else
                        {
                            //POR VENCER
                            if (fecha >= fechaHOY)
                            {
                                if (fecha < fechaHOY.AddDays(7))
                                {
                                    //0-7 días
                                    d07 += diff;
                                    tD07 += d07;
                                    break;
                                }
                                else if (fecha <= fechaHOY.AddDays(14))
                                {
                                    //8-14
                                    d814 += diff;
                                    tD814 += d814;
                                    break;
                                }
                                else if (fecha <= fechaHOY.AddDays(21))
                                {
                                    //15-21
                                    d1521 += diff;
                                    tD1521 += d1521;
                                    break;
                                }
                                else
                                {
                                    //22 o más
                                    d22 += diff;
                                    tD22 += d22;
                                }
                            }
                        }
                    }
                    //TOTAL
                    dr["vencido"] = vencido.ToString("N1", CultureInfo.InvariantCulture);
                    tVencido += vencido;
                    dr["07"] = d07.ToString("N1", CultureInfo.InvariantCulture);
                    dr["814"] = d814.ToString("N1", CultureInfo.InvariantCulture);
                    dr["1521"] = d1521.ToString("N1", CultureInfo.InvariantCulture);
                    dr["22"] = d22.ToString("N1", CultureInfo.InvariantCulture);
                    vencer = d07 + d814 + d1521 + d22;
                    tVencer += vencer;
                    dr["vencer"] = vencer.ToString("N1", CultureInfo.InvariantCulture);
                    total = vencido + vencer;
                    tTotal += total;
                    dr["total"] = total.ToString("N1", CultureInfo.InvariantCulture);
                    dr["vendedor"] = drC["vendedor"];
                    dr["vacio"] = "";

                    //Variables a ceros
                    vencido = 0;
                    d07 = 0;
                    d814 = 0;
                    d1521 = 0;
                    d22 = 0;
                    vencer = 0;
                    total = 0;

                    dt.Rows.Add(dr);
                }
            }

            lv.DataSource = dt;
            lv.DataBind();

            DataRow drT = dtT.NewRow();
            drT["clave"] = "";
            drT["cliente"] = "";
            drT["vencido"] = tVencido.ToString("N1", CultureInfo.InvariantCulture);
            drT["07"] = tD07.ToString("N1", CultureInfo.InvariantCulture);
            drT["814"] = tD814.ToString("N1", CultureInfo.InvariantCulture);
            drT["1521"] = tD1521.ToString("N1", CultureInfo.InvariantCulture);
            drT["22"] = tD22.ToString("N1", CultureInfo.InvariantCulture);
            drT["vencer"] = tVencer.ToString("N1", CultureInfo.InvariantCulture);
            drT["total"] = tTotal.ToString("N1", CultureInfo.InvariantCulture);
            drT["vendedor"] = "";
            drT["vacio"] = "";

            dtT.Rows.Add(drT);

            lvTotales.DataSource = dtT;
            lvTotales.DataBind();
        }
    }
}