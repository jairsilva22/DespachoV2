using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace despacho
{
    public class cCiudades
    {
        //variables
        private string cadena;

        //propiedades
        public int id { get; set; }
        public string ciudad { get; set; }
        public int idCiudad { get; set; }
        public int idEstado { get; set; }

        //Constructor
        public cCiudades()
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
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO ciudades(ciudad, idCiudad, idEstado, idUsuario, fechaAlta) VALUES(@ciudad, @idCiudad, @idEstado, @idUsuario, GETDATE())", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@ciudad", ciudad));
                        cmd.Parameters.Add(new SqlParameter("@idCiudad", idCiudad));
                        cmd.Parameters.Add(new SqlParameter("@idEstado", idEstado));
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
        public void actualizar(int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE ciudades SET ciudad = @ciudad, idCiudad = @idCiudad,  idEstado = @idEstado, idUsuarioMod = @idUsuarioMod, fechaMod = GETDATE() WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@ciudad", ciudad));
                        cmd.Parameters.Add(new SqlParameter("@idCiudad", idCiudad));
                        cmd.Parameters.Add(new SqlParameter("@idEstado", idEstado));
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioMod", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@id", id));

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
        public void eliminar()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("DELETE ciudades WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", id));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metodo para el combobox de proyectos
        public DataTable obtenerCiudadesByIdEstado(int idEst)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT idCiudad AS id, ciudad FROM ciudades WHERE (idEstado=" + idEst + ") ORDER BY ciudad", conn))
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

        //metodo para obtener id del cliente por su clave
        public int obtenerIdByIdCiudadANDIdEstado(int idEstado, int idCiudad)
        {
            try
            {
                string comando = "SELECT id FROM ciudades WHERE idCiudad = @idCiudad AND idEstado=@idEstado";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idEstado", idEstado));
                        cmd.Parameters.Add(new SqlParameter("@idCiudad", idCiudad));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    return int.Parse(reader["id"].ToString());
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
        //metodo para obtener id del cliente por su clave
        public int obtenerIdCiudadByID(int idC)
        {
            try
            {
                string comando = "SELECT idCiudad FROM ciudades WHERE id = @id";
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
                                    return int.Parse(reader["idCiudad"].ToString());
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
    }
}