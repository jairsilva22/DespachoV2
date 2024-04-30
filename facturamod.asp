<%@  language="VBSCRIPT" codepage="65001" %>
<!--#include file="Connections/Conecta1.asp" -->
<!--#include file="Connections/PTV.asp" -->
<%
Dim Recordset3__MMColParam
Recordset3__MMColParam = "1"
If (Request.Cookies("logn")("idSucursal") <> "") Then 
  Recordset3__MMColParam = Request.Cookies("logn")("idSucursal")
End If
%>
<!--#include file="config.asp"-->
<!--#include file="checklogin.asp"-->
<!--#include file="stylo2.asp"-->
<script language="JavaScript" src="overlib_mini.js"></script>
<%
dim doc'Variable para guardar el tipo de documento

'if para validar el tipo de documento
if Request.Form("abreviatura") = "FA" then
doc = " AND factura = 'True'"
elseif Request.Form("abreviatura") = "CRED" then
doc = " AND nCredito = 'True'"
elseif Request.Form("abreviatura") = "CAR" then
doc = " AND nCargo = 'True'"
end if
%>
<%
Dim MM_editAction
MM_editAction = CStr(Request.ServerVariables("SCRIPT_NAME"))
If (Request.QueryString <> "") Then
  MM_editAction = MM_editAction & "?" & Server.HTMLEncode(Request.QueryString)
End If

' boolean to abort record edit
Dim MM_abortEdit
MM_abortEdit = false
%>
<%
' IIf implementation
Function MM_IIf(condition, ifTrue, ifFalse)
  If condition = "" Then
    MM_IIf = ifFalse
  Else
    MM_IIf = ifTrue
  End If
End Function
%>
<%
If (CStr(Request("MM_update")) = "form1") Then
  If (Not MM_abortEdit) Then
    ' execute the update
    Dim MM_editCmd

  If Request.Form("abreviatura") = "CRED" Then
    vendedor = Request.Form("vendor")
  Else
    vendedor = Request.Form("vendedor")
  End If

    Set MM_editCmd = Server.CreateObject ("ADODB.Command")
    MM_editCmd.ActiveConnection = MM_Conecta1_STRING
    MM_editCmd.CommandText = "UPDATE dbo.factura SET tipo_comprobante = ?, serie = ?, forma_pago = ?, retencion = ?, moneda = ?, idcliente = ?, embarque = ?, ASN = ?, vendedor = ?, ordenCompra = ?, fEmbarque = ?, VA = ?, terminos = ?, abreviatura = ?, cambio = ?, obsCliente = '"&Request.Form("obsCliente")&"', envioSatCancelada = '"&Request.Form("bajar")&"', NumCtaPago = '"&Request.Form("NumCtaPago")&"', metodoPago='"&Request.Form("metodoPago")&"', condicionesDePago='"&Request.Form("condicionesDePago")&"' WHERE idfactura = ?" 
    MM_editCmd.Prepared = true
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param1", 201, 1, 255, Request.Form("tipo_comprobante")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param2", 201, 1, 255, Request.Form("serie")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param3", 201, 1, 255, Request.Form("forma_pago2")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param4", 201, 1, 255, Request.Form("retencion")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param5", 5, 1, -1, MM_IIF(Request.Form("moneda"), Request.Form("moneda"), null)) ' adDouble
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param6", 5, 1, -1, MM_IIF(Request.Form("idcliente"), Request.Form("idcliente"), null)) ' adDouble
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param7", 201, 1, 255, Request.Form("embarque")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param8", 201, 1, 255, Request.Form("ASN")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param9", 201, 1, 255, vendedor) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param10", 201, 1, 255, Request.Form("ordenCompra")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param11", 135, 1, -1, MM_IIF(Request.Form("fEmbarque"), Request.Form("fEmbarque"), null)) ' adDBTimeStamp
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param12", 201, 1, 255, Request.Form("VA")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param14", 201, 1, 255, Request.Form("terminos")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param15", 201, 1, 255, Request.Form("abreviatura")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param16", 5, 1, -1, MM_IIF(Request.Form("cambio"), Request.Form("cambio"), null)) ' adDouble
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param17", 5, 1, -1, MM_IIF(Request.Form("MM_recordId"), Request.Form("MM_recordId"), null)) ' adDouble
    MM_editCmd.Execute
    MM_editCmd.ActiveConnection.Close

    ' append the query string to the redirect URL
    Dim MM_editRedirectUrl

    If Request.Form("abreviatura") = "CRED" Then
      'validamos si se afecta el inventario
      If Request.Form("bajar") = "SI" Then
        'validamos si los clientes son los mismos
        If Request.Form("cliente2") <> Request.Form("idcliente") Then
          'eliminamos todos los detalles
          Set Recordset1_cmd = Server.CreateObject ("ADODB.Command")
          Recordset1_cmd.ActiveConnection = MM_Conecta1_STRING
          Recordset1_cmd.CommandText = "DELETE FROM detFactura WHERE id_factura = "&Request.QueryString("idfactura") 
          Recordset1_cmd.Prepared = true

          Set Recordset1 = Recordset1_cmd.Execute

          'actualizamos a 0 los totales'
          Set Recordset1_cmd = Server.CreateObject ("ADODB.Command")
          Recordset1_cmd.ActiveConnection = MM_Conecta1_STRING
          Recordset1_cmd.CommandText = "UPDATE factura SET iva = 0, subtotal = 0, total = 0, referencia = NULL WHERE idfactura = "&Request.QueryString("idfactura") 
          Recordset1_cmd.Prepared = true

          Set Recordset1 = Recordset1_cmd.Execute

          'vamos a la pantalla de seleccion de folio
          Response.Redirect("selectFactura.asp?idfactura="&Request.QueryString("idfactura")&"&idCliente="&Request.QueryString("idCliente"))
        Else
          'buscamos si tiene detalles
          Set Recordset1_cmd = Server.CreateObject ("ADODB.Command")
          Recordset1_cmd.ActiveConnection = MM_Conecta1_STRING
          Recordset1_cmd.CommandText = "SELECT COUNT(id_detfactura) AS cuantos FROM detFactura WHERE id_factura = "&Request.QueryString("idfactura") 
          Recordset1_cmd.Prepared = true

          Set Recordset1 = Recordset1_cmd.Execute
          Recordset1_numRows = 0

          If Recordset1.Fields.Item("cuantos").Value <> "0" Then
            MM_editRedirectUrl = "detNotaCredito.asp"
          Else
            MM_editRedirectUrl = "selectFactura.asp"
          End If
        End If
      Else
          'actualizamos a null la referencia
          Set Recordset1_cmd = Server.CreateObject ("ADODB.Command")
          Recordset1_cmd.ActiveConnection = MM_Conecta1_STRING
          Recordset1_cmd.CommandText = "UPDATE factura SET referencia = NULL WHERE idfactura = "&Request.QueryString("idfactura") 
          Recordset1_cmd.Prepared = true

          Set Recordset1 = Recordset1_cmd.Execute
        MM_editRedirectUrl = "detfacturaadd.asp"  
      End If
    Else
      MM_editRedirectUrl = "detfacturaadd.asp"
    End If

    If (Request.QueryString <> "") Then
      If (InStr(1, MM_editRedirectUrl, "?", vbTextCompare) = 0) Then
        MM_editRedirectUrl = MM_editRedirectUrl & "?" & Request.QueryString
      Else
        MM_editRedirectUrl = MM_editRedirectUrl & "&" & Request.QueryString
      End If
    End If
    Response.Redirect(MM_editRedirectUrl)
  End If
