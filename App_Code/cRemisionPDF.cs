using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace despacho
{
    public class cRemisionPDF
    {
        //Clases base referencia
        cSolicitudes cSol = new cSolicitudes();
        cDetallesSolicitud cDS = new cDetallesSolicitud();
        cOrdenesDosificacion cOD = new cOrdenesDosificacion();
        //variables
        private string cadena;

        //propiedades
        public string path { get; set; }
        public int idOD { get; set; }
        public string folio { get; set; }
        public string nombreDoc { get; set; }
        public string extencion { get; set; }
        public string sObservaciones { get; set; }
        public string responsabilidadRem { get; set; }
        //string cmoneda;

        //Constructor
        public cRemisionPDF()
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
            Font fontDatos = new Font(FontFactory.GetFont(pathLetter + "arial.ttf", 10, BaseColor.BLACK));
            Font fontDatos1 = new Font(FontFactory.GetFont(FontFactory.TIMES_BOLD, 12, BaseColor.BLACK));
            Font fontDatos2 = new Font(FontFactory.GetFont(pathLetter + "arial.ttf", 14, BaseColor.BLACK));
            Font fontDatos3 = new Font(FontFactory.GetFont(pathLetter + "arial.ttf", 9, Font.BOLD, BaseColor.WHITE));
            Font fontDatos4 = new Font(FontFactory.GetFont(pathLetter + "arial.ttf", 8, Font.BOLD, BaseColor.WHITE));
            Font fontDatos5 = new Font(FontFactory.GetFont(pathLetter + "arial.ttf", 9, Font.BOLD, BaseColor.BLACK));
            Font fontDatos6 = new Font(FontFactory.GetFont(pathLetter + "arial.ttf", 8, Font.BOLD, BaseColor.BLACK));
            Font fontDatosCot = new Font(FontFactory.GetFont(pathLetter + "arial.ttf", 14, Font.BOLD, BaseColor.BLACK));
            Font fontColObservaciones = new Font(FontFactory.GetFont(pathLetter + "arial.ttf", 9, Font.BOLD, BaseColor.BLACK));
            Font fontObservacines = new Font(FontFactory.GetFont(pathLetter + "arial.ttf", 12, BaseColor.BLACK));
            Font fontObs = new Font(FontFactory.GetFont(pathLetter + "arial.ttf", 7, BaseColor.BLACK));
            Font fontTitulo = new Font(FontFactory.GetFont(pathLetter + "arial.ttf", 10, BaseColor.BLACK));
            Font fontTitulo1 = new Font(FontFactory.GetFont(pathLetter + "arial.ttf", 10, BaseColor.WHITE));
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
                DataTable dtRem = cOD.obtenerRemisionByIdOD(idOD);
                //DataTable dtOD = cOD.obtenerDTByID(idSolicitud);

                folio = dtRem.Rows[0]["folio"].ToString();
                responsabilidadRem = dtRem.Rows[0]["responsabilidadRem"].ToString();
                nombrePDF = nombreDoc + folio.ToString() + extencion;
                //varificar si existe la carptea si no crearla
                if (!Directory.Exists(path + "\\Remisiones"))
                {
                    Directory.CreateDirectory(path + "\\Remisiones");
                }

                //verificamos si existe el archivo
                if (File.Exists(path + "\\Remisiones\\" + nombrePDF))
                {
                    //si existe lo borramos y creamos uno nuevo
                    File.Delete(nombrePDF);
                    pdf = PdfWriter.GetInstance(document, new FileStream(path + "\\Remisiones\\" + nombrePDF, FileMode.Create, FileAccess.Write, FileShare.None));
                }
                else
                {
                    //si no existe solo lo creamos
                    pdf = PdfWriter.GetInstance(document, new FileStream(path + "\\Remisiones\\" + nombrePDF, FileMode.Create, FileAccess.Write, FileShare.None));
                }

                //---------------------------------------------------------------------------------------------------------------------------
                // Imagen del logo
                imagen = path + "\\img\\pepi_logo.png";

                Image imagenLogo = Image.GetInstance(@imagen);
                imagenLogo.ScaleAbsolute(140, 70);

                //abrimos el documento
                document.Open();

                //encabezado --------------------------------------------------------------------------------------------------------------------------



                PdfPTable tablaEncabezado = new PdfPTable(2);
                tablaEncabezado.WidthPercentage = 100;

                PdfPTable tablaCellLogoAndEmpresa = new PdfPTable(2);
                PdfPTable tablaEmpresa = new PdfPTable(1); //Tabla de la empresa
                PdfPTable tablaFolio = new PdfPTable(1); //Tabla del folio
                tablaCellLogoAndEmpresa.WidthPercentage = 100;
                tablaEmpresa.WidthPercentage = 100;  //Ancho de la tabla
                tablaFolio.WidthPercentage = 100; //Ancho de la tabla


                PdfPCell empityCell = new PdfPCell(new Phrase("              ", fontDatos1)); //Celda para guardar el texto Remisión
                empityCell.Border = Rectangle.NO_BORDER;
                //        'celdas de la tabla empresa---------------------------------------
                PdfPCell cellEmpresa = new PdfPCell(new Phrase(dtRem.Rows[0]["razon"].ToString(), fontDatosCot)); //'celda pra guardar el nombre de la empresa
                string sucDireccion = "Dirección. " + dtRem.Rows[0]["calle"].ToString() + " #" + dtRem.Rows[0]["numero"].ToString();
                if (!dtRem.Rows[0]["interior"].ToString().Equals(""))
                    sucDireccion += " int. " + dtRem.Rows[0]["interior"].ToString();
                sucDireccion += ", " + dtRem.Rows[0]["colonia"].ToString() + " C.P. " + dtRem.Rows[0]["cp"].ToString();
                PdfPCell cellDireccion = new PdfPCell(new Phrase(sucDireccion, fontDatos2)); //'celda para guardar la direccion de la empresa
                // PdfPCell cellDireccion2 = new PdfPCell(new Phrase("R.F.C." + rfcEmpresa, fontColObservaciones)); //'celda para guardar la direccion de la empresa
                //'------------------------------------------------------------------------------
                //'Propiedades de las celdas de la tabla empresa------------------------------------
                //cellCot.BorderColor = new BaseColor(200, 200, 128);
                cellEmpresa.HorizontalAlignment = Rectangle.ALIGN_CENTER; //'Aliniar
                cellEmpresa.Border = Rectangle.NO_BORDER; //'ocultar borldes
                cellDireccion.HorizontalAlignment = Rectangle.ALIGN_CENTER; //'Aliniar
                cellDireccion.Border = Rectangle.NO_BORDER; //'ocultar borldes

                tablaEmpresa.AddCell(cellEmpresa);// 'Agrega COLUMNA
                tablaEmpresa.AddCell(cellDireccion);// 'Agrega COLUMNA
                                                    // tablaEmpresa.AddCell(cellDireccion2);// 'Agrega COLUMNA

                //'-----------------------------------------------------------------------
                //'celdas de la tabla folio---------------------------------------
                PdfPCell cellCotizacion = new PdfPCell(new Phrase("REMISION:", fontDatos1)); //Celda para guardar el texto Remisión
                PdfPCell cellCotizacion1 = new PdfPCell(new Phrase(dtRem.Rows[0]["folio"].ToString(), fontObservacines)); //Celda para guardar el texto Fecha
                //      '-----------------------------------------------------------------------
                cellCotizacion.HorizontalAlignment = Rectangle.ALIGN_CENTER;// 'Mostra bordes seleccionados
                cellCotizacion1.HorizontalAlignment = Rectangle.ALIGN_CENTER;// 'Mostra bordes seleccionados
                cellCotizacion.Border = Rectangle.NO_BORDER;// 'Mostra bordes seleccionados
                cellCotizacion1.Border = Rectangle.NO_BORDER;// 'Mostra bordes seleccionados


                int[] anchoDatosTB = { 100 };// 'Tamaños de celdas de la tabla encabezado

                tablaFolio.SetWidths(anchoDatosTB); //'Ajusta el tamaño de cada columna
                tablaFolio.AddCell(cellCotizacion);// 'Agrega COLUMNA      
                tablaFolio.AddCell(empityCell);// 'Agrega COLUMNA
                tablaFolio.AddCell(cellCotizacion1);// 'Agrega COLUMNA  
                                                    //tablaFolio.AddCell(cellFolio);// 'Agrega COLUMNA    
                                                    //tablaFolio.AddCell(cellFolio1);// 'Agrega COLUMNA 
                                                    //tablaFolio.AddCell(cellFolio2);// 'Agrega COLUMNA    
                                                    //PdfPTable tablaFolio1 = new PdfPTable(1); //Tabla del folio
                                                    //tablaFolio1.WidthPercentage = 100; //Ancho de la tabla 
                                                    //'------------------------------------------------------------------------------
                                                    //'Propiedades de la tabla empresa------------------------------------


                PdfPCell cell = new PdfPCell(imagenLogo); // 'celda para guardar el logo tipo
                PdfPCell cell2 = new PdfPCell(tablaEmpresa); //'celda para guardar la empresa
                PdfPCell cell3 = new PdfPCell(tablaFolio); //'celda paa guardar la tabla folio
                                                           //PdfPCell cell4 = new PdfPCell(tablaFolio1); //'celda paa guardar la tabla folio

                cell.Border = Rectangle.NO_BORDER; //'ocultar borldes
                cell2.Border = Rectangle.NO_BORDER; //'ocultar borldes
                                                    //cell3.Border = Rectangle.NO_BORDER; //'ocultar borldes
                                                    //cell4.Border = Rectangle.NO_BORDER; //'ocultar borldes

                int[] anchoDatosLogoAndEmpresa = { 35, 65 };// 'Tamaños de celdas de la tabla encabezado
                tablaCellLogoAndEmpresa.SetWidths(anchoDatosLogoAndEmpresa);
                tablaCellLogoAndEmpresa.AddCell(cell);
                tablaCellLogoAndEmpresa.AddCell(cell2);

                int[] anchoDatos = { 80, 20 };// 'Tamaños de celdas de la tabla encabezado

                tablaEncabezado.SetWidths(anchoDatos); //'Ajusta el tamaño de cada columna
                tablaEncabezado.AddCell(tablaCellLogoAndEmpresa); //'Agrega COLUMNA
                //tablaEncabezado.AddCell(cell2); //'Agrega COLUMNA
                tablaEncabezado.AddCell(cell3); //'Agrega COLUMNA
                                                //tablaEncabezado.AddCell(cell4); //'Agrega COLUMNA

                //'Propiedades del documento
                document.Add(tablaEncabezado);// ' Agrega la tabla al documento

                //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

                string pDireccion = dtRem.Rows[0]["pCalle"].ToString() + " #" + dtRem.Rows[0]["pNumero"].ToString();
                if (!dtRem.Rows[0]["pInterior"].ToString().Equals(""))
                    pDireccion += " int. " + dtRem.Rows[0]["pInterior"].ToString();
                pDireccion += ", " + dtRem.Rows[0]["pColonia"].ToString() + " C.P. " + dtRem.Rows[0]["pCP"].ToString();

                //'----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

                PdfPTable tablaGral = new PdfPTable(2);
                tablaGral.WidthPercentage = 100;// 'Ancho de la tabla
                tablaGral.DefaultCell.FixedHeight = 60f;
                PdfPTable tablaCustomer = new PdfPTable(1);
                tablaCustomer.WidthPercentage = 100;// 'Ancho de la tabla
                tablaCustomer.DefaultCell.FixedHeight = 60f;
                PdfPTable tablaProject = new PdfPTable(1);
                tablaProject.WidthPercentage = 100;// 'Ancho de la tabla
                tablaProject.DefaultCell.FixedHeight = 60f;

                //celdas de la tabla de articulos-------------------------------------------------
                PdfPCell cellCustomer = new PdfPCell(new Phrase("C O B R A R  A:", fontTitulo1)); //Celda para guardar el texto Cliente
                PdfPCell cellProject = new PdfPCell(new Phrase("P R O Y E C T O: ", fontTitulo1)); //Celda para guardar el texto Proyecto

                //------------------------------------------------------------------------------------
                //Propiedades de las celdas de la tabla de articulos------------------------------------
                cellCustomer.BackgroundColor = new BaseColor(34, 46, 112); //Color del borde
                cellCustomer.Border = 12; //Mostrar bordes seleccionados
                cellCustomer.HorizontalAlignment = Rectangle.ALIGN_LEFT; //Aliniar
                cellCustomer.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                cellCustomer.FixedHeight = 20f;

                cellProject.BackgroundColor = new BaseColor(34, 46, 112); //Color del borde
                cellProject.Border = 12; //Mostrar bordes seleccionados
                cellCustomer.HorizontalAlignment = Rectangle.ALIGN_LEFT; //Aliniar
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

                tablaProject.SetWidths(anchoProject); //Ajusta el tamaño de cada columna        


                //celdas de la tabla de Customers Columna1-------------------------------------------------
                PdfPCell cellCustomerF1 = new PdfPCell(new Phrase("Cliente: \n" + dtRem.Rows[0]["nombre"].ToString() + "\n" +
                    "Teléfono: " + dtRem.Rows[0]["telefono"].ToString() + "/" + dtRem.Rows[0]["celular"].ToString() + "\n" +
                    "Correo: " + dtRem.Rows[0]["email"].ToString(), fontDatos));
                //celdas de la tabla de Customers Columna1-------------------------------------------------
                PdfPCell cellProjectF1 = new PdfPCell(new Phrase("Dirección de entrega: \n" + pDireccion, fontDatos));

                //Propiedades de las celdas de la tabla articulos
                cellCustomerF1.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cellCustomerF1.Border = Rectangle.NO_BORDER;
                cellCustomerF1.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                cellCustomerF1.BackgroundColor = colorB;
                cellCustomerF1.FixedHeight = 20f;

                cellProjectF1.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cellProjectF1.Border = Rectangle.NO_BORDER;
                cellProjectF1.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                cellProjectF1.BackgroundColor = colorB;

                //Inserta los valores en la factura---------------------------------------------------                   
                tablaCustomer.AddCell(cellCustomerF1); //Agrega COLUMNA

                tablaProject.AddCell(cellProjectF1); //Agrega COLUMNA

                tablaGral.AddCell(tablaCustomer);
                tablaGral.AddCell(tablaProject);

                tablaGral.SpacingBefore = 5;
                document.Add(tablaGral);// Agrega la tabla al documento

                //'----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

                PdfPTable tablaGral2 = new PdfPTable(6);
                tablaGral2.WidthPercentage = 100;// 'Ancho de la tabla
                tablaGral2.DefaultCell.FixedHeight = 80f;

                //celdas de la tabla de articulos-------------------------------------------------
                PdfPCell cellG21 = new PdfPCell(new Phrase("INI. CARGA", fontTitulo1)); //Celda para guardar el texto Carga Inicial
                PdfPCell cellG22 = new PdfPCell(new Phrase("FÓRMULA", fontTitulo1)); //Celda para guardar el texto Formula
                PdfPCell cellG23 = new PdfPCell(new Phrase("CARGA M3", fontTitulo1)); //Celda para guardar el texto Carga Inicial
                PdfPCell cellG24 = new PdfPCell(new Phrase("M3 ORDENADOS", fontTitulo1)); //Celda para guardar el texto Formula
                PdfPCell cellG25 = new PdfPCell(new Phrase("No. CAMIÓN", fontTitulo1)); //Celda para guardar el texto Carga Inicial
                PdfPCell cellG26 = new PdfPCell(new Phrase("NOMBRE DEL OPERADOR", fontTitulo1)); //Celda para guardar el texto Formula

                //------------------------------------------------------------------------------------
                //Propiedades de las celdas de la tabla de articulos------------------------------------
                cellG21.BackgroundColor = new BaseColor(34, 46, 112); //Color del borde
                cellG21.Border = 12; //Mostrar bordes seleccionados
                cellG21.HorizontalAlignment = Rectangle.ALIGN_CENTER; //Aliniar
                cellG21.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                cellG21.FixedHeight = 20f;
                //Propiedades de las celdas de la tabla de articulos------------------------------------
                cellG22.BackgroundColor = new BaseColor(34, 46, 112); //Color del borde
                cellG22.Border = 12; //Mostrar bordes seleccionados
                cellG22.HorizontalAlignment = Rectangle.ALIGN_CENTER; //Aliniar
                cellG22.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                //Propiedades de las celdas de la tabla de articulos------------------------------------
                cellG23.BackgroundColor = new BaseColor(34, 46, 112); //Color del borde
                cellG23.Border = 12; //Mostrar bordes seleccionados
                cellG23.HorizontalAlignment = Rectangle.ALIGN_CENTER; //Aliniar
                cellG23.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                //Propiedades de las celdas de la tabla de articulos------------------------------------
                cellG24.BackgroundColor = new BaseColor(34, 46, 112); //Color del borde
                cellG24.Border = 12; //Mostrar bordes seleccionados
                cellG24.HorizontalAlignment = Rectangle.ALIGN_CENTER; //Aliniar
                cellG24.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                //Propiedades de las celdas de la tabla de articulos------------------------------------
                cellG25.BackgroundColor = new BaseColor(34, 46, 112); //Color del borde
                cellG25.Border = 12; //Mostrar bordes seleccionados
                cellG25.HorizontalAlignment = Rectangle.ALIGN_CENTER; //Aliniar
                cellG25.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                //Propiedades de las celdas de la tabla de articulos------------------------------------
                cellG26.BackgroundColor = new BaseColor(34, 46, 112); //Color del borde
                cellG26.Border = 12; //Mostrar bordes seleccionados
                cellG26.HorizontalAlignment = Rectangle.ALIGN_CENTER; //Aliniar
                cellG26.BorderColor = new BaseColor(0, 0, 0); //Color del borde

                colorB = color1;

                int[] anchoGral2 = { 10, 35, 10, 15, 10, 20 };// 'Tamaños de las celdas de la tabla Gral

                //------------------------------------------------------------------------------------
                //Propiedades de la tabla de articulos---------------------------------------------------
                tablaGral2.SetWidths(anchoGral2); //Ajusta el tamaño de cada columna        
                tablaGral2.AddCell(cellG21); //Agrega COLUMNA
                tablaGral2.AddCell(cellG22); //Agrega COLUMNA
                tablaGral2.AddCell(cellG23); //Agrega COLUMNA
                tablaGral2.AddCell(cellG24); //Agrega COLUMNA
                tablaGral2.AddCell(cellG25); //Agrega COLUMNA
                tablaGral2.AddCell(cellG26); //Agrega COLUMNA

                //celdas de la tabla de Customers Columna1-------------------------------------------------
                PdfPCell cellG2DATA1 = new PdfPCell(new Phrase(" - ", fontDatos));
                PdfPCell cellG2DATA2 = new PdfPCell(new Phrase(dtRem.Rows[0]["codigo"].ToString(), fontDatos));
                PdfPCell cellG2DATA3 = new PdfPCell(new Phrase(dtRem.Rows[0]["capacidad"].ToString(), fontDatos));
                PdfPCell cellG2DATA4 = new PdfPCell(new Phrase(dtRem.Rows[0]["cantOrdenada"].ToString(), fontDatos));
                PdfPCell cellG2DATA5 = new PdfPCell(new Phrase(dtRem.Rows[0]["nombreUnidadT"].ToString(), fontDatos));
                PdfPCell cellG2DATA6 = new PdfPCell(new Phrase(dtRem.Rows[0]["chofer"].ToString(), fontDatos));

                //Propiedades de las celdas de la tabla articulos
                cellG2DATA1.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cellG2DATA1.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                cellG2DATA1.BackgroundColor = colorB;
                cellG2DATA1.FixedHeight = 20f;
                //Propiedades de las celdas de la tabla articulos
                cellG2DATA2.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cellG2DATA2.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                cellG2DATA2.BackgroundColor = colorB;
                //Propiedades de las celdas de la tabla articulos
                cellG2DATA3.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cellG2DATA3.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                cellG2DATA3.BackgroundColor = colorB;
                //Propiedades de las celdas de la tabla articulos
                cellG2DATA4.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cellG2DATA4.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                cellG2DATA4.BackgroundColor = colorB;
                //Propiedades de las celdas de la tabla articulos
                cellG2DATA5.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cellG2DATA5.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                cellG2DATA5.BackgroundColor = colorB;
                //Propiedades de las celdas de la tabla articulos
                cellG2DATA6.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cellG2DATA6.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                cellG2DATA6.BackgroundColor = colorB;

                tablaGral2.AddCell(cellG2DATA1);
                tablaGral2.AddCell(cellG2DATA2);
                tablaGral2.AddCell(cellG2DATA3);
                tablaGral2.AddCell(cellG2DATA4);
                tablaGral2.AddCell(cellG2DATA5);
                tablaGral2.AddCell(cellG2DATA6);

                document.Add(tablaGral2);// Agrega la tabla al documento

                //'----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

                PdfPTable tablaGral3 = new PdfPTable(7);
                tablaGral3.WidthPercentage = 100;// 'Ancho de la tabla
                tablaGral3.DefaultCell.FixedHeight = 80f;

                //celdas de la tabla de articulos-------------------------------------------------
                PdfPCell cellG31 = new PdfPCell(new Phrase("FECHA", fontTitulo1)); //Celda para guardar el texto Carga Inicial
                PdfPCell cellG32 = new PdfPCell(new Phrase(" ", fontTitulo1)); //Celda para guardar el texto Formula
                PdfPCell cellG33 = new PdfPCell(new Phrase(" ", fontTitulo1)); //Celda para guardar el texto Carga Inicial
                PdfPCell cellG34 = new PdfPCell(new Phrase("M3 ENVIADOS", fontTitulo1)); //Celda para guardar el texto Formula
                PdfPCell cellG35 = new PdfPCell(new Phrase(" ", fontTitulo1)); //Celda para guardar el texto Carga Inicial
                PdfPCell cellG36 = new PdfPCell(new Phrase("REVENIMIENTO", fontTitulo1)); //Celda para guardar el texto Formula
                PdfPCell cellG37 = new PdfPCell(new Phrase("TICKET No.", fontTitulo1)); //Celda para guardar el texto Formula

                //------------------------------------------------------------------------------------
                //Propiedades de las celdas de la tabla de articulos------------------------------------
                cellG31.BackgroundColor = new BaseColor(34, 46, 112); //Color del borde
                cellG31.Border = 12; //Mostrar bordes seleccionados
                cellG31.HorizontalAlignment = Rectangle.ALIGN_CENTER; //Aliniar
                cellG31.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                cellG31.FixedHeight = 20f;
                //Propiedades de las celdas de la tabla de articulos------------------------------------
                cellG32.BackgroundColor = new BaseColor(34, 46, 112); //Color del borde
                cellG32.Border = 12; //Mostrar bordes seleccionados
                cellG32.HorizontalAlignment = Rectangle.ALIGN_CENTER; //Aliniar
                cellG32.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                //Propiedades de las celdas de la tabla de articulos------------------------------------
                cellG33.BackgroundColor = new BaseColor(34, 46, 112); //Color del borde
                cellG33.Border = 12; //Mostrar bordes seleccionados
                cellG33.HorizontalAlignment = Rectangle.ALIGN_CENTER; //Aliniar
                cellG33.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                //Propiedades de las celdas de la tabla de articulos------------------------------------
                cellG34.BackgroundColor = new BaseColor(34, 46, 112); //Color del borde
                cellG34.Border = 12; //Mostrar bordes seleccionados
                cellG34.HorizontalAlignment = Rectangle.ALIGN_CENTER; //Aliniar
                cellG34.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                //Propiedades de las celdas de la tabla de articulos------------------------------------
                cellG35.BackgroundColor = new BaseColor(34, 46, 112); //Color del borde
                cellG35.Border = 12; //Mostrar bordes seleccionados
                cellG35.HorizontalAlignment = Rectangle.ALIGN_CENTER; //Aliniar
                cellG35.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                //Propiedades de las celdas de la tabla de articulos------------------------------------
                cellG36.BackgroundColor = new BaseColor(34, 46, 112); //Color del borde
                cellG36.Border = 12; //Mostrar bordes seleccionados
                cellG36.HorizontalAlignment = Rectangle.ALIGN_CENTER; //Aliniar
                cellG36.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                //Propiedades de las celdas de la tabla de articulos------------------------------------
                cellG37.BackgroundColor = new BaseColor(34, 46, 112); //Color del borde
                cellG37.Border = 12; //Mostrar bordes seleccionados
                cellG37.HorizontalAlignment = Rectangle.ALIGN_CENTER; //Aliniar
                cellG37.BorderColor = new BaseColor(0, 0, 0); //Color del borde

                colorB = color1;

                int[] anchoGral3 = { 35, 10, 10, 15, 10, 10, 10 };// 'Tamaños de las celdas de la tabla Gral

                //------------------------------------------------------------------------------------
                //Propiedades de la tabla de articulos---------------------------------------------------
                tablaGral3.SetWidths(anchoGral3); //Ajusta el tamaño de cada columna        
                tablaGral3.AddCell(cellG31); //Agrega COLUMNA
                tablaGral3.AddCell(cellG32); //Agrega COLUMNA
                tablaGral3.AddCell(cellG33); //Agrega COLUMNA
                tablaGral3.AddCell(cellG34); //Agrega COLUMNA
                tablaGral3.AddCell(cellG35); //Agrega COLUMNA
                tablaGral3.AddCell(cellG36); //Agrega COLUMNA
                tablaGral3.AddCell(cellG37); //Agrega COLUMNA

                DateTime fechaOD = DateTime.Parse(dtRem.Rows[0]["fecha"].ToString().Substring(0, 11) + dtRem.Rows[0]["hora"].ToString() + ":00");

                //celdas de la tabla de Customers Columna1-------------------------------------------------
                PdfPCell cellG3DATA1 = new PdfPCell(new Phrase(fechaOD.ToString(), fontDatos));
                PdfPCell cellG3DATA2 = new PdfPCell(new Phrase(" - ", fontDatos));
                PdfPCell cellG3DATA3 = new PdfPCell(new Phrase(" - ", fontDatos));
                PdfPCell cellG3DATA4 = new PdfPCell(new Phrase(dtRem.Rows[0]["cantidad"].ToString(), fontDatos));
                PdfPCell cellG3DATA5 = new PdfPCell(new Phrase(" - ", fontDatos));
                PdfPCell cellG3DATA6 = new PdfPCell(new Phrase(dtRem.Rows[0]["revenimiento"].ToString(), fontDatos));
                PdfPCell cellG3DATA7 = new PdfPCell(new Phrase(" - ", fontDatos));

                //Propiedades de las celdas de la tabla articulos
                cellG3DATA1.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cellG3DATA1.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                cellG3DATA1.BackgroundColor = colorB;
                cellG3DATA1.FixedHeight = 20f;
                //Propiedades de las celdas de la tabla articulos
                cellG3DATA2.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cellG3DATA2.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                cellG3DATA2.BackgroundColor = colorB;
                //Propiedades de las celdas de la tabla articulos
                cellG3DATA3.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cellG3DATA3.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                cellG3DATA3.BackgroundColor = colorB;
                //Propiedades de las celdas de la tabla articulos
                cellG3DATA4.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cellG3DATA4.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                cellG3DATA4.BackgroundColor = colorB;
                //Propiedades de las celdas de la tabla articulos
                cellG3DATA5.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cellG3DATA5.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                cellG3DATA5.BackgroundColor = colorB;
                //Propiedades de las celdas de la tabla articulos
                cellG3DATA6.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cellG3DATA6.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                cellG3DATA6.BackgroundColor = colorB;
                //Propiedades de las celdas de la tabla articulos
                cellG3DATA7.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cellG3DATA7.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                cellG3DATA7.BackgroundColor = colorB;

                tablaGral3.AddCell(cellG3DATA1);
                tablaGral3.AddCell(cellG3DATA2);
                tablaGral3.AddCell(cellG3DATA3);
                tablaGral3.AddCell(cellG3DATA4);
                tablaGral3.AddCell(cellG3DATA5);
                tablaGral3.AddCell(cellG3DATA6);
                tablaGral3.AddCell(cellG3DATA7);

                document.Add(tablaGral3);// Agrega la tabla al documento

                //'----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

                PdfPTable tablaGral4 = new PdfPTable(2);
                tablaGral4.WidthPercentage = 100;// 'Ancho de la tabla
                tablaGral4.DefaultCell.FixedHeight = 80f;
                PdfPTable tablaDisclaiment = new PdfPTable(1);
                tablaDisclaiment.WidthPercentage = 100;// 'Ancho de la tabla
                tablaDisclaiment.DefaultCell.FixedHeight = 80f;
                PdfPTable tablaRecibido = new PdfPTable(1);
                tablaRecibido.WidthPercentage = 100;// 'Ancho de la tabla
                tablaRecibido.DefaultCell.FixedHeight = 80f;

                colorB = color1;

                int[] anchoGral4 = { 70, 30 };// 'Tamaños de las celdas de la tabla Gral
                int[] anchoDisclaiment = { 100 };// 'Tamaños de las celdas de la tabla Customer
                int[] anchoRecibido = { 100 };// 'Tamaños de las celdas de la tabla Project

                //------------------------------------------------------------------------------------
                //Propiedades de la tabla de articulos---------------------------------------------------
                tablaGral4.SetWidths(anchoGral4); //Ajusta el tamaño de cada columna        
                tablaDisclaiment.SetWidths(anchoDisclaiment); //Ajusta el tamaño de cada columna    
                tablaRecibido.SetWidths(anchoRecibido); //Ajusta el tamaño de cada columna        


                //celdas de la tabla de Customers Columna1-------------------------------------------------
                PdfPCell cellDisclaiment1 = new PdfPCell(new Phrase(responsabilidadRem, fontDatos6));
                //celdas de la tabla de Customers Columna1-------------------------------------------------
                PdfPCell cellRecibido1 = new PdfPCell(new Phrase("RECIBÍ DE CONFORMIDAD", fontDatos5));
                PdfPCell cellRecibido2 = new PdfPCell(new Phrase("NOMBRE: ___________________", fontDatos6));
                PdfPCell cellRecibido3 = new PdfPCell(new Phrase(" FIRMA: ___________________", fontDatos6));

                //Propiedades de las celdas de la tabla articulos
                cellDisclaiment1.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cellDisclaiment1.Border = Rectangle.NO_BORDER;
                cellDisclaiment1.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                cellDisclaiment1.BackgroundColor = colorB;
                cellDisclaiment1.FixedHeight = 80f;

                //Propiedades de las celdas de la tabla articulos
                cellRecibido1.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cellRecibido1.Border = Rectangle.NO_BORDER;
                cellRecibido1.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                cellRecibido1.BackgroundColor = colorB;
                //Propiedades de las celdas de la tabla articulos
                cellRecibido2.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cellRecibido2.Border = Rectangle.NO_BORDER;
                cellRecibido2.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                cellRecibido2.BackgroundColor = colorB;
                //Propiedades de las celdas de la tabla articulos
                cellRecibido3.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cellRecibido3.Border = Rectangle.NO_BORDER;
                cellRecibido3.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                cellRecibido3.BackgroundColor = colorB;

                //Inserta los valores en la factura---------------------------------------------------                   
                tablaDisclaiment.AddCell(cellDisclaiment1); //Agrega COLUMNA

                tablaRecibido.AddCell(empityCell); //Agrega COLUMNA
                tablaRecibido.AddCell(cellRecibido1); //Agrega COLUMNA
                tablaRecibido.AddCell(cellRecibido2); //Agrega COLUMNA
                tablaRecibido.AddCell(cellRecibido3); //Agrega COLUMNA

                tablaGral4.AddCell(tablaDisclaiment);
                tablaGral4.AddCell(tablaRecibido);

                document.Add(tablaGral4);// Agrega la tabla al documento

                //'----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

                PdfPTable tablaGral5 = new PdfPTable(4);
                tablaGral5.WidthPercentage = 100;// 'Ancho de la tabla
                tablaGral5.DefaultCell.FixedHeight = 80f;

                //celdas de la tabla de articulos-------------------------------------------------
                PdfPCell cellG51 = new PdfPCell(new Phrase("CANTIDAD", fontTitulo1)); //Celda para guardar el texto Carga Inicial
                PdfPCell cellG52 = new PdfPCell(new Phrase("CÓDIGO", fontTitulo1)); //Celda para guardar el texto Formula
                PdfPCell cellG53 = new PdfPCell(new Phrase("DESCRIPCIÓN", fontTitulo1)); //Celda para guardar el texto Carga Inicial
                PdfPCell cellG54 = new PdfPCell(new Phrase("CARGA", fontTitulo1)); //Celda para guardar el texto Formula

                //------------------------------------------------------------------------------------
                //Propiedades de las celdas de la tabla de articulos------------------------------------
                cellG51.BackgroundColor = new BaseColor(34, 46, 112); //Color del borde
                cellG51.Border = 12; //Mostrar bordes seleccionados
                cellG51.HorizontalAlignment = Rectangle.ALIGN_CENTER; //Aliniar
                cellG51.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                cellG51.FixedHeight = 20f;
                //Propiedades de las celdas de la tabla de articulos------------------------------------
                cellG52.BackgroundColor = new BaseColor(34, 46, 112); //Color del borde
                cellG52.Border = 12; //Mostrar bordes seleccionados
                cellG52.HorizontalAlignment = Rectangle.ALIGN_CENTER; //Aliniar
                cellG52.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                //Propiedades de las celdas de la tabla de articulos------------------------------------
                cellG53.BackgroundColor = new BaseColor(34, 46, 112); //Color del borde
                cellG53.Border = 12; //Mostrar bordes seleccionados
                cellG53.HorizontalAlignment = Rectangle.ALIGN_CENTER; //Aliniar
                cellG53.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                //Propiedades de las celdas de la tabla de articulos------------------------------------
                cellG54.BackgroundColor = new BaseColor(34, 46, 112); //Color del borde
                cellG54.Border = 12; //Mostrar bordes seleccionados
                cellG54.HorizontalAlignment = Rectangle.ALIGN_CENTER; //Aliniar
                cellG54.BorderColor = new BaseColor(0, 0, 0); //Color del borde

                colorB = color1;

                int[] anchoGral5 = { 20, 20, 50, 10};// 'Tamaños de las celdas de la tabla Gral

                //------------------------------------------------------------------------------------
                //Propiedades de la tabla de articulos---------------------------------------------------
                tablaGral5.SetWidths(anchoGral5); //Ajusta el tamaño de cada columna        
                tablaGral5.AddCell(cellG51); //Agrega COLUMNA
                tablaGral5.AddCell(cellG52); //Agrega COLUMNA
                tablaGral5.AddCell(cellG53); //Agrega COLUMNA
                tablaGral5.AddCell(cellG54); //Agrega COLUMNA

                //celdas de la tabla de Customers Columna1-------------------------------------------------
                PdfPCell cellG5DATA1 = new PdfPCell(new Phrase(" - ", fontDatos));
                PdfPCell cellG5DATA2 = new PdfPCell(new Phrase(" - ", fontDatos));
                PdfPCell cellG5DATA3 = new PdfPCell(new Phrase(" - ", fontDatos));
                PdfPCell cellG5DATA4 = new PdfPCell(new Phrase(" - ", fontDatos));

                //Propiedades de las celdas de la tabla articulos
                cellG5DATA1.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cellG5DATA1.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                cellG5DATA1.BackgroundColor = colorB;
                cellG5DATA1.FixedHeight = 20f;
                //Propiedades de las celdas de la tabla articulos
                cellG5DATA2.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cellG5DATA2.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                cellG5DATA2.BackgroundColor = colorB;
                //Propiedades de las celdas de la tabla articulos
                cellG5DATA3.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cellG5DATA3.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                cellG5DATA3.BackgroundColor = colorB;
                //Propiedades de las celdas de la tabla articulos
                cellG5DATA4.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cellG5DATA4.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                cellG5DATA4.BackgroundColor = colorB;

                tablaGral5.AddCell(cellG5DATA1);
                tablaGral5.AddCell(cellG5DATA2);
                tablaGral5.AddCell(cellG5DATA3);
                tablaGral5.AddCell(cellG5DATA4);

                document.Add(tablaGral5);// Agrega la tabla al documento

                //'----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

                PdfPTable tablaGral6 = new PdfPTable(2);
                tablaGral6.WidthPercentage = 100;// 'Ancho de la tabla
                tablaGral6.DefaultCell.FixedHeight = 140f;
                PdfPTable tablaEstadisticas = new PdfPTable(3);
                tablaEstadisticas.WidthPercentage = 100;// 'Ancho de la tabla
                PdfPTable tablaObservaciones = new PdfPTable(1);
                tablaObservaciones.WidthPercentage = 100;// 'Ancho de la tabla

                //celdas de la tabla de articulos-------------------------------------------------
                PdfPCell cellEstadisticas11 = new PdfPCell(new Phrase("REGRESO PLANTA:", fontTitulo1)); //Celda para guardar el texto
                PdfPCell cellEstadisticas12 = new PdfPCell(new Phrase("SALIÓ OBRA:", fontTitulo1)); //Celda para guardar el texto
                PdfPCell cellEstadisticas13 = new PdfPCell(new Phrase("TERMINÓ DESCARGA", fontTitulo1)); //Celda para guardar el texto
                PdfPCell cellEstadisticas21 = new PdfPCell(new Phrase("SALIÓ PLANTA:", fontTitulo1)); //Celda para guardar el texto
                PdfPCell cellEstadisticas22 = new PdfPCell(new Phrase("LLEGO TRABAJO:", fontTitulo1)); //Celda para guardar el texto
                PdfPCell cellEstadisticas23 = new PdfPCell(new Phrase("EMPEZÓ DESC:", fontTitulo1)); //Celda para guardar el texto
                PdfPCell cellEstadisticas31 = new PdfPCell(new Phrase("TOTAL VIAJE:", fontTitulo1)); //Celda para guardar el texto
                PdfPCell cellEstadisticas32 = new PdfPCell(new Phrase("TIEMPO OBRA:", fontTitulo1)); //Celda para guardar el texto
                PdfPCell cellEstadisticas33 = new PdfPCell(new Phrase("TIEMPO DESC:", fontTitulo1)); //Celda para guardar el texto
                PdfPCell cellObservaciones = new PdfPCell(new Phrase("OBSERVACIONES:", fontTitulo1)); //Celda para guardar el texto

                //------------------------------------------------------------------------------------
                //Propiedades de las celdas de la tabla de articulos------------------------------------
                cellEstadisticas11.BackgroundColor = new BaseColor(34, 46, 112); //Color del borde
                cellEstadisticas11.Border = 12; //Mostrar bordes seleccionados
                cellEstadisticas11.HorizontalAlignment = Rectangle.ALIGN_CENTER; //Aliniar
                cellEstadisticas11.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                cellEstadisticas11.FixedHeight = 20f;
                //Propiedades de las celdas de la tabla de articulos------------------------------------
                cellEstadisticas12.BackgroundColor = new BaseColor(34, 46, 112); //Color del borde
                cellEstadisticas12.Border = 12; //Mostrar bordes seleccionados
                cellEstadisticas12.HorizontalAlignment = Rectangle.ALIGN_CENTER; //Aliniar
                cellEstadisticas12.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                //Propiedades de las celdas de la tabla de articulos------------------------------------
                cellEstadisticas13.BackgroundColor = new BaseColor(34, 46, 112); //Color del borde
                cellEstadisticas13.Border = 12; //Mostrar bordes seleccionados
                cellEstadisticas13.HorizontalAlignment = Rectangle.ALIGN_CENTER; //Aliniar
                cellEstadisticas13.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                //Propiedades de las celdas de la tabla de articulos------------------------------------
                cellEstadisticas21.BackgroundColor = new BaseColor(34, 46, 112); //Color del borde
                cellEstadisticas21.Border = 12; //Mostrar bordes seleccionados
                cellEstadisticas21.HorizontalAlignment = Rectangle.ALIGN_CENTER; //Aliniar
                cellEstadisticas21.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                cellEstadisticas21.FixedHeight = 20f;
                //Propiedades de las celdas de la tabla de articulos------------------------------------
                cellEstadisticas22.BackgroundColor = new BaseColor(34, 46, 112); //Color del borde
                cellEstadisticas22.Border = 12; //Mostrar bordes seleccionados
                cellEstadisticas22.HorizontalAlignment = Rectangle.ALIGN_CENTER; //Aliniar
                cellEstadisticas22.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                //Propiedades de las celdas de la tabla de articulos------------------------------------
                cellEstadisticas23.BackgroundColor = new BaseColor(34, 46, 112); //Color del borde
                cellEstadisticas23.Border = 12; //Mostrar bordes seleccionados
                cellEstadisticas23.HorizontalAlignment = Rectangle.ALIGN_CENTER; //Aliniar
                cellEstadisticas23.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                //Propiedades de las celdas de la tabla de articulos------------------------------------
                cellEstadisticas31.BackgroundColor = new BaseColor(34, 46, 112); //Color del borde
                cellEstadisticas31.Border = 12; //Mostrar bordes seleccionados
                cellEstadisticas31.HorizontalAlignment = Rectangle.ALIGN_CENTER; //Aliniar
                cellEstadisticas31.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                cellEstadisticas31.FixedHeight = 20f;
                //Propiedades de las celdas de la tabla de articulos------------------------------------
                cellEstadisticas32.BackgroundColor = new BaseColor(34, 46, 112); //Color del borde
                cellEstadisticas32.Border = 12; //Mostrar bordes seleccionados
                cellEstadisticas32.HorizontalAlignment = Rectangle.ALIGN_CENTER; //Aliniar
                cellEstadisticas32.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                //Propiedades de las celdas de la tabla de articulos------------------------------------
                cellEstadisticas33.BackgroundColor = new BaseColor(34, 46, 112); //Color del borde
                cellEstadisticas33.Border = 12; //Mostrar bordes seleccionados
                cellEstadisticas33.HorizontalAlignment = Rectangle.ALIGN_CENTER; //Aliniar
                cellEstadisticas33.BorderColor = new BaseColor(0, 0, 0); //Color del borde

                cellObservaciones.BackgroundColor = new BaseColor(34, 46, 112); //Color del borde
                cellObservaciones.Border = 12; //Mostrar bordes seleccionados
                cellObservaciones.HorizontalAlignment = Rectangle.ALIGN_LEFT; //Aliniar
                cellObservaciones.BorderColor = new BaseColor(0, 0, 0); //Color del borde

                colorB = color1;

                int[] anchoGral6 = { 45, 55 };// 'Tamaños de las celdas de la tabla Gral
                int[] anchoEstadisticas = { 33, 33, 34 };// 'Tamaños de las celdas de la tabla Customer
                int[] anchoObservaciones = { 100 };// 'Tamaños de las celdas de la tabla Project

                //------------------------------------------------------------------------------------
                //Propiedades de la tabla de articulos---------------------------------------------------
                tablaGral6.SetWidths(anchoGral6); //Ajusta el tamaño de cada columna        
                tablaEstadisticas.SetWidths(anchoEstadisticas); //Ajusta el tamaño de cada columna    
                tablaObservaciones.SetWidths(anchoObservaciones); //Ajusta el tamaño de cada columna        


                //celdas de la tabla de Customers Columna1-------------------------------------------------
                PdfPCell cellEstadisticasD11 = new PdfPCell(new Phrase(" - ", fontDatos));
                PdfPCell cellEstadisticasD12 = new PdfPCell(new Phrase(" - ", fontDatos));
                PdfPCell cellEstadisticasD13 = new PdfPCell(new Phrase(" - ", fontDatos));
                PdfPCell cellEstadisticasD21 = new PdfPCell(new Phrase(" - ", fontDatos));
                PdfPCell cellEstadisticasD22 = new PdfPCell(new Phrase(" - ", fontDatos));
                PdfPCell cellEstadisticasD23 = new PdfPCell(new Phrase(" - ", fontDatos));
                PdfPCell cellEstadisticasD31 = new PdfPCell(new Phrase(" - ", fontDatos));
                PdfPCell cellEstadisticasD32 = new PdfPCell(new Phrase(" - ", fontDatos));
                PdfPCell cellEstadisticasD33 = new PdfPCell(new Phrase(" - ", fontDatos));
                //celdas de la tabla de Customers Columna1-------------------------------------------------
                PdfPCell cellObservacionesD = new PdfPCell(new Phrase(" ninguna ", fontDatos));

                //Propiedades de las celdas de la tabla articulos
                cellEstadisticasD11.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cellEstadisticasD11.Border = Rectangle.NO_BORDER;
                cellEstadisticasD11.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                cellEstadisticasD11.BackgroundColor = colorB;
                cellEstadisticasD11.FixedHeight = 20f;
                //Propiedades de las celdas de la tabla articulos
                cellEstadisticasD12.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cellEstadisticasD12.Border = Rectangle.NO_BORDER;
                cellEstadisticasD12.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                cellEstadisticasD12.BackgroundColor = colorB;
                //Propiedades de las celdas de la tabla articulos
                cellEstadisticasD13.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cellEstadisticasD13.Border = Rectangle.NO_BORDER;
                cellEstadisticasD13.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                cellEstadisticasD13.BackgroundColor = colorB;
                //Propiedades de las celdas de la tabla articulos
                cellEstadisticasD21.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cellEstadisticasD21.Border = Rectangle.NO_BORDER;
                cellEstadisticasD21.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                cellEstadisticasD21.BackgroundColor = colorB;
                cellEstadisticasD21.FixedHeight = 20f;
                //Propiedades de las celdas de la tabla articulos
                cellEstadisticasD22.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cellEstadisticasD22.Border = Rectangle.NO_BORDER;
                cellEstadisticasD22.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                cellEstadisticasD22.BackgroundColor = colorB;
                //Propiedades de las celdas de la tabla articulos
                cellEstadisticasD23.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cellEstadisticasD23.Border = Rectangle.NO_BORDER;
                cellEstadisticasD23.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                cellEstadisticasD23.BackgroundColor = colorB;
                //Propiedades de las celdas de la tabla articulos
                cellEstadisticasD31.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cellEstadisticasD31.Border = Rectangle.NO_BORDER;
                cellEstadisticasD31.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                cellEstadisticasD31.BackgroundColor = colorB;
                cellEstadisticasD31.FixedHeight = 20f;
                //Propiedades de las celdas de la tabla articulos
                cellEstadisticasD32.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cellEstadisticasD32.Border = Rectangle.NO_BORDER;
                cellEstadisticasD32.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                cellEstadisticasD32.BackgroundColor = colorB;
                //Propiedades de las celdas de la tabla articulos
                cellEstadisticasD33.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cellEstadisticasD33.Border = Rectangle.NO_BORDER;
                cellEstadisticasD33.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                cellEstadisticasD33.BackgroundColor = colorB;

                cellObservacionesD.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cellObservacionesD.Border = Rectangle.NO_BORDER;
                cellObservacionesD.BorderColor = new BaseColor(0, 0, 0); //Color del borde
                cellObservacionesD.BackgroundColor = colorB;

                //Inserta los valores en la factura---------------------------------------------------                   
                tablaEstadisticas.AddCell(cellEstadisticas11); //Agrega COLUMNA TITTLE
                tablaEstadisticas.AddCell(cellEstadisticas12); //Agrega COLUMNA TITTLE
                tablaEstadisticas.AddCell(cellEstadisticas13); //Agrega COLUMNA TITTLE                
                tablaEstadisticas.AddCell(cellEstadisticasD11); //Agrega COLUMNA DATA
                tablaEstadisticas.AddCell(cellEstadisticasD12); //Agrega COLUMNA DATA
                tablaEstadisticas.AddCell(cellEstadisticasD13); //Agrega COLUMNA DATA
                tablaEstadisticas.AddCell(cellEstadisticas21); //Agrega COLUMNA TITTLE
                tablaEstadisticas.AddCell(cellEstadisticas22); //Agrega COLUMNA TITTLE
                tablaEstadisticas.AddCell(cellEstadisticas23); //Agrega COLUMNA TITTLE                
                tablaEstadisticas.AddCell(cellEstadisticasD21); //Agrega COLUMNA DATA
                tablaEstadisticas.AddCell(cellEstadisticasD22); //Agrega COLUMNA DATA
                tablaEstadisticas.AddCell(cellEstadisticasD23); //Agrega COLUMNA DATA
                tablaEstadisticas.AddCell(cellEstadisticas31); //Agrega COLUMNA TITTLE
                tablaEstadisticas.AddCell(cellEstadisticas32); //Agrega COLUMNA TITTLE
                tablaEstadisticas.AddCell(cellEstadisticas33); //Agrega COLUMNA TITTLE                
                tablaEstadisticas.AddCell(cellEstadisticasD31); //Agrega COLUMNA DATA
                tablaEstadisticas.AddCell(cellEstadisticasD32); //Agrega COLUMNA DATA
                tablaEstadisticas.AddCell(cellEstadisticasD33); //Agrega COLUMNA DATA

                tablaObservaciones.AddCell(cellObservaciones); //Agrega COLUMNA
                tablaObservaciones.AddCell(cellObservacionesD); //Agrega COLUMNA

                tablaGral6.AddCell(tablaEstadisticas);
                tablaGral6.AddCell(tablaObservaciones);

                document.Add(tablaGral6);// Agrega la tabla al documento

                //'----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

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