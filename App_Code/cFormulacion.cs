using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace despacho
{
    public class cFormulacion
    {
        //variables
        private string cadena;

        //propiedades
        public int id { get; set; }
        public int idProducto { get; set; }
        public string material { get; set; }
        public float cantidad { get; set; }
        public string unidad { get; set; }
        public int idUnidad { get; set; }
        public int idMaterial { get; set; }

        //Constructor
        public cFormulacion()
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
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO formulacion(idProducto, material, cantidad, unidad, idUnidad, idMaterial) VALUES(@idProducto, @material, @cantidad, @unidad, @idUnidad, @idMaterial)", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idProducto", idProducto));
                        cmd.Parameters.Add(new SqlParameter("@material", material));
                        cmd.Parameters.Add(new SqlParameter("@cantidad", cantidad));
                        cmd.Parameters.Add(new SqlParameter("@unidad", unidad));
                        cmd.Parameters.Add(new SqlParameter("@idUnidad", idUnidad));
                        cmd.Parameters.Add(new SqlParameter("@idMaterial", idMaterial));
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
                    using (SqlCommand cmd = new SqlCommand("UPDATE formulacion SET cantidad = @cantidad, unidad = @unidad, idUnidad = @idUnidad, idUsuarioMod = @idUsuarioMod, fechaMod = GETDATE() "+
                        "WHERE material = @material AND idProducto = @idProducto", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@cantidad", cantidad));
                        cmd.Parameters.Add(new SqlParameter("@unidad", unidad));
                        cmd.Parameters.Add(new SqlParameter("@idUnidad", idUnidad));
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioMod", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@material", material));
                        cmd.Parameters.Add(new SqlParameter("@idProducto", idProducto));

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
        public void eliminar(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("DELETE formulacion WHERE id = @id", conn))
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

        //metodo para obtener la Formulación por ID de la fórmula
        public DataTable obtenerFormulacionByIdProducto(int idProducto)
        {
            try
            {
                string comando = "SELECT f.id, f.idProducto, f.idMaterial, f.material, f.cantidad, f.unidad, f.idUnidad, m.adicional FROM formulacion AS f INNER JOIN materiales AS m ON f.idMaterial = m.id " +
                    "WHERE(f.idProducto = @idProducto)";

                DataTable dt = new DataTable();

                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idProducto", idProducto));
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

        public bool existeFormulacionByID(int id)
        {
            try
            {
                string comando = "SELECT * FROM formulacion WHERE id = @id";
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

        public bool existeFormulacionByMaterial(int idProducto, string material)
        {
            try
            {
                string comando = "SELECT * FROM formulacion WHERE idProducto = @idProducto AND material = @material";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idProducto", idProducto));
                        cmd.Parameters.Add(new SqlParameter("@material", material));
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

        //Obtener formulacion nombre del material desde tabla materiales
        //metodo para obtener la Formulación por ID de la fórmula
        public DataTable getFormulaByIdProducto(int idProducto) {
            try {
                string comando = "SELECT f.id, f.idProducto, f.idMaterial, m.material, f.cantidad, f.unidad, f.idUnidad, m.adicional, um.unidadSAT, um.descripcion " +
                    "FROM formulacion AS f INNER JOIN materiales AS m ON f.idMaterial = m.id INNER JOIN unidadesDeMedida AS um ON f.idUnidad = um.id " +
                    "WHERE(f.idProducto = @idProducto)";

                DataTable dt = new DataTable();

                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn)) {
                        cmd.Parameters.Add(new SqlParameter("@idProducto", idProducto));
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd)) {
                            da.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
            catch (Exception ex) {
                throw (ex);
            }
        }

    }
}