End If
%>
<%
' *** Delete Record: construct a sql delete statement and execute it
If (CStr(Request("MM_delete")) = "form2" And CStr(Request("MM_recordId")) <> "") Then

  If (Not MM_abortEdit) Then
'------------Recordset para eliminar los detalles de la factura -----------------------------
  Set Recordset1_cmd = Server.CreateObject ("ADODB.Command")
Recordset1_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset1_cmd.CommandText = "DELETE FROM dbo.detFactura WHERE id_factura = ?" 
Recordset1_cmd.Prepared = true
Recordset1_cmd.Parameters.Append Recordset1_cmd.CreateParameter("param1", 5, 1, -1,  Request.Form("MM_recordId")) ' adDouble

Set Recordset1 = Recordset1_cmd.Execute
  
  End If

End If  
%>
<%
' *** Delete Record: construct a sql delete statement and execute it

If (CStr(Request("MM_delete")) = "form2" And CStr(Request("MM_recordId")) <> "") Then

  If (Not MM_abortEdit) Then
    ' execute the delete
    Set MM_editCmd = Server.CreateObject ("ADODB.Command")
    MM_editCmd.ActiveConnection = MM_Conecta1_STRING
    MM_editCmd.CommandText = "DELETE FROM dbo.factura WHERE idfactura = ?"
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param1", 5, 1, -1, Request.Form("MM_recordId")) ' adDouble
    MM_editCmd.Execute
    MM_editCmd.ActiveConnection.Close

    ' append the query string to the redirect URL
    'Dim MM_editRedirectUrl
    MM_editRedirectUrl = "factura.asp"
    If (Request.QueryString <> "") Then
      If (InStr(1, MM_editRedirectUrl, "?", vbTextCompare) = 0) Then
        MM_editRedirectUrl = MM_editRedirectUrl & "?" & Request.QueryString
      Else
        MM_editRedirectUrl = MM_editRedirectUrl & "&" & Request.QueryString
      End If
    End If
    Response.Redirect(MM_editRedirectUrl)
  End If

End If
%>
<%
Dim Recordset1__MMColParam
Recordset1__MMColParam = "1"
If (Request.QueryString("idfactura") <> "") Then 
  Recordset1__MMColParam = Request.QueryString("idfactura")
End If
%>
<%
Dim Recordset1
Dim Recordset1_cmd
Dim Recordset1_numRows

