using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace despacho
{
    public class cMateriales
    {
        //variables
        private string cadena;

        //propiedades
        public int id { get; set; }
        public string material { get; set; }
        public int idUnidad { get; set; }

        //Constructor
        public cMateriales()
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
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO materiales(material, idUnidad, idUsuario, fechaAlta) VALUES(@material, @idUnidad, @idUsuario, GETDATE())", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@material", material));
                        cmd.Parameters.Add(new SqlParameter("@idUnidad", idUnidad));
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
                    using (SqlCommand cmd = new SqlCommand("UPDATE materiales SET material = @material, idUnidad = @idUnidad, idUsuarioMod = @idUsuarioMod, fechaMod = GETDATE() WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@material", material));
                        cmd.Parameters.Add(new SqlParameter("@idUnidad", idUnidad));
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
        public void eliminar(int idM, int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE materiales SET eliminado=1, idUsuarioElimino=@idUsuarioElimino, fechaElimino=GETDATE() WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioElimino", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@id", idM));

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
        public DataTable obtenerMateriales()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT m.id, m.material, udm.unidad FROM materiales AS m INNER JOIN unidadesDeMedida AS udm ON m.idUnidad = udm.id WHERE(m.eliminado IS NULL)", conn))
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

        //metodo para obtener el Perfil por ID
        public void obtenerMaterialByID()
        {
            try
            {
                string comando = "SELECT * FROM materiales WHERE id = @id";
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
                                    material = reader["material"].ToString();
                                    idUnidad = int.Parse(reader["idUnidad"].ToString());
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

        public float getCantidadByIdMaterialANDFecha(string material, int idSucursal, DateTime fecha)
        {
            try
            {
                string comando = "SELECT SUM(bd.cantidad) as cantidad FROM bitacoraDosificacion AS bd INNER JOIN ordenDosificacion AS od ON bd.idOD = od.id WHERE(bd.material = @material) AND(od.fecha = @fecha) AND(od.idSucursal = @idSucursal) " +
                     "AND(od.idEstadoDosificacion > 4) AND(od.idEstadoDosificacion < 12)";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@material", material));
                        cmd.Parameters.Add(new SqlParameter("@fecha", fecha));
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    return float.Parse(reader["cantidad"].ToString());
                                }
                            }
                            return 0;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}