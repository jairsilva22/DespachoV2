using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace despacho
{
    public class cTipoProducto
    {
        //variables
        private string cadena;

        //propiedades
        public int id { get; set; }
        public string tipo { get; set; }
        public int idSucursal { get; set; }
        public bool revenimiento { get; set; }


        //Constructor
        public cTipoProducto()
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
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO tiposProductos(tipo, idSucursal, revenimiento, idUsuario, fechaAlta) " +
                        "VALUES(@tipo, @idSucursal, @revenimiento, @idUsuario, GETDATE())", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@tipo", tipo));
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
                        cmd.Parameters.Add(new SqlParameter("@revenimiento", false));
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

        public void actualizar(int idUT, int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE tiposProductos SET tipo = @tipo, revenimiento = @revenimiento, idUsuarioMod = @idUsuarioMod, fechaMod = GETDATE() WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@tipo", tipo));
                        cmd.Parameters.Add(new SqlParameter("@revenimiento", revenimiento));
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioMod", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@id", idUT));

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
        public void eliminar(int idTP, int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE tiposProductos SET eliminado=1, idUsuarioElimino=@idUsuarioElimino, fechaElimino=GETDATE() WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioElimino", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@id", idTP));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metodo para el combobox de programacion
        public DataTable obtenerTiposProducto()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT tp.id, tp.tipo, tp.idSucursal, tp.revenimiento, s.nombre FROM tiposProductos AS tp INNER JOIN " +
                        "sucursales AS s ON tp.idSucursal = s.id WHERE (idSucursal = @idSucursal) AND (tp.eliminado IS NULL)", conn))
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

        //metodo para el combobox de Factores
        public DataTable obtenerByIdSucursal()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM tiposProductos WHERE idSucursal = @idSucursal AND eliminado IS NULL", conn))
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
        //metodo para obtener el producto por su ID
        public DataTable obtenerTiposProductoByID(int idP)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM tiposProductos WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idP));
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
        //metodo para obtener el idSolicitud por su Folio
        public int obtenerIDByTPAndSucursal()
        {
            try
            {
                string comando = "SELECT id FROM tiposProductos WHERE tipo=@tipo AND idSucursal=@idSucursal";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@tipo", tipo));
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    return int.Parse(reader["id"].ToString());
                                }
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

        public void actualizarEliminacion()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("update tiposProductos set eliminado = null WHERE tipo = @tipo AND idSucursal = @idSucursal  AND idSucursal = @idSucursal", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@tipo", tipo));
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
                       
                        cmd.ExecuteReader();
                   
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
                    using (SqlCommand cmd = new SqlCommand("SELECT id FROM tiposProductos WHERE tipo = @tipo AND idSucursal = @idSucursal", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@tipo", tipo));
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
        public string obtenerTipoProductoByIDTipo(int idTipo)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT tipo FROM tiposProductos WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idTipo));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            return dt.Rows[0][0].ToString();
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