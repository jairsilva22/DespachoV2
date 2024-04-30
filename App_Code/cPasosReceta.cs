using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace despacho
{
    public class cPasosReceta
    {
        //variables
        private string cadena;

        //propiedades
        public int id { get; set; }
        public int idReceta { get; set; }
        public int paso { get; set; }
        public string material { get; set; }
        public int idMaterial { get; set; }
        public float porcentaje { get; set; }

        //Constructor
        public cPasosReceta()
        {
            cadena = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;
        }

        //metodo para insertar
        public void insertar(string percent, int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO pasosReceta(idReceta, paso, material, idMaterial, porcentaje, idUsuario, fechaAlta) VALUES(@idReceta, @paso, @material, @idMaterial, " +
                        "@porcentaje, @idUsuario, GETDATE())", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idReceta", idReceta));
                        cmd.Parameters.Add(new SqlParameter("@paso", paso));
                        cmd.Parameters.Add(new SqlParameter("@material", material));
                        cmd.Parameters.Add(new SqlParameter("@idMaterial", idMaterial));
                        cmd.Parameters.Add(new SqlParameter("@porcentaje", percent));
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
        public void actualizar(string percent, int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE pasosReceta SET paso = @paso, material = @material, idMaterial = @idMaterial, porcentaje = @porcentaje, " +
                        "idUsuarioMod = @idUsuarioMod, fechaMod = GETDATE() WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@paso", paso));
                        cmd.Parameters.Add(new SqlParameter("@material", material));
                        cmd.Parameters.Add(new SqlParameter("@idMaterial", idMaterial));
                        cmd.Parameters.Add(new SqlParameter("@porcentaje", percent));
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
        public void eliminar(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("DELETE pasosReceta WHERE id = @id", conn))
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

        public DataTable getPasosByIdReceta(int idReceta)
        {
            try
            {
                string comando = "SELECT * FROM pasosReceta WHERE idReceta = @idReceta ORDER BY paso";

                DataTable dt = new DataTable();

                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idReceta", idReceta));
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

        public float porcentajeCompletado(int idMaterial, int idReceta)
        {
            try
            {
                string comando = "SELECT SUM(porcentaje) AS Porcentaje FROM pasosReceta WHERE idReceta = @idReceta AND idMaterial = @idMaterial";
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idReceta", idReceta));
                        cmd.Parameters.Add(new SqlParameter("@idMaterial", idMaterial));
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                            if (String.IsNullOrEmpty(dt.Rows[0][0].ToString()))
                            {
                                return 0;
                            }
                            else
                            {
                                return float.Parse(dt.Rows[0][0].ToString());
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

        public int pasosOnReceta(int idReceta)
        {
            try
            {
                string comando = "SELECT max(paso) AS pasos FROM pasosReceta WHERE idReceta = @idReceta";
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idReceta", idReceta));
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                            if (String.IsNullOrEmpty(dt.Rows[0][0].ToString()))
                            {
                                return 0;
                            }
                            else
                            {
                                return int.Parse(dt.Rows[0][0].ToString());
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