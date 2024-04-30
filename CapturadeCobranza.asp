<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!--#include file="Connections/Conecta1.asp" -->
<%
dim folos'Variable para validar el cambio del folio
dim freferencia'Varable para guardar el filtro por referencia
dim ftipoPago'Variable para guardar el filtro por pago
dim ffechaPago'Variable para guardar el filtro por fecha de pago
dim tipoPago'Variable para guardar el pago
dim banco'Variable para guardar el banco
dim fusuario'Variable para guardar el filtro por usuario
dim ffolio'Variable para guardar el filtro por folio
dim pagos'Variable para guardar los que se ha pagado
dim estcobranza'Varaible para guardar el estatus de pago
dim folio'Variable para guardar el folio de la factura
dim monto'Variable para guardar el total de la factura
dim saldo'Variable para guardar el saldo de la factura
dim bandera'Variable para validar si no exoste la factura
dim pagado'Variable para guardar cuanto se ha pagado

'inicializar variables
fusuario = " AND logCXC.idusuario = " & Request.Cookies("login")("id")
monto = 0
pagado = 0
saldo = 0
'/////////////////////

'if para validar que se filtra por tipo de pago
if Request.Form("referencia") <> "" then'referencia
fusuario = ""
freferencia = " AND logCXC.refecencia = '" & Request.Form("referencia") & "'"
end if'referencia

'if para validar que se filtra por tipo de pago
if Request.Form("tipoPago") <> "" then'tipoPago
ftipoPago = " AND logCXC.tipo_pago = " & Request.Form("tipoPago")
end if'tipoPago

'if para validar que se filtra por folio
if Request.Form("fechaPago") <> "" then'folio
fusuario = ""
ffechaPago = " AND CONVERT(varchar,logCXC.fecha_captura,103) = CONVERT(varchar,'" & Request.Form("fechaPago") & "',103)"
end if'folio

'if para validar que se filtra por folio
if Request.Form("folio2") <> "" then'folio
fusuario = ""
ffolio = " AND logCXC.folio = " & Request.Form("folio2")
end if'folio

%>
<%
Dim Recordset5
Dim Recordset5_cmd
Dim Recordset5_numRows

Set Recordset5_cmd = Server.CreateObject ("ADODB.Command")
Recordset5_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset5_cmd.CommandText = "SELECT * FROM dbo.logCXC WHERE logCXC.idcc > 0 "&fusuario&ffolio&ffechaPago&ftipoPago&freferencia&" ORDER BY idcc DESC"
Recordset5_cmd.Prepared = true
Recordset5_cmd.Parameters.Append Recordset5_cmd.CreateParameter("param1", 5, 1, -1, Recordset5__MMColParam) ' adDouble
    'Response.Write(Recordset5_cmd.CommandText)
Set Recordset5 = Recordset5_cmd.Execute
Recordset5_numRows = 0
%>
<%
Dim Repeat1__numRows
Dim Repeat1__index

Repeat1__numRows = 15
Repeat1__index = 0
Recordset5_numRows = Recordset5_numRows + Repeat1__numRows
%>
<%
'  *** Recordset Stats, Move To Record, and Go To Record: declare stats variables

Dim Recordset5_total
Dim Recordset5_first
Dim Recordset5_last

' set the record count
Recordset5_total = Recordset5.RecordCount

' set the number of rows displayed on this page
If (Recordset5_numRows < 0) Then
  Recordset5_numRows = Recordset5_total
Elseif (Recordset5_numRows = 0) Then
  Recordset5_numRows = 1
End If

' set the first and last displayed record
Recordset5_first = 1
Recordset5_last  = Recordset5_first + Recordset5_numRows - 1

' if we have the correct record count, check the other stats
If (Recordset5_total <> -1) Then
  If (Recordset5_first > Recordset5_total) Then
    Recordset5_first = Recordset5_total
  End If
  If (Recordset5_last > Recordset5_total) Then
    Recordset5_last = Recordset5_total
  End If
  If (Recordset5_numRows > Recordset5_total) Then
    Recordset5_numRows = Recordset5_total
  End If
End If
%>
<%
Dim MM_paramName 
%>
<%
' *** Move To Record and Go To Record: declare variables

Dim MM_rs
Dim MM_rsCount
Dim MM_size
Dim MM_uniqueCol
Dim MM_offset
Dim MM_atTotal
Dim MM_paramIsDefined

Dim MM_param
Dim MM_index

