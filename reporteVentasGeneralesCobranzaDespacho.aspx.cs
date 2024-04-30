using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace despacho
{
    public partial class reporteVentasGeneralesCobranzaDespacho : System.Web.UI.Page
    {
        int idSucursal = 0;
        DateTime fechaInicio = DateTime.Now;
        DateTime fechaFin = DateTime.Now;
        cSucursales cSuc = new cSucursales();
        cContpaq cContpaq = new cContpaq();
        int ndtCuenta = 0;
        double TotalVencido = 0;
        double PorVencer = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            lblFechaFin.Text += Request.QueryString["FechaInicio"];

            if (Request.Cookies["ksroc"]["idSucursal"] != "" && Request.Cookies["ksroc"]["idSucursal"] != null)
            {
                idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
            }
            else
            {
                idSucursal = int.Parse(Request.QueryString["idSucursal"]);
            }

            cSuc.id = idSucursal;
            cSuc.nombre = cSuc.obtenerNombreSucursalByID(idSucursal);

            imagen.InnerHtml = "<img src='img/pepi_logo.png' width='100' height='78'/>&nbsp;&nbsp;";

            llenarLV();
        }

        public string ToFormat24h(DateTime dt)
        {
            return dt.ToString("yyyy/MM/dd HH:mm:ss");
        }

        public void llenarAgGrid(DataTable dt)
        {
            var reporteVGC = new List<reporteVGC>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //Añadir movimiento a la lista
                reporteVGC.Add(new reporteVGC() { codigo = dt.Rows[i]["Codigo"].ToString(), nomCliente = dt.Rows[i]["Nombre"].ToString(), nomAgente = dt.Rows[i]["Agente"].ToString(), totVencido = dt.Rows[i]["TotalVencido"].ToString(), totAVencer = dt.Rows[i]["TotalporVencer"].ToString(), total = dt.Rows[i]["Total"].ToString(), sucursal = dt.Rows[i]["Sucursal"].ToString() });

            }

            var serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = 2147483647;
            var serializedResult = serializer.Serialize(reporteVGC);

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", "llenarAgGrid(" + serializedResult + ");", true);
        }


        protected void llenarLV()
        {
            //Cadena de conexión para las distintas bases de datos
            string cadena;

            //Establecemos la tabla del reporte y sus columnas
            DataTable dtcuenta = new DataTable();
            dtcuenta.Columns.Add("Codigo");
            dtcuenta.Columns.Add("Nombre");
            dtcuenta.Columns.Add("Agente");
            dtcuenta.Columns.Add("TotalVencido");
            dtcuenta.Columns.Add("TotalporVencer");
            dtcuenta.Columns.Add("Total");
            dtcuenta.Columns.Add("Sucursal");

            //declaramos las fechas del reporte en base al filtro de la pag. anterior
            fechaInicio = DateTime.Parse(Request.QueryString["FechaInicio"]);

            //Validar perfil
            string perfil = Request.Cookies["login"]["idPerfil"];

            DataTable dtCTA;
            dtCTA = obtenerVentasGenerales(Request.QueryString["FechaInicio"]);
            llenarDataTable(dtCTA, dtcuenta);

            double totalEmpresa = TotalVencido + PorVencer;
            lblTotal.Text = "$" + totalEmpresa.ToString("#,##0.00");
            lblTotalVencer.Text = "$" + PorVencer.ToString("#,##0.00");
            lblTotalVencido.Text = "$" + TotalVencido.ToString("#,##0.00");

            llenarAgGrid(dtcuenta);
        }
                
        //metodo para obtener los Reporte de Ventas Generales Cobranza por sucursal
        public DataTable obtenerVentasGenerales(string fechaInicio)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnx"].ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT s.id, s.idCliente, C.clave, C.nombre, s.idFormaPago, s.fecha,  " +
                        "(SELECT ISNULL(SUM(ISNULL(total, 0)), 0) AS Expr1 FROM detallesSolicitud AS ds WHERE(ds.idSolicitud = s.id) AND (ds.eliminado IS NULL)) AS debe,   " +
                        "(SELECT ISNULL(SUM(ISNULL(monto, 0)), 0) AS Expr1 FROM pagosFinanzas AS pf WHERE(idSolicitud = s.id) AND (pf.estatus = 'Pagado')) AS pagados, " +
                        "s.reqFac, C.idVendedor, V.nombre AS vendedor, su.nombre AS sucursal " +
                        "FROM solicitudes AS s INNER JOIN clientes as C on s.idCliente = C.id INNER JOIN usuarios as V on C.idVendedor = V.id INNER JOIN sucursales AS SU ON s.idSucursal = su.id RIGHT JOIN ordenes AS o ON o.idSolicitud = s.id RIGHT JOIN ordenDosificacion AS od ON od.idOrden = o.id " +
                        "WHERE (s.eliminada IS NULL) AND s.fecha BETWEEN '2010-03-19' AND '2021-12-01' AND su.id IN (1, 2, 3, 1006) and od.cantidad > 0 and od.eliminado IS NULL " +
                        "order by V.nombre, reqFac desc", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@fechaInicio", fechaInicio));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            if (dt.Rows.Count == 0)
                            {
                                return null;
                            }
                            else
                            {
                                return dt;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                return null;
                //throw (ex);
            }
        }

        //Llenar datatable con un método aplicable a todas las sucursales
        public void llenarDataTable(DataTable dtCTA, DataTable dtcuenta)
        {

            string pivoteCliente = "";
            string Nombresucursal;
            //string pivote2 = dtCTAporVencer.Rows[0]["CIDAGENTE"].ToString();


            double vencer = 0, vencido = 0;

            if (dtCTA == null)
            {
                DataRow rw = dtcuenta.NewRow();
                rw["Codigo"] = "#";
                rw["Nombre"] = "No hay datos";
                rw["Agente"] = "No hay datos";
                float rwTotalVencido = 0;
                float rwTotal = 0;
                rw["Total"] = "$" + rwTotal.ToString("#,##0.00");
                rw["TotalporVencer"] = "$" + rwTotalVencido.ToString("#,##0.00");
                rw["TotalVencido"] = "$0";
                rw["Sucursal"] = "No hay datos";
                dtcuenta.Rows.Add(rw);
                ndtCuenta++;

            }
            else
            {
                for (int i = 0; i < (dtCTA.Rows.Count); i++)
                {

                    //Definimos una fila para la tabla del reporte y llenamos con informacion de nuestra consulta
                    DataRow rw = dtcuenta.NewRow();
                    Nombresucursal = dtCTA.Rows[i]["sucursal"].ToString();

                    // Por Vencer
                    if (DateTime.Parse(Request.QueryString["FechaInicio"]) <= DateTime.Parse(dtCTA.Rows[i]["fecha"].ToString()))
                    {
                        rw["Codigo"] = dtCTA.Rows[i]["idCliente"].ToString();
                        rw["Nombre"] = dtCTA.Rows[i]["nombre"].ToString();
                        rw["Agente"] = dtCTA.Rows[i]["vendedor"].ToString();
                        float rwTotalVencido = float.Parse(dtCTA.Rows[i]["debe"].ToString());
                        float rwTotal = float.Parse(dtCTA.Rows[i]["debe"].ToString());
                        rw["Total"] = "$" + rwTotal.ToString("#,##0.00");
                        rw["TotalporVencer"] = "$" + rwTotalVencido.ToString("#,##0.00");
                        rw["TotalVencido"] = "$0";
                        vencer = double.Parse(dtCTA.Rows[i]["debe"].ToString());
                    }

                    //Vencidos
                    if (DateTime.Parse(Request.QueryString["FechaInicio"]) > DateTime.Parse(dtCTA.Rows[i]["fecha"].ToString()))
                    {
                        rw["Codigo"] = dtCTA.Rows[i]["idCliente"].ToString();
                        rw["Nombre"] = dtCTA.Rows[i]["nombre"].ToString();
                        rw["Agente"] = dtCTA.Rows[i]["vendedor"].ToString();
                        float rwTotal = float.Parse(dtCTA.Rows[i]["debe"].ToString());
                        rw["Total"] = "$" + rwTotal.ToString("#,##0.00");
                        float rwTotalVencido = float.Parse(dtCTA.Rows[i]["debe"].ToString());
                        rw["TotalVencido"] = "$" + rwTotalVencido.ToString("#,##0.00");
                        rw["TotalporVencer"] = "$0";
                        vencido = double.Parse(dtCTA.Rows[i]["debe"].ToString());
                    }

                    //vencido
                    if (DateTime.Parse(Request.QueryString["FechaInicio"]) > DateTime.Parse(dtCTA.Rows[i]["fecha"].ToString()))
                    {
                        TotalVencido += float.Parse(dtCTA.Rows[i]["debe"].ToString());
                    }

                    //Por Vencer
                    if (DateTime.Parse(Request.QueryString["FechaInicio"]) <= DateTime.Parse(dtCTA.Rows[i]["fecha"].ToString()))
                    {
                        PorVencer += float.Parse(dtCTA.Rows[i]["debe"].ToString());
                    }



                    //No mostrar filas del mismo cliente con el mismo agente
                    if (pivoteCliente == dtCTA.Rows[i]["idCliente"].ToString())
                    {
                        // Por Vencer
                        if (DateTime.Parse(Request.QueryString["FechaInicio"]) <= DateTime.Parse(dtCTA.Rows[i]["fecha"].ToString()))
                        {
                            string acumulado = dtcuenta.Rows[ndtCuenta - 1]["TotalporVencer"].ToString().Replace(",", "");
                            acumulado = acumulado.Replace("$", "");
                            double acumuladoNumero = double.Parse(acumulado);
                            acumuladoNumero += vencer;
                            //Total cliente
                            string acumuladoTotal = dtcuenta.Rows[ndtCuenta - 1]["Total"].ToString().Replace(",", ""); ;
                            acumuladoTotal = acumuladoTotal.Replace("$", "");
                            double totalClientePro = double.Parse(acumuladoTotal);
                            totalClientePro += vencer;

                            dtcuenta.Rows[ndtCuenta - 1]["TotalporVencer"] = "$" + acumuladoNumero.ToString("#,##0.00");
                            dtcuenta.Rows[ndtCuenta - 1]["Total"] = "$" + totalClientePro.ToString("#,##0.00");
                        }

                        //Vencidos
                        if (DateTime.Parse(Request.QueryString["FechaInicio"]) > DateTime.Parse(dtCTA.Rows[i]["fecha"].ToString()))
                        {
                            string acumulado = dtcuenta.Rows[ndtCuenta - 1]["TotalVencido"].ToString().Replace(",", "");
                            acumulado = acumulado.Replace("$", "");
                            double acumuladoNumero = double.Parse(acumulado);
                            acumuladoNumero += vencido;

                            //Total cliente
                            string acumuladoTotal = dtcuenta.Rows[ndtCuenta - 1]["Total"].ToString().Replace(",", ""); ;
                            acumuladoTotal = acumuladoTotal.Replace("$", "");
                            double totalClientePro = double.Parse(acumuladoTotal);
                            totalClientePro += vencido;

                            dtcuenta.Rows[ndtCuenta - 1]["TotalVencido"] = "$" + acumuladoNumero.ToString("#,##0.00");
                            dtcuenta.Rows[ndtCuenta - 1]["Total"] = "$" + totalClientePro.ToString("#,##0.00");
                        }
                    }
                    else
                    {
                        
                        if (dtCTA.Rows[i]["reqFac"].ToString().Equals("SI")) {
                            Nombresucursal += " Facturable";
                        }
                        else {
                            Nombresucursal += " Ventas General";
                        }
                        //Agregamos la fila a la tabla del reporte
                        rw["Sucursal"] = Nombresucursal;
                        dtcuenta.Rows.Add(rw);
                        ndtCuenta++;
                        pivoteCliente = dtCTA.Rows[i]["idCliente"].ToString();
                    }


                    //lvCliente.DataSource = dtcuenta;
                    //lvCliente.DataBind();
                }
            }
            //DataRow rw4 = dtcuenta.NewRow();
            //rw4["TotalVencido"] = " $" + SumaVencido.ToString("#,##0.00") + "";
            //rw4["TotalporVencer"] = " $" + SumaporVencer.ToString("#,##0.00") + "";
            //rw4["Total"] = " $" + SumaTotal.ToString("#,##0.00") + "";
            //rw4["Agente"] = "TOTAL SUCURSAL";
            //dtcuenta.Rows.Add(rw4);
        }

    }
}