Set Recordset1_cmd = Server.CreateObject ("ADODB.Command")
Recordset1_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset1_cmd.CommandText = "SELECT ASN, tipo_comprobante, serie, retencion, idcliente, vendedor, embarque, ordenCompra, fEmbarque, VA, FOB, terminos, idfactura, abreviatura, forma_pago, moneda, cambio, obsCliente, envioSatCancelada, NumCtaPago, metodoPago, condicionesDePago FROM dbo.factura WHERE idfactura = ?" 
Recordset1_cmd.Prepared = true
Recordset1_cmd.Parameters.Append Recordset1_cmd.CreateParameter("param1", 5, 1, -1, Recordset1__MMColParam) ' adDouble

Set Recordset1 = Recordset1_cmd.Execute
Recordset1_numRows = 0
%>
<%
Dim Recordset2__MMColParam
Recordset2__MMColParam = "1"
If (Request.Cookies("logn")("idSucursal") <> "") Then 
  Recordset2__MMColParam = Request.Cookies("logn")("idSucursal")
End If
%>
<%
Dim Recordset2
Dim Recordset2_cmd
Dim Recordset2_numRows

Set Recordset2_cmd = Server.CreateObject ("ADODB.Command")
Recordset2_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset2_cmd.CommandText = "SELECT * FROM dbo.clientesFacturacion WHERE idempresa = ? ORDER BY nombreCliente ASC" 
Recordset2_cmd.Prepared = true
Recordset2_cmd.Parameters.Append Recordset2_cmd.CreateParameter("param1", 5, 1, -1, Recordset2__MMColParam) ' adDouble

Set Recordset2 = Recordset2_cmd.Execute
Recordset2_numRows = 0
%>
<%
Dim Recordset3
Dim Recordset3_cmd
Dim Recordset3_numRows

Set Recordset3_cmd = Server.CreateObject ("ADODB.Command")
Recordset3_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset3_cmd.CommandText = "SELECT * FROM dbo.documento WHERE idempresa = ? ORDER BY descripcion ASC" 
Recordset3_cmd.Prepared = true
Recordset3_cmd.Parameters.Append Recordset3_cmd.CreateParameter("param1", 5, 1, -1, Recordset3__MMColParam) ' adDouble

Set Recordset3 = Recordset3_cmd.Execute
Recordset3_numRows = 0
%>
<%
Dim Recordset5
Dim Recordset5_cmd
Dim Recordset5_numRows

Set Recordset5_cmd = Server.CreateObject ("ADODB.Command")
Recordset5_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset5_cmd.CommandText = "SELECT * FROM dbo.formPago ORDER BY descripcion ASC" 
Recordset5_cmd.Prepared = true

Set Recordset5 = Recordset5_cmd.Execute
Recordset5_numRows = 0
%>
<%
Dim Recordset6__MMColParam
Recordset6__MMColParam = "1"
If (Request.Cookies("logn")("idSucursal") <> "") Then 
  Recordset6__MMColParam = Request.Cookies("logn")("idSucursal")
End If
%>
<%
Dim Recordset6
Dim Recordset6_cmd
Dim Recordset6_numRows

Set Recordset6_cmd = Server.CreateObject ("ADODB.Command")
Recordset6_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset6_cmd.CommandText = "SELECT * FROM dbo.sucursales AS empresas, dbo.ciudades AS ciudad, dbo.estados AS estado WHERE empresas.id = ? AND ciudad.id = empresas.idCiudad AND estado.id = empresas.idEstado" 
Recordset6_cmd.Prepared = true
Recordset6_cmd.Parameters.Append Recordset6_cmd.CreateParameter("param1", 5, 1, -1, Recordset6__MMColParam) ' adDouble

Set Recordset6 = Recordset6_cmd.Execute
Recordset6_numRows = 0
%>
<%
Dim Recordset7
Dim Recordset7_cmd
Dim Recordset7_numRows

Set Recordset7_cmd = Server.CreateObject ("ADODB.Command")
Recordset7_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset7_cmd.CommandText = "SELECT * FROM dbo.moneda ORDER BY descripcion ASC" 
Recordset7_cmd.Prepared = true

Set Recordset7 = Recordset7_cmd.Execute
Recordset7_numRows = 0
%>
<%
Dim Recordset4__MMColParam
Recordset4__MMColParam = "1"
If (Request.Cookies("logn")("idSucursal") <> "") Then 
  Recordset4__MMColParam = Request.Cookies("logn")("idSucursal")
End If
%>
<%
Dim Recordset4
Dim Recordset4_cmd
Dim Recordset4_numRows

Set Recordset4_cmd = Server.CreateObject ("ADODB.Command")
Recordset4_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset4_cmd.CommandText = "SELECT * FROM dbo.folios WHERE idEmpresa = ?"&doc
Recordset4_cmd.Prepared = true
Recordset4_cmd.Parameters.Append Recordset4_cmd.CreateParameter("param1", 5, 1, -1, Recordset4__MMColParam) ' adDouble

Set Recordset4 = Recordset4_cmd.Execute
Recordset4_numRows = 0

Dim Recordset8
Dim Recordset8_cmd
Dim Recordset8_numRows