Set MM_rs    = Recordset5
MM_rsCount   = Recordset5_total
MM_size      = Recordset5_numRows
MM_uniqueCol = ""
MM_paramName = ""
MM_offset = 0
MM_atTotal = false
MM_paramIsDefined = false
If (MM_paramName <> "") Then
  MM_paramIsDefined = (Request.QueryString(MM_paramName) <> "")
End If
%>
<%
' *** Move To Record: handle 'index' or 'offset' parameter

if (Not MM_paramIsDefined And MM_rsCount <> 0) then

  ' use index parameter if defined, otherwise use offset parameter
  MM_param = Request.QueryString("index")
  If (MM_param = "") Then
    MM_param = Request.QueryString("offset")
  End If
  If (MM_param <> "") Then
    MM_offset = Int(MM_param)
  End If

  ' if we have a record count, check if we are past the end of the recordset
  If (MM_rsCount <> -1) Then
    If (MM_offset >= MM_rsCount Or MM_offset = -1) Then  ' past end or move last
      If ((MM_rsCount Mod MM_size) > 0) Then         ' last page not a full repeat region
        MM_offset = MM_rsCount - (MM_rsCount Mod MM_size)
      Else
        MM_offset = MM_rsCount - MM_size
      End If
    End If
  End If

  ' move the cursor to the selected record
  MM_index = 0
  While ((Not MM_rs.EOF) And (MM_index < MM_offset Or MM_offset = -1))
    MM_rs.MoveNext
    MM_index = MM_index + 1
  Wend
  If (MM_rs.EOF) Then 
    MM_offset = MM_index  ' set MM_offset to the last possible record
  End If

End If
%>
<%
' *** Move To Record: if we dont know the record count, check the display range

If (MM_rsCount = -1) Then

  ' walk to the end of the display range for this page
  MM_index = MM_offset
  While (Not MM_rs.EOF And (MM_size < 0 Or MM_index < MM_offset + MM_size))
    MM_rs.MoveNext
    MM_index = MM_index + 1
  Wend

  ' if we walked off the end of the recordset, set MM_rsCount and MM_size
  If (MM_rs.EOF) Then
    MM_rsCount = MM_index
    If (MM_size < 0 Or MM_size > MM_rsCount) Then
      MM_size = MM_rsCount
    End If
  End If

  ' if we walked off the end, set the offset based on page size
  If (MM_rs.EOF And Not MM_paramIsDefined) Then
    If (MM_offset > MM_rsCount - MM_size Or MM_offset = -1) Then
      If ((MM_rsCount Mod MM_size) > 0) Then
        MM_offset = MM_rsCount - (MM_rsCount Mod MM_size)
      Else
        MM_offset = MM_rsCount - MM_size
      End If
    End If
  End If

  ' reset the cursor to the beginning
  If (MM_rs.CursorType > 0) Then
    MM_rs.MoveFirst
  Else
    MM_rs.Requery
  End If

  ' move the cursor to the selected record
  MM_index = 0
  While (Not MM_rs.EOF And MM_index < MM_offset)
    MM_rs.MoveNext
    MM_index = MM_index + 1
  Wend
End If
%>
<%
' *** Move To Record: update recordset stats

' set the first and last displayed record
Recordset5_first = MM_offset + 1
Recordset5_last  = MM_offset + MM_size

If (MM_rsCount <> -1) Then
  If (Recordset5_first > MM_rsCount) Then
    Recordset5_first = MM_rsCount
  End If
  If (Recordset5_last > MM_rsCount) Then
    Recordset5_last = MM_rsCount
  End If
End If

' set the boolean used by hide region to check if we are on the last record
MM_atTotal = (MM_rsCount <> -1 And MM_offset + MM_size >= MM_rsCount)
%>
<%
' *** Go To Record and Move To Record: create strings for maintaining URL and Form parameters

Dim MM_keepNone
Dim MM_keepURL
Dim MM_keepForm
Dim MM_keepBoth

Dim MM_removeList
Dim MM_item
Dim MM_nextItem

' create the list of parameters which should not be maintained
MM_removeList = "&index="
If (MM_paramName <> "") Then
  MM_removeList = MM_removeList & "&" & MM_paramName & "="
End If

MM_keepURL=""
MM_keepForm=""
MM_keepBoth=""
MM_keepNone=""

' add the URL parameters to the MM_keepURL string
For Each MM_item In Request.QueryString
  MM_nextItem = "&" & MM_item & "="
  If (InStr(1,MM_removeList,MM_nextItem,1) = 0) Then
    MM_keepURL = MM_keepURL & MM_nextItem & Server.URLencode(Request.QueryString(MM_item))
  End If
