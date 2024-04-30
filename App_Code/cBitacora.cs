using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace despacho
{
    public class cBitacora
    {
        //variables
        private string cadena;

        //propiedades
        public int id { get; set; }
        public int idMaster { get; set; }
        public string accion { get; set; }
        public string motivo { get; set; }

        //Constructor
        public cBitacora()
        {
            cadena = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;
        }

        //metodo para insertar
        public void insertar(int idM, string sAccion, int idUsuarioActivo, string sTabla, string sMotivo = "")
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO " + sTabla + "Bitacora (idMaster, accion, motivo, idUsuario, fecha) VALUES(@idMaster, @accion, @motivo, @idUsuario, GETDATE())", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idMaster", idM));
                        cmd.Parameters.Add(new SqlParameter("@accion", sAccion));
                        cmd.Parameters.Add(new SqlParameter("@motivo", sMotivo));
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


        //Método para obtener el último id registrado en la tabla
        public int getID(string sTable)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT count(id) AS rows FROM " + sTable, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    return int.Parse(reader["rows"].ToString());
                                }
                                return 0;
                            }
                        }
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                return 0;
                throw (ex);
            }
        }

        //metodo para obtener el Perfil por ID
        public void obtenerBitacora(string sTabla, int idM)
        {
            try
            {
                string comando = "SELECT * FROM " + sTabla + " WHERE idMaster = @id";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idM));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    id = int.Parse(reader["id"].ToString());
                                    accion = reader["accion"].ToString();
                                    motivo = reader["motivo"].ToString();
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
        public string getHoraBitByStatus(int idM, string value, bool activo = true)
        {
            try
            {
                string comando = "SELECT fecha FROM ordenDosificacionBitacora WHERE idMaster = " + idM + " AND motivo LIKE '%" + value + "%'";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    return reader["fecha"].ToString();
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

                throw (ex);
            }
        }

        public DataTable obtenerBitacoraDT(string sTabla, int idM)
        {
            sTabla += "Bitacora";
            try
            {
                string comando = "SELECT bit.id, bit.idMaster, bit.accion, bit.motivo, u.usuario, bit.fecha " +
                    "FROM " + sTabla + " AS bit INNER JOIN usuarios AS u ON bit.idUsuario = u.id " +
                    "WHERE bit.idMaster = @id";

                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idM));
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
    }
}