Set Recordset8_cmd = Server.CreateObject ("ADODB.Command")
Recordset8_cmd.ActiveConnection = MM_Conecta1_STRING
'Recordset8_cmd.CommandText = "SELECT DISTINCT(vendedor) FROM dbo.factura WHERE vendedor IS NOT NULL AND vendedor <> '' AND abreviatura = 'FA' AND ASN <> '' ORDER BY vendedor ASC" 
Recordset8_cmd.CommandText = "SELECT nombre AS vendedor FROM usuarios JOIN perfiles ON idPerfil = perfiles.id WHERE perfiles.descripcion = 'vendedor' or perfiles.descripcion = 'VENDEDOR' AND idSucursal = "&Request.Cookies("login")("idSucursal")&" ORDER BY vendedor ASC" 
Recordset8_cmd.Prepared = true

Set Recordset8 = Recordset8_cmd.Execute
%>
<script language="javascript1.2">
var abreviatura = new Array()//array para guardar las abrebiaturas
var iddocumento = new Array()//array para guardar los id de los odcumetnos
var n//variable contador

var tcambio = new Array()//array para guardar los cambios
var idmoneda = new Array()//array para guardar los id de las monedas
var a//variable contador

a = 0
n = 0
</script>
<%
dim tdoc
dim serie
dim fpago
dim retencion
dim cliente
dim entrega
dim asn
dim vendedor
dim ocompra
dim fembarque
dim va
dim fob
dim terminos
dim abreviatura
dim moneda
dim cambio
dim rebajar

if Request.Form("embarque") <> "" then
  entrega = Request.Form("embarque")
else
  entrega = Recordset1.Fields.Item("embarque").Value
end if

if Request.Form("terminos") <> "" then
  terminos = Request.Form("terminos")
else
  terminos = Recordset1.Fields.Item("terminos").Value
end if

if Request.Form("cambio") <> "" then
cambio = Request.Form("cambio")
else
cambio = Recordset1.Fields.Item("cambio").Value
end if

if Request.Form("moneda") <> "" then
moneda = Request.Form("moneda")
else
moneda = Recordset1.Fields.Item("moneda").Value
end if


if Request.Form("tipo_comprobante") <> "" then
tdoc = Request.Form("tipo_comprobante")
else
tdoc = Recordset1.Fields.Item("tipo_comprobante").Value
end if

if Request.Form("serie") <> "" then
serie = Request.Form("serie")
else
serie = Recordset1.Fields.Item("serie").Value
end if

if Request.Form("forma_pago2") <> "" then
fpago = Request.Form("forma_pago2")
else
fpago = Recordset1.Fields.Item("forma_pago").Value
end if

if Request.Form("retencion") <> "" then
retencion = Request.Form("retencion")
else
retencion = Recordset1.Fields.Item("retencion").Value
end if

if Request.Form("idcliente") <> "" then
cliente = Request.Form("idcliente")
else
cliente = Recordset1.Fields.Item("idcliente").Value
end if

if Request.Form("ASN") <> "" then
asn = Request.Form("ASN")
else
asn = Recordset1.Fields.Item("ASN").Value
end if

if Request.Form("vendedor") <> "" then
vendedor = Request.Form("vendedor")
else
vendedor = Recordset1.Fields.Item("vendedor").Value
end if

if Request.Form("ordenCompra") <> "" then
ocompra = Request.Form("ordenCompra")
else
ocompra = Recordset1.Fields.Item("ordenCompra").Value 
end if

if Request.Form("fEmbarque") <> "" then
fembarque = Request.Form("fEmbarque")
else
fembarque = Recordset1.Fields.Item("fEmbarque").Value
end if

if Request.Form("VA") <> "" then
va = Request.Form("VA")
else
va = Recordset1.Fields.Item("VA").Value
end if

if Request.Form("FOB") <> "" then
fob = Request.Form("FOB")
else
fob = Recordset1.Fields.Item("FOB").Value
end if

if Request.Form("abreviatura") <> "" then
abreviatura = Request.Form("abreviatura")
else
abreviatura = Recordset1.Fields.Item("abreviatura").Value
end if

If Request.Form("bajar") <> "" Then
  rebajar = Request.Form("bajar")
Else
  rebajar = Recordset1.Fields.Item("envioSatCancelada").Value
End If

If Request.Form("metodoPago") <> "" Then
  metodoPago = Request.Form("metodoPago")
Else
  metodoPago = Recordset1.Fields.Item("metodoPago").Value
End If

If Request.Form("NumCtaPago") <> "" Then
  NumCtaPago = Request.Form("NumCtaPago")
Else
  NumCtaPago = Recordset1.Fields.Item("NumCtaPago").Value
End If

If Request.Form("condicionesDePago") <> "" Then
  condicionesDePago = Request.Form("condicionesDePago")
Else
  condicionesDePago = Recordset1.Fields.Item("condicionesDePago").Value
End If


If abreviatura = "CRED" Then
    obsCliente = Recordset1.Fields.Item("obsCliente").Value
Else
    Set Recordset10_cmd = Server.CreateObject ("ADODB.Command")
    Recordset10_cmd.ActiveConnection = MM_Conecta1_STRING
    Recordset10_cmd.CommandText = "SELECT obsFactura FROM dbo.confimenor WHERE idconf = 1 AND refacciones = 'True'" 
    Recordset10_cmd.Prepared = true
    Set Recordset10 = Recordset10_cmd.Execute
    
    if not Recordset10.eof then
    obsCliente = "Vendedor: " & Request.Form("vendedor")&" "&Recordset10.Fields.Item("obsFactura").Value
    else
    obsCliente = "Vendedor: " & Request.Form("vendedor")
    end if