Next

' add the Form variables to the MM_keepForm string
For Each MM_item In Request.Form
  MM_nextItem = "&" & MM_item & "="
  If (InStr(1,MM_removeList,MM_nextItem,1) = 0) Then
    MM_keepForm = MM_keepForm & MM_nextItem & Server.URLencode(Request.Form(MM_item))
  End If
Next

' create the Form + URL string and remove the intial '&' from each of the strings
MM_keepBoth = MM_keepURL & MM_keepForm
If (MM_keepBoth <> "") Then 
  MM_keepBoth = Right(MM_keepBoth, Len(MM_keepBoth) - 1)
End If
If (MM_keepURL <> "")  Then
  MM_keepURL  = Right(MM_keepURL, Len(MM_keepURL) - 1)
End If
If (MM_keepForm <> "") Then
  MM_keepForm = Right(MM_keepForm, Len(MM_keepForm) - 1)
End If

' a utility function used for adding additional parameters to these strings
Function MM_joinChar(firstItem)
  If (firstItem <> "") Then
    MM_joinChar = "&"
  Else
    MM_joinChar = ""
  End If
End Function
%>
<%
' *** Move To Record: set the strings for the first, last, next, and previous links

Dim MM_keepMove
Dim MM_moveParam
Dim MM_moveFirst
Dim MM_moveLast
Dim MM_moveNext
Dim MM_movePrev

Dim MM_urlStr
Dim MM_paramList
Dim MM_paramIndex
Dim MM_nextParam

MM_keepMove = MM_keepBoth
MM_moveParam = "index"

' if the page has a repeated region, remove 'offset' from the maintained parameters
If (MM_size > 1) Then
  MM_moveParam = "offset"
  If (MM_keepMove <> "") Then
    MM_paramList = Split(MM_keepMove, "&")
    MM_keepMove = ""
    For MM_paramIndex = 0 To UBound(MM_paramList)
      MM_nextParam = Left(MM_paramList(MM_paramIndex), InStr(MM_paramList(MM_paramIndex),"=") - 1)
      If (StrComp(MM_nextParam,MM_moveParam,1) <> 0) Then
        MM_keepMove = MM_keepMove & "&" & MM_paramList(MM_paramIndex)
      End If
    Next
    If (MM_keepMove <> "") Then
      MM_keepMove = Right(MM_keepMove, Len(MM_keepMove) - 1)
    End If
  End If
End If

' set the strings for the move to links
If (MM_keepMove <> "") Then 
  MM_keepMove = Server.HTMLEncode(MM_keepMove) & "&"
End If

MM_urlStr = Request.ServerVariables("URL") & "?" & MM_keepMove & MM_moveParam & "="

MM_moveFirst = MM_urlStr & "0"
MM_moveLast  = MM_urlStr & "-1"
MM_moveNext  = MM_urlStr & CStr(MM_offset + MM_size)
If (MM_offset - MM_size < 0) Then
  MM_movePrev = MM_urlStr & "0"
Else
  MM_movePrev = MM_urlStr & CStr(MM_offset - MM_size)
End If
%>
<!--#include file="config.asp"--> 
<!--#include file="checklogin.asp"-->
<!--#include file="stylo2.asp"-->
<%
Dim Recordset1
Dim Recordset1_cmd
Dim Recordset1_numRows

Set Recordset1_cmd = Server.CreateObject ("ADODB.Command")
Recordset1_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset1_cmd.CommandText = "SELECT * FROM dbo.bancos ORDER BY nombrebanco ASC" 
Recordset1_cmd.Prepared = true

Set Recordset1 = Recordset1_cmd.Execute
Recordset1_numRows = 0
%>
<%
Dim Recordset2
Dim Recordset2_cmd
Dim Recordset2_numRows

Set Recordset2_cmd = Server.CreateObject ("ADODB.Command")
Recordset2_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset2_cmd.CommandText = "SELECT * FROM dbo.tipodepago ORDER BY rango ASC" 
Recordset2_cmd.Prepared = true

Set Recordset2 = Recordset2_cmd.Execute
Recordset2_numRows = 0
%>
<%
Dim Recordset3__MMColParam
Recordset3__MMColParam = "000000000"
If (Request.Form("folio") <> "") Then 
  Recordset3__MMColParam = Request.Form("folio")
End If
%>
<%
Dim Recordset3
Dim Recordset3_cmd
Dim Recordset3_numRows

