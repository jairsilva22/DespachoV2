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
    public class cProgramacion
    {
        //variables
        private string cadena;

        //propiedades
        public int id { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public string unidad { get; set; }
        public int idUDM { get; set; }
        public int idTipoProducto { get; set; }
        public int idCategoria { get; set; }

        //Constructor
        public cProgramacion()
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
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO productos(codigo, descripcion, unidad, idUDM, idTipoProducto, idUsuario, fechaAlta) VALUES (@codigo, @descripcion, @unidad, @idUDM, @idTipoProducto, @idUsuario, GETDATE())", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@codigo", codigo));
                        cmd.Parameters.Add(new SqlParameter("@descripcion", descripcion));
                        cmd.Parameters.Add(new SqlParameter("@unidad", unidad));
                        cmd.Parameters.Add(new SqlParameter("@idUDM", idUDM));
                        cmd.Parameters.Add(new SqlParameter("@idTipoProducto", idTipoProducto));
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
                    using (SqlCommand cmd = new SqlCommand("UPDATE productos SET codigo = @codigo, descripcion = @descripcion, unidad = @unidad, idUDM = @idUDM, idTipoProducto = @idTipoProducto, idUsuarioMod = @idUsuarioMod, fechaMod = GETDATE() WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@codigo", codigo));
                        cmd.Parameters.Add(new SqlParameter("@descripcion", descripcion));
                        cmd.Parameters.Add(new SqlParameter("@unidad", unidad));
                        cmd.Parameters.Add(new SqlParameter("@idUDM", idUDM));
                        cmd.Parameters.Add(new SqlParameter("@idTipoProducto", idTipoProducto));
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
                    using (SqlCommand cmd = new SqlCommand("UPDATE productos SET eliminado=1, fechaElimino=GETDATE(), idUsuarioElimino=@idUsuarioElimino WHERE id=@id", conn))
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

        //metodo para el ListView de proyectos
        public DataTable obtenerProductos()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT p.id, p.codigo, p.descripcion, p.unidad, tp.tipo, p.eliminado FROM productos AS p INNER JOIN tiposProductos AS tp ON p.idTipoProducto = tp.id WHERE (p.eliminado IS NULL)", conn))
                    {
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
        }//metodo para el combobox de proyectos
        public DataTable obtenerProductosByTipoAndCodigo(int idTipoProducto, string codigo)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM productos WHERE idTipoProducto = @idTipoProducto AND codigo = @codigo ORDER BY id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idTipoProducto", idTipoProducto));
                        cmd.Parameters.Add(new SqlParameter("@codigo", codigo));
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
        public DataTable obtenerProductoByID(int idP)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM productos WHERE id = @id", conn))
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
        //metodo para obtener el producto por su Código
        public DataTable obtenerProductoByCodigo(string code)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT codigo, descripcion, unidad, idUDM, idTipoProducto FROM productos WHERE codigo = @codigo", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@codigo", code));
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

        public bool existeProducto(int existe)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT id FROM productos WHERE descripcion = @descripcion AND id <> @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@descripcion", descripcion));
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

        public bool existeProducto()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT id FROM productos WHERE descripcion = @descripcion", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@descripcion", descripcion));
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

        //metodo para obtener el idSolicitud por su Folio
        public int obtenerIDProductoByCodigo(string sCodigo)
        {
            try
            {
                string comando = "SELECT id FROM productos WHERE codigo = @codigo";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@codigo", sCodigo));
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
    }
}