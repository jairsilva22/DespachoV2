using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace despacho
{
    public class cMenu
    {
        //variables
        private string cadena;
        private string tabla = "menuDespacho";

        //propiedades
        public int id { get; set; }
        public string etiqueta { get; set; }
        public string url { get; set; }
        public string icono { get; set; }
        public string nodoPadre { get; set; }
        public string nodo { get; set; }
        public int activo { get; set; }
        public int idSucursal { get; set; }
        public int idPerfil { get; set; }


        //Constructor
        public cMenu()
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
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO " + tabla + " (etiqueta, url, icono, nodoPadre, nodo, activo, idSucursal, idUsuario, fechaAlta) " +
                        "VALUES(@etiqueta, @url, @icono, @nodoPadre, @nodo, @activo, @idSucursal, @idUsuario, GETDATE())", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@etiqueta", etiqueta));
                        cmd.Parameters.Add(new SqlParameter("@url", url));
                        cmd.Parameters.Add(new SqlParameter("@icono", icono));
                        cmd.Parameters.Add(new SqlParameter("@nodoPadre", nodoPadre));
                        cmd.Parameters.Add(new SqlParameter("@nodo", nodo));
                        cmd.Parameters.Add(new SqlParameter("@activo", activo));
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
                    using (SqlCommand cmd = new SqlCommand("UPDATE " + tabla + " SET etiqueta = @etiqueta, url = @url, icono = @icono, nodoPadre = @nodoPadre, nodo = @nodo, activo = @activo, " +
                        "idSucursal = @idSucursal, idUsuarioMod = @idUsuarioMod, fechaMod = GETDATE() WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@etiqueta", etiqueta));
                        cmd.Parameters.Add(new SqlParameter("@url", url));
                        cmd.Parameters.Add(new SqlParameter("@icono", icono));
                        cmd.Parameters.Add(new SqlParameter("@nodoPadre", nodoPadre));
                        cmd.Parameters.Add(new SqlParameter("@nodo", nodo));
                        cmd.Parameters.Add(new SqlParameter("@activo", activo));
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
                    using (SqlCommand cmd = new SqlCommand("UPDATE " + tabla + " SET eliminado=1, idUsuarioElimino=@idUsuarioElimino, fechaElimino=GETDATE() WHERE id = @id", conn))
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
                    using (SqlCommand cmd = new SqlCommand("SELECT m.etiqueta, m.url, m.icono, m.nodoPadre, m.nodo, m.activo, s.nombre AS sucursal FROM " + tabla + " AS m INNER JOIN " +
                        "sucursales AS s ON m.idSucursal = s.id WHERE (m.eliminado IS NULL) AND (m.idSucursal = @idSucursal)", conn))
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
                        //cmd.Parameters.Add(new SqlParameter("@idTipoProducto", idTipoProducto));
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
        public DataTable obtenerByIdPerfil(int idPadre = 0)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    string sCmd = "SELECT m.id, p.descripcion, m.etiqueta, m.url, m.icono, m.idMenuPadre, m.orden, s.nombre, pr.activo FROM perfiles AS p INNER JOIN privilegiosDespacho AS pr ON p.id = pr.idPerfil INNER JOIN " +
                        "menuDespacho AS m ON pr.idMenu = m.id INNER JOIN sucursales AS s ON pr.idSucursal = s.id WHERE m.eliminado IS NULL AND pr.idSucursal = @idSucursal AND pr.idPerfil = @idPerfil";
                    sCmd += " AND m.idMenuPadre=@idMenuPadre";
                    sCmd += " ORDER BY m.orden";
                    using (SqlCommand cmd = new SqlCommand(sCmd, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idPerfil", idPerfil));
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
                        cmd.Parameters.Add(new SqlParameter("@idMenuPadre", idPadre));
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


        public DataTable obtenerByIdPerfilReportes(int idPadre = 0)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    string sCmd = "SELECT m.id, p.descripcion, m.etiqueta, m.url, m.icono, m.idMenuPadre, m.orden, s.nombre, pr.activo FROM perfiles AS p INNER JOIN privilegiosDespacho AS pr ON p.id = pr.idPerfil INNER JOIN " +
                        "menuDespacho AS m ON pr.idMenu = m.id INNER JOIN sucursales AS s ON pr.idSucursal = s.id WHERE m.eliminado IS NULL AND pr.idSucursal = @idSucursal AND pr.idPerfil = @idPerfil";
                    sCmd += " AND m.idMenuPadre=@idMenuPadre";
                    sCmd += " ORDER BY m.orden";
                    using (SqlCommand cmd = new SqlCommand(sCmd, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idPerfil", idPerfil));
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
                        cmd.Parameters.Add(new SqlParameter("@idMenuPadre", idPadre));
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

        public DataTable obtenerTiposReportes(int idPadre = 0)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    string sCmd = "SELECT DISTINCT tipo, orderTipo FROM perfiles AS p INNER JOIN privilegiosDespacho AS pr ON p.id = pr.idPerfil INNER JOIN " +
                        "menuDespacho AS m ON pr.idMenu = m.id INNER JOIN sucursales AS s ON pr.idSucursal = s.id WHERE m.eliminado IS NULL AND pr.idSucursal = @idSucursal AND pr.idPerfil = @idPerfil";
                    sCmd += " AND m.idMenuPadre=@idMenuPadre ORDER BY orderTipo";
                    using (SqlCommand cmd = new SqlCommand(sCmd, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idPerfil", idPerfil));
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
                        cmd.Parameters.Add(new SqlParameter("@idMenuPadre", idPadre));
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

        public DataTable obtenerMenuReportesByTipo(int idPadre = 0, string tipo = "")
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    string sCmd = "SELECT m.id, p.descripcion, m.etiqueta, m.url, m.icono, m.idMenuPadre, m.orden, s.nombre, pr.activo  FROM perfiles AS p INNER JOIN privilegiosDespacho AS pr ON p.id = pr.idPerfil INNER JOIN " +
                        "menuDespacho AS m ON pr.idMenu = m.id INNER JOIN sucursales AS s ON pr.idSucursal = s.id WHERE m.eliminado IS NULL AND pr.idSucursal = @idSucursal AND pr.idPerfil = @idPerfil";
                    sCmd += " AND m.idMenuPadre=@idMenuPadre AND tipo = @tipo";
                    sCmd += " ORDER BY m.orden";
                    using (SqlCommand cmd = new SqlCommand(sCmd, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idPerfil", idPerfil));
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
                        cmd.Parameters.Add(new SqlParameter("@idMenuPadre", idPadre));
                        cmd.Parameters.Add(new SqlParameter("@tipo", tipo));
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

        //metodo para obtener el Menu completo
        public DataTable obtenerMenu()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    string sCmd = "SELECT * FROM menuDespacho WHERE(activo = 1)";
                    using (SqlCommand cmd = new SqlCommand(sCmd, conn))
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
                                    //elemento = reader["elemento"].ToString();
                                    //idTipoProducto = int.Parse(reader["idTipoProducto"].ToString());
                                    idSucursal = int.Parse(reader["idSucursal"].ToString());
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception )
            {

            }
        }
        public int countMenu()
        {

            try
            {
                string comando = "SELECT count(*) as total FROM menuDespacho WHERE activo=1 AND eliminado IS NULL";
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
                                    return int.Parse(reader["total"].ToString());
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
        public bool existe()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT id FROM elementos WHERE elemento = @elemento AND idTipoProducto = @idTipoProducto AND idSucursal = @idSucursal", conn))
                    {
                        //cmd.Parameters.Add(new SqlParameter("@elemento", elemento));
                        //cmd.Parameters.Add(new SqlParameter("@idTipoProducto", idTipoProducto));
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

        public DataTable obtenerTipoCapacitacion()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    string sCmd = "SELECT DISTINCT tipo, ordenTipo FROM perfiles AS p INNER JOIN privilegiosDespacho AS pr ON p.id = pr.idPerfil INNER JOIN " +
                        "menuCapacitaciones AS m ON pr.idMenu = m.id INNER JOIN sucursales AS s ON pr.idSucursal = s.id WHERE m.eliminado IS NULL AND pr.idSucursal = @idSucursal AND pr.idPerfil = @idPerfil";
                    sCmd += " ORDER BY ordenTipo";
                    using (SqlCommand cmd = new SqlCommand(sCmd, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idPerfil", idPerfil));
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

        public DataTable obtenerMenuCapacitacionByTipo(string tipo = "")
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    string sCmd = "SELECT m.id, p.descripcion, m.etiqueta, m.url, m.orden, s.nombre, pr.activo  FROM perfiles AS p INNER JOIN privilegiosDespacho AS pr ON p.id = pr.idPerfil INNER JOIN " +
                        "menuCapacitaciones AS m ON pr.idMenu = m.id INNER JOIN sucursales AS s ON pr.idSucursal = s.id WHERE m.eliminado IS NULL AND pr.idSucursal = @idSucursal AND pr.idPerfil = @idPerfil";
                    sCmd += " AND tipo = @tipo";
                    sCmd += " ORDER BY m.orden";
                    using (SqlCommand cmd = new SqlCommand(sCmd, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idPerfil", idPerfil));
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
                        cmd.Parameters.Add(new SqlParameter("@tipo", tipo));
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
    }
}