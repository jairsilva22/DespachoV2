using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace despacho
{
    public class cUTransporte
    {
        //variables
        private string cadena;

        //propiedades
        public int id { get; set; }
        public string nombre { get; set; }
        public string matricula { get; set; }
        public float capacidad { get; set; }
        public float capacidadMax { get; set; }
        public string combustible { get; set; }
        public string color { get; set; }
        public int idEstadoUnidad { get; set; }
        public int idUDM { get; set; }
        public int idTipoUT { get; set; }
        public int idSucursal { get; set; }
        //public int idChofer { get; set; }


        //Constructor
        public cUTransporte()
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
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO unidadesTransporte(nombre, matricula, capacidad, capacidadMax, combustible, color, idUDM, idTipoUT, idSucursal, idUsuario, fechaAlta) " +
                        "VALUES(@nombre, @matricula, @capacidad, @capacidadMax, @color, @combustible, @idUDM, @idTipoUT, @idSucursal, @idUsuario, GETDATE())", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                        cmd.Parameters.Add(new SqlParameter("@matricula", matricula));
                        cmd.Parameters.Add(new SqlParameter("@capacidad", capacidad));
                        cmd.Parameters.Add(new SqlParameter("@capacidadMax", capacidadMax));
                        cmd.Parameters.Add(new SqlParameter("@combustible", combustible));
                        cmd.Parameters.Add(new SqlParameter("@color", color));
                        cmd.Parameters.Add(new SqlParameter("@idUDM", idUDM));
                        cmd.Parameters.Add(new SqlParameter("@idTipoUT", idTipoUT));
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
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
        public void actualizar(int idU, int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE unidadesTransporte SET nombre = @nombre, matricula = @matricula, capacidad = @capacidad, capacidadMax = @capacidadMax, combustible = @combustible, color = @color, " +
                        "idEstadoUnidad = @idEstadoUnidad, idUDM = @idUDM, " +
                        "idTipoUT = @idTipoUT, idSucursal = @idSucursal, idUsuarioMod = @idUsuarioMod, fechaMod = GETDATE() WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                        cmd.Parameters.Add(new SqlParameter("@matricula", matricula));
                        cmd.Parameters.Add(new SqlParameter("@capacidad", capacidad));
                        cmd.Parameters.Add(new SqlParameter("@capacidadMax", capacidadMax));
                        cmd.Parameters.Add(new SqlParameter("@combustible", combustible));
                        cmd.Parameters.Add(new SqlParameter("@color", color));
                        cmd.Parameters.Add(new SqlParameter("@idEstadoUnidad", idEstadoUnidad));
                        cmd.Parameters.Add(new SqlParameter("@idUDM", idUDM));
                        cmd.Parameters.Add(new SqlParameter("@idTipoUT", idTipoUT));
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioMod", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@id", idU));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metodo para actualizar Estado Unidad 
        public void actualizarEstado(int idU, int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE unidadesTransporte SET idEstadoUnidad = @idEstadoUnidad, " +
                        "idUsuarioMod = @idUsuarioMod, fechaMod = GETDATE() WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idEstadoUnidad", idEstadoUnidad));
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioMod", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@id", idU));

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
        public void eliminar(int idUT, int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE unidadesTransporte SET eliminada=1, idUsuarioElimino=@idUsuarioElimino, fechaElimino=GETDATE() WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioElimino", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@id", idUT));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        //metodo para asignar chofer a la unidad 
        //public void asignarChofer(int idU, int idC, int idUsuarioActivo)
        //{
        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(cadena))
        //        {
        //            conn.Open();
        //            using (SqlCommand cmd = new SqlCommand("UPDATE unidadesTransporte SET idChofer = @idChofer, idUsuarioMod = @idUsuarioMod, fechaMod = GETDATE() WHERE id = @id", conn))
        //            {
        //                cmd.Parameters.Add(new SqlParameter("@idChofer", idC));
        //                cmd.Parameters.Add(new SqlParameter("@idUsuarioMod", idUsuarioActivo));
        //                cmd.Parameters.Add(new SqlParameter("@id", idU));

        //                int filasAfectadas = cmd.ExecuteNonQuery();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //    }
        //}

        //metodo para el combobox de programacion
        public DataTable obtenerUT()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM unidadesTransporte Where eliminada IS NULL", conn))
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                        return dt;
                    }
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public DataTable obtenerUnidadesTransporte(int tipo)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT ut.id, ut.nombre, tut.tipo FROM unidadesTransporte AS ut INNER JOIN tiposUnidadTransporte AS tut ON ut.idTipoUT = tut.id " +
                        "WHERE(tut.idTipoProducto = @idTP)", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idTP", tipo));
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

        //metodo para el combobox de programacion
        public DataTable obtenerUTByID(int idUT)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM unidadesTransporte WHERE id = @id AND eliminada IS NULL", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idUT));
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

        //metodo para la vista de Unidades de Transporte
        public DataTable obtenerUTView()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT ut.id, ut.nombre, ut.matricula, ut.capacidad, ut.capacidadMax, ut.combustible, ut.color, udm.unidad, tut.tipo, s.nombre AS sucursal " + 
                        "FROM unidadesTransporte AS ut INNER JOIN tiposUnidadTransporte AS tut ON ut.idTipoUT = tut.id INNER JOIN unidadesDeMedida AS udm ON ut.idUDM = udm.id INNER JOIN " +
                        "sucursales AS s ON ut.idSucursal = s.id WHERE (ut.idSucursal = @idSucursal) AND (ut.eliminada IS NULL)", conn))
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

        //metodo para el combobox de programacion
        public DataTable obtenerUTBySucursal(int idS)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM unidadesTransporte WHERE idSucursal = @idSucursal", conn))
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

        //metodo para obtener el Perfil por ID
        public void obtenerByID(int idT)
        {
            try
            {
                string comando = "SELECT * FROM unidadesTransporte WHERE id = @id";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idT));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    nombre = reader["nombre"].ToString();
                                    matricula = reader["matricula"].ToString();
                                    capacidad = float.Parse(reader["capacidad"].ToString());
                                    capacidadMax = float.Parse(reader["capacidadMax"].ToString());
                                    color = reader["color"].ToString();
                                    idEstadoUnidad = int.Parse(reader["idEstadoUnidad"].ToString());
                                    idUDM = int.Parse(reader["idUDM"].ToString());
                                    idTipoUT = int.Parse(reader["idTipoUT"].ToString());
                                    idSucursal = int.Parse(reader["idSucursal"].ToString());
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

        //metodo para obtener Las unidades Disponibles segun el tipo de Unidad
        public DataTable obtenerUDByIDOD(int idOD)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT ut.id, ut.nombre, ut.matricula, ut.color, eu.estado, tiposUnidadTransporte.tipo, od.idTUT, ut.idTipoUT, od.id " +
                        "FROM unidadesTransporte AS ut INNER JOIN estadosUnidad AS eu ON ut.idEstadoUnidad = eu.id INNER JOIN "+ 
                        "tiposUnidadTransporte ON ut.idTipoUT = tiposUnidadTransporte.id INNER JOIN ordenDosificacion AS od ON ut.idTipoUT = od.idTUT " +
                        "WHERE(od.id = @id) AND(eu.id = 1)", conn))
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
        public bool existeUT(string sNombre, string sColor, int sIdS)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT id FROM unidadesTransporte WHERE nombre = @nombre AND color = @color AND idSucursal = @idSucursal", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@nombre", sNombre));
                        cmd.Parameters.Add(new SqlParameter("@color", sColor));
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", sIdS));
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
        public string ObtenerNombreUnidad(int idUnidad)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT nombre FROM unidadesTransporte WHERE id = @idUnidad", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idUnidad", idUnidad));
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
        public int obtenerCantidaViajesByIDUnidad(int idUnidad, DateTime fecha)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT fecha, count(id) AS viajes FROM ordenDosificacion WHERE idUnidadTransporte = @idUnidad " +
                        " AND fecha = CONVERT(datetime, @fecha, 103) GROUP BY fecha", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idUnidad", idUnidad));
                        cmd.Parameters.Add(new SqlParameter("@fecha", fecha));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                return int.Parse(dt.Rows[0]["viajes"].ToString());
                            } else
                            {
                                return 0;
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
        public int obtenerIDChoferUnidadByIDUnidad(int idUnidad)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT idChofer FROM unidadesTChoferes WHERE idUnidad = @idUnidad AND eliminada IS NULL", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idUnidad", idUnidad));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            return int.Parse(dt.Rows[0]["idChofer"].ToString());
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public int obtenerIDTipoUnidadByIDUnidad(int idUnidad)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT ISNULL(idTipoUT, 0) AS idTipoUT FROM unidadesTransporte WHERE id = @idUT", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idUT", idUnidad));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            return int.Parse(dt.Rows[0]["idTipoUT"].ToString());
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public bool existeIDTUT(int idUnidad)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT idTipoUT FROM unidadesTransporte WHERE id = @idUT", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idUT", idUnidad));
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
    }
}