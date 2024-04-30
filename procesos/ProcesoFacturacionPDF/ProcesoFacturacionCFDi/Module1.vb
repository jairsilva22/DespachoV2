Imports System.IO

Module Module1
    Dim cPDF As New classPDF
    Dim idFactura As String
    Dim aTimer As New System.Timers.Timer 'Variable para guardar el timer
    Public cUtilerias As New utilerias 'Variable para guardar las utilerias
    Private Sub Facturar(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs)
        Try
            cUtilerias.cadenaConexion = cUtilerias.configuraciones("CADENA")
            cUtilerias.fuentes = cUtilerias.configuraciones("FUENTES")
            Dim logFacturas As DataTable = cUtilerias.consultas("SELECT * FROM dbo.factura WHERE generaPDF = 'True' and (estatus = 'Facturada' or estatus = 'Cancelada') and timbre = 'NO' ORDER BY idfactura ASC", cUtilerias.cadenaConexion) 'Variable para traer el archivo

            'if para validar que tiene facturas para procesar        
            If logFacturas.Rows.Count <> 0 Then 'logFacturas  
                Console.WriteLine("Comenzando el proceso de facturaciòn")
                Console.WriteLine("e-ControCFDi V1.0" + DateTime.Now)
                aTimer.Enabled = False


                Dim folio As String
                Dim num As Integer = 1
                Dim ruta As String
                Dim archivo As String
                Dim archivoPDF As String
                Dim archivoXML As String
                Dim carpeta As String
                Dim configuraciones As DataTable = cUtilerias.consultas("SELECT * FROM dbo.configmenor WHERE idempresa = " & logFacturas.Rows(0)("idempresa"), cUtilerias.cadenaConexion) 'Variable para traer las configuraciones
                Dim empresa As DataTable = cUtilerias.consultas("SELECT Ncarpeta, carpetaTimbre, imagenUUID, anchoUUID, altoUUID, Ncarpeta, rfc, nombre, razon, calle, codigoPostal, pais.pais, estado.estado, ciudad.ciudad, colonia, numero as noExterior,c_RegimenFiscal, logo, ancho, alto, registroPatronal FROM dbo.sucursales as empresas, dbo.pais, dbo.estados as estado, dbo.ciudades as ciudad WHERE ciudad.id = empresas.idciudad AND estado.id = empresas.idestado AND pais.idpais = empresas.idpais AND empresas.id = " & logFacturas.Rows(0)("idempresa"), cUtilerias.cadenaConexion) 'Variable para infromacion de la empresa                    
                Dim facturas As DataTable = cUtilerias.consultas("SELECT Prueba, estatus, terminos, total, iva, subtotal, retencion, metodoPago, condicionesDePago, idfactura,  factura.serie, folio, abreviatura, fechacfd,fechasellado, factura.fechaalta as fechaalta, moneda.cmoneda as moneda, metodoPago, embarque, NumCtaPago, ordenCompra, lugarExpedicion, idcliente, documento.descripcion as documento, obsCliente, numero_certificado, cadena_original, factura.serie, forma_pago, vendedor, factura.idusuario, asn FROM dbo.factura, dbo.moneda, dbo.documento WHERE documento.iddocumento = factura.tipo_comprobante AND moneda.idmd = factura.moneda AND (estatus = 'Facturada' or estatus = 'Cancelada') AND idfactura = " & logFacturas.Rows(0)("idfactura"), cUtilerias.cadenaConexion) 'Variable para guardar las facturas para procesar
                Dim facturasLog As DataTable = cUtilerias.consultas("SELECT * FROM dbo.log_idCO WHERE id_factura = " & logFacturas.Rows(0)("idfactura"), cUtilerias.cadenaConexion) 'Variable para guardar las facturas para procesar        

                'if para valida que tiene datos
                If configuraciones.Rows.Count <> 0 Then 'configuraciones
                    cUtilerias.impuesto = configuraciones.Rows(0)("iva").ToString
                    archivo = configuraciones.Rows(0)("path").ToString
                    ruta = configuraciones.Rows(0)("path").ToString
                    carpeta = configuraciones.Rows(0)("path").ToString
                End If 'configuraciones


                'if para valida que tiene datos
                If empresa.Rows.Count <> 0 Then 'configuraciones

                    ' No encuentra la rutaa en esta parte!!!!

                    cUtilerias.imagenUUID = empresa.Rows(0)("imagenUUID").ToString
                    cUtilerias.anchoUUID = empresa.Rows(0)("anchoUUID").ToString
                    cUtilerias.altoUUID = empresa.Rows(0)("altoUUID").ToString
                    'cUtilerias.lugarExpedicion = empresa.Rows(0)("lugarExpedicion").ToString
                    cUtilerias.altoLogoEmpresa = empresa.Rows(0)("alto").ToString
                    cUtilerias.anchoLogoEmpresa = empresa.Rows(0)("ancho").ToString
                    cUtilerias.logoEmpresa = empresa.Rows(0)("logo").ToString
                    cUtilerias.NombreEmpres = empresa.Rows(0)("razon").ToString
                    cUtilerias.direccionEmpresa = empresa.Rows(0)("calle").ToString & " # " & empresa.Rows(0)("noExterior").ToString & " " & empresa.Rows(0)("colonia").ToString
                    cUtilerias.direccionEmpresa2 = empresa.Rows(0)("ciudad").ToString & " " & empresa.Rows(0)("estado").ToString & " C.P." & empresa.Rows(0)("codigoPostal").ToString & " " & empresa.Rows(0)("pais").ToString
                    If empresa.Rows(0)("c_RegimenFiscal").ToString = "601" Then
                        cUtilerias.direccionEmpresa3 = "Régimen Fiscal: General de Ley Personas Morales"
                    Else
                        cUtilerias.direccionEmpresa3 = "Régimen Fiscal: " & empresa.Rows(0)("c_RegimenFiscal").ToString
                    End If
                    cUtilerias.direccionEmpresa4 = "R.F.C.: " & empresa.Rows(0)("rfc").ToString & "    Registro Patronal: " & empresa.Rows(0)("registroPatronal").ToString
                    archivo = archivo & empresa.Rows(0)("carpetaTimbre").ToString & "\"
                    ruta = ruta & empresa.Rows(0)("carpetaTimbre").ToString & "\"
                    carpeta = carpeta & empresa.Rows(0)("Ncarpeta").ToString
                    cUtilerias.UUID = "?re=" & cUtilerias.RFCUUID(empresa.Rows(0)("rfc").ToString)
                End If 'configuraciones

                'if para validar que tiene datos

                If facturasLog.Rows(0)("nombre_archivo").ToString <> "" Then 'nombre_archivo

                    'archivoXML = archivo & Year(Today) & "_" & facturas.Rows(0)("folio").ToString & facturas.Rows(0)("serie").ToString & "_" & facturas.Rows(0)("abreviatura").ToString & "_" & facturas.Rows(0)("idfactura").ToString & "CO.xml"
                    archivoXML = archivo & facturasLog.Rows(0)("nombre_archivo").ToString
                    Console.WriteLine("Procesando Normal " & archivoXML)

                    'archivoPDF = archivo & Year(Today) & "_" & facturas.Rows(0)("folio").ToString & facturas.Rows(0)("serie").ToString & "_" & facturas.Rows(0)("abreviatura").ToString & "_" & facturas.Rows(0)("idfactura").ToString & "CO.pdf"
                    archivoPDF = archivo & facturasLog.Rows(0)("archivo_pdf").ToString
                Else
                    Console.WriteLine("sin nombre----->" & facturasLog.Rows(0)("archivo_pdf").ToString)
                End If 'nombre_archivo

                'if para validar que tiene datos
                ' si le hago hasta aqui no me funciona y me marca el error de la conversion de la cadena "" en el tipo 'date' no es valida

                If facturas.Rows.Count <> 0 Then 'facturas
                    Dim cliente As DataTable = cUtilerias.consultas("SELECT rfcCliente, nombreCliente, nombreEmpresa, calleCliente, noExterior, noInterior, colonia, pais.pais, estado.estado, ciudad.ciudad, codigoPostalCliente, telefonoCliente, usoCFDI, truncar, isr, retencion, terminoPago FROM dbo.clientesFacturacion as clientes, dbo.pais, dbo.estados as estado, dbo.ciudades as ciudad WHERE ciudad.id = clientes.ciudadCliente AND estado.id = clientes.estadoCliente AND pais.idpais = clientes.paisCliente AND idCliente = " & facturas.Rows(0)("idcliente").ToString, cUtilerias.cadenaConexion) 'Variable para guardar las facturas para procesar        

                    'if para validar que tiene retencion
                    If facturas.Rows(0)("retencion").ToString < 0 Then 'retencion
                        cUtilerias.retencion = 16
                    Else 'retencion
                        cUtilerias.retencion = 0
                    End If 'retencion
                    'if para validar el tipo de moneda
                    If facturas.Rows(0)("moneda").ToString = "USD" Then 'moneda
                        cUtilerias.monedaTexto = "DóLARES"
                    Else 'moneda
                        cUtilerias.monedaTexto = "PESOS"
                    End If 'moneda
                    cUtilerias.cadnaOriginal = facturas.Rows(0)("cadena_original").ToString
                    cUtilerias.CDS = facturas.Rows(0)("numero_certificado").ToString
                    cUtilerias.folioFactura = facturas.Rows(0)("serie").ToString & " 00" & facturas.Rows(0)("folio").ToString
                    cUtilerias.Total = cUtilerias.truncarAdos(facturas.Rows(0)("total").ToString)
                    cUtilerias.iva = cUtilerias.truncarAdos(facturas.Rows(0)("iva").ToString)
                    cUtilerias.subTotal = cUtilerias.truncarAdos(facturas.Rows(0)("subtotal").ToString)
                    ' cUtilerias.condicionesPago = facturas.Rows(0)("condicionesDePago").ToString
                    cUtilerias.numeroCuenta = facturas.Rows(0)("NumCtaPago").ToString
                    cUtilerias.metodoPago = facturas.Rows(0)("metodoPago").ToString
                    cUtilerias.asn = facturas.Rows(0)("asn").ToString
                    Dim tipo As DataTable = cUtilerias.consultas("SELECT descripcion FROM formaPagoSat WHERE codigo = '" & cUtilerias.metodoPago & "'", cUtilerias.cadenaConexion)
                    If tipo.Rows.Count <> 0 Then
                        cUtilerias.metodoPago = cUtilerias.metodoPago & "-" & tipo.Rows(0)("descripcion")
                    End If

                    cUtilerias.ordenCompra = "  "
                    cUtilerias.ordenCompra = facturas.Rows(0)("ordenCompra").ToString
                    cUtilerias.lugarExpedicion = facturas.Rows(0)("lugarExpedicion").ToString
                    cUtilerias.moneda = facturas.Rows(0)("moneda").ToString
                    cUtilerias.consignatario = facturas.Rows(0)("embarque").ToString
                    cUtilerias.fechaVenta = facturas.Rows(0)("fechaalta").ToString
                    cUtilerias.fechaCfD = facturas.Rows(0)("fechasellado").ToString
                    cUtilerias.comprobante = UCase(facturas.Rows(0)("documento").ToString)
                    idFactura = facturas.Rows(0)("idfactura").ToString

                    'validar si es una NC o factura para las observaciones
                    cUtilerias.obsCliente = "   "
                    If facturas.Rows(0)("documento").ToString = "Nota de Credito" Then
                        'consultar el vendedor de la factura y el de la NC
                        Dim usuario As DataTable = cUtilerias.consultas("SELECT nombre FROM dbo.usuarios WHERE idusu = " & facturas.Rows(0)("idusuario").ToString, cUtilerias.cadenaConexion) 'Variable para guardar las facturas para procesar        
                        cUtilerias.obsCliente = "Facturo " + facturas.Rows(0)("vendedor").ToString + ", NC " + Trim(usuario.Rows(0)("nombre").ToString) + ". " + Replace((Replace((Trim(facturas.Rows(0)("obsCliente").ToString)), "\n", "")), "\r", "")
                    Else
                        cUtilerias.obsCliente = Replace((Replace((Trim(facturas.Rows(0)("obsCliente").ToString)), "\n", "")), "\r", "")
                    End If

                    cUtilerias.hayretencion = facturas.Rows(0)("retencion")
                    cUtilerias.formaPago = facturas.Rows(0)("forma_pago").ToString
                    cUtilerias.estatus = facturas.Rows(0)("estatus").ToString

                    If (cUtilerias.formaPago) = "PAGO EN UNA SOLA EXHIBICION" Or (cUtilerias.formaPago) = "1" Then
                        cUtilerias.formaPago = "PUE"
                    ElseIf (cUtilerias.formaPago) = "PAGO EN PARCIALIDADES O DIFERIDO" Or (cUtilerias.formaPago) = "2" Then
                        cUtilerias.formaPago = "PPD"
                    End If

                    Dim CondP As DataTable = cUtilerias.consultas("Select descripcion FROM formPago WHERE forma_Pago = '" & cUtilerias.formaPago & "'", cUtilerias.cadenaConexion)
                    If CondP.Rows.Count <> 0 Then
                        cUtilerias.condicionesPago = CondP.Rows(0)("descripcion")
                    End If


                    'if para validar que tiene datos
                    If cliente.Rows.Count <> 0 Then 'cliente
                        ' si inhabilito esta parte no me encuentra la ruta de acceso!!!! asi que aqui no hay que modificar
                        cUtilerias.paisCliente = cliente.Rows(0)("pais").ToString
                        cUtilerias.codigoPostalCliente = cliente.Rows(0)("codigoPostalCliente").ToString
                        cUtilerias.rfcCliente = Replace(cliente.Rows(0)("rfcCliente").ToString, ".", "")
                        cUtilerias.telefonoCliente = cliente.Rows(0)("telefonoCliente").ToString
                        cUtilerias.EstadoCliente = cliente.Rows(0)("estado").ToString
                        cUtilerias.CiudadCliente = cliente.Rows(0)("ciudad").ToString
                        cUtilerias.coloniaCliente = cliente.Rows(0)("colonia").ToString
                        cUtilerias.direccionCliente = cliente.Rows(0)("calleCliente").ToString & " No. " & cliente.Rows(0)("noExterior").ToString & " - " & cliente.Rows(0)("noInterior").ToString
                        cUtilerias.nombreCliente = cliente.Rows(0)("nombreEmpresa").ToString
                        cUtilerias.UUID = cUtilerias.UUID & "&rr=" & Replace(cUtilerias.RFCUUID(Replace(cliente.Rows(0)("rfcCliente").ToString, "-", "")), ".", "")
                        cUtilerias.usoCFDI = cliente.Rows(0)("usoCFDI").ToString()
                        cUtilerias.truncar = cliente.Rows(0)("truncar").ToString()
                        cUtilerias.hayisr = cliente.Rows(0)("isr").ToString

                        cUtilerias.condicionesVenta = cliente.Rows(0)("terminoPago").ToString + " " + facturas.Rows(0)("terminos").ToString
                        ' cUtilerias.hayretencion = cliente.Rows(0)("retencion")

                        Dim usoCfdiDesc As DataTable = cUtilerias.consultas("SELECT * FROM dbo.usoCfdi WHERE ClaveSat = '" & Trim(cliente.Rows(0)("usoCFDI").ToString()) & "'", cUtilerias.cadenaConexion) 'Variable para guardar las facturas para procesar        

                        If usoCfdiDesc.Rows.Count <> 0 Then
                            cUtilerias.usoCFDI = cliente.Rows(0)("usoCFDI").ToString() & " - " & usoCfdiDesc.Rows(0)("descripcion").ToString()
                        End If

                    End If 'cliente

                    cUtilerias.UUID = cUtilerias.UUID & "&tt=" & cUtilerias.TotalUUID(cUtilerias.Total)

                    If File.Exists(archivoXML) Then
                        cUtilerias.LeerXML(archivoXML)
                        'Else
                        '    num = 1
                        '    While (File.Exists(archivo & Year(Today) & "_" & (CInt(logFacturas.Rows(0)("folio").ToString) - num) & logFacturas.Rows(0)("serie").ToString & "_" & logFacturas.Rows(0)("abreviatura").ToString & "_" & logFacturas.Rows(0)("idfactura").ToString & "CO.xml") = False)
                        '        num += 1
                        '    End While

                        '    'renombrar el xml
                        '    My.Computer.FileSystem.RenameFile(archivo & Year(Today) & "_" & (CInt(logFacturas.Rows(0)("folio").ToString) - num) & logFacturas.Rows(0)("serie").ToString & "_" & logFacturas.Rows(0)("abreviatura").ToString & "_" & logFacturas.Rows(0)("idfactura").ToString & "CO.xml", Year(Today) & "_" & (CInt(logFacturas.Rows(0)("folio").ToString)) & logFacturas.Rows(0)("serie").ToString & "_" & logFacturas.Rows(0)("abreviatura").ToString & "_" & logFacturas.Rows(0)("idfactura").ToString & "CO.xml")

                        '    cUtilerias.LeerXML(archivoXML)
                    End If

                    Console.WriteLine("")
                        Console.WriteLine("Generando PDF")
                        cPDF.llenarPDF(ruta, archivoPDF, facturas.Rows(0)("idfactura").ToString, carpeta)
                        Console.WriteLine("Actualizar datos")
                        cUtilerias.EjecutaQuery("UPDATE dbo.factura SET generaPDF = 0, imprimir = 'SI', UUID = '" & cUtilerias.UUID & "', selloCFD = '" & cUtilerias.selloCFD & "', noCertificadoSAT = '" & cUtilerias.noCertificadoSAT & "', selloSAT = '" & cUtilerias.SELLO & "', FechaTimbrado = '" & cUtilerias.FechaTimbrado & "' WHERE idfactura = " & facturas.Rows(0)("idfactura").ToString, cUtilerias.cadenaConexion)
                    End If 'facturas
                    Console.WriteLine("Fin del proceso")
                    Console.WriteLine("")
                    aTimer.Enabled = True
                End If 'logFacturas  
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Console.WriteLine(ex.StackTrace)
            Console.WriteLine("Error")
            cUtilerias.EjecutaQuery("UPDATE dbo.factura SET generaPDF = 0 WHERE idfactura = " & idFactura, cUtilerias.cadenaConexion)

            Console.WriteLine("Fin del proceso")
            Console.WriteLine("")
            aTimer.Enabled = True
        End Try
    End Sub

    Sub Main()
        aTimer.AutoReset = True
        aTimer.Interval = 5000 '10 seconds 
        AddHandler aTimer.Elapsed, AddressOf Facturar
        aTimer.Start()
        Console.WriteLine("Presiona Enter para Salir del programa.")
        Console.ReadLine()
    End Sub

End Module