Set Recordset3_cmd = Server.CreateObject ("ADODB.Command")
Recordset3_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset3_cmd.CommandText = "SELECT * FROM dbo.factura WHERE folio = ?" 
Recordset3_cmd.Prepared = true
Recordset3_cmd.Parameters.Append Recordset3_cmd.CreateParameter("param1", 5, 1, -1, Recordset3__MMColParam) ' adDouble

Set Recordset3 = Recordset3_cmd.Execute
Recordset3_numRows = 0
%>
<%
Dim Recordset4
Dim Recordset4_cmd
Dim Recordset4_numRows

Set Recordset4_cmd = Server.CreateObject ("ADODB.Command")
Recordset4_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset4_cmd.CommandText = "SELECT * FROM dbo.logCXC, tipodepago WHERE idtipo = tipo_pago ORDER BY idcc DESC" 
Recordset4_cmd.Prepared = true

Set Recordset4 = Recordset4_cmd.Execute
Recordset4_numRows = 0
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
'if para valdiar que se mando insertar
If (CStr(Request("MM_insert")) = "form1") Then
  If (Not MM_abortEdit) Then
  fecha = Request.Form("dia") &"/" & Request.Form("mes") & "/" & Request.Form("yearcxc")  
  pagos = CDbl(Request.Form("pagos")) + Cdbl(Request.Form("total"))
  
  if pagos >= CDbl(Request.Form("monto")) then
  estcobranza = "Pagada"
  else
  estcobranza = "Pagada parcial"
  end if
  
  Set RSFactura_cmd = Server.CreateObject ("ADODB.Command")
RSFactura_cmd.ActiveConnection = MM_Conecta1_STRING
RSFactura_cmd.CommandText = "UPDATE dbo.factura SET estcobranza = ? WHERE folio = ?" 
RSFactura_cmd.Prepared = true
RSFactura_cmd.Parameters.Append RSFactura_cmd.CreateParameter("param1", 201, 1, 255, estcobranza) ' adLongVarChar
RSFactura_cmd.Parameters.Append RSFactura_cmd.CreateParameter("param2", 5, 1, -1, MM_IIF(Request.Form("folio"), Request.Form("folio"), null)) ' adDouble

Set RSFactura = RSFactura_cmd.Execute   
  End If
End If
%>
<%
If (CStr(Request("MM_insert")) = "form1") Then
  If (Not MM_abortEdit) Then
    ' execute the insert
    Dim MM_editCmd

    Set MM_editCmd = Server.CreateObject ("ADODB.Command")
    MM_editCmd.ActiveConnection = MM_Conecta1_STRING
    MM_editCmd.CommandText = "INSERT INTO dbo.logCXC (folio, fecha_captura, anterior, tipo_pago, refecencia, cuenta_cliente, banco, monto, cantidad_recibida, cambio, saldo, ntarjeta, observaciones, total, idusuario, fechaalta) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)" 
    MM_editCmd.Prepared = true
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param1", 5, 1, -1, MM_IIF(Request.Form("folio"), Request.Form("folio"), null)) ' adDouble
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param2", 135, 1, -1, MM_IIF(fecha, fecha, null)) ' adDBTimeStamp
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param3", 201, 1, 255, MM_IIF(Request.Form("anterior"), Request.Form("anterior"), "no")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param4", 5, 1, -1, MM_IIF(Request.Form("tipo_pago"), Request.Form("tipo_pago"), null)) ' adDouble
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param5", 201, 1, 255, Request.Form("refecencia")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param6", 201, 1, 255, Request.Form("cuenta_cliente")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param7", 5, 1, -1, MM_IIF(Request.Form("banco"), Request.Form("banco"), null)) ' adDouble
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param8", 5, 1, -1, MM_IIF(Request.Form("monto"), Request.Form("monto"), null)) ' adDouble
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param9", 5, 1, -1, MM_IIF(Request.Form("cantidad_recibida"), Request.Form("cantidad_recibida"), null)) ' adDouble
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param10", 5, 1, -1, MM_IIF(Request.Form("cambio"), Request.Form("cambio"), null)) ' adDouble
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param11", 5, 1, -1, MM_IIF(Request.Form("saldo"), Request.Form("saldo"), null)) ' adDouble
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param12", 201, 1, 255, Request.Form("ntarjeta")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param13", 201, 1, 8000, Request.Form("observaciones")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param14", 5, 1, -1, MM_IIF(Request.Form("total"), Request.Form("total"), null)) ' adDouble
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param15", 5, 1, -1, MM_IIF(Request.Form("idusuario"), Request.Form("idusuario"), null)) ' adDouble
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param16", 135, 1, -1, MM_IIF(Request.Form("fechaalta"), Request.Form("fechaalta"), null)) ' adDBTimeStamp
    MM_editCmd.Execute
    MM_editCmd.ActiveConnection.Close

    ' append the query string to the redirect URL
    Dim MM_editRedirectUrl
    MM_editRedirectUrl = "capturadecobranza.asp"
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
'if para validar que existe
if NOT Recordset3.EOF then'Recordset3
bandera = "si"
folio = (Recordset3.Fields.Item("folio").Value)
monto = (Recordset3.Fields.Item("total").Value)

