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
    public class cProductosFactor
    {
        //variables
        private string cadena;
        private string tabla;

        //propiedades
        public int id { get; set; }
        public int idTipoProducto { get; set; }
        public string factor { get; set; }
        public string porcentaje { get; set; }
        public int idSucursal { get; set; }

        //Constructor
        public cProductosFactor()
        {
            cadena = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;
            tabla = "productosFactor";
        }

        //metodo para insertar
        public void insertar(int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO " + tabla + "(idTipoProducto, factor, porcentaje, idSucursal, idUsuario, fechaAlta) " +
                        "VALUES (@idTipoProducto, @factor, @porcentaje, @idSucursal, @idUsuario, GETDATE())", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idTipoProducto", idTipoProducto));
                        cmd.Parameters.Add(new SqlParameter("@factor", factor));
                        cmd.Parameters.Add(new SqlParameter("@porcentaje", porcentaje));
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
                    using (SqlCommand cmd = new SqlCommand("UPDATE " + tabla + " SET idTipoProducto = @idTipoProducto, " +
                        "factor = @factor, porcentaje = @porcentaje, idSucursal = @idSucursal, idUsuarioMod = @idUsuarioMod, fechaMod = GETDATE() WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idTipoProducto", idTipoProducto));
                        cmd.Parameters.Add(new SqlParameter("@factor", factor));
                        cmd.Parameters.Add(new SqlParameter("@porcentaje", porcentaje));
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
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
        public void eliminar(int idF, int idUsuario)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE " + tabla + " SET eliminado=1, fechaElimino=GETDATE(), idUsuarioElimino=@idUsuarioElimino WHERE id=@id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioElimino", idUsuario));
                        cmd.Parameters.Add(new SqlParameter("@id", idF));

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
        public DataTable obtenerView()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT pf.id, pf.idTipoProducto, tp.tipo, pf.factor, pf.porcentaje, pf.idSucursal, s.nombre FROM productosFactor AS pf INNER JOIN " +
                        "tiposProductos AS tp ON pf.idTipoProducto = tp.id INNER JOIN sucursales AS s ON pf.idSucursal = s.id WHERE(pf.eliminado IS NULL) AND (pf.idSucursal=@idSucursal)", conn))
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
        //metodo para el combobox de proyectos
        public DataTable obtenerByTP(int idTipoProducto)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT pf.id, pf.idTipoProducto, tp.tipo, pf.factor, pf.porcentaje, pf.idSucursal, s.nombre FROM productosFactor AS pf INNER JOIN " +
                        "tiposProductos AS tp ON pf.idTipoProducto = tp.id INNER JOIN sucursales AS s ON pf.idSucursal = s.id WHERE (pf.idTipoProducto = @idTipoProducto) AND " +
                        "(pf.idSucursal = @idSucursal) AND (pf.eliminado IS NULL)", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idTipoProducto", idTipoProducto));
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
        public DataTable obtenerByID(int idP)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT pf.id, pf.idTipoProducto, tp.tipo, pf.factor, pf.porcentaje, pf.idSucursal, s.nombre FROM productosFactor AS pf INNER JOIN " +
                        "tiposProductos AS tp ON pf.idTipoProducto = tp.id INNER JOIN sucursales AS s ON pf.idSucursal = s.id WHERE (pf.id = @id)", conn))
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

        public bool existe(int existe)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT id FROM " + tabla + " WHERE id = @id", conn))
                    {
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
        public bool existeByTPAndFactorAndSucursal()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT id FROM " + tabla + " WHERE idTipoProducto = @idTipoProducto AND factor = @factor AND idSucursal = @idSucursal", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idTipoProducto", idTipoProducto));
                        cmd.Parameters.Add(new SqlParameter("@factor", factor));
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

        public void actualizarEliminacion()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("update productosFactor set eliminado = null where idTipoProducto = @idTipoProducto AND factor = @factor AND idSucursal = @idSucursal", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idTipoProducto", idTipoProducto));
                        cmd.Parameters.Add(new SqlParameter("@factor", factor));
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
    }
}