using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace despacho
{
    public class cElementos
    {
        //variables
        private string cadena;

        //propiedades
        public int id { get; set; }
        public string elemento { get; set; }
        public int idTipoProducto { get; set; }
        public int idSucursal { get; set; }


        //Constructor
        public cElementos()
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
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO elementos(elemento, idTipoProducto, idSucursal, idUsuario, fechaAlta) " +
                        "VALUES(@elemento, @idTipoProducto, @idSucursal, @idUsuario, GETDATE())", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@elemento", elemento));
                        cmd.Parameters.Add(new SqlParameter("@idTipoProducto", idTipoProducto));
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
        public void actualizar(int idE, int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE elementos SET elemento = @elemento, idTipoProducto = @idTipoProducto, idSucursal = @idSucursal, idUsuarioMod = @idUsuarioMod, fechaMod = GETDATE() WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@elemento", elemento));
                        cmd.Parameters.Add(new SqlParameter("@idTipoProducto", idTipoProducto));
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioMod", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@id", idE));

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
        public void eliminar(int idE, int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE elementos SET eliminado=1, intUsuarioElimino=@idUsuarioElimino, fechaElimino=GETDATE() WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioElimino", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@id", idE));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public DataTable obtenerView()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT e.id, e.elemento, tp.tipo, s.nombre FROM elementos AS e INNER JOIN sucursales AS s ON e.idSucursal = s.id INNER JOIN " +
                         "tiposProductos AS tp ON e.idTipoProducto = tp.id WHERE (e.eliminado IS NULL) AND (e.idSucursal = @idSucursal)", conn))
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

        //metodo para el combobox de solicitudes
        public DataTable obtenerByTipoProductoAndIdSucursal()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM elementos WHERE idTipoProducto = @idTipoProducto AND idSucursal = @idSucursal AND eliminado IS NULL", conn))
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
        public DataTable obtenerByID(int idE)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM elementos WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idE));
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

        public void obtenerByIdE(int idE)
        {

            try
            {
                string comando = "SELECT * FROM elementos WHERE id = @id";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idE));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    elemento = reader["elemento"].ToString();
                                    idTipoProducto = int.Parse(reader["idTipoProducto"].ToString());
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
        public void actualizarEliminacion()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("update elementos set eliminado=null WHERE elemento = @elemento AND idTipoProducto = @idTipoProducto AND idSucursal = @idSucursal", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@elemento", elemento));
                        cmd.Parameters.Add(new SqlParameter("@idTipoProducto", idTipoProducto));
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
                        //cmd.Parameters.Add(new SqlParameter("@id", existe));
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
                    using (SqlCommand cmd = new SqlCommand("SELECT id FROM elementos WHERE elemento = @elemento AND idTipoProducto = @idTipoProducto AND idSucursal = @idSucursal", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@elemento", elemento));
                        cmd.Parameters.Add(new SqlParameter("@idTipoProducto", idTipoProducto));
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