'-----------------Recordset para consultar cuanto se a pagado de la factura---------------------------------
Set RSMonto_cmd = Server.CreateObject ("ADODB.Command")
RSMonto_cmd.ActiveConnection = MM_Conecta1_STRING
RSMonto_cmd.CommandText = "SELECT SUM(total) as pagado FROM dbo.logCXC WHERE estatus <> 'Cancelado' AND folio = " & folio
RSMonto_cmd.Prepared = true

Set RSMonto = RSMonto_cmd.Execute

'if para validar que hay pagos
if RSMonto.Fields.Item("pagado").Value <> "" then'pagado
saldo =  RSMonto.Fields.Item("pagado").Value
pagado = RSMonto.Fields.Item("pagado").Value
end if'pagado
saldo = monto - saldo
elseif Request.Form("folio") <> "" then
bandera = "no"
end if'Recordset3
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<style type="text/css">
<!--
.stilo1 {
	font-size: 12px;
}
.stilo1 {
	font-weight: bold;
}
.stilo1 {
	text-align: center;
}
.stilo2 {
	font-size: 12px;
}
.styloencabezado td {
	font-weight: bold;
}
-->
</style>
<!-- InstanceBegin template="/Templates/plantillacfdadmin.dwt.asp" codeOutsideHTMLIsLocked="false" -->
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<!-- InstanceBeginEditable name="doctitle" -->
<title><%=titlePage%></title>
    <link rel="stylesheet" type="text/css" href="calendar/tcal.css" /> 
<script type="text/javascript" src="calendar/tcal.js"></script>
<!-- InstanceEndEditable -->

<style type="text/css">

<!--
body {
	background-color: #fff;
	margin-left: 0px;
	margin-top: 0px;
	margin-right: 0px;
	margin-bottom: 0px;
}
.Estilo1 {color: #000000}
.Estilo7 {font-size: 9px}
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
	width:800px;
}
-->
</style>
<!-- InstanceBeginEditable name="head" -->

<!-- InstanceEndEditable -->
</head>

<body>

      <!-- InstanceBeginEditable name="EditRegion1" -->
<!--<table width="200" border="0" align="center">
  <tr align="center">
    <td>Caja</td>
  </tr>
</table>-->
<!--<% If bandera = "no" Then %>
<table width="256" border="0" align="center">
  <tr align="center" class="styloencabezado">
    <td>El folio <%=Request.Form("folio")%> no  existe</td>
  </tr>
</table>
<% End If %>-->
<!--<% If not recordset3.EOF then 
      if Recordset3.fields.item("tipo_comprobante").value = 2 Then %>
<table width="256" border="0" align="center">
  <tr align="center" class="styloencabezado">
    <td>El folio <%=Request.Form("folio")%> pertenece a una nota de credito</td>
  </tr>
</table>-->
<% 
monto=0
saldo=0
End If 
End if %>
<!--<form action="<%=MM_editAction%>" method="POST" name="form1" id="form1" onSubmit="return val(this)">
  <table align="center">
    <tr valign="baseline">
      <td nowrap="nowrap" align="right">Folio:</td>
      <td><input name="folio" type="text" value="<%=folio%>" size="32" /></td>
    </tr>
    <tr valign="baseline">
      <td nowrap="nowrap" align="right">Fecha de pago:</td>
      <td><label>
        <input name="dia" type="text" id="dia" value="<%=Day(Date())%>" size="5" />
        /
        <input name="mes" type="text" id="mes" value="<%=Month(Date())%>" size="5" />
        /
        <input name="yearcxc" type="text" id="yearcxc" value="<%=year(Date())%>" size="5" />
      </label></td>
    </tr>
    <tr valign="baseline">
      <td nowrap="nowrap" align="right">Factura en papel:</td>
      <td><label>
        <input name="anterior" type="checkbox" id="anterior" value="si" />
      </label></td>
    </tr>
    <tr valign="baseline">
      <td nowrap="nowrap" align="right">Tipo pago:</td>
      <td><select name="tipo_pago">
        <%
