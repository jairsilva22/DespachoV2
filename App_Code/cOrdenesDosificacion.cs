using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace despacho
{
    public class cOrdenesDosificacion
    {
        //Clases base referencia
        cBitacora cBit = new cBitacora();
        cAlertaP cAP = new cAlertaP();
        cSucursales cSuc = new cSucursales();

        //variables
        private string cadena;

        //propiedades
        public int id { get; set; }
        public string folio { get; set; }
        public int idSucursal { get; set; }
        public DateTime fecha { get; set; }
        public string hora { get; set; }
        public int idOrden { get; set; }
        public int idProducto { get; set; }
        public float cantidad { get; set; }
        public int idUDM { get; set; }
        public int idTUT { get; set; }
        public int idDetalleSolicitud { get; set; }
        public int idEstadoDosificacion { get; set; }
        public int idUnidadTransporte { get; set; }
        public string colorBell { get; set; }
        public float revenimiento { get; set; }

        //Constructor
        public cOrdenesDosificacion()
        {
            cadena = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;
        }

        //metodo para insertar
        public void insertar(int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO ordenDosificacion(folio, idSucursal, fecha, hora, idOrden, idProducto, cantidad, idUDM, idDetalleSolicitud, idEstadoDosificacion, idUnidadTransporte, " +
                        "revenimiento, idUsuario, fechaAlta) VALUES (@folio ,@idSucursal, @fecha, @hora, @idOrden, @idProducto, @cantidad, @idUDM, @idDetalleSolicitud, @idEstadoDosificacion, @idUnidadTransporte, @revenimiento, " +
                        "@idUsuario, GETDATE())", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@folio", folio));
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
                        cmd.Parameters.Add(new SqlParameter("@fecha", fecha));
                        cmd.Parameters.Add(new SqlParameter("@hora", hora));
                        cmd.Parameters.Add(new SqlParameter("@idOrden", idOrden));
                        cmd.Parameters.Add(new SqlParameter("@idProducto", idProducto));
                        cmd.Parameters.Add(new SqlParameter("@cantidad", cantidad.ToString("0.00")));
                        cmd.Parameters.Add(new SqlParameter("@idUDM", idUDM));
                        cmd.Parameters.Add(new SqlParameter("@idDetalleSolicitud", idDetalleSolicitud));
                        cmd.Parameters.Add(new SqlParameter("@idEstadoDosificacion", idEstadoDosificacion));
                        cmd.Parameters.Add(new SqlParameter("@idUnidadTransporte", idUnidadTransporte));
                        cmd.Parameters.Add(new SqlParameter("@revenimiento", revenimiento));
                        cmd.Parameters.Add(new SqlParameter("@idUsuario", idUsuarioActivo));

                        int filas = cmd.ExecuteNonQuery();
                        string sTabla = "ordenDosificacion";
                        cBit.insertar(cBit.getID(sTabla), "INSERT", idUsuarioActivo, sTabla);
                    }
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metodo para actualizar 
        public void actualizar(int idO, int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE ordenDosificacion SET idSucursal = @idSucursal, fecha = @fecha, hora = @hora, idOrden = @idOrden, idProducto = @idProducto, " +
                        "cantidad = @cantidad, idUDM = @idUDM, idDetalleSolicitud = @idDetalleSolicitud, idEstadoDosificacion = @idEstadoDosificacion, " +
                        "idUnidadTransporte = @idUnidadTransporte, idUsuarioMod = @idUsuarioMod, fechaMod = GETDATE() WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
                        cmd.Parameters.Add(new SqlParameter("@fecha", fecha));
                        cmd.Parameters.Add(new SqlParameter("@hora", hora));
                        cmd.Parameters.Add(new SqlParameter("@idOrden", idOrden));
                        cmd.Parameters.Add(new SqlParameter("@idProducto", idProducto));
                        cmd.Parameters.Add(new SqlParameter("@cantidad", cantidad));
                        cmd.Parameters.Add(new SqlParameter("@idUDM", idUDM));
                        cmd.Parameters.Add(new SqlParameter("@idDetalleSolicitud", idDetalleSolicitud));
                        cmd.Parameters.Add(new SqlParameter("@idEstadoDosificacion", idEstadoDosificacion));
                        cmd.Parameters.Add(new SqlParameter("@idUnidadTransporte", idUnidadTransporte));
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioMod", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@id", idO));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        cBit.insertar(idO, "Update", idUsuarioActivo, "ordenDosificacion");
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metodo para actualizar 
        public void actualizarODFH(int idOD, int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE ordenDosificacion SET fecha = @fecha, hora = @hora, idUsuarioMod = @idUsuarioMod, fechaMod = GETDATE() WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@fecha", fecha));
                        cmd.Parameters.Add(new SqlParameter("@hora", hora));
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioMod", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@id", idOD));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        cBit.insertar(idOD, "Update", idUsuarioActivo, "ordenDosificacion");
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metodo para actualizar 
        public void asignarUnidadTransporte(int idO, int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE ordenDosificacion SET idUnidadTransporte = @idUnidadTransporte, idUsuarioMod = @idUsuarioMod, fechaMod = GETDATE() WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idEstadoDosificacion", idEstadoDosificacion));
                        cmd.Parameters.Add(new SqlParameter("@idUnidadTransporte", idUnidadTransporte));
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioMod", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@id", idO));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        cBit.insertar(idO, "Update", idUsuarioActivo, "ordenDosificacion", "Se Actualizó campo idUnidad de Transporte = " + idUnidadTransporte.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metodo para actualizar 
        public void actualizarProducto(int idO, int idOD, int idProducto, int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE ordenDosificacion SET idProducto = @idProducto, idUsuarioMod = @idUsuarioMod, fechaMod = GETDATE() WHERE idOrden = @idOrden", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idProducto", idProducto));
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioMod", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@idOrden", idO));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        cBit.insertar(idOD, "Update", idUsuarioActivo, "ordenDosificacion", "Se Actualizó campo idProducto = " + idProducto.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metodo para actualizar 
        public void liberarUnidadT(int idOD, int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE ordenDosificacion SET idUnidadTransporte = 1, idUsuarioMod = @idUsuarioMod, fechaMod = GETDATE() WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioMod", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@id", idOD));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        cBit.insertar(idOD, "Update", idUsuarioActivo, "ordenDosificacion", "Se Actualizó campo idUnidadTransporte a = " + idUnidadTransporte.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metodo para actualizar el color de la alarma 
        public void asignarColorBell(int idOD, int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE ordenDosificacion SET colorBell = @colorBell, idUsuarioMod = @idUsuarioMod, fechaMod = GETDATE() WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@colorBell", colorBell));
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioMod", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@id", idOD));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        cBit.insertar(idOD, "Update", idUsuarioActivo, "ordenDosificacion", "Se Actualizó campo colorBell a = " + colorBell);
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metodo para actualizar 
        public void actualizarEstadoDosificacion(int idO, int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE ordenDosificacion SET idEstadoDosificacion = @idEstadoDosificacion, idUsuarioMod = @idUsuarioMod, fechaMod = GETDATE() WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idEstadoDosificacion", idEstadoDosificacion));
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioMod", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@id", idO));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        cBit.insertar(idO, "Update", idUsuarioActivo, "ordenDosificacion", "Se Actualizó campo Estado de dosificacion a = " + idEstadoDosificacion.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metodo para eliminar
        public void eliminar(int idO, int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE ordenDosificacion SET eliminado=1, fechaElimino=GETDATE(), idUsuarioElimino=@idUsuarioElimino WHERE id=@id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioElimino", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@id", idO));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        cBit.insertar(idO, "Delete", idUsuarioActivo, "ordenDosificacion");
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metodo para generar el folio de la remisión u orden de entrega previo al alta
        public string generarFolio()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT s.codigo FROM ordenDosificacion AS od INNER JOIN sucursales AS s ON od.idSucursal = s.id WHERE(od.idSucursal = @idSucursal)", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            try
                            {
                                return dt.Rows[0]["codigo"].ToString() + (dt.Rows.Count + 1);
                            }
                            catch (Exception)
                            {
                                return cSuc.obtenerCodigoSucursal(idSucursal).ToString() + "1";
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        //metodo para el ListView de Dosificación
        public DataTable obtenerODByIdOrden(int idO)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT od.id, od.fecha, od.hora, od.folio, od.idOrden, p.id AS idP, p.codigo, p.descripcion, p.carga, p.idTipoProducto, od.cantidad, " +
                        "udm.unidad, tp.tipo AS tipoP " +
                        "FROM ordenDosificacion AS od INNER JOIN productos AS p ON od.idProducto = p.id INNER JOIN unidadesDeMedida AS udm ON od.idUDM = udm.id INNER JOIN " +
                        "ordenes AS o ON od.idOrden = o.id INNER JOIN solicitudes AS s ON o.idSolicitud = s.id  INNER JOIN " +
                        "tiposProductos AS tp ON p.idTipoProducto = tp.id WHERE(od.idOrden = @idOrden) AND (od.eliminado IS NULL)", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idOrden", idO));
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

        //metodo para el ListView de Dosificación
        public DataTable obtenerRemisiones(int idS)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT folioR, OD.eliminado, OD.idSucursal FROM solicitudes AS S JOIN ordenes AS O ON O.idSolicitud = S.id JOIN ordenDosificacion AS OD ON OD.idOrden = O.id WHERE folioR IS NOT NULL AND S.id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idS));
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

        //metodo para el ListView de Dosificación
        public DataTable obtenerRemisionByIdOD(int idOD)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    /*SELECT od.id, od.folio, su.razon, su.responsabilidadRem, su.calle, su.numero, su.interior, su.colonia, su.cp, c.nombre, c.telefono, " +
                    "c.celular, c.email, p.calle AS pCalle, p.numero AS pNumero, p.interior AS pInterior, p.colonia AS pColonia, p.cp AS pCP, p.nombre AS proyecto, prod.codigo, prod.descripcion, " +
                    "prod.unidad, ut.capacidad, ds.cantidad AS cantOrdenada, ds.revenimiento, ut.nombre AS nombreUnidadT, usuarios.nombre AS chofer, od.fecha, od.hora, od.cantidad, odb.motivo, " +
                    "odb.fecha AS fechaBit, ds.cantidadEntregada FROM ordenDosificacion AS od INNER JOIN ordenes AS o ON od.idOrden = o.id INNER JOIN solicitudes AS s ON o.idSolicitud = s.id " +
                    "INNER JOIN clientes AS c ON s.idCliente = c.id INNER JOIN proyectos AS p ON s.idProyecto = p.id INNER JOIN sucursales AS su ON od.idSucursal = su.id INNER JOIN " +
                    "detallesSolicitud AS ds ON od.idDetalleSolicitud = ds.id INNER JOIN productos AS prod ON ds.idProducto = prod.id INNER JOIN " +
                    "unidadesTransporte AS ut ON od.idUnidadTransporte = ut.id INNER JOIN " +
                    "unidadesTChoferes ON ut.id = unidadesTChoferes.idUnidad INNER JOIN usuarios ON unidadesTChoferes.idChofer = usuarios.id  INNER JOIN " +
                    "ordenDosificacionBitacora AS odb ON od.id = odb.idMaster WHERE(od.id = @id)*/
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT od.id, od.folio, su.razon, su.responsabilidadRem, su.calle, su.numero, su.interior, su.colonia, su.cp, c.nombre, c.telefono, " +
                        "c.celular, c.email, p.calle AS pCalle, p.numero AS pNumero, p.interior AS pInterior, p.colonia AS pColonia, p.cp AS pCP, p.nombre AS proyecto, prod.codigo, prod.descripcion, " +
                        "prod.unidad, ut.capacidad, ds.cantidad AS cantOrdenada, ds.revenimiento, ut.nombre AS nombreUnidadT, usuarios.nombre AS chofer, od.fecha, od.hora, od.cantidad, odb.motivo, " +
                        "odb.fecha AS fechaBit, ds.cantidadEntregada, v.nombre AS vendedor, od.folioR, o.comentarios FROM ordenDosificacion AS od INNER JOIN ordenes AS o ON od.idOrden = o.id INNER JOIN solicitudes AS s ON o.idSolicitud = s.id " +
                        "INNER JOIN clientes AS c ON s.idCliente = c.id INNER JOIN proyectos AS p ON s.idProyecto = p.id INNER JOIN sucursales AS su ON od.idSucursal = su.id INNER JOIN " +
                        "detallesSolicitud AS ds ON od.idDetalleSolicitud = ds.id INNER JOIN productos AS prod ON ds.idProducto = prod.id INNER JOIN " +
                        "unidadesTransporte AS ut ON od.idUnidadTransporte = ut.id INNER JOIN " +
                        "unidadesTChoferes ON ut.id = unidadesTChoferes.idUnidad INNER JOIN usuarios ON unidadesTChoferes.idChofer = usuarios.id  INNER JOIN " +
                        "ordenDosificacionBitacora AS odb ON od.id = odb.idMaster INNER JOIN usuarios AS v ON s.idVendedor = v.id WHERE(od.id = @id)", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idOD));
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
        //metodo para obtener el total de dosificado por Orden
        public float obtenerQtyByOrdenAndIdProducto(int idO, int idP)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT SUM(od.cantidad) AS Total " +
                        "FROM ordenDosificacion AS od INNER JOIN productos AS p ON od.idProducto = p.id INNER JOIN unidadesDeMedida AS udm ON od.idUDM = udm.id INNER JOIN " +
                         "ordenes AS o ON od.idOrden = o.id INNER JOIN solicitudes AS s ON o.idSolicitud = s.id INNER JOIN tiposProductos AS tp ON p.idTipoProducto = tp.id " +
                         "WHERE(od.idOrden = @idOrden) AND (p.id = @idP) AND (od.eliminado IS NULL)", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idOrden", idO));
                        cmd.Parameters.Add(new SqlParameter("@idP", idP));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    try
                                    {
                                        return float.Parse(reader["Total"].ToString());
                                    }
                                    catch (Exception)
                                    {
                                        return 0;
                                    }
                                }
                                return 0;
                            }
                        }
                        return 0;
                    }
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        //metodo para llenar el ListView hijo de Ordenes en el Dashboard de acuerdo al ID de la orden
        public DataTable obtenerODDFiltered(int idO, string fClaveCliente = "", int fIdOrden = 0, int opc = 1, bool finish = false, string fFechaI = "", string fFechaF = "", bool fProgramado = true)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    string selectComand = "od.id, od.idOrden, od.fecha, od.hora, p.codigo, p.descripcion, od.cantidad, udm.unidad, ds.revenimiento, tut.tipo, od.folio, " +
                       "pr.nombre, pr.calle, pr.numero, pr.interior, pr.colonia, pr.cp, cl.clave, cl.nombre AS cliente, od.idEstadoDosificacion, ed.estado, ed.porcentaje, ds.id AS idDS, ds.cantidadEntregada, " +
                       "ut.id AS idUTransporte, ut.nombre AS uTransporte, ut.color, ch.nombre AS chofer, od.colorBell, od.folioR ";
                    string fromComand = "ordenes AS o INNER JOIN solicitudes AS s ON o.idSolicitud = s.id INNER JOIN clientes AS cl ON s.idCliente = cl.id INNER JOIN " +
                         "ordenDosificacion AS od ON o.id = od.idOrden INNER JOIN productos AS p ON od.idProducto = p.id INNER JOIN unidadesDeMedida AS udm ON od.idUDM = udm.id LEFT OUTER JOIN " +
                         "tiposUnidadTransporte AS tut ON od.idTUT = tut.id INNER JOIN proyectos AS pr ON s.idProyecto = pr.id INNER JOIN estadosDosificacion AS ed ON od.idEstadoDosificacion = ed.id INNER JOIN " +
                         "detallesSolicitud AS ds ON od.idDetalleSolicitud = ds.id INNER JOIN unidadesTransporte AS ut ON od.idUnidadTransporte = ut.id INNER JOIN " +
                         "unidadesTChoferes AS utc ON od.idUnidadTransporte = utc.idUnidad INNER JOIN usuarios AS ch ON utc.idChofer = ch.id ";
                    string whereComand = "(od.eliminado IS NULL) AND (od.idOrden = @id) ";
                    whereComand += " AND s.idSucursal = " + idSucursal;
                    string orderByComand = "od.fecha ASC, od.hora ASC, od.idOrden, od.id ASC;";
                    if (!fClaveCliente.Equals(""))
                        whereComand += " AND cl.clave = '" + fClaveCliente + "'";
                    if (!finish)
                        whereComand += " AND od.idEstadoDosificacion < 11";
                    if (!fIdOrden.Equals(0))
                        whereComand += " AND o.id = " + fIdOrden;

                    switch (opc)
                    {
                        case 0:
                            whereComand += " AND od.fecha <= '" + fFechaI + "'";
                            break;
                        case 1:
                            whereComand += " AND od.fecha = '" + fFechaI + "'";
                            break;
                        case 2:
                            whereComand += " AND od.fecha = '" + fFechaI + "'";
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                        case 5:
                            whereComand += " AND od.fecha BETWEEN '" + fFechaI + "' AND '" + fFechaF + "'";
                            break;
                    }

                    using (SqlCommand cmd = new SqlCommand("SELECT " + selectComand + "FROM " + fromComand + "WHERE " + whereComand + " ORDER BY " + orderByComand, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idO));
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

        public DataTable obtenerODDFiltereD(int idO, int opc = 1, bool finish = false, string fFechaI = "", string fFechaF = "", bool fProgramado = true)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    string selectComand = "od.id, od.idOrden, FORMAT(od.fecha, 'dd/MM/yyyy') as fecha, od.hora, p.id AS idProducto, p.codigo, p.descripcion, tp.tipo AS tp, od.cantidad, udm.unidad, ds.revenimiento, tut.tipo, od.folio, " +
                       "pr.nombre, pr.calle, pr.numero, pr.interior, pr.colonia, pr.cp, cl.clave, cl.nombre AS cliente, od.idEstadoDosificacion, ed.estado, ed.porcentaje, ds.id AS idDS, ds.cantidadEntregada, " +
                       "ut.id AS idUTransporte, ut.nombre AS uTransporte, ut.color, ch.nombre AS chofer, od.colorBell ";
                    string fromComand = "ordenes AS o INNER JOIN solicitudes AS s ON o.idSolicitud = s.id INNER JOIN clientes AS cl ON s.idCliente = cl.id INNER JOIN " +
                         "ordenDosificacion AS od ON o.id = od.idOrden INNER JOIN productos AS p ON od.idProducto = p.id INNER JOIN unidadesDeMedida AS udm ON od.idUDM = udm.id LEFT OUTER JOIN " +
                         "tiposUnidadTransporte AS tut ON od.idTUT = tut.id INNER JOIN proyectos AS pr ON s.idProyecto = pr.id INNER JOIN estadosDosificacion AS ed ON od.idEstadoDosificacion = ed.id INNER JOIN " +
                         "detallesSolicitud AS ds ON od.idDetalleSolicitud = ds.id INNER JOIN unidadesTransporte AS ut ON od.idUnidadTransporte = ut.id INNER JOIN " +
                         "unidadesTChoferes AS utc ON od.idUnidadTransporte = utc.idUnidad INNER JOIN usuarios AS ch ON utc.idChofer = ch.id INNER JOIN tiposProductos AS tp ON p.idTipoProducto = tp.id ";
                    string whereComand = "(od.eliminado IS NULL) AND (od.idOrden = @id) ";
                    whereComand += " AND s.idSucursal = " + idSucursal;
                    string orderByComand = "od.fecha ASC, od.hora ASC, od.idOrden, od.id ASC;";
                    if (!finish)
                        whereComand += " AND od.idEstadoDosificacion < 11";

                    switch (opc)
                    {
                        case 0:
                            whereComand += " AND od.fecha <= '" + fFechaI + "'";
                            break;
                        case 1:
                            whereComand += " AND od.fecha = '" + fFechaI + "'";
                            break;
                        case 2:
                            whereComand += " AND od.fecha = '" + fFechaI + "'";
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                        case 5:
                            whereComand += " AND od.fecha BETWEEN '" + fFechaI + "' AND '" + fFechaF + "'";
                            break;
                    }

                    using (SqlCommand cmd = new SqlCommand("SELECT " + selectComand + "FROM " + fromComand + "WHERE " + whereComand + " ORDER BY " + orderByComand, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idO));
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

        public DataTable obtenerOPFiltro(string fClaveCliente = "", string fVendedor = "", string fCodigo = "", string fUnidad = "", string fChofer = "", int opc = 1, bool finish = false, string fFechaI = "", string fFechaF = "", bool fProgramado = true)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    string selectComand = "cl.clave, v.id AS idV, p.codigo, ut.nombre AS uNombre, ch.id AS idCh, od.id, od.idOrden, s.id AS idS, o.folio AS folioOrden, ds.id AS idDS,ch.id AS idCh, FORMAT(od.fecha, 'dd/MM/yyyy') AS fecha,od.hora, p.id AS idProducto, " +
                        "p.descripcion, ds.cantidad AS cTotal, od.cantidad, udm.unidad, ds.revenimiento, tut.tipo, od.folio, pr.nombre, pr.calle, pr.numero, pr.interior, pr.colonia, pr.cp, cl.nombre AS cliente," +
                        "od.idEstadoDosificacion, ed.estado, ed.porcentaje,ds.cantidadEntregada, ut.id AS idUTransporte, ut.nombre AS uTransporte, ut.color, ch.nombre AS chofer, od.colorBell, v.nombre AS vendedor, e.elemento ";
                    string fromComand = "ordenes AS o INNER JOIN " +
                         "solicitudes AS s ON o.idSolicitud = s.id INNER JOIN " +
                         "clientes AS cl ON s.idCliente = cl.id INNER JOIN " +
                         "ordenDosificacion AS od ON o.id = od.idOrden INNER JOIN " +
                         "productos AS p ON od.idProducto = p.id INNER JOIN " +
                         "unidadesDeMedida AS udm ON od.idUDM = udm.id INNER JOIN " +
                         "detallesSolicitud AS ds ON od.idDetalleSolicitud = ds.id LEFT OUTER JOIN " +
                         "tiposUnidadTransporte AS tut ON od.idTUT = tut.id INNER JOIN " +
                         "proyectos AS pr ON s.idProyecto = pr.id INNER JOIN " +
                         "estadosDosificacion AS ed ON od.idEstadoDosificacion = ed.id INNER JOIN " +
                         "unidadesTransporte AS ut ON od.idUnidadTransporte = ut.id INNER JOIN " +
                         "unidadesTChoferes AS utc ON od.idUnidadTransporte = utc.idUnidad INNER JOIN " +
                         "usuarios AS ch ON utc.idChofer = ch.id INNER JOIN " +
                         "usuarios AS v ON o.idVendedor = v.id INNER JOIN " +
                         "detallesSolicitud ON od.idDetalleSolicitud = detallesSolicitud.id LEFT JOIN " +
                         "elementos AS e ON detallesSolicitud.idElemento = e.id ";
                    string whereComand = "(od.eliminado IS NULL) ";
                    whereComand += " AND s.idSucursal = " + idSucursal;
                    if (!finish)
                        whereComand += " AND od.idEstadoDosificacion < 11";
                    string orderByComand = "od.fecha ASC, od.hora ASC, od.idOrden, od.id ASC;";
                    if (!fClaveCliente.Equals(""))
                        whereComand += " AND cl.clave = '" + fClaveCliente + "'";
                    if (!fVendedor.Equals(""))
                        whereComand += " AND v.id = '" + fVendedor + "'";
                    if (!fCodigo.Equals(""))
                        whereComand += " AND p.codigo = '" + fCodigo + "'";
                    if (!fUnidad.Equals(""))
                        whereComand += " AND ut.nombre = '" + fUnidad + "'";
                    if (!fChofer.Equals(""))
                        whereComand += " AND ch.id = '" + fChofer + "'";
                    //if (!fIdOrden.Equals(0))
                    //    whereComand += " AND o.id = " + fIdOrden;
                    switch (opc)
                    {
                        case 0:
                            whereComand += " AND od.fecha <= '" + fFechaI + "'";
                            //orderByComand = "od.fecha DESC, od.hora ASC, od.id ASC;";
                            break;
                        case 1:
                            whereComand += " AND od.fecha = '" + fFechaI + "'";
                            break;
                        case 2:
                            whereComand += " AND od.fecha = '" + fFechaI + "'";
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                        case 5:
                            whereComand += " AND od.fecha BETWEEN '" + fFechaI + "' AND '" + fFechaF + "'";
                            break;
                    }


                    using (SqlCommand cmd = new SqlCommand("SELECT " + selectComand + "FROM " + fromComand + "WHERE " + whereComand + " ORDER BY " + orderByComand, conn))
                    {
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

        public DataTable obtenerODFiltered(string fClaveCliente = "", string fVendedor = "", string fCodigo = "", string fUnidad = "", string fChofer = "", int opc = 1, bool finish = false, string fFechaI = "", string fFechaF = "", bool fProgramado = true)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    string selectComand = "od.id, od.folio, FORMAT(od.fecha, 'dd/MM/yyyy') as fecha, od.hora, pr.calle, pr.numero, pr.interior, pr.colonia, pr.cp, p.codigo, od.cantidad, udm.unidad, ds.revenimiento, " +
                        "tut.tipo, cl.id AS idCl, cl.clave, cl.nombre AS cliente, ed.estado, ed.porcentaje, ut.nombre AS uNombre, ch.id AS idCh, ch.nombre AS chofer, v.id AS idV, v.nombre AS vendedor, e.elemento, " +
                        "o.folio AS folioOrden, o.id AS idOrden, ds.cantidad AS cantOrdenada ";
                    string fromComand = "ordenes AS o INNER JOIN solicitudes AS s ON o.idSolicitud = s.id INNER JOIN clientes AS cl ON s.idCliente = cl.id INNER JOIN " +
                        "ordenDosificacion AS od ON o.id = od.idOrden INNER JOIN productos AS p ON od.idProducto = p.id INNER JOIN unidadesDeMedida AS udm ON od.idUDM = udm.id LEFT OUTER JOIN " +
                        "tiposUnidadTransporte AS tut ON od.idTUT = tut.id INNER JOIN proyectos AS pr ON s.idProyecto = pr.id INNER JOIN estadosDosificacion AS ed ON od.idEstadoDosificacion = ed.id INNER JOIN " +
                        "unidadesTransporte AS ut ON od.idUnidadTransporte = ut.id INNER JOIN unidadesTChoferes AS utc ON od.idUnidadTransporte = utc.idUnidad INNER JOIN " +
                        "usuarios AS ch ON utc.idChofer = ch.id INNER JOIN usuarios as v ON o.idVendedor = v.id INNER JOIN " +
                        "detallesSolicitud as ds ON od.idDetalleSolicitud = ds.id LEFT JOIN elementos AS e ON ds.idElemento = e.id ";
                    string whereComand = "(o.eliminado IS NULL) AND (od.eliminado IS NULL) ";
                    whereComand += " AND s.idSucursal = " + idSucursal;
                    string orderByComand = "od.fecha ASC, od.hora ASC, od.idOrden, od.id ASC;";
                    if (!fClaveCliente.Equals(""))
                        whereComand += " AND cl.clave = '" + fClaveCliente + "'";
                    if (!fVendedor.Equals(""))
                        whereComand += " AND v.id = '" + fVendedor + "'";
                    if (!fCodigo.Equals(""))
                        whereComand += " AND p.codigo = '" + fCodigo + "'";
                    if (!fUnidad.Equals(""))
                        whereComand += " AND ut.nombre = '" + fUnidad + "'";
                    if (!fChofer.Equals(""))
                        whereComand += " AND ch.id = '" + fChofer + "'";
                    if (!finish)
                        whereComand += " AND od.idEstadoDosificacion < 11";
                    switch (opc)
                    {
                        case 0:
                            whereComand += " AND od.fecha <= '" + fFechaI + "'";
                            break;
                        case 1:
                            whereComand += " AND od.fecha = '" + fFechaI + "'";
                            break;
                        case 2:
                            whereComand += " AND od.fecha = '" + fFechaI + "'";
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                        case 5:
                            whereComand += " AND od.fecha BETWEEN '" + fFechaI + "' AND '" + fFechaF + "'";
                            break;
                    }

                    using (SqlCommand cmd = new SqlCommand("SELECT " + selectComand + "FROM " + fromComand + "WHERE " + whereComand + " ORDER BY " + orderByComand, conn))
                    {
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
        //Metodo para quitar duplicados de obtenerODFiltered
        public DataTable obtenerODFilteredSinDuplicados(DataTable dtO)
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
                    idO = dtO.Rows[i]["idOrden"].ToString();
                    //if (i.Equals(0))
                    //    dtX.ImportRow(dtO.Rows[i]);
                    for (int j = 0; j < dtO.Rows.Count; j++)
                    {
                        try
                        {
                            if (dtO.Rows[j]["idOrden"].ToString().Equals(idO))
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


            //int idO = 0;
            //DataTable dt = new DataTable();
            //dt = dtO.Copy();
            //dt.Clear();
            //foreach (DataRow r in dt.Rows)
            //{
            //    r.Delete();
            //}
            //for (int i = 0; i < dtO.Rows.Count; i++)
            //{
            //    if (!i.Equals(0))
            //    {
            //        if (!int.Parse(dtO.Rows[i]["idOrden"].ToString()).Equals(idO))
            //        {
            //            idO = int.Parse(dtO.Rows[i]["idOrden"].ToString());
            //            dt.ImportRow(dtO.Rows[i]);
            //        }
            //    }
            //    else
            //    {
            //        idO = int.Parse(dtO.Rows[i]["idOrden"].ToString());
            //        dt.ImportRow(dtO.Rows[i]);
            //    }
            //}
            //return dt;
           
            //int idO = 0, aux = 0;
            //DataTable dtX = new DataTable();
            //dtX = dtO.Copy();
            //dtX.Clear();
            //foreach (DataRow r in dtX.Rows)
            //{
            //    r.Delete();
            //}
            //for (int i = 0; i < dtO.Rows.Count; i++)
            //{
            //    try
            //    {
            //        idO = int.Parse(dtO.Rows[i]["id"].ToString());
            //        //if (i.Equals(0))
            //        //    dtX.ImportRow(dtO.Rows[i]);
            //        for (int j = 0; j < dtO.Rows.Count; j++)
            //        {
            //            try
            //            {
            //                if (int.Parse(dtO.Rows[j]["id"].ToString()).Equals(idO))
            //                {
            //                    if (aux.Equals(0))
            //                        dtX.ImportRow(dtO.Rows[j]);
            //                    aux = 1;
            //                    dtO.Rows[j].Delete();
            //                }
            //            }
            //            catch (Exception)
            //            {

            //            }
            //        }
            //        aux = 0;
            //    }
            //    catch (Exception)
            //    {
            //    }
            //}
            //return dtX;
        }
        //Método para obtener el nombre del estado de dosificación
        public string getEstadoDosificacion(int idE)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT estado FROM estadosDosificacion WHERE(id = @id)", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idE));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    return reader["estado"].ToString();
                                }
                                return "";
                            }
                        }
                        return "";
                    }
                }
            }
            catch (Exception ex)
            {
                return "";
                throw (ex);
            }
        }
        //Método para obtener el id del estado de dosificación
        public int getidEDByIdOD(int idOD)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT idEstadoDosificacion FROM ordenDosificacion WHERE(id = @id)", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idOD));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    return int.Parse(reader["idEstadoDosificacion"].ToString());
                                }
                                return 1000;
                            }
                        }
                        return 1000;
                    }
                }
            }
            catch (Exception ex)
            {
                return 1000;
                throw (ex);
            }
        }

        public string getDiffTime(DateTime dTime, DataTable dt)
        {
            try
            {
                TimeSpan result = DateTime.Now.Subtract(dTime);
                if (dTime < DateTime.Now)
                    return "#FF0000";
                float min = Math.Abs(float.Parse(result.TotalMinutes.ToString()));
                string sColor = "#FFFFFF";
                foreach (DataRow dr in dt.Rows)
                {
                    int tiempo = int.Parse(dr["tiempo"].ToString());
                    if (min <= tiempo)
                        sColor = dr["color"].ToString();
                }
                return sColor;
            }
            catch (Exception)
            {
                return "#FF0000";
            }
        }
        public DataTable obtenerProduccionChofer(int idSuc, int tipoProducto, DateTime fechaI, DateTime fechaF, string param)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT utc.idChofer, ut.id AS idtrans,ordenes.fecha, solicitudes.idSucursal as planta, productos.idTipoProducto, od.idUDM, FORMAT(SUM(od.cantidad), 'N2') AS cantidad " +
                        " FROM unidadesTransporte AS ut, ordenes, solicitudes, detallesSolicitud AS ds, productos, ordenDosificacion AS od, unidadesTChoferes AS utc WHERE ordenes.idSolicitud = ds.idSolicitud AND solicitudes.id = ds.idSolicitud AND productos.id = ds.idProducto " +
                        " AND OD.idOrden = ordenes.id AND ut.id = od.idUnidadTransporte AND ds.id = od.idDetalleSolicitud AND utc.idUnidad = od.idUnidadTransporte AND utc.idSucursal = solicitudes.idSucursal AND utc.eliminada IS NULL " +
                        " AND (ordenes.fecha BETWEEN CONVERT(datetime, @fechaI, 103) AND CONVERT(datetime, @fechaF, 103)) AND solicitudes.idSucursal = @idSucursal AND productos.idTipoProducto = @tipoProducto " + param +
                        " GROUP BY utc.idChofer, ut.id, ordenes.fecha, solicitudes.idSucursal, productos.idTipoProducto, od.idUDM ORDER BY utc.idChofer ", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSuc));
                        cmd.Parameters.Add(new SqlParameter("@tipoProducto", tipoProducto));
                        cmd.Parameters.Add(new SqlParameter("@fechaI", fechaI));
                        cmd.Parameters.Add(new SqlParameter("@fechaF", fechaF));
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
        public DataTable obtenerTotalesProduccionChofer(int idSuc, int tipoProducto, DateTime fechaI, DateTime fechaF, string param)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT utc.idChofer, ut.id AS idtrans, count(od.id) AS viajes, FORMAT(SUM(od.cantidad), 'N2') AS cantidad " +
                        " FROM unidadesTransporte as ut, ordenes, solicitudes, detallesSolicitud AS ds, productos, ordenDosificacion AS od, unidadesTChoferes AS utc " +
                        " WHERE ordenes.idSolicitud = ds.idSolicitud AND solicitudes.id = ds.idSolicitud AND productos.id = ds.idProducto AND OD.idOrden = ordenes.id AND ut.id = od.idUnidadTransporte AND ds.id = od.idDetalleSolicitud AND utc.idUnidad = od.idUnidadTransporte " +
                        " AND utc.idSucursal = solicitudes.idSucursal AND utc.eliminada IS NULL " +
                        " AND (ordenes.fecha BETWEEN CONVERT(datetime, @fechaI, 103) AND CONVERT(datetime, @fechaF, 103)) AND solicitudes.idSucursal = @idSucursal AND productos.idTipoProducto = @tipoProducto " + param +
                        " GROUP BY utc.idChofer, ut.id ORDER BY utc.idChofer ", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSuc));
                        cmd.Parameters.Add(new SqlParameter("@tipoProducto", tipoProducto));
                        cmd.Parameters.Add(new SqlParameter("@fechaI", fechaI));
                        cmd.Parameters.Add(new SqlParameter("@fechaF", fechaF));
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
        public DataTable obtenerPromedioDiaProduccionChofer(int idSuc, int tipoProducto, DateTime fechaI, DateTime fechaF, string param)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("select utc.idChofer, ut.id as idtrans,FORMAT(SUM(od.cantidad), 'N2') as cantidad, count(od.id) as viajes from unidadesTransporte as ut, ordenes, solicitudes, detallesSolicitud as ds, productos, ordenDosificacion as od, unidadesTChoferes as utc " +
                        " WHERE ordenes.idSolicitud = ds.idSolicitud AND solicitudes.id = ds.idSolicitud AND productos.id = ds.idProducto AND OD.idOrden = ordenes.id and ut.id = od.idUnidadTransporte and ds.id = od.idDetalleSolicitud " +
                        " and utc.idUnidad = od.idUnidadTransporte and utc.idSucursal = solicitudes.idSucursal and utc.eliminada is null " +
                        " AND (ordenes.fecha BETWEEN CONVERT(datetime, @fechaI, 103) AND CONVERT(datetime, @fechaF, 103)) AND solicitudes.idSucursal = @idSucursal AND productos.idTipoProducto = @tipoProducto " + param +
                        " group by utc.idChofer, ut.id, solicitudes.idSucursal, productos.idTipoProducto, od.idUDM order by utc.idChofer", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSuc));
                        cmd.Parameters.Add(new SqlParameter("@tipoProducto", tipoProducto));
                        cmd.Parameters.Add(new SqlParameter("@fechaI", fechaI));
                        cmd.Parameters.Add(new SqlParameter("@fechaF", fechaF));
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
        public DataTable obtenerProduccionChoferByID(int idSuc, int tipoProducto, DateTime fechaI, DateTime fechaF, int idTrans)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("select utc.idChofer, ut.id as idtrans,ordenes.fecha, solicitudes.idSucursal, productos.idTipoProducto, od.idUDM, FORMAT(SUM(od.cantidad), 'N2') as cantidad from unidadesTransporte as ut, ordenes, solicitudes, detallesSolicitud as ds, productos, ordenDosificacion as od, unidadesTChoferes as utc " +
                        " WHERE ordenes.idSolicitud = ds.idSolicitud AND solicitudes.id = ds.idSolicitud AND productos.id = ds.idProducto AND OD.idOrden = ordenes.id and ut.id = od.idUnidadTransporte and ds.id = od.idDetalleSolicitud and utc.idUnidad = od.idUnidadTransporte " +
                        " and utc.idSucursal = solicitudes.idSucursal and utc.eliminada is null " +
                        " AND (ordenes.fecha BETWEEN CONVERT(datetime, @fechaI, 103) AND CONVERT(datetime, @fechaF, 103)) AND solicitudes.idSucursal = @idSucursal AND productos.idTipoProducto = @tipoProducto " +
                        " AND ut.id = @idTrans" +
                        " group by utc.idChofer, ut.id, ordenes.fecha, solicitudes.idSucursal, productos.idTipoProducto, od.idUDM order by utc.idChofer", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSuc));
                        cmd.Parameters.Add(new SqlParameter("@tipoProducto", tipoProducto));
                        cmd.Parameters.Add(new SqlParameter("@idTrans", idTrans));
                        cmd.Parameters.Add(new SqlParameter("@fechaI", fechaI));
                        cmd.Parameters.Add(new SqlParameter("@fechaF", fechaF));
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
        public DataTable obtenerProduccionTicket(int idSuc, DateTime fechaI, DateTime fechaF, string param)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT od.id, od.fecha, od.hora, od.idOrden, od.idUnidadTransporte, od.idSucursal, od.Cantidad, od.idUDM, s.idCliente, od.idUnidadTransporte, od.idProducto " +
                        " FROM ordenDosificacion as od, solicitudes as s, ordenes as o " +
                        " WHERE od.idOrden = o.id AND o.idSolicitud = s.id AND od.idSucursal = s.idSucursal AND (od.fecha BETWEEN CONVERT(datetime, @fechaI, 103) AND CONVERT(datetime, @fechaF, 103)) AND s.idSucursal = @idSucursal" + param, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSuc));
                        cmd.Parameters.Add(new SqlParameter("@fechaI", fechaI));
                        cmd.Parameters.Add(new SqlParameter("@fechaF", fechaF));
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
        public DataTable obtenerProduccionProyecto(int idSuc, DateTime fechaI, DateTime fechaF)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT od.id, od.fecha, s.idSucursal as planta, p.idCliente, p.id as Proyecto, FORMAT(SUM(od.cantidad), 'N2') as cantidad, od.idUDM " +
                        " FROM ordenDosificacion as od, solicitudes as s, detallesSolicitud as ds, proyectos as p, ordenes as o WHERE o.id = od.idOrden AND od.idSucursal = s.idSucursal AND od.idDetalleSolicitud = ds.id " +
                        " AND s.idProyecto = p.id AND s.idCliente = p.idCliente AND o.idSolicitud = s.id AND ds.idSolicitud = s.id AND o.idSolicitud = ds.idSolicitud " +
                        " AND (od.fecha BETWEEN CONVERT(datetime, @fechaI, 103) AND CONVERT(datetime, @fechaF, 103)) AND s.idSucursal = @idSucursal " +
                        " GROUP BY od.id, od.fecha, s.idSucursal, p.idCliente, p.id, od.idUDM", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSuc));
                        cmd.Parameters.Add(new SqlParameter("@fechaI", fechaI));
                        cmd.Parameters.Add(new SqlParameter("@fechaF", fechaF));
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
        public DataTable obtenerTicketsBySuc(int idSuc)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * from ordenDosificacion WHERE idSucursal = @idSucursal ", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSuc));
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
        public DataTable obtenerComparacionBacheos(int idSuc, DateTime fechaI, DateTime fechaF, string param)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT od.idOrden, od.id AS Ticket, od.idSucursal, od.idUnidadTransporte, s.idCliente, od.idProducto, od.hora, FORMAT(SUM(od.cantidad), 'N2') AS cantidad," +
                        " od.idUDM, FORMAT(SUM(od.cantidad), 'N2') AS Tar FROM ordenDosificacion AS od, solicitudes AS s, detallesSolicitud AS ds, ordenes AS o " +
                        " WHERE o.id = od.idOrden AND od.idSucursal = s.idSucursal AND od.idDetalleSolicitud = ds.id AND o.idSolicitud = s.id AND ds.idSolicitud = s.id AND o.idSolicitud = ds.idSolicitud " +
                        " AND (od.fecha BETWEEN CONVERT(datetime, @fechaI, 103) AND CONVERT(datetime, @fechaF, 103)) AND s.idSucursal = @idSucursal " + param +
                        " GROUP BY od.idOrden, od.id, od.idSucursal, od.idUnidadTransporte, s.idCliente, od.idProducto, od.hora, od.idUDM ORDER BY od.idOrden", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSuc));
                        cmd.Parameters.Add(new SqlParameter("@fechaI", fechaI));
                        cmd.Parameters.Add(new SqlParameter("@fechaF", fechaF));
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
        public DataTable obtenerProduccionVenta(int idSuc, DateTime fechaI, DateTime fechaF, string param)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT s.idCliente, s.idProyecto, od.idProducto, od.fecha, od.cantidad, od.idUDM FROM solicitudes AS s, detallesSolicitud AS ds, ordenes AS o, ordenDosificacion AS od, productos AS p, clientes AS c " +
                                                          " WHERE s.id = o.idSolicitud AND s.id = ds.idSolicitud AND o.idSolicitud = ds.idSolicitud AND od.idOrden = o.id AND od.idDetalleSolicitud = ds.id AND p.id = od.idProducto AND p.id = ds.idProducto AND c.id = s.idCliente AND c.eliminado IS NULL " +
                                                          " AND (od.fecha BETWEEN CONVERT(datetime, @fechaI, 103) AND CONVERT(datetime, @fechaF, 103)) AND s.idSucursal = @idSucursal and od.cantidad > 0 " + param +
                                                          " GROUP by s.idCliente, s.idProyecto, od.idProducto, od.fecha, od.cantidad, od.idUDM order by idCliente", conn))
                                                            {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSuc));
                        cmd.Parameters.Add(new SqlParameter("@fechaI", fechaI));
                        cmd.Parameters.Add(new SqlParameter("@fechaF", fechaF));
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
        public DataTable obtenerOrdenesBySuc(int idSuc)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT o.id FROM ordenes AS o, solicitudes AS s where o.idSolicitud = s.id AND idSucursal = @idSucursal", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSuc));
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
        public DataTable obtenerEstatusTicketC(int idSuc, DateTime fechaI, DateTime fechaF, string param)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT o.fecha, o.id, FORMAT(SUM(od.cantidad), 'N2') AS cantidad, s.idCliente from ordenes AS o, ordenDosificacion AS od, solicitudes AS s, clientes as c " +
                        " WHERE o.id = od.idOrden AND o.idSolicitud = s.id AND c.id = s.idCliente" +
                        " AND (od.fecha BETWEEN CONVERT(datetime, @fechaI, 103) AND CONVERT(datetime, @fechaF, 103)) AND s.idSucursal = @idSucursal AND c.eliminado IS NULL" + param +
                        " group by o.fecha, o.id, s.idCliente", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSuc));
                        cmd.Parameters.Add(new SqlParameter("@fechaI", fechaI));
                        cmd.Parameters.Add(new SqlParameter("@fechaF", fechaF));
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
        public DataTable obtenerODyUnidadByIDOrden(int idO)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT od.id, idUnidadTransporte FROM ordenDosificacion AS od, ordenDosificacionBitacora AS odb " +
                        " WHERE idMaster = od.id AND idOrden = @idOrden GROUP BY od.id, idUnidadTransporte", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idOrden", idO));
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
        public string obtenerImpresion(int idOD)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT TOP(1) od.fechaAlta from ordenDosificacion as od, ordenDosificacionBitacora as odb" +
                        " where idMaster = od.id and idMaster = @idOD", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idOD", idOD));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    return reader["fechaAlta"].ToString();
                                }
                                return "";
                            }
                        }
                        return "";
                    }
                }
            }
            catch (Exception ex)
            {
                return "";
                throw (ex);
            }
        }
        public string obtenerAprobado(int idOD)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("select TOP(1) motivo, odb.fecha from ordenDosificacion as od, ordenDosificacionBitacora as odb " +
                        " where idMaster = od.id and idMaster = @idOD and motivo like '%Se Actualizó campo Estado de dosificacion a = 2'", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idOD", idOD));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    return reader["fecha"].ToString();
                                }
                                return "";
                            }
                        }
                        return "";
                    }
                }
            }
            catch (Exception ex)
            {
                return "";
                throw (ex);
            }
        }
        public string obtenerDosificar(int idOD)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("select TOP(1) motivo, odb.fecha from ordenDosificacion as od, ordenDosificacionBitacora as odb " +
                        " where idMaster = od.id and idMaster = @idOD and motivo like '%Se Actualizó campo Estado de dosificacion a = 4'", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idOD", idOD));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    return reader["fecha"].ToString();
                                }
                                return "";
                            }
                        }
                        return "";
                    }
                }
            }
            catch (Exception ex)
            {
                return "";
                throw (ex);
            }
        }
        public string obtenerDosificando(int idOD)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("select TOP(1) motivo, odb.fecha from ordenDosificacion as od, ordenDosificacionBitacora as odb " +
                        " where idMaster = od.id and idMaster = @idOD and motivo like '%Se Actualizó campo Estado de dosificacion a = 5'", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idOD", idOD));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    return reader["fecha"].ToString();
                                }
                                return "";
                            }
                        }
                        return "";
                    }
                }
            }
            catch (Exception ex)
            {
                return "";
                throw (ex);
            }
        }
        public string obtenerDosificado(int idOD)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("select TOP(1) motivo, odb.fecha from ordenDosificacion as od, ordenDosificacionBitacora as odb " +
                        " where idMaster = od.id and idMaster = @idOD and motivo like '%Se Actualizó campo Estado de dosificacion a = 6'", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idOD", idOD));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    return reader["fecha"].ToString();
                                }
                                return "";
                            }
                        }
                        return "";
                    }
                }
            }
            catch (Exception ex)
            {
                return "";
                throw (ex);
            }
        }
        public string obtenerCamino(int idOD)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("select TOP(1) motivo, odb.fecha from ordenDosificacion as od, ordenDosificacionBitacora as odb " +
                        " where idMaster = od.id and idMaster = @idOD and motivo like '%Se Actualizó campo Estado de dosificacion a = 8'", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idOD", idOD));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    return reader["fecha"].ToString();
                                }
                                return "";
                            }
                        }
                        return "";
                    }
                }
            }
            catch (Exception ex)
            {
                return "";
                throw (ex);
            }
        }
        public string obtenerConCliente(int idOD)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("select TOP(1) motivo, odb.fecha from ordenDosificacion as od, ordenDosificacionBitacora as odb " +
                        " where idMaster = od.id and idMaster = @idOD and motivo like '%Se Actualizó campo Estado de dosificacion a = 9'", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idOD", idOD));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    return reader["fecha"].ToString();
                                }
                                return "";
                            }
                        }
                        return "";
                    }
                }
            }
            catch (Exception ex)
            {
                return "";
                throw (ex);
            }
        }
        public string obtenerRegreso(int idOD)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("select TOP(1) motivo, odb.fecha from ordenDosificacion as od, ordenDosificacionBitacora as odb " +
                        " where idMaster = od.id and idMaster = @idOD and motivo like '%Se Actualizó campo Estado de dosificacion a = 10'", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idOD", idOD));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    return reader["fecha"].ToString();
                                }
                                return "";
                            }
                        }
                        return "";
                    }
                }
            }
            catch (Exception ex)
            {
                return "";
                throw (ex);
            }
        }
        public DataTable obtenerProgramacionOrdenes(int idSuc, DateTime fechaI, DateTime fechaF)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT s.idSucursal, od.fecha, od.id as ordenD, o.id AS orden, s.idCliente, od.idProducto, od.fechaalta, od.hora, od.cantidad, od.idUnidadTransporte, ds.CantidadEntregada, idDetalleSolicitud" +
                        " FROM ordenes AS o, ordenDosificacion AS od, solicitudes AS s, detallesSolicitud AS ds, clientes AS c " +
                        " WHERE o.id = od.idOrden AND o.idSolicitud = s.id AND s.id = ds.idSolicitud AND od.idDetalleSolicitud = ds.id AND c.id = s.idCliente AND c.eliminado IS NULL " +
                        " AND (o.fecha BETWEEN CONVERT(datetime, @fechaI, 103) AND CONVERT(datetime, @fechaF, 103)) AND s.idSucursal = @idSucursal AND c.eliminado IS NULL " +
                        " GROUP BY s.idSucursal, od.fecha, o.id, od.id, s.idCliente, od.idProducto, od.fechaalta, od.hora, od.cantidad, od.idUnidadTransporte, ds.CantidadEntregada, idDetalleSolicitud ORDER BY od.fecha", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSuc));
                        cmd.Parameters.Add(new SqlParameter("@fechaI", fechaI));
                        cmd.Parameters.Add(new SqlParameter("@fechaF", fechaF));
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
        public float obtenerCantidadByOD(int idOD)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT cantidad FROM ordenDosificacion WHERE id = @idOD", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idOD", idOD));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            return float.Parse(dt.Rows[0][0].ToString());
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public DataTable obtenerDispatchPlan(int idSuc, DateTime fecha)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT idOrden, od.id, idUnidadTransporte, od.idProducto, o.idSolicitud, idCliente, od.cantidad " +
                        " FROM ordenDosificacion as od, ordenes as o, solicitudes as s, detallesSolicitud as ds " +
                        " WHERE idOrden = o.id AND o.idSolicitud = s.id AND ds.idSolicitud = s.id AND od.idDetalleSolicitud = ds.id " +
                        " AND od.fecha = CONVERT(datetime, @fecha, 103) AND s.idSucursal = @idSucursal " +
                        " GROUP BY idOrden, od.id, idUnidadTransporte, od.idProducto, o.idSolicitud, idCliente, od.cantidad", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSuc));
                        cmd.Parameters.Add(new SqlParameter("@fecha", fecha));
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

        public DataTable obtenerRemisionesGrupoViaje(int idOrden, int grupoViaje) {
            try {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    string command = @"SELECT od.id, tp.tipo as tp, od.idEstadoDosificacion, od.folioR, od.idOrden
                                                    FROM ordenDosificacion AS od
	                                                    LEFT JOIN ordenes AS o ON o.id = od.idOrden
	                                                    LEFT JOIN productos AS p ON p.id = od.idProducto
	                                                    LEFT JOIN tiposProductos AS tp ON tp.id = p.idTipoProducto
                                                    WHERE idOrden = @idOrden AND grupoViaje = @grupoViaje AND idEstadoDosificacion != 15";

                    using (SqlCommand cmd = new SqlCommand(command, conn)) {
                        cmd.Parameters.Add(new SqlParameter("@idOrden", idOrden));
                        cmd.Parameters.Add(new SqlParameter("@grupoViaje", grupoViaje));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }

            }
            catch (Exception ex) {
                throw (ex);
            }
        }

        public DataTable getProductoPDFGrupoViaje(int idOrden, int grupoViaje) {
            try {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    //NOTA: REVISAR SI EL CHOFER ACTIVO ES EL QUE SALDRÁ COMO CHOFER EN EL PDF DE LA REMISIÓN - ENRIQUE SANDOVAL 20/10/23
                    string command = @"SELECT od.id, p.codigo, od.idProducto, od.cantidad, ds.cantidad as cantOrdenada, ut.nombre AS nombreUnidadT, u.nombre AS chofer
                                                    FROM ordenDosificacion AS od
	                                                    LEFT JOIN productos AS p ON p.id = od.idProducto
	                                                    LEFT JOIN detallesSolicitud AS ds ON ds.id = od.idDetalleSolicitud
	                                                    INNER JOIN unidadesTransporte AS ut ON ut.id = od.idUnidadTransporte
	                                                    INNER JOIN unidadesTChoferes AS utc ON ut.id = utc.idUnidad
	                                                    INNER JOIN usuarios AS u ON u.id = utc.idChofer
                                                    WHERE idOrden = @idOrden AND grupoViaje = @grupoViaje AND idEstadoDosificacion != 15";//Agregar AND utc.activo = 1 cuando se valide el chofer

                    using (SqlCommand cmd = new SqlCommand(command, conn)) {
                        cmd.Parameters.Add(new SqlParameter("@idOrden", idOrden));
                        cmd.Parameters.Add(new SqlParameter("@grupoViaje", grupoViaje));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }

            }
            catch (Exception ex) {
                throw (ex);
            }
        }
    }
}