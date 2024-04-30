Imports System.IO
Imports System.Data.SqlClient
'Imports TimbreFINKOK.DemoTimbreFINKOK
Imports TimbreFINKOK.timbrefinkok
Imports System.Text
Imports System.Net

Module Module1
    Dim aTimer As New System.Timers.Timer 'Variable para guardar el timer
    Public cUtilerias As New utilerias 'Variable para guardar las utilerias

    Private Sub CadenaOrinal(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs)
        Try
            Dim factura As DataTable = cUtilerias.consultas("SELECT * FROM dbo.factura WHERE timbre ='SI' ORDER BY idfactura ASC", cUtilerias.cadenaConexion) 'Variable para consultar la factura                  

            'if para validar que tiene datos
            If factura.Rows.Count <> 0 Then 'factura                
                Dim idFactura 'Variable para guardar el id de la factura
                Dim idEmpresa 'variable para guardar id de empresa

                Dim ruta, carpetasellados As String
                Dim archivo As String
                Dim nombreArch As Object = vbNull

                'cUtilerias.EjecutaQuery("UPDATE dbo.detFactura SET  descuento=0  WHERE descuento is NULL", cUtilerias.cadenaConexion) 'Actualizar factur
                idEmpresa = factura.Rows(0)("idempresa").ToString 'agregado para buscar la empresa

                Dim configmenor As DataTable = cUtilerias.consultas("SELECT * FROM dbo.configmenor WHERE idEmpresa = " + idEmpresa, cUtilerias.cadenaConexion) 'Variable para consultar la empresa
                If configmenor.Rows.Count <> 0 Then
                    ruta = configmenor.Rows(0)("path").ToString
                    carpetasellados = configmenor.Rows(0)("path").ToString
                End If

                idFactura = factura.Rows(0)("idfactura").ToString


                Dim empresa As DataTable = cUtilerias.consultas("SELECT * FROM dbo.sucursales WHERE id = " + idEmpresa, cUtilerias.cadenaConexion) 'Variable para consultar la empresa

                If empresa.Rows.Count <> 0 Then
                    ruta = ruta & empresa.Rows(0)("carpetaTimbre").ToString
                    carpetasellados = carpetasellados & empresa.Rows(0)("Ncarpeta").ToString
                End If
                'Dim detFactura As DataTable = cUtilerias.consultas("SELECT * FROM dbo.detFactura WHERE id_factura = " & factura.Rows(0)("idfactura").ToString, cUtilerias.cadenaConexion) 'Variable para consultar los articulos de la factura

                Dim configuraciones As DataTable = cUtilerias.consultas("SELECT * FROM dbo.log_idCO WHERE id_factura = " + idFactura.ToString(), cUtilerias.cadenaConexion) 'Variable para consultar las configuraciones
                If configuraciones.Rows.Count <> 0 Then
                    archivo = configuraciones.Rows(0)("nombre_archivo")
                    nombreArch = Split(archivo, "\") 'agregado 02/11/2020
                End If

                Console.WriteLine("Comenzando Timbrado 2021")
                Console.WriteLine("e-ControCFDi V3.3" + DateTime.Now)
                Console.WriteLine(" ")
                aTimer.Enabled = False

                Dim xml As String
                'xml = carpetasellados & "\" & archivo
                xml = carpetasellados & "\" & nombreArch(UBound(nombreArch)) 'agregado 02/11/2020
                Try
                    Dim SelloSAT As String = ""
                    Dim noCertificadoSAT As String = ""
                    Dim FechaTimbrado As String = ""
                    Dim uuid As String = ""
                    Dim xmlTimbrado As String = ""
                    Dim xmlCfdi As New System.Xml.XmlDocument()
                    Dim timbrado As New timbrefinkok.StampSOAP
                    Dim timb As New sign_stamp
                    Dim ResponseTimbrar As New sign_stampResponse
                    Dim MensajeIncidencia As String = ""
                    Dim CodigoIncidencia As String = ""

                    xmlCfdi.Load(xml) 'Cargamos el archivo XML. 
                    ServicePointManager.Expect100Continue = True
                    ServicePointManager.SecurityProtocol = DirectCast(3072, SecurityProtocolType)
                    timb.xml = stringToBase64ByteArray(xmlCfdi.OuterXml)

                    timb.username = "soporte@catedralsoftware.com"
                    timb.password = "Atrejo*1321"

                    ResponseTimbrar = timbrado.sign_stamp(timb)
                    If DirectCast(ResponseTimbrar.sign_stampResult, AcuseRecepcionCFDI).Incidencias.Length > 0 Then
                        CodigoIncidencia = DirectCast(ResponseTimbrar.sign_stampResult, AcuseRecepcionCFDI).Incidencias(0).CodigoError
                        MensajeIncidencia = DirectCast(ResponseTimbrar.sign_stampResult, AcuseRecepcionCFDI).Incidencias(0).MensajeIncidencia
                    Else
                        CodigoIncidencia = "000"
                        MensajeIncidencia = "Timbrado Satisfactorio"
                    End If
                    If CodigoIncidencia = "000" Then
                        uuid = ResponseTimbrar.sign_stampResult.UUID
                        xmlTimbrado = ResponseTimbrar.sign_stampResult.xml
                        FechaTimbrado = ResponseTimbrar.sign_stampResult.Fecha
                        SelloSAT = ResponseTimbrar.sign_stampResult.SatSeal
                        noCertificadoSAT = ResponseTimbrar.sign_stampResult.NoCertificadoSAT
                        xmlCfdi.LoadXml(xmlTimbrado)
                        xml = ruta & "\" & archivo
                        xmlCfdi.Save(xml)
                        'txtUUID.Text = uuid


                    ElseIf CodigoIncidencia = "707" Then
                        Dim ResponseIsTimbrado As New stampedResponse
                        Dim isTimbrado As New stamped
                        'isTimbrado.xml = Convert.ToBase64CharArray(xmlCfdi.OuterXml)
                        isTimbrado.username = "soporte@catedralsoftware.com"
                        isTimbrado.password = "Bit49*2009"
                        ResponseIsTimbrado = timbrado.stamped(isTimbrado)
                        uuid = ResponseIsTimbrado.stampedResult.UUID
                        xmlTimbrado = ResponseIsTimbrado.stampedResult.xml
                        FechaTimbrado = ResponseIsTimbrado.stampedResult.Fecha
                        SelloSAT = ResponseIsTimbrado.stampedResult.SatSeal
                        noCertificadoSAT = ResponseIsTimbrado.stampedResult.NoCertificadoSAT
                        xmlCfdi.LoadXml(xmlTimbrado)
                        xml = ruta & "\" & archivo
                        xmlCfdi.Save(xml)
                        'txtUUID.Text = uuid
                    Else
                        Console.WriteLine(MensajeIncidencia)
                        cUtilerias.EjecutaQuery("UPDATE dbo.factura SET  timbre = 'Error' WHERE idfactura =  " & factura.Rows(0)("idfactura").ToString, cUtilerias.cadenaConexion) 'Actualizar factura
                        cUtilerias.EjecutaQuery("UPDATE dbo.log_idCO SET observaciones = '" & MensajeIncidencia & "' WHERE id_factura =" & factura.Rows(0)("idfactura").ToString, cUtilerias.cadenaConexion) 'Actualizar factura

                    End If
                    If CodigoIncidencia = "000" Then
                        cUtilerias.EjecutaQuery("UPDATE dbo.factura SET generaPDF = 1, timbre = 'NO' WHERE idfactura =  " & factura.Rows(0)("idfactura").ToString, cUtilerias.cadenaConexion) 'Actualizar factura
                        cUtilerias.EjecutaQuery("UPDATE dbo.log_idCO SET observaciones = '" & MensajeIncidencia & "' WHERE id_factura =" & factura.Rows(0)("idfactura").ToString, cUtilerias.cadenaConexion) 'Actualizar factura
                        Console.WriteLine("Timbrado exitoso.")
                    Else
                        Console.WriteLine("Timbrado no Realizado")
                    End If
                Catch ex As Exception
                    cUtilerias.EjecutaQuery("UPDATE dbo.factura SET  timbre = 'Error' WHERE idfactura =  " & factura.Rows(0)("idfactura").ToString, cUtilerias.cadenaConexion) 'Actualizar factura
                    cUtilerias.EjecutaQuery("UPDATE dbo.log_idCO SET observaciones = '" & ex.Message & "' WHERE id_factura =" & factura.Rows(0)("idfactura").ToString, cUtilerias.cadenaConexion) 'Actualizar factura
                    Console.WriteLine("No se realizo el timbrado " & ex.Message)
                Finally
                    'Windows.Forms.Cursor.Current = Cursors.Default
                End Try

2:
                'Dim cliente As DataTable = cUtilerias.consultas("SELECT rfcCliente, nombreCliente, calleCliente, noExterior, colonia, pais.pais, estado.estado, ciudad.ciudad, codigoPostalCliente FROM dbo.clientes, dbo.pais, dbo.estado, dbo.ciudad WHERE ciudad.idciudad = clientes.ciudadCliente AND estado.idestado = clientes.estadoCliente AND pais.idpais = clientes.paisCliente AND idCliente = " & idcliente, cUtilerias.cadenaConexion) 'Variabla para consultar el cliente
                Console.WriteLine("Fin del proceso")
                Console.WriteLine(" ")

                aTimer.Enabled = True
            End If 'factura
        Catch ex As Exception
            Console.WriteLine(ex.Message)

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
        Console.WriteLine("ARCA-NET Timbrado FINKOK-2017")
        Console.ReadLine()
    End Sub



    Public Function stringToBase64ByteArray(ByVal input As String) As Byte()
        Dim ret As Byte() = Encoding.UTF8.GetBytes(input)
        Dim s As String = Convert.ToBase64String(ret)
        ret = Convert.FromBase64String(s)
        Return ret
    End Function

End Module