While (NOT Recordset2.EOF)
%>
        <option value="<%=(Recordset2.Fields.Item("idtipo").Value)%>"><%=(Recordset2.Fields.Item("Tipo").Value)%></option>
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
    </tr>
    <tr valign="baseline">
      <td nowrap="nowrap" align="right">Referencia:</td>
      <td><input type="text" name="refecencia" value="" size="32" /></td>
    </tr>
    <tr valign="baseline" bgcolor="#66D9FF">
      <td colspan="2" align="center" nowrap="nowrap">Cuenta deposito</td>
      </tr>
    <tr valign="baseline">
      <td nowrap="nowrap" align="right">Cuenta:</td>
      <td><input name="ntarjeta" type="text" id="ntarjeta" size="32" /></td>
    </tr>
    <tr valign="baseline" bgcolor="#00B97C">
      <td colspan="2" align="center" nowrap="nowrap">Cuenta cliente</td>
    </tr>
    <tr valign="baseline">
      <td align="right" nowrap="nowrap">Cuenta:</td>
      <td><label>
        <input name="cuenta_cliente" type="text" id="cuenta_cliente" size="32" />
      </label></td>
    </tr>
    <tr valign="baseline">
      <td align="right" nowrap="nowrap">Banco:</td>
      <td><select name="banco">
        <option value="">Seleccionar</option>
        <%
While (NOT Recordset1.EOF)
%>
        <option value="<%=(Recordset1.Fields.Item("idbanco").Value)%>"><%=(Recordset1.Fields.Item("nombrebanco").Value)%></option>
        <%
  Recordset1.MoveNext()
Wend
If (Recordset1.CursorType > 0) Then
  Recordset1.MoveFirst
Else
  Recordset1.Requery
End If
%>
      </select></td>
    </tr>
    <tr valign="baseline">
      <td colspan="2" nowrap="nowrap"><hr width="100%" /></td>
      </tr>
    <tr valign="baseline" bgcolor="#CCCCCC">
      <td align="right" nowrap="nowrap">Monto:</td>
      <td><input name="monto" type="text" value="<%=monto%>" size="32" readonly="readonly" /></td>
    </tr>
    <tr valign="baseline" bgcolor="#CBDFF5" class="stylo49">
      <td align="right" nowrap="nowrap">&nbsp;</td>
      <td>&nbsp;</td>
    </tr>
    <tr valign="baseline" bgcolor="#CBDFF5">
      <td align="right" nowrap="nowrap">Cantidad recibida:</td>
      <td><input type="text" name="cantidad_recibida" value="" size="32" onChange="valor()"/></td>
    </tr>
    <tr valign="baseline" bgcolor="#CBDFF5" class="stylo49">
      <td align="right" nowrap="nowrap">&nbsp;</td>
      <td>&nbsp;</td>
    </tr>
    <tr valign="baseline">
      <td nowrap="nowrap" align="right">Cambio:</td>
      <td><input name="cambio" type="text" value="" size="32" readonly="readonly" /></td>
    </tr>
    <tr valign="baseline">
      <td nowrap="nowrap" align="right">Saldo:</td>
      <td><input name="saldo" type="text" value="<%=saldo%>" size="32" readonly="readonly" /></td>
    </tr>
    <tr valign="top">
      <td align="right" nowrap="nowrap">Observaciones:</td>
      <td><label>
        <textarea name="observaciones" id="observaciones" cols="28" rows="5"></textarea>
        </label></td>
    </tr>
    <tr valign="baseline">
      <td nowrap="nowrap" align="right">&nbsp;</td>
      <td><input type="submit" value="Guardar" /></td>
    </tr>
  </table>
  <input type="hidden" name="MM_insert" value="form1" />
  <input type="hidden" name="total" id="total" />
  <label>
    <input name="pagos" type="hidden" id="pagos" value="<%=pagado%>" />
  </label>
  <input name="idusuario" type="hidden" id="idusuario" value="<%=Session("site_id")%>" />
  <input name="fechaalta" type="hidden" id="fechaalta" value="<%=Date()&" "&FormatDateTime(now, 4)%>" />
</form>-->
        <p>&nbsp;</p>
<table width="90%" border="0" align="center">
  <tr>
    <td><form id="form2" name="form2" method="post" action="">
      Buscar Folio 
      <label>
        <input name="folio2" type="text" id="folio2" size="10" value="<%=Request.Form("folio2") %>" />
      Fecha de pago
      <input type="text" name="fechaPago" class="tcal" id="fechaPago" readonly="readonly" value="<%= Request.Form("fechaPago") %>" />