End If

Dim RecordsetFormSAT
Dim RecordsetFormSAT_cmd
Dim RecordsetFormSAT_numRows

Set RecordsetFormSAT_cmd = Server.CreateObject ("ADODB.Command")
RecordsetFormSAT_cmd.ActiveConnection = MM_Conecta1_STRING
RecordsetFormSAT_cmd.CommandText = "SELECT * FROM dbo.formaPagoSat ORDER BY descripcion ASC" 
RecordsetFormSAT_cmd.Prepared = true

Set RecordsetFormSAT = RecordsetFormSAT_cmd.Execute
RecordsetFormSAT_numRows = 0
%>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<!-- InstanceBegin template="Templates/plantillacfd.dwt.asp" codeOutsideHTMLIsLocked="false" -->
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<link rel="stylesheet" href="efectos/css/demos.css" media="screen" type="text/css">
    
	<script type="text/javascript" src="efectos/js/menu-for-applications.js"></script>
    <script language="JavaScript" src="jsF/overlib_mini.js"></script>
    <!-- InstanceBeginEditable name="doctitle" -->
   <title><%=titlePage%></title>
    <link rel="stylesheet" href="jsF/css.css" type="text/css" media="screen"  />
    <link rel="stylesheet" href="jsF/bootstrap.css" />
    <script src="jsF/jquery.min.js" type="text/javascript"></script>
    <script src="jsF/bootstrap.min.js"></script>
    <!-- InstanceEndEditable -->
    <style type="text/css">
        <!--
        body {
            background-color: white;
            margin-left: 0px;
            margin-top: 0px;
            margin-right: 0px;
            margin-bottom: 0px;
        }

        .Estilo1 {
            color: #000000
        }

        .Estilo7 {
            font-size: 9px
        }

        .Estilo11 {
            font-weight: bold;
            color: #000000;
            font-size: 14px;
        }

        .template1 {
            font-size: 12px;
        }

        .inicio {
            background-position: center;
            height: auto;
            width: 800px;
        }
        -->
    </style>
    <!-- InstanceBeginEditable name="head" -->
    <script>
            function SoloNumerosDec(evt) {
        if (window.event) {//asignamos el valor de la tecla a keynum
            keynum = evt.keyCode; //IE
        }
        else {
            keynum = evt.which; //FF
        }
        //comprobamos si se encuentra en el rango numérico y que teclas no recibirá.
        if ((keynum > 47 && keynum < 58) || keynum == 8 || keynum == 13 || keynum == 6 || keynum == 46) {
            return true;
        }
        else {
            return false;
        }
    }
    </script>
    <!-- InstanceEndEditable -->
</head>

