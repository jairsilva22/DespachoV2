using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace despacho
{
    public class cDepartamento
    {
        //variables
        private string cadena;

        //propiedades
        public int idDpto { get; set; }
        public int idEdoR { get; set; }
        public int idUsuario { get; set; }
        public string departamento { get; set; }
        public string nombreER { get; set; }
        public int idSucursal { get; set; }

        //Constructor
        public cDepartamento()
        {
            cadena = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;
        }

        //metodo para insertar un nuevo usuario
        public void insertar()
        {
            try
            {
                string comando = "INSERT INTO departamento(departamento, idSucursal, idUsuario, fechaAlta)";
                comando += "VALUES(@dpto, @idSucursal, @idUsuario, GETDATE())";

                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@dpto", departamento));
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
                        cmd.Parameters.Add(new SqlParameter("@idUsuario", idUsuario));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public bool existeDpto()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT idDpto FROM departamento WHERE departamento = @dpto AND idSucursal=@idSucursal " +
                        "AND eliminado IS NULL", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@dpto", departamento));
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    idDpto = int.Parse(reader["idDpto"].ToString());
                                }
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

        public bool existeDpto2()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT idDpto FROM departamento WHERE departamento = @dpto AND idDpto != @id AND idSucursal = @idS " +
                        "AND eliminado IS NULL", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@dpto", departamento));
                        cmd.Parameters.Add(new SqlParameter("@id", idDpto));
                        cmd.Parameters.Add(new SqlParameter("@idS", idSucursal));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    idDpto = int.Parse(reader["idDpto"].ToString());
                                }
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

        public void ultimoID()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT MAX(idDpto) AS idDpto FROM departamento WHERE idSucursal=@idSucursal " +
                        "AND eliminado IS NULL", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    idDpto = int.Parse(reader["idDpto"].ToString());
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

        public void obtenerDpto()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT idDpto, departamento, ISNULL(idEdoR, 0) AS idEdoR FROM departamento WHERE idDpto = @id ", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idDpto));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    idDpto = int.Parse(reader["idDpto"].ToString());
                                    idEdoR = int.Parse(reader["idEdoR"].ToString());
                                    departamento = (reader["departamento"].ToString());
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

        public void buscarEdoR()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT nombre FROM estadosResultado WHERE id = @id ", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idEdoR));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    nombreER = (reader["nombre"].ToString());
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

        public DataTable obtenerDptos()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM departamento WHERE eliminado is null AND idSucursal = @idS", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idS", idSucursal));
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

        public DataTable obtenerEstadoR()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT id, nombre FROM estadosResultado ORDER BY nombre", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idS", idSucursal));
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
        //metodo para insertar un nuevo usuario
        public void insertarDpto()
        {
            try
            {
                string comando = "INSERT INTO departamento(departamento, idSucursal, idUsuario, fechaAlta, idEdoR)";
                comando += "VALUES(@dpto, @idSucursal, @idUsuario, GETDATE(), @idER)";

                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@dpto", departamento));
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
                        cmd.Parameters.Add(new SqlParameter("@idUsuario", idUsuario));
                        cmd.Parameters.Add(new SqlParameter("@idER", idEdoR));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void modificarDpto()
        {
            try
            {
                string comando = "UPDATE departamento SET departamento = @dpto, idUsuarioMod = @idUsuario, fechaMod = GETDATE(), idEdoR = @idER WHERE idDpto = @id";

                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@dpto", departamento));
                        cmd.Parameters.Add(new SqlParameter("@id", idDpto));
                        cmd.Parameters.Add(new SqlParameter("@idUsuario", idUsuario));
                        cmd.Parameters.Add(new SqlParameter("@idER", idEdoR));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public void eliminar(int idU, int idUsuarioActivo)
        {
            try
            {
                string comando = "UPDATE departamento SET eliminado=1, idUsuarioElimino=@idUsuarioElimino, fechaElimino=GETDATE() WHERE idDpto = @id";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
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

    }
}