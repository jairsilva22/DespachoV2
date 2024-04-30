using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace despacho
{
    public class cDosificacion
    {
        //Clases base referencia
        cBitacora cBit = new cBitacora();
        //variables
        private string cadena;

        //propiedades
        public int idSucursal { get; set; }
        public int id { get; set; }
        public string folio { get; set; }
        public DateTime fecha { get; set; }
        public string hora { get; set; }
        public int idOrden { get; set; }
        public int idProducto { get; set; }
        public float cantidad { get; set; }
        public int idUDM { get; set; }
        public int idTUT { get; set; }
        public int idDetalleSolicitud { get; set; }
        public int idEstadoDosificacion { get; set; }
        public int idUnidadTransporte { get; set; }
        public string colorBell { get; set; }

        //Constructor
        public cDosificacion()
        {
            cadena = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;
        }

        public DataTable obtenerODSinDuplicados(DataTable dtO)
        {
            string idO = "", aux = "0";
            DataTable dtX = new DataTable();
            dtX = dtO.Copy();
            dtX.Clear();
            foreach (DataRow r in dtX.Rows)
            {
                r.Delete();
            }
            for (int i = 0; i < dtO.Rows.Count; i++)
            {
                try
                {
                    idO = dtO.Rows[i]["idOrden"].ToString();
                    //if (i.Equals(0))
                    //    dtX.ImportRow(dtO.Rows[i]);
                    for (int j = 0; j < dtO.Rows.Count; j++)
                    {
                        try
                        {
                            if (dtO.Rows[j]["idOrden"].ToString().Equals(idO))
                            {
                                if (aux.Equals("0"))
                                    dtX.ImportRow(dtO.Rows[j]);
                                aux = "1";
                                dtO.Rows[j].Delete();
                            }
                        }
                        catch (Exception)
                        {

                        }
                    }
                    aux = "0";
                }
                catch (Exception)
                {
                }
            }
            return dtX;
            //int idO = 0;
            //DataTable dt = new DataTable();
            //dt = dtO.Copy();
            //dt.Clear();
            //foreach (DataRow r in dt.Rows)
            //{
            //    r.Delete();
            //}
            //for (int i = 0; i < dtO.Rows.Count; i++)
            //{
            //    if (!i.Equals(0))
            //    {
            //        if (!int.Parse(dtO.Rows[i]["idOrden"].ToString()).Equals(idO))
            //        {
            //            idO = int.Parse(dtO.Rows[i]["idOrden"].ToString());
            //            dt.ImportRow(dtO.Rows[i]);
            //        }
            //    }
            //    else
            //    {
            //        idO = int.Parse(dtO.Rows[i]["idOrden"].ToString());
            //        dt.ImportRow(dtO.Rows[i]);
            //    }
            //}
            //return dt;
        }

        public DataTable obtenerOPFiltro(string fClaveCliente = "", string fVendedor = "", string fCodigo = "", string fUnidad = "", string fChofer = "", int opc = 1, bool finish = false, string fFechaI = "", string fFechaF = "", bool fProgramado = true)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    string selectComand = "cl.clave, v.id AS idV, p.codigo, ut.nombre AS uNombre, ch.id AS idCh, od.id, od.idOrden, s.id AS idS, o.folio AS folioOrden, ds.id AS idDS,ch.id AS idCh, FORMAT(od.fecha, 'dd/MM/yyyy') AS fecha,od.hora, p.id AS idProducto, " +
                        "p.descripcion, ds.cantidad AS cTotal, od.cantidad, udm.unidad, ds.revenimiento, tut.tipo, od.folio, pr.nombre, pr.calle, pr.numero, pr.interior, pr.colonia, pr.cp, cl.nombre AS cliente," +
                        "od.idEstadoDosificacion, ed.estado, ed.porcentaje,ds.cantidadEntregada, ut.id AS idUTransporte, ut.nombre AS uTransporte, ut.color, ch.nombre AS chofer, od.colorBell, v.nombre AS vendedor, e.elemento, od.folioR, bba.nombre AS bomba, s.idEstadoSolicitud, es.estado AS estadoSolicitud ";
                    string fromComand = "ordenes AS o INNER JOIN " +
                         "solicitudes AS s ON o.idSolicitud = s.id INNER JOIN " +
                         "clientes AS cl ON s.idCliente = cl.id INNER JOIN " +
                         "ordenDosificacion AS od ON o.id = od.idOrden INNER JOIN " +
                         "productos AS p ON od.idProducto = p.id INNER JOIN " +
                         "unidadesDeMedida AS udm ON od.idUDM = udm.id INNER JOIN " +
                         "detallesSolicitud AS ds ON od.idDetalleSolicitud = ds.id LEFT OUTER JOIN " +
                         "tiposUnidadTransporte AS tut ON od.idTUT = tut.id INNER JOIN " +
                         "proyectos AS pr ON s.idProyecto = pr.id INNER JOIN " +
                         "estadosDosificacion AS ed ON od.idEstadoDosificacion = ed.id INNER JOIN " +
                         "estadosSolicitud AS es ON s.idEstadoSolicitud = es.id INNER JOIN " +
                         "unidadesTransporte AS ut ON od.idUnidadTransporte = ut.id INNER JOIN " +
                         "unidadesTChoferes AS utc ON od.idUnidadTransporte = utc.idUnidad INNER JOIN " +
                         "usuarios AS ch ON utc.idChofer = ch.id INNER JOIN " +
                         "usuarios AS v ON o.idVendedor = v.id INNER JOIN " +
                         "elementos AS e ON ds.idElemento = e.id LEFT OUTER JOIN " +
                         "unidadesTransporte AS bba ON od.idBomba = bba.id ";
                    string whereComand = "(o.eliminado IS NULL) AND (od.eliminado IS NULL) AND (utc.activo=1) ";
                    whereComand += " AND s.idSucursal = " + idSucursal;
                    if (!finish)
                        whereComand += " AND od.idEstadoDosificacion < 11";
                    string orderByComand = "od.fecha ASC, od.hora ASC, od.idOrden, od.id ASC;";
                    if (!fClaveCliente.Equals(""))
                        whereComand += " AND cl.clave = '" + fClaveCliente + "'";
                    if (!fVendedor.Equals(""))
                        whereComand += " AND v.id = '" + fVendedor + "'";
                    if (!fCodigo.Equals(""))
                        whereComand += " AND p.codigo = '" + fCodigo + "'";
                    if (!fUnidad.Equals(""))
                        whereComand += " AND ut.nombre = '" + fUnidad + "'";
                    if (!fChofer.Equals(""))
                        whereComand += " AND ch.id = '" + fChofer + "'";
                    //if (!fIdOrden.Equals(0))
                    //    whereComand += " AND o.id = " + fIdOrden;
                    switch (opc)
                    {
                        case 0:
                            whereComand += " AND od.fecha <= '" + fFechaI + "'";
                            //orderByComand = "od.fecha DESC, od.hora ASC, od.id ASC;";
                            break;
                        case 1:
                            string a = "2021-05-22";
                            //whereComand += " AND od.fecha = '" + fFechaI + "'";
                            whereComand += " AND od.fecha = '" + fFechaI + "'";
                            break;
                        case 2:
                            whereComand += " AND od.fecha = '" + fFechaI + "'";
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                        case 5:
                            whereComand += " AND od.fecha BETWEEN '" + fFechaI + "' AND '" + fFechaF + "'";
                            break;
                    }


                    using (SqlCommand cmd = new SqlCommand("SELECT " + selectComand + "FROM " + fromComand + "WHERE " + whereComand + " ORDER BY " + orderByComand, conn))
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
        public DataTable obtenerRepOPFiltro(string fClaveCliente = "", string fVendedor = "", string fCodigo = "", string fUnidad = "", string fChofer = "", string fSucursal = "", int opc = 1, bool finish = false, string fFechaI = "", string fFechaF = "", bool fProgramado = true)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    string selectComand = "cl.clave, v.id AS idV, p.codigo, ut.nombre AS uNombre, ch.id AS idCh, s.idSucursal AS idSuc, od.id, od.idOrden, s.id AS idS, o.folio AS folioOrden, ds.id AS idDS,ch.id AS idCh, FORMAT(od.fecha, 'dd/MM/yyyy') AS fecha,od.hora, p.id AS idProducto, " +
                        "p.descripcion, ds.cantidad AS cTotal, od.cantidad, udm.unidad, ds.revenimiento, tut.tipo, od.folio, pr.nombre, pr.calle, pr.numero, pr.interior, pr.colonia, pr.cp, cl.nombre AS cliente, suc.nombre AS sucursal, " +
                        "od.idEstadoDosificacion, ed.estado, ed.porcentaje,ds.cantidadEntregada, ut.id AS idUTransporte, ut.nombre AS uTransporte, ut.color, ch.nombre AS chofer, od.colorBell, v.nombre AS vendedor, e.elemento ";
                    string fromComand = "ordenes AS o INNER JOIN " +
                         "solicitudes AS s ON o.idSolicitud = s.id INNER JOIN " +
                         "sucursales AS suc ON s.idSucursal = suc.id INNER JOIN " +
                         "clientes AS cl ON s.idCliente = cl.id INNER JOIN " +
                         "ordenDosificacion AS od ON o.id = od.idOrden INNER JOIN " +
                         "productos AS p ON od.idProducto = p.id INNER JOIN " +
                         "unidadesDeMedida AS udm ON od.idUDM = udm.id INNER JOIN " +
                         "detallesSolicitud AS ds ON od.idDetalleSolicitud = ds.id LEFT OUTER JOIN " +
                         "tiposUnidadTransporte AS tut ON od.idTUT = tut.id INNER JOIN " +
                         "proyectos AS pr ON s.idProyecto = pr.id INNER JOIN " +
                         "estadosDosificacion AS ed ON od.idEstadoDosificacion = ed.id INNER JOIN " +
                         "unidadesTransporte AS ut ON od.idUnidadTransporte = ut.id INNER JOIN " +
                         "unidadesTChoferes AS utc ON od.idUnidadTransporte = utc.idUnidad INNER JOIN " +
                         "usuarios AS ch ON utc.idChofer = ch.id INNER JOIN " +
                         "usuarios AS v ON o.idVendedor = v.id INNER JOIN " +
                         "elementos AS e ON ds.idElemento = e.id ";
                    string whereComand = "(od.eliminado IS NULL) AND (utc.activo=1) ";
                    if (!finish)
                        whereComand += " AND od.idEstadoDosificacion < 11";
                    string orderByComand = "od.fecha ASC, od.hora ASC, od.idOrden, od.id ASC;";
                    if (!fClaveCliente.Equals(""))
                        whereComand += " AND cl.clave = '" + fClaveCliente + "'";
                    if (!fVendedor.Equals(""))
                        whereComand += " AND v.id = '" + fVendedor + "'";
                    if (!fCodigo.Equals(""))
                        whereComand += " AND p.codigo = '" + fCodigo + "'";
                    if (!fUnidad.Equals(""))
                        whereComand += " AND ut.nombre = '" + fUnidad + "'";
                    if (!fChofer.Equals(""))
                        whereComand += " AND ch.id = '" + fChofer + "'";
                    if (!fSucursal.Equals(""))
                        whereComand += " AND suc.id = '" + fSucursal + "'";
                    //if (!fIdOrden.Equals(0))
                    //    whereComand += " AND o.id = " + fIdOrden;
                    switch (opc)
                    {
                        case 0:
                            whereComand += " AND od.fecha <= '" + fFechaI + "'";
                            //orderByComand = "od.fecha DESC, od.hora ASC, od.id ASC;";
                            break;
                        case 1:
                            whereComand += " AND od.fecha = '" + fFechaI + "'";
                            break;
                        case 2:
                            whereComand += " AND od.fecha = '" + fFechaI + "'";
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                        case 5:
                            whereComand += " AND od.fecha BETWEEN '" + fFechaI + "' AND '" + fFechaF + "'";
                            break;
                    }


                    using (SqlCommand cmd = new SqlCommand("SELECT " + selectComand + "FROM " + fromComand + "WHERE " + whereComand + " ORDER BY " + orderByComand, conn))
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

        public DataTable obtenerODDFiltered(int idO, int opc = 1, bool finish = false, string fFechaI = "", string fFechaF = "", bool fProgramado = true)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    string selectComand = "od.id, od.idOrden, FORMAT(od.fecha, 'dd/MM/yyyy') as fecha, od.hora, p.id AS idProducto, p.codigo, p.descripcion, tp.tipo AS tp, od.cantidad, udm.unidad, ds.revenimiento, tut.tipo, od.folio, " +
                       "pr.nombre, pr.calle, pr.numero, pr.interior, pr.colonia, pr.cp, cl.clave, cl.nombre AS cliente, od.idEstadoDosificacion, ed.estado, ed.porcentaje, ds.id AS idDS, ds.cantidadEntregada, " +
                       "ut.id AS idUTransporte, ut.nombre AS uTransporte, ut.color, ch.nombre AS chofer, od.colorBell, od.folio, od.folioR ";
                    string fromComand = "ordenes AS o INNER JOIN solicitudes AS s ON o.idSolicitud = s.id INNER JOIN clientes AS cl ON s.idCliente = cl.id INNER JOIN " +
                         "ordenDosificacion AS od ON o.id = od.idOrden INNER JOIN productos AS p ON od.idProducto = p.id INNER JOIN unidadesDeMedida AS udm ON od.idUDM = udm.id LEFT OUTER JOIN " +
                         "tiposUnidadTransporte AS tut ON od.idTUT = tut.id INNER JOIN proyectos AS pr ON s.idProyecto = pr.id INNER JOIN estadosDosificacion AS ed ON od.idEstadoDosificacion = ed.id INNER JOIN " +
                         "detallesSolicitud AS ds ON od.idDetalleSolicitud = ds.id INNER JOIN unidadesTransporte AS ut ON od.idUnidadTransporte = ut.id INNER JOIN " +
                         "unidadesTChoferes AS utc ON od.idUnidadTransporte = utc.idUnidad INNER JOIN usuarios AS ch ON utc.idChofer = ch.id INNER JOIN tiposProductos AS tp ON p.idTipoProducto = tp.id ";
                    string whereComand = "(od.eliminado IS NULL) AND (od.idOrden = @id) AND (utc.activo=1) ";
                    whereComand += "AND s.idSucursal = " + idSucursal;
                    string orderByComand = "od.fecha ASC, od.hora ASC, od.idOrden, od.id ASC;";
                    if (!finish)
                        whereComand += " AND od.idEstadoDosificacion < 11";

                    switch (opc)
                    {
                        case 0:
                            whereComand += " AND od.fecha <= '" + fFechaI + "'";
                            break;
                        case 1:
                            string a = "2021-05-22";
                            whereComand += " AND od.fecha <= '" + fFechaI + "'";
                            break;
                        case 2:
                            whereComand += " AND od.fecha = '" + fFechaI + "'";
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                        case 5:
                            whereComand += " AND od.fecha BETWEEN '" + fFechaI + "' AND '" + fFechaF + "'";
                            break;
                    }

                    using (SqlCommand cmd = new SqlCommand("SELECT " + selectComand + "FROM " + fromComand + "WHERE " + whereComand + " ORDER BY " + orderByComand, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idO));
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

        public DataTable proyeccion(int opc, string fFechaI, string fFechaF = "", string tp = "", string opt = "")
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    string selectComand = "SELECT ISNULL(SUM(ds.cantidad), 0) AS cantidadSolicitada, ISNULL(SUM(ds.cantidadEntregada), 0) AS cantidadEntregada ";
                    string fromComand = "FROM ordenes AS o INNER JOIN solicitudes AS s ON o.idSolicitud = s.id INNER JOIN detallesSolicitud AS ds ON s.id = ds.idSolicitud INNER JOIN productos AS p ON ds.idProducto = p.id " +
                        "INNER JOIN tiposProductos AS tp ON p.idTipoProducto = tp.id ";
                    string whereComand = "WHERE (o.eliminado IS NULL) AND (s.eliminada IS NULL) AND (ds.eliminado IS NULL) AND (s.idSucursal = @idSucursal) ";

                    switch (opc)
                    {
                        case 0:
                            whereComand += " AND (o.fecha <= '" + fFechaI + "')";
                            break;
                        case 1:
                            whereComand += " AND (o.fecha = '" + fFechaI + "')";
                            break;
                        case 2:
                            whereComand += " AND (o.fecha = '" + fFechaI + "')";
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                        case 5:
                            whereComand += " AND (o.fecha BETWEEN '" + fFechaI + "' AND '" + fFechaF + "')";
                            break;
                    }

                    if (!tp.Equals(""))
                        whereComand += " AND ( LOWER(tp.tipo) LIKE '%" + tp + "%')";
                    if (!opt.Equals(""))
                    {
                        if (opt.Equals("otro"))
                            whereComand += "AND (LOWER(p.codigo)  NOT LIKE '%td%') AND (LOWER(p.codigo)  NOT LIKE '%bba%')";
                        else
                            whereComand += " AND (LOWER(p.codigo) LIKE '%" + opt + "%')";
                    }

                    using (SqlCommand cmd = new SqlCommand(selectComand + fromComand + whereComand, conn))
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

        public DataTable proyeccionOLD(int opc, string fFechaI = "", string fFechaF = "")
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    string selectComand = "SELECT od.id, od.idSucursal, p.codigo, tp.tipo, od.cantidad, od.idEstadoDosificacion ";
                    string fromComand = "FROM ordenDosificacion AS od INNER JOIN productos AS p ON od.idProducto = p.id INNER JOIN tiposProductos AS tp ON p.idTipoProducto = tp.id ";
                    string whereComand = "WHERE (od.eliminado IS NULL) AND od.idSucursal = @idSucursal ";

                    switch (opc)
                    {
                        case 0:
                            whereComand += " AND od.fecha <= '" + fFechaI + "'";
                            break;
                        case 1:
                            whereComand += " AND od.fecha = '" + fFechaI + "'";
                            break;
                        case 2:
                            whereComand += " AND od.fecha = '" + fFechaI + "'";
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                        case 5:
                            whereComand += " AND od.fecha BETWEEN '" + fFechaI + "' AND '" + fFechaF + "'";
                            break;
                    }

                    using (SqlCommand cmd = new SqlCommand(selectComand + fromComand + whereComand, conn))
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
        public DataTable obtenerRepODDFiltered(int idO, int opc = 1, bool finish = false, string fFechaI = "", string fFechaF = "", bool fProgramado = true)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    string selectComand = "od.id, od.idOrden, FORMAT(od.fecha, 'dd/MM/yyyy') as fecha, od.hora, p.id AS idProducto, p.codigo, p.descripcion, tp.tipo AS tp, od.cantidad, udm.unidad, ds.revenimiento, tut.tipo, od.folio, " +
                       "pr.nombre, pr.calle, pr.numero, pr.interior, pr.colonia, pr.cp, cl.clave, cl.nombre AS cliente, od.idEstadoDosificacion, ed.estado, ed.porcentaje, ds.id AS idDS, ds.cantidadEntregada, " +
                       "ut.id AS idUTransporte, ut.nombre AS uTransporte, ut.color, ch.nombre AS chofer, od.colorBell, od.folio, od.folioR, od.idSucursal ";
                    string fromComand = "ordenes AS o INNER JOIN solicitudes AS s ON o.idSolicitud = s.id INNER JOIN clientes AS cl ON s.idCliente = cl.id INNER JOIN " +
                         "ordenDosificacion AS od ON o.id = od.idOrden INNER JOIN productos AS p ON od.idProducto = p.id INNER JOIN unidadesDeMedida AS udm ON od.idUDM = udm.id LEFT OUTER JOIN " +
                         "tiposUnidadTransporte AS tut ON od.idTUT = tut.id INNER JOIN proyectos AS pr ON s.idProyecto = pr.id INNER JOIN estadosDosificacion AS ed ON od.idEstadoDosificacion = ed.id INNER JOIN " +
                         "detallesSolicitud AS ds ON od.idDetalleSolicitud = ds.id INNER JOIN unidadesTransporte AS ut ON od.idUnidadTransporte = ut.id INNER JOIN " +
                         "unidadesTChoferes AS utc ON od.idUnidadTransporte = utc.idUnidad INNER JOIN usuarios AS ch ON utc.idChofer = ch.id INNER JOIN tiposProductos AS tp ON p.idTipoProducto = tp.id ";
                    string whereComand = "(od.eliminado IS NULL) AND (od.idOrden = @id) AND (utc.activo=1) ";
                    string orderByComand = "od.fecha ASC, od.hora ASC, od.idOrden, od.id ASC;";
                    if (!finish)
                        whereComand += " AND od.idEstadoDosificacion < 11";

                    switch (opc)
                    {
                        case 0:
                            whereComand += " AND od.fecha <= '" + fFechaI + "'";
                            break;
                        case 1:
                            whereComand += " AND od.fecha = '" + fFechaI + "'";
                            break;
                        case 2:
                            whereComand += " AND od.fecha = '" + fFechaI + "'";
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                        case 5:
                            whereComand += " AND od.fecha BETWEEN '" + fFechaI + "' AND '" + fFechaF + "'";
                            break;
                    }

                    using (SqlCommand cmd = new SqlCommand("SELECT " + selectComand + "FROM " + fromComand + "WHERE " + whereComand + " ORDER BY " + orderByComand, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idO));
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

        public DataTable obtenerAlertasByIdSucursal(int id)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM configAlertaP WHERE idSucursal = @id AND eliminado IS NULL ORDER BY tiempo DESC", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", id));
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
        public string getDiffTime(DateTime dTime, DataTable dt)
        {
            try
            {
                TimeSpan result = DateTime.Now.Subtract(dTime);
                if (dTime < DateTime.Now)
                    return "#FF0000";
                float min = Math.Abs(float.Parse(result.TotalMinutes.ToString()));
                string sColor = "#FFFFFF";
                foreach (DataRow dr in dt.Rows)
                {
                    int tiempo = int.Parse(dr["tiempo"].ToString());
                    if (min <= tiempo)
                        sColor = dr["color"].ToString();
                }
                return sColor;
            }
            catch (Exception)
            {
                return "#FF0000";
            }
        }
        //Método para obtener el nombre del estado de dosificación
        public string getEstadoDosificacion(int idE, string value = "")
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    string comando = "";
                    if (value.Equals(""))
                        comando = "SELECT estado FROM estadosDosificacion WHERE(id = @id)";
                    else
                        comando = "SELECT estado FROM estadosDosificacion WHERE (orden = @id) AND (contiene = @value)";
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idE));
                        if (!value.Equals(""))
                            cmd.Parameters.Add(new SqlParameter("@value", value));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    return reader["estado"].ToString();
                                }
                                return "";
                            }
                        }
                        return "";
                    }
                }
            }
            catch (Exception ex)
            {
                return "";
                throw (ex);
            }
        }

        //Método para obtener el nombre del estado de Solicitud
        public string getEstadoSolicitud(int idS)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    string comando = "";
                    comando = "SELECT idEstadoSolicitud FROM solicitudes WHERE (id = @idS)";
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idS", idS));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    return reader["idEstadoSolicitud"].ToString();
                                }
                                return "";
                            }
                        }
                        return "";
                    }
                }
            }
            catch (Exception ex)
            {
                return "";
                throw (ex);
            }
        }

        public bool existeMTD(int idOD)
        {
            try
            {
                string comando = "SELECT idOD FROM dosificacionTotalMaterial WHERE idOD = @idOD";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idOD", idOD));
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

        public void executeSQL(string sSQL)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sSQL, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {

                throw (ex);
            }
        }
        public bool existeBitacora(int idOD)
        {
            try
            {
                string comando = "SELECT idOD FROM bitacoraDosificacion WHERE idOD = @idOD";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idOD", idOD));
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

        public void saveDosificacionTotal(int idOD, string dtMaterial, float dtCantidad, string dtUnidad)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO dosificacionTotalMaterial(idOD, material, cantidad, unidad) VALUES(@idOD, @material, @cantidad, @unidad)", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idOD", idOD));
                        cmd.Parameters.Add(new SqlParameter("@material", dtMaterial));
                        cmd.Parameters.Add(new SqlParameter("@cantidad", dtCantidad.ToString("0.00")));
                        cmd.Parameters.Add(new SqlParameter("@unidad", dtUnidad));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public DataTable getDosificacionTotalDeMaterial(int idOD)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT material, cantidad, unidad FROM dosificacionTotalMaterial WHERE idOD = @idOD", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idOD", idOD));


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

        public string getPorcentaje(decimal p)
        {
            try
            {
                if (p == 1)
                {
                    return "100%";
                }
                else if (p.ToString().Substring(0, 2).Equals("0.") && p.ToString().Length.Equals(3))
                {
                    return p.ToString().Substring(2, 1).ToString() + "0%";
                }
                else
                {
                    return int.Parse(p.ToString().Substring(2, 2).ToString()).ToString() + "%";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void addOnBitacora(int idOD, string material, float cantidad, string unidad, string porcentaje, string progreso)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO bitacoraDosificacion(idOD, material, cantidad, unidad, porcentaje, progreso, fecha) VALUES(@idOD, @material, @cantidad, @unidad, @porcentaje, @progreso, GETDATE())", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idOD", idOD));
                        cmd.Parameters.Add(new SqlParameter("@material", material));
                        cmd.Parameters.Add(new SqlParameter("@cantidad", cantidad.ToString("0.00")));
                        cmd.Parameters.Add(new SqlParameter("@unidad", unidad));
                        cmd.Parameters.Add(new SqlParameter("@porcentaje", porcentaje));
                        cmd.Parameters.Add(new SqlParameter("@progreso", progreso));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public DataTable getBitacoraDosificacion(int idOD)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT od.folio, p.codigo, bd.material, bd.cantidad, bd.unidad, bd.porcentaje, bd.progreso " + 
                        "FROM bitacoraDosificacion AS bd INNER JOIN ordenDosificacion AS od ON bd.idOD = od.id INNER JOIN productos AS p ON od.idProducto = p.id " +
                        "WHERE(bd.idOD = @idOD)", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idOD", idOD));


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

        public void actualizarEstadoDosificacion(int idO, int idED, int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE ordenDosificacion SET idEstadoDosificacion = @idEstadoDosificacion, idUsuarioMod = @idUsuarioMod, fechaMod = GETDATE() WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idEstadoDosificacion", idED));
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioMod", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@id", idO));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        cBit.insertar(idO, "Update", idUsuarioActivo, "ordenDosificacion", "Se Actualizó campo Estado de dosificacion a = " + idED.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void actualizarEstadoSolicitud(int idS, int idES, int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE solicitudes SET idEstadoSolicitud = @idEstadoSolicitud, idUsuarioMod = @idUsuarioMod, fechaMod = GETDATE() WHERE id = @idS", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idEstadoSolicitud", idES));
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioMod", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@idS", idS));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //metodo para actualizar el color de la alarma 
        public void asignarColorBell(int idOD, string cb, int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE ordenDosificacion SET colorBell = @colorBell, idUsuarioMod = @idUsuarioMod, fechaMod = GETDATE() WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@colorBell", cb));
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioMod", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@id", idOD));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        cBit.insertar(idOD, "Update", idUsuarioActivo, "ordenDosificacion", "Se Actualizó campo colorBell a = " + colorBell);
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        //metodo para actualizar Estado Unidad 
        public void actualizarEstado(int idU, int idEstadoUnidad, int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE unidadesTransporte SET idEstadoUnidad = @idEstadoUnidad, " +
                        "idUsuarioMod = @idUsuarioMod, fechaMod = GETDATE() WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idEstadoUnidad", idEstadoUnidad));
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
                        cmd.Parameters.Add(new SqlParameter("@cantidadEntregada", cE.ToString("0.00")));
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

        //metodo para actualizar Estado Unidad de Transporte
        public void actualizarEstadoUnidadTransporte(int idU, int idEU, int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE unidadesTransporte SET idEstadoUnidad = @idEstadoUnidad, " +
                        "idUsuarioMod = @idUsuarioMod, fechaMod = GETDATE() WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idEstadoUnidad", idEU));
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

        //metodo para actualizar la Unidad asignada a la Orden de entrega
        public void asignarUnidadTransporteAOD(int idO, int idUT, int idUsuarioActivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE ordenDosificacion SET idUnidadTransporte = @idUnidadTransporte, idUsuarioMod = @idUsuarioMod, fechaMod = GETDATE() WHERE id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idUnidadTransporte", idUT));
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioMod", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@id", idO));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        cBit.insertar(idO, "Update", idUsuarioActivo, "ordenDosificacion", "Se Actualizó campo idUnidad de Transporte = " + idUT.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        //metodo para obtener Las unidades Disponibles segun el tipo de Unidad
        public DataTable obtenerUDByIDOD(int idOD)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT od.id, od.folio, p.codigo, od.cantidad, p.carga, tp.id AS idTP, tp.tipo AS tipoP, tut.id AS idTUT, tut.tipo, tut.capacidad AS capacidadTUT, " +
                        "ut.id AS idUT, ut.nombre, ut.capacidad AS capacidadUT, ut.capacidadMax AS capacidadUTMax, eu.estado FROM ordenDosificacion AS od INNER JOIN " +
                        "productos AS p ON od.idProducto = p.id INNER JOIN tiposProductos AS tp ON p.idTipoProducto = tp.id INNER JOIN tiposUnidadTransporte AS tut ON tp.id = tut.idTipoProducto INNER JOIN " +
                         "unidadesTransporte AS ut ON tut.id = ut.idTipoUT INNER JOIN estadosUnidad AS eu ON ut.idEstadoUnidad = eu.id " +
                        "WHERE(od.id = @id) AND(eu.id = 1)", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idOD));
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

        public DataTable obtenerBitacoraDosificacion()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT s.folio, od.folio AS remision, bd.material, bd.cantidad, bd.unidad, bd.porcentaje, bd.progreso, p.codigo AS producto " +
                        "FROM bitacoraDosificacion AS bd INNER JOIN ordenDosificacion AS od ON bd.idOD = od.id INNER JOIN ordenes AS o ON od.idOrden = o.id INNER JOIN " +
                        "solicitudes AS s ON o.idSolicitud = s.id INNER JOIN productos AS p ON od.idProducto = p.id", conn))
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
        public DataTable obtenerActualBacheos(int idOD, string material)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT material, FORMAT(SUM(cantidad),'N2') AS cant, unidad  FROM bitacoraDosificacion " +
                        " WHERE idOD = @idOD AND material = @material GROUP BY material, unidad", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idOD", idOD));
                        cmd.Parameters.Add(new SqlParameter("@material", material));


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

        //metodo para obtener el total de dosificado por Orden
        public float obtenerQtyByOrdenAndIdProductoDosificada(int idO, int idP)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT SUM(od.cantidad) AS Total " +
                        "FROM ordenDosificacion AS od INNER JOIN productos AS p ON od.idProducto = p.id INNER JOIN unidadesDeMedida AS udm ON od.idUDM = udm.id INNER JOIN " +
                         "ordenes AS o ON od.idOrden = o.id INNER JOIN solicitudes AS s ON o.idSolicitud = s.id INNER JOIN tiposProductos AS tp ON p.idTipoProducto = tp.id " +
                         "WHERE(od.idOrden = @idOrden) AND (p.id = @idP) AND (od.eliminado IS NULL) AND (od.idEstadoDosificacion > 4) AND (od.idEstadoDosificacion < 12)", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idOrden", idO));
                        cmd.Parameters.Add(new SqlParameter("@idP", idP));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    try
                                    {
                                        return float.Parse(reader["Total"].ToString());
                                    }
                                    catch (Exception)
                                    {
                                        return 0;
                                    }
                                }
                                return 0;
                            }
                        }
                        return 0;
                    }
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public string obtenerCPByAsentaAndIdC(string asenta, int idCiudad)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT cp FROM codigosPostales WHERE (idCiudad = @idCiudad) AND (asenta = @asenta)", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idCiudad", idCiudad));
                        cmd.Parameters.Add(new SqlParameter("@asenta", asenta));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            return dt.Rows[0][0].ToString();
                        }
                    }

                }

            }
            catch (Exception)
            {
                return "";
            }
        }
        //metodo para el combobox de proyectos
        public DataTable obtenerCiudadesByIdEstado(int idEst)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT idCiudad AS id, ciudad FROM ciudades WHERE (idEstado=" + idEst + ") ORDER BY ciudad", conn))
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
        public DataTable obtenerEstados()
        {
            try
            {
                string comando = "SELECT id, estado FROM estados";

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
        public DataTable getColoniasByCP(int cp)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM codigosPostales WHERE(cp = @cp)", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@cp", cp));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            try
                            {
                                return dt;
                            }
                            catch (Exception)
                            {
                                return null;
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
        public DataTable getColoniasByIdC(int idC)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT asenta FROM codigosPostales WHERE (idCiudad = @idCiudad)", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idCiudad", idC));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            try
                            {
                                return dt;
                            }
                            catch (Exception)
                            {
                                return null;
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
        //metodo para el combobox de Elementos
        public DataTable obtenerByTipoProductoAndIdSucursal(int idTipoProducto)
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
        public DataTable obtenerDetallesSolicitud(int idDS, int idSucursal)
        {
            try
            {
                DataTable dt = new DataTable();
                string comando = "SELECT ds.id, ds.idSolicitud, ds.programada, ds.idElemento, e.elemento, ds.idProducto, p.codigo, p.descripcion, p.carga, udm.unidad, udm.id AS idUDM, ds.cantidad, ds.tamano, " +
                    "ds.cantidadEntregada, ds.revenimiento, p.idTipoProducto, tp.tipo, ds.precioU, ds.idFactor, pf.factor, pf.porcentaje, ds.precioF, ds.subtotal, ds.iva, ds.total, ds.precioFIVA " +
                    "FROM detallesSolicitud AS ds INNER JOIN productos AS p ON ds.idProducto = p.id INNER JOIN unidadesDeMedida AS udm ON p.idUDM = udm.id INNER JOIN " +
                    "tiposProductos AS tp ON p.idTipoProducto = tp.id INNER JOIN productosFactor AS pf ON ds.idFactor = pf.id LEFT JOIN elementos AS e ON ds.idElemento = e.id " +
                    "WHERE(ds.id = @id) AND(ds.eliminado IS NULL) AND (p.idSucursal=@idSucursal)";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", idDS));
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

        //metodo para obtener la solicitud por ID
        public DataTable obtenerSolicitudByID(int idS)
        {
            try
            {
                DataTable dt = new DataTable();
                string comando = "SELECT o.id, o.folio, o.idSolicitud, o.fecha, o.hora, o.comentarios, o.ubicacion, c.clave, c.nombre, s.idEstadoSolicitud, s.idProyecto, s.idFormaPago,  " +
                    "s.idVendedor, o.idVendedor AS oIdVendedor, s.idCliente, s.folio AS folioS, o.comentarios, s.reqFac FROM ordenes AS o INNER JOIN solicitudes AS s ON o.idSolicitud = s.id INNER JOIN clientes AS c ON s.idCliente = c.id " +
                    "WHERE o.idSolicitud = @id";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
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

        //Agregado - Metodo para sumar la cantidad entregada de las remisiones - Enrique Sandoval 10-11-2022
        public string obtenerCantE(int idO) {
            try {
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    string comando = "";
                    comando = "SELECT ISNULL(SUM(cantidad),0) AS cantidadEntregada FROM ordenDosificacion WHERE idOrden = @id0 AND idEstadoDosificacion in (7,8,9,10,11)";
                    using (SqlCommand cmd = new SqlCommand(comando, conn)) {
                        cmd.Parameters.Add(new SqlParameter("@id0", idO));
                        using (SqlDataReader reader = cmd.ExecuteReader()) {
                            if (reader.HasRows) {
                                while (reader.Read()) {
                                    string param = reader["cantidadEntregada"].ToString();
                                    return param;
                                }
                                return "";
                            }
                        }
                        return "";
                    }
                }
            }
            catch (Exception ex) {
                return "";
                throw (ex);
            }
        }

        //Agregado el 10-11-2022 por Enrique Sandoval
        public void setCantidadEntregadaByOrden(int idOrden, string cantidad, int idUsuarioActivo) {
            try {
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE detallesSolicitud SET cantidadEntregada=@cantidad, idUsuarioMod=@idUsuarioMod, fechaMod=GETDATE() WHERE id= (SELECT TOP(1)idDetalleSolicitud FROM ordenDosificacion where idOrden = @idOrden)", conn)) {
                        cmd.Parameters.Add(new SqlParameter("@cantidad", cantidad));
                        cmd.Parameters.Add(new SqlParameter("@idUsuarioMod", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@idOrden", idOrden));

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        //cBit.insertar(idO, "Update", idUsuarioActivo, "ordenes", "Se cambió de estatus a programada, se generaron las órdenes de entrega con la totalidad de los detalles de la solicitud");
                    }
                }
            }
            catch (Exception ex) {
                throw (ex);
            }
        }

        public bool isTravelGroup(int id) {
            try {
                string comando = "SELECT ISNULL(grupoViaje, 0) AS grupoViaje FROM ordenDosificacion WHERE id =@id";
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn)) {
                        cmd.Parameters.Add(new SqlParameter("@id", id));
                        using (SqlDataReader reader = cmd.ExecuteReader()) {
                            if (reader.HasRows) {
                                while (reader.Read()) {
                                    if (float.Parse(reader["grupoViaje"].ToString()) != 0)
                                        return true;
                                    else
                                        return false;
                                }
                            }
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex) {
                return false;
                throw (ex);
            }
        }

        public int getTravelGroup(int id) {
            try {
                string comando = "SELECT ISNULL(grupoViaje, 0) AS grupoViaje FROM ordenDosificacion WHERE id =@id";
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn)) {
                        cmd.Parameters.Add(new SqlParameter("@id", id));
                        using (SqlDataReader reader = cmd.ExecuteReader()) {
                            if (reader.HasRows) {
                                while (reader.Read()) {
                                    if (int.Parse(reader["grupoViaje"].ToString()) != 0)
                                        return int.Parse(reader["grupoViaje"].ToString());
                                    else
                                        return 0;
                                }
                            }
                            return 0;
                        }
                    }
                }
            }
            catch (Exception ex) {
                return 0;
                throw (ex);
            }
        }

        public DataTable obtenerODSinDuplicados(DataTable dtO, string criterio) {
            string idO = "", aux = "0";
            DataTable dtX = new DataTable();
            dtX = dtO.Copy();
            dtX.Clear();
            foreach (DataRow r in dtX.Rows) {
                r.Delete();
            }
            for (int i = 0; i < dtO.Rows.Count; i++) {
                try {
                    idO = dtO.Rows[i][criterio].ToString();
                    //if (i.Equals(0))
                    //    dtX.ImportRow(dtO.Rows[i]);
                    for (int j = 0; j < dtO.Rows.Count; j++) {
                        try {
                            if (dtO.Rows[j][criterio].ToString().Equals(idO)) {
                                if (aux.Equals("0"))
                                    dtX.ImportRow(dtO.Rows[j]);
                                aux = "1";
                                dtO.Rows[j].Delete();
                            }
                        }
                        catch (Exception) {

                        }
                    }
                    aux = "0";
                }
                catch (Exception) {
                }
            }
            return dtX;
        }

        public string obtenerFolioOD(int idOD) {
            try {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT folioR FROM ordenDosificacion WHERE id=@id ", conn)) {
                        cmd.Parameters.Add(new SqlParameter("@id", idOD));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            return dt.Rows[0][0].ToString();
                        }
                    }

                }

            }
            catch (Exception ex) {
                throw (ex);
            }
        }

        //Método para obtener la cantidad entregada por idProducto dentro de una misma orden
        public Dictionary<int, string> obtenerCantEntregadaVariasOD(int idO) {
            Dictionary<int, string> cantidadesPorProducto = new Dictionary<int, string>();
            try {
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    string comando = "SELECT idProducto, ISNULL(SUM(cantidad), 0) AS cantidadEntregada FROM ordenDosificacion WHERE idOrden = @id0 AND idEstadoDosificacion IN (7, 8, 9, 10, 11) GROUP BY idProducto";
                    using (SqlCommand cmd = new SqlCommand(comando, conn)) {
                        cmd.Parameters.Add(new SqlParameter("@id0", idO));
                        using (SqlDataReader reader = cmd.ExecuteReader()) {
                            while (reader.Read()) {
                                int idProducto = reader.GetInt32(0);
                                string cantidadEntregada = reader["cantidadEntregada"].ToString();
                                cantidadesPorProducto.Add(idProducto, cantidadEntregada);
                            }
                        }
                    }
                }
            }
            catch (Exception ex) {
                // Manejar la excepción o registrarla, según sea necesario.
                throw ex;
            }
            return cantidadesPorProducto;
        }

    }
}