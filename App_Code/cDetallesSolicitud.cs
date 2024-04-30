using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace despacho
{
    public class cDetallesSolicitud
    {
        //variables
        private string cadena;

        //propiedades
        public int id { get; set; }
        public int idSolicitud { get; set; }
        public int idElemento { get; set; }
        public bool programada { get; set; }
        public int idProducto { get; set; }
        public string cantidad { get; set; }
        public string tamano { get; set; }
        public string cantidadEntregada { get; set; }
        public string revenimiento { get; set; }
        public string precioU { get; set; }
        public int idFactor { get; set; }
        public string precioF { get; set; }
        public string subtotal { get; set; }
        public string iva { get; set; }
        public string total { get; set; }
        public string precioFIVA { get; set; }

        //Constructor
        public cDetallesSolicitud()
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
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO detallesSolicitud(idSolicitud, idElemento, idProducto, cantidad, tamano, cantidadEntregada, revenimiento, precioU, " +
                        "idFactor, precioF, subtotal, iva, total, idUsuario, fechaAlta, precioFIVA) VALUES(@idSolicitud, @idElemento, @idProducto, @cantidad, @tamano, @cantidadEntregada, @revenimiento, @precioU, " +
                        "@idFactor, @precioF, @subtotal, @iva, @total, @idUsuario, GETDATE(), @precioFIVA)", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSolicitud", idSolicitud));
                        cmd.Parameters.Add(new SqlParameter("@idElemento", idElemento));
                        cmd.Parameters.Add(new SqlParameter("@idProducto", idProducto));
                        cmd.Parameters.Add(new SqlParameter("@cantidad", cantidad));
                        cmd.Parameters.Add(new SqlParameter("@tamano", tamano));
                        cmd.Parameters.Add(new SqlParameter("@cantidadEntregada", cantidadEntregada));
                        cmd.Parameters.Add(new SqlParameter("@revenimiento", revenimiento));
                        cmd.Parameters.Add(new SqlParameter("@precioU", precioU));
                        cmd.Parameters.Add(new SqlParameter("@idFactor", idFactor));
                        cmd.Parameters.Add(new SqlParameter("@precioF", precioF));
                        cmd.Parameters.Add(new SqlParameter("@subtotal", subtotal));
                        cmd.Parameters.Add(new SqlParameter("@iva", iva));
                        cmd.Parameters.Add(new SqlParameter("@total", total));
                        cmd.Parameters.Add(new SqlParameter("@idUsuario", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@precioFIVA", precioFIVA != null ? precioFIVA : (object)DBNull.Value));

                        int filas = cmd.ExecuteNonQuery();


                        throw new Exception("ok");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ;
            }
        }

        //metodo para actualizar 
        public void actualizar(int idP, int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE detallesSolicitud SET idElemento = @idElemento, idProducto = @idProducto, cantidad = @cantidad, " +
                        "revenimiento = @revenimiento, precioU = @precioU, idFactor = @idFactor, precioF = @precioF, subtotal = @subtotal, iva = @iva, total = @total, " +
                        "idUsuarioMod = @idUsuarioMod, fechaMod = GETDATE() WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idElemento", idElemento));
                        cmd.Parameters.Add(new SqlParameter("@idProducto", idProducto));
                        cmd.Parameters.Add(new SqlParameter("@cantidad", cantidad));
                        cmd.Parameters.Add(new SqlParameter("@revenimiento", revenimiento));
                        cmd.Parameters.Add(new SqlParameter("@precioU", precioU));
                        cmd.Parameters.Add(new SqlParameter("@idFactor", idFactor));
                        cmd.Parameters.Add(new SqlParameter("@precioF", precioF));
                        cmd.Parameters.Add(new SqlParameter("@subtotal", subtotal));
                        cmd.Parameters.Add(new SqlParameter("@iva", iva));
                        cmd.Parameters.Add(new SqlParameter("@total", total));
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioMod", idUsuarioActivo));
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

        //metodo para eliminar
        public void eliminar(int idP, int idUsuarioElimino)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE detallesSolicitud SET eliminado = 1, idUsuarioElimino = @idUsuarioElimino, fechaElimino= GETDATE() WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioElimino", idUsuarioElimino));
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

        //metodo para cambiar a Programada el detalle de la solicitud
        public void setProgramada(int idDS, bool val, int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE detallesSolicitud SET programada=@programada, fechaMod=GETDATE(), idUsuarioMod=@idUsuarioMod WHERE id=@id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioMod", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@programada", val));
                        cmd.Parameters.Add(new SqlParameter("@id", idDS));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metodo para cambiar la Cantidad Entregada del producto solicitado
        public void setCantidadEntregada(int idDS, float cEntregada, int idUsuarioActivo)
        {
            try
            {
                float cE = getCantidadEntregada(idDS) + cEntregada;
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE detallesSolicitud SET cantidadEntregada = @cantidadEntregada, fechaMod=GETDATE(), idUsuarioMod=@idUsuarioMod WHERE id=@id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@cantidadEntregada", cE));
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioMod", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@id", idDS));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public float getCantidadEntregada(int idDS)
        {

            try
            {
                string comando = "SELECT cantidadEntregada FROM detallesSolicitud WHERE id = @id AND eliminado IS NULL";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idDS));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    return float.Parse(reader["cantidadEntregada"].ToString());
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

        //metodo para obtener el detalle específico de la solicitud por su ID
        public void obtenerDetalleSolicitudByID()
        {
            try
            {
                string comando = "SELECT * FROM detallesSolicitud WHERE id = @id AND eliminado IS NULL";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", id));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    idSolicitud = int.Parse(reader["idSolicitud"].ToString());
                                    idElemento = int.Parse(reader["idElemento"].ToString());
                                    programada = bool.Parse(reader["programada"].ToString());
                                    idProducto = int.Parse(reader["idProducto"].ToString());
                                    cantidad = reader["cantidad"].ToString();
                                    tamano = reader["tamano"].ToString();
                                    cantidadEntregada = reader["cantidadEntregada"].ToString();
                                    revenimiento = reader["revenimiento"].ToString();
                                    precioU = reader["precioU"].ToString();
                                    idFactor = int.Parse(reader["idFactor"].ToString());
                                    precioF = reader["precioF"].ToString();
                                    subtotal = reader["subtotal"].ToString();
                                    iva = reader["iva"].ToString();
                                    total = reader["total"].ToString();
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
        public DataTable obtenerDetallesSolicitud(int idS)
        {
            try
            {
                DataTable dt = new DataTable();
                string comando = "SELECT ds.id, ds.idSolicitud, ds.programada, ds.idElemento, e.elemento, ds.idProducto, p.codigo, p.descripcion, p.carga, udm.unidad, udm.id AS idUDM, ds.cantidad, ds.tamano, " +
                    "ds.cantidadEntregada, ds.revenimiento, p.idTipoProducto, tp.tipo, ds.precioU, ds.idFactor, pf.factor, pf.porcentaje, ds.precioF, ds.precioFIVA, ds.subtotal, ds.iva, ds.total " +
                    "FROM detallesSolicitud AS ds INNER JOIN productos AS p ON ds.idProducto = p.id INNER JOIN unidadesDeMedida AS udm ON p.idUDM = udm.id INNER JOIN " +
                    "tiposProductos AS tp ON p.idTipoProducto = tp.id INNER JOIN productosFactor AS pf ON ds.idFactor = pf.id LEFT JOIN elementos AS e ON ds.idElemento = e.id " +
                    "WHERE(ds.idSolicitud = @idSolicitud) AND(ds.eliminado IS NULL)";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSolicitud", idS));
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

        //Existe el producto en el detalle de la solicitud
        public bool existe()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT id FROM detallesSolicitud WHERE (idSolicitud = @idSolicitud) AND(idProducto = @idProducto) AND (eliminado IS NULL)", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSolicitud", idSolicitud));
                        cmd.Parameters.Add(new SqlParameter("@idProducto", idProducto));
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
        //Existe el producto en el detalle de la solicitud
        public bool existenDetalles()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM detallesSolicitud WHERE (idSolicitud = @idSolicitud) AND (eliminado IS NULL)", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSolicitud", idSolicitud));
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

        //metodo para actualizar 
        public void actualizarCantSolicitada(int idDS, int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE detallesSolicitud SET idElemento = @idElemento, cantidad = @cantidad, " +
                        "revenimiento = @revenimiento, precioF = @precioF, precioFIVA = @precioFIVA, subtotal = @subtotal, total = @total, " +
                        "idUsuarioMod = @idUsuarioMod, fechaMod = GETDATE() WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idElemento", idElemento));
                        cmd.Parameters.Add(new SqlParameter("@cantidad", cantidad));
                        cmd.Parameters.Add(new SqlParameter("@revenimiento", revenimiento));
                        cmd.Parameters.Add(new SqlParameter("@precioF", precioF));
                        cmd.Parameters.Add(new SqlParameter("@precioFIVA", precioFIVA));
                        cmd.Parameters.Add(new SqlParameter("@subtotal", subtotal));
                        cmd.Parameters.Add(new SqlParameter("@total", total));
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioMod", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@id", idDS));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        //metodo para actualizar 
        public void actualizarProdAndCantSolicitada(int idDS, int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE detallesSolicitud SET idProducto = @idProducto, idElemento = @idElemento, cantidad = @cantidad, " +
                        "revenimiento = @revenimiento, precioF = @precioF, subtotal = @subtotal, total = @total, " +
                        "idUsuarioMod = @idUsuarioMod, fechaMod = GETDATE() WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idProducto", idProducto));
                        cmd.Parameters.Add(new SqlParameter("@idElemento", idElemento));
                        cmd.Parameters.Add(new SqlParameter("@cantidad", cantidad));
                        cmd.Parameters.Add(new SqlParameter("@revenimiento", revenimiento));
                        cmd.Parameters.Add(new SqlParameter("@precioF", precioF));
                        cmd.Parameters.Add(new SqlParameter("@subtotal", subtotal));
                        cmd.Parameters.Add(new SqlParameter("@total", total));
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioMod", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@id", idDS));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //Buscar cantidad entregada con el idOrden
        public float getCantidadEntregadaByIdOrden(int idOrden) {

            try {
                string comando = "SELECT ISNULL(cantidadEntregada, 0) As cantidadEntregada FROM detallesSolicitud WHERE id = (SELECT TOP(1)idDetalleSolicitud FROM ordenDosificacion where idOrden = @idOrden) AND eliminado IS NULL";
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn)) {
                        cmd.Parameters.Add(new SqlParameter("@idOrden", idOrden));
                        using (SqlDataReader reader = cmd.ExecuteReader()) {
                            if (reader.HasRows) {
                                while (reader.Read()) {
                                    return float.Parse(reader["cantidadEntregada"].ToString());
                                }
                            }
                            return 0;
                        }
                    }
                }
            }
            catch (Exception) {
                return 0;
            }
        }


        //Acutalizar precio detalle solicitud para cambio de precioFIVA
        public void actualizarPrecioDS(int idDS, decimal precioFIVA, decimal precioF, decimal total, decimal subtotal)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE detallesSolicitud SET  " +
                        "precioF = @precioF, precioFIVA = @precioFIVA, subtotal = @subtotal, total = @total, " +
                        " fechaMod = GETDATE() WHERE id = @id", conn))
                    {

                        cmd.Parameters.Add(new SqlParameter("@precioF", precioF));
                        cmd.Parameters.Add(new SqlParameter("@precioFIVA", precioFIVA));
                        cmd.Parameters.Add(new SqlParameter("@subtotal", subtotal));
                        cmd.Parameters.Add(new SqlParameter("@total", total));
                        cmd.Parameters.Add(new SqlParameter("@id", idDS));

                        int filasAfectadas = cmd.ExecuteNonQuery();
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