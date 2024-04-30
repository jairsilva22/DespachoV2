using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using despacho.App_Code;

namespace despacho
{
    public class cProductos
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
        public float precio { get; set; }
        public float peso { get; set; }
        public string iva { get; set; }
        public int carga { get; set; }
        public int idSucursal { get; set; }
        public string codigoSAT { get; set; }
        public int idContpaq { get; set; }
        public int idUsuario { get; set; }

        //Constructor
        public cProductos()
        {
            cadena = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;
        }

        //metodo para verificar que no se este insertando el codigo duplicado
        private bool productoExistente(string codigo)
        {
            using (SqlConnection conn = new SqlConnection(cadena))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM productos WHERE codigo = @codigo", conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@codigo", codigo));

                    int count = (int)cmd.ExecuteScalar();

                    return count > 0;
                }
            }
        }


        //metodo para insertar
        public void insertar(int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {

                    //Tambien se agrego esta parte del codigo en el cual mando a llamar el metodo producto existente para que verifique que no se dupliquen 

                    if (productoExistente(codigo))
                    {
                        // Código duplicado, puedes manejar la lógica correspondiente, lanzar una excepción, mostrar un mensaje, etc.
                        throw new Exception("Ya existe un producto con el mismo código.");
                        
                  
                    }
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO productos(codigo, descripcion, unidad, idUDM, idTipoProducto, idCategoria, precio, peso, iva, carga, idSucursal, codigoSAT, idUsuario, fechaAlta) " +
                        "VALUES (@codigo, @descripcion, @unidad, @idUDM, @idTipoProducto, @idCategoria, @precio, @peso, @iva, @carga, @idSucursal, @codigoSAT, @idUsuario, GETDATE())", conn))
                    {

                        cmd.Parameters.Add(new SqlParameter("@codigo", codigo));
                        cmd.Parameters.Add(new SqlParameter("@descripcion", descripcion));
                        cmd.Parameters.Add(new SqlParameter("@unidad", unidad));
                        cmd.Parameters.Add(new SqlParameter("@idUDM", idUDM));
                        cmd.Parameters.Add(new SqlParameter("@idTipoProducto", idTipoProducto));
                        cmd.Parameters.Add(new SqlParameter("@idCategoria", idCategoria));

                        //convertia los datofloat a string    checar eso 
                       // cmd.Parameters.Add(new SqlParameter("@precio", precio.ToString("0.00")));
                        //cmd.Parameters.Add(new SqlParameter("@peso", peso.ToString("0.00")));

                        cmd.Parameters.Add(new SqlParameter("@precio", precio));
                        cmd.Parameters.Add(new SqlParameter("@peso", peso));
                        cmd.Parameters.Add(new SqlParameter("@iva", iva));
                        cmd.Parameters.Add(new SqlParameter("@carga", carga));
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
                        cmd.Parameters.Add(new SqlParameter("@codigoSAT", codigoSAT));
                        cmd.Parameters.Add(new SqlParameter("@idUsuario", idUsuarioActivo));

                        int filas = cmd.ExecuteNonQuery();

                        throw new Exception("ok");
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
                    using (SqlCommand cmd = new SqlCommand("UPDATE productos SET codigo = @codigo, descripcion = @descripcion, unidad = @unidad, idUDM = @idUDM, idTipoProducto = @idTipoProducto, " +
                        "idCategoria = @idCategoria, precio = @precio, peso = @peso, iva = @iva, carga = @carga, idSucursal = @idSucursal, codigoSAT = @codigoSAT, idUsuarioMod = @idUsuarioMod, fechaMod = GETDATE() " +
                        "WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@codigo", codigo));
                        cmd.Parameters.Add(new SqlParameter("@descripcion", descripcion));
                        cmd.Parameters.Add(new SqlParameter("@unidad", unidad));
                        cmd.Parameters.Add(new SqlParameter("@idUDM", idUDM));
                        cmd.Parameters.Add(new SqlParameter("@idTipoProducto", idTipoProducto));
                        cmd.Parameters.Add(new SqlParameter("@idCategoria", idCategoria));
                        cmd.Parameters.Add(new SqlParameter("@precio", precio));
                        cmd.Parameters.Add(new SqlParameter("@peso", peso));
                        cmd.Parameters.Add(new SqlParameter("@iva", iva));
                        cmd.Parameters.Add(new SqlParameter("@carga", carga));
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
                        cmd.Parameters.Add(new SqlParameter("@codigoSAT", codigoSAT));
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioMod", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@id", id));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        throw new Exception("ok");
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
                        throw new Exception("ok");
                    }
                }
            }
            catch (Exception)
            {
                throw;
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
                    using (SqlCommand cmd = new SqlCommand("SELECT p.id, p.codigo, p.descripcion, p.unidad, tp.tipo, p.precio, p.iva, p.peso, c.nombre AS categoria " +
                        "FROM productos AS p INNER JOIN tiposProductos AS tp ON p.idTipoProducto = tp.id INNER JOIN categoriasProductos AS c ON p.idCategoria = c.id " +
                        "WHERE (p.idSucursal = @idSucursal) AND (p.eliminado IS NULL)", conn))
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
        public DataTable obtenerProductosBySuc(int idSuc)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT id, codigo FROM productos WHERE idSucursal = @idSuc", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSuc", idSuc));
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
        public DataTable obtenerProduccionTotal(int idSuc, int tipoProducto, DateTime fechaI, DateTime fechaF)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT ordenes.fecha, solicitudes.idSucursal AS planta, tiposProductos.tipo, FORMAT(SUM(cantidad),'N2') AS cantidad, FORMAT(SUM(subtotal), 'N2') AS subtotal, " +
                        " FORMAT(SUM(subtotal * iva), 'N2') AS iva, FORMAT(SUM(total), 'N2') AS total, idUDM FROM productos, tiposProductos, ordenes, detallesSolicitud, solicitudes " +
                        " WHERE productos.idTipoProducto = tiposProductos.id AND ordenes.idSolicitud = detallesSolicitud.idSolicitud AND detallesSolicitud.idProducto = productos.id AND ordenes.idSolicitud = solicitudes.id " +
                        " AND idTipoProducto = @tipoProducto AND solicitudes.idSucursal = @idSucursal AND detallesSolicitud.cantidadEntregada > 0 " +
                        " AND (ordenes.fecha BETWEEN CONVERT(datetime, @fechaI, 103) AND CONVERT(datetime, @fechaF, 103))" +
                        " AND (solicitudes.eliminada IS NULL) AND (ordenes.eliminado IS NULL) AND (detallesSolicitud.eliminado IS NULL) " +
                        " GROUP BY ordenes.fecha, solicitudes.idSucursal, tiposProductos.tipo, idUDM ORDER BY ordenes.fecha ", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSuc));
                        cmd.Parameters.Add(new SqlParameter("@tipoProducto", tipoProducto));
                        cmd.Parameters.Add(new SqlParameter("@fechaI", fechaI));
                        cmd.Parameters.Add(new SqlParameter("@fechaF", fechaF));
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
        public DataTable obtenerProduccionTotalByDia(int idSuc, int tipoProducto, DateTime fecha)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT o.fecha, s.idSucursal AS planta, tp.tipo, ds.cantidad, ds.cantidadEntregada, ds.precioF, ds.subtotal, ds.iva, ds.total, p.idUDM " +
                        "FROM detallesSolicitud AS ds INNER JOIN ordenes AS o ON ds.idSolicitud = o.idSolicitud INNER JOIN productos AS p INNER JOIN " +
                        "tiposProductos AS tp ON p.idTipoProducto = tp.id ON ds.idProducto = p.id INNER JOIN " +
                        "solicitudes AS s ON o.idSolicitud = s.id WHERE(p.idTipoProducto = @tipoProducto) AND(s.idSucursal = @idSucursal) AND(ds.cantidadEntregada > 0) " +
                        "AND(o.fecha = CONVERT(datetime, @fecha, 102)) AND(s.eliminada IS NULL) AND(o.eliminado IS NULL) AND(ds.eliminado IS NULL) ", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSuc));
                        cmd.Parameters.Add(new SqlParameter("@tipoProducto", tipoProducto));
                        cmd.Parameters.Add(new SqlParameter("@fecha", fecha));
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
        public DataTable obtenerProduccionMezcla(int idSuc, int tipoProducto, DateTime fechaI, DateTime fechaF, string param)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT ordenes.fecha, solicitudes.idSucursal AS planta, productos.codigo, tiposProductos.tipo, " +
                        " FORMAT(sum(cantidad), 'N2') AS cantidad, idUDM FROM productos, tiposProductos, ordenes, detallesSolicitud, solicitudes, categoriasProductos " +
                        " WHERE productos.idTipoProducto = tiposProductos.id AND ordenes.idSolicitud = detallesSolicitud.idSolicitud AND detallesSolicitud.idProducto = productos.id AND ordenes.idSolicitud = solicitudes.id " +
                        " AND productos.idTipoProducto = @tipoProducto AND solicitudes.idSucursal = @idSucursal AND (ordenes.fecha BETWEEN CONVERT(datetime, @fechaI, 103) AND CONVERT(datetime, @fechaF, 103)) " +
                        " AND productos.idCategoria = categoriasProductos.id " + param + "AND  detallesSolicitud.cantidad = detallesSolicitud.cantidadEntregada AND (ordenes.eliminado IS NULL) " +
                        "AND (detallesSolicitud.eliminado IS NULL) AND (ordenes.eliminado IS NULL) AND (solicitudes.eliminada IS NULL) " +
                        " GROUP BY productos.codigo, ordenes.fecha, solicitudes.idSucursal, tiposProductos.tipo, idUDM ORDER BY ordenes.fecha", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSuc));
                        cmd.Parameters.Add(new SqlParameter("@tipoProducto", tipoProducto));
                        cmd.Parameters.Add(new SqlParameter("@fechaI", fechaI));
                        cmd.Parameters.Add(new SqlParameter("@fechaF", fechaF));
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

        public DataTable obtenerProduccionCamion(int idSuc, int tipoProducto, DateTime fechaI, DateTime fechaF, string param)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("select ut.id as idtrans,ordenes.fecha, solicitudes.idSucursal, productos.idTipoProducto, od.idUDM, SUM(od.cantidad) as cantidad " +
                        " from unidadesTransporte as ut, ordenes, solicitudes, detallesSolicitud as ds, productos, ordenDosificacion as od " +
                        " WHERE ordenes.idSolicitud = ds.idSolicitud AND solicitudes.id = ds.idSolicitud AND productos.id = ds.idProducto AND OD.idOrden = ordenes.id and (ut.id = od.idUnidadTransporte or ut.id = od.idBomba) and ds.id = od.idDetalleSolicitud " +
                        " AND (ordenes.fecha BETWEEN CONVERT(datetime, @fechaI, 103) AND CONVERT(datetime, @fechaF, 103)) AND solicitudes.idSucursal = @idSucursal " +
                        " AND productos.idTipoProducto = @tipoProducto" + param +
                        " group by ordenes.fecha, ut.id, solicitudes.idSucursal, productos.idTipoProducto, od.idUDM order by ut.id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSuc));
                        cmd.Parameters.Add(new SqlParameter("@tipoProducto", tipoProducto));
                        cmd.Parameters.Add(new SqlParameter("@fechaI", fechaI));
                        cmd.Parameters.Add(new SqlParameter("@fechaF", fechaF));
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
        public DataTable obtenerProduccionCamionByID(int idSuc, int tipoProducto, DateTime fechaI, DateTime fechaF, int idTrans)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("select ut.id as idtrans,ordenes.fecha, solicitudes.idSucursal, productos.idTipoProducto, od.idUDM, FORMAT(SUM(od.cantidad), 'N2') as cantidad " +
                        " from unidadesTransporte as ut, ordenes, solicitudes, detallesSolicitud as ds, productos, ordenDosificacion as od " +
                        " WHERE ordenes.idSolicitud = ds.idSolicitud AND solicitudes.id = ds.idSolicitud AND productos.id = ds.idProducto AND OD.idOrden = ordenes.id and (ut.id = od.idUnidadTransporte or ut.id = od.idBomba) and ds.id = od.idDetalleSolicitud " +
                        " AND (ordenes.fecha BETWEEN CONVERT(datetime, @fechaI, 103) AND CONVERT(datetime, @fechaF, 103)) AND solicitudes.idSucursal = @idSucursal AND ut.id = @idTrans" +
                        " AND productos.idTipoProducto = @tipoProducto" +
                        " group by ordenes.fecha, ut.id, solicitudes.idSucursal, productos.idTipoProducto, od.idUDM order by ut.id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSuc));
                        cmd.Parameters.Add(new SqlParameter("@tipoProducto", tipoProducto));
                        cmd.Parameters.Add(new SqlParameter("@idTrans", idTrans));
                        cmd.Parameters.Add(new SqlParameter("@fechaI", fechaI));
                        cmd.Parameters.Add(new SqlParameter("@fechaF", fechaF));
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
        public DataTable obtenerPromedioDiaProduccionCamion(int idSuc, int tipoProducto, DateTime fechaI, DateTime fechaF, string param)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("select ut.id as idtrans,count(od.id) as totalTrans,SUM(od.cantidad) as cantidad, count(od.id) as viajes " +
                        " from unidadesTransporte as ut, ordenes, solicitudes, detallesSolicitud as ds, productos, ordenDosificacion as od " +
                        "WHERE ordenes.idSolicitud = ds.idSolicitud AND solicitudes.id = ds.idSolicitud AND productos.id = ds.idProducto AND OD.idOrden = ordenes.id and (ut.id = od.idUnidadTransporte or ut.id = od.idBomba) and ds.id = od.idDetalleSolicitud " +
                        " AND (ordenes.fecha BETWEEN CONVERT(datetime, @fechaI, 103) AND CONVERT(datetime, @fechaF, 103)) AND solicitudes.idSucursal = @idSucursal " + param +
                        " group by ut.id, solicitudes.idSucursal, productos.idTipoProducto, od.idUDM order by ut.id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSuc));
                        cmd.Parameters.Add(new SqlParameter("@tipoProducto", tipoProducto));
                        cmd.Parameters.Add(new SqlParameter("@fechaI", fechaI));
                        cmd.Parameters.Add(new SqlParameter("@fechaF", fechaF));
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
        public DataTable obtenerTotalesProduccionCamion(int idSuc, int tipoProducto, DateTime fechaI, DateTime fechaF, string param)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("select ut.id as idtrans, SUM(od.cantidad) as cantidad, count(od.id) as viajes from unidadesTransporte as ut, ordenes, solicitudes, detallesSolicitud as ds, productos, ordenDosificacion as od " +
                        " WHERE ordenes.idSolicitud = ds.idSolicitud AND solicitudes.id = ds.idSolicitud AND productos.id = ds.idProducto AND OD.idOrden = ordenes.id and (ut.id = od.idUnidadTransporte or ut.id = od.idBomba) and ds.id = od.idDetalleSolicitud " +
                        " AND (ordenes.fecha BETWEEN CONVERT(datetime, @fechaI, 103) AND CONVERT(datetime, @fechaF, 103)) AND solicitudes.idSucursal = @idSucursal " + param +
                        " group by ut.id, solicitudes.idSucursal, productos.idTipoProducto, od.idUDM order by ut.id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSuc));
                        cmd.Parameters.Add(new SqlParameter("@tipoProducto", tipoProducto));
                        cmd.Parameters.Add(new SqlParameter("@fechaI", fechaI));
                        cmd.Parameters.Add(new SqlParameter("@fechaF", fechaF));
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
        public DataTable obtenerProduccionMezclaAgrupaMezcla(int idSuc, int tipoProducto, DateTime fechaI, DateTime fechaF, string param)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT solicitudes.idSucursal AS planta, productos.codigo, tiposProductos.tipo, " +
                        " FORMAT(sum(cantidad), 'N2') AS cantidad, idUDM FROM productos, tiposProductos, ordenes, detallesSolicitud, solicitudes, categoriasProductos " +
                        " WHERE productos.idTipoProducto = tiposProductos.id AND ordenes.idSolicitud = detallesSolicitud.idSolicitud AND detallesSolicitud.idProducto = productos.id AND ordenes.idSolicitud = solicitudes.id " +
                        " AND productos.idCategoria = categoriasProductos.id " +
                        " AND productos.idTipoProducto = @tipoProducto AND solicitudes.idSucursal = @idSucursal AND (ordenes.fecha BETWEEN CONVERT(datetime, @fechaI, 103) AND CONVERT(datetime, @fechaF, 103)) " + param +
                        "AND  detallesSolicitud.cantidad = detallesSolicitud.cantidadEntregada AND (ordenes.eliminado IS NULL) AND (detallesSolicitud.eliminado IS NULL) AND (solicitudes.eliminada IS NULL) " +
                        " GROUP BY productos.codigo, solicitudes.idSucursal, tiposProductos.tipo, idUDM", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSuc));
                        cmd.Parameters.Add(new SqlParameter("@tipoProducto", tipoProducto));
                        cmd.Parameters.Add(new SqlParameter("@fechaI", fechaI));
                        cmd.Parameters.Add(new SqlParameter("@fechaF", fechaF));
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
        public DataTable obtenerProduccionMezclaAgrupaPlanta(int idSuc, int tipoProducto, DateTime fechaI, DateTime fechaF, string param)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT solicitudes.idSucursal AS planta, tiposProductos.tipo, " +
                        " FORMAT(sum(cantidad), 'N2') AS cantidad, idUDM FROM productos, tiposProductos, ordenes, detallesSolicitud, solicitudes, categoriasProductos " +
                        " WHERE productos.idTipoProducto = tiposProductos.id AND ordenes.idSolicitud = detallesSolicitud.idSolicitud AND detallesSolicitud.idProducto = productos.id AND ordenes.idSolicitud = solicitudes.id " +
                        " AND productos.idCategoria = categoriasProductos.id" +
                        " AND productos.idTipoProducto = @tipoProducto AND solicitudes.idSucursal = @idSucursal AND (ordenes.fecha BETWEEN CONVERT(datetime, @fechaI, 103) AND CONVERT(datetime, @fechaF, 103)) " + param +
                        " AND  detallesSolicitud.cantidad = detallesSolicitud.cantidadEntregada AND (ordenes.eliminado IS NULL) AND (detallesSolicitud.eliminado IS NULL) " +
                        " GROUP BY solicitudes.idSucursal, tiposProductos.tipo, idUDM", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSuc));
                        cmd.Parameters.Add(new SqlParameter("@tipoProducto", tipoProducto));
                        cmd.Parameters.Add(new SqlParameter("@fechaI", fechaI));
                        cmd.Parameters.Add(new SqlParameter("@fechaF", fechaF));
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
        public DataTable obtenerTotalesProduccionTotal(int idSuc, int tipoProducto, DateTime fechaI, DateTime fechaF)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT  tiposProductos.tipo, FORMAT(SUM(cantidad),'N2') AS cantidad, FORMAT(SUM(subtotal), 'N2') AS subtotal, " +
                        " FORMAT(SUM(subtotal * .16), 'N2') AS iva, FORMAT(SUM(total), 'N2') AS total FROM productos, tiposProductos, ordenes, detallesSolicitud, solicitudes " +
                        " WHERE productos.idTipoProducto = tiposProductos.id AND ordenes.idSolicitud = detallesSolicitud.idSolicitud AND detallesSolicitud.idProducto = productos.id AND ordenes.idSolicitud = solicitudes.id " +
                        " AND idTipoProducto = @tipoProducto AND solicitudes.idSucursal = @idSucursal AND (ordenes.fecha BETWEEN CONVERT(datetime, @fechaI, 103) AND CONVERT(datetime, @fechaF, 103))" +
                        " AND  detallesSolicitud.cantidad = detallesSolicitud.cantidadEntregada AND (ordenes.eliminado IS NULL) AND (detallesSolicitud.eliminado IS NULL) " +
                        " GROUP BY tiposProductos.tipo ", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSuc));
                        cmd.Parameters.Add(new SqlParameter("@tipoProducto", tipoProducto));
                        cmd.Parameters.Add(new SqlParameter("@fechaI", fechaI));
                        cmd.Parameters.Add(new SqlParameter("@fechaF", fechaF));
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
        public DataTable obtenerProduccionCliente(int idSuc, DateTime fechaI, DateTime fechaF, string orden)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT ordenes.fecha, solicitudes.idSucursal AS planta, solicitudes.idcliente AS cliente, solicitudes.idVendedor AS vendedor, " +
                        " FORMAT(SUM(detallesSolicitud.cantidad), 'N2') AS cantidad, productos.idUDM, FORMAT(ISNULL(SUM(total), '0'), 'N2') AS total, FORMAT(ISNULL(AVG(total), '0'), 'N2') AS promedio, FORMAT((SUM(total)/SUM(detallesSolicitud.cantidad)), 'N2') AS prom2 " +
                        " FROM ordenes, solicitudes, detallesSolicitud, clientes, productos, usuarios, ordenDosificacion " +
                        " WHERE ordenes.idSolicitud = solicitudes.id AND detallesSolicitud.idSolicitud = solicitudes.id AND solicitudes.idCliente = clientes.id AND detallesSolicitud.idProducto = productos.id " +
                        " AND solicitudes.idVendedor = usuarios.id AND ordenes.id = ordenDosificacion.idOrden AND solicitudes.idSucursal = @idSucursal AND (ordenes.fecha BETWEEN CONVERT(datetime, @fechaI, 103) AND CONVERT(datetime, @fechaF, 103)) " +
                        " AND usuarios.eliminado IS NULL AND (detallesSolicitud.cantidad>0) AND (solicitudes.eliminada IS NULL) AND (detallesSolicitud.eliminado IS NULL) AND (ordenDosificacion.eliminado IS NULL)" +
                        " GROUP BY solicitudes.idcliente, solicitudes.idVendedor, ordenes.fecha, solicitudes.idSucursal, productos.idUDM " + orden, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSuc));
                        cmd.Parameters.Add(new SqlParameter("@fechaI", fechaI));
                        cmd.Parameters.Add(new SqlParameter("@fechaF", fechaF));
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
        public DataTable obtenerProduccionClienteDetalle(int idSuc, DateTime fechaI, DateTime fechaF, string orden, string param)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT ordenes.fecha, solicitudes.idSucursal AS planta, solicitudes.idcliente AS cliente, solicitudes.idVendedor AS vendedor, " +
                        " FORMAT(SUM(detallesSolicitud.cantidad), 'N2') AS cantidad, productos.idUDM, FORMAT(ISNULL(SUM(total), '0'), 'N2') AS total, FORMAT(ISNULL(AVG(total), '0'), 'N2') AS promedio, FORMAT((SUM(total)/SUM(detallesSolicitud.cantidad)), 'N2') AS prom2 " +
                        " FROM ordenes, solicitudes, detallesSolicitud, clientes, productos, usuarios, ordenDosificacion " +
                        " WHERE ordenes.idSolicitud = solicitudes.id AND detallesSolicitud.idSolicitud = solicitudes.id AND solicitudes.idCliente = clientes.id AND detallesSolicitud.idProducto = productos.id " +
                        " AND solicitudes.idVendedor = usuarios.id AND ordenes.id = ordenDosificacion.idOrden AND solicitudes.idSucursal = @idSucursal AND (ordenes.fecha BETWEEN CONVERT(datetime, @fechaI, 103) AND CONVERT(datetime, @fechaF, 103)) " +
                        " AND usuarios.eliminado IS NULL AND (solicitudes.eliminada IS NULL) AND (detallesSolicitud.eliminado IS NULL) AND (ordenDosificacion.eliminado IS NULL) " + param +
                        " GROUP BY solicitudes.idcliente, solicitudes.idVendedor, ordenes.fecha, solicitudes.idSucursal, productos.idUDM " + orden, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSuc));
                        cmd.Parameters.Add(new SqlParameter("@fechaI", fechaI));
                        cmd.Parameters.Add(new SqlParameter("@fechaF", fechaF));
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

        public DataTable ReportesVentasPorVendeor(int idSuc, DateTime fecha, string orden, string param)
        {
            try
            {

                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT S.idVendedor,S.idCliente, S.id, E.id,E.nombre, C.id,C.nombre,DS.cantidad,DS.total,DS.idProducto,DS.idSolicitud,DS.fechaAlta, P.id,P.descripcion,P.unidad, O.idSolicitud " +
                        "FROM solicitudes AS S " +
                        "JOIN empleados AS E ON  S.idVendedor = E.id" +
                        "JOIN clientes AS C ON S.idCliente = C.id" +
                        "JOIN detallesSolicitud AS DS ON S.id = DS.idSolicitud" +
                        "JOIN productos AS P ON P.id = DS.idProducto" +
                        "JOIN ordenes AS O ON  DS.idSolicitud = O.idSolicitud   ORDER BY S.idCliente" + param, conn))

                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSuc));
                        cmd.Parameters.Add(new SqlParameter("@fecha", fecha));

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

        public DataTable obtenerProduccionClienteDetalleByDia(int idSuc, DateTime fecha, string orden, string param)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT o.fecha, s.idSucursal AS planta, s.idCliente AS cliente, s.idVendedor AS vendedor, ds.cantidad, ds.cantidadEntregada, " +
                        "ds.precioF, ds.iva, ds.total, p.idUDM " +
                        "FROM detallesSolicitud AS ds INNER JOIN ordenes AS o ON ds.idSolicitud = o.idSolicitud INNER JOIN productos AS p INNER JOIN " +
                        "tiposProductos AS tp ON p.idTipoProducto = tp.id ON ds.idProducto = p.id INNER JOIN " +
                        "solicitudes AS s ON o.idSolicitud = s.id WHERE (s.idSucursal = @idSucursal) AND(ds.cantidadEntregada > 0) " +
                        "AND(o.fecha = CONVERT(datetime, @fecha, 102)) AND(s.eliminada IS NULL) AND(o.eliminado IS NULL) AND(ds.eliminado IS NULL) " + param, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSuc));
                        cmd.Parameters.Add(new SqlParameter("@fecha", fecha));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }



                //using (SqlConnection conn = new SqlConnection(cadena))
                //{
                //    conn.Open();
                //    using (SqlCommand cmd = new SqlCommand("SELECT o.fecha, s.idSucursal AS planta, s.idCliente AS cliente, s.idVendedor AS vendedor, ds.cantidad, ds.cantidadEntregada, " +
                //        "ds.precioF, ds.iva, p.idUDM FROM ordenes AS o INNER JOIN solicitudes AS s ON o.idSolicitud = s.id INNER JOIN detallesSolicitud AS ds ON s.id = ds.idSolicitud INNER JOIN " +
                //        "clientes AS c ON s.idCliente = c.id INNER JOIN productos AS p ON ds.idProducto = p.id INNER JOIN usuarios AS u ON s.idVendedor = u.id " +
                //        "WHERE(s.idSucursal = @idSucursal) AND(o.fecha = CONVERT(datetime, @fecha, 102)) AND(s.eliminada IS NULL) AND(ds.eliminado IS NULL) AND (o.eliminado IS NULL) " +
                //        "AND(ds.cantidadEntregada > 0) " + param
                //         + orden, conn))
                //    {
                //        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSuc));
                //        cmd.Parameters.Add(new SqlParameter("@fecha", fecha));
                //        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                //        {
                //            sda.Fill(dt);
                //            return dt;
                //        }
                //    }
                //}

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        //metodo para el combobox de proyectos
        //public DataTable obtenerProductosByTipoAndCodigo(int idTipoProducto, string codigo)
        //{
        //    try
        //    {
        //        DataTable dt = new DataTable();
        //        using (SqlConnection conn = new SqlConnection(cadena))
        //        {
        //            conn.Open();
        //            using (SqlCommand cmd = new SqlCommand("SELECT * FROM productos WHERE (p.idSucursal = @idSucursal) AND idTipoProducto = @idTipoProducto AND codigo = @codigo ORDER BY id", conn))
        //            {
        //                cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
        //                cmd.Parameters.Add(new SqlParameter("@idTipoProducto", idTipoProducto));
        //                cmd.Parameters.Add(new SqlParameter("@codigo", codigo));
        //                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
        //                {
        //                    sda.Fill(dt);
        //                    return dt;
        //                }
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //    }
        //}

        //metodo para obtener el producto por su ID
        public DataTable obtenerProductoByID(int idP)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT p.*, um.unidadSAT FROM productos AS p INNER JOIN unidadesDeMedida AS um ON idUDM = um.id WHERE p.id = @id", conn))
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
                    using (SqlCommand cmd = new SqlCommand("SELECT p.id, p.codigo, p.descripcion, p.unidad, p.idUDM, p.idTipoProducto, p.precio, p.peso, p.iva, tp.revenimiento " +
                        "FROM productos AS p INNER JOIN tiposProductos AS tp ON p.idTipoProducto = tp.id WHERE p.codigo = @codigo AND p.idSucursal = @idSucursal AND p.eliminado IS NULL", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@codigo", code));
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
                string comando = "SELECT id FROM productos WHERE codigo = @codigo AND idSucursal=@idSucursal";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@codigo", sCodigo));
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

        //metodo para obtener el idSolicitud por su Folio
        public int obtenerCarga()
        {
            try
            {
                string comando = "SELECT carga FROM productos WHERE id = @id AND idSucursal=@idSucursal";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", id));
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursal));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    return int.Parse(reader["carga"].ToString());
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

        //metodo para obtener la Formulación por ID de la fórmula
        public DataTable obtenerFormulacionByIdProducto(int idProducto)
        {
            try
            {
                string comando = "SELECT f.id, f.idProducto, f.idMaterial, f.material, f.cantidad, f.unidad, f.idUnidad, m.adicional FROM formulacion AS f INNER JOIN materiales AS m ON f.idMaterial = m.id " +
                    "WHERE(f.idProducto = @idProducto)";

                DataTable dt = new DataTable();

                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idProducto", idProducto));
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

        public DataTable obtenerFormulacionByIdProductoReporte(int idProducto)
        {
            try
            {
                string comando = "SELECT f.material AS item, m.material AS descripcion, f.cantidad, UDM.unidad FROM productos AS p, formulacion AS f, materiales AS m, unidadesDeMedida AS UDM " +
                    " WHERE p.id = f.idProducto AND m.id = f.idMaterial AND f.idUnidad = UDM.id AND m.idUnidad = UDM.id AND f.idProducto = @idProducto";

                DataTable dt = new DataTable();

                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idProducto", idProducto));
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


        //metodo para obtener el idSolicitud por su Folio
        public int obtenerIdRecetaByID(int idP)
        {
            try
            {
                string comando = "SELECT idReceta FROM productos WHERE id = @idP";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idP", idP));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    return int.Parse(reader["idReceta"].ToString());
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
        public string obtenerCodigoProductoByID(int idProducto)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT codigo FROM productos WHERE id = @idProducto", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idProducto", idProducto));
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
        public string obtenerDescripcionProductoByID(int idProducto)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT descripcion FROM productos WHERE id = @idProducto", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idProducto", idProducto));
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
        public DataTable obtenerPrecioProducto(int idSuc, string param)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT idProducto FROM clientes AS c, solicitudes AS s, detallesSolicitud AS ds " +
                        " WHERE c.id = s.idCliente AND ds.idSolicitud = s.id AND c.eliminado IS NULL AND s.idSucursal = @idSucursal " + param + " GROUP BY idProducto", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSuc));
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
        public int obtenerCategoridProducto(int idProducto)
        {
            try
            {
                string comando = "SELECT idCategoria FROM productos WHERE id = @idProducto";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idProducto", idProducto));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    return int.Parse(reader["idCategoria"].ToString());
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
        public int obtenerTipoByidProducto(int idProducto)
        {
            try
            {
                string comando = "SELECT idTipoProducto FROM productos WHERE id = @idProducto";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idProducto", idProducto));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    return int.Parse(reader["idTipoProducto"].ToString());
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
        public string obtenerPrecioByIDProducto(int idProducto)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT FORMAT(precio, 'N2') as precio FROM productos WHERE id = @idProducto", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idProducto", idProducto));
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

        public bool hasFormula() {
            try {
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM formulacion WHERE idProducto = @id;", conn)) {
                        cmd.Parameters.Add(new SqlParameter("@id", id));

                        object result = cmd.ExecuteScalar();
                        return result != null && result != DBNull.Value; // Retorna true si existe el código, false si no.
                    }
                }
            }
            catch (Exception ex) {
                return false;
            }
        }

        public bool hasIdContpaq() {
            try {
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM productos WHERE idContpaq = @idContpaq;", conn)) {
                        cmd.Parameters.Add(new SqlParameter("@idContpaq", idContpaq));

                        object result = cmd.ExecuteScalar();
                        return result != null && result != DBNull.Value; // Retorna true si existe el código, false si no.
                    }
                }
            }
            catch (Exception ex) {
                return false;
            }
        }

        public void ActualizarIdContpaq() {
            string command = @"UPDATE productos SET idContpaq = @idContpaq WHERE id = @id;";
            try {
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(command, conn)) {
                        cmd.Parameters.Add(new SqlParameter("@idContpaq", idContpaq));
                        cmd.Parameters.Add(new SqlParameter("@id", id));
                        
                        int row = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        //Formulacion de materiales
        public void homologarFormula(string conexion, cProductosContpaq cProducto) {
            cFormulacion cFormula = new cFormulacion();
            if (hasFormula()) {
                DataTable dtMateriales = cFormula.getFormulaByIdProducto(id);
                cProductosContpaq material = new cProductosContpaq(conexion);
                List<int> list = new List<int>();
                List<decimal> cantidades = new List<decimal>();
                foreach (DataRow dr in dtMateriales.Rows) {
                    material.CNOMBREPRODUCTO = dr["material"].ToString();
                    if (material.existeNombre()) {
                        list.Add(material.getIdProductoByName());
                        cantidades.Add(decimal.Parse(dr["cantidad"].ToString()));
                    }
                    else {
                        material.CCODIGOPRODUCTO = dr["material"].ToString();
                        material.CPESOPRODUCTO = 0;
                        material.CNOMBREPRODUCTO = dr["material"].ToString();
                        material.CIDUNIXML = material.obtenerCIDUNIXML(dr["idUnidad"].ToString(), dr["descripcion"].ToString(), dr["unidadSAT"].ToString());
                        material.CCLAVESAT = "";
                        material.CPRECIO1 = 0;
                        material.CDESCRIPCIONPRODUCTO = dr["material"].ToString();
                        material.CBANCOMPONENTE = 0;

                        list.Add(material.InsertarMaterial(cProducto.idSucursal));
                        cantidades.Add(decimal.Parse(dr["cantidad"].ToString()));
                    }
                }
                cProducto.eliminarFormula();

                cProducto.agregarFormula(list, cantidades);
            }
        }

        public bool eliminarContpaq() {
            bool isDeleted = false;
            DataTable dtProducto = obtenerProductoByID(id);

            cContpaq contpaq = new cContpaq();
            string conexion = contpaq.cadenaContpaq(idSucursal);
            cProductosContpaq cProducto = new cProductosContpaq(conexion);
            cProducto.idSucursal = idSucursal;
            cProducto.idUsuario = idUsuario;

            foreach (DataRow dr in dtProducto.Rows) {
                //Si no tiene idContpaq
                if (dtProducto.Rows[0]["idContpaq"].ToString().Equals("") || dtProducto.Rows[0]["idContpaq"].ToString() == null) {
                    cProducto.CCODIGOPRODUCTO = dtProducto.Rows[0]["codigo"].ToString().Replace("-", "");
                    if (cProducto.existeCodigo()) {
                        cProducto.CIDPRODUCTO = cProducto.getIdProductoByCodigo();
                        idContpaq = cProducto.CIDPRODUCTO;
                        //Buscar si el cidproducto está como idcontpaq en despacho
                        if (hasIdContpaq()) {
                            cLogError error = new cLogError();
                            error.idUsuario = idUsuario;
                            error.idProducto = id;
                            error.error = "El producto de contpaq existe para otro producto en despacho";
                            error.tabla = "PEPI.productos";
                            error.metodo = "eliminarContpaq";
                            error.insertarError();
                            isDeleted = false;
                        }
                        else {
                            isDeleted = cProducto.eliminarProducto();
                            //Comprobar si tiene fórmula
                            if (hasFormula()) {
                                //Eliminar fórmula
                                cProducto.eliminarFormula();
                            }

                        }
                    }
                }
                //Si tiene idContpaq
                else {
                    cProducto.CIDPRODUCTO = int.Parse(dtProducto.Rows[0]["idContpaq"].ToString());
                    isDeleted = cProducto.eliminarProducto();
                    //Comprobar si tiene fórmula
                    if (hasFormula()) {
                        //Eliminar fórmula
                        cProducto.eliminarFormula();
                    }
                }
            }

            return isDeleted;

        }

        //Homologación de productos con Contpaq - Agregado por Enrique Sandoval 23-10-2023
        public bool homologar(int idProducto, int sucursal, int idUsuario) {
            cContpaq contpaq = new cContpaq();
            DataTable dtProducto = obtenerProductoByID(idProducto);
            id = idProducto;
            
            //Obtener conexión a BD de contpaq según la sucursal
            string conexion = contpaq.cadenaContpaq(sucursal);

            cProductosContpaq cProducto = new cProductosContpaq(conexion);
            cProducto.idUsuario = idUsuario;
            cProducto.idSucursal = sucursal;
            if (dtProducto.Rows[0]["idContpaq"].ToString().Equals("") || dtProducto.Rows[0]["idContpaq"].ToString() == null) {
                //Asignar valores del producto de despacho al de Contpaq
                cProducto.CCODIGOPRODUCTO = dtProducto.Rows[0]["codigo"].ToString().Replace("-", "");
                cProducto.CPESOPRODUCTO = decimal.Parse(dtProducto.Rows[0]["peso"].ToString());
                cProducto.CNOMBREPRODUCTO = dtProducto.Rows[0]["descripcion"].ToString();
                cProducto.CIDUNIXML = cProducto.obtenerCIDUNIXML(dtProducto.Rows[0]["unidad"].ToString(), dtProducto.Rows[0]["unidad"].ToString(), dtProducto.Rows[0]["unidadSAT"].ToString());
                cProducto.CCLAVESAT = dtProducto.Rows[0]["codigoSAT"].ToString();
                cProducto.CPRECIO1 = decimal.Parse(dtProducto.Rows[0]["precio"].ToString());
                cProducto.CDESCRIPCIONPRODUCTO = dtProducto.Rows[0]["descripcion"].ToString();
                cProducto.CBANCOMPONENTE = 1;
                
                //Buscar codigo en contpaq
                if (cProducto.existeCodigo()) {
                    cProducto.CIDPRODUCTO = cProducto.getIdProductoByCodigo();
                    idContpaq = cProducto.CIDPRODUCTO;
                    //Buscar cidproducto en idcontpaq despacho
                    if (hasIdContpaq()) {
                        //Mostrar mensaje
                        return false;
                    }
                    else {
                        //Actualizar producto en Contpaq
                        cProducto.Actualizar();
                        //Homologar formula
                        homologarFormula(conexion, cProducto);
                        //Actualizar idContpaq
                        ActualizarIdContpaq();
                    }
                    
                }
                else {
                    
                    //Ejecutar el INSERT y obtener el CIDPRODUCTO
                    cProducto.CIDPRODUCTO = cProducto.Insertar(cProducto.idSucursal);
                    idContpaq = cProducto.CIDPRODUCTO;
                    //Homologar formula
                    homologarFormula(conexion, cProducto);
                    //Actualizar idContpaq
                    ActualizarIdContpaq();
                }
            }
            else {
                cProducto.CIDPRODUCTO = int.Parse(dtProducto.Rows[0]["idContpaq"].ToString());
                //Asignar valores del producto de despacho al de Contpaq
                cProducto.CCODIGOPRODUCTO = dtProducto.Rows[0]["codigo"].ToString().Replace("-", "");
                cProducto.CPESOPRODUCTO = decimal.Parse(dtProducto.Rows[0]["peso"].ToString());
                cProducto.CNOMBREPRODUCTO = dtProducto.Rows[0]["descripcion"].ToString();
                cProducto.CIDUNIXML = cProducto.obtenerCIDUNIXML(dtProducto.Rows[0]["unidad"].ToString(), dtProducto.Rows[0]["unidad"].ToString(), dtProducto.Rows[0]["unidadSAT"].ToString());
                cProducto.CCLAVESAT = dtProducto.Rows[0]["codigoSAT"].ToString();
                cProducto.CPRECIO1 = decimal.Parse(dtProducto.Rows[0]["precio"].ToString());
                cProducto.CDESCRIPCIONPRODUCTO = dtProducto.Rows[0]["descripcion"].ToString();
                cProducto.CBANCOMPONENTE = 1;
                
                //Actualizar producto en Contpaq
                cProducto.Actualizar();
                
                homologarFormula(conexion, cProducto);
            }

            return true;
        }

    }
}