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
    public partial class reporteBitacoraDosificacion : System.Web.UI.Page
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
                lblFechaIni.Text = Request.QueryString["fechaIni"];
                lblFechaFin.Text = Request.QueryString["fechaFin"];
                idSucursal = int.Parse(Request.QueryString["idSucursal"].ToString());

                idSucursalCompras = cSuc.obtenerComprasID(idSucursal);

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
                Response.AddHeader("content-disposition", "attachment;filename=ReporteBitacoraDosificacion-" + DateTime.Today.ToShortDateString() + ".xls");
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

        public DataTable getBitacoraDosificacion(string fIni, string fFin)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnx"].ConnectionString))
                {
                    conn.Open();
                    string selectComand = "SELECT  od.id, o.folio AS orden, od.folio, od.folioR, bd.bachada, bd.captura, bd.idMaterial, bd.material, bd.udm, bd.cantEnviada, bd.cantAjustada, bd.cantDosificada, bd.captura, bs.inicioCarga AS fecha, bs.inicioCarga, bs.finCarga, bs.inicioDescarga, bs.finDescarga ";
                    string fromComand = "FROM bitacoraODosificacion AS bd INNER JOIN bitacoraSimulacion AS bs ON bd.idOD = bs.idOD AND bd.bachada = bs.bachada INNER JOIN " +
                        "ordenDosificacion AS od ON bd.idOD = od.id INNER JOIN ordenes AS o ON od.idOrden = o.id ";
                    string whereComand = "WHERE (od.idSucursal = @idSucursal) AND od.fecha BETWEEN '" + fIni + "' AND '" + fFin + "'";


                    using (SqlCommand cmd = new SqlCommand(selectComand + fromComand + whereComand, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
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
        public DataTable getTAGsByIdOD(int idOD, int idM)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnx"].ConnectionString))
                {
                    conn.Open();
                    string selectComand = "SELECT mt.idMaterial, mt.TAG, bacs.tag AS TAG2, bacs.valor, mt.orden ";
                    string fromComand = "FROM bitacoraAjusteCargaSimulacion AS bacs INNER JOIN materialTAGs AS mt ON bacs.tag = mt.TAG ";
                    string whereComand = "WHERE (bacs.idOD=@idOD) AND (mt.idMaterial=@idM) ";
                    string orderComand = "ORDER BY mt.orden ";

                    using (SqlCommand cmd = new SqlCommand(selectComand + fromComand + whereComand + orderComand, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idOD", idOD));
                        cmd.Parameters.Add(new SqlParameter("@idM", idM));
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
            dt.Columns.Add("Orden");
            dt.Columns.Add("Folio");
            dt.Columns.Add("Remision");
            dt.Columns.Add("Bachada");
            dt.Columns.Add("Captura");
            dt.Columns.Add("Material");
            dt.Columns.Add("Objetivo");
            decimal objetivo = 0, tObjetivo = 0;
            dt.Columns.Add("Real");
            decimal real = 0, tReal = 0;
            dt.Columns.Add("Diferencia");
            decimal diferencia = 0, diferenciaP = 0, diferenciaN = 0, tDiferenciaP = 0, tDiferenciaN = 0;
            dt.Columns.Add("Porcentaje");
            dt.Columns.Add("TAG_VI");
            dt.Columns.Add("CANT_VI");
            dt.Columns.Add("TAG_VMF");
            dt.Columns.Add("CANT_VMF");
            dt.Columns.Add("TAG_VTF");
            dt.Columns.Add("CANT_VTF");
            dt.Columns.Add("Fecha");
            dt.Columns.Add("InicioCarga");
            dt.Columns.Add("FinCarga");
            dt.Columns.Add("DiferenciaCarga");
            dt.Columns.Add("InicioDescarga");
            dt.Columns.Add("FinDescarga");
            dt.Columns.Add("DiferenciaDescarga");

            DateTime dateIC = DateTime.Now;
            DateTime dateFC = DateTime.Now;
            TimeSpan tsC = new TimeSpan();
            DateTime dateID = DateTime.Now;
            DateTime dateFD = DateTime.Now;
            TimeSpan tsD = new TimeSpan();

            DataTable dtT = dt.Clone();

            DataTable dtBD = getBitacoraDosificacion(lblFechaIni.Text, lblFechaFin.Text);

            foreach (DataRow drBD in dtBD.Rows)
            {
                DataRow dr = dt.NewRow();
                dr["Orden"] = drBD["orden"];
                dr["Folio"] = drBD["folio"];
                dr["Remision"] = drBD["folioR"];
                dr["Bachada"] = drBD["bachada"];
                dr["Captura"] = drBD["captura"];
                dr["Material"] = drBD["material"] + " - " + drBD["udm"];
                dr["Objetivo"] = drBD["cantEnviada"];
                objetivo = decimal.Parse(drBD["cantEnviada"].ToString());
                tObjetivo += objetivo;
                dr["Real"] = drBD["cantDosificada"];
                real = decimal.Parse(drBD["cantDosificada"].ToString());
                tReal += real;

                diferencia = real - objetivo;
                if (diferencia < 0) { 
                    diferenciaN = diferencia;
                    tDiferenciaN += diferenciaN;
                }
                else { 
                    diferenciaP = diferencia;
                    tDiferenciaP += diferenciaP;
                }
                dr["Diferencia"] = diferencia.ToString();
                if (!real.Equals(0)) 
                    if (diferencia < 0)
                        dr["porcentaje"] = String.Format(CultureInfo.InvariantCulture, "{0:#0.##%}", 0);
                    else
                        dr["porcentaje"] = String.Format(CultureInfo.InvariantCulture, "{0:#0.##%}", diferencia / real);
                else
                {
                    dr["porcentaje"] = String.Format(CultureInfo.InvariantCulture, "{0:#0.##%}", 0);
                }

                DataTable dtTAGs = getTAGsByIdOD(int.Parse(drBD["id"].ToString()), int.Parse(drBD["idMaterial"].ToString()));

                try
                {
                    dr["TAG_VI"] = dtTAGs.Rows[0]["TAG"].ToString();
                    dr["CANT_VI"] = dtTAGs.Rows[0]["valor"].ToString();
                    dr["TAG_VMF"] = dtTAGs.Rows[1]["TAG"].ToString();
                    dr["CANT_VMF"] = dtTAGs.Rows[1]["valor"].ToString();
                    dr["TAG_VTF"] = dtTAGs.Rows[2]["TAG"].ToString();
                    dr["CANT_VTF"] = dtTAGs.Rows[2]["valor"].ToString();
                }
                catch (Exception)
                {
                    dr["TAG_VI"] = "";
                    dr["CANT_VI"] = "";
                    dr["TAG_VMF"] = "";
                    dr["CANT_VMF"] = "";
                    dr["TAG_VTF"] = "";
                    dr["CANT_VTF"] = "";
                }

                dr["Fecha"] = drBD["fecha"].ToString().Substring(0,10);
                try
                {
                    dr["InicioCarga"] = drBD["inicioCarga"].ToString().Substring(11, drBD["inicioCarga"].ToString().Length - 16);
                    dateIC = DateTime.Parse(drBD["inicioCarga"].ToString());
                }
                catch (Exception)
                {
                    dr["InicioCarga"] = "";
                }
                try
                {
                    dr["FinCarga"] = drBD["finCarga"].ToString().Substring(11, drBD["finCarga"].ToString().Length - 16);
                    dateFC = DateTime.Parse(drBD["finCarga"].ToString());
                }
                catch (Exception)
                {
                    dr["FinCarga"] = "";
                }
                try
                {
                    tsC = dateFC - dateIC;
                    if (tsC.TotalSeconds>0)
                        dr["DiferenciaCarga"] = tsC.Minutes + " min " + tsC.Seconds + " seg";
                    else
                        dr["DiferenciaCarga"] = "";
                }
                catch (Exception)
                {
                    dr["DiferenciaCarga"] = "";
                }

                try
                {
                    dr["InicioDescarga"] = drBD["inicioDescarga"].ToString().Substring(11, drBD["inicioDescarga"].ToString().Length - 16);
                    dateID = DateTime.Parse(drBD["InicioDescarga"].ToString());
                }
                catch (Exception)
                {
                    dr["InicioDescarga"] = "";
                }
                try
                {
                    dr["FinDescarga"] = drBD["finDescarga"].ToString().Substring(11, drBD["finDescarga"].ToString().Length - 16);
                    dateFD = DateTime.Parse(drBD["FinDescarga"].ToString());
                }
                catch (Exception)
                {
                    dr["FinDescarga"] = "";
                }
                try
                {
                    tsD = dateFD - dateID;
                    if (tsD.TotalSeconds > 0)
                        dr["DiferenciaDescarga"] = tsD.Minutes + " min " + tsD.Seconds + " seg";
                    else
                        dr["DiferenciaDescarga"] = "";
                }
                catch (Exception)
                {
                    dr["DiferenciaDescarga"] = "";
                }

                dt.Rows.Add(dr);
            }

            lv.DataSource = dt;
            lv.DataBind();

            //DataRow drT = dtT.NewRow();
            //drT["clave"] = "";
            //drT["cliente"] = "";
            //drT["vencido"] = tVencido.ToString("N1", CultureInfo.InvariantCulture);
            //drT["07"] = tD07.ToString("N1", CultureInfo.InvariantCulture);
            //drT["814"] = tD814.ToString("N1", CultureInfo.InvariantCulture);
            //drT["1521"] = tD1521.ToString("N1", CultureInfo.InvariantCulture);
            //drT["22"] = tD22.ToString("N1", CultureInfo.InvariantCulture);
            //drT["vencer"] = tVencer.ToString("N1", CultureInfo.InvariantCulture);
            //drT["total"] = tTotal.ToString("N1", CultureInfo.InvariantCulture);
            //drT["vendedor"] = "";
            //drT["vacio"] = "";

            //dtT.Rows.Add(drT);

            //lvTotales.DataSource = dtT;
            //lvTotales.DataBind();
        }
    }
}