Tipo pago 
<select name="tipoPago" id="tipoPago">
  <option value="">Todos</option>
  <%
While (NOT Recordset2.EOF)
      if Recordset2.Fields.Item("idtipo").Value = Request.Form("tipoPago") Then 
      caso = "selected='selected'"
      else
      caso = ""
      end if
%>
  <option value="<%=(Recordset2.Fields.Item("idtipo").Value)%>"><%=(Recordset2.Fields.Item("Tipo").Value)%></option>
  <%
  Recordset2.MoveNext()
Wend
If (Recordset2.CursorType > 0) Then
  Recordset2.MoveFirst
Else
  Recordset2.Requery
End If
%>
</select>
      </label>
      <label>
        <input type="submit" name="button" id="button" value="Enviar" />
      </label>
    </form></td>
  </tr>
  <tr>
    <td><hr width="100%" /></td>
  </tr>
</table>
<p>&nbsp;</p>
<table border="0" align="center">
  <tr class="stilo1" bgcolor="#e33045">
    <td rowspan="2"><strong>FOLIO</strong></td>
    <td rowspan="2"><strong>FECHA DE PAGO</strong></td>
    <td rowspan="2">PAPEL</td>
    <td rowspan="2">TIPO PAGO</td>
    <td rowspan="2">REFERENCIA</td>
    <td>Deposito</td>
    <td colspan="2">Cliente</td>
    <td rowspan="2">MONTO</td>
    <td rowspan="2">CANTIDAD RECIBIDA</td>
    <td rowspan="2">Saldo</td>
    <td rowspan="2">ESTATUS</td>
    <td rowspan="2">OBSERVACIONES</td>
    <td rowspan="2">MOD</td>
  </tr>
  <tr class="stilo1" bgcolor="#e33045">
    <td>Cuenta</td>
    <td>Cuenta</td>
    <td>Banco</td>
  </tr>
  <% While ((Repeat1__numRows <> 0) AND (NOT Recordset5.EOF)) %>
<%
'inicializar variables
banco = "&nbsp;"
tipoPago = "&nbsp;"
'///////////////////////

'if para validar el cambio de color
if color = cgrid2 then'color
color = cgrid1
else'color
color = cgrid2
end if'color

'if para validar que tenga banco
if Recordset5.Fields.Item("tipo_pago").Value <> "" then'banco
'-----------------Recordset para consultar el banco---------------------------------
Set RSTipoPago_cmd = Server.CreateObject ("ADODB.Command")
RSTipoPago_cmd.ActiveConnection = MM_Conecta1_STRING
RSTipoPago_cmd.CommandText = "SELECT * FROM dbo.tipodepago WHERE idtipo = " & Recordset5.Fields.Item("tipo_pago").Value
RSTipoPago_cmd.Prepared = true

Set RSTipoPago = RSTipoPago_cmd.Execute

'if para validar que hay datos
if NOT RSTipoPago.EOF then'RSBanco
tipoPago = RSTipoPago.Fields.Item("Tipo").Value
end if'RSBanco
end if'banco

'if para validar que tenga banco
if Recordset5.Fields.Item("banco").Value <> "" then'banco
'-----------------Recordset para consultar el banco---------------------------------
Set RSBanco_cmd = Server.CreateObject ("ADODB.Command")
RSBanco_cmd.ActiveConnection = MM_Conecta1_STRING
RSBanco_cmd.CommandText = "SELECT * FROM dbo.bancos WHERE idbanco = " & Recordset5.Fields.Item("banco").Value
RSBanco_cmd.Prepared = true

Set RSBanco = RSBanco_cmd.Execute

'if para validar que hay datos
if NOT RSBanco.EOF then'RSBanco
banco = RSBanco.Fields.Item("descripcion").Value
end if'RSBanco
end if'banco

if Recordset5.Fields.Item("folio").Value <> "" then
'-----------------Recordset para consultar los archivos xml y pdf de la factura---------------------------------
Set RSMonto_cmd = Server.CreateObject ("ADODB.Command")
RSMonto_cmd.ActiveConnection = MM_Conecta1_STRING
RSMonto_cmd.CommandText = "SELECT SUM(total) as pagado FROM dbo.logCXC WHERE folio = " & Recordset5.Fields.Item("folio").Value
RSMonto_cmd.Prepared = true

Set RSMonto = RSMonto_cmd.Execute

