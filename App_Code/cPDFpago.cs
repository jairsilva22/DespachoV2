using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace despacho
{

    public class cPDFpago
    {
        cClientes cte = new cClientes();
        cPagos pago = new cPagos();
        cSolicitudes sol = new cSolicitudes();

        private string cadena;
       
        //propiedades
        public string path { get; set; }
        public int idSolicitud { get; set; }
        public int idCliente { get; set; }
        public int idPago { get; set; }
        public string folio { get; set; }
        public string nombreDoc { get; set; }
        public string sObservaciones { get; set; }
        public string cmoneda { get; set; }

        public cPDFpago()
        {
            cadena = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;
        }

        public string generarPDF()
        {
            
            string pathLetter = path + @"fonts\";
            string nombrePDF = nombreDoc;
            string imagen;
            string folio = "";
            string nombreCte = "";
            string domicilio = "";
            string colonia = "";
            string codp = "";
            string estado = "";
            string ciudad = "";
            string clave = "";
            string telefono = "";
            string fecha;

            //indicamos el tamaño de la hoja del pdf
            Document document = new Document(PageSize.LETTER);
            PdfWriter pdf;
            //Declaracion de las Fuentes y propiedades utilizadas
            Font arial12 = new Font(FontFactory.GetFont(pathLetter + "arial.ttf", 12, BaseColor.BLACK));
            Font arial12N = new Font(FontFactory.GetFont(pathLetter + "arial.ttf", 12, Font.BOLD, BaseColor.BLACK));
            Font times12 = new Font(FontFactory.GetFont(FontFactory.TIMES_BOLD, 12, BaseColor.BLACK));
            Font arial14 = new Font(FontFactory.GetFont(pathLetter + "arial.ttf", 14, BaseColor.BLACK));
            Font arial9NW = new Font(FontFactory.GetFont(pathLetter + "arial.ttf", 9, Font.BOLD, BaseColor.WHITE));
            Font arial8NW = new Font(FontFactory.GetFont(pathLetter + "arial.ttf", 8, Font.BOLD, BaseColor.WHITE));
            Font arial14N = new Font(FontFactory.GetFont(pathLetter + "arial.ttf", 14, Font.BOLD, BaseColor.BLACK));
            Font arial9N = new Font(FontFactory.GetFont(pathLetter + "arial.ttf", 9, Font.BOLD, BaseColor.BLACK));
            Font arial7 = new Font(FontFactory.GetFont(pathLetter + "arial.ttf", 7, BaseColor.BLACK));
            Font arial10 = new Font(FontFactory.GetFont(pathLetter + "arial.ttf", 10, BaseColor.BLACK));
            Font arial16W = new Font(FontFactory.GetFont(pathLetter + "arial.ttf", 16, BaseColor.WHITE));
            Font arial14W = new Font(FontFactory.GetFont(pathLetter + "arial.ttf", 14, BaseColor.WHITE));
            Font arial11W = new Font(FontFactory.GetFont(pathLetter + "arial.ttf", 11, BaseColor.WHITE));
            Font arial11 = new Font(FontFactory.GetFont(pathLetter + "arial.ttf", 11, BaseColor.BLACK));
            Font arial28 = new Font(FontFactory.GetFont(pathLetter + "arial.ttf", 28, Font.BOLD, BaseColor.RED));

            Font arial12NW = new Font(FontFactory.GetFont(pathLetter + "arial.ttf", 12, Font.BOLD, BaseColor.WHITE));
            //Color de fondo de las columnas de la tabla
            BaseColor colorTabla = new BaseColor(5, 51, 136);
            BaseColor ColorHeaders = new BaseColor(227, 48, 69);
            BaseColor colorEnc = new BaseColor(0, 0, 255, 0);
            BaseColor color1 = new BaseColor(255, 255, 255);
            BaseColor color2 = new BaseColor(236, 236, 236);

            try
            {
                //varificar si existe la carptea si no crearla
                if (!Directory.Exists(path + "\\Pagos"))
                {
                    Directory.CreateDirectory(path + "\\Pagos");
                }

                //verificamos si existe el archivo
                if (File.Exists(path + "\\Pagos\\" + nombrePDF))
                {
                    //si existe lo borramos y creamos uno nuevo
                    File.Delete(nombrePDF);
                    pdf = PdfWriter.GetInstance(document, new FileStream(path + "\\Pagos\\" + nombrePDF, FileMode.Create, FileAccess.Write, FileShare.None));
                }
                else
                {
                    //si no existe solo lo creamos
                    pdf = PdfWriter.GetInstance(document, new FileStream(path + "\\Pagos\\" + nombrePDF, FileMode.Create, FileAccess.Write, FileShare.None));
                }

                //buscamos datos del cliente
                sol.obtenerSolicitudByID(idSolicitud);
                cmoneda = "MXN";
                colonia = sol.colonia;
                clave = sol.clave;
                nombreCte = sol.nombreCliente;
                codp = sol.cp;
                estado = sol.estado;
                ciudad = sol.ciudad;
                folio = sol.folio;
                telefono = sol.celular;
                DateTime fecha1 = DateTime.Parse(pago.obtenerFechaPagoByID(idPago));
                fecha = fecha1.ToString("dd/MM/yyyy");
                folio = pago.obtenerFolioByID(idPago);
                if (sol.interior != "")
                {
                    domicilio = sol.calle + " #" + sol.numero + "-"+sol.interior;
                }
                else
                {
                    domicilio = sol.calle + " #" + sol.numero;
                }
               
                // Imagen del logo
                imagen = path + "\\img\\pepi_logo.png";
                Image imagenLogo = Image.GetInstance(@imagen);
                imagenLogo.ScaleAbsolute(140, 60);

                //abrimos el documento
                document.Open();

                PdfPTable tablaEncabezado = new PdfPTable(2);
                tablaEncabezado.WidthPercentage = 100;

                PdfPTable tablaFolio = new PdfPTable(2);
                tablaFolio.WidthPercentage = 100;
               
                PdfPCell cellImg = new PdfPCell(imagenLogo);
                PdfPCell cellTitulo = new PdfPCell(new Phrase("PAGO DE SOLICITUD", arial12NW));

                PdfPCell cellFolio1 = new PdfPCell(new Phrase("Folio: ", arial12));
                PdfPCell cellFolio2 = new PdfPCell(new Phrase(folio, arial12N));
                PdfPCell cellFecha1 = new PdfPCell(new Phrase("Fecha: ", arial12));
                PdfPCell cellFecha2 = new PdfPCell(new Phrase(fecha, arial12N));
                PdfPCell cellTablaFolio = new PdfPCell(tablaFolio);

                cellImg.Border = Rectangle.NO_BORDER;
                cellImg.Rowspan = 2;
                cellTitulo.Border = Rectangle.NO_BORDER;
                cellTitulo.BackgroundColor = colorTabla;
                cellTitulo.HorizontalAlignment = Rectangle.ALIGN_CENTER;

                cellFolio1.Border = Rectangle.NO_BORDER;
                cellFolio2.Border = Rectangle.NO_BORDER;
                cellFolio1.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                cellFolio2.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                cellFecha1.Border = Rectangle.NO_BORDER;
                cellFecha2.Border = Rectangle.NO_BORDER;
                cellFecha1.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                cellFecha2.HorizontalAlignment = Rectangle.ALIGN_RIGHT;

                cellTablaFolio.Border = Rectangle.NO_BORDER;

                tablaFolio.AddCell(cellFolio1);
                tablaFolio.AddCell(cellFolio2);
                tablaFolio.AddCell(cellFecha1);
                tablaFolio.AddCell(cellFecha2);

                tablaEncabezado.AddCell(cellImg);
                tablaEncabezado.AddCell(cellTitulo);
                tablaEncabezado.AddCell(cellTablaFolio);

                document.Add(tablaEncabezado);

                PdfPTable tableCliente = new PdfPTable(1);
                tableCliente.WidthPercentage = 100;

                PdfPCell cellClave1 = new PdfPCell(new Phrase("Clave: ", arial12));
                PdfPCell cellClave2 = new PdfPCell(new Phrase(clave, arial12));
                PdfPCell cellNombre1 = new PdfPCell(new Phrase("Cliente: ", arial12));
                PdfPCell cellNombre2 = new PdfPCell(new Phrase(nombreCte, arial12));
                PdfPCell cellDireccion1 = new PdfPCell(new Phrase("Domicilio: ", arial12));
                PdfPCell cellDireccion2 = new PdfPCell(new Phrase(domicilio, arial12));
                PdfPCell cellTelefono1 = new PdfPCell(new Phrase("Teléfono: ", arial12));
                PdfPCell cellTelefono2 = new PdfPCell(new Phrase(telefono, arial12));
                PdfPCell cellColonia1 = new PdfPCell(new Phrase("Colonia: ", arial12));
                PdfPCell cellColonia2 = new PdfPCell(new Phrase(colonia, arial12));
                PdfPCell cellCp1 = new PdfPCell(new Phrase("Código Postal:  ", arial12));
                PdfPCell cellCp2 = new PdfPCell(new Phrase(codp, arial12));
                PdfPCell cellCd1 = new PdfPCell(new Phrase("Ciudad: ", arial12));
                PdfPCell cellCd2 = new PdfPCell(new Phrase(ciudad, arial12));
                PdfPCell cellEdo1 = new PdfPCell(new Phrase("Estado: ", arial12));
                PdfPCell cellEdo2 = new PdfPCell(new Phrase(estado, arial12));

                cellClave1.Border = Rectangle.NO_BORDER;
                cellClave2.Border = Rectangle.NO_BORDER;
                cellNombre1.Border = Rectangle.NO_BORDER;
                cellNombre2.Border = Rectangle.NO_BORDER;
                cellDireccion1.Border = Rectangle.NO_BORDER;
                cellDireccion2.Border = Rectangle.NO_BORDER;
                cellTelefono1.Border = Rectangle.NO_BORDER;
                cellTelefono2.Border = Rectangle.NO_BORDER;
                cellColonia1.Border = Rectangle.NO_BORDER;
                cellColonia2.Border = Rectangle.NO_BORDER;
                cellCp1.Border = Rectangle.NO_BORDER;
                cellCp2.Border = Rectangle.NO_BORDER;
                cellCd1.Border = Rectangle.NO_BORDER;
                cellCd2.Border = Rectangle.NO_BORDER;
                cellEdo1.Border = Rectangle.NO_BORDER;
                cellEdo2.Border = Rectangle.NO_BORDER;

                PdfPTable tabladato = new PdfPTable(4);
                tabladato.WidthPercentage = 100;
                int[] anchodato = { 5, 15, 5, 30 };

                tabladato.SetWidths(anchodato);
                tabladato.AddCell(cellClave1);
                tabladato.AddCell(cellClave2);
                tabladato.AddCell(cellNombre1);
                tabladato.AddCell(cellNombre2);

                PdfPCell celldato = new PdfPCell(tabladato);
                celldato.Border = Rectangle.NO_BORDER;

                PdfPTable tabladato1 = new PdfPTable(4);
                tabladato1.WidthPercentage = 100;
                int[] anchodato1 = { 8, 30, 8, 30 };

                tabladato1.SetWidths(anchodato1);
                tabladato1.AddCell(cellDireccion1);
                tabladato1.AddCell(cellDireccion2);
                tabladato1.AddCell(cellTelefono1);
                tabladato1.AddCell(cellTelefono2);

                PdfPCell celldato1 = new PdfPCell(tabladato1);
                celldato1.Border = Rectangle.NO_BORDER;

                PdfPTable tabladato2 = new PdfPTable(4);
                tabladato2.WidthPercentage = 100;
                int[] anchodato2 = { 8, 40, 15, 20 };

                tabladato2.SetWidths(anchodato2);
                tabladato2.AddCell(cellColonia1);
                tabladato2.AddCell(cellColonia2);
                tabladato2.AddCell(cellCp1);
                tabladato2.AddCell(cellCp2);

                PdfPCell celldato2 = new PdfPCell(tabladato2);
                celldato2.Border = Rectangle.NO_BORDER;

                PdfPTable tabladato3 = new PdfPTable(4);
                tabladato3.WidthPercentage = 100;
                int[] anchodato3 = { 7, 30, 7, 30 };

                tabladato3.SetWidths(anchodato3);
                tabladato3.AddCell(cellCd1);
                tabladato3.AddCell(cellCd2);
                tabladato3.AddCell(cellEdo1);
                tabladato3.AddCell(cellEdo2);

                PdfPCell celldato3 = new PdfPCell(tabladato3);
                celldato3.Border = Rectangle.NO_BORDER;

                PdfPCell cellInfo = new PdfPCell(new Phrase("PEPI", arial14W));
                
                cellInfo.Border = Rectangle.NO_BORDER;
                cellInfo.BackgroundColor = colorTabla;

                PdfPCell cellvacia = new PdfPCell(new Phrase(" ", arial12));
                cellvacia.Border = Rectangle.NO_BORDER;
                
                tableCliente.AddCell(cellvacia);
                tableCliente.AddCell(cellInfo);
                tableCliente.AddCell(celldato);
                tableCliente.AddCell(celldato1);
                tableCliente.AddCell(celldato2);
                tableCliente.AddCell(celldato3);
                tableCliente.AddCell(cellvacia);
               
                document.Add(tableCliente);

                PdfPTable tablaConceptos = new PdfPTable(5);
                tablaConceptos.WidthPercentage = 100;
                
                PdfPCell cell1 = new PdfPCell(new Phrase("Cantidad", arial12NW));
                PdfPCell cell2 = new PdfPCell(new Phrase("Unidad", arial12NW));
                PdfPCell cell3 = new PdfPCell(new Phrase("Descripción", arial12NW));
                PdfPCell cell4 = new PdfPCell(new Phrase("Valor Unitario", arial12NW));
                PdfPCell cell5 = new PdfPCell(new Phrase("Importe", arial12NW));

                cell1.BackgroundColor = ColorHeaders;
                cell1.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell2.BackgroundColor = ColorHeaders;
                cell2.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell3.BackgroundColor = ColorHeaders;
                cell3.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell4.BackgroundColor = ColorHeaders;
                cell4.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell5.BackgroundColor = ColorHeaders;
                cell5.HorizontalAlignment = Rectangle.ALIGN_CENTER;

                int[] conceptos = { 10, 10, 30, 10, 10 };

                tablaConceptos.SetWidths(conceptos);
                tablaConceptos.AddCell(cell1);
                tablaConceptos.AddCell(cell2);
                tablaConceptos.AddCell(cell3);
                tablaConceptos.AddCell(cell4);
                tablaConceptos.AddCell(cell5);

                DataTable dt = sol.obtenerDetallesSolicitud(idSolicitud);
                for(int i = 0; i < dt.Rows.Count; i++)
                {
                    string precio = float.Parse(dt.Rows[i]["precioF"].ToString()).ToString("#,#0.00");
                    string subtotalP = float.Parse(dt.Rows[i]["subtotal"].ToString()).ToString("#,#0.00");
                    PdfPCell cell6 = new PdfPCell(new Phrase(dt.Rows[i]["cantidad"].ToString()));
                    PdfPCell cell7 = new PdfPCell(new Phrase(dt.Rows[i]["unidadSAT"].ToString()));
                    PdfPCell cell8 = new PdfPCell(new Phrase(dt.Rows[i]["prodDesc"].ToString()));
                    PdfPCell cell9 = new PdfPCell(new Phrase(precio));
                    PdfPCell cell10 = new PdfPCell(new Phrase(subtotalP));


                    cell6.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                    cell7.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                    cell9.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                    cell10.HorizontalAlignment = Rectangle.ALIGN_RIGHT;

                    tablaConceptos.AddCell(cell6);
                    tablaConceptos.AddCell(cell7);
                    tablaConceptos.AddCell(cell8);
                    tablaConceptos.AddCell(cell9);
                    tablaConceptos.AddCell(cell10);
                }

                document.Add(tablaConceptos);

                //obtenenemos subtotal
                float subtotal = float.Parse(sol.obtenerSubtotalSolicitud(idSolicitud));
                //obtenemos IVA
                float iva = float.Parse(sol.obtenerIVASolicitud(idSolicitud));
                //obtenemos el iva total 
                float ivaTotal = subtotal * iva;
                //obtenemos total
                float total = float.Parse(sol.obtenerMontoSolicitud(idSolicitud));

                PdfPTable tablaFinal = new PdfPTable(2);
                tablaFinal.WidthPercentage = 100;

                PdfPTable tablaImporteLetra = new PdfPTable(1);
                tablaImporteLetra.WidthPercentage = 100;

                PdfPTable tablaSubtotales = new PdfPTable(2);
                tablaSubtotales.WidthPercentage = 100;
                
                PdfPCell cellImporte1 = new PdfPCell(new Phrase("Importe con letra", arial12NW)); 
                PdfPCell cellImporte2 = new PdfPCell(new Phrase(enLetras(total.ToString("#,#0.00")), arial11));
                PdfPCell cellImporte3 = new PdfPCell(new Phrase(" ", arial11));

                cellImporte1.BackgroundColor = colorTabla;
                cellImporte1.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cellImporte2.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cellImporte2.Rowspan = 1;
                cellImporte3.Border = Rectangle.NO_BORDER;

                tablaImporteLetra.AddCell(cellImporte1);
                tablaImporteLetra.AddCell(cellImporte2);
                tablaImporteLetra.AddCell(cellImporte3);

                PdfPCell cellSubtotal1 = new PdfPCell(new Phrase("Subtotal:", arial12));
                PdfPCell cellSubtotal2 = new PdfPCell(new Phrase(subtotal.ToString("#,#0.00"), arial12));
                PdfPCell cellIVA1 = new PdfPCell(new Phrase("I.V.A.:", arial12));
                PdfPCell cellIVA2 = new PdfPCell(new Phrase(ivaTotal.ToString("#,#0.00"), arial12));
                PdfPCell cellTotal1 = new PdfPCell(new Phrase("Total:", arial12));
                PdfPCell cellTotal2 = new PdfPCell(new Phrase(total.ToString("#,#0.00"), arial12));
                PdfPCell cellPago1 = new PdfPCell(new Phrase("Pago:", arial12));
                float tmp_pago = pago.obtenerPagadoFinanzasByIdPago(idPago);
                PdfPCell cellPago2 = new PdfPCell(new Phrase((tmp_pago).ToString("#,#0.00"), arial12));
                PdfPCell cellPagado1 = new PdfPCell(new Phrase("Pagado:", arial12));
                PdfPCell cellPagado2 = new PdfPCell(new Phrase(pago.obtenerPagoFinanzasByIdSol(idSolicitud).ToString("#,#0.00"), arial12));
                PdfPCell cellSaldo1 = new PdfPCell(new Phrase("Saldo:", arial12));
                PdfPCell cellSaldo2 = new PdfPCell(new Phrase(pago.obtenerSaldoFinanzasByIdPago(idPago).ToString("#,#0.00"), arial12));

                cellSubtotal1.Border = Rectangle.NO_BORDER;
                cellSubtotal2.Border = Rectangle.NO_BORDER;
                cellIVA1.Border = Rectangle.NO_BORDER;
                cellIVA2.Border = Rectangle.NO_BORDER;
                cellTotal1.Border = Rectangle.NO_BORDER;
                cellTotal2.Border = Rectangle.NO_BORDER;
                cellPago1.Border = Rectangle.NO_BORDER;
                cellPago2.Border = Rectangle.NO_BORDER;
                cellPagado1.Border = Rectangle.NO_BORDER;
                cellPagado2.Border = Rectangle.NO_BORDER;
                cellSaldo1.Border = Rectangle.NO_BORDER;
                cellSaldo2.Border = Rectangle.NO_BORDER;

                cellSubtotal1.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                cellSubtotal2.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                cellIVA1.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                cellIVA2.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                cellTotal1.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                cellTotal2.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                cellPago1.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                cellPago2.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                cellPagado1.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                cellPagado2.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                cellSaldo1.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                cellSaldo2.HorizontalAlignment = Rectangle.ALIGN_RIGHT;

                tablaSubtotales.AddCell(cellSubtotal1);
                tablaSubtotales.AddCell(cellSubtotal2);
                tablaSubtotales.AddCell(cellIVA1);
                tablaSubtotales.AddCell(cellIVA2);
                tablaSubtotales.AddCell(cellTotal1);
                tablaSubtotales.AddCell(cellTotal2);
                tablaSubtotales.AddCell(cellPago1);
                tablaSubtotales.AddCell(cellPago2);
                tablaSubtotales.AddCell(cellPagado1);
                tablaSubtotales.AddCell(cellPagado2);
                tablaSubtotales.AddCell(cellSaldo1);
                tablaSubtotales.AddCell(cellSaldo2);

                PdfPCell cellTabla1 = new PdfPCell(tablaImporteLetra);
                PdfPCell cellTabla2 = new PdfPCell(tablaSubtotales);

                cellTabla1.Border = Rectangle.NO_BORDER;
                cellTabla2.Border = Rectangle.NO_BORDER;

                int[] ancho = { 70, 30 };
                tablaFinal.SetWidths(ancho);
                tablaFinal.AddCell(cellTabla1);
                tablaFinal.AddCell(cellTabla2);
                

                PdfPTable tableVacia = new PdfPTable(1);
                tableVacia.WidthPercentage = 100;

                PdfPCell cellvacia1 = new PdfPCell(new Phrase(" ", arial12));
                cellvacia1.Border = Rectangle.NO_BORDER;

                tableVacia.AddCell(cellvacia1);

                document.Add(tableVacia);
                document.Add(tablaFinal);
                

                //verificamos que el documento no esté abierto
                if (document.IsOpen())
                {
                    //si está abierto lo cerramos
                    document.Close();
                    // document.Dispose();
                }

                return "Creado";
            }
            catch(Exception ex)
            {
                //si hubo un error, cerramos el archivo
                if (document.IsOpen())
                {
                    document.Close();
                    //document.Dispose();
                }
                return ex.Message;
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
            dec = cmoneda;

            if (decimales > 0)
            {
                dec = " " + cmoneda + " " + decimales.ToString();
            }
            else
            {
                dec = " " + cmoneda + " " + " 00";
            }

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