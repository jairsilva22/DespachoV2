using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace despacho
{
    public class cSucursales
    {
        //variables
        private string cadena;
        private string cadenaCompras;
        string comando = string.Empty;

        //propiedades
        public int id { get; set; }
        public string nombre { get; set; }
        public string razon { get; set; }
        public string codigo { get; set; }
        public int idPais { get; set; }
        public int idCiudad { get; set; }
        public int idEstado { get; set; }
        public int cp { get; set; }
        public string colonia { get; set; }
        public string calle { get; set; }
        public int numero { get; set; }
        public string interior { get; set; }
        public int idSucursal { get; set; }
        public string rfc { get; set; }
        public int regimenFiscal { get; set; }
        public string nombreCer { get; set; }
        public string nombreKey { get; set; }
        public string logo { get; set; }
        public string ncarpeta { get; set; }
        public string carpetaTimbre { get; set; }
        public string ancho { get; set; }
        public string alto { get; set; }
        public string contraArchivos { get; set; }
        public int iva { get; set; }
        public string path { get; set; }
        public string pathUtilerias { get; set; }
        public string smtp { get; set; }
        public string correo { get; set; }
        public string pass { get; set; }
        public string cad { get; set; }
        public string comisionDirecto { get; set; }
        public string comisionIndirecto { get; set; }
        public string observacionesCot { get; set; }
        public string responsabilidadRem { get; set; }
        public string responsabilidadRemB { get; set; }
        public bool simulacion { get; set; }
        public int idSucursalCompras { get; set; }


        //Constructor
        public cSucursales()
        {
            cadena = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;
            //cadenaCompras = ConfigurationManager.ConnectionStrings["cnxCompras"].ConnectionString;
        }

        //metodo para insertar
        public void insertar(int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    //using (SqlCommand cmd = new SqlCommand("INSERT INTO sucursales(nombre, codigo, razon, idCiudad, idEstado, cp, colonia, calle, numero, interior, idUsuario, fechaAlta, rfc, " +
                    //    "c_RegimenFiscal, nombrecer, nombrekey, logo, ancho, alto, NCarpeta, carpetaTimbre, contrasenaArchivos, idPais, comisionDirecto, comisionIndirecto, observacionesCot, responsabilidadRem, responsabilidadRemB, simulacion) " +
                    //    "VALUES(@nombre, @codigo, @razon, @idCiudad, @idEstado, @cp, @colonia, @calle, @numero, @interior, @idUsuario, GETDATE(), @rfc, @regimenFiscal, @nombreCer, @nombreKey,@logo, @ancho, @alto," +
                    //    "@ncarpeta, @carpetatimbre, @contraArchivos, @idPais, @comisionDirecto, @comisionIndirecto, @observacionesCot, @responsabilidadRem, @responsabilidadRemB, @simulacion); SELECT SCOPE_IDENTITY() as id", conn))
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO sucursales(nombre, codigo, razon, idCiudad, idEstado, cp, colonia, calle, numero, interior, idUsuario, fechaAlta, rfc, " +
                        "c_RegimenFiscal, logo, ancho, alto, idPais, comisionDirecto, comisionIndirecto, observacionesCot, responsabilidadRem, responsabilidadRemB, simulacion, idSucursalCompras) " +
                        "VALUES(@nombre, @codigo, @razon, @idCiudad, @idEstado, @cp, @colonia, @calle, @numero, @interior, @idUsuario, GETDATE(), @rfc, @regimenFiscal, @logo, @ancho, @alto," +
                        "@idPais, @comisionDirecto, @comisionIndirecto, @observacionesCot, @responsabilidadRem, @responsabilidadRemB, @simulacion, @idSucursalCompras); SELECT SCOPE_IDENTITY() as id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                        cmd.Parameters.Add(new SqlParameter("@codigo", codigo));
                        cmd.Parameters.Add(new SqlParameter("@razon", razon));
                        cmd.Parameters.Add(new SqlParameter("@idCiudad", idCiudad));
                        cmd.Parameters.Add(new SqlParameter("@idPais", idPais));
                        cmd.Parameters.Add(new SqlParameter("@idEstado", idEstado));
                        cmd.Parameters.Add(new SqlParameter("@cp", cp));
                        cmd.Parameters.Add(new SqlParameter("@colonia", colonia));
                        cmd.Parameters.Add(new SqlParameter("@calle", calle));
                        cmd.Parameters.Add(new SqlParameter("@numero", numero));
                        cmd.Parameters.Add(new SqlParameter("@interior", interior));
                        cmd.Parameters.Add(new SqlParameter("@idUsuario", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@rfc", rfc));
                        cmd.Parameters.Add(new SqlParameter("@regimenFiscal", regimenFiscal));
                        //cmd.Parameters.Add(new SqlParameter("@nombreCer", nombreCer));
                        //cmd.Parameters.Add(new SqlParameter("@nombreKey", nombreKey));
                        cmd.Parameters.Add(new SqlParameter("@logo", logo));
                        cmd.Parameters.Add(new SqlParameter("@ancho", ancho));
                        cmd.Parameters.Add(new SqlParameter("@alto", alto));
                        //cmd.Parameters.Add(new SqlParameter("@ncarpeta", ncarpeta));
                        //cmd.Parameters.Add(new SqlParameter("@carpetatimbre", carpetaTimbre));
                        //cmd.Parameters.Add(new SqlParameter("@contraArchivos", contraArchivos));
                        cmd.Parameters.Add(new SqlParameter("@comisionDirecto", comisionDirecto));
                        cmd.Parameters.Add(new SqlParameter("@comisionIndirecto", comisionIndirecto));
                        cmd.Parameters.Add(new SqlParameter("@observacionesCot", observacionesCot));
                        cmd.Parameters.Add(new SqlParameter("@responsabilidadRem", responsabilidadRem));
                        cmd.Parameters.Add(new SqlParameter("@responsabilidadRemB", responsabilidadRemB));
                        cmd.Parameters.Add(new SqlParameter("@simulacion", simulacion));
                        cmd.Parameters.Add(new SqlParameter("@idSucursalCompras", idSucursalCompras));

                        //int filas = cmd.ExecuteNonQuery();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    id = int.Parse(reader["id"].ToString());
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

        public void insertarConfig(int idSuc)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO configMenor(iva, path, idempresa, Path_arch_utiles, smtp, correo, password, cadena) " +
                        "VALUES(@iva, @path, @idempresa, @pathUtilidades, @smtp, @correo, @pass, @cadena)", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@iva", iva));
                        cmd.Parameters.Add(new SqlParameter("@path", path));
                        cmd.Parameters.Add(new SqlParameter("@idempresa", idSuc));
                        cmd.Parameters.Add(new SqlParameter("@pathUtilidades", pathUtilerias));
                        cmd.Parameters.Add(new SqlParameter("@smtp", smtp));
                        cmd.Parameters.Add(new SqlParameter("@correo", correo));
                        cmd.Parameters.Add(new SqlParameter("@pass", pass));
                        cmd.Parameters.Add(new SqlParameter("@cadena", cad));

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
        public void actualizar(int idS, int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE sucursales SET nombre = @nombre, razon = @razon, idCiudad = @idCiudad, idEstado = @idEstado, cp = @cp, colonia = @colonia, calle = @calle, " +
                        "numero = @numero, interior = @interior, idUsuarioMod = @idUsuarioMod, fechaMod = GETDATE(), rfc = @rfc, c_RegimenFiscal = @regimenfiscal, " +
                        " logo = @logo, ancho = @ancho, alto = @alto, comisionDirecto = @comisionDirecto, " +
                        "comisionIndirecto = @comisionIndirecto, observacionesCot = @observacionesCot, responsabilidadRem = @responsabilidadRem, responsabilidadRemB = @responsabilidadRemB, simulacion = @simulacion, " +
                        " idSucursalCompras = @idSucursalCompras WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                        cmd.Parameters.Add(new SqlParameter("@razon", razon));
                        cmd.Parameters.Add(new SqlParameter("@idCiudad", idCiudad));
                        cmd.Parameters.Add(new SqlParameter("@idEstado", idEstado));
                        cmd.Parameters.Add(new SqlParameter("@cp", cp));
                        cmd.Parameters.Add(new SqlParameter("@colonia", colonia));
                        cmd.Parameters.Add(new SqlParameter("@calle", calle));
                        cmd.Parameters.Add(new SqlParameter("@numero", numero));
                        cmd.Parameters.Add(new SqlParameter("@interior", interior));
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioMod", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@id", idS));
                        cmd.Parameters.Add(new SqlParameter("@rfc", rfc));
                        cmd.Parameters.Add(new SqlParameter("@regimenFiscal", regimenFiscal));
                        cmd.Parameters.Add(new SqlParameter("@logo", logo));
                        cmd.Parameters.Add(new SqlParameter("@ancho", ancho));
                        cmd.Parameters.Add(new SqlParameter("@alto", alto));
                        //cmd.Parameters.Add(new SqlParameter("@ncarpeta", ncarpeta));
                        //cmd.Parameters.Add(new SqlParameter("@carpetatimbre", carpetaTimbre));
                        //cmd.Parameters.Add(new SqlParameter("@contraArchivos", contraArchivos));
                        cmd.Parameters.Add(new SqlParameter("@comisionDirecto", comisionDirecto));
                        cmd.Parameters.Add(new SqlParameter("@comisionIndirecto", comisionIndirecto));
                        cmd.Parameters.Add(new SqlParameter("@observacionesCot", observacionesCot));
                        cmd.Parameters.Add(new SqlParameter("@responsabilidadRem", responsabilidadRem));
                        cmd.Parameters.Add(new SqlParameter("@responsabilidadRemB", responsabilidadRemB));
                        cmd.Parameters.Add(new SqlParameter("@simulacion", simulacion));
                        cmd.Parameters.Add(new SqlParameter("@idSucursalCompras", idSucursalCompras));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public void actualizarConfig(int idS, int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE configmenor SET iva = @iva, correo = @correo, password = @pass " +
                        " WHERE idempresa = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@iva", iva));
                        cmd.Parameters.Add(new SqlParameter("@path", path));
                        cmd.Parameters.Add(new SqlParameter("@idempresa", idS));
                        cmd.Parameters.Add(new SqlParameter("@pathUtilidades", pathUtilerias));
                        cmd.Parameters.Add(new SqlParameter("@smtp", smtp));
                        cmd.Parameters.Add(new SqlParameter("@correo", correo));
                        cmd.Parameters.Add(new SqlParameter("@pass", pass));
                        cmd.Parameters.Add(new SqlParameter("@cadena", cad));
                        cmd.Parameters.Add(new SqlParameter("@id", idS));

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
        public void eliminar(int idS, int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE sucursales SET elimino = 1, idUsuarioElimino = @idUsuarioElimino, fechaElimino= GETDATE() WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioElimino", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@id", idS));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        //metodo para el combobox
        public DataTable obtenerSucursal(int idS)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM sucursales WHERE id = @id AND elimino IS NULL", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idS));
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
        public DataTable obtenerConfig(int idS)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM configmenor WHERE idempresa = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idS));
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

        //metodo para el combobox
        public DataTable obtenerSucursales()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT id, nombre FROM sucursales WHERE elimino IS NULL", conn))
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                        return dt;
                    }
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        //metodo para el combobox
        public DataTable obtenerSucursalesCompras()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadenaCompras))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT idpy AS id, proyecto AS nombre FROM ksroc.proyecto WHERE activo = 1", conn))
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                        return dt;
                    }
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        //metodo para la vista de sucursales
        public DataTable obtenerSucursalesView()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT s.id, s.nombre, s.codigo, c.ciudad FROM sucursales AS s INNER JOIN " +
                        "ciudades AS c ON s.idCiudad = c.id WHERE(s.elimino IS NULL)", conn))
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                        return dt;
                    }
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metodo para obtener el nombre de la Sucursal por su ID
        public string obtenerNombreSucursalByID(int idS)
        {
            try
            {
                string comando = "SELECT nombre FROM sucursales WHERE id = @id";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idS));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    return reader["nombre"].ToString();
                                }
                            }
                        }
                    }
                    return "";
                }
            }
            catch (Exception ex)
            {
                return "";
                throw (ex);
            }
        }

        public DataTable obtenerSucursalByID(int idS)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT id, nombre FROM sucursales WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idS));
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
        //metodo para obtener el Código de la sucursal previo al alta de cliente
        public string obtenerCodigoSucursal(int idS)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT codigo FROM sucursales WHERE(id = @idSucursal)", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idS));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            return dt.Rows[0]["codigo"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metodo para obtener el idSucursal de compras by idSucursal dosificación DB
        public int obtenerComprasID(int idS)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT idSucursalCompras FROM sucursales WHERE(id = @idSucursal)", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idS));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            return int.Parse(dt.Rows[0]["idSucursalCompras"].ToString());
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
                    using (SqlCommand cmd = new SqlCommand("SELECT id FROM sucursales WHERE nombre = @nombre AND codigo = @codigo", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                        cmd.Parameters.Add(new SqlParameter("@codigo", codigo));
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

        public string obtenerComisionDirecto(int idS)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT comisionDirecto FROM sucursales WHERE id = @idSucursal", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idS));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            return dt.Rows[0]["comisionDirecto"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public string obtenerComisionIndirecto(int idS)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT comisionIndirecto FROM sucursales WHERE id = @idSucursal", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idS));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            return dt.Rows[0]["comisionIndirecto"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void buscarCarpetaTimbre()
        {
            try
            {
                comando = "SELECT CONCAT(path, carpetaTimbre) AS carpetaTimbre FROM sucursales JOIN configmenor ON idempresa = id WHERE id = @id";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idSucursal));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    carpetaTimbre = reader["carpetaTimbre"].ToString();
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
    }
}