if RSMonto.Fields.Item("pagado").Value <> "" then
saldo = RSMonto.Fields.Item("pagado").Value
end if
end if
if Recordset5.Fields.Item("estatus").Value="Cancelado" then
   CR=0
else 
   CR=Recordset5.Fields.Item("cantidad_recibida").Value
end if
saldo1 = (Recordset5.Fields.Item("Saldo").Value - CR)
if saldo1 < 0 then 
saldo1 = 0
end if
%>
    <tr class="stilo2">
      <td><%=(Recordset5.Fields.Item("folio").Value)%></td>
      <td><%=(Recordset5.Fields.Item("fecha_captura").Value)%></td>
      <td><%=(Recordset5.Fields.Item("anterior").Value)%></td>
      <td><%=(tipoPago)%></td>
      <td><%=(Recordset5.Fields.Item("refecencia").Value)%></td>
      <td><%=(Recordset5.Fields.Item("ntarjeta").Value)%></td>
      <td><%=(Recordset5.Fields.Item("cuenta_cliente").Value)%></td>
      <td><%=(banco)%></td>
      <td><%=(Recordset5.Fields.Item("monto").Value)%></td>
      <td><%=(Recordset5.Fields.Item("cantidad_recibida").Value)%></td>
      <td><%=(saldo1)%></td>
      <td><%=(Recordset5.Fields.Item("estatus").Value)%></td>
      <td><%=(Recordset5.Fields.Item("observaciones").Value)%></td>
      <td align="center"><a href="pagosmod2.asp?folio=<%=(Recordset5.Fields.Item("folio").Value)%>&idcc=<%=(Recordset5.Fields.Item("idcc").Value)%>&idcliente=<%=request.QueryString("idcliente")%>"><img src="imagenes/database_table_(edit)_16x16.gif" width="16" height="16" border="0" /></a></td>
    </tr>
    <% 
  Repeat1__index=Repeat1__index+1
  Repeat1__numRows=Repeat1__numRows-1
  Recordset5.MoveNext()
Wend
%>
</table>
<p>&nbsp;</p>
<table border="0" align="center">
  <tr>
    <td><% If MM_offset <> 0 Then %>
        <a href="<%=MM_moveFirst%>"><img src="First.gif" border="0"></a>
        <% End If ' end MM_offset <> 0 %></td>
    <td><% If MM_offset <> 0 Then %>
        <a href="<%=MM_movePrev%>"><img src="Previous.gif" border="0"></a>
        <% End If ' end MM_offset <> 0 %></td>
    <td><% If Not MM_atTotal Then %>
        <a href="<%=MM_moveNext%>"><img src="Next.gif" border="0"></a>
        <% End If ' end Not MM_atTotal %></td>
    <td><% If Not MM_atTotal Then %>
        <a href="<%=MM_moveLast%>"><img src="Last.gif" border="0"></a>
        <% End If ' end Not MM_atTotal %></td>
    </tr>
</table>
<p>&nbsp;</p>
      <!-- InstanceEndEditable -->
    
</body>
<!-- InstanceEnd --></html><%
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
Recordset4.Close()
Set Recordset4 = Nothing
%>
<%
Recordset5.Close()
Set Recordset5 = Nothing
%>
<script language="javascript1.2">
if ('<%=Request.QueryString("cargar")%>' == "si")
{
	parent.location.reload()
}

function val(f)
{
	var message = "Falta:.\n\n";
	if (f.folio.value == "")
	{
      message = message + "- Folio.\n\n";
      alert (message)		
		return false;
	}
	if (f.monto.value == "")
	{
      message = message + "- Monto.\n\n";
      alert (message)	
		return false;
	}
	else
	{
		return true;
	}
}
function valor()
{
	var saldo
	saldo = document.form1.cantidad_recibida.value - document.form1.saldo.value		
	if (saldo >=0)
	{
		document.form1.total.value = document.form1.cantidad_recibida.value - saldo
		document.form1.cambio.value = saldo
	}
	else
	{
		document.form1.total.value = document.form1.cantidad_recibida.value
		document.form1.cambio.value = 0
	}
	
}

</script>
<script language="JavaScript">
document.onkeydown = checkKeycode//llamar la fucnion checkKeycode(eliminar tecla suprimir)
//fucnion para eliminaro con el tecla suprimir
function checkKeycode(e) {
var keycode;//variable para guardar la tecla
//if para valdiar si se presiono una tecla
if (window.event) keycode = window.event.keyCode;
else if (e) keycode = e.which;
//if para valdiar el enter
if (keycode == 13)
{
document.form1.MM_insert.value = "00000000"
document.form1.submit();
}
}
</script>
