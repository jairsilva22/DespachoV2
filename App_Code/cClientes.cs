using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace despacho
{
    public class cClientes
    {
        //Clases base referencia
        cSucursales cSuc = new cSucursales();

        //variables
        private string cadena;

        //propiedades
        public int id { get; set; }
        public string clave { get; set; }
        public string nombre { get; set; }
        public string calle { get; set; }
        public int numero { get; set; }
        public string interior { get; set; }
        public string colonia { get; set; }
        public int cp { get; set; }
        public string estado { get; set; }
        public int idEstado { get; set; }
        public string ciudad { get; set; }
        public int idCiudad { get; set; }
        public string email { get; set; }
        public string telefono { get; set; }
        public string rfc { get; set; }
        public string nombreEmpresa { get; set; }
        public string celular { get; set; }
        public int idVendedor { get; set; }
        public int idFormaPago { get; set; }
        public int idSucursal { get; set; }
        public int iva { get; set; }
        public bool directo { get; set; }
        public int mod { get; set; }
        public int select { get; set; }
        public string fp { get; set; }

        //Constructor
        public cClientes()
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
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO clientes(clave, nombre, cp, calle, numero, interior, colonia, estado, idEstado, ciudad, idCiudad, email, telefono, celular, idVendedor, idFormaPago, idSucursal, directo, idUsuario, fechaAlta) "+
                        "VALUES(@clave, @nombre, @cp, @calle, @numero, @interior, @colonia, @estado, @idEstado, @ciudad, @idCiudad, @email, @telefono, @celular, @idVendedor, @idFormaPago, @idSucursal, @directo, @idUsuario, GETDATE())", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@clave", clave));
                        cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                        cmd.Parameters.Add(new SqlParameter("@cp", cp));
                        cmd.Parameters.Add(new SqlParameter("@calle", calle));
                        cmd.Parameters.Add(new SqlParameter("@numero", numero));
                        cmd.Parameters.Add(new SqlParameter("@interior", interior));
                        cmd.Parameters.Add(new SqlParameter("@colonia", colonia));
                        cmd.Parameters.Add(new SqlParameter("@estado", estado));
                        cmd.Parameters.Add(new SqlParameter("@idEstado", idEstado));
                        cmd.Parameters.Add(new SqlParameter("@ciudad", ciudad));
                        cmd.Parameters.Add(new SqlParameter("@idCiudad", idCiudad));
                        cmd.Parameters.Add(new SqlParameter("@email", email));
                        cmd.Parameters.Add(new SqlParameter("@telefono", telefono));
                        cmd.Parameters.Add(new SqlParameter("@celular", celular));
                        cmd.Parameters.Add(new SqlParameter("@idVendedor", idVendedor));
                        cmd.Parameters.Add(new SqlParameter("@idFormaPago", idFormaPago));
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
                        cmd.Parameters.Add(new SqlParameter("@directo", directo));
                        cmd.Parameters.Add(new SqlParameter("@idUsuario", idUsuarioActivo));

                        int filas = cmd.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metodo para actualizar 
        public void actualizar(int idC, int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE clientes SET nombre = @nombre, cp = @cp, calle = @calle, numero = @numero, interior = @interior, "+
                        "colonia = @colonia, estado = @estado, idEstado = @idEstado, ciudad = @ciudad, idCiudad = @idCiudad, " +
                        "email = @email, telefono = @telefono, celular = @celular, idVendedor = @idVendedor, idFormaPago = @idFormaPago, directo = @directo, " +
                        "idUsuarioMod = @idUsuarioMod, fechaMod = GETDATE() WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                        cmd.Parameters.Add(new SqlParameter("@cp", cp));
                        cmd.Parameters.Add(new SqlParameter("@calle", calle));
                        cmd.Parameters.Add(new SqlParameter("@numero", numero));
                        cmd.Parameters.Add(new SqlParameter("@interior", interior));
                        cmd.Parameters.Add(new SqlParameter("@colonia", colonia));
                        cmd.Parameters.Add(new SqlParameter("@estado", estado));
                        cmd.Parameters.Add(new SqlParameter("@idEstado", idEstado));
                        cmd.Parameters.Add(new SqlParameter("@ciudad", ciudad));
                        cmd.Parameters.Add(new SqlParameter("@idCiudad", idCiudad));
                        cmd.Parameters.Add(new SqlParameter("@email", email));
                        cmd.Parameters.Add(new SqlParameter("@telefono", telefono));
                        cmd.Parameters.Add(new SqlParameter("@celular", celular));
                        cmd.Parameters.Add(new SqlParameter("@idVendedor", idVendedor));
                        cmd.Parameters.Add(new SqlParameter("@idFormaPago", idFormaPago));
                        cmd.Parameters.Add(new SqlParameter("@directo", directo));
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioMod", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@id", idC));

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
        public void eliminar(int idC, int idUsuario)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE clientes SET eliminado = 1, idUsuarioElimino = @idUsuarioElimino, fechaEliminado = GETDATE() WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioElimino", idUsuario));
                        cmd.Parameters.Add(new SqlParameter("@id", idC));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        //metodo para obtener el folio de la solicitud previo al alta
        public string generarClave()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT s.codigo FROM clientes AS c INNER JOIN sucursales AS s ON c.idSucursal = s.id WHERE(c.idSucursal = @idSucursal)", conn))
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

        //metodo para obtener el id de perfil en el alta de Perfiles
        public DataTable obtenerNombreCliente(string name)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT id, nombre FROM clientes WHERE nombre LIKE '%" + name + "%' ORDER BY nombre", conn))
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
        public string obtenerNombreClienteByID(int idCliente)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT nombre FROM clientes WHERE id = @idCliente", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idCliente", idCliente));
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
        public DataTable obtenerDireccionClienteByID(int idCliente)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT cp, calle, numero, interior, colonia FROM clientes WHERE id = @idCliente", conn))
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
        //metodo para obtener id y nombre de los clientes para Solicitud
        public DataTable obtenerClientes()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT id, nombre FROM clientes  ORDER BY id ASC", conn))
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

        public DataTable buscarClientes()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT clientes.id, clientes.nombre, clave, formasPago.nombre AS formaPago FROM clientes LEFT JOIN formasPago ON formasPago.id = clientes.idFormaPago WHERE clientes.idSucursal = @idSucursal ORDER BY id ASC", conn))
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

        //metodo para obtener id y nombre de los clientes para Solicitud
        public DataTable obtenerClientesCF()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT c.id, c.nombre FROM clientes AS c JOIN solicitudes AS s ON s.idCliente = c.id WHERE c.idSucursal = @id ORDER BY c.nombre", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idSucursal));
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

        public DataTable obtenerClientesFacturacion()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT idCliente, nombreCliente FROM clientesFacturacion", conn))
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

        //metodo para obtener datos de los clientes para Modificarlos
        public void obtenerClienteByID(int idC)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM clientes WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idC));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    clave = reader["clave"].ToString();
                                    nombre = reader["nombre"].ToString();
                                    if (reader["cp"].ToString() != "") {
                                        cp = int.Parse(reader["cp"].ToString());
                                    }                                    
                                    calle = reader["calle"].ToString();
                                    if (reader["numero"].ToString() != "") {
                                        numero = int.Parse(reader["numero"].ToString());
                                    }                                    
                                    interior = reader["interior"].ToString();
                                    colonia = reader["colonia"].ToString();
                                    estado = reader["estado"].ToString();
                                    if (reader["idEstado"].ToString()!= "") {
                                        idEstado = int.Parse(reader["idEstado"].ToString());
                                    }                                    
                                    ciudad = reader["ciudad"].ToString();
                                    if (reader["idCiudad"].ToString()!= "") {
                                        idCiudad = int.Parse(reader["idCiudad"].ToString());
                                    }
                                    email = reader["email"].ToString();
                                    telefono = reader["telefono"].ToString();
                                    celular = reader["celular"].ToString();
                                    if (reader["idVendedor"].ToString()!= "") {
                                        idVendedor = int.Parse(reader["idVendedor"].ToString());
                                    }
                                    if (reader["idFormaPago"].ToString() != "") {
                                        idFormaPago = int.Parse(reader["idFormaPago"].ToString());
                                    }
                                    if (reader["idSucursal"].ToString() != "") {
                                        idSucursal = int.Parse(reader["idSucursal"].ToString());
                                    }
                                    
                                    try
                                    {
                                        directo = bool.Parse(reader["directo"].ToString());
                                    }
                                    catch (Exception)
                                    {
                                        directo = false;
                                    }
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
        //metodo para obtener datos para ListView para Clientes
        public DataTable obtenerClientesActivos()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT cl.id, cl.clave, cl.nombre, fp.nombre AS formaPago, v.nombre AS vendedor, cl.estado, cl.ciudad FROM clientes AS cl LEFT JOIN formasPago AS fp " +
                        "ON cl.idFormaPago = fp.id LEFT JOIN usuarios AS v ON cl.idVendedor = v.id WHERE (cl.idSucursal = @idSucursal) AND (cl.eliminado IS NULL) ORDER BY cl.nombre", conn))
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
        public DataTable obtenerClientesBySuc()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT cl.id, cl.clave, cl.nombre, fp.nombre AS formaPago, v.nombre AS vendedor, cl.estado, cl.ciudad FROM clientes AS cl INNER JOIN formasPago AS fp " +
                        "ON cl.idFormaPago = fp.id INNER JOIN usuarios AS v ON cl.idVendedor = v.id WHERE (cl.idSucursal = @idSucursal) ORDER BY cl.id", conn))
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

        //metodo para obtener id del cliente por su clave
        public int obtenerIdByClave(string claveCliente)
        {
            try
            {
                string comando = "SELECT id FROM clientes WHERE clave = @clave";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@clave", claveCliente));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    return int.Parse(reader["id"].ToString());
                                }
                            } else
                            {
                                return 0;
                            }
                            return 0;
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

        //metodo para obtener nombre del cliente por su clave
        public string obtenerNombreByClave(string claveCliente)
        {
            try
            {
                string comando = "SELECT nombre FROM clientes WHERE clave = @clave";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@clave", claveCliente));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    return reader["nombre"].ToString();
                                }
                            }
                            else
                            {
                                return null;
                            }
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
                throw (ex);
            }
        }

        //metodo para obtener nombre del cliente por su clave
        public string obtenerClaveByNombre(string nombreCliente)
        {
            try
            {
                string comando = "SELECT clave FROM clientes WHERE nombre LIKE @nombre AND idSucursal=@idSucursal";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@nombre", nombreCliente));
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    return reader["clave"].ToString();
                                }
                            }
                            else
                            {
                                return null;
                            }
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
                throw (ex);
            }
        }

        //metodo para obtener el Perfil por ID
        public string obtenerClaveByID(int idC)
        {
            try
            {
                string comando = "SELECT clave FROM clientes WHERE id = @id";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idC));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    return reader["clave"].ToString();
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

        public bool existe()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT id FROM clientes WHERE nombre = @nombre AND idSucursal = @idSucursal", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
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
        public bool existeClienteEnFacturacion()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM clientesFacturacion WHERE nombreCliente = @nombre AND rfcCliente = @rfc", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                        cmd.Parameters.Add(new SqlParameter("@rfc", rfc));
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
        public string tieneDiasCreditoByID(int idCliente)
        {
            try
            {
                string comando = "SELECT diasCredito FROM clientesFacturacion WHERE idCliente = @idCliente";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idCliente", idCliente));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    return reader["diasCredito"].ToString();
                                }
                            }
                            else
                            {
                                return "";
                            }
                            return "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return "";
                throw (ex);
            }
        }
        public int obtenerDiasCreditoByID(int idCliente)
        {
            try
            {
                string comando = "SELECT diasCredito FROM clientesFacturacion WHERE id = @idCliente";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idCliente", idCliente));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    return int.Parse(reader["diasCredito"].ToString());
                                }
                            }
                            else
                            {
                                return 0;
                            }
                            return 0;
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
        public DataTable ObtenerPrecioCliente(int idSuc, string param)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT c.id, c.idFormaPago FROM clientes as c, solicitudes as s, detallesSolicitud as ds " +
                        " WHERE c.id = s.idCliente AND ds.idSolicitud = s.id AND s.idSucursal = @idSuc AND c.eliminado IS NULL " + param + " GROUP BY c.id, c.idFormaPago", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSuc", idSuc));
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
        public string MontoFactorClienteByID(int idCliente)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT FORMAT(SUM(precioF), 'N2') as montoDisc from detallesSolicitud as ds, solicitudes as s " +
                        " where ds.idSolicitud = s.id and idCliente = @idCliente and precioU != precioF", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idCliente", idCliente));
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
        public string cantidadEntregadaByIDCliente(int idCliente)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT FORMAT(SUM(cantidadEntregada), 'N2') as cantEntregada from detallesSolicitud as ds, solicitudes as s " +
                        " where ds.idSolicitud = s.id and idCliente = @idCliente", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idCliente", idCliente));
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
        public string cantidadCargadaByIDCliente(int idCliente)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT FORMAT(SUM(cantidad), 'N2') as montoDisc from detallesSolicitud as ds, solicitudes as s " +
                        " where ds.idSolicitud = s.id and idCliente = @idCliente", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idCliente", idCliente));
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

        //metodo para Solicitudes
        public DataTable obtenerDatosByClaveCliente(string claveClt)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM clientes WHERE clave = @clave", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@clave", claveClt));
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