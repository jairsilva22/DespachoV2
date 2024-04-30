using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace despacho
{
    public class cTipoUT
    {
        //variables
        private string cadena;

        //propiedades
        public int id { get; set; }
        public string tipo { get; set; }
        public float capacidad { get; set; }
        public int idUDM { get; set; }
        public int idTipoProducto { get; set; }
        public bool carga { get; set; }
        public float peso { get; set; }
        public int idSucursal { get; set; }


        //Constructor
        public cTipoUT()
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
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO tiposUnidadTransporte(tipo, capacidad, idUDM, idTipoProducto, carga, peso, idSucursal, idUsuario, fechaAlta) " +
                        "VALUES(@tipo, @capacidad, @idUDM, @idTipoProducto, @carga, @peso, @idSucursal, @idUsuario, GETDATE())", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@tipo", tipo));
                        cmd.Parameters.Add(new SqlParameter("@capacidad", capacidad));
                        cmd.Parameters.Add(new SqlParameter("@idUDM", idUDM));
                        cmd.Parameters.Add(new SqlParameter("@idTipoProducto", idTipoProducto));
                        cmd.Parameters.Add(new SqlParameter("@carga", carga));
                        cmd.Parameters.Add(new SqlParameter("@peso", peso.ToString("0.00")));
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
        public void actualizar(int idUT, int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE tiposUnidadTransporte SET tipo = @tipo, capacidad = @capacidad, idUDM = @idUDM, idTipoProducto = @idTipoProducto, "+
                        "carga = @carga, peso = @peso, idUsuarioMod = @idUsuarioMod, fechaMod = GETDATE() WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@tipo", tipo));
                        cmd.Parameters.Add(new SqlParameter("@capacidad", capacidad));
                        cmd.Parameters.Add(new SqlParameter("@idUDM", idUDM));
                        cmd.Parameters.Add(new SqlParameter("@idTipoProducto", idTipoProducto));
                        cmd.Parameters.Add(new SqlParameter("@carga", carga));
                        cmd.Parameters.Add(new SqlParameter("@peso", peso.ToString("0.00")));
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
        public void eliminar(int idUT, int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE tiposUnidadTransporte SET eliminada=1, idUsuarioElimino=@idUsuarioElimino, fechaElimino=GETDATE() WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioElimino", idUsuarioActivo));
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

        //metodo para el combobox de programacion
        public DataTable obtenerUT()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT tut.id, tut.tipo, tut.capacidad, udm.unidad, tut.carga, tut.peso FROM tiposUnidadTransporte AS tut INNER JOIN " +
                         "unidadesDeMedida AS udm ON tut.idUDM = udm.id WHERE (idSucursal = @idSucursal) AND (tut.eliminada IS NULL)", conn))
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

        //metodo para el ListView de TiposUT
        public DataTable obtenerTiposUT()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT tut.id, tut.tipo, tut.capacidad, udm.unidad, tp.tipo AS tipoProducto, tut.carga, tut.peso FROM tiposUnidadTransporte AS tut INNER JOIN " +
                        "unidadesDeMedida AS udm ON tut.idUDM = udm.id INNER JOIN tiposProductos AS tp ON tut.idTipoProducto = tp.id WHERE (tut.eliminada IS NULL)", conn))
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

        //metodo para el ListView de TiposUT
        public DataTable obtenerTiposUTByID(int idUT)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM tiposUnidadTransporte WHERE id = @id AND eliminada IS NULL", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idUT));
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

        //metodo para el ListView de TiposUT
        public DataTable obtenerTiposUTBySucursal()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT tut.id, tut.tipo, tut.capacidad, udm.unidad, tp.tipo AS tipoProducto, tut.carga, tut.peso FROM tiposUnidadTransporte AS tut INNER JOIN " +
                        "unidadesDeMedida AS udm ON tut.idUDM = udm.id INNER JOIN tiposProductos AS tp ON tut.idTipoProducto = tp.id WHERE (tut.eliminada IS NULL) AND (tut.idSucursal = @idS)", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idS", idSucursal));
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

        //metodo para el combobox de programacion
        public DataTable obtenerUTByIdTipoProducto(int idTP)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT tut.id, tut.tipo, tut.capacidad, tut.idTipoProducto, udm.unidad FROM tiposUnidadTransporte AS tut INNER JOIN " +
                         "unidadesDeMedida AS udm ON tut.idUDM = udm.id WHERE(tut.idTipoProducto = @idTipoProducto)", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idTipoProducto", idTP));
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

        //metodo para el combobox de programacion
        public DataTable obtenerUTByIdTipoProductoAndIsCarga(int idTP)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT tut.id, tut.tipo, tut.capacidad, tut.peso, tut.idTipoProducto, udm.unidad FROM tiposUnidadTransporte AS tut INNER JOIN " +
                         "unidadesDeMedida AS udm ON tut.idUDM = udm.id WHERE (tut.idTipoProducto = @idTipoProducto) AND (tut.idSucursal=@idSucursal)", conn))
                        //"unidadesDeMedida AS udm ON tut.idUDM = udm.id WHERE (tut.idTipoProducto = @idTipoProducto) AND (tut.carga = 1) AND (tut.idSucursal=@idSucursal)", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idTipoProducto", idTP));
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

        //metodo para el combobox de programacion
        public DataTable obtenerUTBySucursal(int idS)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM unidadesTransporte WHERE idSucursal = @idSucursal", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idS));
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

        //metodo para obtener el id de perfil en el alta de Perfiles
        public int obtenerUnidadT()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT id FROM perfiles WHERE descripcion = @descripcion", conn))
                    {
                        //cmd.Parameters.Add(new SqlParameter("@descripcion", cp));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            return int.Parse(dt.Rows[0][0].ToString());
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metodo para obtener la capacidad Máxima de Carga por ID del Tipo de Unidad
        public float obtenerCargaMax()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT MAX(capacidad) AS capacidad FROM tiposUnidadTransporte WHERE idTipoProducto = @idTipoProducto AND eliminada IS NULL", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idTipoProducto", idTipoProducto));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            return float.Parse(dt.Rows[0][0].ToString());
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public bool existe(string tipoU, float capacidadU, int idUDMU, int idTipoPU)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM tiposUnidadTransporte WHERE tipo = @tipo AND capacidad = @capacidad AND idUDM = @idUDM AND idTipoProducto = @idTipoProducto", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@tipo", tipoU));
                        cmd.Parameters.Add(new SqlParameter("@capacidad", capacidadU));
                        cmd.Parameters.Add(new SqlParameter("@idUDM", idUDMU));
                        cmd.Parameters.Add(new SqlParameter("@idTipoProducto", idTipoPU));
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