using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace despacho
{
    public class cTurnos
    {
        //variables
        private string cadena;

        //propiedades
        public int id { get; set; }
        public string turno { get; set; }
        public bool activo { get; set; }

        //Constructor
        public cTurnos()
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
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO proyectos(nombre, calle, numero, interior, colonia, cp, idCliente, idUsuario, fechaAlta) VALUES(@nombre, @calle, @numero, @interior, @colonia, @cp, @idCliente, @idUsuario, GETDATE())", conn))
                    {
                        //cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                        //cmd.Parameters.Add(new SqlParameter("@calle", calle));
                        //cmd.Parameters.Add(new SqlParameter("@numero", numero));
                        //cmd.Parameters.Add(new SqlParameter("@interior", interior));
                        //cmd.Parameters.Add(new SqlParameter("@colonia", colonia));
                        //cmd.Parameters.Add(new SqlParameter("@cp", cp));
                        //cmd.Parameters.Add(new SqlParameter("@idCliente", idCliente));
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
                    using (SqlCommand cmd = new SqlCommand("UPDATE proyectos SET nombre = @nombre, calle = @calle, numero = @numero, interior = @interior, colonia = @colonia, cp = @cp, idCliente = @idCliente, idUsuarioMod = @idUsuarioMod, fechaMod = GETDATE() WHERE id = @id", conn))
                    {
                        //cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                        //cmd.Parameters.Add(new SqlParameter("@calle", calle));
                        //cmd.Parameters.Add(new SqlParameter("@numero", numero));
                        //cmd.Parameters.Add(new SqlParameter("@interior", interior));
                        //cmd.Parameters.Add(new SqlParameter("@colonia", colonia));
                        //cmd.Parameters.Add(new SqlParameter("@cp", cp));
                        //cmd.Parameters.Add(new SqlParameter("@idCliente", idCliente));
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
                    using (SqlCommand cmd = new SqlCommand("DELETE proyectos WHERE id = @id", conn))
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
        public DataTable obtenerTurnos()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT id, turno FROM turnos", conn))
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

        //metodo para obtener el id de perfil en el alta de Perfiles
        public int obtenerPerfil()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT id FROM perfiles WHERE descripcion = @descripcion", conn))
                    {
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
                throw (ex);
            }
        }

        //metodo para obtener el Perfil por ID
        public void obtenerPerfilByID()
        {
            try
            {
                string comando = "SELECT * FROM perfiles WHERE id = @id";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", id));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                //    descripcion = reader["descripcion"].ToString();
                                //    activo = bool.Parse(reader["activo"].ToString());
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
        public bool existePerfil(int existe)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT id FROM perfiles WHERE descripcion = @nombre AND id <> @id", conn))
                    {
                        //cmd.Parameters.Add(new SqlParameter("@nombre", descripcion));
                        cmd.Parameters.Add(new SqlParameter("@id", existe));
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

        public bool existePerfil()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT id FROM perfiles WHERE descripcion = @nombre", conn))
                    {
                        //cmd.Parameters.Add(new SqlParameter("@nombre", descripcion));
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