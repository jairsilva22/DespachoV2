using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace despacho
{
    /// <summary>
    /// Summary description for Usuarios
    /// </summary>
    public class cUsuarios
    {
        //variables
        private string cadena;

        //propiedades
        public int id { get; set; }
        public string nombre { get; set; }
        public string usuario { get; set; }
        public string password { get; set; }
        public bool activo { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public int idTurno { get; set; }
        public int idPerfil { get; set; }
        public int idSucursal { get; set; }

        //Constructor
        public cUsuarios()
        {
            cadena = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;
        }

        //metodo para login
        public bool login(string user, string psswd, int Sucursal)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT usuarios.id, usuarios.idPerfil FROM usuarios JOIN perfiles ON idPerfil = perfiles.id WHERE usuario = @user AND password = @psswd AND idSucursal = @idSucursal AND usuarios.activo = 'True'", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@user", user));
                        cmd.Parameters.Add(new SqlParameter("@psswd", psswd));
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", Sucursal));

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    id = int.Parse(reader[0].ToString());
                                    usuario = user;
                                    idPerfil = int.Parse(reader[1].ToString());
                                    idSucursal = Sucursal;
                                }
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

        //metodo para insertar un nuevo usuario
        public void insertar(int idUsuarioActivo)
        {
            try
            {
                string comando = "INSERT INTO usuarios(nombre, usuario, password, activo, telefono, email, idTurno, idPerfil, idSucursal, idUsuario, fechaAlta)";
                comando += "VALUES(@nombre, @usuario, @password, 1, @telefono, @email, @idTurno, @idPerfil, @idSucursal, @idUsuario, GETDATE())";

                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                        cmd.Parameters.Add(new SqlParameter("@usuario", usuario));
                        cmd.Parameters.Add(new SqlParameter("@password", password));
                        cmd.Parameters.Add(new SqlParameter("@telefono", telefono));
                        cmd.Parameters.Add(new SqlParameter("@email", email));
                        cmd.Parameters.Add(new SqlParameter("@idTurno", idTurno));
                        cmd.Parameters.Add(new SqlParameter("@idPerfil", idPerfil));
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
                        cmd.Parameters.Add(new SqlParameter("@idUsuario", idUsuarioActivo));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metdodo para actualizar un usuario
        public void actualizar(int idU, int idUsuarioActivo)
        {
            try
            {
                string comando = "UPDATE usuarios SET nombre = @nombre, usuario = @usuario, password = @password, activo = @activo,";
                comando += " telefono = @telefono, email = @email, idPerfil = @idPerfil,";
                comando += " idUsuarioMod = @idUsuarioMod, fechaMod = GETDATE() WHERE Id = @id";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                        cmd.Parameters.Add(new SqlParameter("@usuario", usuario));
                        cmd.Parameters.Add(new SqlParameter("@password", password));
                        cmd.Parameters.Add(new SqlParameter("@activo", activo));
                        cmd.Parameters.Add(new SqlParameter("@telefono", telefono));
                        cmd.Parameters.Add(new SqlParameter("@email", email));
                        cmd.Parameters.Add(new SqlParameter("@idPerfil", idPerfil));
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioMod", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@id", idU));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {

                throw (ex);
            }
        }

        //metodo para eliminar un usuario
        public void eliminar(int idU, int idUsuarioActivo)
        {
            try
            {
                string comando = "UPDATE usuarios SET activo=0, eliminado=1, idUsuarioElimino=@idUsuarioElimino, fechaElimino=GETDATE() WHERE id = @id";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioElimino", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@id", idU));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {

                throw (ex);
            }
        }

        //metodo para buscar un usuario
        public void obtenerUsuario(int idU)
        {
            try
            {
                string comando = "SELECT * FROM usuarios WHERE id = @id";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idU));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    nombre = reader["nombre"].ToString();
                                    usuario = reader["usuario"].ToString();
                                    password = reader["password"].ToString();
                                    activo = bool.Parse(reader["activo"].ToString());
                                    telefono = reader["telefono"].ToString();
                                    email = reader["email"].ToString();
                                    idTurno = int.Parse(reader["idTurno"].ToString());
                                    idPerfil = int.Parse(reader["idPerfil"].ToString());
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

        public DataTable obtenerUsuarios()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.usuarios ORDER BY Nombre ASC", conn))
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

        //metodos para buscar todos los usuarios
        public DataTable users(string param)
        {
            try
            {
                string comando = "SELECT users.id AS id, nombre, telefono, categoria.categoria, puesto.puesto, email, fechaAlta, activo, foto";
                comando += " FROM dbo.users JOIN categoria ON idCat = users.categoria JOIN puesto ON puesto.id = users.puesto WHERE users.Id > 0" + param + " ORDER BY Id DESC";
                DataTable dt = new DataTable();
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

        public DataTable users()
        {
            try
            {
                string comando = "SELECT id, Nombre FROM users ORDER BY Nombre";

                DataTable dt = new DataTable();
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

        public DataTable obtenerUsuariosActivosBySucursal(int idSucursal)
        {
            try
            {
                string comando = "SELECT * FROM usuarios ORDER BY nombre WHERE idSucursal = @idSucursal AND eliminado IS NULL AND activo=1";

                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
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
        public DataTable obtenerUsuarioByUsuario(string sUsr)
        {
            try
            {
                string comando = "SELECT * FROM usuarios WHERE usuario = @usuario";

                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@usuario", sUsr));
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
        public DataTable obtenerUsuarioByNombre(string sNombre)
        {
            try
            {
                string comando = "SELECT * FROM usuarios WHERE nombre = @nombre";

                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@nombre", sNombre));
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
        //Método para llenar la vista de usuarios
        public DataTable obtenerUsuariosBySucursal(int idSucursal)
        {
            try
            {
                string comando = "SELECT u.id, u.usuario, u.nombre, t.turno, p.descripcion AS perfil, u.activo FROM usuarios AS u INNER JOIN " +
                    "perfiles AS p ON u.idPerfil = p.id INNER JOIN turnos AS T ON u.idTurno = t.id WHERE u.idSucursal = @idSucursal AND u.eliminado IS NULL";

                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
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
        public DataTable obtenerUsuarioByID(int idU)
        {
            try
            {
                string comando = "SELECT u.nombre, p.descripcion, su.nombre AS sucursal FROM usuarios AS u INNER JOIN perfiles AS p ON u.idPerfil = p.id INNER JOIN sucursales AS su ON u.idSucursal = su.id WHERE(u.id = @id)";

                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idU));
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
        public DataTable obtenerUsuariosByPefil(int idPerfil)
        {
            try
            {
                string comando = "SELECT id, nombre FROM usuarios ORDER BY nombre WHERE idPerfil = @idPerfil";

                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idPerfil", usuario));
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

        public DataTable obtenerUsuariosByPefilAndNombre(int Perfil, int Sucursal, string Nombre)
        {
            try
            {
                string comando = "SELECT id, nombre FROM usuarios WHERE (idPerfil = @idPerfil) AND (idSucursal = @idSurcursal) AND (nombre LIKE '%@Nombre%') ORDER BY nombre";

                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idPerfil", Perfil));
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", Sucursal));
                        cmd.Parameters.Add(new SqlParameter("@Nombre", Nombre));
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
        public DataTable obtenerUsuariosActivosByPefilAndSucursal(int idPerfil, int idSucursal)
        {
            try
            {
                //string comando = "SELECT id, nombre, activo, idPerfil, idSucursal FROM usuarios WHERE (activo = 1) AND (idPerfil = @idPerfil) AND (idSucursal = @idSucursal)";
                string comando = "SELECT u.id, u.usuario, u.nombre, t.turno, p.descripcion AS perfil, u.activo, u.idPerfil, u.idSucursal FROM usuarios AS u INNER JOIN " +
                    "perfiles AS p ON u.idPerfil = p.id INNER JOIN turnos AS T ON u.idTurno = t.id WHERE u.activo = 1 AND u.idPerfil = @idPerfil AND u.idSucursal = @idSucursal AND u.eliminado IS NULL";
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idPerfil", idPerfil));
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
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

        public DataTable obtenerChoferesActivosByPefilAndSucursal(int idPerfil, int idSucursal)
        {
            try
            {
                string comando = "SELECT u.id, u.usuario, u.nombre, T.turno, p.descripcion AS perfil, u.activo, u.idPerfil, u.idSucursal " +
                    "FROM usuarios AS u INNER JOIN perfiles AS p ON u.idPerfil = p.id INNER JOIN turnos AS T ON u.idTurno = T.id " +
                    "WHERE u.activo = 1 AND u.idPerfil = @idPerfil AND u.idSucursal = @idSucursal AND u.eliminado IS NULL";
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idPerfil", idPerfil));
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
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

        public DataTable obtenerChoferesOfUTC(int idSucursal)
        {
            try
            {
                string comando = "SELECT ut.id, ch.usuario, ch.nombre, p.descripcion AS perfil, turnos.turno, utc.activo, ut.nombre AS unidad " +
                    "FROM unidadesTChoferes AS utc INNER JOIN usuarios AS ch ON utc.idChofer = ch.id INNER JOIN unidadesTransporte AS ut ON utc.idUnidad = ut.id INNER JOIN " +
                    "perfiles AS p ON ch.idPerfil = p.id INNER JOIN turnos ON ch.idTurno = turnos.id WHERE utc.idSucursal = @idSucursal AND ch.activo = 1 " +
                    "AND utc.eliminada IS NULL ORDER BY ut.id";
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
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
        public DataTable obtenerChoferesActivosBySucursal(int idSucursal)
        {
            try
            {
                string comando = "SELECT * FROM usuarios WHERE idPerfil = 1003 AND eliminado IS NULL AND idSucursal = @idSucursal";
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
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
        //metodo para saber si ya existe el usuario registrado
        public bool existeUsuario(string user)
        {
            try
            {
                string comando = "SELECT Id FROM usuarios WHERE usuario = @user AND idSucursal = @idSucursal";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@user", user));
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

        //metodo para saber si ya se registró el nombre del usuario
        public bool existeNombreUsuario()
        {
            try
            {
                string comando = "SELECT Id FROM users WHERE Nombre = @nombre";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
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

        //metodo para modificar el perfil del usuario
        public void modificarPerfil(int idUsuarioActivo)
        {
            try
            {
                string comando = "UPDATE usuarios SET usuario = @usuario, password = @password, idPerfil = @idPerfil email = @email, idUsuarioMod = @idUsuarioMod, fechaMod = GETDATE()";
                comando += " WHERE Id = @id";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@usuario", usuario));
                        cmd.Parameters.Add(new SqlParameter("@password", password));
                        cmd.Parameters.Add(new SqlParameter("@idPerfil", idPerfil));
                        cmd.Parameters.Add(new SqlParameter("@email", email));
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

        public string obtenerNombreUsuario(int idUsr)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT nombre FROM usuarios WHERE id = @idUser", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idUser", idUsr));
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
        public DataTable obtenerVendedoresActivos()
        {
            try
            {
                string comando = "SELECT id, nombre FROM usuarios WHERE idPerfil = 2 AND eliminado is null";

                DataTable dt = new DataTable();
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

        public DataTable obtenerVendedoresActivosBySuc(int idSuc)
        {
            try
            {
                string comando = "SELECT id, nombre FROM usuarios WHERE idPerfil = 2 AND eliminado is null AND idSucursal = @idSuc ORDER BY nombre";

                DataTable dt = new DataTable();
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
        public DataTable obtenerVendedoresBySuc(int idSuc)
        {
            try
            {
                string comando = "SELECT id, nombre FROM usuarios WHERE idPerfil = 2 AND idSucursal = @idSuc ORDER BY id";

                DataTable dt = new DataTable();
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
    }
}