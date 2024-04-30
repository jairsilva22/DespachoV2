using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace despacho
{
    /// <summary>
    /// Summary description for Usuarios
    /// </summary>
    public class cEmpleadosPercepciones
    {
        //variables
        private string cadena;

        //propiedades
        public int id { get; set; }
        public int idEmpleado { get; set; }
        public int idPercepcion { get; set; }
        public string monto { get; set; }
        public DateTime fecha { get; set; }
        public int idSucursal { get; set; }
        public int mes { get; set; }

        //Constructor
        public cEmpleadosPercepciones()
        {
            cadena = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;
        }

        //metodo para insertar un nuevo usuario
        public void insertar(int idUsuarioActivo)
        {
            try
            {
                string comando = "INSERT INTO empleadosPercepciones(idEmpleado, idPercepcion, monto, fecha, idSucursal, idUsuario, fechaAlta, mes)";
                comando += "VALUES(@idEmpleado, @idPercepcion, @monto, @fecha, @idSucursal, @idUsuario, GETDATE(), @mes)";

                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idEmpleado", idEmpleado));
                        cmd.Parameters.Add(new SqlParameter("@idPercepcion", idPercepcion));
                        cmd.Parameters.Add(new SqlParameter("@monto", monto));
                        cmd.Parameters.Add(new SqlParameter("@fecha", fecha));
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
                        cmd.Parameters.Add(new SqlParameter("@idUsuario", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@mes", mes));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metdodo para actualizar un usuario
        public void actualizar(int idU, int idUsuarioActivo)
        {
            try
            {
                string comando = "UPDATE empleadosPercepciones SET idEmpleado = @idEmpleado, idPercepcion = @idPercepcion, ";
                comando += "monto = @monto, fecha = @fecha, ";
                comando += "idUsuarioMod = @idUsuarioMod, fechaMod = GETDATE() WHERE Id = @id";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idEmpleado", idEmpleado));
                        cmd.Parameters.Add(new SqlParameter("@idPercepcion", idPercepcion));
                        cmd.Parameters.Add(new SqlParameter("@monto", monto));
                        cmd.Parameters.Add(new SqlParameter("@fecha", fecha));
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

        //metodo para eliminar un usuario
        public void eliminar(int idU, int idUsuarioActivo)
        {
            try
            {
                string comando = "UPDATE empleadosPercepciones SET eliminado=1, idUsuarioElimino=@idUsuarioElimino, fechaElimino=GETDATE() WHERE id = @id";
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

        //metodo para buscar un usuario
        public void obtenerEmpleado(int idU)
        {
            try
            {
                string comando = "SELECT * FROM empleadosPercepciones WHERE id = @id";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idU));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    idEmpleado = int.Parse(reader["idEmpleado"].ToString());
                                    idPercepcion = int.Parse(reader["idPercepcion"].ToString());
                                    monto = reader["monto"].ToString();
                                    fecha = DateTime.Parse(reader["fecha"].ToString());
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

        public DataTable obtenerDatosByIdEmpleado(string idSucursal)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT ep.id, ep.idEmpleado, e.nombre, ep.idPercepcion, tp.percepcion, ep.monto, ep.fecha, e.codigo, ep.mes " +
                        "FROM empleadosPercepciones AS ep INNER JOIN empleados AS e ON ep.idEmpleado = e.id INNER JOIN tiposPercepcion AS tp ON ep.idPercepcion = tp.id " +
                        "WHERE (ep.idEmpleado = @idEmpleado) AND (ep.eliminado IS NULL) and ep.idsucursal=@idSucursal ORDER BY ep.id DESC", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idEmpleado", idEmpleado));
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
        public DataTable obtenerDatosBySucursal(int idSucursal)
        {
            try
            {
                string comando = "SELECT ep.id, ep.idEmpleado, e.nombre, ep.idPercepcion, tp.percepcion, ep.monto, ep.fecha, e.codigo, ep.mes " +
                        "FROM empleadosPercepciones AS ep INNER JOIN empleados AS e ON ep.idEmpleado = e.codigo INNER JOIN tiposPercepcion AS tp ON ep.idPercepcion = tp.id " +
                        "WHERE (ep.idSucursal = @idSucursal) AND (ep.eliminado IS NULL) ORDER BY ep.id DESC";

                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
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
        //Método para llenar la vista de usuarios
        public DataTable obtenerBySucursal(int idSucursal)
        {
            try
            {
                string comando = "SELECT * FROM empleadosPercepciones  " +
                    "WHERE idSucursal = @idSucursal AND eliminado IS NULL";

                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
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
        public DataTable obtenerByID(int idU)
        {
            try
            {
                string comando = "SELECT * FROM empleadosPercepciones WHERE(id = @id)";

                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idU));
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
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

        public bool existe()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT id FROM empleadosPercepciones WHERE idEmpleado = @idEmpleado AND idPercepcion = @idPercepcion AND fecha=@fecha AND idSucursal=@idSucursal " +
                        "AND eliminado IS NULL", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idEmpleado", idEmpleado));
                        cmd.Parameters.Add(new SqlParameter("@idPercepcion", idPercepcion));
                        cmd.Parameters.Add(new SqlParameter("@fecha", fecha));
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

        public DataTable obtenerIdByEmpleadoPercepcionFecha()
        {
            try
            {
                string comando = "SELECT id FROM empleadosPercepciones WHERE idEmpleado = @idEmpleado AND idPercepcion = @idPercepcion AND fecha = @fecha AND idSucursal = @idSucursal " +
                        "AND eliminado IS NULL";

                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idEmpleado", idEmpleado));
                        cmd.Parameters.Add(new SqlParameter("@idPercepcion", idPercepcion));
                        cmd.Parameters.Add(new SqlParameter("@fecha", fecha));
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
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

        public DataTable ObtenerDatosPEPI(string comando)
        {
            SqlConnection con = new SqlConnection();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;
            SqlCommand com = con.CreateCommand();
            con.Open();
            com.CommandText = comando;
            //System.Windows.Forms.MessageBox.Show(comando);
            da.SelectCommand = com;
            com.CommandTimeout = 0;
            dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
        }
        public DataTable ObtenerDatosCOMESaltilloConcretos(string comando)
        {
            SqlConnection con = new SqlConnection();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["ConcretoSaltillo"].ConnectionString;
            SqlCommand com = con.CreateCommand();
            con.Open();
            com.CommandText = comando;
            //System.Windows.Forms.MessageBox.Show(comando);
            da.SelectCommand = com;
            com.CommandTimeout = 0;
            dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
        }
        public DataTable ObtenerDatosCOMIrapuatoConcretos(string comando)
        {
            SqlConnection con = new SqlConnection();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["ConcretoIrapuato"].ConnectionString;
            SqlCommand com = con.CreateCommand();
            con.Open();
            com.CommandText = comando;
            //System.Windows.Forms.MessageBox.Show(comando);
            da.SelectCommand = com;
            com.CommandTimeout = 0;
            dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
        }
        public DataTable ObtenerDatosCOMIrapuatoBlock(string comando)
        {
            SqlConnection con = new SqlConnection();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["BlockIrapuato"].ConnectionString;
            SqlCommand com = con.CreateCommand();
            con.Open();
            com.CommandText = comando;
            //System.Windows.Forms.MessageBox.Show(comando);
            da.SelectCommand = com;
            com.CommandTimeout = 0;
            dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
        }
        public DataTable ObtenerDatosCOMSaltilloBlock(string comando)
        {
            SqlConnection con = new SqlConnection();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["BlockSaltillo"].ConnectionString;
            SqlCommand com = con.CreateCommand();
            con.Open();
            com.CommandText = comando;
            //System.Windows.Forms.MessageBox.Show(comando);
            da.SelectCommand = com;
            com.CommandTimeout = 0;
            dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
        }
        public DataTable ObtenerDatosCOMExternos(string comando)
        {
            SqlConnection con = new SqlConnection();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["Externos"].ConnectionString;
            SqlCommand com = con.CreateCommand();
            con.Open();
            com.CommandText = comando;
            //System.Windows.Forms.MessageBox.Show(comando);
            da.SelectCommand = com;
            com.CommandTimeout = 0;
            dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
        }
        public DataTable ObtenerDatosCOMNomina(string comando)
        {
            SqlConnection con = new SqlConnection();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["Nomina"].ConnectionString;
            SqlCommand com = con.CreateCommand();
            con.Open();
            com.CommandText = comando;
            //System.Windows.Forms.MessageBox.Show(comando);
            da.SelectCommand = com;
            com.CommandTimeout = 0;
            dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
        }
        public DataTable ObtenerDatosSaltilloConcretosRemisiones(string comando)
        {
            SqlConnection con = new SqlConnection();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["contpaqcnx"].ConnectionString;
            SqlCommand com = con.CreateCommand();
            con.Open();
            com.CommandText = comando;
            //System.Windows.Forms.MessageBox.Show(comando);
            da.SelectCommand = com;
            com.CommandTimeout = 0;
            dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
        }
        public DataTable ObtenerDatosSaltilloBlockRemisiones(string comando)
        {
            SqlConnection con = new SqlConnection();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["BlockSaltilloRemisiones"].ConnectionString;
            SqlCommand com = con.CreateCommand();
            con.Open();
            com.CommandText = comando;
            //System.Windows.Forms.MessageBox.Show(comando);
            da.SelectCommand = com;
            com.CommandTimeout = 0;
            dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
        }
        public DataTable ObtenerDatosIrapuatoConcretosRemisiones(string comando)
        {
            SqlConnection con = new SqlConnection();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["contpaqcnx"].ConnectionString;
            SqlCommand com = con.CreateCommand();
            con.Open();
            com.CommandText = comando;
            //System.Windows.Forms.MessageBox.Show(comando);
            da.SelectCommand = com;
            com.CommandTimeout = 0;
            dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
        }
        public DataTable ObtenerDatosIrapuatoBlockRemisiones(string comando)
        {
            SqlConnection con = new SqlConnection();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["BlockIrapuatoRemisiones"].ConnectionString;
            SqlCommand com = con.CreateCommand();
            con.Open();
            com.CommandText = comando;
            //System.Windows.Forms.MessageBox.Show(comando);
            da.SelectCommand = com;
            com.CommandTimeout = 0;
            dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
        }
        public DataTable ObtenerDatosSaltilloConcretoBanco(string comando)
        {
            SqlConnection con = new SqlConnection();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["BancoConcretosSaltillo"].ConnectionString;
            SqlCommand com = con.CreateCommand();
            con.Open();
            com.CommandText = comando;
            //System.Windows.Forms.MessageBox.Show(comando);
            da.SelectCommand = com;
            com.CommandTimeout = 0;
            dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
        }
        public DataTable ObtenerDatosSaltilloBlockBanco(string comando)
        {
            SqlConnection con = new SqlConnection();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["BancoBlockSaltillo"].ConnectionString;
            SqlCommand com = con.CreateCommand();
            con.Open();
            com.CommandText = comando;
            //System.Windows.Forms.MessageBox.Show(comando);
            da.SelectCommand = com;
            com.CommandTimeout = 0;
            dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
        }
        public DataTable ObtenerDatosIrapuatoConcretosBanco(string comando)
        {
            SqlConnection con = new SqlConnection();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["BancoConcretosIrapuato"].ConnectionString;
            SqlCommand com = con.CreateCommand();
            con.Open();
            com.CommandText = comando;
            //System.Windows.Forms.MessageBox.Show(comando);
            da.SelectCommand = com;
            com.CommandTimeout = 0;
            dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
        }
        public DataTable ObtenerDatosIrapuatoBlockBanco(string comando)
        {
            SqlConnection con = new SqlConnection();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["BancoBlockIrapuato"].ConnectionString;
            SqlCommand com = con.CreateCommand();
            con.Open();
            com.CommandText = comando;
            //System.Windows.Forms.MessageBox.Show(comando);
            da.SelectCommand = com;
            com.CommandTimeout = 0;
            dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
        }
        //metodo para saber si ya existe el usuario registrado
        //public bool existe(string name)
        //{
        //    try
        //    {
        //        string comando = "SELECT Id FROM empleados WHERE nombre like '@nombre' AND idSucursal = @idSucursal";
        //        using (SqlConnection conn = new SqlConnection(cadena))
        //        {
        //            conn.Open();
        //            using (SqlCommand cmd = new SqlCommand(comando, conn))
        //            {
        //                cmd.Parameters.Add(new SqlParameter("@nombre", name));
        //                cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
        //                using (SqlDataReader reader = cmd.ExecuteReader())
        //                {
        //                    if (reader.HasRows)
        //                    {
        //                        return true;
        //                    }
        //                    else
        //                    {
        //                        return false;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        throw (ex);
        //    }
        //}

    }
}