using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace despacho
{
    public class cDocumento
    {
        //variables
        string cadena = string.Empty;
        string comando = string.Empty;

        //propiedades

        public int iddocumento { get; set; }
        public string descripcion { get; set; }
        public string abreviatura { get; set; }
        public string tipo { get; set; }
        public string efecto { get; set; }
        public string abrebiatura { get; set; }
        public string serie { get; set; }
        public string formato { get; set; }
        public int idSucursal { get; set; }
        public int idUsuarioActivo { get; set; }

        public cDocumento()
        {
            cadena = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;
        }

        //metodo para obtener los datos de los documentos
       
        public void insertar()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO documento(descripcion, tipo, abrebiatura, serie, efecto, formato, idempresa, idUsuario, fechaAlta) " +
                        "VALUES (@descripcion, @tipo, @abrebiatura, @serie, @efecto, @formato, @idSucursal, @idUsuario, GETDATE())", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@descripcion", descripcion));
                        cmd.Parameters.Add(new SqlParameter("@tipo", tipo));
                        cmd.Parameters.Add(new SqlParameter("@abrebiatura", abrebiatura));
                        cmd.Parameters.Add(new SqlParameter("@serie", serie));
                        cmd.Parameters.Add(new SqlParameter("@efecto", efecto));
                        cmd.Parameters.Add(new SqlParameter("@formato", formato));
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
        public void eliminar(int idDoc, int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE documento SET eliminado=1, idUsuarioElimino=@idUsuarioElimino, fechaElimino=GETDATE() WHERE idDocumento = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioElimino", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@id", idDoc));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public void actualizar(int idDoc, int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE documento SET descripcion = @descripcion, tipo = @tipo, abrebiatura = @abrebiatura, serie = @serie, " +
                        " efecto = efecto, formato = @formato, idEmpresa = @idSucursal, usuarioMod = @idUsuarioMod, fechaMod = GETDATE() WHERE idDocumento = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@descripcion", descripcion));
                        cmd.Parameters.Add(new SqlParameter("@tipo", tipo));
                        cmd.Parameters.Add(new SqlParameter("@abrebiatura", abrebiatura));
                        cmd.Parameters.Add(new SqlParameter("@serie", serie));
                        cmd.Parameters.Add(new SqlParameter("@efecto", efecto));
                        cmd.Parameters.Add(new SqlParameter("@formato", formato));
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioMod", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@id", idDoc));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public DataTable documentos()
        {
            try
            {
                DataTable dt = new DataTable();
                comando = "SELECT * FROM documento ORDER BY descripcion";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
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
        public DataTable obtenerDocumentos()
        {
            try
            {
                DataTable dt = new DataTable();
                comando = "SELECT idDocumento, descripcion, tipo, abrebiatura, serie, efecto, formato, sucursales.nombre as nombreSuc FROM documento, sucursales " +
                    "WHERE idempresa = sucursales.id AND eliminado IS NULL";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
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
        public DataTable obtenerDocumentosbySuc(int idSuc)
        {
            try
            {
                DataTable dt = new DataTable();
                comando = "SELECT idDocumento, descripcion, tipo, abrebiatura, serie, efecto, formato, sucursales.nombre as nombreSuc FROM documento, sucursales " +
                    "WHERE idempresa = sucursales.id AND eliminado IS NULL AND idEmpresa = @idSuc";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSuc", idSuc));
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
        public DataTable obtenerDocumentosbySuc2(int idSuc)
        {
            try
            {
                DataTable dt = new DataTable();
                comando = "SELECT idDocumento, descripcion, tipo, abrebiatura, serie, efecto, formato, sucursales.nombre as nombreSuc FROM documento, sucursales " +
                    "WHERE idempresa = sucursales.id AND eliminado IS NULL AND descripcion = 'Factura' AND idEmpresa = @idSuc";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSuc", idSuc));
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
        public DataTable obtenerDocumentoByID(int idDoc)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM documento WHERE idDocumento = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idDoc));
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
        public bool existe(string descripcion, string tipo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM documento WHERE descripcion = @descripcion AND tipo = @tipo", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@descripcion", descripcion));
                        cmd.Parameters.Add(new SqlParameter("@tipo", tipo));
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