<body>
    <table width="1024" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
       
        <tr>
            <td>
                <!-- InstanceBeginEditable name="EditRegion1" -->
                <p>&nbsp;</p>
                <div   style="overflow:auto; width: 100%; height: 100%; scrollbar-arrow-color : #B2B2B2; scrollbar-face-color : #3FA5DC;
 scrollbar-track-color:#B6D0F3;"  >
                <form id="form1" name="form1" method="POST" action="<%=MM_editAction%>" onsubmit="return val(this)">
                    <table width="80%" border="0" align="center">
                        <tr>
                            <td>
                                <table width="50%" border="0" align="center">
                                    <tr align="center">
                                        <td><strong>Seleccione los datos necesario   para crear la modificacion de la factura</strong></td>
                                    </tr>
                                    <tr>
                                        <td align="right">&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="87%" border="1" align="center" cellspacing="1">
                                    <tr>
                                        <th colspan="4" align="center" bgcolor="<%=ctabla%>" scope="col">Emisor</th>
                                    </tr>
                                    <tr>
                                        <td width="160">Nombre</td>
                                        <td width="251"><%=(Recordset6.Fields.Item("nombre").Value)%></td>
                                        <td width="188">Factura Interna</td>
                                        <td width="278"><%=Request.QueryString("nfactura")%></td>
                                    </tr>
                                    <tr>
                                        <td>R.F.C.</td>
                                        <td><%=(Recordset6.Fields.Item("rfc").Value)%></td>
                                        
                                    </tr>
                                    <tr>
                                        <td>Calle</td>
                                        <td><%=(Recordset6.Fields.Item("calle").Value)%></td>
                                        <td>Estado</td>
                                        <td><%=(Recordset6.Fields.Item("estado").Value)%></td>
                                    </tr>
                                    <tr>
                                        <td>Codigo Postal</td>
                                        <td><%=(Recordset6.Fields.Item("codigoPostal").Value)%></td>
                                        <td>Ciudad</td>
                                        <td><%=(Recordset6.Fields.Item("ciudad").Value)%></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%" border="0" align="center">
                                    <tr>
                                        <td align="right">Tipo de Documento:</td>
                                        <td bgcolor="#99FFCC">
                                            <select name="tipo_comprobante" id="tipo_comprobante" onchange="iniciales()">
                                                <option value="" <%If (Not isNull(tdoc)) Then If ("" = CStr(tdoc)) Then Response.Write("selected=""selected""") : Response.Write("")%>>Seleccionar</option>
                                                <%
                                                While (NOT Recordset3.EOF)
                                                %>
                                                <script language="JavaScript1.2" type="text/javascript">
                                                    abreviatura[n] = '<%=(Recordset3.Fields.Item("abrebiatura").Value)%>'
                                                    iddocumento[n] = '<%=(Recordset3.Fields.Item("iddocumento").Value)%>'
                                                    n = n + 1
                                                </script>
                                                <option value="<%=(Recordset3.Fields.Item("iddocumento").Value)%>" <%If (Not isNull(tdoc)) Then If (CStr(Recordset3.Fields.Item("iddocumento").Value) = CStr(tdoc)) Then Response.Write("selected=""selected""") : Response.Write("")%>><%=(Recordset3.Fields.Item("descripcion").Value)%></option>
                                                <%
                                                  Recordset3.MoveNext()
                                                Wend
                                                If (Recordset3.CursorType > 0) Then
                                                  Recordset3.MoveFirst
                                                Else
                                                  Recordset3.Requery
                                                End If
                                                %>
                                            </select></td>
                                        <td>Método de Pago</td>
                                        <td bgcolor="#99FFCC">
                                            <select name="forma_pago2" id="forma_pago2">
                                                <option value="" <%If (Not isNull(fpago)) Then If ("" = CStr(fpago)) Then Response.Write("selected=""selected""") : Response.Write("")%>>Seleccionar</option>
                                                <%
                                                While (NOT Recordset5.EOF)
                                                %>
                                                <option value="<%=(Recordset5.Fields.Item("idpago").Value)%>" <%If (Not isNull(fpago)) Then If (CStr(Recordset5.Fields.Item("idpago").Value) = CStr(fpago)) Then Response.Write("selected=""selected""") : Response.Write("")%>><%=(Recordset5.Fields.Item("forma_pago").Value)%> - <%=(Recordset5.Fields.Item("descripcion").Value)%></option>
                                                <%
                                                  Recordset5.MoveNext()
                                                Wend
                                                If (Recordset5.CursorType > 0) Then
                                                  Recordset5.MoveFirst
                                                Else
                                                  Recordset5.Requery
                                                End If
                                                %>
                                            </select></td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="right">Series</td>
                                        <td bgcolor="#99FFCC">
                                            <select name="serie" id="serie">
                                                <option value="" <%If (Not isNull(serie)) Then If ("" = CStr(serie)) Then Response.Write("selected=""selected""") : Response.Write("")%>>Seleccionar</option>
                                                <%
                                                While (NOT Recordset4.EOF)
                                                %>
                                                <option value="<%=(Recordset4.Fields.Item("Serie").Value)%>" <%If (Not isNull(serie)) Then If (CStr(Recordset4.Fields.Item("Serie").Value) = CStr(serie)) Then Response.Write("selected=""selected""") : Response.Write("")%>><%=(Recordset4.Fields.Item("Serie").Value)%></option>
                                                <%
                                                  Recordset4.MoveNext()
                                                Wend
                                                If (Recordset4.CursorType > 0) Then
                                                  Recordset4.MoveFirst
                                                Else
                                                  Recordset4.Requery
                                                End If
                                                %>
                                            </select></td>
                                        <td>Moneda:</td>
                                        <td bgcolor="#99FFCC">
                                            <select name="moneda" id="moneda" onchange="fcambio()">
                                                <option value="" <%If (Not isNull(moneda)) Then If ("" = CStr(moneda)) Then Response.Write("selected=""selected""") : Response.Write("")%>>Seleccionar</option>
                                                <%
                                                While (NOT Recordset7.EOF)
                                                %>
                                                <script language="JavaScript1.2" type="text/javascript">
                                                    //Se llenan los array				
                                                    tcambio[a] = '<%=(Recordset7.Fields.Item("tcambio").Value)%>'
                                                    idmoneda[a] = '<%=(Recordset7.Fields.Item("idmd").Value)%>'
                                                    a = a + 1
                                                </script>
                                                <option value="<%=(Recordset7.Fields.Item("idmd").Value)%>" <%If (Not isNull(moneda)) Then If (CStr(Recordset7.Fields.Item("idmd").Value) = CStr(moneda)) Then Response.Write("selected=""selected""") : Response.Write("")%>><%=(Recordset7.Fields.Item("descripcion").Value)%></option>
                                                <%
                                                  Recordset7.MoveNext()
                                                Wend
                                                If (Recordset7.CursorType > 0) Then
                                                  Recordset7.MoveFirst
                                                Else
                                                  Recordset7.Requery
