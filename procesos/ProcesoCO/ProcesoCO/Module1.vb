Imports System.IO
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions

Module Module1
    Dim aTimer As New System.Timers.Timer 'Variable para guardar el timer
    Public cUtilerias As New utilerias 'Variable para guardar las utilerias

    Private Sub CadenaOrinal(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs)
        Try
            Dim factura As DataTable = cUtilerias.consultas("SELECT retencion, folio, ISNULL(tasa,0) AS tasa, idfactura, total, subtotal, fechacfd, forma_Pago, condicionesDePago, moneda.cMoneda as moneda, moneda.tcambio as tCambio, iva, documento.tipo, documento.efecto, metodoPago, NumCtaPago, idcliente, factura.serie, factura.idempresa, motivoTraslado, CertificadoOrigen, ClaveDePedimento, Incoterm, Subdivision, TipoCambioUSD, TotalUSD, TipoOperacion, NumCertificadoOrigen, NumeroExportadorConfiable, Observaciones, numRegIdTrib, nombreDestino, numRegIdTribDestino, municipioDestino, estadoDestino, codigoPostalDestino, calleDestino, exteriorDestino, interiorDestino, coloniaDestino, localidadDestino, referenciaDestino, paisDestino, ISNULL(ASN,'NO') AS ASN FROM dbo.factura, dbo.moneda, dbo.documento WHERE documento.iddocumento = factura.tipo_comprobante AND moneda.idmd = factura.moneda AND estatus = 'ProcesoCo' and total <> 0 ORDER BY idfactura ASC", cUtilerias.cadenaConexion) 'Variable para consultar la factura                  

            'if para validar que tiene datos
            If factura.Rows.Count <> 0 Then 'factura                
                Dim tasa 'Variable para guardar la tasa de iva    
                Dim calleCliente 'Variable para guardar la calle del cliente
                Dim noExteriorCliente 'Variable para guardar el numero exterior postal del cliente
                Dim coloniaCliente 'Variable para guardar la colonia postal del cliente
                Dim localidadCliente 'Variable para guardar la localidad postal del cliente
                Dim municipioCliente 'Variable para guardar el municipio postal del cliente
                Dim estadoCliente 'Variable para guardar el estado postal del cliente
                Dim paisCliente 'Variable para guardar el pais postal del cliente
                Dim codigoPostalCliente 'Variable para guardar el codigo postal del cliente
                Dim rfcCliente 'Variable para guardar el rfc del cliente
                Dim nombreCliente 'Variable para guardar el nombre del cliente
                Dim idcliente 'Variable para guardar el cliente
                Dim vRegimen 'Variable para guardar el regimen fiscal
                Dim vcodigoPostal 'Variable para guardar el codigo postal
                Dim vpais 'Variable para guardar el pais
                Dim vestado 'Variable para guardar el estado
                Dim vmunicipio 'Variable para guardar el municipio
                Dim vlocalidad 'Variable para guardar la localidad
                Dim vcolonia 'Variable para guardar la colonia 
                Dim vnoExterior 'Variable para guardar el numero exterior  
                Dim vCalle 'Variable para gudar la caller de la empresa
                'Dim vLugarExpedicion 'Variable para guardar el lugar de expedidicon
                Dim nombre 'Variable para guardar el nombre de la empresa
                Dim rfc 'Variable para guardar el RFC de la empresa
                Dim NumCtaPago 'Variable para guardar el numero de cuenta
                Dim LugarExpedicion 'Variable para guardar el lugar de expedicion
                Dim metodoDePago 'Variable para guardar el metodo de pago
                Dim tipoDeComprobante 'Variable para guardar el timpo de comprobante
                Dim ivaFactura As Double 'Variable para guardar el iva de la factura
                Dim retencion 'Variable para guardar la retencion
                Dim aRetencion 'Variable para guardar la retencion
                Dim total As Double 'Variable para guardar el total
                Dim tCambio 'Variable para guardar el tipo de cambio
                Dim moneda 'Variable para guardar la moneda
                Dim subTotal As Double 'Variable para guadar el subtotal
                Dim condicionesDePago 'Variable para guardar las condiciones de pago
                Dim formaDePago 'Variable para guardar la forma de pago
                Dim carpeta ''Variable para guardar la carpeta de los CO
                Dim idFactura 'Variable para guardar el iva de la factura
                Dim fecha 'Variable para guarar la fecha con nomenclatura de CFD
                Dim mes_arr 'Variable para guardar el mes
                Dim dia_arr 'Variable para guardar el dia
                Dim fechaCfd 'Variable para guardar la fechaCfd
                Dim miXml As String 'Variable para guardar el texto del XML 
                Dim iva As String 'Variable para guardar el iva
                Dim pathconfi As String 'Variable para guardar el path del sistema
                Dim nomArchivo As String 'Variable apra guardar el nombre del archivo
                Dim idFolios As Integer 'Variable para guardar el id de los folios
                Dim folio As String 'Variable para guardar el folio
                Dim nParte As String 'Variable para guardar el numero de parte
                Dim totalConcepto 'Variable para guardar el descuento
                Dim efecto 'variable para guardar el efecto del tipo de comprobante
                Dim usoCFDI ' variable para guardar el uso de cfdi
                Dim impuesto As Double 'variable par guardar la clave del impuesto total
                Dim nParteP ' variable para guardar nparte del producto
                Dim precioUnitario 'variable para guardar el tipo factor del traslado total
                Dim residenciaFis 'variable para guardar la residencia fiscal
                Dim numRegIdTrib 'vraiable para guradar el numregidtrib
                Dim c_unidad 'variable para guardar la clave de la unidad
                Dim retencionCte 'variable para guardar la retencion del cliente
                Dim tazaIva 'variable para guardar el iva del cliente
                Dim serie As String 'Variable para guardar el serie                
                Dim truncar As String = ""
                Dim noParte() As String
                Dim descripcion As String
                Dim totalImpuestosTrasladados As Double
                Dim hayretencion As String
                Dim hayIsr As String
                Dim isr As Double = 0
                Dim totalRetencion As Double = 0
                Dim cteRetencion As String
                Dim isrConcepto As Double = 0
                Dim base As Double = 0
                Dim unidad
                Dim ASN As String = ""
                Dim totalVta As Integer
                Dim totalFac As Integer
                '/////Comercio Exterior/////
                Dim certificadoOrigen As String = ""
                Dim claveDePedimento As String = ""
                Dim numCertificadoOrigen As String = 0
                Dim numExportadorConfiable As String = ""
                Dim incoterm As String = ""
                Dim subdivision As String = ""
                Dim tipoCambioUsd As String = ""
                Dim tipoOperacion As String = ""
                Dim totalUsd As String = ""
                Dim observaciones As String = ""
                Dim estadoComExt As String = ""
                Dim ciudadComExt As String = ""
                Dim xmlComExt As String = ""
                Dim nombreDestino As String = ""
                Dim numRegIdTribDestino As String = ""
                Dim paisDestino As String = ""
                Dim municipioDestino As String = ""
                Dim estadoDestino As String = ""
                Dim codigoPostalDestino As String = ""
                Dim calleDestino As String = ""
                Dim exteriorDestino As String = ""
                Dim interiorDestino As String = ""
                Dim coloniaDestino As String = ""
                Dim localidadDestino As String = ""
                Dim referenciaDestino As String = ""
                Dim motivoTraslado As String = ""
                Dim numPedimento As String = ""

                ASN = factura.Rows(0)("ASN").ToString


                cUtilerias.EjecutaQuery("UPDATE dbo.detFactura SET descuento=0 WHERE descuento is NULL", cUtilerias.cadenaConexion) 'Actualizar factur

                'Dim empresa As DataTable = cUtilerias.consultas("SELECT lugarExpedicion, rfc, nombre, calle, codigoPostal, pais.descripcion AS paisComExt, estado.descripcion AS estadoComExt, ciudad.descripcion AS ciudadComExt, colonia, coloniaComExt, noExterior, c_regimenFiscal, Ncarpeta FROM dbo.empresas, dbo.pais, dbo.estado, dbo.ciudad WHERE ciudad.idciudad = empresas.ciudad AND estado.idestado = empresas.estado AND pais.idpais = empresas.pais AND idEmpresa = " & factura.Rows(0)("idempresa").ToString, cUtilerias.cadenaConexion) 'Variable para consultar la empresa
                Dim empresa As DataTable = cUtilerias.consultas("SELECT rfc, razon AS nombre, calle, cp AS codigoPostal, pais.descripcion AS paisComExt, estado.estado AS estadoComExt, ciudad.ciudad AS ciudadComExt, colonia, coloniaComExt, numero, c_regimenFiscal, Ncarpeta FROM dbo.sucursales AS empresas, dbo.pais, dbo.estados AS estado, dbo.ciudades AS ciudad WHERE ciudad.id = empresas.idCiudad AND estado.id = empresas.idEstado AND pais.idpais = empresas.idPais AND empresas.id = " & factura.Rows(0)("idempresa").ToString, cUtilerias.cadenaConexion) 'Variable para consultar la empresa
                Dim detFactura As DataTable = cUtilerias.consultas("SELECT * FROM dbo.detFactura WHERE id_factura = " & factura.Rows(0)("idfactura").ToString, cUtilerias.cadenaConexion) 'Variable para consultar los articulos de la factura

                'validamos si tiene detalles en factura
                If detFactura.Rows.Count <> 0 Then
                    totalFac = detFactura.Rows.Count
                Else
                    totalFac = 0
                End If
                Dim entra As Boolean
                entra = False
                'validamos si tiene venta 
                If ASN <> "NO" And ASN <> "" Then
                    entra = True
                    'buscamos los detalles de la venta 
                    Dim ventaDet As DataTable = cUtilerias.consultas("SELECT * FROM ventaslog WHERE nventa= '" + ASN + "'", cUtilerias.cadenaConexion)
                    If ventaDet.Rows.Count <> 0 Then

                        totalVta = ventaDet.Rows.Count
                    Else
                        totalVta = 0
                    End If
                End If

                'validamos si coinciden los detalles 

                If totalVta <> totalFac And entra Then
                    Console.WriteLine("Comenzando el proceso de CO")
                    Console.WriteLine("e-ControCFDi V3.3 " + DateTime.Now)
                    Console.WriteLine("Detalles duplicados")
                    cUtilerias.EjecutaQuery("UPDATE dbo.factura SET estatus = 'Error' WHERE idfactura = " & factura.Rows(0)("idfactura").ToString, cUtilerias.cadenaConexion) 'Actualizar factura
                    Console.WriteLine("Fin del proceso")
                    Console.WriteLine("")
                    aTimer.Enabled = True

                Else
                    'si son iguales manda a procesr 
                    ivaFactura = (detFactura.Rows(0)("iva").ToString)

                    Dim configuraciones As DataTable = cUtilerias.consultas("SELECT * FROM dbo.configmenor WHERE idempresa = " & factura.Rows(0)("idempresa").ToString, cUtilerias.cadenaConexion) 'Variable para consultar las configuraciones
                    Dim folios As DataTable = cUtilerias.consultas("SELECT * FROM dbo.folios WHERE idEmpresa = " & factura.Rows(0)("idempresa").ToString & " AND Serie = '" & factura.Rows(0)("serie").ToString & "'", cUtilerias.cadenaConexion) 'Variabla para consultar los folios

                    Console.WriteLine("Comenzando el proceso de CO")
                    Console.WriteLine("e-ControCFDi V3.3 " + DateTime.Now)
                    Console.WriteLine("")
                    aTimer.Stop()

                    cUtilerias.EjecutaQuery("UPDATE dbo.factura SET fechacfd = GETDATE(), estatus = 'Procesando'  WHERE idfactura = " & factura.Rows(0)("idfactura").ToString, cUtilerias.cadenaConexion) 'Actualizar factura

                    moneda = (factura.Rows(0)("moneda").ToString)
                    NumCtaPago = (factura.Rows(0)("NumCtaPago").ToString)

                    tipoDeComprobante = (factura.Rows(0)("tipo").ToString)
                    efecto = (factura.Rows(0)("efecto").ToString)
                    ivaFactura = (factura.Rows(0)("iva").ToString)
                    'consultar el tipo de cambio del dia 
                    Dim mnda As DataTable = cUtilerias.consultas("SELECT id, cambio, cambio.fechaAlta AS fecha, cmoneda, fechaCambio FROM dbo.cambio JOIN moneda ON moneda.idmd = cambio.idMoneda WHERE cMoneda = '" & moneda & "' AND (DAY(fechaCambio) = " & Day(Now()) & " AND MONTH(fechaCambio)= " & Month(Now()) & " AND YEAR(fechaCambio)=" & Year(Now()) & ") ORDER BY fechaCambio DESC", cUtilerias.cadenaConexion)
                    If mnda.Rows.Count > 0 Then 'cliente
                        tCambio = mnda.Rows(0)("cambio").ToString
                    Else
                        tCambio = (factura.Rows(0)("tCambio").ToString)
                    End If

                    condicionesDePago = Trim(factura.Rows(0)("condicionesDePago").ToString)

                    cadenavalidacion = condicionesDePago
                    Validacion()
                    condicionesDePago = cadenavalidacion


                    formaDePago = (factura.Rows(0)("metodoPago").ToString)
                    fechaCfd = Date.Now.ToString("dd/MM/yyyy HH: mm:ss")
                    idcliente = factura.Rows(0)("idcliente").ToString
                    iva = factura.Rows(0)("tasa").ToString
                    nomArchivo = factura.Rows(0)("idfactura").ToString & "CO.xml"
                    folio = factura.Rows(0)("folio").ToString
                    serie = factura.Rows(0)("serie").ToString
                    If factura.Rows(0)("tasa").ToString <> "" Then
                        tazaIva = (factura.Rows(0)("tasa").ToString)
                    Else
                        tazaIva = 0
                    End If


                    If (factura.Rows(0)("forma_pago").ToString) = "PAGO EN UNA SOLA EXHIBICION" Or (factura.Rows(0)("forma_pago").ToString) = "1" Then
                        metodoDePago = "PUE"
                    ElseIf (factura.Rows(0)("forma_pago").ToString) = "PAGO EN PARCIALIDADES O DIFERIDO" Or (factura.Rows(0)("forma_pago").ToString) = "2" Then
                        metodoDePago = "PPD"
                    End If

                    '///Comercio Exterior///
                    motivoTraslado = factura.Rows(0)("motivoTraslado").ToString
                    certificadoOrigen = factura.Rows(0)("CertificadoOrigen").ToString
                    claveDePedimento = factura.Rows(0)("ClaveDePedimento").ToString
                    incoterm = factura.Rows(0)("Incoterm").ToString
                    subdivision = (factura.Rows(0)("Subdivision").ToString)
                    tipoCambioUsd = (factura.Rows(0)("TipoCambioUSD").ToString)
                    totalUsd = (factura.Rows(0)("TotalUSD").ToString)
                    tipoOperacion = (factura.Rows(0)("TipoOperacion").ToString)
                    numCertificadoOrigen = factura.Rows(0)("NumCertificadoOrigen").ToString
                    numExportadorConfiable = factura.Rows(0)("NumeroExportadorConfiable").ToString
                    observaciones = factura.Rows(0)("Observaciones").ToString
                    numRegIdTrib = (factura.Rows(0)("numRegIdTrib").ToString)
                    nombreDestino = factura.Rows(0)("nombreDestino").ToString
                    numRegIdTribDestino = factura.Rows(0)("numRegIdTribDestino").ToString
                    paisDestino = factura.Rows(0)("paisDestino").ToString
                    municipioDestino = factura.Rows(0)("municipioDestino").ToString
                    estadoDestino = factura.Rows(0)("estadoDestino").ToString
                    codigoPostalDestino = factura.Rows(0)("codigoPostalDestino").ToString
                    calleDestino = factura.Rows(0)("calleDestino").ToString
                    exteriorDestino = factura.Rows(0)("exteriorDestino").ToString
                    interiorDestino = factura.Rows(0)("interiorDestino").ToString
                    coloniaDestino = factura.Rows(0)("coloniaDestino").ToString
                    localidadDestino = factura.Rows(0)("localidadDestino").ToString
                    referenciaDestino = factura.Rows(0)("referenciaDestino").ToString

                    hayretencion = factura.Rows(0)("retencion").ToString
                    'if para validar que tiene retencion
                    'If factura.Rows(0)("retencion").ToString < 0 Then 'retencion
                    '    retencion = -1 * factura.Rows(0)("retencion").ToString
                    '    aRetencion = "totalImpuestosRetenidos=" & Chr(34) & -1 * factura.Rows(0)("retencion").ToString & Chr(34)
                    'ElseIf factura.Rows(0)("retencion").ToString > 0 Then 'retencion
                    '    retencion = factura.Rows(0)("retencion").ToString
                    '    aRetencion = "totalImpuestosRetenidos=" & Chr(34) & factura.Rows(0)("retencion").ToString & Chr(34)
                    'ElseIf factura.Rows(0)("retencion").ToString = 0 Then
                    '    aRetencion = ""
                    'End If 'retencion

                    cUtilerias.EjecutaQuery("UPDATE dbo.clientesFacturacion SET retencion = 'False' WHERE retencion is null", cUtilerias.cadenaConexion) 'Actualizar factura

                    Dim cliente As DataTable = cUtilerias.consultas("SELECT rfcCliente, retencion, nombreCliente, nombreEmpresa, numRegIdTrib, tazaIva, calleCliente, 
noExterior, colonia, pais.pais, estado.estado, ciudad.ciudad, codigoPostalCliente, usoCFDI, pais.descripcion, estado.estado AS estadoComExt, ciudad.ciudad 
AS ciudadComExt, isr, truncar FROM dbo.clientesFacturacion AS clientes, dbo.pais AS pais, dbo.estados AS estado, dbo.ciudades AS ciudad WHERE ciudad.id = clientes.ciudadCliente AND estado.id = clientes.estadoCliente 
AND pais.idpais = clientes.paisCliente AND idCliente = " & idcliente, cUtilerias.cadenaConexion) 'Variabla para consultar el cleinte                

                    'if para validar que tiene datos
                    If cliente.Rows.Count Then 'cliente
                        rfcCliente = (cliente.Rows(0)("rfcCliente").ToString)
                        cadenavalidacion = rfcCliente
                        Validacion()
                        validacion2()

                        cadenavalidacion = Replace(cadenavalidacion, ".", "")
                        cadenavalidacion = Replace(cadenavalidacion, "-", "")
                        rfcCliente = Replace(cadenavalidacion, " ", "")

                        nombreCliente = (cliente.Rows(0)("nombreEmpresa").ToString)
                        cadenavalidacion = nombreCliente
                        Validacion()
                        nombreCliente = cadenavalidacion

                        calleCliente = (cliente.Rows(0)("calleCliente").ToString)
                        cadenavalidacion = calleCliente
                        Validacion()
                        calleCliente = cadenavalidacion

                        ' hayretencion = cliente.Rows(0)("retencion").ToString
                        hayIsr = cliente.Rows(0)("isr").ToString

                        truncar = (cliente.Rows(0)("truncar").ToString)
                        noExteriorCliente = (cliente.Rows(0)("noExterior").ToString)
                        coloniaCliente = (cliente.Rows(0)("colonia").ToString)
                        localidadCliente = (cliente.Rows(0)("ciudad").ToString)
                        paisCliente = (cliente.Rows(0)("pais").ToString)
                        estadoCliente = (cliente.Rows(0)("estado").ToString)
                        municipioCliente = (cliente.Rows(0)("ciudad").ToString)
                        codigoPostalCliente = (cliente.Rows(0)("codigoPostalCliente").ToString)
                        usoCFDI = (cliente.Rows(0)("usoCFDI").ToString)
                        '///para comercio exterior///
                        residenciaFis = (cliente.Rows(0)("descripcion").ToString)
                        estadoComExt = cliente.Rows(0)("estadoComExt").ToString
                        ciudadComExt = cliente.Rows(0)("ciudadComExt").ToString
                        numRegIdTrib = (cliente.Rows(0)("numRegIdTrib").ToString)
                        'retencionCte = (cliente.Rows(0)("retencion").ToString)
                        '  tazaIva = (cliente.Rows(0)("tazaIva").ToString)

                    End If 'cliente

                    'if para validar que tiene datos
                    If empresa.Rows.Count <> 0 Then 'empresa
                        'vLugarExpedicion = (empresa.Rows(0)("lugarExpedicion").ToString)

                        vCalle = (empresa.Rows(0)("calle").ToString)
                        cadenavalidacion = vCalle
                        Validacion()
                        vCalle = cadenavalidacion

                        vcodigoPostal = (empresa.Rows(0)("codigoPostal").ToString)
                        '//vlocalidad = (empresa.Rows(0)("ciudad").ToString)
                        vnoExterior = (empresa.Rows(0)("numero").ToString)
                        vRegimen = (empresa.Rows(0)("c_regimenFiscal").ToString)
                        nombre = (empresa.Rows(0)("nombre").ToString)
                        rfc = (empresa.Rows(0)("rfc").ToString)
                        'LugarExpedicion = (empresa.Rows(0)("lugarExpedicion").ToString)
                        carpeta = empresa.Rows(0)("Ncarpeta").ToString
                        Console.WriteLine(carpeta)
                        '///Comercio Exterior///
                        vpais = (empresa.Rows(0)("paisComExt").ToString)
                        vestado = (empresa.Rows(0)("estadoComExt").ToString)
                        vmunicipio = (empresa.Rows(0)("ciudadComExt").ToString)
                        vcolonia = (empresa.Rows(0)("coloniaComExt").ToString)
                    End If 'empresa

                    'if para validar que tiene datos
                    If folios.Rows.Count <> 0 Then 'folios
                        idFolios = folios.Rows(0)("idLogs").ToString
                    Else
                        Dim logidco As DataTable = cUtilerias.consultas("SELECT * FROM dbo.log_idCO WHERE id_factura = " & factura.Rows(0)("idfactura").ToString, cUtilerias.cadenaConexion) 'Variabla para consultar el cleinte                

                        'if para validar que tiene datos
                        If logidco.Rows.Count Then 'cliente
                            idFolios = logidco.Rows(0)("id_folios").ToString
                        End If

                    End If 'folios

                    'if para validar que tiene datos
                    If configuraciones.Rows.Count <> 0 Then 'configuraciones
                        pathconfi = configuraciones.Rows(0)("path").ToString
                    End If 'configuraciones

                    'if para validar si el mes es mero que 10
                    If Month(fechaCfd) <= 9 Then 'month
                        mes_arr = "0" & Month(fechaCfd)
                    Else 'month
                        mes_arr = Month(fechaCfd)
                    End If 'month

                    'if para validar si el dia es menor que 10
                    If Day(fechaCfd) <= 9 Then 'day
                        dia_arr = "0" & Day(fechaCfd)
                    Else 'day
                        dia_arr = Day(fechaCfd)
                    End If 'day

                    fecha = Year(fechaCfd) & "-" & mes_arr & "-" & dia_arr & "T" & FormatDateTime(fechaCfd, 4)
                    'if para validar que los segundos son mero qie 10
                    If Second(fechaCfd) < 10 Then 'second
                        fecha = fecha & ":0" & Second(fechaCfd)
                    Else 'second
                        fecha = fecha & ":" & Second(fechaCfd)
                    End If 'second

                    'si el cliente trae truncar, hacer las primero las operaciones del subtotal e iva
                    If truncar = "si" Then
                        'buscamos los detalles de la factura
                        Dim conceptos As DataTable = cUtilerias.consultas("SELECT precio_unitario, cantidad, descuento, isnull(impuestoP, 0) as impuestoP FROM detFactura WHERE id_factura = " & factura.Rows(0)("idfactura").ToString, cUtilerias.cadenaConexion)

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
                            If factura.Rows(0)("iva").ToString <> 0 Then
                                impuesto = cUtilerias.truncarAdos(totalConcepto * (tazaIva / 100))
                                totalImpuestosTrasladados += cUtilerias.truncarAdos(impuesto)
                            End If

                        Next

                        subTotal = cUtilerias.truncarAdos(subTotal)
                    Else
                        'obtenemos la suma de los conceptos
                        Dim conceptos As DataTable = cUtilerias.consultas("SELECT precio_unitario, cantidad, descuento, isnull(impuestoP, 0) as impuestoP FROM detFactura WHERE id_factura = " & factura.Rows(0)("idfactura").ToString, cUtilerias.cadenaConexion)
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

                            ' precioUnitario = cUtilerias.truncarAseis(conceptos.Rows(i)("precio_unitario").ToString)
                            totalConcepto = cUtilerias.redondear(precioUnitario * conceptos.Rows(i)("cantidad").ToString)
                            subTotal += cUtilerias.redondear(totalConcepto)
                            ' Console.Write(factura.Rows(0)("iva").ToString)
                            'validamos si la factura tiene iva
                            If factura.Rows(0)("iva").ToString <> 0 Then
                                impuesto = cUtilerias.redondear(totalConcepto * (tazaIva / 100))
                                totalImpuestosTrasladados += cUtilerias.redondear(impuesto)
                            End If

                        Next

                        subTotal = cUtilerias.redondear(subTotal)
                    End If

                    'limpiamos las variables de los conceptos
                    impuesto = 0
                    totalConcepto = 0
                    precioUnitario = 0

                    'obtenemos el total
                    If truncar = "si" Then
                        total = cUtilerias.truncarAdos(totalImpuestosTrasladados + subTotal)
                        totalImpuestosTrasladados = 0
                    Else
                        total = cUtilerias.redondear(totalImpuestosTrasladados + subTotal)
                        totalImpuestosTrasladados = 0
                    End If

                    'validamos si hay isr
                    If hayIsr <> "" And hayIsr = "si" Then
                        isr = (subTotal * 0.15)
                        total = CDbl(subTotal) - isr

                        If truncar = "si" Then
                            total = cUtilerias.truncarAdos(total)
                        Else
                            total = cUtilerias.redondear(total)
                        End If
                    End If

                    'hayIsr = cliente.Rows(0)("isr").ToString

                    'comenzamos con el XML
                    miXml = miXml & "<?xml version=" & Chr(34) & "1.0" & Chr(34) & "?>"

#Region "Comprobante"
                    '<Comprobante>
                    miXml = miXml & "<cfdi:Comprobante"
                    miXml = miXml & " xmlns:cfdi=" & Chr(34) & "http://www.sat.gob.mx/cfd/3" & Chr(34)
                    miXml = miXml & " xmlns:xsi = " & Chr(34) & "http://www.w3.org/2001/XMLSchema-instance" & Chr(34)
                    '///Comercio Exterior///
                    If claveDePedimento <> "" Then
                        miXml = miXml & " xmlns:cce11=" & Chr(34) & "http://www.sat.gob.mx/ComercioExterior11" & Chr(34)
                    End If
                    miXml = miXml & " xsi:schemaLocation=" & Chr(34) & "http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv33.xsd" & Chr(34)
                    miXml = miXml & " Version=" & Chr(34) & "3.3" & Chr(34)
                    'validamos si la serie no va vacía
                    'If serie <> "" Then
                    '    miXml = miXml & " Serie=" & Chr(34) & serie & Chr(34)
                    'End If
                    miXml = miXml & " Folio=" & Chr(34) & folio & Chr(34)
                    miXml = miXml & " Fecha=" & Chr(34) & fecha & Chr(34)
                    miXml = miXml & " FormaPago=" & Chr(34) & formaDePago & Chr(34)
                    miXml = miXml & " NoCertificado=" & Chr(34) & Chr(34)
                    miXml = miXml & " Certificado=" & Chr(34) & Chr(34)
                    miXml = miXml & " Sello=" & Chr(34) & Chr(34)
                    'validamos si las condiciones de pago no va vacía
                    If condicionesDePago <> "" Then
                        miXml = miXml & " CondicionesDePago=" & Chr(34) & condicionesDePago & Chr(34)
                    End If
                    miXml = miXml & " SubTotal=" & Chr(34) & (subTotal) & Chr(34)
                    miXml = miXml & " Moneda=" & Chr(34) & moneda & Chr(34)
                    miXml = miXml & " TipoCambio=" & Chr(34) & tCambio & Chr(34)

                    If hayretencion > 0 Then
                        retencionCte = 16
                        miXml = miXml & " Total=" & Chr(34) & (subTotal) & Chr(34)
                    Else
                        miXml = miXml & " Total=" & Chr(34) & (total) & Chr(34)
                    End If
                    miXml = miXml & " TipoDeComprobante=" & Chr(34) & efecto & Chr(34)
                    miXml = miXml & " MetodoPago=" & Chr(34) & metodoDePago & Chr(34)
                    miXml = miXml & " LugarExpedicion=" & Chr(34) & vcodigoPostal & Chr(34)
                    miXml = miXml & " >"

#End Region

#Region "CFDI Relacionado"
                    'cfdi relacionado
                    'If tipoRelacion <> 0 Then
                    'miXml = miXml & "<cfdi:CfdiRelacionados TipoRelacion=" & Chr(34) & tipoRelacion & Chr(34) & ">"
                    'miXml = miXml & "<cdfi:CfdiRelacionado UUID=" & Chr(34) & Chr(34) & "/>"
                    'miXml = miXml & "</cfdi:CfdiRelacionados>"
                    'End If
#End Region
                    '

                    'obtenemos la suma de los conceptos
                    Dim Anticipos As DataTable = cUtilerias.consultas("SELECT * FROM anticipo WHERE idFacturaRelacionada = " & factura.Rows(0)("idfactura").ToString, cUtilerias.cadenaConexion)
                    'Console.Write("SELECT precio_unitario, cantidad FROM detFactura WHERE id_factura = " & factura.Rows(0)("idfactura").ToString)


                    Dim uuidAnticipo As String()

                    For i = 0 To Anticipos.Rows.Count - 1
                        'precioUnitario = conceptos.Rows(i)("precio_unitario").ToString
                        'buscar el uuide de la factura aplicada
                        Dim AnticiposFactApp As DataTable = cUtilerias.consultas("SELECT UUID FROM factura WHERE idFactura = " & Anticipos.Rows(0)("idFacturaAplicada").ToString, cUtilerias.cadenaConexion)
                        For j = 0 To AnticiposFactApp.Rows.Count - 1
                            'uuidAnticipo = Split(AnticiposFactApp.Rows(0)("UUID").ToString, "&id=")
                            uuidAnticipo = Split(AnticiposFactApp.Rows(0)("UUID").ToString, "=")
                        Next

                        miXml = miXml & "<cfdi:CfdiRelacionados TipoRelacion=" & Chr(34) & "07" & Chr(34) & ">"
                        miXml = miXml & "<cfdi:CfdiRelacionado UUID=" & Chr(34) & uuidAnticipo(4) & Chr(34) & " />"
                        miXml = miXml & "</cfdi:CfdiRelacionados>"

                        'agregar los nodos
                        '-<cfdiCfdiRelacionados TipoRelacion = "07" >
                        '
                        '</cfdiCfdiRelacionados>

                    Next

#Region "Emisor"
                    'Cfdi Emisor

                    cadenavalidacion = rfc
                    Validacion()
                    rfc = cadenavalidacion
                    miXml = miXml & "<cfdi:Emisor"
                    miXml = miXml & " Rfc=" & Chr(34) & rfc & Chr(34)

                    cadenavalidacion = nombre
                    Validacion()
                    nombre = cadenavalidacion
                    miXml = miXml & " Nombre=" & Chr(34) & nombre & Chr(34)
                    miXml = miXml & " RegimenFiscal=" & Chr(34) & vRegimen & Chr(34)
                    miXml = miXml & " />"
#End Region

#Region "Receptor"
                    'Cfdi Receptor
                    miXml = miXml & "<cfdi:Receptor"

                    cadenavalidacion = rfcCliente
                    Validacion()
                    rfcCliente = cadenavalidacion
                    miXml = miXml & " Rfc=" & Chr(34) & rfcCliente & Chr(34)

                    cadenavalidacion = nombreCliente
                    Validacion()
                    nombreCliente = cadenavalidacion
                    miXml = miXml & " Nombre=" & Chr(34) & nombreCliente & Chr(34)
                    ' para validar qie incluya la residencia fiscal
                    If residenciaFis <> "" And residenciaFis <> "MEX" And rfcCliente <> "XEXX010101000" Then
                        miXml = miXml & " ResidenciaFiscal=" & Chr(34) & residenciaFis & Chr(34)
                    End If
                    'para validar que el cliente tenga el numRegIdTrib
                    If numRegIdTrib <> "" Then
                        miXml = miXml & " NumRegIdTrib=" & Chr(34) & numRegIdTrib & Chr(34)
                    End If
                    miXml = miXml & " UsoCFDI=" & Chr(34) & usoCFDI & Chr(34)
                    miXml = miXml & " />"
#End Region

#Region "Conceptos"
                    'conceptos
                    miXml = miXml & "<cfdi:Conceptos>"
                    'for para recorer los articulos
                    For i As Integer = 0 To detFactura.Rows.Count - 1

                        'validamos si es de ksr o de otro cliente
                        If InStr(detFactura.Rows(i)("nparte").ToString, "PO") <> 0 Then
                            noParte = detFactura.Rows(i)("nparte").ToString.Split("PO.")

                            'if para validar que tiene numero de parte
                            If (noParte(0)) <> "" Then 'nparte
                                'Trim(detFactura.Rows(i)("descripcion").ToString)
                                nParte = Trim(noParte(0))
                            Else 'nparte
                                nParte = 0
                            End If 'nparte
                        Else
                            'if para validar que tiene numero de parte por default  ---30102004 Lamina de acero----
                            If (detFactura.Rows(i)("nparte").ToString) <> "" Then 'nparte

                                'Trim(detFactura.Rows(i)("descripcion").ToString)
                                nParte = Trim(detFactura.Rows(i)("nparte").ToString)
                            Else 'nparte
                                nParte = 0
                            End If 'nparte
                        End If

                        If (detFactura.Rows(i)("cantidad").ToString) <> 0 Then

                            If detFactura.Rows(i)("unidad").ToString() = "PZA" Then
                                unidad = "PIEZA"
                                cUtilerias.EjecutaQuery("UPDATE dbo.detFactura SET unidad = '" & unidad & "' WHERE Id_detFactura = " & detFactura.Rows(i)("id_detFactura").ToString, cUtilerias.cadenaConexion) 'Actualizar factura
                            ElseIf detFactura.Rows(i)("unidad").ToString() = "MTO" Then
                                unidad = "METRO"
                                cUtilerias.EjecutaQuery("UPDATE dbo.detFactura SET unidad = '" & unidad & "' WHERE Id_detFactura = " & detFactura.Rows(i)("id_detFactura").ToString, cUtilerias.cadenaConexion) 'Actualizar factura
                            Else
                                unidad = detFactura.Rows(i)("unidad").ToString()
                            End If


                            'referencia a la unidad
                            Dim claveUnidad As DataTable = cUtilerias.consultas("Select unidadSAT AS nombre from unidadesDeMedida where descripcion = '" & unidad & "'", cUtilerias.cadenaConexion) 'Variable para consultar la unidad

                            'if para validar que tiene datos
                            If claveUnidad.Rows.Count <> 0 Then 'clave unidad
                                c_unidad = (claveUnidad.Rows(0)("nombre").ToString)
                            End If 'clave unidad

                            'validamos que la clave del sat venga vacía para buscarla
                            If (detFactura.Rows(i)("claveProdServ").ToString) = "" Then
                                'producto por default  ---30102004 Lamina de acero----
                                Dim claveProdServ As DataTable = cUtilerias.consultas("SELECT codigoSAT AS claveProdServ FROM dbo.productos WHERE codigo='" & Trim(nParte) & "'", cUtilerias.cadenaConexion) 'Variable para consultar la unidad

                                'if para validar que tiene datos
                                If claveProdServ.Rows.Count <> 0 Then 'producto
                                    nParteP = Trim(claveProdServ.Rows(0)("claveProdServ").ToString)
                                Else
                                    nParteP = "32131000"  'KSR"25171702 nova 30102004"
                                End If 'producto Ð

                                'actualizamos el producto en la tabla de detalles
                                cUtilerias.EjecutaQuery("UPDATE dbo.detFactura SET claveProdServ = '" & nParteP & "' , claveUnidad = '" & c_unidad & "' WHERE Id_detFactura = " & detFactura.Rows(i)("id_detFactura").ToString, cUtilerias.cadenaConexion) 'Actualizar factura
                            Else
                                'si no viene vacía la tomamos directamente y solo actualizamos la clave de la unidad
                                nParteP = Trim(detFactura.Rows(i)("claveProdServ").ToString())
                                cUtilerias.EjecutaQuery("UPDATE dbo.detFactura SET claveUnidad = '" & c_unidad & "' WHERE Id_detFactura = " & detFactura.Rows(i)("id_detFactura").ToString, cUtilerias.cadenaConexion) 'Actualizar factura
                            End If

                            'descripcion del material
                            descripcion = detFactura.Rows(i)("descripcion").ToString()
                            cadenavalidacion = descripcion
                            Validacion()
                            descripcion = cadenavalidacion
                            descripcion = Regex.Replace(descripcion, "\s+", " ")
                            precioUnitario = detFactura.Rows(i)("precio_unitario").ToString
                            'para el precio Unitario
                            If truncar = "si" Then

                                If detFactura.Rows(i)("impuestoP").ToString > 0 And detFactura.Rows(i)("impuestoP").ToString <> "" Then
                                    precioUnitario = cUtilerias.truncarAseis(precioUnitario * ((100 + (detFactura.Rows(i)("impuestoP").ToString)) / 100))
                                Else
                                    precioUnitario = cUtilerias.truncarAseis(precioUnitario)
                                End If

                                If detFactura.Rows(i)("descuento").ToString > 0 Then
                                    precioUnitario = cUtilerias.truncarAseis(precioUnitario * ((100 - (detFactura.Rows(i)("descuento").ToString)) / 100))
                                Else
                                    precioUnitario = cUtilerias.truncarAseis(precioUnitario)
                                End If

                                totalConcepto = cUtilerias.truncarAdos(precioUnitario * detFactura.Rows(i)("cantidad").ToString)
                            Else

                                If detFactura.Rows(i)("impuestoP").ToString > 0 And detFactura.Rows(i)("impuestoP").ToString <> "" Then
                                    precioUnitario = cUtilerias.truncarAseis(precioUnitario * ((100 + (detFactura.Rows(i)("impuestoP").ToString)) / 100))
                                Else
                                    precioUnitario = cUtilerias.truncarAseis(precioUnitario)
                                End If

                                If detFactura.Rows(i)("descuento").ToString > 0 Then
                                    precioUnitario = cUtilerias.truncarAseis(precioUnitario * ((100 - (detFactura.Rows(i)("descuento").ToString)) / 100))
                                Else
                                    precioUnitario = cUtilerias.truncarAseis(precioUnitario)
                                End If

                                totalConcepto = cUtilerias.redondear(precioUnitario * detFactura.Rows(i)("cantidad").ToString)
                            End If

                            miXml = miXml & "<cfdi:Concepto"
                            miXml = miXml & " ClaveProdServ=" & Chr(34) & nParteP & Chr(34)
                            miXml = miXml & " Cantidad=" & Chr(34) & (detFactura.Rows(i)("cantidad").ToString) & Chr(34)
                            miXml = miXml & " ClaveUnidad=" & Chr(34) & c_unidad & Chr(34)
                            miXml = miXml & " Unidad=" & Chr(34) & (unidad) & Chr(34)
                            miXml = miXml & " Descripcion=" & Chr(34) & Trim(descripcion) & Chr(34)
                            miXml = miXml & " ValorUnitario=" & Chr(34) & precioUnitario & Chr(34)
                            miXml = miXml & " Importe=" & Chr(34) & totalConcepto & Chr(34)
                            miXml = miXml & " >"

                            If factura.Rows(0)("iva").ToString <> 0 Or retencionCte <> 0 Or hayIsr = "si" Then

                                miXml = miXml & "<cfdi:Impuestos>"

                                'IVA
                                If factura.Rows(0)("iva").ToString <> 0 Then
                                    'calcular el importe total del concepto
                                    If truncar = "si" Then
                                        impuesto = cUtilerias.truncarAdos(totalConcepto * (tazaIva / 100))
                                        totalImpuestosTrasladados += cUtilerias.truncarAdos(impuesto)
                                    Else
                                        impuesto = cUtilerias.redondear(totalConcepto * (tazaIva / 100))
                                        totalImpuestosTrasladados += cUtilerias.redondear(impuesto)
                                    End If

                                    If factura.Rows(0)("iva").ToString <> 0 Then
                                        miXml = miXml & "<cfdi:Traslados>"
                                        miXml = miXml & "<cfdi:Traslado"
                                        miXml = miXml & " Base=" & Chr(34) & totalConcepto & Chr(34)
                                        miXml = miXml & " Impuesto=" & Chr(34) & "002" & Chr(34)
                                        miXml = miXml & " TipoFactor=" & Chr(34) & "Tasa" & Chr(34)
                                        miXml = miXml & " TasaOCuota=" & Chr(34) & (tazaIva / 100) & "0000" & Chr(34)
                                        miXml = miXml & " Importe=" & Chr(34) & (impuesto) & Chr(34)
                                        miXml = miXml & " />"
                                        miXml = miXml & "</cfdi:Traslados>"
                                    End If
                                End If

                                'if para validar que tiene retencion ya sea normal o isr
                                If retencionCte <> 0 Or (hayIsr <> "" And hayIsr = "si") Then

                                    miXml = miXml & "<cfdi:Retenciones>"

                                    'Retencion
                                    If retencionCte <> 0 Then 'retencion
                                        If truncar = "si" Then
                                            cteRetencion = (totalConcepto) * (retencionCte / 100)
                                            cteRetencion = cUtilerias.truncarAdos(cteRetencion)
                                            retencion += cUtilerias.truncarAdos(cteRetencion)
                                        Else
                                            cteRetencion = (totalConcepto) * (retencionCte / 100)
                                            cteRetencion = cUtilerias.redondear(cteRetencion)
                                            retencion += cUtilerias.redondear(cteRetencion)
                                        End If

                                        miXml = miXml & "<cfdi:Retencion"
                                        miXml = miXml & " Base=" & Chr(34) & totalConcepto & Chr(34)
                                        miXml = miXml & " Impuesto=" & Chr(34) & "002" & Chr(34)
                                        miXml = miXml & " TipoFactor=" & Chr(34) & "Tasa" & Chr(34)
                                        miXml = miXml & " TasaOCuota=" & Chr(34) & (retencionCte / 100) & "0000" & Chr(34)
                                        miXml = miXml & " Importe=" & Chr(34) & cteRetencion & Chr(34)
                                        miXml = miXml & "/>"
                                    End If

                                    'ISR
                                    If hayIsr <> "" And hayIsr = "si" Then
                                        If truncar = "si" Then
                                            isrConcepto = cUtilerias.truncarAdos(totalConcepto * 0.15)
                                        Else
                                            isrConcepto = cUtilerias.redondear(totalConcepto * 0.15)
                                        End If

                                        miXml = miXml & "<cfdi:Retencion"
                                        miXml = miXml & " Base=" & Chr(34) & totalConcepto & Chr(34)
                                        miXml = miXml & " Impuesto=" & Chr(34) & "001" & Chr(34)
                                        miXml = miXml & " TipoFactor=" & Chr(34) & "Tasa" & Chr(34)
                                        miXml = miXml & " TasaOCuota=" & Chr(34) & 0.15 & "0000" & Chr(34)
                                        miXml = miXml & " Importe=" & Chr(34) & isrConcepto & Chr(34)
                                        miXml = miXml & " />"
                                    End If

                                    miXml = miXml & "</cfdi:Retenciones>"
                                End If

                                miXml = miXml & "</cfdi:Impuestos>"
                            End If

                            'validamos si se va a agregar el complemento de comercio exterior
                            If claveDePedimento <> "" Then
                                xmlComExt = xmlComExt & "<cce11:Mercancia"
                                xmlComExt = xmlComExt & " NoIdentificacion=" & Chr(34) & detFactura.Rows(i)("noIdentificacion").ToString & Chr(34)
                                xmlComExt = xmlComExt & " FraccionArancelaria=" & Chr(34) & detFactura.Rows(i)("FraccionArancelaria").ToString & Chr(34)
                                xmlComExt = xmlComExt & " CantidadAduana=" & Chr(34) & detFactura.Rows(i)("CantidadAduana").ToString & Chr(34)
                                xmlComExt = xmlComExt & " UnidadAduana=" & Chr(34) & detFactura.Rows(i)("UnidadAduana").ToString & Chr(34)
                                xmlComExt = xmlComExt & " ValorUnitarioAduana=" & Chr(34) & detFactura.Rows(i)("ValorUnitarioAduana").ToString & Chr(34)
                                xmlComExt = xmlComExt & " ValorDolares=" & Chr(34) & detFactura.Rows(i)("ValorDolares").ToString & Chr(34)
                                xmlComExt = xmlComExt & " />"
                            End If
                            If detFactura.Rows(i)("numPedimento").ToString <> "" Then
                                numPedimento = detFactura.Rows(i)("numPedimento").ToString()
                                cadenavalidacion = numPedimento
                                Validacion()
                                numPedimento = cadenavalidacion

                                formatoPedimento = numPedimento
                                formato()
                                numPedimento = formatoPedimento


                                miXml = miXml & "<cfdi:InformacionAduanera NumeroPedimento=" & Chr(34) & numPedimento & Chr(34) & "/>"
                            End If
                            miXml = miXml & "</cfdi:Concepto>"

                        End If
                    Next

                    miXml = miXml & "</cfdi:Conceptos>"
#End Region

#Region "Impuestos"


                    If factura.Rows(0)("iva").ToString <> 0 Or hayIsr <> "" And hayIsr = "si" Or retencionCte <> "0" Then
                        miXml = miXml & "<cfdi:Impuestos "

                        'IVA
                        If truncar = "si" Then
                            totalImpuestosTrasladados = cUtilerias.truncarAdos(totalImpuestosTrasladados)
                        Else
                            totalImpuestosTrasladados = cUtilerias.redondear(totalImpuestosTrasladados)
                        End If

                        If factura.Rows(0)("iva").ToString <> 0 Then
                            miXml = miXml & "TotalImpuestosTrasladados=" & Chr(34) & totalImpuestosTrasladados & Chr(34)
                        End If

                        'Retenciones
                        If retencionCte <> 0 Or (hayIsr <> "" And hayIsr = "si") Then
                            'Retencion IEFS
                            If retencionCte <> 0 Then
                                If truncar = "si" Then
                                    retencion = cUtilerias.truncarAdos(retencion)
                                Else
                                    retencion = cUtilerias.redondear(retencion)
                                End If
                            End If

                            'Retencion ISR
                            If hayIsr <> "" And hayIsr = "si" Then

                                If truncar = "si" Then
                                    isr = cUtilerias.truncarAdos(isr)
                                Else
                                    isr = cUtilerias.redondear(isr)
                                End If

                                'Actualizar factura
                                cUtilerias.EjecutaQuery("UPDATE dbo.factura SET retensionIsr = " & isr & " WHERE idfactura = " & factura.Rows(0)("idfactura").ToString, cUtilerias.cadenaConexion)

                            End If

                            If truncar = "si" Then
                                totalRetencion = cUtilerias.truncarAdos(retencion + isr)
                            Else
                                totalRetencion = cUtilerias.redondear(retencion + isr)
                            End If

                            miXml = miXml & " TotalImpuestosRetenidos=" & Chr(34) & totalRetencion & Chr(34)
                        End If

                        miXml = miXml & ">"

                        If retencionCte <> 0 Then 'retencion

                            If truncar = "si" Then
                                retencion = cUtilerias.truncarAdos(retencion)
                            Else
                                retencion = cUtilerias.redondear(retencion)
                            End If

                            miXml = miXml & "<cfdi:Retenciones>"
                            miXml = miXml & "<cfdi:Retencion"
                            miXml = miXml & " Impuesto=" & Chr(34) & "002" & Chr(34)
                            miXml = miXml & " Importe=" & Chr(34) & (retencion) & Chr(34)
                            miXml = miXml & " />"
                            miXml = miXml & "</cfdi:Retenciones>"

                        End If

                        If hayIsr <> "" And hayIsr = "si" Then
                            'ISR
                            If truncar = "si" Then
                                isr = cUtilerias.truncarAdos(isr)
                            Else
                                isr = cUtilerias.redondear(isr)
                            End If

                            miXml = miXml & "<cfdi:Retenciones>"
                            miXml = miXml & "<cfdi:Retencion"
                            miXml = miXml & " Impuesto=" & Chr(34) & "001" & Chr(34)
                            miXml = miXml & " Importe=" & Chr(34) & isr & Chr(34)
                            miXml = miXml & "/>"
                            miXml = miXml & "</cfdi:Retenciones>"

                        End If 'retencion

                        'IVA
                        If factura.Rows(0)("iva").ToString <> 0 Then
                            miXml = miXml & "<cfdi:Traslados>"
                            miXml = miXml & "<cfdi:Traslado"
                            miXml = miXml & " Impuesto=" & Chr(34) & "002" & Chr(34)
                            miXml = miXml & " TipoFactor=" & Chr(34) & "Tasa" & Chr(34)
                            miXml = miXml & " TasaOCuota=" & Chr(34) & (tazaIva / 100) & "0000" & Chr(34)
                            miXml = miXml & " Importe=" & Chr(34) & (totalImpuestosTrasladados) & Chr(34)
                            miXml = miXml & " />"
                            miXml = miXml & "</cfdi:Traslados>"
                        End If

                        miXml = miXml & "</cfdi:Impuestos>"
                    End If
#End Region

#Region "ComercioExterior"
                    'validamos si se va a agregar lo de comercio exterior
                    If claveDePedimento <> "" Then
                        miXml = miXml & "<cfdi:Complemento>"
                        If claveDePedimento <> "" Then
                            If InStr(totalUsd, ".") = 0 Then
                                totalUsd = totalUsd & ".00"
                            Else
                                If truncar = "si" Then
                                    totalUsd = cUtilerias.truncarAdos(totalUsd)
                                Else
                                    totalUsd = cUtilerias.redondear(totalUsd)
                                End If
                            End If

                            miXml = miXml & "<cce11:ComercioExterior"
                            miXml = miXml & " xsi:schemaLocation=" & Chr(34) & "http://www.sat.gob.mx/ComercioExterior11 http://www.sat.gob.mx/sitio_internet/cfd/ComercioExterior11/ComercioExterior11.xsd" & Chr(34)
                            miXml = miXml & " Version=" & Chr(34) & "1.1" & Chr(34)
                            If efecto = "T" Then
                                miXml = miXml & " MotivoTraslado=" & Chr(34) & motivoTraslado & Chr(34)
                            End If
                            miXml = miXml & " TipoOperacion=" & Chr(34) & tipoOperacion & Chr(34)
                            miXml = miXml & " ClaveDePedimento=" & Chr(34) & claveDePedimento & Chr(34)
                            miXml = miXml & " CertificadoOrigen=" & Chr(34) & certificadoOrigen & Chr(34)
                            miXml = miXml & " Incoterm=" & Chr(34) & incoterm & Chr(34)
                            miXml = miXml & " Subdivision=" & Chr(34) & subdivision & Chr(34)
                            miXml = miXml & " TipoCambioUSD=" & Chr(34) & tipoCambioUsd & Chr(34)
                            miXml = miXml & " TotalUSD=" & Chr(34) & totalUsd & Chr(34)
                            miXml = miXml & " >"

                            miXml = miXml & "<cce11:Emisor>"
                            miXml = miXml & "<cce11:Domicilio"
                            miXml = miXml & " Calle=" & Chr(34) & vCalle.ToString & Chr(34)
                            If vnoExterior.ToString <> "" Then
                                miXml = miXml & " NumeroExterior=" & Chr(34) & vnoExterior.ToString & Chr(34)
                            End If
                            miXml = miXml & " Colonia=" & Chr(34) & vcolonia & Chr(34)
                            miXml = miXml & " Municipio=" & Chr(34) & vmunicipio & Chr(34)
                            miXml = miXml & " Estado=" & Chr(34) & vestado & Chr(34)
                            miXml = miXml & " Pais=" & Chr(34) & vpais & Chr(34)
                            miXml = miXml & " CodigoPostal=" & Chr(34) & vcodigoPostal & Chr(34)
                            miXml = miXml & " />"
                            miXml = miXml & "</cce11:Emisor>"

                            miXml = miXml & "<cce11:Receptor>"
                            miXml = miXml & "<cce11:Domicilio"
                            miXml = miXml & " Calle=" & Chr(34) & calleCliente.ToString & Chr(34)
                            If noExteriorCliente.ToString <> "" Then
                                miXml = miXml & " NumeroExterior=" & Chr(34) & noExteriorCliente.ToString & Chr(34)
                            End If
                            If coloniaCliente.ToString <> "" Then
                                miXml = miXml & " Colonia=" & Chr(34) & coloniaCliente.ToString & Chr(34)
                            End If
                            If ciudadComExt <> "" Then
                                miXml = miXml & " Municipio=" & Chr(34) & ciudadComExt & Chr(34)
                            End If
                            miXml = miXml & " Estado=" & Chr(34) & estadoComExt & Chr(34)
                            miXml = miXml & " Pais=" & Chr(34) & residenciaFis & Chr(34)
                            miXml = miXml & " CodigoPostal=" & Chr(34) & codigoPostalCliente & Chr(34)
                            miXml = miXml & " />"
                            miXml = miXml & "</cce11:Receptor>"

                            'validamos si el tipo de documento es de traslado
                            If efecto = "T" Then
                                miXml = miXml & "<cce11:Destinatario"
                                miXml = miXml & " NumRegIdTrib=" & Chr(34) & numRegIdTribDestino & Chr(34)
                                miXml = miXml & " Nombre=" & Chr(34) & nombreDestino & Chr(34)
                                miXml = miXml & ">"
                                miXml = miXml & "<cce11:Domicilio"
                                '///
                                miXml = miXml & " Calle=" & Chr(34) & calleDestino.ToString & Chr(34)
                                If exteriorDestino <> "" Then
                                    miXml = miXml & " NumeroExterior=" & Chr(34) & exteriorDestino.ToString & Chr(34)
                                End If
                                If interiorDestino <> "" Then
                                    miXml = miXml & " NumeroInterior=" & Chr(34) & interiorDestino.ToString & Chr(34)
                                End If
                                If coloniaDestino <> "" Then
                                    miXml = miXml & " Colonia=" & Chr(34) & coloniaDestino & Chr(34)
                                End If

                                miXml = miXml & " Municipio=" & Chr(34) & municipioDestino & Chr(34)
                                miXml = miXml & " Estado=" & Chr(34) & estadoDestino & Chr(34)
                                miXml = miXml & " Pais=" & Chr(34) & paisDestino & Chr(34)
                                miXml = miXml & " CodigoPostal=" & Chr(34) & codigoPostalDestino & Chr(34)
                                '///
                                miXml = miXml & "/>"
                                miXml = miXml & "</cce11:Destinatario>"
                            End If

                            miXml = miXml & "<cce11:Mercancias>"
                            miXml = miXml & xmlComExt
                            miXml = miXml & "</cce11:Mercancias>"
                            miXml = miXml & "</cce11:ComercioExterior>"

                        End If

                        miXml = miXml & "</cfdi:Complemento>"
                    End If

#End Region

                    miXml = miXml & "</cfdi:Comprobante>"

                    'creamos el nombre del archivo 
                    Dim archivo = pathconfi & carpeta & "\" & factura.Rows(0)("idfactura").ToString & "CO.xml"

                    'conectamos con el FSO 
                    Dim confile = CreateObject("scripting.filesystemobject")

                    'creamos el objeto TextStream 
                    Dim fich = confile.CreateTextFile(archivo)

                    'Escribir en el archivo
                    fich.write(Trim(miXml))

                    'cerramos el fichero 
                    fich.close()

                    'volvemos a abrir el fichero para lectura 
                    fich = confile.OpenTextFile(archivo)

                    'leemos el contenido del fichero 
                    Dim texto_fichero = fich.readAll()

                    'cerramos el fichero 
                    fich.close()

                    cUtilerias.EjecutaQuery("INSERT INTO dbo.log_idCO(fecha, fecha_alta, nombre_archivo, carpeta, status, id_empresa, id_usuario, id_factura, id_folios) VALUES (GETDATE(), GETDATE(), '" & nomArchivo & "', '" & carpeta & "', 'pendiente', '" & factura.Rows(0)("idempresa").ToString & "', '0', '" & factura.Rows(0)("idfactura").ToString & "', '" & idFolios & "')", cUtilerias.cadenaConexion)
                    Console.WriteLine("Fin del proceso")

                    aTimer.Enabled = True

                End If


            End If 'factura
        Catch ex As Exception
            Console.WriteLine(ex)
            Console.WriteLine("")
            aTimer.Enabled = True
        End Try
    End Sub

    Sub Main()
        cUtilerias.cadenaConexion = cUtilerias.configuraciones("CADENA")
        cUtilerias.tiempo = cUtilerias.configuraciones("TIEMPO")
        aTimer.AutoReset = True
        aTimer.Interval = cUtilerias.tiempo '5 seconds 
        AddHandler aTimer.Elapsed, AddressOf CadenaOrinal
        aTimer.Start()
        Console.WriteLine("Presiona Enter para Salir del programa.")
        Console.ReadLine()
    End Sub

    Sub Validacion()

        cadenavalidacion = Replace(cadenavalidacion, "°", " ")
        cadenavalidacion = Replace(cadenavalidacion, Chr(34), "")
        cadenavalidacion = Replace(cadenavalidacion, "<", "&lt;")
        cadenavalidacion = Replace(cadenavalidacion, ">", "&gt;")
        cadenavalidacion = Replace(cadenavalidacion, "'", "")
        cadenavalidacion = Replace(cadenavalidacion, "´", "")
        cadenavalidacion = Replace(cadenavalidacion, "°", "")
        cadenavalidacion = Replace(cadenavalidacion, "º", " ")
        cadenavalidacion = Replace(cadenavalidacion, "&", "&amp;")

        ' cadenavalidacion = Replace(cadenavalidacion, " ", "")
        cadenavalidacion = Replace(cadenavalidacion, "-", "")
        cadenavalidacion = Replace(cadenavalidacion, "/", "")
        cadenavalidacion = Replace(cadenavalidacion, "¡", "")
        cadenavalidacion = Replace(cadenavalidacion, "¿", "")
        cadenavalidacion = Replace(cadenavalidacion, "ā", "a")
        cadenavalidacion = Replace(cadenavalidacion, "ă", "a")

        cadenavalidacion = Replace(cadenavalidacion, "á", "a")
        cadenavalidacion = Replace(cadenavalidacion, "é", "e")
        cadenavalidacion = Replace(cadenavalidacion, "í", "i")
        cadenavalidacion = Replace(cadenavalidacion, "ó", "o")
        cadenavalidacion = Replace(cadenavalidacion, "ú", "u")

        cadenavalidacion = Replace(cadenavalidacion, "Á", "A")
        cadenavalidacion = Replace(cadenavalidacion, "É", "E")
        cadenavalidacion = Replace(cadenavalidacion, "Í", "I")
        cadenavalidacion = Replace(cadenavalidacion, "Ó", "O")
        cadenavalidacion = Replace(cadenavalidacion, "Ú", "U")

        cadenavalidacion = Replace(cadenavalidacion, "Ñ", "N")
        cadenavalidacion = Replace(cadenavalidacion, "Ç", "C")
        cadenavalidacion = Replace(cadenavalidacion, "ñ", "n")
        cadenavalidacion = Replace(cadenavalidacion, "ç", "c")
        cadenavalidacion = Replace(cadenavalidacion, "Ð", "N")

        cadenavalidacion = Replace(cadenavalidacion, "à", "a")
        cadenavalidacion = Replace(cadenavalidacion, "è", "e")
        cadenavalidacion = Replace(cadenavalidacion, "ì", "i")
        cadenavalidacion = Replace(cadenavalidacion, "ò", "o")
        cadenavalidacion = Replace(cadenavalidacion, "ù", "u")

        cadenavalidacion = Replace(cadenavalidacion, "À", "A")
        cadenavalidacion = Replace(cadenavalidacion, "È", "E")
        cadenavalidacion = Replace(cadenavalidacion, "Ì", "I")
        cadenavalidacion = Replace(cadenavalidacion, "Ò", "O")
        cadenavalidacion = Replace(cadenavalidacion, "Ù", "U")

        cadenavalidacion = Replace(cadenavalidacion, "ä", "a")
        cadenavalidacion = Replace(cadenavalidacion, "ë", "e")
        cadenavalidacion = Replace(cadenavalidacion, "ï", "i")
        cadenavalidacion = Replace(cadenavalidacion, "ö", "o")
        cadenavalidacion = Replace(cadenavalidacion, "ü", "u")

        cadenavalidacion = Replace(cadenavalidacion, "Ä", "A")
        cadenavalidacion = Replace(cadenavalidacion, "Ë", "E")
        cadenavalidacion = Replace(cadenavalidacion, "Ï", "I")
        cadenavalidacion = Replace(cadenavalidacion, "Ö", "O")
        cadenavalidacion = Replace(cadenavalidacion, "Ü", "U")

        cadenavalidacion = Replace(cadenavalidacion, "â", "a")
        cadenavalidacion = Replace(cadenavalidacion, "ê", "e")
        cadenavalidacion = Replace(cadenavalidacion, "î", "i")
        cadenavalidacion = Replace(cadenavalidacion, "ô", "o")
        cadenavalidacion = Replace(cadenavalidacion, "û", "u")

        cadenavalidacion = Replace(cadenavalidacion, "Â", "A")
        cadenavalidacion = Replace(cadenavalidacion, "Ê", "E")
        cadenavalidacion = Replace(cadenavalidacion, "Î", "I")
        cadenavalidacion = Replace(cadenavalidacion, "Ô", "O")
        cadenavalidacion = Replace(cadenavalidacion, "Û", "U")

        cadenavalidacion = Replace(cadenavalidacion, "½", "1/2")
        cadenavalidacion = Replace(cadenavalidacion, "¼", "1/4")
        cadenavalidacion = Replace(cadenavalidacion, "Ø", " ")
        cadenavalidacion = Replace(cadenavalidacion, "~", " ")
        cadenavalidacion = Replace(cadenavalidacion, "™", " ")
        cadenavalidacion = Replace(cadenavalidacion, "²", " ")
        cadenavalidacion = Replace(cadenavalidacion, "³", " ")
        cadenavalidacion = Trim(cadenavalidacion)

    End Sub

    Sub validacion2()
        cadenavalidacion = cadenavalidacion.Replace(" ", "")
    End Sub

    Sub formato()
        Dim arregoP As String

        arregoP = formatoPedimento
        formatoPedimento = ""

        For I As Integer = 0 To arregoP.ToArray.Count - 1
            Select Case (I)
                Case 2, 4, 8
                    formatoPedimento = formatoPedimento + "  "
            End Select

            formatoPedimento = formatoPedimento + arregoP(I)

        Next


    End Sub

End Module