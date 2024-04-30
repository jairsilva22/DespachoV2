using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace despacho {
    public class cProductosContpaq {
        //Variable de conexion
        private string cadena;

        public int CIDPRODUCTO { get; set; }
        public string CCODIGOPRODUCTO { get; set; }
        public string CNOMBREPRODUCTO { get; set; }
        public int CIDUNIXML { get; set; }
        public string CCLAVESAT { get; set; }
        public decimal CPRECIO1 { get; set; }
        public decimal CPESOPRODUCTO { get; set; }
        public string CDESCRIPCIONPRODUCTO { get; set; }
        public int CBANCOMPONENTE { get; set; }
        public int idUsuario { get; set; }
        public int idSucursal { get; set; }

        // Constructor vacío
        public cProductosContpaq(string cadena) {
            this.cadena = cadena;
        }

        //Insertar material en la base de datos de contpaq
        public int InsertarMaterial(int idSucursal) {
            CIDPRODUCTO = 0;
            string command = "";
            try {
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(CIDPRODUCTO), 0) + 1 AS NuevoCIDProducto FROM admProductos", conn)) {
                        CIDPRODUCTO = (int)cmd.ExecuteScalar();
                    }
                    switch (idSucursal) {
                        case 1:
                            command = "INSERT INTO admProductos (CIDPRODUCTO, CCODIGOPRODUCTO, CNOMBREPRODUCTO, CIDUNIXML, CCLAVESAT, CPRECIO1, CPESOPRODUCTO, CFECHAALTAPRODUCTO, CTIPOPRODUCTO, CSTATUSPRODUCTO, CIDMONEDA, CDESCRIPCIONPRODUCTO, CMETODOCOSTEO, CFECHABAJA, CFECHAEXTRA, CTIMESTAMP, CFECHAERRORCOSTO, CFECCOSEX1, CFECCOSEX2, CFECCOSEX3, CFECCOSEX4, CFECCOSEX5, CBANPRECIO, CBANIMPUESTO, CDESGLOSAI2, CBANCOMPONENTE, CCONTROLEXISTENCIA, CIDFOTOPRODUCTO, CCOMVENTAEXCEPPRODUCTO, CCOMCOBROEXCEPPRODUCTO, CCOSTOESTANDAR, CMARGENUTILIDAD, CIDUNIDADBASE, CIDUNIDADNOCONVERTIBLE, CIMPUESTO1, CIMPUESTO2, CIMPUESTO3, CRETENCION1, CRETENCION2, CIDPADRECARACTERISTICA1, CIDPADRECARACTERISTICA2, CIDPADRECARACTERISTICA3, CIDVALORCLASIFICACION1, CIDVALORCLASIFICACION2, CIDVALORCLASIFICACION3, CIDVALORCLASIFICACION4, CIDVALORCLASIFICACION5, CIDVALORCLASIFICACION6, CIMPORTEEXTRA1, CIMPORTEEXTRA2, CIMPORTEEXTRA3, CIMPORTEEXTRA4, CPRECIO2, CPRECIO3, CPRECIO4, CPRECIO5, CPRECIO6, CPRECIO7, CPRECIO8, CPRECIO9, CPRECIO10, CBANUNIDADES, CBANCARACTERISTICAS, CBANMETODOCOSTEO, CBANMAXMIN, CBANCODIGOBARRA, CERRORCOSTO, CPRECIOCALCULADO, CESTADOPRECIO, CBANUBICACION, CESEXENTO, CEXISTENCIANEGATIVA, CCOSTOEXT1, CCOSTOEXT2, CCOSTOEXT3, CCOSTOEXT4, CCOSTOEXT5, CMONCOSEX1, CMONCOSEX2, CMONCOSEX3, CMONCOSEX4, CMONCOSEX5, CBANCOSEX, CESCUOTAI2, CESCUOTAI3, CIDUNIDADCOMPRA, CIDUNIDADVENTA, CSUBTIPO, CUSABASCU, CTIPOPAQUE, CPRECSELEC, CNOMODCOMP, CNODESCOMP, CCANTIDADFISCAL, CSEGCONTPRODUCTO1, CSEGCONTPRODUCTO2, CSEGCONTPRODUCTO3, CTEXTOEXTRA1, CTEXTOEXTRA2, CTEXTOEXTRA3, CCODALTERN, CNOMALTERN, CDESCCORTA, CSEGCONTPRODUCTO4, CSEGCONTPRODUCTO5, CSEGCONTPRODUCTO6, CSEGCONTPRODUCTO7, CCTAPRED) " +
                            "VALUES (@cidproducto, @ccodigoproducto, @cnombreproducto, @cidunixml, @cclavesat, @cprecio1, @cpesoproducto, GETDATE(), 1, 1, 1, @cdescripcionproducto, 1, '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', 1, 1, 1, @cbancomponente, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '', '', '', '', '', '', '', '', '', '', '', '', '', ''); SELECT SCOPE_IDENTITY()";
                            break;
                        case 3:
                            command = "INSERT INTO admProductos (CIDPRODUCTO, CCODIGOPRODUCTO, CNOMBREPRODUCTO, CIDUNIXML, CCLAVESAT, CPRECIO1, CPESOPRODUCTO, CFECHAALTAPRODUCTO, CTIPOPRODUCTO, CSTATUSPRODUCTO, CIDMONEDA, CDESCRIPCIONPRODUCTO, CMETODOCOSTEO, CFECHABAJA, CFECHAEXTRA, CTIMESTAMP, CFECHAERRORCOSTO, CFECCOSEX1, CFECCOSEX2, CFECCOSEX3, CFECCOSEX4, CFECCOSEX5, CBANPRECIO, CBANIMPUESTO, CDESGLOSAI2, CBANCOMPONENTE, CCONTROLEXISTENCIA, CIDFOTOPRODUCTO, CCOMVENTAEXCEPPRODUCTO, CCOMCOBROEXCEPPRODUCTO, CCOSTOESTANDAR, CMARGENUTILIDAD, CIDUNIDADBASE, CIDUNIDADNOCONVERTIBLE, CIMPUESTO1, CIMPUESTO2, CIMPUESTO3, CRETENCION1, CRETENCION2, CIDPADRECARACTERISTICA1, CIDPADRECARACTERISTICA2, CIDPADRECARACTERISTICA3, CIDVALORCLASIFICACION1, CIDVALORCLASIFICACION2, CIDVALORCLASIFICACION3, CIDVALORCLASIFICACION4, CIDVALORCLASIFICACION5, CIDVALORCLASIFICACION6, CIMPORTEEXTRA1, CIMPORTEEXTRA2, CIMPORTEEXTRA3, CIMPORTEEXTRA4, CPRECIO2, CPRECIO3, CPRECIO4, CPRECIO5, CPRECIO6, CPRECIO7, CPRECIO8, CPRECIO9, CPRECIO10, CBANUNIDADES, CBANCARACTERISTICAS, CBANMETODOCOSTEO, CBANMAXMIN, CBANCODIGOBARRA, CERRORCOSTO, CPRECIOCALCULADO, CESTADOPRECIO, CBANUBICACION, CESEXENTO, CEXISTENCIANEGATIVA, CCOSTOEXT1, CCOSTOEXT2, CCOSTOEXT3, CCOSTOEXT4, CCOSTOEXT5, CMONCOSEX1, CMONCOSEX2, CMONCOSEX3, CMONCOSEX4, CMONCOSEX5, CBANCOSEX, CESCUOTAI2, CESCUOTAI3, CIDUNIDADCOMPRA, CIDUNIDADVENTA, CSUBTIPO, CUSABASCU, CTIPOPAQUE, CPRECSELEC, CNODESCOMP, CCANTIDADFISCAL, CSEGCONTPRODUCTO1, CSEGCONTPRODUCTO2, CSEGCONTPRODUCTO3, CTEXTOEXTRA1, CTEXTOEXTRA2, CTEXTOEXTRA3, CCODALTERN, CNOMALTERN, CDESCCORTA, CSEGCONTPRODUCTO4, CSEGCONTPRODUCTO5, CSEGCONTPRODUCTO6, CSEGCONTPRODUCTO7, CCTAPRED) " +
                            "VALUES (@cidproducto, @ccodigoproducto, @cnombreproducto, @cidunixml, @cclavesat, @cprecio1, @cpesoproducto, GETDATE(), 1, 1, 1, @cdescripcionproducto, 1, '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', 1, 1, 1, @cbancomponente, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '', '', '', '', '', '', '', '', '', '', '', '', '', ''); SELECT SCOPE_IDENTITY()";
                            break;
                        case 2:
                            command = "INSERT INTO admProductos (CIDPRODUCTO, CCODIGOPRODUCTO, CNOMBREPRODUCTO, CIDUNIXML, CCLAVESAT, CPRECIO1, CPESOPRODUCTO, CFECHAALTAPRODUCTO, CTIPOPRODUCTO, CSTATUSPRODUCTO, CIDMONEDA, CDESCRIPCIONPRODUCTO, CMETODOCOSTEO, CFECHABAJA, CFECHAEXTRA, CTIMESTAMP, CFECHAERRORCOSTO, CFECCOSEX1, CFECCOSEX2, CFECCOSEX3, CFECCOSEX4, CFECCOSEX5, CBANPRECIO, CBANIMPUESTO, CDESGLOSAI2, CBANCOMPONENTE, CCONTROLEXISTENCIA, CIDFOTOPRODUCTO, CCOMVENTAEXCEPPRODUCTO, CCOMCOBROEXCEPPRODUCTO, CCOSTOESTANDAR, CMARGENUTILIDAD, CIDUNIDADBASE, CIDUNIDADNOCONVERTIBLE, CIMPUESTO1, CIMPUESTO2, CIMPUESTO3, CRETENCION1, CRETENCION2, CIDPADRECARACTERISTICA1, CIDPADRECARACTERISTICA2, CIDPADRECARACTERISTICA3, CIDVALORCLASIFICACION1, CIDVALORCLASIFICACION2, CIDVALORCLASIFICACION3, CIDVALORCLASIFICACION4, CIDVALORCLASIFICACION5, CIDVALORCLASIFICACION6, CIMPORTEEXTRA1, CIMPORTEEXTRA2, CIMPORTEEXTRA3, CIMPORTEEXTRA4, CPRECIO2, CPRECIO3, CPRECIO4, CPRECIO5, CPRECIO6, CPRECIO7, CPRECIO8, CPRECIO9, CPRECIO10, CBANUNIDADES, CBANCARACTERISTICAS, CBANMETODOCOSTEO, CBANMAXMIN, CBANCODIGOBARRA, CERRORCOSTO, CPRECIOCALCULADO, CESTADOPRECIO, CBANUBICACION, CESEXENTO, CEXISTENCIANEGATIVA, CCOSTOEXT1, CCOSTOEXT2, CCOSTOEXT3, CCOSTOEXT4, CCOSTOEXT5, CMONCOSEX1, CMONCOSEX2, CMONCOSEX3, CMONCOSEX4, CMONCOSEX5, CBANCOSEX, CESCUOTAI2, CESCUOTAI3, CIDUNIDADCOMPRA, CIDUNIDADVENTA, CSUBTIPO, CUSABASCU, CTIPOPAQUE, CPRECSELEC, CNOMODCOMP, CNODESCOMP, CCANTIDADFISCAL, CSEGCONTPRODUCTO1, CSEGCONTPRODUCTO2, CSEGCONTPRODUCTO3, CTEXTOEXTRA1, CTEXTOEXTRA2, CTEXTOEXTRA3, CCODALTERN, CNOMALTERN, CDESCCORTA, CSEGCONTPRODUCTO4, CSEGCONTPRODUCTO5, CSEGCONTPRODUCTO6, CSEGCONTPRODUCTO7, CCTAPRED) " +
                            "VALUES (@cidproducto, @ccodigoproducto, @cnombreproducto, @cidunixml, @cclavesat, @cprecio1, @cpesoproducto, GETDATE(), 1, 1, 1, @cdescripcionproducto, 1, '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', 1, 1, 1, @cbancomponente, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '', '', '', '', '', '', '', '', '', '', '', '', '', ''); SELECT SCOPE_IDENTITY()";
                            break;
                        case 1006:
                            command = "INSERT INTO admProductos (CIDPRODUCTO, CCODIGOPRODUCTO, CNOMBREPRODUCTO, CIDUNIXML, CCLAVESAT, CPRECIO1, CPESOPRODUCTO, CFECHAALTAPRODUCTO, CTIPOPRODUCTO, CSTATUSPRODUCTO, CIDMONEDA, CDESCRIPCIONPRODUCTO, CMETODOCOSTEO, CFECHABAJA, CFECHAEXTRA, CTIMESTAMP, CFECHAERRORCOSTO, CFECCOSEX1, CFECCOSEX2, CFECCOSEX3, CFECCOSEX4, CFECCOSEX5, CBANPRECIO, CBANIMPUESTO, CDESGLOSAI2, CBANCOMPONENTE, CCONTROLEXISTENCIA, CIDFOTOPRODUCTO, CCOMVENTAEXCEPPRODUCTO, CCOMCOBROEXCEPPRODUCTO, CCOSTOESTANDAR, CMARGENUTILIDAD, CIDUNIDADBASE, CIDUNIDADNOCONVERTIBLE, CIMPUESTO1, CIMPUESTO2, CIMPUESTO3, CRETENCION1, CRETENCION2, CIDPADRECARACTERISTICA1, CIDPADRECARACTERISTICA2, CIDPADRECARACTERISTICA3, CIDVALORCLASIFICACION1, CIDVALORCLASIFICACION2, CIDVALORCLASIFICACION3, CIDVALORCLASIFICACION4, CIDVALORCLASIFICACION5, CIDVALORCLASIFICACION6, CIMPORTEEXTRA1, CIMPORTEEXTRA2, CIMPORTEEXTRA3, CIMPORTEEXTRA4, CPRECIO2, CPRECIO3, CPRECIO4, CPRECIO5, CPRECIO6, CPRECIO7, CPRECIO8, CPRECIO9, CPRECIO10, CBANUNIDADES, CBANCARACTERISTICAS, CBANMETODOCOSTEO, CBANMAXMIN, CBANCODIGOBARRA, CERRORCOSTO, CPRECIOCALCULADO, CESTADOPRECIO, CBANUBICACION, CESEXENTO, CEXISTENCIANEGATIVA, CCOSTOEXT1, CCOSTOEXT2, CCOSTOEXT3, CCOSTOEXT4, CCOSTOEXT5, CMONCOSEX1, CMONCOSEX2, CMONCOSEX3, CMONCOSEX4, CMONCOSEX5, CBANCOSEX, CESCUOTAI2, CESCUOTAI3, CIDUNIDADCOMPRA, CIDUNIDADVENTA, CSUBTIPO, CUSABASCU, CTIPOPAQUE, CPRECSELEC, CNOMODCOMP, CNODESCOMP, CCANTIDADFISCAL, CSEGCONTPRODUCTO1, CSEGCONTPRODUCTO2, CSEGCONTPRODUCTO3, CTEXTOEXTRA1, CTEXTOEXTRA2, CTEXTOEXTRA3, CCODALTERN, CNOMALTERN, CDESCCORTA, CSEGCONTPRODUCTO4, CSEGCONTPRODUCTO5, CSEGCONTPRODUCTO6, CSEGCONTPRODUCTO7, CCTAPRED) " +
                            "VALUES (@cidproducto, @ccodigoproducto, @cnombreproducto, @cidunixml, @cclavesat, @cprecio1, @cpesoproducto, GETDATE(), 1, 1, 1, @cdescripcionproducto, 1, '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', 1, 1, 1, @cbancomponente, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '', '', '', '', '', '', '', '', '', '', '', '', '', ''); SELECT SCOPE_IDENTITY()";
                            break;
                        default:
                            command = "INSERT INTO admProductos (CIDPRODUCTO, CCODIGOPRODUCTO, CNOMBREPRODUCTO, CIDUNIXML, CCLAVESAT, CPRECIO1, CPESOPRODUCTO, CFECHAALTAPRODUCTO, CTIPOPRODUCTO, CSTATUSPRODUCTO, CIDMONEDA, CDESCRIPCIONPRODUCTO, CMETODOCOSTEO, CFECHABAJA, CFECHAEXTRA, CTIMESTAMP, CFECHAERRORCOSTO, CFECCOSEX1, CFECCOSEX2, CFECCOSEX3, CFECCOSEX4, CFECCOSEX5, CBANPRECIO, CBANIMPUESTO, CDESGLOSAI2, CBANCOMPONENTE, CCONTROLEXISTENCIA, CIDFOTOPRODUCTO, CCOMVENTAEXCEPPRODUCTO, CCOMCOBROEXCEPPRODUCTO, CCOSTOESTANDAR, CMARGENUTILIDAD, CIDUNIDADBASE, CIDUNIDADNOCONVERTIBLE, CIMPUESTO1, CIMPUESTO2, CIMPUESTO3, CRETENCION1, CRETENCION2, CIDPADRECARACTERISTICA1, CIDPADRECARACTERISTICA2, CIDPADRECARACTERISTICA3, CIDVALORCLASIFICACION1, CIDVALORCLASIFICACION2, CIDVALORCLASIFICACION3, CIDVALORCLASIFICACION4, CIDVALORCLASIFICACION5, CIDVALORCLASIFICACION6, CIMPORTEEXTRA1, CIMPORTEEXTRA2, CIMPORTEEXTRA3, CIMPORTEEXTRA4, CPRECIO2, CPRECIO3, CPRECIO4, CPRECIO5, CPRECIO6, CPRECIO7, CPRECIO8, CPRECIO9, CPRECIO10, CBANUNIDADES, CBANCARACTERISTICAS, CBANMETODOCOSTEO, CBANMAXMIN, CBANCODIGOBARRA, CERRORCOSTO, CPRECIOCALCULADO, CESTADOPRECIO, CBANUBICACION, CESEXENTO, CEXISTENCIANEGATIVA, CCOSTOEXT1, CCOSTOEXT2, CCOSTOEXT3, CCOSTOEXT4, CCOSTOEXT5, CMONCOSEX1, CMONCOSEX2, CMONCOSEX3, CMONCOSEX4, CMONCOSEX5, CBANCOSEX, CESCUOTAI2, CESCUOTAI3, CIDUNIDADCOMPRA, CIDUNIDADVENTA, CSUBTIPO, CUSABASCU, CTIPOPAQUE, CPRECSELEC, CNOMODCOMP, CNODESCOMP, CCANTIDADFISCAL, CSEGCONTPRODUCTO1, CSEGCONTPRODUCTO2, CSEGCONTPRODUCTO3, CTEXTOEXTRA1, CTEXTOEXTRA2, CTEXTOEXTRA3, CCODALTERN, CNOMALTERN, CDESCCORTA, CSEGCONTPRODUCTO4, CSEGCONTPRODUCTO5, CSEGCONTPRODUCTO6, CSEGCONTPRODUCTO7, CCTAPRED) " +
                            "VALUES (@cidproducto, @ccodigoproducto, @cnombreproducto, @cidunixml, @cclavesat, @cprecio1, @cpesoproducto, GETDATE(), 1, 1, 1, @cdescripcionproducto, 1, '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', 1, 1, 1, @cbancomponente, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '', '', '', '', '', '', '', '', '', '', '', '', '', ''); SELECT SCOPE_IDENTITY()";
                            break;
                    }
                    using (SqlCommand cmd = new SqlCommand(command, conn)) {
                        cmd.Parameters.Add(new SqlParameter("@cidproducto", CIDPRODUCTO));
                        cmd.Parameters.Add(new SqlParameter("@ccodigoproducto", CCODIGOPRODUCTO));
                        cmd.Parameters.Add(new SqlParameter("@cnombreproducto", CNOMBREPRODUCTO));
                        cmd.Parameters.Add(new SqlParameter("@cidunixml", CIDUNIXML));
                        cmd.Parameters.Add(new SqlParameter("@cclavesat", CCLAVESAT));
                        cmd.Parameters.Add(new SqlParameter("@cprecio1", CPRECIO1));
                        cmd.Parameters.Add(new SqlParameter("@cpesoproducto", CPESOPRODUCTO));
                        cmd.Parameters.Add(new SqlParameter("@cdescripcionproducto", CDESCRIPCIONPRODUCTO));
                        cmd.Parameters.Add(new SqlParameter("@cbancomponente", CBANCOMPONENTE));
                        // Agrega las demás propiedades correspondientes a las columnas
                        object result = cmd.ExecuteScalar();
                        if (result != DBNull.Value) {
                            CIDPRODUCTO = Convert.ToInt32(result);
                        }
                        else {
                            cLogError error = new cLogError();
                            error.idUsuario = idUsuario;
                            error.idProducto = CIDPRODUCTO;
                            error.error = "ERROR: No se pudo insertar el producto";
                            error.tabla = "CONTPAQ.admProductos";
                            error.metodo = "Insertar";
                            error.insertarError();
                        }
                    }
                }
            }
            catch (Exception ex) {
                cLogError error = new cLogError();
                error.idUsuario = idUsuario;
                error.idProducto = CIDPRODUCTO;
                error.error = ex.Message;
                error.tabla = "CONTPAQ.admProductos";
                error.metodo = "InsertarMaterial";
                error.insertarError();
            }
            return CIDPRODUCTO;
        }
        
        //Insertar producto en la base de datos de contpaq
        public int Insertar(int idSucursal) {
            CIDPRODUCTO = 0;
            string command = "";
            try {
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(CIDPRODUCTO), 0) + 1 AS NuevoCIDProducto FROM admProductos", conn)) {
                        CIDPRODUCTO = (int)cmd.ExecuteScalar();
                    }
                    switch (idSucursal) {
                        case 1:
                            command = "INSERT INTO admProductos (CIDPRODUCTO, CCODIGOPRODUCTO, CNOMBREPRODUCTO, CIDUNIXML, CCLAVESAT, CPRECIO1, CPESOPRODUCTO, CFECHAALTAPRODUCTO, CTIPOPRODUCTO, CSTATUSPRODUCTO, CIDMONEDA, CDESCRIPCIONPRODUCTO, CMETODOCOSTEO, CFECHABAJA, CFECHAEXTRA, CTIMESTAMP, CFECHAERRORCOSTO, CFECCOSEX1, CFECCOSEX2, CFECCOSEX3, CFECCOSEX4, CFECCOSEX5, CBANPRECIO, CBANIMPUESTO, CDESGLOSAI2, CBANCOMPONENTE, CCONTROLEXISTENCIA, CIDFOTOPRODUCTO, CCOMVENTAEXCEPPRODUCTO, CCOMCOBROEXCEPPRODUCTO, CCOSTOESTANDAR, CMARGENUTILIDAD, CIDUNIDADBASE, CIDUNIDADNOCONVERTIBLE, CIMPUESTO1, CIMPUESTO2, CIMPUESTO3, CRETENCION1, CRETENCION2, CIDPADRECARACTERISTICA1, CIDPADRECARACTERISTICA2, CIDPADRECARACTERISTICA3, CIDVALORCLASIFICACION1, CIDVALORCLASIFICACION2, CIDVALORCLASIFICACION3, CIDVALORCLASIFICACION4, CIDVALORCLASIFICACION5, CIDVALORCLASIFICACION6, CIMPORTEEXTRA1, CIMPORTEEXTRA2, CIMPORTEEXTRA3, CIMPORTEEXTRA4, CPRECIO2, CPRECIO3, CPRECIO4, CPRECIO5, CPRECIO6, CPRECIO7, CPRECIO8, CPRECIO9, CPRECIO10, CBANUNIDADES, CBANCARACTERISTICAS, CBANMETODOCOSTEO, CBANMAXMIN, CBANCODIGOBARRA, CERRORCOSTO, CPRECIOCALCULADO, CESTADOPRECIO, CBANUBICACION, CESEXENTO, CEXISTENCIANEGATIVA, CCOSTOEXT1, CCOSTOEXT2, CCOSTOEXT3, CCOSTOEXT4, CCOSTOEXT5, CMONCOSEX1, CMONCOSEX2, CMONCOSEX3, CMONCOSEX4, CMONCOSEX5, CBANCOSEX, CESCUOTAI2, CESCUOTAI3, CIDUNIDADCOMPRA, CIDUNIDADVENTA, CSUBTIPO, CUSABASCU, CTIPOPAQUE, CPRECSELEC, CNOMODCOMP, CNODESCOMP, CCANTIDADFISCAL, CSEGCONTPRODUCTO1, CSEGCONTPRODUCTO2, CSEGCONTPRODUCTO3, CTEXTOEXTRA1, CTEXTOEXTRA2, CTEXTOEXTRA3, CCODALTERN, CNOMALTERN, CDESCCORTA, CSEGCONTPRODUCTO4, CSEGCONTPRODUCTO5, CSEGCONTPRODUCTO6, CSEGCONTPRODUCTO7, CCTAPRED) " +
                            "VALUES (@cidproducto, @ccodigoproducto, @cnombreproducto, @cidunixml, @cclavesat, @cprecio1, @cpesoproducto, GETDATE(), 1, 1, 1, @cdescripcionproducto, 1, '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', 1, 1, 1, @cbancomponente, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '', '', '', '', '', '', '', '', '', '', '', '', '', ''); SELECT SCOPE_IDENTITY()";
                            break;
                        case 3:
                            command = "INSERT INTO admProductos (CIDPRODUCTO, CCODIGOPRODUCTO, CNOMBREPRODUCTO, CIDUNIXML, CCLAVESAT, CPRECIO1, CPESOPRODUCTO, CFECHAALTAPRODUCTO, CTIPOPRODUCTO, CSTATUSPRODUCTO, CIDMONEDA, CDESCRIPCIONPRODUCTO, CMETODOCOSTEO, CFECHABAJA, CFECHAEXTRA, CTIMESTAMP, CFECHAERRORCOSTO, CFECCOSEX1, CFECCOSEX2, CFECCOSEX3, CFECCOSEX4, CFECCOSEX5, CBANPRECIO, CBANIMPUESTO, CDESGLOSAI2, CBANCOMPONENTE, CCONTROLEXISTENCIA, CIDFOTOPRODUCTO, CCOMVENTAEXCEPPRODUCTO, CCOMCOBROEXCEPPRODUCTO, CCOSTOESTANDAR, CMARGENUTILIDAD, CIDUNIDADBASE, CIDUNIDADNOCONVERTIBLE, CIMPUESTO1, CIMPUESTO2, CIMPUESTO3, CRETENCION1, CRETENCION2, CIDPADRECARACTERISTICA1, CIDPADRECARACTERISTICA2, CIDPADRECARACTERISTICA3, CIDVALORCLASIFICACION1, CIDVALORCLASIFICACION2, CIDVALORCLASIFICACION3, CIDVALORCLASIFICACION4, CIDVALORCLASIFICACION5, CIDVALORCLASIFICACION6, CIMPORTEEXTRA1, CIMPORTEEXTRA2, CIMPORTEEXTRA3, CIMPORTEEXTRA4, CPRECIO2, CPRECIO3, CPRECIO4, CPRECIO5, CPRECIO6, CPRECIO7, CPRECIO8, CPRECIO9, CPRECIO10, CBANUNIDADES, CBANCARACTERISTICAS, CBANMETODOCOSTEO, CBANMAXMIN, CBANCODIGOBARRA, CERRORCOSTO, CPRECIOCALCULADO, CESTADOPRECIO, CBANUBICACION, CESEXENTO, CEXISTENCIANEGATIVA, CCOSTOEXT1, CCOSTOEXT2, CCOSTOEXT3, CCOSTOEXT4, CCOSTOEXT5, CMONCOSEX1, CMONCOSEX2, CMONCOSEX3, CMONCOSEX4, CMONCOSEX5, CBANCOSEX, CESCUOTAI2, CESCUOTAI3, CIDUNIDADCOMPRA, CIDUNIDADVENTA, CSUBTIPO, CUSABASCU, CTIPOPAQUE, CPRECSELEC, CNODESCOMP, CCANTIDADFISCAL, CSEGCONTPRODUCTO1, CSEGCONTPRODUCTO2, CSEGCONTPRODUCTO3, CTEXTOEXTRA1, CTEXTOEXTRA2, CTEXTOEXTRA3, CCODALTERN, CNOMALTERN, CDESCCORTA, CSEGCONTPRODUCTO4, CSEGCONTPRODUCTO5, CSEGCONTPRODUCTO6, CSEGCONTPRODUCTO7, CCTAPRED) " +
                            "VALUES (@cidproducto, @ccodigoproducto, @cnombreproducto, @cidunixml, @cclavesat, @cprecio1, @cpesoproducto, GETDATE(), 1, 1, 1, @cdescripcionproducto, 1, '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', 1, 1, 1, @cbancomponente, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '', '', '', '', '', '', '', '', '', '', '', '', '', ''); SELECT SCOPE_IDENTITY()";
                            break;
                        case 2:
                            command = "INSERT INTO admProductos (CIDPRODUCTO, CCODIGOPRODUCTO, CNOMBREPRODUCTO, CIDUNIXML, CCLAVESAT, CPRECIO1, CPESOPRODUCTO, CFECHAALTAPRODUCTO, CTIPOPRODUCTO, CSTATUSPRODUCTO, CIDMONEDA, CDESCRIPCIONPRODUCTO, CMETODOCOSTEO, CFECHABAJA, CFECHAEXTRA, CTIMESTAMP, CFECHAERRORCOSTO, CFECCOSEX1, CFECCOSEX2, CFECCOSEX3, CFECCOSEX4, CFECCOSEX5, CBANPRECIO, CBANIMPUESTO, CDESGLOSAI2, CBANCOMPONENTE, CCONTROLEXISTENCIA, CIDFOTOPRODUCTO, CCOMVENTAEXCEPPRODUCTO, CCOMCOBROEXCEPPRODUCTO, CCOSTOESTANDAR, CMARGENUTILIDAD, CIDUNIDADBASE, CIDUNIDADNOCONVERTIBLE, CIMPUESTO1, CIMPUESTO2, CIMPUESTO3, CRETENCION1, CRETENCION2, CIDPADRECARACTERISTICA1, CIDPADRECARACTERISTICA2, CIDPADRECARACTERISTICA3, CIDVALORCLASIFICACION1, CIDVALORCLASIFICACION2, CIDVALORCLASIFICACION3, CIDVALORCLASIFICACION4, CIDVALORCLASIFICACION5, CIDVALORCLASIFICACION6, CIMPORTEEXTRA1, CIMPORTEEXTRA2, CIMPORTEEXTRA3, CIMPORTEEXTRA4, CPRECIO2, CPRECIO3, CPRECIO4, CPRECIO5, CPRECIO6, CPRECIO7, CPRECIO8, CPRECIO9, CPRECIO10, CBANUNIDADES, CBANCARACTERISTICAS, CBANMETODOCOSTEO, CBANMAXMIN, CBANCODIGOBARRA, CERRORCOSTO, CPRECIOCALCULADO, CESTADOPRECIO, CBANUBICACION, CESEXENTO, CEXISTENCIANEGATIVA, CCOSTOEXT1, CCOSTOEXT2, CCOSTOEXT3, CCOSTOEXT4, CCOSTOEXT5, CMONCOSEX1, CMONCOSEX2, CMONCOSEX3, CMONCOSEX4, CMONCOSEX5, CBANCOSEX, CESCUOTAI2, CESCUOTAI3, CIDUNIDADCOMPRA, CIDUNIDADVENTA, CSUBTIPO, CUSABASCU, CTIPOPAQUE, CPRECSELEC, CNOMODCOMP, CNODESCOMP, CCANTIDADFISCAL, CSEGCONTPRODUCTO1, CSEGCONTPRODUCTO2, CSEGCONTPRODUCTO3, CTEXTOEXTRA1, CTEXTOEXTRA2, CTEXTOEXTRA3, CCODALTERN, CNOMALTERN, CDESCCORTA, CSEGCONTPRODUCTO4, CSEGCONTPRODUCTO5, CSEGCONTPRODUCTO6, CSEGCONTPRODUCTO7, CCTAPRED) " +
                            "VALUES (@cidproducto, @ccodigoproducto, @cnombreproducto, @cidunixml, @cclavesat, @cprecio1, @cpesoproducto, GETDATE(), 1, 1, 1, @cdescripcionproducto, 1, '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', 1, 1, 1, @cbancomponente, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '', '', '', '', '', '', '', '', '', '', '', '', '', ''); SELECT SCOPE_IDENTITY()";
                            break;
                        case 1006:
                            command = "INSERT INTO admProductos (CIDPRODUCTO, CCODIGOPRODUCTO, CNOMBREPRODUCTO, CIDUNIXML, CCLAVESAT, CPRECIO1, CPESOPRODUCTO, CFECHAALTAPRODUCTO, CTIPOPRODUCTO, CSTATUSPRODUCTO, CIDMONEDA, CDESCRIPCIONPRODUCTO, CMETODOCOSTEO, CFECHABAJA, CFECHAEXTRA, CTIMESTAMP, CFECHAERRORCOSTO, CFECCOSEX1, CFECCOSEX2, CFECCOSEX3, CFECCOSEX4, CFECCOSEX5, CBANPRECIO, CBANIMPUESTO, CDESGLOSAI2, CBANCOMPONENTE, CCONTROLEXISTENCIA, CIDFOTOPRODUCTO, CCOMVENTAEXCEPPRODUCTO, CCOMCOBROEXCEPPRODUCTO, CCOSTOESTANDAR, CMARGENUTILIDAD, CIDUNIDADBASE, CIDUNIDADNOCONVERTIBLE, CIMPUESTO1, CIMPUESTO2, CIMPUESTO3, CRETENCION1, CRETENCION2, CIDPADRECARACTERISTICA1, CIDPADRECARACTERISTICA2, CIDPADRECARACTERISTICA3, CIDVALORCLASIFICACION1, CIDVALORCLASIFICACION2, CIDVALORCLASIFICACION3, CIDVALORCLASIFICACION4, CIDVALORCLASIFICACION5, CIDVALORCLASIFICACION6, CIMPORTEEXTRA1, CIMPORTEEXTRA2, CIMPORTEEXTRA3, CIMPORTEEXTRA4, CPRECIO2, CPRECIO3, CPRECIO4, CPRECIO5, CPRECIO6, CPRECIO7, CPRECIO8, CPRECIO9, CPRECIO10, CBANUNIDADES, CBANCARACTERISTICAS, CBANMETODOCOSTEO, CBANMAXMIN, CBANCODIGOBARRA, CERRORCOSTO, CPRECIOCALCULADO, CESTADOPRECIO, CBANUBICACION, CESEXENTO, CEXISTENCIANEGATIVA, CCOSTOEXT1, CCOSTOEXT2, CCOSTOEXT3, CCOSTOEXT4, CCOSTOEXT5, CMONCOSEX1, CMONCOSEX2, CMONCOSEX3, CMONCOSEX4, CMONCOSEX5, CBANCOSEX, CESCUOTAI2, CESCUOTAI3, CIDUNIDADCOMPRA, CIDUNIDADVENTA, CSUBTIPO, CUSABASCU, CTIPOPAQUE, CPRECSELEC, CNOMODCOMP, CNODESCOMP, CCANTIDADFISCAL, CSEGCONTPRODUCTO1, CSEGCONTPRODUCTO2, CSEGCONTPRODUCTO3, CTEXTOEXTRA1, CTEXTOEXTRA2, CTEXTOEXTRA3, CCODALTERN, CNOMALTERN, CDESCCORTA, CSEGCONTPRODUCTO4, CSEGCONTPRODUCTO5, CSEGCONTPRODUCTO6, CSEGCONTPRODUCTO7, CCTAPRED) " +
                            "VALUES (@cidproducto, @ccodigoproducto, @cnombreproducto, @cidunixml, @cclavesat, @cprecio1, @cpesoproducto, GETDATE(), 1, 1, 1, @cdescripcionproducto, 1, '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', 1, 1, 1, @cbancomponente, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '', '', '', '', '', '', '', '', '', '', '', '', '', ''); SELECT SCOPE_IDENTITY()";
                            break;
                        default:
                            command = "INSERT INTO admProductos (CIDPRODUCTO, CCODIGOPRODUCTO, CNOMBREPRODUCTO, CIDUNIXML, CCLAVESAT, CPRECIO1, CPESOPRODUCTO, CFECHAALTAPRODUCTO, CTIPOPRODUCTO, CSTATUSPRODUCTO, CIDMONEDA, CDESCRIPCIONPRODUCTO, CMETODOCOSTEO, CFECHABAJA, CFECHAEXTRA, CTIMESTAMP, CFECHAERRORCOSTO, CFECCOSEX1, CFECCOSEX2, CFECCOSEX3, CFECCOSEX4, CFECCOSEX5, CBANPRECIO, CBANIMPUESTO, CDESGLOSAI2, CBANCOMPONENTE, CCONTROLEXISTENCIA, CIDFOTOPRODUCTO, CCOMVENTAEXCEPPRODUCTO, CCOMCOBROEXCEPPRODUCTO, CCOSTOESTANDAR, CMARGENUTILIDAD, CIDUNIDADBASE, CIDUNIDADNOCONVERTIBLE, CIMPUESTO1, CIMPUESTO2, CIMPUESTO3, CRETENCION1, CRETENCION2, CIDPADRECARACTERISTICA1, CIDPADRECARACTERISTICA2, CIDPADRECARACTERISTICA3, CIDVALORCLASIFICACION1, CIDVALORCLASIFICACION2, CIDVALORCLASIFICACION3, CIDVALORCLASIFICACION4, CIDVALORCLASIFICACION5, CIDVALORCLASIFICACION6, CIMPORTEEXTRA1, CIMPORTEEXTRA2, CIMPORTEEXTRA3, CIMPORTEEXTRA4, CPRECIO2, CPRECIO3, CPRECIO4, CPRECIO5, CPRECIO6, CPRECIO7, CPRECIO8, CPRECIO9, CPRECIO10, CBANUNIDADES, CBANCARACTERISTICAS, CBANMETODOCOSTEO, CBANMAXMIN, CBANCODIGOBARRA, CERRORCOSTO, CPRECIOCALCULADO, CESTADOPRECIO, CBANUBICACION, CESEXENTO, CEXISTENCIANEGATIVA, CCOSTOEXT1, CCOSTOEXT2, CCOSTOEXT3, CCOSTOEXT4, CCOSTOEXT5, CMONCOSEX1, CMONCOSEX2, CMONCOSEX3, CMONCOSEX4, CMONCOSEX5, CBANCOSEX, CESCUOTAI2, CESCUOTAI3, CIDUNIDADCOMPRA, CIDUNIDADVENTA, CSUBTIPO, CUSABASCU, CTIPOPAQUE, CPRECSELEC, CNOMODCOMP, CNODESCOMP, CCANTIDADFISCAL, CSEGCONTPRODUCTO1, CSEGCONTPRODUCTO2, CSEGCONTPRODUCTO3, CTEXTOEXTRA1, CTEXTOEXTRA2, CTEXTOEXTRA3, CCODALTERN, CNOMALTERN, CDESCCORTA, CSEGCONTPRODUCTO4, CSEGCONTPRODUCTO5, CSEGCONTPRODUCTO6, CSEGCONTPRODUCTO7, CCTAPRED) " +
                            "VALUES (@cidproducto, @ccodigoproducto, @cnombreproducto, @cidunixml, @cclavesat, @cprecio1, @cpesoproducto, GETDATE(), 1, 1, 1, @cdescripcionproducto, 1, '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', '1899-12-30', 1, 1, 1, @cbancomponente, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '', '', '', '', '', '', '', '', '', '', '', '', '', ''); SELECT SCOPE_IDENTITY()";
                            break;
                    }
                    using (SqlCommand cmd = new SqlCommand(command, conn)) {
                        cmd.Parameters.Add(new SqlParameter("@cidproducto", CIDPRODUCTO));
                        cmd.Parameters.Add(new SqlParameter("@ccodigoproducto", CCODIGOPRODUCTO));
                        cmd.Parameters.Add(new SqlParameter("@cnombreproducto", CNOMBREPRODUCTO));
                        cmd.Parameters.Add(new SqlParameter("@cidunixml", CIDUNIXML));
                        cmd.Parameters.Add(new SqlParameter("@cclavesat", CCLAVESAT));
                        cmd.Parameters.Add(new SqlParameter("@cprecio1", CPRECIO1));
                        cmd.Parameters.Add(new SqlParameter("@cpesoproducto", CPESOPRODUCTO));
                        cmd.Parameters.Add(new SqlParameter("@cdescripcionproducto", CDESCRIPCIONPRODUCTO));
                        cmd.Parameters.Add(new SqlParameter("@cbancomponente", CBANCOMPONENTE));
                        // Agrega las demás propiedades correspondientes a las columnas

                        cmd.ExecuteScalar();

                    }
                    using (SqlCommand cmd = new SqlCommand("SELECT CIDPRODUCTO FROM admProductos WHERE CIDPRODUCTO = @id", conn)) {
                        cmd.Parameters.Add(new SqlParameter("@id", CIDPRODUCTO));
                        object result = cmd.ExecuteScalar();
                        if (result != DBNull.Value) {
                            CIDPRODUCTO = Convert.ToInt32(result);
                        }
                        else {
                            cLogError error = new cLogError();
                            error.idUsuario = idUsuario;
                            error.idProducto = CIDPRODUCTO;
                            error.error = "ERROR: No se pudo insertar el producto";
                            error.tabla = "CONTPAQ.admProductos";
                            error.metodo = "Insertar";
                            error.insertarError();
                            return 0;
                        }

                        return CIDPRODUCTO;
                    }
                }
            }
            catch (Exception ex) {
                cLogError error = new cLogError();
                error.idUsuario = idUsuario;
                error.idProducto = CIDPRODUCTO;
                error.error = ex.Message;
                error.tabla = "CONTPAQ.admProductos";
                error.metodo = "Insertar";
                error.insertarError();
            }

            return CIDPRODUCTO;
        }

        public int obtenerCIDUNIXML(string unidad) {
            int CIDUnidad = 0;
            try {
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT CIDUNIDAD FROM admUnidadesMedidaPeso WHERE CABREVIATURA = @unidad", conn)) {
                        cmd.Parameters.Add(new SqlParameter("@unidad", unidad));

                        CIDUnidad = (int)cmd.ExecuteScalar();
                        return CIDUnidad;
                    }
                }
            }
            catch (Exception ex) {
                return 0;
            }
        }

        public int obtenerCIDUNIXML(string unidad, string nombreUnidad, string claveSat) {
            int CIDUnidad = 0;

            try {
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT CIDUNIDAD FROM admUnidadesMedidaPeso WHERE CABREVIATURA = @unidad", conn)) {
                        cmd.Parameters.Add(new SqlParameter("@unidad", unidad));

                        object result = cmd.ExecuteScalar();

                        if (result != null) {
                            CIDUnidad = (int)result;
                        }
                        else {
                            // Si no se encontró un registro, insertar uno y obtener el nuevo CIDUNIDAD
                            using (SqlCommand insertCmd = new SqlCommand("INSERT INTO admUnidadesMedidaPeso (CIDUNIDAD, CNOMBREUNIDAD, CABREVIATURA, CDESPLIEGUE, CCLAVEINT, CCLAVESAT) " +
                                "VALUES ((SELECT ISNULL(MAX(CIDUNIDAD), 0) + 1 FROM admUnidadesMedidaPeso), @nombreUnidad, @unidad, @unidad, @claveSat, @claveSat)", conn)) {
                                insertCmd.Parameters.Add(new SqlParameter("@nombreUnidad", nombreUnidad));
                                insertCmd.Parameters.Add(new SqlParameter("@unidad", unidad));
                                insertCmd.Parameters.Add(new SqlParameter("@claveSat", claveSat));

                                insertCmd.ExecuteNonQuery();

                                // Obtener el CIDUNIDAD del nuevo registro
                                using (SqlCommand selectNewCIDCmd = new SqlCommand("SELECT ISNULL(MAX(CIDUNIDAD), 0) FROM admUnidadesMedidaPeso", conn)) {
                                    CIDUnidad = Convert.ToInt32(selectNewCIDCmd.ExecuteScalar());
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex) {
                cLogError error = new cLogError();
                error.idUsuario = idUsuario;
                error.idProducto = CIDPRODUCTO;
                error.error = ex.Message;
                error.tabla = "CONTPAQ.admUnidadesMedidaPeso";
                error.metodo = "obtenerCIDUNIXML";
                error.insertarError();
                return 0;
            }

            return CIDUnidad;
        }


        public bool existeCodigo() {
            try {
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM admProductos WHERE CCODIGOPRODUCTO = @codigo;", conn)) {
                        cmd.Parameters.Add(new SqlParameter("@codigo", CCODIGOPRODUCTO));

                        object result = cmd.ExecuteScalar();
                        return result != null && result != DBNull.Value; // Retorna true si existe el código, false si no.
                    }
                }
            }
            catch (Exception ex) {
                return false;
            }
        }

        //Para buscar materiales en la formulacion
        public bool existeNombre() {
            try {
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM admProductos WHERE CNOMBREPRODUCTO = @nombre;", conn)) {
                        cmd.Parameters.Add(new SqlParameter("@nombre", CNOMBREPRODUCTO));

                        object result = cmd.ExecuteScalar();
                        return result != null && result != DBNull.Value; // Retorna true si existe el código, false si no.
                    }
                }
            }
            catch (Exception ex) {
                return false;
            }
        }

        public int getIdProductoByCodigo() {
            int cidproducto = 0;
            try {
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT CIDPRODUCTO FROM admProductos WHERE CCODIGOPRODUCTO = @codigo", conn)) {
                        cmd.Parameters.Add(new SqlParameter("@codigo", CCODIGOPRODUCTO));

                        cidproducto = (int)cmd.ExecuteScalar();
                        return cidproducto;
                    }
                }
            }
            catch (Exception ex) {
                return 0;
            }
        }

        public int getIdProductoByName() {
            int cidproducto;
            try {
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT CIDPRODUCTO FROM admProductos WHERE CNOMBREPRODUCTO = @nombre", conn)) {
                        cmd.Parameters.Add(new SqlParameter("@nombre", CNOMBREPRODUCTO));

                        cidproducto = (int)cmd.ExecuteScalar();
                        return cidproducto;
                    }
                }
            }
            catch (Exception ex) {
                return 0;
            }
        }

        public void Actualizar() {
            string command = @"UPDATE admProductos
                                            SET
                                                CCODIGOPRODUCTO = @ccodigoproducto,
                                                CNOMBREPRODUCTO = @cnombreproducto,
                                                CIDUNIXML = @cidunixml,
                                                CCLAVESAT = @cclavesat,
                                                CPRECIO1 = @cprecio1,
                                                CPESOPRODUCTO = @cpesoproducto,
                                                CDESCRIPCIONPRODUCTO = @cdescripcionproducto,
                                                CBANCOMPONENTE = @cbancomponente
                                            WHERE CIDPRODUCTO = @cidproducto;";
            try {
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(command, conn)) {
                        cmd.Parameters.Add(new SqlParameter("@cidproducto", CIDPRODUCTO));
                        cmd.Parameters.Add(new SqlParameter("@ccodigoproducto", CCODIGOPRODUCTO));
                        cmd.Parameters.Add(new SqlParameter("@cnombreproducto", CNOMBREPRODUCTO));
                        cmd.Parameters.Add(new SqlParameter("@cidunixml", CIDUNIXML));
                        cmd.Parameters.Add(new SqlParameter("@cclavesat", CCLAVESAT));
                        cmd.Parameters.Add(new SqlParameter("@cprecio1", CPRECIO1));
                        cmd.Parameters.Add(new SqlParameter("@cpesoproducto", CPESOPRODUCTO));
                        cmd.Parameters.Add(new SqlParameter("@cdescripcionproducto", CDESCRIPCIONPRODUCTO));
                        cmd.Parameters.Add(new SqlParameter("@cbancomponente", CBANCOMPONENTE));

                        cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex) {
                cLogError error = new cLogError();
                error.idUsuario = idUsuario;
                error.idProducto = CIDPRODUCTO;
                error.error = ex.Message;
                error.tabla = "CONTPAQ.admProductos";
                error.metodo = "Actualizar";
                error.insertarError();
            }
        }

        //Eliminar formula de contpaq
        public void eliminarFormula() {
            try {
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM admComponentesPaquete WHERE CIDPAQUETE = @cidproducto", conn)) {
                        cmd.Parameters.AddWithValue("@cidproducto", CIDPRODUCTO);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex) {
                cLogError error = new cLogError();
                error.idUsuario = idUsuario;
                error.idProducto = CIDPRODUCTO;
                error.error = ex.Message;
                error.tabla = "CONTPAQ.admComponentesPaquete";
                error.metodo = "eliminarFormula";
                error.insertarError();
            }
        }

        public void agregarFormula(List<int> list, List<decimal> cantidades) {
            for (int i = 0; i < list.Count; i++) {
                try {
                    using (SqlConnection conn = new SqlConnection(cadena)) {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO admComponentesPaquete VALUES((SELECT ISNULL(MAX(CIDCOMPONENTE), 0) + 1 FROM admComponentesPaquete), @cidpaquete, @cidproducto, @ccantidadproducto, 0, 0, 0, 1, 1)", conn)) {
                            cmd.Parameters.Add(new SqlParameter("@cidpaquete", CIDPRODUCTO));
                            cmd.Parameters.Add(new SqlParameter("@cidproducto", list[i]));
                            cmd.Parameters.Add(new SqlParameter("@ccantidadproducto", cantidades[i]));

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex) {
                    cLogError error = new cLogError();
                    error.idUsuario = idUsuario;
                    error.idProducto = CIDPRODUCTO;
                    error.error = ex.Message;
                    error.tabla = "CONTPAQ.admComponentesPaquete";
                    error.metodo = "agregarFormula";
                    error.insertarError();
                }
            }
        }

        //Eliminar producto cambiando el estatus a 0
        public bool eliminarProducto() {
            string command = @"UPDATE admProductos
                                            SET
                                                CSTATUSPRODUCTO = 0 
                                            WHERE CIDPRODUCTO = @cidproducto;";
            try {
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(command, conn)) {
                        cmd.Parameters.Add(new SqlParameter("@cidproducto", CIDPRODUCTO));

                        cmd.ExecuteScalar();
                        return true;
                    }
                }
            }
            catch (Exception ex) {
                cLogError error = new cLogError();
                error.idUsuario = idUsuario;
                error.idProducto = CIDPRODUCTO;
                error.error = ex.Message;
                error.tabla = "CONTPAQ.admProductos";
                error.metodo = "eliminarProducto";
                error.insertarError();
            }
            return false;
        }
    }
}