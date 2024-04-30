using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace despacho
{
    public class cPrivilegios
    {
        cMenu cM = new cMenu();
        //variables
        private string cadena;
        private string tabla = "privilegiosDespacho";

        //propiedades
        public int id { get; set; }
        public int idPerfil { get; set; }
        public int idMenu { get; set; }
        public int idSucursal { get; set; }
        public bool activo { get; set; }


        //Constructor
        public cPrivilegios()
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
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO " + tabla + " (idPerfil, idMenu, idSucursal, activo, idUsuario, fechaAlta) " +
                        "VALUES(@idPerfil, @idMenu, @idSucursal, @activo, @idUsuario, GETDATE())", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idPerfil", idPerfil));
                        cmd.Parameters.Add(new SqlParameter("@idMenu", idMenu));
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
                        cmd.Parameters.Add(new SqlParameter("@activo", activo));
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
                    using (SqlCommand cmd = new SqlCommand("UPDATE " + tabla + " SET idPerfil = @idPerfil, idMenu = @idMenu, idSucursal = @idSucursal, activo = @activo, " +
                        "idUsuarioMod = @idUsuarioMod, fechaMod = GETDATE() WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idPerfil", idPerfil));
                        cmd.Parameters.Add(new SqlParameter("@idMenu", idMenu));
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
                        cmd.Parameters.Add(new SqlParameter("@activo", activo));
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


        public DataTable obtenerView()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    string sCmd = "SELECT m.id AS idM, m.etiqueta, m.url, m.icono, m.idMenuPadre, m.orden, pr.id AS idPr, pr.activo FROM menuDespacho AS m LEFT OUTER JOIN privilegiosDespacho AS pr ON m.id = pr.idMenu " +
                        "WHERE(m.activo = 1) AND (pr.idPerfil = @idPerfil) ORDER BY m.id, m.idMenuPadre";
                    using (SqlCommand cmd = new SqlCommand(sCmd, conn))
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
        //metodo para actualizar 
        public void setActivo(bool value, int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE " + tabla + " SET activo = @activo, " +
                        "idUsuarioMod = @idUsuarioMod, fechaMod = GETDATE() WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@activo", value));
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
        //metodo para actualizar todos
        public void setActivoAll(bool value, int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE " + tabla + " SET activo = @activo, " +
                        "idUsuarioMod = @idUsuarioMod, fechaMod = GETDATE() WHERE idPerfil = @idPerfil AND idSucursal = @idSucursal", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@activo", value));
                        cmd.Parameters.Add(new SqlParameter("@idPerfil", idPerfil));
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

        //metodo para obtener el Menu completo con los privilegios del Perfil
        public DataTable obtenerMenuWithPrivilegios()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    string sCmd = "SELECT m.id AS idM, m.etiqueta, m.url, m.icono, m.idMenuPadre, m.orden, pr.id AS idPr, pr.activo FROM menuDespacho AS m LEFT OUTER JOIN privilegiosDespacho AS pr ON m.id = pr.idMenu " +
                        "WHERE(m.activo = 1) AND (pr.idPerfil = @idPerfil) AND (pr.idSucursal = @idSucursal) ORDER BY m.id, m.idMenuPadre";
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

        public bool crearPrivilegios(int idUsuarioActivo)
        {
            try
            {
                DataTable dtM = cM.obtenerMenu();
                foreach (DataRow drM in dtM.Rows)
                {
                    idMenu = int.Parse(drM["id"].ToString());
                    if (!existe())
                    {
                        activo = true;
                        insertar(idUsuarioActivo);
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
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
                    using (SqlCommand cmd = new SqlCommand("SELECT id FROM " + tabla + " WHERE idPerfil = @idPerfil AND idMenu = @idMenu AND idSucursal = @idSucursal", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idPerfil", idPerfil));
                        cmd.Parameters.Add(new SqlParameter("@idMenu", idMenu));
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