using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace despacho
{
    public class cBasculas
    {
        //variables
        private string cadena;

        //propiedades
        public int id { get; set; }
        public string nombre { get; set; }
        public string capacidad { get; set; }
        public string limite { get; set; }
        public int idUDM { get; set; }
        public int idDosificadora { get; set; }
        public int idSucursal { get; set; }

        //Constructor
        public cBasculas()
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
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO basculas(nombre, capacidad, limite, idUDM, idDosificadora, idSucursal, idUsuario, fechaAlta) " +
                        "VALUES(@nombre, @capacidad, @limite, @idUDM, @idDosificadora, @idSucursal, @idUsuario, GETDATE())", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                        cmd.Parameters.Add(new SqlParameter("@capacidad", capacidad));
                        cmd.Parameters.Add(new SqlParameter("@limite", limite));
                        cmd.Parameters.Add(new SqlParameter("@idUDM", idUDM));
                        cmd.Parameters.Add(new SqlParameter("@idDosificadora", idDosificadora));
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
        public void actualizar(int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE basculas SET nombre = @nombre, capacidad = @capacidad, limite = @limite, idUDM = @idUDM, idDosificadora = @idDosificadora, " +
                        "idUsuarioMod = @idUsuarioMod, fechaMod = GETDATE() WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                        cmd.Parameters.Add(new SqlParameter("@capacidad", capacidad));
                        cmd.Parameters.Add(new SqlParameter("@limite", limite));
                        cmd.Parameters.Add(new SqlParameter("@idUDM", idUDM));
                        cmd.Parameters.Add(new SqlParameter("@idDosificadora", idDosificadora));
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
        public void eliminar(int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE basculas SET eliminado = 1, idUsuarioElimino = @idUsuarioElimino, fechaEliminado = GETDATE() WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioElimino", idUsuarioActivo));
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

        //metodo para obtener el id de perfil en el alta de Perfiles
        public DataTable obtenerView()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT b.id, b.nombre, b.capacidad, b.limite, b.idUDM, b.idDosificadora, s.nombre AS sucursal, udm.unidad, d.nombre AS dosificadora " +
                        "FROM sucursales AS s INNER JOIN basculas AS b ON s.id = b.idSucursal INNER JOIN unidadesDeMedida AS udm ON b.idUDM = udm.id INNER JOIN dosificadora AS d ON b.idDosificadora = d.id " +
                        "WHERE(b.elimino IS NULL) AND(b.idSucursal = @idSucursal)", conn))
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

        //metodo para obtener el Perfil por ID
        public void obtenerBasculaByID()
        {
            try
            {
                string comando = "SELECT * FROM basculas WHERE id = @id";
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
                                    nombre = reader["nombre"].ToString();
                                    capacidad = reader["capacidad"].ToString();
                                    limite = reader["limite"].ToString();
                                    idUDM = int.Parse(reader["idUDM"].ToString());
                                    idDosificadora = int.Parse(reader["idDosificadora"].ToString());
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
        public bool existe()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT id FROM basculas WHERE nombre = @nombre AND idSucursal = @idSucursal", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
                        //cmd.Parameters.Add(new SqlParameter("@id", existe));
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