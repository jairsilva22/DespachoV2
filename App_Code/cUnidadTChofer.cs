using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace despacho
{
    public class cUnidadTChofer
    {
        //variables
        private string cadena;

        //propiedades
        public int id { get; set; }
        public int idUnidad { get; set; }
        public int idChofer { get; set; }
        public bool activo { get; set; }
        public int idSucursal { get; set; }


        //Constructor
        public cUnidadTChofer()
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
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO unidadesTChoferes(idUnidad, idChofer, activo, idSucursal, idUsuario, fechaAlta) " +
                        "VALUES(@idUnidad, @idChofer, @activo, @idSucursal, @idUsuario, GETDATE())", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idUnidad", idUnidad));
                        cmd.Parameters.Add(new SqlParameter("@idChofer", idChofer));
                        cmd.Parameters.Add(new SqlParameter("@activo", true));
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
                    using (SqlCommand cmd = new SqlCommand("UPDATE unidadesTChoferes SET idUnidad = @idUnidad, idChofer = @idChofer, activo = @activo, " +
                        "idSucursal = @idSucursal, idUsuarioMod = @idUsuarioMod, fechaMod = GETDATE() WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idUnidad", idUnidad));
                        cmd.Parameters.Add(new SqlParameter("@idChofer", idChofer));
                        cmd.Parameters.Add(new SqlParameter("@activo", true));
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

        //metodo para eliminar
        public void eliminar(int idU, int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE unidadesTChoferes SET eliminada=1, idUsuarioElimino=@idUsuarioElimino, fechaElimino=GETDATE() WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioElimino", idUsuarioActivo));
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
        //metodo para descativar choferes previo nueva alta
        public void desactivarChoferes(int idU, int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE unidadesTChoferes SET activo='False', idUsuarioMod=@idUsuarioMod, fechaMod=GETDATE() WHERE idUnidad = @idUnidad", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioMod", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@idUnidad", idU));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        //metodo para descativar choferes previo nueva alta
        public void desactivarChofer(int idU, int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE unidadesTChoferes SET activo='False', idUsuarioMod=@idUsuarioMod, fechaMod=GETDATE() WHERE id = @id", conn))
                    {
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
        public DataTable obtenerUnidadesAsignadasByIDUnidadANDIDSucursal(int idU, int idS)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT uc.id, uc.idUnidad, ut.nombre AS unidad, uc.idChofer, ch.nombre AS chofer, uc.activo FROM unidadesTChoferes AS uc INNER JOIN " +
                        "unidadesTransporte AS ut ON uc.idUnidad = ut.id INNER JOIN usuarios AS ch ON uc.idChofer = ch.id WHERE(uc.idUnidad = @idUnidad) AND (uc.idSucursal = @idSucursal) AND(uc.eliminada IS NULL)", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idUnidad", idU));
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

        public int existeChofer(int idC)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT id FROM unidadesTChoferes WHERE idChofer = @id AND eliminada IS NULL", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idC));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    return int.Parse(reader["id"].ToString());
                                }
                                return 0;
                            }
                            else
                            {
                                return 0;
                            }
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

        public int existeChoferEnUnidad(int idC)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT id FROM unidadesTChoferes WHERE idChofer = @id AND idUnidad = @idUnidad AND eliminada IS NULL", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idC));
                        cmd.Parameters.Add(new SqlParameter("@idUnidad", idUnidad));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    return int.Parse(reader["id"].ToString());
                                }
                                return 0;
                            }
                            else
                            {
                                return 0;
                            }
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
        public string obtenerUnidadByIdChofer(int idC)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT ut.nombre AS unidad FROM unidadesTChoferes AS uc INNER JOIN unidadesTransporte AS ut ON uc.idUnidad = ut.id WHERE(uc.idChofer = @id)", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idC));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    return reader["unidad"].ToString();
                                }
                                return "";
                            }
                            else
                            {
                                return "";
                            }
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
    }
}