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
    public class cOrdenes
    {
        //Clases base referencia
        cBitacora cBit = new cBitacora();

        //variables
        private string cadena;

        //propiedades
        public int id { get; set; }
        public string folio { get; set; }
        public string folioSolicitud { get; set; }
        public int idSucursal { get; set; }
        public int idSolicitud { get; set; }
        public int idVendedor { get; set; }
        public DateTime fecha { get; set; }
        public string hora { get; set; }
        public string comentarios { get; set; }
        public string ubicacion { get; set; }
        public bool programada { get; set; }
        public string nombreC { get; set; }
        public string reqFac { get; set; }

        //Constructor
        public cOrdenes()
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
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO ordenes(folio, idSolicitud, idVendedor, fecha, hora, comentarios, ubicacion, programada, idUsuario, fechaAlta, reqFac) " +
                        "VALUES (@folio, @idSolicitud, @idVendedor, @fecha, @hora, @comentarios, @ubicacion, @programada, @idUsuario, GETDATE(), @reqFac)", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@folio", folio));
                        cmd.Parameters.Add(new SqlParameter("@idSolicitud", idSolicitud));
                        cmd.Parameters.Add(new SqlParameter("@idVendedor", idVendedor));
                        cmd.Parameters.Add(new SqlParameter("@fecha", fecha));
                        cmd.Parameters.Add(new SqlParameter("@hora", hora));
                        cmd.Parameters.Add(new SqlParameter("@comentarios", comentarios));
                        cmd.Parameters.Add(new SqlParameter("@ubicacion", ubicacion));
                        cmd.Parameters.Add(new SqlParameter("@programada", false));
                        cmd.Parameters.Add(new SqlParameter("@idUsuario", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@reqFac", reqFac));

                        int filas = cmd.ExecuteNonQuery();
                        string sTabla = "ordenes";
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
                    using (SqlCommand cmd = new SqlCommand("UPDATE ordenes SET idSolicitud = @idSolicitud, idVendedor = @idVendedor, fecha = @fecha, hora = @hora, comentarios = @comentarios, ubicacion = @ubicacion, idUsuarioMod = @idUsuarioMod, fechaMod = GETDATE(), reqFac = @reqFac WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSolicitud", idSolicitud));
                        cmd.Parameters.Add(new SqlParameter("@idVendedor", idVendedor));
                        cmd.Parameters.Add(new SqlParameter("@fecha", fecha));
                        cmd.Parameters.Add(new SqlParameter("@hora", hora));
                        cmd.Parameters.Add(new SqlParameter("@comentarios", comentarios));
                        cmd.Parameters.Add(new SqlParameter("@ubicacion", ubicacion));
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioMod", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@reqFac", reqFac));
                        cmd.Parameters.Add(new SqlParameter("@id", idO));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        cBit.insertar(idO, "Update", idUsuarioActivo, "ordenes");
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void actualizarReqFac(int idO, int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE ordenes SET reqFac = @reqFac, idUsuarioMod = @idUsuarioMod, fechaMod = GETDATE() WHERE id = @id", conn))
                    {
                        
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioMod", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@reqFac", reqFac));
                        cmd.Parameters.Add(new SqlParameter("@id", idO));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        cBit.insertar(idO, "Update", idUsuarioActivo, "ordenes");
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metodo para actualizar 
        public void actualizarOFH(int idOD, int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE ordenes SET idVendedor = @idVendedor, fecha = @fecha, hora = @hora, idUsuarioMod = @idUsuarioMod, fechaMod = GETDATE() WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idVendedor", idVendedor));
                        cmd.Parameters.Add(new SqlParameter("@fecha", fecha));
                        cmd.Parameters.Add(new SqlParameter("@hora", hora));
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioMod", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@id", idOD));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        //metodo para actualizar 
        public void actualizarOFHC(int idOD, int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE ordenes SET idVendedor = @idVendedor, fecha = @fecha, hora = @hora, comentarios = @comentarios, idUsuarioMod = @idUsuarioMod, fechaMod = GETDATE() WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idVendedor", idVendedor));
                        cmd.Parameters.Add(new SqlParameter("@fecha", fecha));
                        cmd.Parameters.Add(new SqlParameter("@hora", hora));
                        cmd.Parameters.Add(new SqlParameter("@comentarios", comentarios));
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioMod", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@id", idOD));

                        int filasAfectadas = cmd.ExecuteNonQuery();
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
                    using (SqlCommand cmd = new SqlCommand("UPDATE ordenes SET eliminado=1, fechaElimino=GETDATE(), idUsuarioElimino=@idUsuarioElimino WHERE id=@id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioElimino", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@id", idO));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        cBit.insertar(idO, "Delete", idUsuarioActivo, "ordenes");
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        //metodo para eliminar
        public void eliminarOdsByIdOrden(int idO, int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE ordenDosificacion SET eliminado=1, fechaElimino=GETDATE(), idUsuarioElimino=@idUsuarioElimino WHERE idOrden=@idOrden", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioElimino", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@idOrden", idO));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        //metodo para cambiar a Programada la orden
        public void setProgramada(int idO, bool val, int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE ordenes SET programada=@programada, fechaProgramacion=GETDATE(), idUsuarioProgramo=@idUsuarioProgramo WHERE id=@id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@programada", val));
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioProgramo", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@id", idO));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        cBit.insertar(idO, "Update", idUsuarioActivo, "ordenes", "Se cambió de estatus a programada, se generaron las órdenes de entrega con la totalidad de los detalles de la solicitud");
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metodo para el ListView de proyectos
        public DataTable obtenerOrdenes(string fIni, string fFin = "")
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    string selectComand = "o.id, o.fecha, o.hora, o.idSolicitud, o.folio, s.idVendedor AS idV1, v1.nombre AS nombreV1, o.idVendedor AS idV2, v2.nombre AS nombreV2, o.comentarios, " +
                        "o.ubicacion, cl.clave, cl.nombre AS cliente ";
                    string fromComand = "ordenes AS o INNER JOIN solicitudes AS s ON o.idSolicitud = s.id INNER JOIN usuarios AS v1 ON s.idVendedor = v1.id INNER JOIN " +
                         "usuarios AS v2 ON o.idVendedor = v2.id INNER JOIN clientes AS cl ON s.idCliente = cl.id ";
                    string whereComand = "(s.idSucursal = @idSucursal) AND (o.programada = @programada) AND (o.eliminado IS NULL) ";
                    string orderByComand = "o.fecha DESC;";

                    if (fFin.Equals(fIni))
                        whereComand += " AND (o.fecha = '" + fIni + "') ";
                    else
                        whereComand += " AND (o.fecha BETWEEN '" + fIni + "' AND '" + fFin + "') ";

                    using (SqlCommand cmd = new SqlCommand("SELECT " + selectComand + "FROM " + fromComand + "WHERE " + whereComand + " ORDER BY " + orderByComand, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
                        cmd.Parameters.Add(new SqlParameter("@programada", programada));
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
        //metodo para saber si la orden ya está programada
        public bool esProgramada(int idO)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT id FROM ordenes WHERE id = @id AND programada=1", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idO));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    return true;
                                }
                                return false;
                            }
                        }
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
                throw (ex);
            }
        }

        public bool esOrdenSinViajesDosificados(int idOrden)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT ISNULL(SUM(cantidad),0) AS qty FROM ordenDosificacion WHERE (idOrden=@idOrden) AND (eliminado IS NULL) AND (idEstadoDosificacion > 4) AND (idEstadoDosificacion < 12)", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idOrden", idOrden));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            if (float.Parse(dt.Rows[0][0].ToString()) > 0)
                                return true;
                            else
                                return false;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metodo para el ListView de proyectos
        public DataTable obtenerOrdenesProgramación()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT od.idOrden, od.fecha, od.hora, p.codigo, p.descripcion, od.cantidad, udm.unidad, od.revenimiento, tut.tipo, o.folio, " +
                        "pr.nombre, pr.calle, pr.numero, pr.interior, pr.colonia, pr.cp, cl.clave, cl.nombre AS cliente, od.idEstadoDosificacion, ed.estado, ds.cantidadEntregada " +
                        "FROM ordenes AS o INNER JOIN solicitudes AS s ON o.idSolicitud = s.id INNER JOIN clientes AS cl ON s.idCliente = cl.id INNER JOIN " +
                         "ordenDosificacion AS od ON o.id = od.idOrden INNER JOIN productos AS p ON od.idProducto = p.id INNER JOIN unidadesDeMedida AS udm ON od.idUDM = udm.id INNER JOIN " +
                         "tiposUnidadTransporte AS tut ON od.idTUT = tut.id INNER JOIN proyectos AS pr ON s.idProyecto = pr.id INNER JOIN estadosDosificacion AS ed ON od.idEstadoDosificacion = ed.id INNER JOIN " +
                         "detallesSolicitud AS ds ON od.idDetalleSolicitud = ds.id WHERE (od.eliminado IS NULL) ORDER BY od.idOrden ORDER BY o.fecha DESC", conn))
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
        public DataTable obtenerODSinDuplicados(DataTable dtO)
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
        }

        //metodo para el ListView de proyectos 
        public DataTable obtenerOrdenesProgramaciónFiltro(string fClaveCliente = "", int fIdOrden = 0, int opc = 1, bool finish = false, string fFechaI = "", string fFechaF = "", bool fProgramado = true)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    string selectComand = "od.idOrden, s.id AS idS, ds.id AS idDS, od.fecha, od.hora, p.id AS idP, p.codigo, p.descripcion, ds.cantidad AS cTotal, od.cantidad, udm.unidad, od.revenimiento, tut.id AS idTUT, tut.tipo, o.folio, " +
                        "pr.nombre, pr.calle, pr.numero, pr.interior, pr.colonia, pr.cp, cl.clave, cl.nombre AS cliente, od.idEstadoDosificacion, ed.estado, ds.cantidadEntregada ";
                    string fromComand = "ordenes AS o INNER JOIN solicitudes AS s ON o.idSolicitud = s.id INNER JOIN clientes AS cl ON s.idCliente = cl.id INNER JOIN " +
                         "ordenDosificacion AS od ON o.id = od.idOrden INNER JOIN productos AS p ON od.idProducto = p.id INNER JOIN unidadesDeMedida AS udm ON od.idUDM = udm.id LEFT OUTER JOIN " +
                         "tiposUnidadTransporte AS tut ON od.idTUT = tut.id INNER JOIN proyectos AS pr ON s.idProyecto = pr.id INNER JOIN estadosDosificacion AS ed ON od.idEstadoDosificacion = ed.id INNER JOIN " +
                         "detallesSolicitud AS ds ON od.idDetalleSolicitud = ds.id ";
                    string whereComand = "(od.eliminado IS NULL) ";
                    whereComand += " AND s.idSucursal = " + idSucursal;
                    if (!finish)
                        whereComand += " AND od.idEstadoDosificacion < 11";
                    string orderByComand = "od.fecha ASC, od.hora ASC, od.id ASC;";
                    if (!fClaveCliente.Equals(""))
                        whereComand += " AND cl.clave = '" + fClaveCliente + "'";
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
        //metodo para obtener la Orden por ID
        public void obtenerOrdenByID(int idO)
        {
            try
            {
                string comando = "SELECT o.folio, s.folio AS folioS, s.idSucursal, o.idVendedor AS vAprobo, o.id, o.idSolicitud, o.idVendedor, o.fecha, o.hora, o.comentarios, o.ubicacion, o.programada, " +
                    "o.fechaProgramacion, o.idUsuarioProgramo, o.idUsuario, o.fechaAlta, o.idUsuarioMod, o.fechaMod, o.eliminado, o.fechaElimino, o.idUsuarioElimino, c.nombre " +
                    "FROM ordenes AS o INNER JOIN solicitudes AS s ON o.idSolicitud = s.id INNER JOIN clientes AS c ON s.idCliente = c.id WHERE(o.id = @id)";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idO));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    folio = reader["folio"].ToString();
                                    folioSolicitud = reader["folioS"].ToString();
                                    idSucursal = int.Parse(reader["idSucursal"].ToString());
                                    idSolicitud = int.Parse(reader["idSolicitud"].ToString());
                                    idVendedor = int.Parse(reader["idVendedor"].ToString());
                                    fecha = DateTime.Parse(reader["fecha"].ToString());
                                    hora = reader["hora"].ToString();
                                    comentarios = reader["comentarios"].ToString();
                                    ubicacion = reader["ubicacion"].ToString();
                                    nombreC = reader["nombre"].ToString();
                                }
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

        public DataTable obtenerOrdenesComisiones(int idSuc, int idVend, DateTime fechaI, DateTime fechaF)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT ordenes.id, ordenes.fecha, nombre, clientes.id as idCliente, ordenes.idSolicitud " +
                        " FROM ordenes, solicitudes, clientes, ordenDosificacion as od, ordenDosificacionBitacora as odb " +
                        " WHERE ordenes.idSolicitud = solicitudes.id AND clientes.id = idCliente AND ordenes.id = od.idOrden AND od.id = odb.idMaster AND(motivo LIKE '%Se Actualizó campo Estado de dosificacion a = 7') " +
                        " AND solicitudes.idSucursal = @idSuc AND ordenes.idVendedor = @idVend AND(ordenes.fecha BETWEEN CONVERT(datetime, @fechaI, 103) AND CONVERT(datetime, @fechaF, 103)) " +
                        " GROUP BY ordenes.id, ordenes.fecha, nombre, clientes.id, ordenes.idSolicitud ORDER BY ordenes.id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSuc", idSuc));
                        cmd.Parameters.Add(new SqlParameter("@idVend", idVend));
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
        public DataTable obtenerTotalesOrdenes(int idSuc, int idVend, DateTime fechaI, DateTime fechaF)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT FORMAT(SUM(subtotal), 'N2') as subtotal, FORMAT(SUM(total),'N2') AS total, FORMAT(SUM(subtotal*.16), 'N2') as iva " +
                        " FROM detallesSolicitud, solicitudes, ordenes " +
                        " WHERE solicitudes.id = detallesSolicitud.idSolicitud AND ordenes.idSolicitud = detallesSolicitud.idSolicitud " +
                        " AND solicitudes.idSucursal = @idSuc AND ordenes.idVendedor = @idVend" +
                        " AND (ordenes.fecha BETWEEN CONVERT(datetime, @fechaI, 103) AND CONVERT(datetime, @fechaF, 103))", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSuc", idSuc));
                        cmd.Parameters.Add(new SqlParameter("@idVend", idVend));
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
        public string obtenerFechaOrdenByID(int idOrden)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT ordenes.fecha FROM ordenes WHERE ordenes.id = @idOrden", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idOrden", idOrden));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            return dt.Rows[0][0].ToString();
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public float obtenerCantidadTotalByOrden(int idOrden)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT SUM(cantidad) AS cantOrden FROM ordenDosificacion WHERE idOrden = @idOrden", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idOrden", idOrden));
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
        public string obtenerFechasDeEntregaODByIDOrden(int idOrden)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT TOP(1)odb.fecha FROM ordenes AS o, ordenDosificacion AS od, ordenDosificacionBitacora as odb" +
                        "  WHERE o.id = od.idOrden AND odb.idMaster = od.id and od.idOrden = @idOrden AND(motivo LIKE '%Se Actualizó campo Estado de dosificacion a = 7')", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idOrden", idOrden));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            return dt.Rows[0][0].ToString();
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public string ObtenerUbicacionByIDOrden(int idOrden)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT ubicacion FROM Ordenes WHERE id = @idOrden", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idOrden", idOrden));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            return dt.Rows[0][0].ToString();
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public int obtenerIdByIdSol()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT id FROM Ordenes WHERE idSolicitud = @idSolicitud AND (eliminado IS NULL)", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSolicitud", idSolicitud));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            return int.Parse(dt.Rows[0][0].ToString());
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                return 0;
                throw (ex);
            }
        }
    }
}