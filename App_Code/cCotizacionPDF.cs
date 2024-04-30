using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace despacho
{
    public class cCotizacionPDF
    {
        //Clases base referencia
        cSolicitudes cSol = new cSolicitudes();
        cDetallesSolicitud cDS = new cDetallesSolicitud();
        //variables
        private string cadena;
        private bool rev = true;

        //propiedades
        public string path { get; set; }
        public int idSolicitud { get; set; }
        public string folio { get; set; }
        public string nombreDoc { get; set; }
        public string extencion { get; set; }
        public string sObservaciones { get; set; }

        //string cmoneda;

        //Constructor
        public cCotizacionPDF()
        {
            cadena = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;
        }
        public string generarPdf()
        {

            string pathLetter = path + @"fonts\";
            string nombrePDF = nombreDoc;
            string imagen;

            //indicamos el tamaño de la hoja del pdf
            Document document = new Document(PageSize.LETTER);
            PdfWriter pdf;
            //Declaracion de las Fuentes y propiedades utilizadas
            Font fontDatos = new Font(FontFactory.GetFont(pathLetter + "arial.ttf", 12, BaseColor.BLACK));
            Font fontDatos1 = new Font(FontFactory.GetFont(FontFactory.TIMES_BOLD, 12, BaseColor.BLACK));
            Font fontDatos2 = new Font(FontFactory.GetFont(pathLetter + "arial.ttf", 14, BaseColor.BLACK));
            Font fontDatos3 = new Font(FontFactory.GetFont(pathLetter + "arial.ttf", 9, Font.BOLD, BaseColor.WHITE));
            Font fontDatos4 = new Font(FontFactory.GetFont(pathLetter + "arial.ttf", 8, Font.BOLD, BaseColor.WHITE));
            Font fontDatosCot = new Font(FontFactory.GetFont(pathLetter + "arial.ttf", 14, Font.BOLD, BaseColor.BLACK));
            Font fontColObservaciones = new Font(FontFactory.GetFont(pathLetter + "arial.ttf", 9, Font.BOLD, BaseColor.BLACK));
            Font fontObservacines = new Font(FontFactory.GetFont(pathLetter + "arial.ttf", 12, BaseColor.BLACK));
            Font fontObs = new Font(FontFactory.GetFont(pathLetter + "arial.ttf", 7, BaseColor.BLACK));
            Font fontTitulo = new Font(FontFactory.GetFont(pathLetter + "arial.ttf", 10, BaseColor.BLACK));
            Font fontTitulo1 = new Font(FontFactory.GetFont(pathLetter + "arial.ttf", 16, BaseColor.WHITE));
            Font fontTitulo2 = new Font(FontFactory.GetFont(pathLetter + "arial.ttf", 14, BaseColor.WHITE));
            Font fontTitulo0 = new Font(FontFactory.GetFont(pathLetter + "arial.ttf", 11, BaseColor.WHITE));
            Font fontTitulo01 = new Font(FontFactory.GetFont(pathLetter + "arial.ttf", 11, BaseColor.BLACK));
            Font stylo32 = new Font(FontFactory.GetFont(pathLetter + "arial.ttf", 28, Font.BOLD, BaseColor.RED));

            Font fontDatosE = new Font(FontFactory.GetFont(pathLetter + "arial.ttf", 12, Font.BOLD, BaseColor.WHITE));
            //Color de fondo de las columnas de la tabla
            BaseColor ColorHeaders = new BaseColor(0, 204, 51, 100);
            BaseColor colorEnc = new BaseColor(0, 0, 255, 0);
            BaseColor color1 = new BaseColor(255, 255, 255);
            BaseColor color2 = new BaseColor(236, 236, 236);

            try
            {
                DataTable dtSolicitud = cSol.obtenerDTByID(idSolicitud);
                //Obtener el detalle de la solicitud dtSolicitudDetalle.Rows[0][""].ToString()
                DataTable dtSolicitudDetalle = cDS.obtenerDetallesSolicitud(idSolicitud);

                folio = dtSolicitud.Rows[0]["folio"].ToString();
                sObservaciones = dtSolicitud.Rows[0]["observacionesCot"].ToString();
                nombrePDF = nombreDoc + folio + extencion;
                //varificar si existe la carptea si no crearla
                if (!Directory.Exists(path + "\\Cotizacion"))
                {
                    Directory.CreateDirectory(path + "\\Cotizacion");
                }

                //verificamos si existe el archivo
                if (File.Exists(path + "\\Cotizacion\\" + nombrePDF))
                {
                    //si existe lo borramos y creamos uno nuevo
                    File.Delete(nombrePDF);
                    pdf = PdfWriter.GetInstance(document, new FileStream(path + "\\Cotizacion\\" + nombrePDF, FileMode.Create, FileAccess.Write, FileShare.None));
                }
                else
                {
                    //si no existe solo lo creamos
                    pdf = PdfWriter.GetInstance(document, new FileStream(path + "\\Cotizacion\\" + nombrePDF, FileMode.Create, FileAccess.Write, FileShare.None));
                }

                //---------------------------------------------------------------------------------------------------------------------------
                // Imagen del logo
                imagen = path + "\\img\\pepi_logo.png";

                Image imagenLogo = Image.GetInstance(@imagen);
                imagenLogo.ScaleAbsolute(140, 70);

                //abrimos el documento
                document.Open();

                //encabezado --------------------------------------------------------------------------------------------------------------------------

                PdfPTable tablaEncabezado = new PdfPTable(3);
                tablaEncabezado.WidthPercentage = 105;

                PdfPTable tablaEmpresa = new PdfPTable(1); //Tabla de la empresa
                PdfPTable tablaFolio = new PdfPTable(2); //Tabla del folio
                tablaFolio.WidthPercentage = 100; //Ancho de la tabla
                tablaEmpresa.WidthPercentage = 100;  //Ancho de la tabla

                //'-----------------------------------------------------------------------
                //'celdas de la tabla folio---------------------------------------
                PdfPCell cellCotizacion = new PdfPCell(new Phrase("Fecha:", fontDatos1)); //Celda para guardar el texto factura
                                                                                 //      '-----------------------------------------------------------------------

                PdfPCell cellCotizacion1 = new PdfPCell(new Phrase(Convert.ToDateTime(dtSolicitud.Rows[0]["fecha"].ToString()).ToString("dd/MM/yyyy"), fontObservacines)); //Celda para guardar el texto Fecha
                                                                                        //        'celdas de la tabla empresa---------------------------------------
                PdfPCell cellEmpresa = new PdfPCell(new Phrase(dtSolicitud.Rows[0]["razon"].ToString(), fontDatosCot)); //'celda pra guardar el nombre de la empresa
                string sucDireccion = "Dirección. " + dtSolicitud.Rows[0]["sCalle"].ToString() + " #" + dtSolicitud.Rows[0]["sNumero"].ToString();
                if (!dtSolicitud.Rows[0]["sInterior"].ToString().Equals(""))
                    sucDireccion += " int. " + dtSolicitud.Rows[0]["sInterior"].ToString();
                sucDireccion += ", " + dtSolicitud.Rows[0]["sColonia"].ToString() + " C.P. " + dtSolicitud.Rows[0]["sCp"].ToString();
                PdfPCell cellDireccion = new PdfPCell(new Phrase(sucDireccion, fontDatos2)); //'celda para guardar la direccion de la empresa
                // PdfPCell cellDireccion2 = new PdfPCell(new Phrase("R.F.C." + rfcEmpresa, fontColObservaciones)); //'celda para guardar la direccion de la empresa
                cellCotizacion.HorizontalAlignment = Rectangle.ALIGN_RIGHT;// 'Mostra bordes seleccionados

                cellCotizacion.Border = Rectangle.NO_BORDER;// 'Mostra bordes seleccionados

                cellCotizacion1.Border = Rectangle.NO_BORDER;// 'Mostra bordes seleccionados

                //'------------------------------------------------------------------------------
                //'Propiedades de las celdas de la tabla empresa------------------------------------
                //cellCot.BorderColor = new BaseColor(200, 200, 128);
                cellEmpresa.HorizontalAlignment = Rectangle.ALIGN_CENTER; //'Aliniar
                cellEmpresa.Border = Rectangle.NO_BORDER; //'ocultar borldes
                cellDireccion.HorizontalAlignment = Rectangle.ALIGN_CENTER; //'Aliniar
                cellDireccion.Border = Rectangle.NO_BORDER; //'ocultar borldes

                int[] anchoDatosTB = { 50, 50 };// 'Tamaños de celdas de la tabla encabezado

                tablaFolio.SetWidths(anchoDatosTB); //'Ajusta el tamaño de cada columna

                tablaFolio.AddCell(cellCotizacion);// 'Agrega COLUMNA      
                tablaFolio.AddCell(cellCotizacion1);// 'Agrega COLUMNA
                                                    //tablaFolio.AddCell(cellFolio);// 'Agrega COLUMNA    
                                                    //tablaFolio.AddCell(cellFolio1);// 'Agrega COLUMNA 
                                                    //tablaFolio.AddCell(cellFolio2);// 'Agrega COLUMNA    
                                                    //PdfPTable tablaFolio1 = new PdfPTable(1); //Tabla del folio
                                                    //tablaFolio1.WidthPercentage = 100; //Ancho de la tabla 
                                                //'------------------------------------------------------------------------------
                                                //'Propiedades de la tabla empresa------------------------------------

                tablaEmpresa.AddCell(cellEmpresa);// 'Agrega COLUMNA
                tablaEmpresa.AddCell(cellDireccion);// 'Agrega COLUMNA
                                                    // tablaEmpresa.AddCell(cellDireccion2);// 'Agrega COLUMNA

                PdfPCell cell = new PdfPCell(imagenLogo); // 'celda para guardar el logo tipo
                PdfPCell cell2 = new PdfPCell(tablaEmpresa); //'celda para guardar la empresa
                PdfPCell cell3 = new PdfPCell(tablaFolio); //'celda paa guardar la tabla folio
                                                           //PdfPCell cell4 = new PdfPCell(tablaFolio1); //'celda paa guardar la tabla folio

                cell.Border = Rectangle.NO_BORDER; //'ocultar borldes
                cell2.Border = Rectangle.NO_BORDER; //'ocultar borldes
                cell3.Border = Rectangle.NO_BORDER; //'ocultar borldes
                                                    //cell4.Border = Rectangle.NO_BORDER; //'ocultar borldes
                int[] anchoDatos = { 25, 45, 30 };// 'Tamaños de celdas de la tabla encabezado

                tablaEncabezado.SetWidths(anchoDatos); //'Ajusta el tamaño de cada columna
                tablaEncabezado.AddCell(cell); //'Agrega COLUMNA
                tablaEncabezado.AddCell(cell2); //'Agrega COLUMNA
                tablaEncabezado.AddCell(cell3); //'Agrega COLUMNA
                                                //tablaEncabezado.AddCell(cell4); //'Agrega COLUMNA

                //'Propiedades del documento
                document.Add(tablaEncabezado);// ' Agrega la tabla al documento

                //imagen = path + "\\img\\cotizacion.png";
                //imagenLogo = Image.GetInstance(@imagen);
                //imagenLogo.ScaleAbsolute(610, 30);

                //PdfPTable tablaFoot = new PdfPTable(1);

                //PdfPCell cellPaglp = new PdfPCell(imagenLogo); //Celda para guardar el texto RETENCION

                //// cellPaglp.HorizontalAlignment = Rectangle.ALIGN_CENTER;

                //cellPaglp.Border = Rectangle.NO_BORDER;
                //tablaFoot.WidthPercentage = 113; //'Ancho de la tablas

                //tablaFoot.AddCell(cellPaglp); //Agrega COLUMNA
                //tablaFoot.SpacingBefore = 15;
                //document.Add(tablaFoot);

                //tabla detalles del cliente---------------------------------------------------
                //tABLE CON LOS DETALLES DE CLIENTE
                string dias = "";
                DateTime oldDate = DateTime.Now;
                DateTime newDate = DateTime.Now;
                // Difference in days, hours, and minutes.
                TimeSpan ts = newDate - oldDate;

                // Difference in days.
                if (oldDate == newDate)
                {
                    dias = "1";
                }
                else
                {
                    dias = ts.Days.ToString();
                }

                string dia;
                if (ts.Days > 1)
                {
                    dia = ts.Days.ToString();
                }
                else
                {
                    dia = "1";
                }
                dias = dia;

                //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

                string cDireccion = dtSolicitud.Rows[0]["calle"].ToString() + " #" + dtSolicitud.Rows[0]["numero"].ToString();
                if (!dtSolicitud.Rows[0]["interior"].ToString().Equals(""))
                    cDireccion += " int. " + dtSolicitud.Rows[0]["interior"].ToString();
                cDireccion += ", " + dtSolicitud.Rows[0]["colonia"].ToString() + " C.P. " + dtSolicitud.Rows[0]["cp"].ToString();

                string pDireccion = dtSolicitud.Rows[0]["calleP"].ToString() + " #" + dtSolicitud.Rows[0]["numP"].ToString();
                if (!dtSolicitud.Rows[0]["intP"].ToString().Equals(""))
                    pDireccion += " int. " + dtSolicitud.Rows[0]["intP"].ToString();
                pDireccion += ", " + dtSolicitud.Rows[0]["colP"].ToString() + " C.P. " + dtSolicitud.Rows[0]["cpP"].ToString();

                //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

                PdfPTable tablaGral = new PdfPTable(2);
                tablaGral.WidthPercentage = 100;// 'Ancho de la tabla
                tablaGral.DefaultCell.FixedHeight = 80f;
                PdfPTable tablaCustomer = new PdfPTable(1);
                tablaCustomer.WidthPercentage = 100;// 'Ancho de la tabla
                tablaCustomer.DefaultCell.FixedHeight = 80f;
                PdfPTable tablaProject = new PdfPTable(1);
                tablaProject.WidthPercentage = 100;// 'Ancho de la tabla
                tablaProject.DefaultCell.FixedHeight = 80f;

                //celdas de la tabla de articulos-------------------------------------------------
                PdfPCell cellCustomer = new PdfPCell(new Phrase("C  O  T  I  Z  A  C  I  Ó  N", fontTitulo1)); //Celda para guardar el texto Cliente
                PdfPCell cellProject = new PdfPCell(new Phrase("FOLIO: " + dtSolicitud.Rows[0]["folio"].ToString(), fontTitulo1)); //Celda para guardar el texto Proyecto

                //------------------------------------------------------------------------------------
                //Propiedades de las celdas de la tabla de articulos------------------------------------
                cellCustomer.BackgroundColor = new BaseColor(34, 46, 112); //Color del borde
                cellCustomer.Border = 12; //Mostrar bordes seleccionados
                cellCustomer.HorizontalAlignment = Rectangle.ALIGN_CENTER; //Aliniar
                cellCustomer.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                cellCustomer.FixedHeight = 20f;

                cellProject.BackgroundColor = new BaseColor(34, 46, 112); //Color del borde
                cellProject.Border = 12; //Mostrar bordes seleccionados
                cellProject.HorizontalAlignment = Rectangle.ALIGN_RIGHT; //Aliniar
                cellProject.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                cellCustomer.FixedHeight = 20f;

                BaseColor colorB = color1;

                int[] anchoGral = { 50, 50 };// 'Tamaños de las celdas de la tabla Gral
                int[] anchoCustomer = { 100 };// 'Tamaños de las celdas de la tabla Customer
                int[] anchoProject = { 100 };// 'Tamaños de las celdas de la tabla Project

                //------------------------------------------------------------------------------------
                //Propiedades de la tabla de articulos---------------------------------------------------
                tablaGral.SetWidths(anchoGral); //Ajusta el tamaño de cada columna        
                tablaGral.AddCell(cellCustomer); //Agrega COLUMNA
                tablaGral.AddCell(cellProject); //Agrega COLUMNA

                tablaCustomer.SetWidths(anchoCustomer); //Ajusta el tamaño de cada columna        
                //tablaCustomer.AddCell(cellCustomer); //Agrega COLUMNA

                tablaProject.SetWidths(anchoProject); //Ajusta el tamaño de cada columna        
                //tablaProject.AddCell(cellProject); //Agrega COLUMNA


                //celdas de la tabla de Customers Columna1-------------------------------------------------
                PdfPCell cellCustomerF1 = new PdfPCell(new Phrase("Cliente: \n" + dtSolicitud.Rows[0]["cliente"].ToString() + "\n" +
                    "Teléfono: " + dtSolicitud.Rows[0]["telefono"].ToString() + "/" + dtSolicitud.Rows[0]["celular"].ToString() + "\n" +
                    "Correo: " + dtSolicitud.Rows[0]["email"].ToString(), fontDatos));
                //PdfPCell cellCustomerF2 = new PdfPCell(new Phrase(dtSolicitud.Rows[0]["cliente"].ToString(), fontDatos));
                //PdfPCell cellCustomerF3 = new PdfPCell(new Phrase("Teléfono: " + dtSolicitud.Rows[0]["telefono"].ToString() + "/" + dtSolicitud.Rows[0]["celular"].ToString(), fontDatos));
                //PdfPCell cellCustomerF4 = new PdfPCell(new Phrase("Correo: " + dtSolicitud.Rows[0]["email"].ToString(), fontDatos));
                //PdfPCell cellCustomerF5 = new PdfPCell(new Phrase("", fontTitulo));
                //celdas de la tabla de Customers Columna1-------------------------------------------------
                PdfPCell cellProjectF1 = new PdfPCell(new Phrase("Dirección de entrega: \n" + pDireccion, fontDatos));
                //PdfPCell cellProjectF2 = new PdfPCell(new Phrase(pDireccion, fontDatos));
                //PdfPCell cellProjectF3 = new PdfPCell(new Phrase("", fontTitulo));
                //PdfPCell cellProjectF4 = new PdfPCell(new Phrase("", fontTitulo));
                //PdfPCell cellProjectF5 = new PdfPCell(new Phrase("", fontTitulo));

                //Propiedades de las celdas de la tabla articulos
                cellCustomerF1.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cellCustomerF1.Border = Rectangle.NO_BORDER;
                cellCustomerF1.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                cellCustomerF1.BackgroundColor = colorB;
                cellCustomerF1.FixedHeight = 20f;

                //cellCustomerF2.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                //cellCustomerF2.Border = Rectangle.NO_BORDER;
                //cellCustomerF2.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                //cellCustomerF2.BackgroundColor = colorB;
                //cellCustomerF2.FixedHeight = 20f;

                //cellCustomerF3.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                //cellCustomerF3.Border = Rectangle.NO_BORDER;
                //cellCustomerF3.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                //cellCustomerF3.BackgroundColor = colorB;
                //cellCustomerF3.FixedHeight = 20f;

                //cellCustomerF4.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                //cellCustomerF4.Border = Rectangle.NO_BORDER;
                //cellCustomerF4.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                //cellCustomerF4.BackgroundColor = colorB;
                //cellCustomerF4.FixedHeight = 20f;

                //cellCustomerF5.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                //cellCustomerF5.Border = Rectangle.NO_BORDER;
                //cellCustomerF5.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                //cellCustomerF5.BackgroundColor = colorB;
                //cellCustomerF5.FixedHeight = 20f;

                cellProjectF1.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cellProjectF1.Border = Rectangle.NO_BORDER;
                cellProjectF1.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                cellProjectF1.BackgroundColor = colorB;

                //cellProjectF2.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                //cellProjectF2.Border = Rectangle.NO_BORDER;
                //cellProjectF2.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                //cellProjectF2.BackgroundColor = colorB;

                //cellProjectF3.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                //cellProjectF3.Border = Rectangle.NO_BORDER;
                //cellProjectF3.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                //cellProjectF3.BackgroundColor = colorB;

                //cellProjectF4.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                //cellProjectF4.Border = Rectangle.NO_BORDER;
                //cellProjectF4.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                //cellProjectF4.BackgroundColor = colorB;

                //cellProjectF5.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                //cellProjectF5.Border = Rectangle.NO_BORDER;
                //cellProjectF5.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                //cellProjectF5.BackgroundColor = colorB;

                //Inserta los valores en la factura---------------------------------------------------                   
                tablaCustomer.AddCell(cellCustomerF1); //Agrega COLUMNA
                //tablaCustomer.AddCell(cellCustomerF2); //Agrega COLUMNA
                //tablaCustomer.AddCell(cellCustomerF3); //Agrega COLUMNA
                //tablaCustomer.AddCell(cellCustomerF4); //Agrega COLUMNA
                //tablaCustomer.AddCell(cellCustomerF5); //Agrega COLUMNA

                tablaProject.AddCell(cellProjectF1); //Agrega COLUMNA
                //tablaProject.AddCell(cellProjectF2); //Agrega COLUMNA
                //tablaProject.AddCell(cellProjectF3); //Agrega COLUMNA
                //tablaProject.AddCell(cellProjectF4); //Agrega COLUMNA
                //tablaProject.AddCell(cellProjectF5); //Agrega COLUMNA

                tablaGral.AddCell(tablaCustomer);
                tablaGral.AddCell(tablaProject);

                tablaGral.SpacingBefore = 20;
                document.Add(tablaGral);// Agrega la tabla al documento

                //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

                if (dtSolicitudDetalle.Rows[0]["revenimiento"].ToString().Equals("") || dtSolicitudDetalle.Rows[0]["revenimiento"].ToString().Equals("0"))
                    rev = false;

                PdfPTable tablaArticulos;
                if (rev)
                    tablaArticulos = new PdfPTable(6);
                else
                    tablaArticulos = new PdfPTable(5);
                tablaArticulos.WidthPercentage = 100;// 'Ancho de la tabla
                tablaArticulos.DefaultCell.FixedHeight = 80f;


                //celdas de la tabla de articulos-------------------------------------------------
                //PdfPCell cellProducto = new PdfPCell(new Phrase("PRODUCTO", fontDatos3)); //Celda para guardar el texto NUMERO DE PARTE
                PdfPCell cellCodigo = new PdfPCell(new Phrase("CÓDIGO", fontDatos3)); //Celda para guardar el texto CÓDIGO
                PdfPCell cellDescripcion = new PdfPCell(new Phrase("DESCRIPCIÓN", fontDatos3)); //Celda para guardar el texto DESCRIPCION
                PdfPCell cellCantidad = new PdfPCell(new Phrase("CANT", fontDatos3)); //Celda para guardar el texto CANTIDAD
                //PdfPCell cellUDM = new PdfPCell(new Phrase("UNIDAD", fontDatos3)); //Celda para guardar el texto Unidad de Medida
                PdfPCell cellRevenimiento = new PdfPCell(new Phrase("REV", fontDatos3)); //Celda para guardar el texto Revenimiento         
                //PdfPCell cellPrecio = new PdfPCell(new Phrase("P/U", fontDatos3)); //Celda para guardar el texto PRECIO UNITARIO
                //PdfPCell cellFactor = new PdfPCell(new Phrase("FACTOR", fontDatos3)); //Celda para guardar el texto FACTOR
                PdfPCell cellPrecioF = new PdfPCell(new Phrase("PRECIO", fontDatos4)); //Celda para guardar el texto PRECIO CON FACTOR
                PdfPCell cellSubtotalX = new PdfPCell(new Phrase("TOTAL", fontDatos3)); //Celda para guardar el texto SUBTOTAL
                //PdfPCell cellIVA = new PdfPCell(new Phrase("IVA", fontDatos3)); //Celda para guardar el texto IVA
                //PdfPCell cellTotal = new PdfPCell(new Phrase("TOTAL ", fontDatos3)); //Celda para guardar el texto TOTAL

                //--------------------------------------------------------------------------------------
                //celdas de la tabla de articulos------------------------------------------------ -
                //PdfPCell cellProductoVacia = new PdfPCell(new Phrase(" ")); //Celda vacia para dar espacio
                PdfPCell cellCodigoVacia = new PdfPCell(new Phrase(" ")); //Celda vacia para dar espacio
                PdfPCell cellDescripcionVacia = new PdfPCell(new Phrase(" ")); //Celda vacia para dar espacio
                PdfPCell cellCantidadVacia = new PdfPCell(new Phrase(" ")); //Celda vacia para dar espacio
                //PdfPCell cellUDMVacia = new PdfPCell(new Phrase(" ")); //Celda vacia para dar espacio
                PdfPCell cellRevenimientoVacia = new PdfPCell(new Phrase(" ")); //Celda vacia para dar espacio
                //PdfPCell cellPrecioVacia = new PdfPCell(new Phrase(" ")); //Celda vacia para dar espacio
                //PdfPCell cellFactorVacia = new PdfPCell(new Phrase(" ")); //Celda vacia para dar espacio
                PdfPCell cellPrecioFVacia = new PdfPCell(new Phrase(" ")); //Celda vacia para dar espacio
                PdfPCell cellSubtotalVacia = new PdfPCell(new Phrase(" ")); //Celda vacia para dar espacio
                //PdfPCell cellIVAVacia = new PdfPCell(new Phrase(" ")); //Celda vacia para dar espacio
                //PdfPCell cellTotalVacia = new PdfPCell(new Phrase(" ")); //Celda vacia para dar espacio

                //PdfPCell cellProductoVacia1 = new PdfPCell(new Phrase(" ")); //Celda vacia para dar espacio
                PdfPCell cellCodigoVacia1 = new PdfPCell(new Phrase(" ")); //Celda vacia para dar espacio
                PdfPCell cellDescripcionVacia1 = new PdfPCell(new Phrase(" ")); //Celda vacia para dar espacio
                PdfPCell cellCantidadVacia1 = new PdfPCell(new Phrase(" ")); //Celda vacia para dar espacio
                //PdfPCell cellUDMVacia1 = new PdfPCell(new Phrase(" ")); //Celda vacia para dar espacio
                PdfPCell cellRevenimientoVacia1 = new PdfPCell(new Phrase(" ")); //Celda vacia para dar espacio
                //PdfPCell cellPrecioVacia1 = new PdfPCell(new Phrase(" ")); //Celda vacia para dar espacio
                //PdfPCell cellFactorVacia1 = new PdfPCell(new Phrase(" ")); //Celda vacia para dar espacio
                PdfPCell cellPrecioFVacia1 = new PdfPCell(new Phrase(" ")); //Celda vacia para dar espacio
                PdfPCell cellSubtotalVacia1 = new PdfPCell(new Phrase(" ")); //Celda vacia para dar espacio
                //PdfPCell cellIVAVacia1 = new PdfPCell(new Phrase(" ")); //Celda vacia para dar espacio
                //PdfPCell cellTotalVacia1 = new PdfPCell(new Phrase(" ")); //Celda vacia para dar espacio

                //PdfPCell cellProductoVacia2 = new PdfPCell(new Phrase(" ")); //Celda vacia para dar espacio
                PdfPCell cellCodigoVacia2 = new PdfPCell(new Phrase(" ")); //Celda vacia para dar espacio
                PdfPCell cellDescripcionVacia2 = new PdfPCell(new Phrase(" ")); //Celda vacia para dar espacio
                PdfPCell cellCantidadVacia2 = new PdfPCell(new Phrase(" ")); //Celda vacia para dar espacio
                //PdfPCell cellUDMVacia2 = new PdfPCell(new Phrase(" ")); //Celda vacia para dar espacio
                PdfPCell cellRevenimientoVacia2 = new PdfPCell(new Phrase(" ")); //Celda vacia para dar espacio
                //PdfPCell cellPrecioVacia2 = new PdfPCell(new Phrase(" ")); //Celda vacia para dar espacio
                //PdfPCell cellFactorVacia2 = new PdfPCell(new Phrase(" ")); //Celda vacia para dar espacio
                PdfPCell cellPrecioFVacia2 = new PdfPCell(new Phrase(" ")); //Celda vacia para dar espacio
                PdfPCell cellSubtotalVacia2 = new PdfPCell(new Phrase(" ")); //Celda vacia para dar espacio
                //PdfPCell cellIVAVacia2 = new PdfPCell(new Phrase(" ")); //Celda vacia para dar espacio
                //PdfPCell cellTotalVacia2 = new PdfPCell(new Phrase(" ")); //Celda vacia para dar espacio

                //------------------------------------------------------------------------------------
                //Propiedades de las celdas de la tabla de articulos------------------------------------
                //cellProducto.BackgroundColor = new BaseColor(227, 48, 69); //Color del borde
                //cellProducto.Border = Rectangle.NO_BORDER; //Mostrar bordes seleccionados
                //cellProducto.HorizontalAlignment = Rectangle.ALIGN_CENTER; //Aliniar
                //cellProducto.BorderColor = new BaseColor(0, 0, 0); //Color del borde

                cellCodigo.BackgroundColor = new BaseColor(227, 48, 69); //Color del borde
                cellCodigo.Border = Rectangle.NO_BORDER; //Mostrar bordes seleccionados
                cellCodigo.HorizontalAlignment = Rectangle.ALIGN_CENTER; //Aliniar
                cellCodigo.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                cellCodigo.FixedHeight = 20f;

                cellDescripcion.BackgroundColor = new BaseColor(227, 48, 69); //Color del borde
                cellDescripcion.Border = Rectangle.NO_BORDER; //Mostrar bordes seleccionados
                cellDescripcion.HorizontalAlignment = Rectangle.ALIGN_CENTER; //Aliniar
                cellDescripcion.BorderColor = new BaseColor(0, 0, 0); //Color del borde

                cellCantidad.BackgroundColor = new BaseColor(227, 48, 69); //Color del borde
                cellCantidad.Border = Rectangle.NO_BORDER; //Mostrar bordes seleccionados
                cellCantidad.HorizontalAlignment = Rectangle.ALIGN_CENTER; //Aliniar
                cellCantidad.BorderColor = new BaseColor(0, 0, 0); //Color del borde

                //cellUDM.BackgroundColor = new BaseColor(227, 48, 69); //Color del borde
                //cellUDM.Border = Rectangle.NO_BORDER; //Mostrar bordes seleccionados
                //cellUDM.HorizontalAlignment = Rectangle.ALIGN_CENTER; //Aliniar
                //cellUDM.BorderColor = new BaseColor(0, 0, 0); //Color del borde

                cellRevenimiento.BackgroundColor = new BaseColor(227, 48, 69); //Color del borde
                cellRevenimiento.Border = Rectangle.NO_BORDER; //Mostrar bordes seleccionados
                cellRevenimiento.HorizontalAlignment = Rectangle.ALIGN_CENTER; //Aliniar
                cellRevenimiento.BorderColor = new BaseColor(0, 0, 0); //Color del borde

                //cellPrecio.BackgroundColor = new BaseColor(227, 48, 69); //Color del borde
                //cellPrecio.Border = Rectangle.NO_BORDER; //Mostrar bordes seleccionados
                //cellPrecio.HorizontalAlignment = Rectangle.ALIGN_CENTER; //Aliniar
                //cellPrecio.BorderColor = new BaseColor(0, 0, 0); //Color del borde

                //cellFactor.BackgroundColor = new BaseColor(227, 48, 69); //Color del borde
                //cellFactor.Border = Rectangle.NO_BORDER; //Mostrar bordes seleccionados
                //cellFactor.HorizontalAlignment = Rectangle.ALIGN_CENTER; //Aliniar
                //cellFactor.BorderColor = new BaseColor(0, 0, 0); //Color del borde

                cellPrecioF.BackgroundColor = new BaseColor(227, 48, 69); //Color del borde
                cellPrecioF.Border = Rectangle.NO_BORDER; //Mostrar bordes seleccionados
                cellPrecioF.HorizontalAlignment = Rectangle.ALIGN_CENTER; //Aliniar
                cellPrecioF.BorderColor = new BaseColor(0, 0, 0); //Color del borde

                cellSubtotalX.BackgroundColor = new BaseColor(227, 48, 69); //Color del borde
                cellSubtotalX.Border = Rectangle.NO_BORDER; //Mostrar bordes seleccionados
                cellSubtotalX.HorizontalAlignment = Rectangle.ALIGN_CENTER; //Aliniar
                cellSubtotalX.BorderColor = new BaseColor(0, 0, 0); //Color del borde

                //cellIVA.BackgroundColor = new BaseColor(227, 48, 69); //Color del borde
                //cellIVA.Border = Rectangle.NO_BORDER; //Mostrar bordes seleccionados
                //cellIVA.HorizontalAlignment = Rectangle.ALIGN_CENTER; //Aliniar
                //cellIVA.BorderColor = new BaseColor(0, 0, 0); //Color del borde

                //cellTotal.BackgroundColor = new BaseColor(227, 48, 69); //Color del borde
                //cellTotal.Border = Rectangle.NO_BORDER; //Mostrar bordes seleccionados
                //cellTotal.HorizontalAlignment = Rectangle.ALIGN_CENTER; //Aliniar
                //cellTotal.BorderColor = new BaseColor(0, 0, 0); //Color del borde

                //cellProductoVacia1.Border = Rectangle.TOP_BORDER;
                cellCodigoVacia1.Border = Rectangle.TOP_BORDER;
                cellDescripcionVacia1.Border = Rectangle.TOP_BORDER;
                cellCantidadVacia1.Border = Rectangle.TOP_BORDER;
                //cellUDMVacia1.Border = Rectangle.TOP_BORDER;
                cellRevenimientoVacia1.Border = Rectangle.TOP_BORDER;
                //cellPrecioVacia1.Border = Rectangle.TOP_BORDER;
                //cellFactorVacia1.Border = Rectangle.TOP_BORDER;
                cellPrecioFVacia1.Border = Rectangle.TOP_BORDER;
                cellSubtotalVacia1.Border = Rectangle.TOP_BORDER;
                //cellIVAVacia1.Border = Rectangle.TOP_BORDER;
                //cellTotalVacia1.Border = Rectangle.TOP_BORDER;

                //cellProductoVacia2.Border = Rectangle.TOP_BORDER;
                cellCodigoVacia2.Border = Rectangle.TOP_BORDER;
                cellDescripcionVacia2.Border = Rectangle.TOP_BORDER;
                cellCantidadVacia2.Border = Rectangle.TOP_BORDER;
                //cellUDMVacia2.Border = Rectangle.TOP_BORDER;
                cellRevenimientoVacia2.Border = Rectangle.TOP_BORDER;
                //cellPrecioVacia2.Border = Rectangle.TOP_BORDER;
                //cellFactorVacia2.Border = Rectangle.TOP_BORDER;
                cellPrecioFVacia2.Border = Rectangle.TOP_BORDER;
                cellSubtotalVacia2.Border = Rectangle.TOP_BORDER;
                //cellIVAVacia2.Border = Rectangle.TOP_BORDER;
                //cellTotalVacia2.Border = Rectangle.TOP_BORDER;
                
                int[] anchoArticulosA = { 20, 40, 13, 13, 14 };// 'Tamaños de las celdas de la tabla articulos
                
                int[] anchoArticulosB = { 20, 40, 9, 9, 11, 11 };// 'Tamaños de las celdas de la tabla articulos

                //------------------------------------------------------------------------------------
                //Propiedades de la tabla de articulos--------------------------------------------------- 
                if (rev)
                    tablaArticulos.SetWidths(anchoArticulosB); //Ajusta el tamaño de cada columna  
                else
                    tablaArticulos.SetWidths(anchoArticulosA); //Ajusta el tamaño de cada columna       
                //tablaArticulos.AddCell(cellProducto); //Agrega COLUMNA
                tablaArticulos.AddCell(cellCodigo); //Agrega COLUMNA
                tablaArticulos.AddCell(cellDescripcion); //Agrega COLUMNA
                tablaArticulos.AddCell(cellCantidad); //Agrega COLUMNA
                //tablaArticulos.AddCell(cellUDM); //Agrega COLUMNA
                if (rev)
                    tablaArticulos.AddCell(cellRevenimiento); //Agrega COLUMNA
                //tablaArticulos.AddCell(cellPrecio); //Agrega COLUMNA
                //tablaArticulos.AddCell(cellFactor); //Agrega COLUMNA
                tablaArticulos.AddCell(cellPrecioF); //Agrega COLUMNA
                tablaArticulos.AddCell(cellSubtotalX); //Agrega COLUMNA
                //tablaArticulos.AddCell(cellIVA); //Agrega COLUMNA
                //tablaArticulos.AddCell(cellTotal); //Agrega COLUMNA

                int i = 0;
                //bool traSuma = false;
                //bool SegSuma = false;
                //double ttal = 0;
                double iva = 0, subtotal = 0, subtotal1 = 0;
                //string descripcion;
                int cont = 1, dRows = 8;
                colorB = color2;

                for (i = 0; i < dtSolicitudDetalle.Rows.Count; i++)
                {
                    if (colorB == color1)
                    {
                        colorB = color2;
                    }
                    else
                    {
                        colorB = color1;
                    }

                    //celdas de la tabla de articulos-------------------------------------------------
                    //PdfPCell cellProducto2 = new PdfPCell(new Phrase(dtSolicitudDetalle.Rows[i]["tipo"].ToString(), fontTitulo));
                    PdfPCell cellCodigo2 = new PdfPCell(new Phrase(dtSolicitudDetalle.Rows[i]["codigo"].ToString(), fontTitulo));
                    PdfPCell cellDescripcion2 = new PdfPCell(new Phrase(dtSolicitudDetalle.Rows[i]["descripcion"].ToString(), fontTitulo));
                    PdfPCell cellCantidad2 = new PdfPCell(new Phrase(dtSolicitudDetalle.Rows[i]["cantidad"].ToString() + " " + dtSolicitudDetalle.Rows[i]["unidad"].ToString().Trim(), fontTitulo));
                    //PdfPCell cellUDM2 = new PdfPCell(new Phrase(dtSolicitudDetalle.Rows[i]["unidad"].ToString(), fontTitulo));
                    PdfPCell cellRevenimiento2 = new PdfPCell(new Phrase(dtSolicitudDetalle.Rows[i]["revenimiento"].ToString(), fontTitulo));
                    //PdfPCell cellPrecio2 = new PdfPCell(new Phrase(decimal.Parse(dtSolicitudDetalle.Rows[i]["precioU"].ToString()).ToString("#,##0.00"), fontTitulo));
                    //PdfPCell cellFactor2 = new PdfPCell(new Phrase(dtSolicitudDetalle.Rows[i]["factor"].ToString(), fontTitulo));
                    PdfPCell cellPrecioF2 = new PdfPCell(new Phrase(decimal.Parse(dtSolicitudDetalle.Rows[i]["precioF"].ToString()).ToString("#,##0.00"), fontTitulo));
                    subtotal1 = double.Parse(dtSolicitudDetalle.Rows[i]["precioF"].ToString()) * double.Parse(dtSolicitudDetalle.Rows[i]["cantidad"].ToString());
                    PdfPCell cellSubtotalX2 = new PdfPCell(new Phrase(decimal.Parse(subtotal1.ToString()).ToString("#,##0.00"), fontTitulo));
                    subtotal = subtotal + subtotal1;
                    iva = double.Parse(dtSolicitudDetalle.Rows[i]["iva"].ToString());
                    //PdfPCell cellIVA2 = new PdfPCell(new Phrase(dtSolicitudDetalle.Rows[i]["iva"].ToString(), fontTitulo));
                    //PdfPCell cellTotal2 = new PdfPCell(new Phrase(decimal.Parse(dtSolicitudDetalle.Rows[i]["total"].ToString()).ToString("#,##0.00"), fontTitulo));

                    //Propiedades de las celdas de la tabla articulos
                    //cellProducto2.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                    //cellProducto2.Border = 12;
                    //cellProducto2.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                    //cellProducto2.BackgroundColor = colorB;
                    //cellProducto2.FixedHeight = 20f;

                    cellCodigo2.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                    cellCodigo2.Border = 12;
                    cellCodigo2.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                    cellCodigo2.BackgroundColor = colorB;
                    cellCodigo2.FixedHeight = 20f;

                    cellDescripcion2.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                    cellDescripcion2.Border = 12;
                    cellDescripcion2.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                    cellDescripcion2.BackgroundColor = colorB;

                    cellCantidad2.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                    cellCantidad2.Border = 12;
                    cellCantidad2.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                    cellCantidad2.BackgroundColor = colorB;

                    //cellUDM2.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                    //cellUDM2.Border = 12;
                    //cellUDM2.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                    //cellUDM2.BackgroundColor = colorB;

                    cellRevenimiento2.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                    cellRevenimiento2.Border = 12;
                    cellRevenimiento2.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                    cellRevenimiento2.BackgroundColor = colorB;

                    //cellPrecio2.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                    //cellPrecio2.Border = 12;
                    //cellPrecio2.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                    //cellPrecio2.BackgroundColor = colorB;

                    //cellFactor2.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                    //cellFactor2.Border = 12;
                    //cellFactor2.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                    //cellFactor2.BackgroundColor = colorB;

                    cellPrecioF2.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                    cellPrecioF2.Border = 12;
                    cellPrecioF2.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                    cellPrecioF2.BackgroundColor = colorB;

                    cellSubtotalX2.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                    cellSubtotalX2.Border = 12;
                    cellSubtotalX2.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                    cellSubtotalX2.BackgroundColor = colorB;

                    //cellIVA2.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                    //cellIVA2.Border = 12;
                    //cellIVA2.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                    //cellIVA2.BackgroundColor = colorB;

                    //cellTotal2.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                    //cellTotal2.Border = 12;
                    //cellTotal2.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                    //cellTotal2.BackgroundColor = colorB;

                    //Inserta los valores en la factura---------------------------------------------------                   
                    //tablaArticulos.AddCell(cellProducto2); //Agrega COLUMNA
                    tablaArticulos.AddCell(cellCodigo2); //Agrega COLUMNA
                    tablaArticulos.AddCell(cellDescripcion2); //Agrega COLUMNA
                    tablaArticulos.AddCell(cellCantidad2); //Agrega COLUMNA
                    //tablaArticulos.AddCell(cellUDM2); //Agrega COLUMNA
                    if (rev)
                        tablaArticulos.AddCell(cellRevenimiento2); //Agrega COLUMNA
                    //tablaArticulos.AddCell(cellPrecio2); //Agrega COLUMNA
                    //tablaArticulos.AddCell(cellFactor2); //Agrega COLUMNA
                    tablaArticulos.AddCell(cellPrecioF2); //Agrega COLUMNA
                    tablaArticulos.AddCell(cellSubtotalX2); //Agrega COLUMNA
                    //tablaArticulos.AddCell(cellIVA2); //Agrega COLUMNA
                    //tablaArticulos.AddCell(cellTotal2); //Agrega COLUMNA
                    cont++;

                }
                if (i< dRows)
                {
                    for (int j = i; j < dRows; j++)
                    {
                        if (colorB == color1)
                        {
                            colorB = color2;
                        }
                        else
                        {
                            colorB = color1;
                        }

                        //Propiedades de las celdas vacias de la tabla articulos
                        //cellProductoVacia2.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        //cellProductoVacia2.Border = 12;
                        //cellProductoVacia2.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                        //cellProductoVacia2.BackgroundColor = colorB;
                        //cellProductoVacia2.FixedHeight = 20f;

                        cellCodigoVacia2.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        cellCodigoVacia2.Border = 12;
                        cellCodigoVacia2.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                        cellCodigoVacia2.BackgroundColor = colorB;
                        cellCodigoVacia2.FixedHeight = 20f;

                        cellDescripcionVacia2.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        cellDescripcionVacia2.Border = 12;
                        cellDescripcionVacia2.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                        cellDescripcionVacia2.BackgroundColor = colorB;

                        cellCantidadVacia2.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        cellCantidadVacia2.Border = 12;
                        cellCantidadVacia2.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                        cellCantidadVacia2.BackgroundColor = colorB;

                        //cellUDMVacia2.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        //cellUDMVacia2.Border = 12;
                        //cellUDMVacia2.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                        //cellUDMVacia2.BackgroundColor = colorB;

                        cellRevenimientoVacia2.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        cellRevenimientoVacia2.Border = 12;
                        cellRevenimientoVacia2.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                        cellRevenimientoVacia2.BackgroundColor = colorB;

                        //cellPrecioVacia2.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        //cellPrecioVacia2.Border = 12;
                        //cellPrecioVacia2.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                        //cellPrecioVacia2.BackgroundColor = colorB;

                        //cellFactorVacia2.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        //cellFactorVacia2.Border = 12;
                        //cellFactorVacia2.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                        //cellFactorVacia2.BackgroundColor = colorB;

                        cellPrecioFVacia2.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        cellPrecioFVacia2.Border = 12;
                        cellPrecioFVacia2.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                        cellPrecioFVacia2.BackgroundColor = colorB;

                        cellSubtotalVacia2.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        cellSubtotalVacia2.Border = 12;
                        cellSubtotalVacia2.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                        cellSubtotalVacia2.BackgroundColor = colorB;

                        //cellIVAVacia2.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        //cellIVAVacia2.Border = 12;
                        //cellIVAVacia2.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                        //cellIVAVacia2.BackgroundColor = colorB;

                        //cellTotalVacia2.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        //cellTotalVacia2.Border = 12;
                        //cellTotalVacia2.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                        //cellTotalVacia2.BackgroundColor = colorB;

                        //tablaArticulos.AddCell(cellProductoVacia2); //Agrega COLUMNA
                        tablaArticulos.AddCell(cellCodigoVacia2); //Agrega COLUMNA
                        tablaArticulos.AddCell(cellDescripcionVacia2); //Agrega COLUMNA
                        tablaArticulos.AddCell(cellCantidadVacia2); //Agrega COLUMNA
                        //tablaArticulos.AddCell(cellUDMVacia2); //Agrega COLUMNA
                        if (rev)
                            tablaArticulos.AddCell(cellRevenimientoVacia2); //Agrega COLUMNA
                        //tablaArticulos.AddCell(cellPrecioVacia2); //Agrega COLUMNA
                        //tablaArticulos.AddCell(cellFactorVacia2); //Agrega COLUMNA
                        tablaArticulos.AddCell(cellPrecioFVacia2); //Agrega COLUMNA
                        tablaArticulos.AddCell(cellSubtotalVacia2); //Agrega COLUMNA
                        //tablaArticulos.AddCell(cellIVAVacia2); //Agrega COLUMNA
                        //tablaArticulos.AddCell(cellTotalVacia2); //Agrega COLUMNA
                    }
                }

                tablaArticulos.SpacingBefore = 20;
                document.Add(tablaArticulos);// Agrega la tabla al documento
                
                //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

                double total = 0;
                iva = iva * subtotal;
                total = subtotal + iva;

                PdfPTable tablaTotalLetra = new PdfPTable(1);

                tablaTotalLetra.WidthPercentage = 100; //'Ancho de la tablas

                //celdas de la tabla fecha-------------------------------------------------------        
                PdfPCell Cellimp = new PdfPCell(new Phrase("IMPORTE CON LETRA", fontDatos3)); //Celda para guardar el texto CANTIDAD
                PdfPCell cellimp2 = new PdfPCell(new Phrase(enLetras(total.ToString("#,##0.00")), fontTitulo)); //celda pra guardar la fecha de facturacion        

                Cellimp.HorizontalAlignment = Rectangle.ALIGN_CENTER; //Aliniar
                                                                      //Cellimp.Border = Rectangle.NO_BORDER;
                Cellimp.BackgroundColor = new BaseColor(227, 48, 69); //Color del borde
                cellimp2.HorizontalAlignment = Rectangle.ALIGN_LEFT; //Mostrar los bordes seleccionados
                                                                     //cellimp2.Border = Rectangle.NO_BORDER;
                                                                     //Propiedades de la tabla SELLO---------------------------------------------------        
                tablaTotalLetra.AddCell(Cellimp); //Agrega COLUMNA
                tablaTotalLetra.AddCell(cellimp2); //Agrega COLUMNA

                tablaTotalLetra.SpacingBefore = 10;
                document.Add(tablaTotalLetra); // Agrega la tabla al documento

                //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

                Regex regHtml = new Regex("<[^>]*>");
                string observaciones = sObservaciones;
                string s = regHtml.Replace(observaciones, "").Replace("&", "").Replace("acute;", "").Replace("ntilde;", "ñ");

                observaciones = s;
                //observaciones = observaciones + "\n" + TrasSeg + observacionesCot + "\n";

                PdfPCell cellImporteLetra = new PdfPCell(new Phrase("Observaciones:\n" + observaciones, fontObs)); //Celda para guardar el texto IMPORTE CON LETRA
                //PdfPCell celltotalLetra = new PdfPCell(new Phrase(" Observaciones", fontDatos)); // " " & cUtilerias.monedaTexto & " " & cUtilerias.centavos & "/100 " & cUtilerias.moneda, fontDatos2)); //Celda para guardar la cantidad en letra
                PdfPCell cellSubtotal = new PdfPCell(new Phrase("Subtotal $:", fontDatos)); //Celda para guardar el texto SUB-TOTAL
                PdfPCell cellSubtotal2 = new PdfPCell(new Phrase(subtotal.ToString("#,##0.00"), fontTitulo01));
                PdfPCell celliva = new PdfPCell(new Phrase("IVA $:", fontDatos)); //Celda para guardar el texto I.V.A
                PdfPCell celliva2 = new PdfPCell(new Phrase(iva.ToString("##,##00.00"), fontTitulo01)); //Celda para guardar el iva
                PdfPCell cellretencion = new PdfPCell(new Phrase("Total $:", fontTitulo2)); //Celda para guardar el texto RETENCION
                PdfPCell cellretencion2 = new PdfPCell(new Phrase((total).ToString("#,##0.00"), fontTitulo0)); //Celda para guardar la retencion
                PdfPCell colV = new PdfPCell(new Phrase(" ", fontTitulo)); //Celda para guardar el texto IMPORTE CON LETRA
                                                                           //  PdfPCell colV2 = new PdfPCell(new Phrase(" ", fontTitulo)); //Celda para guardar el texto IMPORTE CON LETRA

                //Propuedades de la celdas de la tabla totales----------------------------------------
                cellImporteLetra.HorizontalAlignment = Rectangle.ALIGN_LEFT; //Aliniar
                cellImporteLetra.Rowspan = 4;
                cellImporteLetra.Border = Rectangle.NO_BORDER;
                //cellImporteLetra.BackgroundColor = new BaseColor(243, 108, 63); //Color del borde
                cellSubtotal.HorizontalAlignment = Rectangle.ALIGN_RIGHT; //Aliniar
                cellSubtotal.FixedHeight = 20f;
                cellSubtotal.Border = Rectangle.NO_BORDER;
                cellSubtotal2.HorizontalAlignment = Rectangle.ALIGN_RIGHT; //Aliniar
                cellSubtotal2.Border = Rectangle.NO_BORDER;

                celliva.HorizontalAlignment = Rectangle.ALIGN_RIGHT; //Aliniar
                celliva.FixedHeight = 22f;
                celliva.Border = Rectangle.NO_BORDER;

                celliva2.HorizontalAlignment = Rectangle.ALIGN_RIGHT; //Aliminar
                celliva2.Border = Rectangle.NO_BORDER;
                celliva2.FixedHeight = 22f;

                cellretencion.HorizontalAlignment = Rectangle.ALIGN_RIGHT; //Aliniar
                cellretencion.Border = Rectangle.NO_BORDER;
                cellretencion.FixedHeight = 23f;
                cellretencion.BackgroundColor = new BaseColor(227, 48, 69); //Color del borde
                cellretencion2.HorizontalAlignment = Rectangle.ALIGN_RIGHT; //Aliniar
                cellretencion2.Border = Rectangle.NO_BORDER;
                cellretencion2.FixedHeight = 23f;
                cellretencion2.BackgroundColor = new BaseColor(227, 48, 69); //Color del borde
                colV.Border = Rectangle.NO_BORDER;

                colV.Colspan = 2;

                int[] anchoTotales = { 85, 16, 16 }; //'Tamaños de las celdas de la tabla totales

                PdfPTable tablaTotales = new PdfPTable(3);

                tablaTotales.WidthPercentage = 100; //'Ancho de la tablas
                tablaTotales.SetWidths(anchoTotales); //Ajusta el tamaño de cada columna
                tablaTotales.AddCell(cellImporteLetra); //Agrega COLUMNA
                //tablaTotales.AddCell(celltotalLetra); //Agrega COLUMNA
                tablaTotales.AddCell(cellSubtotal); //Agrega COLUMNA
                tablaTotales.AddCell(cellSubtotal2); //Agrega COLUMNA
                tablaTotales.AddCell(celliva); //Agrega COLUMNA
                tablaTotales.AddCell(celliva2); //Agrega COLUMNA
                tablaTotales.AddCell(cellretencion); //Agrega COLUMNA
                tablaTotales.AddCell(cellretencion2); //Agrega COLUMNA
                                                      //tablaTotales.AddCell(colV);
                tablaTotales.SpacingBefore = 10;

                document.Add(tablaTotales); //Agregar la tabla al documento

                //informacion del contacto
                PdfPTable tablaContacto = new PdfPTable(1);
                //tablaFoot.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;

                // PdfPCell cellPagContN = new PdfPCell(new Phrase("Gerente de Refacciones: Miguel Cárdenas", fontTitulo01)); //Celda para guardar el texto RETENCION
                PdfPCell cellPagContN1 = new PdfPCell(new Phrase("Agente de Ventas: " + dtSolicitud.Rows[0]["vendedor"].ToString(), fontTitulo01)); //Celda para guardar la retencion

                //PdfPCell cellPagContW = new PdfPCell(new Phrase("WhatsApp: 8441867079", fontTitulo01)); //Celda para guardar el texto RETENCION
                PdfPCell cellPagContW1 = new PdfPCell(new Phrase("WhatsApp: " + dtSolicitud.Rows[0]["vTelefono"].ToString(), fontTitulo01)); //Celda para guardar la retencion

                //PdfPCell cellPagContE = new PdfPCell(new Phrase("Email: info@lps.mx", fontTitulo01)); //Celda para guardar el texto RETENCION
                PdfPCell cellPagContE1 = new PdfPCell(new Phrase("Email: " + dtSolicitud.Rows[0]["vEmail"].ToString(), fontTitulo01)); //Celda para guardar la retencion

                //PdfPCell cellPagContV = new PdfPCell(new Phrase(" ", fontTitulo01)); //Celda para guardar el texto RETENCION
                PdfPCell cellPagContV1 = new PdfPCell(new Phrase(" ", fontTitulo01)); //Celda para guardar el texto RETENCION

                //cellPagContN.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cellPagContN1.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                //cellPagContN.Border = Rectangle.NO_BORDER;
                cellPagContN1.Border = Rectangle.NO_BORDER;

                //cellPagContW.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cellPagContW1.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                //cellPagContW.Border = Rectangle.NO_BORDER;
                cellPagContW1.Border = Rectangle.NO_BORDER;

                //cellPagContE.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cellPagContE1.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                //cellPagContE.Border = Rectangle.NO_BORDER;
                cellPagContE1.Border = Rectangle.NO_BORDER;

                //cellPagContV.FixedHeight = 10f;
                cellPagContV1.FixedHeight = 10f;
                //cellPagContV.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                //cellPagContV.Border = Rectangle.NO_BORDER;
                cellPagContV1.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cellPagContV1.Border = Rectangle.NO_BORDER;

                //cellPagContN.BackgroundColor = new BaseColor(243, 108, 63); //Color del borde
                //cellPagContN1.BackgroundColor = new BaseColor(34, 46, 112); //Color del borde
                //cellPagContN.FixedHeight = 23f;

                int[] anchoC = { 100 };
                tablaContacto.WidthPercentage = 120; //'Ancho de la tablas
                tablaContacto.SetWidths(anchoC); //Ajusta el tamaño de cada columna

                //tablaContacto.AddCell(cellPagContV); //Agrega COLUMNA
                tablaContacto.AddCell(cellPagContV1); //Agrega COLUMNA
                                                      //tablaContacto.AddCell(cellPagContN); //Agrega COLUMNA
                tablaContacto.AddCell(cellPagContN1); //Agrega COLUMNA
                                                      //tablaContacto.AddCell(cellPagContW); //Agrega COLUMNA
                tablaContacto.AddCell(cellPagContW1); //Agrega COLUMNA
                                                      //tablaContacto.AddCell(cellPagContE); //Agrega COLUMNA
                tablaContacto.AddCell(cellPagContE1); //Agrega COLUMNA
                tablaContacto.AddCell(cellPagContV1); //Agrega COLUMNA

                tablaContacto.SpacingBefore = 30;
                document.Add(tablaContacto);//cerramos el documento

                PdfPTable tablaF = new PdfPTable(2);
                //tablaFoot.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;

                PdfPCell cellPaglp1 = new PdfPCell(new Phrase(" ", fontTitulo2)); //Celda para guardar el texto RETENCION
                PdfPCell cellPagBS = new PdfPCell(new Phrase(" ", fontTitulo2)); //Celda para guardar la retencion

                cellPaglp1.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                cellPagBS.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cellPaglp1.Border = Rectangle.NO_BORDER;
                cellPagBS.Border = Rectangle.NO_BORDER;
                cellPaglp1.BackgroundColor = new BaseColor(34, 46, 112); //Color del borde
                cellPagBS.BackgroundColor = new BaseColor(227, 48, 69); //Color del borde
                cellPaglp1.FixedHeight = 23f;

                int[] anchoF = { 69, 31 };
                tablaF.WidthPercentage = 120; //'Ancho de la tablas
                tablaF.SetWidths(anchoF); //Ajusta el tamaño de cada columna
                tablaF.AddCell(cellPaglp1); //Agrega COLUMNA
                tablaF.AddCell(cellPagBS); //Agrega COLUMNA
                tablaF.SpacingBefore = 30;
                document.Add(tablaF);//cerramos el documento

                //verificamos que el documento no esté abierto
                if (document.IsOpen())
                {
                    //si está abierto lo cerramos
                    document.Close();
                    // document.Dispose();
                }
                return "";
            }
            catch (Exception ex)
            {
                //si hubo un error, cerramos el archivo
                if (document.IsOpen())
                {
                    document.Close();
                    //document.Dispose();

                    return ex.Message.ToString();
                }

                return ex.Message.ToString();
            }
            finally
            {
                pdf = null;
                document = null;
            }
        }

        public string enLetras(string num)
        {
            string res, dec = "";
            int entero;
            int decimales;
            double nro;

            try
            {
                nro = Convert.ToDouble(num);
            }
            catch
            {
                return "";
            }


            entero = Convert.ToInt32(Math.Truncate((nro)));
            decimales = Convert.ToInt32(Math.Round((nro - entero) * 100, 2));
            //dec = cmoneda;

            //if (decimales > 0)
            //{
            //    dec = " " + cmoneda + " " + decimales.ToString();
            //}
            //else
            //{
            //    dec = " " + cmoneda + " " + " 00";
            //}

            res = toText(Convert.ToDouble(entero)) + dec + "/100 ";

            return res;
        }
        private string toText(double value)
        {
            string Num2Text = "";

            value = Math.Truncate(value);

            if (value == 0) Num2Text = "CERO";
            else if (value == 1) Num2Text = "UNO";
            else if (value == 2) Num2Text = "DOS";
            else if (value == 3) Num2Text = "TRES";
            else if (value == 4) Num2Text = "CUATRO";
            else if (value == 5) Num2Text = "CINCO";
            else if (value == 6) Num2Text = "SEIS";
            else if (value == 7) Num2Text = "SIETE";
            else if (value == 8) Num2Text = "OCHO";
            else if (value == 9) Num2Text = "NUEVE";
            else if (value == 10) Num2Text = "DIEZ";
            else if (value == 11) Num2Text = "ONCE";
            else if (value == 12) Num2Text = "DOCE";
            else if (value == 13) Num2Text = "TRECE";
            else if (value == 14) Num2Text = "CATORCE";
            else if (value == 15) Num2Text = "QUINCE";
            else if (value < 20) Num2Text = "DIECI" + toText(value - 10);
            else if (value == 20) Num2Text = "VEINTE";
            else if (value < 30) Num2Text = "VEINTI" + toText(value - 20);
            else if (value == 30) Num2Text = "TREINTA";
            else if (value == 40) Num2Text = "CUARENTA";
            else if (value == 50) Num2Text = "CINCUENTA";
            else if (value == 60) Num2Text = "SESENTA";
            else if (value == 70) Num2Text = "SETENTA";
            else if (value == 80) Num2Text = "OCHENTA";
            else if (value == 90) Num2Text = "NOVENTA";
            else if (value < 100) Num2Text = toText(Math.Truncate(value / 10) * 10) + " Y " + toText(value % 10);
            else if (value == 100) Num2Text = "CIEN";
            else if (value < 200) Num2Text = "CIENTO" + toText(value - 100);
            else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) Num2Text = toText(Math.Truncate(value / 100)) + "CIENTOS";
            else if (value == 500) Num2Text = "QUINIENTOS";
            else if (value == 700) Num2Text = "SETECIENTOS";
            else if (value == 900) Num2Text = "NOVECIENTOS";
            else if (value < 1000) Num2Text = toText(Math.Truncate(value / 100) * 100) + " " + toText(value % 100);
            else if (value == 1000) Num2Text = "MIL";
            else if (value < 2000) Num2Text = "MIL " + toText(value % 1000);
            else if (value < 1000000)
            {
                Num2Text = toText(Math.Truncate(value / 1000)) + " MIL";
                if ((value % 1000) > 0) Num2Text = Num2Text + " " + toText(value % 1000);
            }
            else if (value == 1000000) Num2Text = "UN MILLON";
            else if (value < 2000000) Num2Text = "UN MILLON " + toText(value % 1000000);
            else if (value < 1000000000000)
            {
                Num2Text = toText(Math.Truncate(value / 1000000)) + " MILLONES ";
                if ((value - Math.Truncate(value / 1000000) * 1000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000) * 1000000);
            }
            else if (value == 1000000000000) Num2Text = "UN BILLON";
            else if (value < 2000000000000) Num2Text = "UN BILLON " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            else
            {
                Num2Text = toText(Math.Truncate(value / 1000000000000)) + " BILLONES";
                if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            }
            return Num2Text;
        }
    }
}