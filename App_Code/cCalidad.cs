using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace despacho
{
    public class cCalidad
    {
        //variables
        private string cadena;

        //propiedades
        public int id { get; set; }
        public int idOD { get; set; }
        public int numCilindro { get; set; }
        public DateTime fechaColado { get; set; }
        public int edadEnsaye { get; set; }
        public DateTime fechaEnsaye { get; set; }
        public int resistenciaKG { get; set; }
        public int cargaKG { get; set; }
        public string areaCM { get; set; }
        public string esfuerzoKG { get; set; }
        public string resistenciaP { get; set; }
        public int tamMax { get; set; }
        public int revCM { get; set; }
        public string tipoConcreto { get; set; }
        public float tempAmb { get; set; }
        public float tempConc { get; set; }
        public string aditivo { get; set; }
        public string elemento { get; set; }

        //Constructor
        public cCalidad()
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
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO calidad(idOD, numCilindro, fechaColado, edadEnsaye, fechaEnsayeT, resistenciaKG, cargaKG, areaCM, esfuerzoKG, resistenciaP, " +
                        "tamMax, revCM, tipoConcreto, tempAmb, tempConc, aditivo, elemento, idUsuario, fechaAlta) " +
                        "VALUES (@idOD, @numCilindro, @fechaColado, @edadEnsaye, @fechaEnsaye, @resistenciaKG, @cargaKG, @areaCM, @esfuerzoKG, @resistenciaP, " +
                        "@tamMax, @revCM, @tipoConcreto, @tempAmb, @tempConc, @aditivo, @elemento, @idUsuario, GETDATE())", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idOD", idOD));
                        cmd.Parameters.Add(new SqlParameter("@numCilindro", numCilindro));
                        cmd.Parameters.Add(new SqlParameter("@fechaColado", fechaColado));
                        cmd.Parameters.Add(new SqlParameter("@edadEnsaye", edadEnsaye));
                        cmd.Parameters.Add(new SqlParameter("@fechaEnsaye", fechaEnsaye));
                        cmd.Parameters.Add(new SqlParameter("@resistenciaKG", resistenciaKG));
                        cmd.Parameters.Add(new SqlParameter("@cargaKG", cargaKG));
                        cmd.Parameters.Add(new SqlParameter("@areaCM", areaCM));
                        cmd.Parameters.Add(new SqlParameter("@esfuerzoKG", esfuerzoKG));
                        cmd.Parameters.Add(new SqlParameter("@resistenciaP", resistenciaP));
                        cmd.Parameters.Add(new SqlParameter("@tamMax", tamMax));
                        cmd.Parameters.Add(new SqlParameter("@revCM", revCM));
                        cmd.Parameters.Add(new SqlParameter("@tipoConcreto", tipoConcreto));
                        cmd.Parameters.Add(new SqlParameter("@tempAmb", tempAmb));
                        cmd.Parameters.Add(new SqlParameter("@tempConc", tempConc));
                        cmd.Parameters.Add(new SqlParameter("@aditivo", aditivo));
                        cmd.Parameters.Add(new SqlParameter("@elemento", elemento));
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
                    using (SqlCommand cmd = new SqlCommand("UPDATE calidad SET numCilindro = @numCilindro, fechaColado = @fechaColado, edadEnsaye = @edadEnsaye, fechaEnsayeT = @fechaEnsaye, " +
                        "resistenciaKG = @resistenciaKG, cargaKG = @cargaKG, areaCM = @areaCM, esfuerzoKG = @esfuerzoKG, resistenciaP = @resistenciaP, tamMax = @tamMax, revCM = @revCM, tipoConcreto = @tipoConcreto, " +
                        "tempAmb = @tempAmb, tempConc = @tempConc, aditivo = @aditivo, elemento = @elemento, idUsuarioMod = @idUsuarioMod, fechaMod = GETDATE() WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@numCilindro", numCilindro));
                        cmd.Parameters.Add(new SqlParameter("@fechaColado", fechaColado));
                        cmd.Parameters.Add(new SqlParameter("@edadEnsaye", edadEnsaye));
                        cmd.Parameters.Add(new SqlParameter("@fechaEnsaye", fechaEnsaye));
                        cmd.Parameters.Add(new SqlParameter("@resistenciaKG", resistenciaKG));
                        cmd.Parameters.Add(new SqlParameter("@cargaKG", cargaKG));
                        cmd.Parameters.Add(new SqlParameter("@areaCM", areaCM));
                        cmd.Parameters.Add(new SqlParameter("@esfuerzoKG", esfuerzoKG));
                        cmd.Parameters.Add(new SqlParameter("@resistenciaP", resistenciaP));
                        cmd.Parameters.Add(new SqlParameter("@tamMax", tamMax));
                        cmd.Parameters.Add(new SqlParameter("@revCM", revCM));
                        cmd.Parameters.Add(new SqlParameter("@tipoConcreto", tipoConcreto));
                        cmd.Parameters.Add(new SqlParameter("@tempAmb", tempAmb));
                        cmd.Parameters.Add(new SqlParameter("@tempConc", tempConc));
                        cmd.Parameters.Add(new SqlParameter("@aditivo", aditivo));
                        cmd.Parameters.Add(new SqlParameter("@elemento", elemento));
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
        public void eliminar(int idP, int idUsuario)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE calidad SET eliminado=1, fechaElimino=GETDATE(), idUsuarioElimino=@idUsuarioElimino WHERE id=@id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioElimino", idUsuario));
                        cmd.Parameters.Add(new SqlParameter("@id", idP));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metodo para obtener el registro de calidad por OD
        public void obtenerByID()
        {
            try
            {
                string comando = "SELECT * FROM calidad WHERE id = @id";
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
                                    numCilindro = int.Parse(reader["numCilindro"].ToString());
                                    fechaColado = DateTime.Parse(reader["fechaColado"].ToString());
                                    edadEnsaye = int.Parse(reader["edadEnsaye"].ToString());
                                    fechaEnsaye = DateTime.Parse(reader["fechaEnsayeT"].ToString());
                                    resistenciaKG = int.Parse(reader["resistenciaKG"].ToString());
                                    cargaKG = int.Parse(reader["cargaKG"].ToString());
                                    areaCM = reader["areaCM"].ToString();
                                    esfuerzoKG = reader["esfuerzoKG"].ToString();
                                    resistenciaP = reader["resistenciaP"].ToString();
                                    tamMax = int.Parse(reader["tamMax"].ToString());
                                    revCM = int.Parse(reader["revCM"].ToString());
                                    tipoConcreto = reader["tipoConcreto"].ToString();
                                    tempAmb = int.Parse(reader["tempAmb"].ToString());
                                    tempConc = int.Parse(reader["tempConc"].ToString());
                                    aditivo = reader["aditivo"].ToString();
                                    elemento = reader["elemento"].ToString();
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
        //metodo para el combobox de proyectos
        public DataTable obtenerViewByIdOD(int idOD)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT cal.id, cal.numCilindro, cal.fechaColado, cal.edadEnsaye, cal.fechaEnsayeT, cal.resistenciaKG, cal.cargaKG, cal.areaCM, cal.esfuerzoKG, " +
                        "cal.resistenciaP, cal.tamMax, cal.revCM, cal.tipoConcreto, cl.clave, cl.nombre, pr.nombre AS proyecto, ut.nombre AS unidadT, cal.tempAmb, cal.tempConc, cal.aditivo, cal.elemento, o.folio, od.id " +
                        "FROM calidad AS cal INNER JOIN ordenDosificacion AS od ON cal.idOD = od.id INNER JOIN ordenes AS o ON od.idOrden = o.id INNER JOIN " +
                        "solicitudes AS s ON o.idSolicitud = s.id INNER JOIN clientes AS cl ON s.idCliente = cl.id INNER JOIN proyectos AS pr ON s.idProyecto = pr.id INNER JOIN " +
                        "unidadesTransporte AS ut ON od.idUnidadTransporte = ut.id WHERE cal.idOD = @idOD AND cal.eliminado IS NULL", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idOD", idOD));
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
        //Método para obtener el numCilindro
        public string obtenerElementoByIdOD(int idOD)
        {
            try
            {
                string elemento = ""; // Valor predeterminado en caso de que no se encuentre ningún valor
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT elemento FROM elementos AS E INNER JOIN detallesSolicitud AS DS ON DS.idElemento = E.id INNER JOIN ordenDosificacion AS OD ON OD.idDetalleSolicitud = DS.id WHERE OD.id = @idOD ", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idOD", idOD));
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            elemento = result.ToString();
                        }
                    }
                }
                return elemento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Método para obtener el numCilindro
        public string obtenerCodigoByIdOD(int idOD)
        {
            try
            {
                string codigo = ""; // Valor predeterminado en caso de que no se encuentre ningún valor
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT codigo FROM productos AS P JOIN ordenDosificacion AS O ON O.idProducto = P.id WHERE O.id = @idOD ", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idOD", idOD));
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            codigo = result.ToString();
                        }
                    }
                }
                return codigo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Método para obtener el numCilindro
        public int obtenerNumCilindroMasGrandeByIdOD(int idOD)
        {
            try
            {
                int numCilindro = 0; // Valor predeterminado en caso de que no se encuentre ningún valor
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 cal.numCilindro FROM calidad AS cal WHERE cal.idOD = @idOD AND cal.eliminado IS NULL ORDER BY cal.numCilindro DESC", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idOD", idOD));
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            numCilindro = Convert.ToInt32(result);
                        }
                    }
                }
                return numCilindro;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool existeProducto(int existe)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT id FROM productos WHERE descripcion = @descripcion AND id <> @id", conn))
                    {
                        //cmd.Parameters.Add(new SqlParameter("@descripcion", descripcion));
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

        public bool existe()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT id FROM calidad WHERE idOD = @idOD AND numCilindro = @numCilindro AND fechaColado = @fechaColado AND resistenciaKG = @resistenciaKG AND eliminado is NULL", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idOD", idOD));
                        cmd.Parameters.Add(new SqlParameter("@numCilindro", numCilindro));
                        cmd.Parameters.Add(new SqlParameter("@fechaColado", fechaColado));
                        cmd.Parameters.Add(new SqlParameter("@resistenciaKG", resistenciaKG));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                id = Convert.ToInt32(reader["id"]);
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