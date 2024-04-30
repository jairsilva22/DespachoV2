Imports System.IO
Imports System.Data.SqlClient
Imports System.Xml

Public Class utilerias
    Dim direccionEmp4 As String
    Dim direccionEmp3 As String
    Dim direccionEmp2 As String
    Dim direccionEmp As String
    Dim nombreEmp As String
    Dim folioFac As String
    Dim vComprobante As String
    Dim logoEmp As String
    Dim anchologoEmp As String
    Dim altologoEmp As String
    Dim font As String
    Dim cadena As String
    Dim folio As String
    Dim fCFD As String
    Dim fVenta As String
    Dim cVenta As String
    Dim nCuenta As String
    Dim nomCliente As String
    Dim dirCliente As String
    Dim colCliente As String
    Dim ciuCliente As String
    Dim estCliente As String
    Dim telCliente As String
    Dim clienteRfc As String
    Dim cpCliente As String
    Dim pCliente As String
    Dim consig As String
    Dim vMoneda As String
    Dim lExpedicion As String
    Dim ocompra As String
    Dim mPago As String
    Dim cPago As String
    Dim vsubtotal As String
    Dim vtotal As String
    Dim viva As String
    Dim vimpuesto As String
    Dim vretencion As String
    Dim vcentavos As String
    Dim vmonedaTexto As String
    Dim vUUID As String
    Dim imgUUID As String
    Dim vanchoUUID As String
    Dim valtoUUID As String
    Dim vCDS As String
    Dim vSELLO As String
    Dim co As String
    Dim vselloCFD As String
    Dim vFechaTimbrado As String
    Dim vnoCertificadoSAT As String
    Dim vobsCliente = ""
    Dim _usoCFDI As String
    Dim _formaPago As String
    Dim _certificado
    Dim _truncar As String
    Dim _hayisr As String
    Dim _hayretencion As String
    Dim _asn As String
    Dim _estatus As String

    Public Property estatus() As String
        Get
            Return _estatus
        End Get
        Set(ByVal value As String)
            _estatus = value
        End Set
    End Property

    Public Property hayretencion() As String
        Get
            Return _hayretencion
        End Get
        Set(ByVal value As String)
            _hayretencion = value
        End Set
    End Property

    Public Property asn() As String
        Get
            Return _asn
        End Get
        Set(ByVal value As String)
            _asn = value
        End Set
    End Property

    Public Property hayisr() As String
        Get
            Return _hayisr
        End Get
        Set(ByVal value As String)
            _hayisr = value
        End Set
    End Property

    Public Property certificado() As String
        Get
            Return _certificado
        End Get
        Set(ByVal value As String)
            _certificado = value
        End Set
    End Property

    Public Property truncar() As String
        Get
            Return _truncar
        End Get
        Set(ByVal value As String)
            _truncar = value
        End Set
    End Property

    Public Property obsCliente() As String 'Observaciones
        ' la parte Get es la que devuelve el valor de la propiedad
        Get
            Return vobsCliente
        End Get
        ' la parte Set es la que se usa al asignar el nuevo valor
        Set(ByVal Value As String)
            If Value <> "" Then
                vobsCliente = Value
            End If
        End Set

    End Property

    Public Property usoCFDI() As String
        Get
            Return _usoCFDI
        End Get
        Set(ByVal value As String)
            If value <> "" Then
                _usoCFDI = value
            End If
        End Set
    End Property

    Public Property formaPago() As String
        Get
            Return _formaPago
        End Get
        Set(ByVal value As String)
            _formaPago = value
        End Set

    End Property

    Public Property noCertificadoSAT() As String 'Cadena original
        ' la parte Get es la que devuelve el valor de la propiedad
        Get
            Return vnoCertificadoSAT
        End Get
        ' la parte Set es la que se usa al asignar el nuevo valor
        Set(ByVal Value As String)
            If Value <> "" Then
                vnoCertificadoSAT = Value
            End If
        End Set

    End Property

    Public Property FechaTimbrado() As String 'Cadena original
        ' la parte Get es la que devuelve el valor de la propiedad
        Get
            Return vFechaTimbrado
        End Get
        ' la parte Set es la que se usa al asignar el nuevo valor
        Set(ByVal Value As String)
            If Value <> "" Then
                vFechaTimbrado = Value
            End If
        End Set

    End Property

    Public Property selloCFD() As String 'Cadena original
        ' la parte Get es la que devuelve el valor de la propiedad
        Get
            Return vselloCFD
        End Get
        ' la parte Set es la que se usa al asignar el nuevo valor
        Set(ByVal Value As String)
            If Value <> "" Then
                vselloCFD = Value
            End If
        End Set

    End Property

    Public Property cadnaOriginal() As String 'Cadena original
        ' la parte Get es la que devuelve el valor de la propiedad
        Get
            Return co
        End Get
        ' la parte Set es la que se usa al asignar el nuevo valor
        Set(ByVal Value As String)
            If Value <> "" Then
                co = Value
            End If
        End Set

    End Property

    Public Property SELLO() As String 'Sello
        ' la parte Get es la que devuelve el valor de la propiedad
        Get
            Return vSELLO
        End Get
        ' la parte Set es la que se usa al asignar el nuevo valor
        Set(ByVal Value As String)
            If Value <> "" Then
                vSELLO = Value
            End If
        End Set

    End Property

    Public Property CDS() As String 'CDS
        ' la parte Get es la que devuelve el valor de la propiedad
        Get
            Return vCDS
        End Get
        ' la parte Set es la que se usa al asignar el nuevo valor
        Set(ByVal Value As String)
            If Value <> "" Then
                vCDS = Value
            End If
        End Set

    End Property

    Public Property altoUUID() As String 'Alto de la imange UUID
        ' la parte Get es la que devuelve el valor de la propiedad
        Get
            Return vanchoUUID
        End Get
        ' la parte Set es la que se usa al asignar el nuevo valor
        Set(ByVal Value As String)
            If Value <> "" Then
                vanchoUUID = Value
            End If
        End Set

    End Property

    Public Property anchoUUID() As String 'Ancho de la imagen UUID
        ' la parte Get es la que devuelve el valor de la propiedad
        Get
            Return vanchoUUID
        End Get
        ' la parte Set es la que se usa al asignar el nuevo valor
        Set(ByVal Value As String)
            If Value <> "" Then
                vanchoUUID = Value
            End If
        End Set

    End Property

    Public Property imagenUUID() As String 'Imangen del UUID
        ' la parte Get es la que devuelve el valor de la propiedad
        Get
            Return imgUUID
        End Get
        ' la parte Set es la que se usa al asignar el nuevo valor
        Set(ByVal Value As String)
            If Value <> "" Then
                imgUUID = Value
            End If
        End Set

    End Property

    Public Property UUID() As String 'UUID
        ' la parte Get es la que devuelve el valor de la propiedad
        Get
            Return vUUID
        End Get
        ' la parte Set es la que se usa al asignar el nuevo valor
        Set(ByVal Value As String)
            If Value <> "" Then
                vUUID = Value
            End If
        End Set

    End Property

    Public Property monedaTexto() As String 'Moneda en texto
        ' la parte Get es la que devuelve el valor de la propiedad
        Get
            Return vmonedaTexto
        End Get
        ' la parte Set es la que se usa al asignar el nuevo valor
        Set(ByVal Value As String)
            If Value <> "" Then
                vmonedaTexto = Value
            End If
        End Set

    End Property

    Public Property centavos() As String 'Centavos
        ' la parte Get es la que devuelve el valor de la propiedad
        Get
            Return vcentavos
        End Get
        ' la parte Set es la que se usa al asignar el nuevo valor
        Set(ByVal Value As String)
            If Value <> "" Then
                vcentavos = Value
            End If
        End Set

    End Property

    Public Property retencion() As String 'Retencion
        ' la parte Get es la que devuelve el valor de la propiedad
        Get
            Return vretencion
        End Get
        ' la parte Set es la que se usa al asignar el nuevo valor
        Set(ByVal Value As String)
            If Value <> "" Then
                vretencion = Value
            End If
        End Set

    End Property
    Public Property Total() As String 'Total
        ' la parte Get es la que devuelve el valor de la propiedad
        Get
            Return vtotal
        End Get
        ' la parte Set es la que se usa al asignar el nuevo valor
        Set(ByVal Value As String)
            If Value <> "" Then
                vtotal = Value
            End If
        End Set
    End Property

    Public Property iva() As String 'sub total
        ' la parte Get es la que devuelve el valor de la propiedad
        Get
            Return viva
        End Get
        ' la parte Set es la que se usa al asignar el nuevo valor
        Set(ByVal Value As String)
            If Value <> "" Then
                viva = Value
            End If
        End Set
    End Property

    Public Property subTotal() As String 'sub total
        ' la parte Get es la que devuelve el valor de la propiedad
        Get
            Return vsubtotal
        End Get
        ' la parte Set es la que se usa al asignar el nuevo valor
        Set(ByVal Value As String)
            If Value <> "" Then
                vsubtotal = Value
            End If
        End Set
    End Property

    Public Property impuesto() As String 'sub total
        ' la parte Get es la que devuelve el valor de la propiedad
        Get
            Return vimpuesto
        End Get
        ' la parte Set es la que se usa al asignar el nuevo valor
        Set(ByVal Value As String)
            If Value <> "" Then
                vimpuesto = Value
            End If
        End Set
    End Property
    Public Property condicionesPago() As String 'Condiciones de pago
        ' la parte Get es la que devuelve el valor de la propiedad
        Get
            Return cPago
        End Get
        ' la parte Set es la que se usa al asignar el nuevo valor
        Set(ByVal Value As String)
            If Value <> "" Then
                cPago = Value
            End If
        End Set
    End Property

    Public Property metodoPago() As String 'metodo de pago
        ' la parte Get es la que devuelve el valor de la propiedad
        Get
            Return mPago
        End Get
        ' la parte Set es la que se usa al asignar el nuevo valor
        Set(ByVal Value As String)
            If Value <> "" Then
                mPago = Value
            End If
        End Set
    End Property

    Public Property ordenCompra() As String 'orden de compra
        ' la parte Get es la que devuelve el valor de la propiedad
        Get
            Return ocompra
        End Get
        ' la parte Set es la que se usa al asignar el nuevo valor
        Set(ByVal Value As String)
            If Value <> "" Then
                ocompra = Value
            End If
        End Set
    End Property

    Public Property lugarExpedicion() As String 'lugar de expedicion
        ' la parte Get es la que devuelve el valor de la propiedad
        Get
            Return lExpedicion
        End Get
        ' la parte Set es la que se usa al asignar el nuevo valor
        Set(ByVal Value As String)
            If Value <> "" Then
                lExpedicion = Value
            End If
        End Set
    End Property

    Public Property moneda() As String 'Moneda
        ' la parte Get es la que devuelve el valor de la propiedad
        Get
            Return vMoneda
        End Get
        ' la parte Set es la que se usa al asignar el nuevo valor
        Set(ByVal Value As String)
            If Value <> "" Then
                vMoneda = Value
            End If
        End Set
    End Property

    Public Property consignatario() As String 'Consignatario
        ' la parte Get es la que devuelve el valor de la propiedad
        Get
            Return consig
        End Get
        ' la parte Set es la que se usa al asignar el nuevo valor
        Set(ByVal Value As String)
            If Value <> "" Then
                consig = Value
            End If
        End Set
    End Property

    Public Property paisCliente() As String 'pais del cliente
        ' la parte Get es la que devuelve el valor de la propiedad
        Get
            Return pCliente
        End Get
        ' la parte Set es la que se usa al asignar el nuevo valor
        Set(ByVal Value As String)
            If Value <> "" Then
                pCliente = Value
            End If
        End Set
    End Property

    Public Property codigoPostalCliente() As String 'codigo postal del cliente
        ' la parte Get es la que devuelve el valor de la propiedad
        Get
            Return cpCliente
        End Get
        ' la parte Set es la que se usa al asignar el nuevo valor
        Set(ByVal Value As String)
            If Value <> "" Then
                cpCliente = Value
            End If
        End Set
    End Property

    Public Property rfcCliente() As String 'rfc del cliente
        ' la parte Get es la que devuelve el valor de la propiedad
        Get
            Return clienteRfc
        End Get
        ' la parte Set es la que se usa al asignar el nuevo valor
        Set(ByVal Value As String)
            If Value <> "" Then
                clienteRfc = Value
            End If
        End Set
    End Property

    Public Property telefonoCliente() As String 'direccion del cliente
        ' la parte Get es la que devuelve el valor de la propiedad
        Get
            Return telCliente
        End Get
        ' la parte Set es la que se usa al asignar el nuevo valor
        Set(ByVal Value As String)
            If Value <> "" Then
                telCliente = Value
            End If
        End Set
    End Property
    Public Property EstadoCliente() As String 'direccion del cliente
        ' la parte Get es la que devuelve el valor de la propiedad
        Get
            Return estCliente
        End Get
        ' la parte Set es la que se usa al asignar el nuevo valor
        Set(ByVal Value As String)
            If Value <> "" Then
                estCliente = Value
            End If
        End Set
    End Property

    Public Property CiudadCliente() As String 'direccion del cliente
        ' la parte Get es la que devuelve el valor de la propiedad
        Get
            Return ciuCliente
        End Get
        ' la parte Set es la que se usa al asignar el nuevo valor
        Set(ByVal Value As String)
            If Value <> "" Then
                ciuCliente = Value
            End If
        End Set
    End Property

    Public Property coloniaCliente() As String 'direccion del cliente
        ' la parte Get es la que devuelve el valor de la propiedad
        Get
            Return colCliente
        End Get
        ' la parte Set es la que se usa al asignar el nuevo valor
        Set(ByVal Value As String)
            If Value <> "" Then
                colCliente = Value
            End If
        End Set
    End Property

    Public Property direccionCliente() As String 'direccion del cliente
        ' la parte Get es la que devuelve el valor de la propiedad
        Get
            Return dirCliente
        End Get
        ' la parte Set es la que se usa al asignar el nuevo valor
        Set(ByVal Value As String)
            If Value <> "" Then
                dirCliente = Value
            End If
        End Set
    End Property


    Public Property nombreCliente() As String 'Nombre del cliente
        ' la parte Get es la que devuelve el valor de la propiedad
        Get
            Return nomCliente
        End Get
        ' la parte Set es la que se usa al asignar el nuevo valor
        Set(ByVal Value As String)
            If Value <> "" Then
                nomCliente = Value
            End If
        End Set
    End Property

    Public Property numeroCuenta() As String 'Numero de cuenta
        ' la parte Get es la que devuelve el valor de la propiedad
        Get
            Return nCuenta
        End Get
        ' la parte Set es la que se usa al asignar el nuevo valor
        Set(ByVal Value As String)
            If Value <> "" Then
                nCuenta = Value
            End If
        End Set
    End Property

    Public Property condicionesVenta() As String 'condiciones de venta
        ' la parte Get es la que devuelve el valor de la propiedad
        Get
            Return cVenta
        End Get
        ' la parte Set es la que se usa al asignar el nuevo valor
        Set(ByVal Value As String)
            If Value <> "" Then
                cVenta = Value
            End If
        End Set
    End Property

    Public Property fechaVenta() As String 'fecha de venta
        ' la parte Get es la que devuelve el valor de la propiedad
        Get
            Return fVenta
        End Get
        ' la parte Set es la que se usa al asignar el nuevo valor
        Set(ByVal Value As String)
            If Value <> "" Then
                fVenta = Value
            End If
        End Set
    End Property

    Public Property fechaCfD() As String 'ruta de las fuentes
        ' la parte Get es la que devuelve el valor de la propiedad
        Get
            Return fCFD
        End Get
        ' la parte Set es la que se usa al asignar el nuevo valor
        Set(ByVal Value As String)
            If Value <> "" Then
                fCFD = Value
            End If
        End Set
    End Property
    Public Property fuentes() As String 'ruta de las fuentes
        ' la parte Get es la que devuelve el valor de la propiedad
        Get
            Return font
        End Get
        ' la parte Set es la que se usa al asignar el nuevo valor
        Set(ByVal Value As String)
            If Value <> "" Then
                font = Value
            End If
        End Set
    End Property

    Public Property altoLogoEmpresa() As String 'logo de la empresa
        ' la parte Get es la que devuelve el valor de la propiedad
        Get
            Return altologoEmp
        End Get
        ' la parte Set es la que se usa al asignar el nuevo valor
        Set(ByVal Value As String)
            If Value <> "" Then
                altologoEmp = Value
            End If
        End Set
    End Property

    Public Property anchoLogoEmpresa() As String 'logo de la empresa
        ' la parte Get es la que devuelve el valor de la propiedad
        Get
            Return anchologoEmp
        End Get
        ' la parte Set es la que se usa al asignar el nuevo valor
        Set(ByVal Value As String)
            If Value <> "" Then
                anchologoEmp = Value
            End If
        End Set
    End Property

    Public Property logoEmpresa() As String 'logo de la empresa
        ' la parte Get es la que devuelve el valor de la propiedad
        Get
            Return logoEmp
        End Get
        ' la parte Set es la que se usa al asignar el nuevo valor
        Set(ByVal Value As String)
            If Value <> "" Then
                logoEmp = Value
            End If
        End Set
    End Property
    Public Property direccionEmpresa4() As String 'Direccion de la empresa
        ' la parte Get es la que devuelve el valor de la propiedad
        Get
            Return direccionEmp4
        End Get
        ' la parte Set es la que se usa al asignar el nuevo valor
        Set(ByVal Value As String)
            If Value <> "" Then
                direccionEmp4 = Value
            End If
        End Set
    End Property
    Public Property direccionEmpresa3() As String 'Direccion de la empresa
        ' la parte Get es la que devuelve el valor de la propiedad
        Get
            Return direccionEmp3
        End Get
        ' la parte Set es la que se usa al asignar el nuevo valor
        Set(ByVal Value As String)
            If Value <> "" Then
                direccionEmp3 = Value
            End If
        End Set
    End Property
    Public Property direccionEmpresa2() As String 'Direccion de la empresa
        ' la parte Get es la que devuelve el valor de la propiedad
        Get
            Return direccionEmp2
        End Get
        ' la parte Set es la que se usa al asignar el nuevo valor
        Set(ByVal Value As String)
            If Value <> "" Then
                direccionEmp2 = Value
            End If
        End Set
    End Property

    Public Property direccionEmpresa() As String 'Direccion de la empresa
        ' la parte Get es la que devuelve el valor de la propiedad
        Get
            Return direccionEmp
        End Get
        ' la parte Set es la que se usa al asignar el nuevo valor
        Set(ByVal Value As String)
            If Value <> "" Then
                direccionEmp = Value
            End If
        End Set
    End Property

    Public Property NombreEmpres() As String 'Nombre de la empresa
        ' la parte Get es la que devuelve el valor de la propiedad
        Get
            Return nombreEmp
        End Get
        ' la parte Set es la que se usa al asignar el nuevo valor
        Set(ByVal Value As String)
            If Value <> "" Then
                nombreEmp = Value
            End If
        End Set
    End Property

    Public Property comprobante() As String 'Folio Fiscal
        ' la parte Get es la que devuelve el valor de la propiedad
        Get
            Return vComprobante
        End Get
        ' la parte Set es la que se usa al asignar el nuevo valor
        Set(ByVal Value As String)
            If Value <> "" Then
                vComprobante = Value
            End If
        End Set
    End Property


    Public Property folioFactura() As String 'Folio Fiscal
        ' la parte Get es la que devuelve el valor de la propiedad
        Get
            Return folioFac
        End Get
        ' la parte Set es la que se usa al asignar el nuevo valor
        Set(ByVal Value As String)
            If Value <> "" Then
                folioFac = Value
            End If
        End Set
    End Property

    Public Property cadenaConexion() As String 'cadena de conexion
        ' la parte Get es la que devuelve el valor de la propiedad
        Get
            Return cadena
        End Get
        ' la parte Set es la que se usa al asignar el nuevo valor
        Set(ByVal Value As String)
            If Value <> "" Then
                cadena = Value
            End If
        End Set
    End Property
    'Agregar caracteres al RFC para el UUID
    Public Function RFCUUID(ByVal rfc As String) As String
        Dim caracteres As String = "              " 'Variable para guardar los espascios del formato
        Dim vRFC As String = rfc 'Variable para guardar el total en entro
        Dim relleno As String 'Variable para guardar el relleno de ceros

        relleno = caracteres.Length - vRFC.Length

        vRFC = rfc & Right(caracteres, relleno)

        Return vRFC
    End Function
    'Agregar caracteres al total para el UUID
    Public Function TotalUUID(ByVal total As Double) As String
        Dim caracteres As String = "0000000000" 'Variable para guardar los espascios del formato
        Dim caracteres2 As String = "000000" 'Variable para guardar los espascios del formato
        Dim entreo As String = Math.Round(total, 0) 'Variable para guardar el total en entro
        Dim decimales As String = Mid(total, InStr(total, ".") + 1) 'Variable para guardar los decimales
        Dim relleno As String 'Variable para guardar el relleno de ceros
        Dim relleno2 As String 'Variable para guardar el relleno de ceros

        relleno2 = caracteres2.Length - decimales.Length
        relleno = caracteres.Length - entreo.Length

        decimales = Mid(total, InStr(total, ".") + 1) & Right(caracteres, relleno2)
        entreo = Right(caracteres, relleno) & Math.Round(total, 0) & "." & decimales

        Return entreo
    End Function
    'Leer los datos faltantes de el xml
    Public Function LeerXML(ByVal archivo As String) As Boolean
        Dim reader As New XmlTextReader(archivo)
        While (reader.Read())
            Select Case reader.NodeType
                Case XmlNodeType.Element
                    ' El nodo es un elemento.
                    While reader.MoveToNextAttribute()
                        If reader.Name = "Sello" Then
                            SELLO = reader.Value
                        End If
                        If reader.Name = "UUID" Then
                            UUID = UUID & "&id=" & reader.Value
                        End If
                        If reader.Name = "SelloSAT" Then
                            SELLO = reader.Value
                        End If
                        If reader.Name = "Certificado" Then
                            certificado = reader.Value
                        End If
                        If reader.Name = "FechaTimbrado" Then
                            FechaTimbrado = reader.Value
                        End If
                        If reader.Name = "SelloCFD" Then
                            selloCFD = reader.Value
                        End If
                        If reader.Name = "NoCertificadoSAT" Then
                            noCertificadoSAT = reader.Value
                        End If
                    End While
            End Select
        End While
    End Function

    'Ejecuta una consulta SSQL contra la Base de datos, INSERT UPDATE DELETE
    Public Function EjecutaQuery(ByVal sSQL As String, ByVal sStringConexion As String) As Boolean
        Dim oConn As New SqlConnection(sStringConexion)
        Dim iCant As Integer
        Try
            oConn.Open()
            Dim oCmd As New SqlCommand(sSQL, oConn)
            iCant = oCmd.ExecuteNonQuery()
            Return False
        Catch ex As Exception
            'ErrorPopUp(Now & vbCrLf & ex.Message.ToString & vbCrLf & sSQL)
            Return True
        Finally
            oConn.Close()
        End Try
    End Function

    Public Function digi(ByVal valor)
        Dim digitos
        'if para validar si el subtotal tiene digitos
        If InStr(valor, ".") <> 0 Then 'valor
            digitos = Mid(valor, InStr(valor, ".") + 1)
            'if para validar si tiene un solo digito
            If Len(digitos) <= 1 Then 'digitos
                valor = valor & "0"
            Else 'digitos
                valor = Mid(valor, 1, InStr(valor, ".") - 1)
                valor = valor & "." & Mid(digitos, 1, 2)
            End If 'digitos
        Else 'valor
            valor = valor & ".00"
        End If 'valor
        Return valor
    End Function

    'para truncar a 6 decimales
    Function truncarAseis(ByVal valor)
        Dim digitos As String 'Variable para guardar los digitos

        'if para validar si el subtotal tiene digitos
        If InStr(valor, ".") <> 0 Then 'valor
            digitos = Mid(valor, InStr(valor, ".") + 1)
            'if para validar si tiene un solo digito
            If Len(digitos) <= 1 Then 'digitos
                valor = valor & "0"
            Else 'digitos
                valor = Mid(valor, 1, InStr(valor, ".") - 1)
                valor = valor & "." & Mid(digitos, 1, 6)
            End If 'digitos
        Else 'valor
            valor = valor & ".00"
        End If 'valor
        truncarAseis = valor
    End Function

    'truncar a 2 digitos
    Function truncarAdos(ByVal valor)
        Dim digitos As String 'Variable para guardar los digitos

        'if para validar si el subtotal tiene digitos
        If InStr(valor, ".") <> 0 Then 'valor
            digitos = Mid(valor, InStr(valor, ".") + 1)
            'if para validar si tiene un solo digito
            If Len(digitos) <= 1 Then 'digitos
                valor = valor & "0"
            Else 'digitos
                valor = Mid(valor, 1, InStr(valor, ".") - 1)
                valor = valor & "." & Mid(digitos, 1, 2)
            End If 'digitos
        Else 'valor
            valor = valor & ".00"
        End If 'valor
        truncarAdos = valor
    End Function

    'funcion redondear a 2 digitos
    Function redondear(ByVal val As Double)

        'if para validar si tiene digitos
        If InStr(val, ".") <> 0 Then
            'redondear la funcion
            val = Math.Round(val, 2)
        Else
            val = val & ".00"
        End If
        redondear = val
    End Function


    Public Function consultas(ByVal sSQL As String, ByVal sStringConexion As String) As DataTable
        Dim datos As New DataTable 'Variable para guardar los registros    
        Dim consulta As New SqlDataAdapter(sSQL, sStringConexion) 'Variable para ejecutar la consulta
        Try
            consulta.Fill(datos)
            If datos.Rows.Count > 0 Then
                Return datos
            Else
                Return datos
            End If

        Catch ex As Exception
            Return Nothing
        Finally
            consulta = Nothing
            datos = Nothing
        End Try
    End Function

    Public Function configuraciones(ByVal comparacion As String) As String
        Dim objReader As New StreamReader(My.Application.Info.DirectoryPath & "\config.txt") 'Variable para guardar el archivo
        Dim sLine As String 'Variable para guardar la linea
        Dim arrText As New ArrayList() 'Array para guardar las lineas
        'Llenar el array con las lineas del archivo de configuraciones
        Do
            sLine = objReader.ReadLine()
            If Not sLine Is Nothing Then
                arrText.Add(sLine)
            End If
        Loop Until sLine Is Nothing
        objReader.Close()
        'for para recorrer el array con las lineas del archivo de configuraciones
        For i As Integer = 0 To arrText.Count - 1 'arrText
            'if para validar que tiene cadena de conexion
            If InStr(1, arrText(i), comparacion) > 0 Then 'CADENA
                Return arrText(i + 1)
            End If 'CADENA
            'if para validar que tiene cadena de conexion
            If InStr(1, arrText(i), comparacion) > 0 Then 'FUENTES
                Return arrText(i + 1)
            End If 'FUENTES
        Next 'arrText    
    End Function



    Public Function Letras(ByVal numero As String) As String
        '********Declara variables de tipo cadena************
        Dim palabras, entero, dec, flag As String

        '********Declara variables de tipo entero***********
        Dim num, x, y As Integer

        flag = "N"

        '**********Número Negativo***********
        If Mid(numero, 1, 1) = "-" Then
            numero = Mid(numero, 2, numero.ToString.Length - 1).ToString
            palabras = "menos "
        End If

        '**********Si tiene ceros a la izquierda*************
        For x = 1 To numero.ToString.Length
            If Mid(numero, 1, 1) = "0" Then
                numero = Trim(Mid(numero, 2, numero.ToString.Length).ToString)
                If Trim(numero.ToString.Length) = 0 Then palabras = ""
            Else
                Exit For
            End If
        Next

        '*********Dividir parte entera y decimal************
        For y = 1 To Len(numero)
            If Mid(numero, y, 1) = "." Then
                flag = "S"
            Else
                If flag = "N" Then
                    entero = entero + Mid(numero, y, 1)
                Else
                    dec = dec + Mid(numero, y, 1)
                End If
            End If
        Next y

        If Len(dec) = 1 Then dec = dec & "0"

        '**********proceso de conversión***********
        flag = "N"

        If Val(numero) <= 999999999 Then
            For y = Len(entero) To 1 Step -1
                num = Len(entero) - (y - 1)
                Select Case y
                    Case 3, 6, 9
                        '**********Asigna las palabras para las centenas***********
                        Select Case Mid(entero, num, 1)
                            Case "1"
                                If Mid(entero, num + 1, 1) = "0" And Mid(entero, num + 2, 1) = "0" Then
                                    palabras = palabras & "cien "
                                Else
                                    palabras = palabras & "ciento "
                                End If
                            Case "2"
                                palabras = palabras & "doscientos "
                            Case "3"
                                palabras = palabras & "trescientos "
                            Case "4"
                                palabras = palabras & "cuatrocientos "
                            Case "5"
                                palabras = palabras & "quinientos "
                            Case "6"
                                palabras = palabras & "seiscientos "
                            Case "7"
                                palabras = palabras & "setecientos "
                            Case "8"
                                palabras = palabras & "ochocientos "
                            Case "9"
                                palabras = palabras & "novecientos "
                        End Select
                    Case 2, 5, 8
                        '*********Asigna las palabras para las decenas************
                        Select Case Mid(entero, num, 1)
                            Case "1"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    flag = "S"
                                    palabras = palabras & "diez "
                                End If
                                If Mid(entero, num + 1, 1) = "1" Then
                                    flag = "S"
                                    palabras = palabras & "once "
                                End If
                                If Mid(entero, num + 1, 1) = "2" Then
                                    flag = "S"
                                    palabras = palabras & "doce "
                                End If
                                If Mid(entero, num + 1, 1) = "3" Then
                                    flag = "S"
                                    palabras = palabras & "trece "
                                End If
                                If Mid(entero, num + 1, 1) = "4" Then
                                    flag = "S"
                                    palabras = palabras & "catorce "
                                End If
                                If Mid(entero, num + 1, 1) = "5" Then
                                    flag = "S"
                                    palabras = palabras & "quince "
                                End If
                                If Mid(entero, num + 1, 1) > "5" Then
                                    flag = "N"
                                    palabras = palabras & "dieci"
                                End If
                            Case "2"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    palabras = palabras & "veinte "
                                    flag = "S"
                                Else
                                    palabras = palabras & "veinti"
                                    flag = "N"
                                End If
                            Case "3"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    palabras = palabras & "treinta "
                                    flag = "S"
                                Else
                                    palabras = palabras & "treinta y "
                                    flag = "N"
                                End If
                            Case "4"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    palabras = palabras & "cuarenta "
                                    flag = "S"
                                Else
                                    palabras = palabras & "cuarenta y "
                                    flag = "N"
                                End If
                            Case "5"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    palabras = palabras & "cincuenta "
                                    flag = "S"
                                Else
                                    palabras = palabras & "cincuenta y "
                                    flag = "N"
                                End If
                            Case "6"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    palabras = palabras & "sesenta "
                                    flag = "S"
                                Else
                                    palabras = palabras & "sesenta y "
                                    flag = "N"
                                End If
                            Case "7"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    palabras = palabras & "setenta "
                                    flag = "S"
                                Else
                                    palabras = palabras & "setenta y "
                                    flag = "N"
                                End If
                            Case "8"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    palabras = palabras & "ochenta "
                                    flag = "S"
                                Else
                                    palabras = palabras & "ochenta y "
                                    flag = "N"
                                End If
                            Case "9"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    palabras = palabras & "noventa "
                                    flag = "S"
                                Else
                                    palabras = palabras & "noventa y "
                                    flag = "N"
                                End If
                        End Select
                    Case 4, 7
                        '*********Asigna las palabras para las unidades*********
                        Select Case Mid(entero, num, 1)
                            Case "1"
                                If flag = "N" Then
                                    If y = 1 Then
                                        palabras = palabras & "uno "
                                    Else
                                        palabras = palabras & "un "
                                    End If
                                End If
                            Case "2"
                                If flag = "N" Then palabras = palabras & "dos "
                            Case "3"
                                If flag = "N" Then palabras = palabras & "tres "
                            Case "4"
                                If flag = "N" Then palabras = palabras & "cuatro "
                            Case "5"
                                If flag = "N" Then palabras = palabras & "cinco "
                            Case "6"
                                If flag = "N" Then palabras = palabras & "seis "
                            Case "7"
                                If flag = "N" Then palabras = palabras & "siete "
                            Case "8"
                                If flag = "N" Then palabras = palabras & "ocho "
                            Case "9"
                                If flag = "N" Then palabras = palabras & "nueve "
                        End Select

                    Case 1
                        '*********Asigna las palabras para las unidades*********
                        Select Case Mid(entero, num, 1)
                            Case "1"
                                palabras = palabras & "uno "
                            Case "2"
                                palabras = palabras & "dos "
                            Case "3"
                                palabras = palabras & "tres "
                            Case "4"
                                palabras = palabras & "cuatro "
                            Case "5"
                                palabras = palabras & "cinco "
                            Case "6"
                                palabras = palabras & "seis "
                            Case "7"
                                palabras = palabras & "siete "
                            Case "8"
                                palabras = palabras & "ocho "
                            Case "9"
                                palabras = palabras & "nueve "
                        End Select
                End Select

                '***********Asigna la palabra mil***************
                If y = 4 Then
                    If Mid(entero, 6, 1) <> "0" Or Mid(entero, 5, 1) <> "0" Or Mid(entero, 4, 1) <> "0" Or
                    (Mid(entero, 6, 1) = "0" And Mid(entero, 5, 1) = "0" And Mid(entero, 4, 1) = "0" And
                    Len(entero) <= 6) Then
                        palabras = palabras & "mil "
                        'flag = "N"
                    End If
                End If

                '**********Asigna la palabra millón*************
                If y = 7 Then
                    If Len(entero) = 7 And Mid(entero, 1, 1) = "1" Then
                        palabras = palabras & "millón "
                    Else
                        palabras = palabras & "millones "
                    End If
                End If
            Next y

            '**********Une la parte entera y la parte decimal*************
            If dec <> "" Then
                centavos = dec
                Letras = palabras
            Else
                Letras = palabras
            End If
        Else
            Letras = ""
        End If
    End Function
End Class
