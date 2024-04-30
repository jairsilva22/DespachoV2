using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace despacho
{
    public class cReporteResultados
    {

        public DataTable getIngresos(string fFechaI, string fFechaF, int idSucursal = 0)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnx"].ConnectionString))
                {
                    conn.Open();
                    string selectComand = "SELECT ISNULL(SUM(ds.cantidad), 0) AS cantidadSolicitada, ISNULL(SUM(ds.cantidadEntregada), 0) AS cantidadEntregada, " +
                        " ISNULL(SUM(ds.precioU), 0) AS precioUnitario, ISNULL(SUM(ds.precioF), 0) AS precioFactor ";
                    string fromComand = "FROM ordenes AS o INNER JOIN solicitudes AS s ON o.idSolicitud = s.id INNER JOIN detallesSolicitud AS ds ON s.id = ds.idSolicitud INNER JOIN productos AS p ON ds.idProducto = p.id " +
                        "INNER JOIN tiposProductos AS tp ON p.idTipoProducto = tp.id ";
                    string whereComand = "WHERE (o.eliminado IS NULL) ";

                    if (!idSucursal.Equals(0))
                        whereComand += "AND (s.idSucursal = @idSucursal) ";
                    else
                        whereComand += "AND (s.idSucursal NOT LIKE '5') ";

                    whereComand += " AND (o.fecha BETWEEN '" + fFechaI + "' AND '" + fFechaF + "')";

                    using (SqlCommand cmd = new SqlCommand(selectComand + fromComand + whereComand, conn))
                    {
                        if (!idSucursal.Equals(0))
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
        public DataTable getIngresosM(string fFechaI, string fFechaF, int idP, int idSucursal = 0)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnx"].ConnectionString))
                {
                    conn.Open();
                    string selectComand = "SELECT (ISNULL(ds.cantidadEntregada,0)*ISNULL(ds.precioU,0)) AS Bruto, (ISNULL(ds.cantidadEntregada,0)*ISNULL(ds.precioF,0)) AS Descuento ";
                    string fromComand = "FROM ordenes AS o INNER JOIN solicitudes AS s ON o.idSolicitud = s.id INNER JOIN detallesSolicitud AS ds ON s.id = ds.idSolicitud INNER JOIN productos AS p ON ds.idProducto = p.id " +
                        "INNER JOIN tiposProductos AS tp ON p.idTipoProducto = tp.id ";
                    string whereComand = "WHERE (o.eliminado IS NULL) AND (ds.idProducto=@idP) ";

                    if (!idSucursal.Equals(0))
                        whereComand += "AND (s.idSucursal = @idSucursal) ";
                    else
                        whereComand += "AND (s.idSucursal NOT LIKE '5') ";

                    whereComand += " AND (o.fecha BETWEEN '" + fFechaI + "' AND '" + fFechaF + "')";

                    using (SqlCommand cmd = new SqlCommand(selectComand + fromComand + whereComand, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idP", idP));
                        if (!idSucursal.Equals(0))
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
        public DataTable getProductos(string fFechaI, string fFechaF, int idSucursal = 0)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnx"].ConnectionString))
                {
                    conn.Open();
                    string selectComand = "SELECT DISTINCT(ds.idProducto) AS idProducto ";
                    string fromComand = "FROM ordenes AS o INNER JOIN solicitudes AS s ON o.idSolicitud = s.id INNER JOIN detallesSolicitud AS ds ON s.id = ds.idSolicitud INNER JOIN productos AS p ON ds.idProducto = p.id " +
                        "INNER JOIN tiposProductos AS tp ON p.idTipoProducto = tp.id ";
                    string whereComand = "WHERE (o.eliminado IS NULL) ";

                    if (!idSucursal.Equals(0))
                        whereComand += "AND (s.idSucursal = @idSucursal) ";
                    else
                        whereComand += "AND (s.idSucursal NOT LIKE '5') ";

                    whereComand += " AND (o.fecha BETWEEN '" + fFechaI + "' AND '" + fFechaF + "')";

                    using (SqlCommand cmd = new SqlCommand(selectComand + fromComand + whereComand, conn))
                    {
                        if (!idSucursal.Equals(0))
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

        public DataTable getBitacora(string fFechaI, string fFechaF, int idSucursal = 0)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnx"].ConnectionString))
                {
                    conn.Open();
                    string selectComand = "SELECT bd.idOD, od.idSucursal, bd.idMaterial, bd.material, bd.cantidad, rmcd.costoKG ";
                    string fromComand = "FROM ordenDosificacion AS od INNER JOIN bitacoraDosificacion AS bd ON od.id = bd.idOD RIGHT OUTER JOIN relMaterialesComprasDosificacion AS rmcd ON bd.idMaterial = rmcd.idMaterialDosificacion ";
                    string whereComand = "WHERE (od.eliminado IS NULL) ";

                    if (!idSucursal.Equals(0))
                        whereComand += "AND (od.idSucursal = @idSucursal) ";
                    else
                        whereComand += "AND (od.idSucursal NOT LIKE '5') ";

                    whereComand += " AND (od.fecha BETWEEN '" + fFechaI + "' AND '" + fFechaF + "')";

                    using (SqlCommand cmd = new SqlCommand(selectComand + fromComand + whereComand, conn))
                    {
                        if (!idSucursal.Equals(0))
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

        public DataTable getSalidasCompras(string fFechaI, string fFechaF, int idER, int idSucursal = 0)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnxCompras"].ConnectionString))
                {
                    conn.Open();
                    string selectComand = "SELECT ISNULL(SUM(ISNULL(ds.cantidad * ds.precio, 0)),0) AS total ";
                    string fromComand = "FROM estadosResultado AS er INNER JOIN detSalidas AS ds INNER JOIN ksroc.salida AS s ON ds.idSalida = s.idsal INNER JOIN Estacion1 AS cc ON s.idDestino = cc.IdEstacion ON er.id = cc.idER ";
                    string whereComand = "WHERE (s.estatus='Entregado') AND (cc.idER=@idER) AND (IdEstacion <>3025) ";

                    if (!idSucursal.Equals(0))
                        whereComand += "AND (s.proyecto = @idSucursal) ";
                    else
                        whereComand += "AND (s.proyecto NOT LIKE '1004') ";

                    whereComand += " AND (s.fechaSalida BETWEEN '" + fFechaI + "' AND '" + fFechaF + "')";

                    using (SqlCommand cmd = new SqlCommand(selectComand + fromComand + whereComand, conn))
                    {
                        if (!idSucursal.Equals(0))
                            cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
                        cmd.Parameters.Add(new SqlParameter("@idER", idER));
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

        public DataTable getNomina(int idER, int mes, int idSucursal = 0)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnx"].ConnectionString))
                {
                    conn.Open();
                    string selectComand = "SELECT ep.monto ";
                    string fromComand = "FROM empleados AS e INNER JOIN empleadosPercepciones AS ep ON e.codigo = ep.idEmpleado INNER JOIN departamento AS d ON e.dpto = d.idCompaqi INNER JOIN " +
                         "estadosResultado AS er ON d.idEdoR = er.id ";
                    string whereComand = "WHERE (d.idEdoR=@idER) ";

                    if (!idSucursal.Equals(0))
                        whereComand += "AND (e.idSucursal = @idSucursal) and ep.idsucursal=@idSucursal";
                    else
                        whereComand += "AND (e.idSucursal NOT LIKE '5') ";

                    whereComand += " AND (ep.mes = @mes)";

                    using (SqlCommand cmd = new SqlCommand(selectComand + fromComand + whereComand, conn))
                    {
                        if (!idSucursal.Equals(0))
                            cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
                        cmd.Parameters.Add(new SqlParameter("@mes", mes));
                        cmd.Parameters.Add(new SqlParameter("@idER", idER));
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
    }
}