End If
                                                %>
                                            </select></td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="right">Cliente:</td>
                                        <td>
                                            <select name="idcliente" id="idcliente" onchange="iniciales()">
                                                <option value="" <%If (Not isNull(cliente)) Then If ("" = CStr(cliente)) Then Response.Write("selected=""selected""") : Response.Write("")%>>Seleccionar</option>
                                                <%
                                                While (NOT Recordset2.EOF)
                                                %>
                                                <option value="<%=(Recordset2.Fields.Item("idCliente").Value)%>" <%If (Not isNull(cliente)) Then If (CStr(Recordset2.Fields.Item("idCliente").Value) = CStr(cliente)) Then Response.Write("selected=""selected""") : Response.Write("")%>><%=(Recordset2.Fields.Item("nombreCliente").Value)%></option>
                                                <%
                                                  Recordset2.MoveNext()
                                                Wend
                                                If (Recordset2.CursorType > 0) Then
                                                  Recordset2.MoveFirst
                                                Else
                                                  Recordset2.Requery
                                                End If
                                                %>
                                            </select></td>

                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr valign="top">
                                        <td align="right">Observaciones del cliente:</td>
                                        <td>
                                            <textarea name="obsCliente" id="obsCliente" cols="45" rows="5"><%=obsCliente%></textarea></td>
                                        <td>Bill to:</td>
                                        <td>
                                            <textarea name="embarque" id="embarque" cols="45" rows="5"><%=entrega%></textarea></td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <%If abreviatura = "CRED" Then%>
                                    <tr>
                                        <td align="right">Afectar Inventario:</td>
                                        <td>
                                            <input type="radio" name="bajar" id="bajar" value="SI" <%If rebajar = "SI" Then Response.Write("checked") End If%> />SI
                                            <input type="radio" name="bajar" id="bajar" value="NO" <%If rebajar = "NO" Or rebajar = "" Then Response.Write("checked") End If%> />NO
                                        </td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <%End If%>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="5" align="center" bgcolor="#66CCFF">Datos de factura</td>
                                    </tr>
                                    <tr>
                                        <td align="right">Forma de pago:</td>
                                        <td bgcolor="#CBDFF5">
                                            <label for="metodoPago"></label>
                                            <select name="metodoPago" id="metodoPago">
                                                <option value="" <%If (Not isNull(Request.Form("idcliente"))) Then If ("" = CStr(Request.Form("idcliente"))) Then Response.Write("selected=""selected""") : Response.Write("")%>>Seleccionar</option>
                                                <%
                        While (NOT RecordsetFormSAT.EOF)
                                                %>
                                                <option value="<%=(RecordsetFormSAT.Fields.Item("codigo").Value)%>" <%If (Not isNull(metodoPago)) Then If (CStr(RecordsetFormSAT.Fields.Item("codigo").Value) = CStr(metodoPago)) Then Response.Write("selected=""selected""") : Response.Write("")%>><%=(RecordsetFormSAT.Fields.Item("codigo").Value)%> - <%=(RecordsetFormSAT.Fields.Item("descripcion").Value)%></option>
                                                <%
                          RecordsetFormSAT.MoveNext()
                        Wend
                        If (RecordsetFormSAT.CursorType > 0) Then
                          RecordsetFormSAT.MoveFirst
                        Else
                          RecordsetFormSAT.Requery
                        End If
                                                %>
                                            </select>
                                        </td>
                                        <td align="right">Numero de cuenta:</td>
                                        <td bgcolor="#99FFCC">
                                            <label for="NumCtaPago"></label>
                                            <input name="NumCtaPago" type="text" id="NumCtaPago" value="<%=NumCtaPago%>" size="32" /></td>
                                    </tr>
                                    <tr>
                                        <td align="right">Condiciones de pago:</td>
                                        <td>
                                            <label for="condicionesDePago"></label>
                                            <input name="condicionesDePago" type="text" id="condicionesDePago" value="<%=condicionesDePago%>" size="32" /></td>
                                        <td align="right">&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <table width="100%" border="0" align="center">
                                    <tr bgcolor="#CBDFF5">
                                        <td align="right">ASN:</td>
                                        <td>
                                            <input name="ASN" type="text" id="ASN" value="<%=asn%>" size="10" /></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr align="center" bgcolor="<%=ctabla%>">
                                        <td>Vendedor</td>
                                        <td>Orden de Compra</td>
                                        <td>Fecha de Embarque</td>
                                        <td>V/A</td>
                                        <td>Terminos</td>
                                        <td>Retencion</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <select name="vendedor" id="vendedor" <%If abreviatura = "CRED" Then Response.Write("disabled='true'") End If%>>
                                                <option value="">Seleccionar</option>
                                                <%While(Not Recordset8.EOF)%>
                                                <option value="<%=Recordset8.Fields.Item("vendedor").Value%>" <%IF vendedor = Recordset8.Fields.Item("vendedor").Value Then Response.Write("selected") End If%>><%=Recordset8.Fields.Item("vendedor").Value%></option>
                                                <%  Recordset8.MoveNext
                                                Wend%>
                                            </select>
                                            <input type="hidden" name="vendor" id="vendor" value="<%=vendedor%>">
                                        </td>
                                        <td>
                                            <label>
                                                <input name="ordenCompra" type="text" id="ordenCompra" value="<%=ocompra%>" size="10" />
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <input name="fEmbarque" type="text" id="fEmbarque" value="<%=fembarque%>" />
                                                <a href="javascript:show_calendar('form1.fEmbarque');" onmouseover="window.status='Elige fecha'; overlib('Pulsa para elegir fecha del mes actual en el calendario emergente.'); return true;" onmouseout="window.status=''; nd(); return true;">
                                                    <img src="imagenes/cal.gif" alt="" width="24" height="22" border="0" /></a></label></td>
                                        <td>
                                            <label>
                                                <input name="VA" type="text" id="VA" value="<%=va%>" />
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <input name="terminos" type="text" id="terminos" value="<%=terminos%>" size="10" />
                                            </label>
                                        </td>
                                        <td>
                                            <input name="retencion" type="text" id="retencion" value="<%=retencion%>" size="10" onkeypress="return SoloNumerosDec(event);" /></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td align="center">dd/mm/yyyy</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <input type="submit" name="button2" id="button2" value="Modificar" /></td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                    <div id="overDiv" style="position: absolute; visibility: hidden; z-index: 10;"></div>
                    <p>
                        <input name="idusuario" type="hidden" id="idusuario" value="<%=Request.Cookies("logn")("id")%>" />
                        <input name="fechaalta" type="hidden" id="fechaalta" value="<%=Date()%>" />
                        <input name="idempresa" type="hidden" id="idempresa" value="<%=Request.QueryString("idempresa")%>" />
                        <input name="cliente2" type="hidden" id="cliente2" value="<%= Recordset1.Fields.Item("idcliente").Value %>" />
                        <input name="tasa" type="hidden" id="tasa" value="<%=iva%>" />
                        <input name="abreviatura" type="hidden" id="abreviatura" value="<%=abreviatura%>" />
                        <input name="cambio" type="hidden" id="cambio" value="<%=cambio%>" />
                        <input type="hidden" name="MM_update" value="form1" />
                        <input name="MM_recordId" type="hidden" id="MM_recordId" value="<%= Recordset1.Fields.Item("idfactura").Value %>" />
                    </p>
                </form>
                <form id="form2" name="form2" method="POST" action="<%=MM_editAction%>">
                    <input type="hidden" name="MM_delete" value="form2" />
                    <input type="hidden" name="MM_recordId" value="<%= Recordset1.Fields.Item("idfactura").Value %>" />
                </form>
                    </div>
                <p>&nbsp;</p>
                <p>&nbsp;</p>
                <!-- InstanceEndEditable -->
                <p>&nbsp;</p>
                <%=footerPage%>
            </td>
        </tr>


    </table>

