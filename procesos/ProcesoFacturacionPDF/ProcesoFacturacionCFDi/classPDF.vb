Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.Data.SqlClient
Imports System.IO
Public Class classPDF

    '-------------------------------------------------Elfyr 05-03-2020--------------------------------------------------------------

    Public Function llenarPDF(ByVal ruta As String, ByVal archivoPDF As String, ByVal idfactura As String, ByVal carpeta As String)
        Dim fontpath As String = cUtilerias.fuentes 'Variable para guardar la ruta de las fuetnes
        Dim logo As Image 'Declaracion de una imagen          
        Dim uuid As Image 'Declaracion de una imagen          
        Dim stylo1 As Font = New Font(FontFactory.GetFont(fontpath + "arial.TTF", 11, Font.NORMAL, New BaseColor(0, 0, 0))) 'creamos la fuente         
        Dim stylo2 As Font = New Font(FontFactory.GetFont(fontpath + "arial.TTF", 9, Font.NORMAL, New BaseColor(0, 0, 0))) 'creamos la fuente           
        Dim stylo3 As Font = New Font(FontFactory.GetFont(fontpath + "arial.ttf", 10, Font.BOLD, BaseColor.BLACK)) 'creamos la fuente 
        Dim stylo4 As Font = New Font(FontFactory.GetFont(fontpath + "arial.ttf", 15, Font.BOLD, BaseColor.BLACK)) 'creamos la fuente 
        Dim stylo5 As Font = New Font(FontFactory.GetFont(fontpath + "arial.ttf", 8, Font.BOLD, BaseColor.BLACK)) 'creamos la fuente 
        Dim stylo6 As Font = New Font(FontFactory.GetFont(fontpath + "arial.ttf", 8, BaseColor.BLACK)) 'creamos la fuente 
        Dim stylo7 As Font = New Font(FontFactory.GetFont(fontpath + "arial.ttf", 10, Font.NORMAL, BaseColor.BLACK)) 'creamos la fuente
        Dim stylo8 As Font = New Font(FontFactory.GetFont(fontpath + "arial.ttf", 6, Font.NORMAL, BaseColor.BLACK)) 'creamos la fuente
        Dim stylo9 As Font = New Font(FontFactory.GetFont(fontpath + "arial.ttf", 9, Font.BOLDITALIC, BaseColor.BLACK)) 'creamos la fuente
        Dim stylo10 As Font = New Font(FontFactory.GetFont(fontpath + "arial.ttf", 8, Font.BOLD, BaseColor.BLACK)) 'creamos la fuente 
        Dim stylo11 As Font = New Font(FontFactory.GetFont(fontpath + "arial.ttf", 10, Font.BOLD, BaseColor.BLACK)) 'creamos la fuente 
        Dim stylo12 As Font = New Font(FontFactory.GetFont(fontpath + "arial.ttf", 8, Font.BOLD, BaseColor.BLACK)) 'creamos la fuente
        Dim stylo13 As Font = New Font(FontFactory.GetFont(fontpath + "arial.ttf", 8, Font.NORMAL, BaseColor.BLACK)) 'creamos la fuente
        Dim stylo14 As Font = New Font(FontFactory.GetFont(fontpath + "arial.ttf", 6, Font.NORMAL, BaseColor.BLACK)) 'creamos la fuente 
        Dim stylo15 As Font = New Font(FontFactory.GetFont(fontpath + "arial.ttf", 9, Font.BOLD, New BaseColor(0, 0, 0))) 'creamos la fuente 
        Dim stylo16 As Font = New Font(FontFactory.GetFont(fontpath + "arial.ttf", 8, Font.BOLD, New BaseColor(0, 0, 0))) 'creamos la fuente 
        Dim stylo17 As Font = New Font(FontFactory.GetFont(fontpath + "arial.ttf", 7, Font.BOLD, New BaseColor(0, 0, 0))) 'creamos la fuente 
        Dim stylo18 As Font = New Font(FontFactory.GetFont(fontpath + "arial.ttf", 7, Font.NORMAL, BaseColor.BLACK)) 'creamos la fuente
        Dim stylo19 As Font = New Font(FontFactory.GetFont(fontpath + "arial.TTF", 10, Font.NORMAL, New BaseColor(0, 0, 0))) 'creamos la fuente         
        Dim stylo20 As Font = New Font(FontFactory.GetFont(fontpath + "arial.TTF", 7, Font.NORMAL, New BaseColor(0, 0, 0))) 'creamos la fuente    
        Dim stylo21 As Font = New Font(FontFactory.GetFont(fontpath + "arial.ttf", 6, Font.NORMAL, BaseColor.BLACK)) 'creamos la fuente
        Dim stylo22 As Font = New Font(FontFactory.GetFont(fontpath + "arial.TTF", 13, Font.NORMAL, New BaseColor(0, 0, 0))) 'creamos la fuente         
        Dim stylo23 As Font = New Font(FontFactory.GetFont(fontpath + "arial.TTF", 10, Font.NORMAL, New BaseColor(0, 0, 0))) 'creamos la fuente    
        Dim stylo24 As Font = New Font(FontFactory.GetFont(fontpath + "arial.ttf", 6, Font.BOLD, New BaseColor(0, 0, 0))) 'creamos la fuente 
        Dim stylo25 As Font = New Font(FontFactory.GetFont(fontpath + "arial.ttf", 7, Font.NORMAL, BaseColor.BLACK)) 'creamos la fuente
        Dim stylo26 As Font = New Font(FontFactory.GetFont(fontpath + "arial.ttf", 8, Font.BOLD, BaseColor.BLACK)) 'creamos la fuente 
        Dim stylo27 As Font = New Font(FontFactory.GetFont(fontpath + "arial.ttf", 7, Font.BOLD, BaseColor.BLACK)) 'creamos la fuente 
        Dim stylo28 As Font = New Font(FontFactory.GetFont(fontpath + "arial.TTF", 11, Font.BOLD, New BaseColor(0, 0, 0))) 'creamos la fuente         
        Dim stylo29 As Font = New Font(FontFactory.GetFont(fontpath + "arial.TTF", 9, Font.BOLD, New BaseColor(0, 0, 0))) 'creamos la fuente  
        Dim stylo30 As Font = New Font(FontFactory.GetFont(fontpath + "arial.TTF", 8, Font.BOLD, New BaseColor(0, 0, 0))) 'creamos la fuente
        Dim stylo31 As Font = New Font(FontFactory.GetFont(fontpath + "arial.TTF", 5, Font.BOLD, New BaseColor(0, 0, 0))) 'creamos la fuente
        Dim stylo32 As Font = New Font(FontFactory.GetFont(fontpath + "verdana.ttf", 36, Font.BOLD, BaseColor.RED)) 'creamos la fuente 
        Dim fontArialB As Font = New Font(FontFactory.GetFont(fontpath + "arial.ttf", 16, BaseColor.WHITE))
        Dim arial10B As Font = New Font(FontFactory.GetFont(fontpath + "arial.ttf", 8, BaseColor.WHITE))
        Dim arial10B2 As Font = New Font(FontFactory.GetFont(fontpath + "arial.ttf", 5, BaseColor.WHITE))
        Dim arial10B3 As Font = New Font(FontFactory.GetFont(fontpath + "arial.ttf", 10, BaseColor.WHITE))
        Dim fontDatosE As Font = New Font(FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14, BaseColor.BLACK))
        Dim colorHeaders As BaseColor = New BaseColor(224, 11, 24, 100)
        Dim ColorBco As BaseColor = New BaseColor(255, 255, 255)
        Dim ColorGris As BaseColor = New BaseColor(236, 236, 236)
        Dim fontDatos1 As Font = New Font(FontFactory.GetFont(fontpath + "arial.TTF", 8, BaseColor.BLACK))

        Dim anchoDatos() As Single = {20, 50} 'Tamaños de celdas de la tabla encabezado
        Dim anchoFecha() As Single = {63, 10, 27} 'Tamaños de las celdas de la tabla 
        Dim anchoCliente() As Single = {63, 37} 'Tamaños de las celdas de la tabla fecha
        Dim anchoDatosCliente() As Single = {60, 40} 'Tamaños de las celdas de la tabla fdatos del cliente
        Dim anchorfc() As Single = {50, 20, 30} 'Tamaños de las celdas de la tabla RFC
        Dim anchoMetodoPago() As Single = {15, 15, 20, 15, 18, 7, 4, 10} 'Tamaños de las celdas de la tabla metodo de pago
        Dim anchoArticulos() As Single = {6, 14, 46, 7, 7, 17, 9} 'Tamaños de las celdas de la tabla articulos
        Dim anchoTotales() As Single = {75, 15, 10} 'Tamaños de las celdas de la tabla totales
        Dim anchoFirma() As Single = {59, 39, 2} 'Tamaños de las celdas de la tabla firma
        Dim anchoFiscal() As Single = {18, 2, 80} 'Tamaños de las celdas de la tabla fiscal
        Dim anchoSello() As Single = {25, 1, 74} 'Tamaños de las celdas de la tabla sello
        Dim año As String = Year(cUtilerias.fechaCfD) 'Variable para guardar el año
        Dim mes As String = Month(cUtilerias.fechaCfD) 'Variable para guardar el mes
        Dim dia As String = Day(cUtilerias.fechaCfD) 'Variable para guardar el dia
        Dim hora As String = Hour(cUtilerias.fechaCfD) 'Variable para guardar el hora
        Dim minutos As String = Minute(cUtilerias.fechaCfD) 'Variable para guardar el minutos
        Dim segundos As String = Second(cUtilerias.fechaCfD) 'Variable para guardar el segundos
        Dim vistaRem As String = ""
        Dim anchoTipo() As Single = {100, 100}
        Dim tipoDocumento As String = ""

        'if para validar si es menor que 10
        If mes < 10 Then 'mes
            mes = "0" & mes
        End If 'mes

        'if para validar si es menor que 10
        If dia < 10 Then 'dia
            dia = "0" & dia
        End If 'dia

        'if para validar si es menor que 10
        If hora < 10 Then 'hora
            hora = "0" & hora
        End If 'hora

        'if para validar si es menor que 10
        If minutos < 10 Then 'minutos
            minutos = "0" & minutos
        End If 'minutos

        'if para validar si es menor que 10
        If segundos < 10 Then 'segundos
            segundos = "0" & segundos
        End If 'segundos

        'creamos el documento y configuramos para que el tamaño de hoja sea A4        
        Dim document As Document = New Document(PageSize.A4)
        'creamos un instancia del objeto escritor de documento
        Dim writer As PdfWriter = PdfWriter.GetInstance(document, New System.IO.FileStream(archivoPDF, System.IO.FileMode.Create))
        'abrimos el documento para agregarle contenido
        document.Open()
        '-------------------------------------
        'Declaracion de una imagen
        logo = Image.GetInstance(carpeta & "\" & cUtilerias.logoEmpresa) 'Dirreccion a la imagen que se hace referencia
        logo.SetAbsolutePosition(17, 767) 'Posicion en el eje cartesiano
        logo.ScaleAbsoluteWidth(cUtilerias.anchoLogoEmpresa) 'Ancho de la imagen
        logo.ScaleAbsoluteHeight(cUtilerias.altoLogoEmpresa) 'Altura de la imagen    
        '----------------------------------------------------------------------- 
        'Declaracion de una imagen
        Dim qrcode As pdf.BarcodeQRCode = New pdf.BarcodeQRCode(cUtilerias.UUID, 1, 1, Nothing) 'Variable para guardar el codigo bidimensional

        uuid = Image.GetInstance(qrcode.GetImage) 'Dirreccion a la imagen que se hace referencia
        'uuid.SetAbsolutePosition(17, 767) 'Posicion en el eje cartesiano
        uuid.ScaleAbsoluteWidth(cUtilerias.anchoUUID) 'Ancho de la imagen
        uuid.ScaleAbsoluteHeight(cUtilerias.altoUUID) 'Altura de la imagen    
        '----------------------------------------------------------------------- 
        Dim tablaEncabezado As New PdfPTable(2) 'Tabla encabezado
        Dim tablaEmpresa As New PdfPTable(1) 'Tabla de la empresa
        Dim tablaFolio As New PdfPTable(1) 'Tabla del folio
        Dim tablaFecha As New PdfPTable(1) 'Tabla de la fecha
        Dim tablaCliente As New PdfPTable(2) 'Tabla de datos del cliente y de la venta
        Dim tablaDatosCliente As New PdfPTable(2) 'Tabla del cliente
        Dim tablaVenta As New PdfPTable(1) 'Tabla de la venta
        Dim tablaRFC As New PdfPTable(3) 'Tabla del RFC
        Dim tablaConsignatario As New PdfPTable(1) 'Tabla del Consignatario
        Dim tablaMoneda As New PdfPTable(1) 'Tabla del Moneda
        Dim tablaMetodoPago As New PdfPTable(8)
        Dim tablaArticulos As New PdfPTable(7)
        Dim tablaTotales As New PdfPTable(3)
        Dim tablafirma As New PdfPTable(3)
        Dim tablaFiscal As New PdfPTable(3)
        Dim tablaUUID As New PdfPTable(1)
        Dim TablaSello As New PdfPTable(3)
        Dim tablaObservaciones As New PdfPTable(1)
        Dim tablaRemision As New PdfPTable(1)
        Dim tablaTipo As New PdfPTable(2)
        Dim tablaPedimento As New PdfPTable(3)
        Dim tablaRelacion As New PdfPTable(3)
        '----Propiedades de las tablas---------------------------------
        tablaDatosCliente.WidthPercentage = 100 'Ancho de la tabla
        tablaVenta.WidthPercentage = 100 'Ancho de la tabla
        tablaFolio.WidthPercentage = 100 'Ancho de la tabla
        tablaEmpresa.WidthPercentage = 100 'Ancho de la tabla
        tablaEncabezado.WidthPercentage = 100 'Ancho de la tabla
        tablaFecha.WidthPercentage = 100 'Ancho de la tabla
        tablaCliente.WidthPercentage = 100 'Ancho de la tabla
        tablaConsignatario.WidthPercentage = 100 'Ancho de la tabla
        tablaMoneda.WidthPercentage = 100 'Ancho de la tabla
        tablaMetodoPago.WidthPercentage = 100 'Ancho de la tabla
        tablaArticulos.WidthPercentage = 100 'Ancho de la tabla
        tablaTotales.WidthPercentage = 100 'Ancho de la tabla
        tablafirma.WidthPercentage = 100 'Ancho de la tabla
        tablaFiscal.WidthPercentage = 100 'Ancho de la tabla
        tablaUUID.WidthPercentage = 100 'Ancho de la tabla
        TablaSello.WidthPercentage = 100 'Ancho de la tabla
        tablaObservaciones.WidthPercentage = 100 'Ancho de la tablas
        tablaRemision.WidthPercentage = 100 'Ancho de la tablas
        tablaTipo.WidthPercentage = 100
        tablaPedimento.WidthPercentage = 100 'Ancho de la tablas
        tablaRelacion.WidthPercentage = 100
        '------------------------------------------------------------------
        'celdasa de la tabla firma----------------------------------------------
        Dim cellLineaVacia As New PdfPCell(New Phrase("")) 'Celda vacia para dar espacio
        Dim cellLinea As New PdfPCell(New Phrase("  ", stylo12)) 'Celda para poner la linea de firma        
        Dim cellLineaVacia2 As New PdfPCell(New Phrase("")) 'Celda vacia para dar espacio
        Dim cellRecibiVacia As New PdfPCell(New Phrase("")) 'Celda para guardar el texto RECIBI DE CONFORMIDAD
        Dim cellRecibi As New PdfPCell(New Phrase("RECIBI DE CONFORMIDAD", stylo12)) 'Celda para guardar el texto RECIBI DE CONFORMIDAD
        Dim cellRecibiVacia2 As New PdfPCell(New Phrase("", stylo12)) 'Celda para guardar el texto RECIBI DE CONFORMIDAD        
        '---------------------------------------------------------------------
        'celdasa de la tabla rfc----------------------------------------------
        Dim p3 As Phrase = New Phrase()
        Dim p4 As Phrase = New Phrase()
        Dim p5 As Phrase = New Phrase()

        Dim cellrfcCleinte2 As New Chunk("R.F.C.: ", stylo5) 'Celda para guardar el rfc del cliente
        Dim cellrfcCleinte3 As New Chunk(cUtilerias.rfcCliente, stylo6) 'Celda para guardar el rfc del cliente

        p3.Add(cellrfcCleinte2)
        p3.Add(cellrfcCleinte3)

        Dim cellcodigoPostalCleinte2 As New Chunk("C.P. ", stylo5) 'Celda para guardar el codigo postal del cliente
        Dim cellcodigoPostalCleinte3 As New Chunk(cUtilerias.codigoPostalCliente, stylo6) 'Celda para guardar el codigo postal del cliente

        p4.Add(cellcodigoPostalCleinte2)
        p4.Add(cellcodigoPostalCleinte3)

        Dim cellpaisCleinte2 As New Chunk("País: ", stylo5) 'Celda para guardar el pais del cliente
        Dim cellpaisCleinte3 As New Chunk(cUtilerias.paisCliente, stylo6) 'Celda para guardar el pais del cliente

        p5.Add(cellpaisCleinte2)
        p5.Add(cellpaisCleinte3)

        Dim cellrfcCleinte As New PdfPCell(New Paragraph(p3)) 'Celda para guardar el rfc del cliente
        Dim cellcodigoPostalCleinte As New PdfPCell(New Paragraph(p4)) 'Celda para guardar el codigo postal del cliente
        Dim cellpaisCleinte As New PdfPCell(New Paragraph(p5)) 'Celda para guardar el pais del cliente
        '---------------------------------------------------------------------
        'Propiedades de las celdas de la tabla firma--------------------------------------
        cellLineaVacia.Border = Rectangle.NO_BORDER
        cellLinea.Border = 2
        cellLinea.HorizontalAlignment = Rectangle.ALIGN_CENTER
        cellLineaVacia2.Border = Rectangle.NO_BORDER
        cellRecibiVacia.Border = Rectangle.NO_BORDER
        cellRecibi.Border = Rectangle.NO_BORDER
        cellRecibi.HorizontalAlignment = Rectangle.ALIGN_CENTER
        cellRecibiVacia2.Border = Rectangle.NO_BORDER
        '-------------------------------------------------------------------------------
        'Propiedades de las celdas de la tabla rfc--------------------------------------
        cellrfcCleinte.Border = Rectangle.NO_BORDER
        cellcodigoPostalCleinte.Border = Rectangle.NO_BORDER
        cellpaisCleinte.Border = Rectangle.NO_BORDER
        '-------------------------------------------------------------------------------
        'Propiedades de la tabla rfc-----------------------------------------
        tablaRFC.SetWidths(anchorfc)
        tablaRFC.AddCell(cellrfcCleinte)
        tablaRFC.AddCell(cellcodigoPostalCleinte)
        tablaRFC.AddCell(cellpaisCleinte)
        '-------------------------------------------------------------------------------
        'Propuedades de la tabla fimra'---------------------------------------------------
        tablafirma.SetWidths(anchoFirma)
        tablafirma.AddCell(cellLineaVacia)
        tablafirma.AddCell(cellLinea)
        tablafirma.AddCell(cellLineaVacia2)
        tablafirma.AddCell(cellRecibiVacia)
        tablafirma.AddCell(cellRecibi)
        tablafirma.AddCell(cellRecibiVacia)
        '-------------------------------------------------------------------------------
        'si el cliente trae truncar, hacer las primero las operaciones del subtotal e iva
        Dim precioUnitario
        Dim totalConcepto
        Dim subTotal
        Dim impuesto
        Dim retencion
        Dim iva As Double
        Dim retCte = cUtilerias.hayretencion
        iva = cUtilerias.impuesto

        Dim totalImpuestosTrasladados
        Dim totalImpuestosretenidos
        If cUtilerias.truncar = "si" Then
            'buscamos los detalles de la factura
            Dim conceptos As DataTable = cUtilerias.consultas("SELECT precio_unitario, cantidad, descuento, isnull(impuestoP, 0) as impuestoP FROM detFactura WHERE id_factura = " & idfactura, cUtilerias.cadenaConexion)

            'recorremos los resultados
            For i = 0 To conceptos.Rows.Count - 1
                precioUnitario = conceptos.Rows(i)("precio_unitario").ToString

                If conceptos.Rows(i)("impuestoP").ToString > 0 Then
                    precioUnitario = cUtilerias.truncarAseis(precioUnitario * ((100 + (conceptos.Rows(i)("impuestoP").ToString)) / 100))
                Else
                    precioUnitario = cUtilerias.truncarAseis(precioUnitario)
                End If

                If conceptos.Rows(i)("descuento").ToString > 0 Then
                    precioUnitario = cUtilerias.truncarAseis(precioUnitario * ((100 - (conceptos.Rows(i)("descuento").ToString)) / 100))
                Else
                    precioUnitario = cUtilerias.truncarAseis(precioUnitario)
                End If
                totalConcepto = cUtilerias.truncarAdos(precioUnitario * conceptos.Rows(i)("cantidad").ToString)
                subTotal += cUtilerias.truncarAdos(totalConcepto)
                'validamos si la factura tiene iva
                If iva <> 0 Then
                    impuesto = cUtilerias.truncarAdos(totalConcepto * (iva / 100))
                    totalImpuestosTrasladados += cUtilerias.truncarAdos(impuesto)
                End If
            Next
            subTotal = cUtilerias.truncarAdos(subTotal)
        Else
            'obtenemos la suma de los conceptos
            Dim conceptos As DataTable = cUtilerias.consultas("SELECT precio_unitario, cantidad, descuento, isnull(impuestoP, 0) as impuestoP FROM detFactura WHERE id_factura = " & idfactura, cUtilerias.cadenaConexion)
            'Console.Write("SELECT precio_unitario, cantidad FROM detFactura WHERE id_factura = " & factura.Rows(0)("idfactura").ToString)

            For i = 0 To conceptos.Rows.Count - 1
                precioUnitario = conceptos.Rows(i)("precio_unitario").ToString

                If conceptos.Rows(i)("impuestoP").ToString > 0 Then
                    precioUnitario = cUtilerias.truncarAseis(precioUnitario * ((100 + (conceptos.Rows(i)("impuestoP").ToString)) / 100))
                Else
                    precioUnitario = cUtilerias.truncarAseis(precioUnitario)
                End If

                If conceptos.Rows(i)("descuento").ToString > 0 Then
                    precioUnitario = cUtilerias.truncarAseis(precioUnitario * ((100 - (conceptos.Rows(i)("descuento").ToString)) / 100))
                Else
                    precioUnitario = cUtilerias.truncarAseis(precioUnitario)
                End If

                totalConcepto = cUtilerias.redondear(precioUnitario * conceptos.Rows(i)("cantidad").ToString)
                subTotal += cUtilerias.redondear(totalConcepto)

                'validamos si la factura tiene iva

                If iva <> 0 Then
                    'Console.Write(totalConcepto)
                    impuesto = cUtilerias.redondear(totalConcepto * ((iva) / 100))

                    totalImpuestosTrasladados += cUtilerias.redondear(impuesto)
                End If


                If retCte > 0 Then

                    'Console.Write(totalConcepto)
                    retencion = cUtilerias.redondear(totalConcepto * (0.16))

                    totalImpuestosretenidos += cUtilerias.redondear(retencion)
                Else
                    totalImpuestosretenidos = 0
                End If

            Next

            subTotal = cUtilerias.redondear(subTotal)
        End If

        'agregar la mano de obra




        'limpiamos las variables de los conceptos
        impuesto = 0
        totalConcepto = 0
        precioUnitario = 0

        Dim total
        
        Dim isr
        'obtenemos el total
        If cUtilerias.truncar = "si" Then
            total = cUtilerias.truncarAdos(totalImpuestosTrasladados + subTotal)
            'totalImpuestosTrasladados = 0
        Else
            total = cUtilerias.redondear(totalImpuestosTrasladados + subTotal)
            '  totalImpuestosTrasladados = 0
        End If

        'validamos si hay isr
        If cUtilerias.hayisr <> "" And cUtilerias.hayisr = "si" Then
            isr = (subTotal * 0.15)
            total = CDbl(subTotal) - isr

            If cUtilerias.truncar = "si" Then
                total = cUtilerias.truncarAdos(total)
            Else
                total = cUtilerias.redondear(total)
            End If
        End If

        '------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        'iva -------------------------------------------------------------------------------------------------------------------

        '-----------------------------------------------------------------------------------------------------------------------
        Dim retencionCte
        '  Console.Write(hayretencion)
        If retCte > 0 Then

            total = subTotal
        Else

            total = (total)
        End If
        Dim factApp, folioApp
        Dim totalAnt
        Dim fechaApp
        totalAnt = 0
        folioApp = "  "
        factApp = "  "
        Dim anticipo As DataTable = cUtilerias.consultas("SELECT * FROM anticipo WHERE idFacturaRelacionada = " & idfactura, cUtilerias.cadenaConexion)
        For i = 0 To anticipo.Rows.Count - 1
            totalAnt = anticipo.Rows(i)("montoAnt").ToString
            factApp = anticipo.Rows(i)("idFacturaAplicada").ToString
            fechaApp = anticipo.Rows(i)("fechaAlta").ToString 'FormatDateTime(cUtilerias.fechaVenta, DateFormat.ShortDate)
            Dim folioAnt As DataTable = cUtilerias.consultas("SELECT * FROM factura WHERE idfactura = " & factApp, cUtilerias.cadenaConexion)
            For j = 0 To folioAnt.Rows.Count - 1
                folioApp = folioAnt.Rows(i)("folio").ToString
            Next
        Next

        If totalAnt > 0 Then
            total = total - totalAnt
        End If

        'Celdas de la tabla pedimentos 
        Dim cellCodigoP As New PdfPCell(New Phrase("CODIGO", arial10B)) 'Celda para guardar el texto CANTIDAD
        Dim cellSatP As New PdfPCell(New Phrase("CODIGO SAT", arial10B)) 'Celda para guardar el texto NUMERO DE PARTE
        Dim cellNumP As New PdfPCell(New Phrase("NUMERO PEDIMENTO", arial10B)) 'Celda para guardar el texto DESCRIPCION

        'Propiedades de la tabla pedimentos 
        cellCodigoP.BackgroundColor = colorHeaders
        cellCodigoP.Border = Rectangle.NO_BORDER 'Mostrar bordes seleccionados
        cellCodigoP.HorizontalAlignment = Rectangle.ALIGN_CENTER 'Aliniar
        cellCodigoP.BorderColor = New BaseColor(0, 0, 0) 'Color del borde
        cellSatP.BackgroundColor = colorHeaders
        cellSatP.Border = Rectangle.NO_BORDER 'Mostrar bordes seleccionados
        cellSatP.HorizontalAlignment = Rectangle.ALIGN_CENTER 'Aliniar
        cellSatP.BorderColor = New BaseColor(0, 0, 0) 'Color del borde
        cellNumP.BackgroundColor = colorHeaders
        cellNumP.Border = Rectangle.NO_BORDER 'Mostrar bordes seleccionados
        cellNumP.HorizontalAlignment = Rectangle.ALIGN_CENTER 'Aliniar
        cellNumP.BorderColor = New BaseColor(0, 0, 0) 'Color del borde

        'celdasa de la tabla rfc----------------------------------------------
        Dim cellImporteLetra As New PdfPCell(New Phrase("IMPORTE CON LETRA", arial10B)) 'Celda para guardar el texto IMPORTE CON LETRA
        Dim cellSubtotal As New PdfPCell(New Phrase("SUB-TOTAL $", arial10B)) 'Celda para guardar el texto SUB-TOTAL

        Dim cellSubtotal2 As New PdfPCell(New Phrase(Format(subTotal, "##,##00.00"), stylo10))
        ' Dim cellSubtotal2 As New PdfPCell(New Phrase(Replace(Format(cUtilerias.subTotal, "currency"), "$", ""), stylo6)) 'Celda para guardar el subtotal
        '   Dim totalPro = cUtilerias.digi(ivaTotal + pSubTotal)
        ' total con letra
        ' Console.WriteLine(total)
        cUtilerias.centavos = 0
        Dim totalL
        totalL = Format(total, "##,##00.00").Replace(",", "")

        Dim celltotalLetra As New PdfPCell(New Phrase(UCase(cUtilerias.Letras(totalL)) & " " & cUtilerias.monedaTexto & " " & cUtilerias.centavos & "/100 " & cUtilerias.moneda, stylo10)) 'Celda para guardar la cantidad en letra
        Dim celliva As New PdfPCell(New Phrase("I.V.A." & cUtilerias.impuesto + "%  $", arial10B)) 'Celda para guardar el texto I.V.A


        Dim celliva2 As New PdfPCell(New Phrase(Format(totalImpuestosTrasladados, "##,##00.00"), stylo10)) 'Celda para guardar el iva
        Dim cellretencion As New PdfPCell(New Phrase("RETENCION  $", arial10B)) 'Celda para guardar el texto RETENCION
        Dim cellretencion2 As New PdfPCell(New Phrase(Format(totalImpuestosretenidos, "##,##00.00"), stylo10)) 'Celda para guardar la retencion

        Dim cellAnticipoV As New PdfPCell(New Phrase(" ", stylo10)) 'Celda para guardar el texto RETENCION
        Dim cellAnticipo As New PdfPCell(New Phrase("ANTICIPO  $", arial10B)) 'Celda para guardar el texto RETENCION
        Dim cellAnticipo2 As New PdfPCell(New Phrase(Format(Val(totalAnt), "##,##00.00"), stylo10)) 'Celda para guardar la retencion

        Dim cellFirma As New PdfPCell(tablafirma) ''Celda para guardar la linea de firma
        Dim cellTotal As New PdfPCell(New Phrase("TOTAL  $", arial10B)) 'Celda para guardar el texto TOTAL

        Dim cellTotal2 As New PdfPCell(New Phrase(Format(total, "##,##00.00"), stylo10)) 'Celda para guardar el total
        Dim cellvaciaTotales As New PdfPCell(New Phrase("", stylo12)) 'Celda vacia para dar espacio
        '---------------------------------------------------------------------
        'celdas de la tabla de articulos-------------------------------------------------
        Dim cellCantidad As New PdfPCell(New Phrase("CANT.", arial10B)) 'Celda para guardar el texto CANTIDAD
        Dim cellNumeroParte As New PdfPCell(New Phrase("CODIGO/SAT", arial10B)) 'Celda para guardar el texto NUMERO DE PARTE
        Dim cellDescripcion As New PdfPCell(New Phrase("DESCRIPCIÓN", arial10B)) 'Celda para guardar el texto DESCRIPCION
        Dim cellUnidad As New PdfPCell(New Phrase("UNIDAD/SAT", arial10B2)) 'Celda para guardar el texto UNIDAD DE MEDIDA
        Dim cellPed As New PdfPCell(New Phrase("PEDIMENTO", arial10B2)) 'Celda para guardar el texto UNIDAD DE MEDIDA
        Dim cellPrecio As New PdfPCell(New Phrase("P.UNITARIO  DESC. P. DESC.  IMP", arial10B2)) 'Celda para guardar el texto PRECIO UNITARIO
        Dim cellImporte As New PdfPCell(New Phrase("P.TOTAL", arial10B)) 'Celda para guardar el texto IMPORTE
        '--------------------------------------------------------------------------------------
        'celdas de la tabla de articulos-------------------------------------------------
        Dim cellCantidadVacia As New PdfPCell(New Phrase("")) 'Celda vacia para dar espacio
        Dim cellNumeroParteVacia As New PdfPCell(New Phrase("")) 'Celda vacia para dar espacio
        Dim cellDescripcionVacia As New PdfPCell(New Phrase("")) 'Celda vacia para dar espacio
        Dim cellUnidadVacia As New PdfPCell(New Phrase("")) 'Celda vacia para dar espacio
        Dim cellPedVacia As New PdfPCell(New Phrase("")) 'Celda vacia para dar espacio
        Dim cellPrecioVacia As New PdfPCell(New Phrase("")) 'Celda vacia para dar espacio
        Dim cellImporteVacia As New PdfPCell(New Phrase("")) 'Celda vacia para dar espacio
        Dim cellCantidadVacia2 As New PdfPCell(New Phrase("")) 'Celda vacia para dar espacio
        Dim cellNumeroParteVacia2 As New PdfPCell(New Phrase("")) 'Celda vacia para dar espacio
        Dim cellDescripcionVacia2 As New PdfPCell(New Phrase("")) 'Celda vacia para dar espacio
        Dim cellUnidadVacia2 As New PdfPCell(New Phrase("")) 'Celda vacia para dar espacio
        Dim cellPedVacia2 As New PdfPCell(New Phrase("")) 'Celda vacia para dar espacio
        Dim cellPrecioVacia2 As New PdfPCell(New Phrase("")) 'Celda vacia para dar espacio
        Dim cellImporteVacia2 As New PdfPCell(New Phrase("")) 'Celda vacia para dar espacio
        Dim cellCantidadVacia3 As New PdfPCell(New Phrase("")) 'Celda vacia para dar espacio
        Dim cellNumeroParteVacia3 As New PdfPCell(New Phrase("")) 'Celda vacia para dar espacio
        Dim cellDescripcionVacia3 As New PdfPCell(New Phrase("")) 'Celda vacia para dar espacio
        Dim cellUnidadVacia3 As New PdfPCell(New Phrase("")) 'Celda vacia para dar espacio
        Dim cellPedVacia3 As New PdfPCell(New Phrase("")) 'Celda vacia para dar espacio
        Dim cellPrecioVacia3 As New PdfPCell(New Phrase("")) 'Celda vacia para dar espacio
        Dim cellImporteVacia3 As New PdfPCell(New Phrase("")) 'Celda vacia para dar espacio
        Dim cellCancelar As New PdfPCell(New Phrase("CANCELADA", stylo32)) ' celda para cancelar 
        Dim cellCancelarSAT As New PdfPCell(New Phrase(" ", stylo4)) ' celda para cancelar 

        '--------------------------------------------------------------------------------------
        'Propuedades de la celdas de la tabla totales----------------------------------------
        cellImporteLetra.BackgroundColor = colorHeaders 'Color del borde
        cellImporteLetra.HorizontalAlignment = Rectangle.ALIGN_CENTER 'Aliniar
        cellImporteLetra.Border = Rectangle.NO_BORDER 'Mostrar bordes seleccionados
        cellImporteLetra.BorderColor = New BaseColor(0, 0, 0) 'Color del borde                
        cellSubtotal.BackgroundColor = colorHeaders 'Color del borde
        cellSubtotal.HorizontalAlignment = Rectangle.ALIGN_CENTER 'Aliniar
        cellSubtotal.Border = 1 'Mostrar bordes seleccionados
        cellSubtotal.BorderColor = colorHeaders 'Color del borde
        cellSubtotal2.BackgroundColor = New BaseColor(255, 255, 255) 'Color del borde
        cellSubtotal2.HorizontalAlignment = Rectangle.ALIGN_RIGHT 'Aliniar
        cellSubtotal2.Border = 11 'Mostrar bordes seleccionados
        cellSubtotal2.BorderColor = colorHeaders 'Color del borde
        celltotalLetra.BackgroundColor = New BaseColor(255, 255, 255) 'fondo de la tabla  
        celltotalLetra.Border = 7 'Mostrar bordes seleccionados
        celltotalLetra.BorderColor = colorHeaders 'Color del borde
        celltotalLetra.Rowspan = 2 'Abarcar dos renglones
        celliva.BackgroundColor = colorHeaders 'Color del borde
        celliva.HorizontalAlignment = Rectangle.ALIGN_CENTER 'Aliniar
        celliva.Border = Rectangle.NO_BORDER 'Mostrar bordes seleccionados
        celliva2.BackgroundColor = New BaseColor(255, 255, 255) 'fondo de la tabla  
        celliva2.HorizontalAlignment = Rectangle.ALIGN_RIGHT 'Aliminar
        celliva2.Border = 11 'Mostrar bordes seleccionados
        celliva2.BorderColor = colorHeaders 'Color del borde
        cellretencion.BackgroundColor = colorHeaders 'fondo de la tabla
        cellretencion.HorizontalAlignment = Rectangle.ALIGN_CENTER 'Aliniar
        cellretencion.Border = Rectangle.NO_BORDER 'Mostrar bordes seleccionados
        cellretencion2.BackgroundColor = New BaseColor(255, 255, 255) 'fondo de la tabla
        cellretencion2.HorizontalAlignment = Rectangle.ALIGN_RIGHT 'Aliniar
        cellretencion2.Border = 11 'Mostrar bordes seleccionados
        cellretencion2.BorderColor = colorHeaders 'Color del borde


        cellAnticipoV.BackgroundColor = New BaseColor(255, 255, 255) 'fondo de la tabla  
            cellAnticipoV.Border = Rectangle.NO_BORDER 'Mostrar bordes seleccionados
        cellAnticipo.BackgroundColor = colorHeaders 'fondo de la tabla
        cellAnticipo.HorizontalAlignment = Rectangle.ALIGN_CENTER 'Aliniar
            cellAnticipo.Border = Rectangle.NO_BORDER 'Mostrar bordes seleccionados
            cellAnticipo2.BackgroundColor = New BaseColor(255, 255, 255) 'fondo de la tabla
            cellAnticipo2.HorizontalAlignment = Rectangle.ALIGN_RIGHT 'Aliniar
            cellAnticipo2.Border = 11 'Mostrar bordes seleccionados
            cellAnticipo2.BorderColor = New BaseColor(0, 0, 0) 'Color del borde

       
        cellFirma.HorizontalAlignment = Rectangle.ALIGN_RIGHT 'Aliniar
        cellFirma.Border = Rectangle.NO_BORDER 'Mostrar los bordes seleccionados
        cellTotal.BackgroundColor = colorHeaders 'fondo de la tabla
        cellTotal.HorizontalAlignment = Rectangle.ALIGN_CENTER 'Aliniar
        cellTotal.BorderColor = New BaseColor(0, 0, 0) 'Color del borde
        cellTotal.Border = Rectangle.NO_BORDER 'Mostrar los bordes seleccionados
        cellTotal2.BackgroundColor = New BaseColor(255, 255, 255) 'fondo de la tabla
        cellTotal2.HorizontalAlignment = Rectangle.ALIGN_RIGHT 'Aliniar
        cellTotal2.Border = 11 'Mostrar bordes seleccionados
        cellTotal2.BorderColor = colorHeaders 'Color del borde
        cellvaciaTotales.Colspan = 3 'Unir celdas
        cellvaciaTotales.Border = Rectangle.NO_BORDER 'Mostrar los bordes seleccionados
        '------------------------------------------------------------------------------------
        'Propiedades de las celdas de la tabla de articulos------------------------------------
        cellCantidad.BackgroundColor = colorHeaders 'Color del borde
        cellCantidad.Border = Rectangle.NO_BORDER 'Mostrar bordes seleccionados
        cellCantidad.HorizontalAlignment = Rectangle.ALIGN_CENTER 'Aliniar
        cellCantidad.BorderColor = New BaseColor(0, 0, 0) 'Color del borde
        cellNumeroParte.BackgroundColor = colorHeaders 'Color del borde
        cellNumeroParte.Border = Rectangle.NO_BORDER 'Mostrar bordes seleccionados
        cellNumeroParte.HorizontalAlignment = Rectangle.ALIGN_CENTER 'Aliniar
        cellNumeroParte.BorderColor = New BaseColor(0, 0, 0) 'Color del borde
        cellDescripcion.BackgroundColor = colorHeaders 'Color del borde
        cellDescripcion.Border = Rectangle.NO_BORDER 'Mostrar bordes seleccionados
        cellDescripcion.HorizontalAlignment = Rectangle.ALIGN_CENTER 'Aliniar
        cellDescripcion.BorderColor = New BaseColor(0, 0, 0) 'Color del borde
        cellUnidad.BackgroundColor = colorHeaders 'Color del borde
        cellUnidad.Border = Rectangle.NO_BORDER 'Mostrar bordes seleccionados
        cellUnidad.HorizontalAlignment = Rectangle.ALIGN_CENTER 'Aliniar
        cellUnidad.BorderColor = New BaseColor(0, 0, 0) 'Color del borde   

        cellPed.BackgroundColor = colorHeaders 'Color del borde
        cellPed.Border = Rectangle.NO_BORDER 'Mostrar bordes seleccionados
        cellPed.HorizontalAlignment = Rectangle.ALIGN_CENTER 'Aliniar
        cellPed.BorderColor = New BaseColor(0, 0, 0) 'Color del borde        

        cellPrecio.BackgroundColor = colorHeaders 'Color del borde
        cellPrecio.Border = Rectangle.NO_BORDER 'Mostrar bordes seleccionados
        cellPrecio.HorizontalAlignment = Rectangle.ALIGN_RIGHT 'Aliniar
        cellPrecio.BorderColor = New BaseColor(0, 0, 0) 'Color del borde
        cellImporte.BackgroundColor = colorHeaders 'Color del borde
        cellImporte.Border = Rectangle.NO_BORDER 'Mostrar bordes seleccionados
        cellImporte.HorizontalAlignment = Rectangle.ALIGN_RIGHT 'Aliniar
        cellImporte.BorderColor = New BaseColor(0, 0, 0) 'Color del borde
        '-------------------------------------------------------------------------------------------
        'Propiedades de las celdas de la tabla articulos        
        cellCantidadVacia.Border = 14
        cellCantidadVacia.BorderColor = New BaseColor(0, 0, 0) 'Color del borde
        cellCantidadVacia.FixedHeight = 100.0F
        cellNumeroParteVacia.Border = 14
        cellNumeroParteVacia.BorderColor = New BaseColor(0, 0, 0) 'Color del borde
        cellDescripcionVacia.Border = 14
        cellDescripcionVacia.BorderColor = New BaseColor(0, 0, 0) 'Color del borde
        cellUnidadVacia.Border = 14
        cellUnidadVacia.BorderColor = New BaseColor(0, 0, 0) 'Color del borde

        cellPedVacia.Border = 14
        cellPedVacia.BorderColor = New BaseColor(0, 0, 0) 'Color del borde

        cellPrecioVacia.Border = 14
        cellPrecioVacia.BorderColor = New BaseColor(0, 0, 0) 'Color del borde
        cellImporteVacia.Border = 14
        cellImporteVacia.BorderColor = New BaseColor(0, 0, 0) 'Color del borde
        cellCantidadVacia2.Border = Rectangle.NO_BORDER
        cellNumeroParteVacia2.Border = Rectangle.NO_BORDER
        cellDescripcionVacia2.Border = Rectangle.NO_BORDER
        cellUnidadVacia2.Border = Rectangle.NO_BORDER
        cellPedVacia2.Border = Rectangle.NO_BORDER
        cellPrecioVacia2.Border = Rectangle.NO_BORDER
        cellImporteVacia2.Border = Rectangle.NO_BORDER
        cellCantidadVacia3.Border = 14
        cellCantidadVacia3.BorderColor = New BaseColor(0, 0, 0) 'Color del borde
        cellNumeroParteVacia3.Border = 14
        cellNumeroParteVacia3.BorderColor = New BaseColor(0, 0, 0) 'Color del borde
        cellDescripcionVacia3.Border = 14
        cellDescripcionVacia3.BorderColor = New BaseColor(0, 0, 0) 'Color del borde
        cellUnidadVacia3.Border = 14
        cellUnidadVacia3.BorderColor = New BaseColor(0, 0, 0) 'Color del borde
        cellPedVacia3.Border = 14
        cellPedVacia3.BorderColor = New BaseColor(0, 0, 0) 'Color del borde
        cellPrecioVacia3.Border = 14
        cellPrecioVacia3.BorderColor = New BaseColor(0, 0, 0) 'Color del borde
        cellImporteVacia3.Border = 14
        cellImporteVacia3.BorderColor = New BaseColor(0, 0, 0) 'Color del borde
        cellCantidadVacia3.Border = Rectangle.TOP_BORDER
        cellNumeroParteVacia3.Border = Rectangle.TOP_BORDER
        cellDescripcionVacia3.Border = Rectangle.TOP_BORDER
        cellUnidadVacia3.Border = Rectangle.TOP_BORDER
        cellPedVacia3.Border = Rectangle.TOP_BORDER
        cellPrecioVacia3.Border = Rectangle.TOP_BORDER
        cellImporteVacia3.Border = Rectangle.TOP_BORDER
        cellCancelar.Border = 14
        cellCancelar.BorderColor = New BaseColor(0, 0, 0) 'Color del borde
        cellCancelar.HorizontalAlignment = Rectangle.ALIGN_CENTER 'Aliniar
        cellCancelarSAT.Border = 14
        cellCancelarSAT.BorderColor = New BaseColor(0, 0, 0) 'Color del borde
        cellCancelarSAT.HorizontalAlignment = Rectangle.ALIGN_CENTER 'Aliniar

        cellCancelar.Border = Rectangle.NO_BORDER
        cellCancelarSAT.Border = Rectangle.NO_BORDER

        '------------------------------------------------------------------------------------
        'Propiedades de la tabla de articulos---------------------------------------------------
        tablaArticulos.SetWidths(anchoArticulos) 'Ajusta el tamaño de cada columna        
        tablaArticulos.AddCell(cellCantidad) 'Agrega COLUMNA
        tablaArticulos.AddCell(cellNumeroParte) 'Agrega COLUMNA
        tablaArticulos.AddCell(cellDescripcion) 'Agrega COLUMNA
        tablaArticulos.AddCell(cellUnidad) 'Agrega COLUMNA
        tablaArticulos.AddCell(cellPed) 'Agrega COLUMNA
        tablaArticulos.AddCell(cellPrecio) 'Agrega COLUMNA
        tablaArticulos.AddCell(cellImporte) 'Agrega COLUMNA
        '-------------------------------------------------------------------------------------
        'Propiedades a tabla pedimento 
        tablaPedimento.AddCell(cellCodigoP)
        tablaPedimento.AddCell(cellSatP)
        tablaPedimento.AddCell(cellNumP)
        'Consultar Articulos----------------------------------------------------------------
        Dim qry As String
        qry = ""

        Dim ingPed As String
        ingPed = ""
        Dim numPedimento As String
        numPedimento = ""


        'validar si tiene pedimento
        'Dim valPed As DataTable = cUtilerias.consultas("SELECT * FROM dbo.detallesPed WHERE nventa = '" & cUtilerias.asn & "'", cUtilerias.cadenaConexion)
        Dim numPed As DataTable = cUtilerias.consultas("SELECT * FROM dbo.detFactura WHERE (numPedimento <> '' AND numPedimento is not null) AND id_factura = " & idfactura, cUtilerias.cadenaConexion)

        'If valPed.Rows.Count <> 0 Then
        'ingPed = "1"
        'qry = "SELECT d.id_detFactura, d.descripcion, d.precio_unitario, d.total, d.descuento, d.impuesto, d.id_factura, d.iva, d.unidad, d.no_identificacion, d.aduana_numero, d.aduana_fecha, d.aduana, "
        'qry += "d.predial_no, d.nparte, d.retencion, d.tretencion, d.valorAgregado, d.claveProdServ, d.claveUnidad, det.cantidad, det.idpedimento, d.impuestoP FROM dbo.detFactura d INNER JOIN claves c ON d.nparte = c.clave "
        'qry += "INNER JOIN detallesPed det ON c.idarticulo = det.idarticulo WHERE d.id_factura = " & idfactura & " And det.nventa = '" & cUtilerias.asn & "'"
        'Else
        qry = "SELECT * FROM dbo.detFactura WHERE id_factura = " & idfactura
        'End If

        Dim colorA As BaseColor = ColorGris
        'numero de pedimento
        If numPed.Rows.Count > 0 Then

            numPedimento = "1"
            Dim nParte As String = ""
            For x As Integer = 0 To numPed.Rows.Count - 1
                If colorA IsNot ColorGris Then
                    colorA = ColorGris
                Else
                    colorA = ColorBco
                End If

                If numPed.Rows(x)("nparte").ToString <> "0" Then 'nparte
                    nParte = numPed.Rows(x)("nparte").ToString
                End If 'nparte

                Dim cellCodigoP2 As New PdfPCell(New Phrase(nParte, stylo8))
                Dim cellSatP2 As New PdfPCell(New Phrase(numPed.Rows(x)("claveProdServ").ToString, stylo8))
                Dim cellNumP2 As New PdfPCell(New Phrase(numPed.Rows(x)("numPedimento").ToString, stylo8))

                cellCodigoP2.HorizontalAlignment = Rectangle.ALIGN_CENTER
                cellCodigoP2.Border = 12
                cellCodigoP2.BorderColor = New BaseColor(0, 0, 0) 'Color del borde
                cellCodigoP2.BackgroundColor = colorA 'Color del borde
                cellSatP2.HorizontalAlignment = Rectangle.ALIGN_CENTER
                cellSatP2.Border = 12
                cellSatP2.BorderColor = New BaseColor(0, 0, 0) 'Color del borde
                cellSatP2.BackgroundColor = colorA 'Color del borde
                cellNumP2.HorizontalAlignment = Rectangle.ALIGN_CENTER
                cellNumP2.Border = 12
                cellNumP2.BorderColor = New BaseColor(0, 0, 0) 'Color del borde
                cellNumP2.BackgroundColor = colorA 'Color del borde

                'Agregar valores a la tabla
                tablaPedimento.AddCell(cellCodigoP2)
                tablaPedimento.AddCell(cellSatP2)
                tablaPedimento.AddCell(cellNumP2)
            Next
        End If

        'relacionados agrewgado 31/10/2020
        Dim documento As DataTable = cUtilerias.consultas("SELECT documento.descripcion FROM factura JOIN documento ON documento.iddocumento = factura.tipo_comprobante WHERE factura.idfactura = " & idfactura, cUtilerias.cadenaConexion)
        Dim relacion As DataTable = cUtilerias.consultas("SELECT folioR, UUIDR FROM relacionados WHERE idfacturaI = " & idfactura, cUtilerias.cadenaConexion)
        tipoDocumento = documento.Rows(0)("descripcion").ToString
        'totalRelacion = relacion.Rows.Count

        Dim articulos As DataTable = cUtilerias.consultas(qry, cUtilerias.cadenaConexion)
        Dim factu As DataTable = cUtilerias.consultas("SELECT * FROM dbo.factura WHERE idfactura = " & idfactura, cUtilerias.cadenaConexion)
        'Ciclo importantisimoooo en el codigooo!!!!!! ia que es el que hace el recorrido en todoss los campooss

        'Agregar articulos a latabla-------------------------------------------------------------
        Dim iddetalles As String = 0

        Dim colorB As BaseColor = ColorGris
        For i As Integer = 0 To articulos.Rows.Count - 1 'For
            If colorB IsNot ColorGris Then
                colorB = ColorGris
            Else
                colorB = ColorBco
            End If

            Dim cantidad As String = 0
            Dim nparte As String = 0
            Dim unidad As String = 0
            Dim des As String = 0
            Dim imp As String = 0
            Dim ped As String = 0
            Dim precio_unitario
            'if para validar que tiene cantidad
            If articulos.Rows(i)("cantidad").ToString <> 0 Then 'cantidad
                cantidad = articulos.Rows(i)("cantidad").ToString
            End If 'cantidad

            'if para validar que tiene numero de partes
            If articulos.Rows(i)("nparte").ToString <> "0" Then 'nparte
                nparte = articulos.Rows(i)("nparte").ToString
            End If 'nparte

            If cUtilerias.asn <> "" And ingPed <> "" Then
                Dim pedimento As DataTable = cUtilerias.consultas("SELECT pedimento FROM dbo.pedimento WHERE id = '" & articulos.Rows(i)("idpedimento").ToString & "'", cUtilerias.cadenaConexion)

                If pedimento.Rows.Count <> 0 Then
                    ped = pedimento.Rows(0)("pedimento").ToString
                End If

            End If

            'If cUtilerias.asn <> "" Then
            'Dim pedimento As DataTable = cUtilerias.consultas("SELECT DISTINCT pedimento FROM dbo.pedimento p INNER JOIN detallesPed d ON d.idPedimento = p.id INNER JOIN claves c ON c.idarticulo = d.idarticulo INNER JOIN detfactura dF ON dF.nparte = c.clave WHERE dF.nparte = '" & articulos.Rows(i)("nparte").ToString & "' AND d.nVenta = '" & cUtilerias.asn & "'", cUtilerias.cadenaConexion)

            'If pedimento.Rows.Count <> 0 Then
            'ped = pedimento.Rows(i)("pedimento").ToString
            'End If

            'End If

            'if para validar que tiene unidad
            If articulos.Rows(i)("unidad").ToString <> "0" Then 'unidad
                unidad = articulos.Rows(i)("unidad").ToString
            End If 'unidad

            'if para validar que tiene precio_unitario
            If articulos.Rows(i)("precio_unitario").ToString <> 0 Then 'precio_unitario
                precioUnitario = (articulos.Rows(i)("precio_unitario").ToString)
            End If 'precio_unitario

            'if para validar si tiene descuento
            If articulos.Rows(i)("descuento").ToString <> 0 Then
                des = (articulos.Rows(i)("descuento").ToString)
            Else
                des = 0
            End If

            'if para validar si tiene descuento
            If articulos.Rows(i)("impuestoP").ToString <> 0 And articulos.Rows(i)("impuestoP").ToString <> "" Then
                imp = (articulos.Rows(i)("impuestoP").ToString)
            Else
                imp = 0
            End If

            'if para validar que tiene total
            ' If articulos.Rows(i)("total").ToString <> 0 Then 'total
            'total = Replace(Format(articulos.Rows(i)("total").ToString, "currency"), "$", "")
            'End If 'total

            'para el precio Unitario
            precio_unitario = precioUnitario
            'para el precio Unitario
            If cUtilerias.truncar = "si" Then
                If imp > 0 Then
                    precio_unitario = cUtilerias.truncarAseis(precio_unitario * ((100 + (imp)) / 100))
                Else
                    precio_unitario = cUtilerias.truncarAseis(precio_unitario)
                End If

                If des > 0 Then
                    precio_unitario = cUtilerias.truncarAseis(precio_unitario * ((100 - (des)) / 100))
                Else
                    precio_unitario = cUtilerias.truncarAseis(precio_unitario)
                End If

                totalConcepto = cUtilerias.truncarAdos(precio_unitario * cantidad)
            Else
                If imp > 0 Then
                    precio_unitario = cUtilerias.truncarAseis(precio_unitario * ((100 + (imp)) / 100))
                Else
                    precio_unitario = cUtilerias.truncarAseis(precio_unitario)
                End If

                If des > 0 Then
                    precio_unitario = cUtilerias.truncarAseis(precio_unitario * ((100 - (des)) / 100))
                Else
                    precio_unitario = cUtilerias.truncarAseis(precio_unitario)
                End If

                totalConcepto = cUtilerias.redondear(precio_unitario * cantidad)
            End If

            'Console.WriteLine(precioUnitario + "desc")
            If i = 29 Then

                Dim cellCantidad4 As New PdfPCell(New Phrase(" ", stylo8)) 'Celda para guardar el texto METODO DE PAGO 
                Dim cellNumeroParte4 As New PdfPCell(New Phrase(" ", stylo8)) 'Celda para guardar el METODO DE PAGO 
                Dim cellDescripcion4 As New PdfPCell(New Phrase(" ", stylo8)) 'Celda para guardar el texto CONDICIONES DE PAGO 
                Dim cellUnidad4 As New PdfPCell(New Phrase(" ", stylo8)) 'Celda para guardar las CONDICIONES DE PAGO 
                Dim cellPed4 As New PdfPCell(New Phrase(" ", stylo8)) 'Celda para guardar las CONDICIONES DE PAGO 
                Dim cellPrecio4 As New PdfPCell(New Phrase(" ", stylo8)) 'Celda para guardar las CONDICIONES DE PAGO 
                Dim cellImporte4 As New PdfPCell(New Phrase(" ", stylo8)) 'Celda para guardar las CONDICIONES DE PAGO 

                ' Propiedades de las celdas de la tabla articulos
                cellCantidad4.HorizontalAlignment = Rectangle.ALIGN_CENTER
                cellCantidad4.Border = 12
                cellCantidad4.BorderColor = New BaseColor(0, 0, 0) 'Color del borde
                cellNumeroParte4.HorizontalAlignment = Rectangle.ALIGN_CENTER
                cellNumeroParte4.Border = 12
                cellNumeroParte4.BorderColor = New BaseColor(0, 0, 0) 'Color del borde
                cellDescripcion4.Border = 12
                cellDescripcion4.BorderColor = New BaseColor(0, 0, 0) 'Color del borde
                cellUnidad4.HorizontalAlignment = Rectangle.ALIGN_CENTER
                cellUnidad4.Border = 12
                cellUnidad4.BorderColor = New BaseColor(0, 0, 0) 'Color del borde

                cellPed4.HorizontalAlignment = Rectangle.ALIGN_CENTER
                cellPed4.Border = 12
                cellPed4.BorderColor = New BaseColor(0, 0, 0) 'Color del borde

                cellPrecio4.HorizontalAlignment = Rectangle.ALIGN_RIGHT
                cellPrecio4.Border = 12
                cellPrecio4.BorderColor = New BaseColor(0, 0, 0) 'Color del borde
                cellImporte4.HorizontalAlignment = Rectangle.ALIGN_RIGHT
                cellImporte4.Border = 12
                cellImporte4.BorderColor = New BaseColor(0, 0, 0) 'Color del borde

                tablaArticulos.AddCell(cellCantidad4) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellNumeroParte4) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellDescripcion4) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellUnidad4) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellPed4) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellPrecio4) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellImporte4) 'Agrega COLUMNA

                tablaArticulos.AddCell(cellCantidad4) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellNumeroParte4) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellDescripcion4) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellUnidad4) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellPed4) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellPrecio4) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellImporte4) 'Agrega COLUMNA

                tablaArticulos.AddCell(cellCantidadVacia) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellNumeroParteVacia) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellDescripcionVacia) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellUnidadVacia) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellPedVacia) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellPrecioVacia) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellImporteVacia) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellCantidadVacia2) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellNumeroParteVacia2) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellDescripcionVacia2) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellUnidadVacia2) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellPedVacia2) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellPrecioVacia2) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellImporteVacia2) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellCantidadVacia3) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellNumeroParteVacia3) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellDescripcionVacia3) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellUnidadVacia3) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellPedVacia3) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellPrecioVacia3) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellImporteVacia3) 'Agrega COLUMNA

                tablaArticulos.AddCell(cellCantidad4) 'Agrega COLUMNA
                    tablaArticulos.AddCell(cellNumeroParte4) 'Agrega COLUMNA
                    tablaArticulos.AddCell(cellDescripcion4) 'Agrega COLUMNA
                    tablaArticulos.AddCell(cellUnidad4) 'Agrega COLUMNA
                    tablaArticulos.AddCell(cellPed4) 'Agrega COLUMNA
                    tablaArticulos.AddCell(cellPrecio4) 'Agrega COLUMNA
                    tablaArticulos.AddCell(cellImporte4) 'Agrega COLUMNA

            End If

            'celdas de la tabla de articulos-------------------------------------------------
            Dim cellCantidad2 As New PdfPCell(New Phrase(cantidad, stylo8)) 'Celda para guardar el texto METODO DE PAGO 
            Dim cellNumeroParte2 As New PdfPCell(New Phrase(nparte & " / " & articulos.Rows(i)("claveProdServ").ToString, stylo8)) 'Celda para guardar el METODO DE PAGO 
            Dim cellDescripcion2 As New PdfPCell(New Phrase(articulos.Rows(i)("descripcion").ToString, stylo8)) 'Celda para guardar el texto CONDICIONES DE PAGO 
            Dim cellUnidad2 As New PdfPCell(New Phrase(unidad & " / " & articulos.Rows(i)("claveUnidad").ToString, stylo8)) 'Celda para guardar las CONDICIONES DE PAGO 
            Dim cellPed2 As New PdfPCell(New Phrase(ped, stylo8)) 'Celda para guardar las CONDICIONES DE PAGO 
            Dim cellPrecio2 As New PdfPCell(New Phrase("$ " & Format(Val(precioUnitario), "##,##00.00") & "     " & des & "%    $ " & Format(Val(precio_unitario), "##,##00.00") & "     " & imp & "%", stylo8)) 'Celda para guardar las CONDICIONES DE PAGO 
            Dim cellImporte2 As New PdfPCell(New Phrase("$ " & Format(totalConcepto, "##,##00.00"), stylo8)) 'Celda para guardar las CONDICIONES DE PAGO  Format(Text1.Text, "##,##00.00")
            '-----------------------------------------------------------------------------------

            'Propiedades de las celdas de la tabla articulos
            cellCantidad2.HorizontalAlignment = Rectangle.ALIGN_CENTER
            cellCantidad2.Border = 12
            cellCantidad2.BorderColor = New BaseColor(0, 0, 0) 'Color del borde
            cellCantidad2.BackgroundColor = colorB 'Color del borde

            cellNumeroParte2.HorizontalAlignment = Rectangle.ALIGN_CENTER
            cellNumeroParte2.Border = 12
            cellNumeroParte2.BorderColor = New BaseColor(0, 0, 0) 'Color del borde
            cellNumeroParte2.BackgroundColor = colorB 'Color del borde

            cellDescripcion2.Border = 12
            cellDescripcion2.BorderColor = New BaseColor(0, 0, 0) 'Color del borde
            cellDescripcion2.BackgroundColor = colorB 'Color del borde

            cellUnidad2.HorizontalAlignment = Rectangle.ALIGN_CENTER
            cellUnidad2.Border = 12
            cellUnidad2.BorderColor = New BaseColor(0, 0, 0) 'Color del borde
            cellUnidad2.BackgroundColor = colorB 'Color del borde

            cellPed2.HorizontalAlignment = Rectangle.ALIGN_CENTER
            cellPed2.Border = 12
            cellPed2.BorderColor = New BaseColor(0, 0, 0) 'Color del borde
            cellPed2.BackgroundColor = colorB 'Color del borde

            cellPrecio2.HorizontalAlignment = Rectangle.ALIGN_RIGHT
            cellPrecio2.Border = 12
            cellPrecio2.BorderColor = New BaseColor(0, 0, 0) 'Color del borde
            cellPrecio2.BackgroundColor = colorB 'Color del borde

            cellImporte2.HorizontalAlignment = Rectangle.ALIGN_RIGHT
            cellImporte2.Border = 12
            cellImporte2.BorderColor = New BaseColor(0, 0, 0) 'Color del borde
            cellImporte2.BackgroundColor = colorB 'Color del borde
            '------------------------------------------------------------------------------------
            'Inserta los valores en la factura---------------------------------------------------                   
            tablaArticulos.AddCell(cellCantidad2) 'Agrega COLUMNA
            tablaArticulos.AddCell(cellNumeroParte2) 'Agrega COLUMNA
            tablaArticulos.AddCell(cellDescripcion2) 'Agrega COLUMNA
            tablaArticulos.AddCell(cellUnidad2) 'Agrega COLUMNA
            tablaArticulos.AddCell(cellPed2) 'Agrega COLUMNA
            tablaArticulos.AddCell(cellPrecio2) 'Agrega COLUMNA
            tablaArticulos.AddCell(cellImporte2) 'Agrega COLUMNA

            iddetalles = iddetalles & " " & articulos.Rows(i)("id_detFactura").ToString
            '-------------------------------------------------------------------------------------
        Next 'For



        'agregar los articulos que no aparecen 
        Dim articulosSn As DataTable = cUtilerias.consultas("SELECT * FROM detFactura WHERE id_detFactura NOT IN (" & Replace(Trim(iddetalles), " ", ", ") & ") AND id_factura =" & idfactura, cUtilerias.cadenaConexion)
        'Ciclo importantisimoooo en el codigooo!!!!!! ia que es el que hace el recorrido en todoss los campooss

        'Agregar articulos a la tabla-------------------------------------------------------------


        For i As Integer = 0 To articulosSn.Rows.Count - 1 'For
            If colorB IsNot ColorGris Then
                colorB = ColorGris
            Else
                colorB = ColorBco
            End If

            Dim cantidad As String = 0
            Dim nparte As String = 0
            Dim unidad As String = 0
            Dim des As String = 0
            Dim imp As String = 0
            Dim ped As String = 0
            Dim precio_unitario
            'if para validar que tiene precio_unitario
            If articulosSn.Rows(i)("precio_unitario").ToString <> 0 Then 'precio_unitario
                precioUnitario = (articulosSn.Rows(i)("precio_unitario").ToString)
            End If 'precio_unitario

            'if para validar si tiene descuento
            If articulosSn.Rows(i)("descuento").ToString <> 0 Then
                des = (articulosSn.Rows(i)("descuento").ToString)
            Else
                des = 0
            End If

            'if para validar si tiene descuento
            If articulosSn.Rows(i)("impuestoP").ToString <> 0 And articulosSn.Rows(i)("impuestoP").ToString <> "" Then
                imp = (articulosSn.Rows(i)("impuestoP").ToString)
            Else
                imp = 0
            End If

            'if para validar que tiene total
            ' If articulos.Rows(i)("total").ToString <> 0 Then 'total
            'total = Replace(Format(articulos.Rows(i)("total").ToString, "currency"), "$", "")
            'End If 'total

            'para el precio Unitario
            precio_unitario = precioUnitario
            'para el precio Unitario
            If cUtilerias.truncar = "si" Then
                If imp > 0 Then
                    precio_unitario = cUtilerias.truncarAseis(precio_unitario * ((100 + (imp)) / 100))
                Else
                    precio_unitario = cUtilerias.truncarAseis(precio_unitario)
                End If

                If des > 0 Then
                    precio_unitario = cUtilerias.truncarAseis(precio_unitario * ((100 - (des)) / 100))
                Else
                    precio_unitario = cUtilerias.truncarAseis(precio_unitario)
                End If

                totalConcepto = cUtilerias.truncarAdos(precio_unitario * cantidad)
            Else
                If imp > 0 Then
                    precio_unitario = cUtilerias.truncarAseis(precio_unitario * ((100 + (imp)) / 100))
                Else
                    precio_unitario = cUtilerias.truncarAseis(precio_unitario)
                End If

                If des > 0 Then
                    precio_unitario = cUtilerias.truncarAseis(precio_unitario * ((100 - (des)) / 100))
                Else
                    precio_unitario = cUtilerias.truncarAseis(precio_unitario)
                End If

                totalConcepto = cUtilerias.redondear(precio_unitario * articulosSn.Rows(i)("cantidad").ToString)
            End If
            Dim cellCantidad2 As New PdfPCell(New Phrase(articulosSn.Rows(i)("cantidad").ToString, stylo8)) 'Celda para guardar el texto METODO DE PAGO 
            Dim cellNumeroParte2 As New PdfPCell(New Phrase(" / " & articulosSn.Rows(i)("claveProdServ").ToString, stylo8)) 'Celda para guardar el METODO DE PAGO 
            Dim cellDescripcion2 As New PdfPCell(New Phrase(articulosSn.Rows(i)("descripcion").ToString, stylo8)) 'Celda para guardar el texto CONDICIONES DE PAGO 
            Dim cellUnidad2 As New PdfPCell(New Phrase(articulosSn.Rows(i)("unidad").ToString & " / " & articulosSn.Rows(i)("claveUnidad").ToString, stylo8)) 'Celda para guardar las CONDICIONES DE PAGO 
            Dim cellPed2 As New PdfPCell(New Phrase(" ", stylo8)) 'Celda para guardar las CONDICIONES DE PAGO 
            Dim cellPrecio2 As New PdfPCell(New Phrase("$ " & Format(Val(precioUnitario), "##,##00.00") & "     " & des & "%    $ " & Format(Val(precio_unitario), "##,##00.00") & "     " & imp & "%", stylo8)) 'Celda para guardar las CONDICIONES DE PAGO 
            Dim cellImporte2 As New PdfPCell(New Phrase("$ " & Format(totalConcepto, "##,##00.00"), stylo8)) 'Celda para guardar las CONDICIONES DE PAGO  Format(Text1.Text, "##,##00.00")
            '-----------------------------------------------------------------------------------

            'Propiedades de las celdas de la tabla articulos
            cellCantidad2.HorizontalAlignment = Rectangle.ALIGN_CENTER
            cellCantidad2.Border = 12
            cellCantidad2.BorderColor = New BaseColor(0, 0, 0) 'Color del borde
            cellCantidad2.BorderColor = New BaseColor(0, 0, 0) 'Color del borde

            cellNumeroParte2.HorizontalAlignment = Rectangle.ALIGN_CENTER
            cellNumeroParte2.Border = 12
            cellNumeroParte2.BorderColor = New BaseColor(0, 0, 0) 'Color del borde
            cellDescripcion2.Border = 12
            cellDescripcion2.BorderColor = New BaseColor(0, 0, 0) 'Color del borde
            cellUnidad2.HorizontalAlignment = Rectangle.ALIGN_CENTER
            cellUnidad2.Border = 12
            cellUnidad2.BorderColor = New BaseColor(0, 0, 0) 'Color del borde
            cellPed2.HorizontalAlignment = Rectangle.ALIGN_CENTER
            cellPed2.Border = 12
            cellPed2.BorderColor = New BaseColor(0, 0, 0) 'Color del borde
            cellPrecio2.HorizontalAlignment = Rectangle.ALIGN_RIGHT
            cellPrecio2.Border = 12
            cellPrecio2.BorderColor = New BaseColor(0, 0, 0) 'Color del borde
            cellImporte2.HorizontalAlignment = Rectangle.ALIGN_RIGHT
            cellImporte2.Border = 12
            cellImporte2.BorderColor = New BaseColor(0, 0, 0) 'Color del borde
            '------------------------------------------------------------------------------------
            'Inserta los valores en la factura---------------------------------------------------                   
            tablaArticulos.AddCell(cellCantidad2) 'Agrega COLUMNA
            tablaArticulos.AddCell(cellNumeroParte2) 'Agrega COLUMNA
            tablaArticulos.AddCell(cellDescripcion2) 'Agrega COLUMNA
            tablaArticulos.AddCell(cellUnidad2) 'Agrega COLUMNA
            tablaArticulos.AddCell(cellPed2) 'Agrega COLUMNA
            tablaArticulos.AddCell(cellPrecio2) 'Agrega COLUMNA
            tablaArticulos.AddCell(cellImporte2) 'Agrega COLUMNA
        Next


        If tipoDocumento = "Nota de Credito" Then
            If relacion.Rows.Count > 0 Then
                Dim colDocR = New PdfPCell(New Phrase("CFDIS RELACIONADOS", stylo8))
                colDocR.HorizontalAlignment = Rectangle.ALIGN_CENTER
                colDocR.Colspan = 3
                tablaRelacion.AddCell(colDocR)

                'celdas para la tabla relacion 
                Dim cellFolioR As New PdfPCell(New Phrase("FOLIO", arial10B)) 'Celda para guardar el folio de la factura relacionada
                Dim cellUUIDR As New PdfPCell(New Phrase("ID DOCUMENTO", arial10B)) 'celda para guardar el uuid de la factura relacionada
                Dim cellTipoR As New PdfPCell(New Phrase("TIPO RELACION", arial10B)) 'celda para guardar el uuid de la factura relacionada

                cellFolioR.HorizontalAlignment = Rectangle.ALIGN_CENTER
                cellUUIDR.HorizontalAlignment = Rectangle.ALIGN_CENTER
                cellTipoR.HorizontalAlignment = Rectangle.ALIGN_CENTER

                cellFolioR.BackgroundColor = colorHeaders
                cellUUIDR.BackgroundColor = colorHeaders
                cellTipoR.BackgroundColor = colorHeaders

                cellFolioR.Border = Rectangle.NO_BORDER
                cellUUIDR.Border = Rectangle.NO_BORDER
                cellTipoR.Border = Rectangle.NO_BORDER

                'propiedades de la tabla relacion
                tablaRelacion.AddCell(cellFolioR)
                tablaRelacion.AddCell(cellUUIDR)
                tablaRelacion.AddCell(cellTipoR)

                For i As Integer = 0 To relacion.Rows.Count - 1 'recorrer tablas de relaciones 
                    Dim x = Split(relacion.Rows(i)("UUIDR").ToString, "&id=")

                    Dim cellFolioR1 As New PdfPCell(New Phrase(relacion.Rows(i)("folioR").ToString, stylo8))
                    Dim cellUUIDR1 As New PdfPCell(New Phrase(x(UBound(x)), stylo8))
                    Dim cellTipoR1 As New PdfPCell(New Phrase("01 Nota de crédito de los documentos relacionados", stylo8))

                    cellFolioR1.HorizontalAlignment = Rectangle.ALIGN_CENTER
                    cellUUIDR1.HorizontalAlignment = Rectangle.ALIGN_CENTER
                    cellTipoR1.HorizontalAlignment = Rectangle.ALIGN_CENTER


                    'cellFolioR1.HorizontalAlignment = Rectangle.ALIGN_CENTER
                    'cellFolioR1.Border = 12
                    'cellFolioR1.BorderColor = New BaseColor(0, 0, 128) 'Color del borde

                    'cellUUIDR1.HorizontalAlignment = Rectangle.ALIGN_CENTER
                    'cellUUIDR1.Border = 12
                    'cellUUIDR1.BorderColor = New BaseColor(0, 0, 128) 'Color del borde

                    tablaRelacion.AddCell(cellFolioR1)
                    tablaRelacion.AddCell(cellUUIDR1)
                    tablaRelacion.AddCell(cellTipoR1)

                Next
            End If
        End If



        '--------------------------------------------------------------------------------------------
        'Propiedades de la tabla de articulos---------------------------------------------------       
        'Se valida que (articulos.Rows.Count-1) sea menor a 30 para agregar espacios
        If (articulos.Rows.Count) > 20 And articulos.Rows.Count < 30 Then
            If colorB IsNot ColorGris Then
                colorB = ColorGris
            Else
                colorB = ColorBco
            End If

            Dim cellCantidad3 As New PdfPCell(New Phrase(" ", stylo8)) 'Celda para guardar el texto METODO DE PAGO 
            Dim cellNumeroParte3 As New PdfPCell(New Phrase(" ", stylo8)) 'Celda para guardar el METODO DE PAGO 
            Dim cellDescripcion3 As New PdfPCell(New Phrase(" ", stylo8)) 'Celda para guardar el texto CONDICIONES DE PAGO 
            Dim cellUnidad3 As New PdfPCell(New Phrase(" ", stylo8)) 'Celda para guardar las CONDICIONES DE PAGO 
            Dim cellPed3 As New PdfPCell(New Phrase(" ", stylo8)) 'Celda para guardar las CONDICIONES DE PAGO 
            Dim cellPrecio3 As New PdfPCell(New Phrase(" ", stylo8)) 'Celda para guardar las CONDICIONES DE PAGO 
            Dim cellImporte3 As New PdfPCell(New Phrase(" ", stylo8)) 'Celda para guardar las CONDICIONES DE PAGO 
            Dim cant = 38

            cellCantidad3.HorizontalAlignment = Rectangle.ALIGN_CENTER
            cellCantidad3.Border = 12
            cellCantidad3.BorderColor = New BaseColor(0, 0, 0) 'Color del borde

            cellNumeroParte3.HorizontalAlignment = Rectangle.ALIGN_CENTER
            cellNumeroParte3.Border = 12
            cellNumeroParte3.BorderColor = New BaseColor(0, 0, 0) 'Color del borde

            cellDescripcion3.Border = 12
            cellDescripcion3.BorderColor = New BaseColor(0, 0, 0) 'Color del borde

            cellUnidad3.HorizontalAlignment = Rectangle.ALIGN_CENTER
            cellUnidad3.Border = 12
            cellUnidad3.BorderColor = New BaseColor(0, 0, 0) 'Color del borde

            cellPed3.HorizontalAlignment = Rectangle.ALIGN_CENTER
            cellPed3.Border = 12
            cellPed3.BorderColor = New BaseColor(0, 0, 0) 'Color del borde


            cellPrecio3.HorizontalAlignment = Rectangle.ALIGN_RIGHT
            cellPrecio3.Border = 12
            cellPrecio3.BorderColor = New BaseColor(0, 0, 0) 'Color del borde

            cellImporte3.HorizontalAlignment = Rectangle.ALIGN_RIGHT
            cellImporte3.Border = 12
            cellImporte3.BorderColor = New BaseColor(0, 0, 0) 'Color del borde

            If cUtilerias.estatus = "Cancelada" Then
                tablaArticulos.AddCell(cellCantidad3) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellNumeroParte3) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellCancelar) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellUnidad3) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellPed3) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellPrecio3) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellImporte3) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellCantidad3) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellNumeroParte3) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellCancelarSAT) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellUnidad3) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellPed3) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellPrecio3) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellImporte3) 'Agrega COLUMNA


                cant = 32
            End If

            For i As Integer = articulos.Rows.Count To cant
                tablaArticulos.AddCell(cellCantidad3) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellNumeroParte3) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellDescripcion3) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellUnidad3) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellPed3) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellPrecio3) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellImporte3) 'Agrega COLUMNA
            Next
            tablaArticulos.AddCell(cellCantidadVacia3) 'Agrega COLUMNA
            tablaArticulos.AddCell(cellNumeroParteVacia3) 'Agrega COLUMNA
            tablaArticulos.AddCell(cellDescripcionVacia3) 'Agrega COLUMNA
            tablaArticulos.AddCell(cellUnidadVacia3) 'Agrega COLUMNA
            tablaArticulos.AddCell(cellPedVacia3) 'Agrega COLUMNA
            tablaArticulos.AddCell(cellPrecioVacia3) 'Agrega COLUMNA
            tablaArticulos.AddCell(cellImporteVacia3) 'Agrega COLUMNA

        End If

        If articulos.Rows.Count < 20 Then
            Dim cellCantidad3 As New PdfPCell(New Phrase(" ", stylo8)) 'Celda para guardar el texto METODO DE PAGO 
            Dim cellNumeroParte3 As New PdfPCell(New Phrase(" ", stylo8)) 'Celda para guardar el METODO DE PAGO 
            Dim cellDescripcion3 As New PdfPCell(New Phrase(" ", stylo8)) 'Celda para guardar el texto CONDICIONES DE PAGO 
            Dim cellUnidad3 As New PdfPCell(New Phrase(" ", stylo8)) 'Celda para guardar las CONDICIONES DE PAGO 
            Dim cellPed3 As New PdfPCell(New Phrase(" ", stylo8)) 'Celda para guardar las CONDICIONES DE PAGO 
            Dim cellPrecio3 As New PdfPCell(New Phrase(" ", stylo8)) 'Celda para guardar las CONDICIONES DE PAGO 
            Dim cellImporte3 As New PdfPCell(New Phrase(" ", stylo8)) 'Celda para guardar las CONDICIONES DE PAGO 
            Dim Cantidad = 13

            cellCantidad3.HorizontalAlignment = Rectangle.ALIGN_CENTER
            cellCantidad3.Border = 12
            cellCantidad3.BorderColor = New BaseColor(0, 0, 0) 'Color del borde
            cellNumeroParte3.HorizontalAlignment = Rectangle.ALIGN_CENTER
            cellNumeroParte3.Border = 12
            cellNumeroParte3.BorderColor = New BaseColor(0, 0, 0) 'Color del borde
            cellDescripcion3.Border = 12
            cellDescripcion3.BorderColor = New BaseColor(0, 0, 0) 'Color del borde
            cellUnidad3.HorizontalAlignment = Rectangle.ALIGN_CENTER
            cellUnidad3.Border = 12
            cellUnidad3.BorderColor = New BaseColor(0, 0, 0) 'Color del borde

            cellPed3.HorizontalAlignment = Rectangle.ALIGN_CENTER
            cellPed3.Border = 12
            cellPed3.BorderColor = New BaseColor(0, 0, 0) 'Color del borde

            cellPrecio3.HorizontalAlignment = Rectangle.ALIGN_RIGHT
            cellPrecio3.Border = 12
            cellPrecio3.BorderColor = New BaseColor(0, 0, 0) 'Color del borde
            cellImporte3.HorizontalAlignment = Rectangle.ALIGN_RIGHT
            cellImporte3.Border = 12
            cellImporte3.BorderColor = New BaseColor(0, 0, 0) 'Color del borde
            If cUtilerias.estatus = "Cancelada" Then
                tablaArticulos.AddCell(cellCantidad3) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellNumeroParte3) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellCancelar) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellUnidad3) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellPed3) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellPrecio3) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellImporte3) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellCantidad3) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellNumeroParte3) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellCancelarSAT) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellUnidad3) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellPed3) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellPrecio3) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellImporte3) 'Agrega COLUMNA
                Cantidad = 7
            End If


            For i As Integer = articulos.Rows.Count To Cantidad
                tablaArticulos.AddCell(cellCantidad3) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellNumeroParte3) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellDescripcion3) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellUnidad3) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellPed3) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellPrecio3) 'Agrega COLUMNA
                tablaArticulos.AddCell(cellImporte3) 'Agrega COLUMNA
            Next
            tablaArticulos.AddCell(cellCantidadVacia3) 'Agrega COLUMNA
            tablaArticulos.AddCell(cellNumeroParteVacia3) 'Agrega COLUMNA
            tablaArticulos.AddCell(cellDescripcionVacia3) 'Agrega COLUMNA
            tablaArticulos.AddCell(cellUnidadVacia3) 'Agrega COLUMNA
            tablaArticulos.AddCell(cellPedVacia3) 'Agrega COLUMNA
            tablaArticulos.AddCell(cellPrecioVacia3) 'Agrega COLUMNA
            tablaArticulos.AddCell(cellImporteVacia3) 'Agrega COLUMNA

        End If


        '-------------------------------------------------------------------------------------
        'celdas de la tabla sello-------------------------------------------------
        Dim cellCDS As New PdfPCell(New Phrase("CDS", arial10B)) 'Celda para guardar el texto CDS 
        Dim cellSelloVacia As New PdfPCell(New Phrase("")) 'Celda Vacia para dar espacio
        Dim cellSello As New PdfPCell(New Phrase("SELLO", arial10B)) 'Celda para guardar el texto SELLO
        Dim cellCDS2 As New PdfPCell(New Phrase(cUtilerias.CDS, stylo12)) 'Celda para guardar el CDS 
        Dim cellSelloVacia2 As New PdfPCell(New Phrase("")) 'Celda Vacia para dar espacio
        Dim cellSello2 As New PdfPCell(New Phrase(cUtilerias.SELLO, stylo14)) 'Celda para guardar el SELLO
        Dim cellSelloVacia3 As New PdfPCell(New Phrase(" ")) 'Celda Vacia para dar espacio
        Dim cellCO As New PdfPCell(New Phrase("CADENA ORIGINAL DE TIMBRADO", arial10B)) 'Celda para guardar el texto CADENA ORIGINAL DE TIMBRADO
        Dim cellCO2 As New PdfPCell(New Phrase(cUtilerias.cadnaOriginal, stylo14)) 'Celda para guardar la CADENA ORIGINAL DE TIMBRADO
        Dim cellSelloVacia4 As New PdfPCell(New Phrase(" ")) 'Celda Vacia para dar espacio
        Dim cellTexto As New PdfPCell(New Phrase("ESTE DOCUMENTO ES UNA REPRESENTACION IMPRESA DE UN CFDI RÉGIMEN GENERAL DE LEY PERSONAS MORALES", stylo8)) 'Celda para guardar el texto del comprobante
        'total Format(Text1.Text, "##,##00.00")

        Dim cellTexto2 As New PdfPCell(New Phrase("FOLIO " & factu.Rows(0)("folio").ToString & " POR $  " & Format(total, "##,##00.00") & "  DEBO (EMOS) Y PAGERE (EMOS) INCONDICIONALMENTE EL DIA ____ DE _______ DE ____ EN LA CD. DE SALTILLOS COAH., Y EN LA CIUDAD QUE SE ME (NOS) REQUIERA, POR ESTE PAGARE A LA ORDEN DE " & cUtilerias.NombreEmpres & ", LA CANTIDAD DE  " & factu.Rows(0)("total").ToString & " , VALOR RECIBIDO A MI (NUESTRA) ENTERA SATISFACCION QUEDA CONVENIDO QUE EN CASO DE MORA, EL PRESENTE TITULO CAUSARA UN INTERES DEL_____ % MENSUAL HASTA SU TOTAL LIQUIDACION, SIN QUE POR ELLO SE CONSIDERE PRORROGADO EL PLAZO. TODO MATERIAL QUE AMPARE ESTA FACTURA PODRÁ Y SERÁ RETIRADO DEL LUGAR DONDE HAYA SIDO INSTALADO EN CASO DE INCUMPLIR CON EL DICHO PAGO DE LA MISMA, INDEPENDIENTEMENTE SI FUESE INSTALADO A TERCEROS FECHA: " & factu.Rows(0)("fechacfd").ToString & "        _________________________________ ACEPTO Y FIRMO DE CONSENTIMIENTO ", stylo8)) 'Celda para guardar el texto del comprobante
        '--------------------------------------------------------------------------
        'celdas de la tabla moneda-------------------------------------------------
        Dim cellUUID As New PdfPCell(New Phrase("UUID", arial10B3)) 'Celda para guardar el texto UUID 
        Dim cellUUID2 As New PdfPCell(New Phrase(Mid(cUtilerias.UUID, InStrRev(cUtilerias.UUID, "&") + 4), stylo13)) 'Celda para guardar el UUID
        Dim cellUUID3 As New PdfPCell(uuid) 'Celda para guardar el sello bidimensional
        '--------------------------------------------------------------------------
        'celdas de la tabla moneda-------------------------------------------------

        Dim cellMetodoPago As New PdfPCell(New Phrase("FORMA DE PAGO:", stylo5)) 'Celda para guardar el texto METODO DE PAGO 
        ' Dim cellMetodoPagoVacia As New PdfPCell(New Phrase("jhgjgh")) 'Celda vacia para dar espacio
        Dim cellMetodoPago2 As New PdfPCell(New Phrase(cUtilerias.metodoPago, stylo6)) 'Celda para guardar el METODO DE PAGO

        Dim cellNCuenta As New PdfPCell(New Phrase("No. DE CUENTA DE PAGO:", stylo5)) 'Celda para guardar el texto No. DE CUENTA DE PAGO
        Dim cellNCuenta2 As New PdfPCell(New Phrase(cUtilerias.numeroCuenta, stylo6)) 'Celda para guardar el numero de la cuenta

        Dim cellCondicionesPago As New PdfPCell(New Phrase("CONDICIONES DE PAGO:", stylo5)) 'Celda para guardar el texto CONDICIONES DE PAGO 
        Dim cellCondicionesPago2 As New PdfPCell(New Phrase(cUtilerias.condicionesPago, stylo6)) 'Celda para guardar las CONDICIONES DE PAGO 
        '--------------------------------------------------------------------------
        'celdas de la tabla moneda-------------------------------------------------
        'Dim cellmon As New PdfPCell(New Phrase("MONEDA", stylo5)) 'Celda para guardar el texto MONEDA 
        'Dim cellmon2 As New PdfPCell(New Phrase(cUtilerias.moneda, stylo7)) 'Celda para guardar la moneda
        'Dim cellLugExpedicion As New PdfPCell(New Phrase("LUGAR DE EXPEDICION", stylo5)) 'Celda para guardar el texto LUGAR DE EXPEDICION 
        'Dim cellLugExpedicion2 As New PdfPCell(New Phrase(cUtilerias.lugarExpedicion, stylo7)) 'Celda para guardar el luegar de expedicion
        Dim cellOCompra As New PdfPCell(New Phrase("OC: ", stylo16)) 'Celda para guardar el texto ORDEN DE COMPRA 
        Dim cellOCompra2 As New PdfPCell(New Phrase(cUtilerias.ordenCompra, stylo27)) 'Celda para guardar la orden de compra
        '-------------------------------------------------------------------------
        'celdas de la tabla Consignatario-----------------------------------------
        'Dim cellConsig As New PdfPCell(New Phrase("CONSIGNATARIO", stylo5)) 'Celda para guardar el texto CONSIGNATARIO
        'Dim cellConsig2 As New PdfPCell(New Phrase(cUtilerias.consignatario, stylo9)) 'Celda para guardar el texto CONSIGNATARIO
        '----------------------------------------------------------------------------
        'celdas de la tabla datos del cliente--------------------------------------------
        Dim p1 As Phrase = New Phrase()
        Dim p2 As Phrase = New Phrase()
        Dim p6 As Phrase = New Phrase()

        Dim cellVendido As New PdfPCell(New Phrase("VENDIDO A :", stylo5)) 'Celda para guardar el texto VENDIDO A
        Dim cellCliente As New PdfPCell(New Phrase(cUtilerias.nombreCliente, stylo6)) 'Celda para guardar el nombre del cliente
        Dim cellDireccionCliente As New PdfPCell(New Phrase(cUtilerias.direccionCliente, stylo6)) 'Celda para guardar la direccion del cliente
        Dim cellColoniaCliente As New PdfPCell(New Phrase(cUtilerias.coloniaCliente, stylo6)) 'Celda para guardar la colonia del cliente
        Dim cellCiudadCliente As New PdfPCell(New Phrase(cUtilerias.CiudadCliente, stylo6)) 'Celda para guardar la ciudad del cliente
        Dim cellestadoCliente As New Chunk(cUtilerias.telefonoCliente, stylo6) 'Celda para guardar la ciudad del cliente
        Dim cellestadoCliente2 As New Chunk("Tel:", stylo5) 'Celda para guardar la ciudad del cliente
        Dim celltelefonoCliente2 As New Chunk("Estado: ", stylo5) 'Celda para guardar la ciudad del cliente
        Dim celltelefonoCliente3 As New Chunk(cUtilerias.EstadoCliente, stylo6) 'Celda para guardar la ciudad del cliente
        Dim cellrfcC As New PdfPCell(tablaRFC) 'Celda para guardar la tabla RFC
        Dim cellusoCfdiN2 As New Chunk("USOCFDI :  ", stylo5)
        Dim cellusoCfdiN3 As New Chunk(cUtilerias.usoCFDI, stylo6)
        Dim cellUsoCFDI As New PdfPCell(New Phrase("", stylo6)) 'Celda para guardar la ciudad del cliente

        p1.Add(cellestadoCliente2)
        p1.Add(cellestadoCliente)

        p2.Add(celltelefonoCliente2)
        p2.Add(celltelefonoCliente3)

        p6.Add(cellusoCfdiN2)
        p6.Add(cellusoCfdiN3)

        Dim cellestadoCliente3 As New PdfPCell(New Paragraph(p1)) 'Celda para guardar la ciudad del cliente
        Dim celltelefonoCliente As New PdfPCell(New Paragraph(p2)) 'Celda para guardar la ciudad del cliente
        Dim cellusoCfdiN As New PdfPCell(New Paragraph(p6))
        '----------------------------------------------------------------------
        'celdas de la tabla venta--------------------------------------------------
        Dim cellFechaVenta As New PdfPCell(New Phrase("FECHA DE VENTA", stylo5)) 'Celda para guardar el texto fecha de venta
        Dim cellFechaVenta2 As New PdfPCell(New Phrase(FormatDateTime(cUtilerias.fechaVenta, DateFormat.ShortDate), stylo6)) 'Celda para guardar la fecha de venta
        Dim cellCondicionesVenta As New PdfPCell(New Phrase("CONDICIONES DE VENTAS", stylo5)) 'Celda para guardar el texto condiciones de venta
        Dim cellCondicionesVenta2 As New PdfPCell(New Phrase(cUtilerias.condicionesVenta, stylo6)) 'Celda para guardar las condiciones de venta
        Dim cellFormaPago As New PdfPCell(New Phrase("METODO DE PAGO", stylo5)) 'Celda para guardar el texto condiciones de venta
        Dim cellFormaPago1 As New PdfPCell(New Phrase(cUtilerias.FormaPago, stylo6)) 'Celda para guardar la ciudad del cliente


        '-----------------------------------------------------------------------
        'celdas de la tabla folio---------------------------------------
        Dim cellFolio As New PdfPCell(New Phrase(cUtilerias.folioFactura, stylo4)) 'Celda para guardar el folio
        Dim cellFactrua As New PdfPCell(New Phrase(cUtilerias.comprobante, stylo3)) 'Celda para guardar el texto factura
        Dim cellFolio2 As New PdfPCell(New Phrase("")) 'celda vacia para visulaizar menos ancha la tabla
        '-----------------------------------------------------------------------
        'celdas de la tabla empresa---------------------------------------
        Dim cellEmpresa As New PdfPCell(New Phrase(cUtilerias.NombreEmpres, fontDatosE)) 'celda pra guardar el nombre de la empresa

        Dim cellDireccion As New PdfPCell(New Phrase(cUtilerias.direccionEmpresa, fontDatos1)) 'celda para guardar la direccion de la empresa

        Dim cellDireccion2 As New PdfPCell(New Phrase(cUtilerias.direccionEmpresa2, fontDatos1)) 'celda para guardar la direccion de la empresa

        Dim cellDireccion3 As New PdfPCell(New Phrase(cUtilerias.direccionEmpresa3, stylo30)) 'celda para guardar regimen fiscal de la empresa

        Dim cellDireccion4 As New PdfPCell(New Phrase(cUtilerias.direccionEmpresa4, stylo30)) 'celda para guardar rfc y regimen patronal de la empresa
        '-------------------------------------------------------------------------------
        'celdas de la tabla fecha-------------------------------------------------------
        Dim cellFecha As New PdfPCell(New Phrase("FECHA", stylo5)) 'celda para guardar el texto fecha
        Dim cellFecha2 As New PdfPCell(New Phrase(año & "-" & mes & "-" & dia & "T" & hora & ":" & minutos & ":" & segundos, stylo6)) 'celda pra guardar la fecha de facturacion
        Dim cellFecha3 As New PdfPCell(New Phrase("")) 'celda vacia para monstrar menos ancha la tabla
        '-------------------------------------------------------------------------------
        'celdas de la tabla fecha-------------------------------------------------------        
        Dim cellObservaciones As New PdfPCell(New Phrase("OBSERVACIONES", arial10B)) 'Celda para guardar el texto CANTIDAD
        Dim cellObservaciones2 As New PdfPCell(New Phrase(cUtilerias.obsCliente, stylo28)) 'celda pra guardar la fecha de facturacion        
        '-------------------------------------------------------------------------------
        'Propiedades de las celdas de la tabla de Observaciones------------------------------------
        cellObservaciones.BackgroundColor = colorHeaders 'fondo de la tabla  
        cellObservaciones.HorizontalAlignment = Rectangle.ALIGN_CENTER 'Aliniar
        cellObservaciones.Border = Rectangle.NO_BORDER 'Aliniar
        cellObservaciones2.BorderColor = colorHeaders 'fondo de la tabla  
        cellObservaciones.HorizontalAlignment = Rectangle.ALIGN_CENTER 'Mostrar los bordes seleccionados
        '----------------------------------------------------------------------------------
        'Propiedades de las celdas de la tabla de UUID------------------------------------
        cellCDS.BackgroundColor = colorHeaders 'fondo de la tabla  
        cellCDS.HorizontalAlignment = Rectangle.ALIGN_CENTER 'ALINIAR
        cellCDS.Border = Rectangle.NO_BORDER 'Mostrar los bordes seleccionados 
        cellSelloVacia.Border = Rectangle.NO_BORDER 'Mostrar los bordes seleccionados        
        cellSello.BackgroundColor = colorHeaders 'fondo de la tabla  
        cellSello.HorizontalAlignment = Rectangle.ALIGN_CENTER 'Aliniar       
        cellSello.Border = Rectangle.NO_BORDER 'Mostrar los bordes seleccionados 
        cellCDS2.Border = Rectangle.NO_BORDER 'fondo de la tabla  
        cellCDS2.HorizontalAlignment = Rectangle.ALIGN_CENTER 'ALINIAR
        cellSelloVacia2.Border = Rectangle.NO_BORDER 'Mostrar los bordes seleccionados
        cellSello2.Border = Rectangle.NO_BORDER 'fondo de la tabla        
        cellSelloVacia3.Border = Rectangle.NO_BORDER 'Mostrar los bordes seleccionados 
        cellSelloVacia3.Colspan = 3 'Unir celdas
        cellCO.BackgroundColor = colorHeaders 'fondo de la tabla  
        cellCO.HorizontalAlignment = Rectangle.ALIGN_CENTER 'ALINIAR
        cellCO.Border = Rectangle.NO_BORDER 'Mostrar los bordes seleccionados 
        cellCO.Colspan = 3 'Unir celdas
        cellCO2.Border = Rectangle.NO_BORDER 'Mostrar los bordes seleccionados 
        cellCO2.Colspan = 3 'Unir celdas
        cellSelloVacia4.Border = Rectangle.NO_BORDER 'Mostrar los bordes seleccionados 
        cellSelloVacia4.Colspan = 3 'Unir celdas
        cellTexto.Border = Rectangle.NO_BORDER 'Mostrar los bordes seleccionados 
        cellTexto.Colspan = 3 'Unir celdas
        cellTexto.HorizontalAlignment = Rectangle.ALIGN_CENTER 'ALINIAR
        cellTexto2.Border = Rectangle.NO_BORDER 'Mostrar los bordes seleccionados 
        cellTexto2.Colspan = 3 'Unir celdas
        cellTexto2.HorizontalAlignment = Rectangle.ALIGN_JUSTIFIED 'ALINIAR

        '---------------------------------------------------------------------------------------
        'Propiedades de las celdas de la tabla de UUID------------------------------------
        cellUUID.BackgroundColor = colorHeaders 'fondo de la tabla  
        cellUUID.HorizontalAlignment = Rectangle.ALIGN_CENTER 'Aliniar
        cellUUID.Border = Rectangle.NO_BORDER 'Aliniar
        cellUUID2.BorderColor = colorHeaders 'fondo de la tabla  
        cellUUID2.HorizontalAlignment = Rectangle.ALIGN_CENTER 'Mostrar los bordes seleccionados
        cellUUID3.BorderColor = colorHeaders 'fondo de la tabla  
        cellUUID3.HorizontalAlignment = Rectangle.ALIGN_CENTER 'Aliniar
        cellUUID3.VerticalAlignment = Rectangle.ALIGN_MIDDLE 'Aliniar
        cellUUID3.FixedHeight = 70.0F
        '---------------------------------------------------------------------------------------
        'Propiedades de las celdas de la tabla de metodo de pago------------------------------------

        'cellMetodoPagoVacia.Colspan = 4
        ' cellMetodoPagoVacia.Border = Rectangle.NO_BORDER

        cellMetodoPago.BackgroundColor = New BaseColor(255, 255, 255) 'fondo de la tabla                  
        cellMetodoPago.Border = Rectangle.BOTTOM_BORDER + Rectangle.LEFT_BORDER '7 Mostrar bordes seleccionados
        cellMetodoPago.HorizontalAlignment = Rectangle.ALIGN_LEFT 'Aliniar
        cellMetodoPago.VerticalAlignment = Rectangle.ALIGN_MIDDLE 'Aliniar
        cellMetodoPago.BorderColor = colorHeaders 'Color del borde

        cellMetodoPago2.BackgroundColor = New BaseColor(255, 255, 255) 'fondo de la tabla   
        cellMetodoPago2.Border = Rectangle.BOTTOM_BORDER '11 Mostra bordes seleccionados
        cellMetodoPago2.HorizontalAlignment = Rectangle.ALIGN_LEFT 'Aliniar
        cellMetodoPago2.VerticalAlignment = Rectangle.ALIGN_MIDDLE 'Aliniar
        cellMetodoPago2.BorderColor = colorHeaders 'Color del borde

        cellNCuenta.BackgroundColor = New BaseColor(255, 255, 255) 'fondo de la tabla
        cellNCuenta.Border = Rectangle.BOTTOM_BORDER '3 Mostra bordes seleccionados
        cellNCuenta.HorizontalAlignment = Rectangle.ALIGN_LEFT 'Aliniar
        cellNCuenta.VerticalAlignment = Rectangle.ALIGN_MIDDLE 'Aliniar
        cellNCuenta.BorderColor = colorHeaders 'Color del borde

        cellNCuenta2.BackgroundColor = New BaseColor(255, 255, 255) 'fondo de la tabla       
        cellNCuenta2.Border = Rectangle.BOTTOM_BORDER + Rectangle.RIGHT_BORDER '11Mostra bordes seleccionados
        cellNCuenta2.HorizontalAlignment = Rectangle.ALIGN_LEFT 'Aliniar
        cellNCuenta2.VerticalAlignment = Rectangle.ALIGN_MIDDLE 'Aliniar
        cellNCuenta2.BorderColor = colorHeaders 'Color del borde

        cellCondicionesPago.BackgroundColor = New BaseColor(255, 255, 255) 'fondo de la tabla
        cellCondicionesPago.Border = Rectangle.BOTTOM_BORDER '3Mostra bordes seleccionados
        cellCondicionesPago.HorizontalAlignment = Rectangle.ALIGN_LEFT 'Aliniar
        cellCondicionesPago.VerticalAlignment = Rectangle.ALIGN_MIDDLE 'Aliniar
        cellCondicionesPago.BorderColor = colorHeaders 'Color del borde

        cellCondicionesPago2.BackgroundColor = New BaseColor(255, 255, 255) 'fondo de la tabla       
        cellCondicionesPago2.Border = Rectangle.BOTTOM_BORDER '11Mostra bordes seleccionados
        cellCondicionesPago2.HorizontalAlignment = Rectangle.ALIGN_LEFT 'Aliniar
        cellCondicionesPago2.VerticalAlignment = Rectangle.ALIGN_MIDDLE 'Aliniar
        cellCondicionesPago2.BorderColor = colorHeaders 'Color del borde

        cellOCompra.BackgroundColor = New BaseColor(255, 255, 255) 'fondo de la tabla
        cellOCompra.Border = Rectangle.BOTTOM_BORDER '3Mostra bordes seleccionados
        cellOCompra.HorizontalAlignment = Rectangle.ALIGN_LEFT 'Aliniar
        cellOCompra.VerticalAlignment = Rectangle.ALIGN_MIDDLE 'Aliniar
        cellOCompra.BorderColor = colorHeaders 'Color del borde

        cellOCompra2.BackgroundColor = New BaseColor(255, 255, 255) 'fondo de la tabla       
        cellOCompra2.Border = Rectangle.BOTTOM_BORDER + Rectangle.RIGHT_BORDER '11Mostra bordes seleccionados
        cellOCompra2.HorizontalAlignment = Rectangle.ALIGN_LEFT 'Aliniar
        cellOCompra2.VerticalAlignment = Rectangle.ALIGN_MIDDLE 'Aliniar
        cellOCompra2.BorderColor = colorHeaders 'Color del borde

        '-------------------------------------------------------------------------------------------
        'Propiedades de las celdas de la tabla de la moneda -----------------------------------------------
        'cellmon.Border = Rectangle.NO_BORDER 'Ocultar bordes
        'cellmon2.Border = Rectangle.NO_BORDER 'Ocultar bordes
        'cellLugExpedicion.Border = Rectangle.NO_BORDER 'Ocultar bordes
        'cellLugExpedicion2.Border = Rectangle.NO_BORDER 'Ocultar bordes
        'cellOCompra.Border = Rectangle.NO_BORDER 'Ocultar bordes
        'cellOCompra2.Border = Rectangle.NO_BORDER 'Ocultar bordes
        '------------------------------------------------------------------------------------------
        'Propiedades de las celdas de la tabla del Consignatario -----------------------------------------------
        'cellConsig.Border = Rectangle.NO_BORDER 'Ocultar bordes
        'cellConsig2.Border = Rectangle.NO_BORDER 'Ocultar bordes
        '------------------------------------------------------------------------------------------
        'Propiedades de las celdas de la tabla datos del cliente------------------------------------
        cellVendido.Colspan = 2 'Unir columnas
        cellVendido.Border = Rectangle.NO_BORDER 'Ocultar bordes
        cellCliente.Colspan = 2 'Unir columnas
        cellCliente.Border = Rectangle.NO_BORDER 'Ocultar bordes
        cellDireccionCliente.Colspan = 2 'Unir columnas
        cellDireccionCliente.Border = Rectangle.NO_BORDER 'Ocultar bordes
        cellColoniaCliente.Border = Rectangle.NO_BORDER 'Ocultar bordes
        cellCiudadCliente.Border = Rectangle.NO_BORDER 'Ocultar bordes
        cellestadoCliente3.Border = Rectangle.NO_BORDER 'Ocultar bordes
        celltelefonoCliente.Border = Rectangle.NO_BORDER 'Ocultar bordes
        cellrfcC.Colspan = 2 'Unir columnas
        cellrfcC.Border = Rectangle.NO_BORDER 'Ocultar bordes
        cellusoCfdiN.Border = Rectangle.NO_BORDER
        cellUsoCFDI.Border = Rectangle.NO_BORDER


        '----------------------------------------------------------------------------------
        'Propiedades de las celdas de la tabla venta------------------------------------
        cellFechaVenta.Border = Rectangle.NO_BORDER 'Ocultar bordes
        cellFechaVenta2.Border = Rectangle.NO_BORDER 'Ocultar bordes
        cellCondicionesVenta.Border = Rectangle.NO_BORDER 'Ocultar bordes        
        cellCondicionesVenta2.Border = Rectangle.NO_BORDER 'Ocultar bordes  
        cellFormaPago.Border = Rectangle.NO_BORDER 'Ocultar bordes  
        cellFormaPago1.Border = Rectangle.NO_BORDER 'Ocultar bordes  

        '------------------------------------------------------------------------------
        'Propiedades de las celdas de la tabla fecha------------------------------------
        cellFecha3.Border = Rectangle.NO_BORDER 'Mostra bordes seleccionados 
        cellFecha3.BorderColor = New BaseColor(0, 0, 0) 'Color del borde
        cellFecha2.Border = Rectangle.NO_BORDER 'Mostra bordes seleccionados        
        cellFecha2.BackgroundColor = New BaseColor(255, 255, 255) 'fondo de la tabla
        cellFecha2.BorderColor = New BaseColor(0, 0, 0) 'Color del borde
        cellFecha.BackgroundColor = New BaseColor(255, 255, 255) 'fondo de la tabla
        cellFecha.Border = Rectangle.NO_BORDER 'Mostra bordes seleccionados
        cellFecha.BorderColor = New BaseColor(0, 0, 0) 'Color del borde 
        '------------------------------------------------------------------------------
        'Propiedades de las celdas de la tabla factura------------------------------------
        cellFolio2.Border = Rectangle.NO_BORDER 'Ocultar bordes
        cellFolio.HorizontalAlignment = Rectangle.ALIGN_CENTER 'Aliniar
        cellFolio.BackgroundColor = New BaseColor(255, 255, 255) 'Fondo de la tabla 
        cellFolio.BorderColor = New BaseColor(0, 0, 0)
        cellFactrua.HorizontalAlignment = Rectangle.ALIGN_CENTER 'Mostra bordes seleccionados
        cellFactrua.BackgroundColor = New BaseColor(198, 198, 198) 'Fondo de la tabla
        cellFactrua.BorderColor = New BaseColor(0, 0, 0) 'Color del borde
        '------------------------------------------------------------------------------
        'Propiedades de las celdas de la tabla empresa------------------------------------
        cellEmpresa.HorizontalAlignment = Rectangle.ALIGN_CENTER 'Aliniar
        cellEmpresa.Border = Rectangle.NO_BORDER 'ocultar borldes
        cellEmpresa.BorderColor = New BaseColor(200, 200, 128)
        cellDireccion.HorizontalAlignment = Rectangle.ALIGN_CENTER 'Aliniar
        cellDireccion.Border = Rectangle.NO_BORDER 'ocultar borldes
        cellDireccion2.HorizontalAlignment = Rectangle.ALIGN_CENTER 'Aliniar
        cellDireccion2.Border = Rectangle.NO_BORDER 'ocultar borldes
        cellDireccion3.HorizontalAlignment = Rectangle.ALIGN_CENTER 'Aliniar
        cellDireccion3.Border = Rectangle.NO_BORDER 'ocultar borldes
        cellDireccion4.HorizontalAlignment = Rectangle.ALIGN_CENTER 'Aliniar
        cellDireccion4.Border = Rectangle.NO_BORDER 'ocultar borldes
        '------------------------------------------------------------------------------
        '------------------------------------------------------------------------------
        'tabla de la remision
        Dim remision As DataTable = cUtilerias.consultas("select v.* from ventaslog v INNER JOIN factura f ON f.ASN = v.nventa where idfactura =" & idfactura & " AND tipo = 'REMISION'", cUtilerias.cadenaConexion)
        'Ciclo importantisimoooo en el codigooo!!!!!! ia que es el que hace el recorrido en todoss los campooss

        'Agregar articulos a la tabla-------------------------------------------------------------
        vistaRem = "  "
        For i As Integer = 0 To remision.Rows.Count - 1 'For
            If remision.Rows(i)("numero").ToString <> "" Then
                vistaRem = "1"
                Dim cellremison As New PdfPCell(New Phrase("REMISIÓN " & remision.Rows(i)("numero").ToString, stylo10)) 'Celda para guardar el texto CANTIDAD      
                '-------------------------------------------------------------------------------
                'Propiedades de las celdas de la tabla de remisiones------------------------------------
                cellremison.BorderColor = New BaseColor(0, 0, 0) 'fondo de la tabla  
                cellremison.HorizontalAlignment = Rectangle.ALIGN_CENTER 'Mostrar los bordes seleccionados
                tablaRemision.AddCell(cellremison)
            End If
        Next

        'Propiedades de la tabla SELLO---------------------------------------------------        
        tablaObservaciones.AddCell(cellObservaciones) 'Agrega COLUMNA
        tablaObservaciones.AddCell(cellObservaciones2) 'Agrega COLUMNA
        tablaObservaciones.SpacingBefore = 3

        'tabla anticipo

        Dim tablaAnt As New PdfPTable(3)
        Dim anchoAnt() As Single = {25, 25, 25} 'Tamaños de las celdas de la tabla sello
        tablaAnt.WidthPercentage = 100 'Ancho de la tablas

        Dim cellAntEnc As New PdfPCell(New Phrase("ANTICIPOS", stylo10)) 'celda pra guardar la fecha de facturacion
        Dim cellAntFecha As New PdfPCell(New Phrase("FECHA", stylo10)) 'celda vacia para monstrar menos ancha la tabla
        Dim cellAntFolio As New PdfPCell(New Phrase("FOLIO", stylo10)) 'celda vacia para monstrar menos ancha la tabla
        Dim cellAntTotal As New PdfPCell(New Phrase("IMPORTE", stylo10)) 'celda vacia para monstrar menos ancha la tabla

        Dim cellAntEnc1 As New PdfPCell(New Phrase(" ", stylo10)) 'celda pra guardar la fecha de facturacion
        Dim cellAntFecha1 As New PdfPCell(New Phrase(FormatDateTime(fechaApp, DateFormat.ShortDate), stylo10)) 'celda vacia para monstrar menos ancha la tabla
        Dim cellAntFolio1 As New PdfPCell(New Phrase(folioApp, stylo10)) 'celda vacia para monstrar menos ancha la tabla
        Dim cellAntTotal1 As New PdfPCell(New Phrase(Format(Val(totalAnt), "##,##00.00"), stylo10)) 'celda vacia para monstrar menos ancha la tabla


        cellAntEnc.BackgroundColor = New BaseColor(198, 198, 198) 'fondo de la tabla  
        cellAntEnc.HorizontalAlignment = Rectangle.ALIGN_CENTER 'Aliniar
        cellAntEnc.BorderColor = New BaseColor(0, 0, 0) 'fondo de la tabla  
        cellAntEnc.HorizontalAlignment = Rectangle.ALIGN_CENTER 'Mostrar los bordes seleccionados
        cellAntEnc.Colspan = 3

        cellAntFolio1.HorizontalAlignment = Rectangle.ALIGN_CENTER
        cellAntFecha1.HorizontalAlignment = Rectangle.ALIGN_CENTER
        cellAntTotal1.HorizontalAlignment = Rectangle.ALIGN_CENTER

        'cellAntFecha.BackgroundColor = New BaseColor(198, 198, 198) 'Color del borde
        ' cellAntFecha.Border = Rectangle.NO_BORDER 'Mostrar bordes seleccionados
        cellAntFecha.HorizontalAlignment = Rectangle.ALIGN_CENTER 'Aliniar
        cellAntFecha.BorderColor = New BaseColor(0, 0, 0) 'Color del borde

        'cellAntFolio.BackgroundColor = New BaseColor(198, 198, 198) 'Color del borde
        ' cellAntFolio.Border = Rectangle.NO_BORDER 'Mostrar bordes seleccionados
        cellAntFolio.HorizontalAlignment = Rectangle.ALIGN_CENTER 'Aliniar
        cellAntFolio.BorderColor = New BaseColor(0, 0, 0) 'Color del borde

        ' cellAntTotal.BackgroundColor = New BaseColor(198, 198, 198) 'Color del borde
        '  cellAntTotal.Border = Rectangle.NO_BORDER 'Mostrar bordes seleccionados
        cellAntTotal.HorizontalAlignment = Rectangle.ALIGN_CENTER 'Aliniar
        cellAntTotal.BorderColor = New BaseColor(0, 0, 0) 'Color del borde



        tablaAnt.SetWidths(anchoAnt)

        tablaAnt.AddCell(cellAntEnc) 'Agrega COLUMNA
        tablaAnt.AddCell(cellAntFecha) 'Agrega COLUMNA
        tablaAnt.AddCell(cellAntFolio) 'Agrega COLUMNA 
        tablaAnt.AddCell(cellAntTotal) 'Agrega COLUMNA 
        tablaAnt.AddCell(cellAntFecha1) 'Agrega COLUMNA
        tablaAnt.AddCell(cellAntFolio1) 'Agrega COLUMNA 
        tablaAnt.AddCell(cellAntTotal1) 'Agrega COLUMNA

        '--------------------------------------------------------------------------------
        'Propiedades de la tabla SELLO---------------------------------------------------
        TablaSello.SetWidths(anchoSello) 'Ajusta el tamaño de cada columna
        TablaSello.AddCell(cellCDS) 'Agrega COLUMNA
        TablaSello.AddCell(cellSelloVacia) 'Agrega COLUMNA 
        TablaSello.AddCell(cellSello) 'Agrega COLUMNA 
        TablaSello.AddCell(cellCDS2) 'Agrega COLUMNA
        TablaSello.AddCell(cellSelloVacia2) 'Agrega COLUMNA 
        TablaSello.AddCell(cellSello2) 'Agrega COLUMNA 
        TablaSello.AddCell(cellSelloVacia3) 'Agrega COLUMNA 
        TablaSello.AddCell(cellCO) 'Agrega COLUMNA    
        TablaSello.AddCell(cellCO2) 'Agrega COLUMNA    
        TablaSello.AddCell(cellSelloVacia4) 'Agrega COLUMNA 
        TablaSello.AddCell(cellTexto) 'Agrega COLUMNA 
        TablaSello.AddCell(cellTexto2) 'Agrega COLUMNA 
        '--------------------------------------------------------------------------------
        'Propiedades de la tabla UUID---------------------------------------------------        
        tablaUUID.AddCell(cellUUID) 'Agrega COLUMNA
        tablaUUID.AddCell(cellUUID2) 'Agrega COLUMNA 
        tablaUUID.AddCell(cellUUID3) 'Agrega COLUMNA 
        '--------------------------------------------------------------------------------
        'Propiedades de la tabla moneda---------------------------------------------------
        'tablaMoneda.AddCell(cellmon) 'Agrega COLUMNA
        'tablaMoneda.AddCell(cellmon2) 'Agrega COLUMNA
        'tablaMoneda.AddCell(cellLugExpedicion) 'Agrega COLUMNA
        'tablaMoneda.AddCell(cellLugExpedicion2) 'Agrega COLUMNA
        'tablaMoneda.AddCell(cellOCompra) 'Agrega COLUMNA
        'tablaMoneda.AddCell(cellOCompra2) 'Agrega COLUMNA
        '--------------------------------------------------------------------------------
        'Propiedades de la tabla consignatario------------------------------------------
        'tablaConsignatario.AddCell(cellConsig) 'Agrega COLUMNA
        'tablaConsignatario.AddCell(cellConsig2) 'Agrega COLUMNA
        '--------------------------------------------------------------------------
        'Propiedades de la tabala Datos cleinte-----------------------------------   
        tablaDatosCliente.SetWidths(anchoDatosCliente) 'Ajusta el tamaño de cada columna
        tablaDatosCliente.AddCell(cellVendido) 'Agrega COLUMNA
        tablaDatosCliente.AddCell(cellCliente) 'Agrega COLUMNA
        tablaDatosCliente.AddCell(cellDireccionCliente) 'Agrega COLUMNA
        tablaDatosCliente.AddCell(cellColoniaCliente) 'Agrega COLUMNA
        tablaDatosCliente.AddCell(cellestadoCliente3) 'Agrega COLUMNA
        tablaDatosCliente.AddCell(cellCiudadCliente) 'Agrega COLUMNA
        tablaDatosCliente.AddCell(celltelefonoCliente) 'Agrega COLUMNA

        tablaDatosCliente.AddCell(cellrfcC) 'Agrega COLUMNA
        tablaDatosCliente.AddCell(cellusoCfdiN)
        tablaDatosCliente.AddCell(cellUsoCFDI)

        'Propiedades de la tabla fecha------------------------------------
        'tablaFecha.SetWidths(anchoFecha) 'Ajusta el tamaño de cada columna
        tablaFecha.AddCell(cellFecha3) 'Agrega COLUMNA
        'tablaFecha.AddCell(cellFecha) ''Agega COLUMNA
        'tablaFecha.AddCell(cellFecha2) 'Agrega COLUMNA
        '-----------------------------------------------------------------

        '------------------------------------------------------------------------
        'Propiedades de la tabla venta--------------------------------------  
        tablaVenta.DefaultCell.BorderColor = colorHeaders
        tablaVenta.AddCell(cellFecha)
        tablaVenta.AddCell(cellFecha2)
        tablaVenta.AddCell(cellFechaVenta) 'Agrega COLUMNA
        tablaVenta.AddCell(cellFechaVenta2) 'Agrega COLUMNA
        tablaVenta.AddCell(cellCondicionesVenta) 'Agrega COLUMNA
        tablaVenta.AddCell(cellCondicionesVenta2) 'Agrega COLUMNA
        tablaVenta.AddCell(cellFormaPago) 'Agrega COLUMNA
        tablaVenta.AddCell(cellFormaPago1) 'Agrega COLUMNA

        '------------------------------------------------------------------------------
        'Propiedades de la tabla factura------------------------------------
        tablaFolio.AddCell(cellFactrua) 'Agrega COLUMNA
        tablaFolio.AddCell(cellFolio) 'Agrega COLUMNA
        tablaFolio.AddCell(cellFolio2) 'Agrega COLUMNA
        '------------------------------------------------------------------------------
        'Propiedades de la tabla empresa------------------------------------
        tablaEmpresa.AddCell(cellEmpresa) 'Agrega COLUMNA
        tablaEmpresa.AddCell(cellDireccion) 'Agrega COLUMNA
        tablaEmpresa.AddCell(cellDireccion2) 'Agrega COLUMNA
        tablaEmpresa.AddCell(cellDireccion3) 'Agrega COLUMNA
        tablaEmpresa.AddCell(cellDireccion4) 'Agrega COLUMNA
        '------------------------------------------------------------------------------

        'Cedas de la tabla fiscal------------------------------------------------
        Dim cellfiscal1 As New PdfPCell(tablaUUID)
        Dim cellfiscal2 As New PdfPCell(New Phrase(""))
        Dim cellfiscal3 As New PdfPCell(TablaSello)
        '----------------------------------------------------------------------
        'celdas de la tabla empresa-------------------------------------------------------
        Dim cell As New PdfPCell(logo) 'celda para guardar el logo tipo
        Dim cell2 As New PdfPCell(tablaEmpresa) 'celda para guardar la empresa
        Dim cell3 As New PdfPCell(tablaFolio) 'celda para guardar la tabla folio
        Dim cell4 As New PdfPCell(tablaFecha) 'celda para guardar la tabla fehca
        Dim cellCliente1 As New PdfPCell(tablaDatosCliente) 'celda para guardar la tabla cliente
        Dim cellCliente2 As New PdfPCell(tablaVenta) 'celda para guardar la tabla ventas
        Dim cellconsignatario As New PdfPCell(tablaConsignatario) 'celda para guardar la tabla cliente
        Dim cellmoneda As New PdfPCell(tablaMoneda) 'celda para guardar la tabla ventas
        '------------------------------------------------------------------------------
        'Propiedades de las celdas de la tabla fiscal------------------------------------
        cellfiscal1.HorizontalAlignment = Rectangle.ALIGN_CENTER
        cellfiscal1.VerticalAlignment = Rectangle.ALIGN_MIDDLE
        cellfiscal1.Border = Rectangle.NO_BORDER
        cellfiscal2.Border = Rectangle.NO_BORDER
        cellfiscal3.Border = Rectangle.NO_BORDER
        '----------------------------------------------------------------------------------
        'Propiedades de las celdas de la tabla cliente------------------------------------
        cellCliente1.BackgroundColor = New BaseColor(255, 255, 255) 'fondo de la tabla
        cellCliente1.BorderColor = colorHeaders 'Color del borde
        cellCliente1.Border = Rectangle.TOP_BORDER + Rectangle.LEFT_BORDER + Rectangle.RIGHT_BORDER
        cellCliente2.BackgroundColor = New BaseColor(255, 255, 255) 'fondo de la tabla
        cellCliente2.BorderColor = colorHeaders 'Color del borde
        cellCliente2.Border = Rectangle.TOP_BORDER + Rectangle.LEFT_BORDER + Rectangle.RIGHT_BORDER

        cellconsignatario.BackgroundColor = New BaseColor(255, 255, 255) 'fondo de la tabla
        cellconsignatario.BorderColor = colorHeaders 'Color del borde
        cellconsignatario.Border = Rectangle.LEFT_BORDER + Rectangle.RIGHT_BORDER

        cellmoneda.BackgroundColor = New BaseColor(255, 255, 255) 'fondo de la tabla
        cellmoneda.BorderColor = colorHeaders 'Color del borde
        cellmoneda.Border = Rectangle.LEFT_BORDER + Rectangle.RIGHT_BORDER
        '---------------------------------------------------------------------------
        'Propiedades de las celdas de la tabla encabezado------------------------------------
        cell.Border = Rectangle.NO_BORDER 'ocultar borldes
        cell2.Border = Rectangle.NO_BORDER 'ocultar borldes
        cell3.Border = Rectangle.NO_BORDER 'ocultar borldes
        cell4.Border = Rectangle.BOTTOM_BORDER 'mostrar borldes seleccionados
        cell4.Colspan = 3 'Unir columnas
        'Celdas tipo
        Dim cellTipo As PdfPCell = New PdfPCell(New Phrase("FACTURA", fontArialB))
        Dim cellTipo2 As PdfPCell = New PdfPCell(New Phrase("FOLIO: " + cUtilerias.folioFactura, fontArialB))
        'propiedades de las celdas
        cellTipo.Border = Rectangle.NO_BORDER
        cellTipo.BackgroundColor = colorHeaders
        cellTipo2.Border = Rectangle.NO_BORDER
        cellTipo2.BackgroundColor = colorHeaders
        cellTipo2.HorizontalAlignment = Rectangle.ALIGN_RIGHT

        '------------------------------------------------------------------------------  
        'Propiedad de la tabla tipo
        tablaTipo.SetWidths(anchoTipo)
        tablaTipo.AddCell(cellTipo)
        tablaTipo.AddCell(cellTipo2)
        'Propiedades de la tabla encabezado------------------------------------
        tablaEncabezado.SetWidths(anchoDatos) 'Ajusta el tamaño de cada columna
        tablaEncabezado.AddCell(cell) 'Agrega COLUMNA
        tablaEncabezado.AddCell(cell2) 'Agrega COLUMNA
        'tablaEncabezado.AddCell(cell3) 'Agrega COLUMNA
        'tablaEncabezado.AddCell(cell4) 'Agrega COLUMNA        
        '---------------------------------------------------------------------
        'Propiedades de la tabla cliente---------------------------------------
        tablaMetodoPago.SetWidths(anchoMetodoPago) 'Ajusta el tamaño de cada columna
        tablaMetodoPago.AddCell(cellMetodoPago) 'Agrega COLUMNA
        tablaMetodoPago.AddCell(cellMetodoPago2) 'Agrega COLUMNA
        tablaMetodoPago.AddCell(cellNCuenta) 'Agrega COLUMNA
        tablaMetodoPago.AddCell(cellNCuenta2) 'Agrega COLUMNA
        tablaMetodoPago.AddCell(cellCondicionesPago) 'Agrega COLUMNA
        tablaMetodoPago.AddCell(cellCondicionesPago2) 'Agrega COLUMNA
        tablaMetodoPago.AddCell(cellOCompra) 'Agrega COLUMNA
        tablaMetodoPago.AddCell(cellOCompra2) 'Agrega COLUMNA
        'tablaMetodoPago.AddCell(cellMetodoPagoVacia) 'Agrega COLUMNA
        '--------------------------------------------------------------------
        'Propiedades de la tabla cliente---------------------------------------
        tablaCliente.DefaultCell.BorderColor = colorHeaders
        tablaCliente.SetWidths(anchoCliente) 'Ajusta el tamaño de cada columna
        tablaCliente.AddCell(cellCliente1) 'Agrega COLUMNA
        tablaCliente.AddCell(cellCliente2) 'Agrega COLUMNA
        tablaCliente.AddCell(cellconsignatario) 'Agrega COLUMNA
        tablaCliente.AddCell(cellmoneda) 'Agrega COLUMNA
        '--------------------------------------------------------------------
        'Propiedades de la tabla totales---------------------------------------------------
        tablaTotales.SetWidths(anchoTotales) 'Ajusta el tamaño de cada columna
        tablaTotales.AddCell(cellImporteLetra) 'Agrega COLUMNA
        tablaTotales.AddCell(cellSubtotal) 'Agrega COLUMNA
        tablaTotales.AddCell(cellSubtotal2) 'Agrega COLUMNA
        tablaTotales.AddCell(celltotalLetra) 'Agrega COLUMNA
        tablaTotales.AddCell(celliva) 'Agrega COLUMNA
        tablaTotales.AddCell(celliva2) 'Agrega COLUMNA
        tablaTotales.AddCell(cellretencion) 'Agrega COLUMNA
        tablaTotales.AddCell(cellretencion2) 'Agrega COLUMNA
        If totalAnt > 0 Then
            tablaTotales.AddCell(cellAnticipoV) 'Agrega COLUMNA
            tablaTotales.AddCell(cellAnticipo) 'Agrega COLUMNA
            tablaTotales.AddCell(cellAnticipo2) 'Agrega COLUMNA
        End If
        tablaTotales.AddCell(cellFirma) 'Agrega COLUMNA
        tablaTotales.AddCell(cellTotal) 'Agrega COLUMNA
        tablaTotales.AddCell(cellTotal2) 'Agrega COLUMNA
        tablaTotales.AddCell(cellvaciaTotales) 'Agrega COLUMNA
        '------------------------------------------------------------------
        'Propiedades de la tabla fiscal------------------------------------------
        tablaFiscal.SetWidths(anchoFiscal) 'Ajusta el tamaño de cada columna
        tablaFiscal.AddCell(cellfiscal1) 'Agrega COLUMNA
        tablaFiscal.AddCell(cellfiscal2) 'Agrega COLUMNA
        tablaFiscal.AddCell(cellfiscal3) 'Agrega COLUMNA
        '---------------------------------------------------------------------
        'Propiedades del documeto
        document.Add(tablaEncabezado) ' Agrega la tabla al documento
        document.Add(tablaTipo) 'Agregar tabla tipo de pdf
        document.Add(tablaCliente) ' Agrega la tabla al documento
        document.Add(tablaMetodoPago) ' Agrega la tabla al documento
        'document.Add(tablaFecha)
        document.Add(tablaArticulos) ' Agrega la tabla al documento
        If tipoDocumento = "Nota de Credito" Then
            document.Add(tablaRelacion) 'agrega la tabla relación
        End If
        If numPedimento = "1" Then
            document.Add(tablaPedimento)
        End If
        If totalAnt > 0 Then
            document.Add(tablaAnt) ' Agrega la tabla al documento
        End If
        ' 
        If vistaRem = "1" Then
            document.Add(tablaRemision) ' Agrega la tabla al documento
        End If
        document.Add(tablaObservaciones) ' Agrega la tabla al documento
        document.Add(tablaTotales) 'Agregar la tabla al documento
        document.Add(tablaFiscal) 'Agregr la tabla fiscal
        '------------------------------------------------------------------

        'esto es importante, pues si no cerramos el document entonces no se creara el pdf.
        document.Close()
    End Function
End Class
