using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace despacho
{
    public class cSolicitudes
    {
        //Clases base referencia
        cBitacora cBit = new cBitacora();
        //variables
        private string cadena;

        //propiedades
        public int id { get; set; }
        public string folio { get; set; }
        public DateTime fecha { get; set; }
        public string hora { get; set; }
        public int anio { get; set; }
        public bool orden { get; set; }
        public int idCliente { get; set; }
        public int idVendedor { get; set; }
        public int idEstadoSolicitud { get; set; }
        public int idProyecto { get; set; }
        public int idFormaPago { get; set; }
        public int idProducto { get; set; }
        public int idSucursal { get; set; }
        public int idSolicitud { get; set; }
        public float monto { get; set; }
        public float saldo { get; set; }
        public float pago { get; set; }
        public float fechaPago { get; set; }
        public int cantidad { get; set; }
        public float precioU { get; set; }
        public float precioF { get; set; }
        public float total { get; set; }
        public float subtotal { get; set; }
        public string iva { get; set; }
        public string descripcion { get; set; }
        public string codigo { get; set; }
        public string unidad { get; set; }
        public string claveProdServ { get; set; }
        public string claveUnidad { get; set; }
        public string nombreCliente { get; set; }
        public string colonia { get; set; }
        public string cp { get; set; }
        public string numero { get; set; }
        public string estado { get; set; }
        public string ciudad { get; set; }
        public string clave { get; set; }
        public string calle { get; set; }
        public string interior { get; set; }
        public string celular { get; set; }
        public string reqFac { get; set; }

        //Constructor
        public cSolicitudes()
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
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO solicitudes(folio, fecha, hora, anio, orden, idCliente, idVendedor, idEstadoSolicitud, idProyecto, idFormaPago, idSucursal, idUsuario, fechaAlta, reqFac) VALUES(@folio, @fecha, @hora, @anio, @orden, @idCliente, @idVendedor, @idEstadoSolicitud, @idProyecto, @idFormaPago, @idSucursal, @idUsuario, GETDATE(), @reqFac)", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@folio", folio));
                        cmd.Parameters.Add(new SqlParameter("@fecha", fecha));
                        cmd.Parameters.Add(new SqlParameter("@hora", hora));
                        cmd.Parameters.Add(new SqlParameter("@anio", anio));
                        cmd.Parameters.Add(new SqlParameter("@orden", orden));
                        cmd.Parameters.Add(new SqlParameter("@idCliente", idCliente));
                        cmd.Parameters.Add(new SqlParameter("@idVendedor", idVendedor));
                        cmd.Parameters.Add(new SqlParameter("@idEstadoSolicitud", idEstadoSolicitud));
                        cmd.Parameters.Add(new SqlParameter("@idProyecto", idProyecto));
                        cmd.Parameters.Add(new SqlParameter("@idFormaPago", idFormaPago));
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
                        cmd.Parameters.Add(new SqlParameter("@idUsuario", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@reqFac", reqFac));

                        int filas = cmd.ExecuteNonQuery();

                       
                        string sTabla = "solicitudes";
                        cBit.insertar(cBit.getID(sTabla), "INSERT", idUsuarioActivo, sTabla);
                        throw new Exception("ok");
                    }
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        
       
        //metodo para actualizar 
        public void actualizar(int idUsuarioActivo, int idS)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE solicitudes SET fecha=@fecha, hora=@hora, idCliente=@idCliente, idVendedor=@idVendedor, idEstadoSolicitud=@idEstadoSolicitud, idProyecto=@idProyecto, idFormaPago=@idFormaPago, idSucursal=@idSucursal, idUsuarioMod = @idUsuarioMod, fechaMod = GETDATE(), reqFac = @reqFac WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@fecha", fecha));
                        cmd.Parameters.Add(new SqlParameter("@hora", hora));
                        cmd.Parameters.Add(new SqlParameter("@idCliente", idCliente));
                        cmd.Parameters.Add(new SqlParameter("@idVendedor", idVendedor));
                        cmd.Parameters.Add(new SqlParameter("@idEstadoSolicitud", idEstadoSolicitud));
                        cmd.Parameters.Add(new SqlParameter("@idProyecto", idProyecto));
                        cmd.Parameters.Add(new SqlParameter("@idFormaPago", idFormaPago));
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioMod", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@reqFac", reqFac));
                        cmd.Parameters.Add(new SqlParameter("@id", idS));

                        int filasAfectadas = cmd.ExecuteNonQuery();

                        cBit.insertar(idS, "Update", idUsuarioActivo, "solicitudes");
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metodo para actualizar 
        public void actualizarEstPago(int idUsuarioActivo, int idS, string estatus)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE solicitudes SET estatusPago=@estatus, idUsuarioMod = @idUsuarioMod, fechaMod = GETDATE() WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@estatus", estatus));
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioMod", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@id", idS));

                        int filasAfectadas = cmd.ExecuteNonQuery();

                        cBit.insertar(idS, "Update", idUsuarioActivo, "solicitudes", "sistema realiza modificación estatusPago");
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metodo para actualizar 
        public void actualizarDesdeDosificacion(int idUsuarioActivo, int idS)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE solicitudes SET idCliente=@idCliente, fecha=@fecha, hora=@hora, idVendedor=@idVendedor, idEstadoSolicitud=@idEstadoSolicitud, idProyecto=@idProyecto, " +
                        "idFormaPago=@idFormaPago, idUsuarioMod = @idUsuarioMod, fechaMod = GETDATE(), reqFac = @reqFac WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idCliente", idCliente));
                        cmd.Parameters.Add(new SqlParameter("@fecha", fecha));
                        cmd.Parameters.Add(new SqlParameter("@hora", hora));
                        cmd.Parameters.Add(new SqlParameter("@idVendedor", idVendedor));
                        cmd.Parameters.Add(new SqlParameter("@idEstadoSolicitud", idEstadoSolicitud));
                        cmd.Parameters.Add(new SqlParameter("@idProyecto", idProyecto));
                        cmd.Parameters.Add(new SqlParameter("@idFormaPago", idFormaPago));
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioMod", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@reqFac", reqFac));
                        cmd.Parameters.Add(new SqlParameter("@id", idS));

                        int filasAfectadas = cmd.ExecuteNonQuery();

                        cBit.insertar(idS, "Update", idUsuarioActivo, "solicitudes");
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void agregarIDFactura(int idS, int idF)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE solicitudes SET id_factura = @id_factura WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idS));
                        cmd.Parameters.Add(new SqlParameter("@id_factura", idF));

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
        public void eliminar(int idS, int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE solicitudes SET eliminada = 1, fechaElimino = GETDATE(), idUsuarioElimino = @idUsuario WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idUsuario", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@id", idS));

                        int filasAfectadas = cmd.ExecuteNonQuery();

                        cBit.insertar(idS, "Delete", idUsuarioActivo, "solicitudes");
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metodo para cambiar de solicitud a Orden
        public void cambiarAOrden(int idS, int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE solicitudes SET orden = 1, fechaMod = GETDATE(), idUsuarioMod = @idUsuario WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idUsuario", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@id", idS));

                        int filasAfectadas = cmd.ExecuteNonQuery();

                        cBit.insertar(idS, "Update", idUsuarioActivo, "solicitudes", "Cambio de Estatus: de Solicitud a Orden");
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metodo para cambiar de solicitud a Orden
        public void cambiarASolicitud(int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE solicitudes SET orden = 0, fechaMod = GETDATE(), idUsuarioMod = @idUsuario WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idUsuario", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@id", id));

                        int filasAfectadas = cmd.ExecuteNonQuery();

                        cBit.insertar(id, "Update", idUsuarioActivo, "solicitudes", "Cambio de Estatus: de Orden a Solicitud");
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metodo para Actualizar las observaciones de la solicitud
        public void cambiarObservaciones(int idS, int idUsuarioActivo, string sObs)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE solicitudes SET observaciones = @observaciones, fechaMod = GETDATE(), idUsuarioMod = @idUsuario WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@observaciones", sObs));
                        cmd.Parameters.Add(new SqlParameter("@idUsuario", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@id", idS));

                        int filasAfectadas = cmd.ExecuteNonQuery();

                        cBit.insertar(idS, "Update", idUsuarioActivo, "solicitudes", "Se actualizaron las observaciones de la Cotización");
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metodo para obtener el folio de la solicitud previo al alta
        public string generarFolio(string nomenclatura)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT id FROM solicitudes WHERE anio = @anio AND idSucursal=@idSucursal", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@anio", anio));
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            return nomenclatura + anio.ToString() + (dt.Rows.Count + 1);
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public string obtenerMontoSolicitud(int idSol)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT ISNULL(SUM(total),0) AS total FROM detallesSolicitud WHERE idSolicitud = @idSolicitud AND eliminado IS NULL", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSolicitud", idSol));
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
        public string obtenerMontoSubtotalSolicitud(int idSol)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT ISNULL(SUM(Subtotal),0) AS total FROM detallesSolicitud WHERE idSolicitud = @idSolicitud", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSolicitud", idSol));
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

        //metodo para obtener el nombre de la Sucursal por su ID
        public int obtenerAnioPrimerRegistro(int idSucursal)
        {
            try
            {
                string comando = "";
                if (!idSucursal.Equals(0))
                    comando = "SELECT min(anio) AS anio FROM solicitudes WHERE idSucursal = @idS";
                else
                    comando = "SELECT min(anio) AS anio FROM solicitudes";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        if (!idSucursal.Equals("0"))
                            cmd.Parameters.Add(new SqlParameter("@idS", idSucursal));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    return int.Parse(reader["anio"].ToString());
                                }
                            }
                        }
                    }
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public string obtenerSubtotalSolicitud(int idSol)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT SUM(subtotal) AS subtotal FROM detallesSolicitud WHERE idSolicitud = @idSolicitud", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSolicitud", idSol));
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

        public string obtenerPagadoSolicitud(int idSol)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT SUM(pago) AS pagado FROM pagosFinanzas WHERE idSolicitud = @idSolicitud AND estatus = 'Pagado'", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSolicitud", idSol));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            if (dt.Rows[0][0].ToString().Equals(0) || String.IsNullOrEmpty(dt.Rows[0][0].ToString()))
                                return "0";
                            else
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

        public string obtenerIVASolicitud(int idSol)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT iva FROM detallesSolicitud WHERE idSolicitud = @idSolicitud", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSolicitud", idSol));
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

        //metodo para obtener el folio de la solicitud previo al alta
        public DataTable obtenerSolicitudes(int idS)
        {
            try
            {
                DataTable dt = new DataTable();
                string comando = "SELECT s.id, s.folio, s.fecha, s.hora, cl.clave, cl.nombre AS nombreCliente, cl.email, p.nombre AS proyecto, v.nombre AS vendedor, fp.nombre AS fpago, s.orden AS tipo, es.estado, s.eliminada " +
                    "FROM solicitudes AS s INNER JOIN clientes AS cl ON s.idCliente = cl.id INNER JOIN proyectos AS p ON s.idProyecto = p.id INNER JOIN " +
                    "usuarios AS v ON s.idVendedor = v.id INNER JOIN formasPago AS fp ON s.idFormaPago = fp.id INNER JOIN estadosSolicitud AS es ON s.idEstadoSolicitud = es.id " +
                    "WHERE (cl.idSucursal = @idSucursal) AND (s.eliminada IS NULL) AND (s.orden = 0)";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idS));
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

        //metodo para obtener los datos de la solicitud para generar el PDF
        public DataTable obtenerDTByID(int idS)
        {
            try
            {
                DataTable dt = new DataTable();
                string comando = "SELECT s.folio, s.fecha, s.hora, cl.clave, cl.nombre AS cliente, cl.cp, cl.calle, cl.numero, cl.interior, cl.colonia, cl.estado, cl.ciudad, cl.email, " +
                    "cl.telefono, cl.celular, p.nombre AS proyecto, p.calle AS calleP, p.numero AS numP, p.interior AS intP, p.colonia AS colP, p.cp AS cpP, fp.nombre AS formaPago, " +
                    "es.estado AS estadoS, suc.nombre AS sucursal, suc.razon, suc.observacionesCot, suc.cp AS sCP, suc.colonia AS sColonia, suc.calle AS sCalle, suc.numero AS sNumero, suc.interior AS sInterior, " +
					"v.nombre AS vendedor, v.telefono AS vTelefono, v.email AS vEmail FROM solicitudes AS s INNER JOIN clientes AS cl ON s.idCliente = cl.id INNER JOIN proyectos AS p " +
                    "ON s.idProyecto = p.id INNER JOIN usuarios AS v ON s.idVendedor = v.id INNER JOIN formasPago AS fp ON s.idFormaPago = fp.id INNER JOIN estadosSolicitud AS es " +
                    "ON s.idEstadoSolicitud = es.id INNER JOIN sucursales AS suc ON s.idSucursal = suc.id WHERE(s.id = @id)";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
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

        //metodo para obtener el Perfil por ID
        public void obtenerSolicitudByID(int idSol)
        {
            try
            {
                string comando = "SELECT s.id, s.folio, s.fecha, s.hora, s.anio, s.orden, s.idCliente, s.idVendedor, s.idEstadoSolicitud, s.idProyecto, s.idFormaPago, s.idSucursal, s.idUsuario, " +
                    "s.fechaAlta, s.idUsuarioMod, s.fechaMod, s.eliminada, s.fechaElimino, s.idUsuarioElimino, s.id_factura, c.nombre, " +
                    "c.colonia, c.cp, c.calle, c.numero, c.clave, c.estado, c.ciudad, c.interior, c.celular, s.reqFac "+
                    "FROM solicitudes AS s INNER JOIN clientes AS c ON s.idCliente = c.id WHERE(s.id = @id)";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idSol));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    folio = reader["folio"].ToString();
                                    fecha = DateTime.Parse(reader["fecha"].ToString());
                                    hora = reader["hora"].ToString();
                                    nombreCliente = reader["nombre"].ToString();
                                    colonia = reader["colonia"].ToString();
                                    cp = reader["cp"].ToString();
                                    numero = reader["numero"].ToString();
                                    clave = reader["clave"].ToString();
                                    estado = reader["estado"].ToString();
                                    ciudad = reader["ciudad"].ToString();
                                    calle = reader["calle"].ToString();
                                    celular = reader["celular"].ToString();
                                    interior = reader["interior"].ToString();
                                    orden = bool.Parse(reader["orden"].ToString());
                                    idCliente = int.Parse(reader["idCliente"].ToString());
                                    idVendedor = int.Parse(reader["idVendedor"].ToString());
                                    idEstadoSolicitud = int.Parse(reader["idEstadoSolicitud"].ToString());
                                    idProyecto = int.Parse(reader["idProyecto"].ToString());
                                    idFormaPago = int.Parse(reader["idFormaPago"].ToString());
                                    idSucursal = int.Parse(reader["idSucursal"].ToString());
                                    reqFac = (reader["reqFac"].ToString());
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

        public DataTable obtenerDetallesSolicitud(int idSol)
        {
            try
            {
                DataTable dt = new DataTable();
                string comando = "SELECT cantidad, precioU, precioF, subtotal, total, ds.iva, productos.descripcion as prodDesc, " +
                    " codigo, udm.descripcion as udmDesc, codigoSAT, unidadSAT FROM productos, detallesSolicitud as ds, unidadesDeMedida as udm " +
                    " WHERE ds.idproducto = productos.id AND udm.id = productos.idUDM AND idSolicitud = @idSol";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSol", idSol));
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
        public int obtenerCantidadDetallesSolicitud(int idSol)
        {
            try
            {
                string comando = "SELECT COUNT(id) as cantidad FROM detallesSolicitud WHERE idSolicitud = @idSol";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSol", idSol));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    return int.Parse(reader["cantidad"].ToString());
                                }
                            }
                        }
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                return 0;
                throw (ex);
            }
        }
        public bool existeSolicitud()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT id FROM solicitudes WHERE folio = @folio", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@folio", folio));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
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

        //metodo para obtener el idSolicitud por su Folio
        public int obtenerIDSolicitudByFolio(string sFolio)
        {
            try
            {
                string comando = "SELECT id FROM solicitudes WHERE folio = @folio AND idSucursal = @idSucursal";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@folio", sFolio));
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    return int.Parse(reader["id"].ToString());
                                }
                            }
                        }
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                return 0;
                throw (ex);
            }
        }
        public string obtenerFolioSolicitudByID(string idSolicitud)
        {
            try
            {
                string comando = "SELECT Folio FROM solicitudes WHERE id = @idSolicitud";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSolicitud", idSolicitud));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    return (reader["Folio"].ToString());
                                }
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

        //metodo para mostrar las solicitudes en cobranza finanzas 26-03-2021
    public DataTable mostrarSolicitudesFExcel(int idS, string param)
  {
      try
      {
          DataTable dt = new DataTable();
          /*string comando = "SELECT s.idCliente, s.id, s.folio, s.fecha, s.hora, cl.clave, cl.nombre AS nombreCliente, p.nombre AS proyecto, v.nombre AS vendedor, fp.nombre AS fpago, s.orden AS tipo, es.estado, s.eliminada, s.id_factura " +
              "FROM solicitudes AS s INNER JOIN clientes AS cl ON s.idCliente = cl.id INNER JOIN proyectos AS p ON s.idProyecto = p.id INNER JOIN " +
              "usuarios AS v ON s.idVendedor = v.id INNER JOIN formasPago AS fp ON s.idFormaPago = fp.id INNER JOIN estadosSolicitud AS es ON s.idEstadoSolicitud = es.id " +
              "WHERE (cl.idSucursal = @idSucursal) AND (s.eliminada IS NULL) "+param;*/
          string comando = @"SELECT 
                  ( 
                      SELECT STUFF(( 
                          SELECT ', ' + CONVERT(varchar(max), folio) 
                          FROM pagosFinanzas 
                          WHERE idSolicitud = s.id AND estatus = 'Pagado' 
                          ORDER BY id DESC 
                          FOR XML PATH(''), TYPE 
                      ).value('.', 'NVARCHAR(MAX)'), 1, 2, '') 
                  ) AS 'Folios de Pago', 
                  s.folio AS 'Folio Solicitud',  CONVERT(varchar, o.fecha, 103) AS fecha, s.hora, cl.clave AS 'Clave del Cliente', 
                  cl.nombre AS 'Nombre del Cliente', v.nombre AS vendedor, 
                  fp.nombre AS 'Forma de pago', es.estado,o.folio AS folioOrden,
                  (CONVERT(varchar(10), ds.cantidadEntregada) + ' de ' + CONVERT(varchar(10), ds.cantidad)) AS 'Cant. Entregada', 
                  ds.cantidadEntregada as despachado, ds.cantidad as programado, 
                  ds.total AS Monto, 
                  (ISNULL(SUM(ds.total),0) - ISNULL(SUM(pf.pago), 0)) as Saldo, 
                  CAST(pr.descripcion AS varchar(max)) AS 'Producto', 
                  CASE 
                      WHEN ds.cantidadEntregada <> 0 THEN (ISNULL(SUM(ds.total),0) / ds.cantidadEntregada) 
                      ELSE 0 
                  END AS 'Precio Promedio' 
              FROM solicitudes AS s 
              INNER JOIN clientes AS cl ON s.idCliente = cl.id 
              INNER JOIN proyectos AS p ON s.idProyecto = p.id 
              INNER JOIN usuarios AS v ON s.idVendedor = v.id 
              INNER JOIN formasPago AS fp ON s.idFormaPago = fp.id 
              LEFT JOIN ordenes AS o ON o.idSolicitud = s.id 
              LEFT JOIN ordenDosificacion AS od ON od.idOrden = o.id 
              INNER JOIN estadosSolicitud AS es ON s.idEstadoSolicitud = es.id 
              JOIN detallesSolicitud AS ds ON ds.idSolicitud = s.id 
              LEFT JOIN pagosFinanzas AS pf ON pf.idSolicitud = s.id 
              INNER JOIN productos AS pr ON pr.id = ds.idProducto 
             WHERE (cl.idSucursal = @idSucursal) AND (s.eliminada IS NULL) AND (od.cantidad > 0 AND od.eliminado IS NULL)" + param + @"  
              GROUP BY  ds.total, s.folio,o.folio, o.fecha, s.hora, cl.clave, cl.nombre, v.nombre, fp.nombre, es.estado, o.folio, 
              ds.cantidadEntregada, ds.cantidad, s.id, CAST(pr.descripcion AS varchar(max))";
          using (SqlConnection conn = new SqlConnection(cadena))
          {
              conn.Open();
              using (SqlCommand cmd = new SqlCommand(comando, conn))
              {
                  cmd.Parameters.Add(new SqlParameter("@idSucursal", idS));
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



        //metodo para mostrar las solicitudes en cobranza finanzas 26-03-2021
        public DataTable mostrarSolicitudesF(int idS, string param)
        {
            try
            {
                DataTable dt = new DataTable();
                /*string comando = "SELECT s.idCliente, s.id, s.folio, s.fecha, s.hora, cl.clave, cl.nombre AS nombreCliente, p.nombre AS proyecto, v.nombre AS vendedor, fp.nombre AS fpago, s.orden AS tipo, es.estado, s.eliminada, s.id_factura " +
                    "FROM solicitudes AS s INNER JOIN clientes AS cl ON s.idCliente = cl.id INNER JOIN proyectos AS p ON s.idProyecto = p.id INNER JOIN " +
                    "usuarios AS v ON s.idVendedor = v.id INNER JOIN formasPago AS fp ON s.idFormaPago = fp.id INNER JOIN estadosSolicitud AS es ON s.idEstadoSolicitud = es.id " +
                    "WHERE (cl.idSucursal = @idSucursal) AND (s.eliminada IS NULL) "+param;*/
                string comando = "SELECT DISTINCT s.idCliente, s.id, o.id AS idO, ds.id AS idDS, s.folio, o.fecha, s.hora, cl.clave, cl.nombre AS nombreCliente, p.nombre AS proyecto, v.nombre AS vendedor, " +
                    "fp.nombre AS fpago, s.orden AS tipo, es.estado, s.eliminada, s.id_factura, o.folio AS folioOrden, CONVERT(varchar, o.fecha, 103) AS fechao, ds.cantidad AS cTotal, " +
                    "ds.cantidadEntregada, s.estatusPago, ISNULL(od.eliminado, 0) AS eliminadod " +
                    " FROM solicitudes AS s INNER JOIN clientes AS cl ON s.idCliente = cl.id INNER JOIN proyectos AS p ON s.idProyecto = p.id INNER JOIN" +
                    " usuarios AS v ON s.idVendedor = v.id INNER JOIN formasPago AS fp ON s.idFormaPago = fp.id " +
                    " LEFT JOIN ordenes AS o ON o.idSolicitud = s.id LEFT JOIN ordenDosificacion AS od ON od.idOrden = o.id" +
                    " INNER JOIN estadosSolicitud AS es ON s.idEstadoSolicitud = es.id JOIN detallesSolicitud AS ds ON ds.idSolicitud = s.id " +
                    " WHERE (cl.idSucursal = @idSucursal) AND (s.eliminada IS NULL) AND (od.cantidad > 0 AND od.eliminado IS NULL) " + param;
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idS));
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

        //metodo para mostrar las solicitudes en cobranza vendedores 26-03-2021
        public DataTable mostrarSolicitudesV(int idS, int idvendedor)
        {
            try
            {
                DataTable dt = new DataTable();
                string comando = "SELECT s.id, s.folio, s.fecha, s.hora, cl.clave, s.idCliente, cl.nombre AS nombreCliente, p.nombre AS proyecto, v.nombre AS vendedor, fp.nombre AS fpago, s.orden AS tipo, es.estado, s.eliminada " +
                    "FROM solicitudes AS s INNER JOIN clientes AS cl ON s.idCliente = cl.id INNER JOIN proyectos AS p ON s.idProyecto = p.id INNER JOIN " +
                    "usuarios AS v ON s.idVendedor = v.id INNER JOIN formasPago AS fp ON s.idFormaPago = fp.id INNER JOIN estadosSolicitud AS es ON s.idEstadoSolicitud = es.id " +
                    "WHERE (cl.idSucursal = @idSucursal) AND (s.eliminada IS NULL) AND (s.idVendedor = @idVendedor)";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idS));
                        cmd.Parameters.Add(new SqlParameter("@idVendedor", idvendedor));
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
        public bool existePagoSolicitud(int idSolicitud)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT pago FROM pagosFinanzas WHERE idSolicitud = @idSolicitud", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSolicitud", idSolicitud));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
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


        //función que obtiene la suma de los totalContpaq de las ordenesDosificación para una orden
        public string obtenerMontoOrdenDosificacionContpaq(int idOrden)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT ISNULL(SUM(totalContpaq), 0) AS total FROM ordenDosificacion WHERE idOrden = @idOrden", conn))
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
                throw ex;
            }
        }



    }

}