</body>
<!-- InstanceEnd -->
</html>
<%
Recordset1.Close()
Set Recordset1 = Nothing
%>
<%
Recordset2.Close()
Set Recordset2 = Nothing
%>
<%
Recordset3.Close()
Set Recordset3 = Nothing
%>
<%
Recordset5.Close()
Set Recordset5 = Nothing
%>
<%
Recordset6.Close()
Set Recordset6 = Nothing
%>
<%
Recordset7.Close()
Set Recordset7 = Nothing
%>
<%
Recordset8.Close()
Set Recordset8 = Nothing
%>
<%
Recordset4.Close()
Set Recordset4 = Nothing
%>
<script language="javascript1.2">
function val(f)
{
  var message = "Falta:.\n\n";
  if (f.idcliente.value == "")
  {
  message = message + "- Cliente.\n\n";
  alert (message)
  return false;
  }
  if (f.tipo_comprobante.value == "")
  {
  message = message + "- Tipo de Documento.\n\n";
  alert (message)
  return false;
  }
  if (f.serie.value == "")
  {
  message = message + "- Serie.\n\n";
  alert (message)
  return false;
  }
  if (f.forma_pago2.value == "")
  {
  message = message + "- Forma de Pago.\n\n";
  alert (message)
  return false;
  }

  var combo = document.getElementById("tipo_comprobante");
  var selected = combo.options[combo.selectedIndex].text;
  var cuantos = 0;

  if(selected == "Factura")
  {
    if(f.vendedor.value == "")
    {
      message = message + "El Vendedor\n\n"
      alert(message)
      return false; 
    }
  }
  else
  {
  return true
  }
}

function iniciales()
{
var indice = document.form1.tipo_comprobante.selectedIndex //variable para guardar el indice de la opcion seleccionada del menu tipo de cocumento
var valor = document.form1.tipo_comprobante.options[indice].value //variable para guardar el valor de la opcion seleccionada del menu tipo de cocumento
for (i=0;i<=n;i++)
{
	if (iddocumento[i] == valor)
	{
		document.form1.abreviatura.value =  abreviatura[i]		
	}
}
document.form1.MM_update.value = "0000"
document.form1.submit()
}

function fcambio()
{
var indice = document.form1.moneda.selectedIndex //variable para guardar el indice de la opcion seleccionada del menu tipo de cocumento
var valor = document.form1.moneda.options[indice].value //variable para guardar el valor de la opcion seleccionada del menu tipo de cocumento
for (i=0;i<=n;i++)
{
	if (idmoneda[i] == valor)
	{
		document.form1.cambio.value =  tcambio[i]
	}
}
}
</script>
