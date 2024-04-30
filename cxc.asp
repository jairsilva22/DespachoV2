<%@LANGUAGE="VBSCRIPT"%>
<!--#include file="Connections/Conecta1.asp" -->
<!--#include file="config.asp"--> 
<!--#include file="checklogin.asp"-->
<!--#include file="stylo2.asp"-->
<%
'para truncar a 6 decimales
    Function truncarAseis(valor)
        Dim digitos 'Variable para guardar los digitos

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
    Function truncarAdos( valor)
        Dim digitos'Variable para guardar los digitos

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
    Function redondear(val)

        'if para validar si tiene digitos
        If InStr(val, ".") <> 0 Then
            'redondear la funcion
            val = Round(val, 2)
        Else
            val = val & ".00"
        End If
        redondear = val
    End Function

dim formaPago'Variable para guardar la forma de pago
dim cliente'Variable para guardar el cliente que se consulto
dim folio'Variable para guardar el folio que se consulto
dim estatus'Variable para guardar el estatus que se consulto
dim ordenar'Variable par aordenar la lista de facturas
dim oritentacion'Variable para guardar la orientacion de la lista
dim color'Variable para guardar el color de los renglones'
dim archvixml'Variable para guardar el archivo xml de la factura
dim archvipdf'Variable para guardar el archivo pdf de la factura
dim documento'Variable para guardar eldocumento
dim estado'Varaible para guardar el estaod del comprobante
dim vendedor'Variable para guardar el vendor a filtrar
dim saldo'Variable apra guardar la cantidad de pagos
dim estcobranza'Variable para guardar es etatus de cobranza
dim cargo

formaPago = ""
color = cgrid2
oritentacion = " DESC"
ordenar = " factura.idfactura"
estatus = " AND factura.estatus = 'Facturada'"
estcobranza = " AND estcobranza != 'Pagada'"


'if para validar que el menu formaPago no venga solo
if Request.Form("formaPago") <> "" AND Request.Form("formaPago") <> "0" then'formaPago
formaPago = " AND forma_pago = "&Request.Form("formaPago")
elseif Request.Form("formaPago") = "0" then
formaPago = ""
end if'formaPago

'if para validar que el menu estcobranza no venga solo
if Request.Form("estcobranza") <> "" AND Request.Form("estcobranza") <> "0" then'clientes
estcobranza = " AND estcobranza = '"&Request.Form("estcobranza")&"'"
elseif Request.Form("estcobranza") = "0"  then
estcobranza = ""
end if'clientes

'if para validar que el menu vendedor no venga solo
if Request.Form("vendedor") <> "" then'clientes
vendedor = " AND vendedor = '" & Request.Form("vendedor") & "'"
end if'clientes

'if para validar que el menu clitene no venga solo
if Request.Form("documento") <> "" then'clientes
documento = " AND tipo_comprobante = " & Request.Form("documento") 
end if'clientes

'if para validar que el menu clitene no venga solo
if Request.Form("clientes") <> "" then'clientes
cliente = " AND factura.idcliente = " & Request.Form("clientes") 
end if'clientes

'if para validar que el textbox folio no venga solo
if Request.Form("folio") <> "" then'
folio = " AND factura.folio = '" & Request.Form("folio") & "'"
end if'folio

'if para validar que el menu estatus no venga solo
if Request.Form("estatus") <> "" then'
estatus = " AND factura.estatus = '" & Request.Form("estatus") & "'"
end if'folio

'if para validar que el menu estatus no venga solo
if Request.Form("ordenar") <> "" then'
ordenar = " " & Request.Form("ordenar")
end if'folio

'if para validar que el menu estatus no venga solo
if Request.Form("orientacion") <> "" then'
oritentacion = " " & Request.Form("orientacion")
end if'folio
%>
<%
Dim Recordset1__MMColParam
Recordset1__MMColParam = "1"
If (Request.Cookies("login")("idSucursal") <> "") Then 
  Recordset1__MMColParam = Request.Cookies("login")("idSucursal")
End If
   ' Response.Write(Recordset1__MMColParam)
%>
<%
Dim Recordset1
Dim Recordset1_cmd
Dim Recordset1_numRows

Set Recordset1_cmd = Server.CreateObject ("ADODB.Command")
Recordset1_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset1_cmd.CommandText = "SELECT idfactura,folio,nombreCliente, rfcCliente,estatus,factura.fechaalta as fechaalta,carpetaTimbre,Ncarpeta,factura.idcliente as idcliente, clientes.idCliente as idcliente, documento.descripcion as documento, estadoComprobante, vendedor, total, estcobranza, formPago.descripcion as formapago, estatus, clientes.truncar, factura.retencion, factura.tasa FROM dbo.factura, dbo.clientesFacturacion AS clientes, dbo.sucursales AS empresas, dbo.documento, formPago WHERE factura.idempresa = ? AND clientes.idCliente = factura.idcliente AND empresas.id = ? AND tipo_comprobante = iddocumento AND idpago = factura.forma_pago"&cliente&folio&estatus&documento&vendedor&estatus&estcobranza&formaPago&" ORDER BY "&ordenar&oritentacion
'response.write Recordset1_cmd.CommandText
Recordset1_cmd.Prepared = true
Recordset1_cmd.Parameters.Append Recordset1_cmd.CreateParameter("param1", 5, 1, -1, Recordset1__MMColParam) ' adDouble
Recordset1_cmd.Parameters.Append Recordset1_cmd.CreateParameter("param2", 5, 1, -1, Recordset1__MMColParam) ' adDouble
'Response.write Recordset1_cmd.CommandText
Set Recordset1 = Recordset1_cmd.Execute
Recordset1_numRows = 0
%>
<%
Dim Repeat1__numRows
Dim Repeat1__index

Repeat1__numRows = 15
Repeat1__index = 0
Recordset1_numRows = Recordset1_numRows + Repeat1__numRows
%>
<%
Dim Recordset2__MMColParam
Recordset2__MMColParam2 = "1"
If (Request.Cookies("login")("idSucursal") <> "") Then 
  Recordset2__MMColParam = Request.Cookies("login")("idSucursal")
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
Recordset2_numRows = 15
%>
<%
Dim Recordset3__MMColParam
Recordset3__MMColParam = "1"
If (Request.Cookies("login")("idSucursal") <> "") Then 
  Recordset3__MMColParam = Request.Cookies("login")("idSucursal")
End If
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
Dim RecordsetS
Dim RecordsetS_cmd
Dim RecordsetS_numRows

Set RecordsetS_cmd = Server.CreateObject ("ADODB.Command")
RecordsetS_cmd.ActiveConnection = MM_Conecta1_STRING
RecordsetS_cmd.CommandText = "SELECT * FROM dbo.sucursales WHERE id = ? " 
RecordsetS_cmd.Prepared = true
RecordsetS_cmd.Parameters.Append Recordset3_cmd.CreateParameter("param1", 5, 1, -1, Recordset3__MMColParam) ' adDouble

Set RecordsetS = RecordsetS_cmd.Execute
RecordsetS_numRows = 0
%>
<%
Dim Recordset4__MMColParam
Recordset4__MMColParam = "1"
If (Request.Cookies("login")("idSucursal") <> "") Then 
  Recordset4__MMColParam = Request.Cookies("login")("idSucursal")
End If
%>
<%
Dim Recordset4
Dim Recordset4_cmd
Dim Recordset4_numRows

Set Recordset4_cmd = Server.CreateObject ("ADODB.Command")
Recordset4_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset4_cmd.CommandText = "SELECT * FROM dbo.usuarios, dbo.perfiles WHERE perfiles.id = idPerfil  AND usuarios.idSucursal = ?" 
Recordset4_cmd.Prepared = true
Recordset4_cmd.Parameters.Append Recordset4_cmd.CreateParameter("param1", 5, 1, -1, Recordset4__MMColParam) ' adDouble

Set Recordset4 = Recordset4_cmd.Execute
Recordset4_numRows = 0
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
'  *** Recordset Stats, Move To Record, and Go To Record: declare stats variables

Dim Recordset1_total
Dim Recordset1_first
Dim Recordset1_last

' set the record count
Recordset1_total = Recordset1.RecordCount

' set the number of rows displayed on this page
If (Recordset1_numRows < 0) Then
  Recordset1_numRows = Recordset1_total
Elseif (Recordset1_numRows = 0) Then
  Recordset1_numRows = 1
End If

' set the first and last displayed record
Recordset1_first = 1
Recordset1_last  = Recordset1_first + Recordset1_numRows - 1

' if we have the correct record count, check the other stats
If (Recordset1_total <> -1) Then
  If (Recordset1_first > Recordset1_total) Then
    Recordset1_first = Recordset1_total
  End If
  If (Recordset1_last > Recordset1_total) Then
    Recordset1_last = Recordset1_total
  End If
  If (Recordset1_numRows > Recordset1_total) Then
    Recordset1_numRows = Recordset1_total
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

Set MM_rs    = Recordset1
MM_rsCount   = Recordset1_total
MM_size      = Recordset1_numRows
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
Recordset1_first = MM_offset + 1
Recordset1_last  = MM_offset + MM_size

If (MM_rsCount <> -1) Then
  If (Recordset1_first > MM_rsCount) Then
    Recordset1_first = MM_rsCount
  End If
  If (Recordset1_last > MM_rsCount) Then
    Recordset1_last = MM_rsCount
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
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml"><style type="text/css">
<!--
.vendedor {
	font-size: 10px;
}
-->
</style><!-- InstanceBegin template="/Templates/plantillacfd.dwt.asp" codeOutsideHTMLIsLocked="false" -->
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<!-- InstanceBeginEditable name="doctitle" -->
<title><%=titlePage%></title>
<!--<link rel="stylesheet" href="css.css" type="text/css" media="screen"  />-->
<!-- InstanceEndEditable -->
    <link href="css/main.css" rel="stylesheet" media="screen" />
<!--<link rel="stylesheet" href="css.css" type="text/css" media="screen"  />-->
     <!-- bootstrap CSS -->











  
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
<!-- InstanceBeginEditable name="head" --><!-- InstanceEndEditable -->
</head>

<body>

      <!-- InstanceBeginEditable name="EditRegion1" -->
      <p>&nbsp;</p>
      <table width="95%" border="0" align="center">
        <tr>
          <td width="11%">Facturas</td>
          <td width="72%">Empresa:<strong><%=RecordsetS.Fields.Item("nombre").Value%></strong></td>
          <td width="17%"><a href="facturaadd.asp?idempresa=<%=Request.Cookies("login")("idSucursal")%>"><img src="imagenes/application_(add)_16x16.gif" width="16" height="16" border="0" />Agregar</a></td>
        </tr>
        <tr>
          <td colspan="3"><hr width="100%" /></td>
        </tr>
        <tr>
          <td colspan="3"><form id="form1" name="form1" method="post" action="">
            Buscar Facturas Cliente 
                <label>
                  <select name="clientes" id="clientes">
                    <option value="" <%If (Not isNull(Request.Form("clientes"))) Then If ("" = CStr(Request.Form("clientes"))) Then Response.Write("selected=""selected""") : Response.Write("")%>>Todos</option>
                    <%
While (NOT Recordset2.EOF)
%>
<option value="<%=(Recordset2.Fields.Item("idCliente").Value)%>" <%If (Not isNull(Request.Form("clientes"))) Then If (CStr(Recordset2.Fields.Item("idCliente").Value) = CStr(Request.Form("clientes"))) Then Response.Write("selected=""selected""") : Response.Write("")%> ><%=(Recordset2.Fields.Item("nombreCliente").Value)%></option>
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
              
            Folio 
            <input name="folio" type="text" id="folio" value="<%=Request.Form("folio")%>" size="5" />
            Estatus
          
            <select name="estatus" id="estatus">
              <option value="Facturada" <%If (Not isNull(Request.Form("estatus"))) Then If ("Facturada" = CStr(Request.Form("estatus"))) Then Response.Write("selected=""selected""") : Response.Write("")%>>Facturada</option>            
              <option value="Pendiente" <%If (Not isNull(Request.Form("estatus"))) Then If ("Pendiente" = CStr(Request.Form("estatus"))) Then Response.Write("selected=""selected""") : Response.Write("")%>>Pendiente</option>
              <option value="Cancelada" <%If (Not isNull(Request.Form("estatus"))) Then If ("Cancelada" = CStr(Request.Form("estatus"))) Then Response.Write("selected=""selected""") : Response.Write("")%>>Cancelada</option>
              <option value="Error" <%If (Not isNull(Request.Form("estatus"))) Then If ("Error" = CStr(Request.Form("estatus"))) Then Response.Write("selected=""selected""") : Response.Write("")%>>Error</option>
            </select>
              </label>
          Estatus de cobro 
          <label>
            <select name="estcobranza" id="estcobranza">
              <option value="0" <%If (Not isNull(Request.Form("estcobranza"))) Then If ("" = CStr(Request.Form("estcobranza"))) Then Response.Write("selected=""selected""") : Response.Write("")%>>Todos</option>
              <option value="Pendiente" <%If (Not isNull(Request.Form("estcobranza"))) Then If ("Pendiente" = CStr(Request.Form("estcobranza"))) Then Response.Write("selected=""selected""") : Response.Write("")%>>Pendiente</option>
              <option value="Pagada parcial" <%If (Not isNull(Request.Form("estcobranza"))) Then If ("Pagada Parcial" = CStr(Request.Form("estcobranza"))) Then Response.Write("selected=""selected""") : Response.Write("")%>>Pagada Parcial</option>
              <option value="Pagada" <%If (Not isNull(Request.Form("estcobranza"))) Then If ("Pagada" = CStr(Request.Form("estcobranza"))) Then Response.Write("selected=""selected""") : Response.Write("")%>>Pagada</option>
            </select>
          </label>
          Tipo de Comprobante
<label>
            <select name="documento" id="documento">
              <option value="" <%If (Not isNull(Request.Form("documento"))) Then If ("" = CStr(Request.Form("documento"))) Then Response.Write("selected=""selected""") : Response.Write("")%>>Todos</option>
              <%
While (NOT Recordset3.EOF)
%>
              <option value="<%=(Recordset3.Fields.Item("iddocumento").Value)%>" <%If (Not isNull(Request.Form("documento"))) Then If (CStr(Recordset3.Fields.Item("iddocumento").Value) = CStr(Request.Form("documento"))) Then Response.Write("selected=""selected""") : Response.Write("")%> ><%=(Recordset3.Fields.Item("descripcion").Value)%></option>
              <%
  Recordset3.MoveNext()
Wend
If (Recordset3.CursorType > 0) Then
  Recordset3.MoveFirst
Else
  Recordset3.Requery
End If
%>
            </select>
          </label>
          Forma de pago 
          <label>
            <select name="formaPago" id="formaPago">
              <option value="" <%If (Not isNull(Request.Form("formaPago"))) Then If ("" = CStr(Request.Form("formaPago"))) Then Response.Write("selected=""selected""") : Response.Write("")%>>Todos</option>
              <%
While (NOT Recordset5.EOF)
%>
              <option value="<%=(Recordset5.Fields.Item("idpago").Value)%>" <%If (Not isNull(Request.Form("formaPago"))) Then If (CStr(Recordset5.Fields.Item("idpago").Value) = CStr(Request.Form("formaPago"))) Then Response.Write("selected=""selected""") : Response.Write("")%> ><%=(Recordset5.Fields.Item("descripcion").Value)%></option>
              <%
  Recordset5.MoveNext()
Wend
If (Recordset5.CursorType > 0) Then
  Recordset5.MoveFirst
Else
  Recordset5.Requery
End If
%>
            </select>
          </label>
          Ordenar
<label>
            <select name="ordenar" id="ordenar">
              <option value="factura.fechaalta" <%If (Not isNull(Request.Form("ordenar"))) Then If ("factura.fechaalta" = CStr(Request.Form("ordenar"))) Then Response.Write("selected=""selected""") : Response.Write("")%>>Fecha</option>
<option value="estatus" <%If (Not isNull(Request.Form("ordenar"))) Then If ("estatus" = CStr(Request.Form("ordenar"))) Then Response.Write("selected=""selected""") : Response.Write("")%>>Estatus</option>            
              <option value="idfactura" <%If (Not isNull(Request.Form("ordenar"))) Then If ("idfactura" = CStr(Request.Form("ordenar"))) Then Response.Write("selected=""selected""") : Response.Write("")%>>Id</option>
              <option value="folio" <%If (Not isNull(Request.Form("ordenar"))) Then If ("folio" = CStr(Request.Form("ordenar"))) Then Response.Write("selected=""selected""") : Response.Write("")%>>Folio</option>
              <option value="nombreCliente" <%If (Not isNull(Request.Form("ordenar"))) Then If ("nombreCliente" = CStr(Request.Form("ordenar"))) Then Response.Write("selected=""selected""") : Response.Write("")%>>Cliente</option>
            </select>
          </label>
          ASC
          <label>
<input <%If (CStr(Request.Form("orientacion")) = CStr("ASC")) Then Response.Write("checked=""checked""") : Response.Write("")%> name="orientacion" type="checkbox" id="orientacion" value="ASC" />
          </label>
          <label>
            <br />
            Vendedor
            <select name="vendedor" id="vendedor">
              <option value="" <%If (Not isNull(Request.Form("vendedor"))) Then If ("" = CStr(Request.Form("vendedor"))) Then Response.Write("selected=""selected""") : Response.Write("")%>>Todos</option>
              <%
While (NOT Recordset4.EOF)
%>
              <option value="<%=(Recordset4.Fields.Item("nombre").Value)%>" <%If (Not isNull(Request.Form("vendedor"))) Then If (CStr(Recordset4.Fields.Item("nombre").Value) = CStr(Request.Form("vendedor"))) Then Response.Write("selected=""selected""") : Response.Write("")%> ><%=(Recordset4.Fields.Item("nombre").Value)%></option>
              <%
  Recordset4.MoveNext()
Wend
If (Recordset4.CursorType > 0) Then
  Recordset4.MoveFirst
Else
  Recordset4.Requery
End If
%>
            </select>
            <br />
<input type="submit" name="button" id="button" value="Enviar" />
          </label>
          </form></td>
        </tr>
      </table>
      &nbsp;
      <table width="100%" height="712" border="0">
        <tr valign="top">
          <td>
            <table border="0" align="center">
              <tr align="center" bgcolor="#e33045">
                <td bgcolor="#e33045">Folio</td>
                <td>Cliente</td>
                <td>RFC</td>
                <td> Comprobante</td>
                <td>Vendedor</td>
                <td>Forma Pago</td>
                <td>Imprimir</td>
                <td>Enviar</td>
                <td>Cobrar</td>
              </tr>
              <% While ((Repeat1__numRows <> 0) AND (NOT Recordset1.EOF)) %>
              <%
archvixml = "&nbsp;"
archvipdf = "&nbsp;"
saldo = 0
cargo = 0

if Recordset1.Fields.Item("estadoComprobante").Value  = 1 then
estado = "Vigente"
elseif Recordset1.Fields.Item("estadoComprobante").Value  = 0 then
estado = "Cancelado"
end if

'if para validar el cambio del color
if color = cgrid2 then'color
color = cgrid1
else'color
color = cgrid2
end if'color

'-----------------Recordset para consultar los archivos xml y pdf de la factura---------------------------------
Set RSArchivos_cmd = Server.CreateObject ("ADODB.Command")
RSArchivos_cmd.ActiveConnection = MM_Conecta1_STRING
RSArchivos_cmd.CommandText = "SELECT * FROM dbo.log_idCO WHERE id_factura = " & Recordset1.Fields.Item("idfactura").Value
RSArchivos_cmd.Prepared = true

Set RSArchivos = RSArchivos_cmd.Execute

'if para validar que existan los archivos
if NOT RSArchivos.EOF then
archvixml = RSArchivos.Fields.Item("nombre_archivo").Value
archvipdf = RSArchivos.Fields.Item("archivo_pdf").Value
end if

if Recordset1.Fields.Item("folio").Value <> "" then
'-----------------Recordset para consultar la suma de los pagos---------------------------------
Set RSMonto_cmd = Server.CreateObject ("ADODB.Command")
RSMonto_cmd.ActiveConnection = MM_Conecta1_STRING
RSMonto_cmd.CommandText = "SELECT SUM(total) as pagado FROM dbo.logCXC INNER JOIN tipodepago ON tipo_pago = idtipo WHERE folio =" & Recordset1.Fields.Item("folio").Value & "and estatus = 'Pagado' AND (tipodepago.tipo <> 'Nota de Cargo')"
'response.Write(RSMonto_cmd.CommandText)
RSMonto_cmd.Prepared = true

Set RSMonto = RSMonto_cmd.Execute

if RSMonto.Fields.Item("pagado").Value <> "" then
    saldo = RSMonto.Fields.Item("pagado").Value
end if

                  '----------------------------------------------- recordset para la suma de notas de cargo
Set RSMontoCargo_cmd = Server.CreateObject ("ADODB.Command")
RSMontoCargo_cmd.ActiveConnection = MM_Conecta1_STRING
RSMontoCargo_cmd.CommandText = "SELECT isnull(SUM(total),0) as cargo FROM dbo.logCXC INNER JOIN tipodepago ON tipo_pago = idtipo WHERE folio = " & Recordset1.Fields.Item("folio").Value & "and estatus = 'Pagado' AND (tipodepago.tipo = 'Nota de Cargo')"
'response.Write(RSMontoCargo_cmd.CommandText)
RSMontoCargo_cmd.Prepared = true

Set RSMontoCargo = RSMontoCargo_cmd.Execute
                 

cargo =  RSMontoCargo.fields.item("cargo").value
 'response.Write(cargo)
end if

'calcular total'
Dim recordsetTotal
Dim recordsetTotal_cmd
Dim recordsetTotal_numRows

Set recordsetTotal_cmd = Server.CreateObject ("ADODB.Command")
recordsetTotal_cmd.ActiveConnection = MM_Conecta1_STRING
recordsetTotal_cmd.CommandText = "SELECT precio_unitario, descuento, cantidad, nparte, descripcion, unidad FROM dbo.detfactura WHERE id_factura = "&(Recordset1.Fields.Item("idfactura").Value)
recordsetTotal_cmd.Prepared = true

Set recordsetTotal = recordsetTotal_cmd.Execute
recordsetTotal_numRows = 0

retCte = recordset1.Fields.Item("retencion").Value
iva = Recordset1.Fields.Item("tasa").Value

Dim totalImpuestosTrasladados
totalImpuestosTrasladados=0
Dim totalImpuestosretenidos
totalImpuestosretenidos=0
total = 0
subtotal = 0
'ciclo para sacar el resultaco
While Not recordsetTotal.EOF
          cantidad = recordsetTotal.Fields.item("cantidad").Value
          nparte=recordsetTotal.Fields.Item("nparte").Value
          descripcion = recordsetTotal.Fields.Item("descripcion").Value
          unidad = recordsetTotal.Fields.item("unidad").Value
          precioUnitario = (recordsetTotal.Fields.item("precio_unitario").Value)
            'if Recordset1.Fields.Item("idfactura").Value = 469 then
             '     Response.Write("PRECIO U: "&precioUnitario&" CANTIDAD: "&cantidad&"<br>")
              '    end if
            'if para validar si tiene descuento
            If recordsetTotal.Fields("descuento").Value <> 0 Then
                des = (recordsetTotal.Fields("descuento").Value)
            Else
                des = 0
            End If

          If Recordset1.Fields.item("truncar").Value = "si" Then
                    If des > 0 Then
                        precioUnitario = truncarAseis(precioUnitario * ((100 - (des)) / 100))
                    Else
                        precioUnitario = truncarAseis(precioUnitario)
                    End If
                    totalConcepto = truncarAdos(precioUnitario * cantidad)
                    subTotal = subtotal + truncarAdos(totalConcepto)
                    'validamos si la factura tiene iva
                    If iva <> 0 Then
                        impuesto = truncarAdos(totalConcepto * (iva / 100))
                        totalImpuestosTrasladados = totalImpuestosTrasladados + truncarAdos(impuesto)
                    End If
                    subTotal = truncarAdos(subTotal)
        Else
                If des > 0 Then
                    precioUnitario = truncarAseis(precioUnitario * ((100 - (des)) / 100))
                Else
                    precioUnitario = truncarAseis(precioUnitario)
                End If

                totalConcepto = redondear(precioUnitario * cantidad)
                subTotal = CDbl(subtotal) + redondear(totalConcepto)
 'validamos si la factura tiene iva
                  

                If iva <> 0 Then
                    'Console.Write(totalConcepto)
                    impuesto = redondear(totalConcepto * ((iva) / 100))

                    totalImpuestosTrasladados = totalImpuestosTrasladados +  redondear(impuesto)
                End If
                  'if Recordset1.Fields.Item("idfactura").Value = 469 then
                   ' Response.Write("SUBTOTAL: "&subtotal&" IMPUESTOS: "&totalImpuestosTrasladados&"<br>")
                  'end if 

                If retCte > 0 Then

                    'Console.Write(totalConcepto)
                    retencion = redondear(totalConcepto * (0.16))

                    totalImpuestosretenidos = totalImpuestosretenidos +redondear(retencion)
                Else
                    totalImpuestosretenidos = 0
                End If
                  subTotal = redondear(subTotal)
                 
      end if

'limpiamos las variables de los conceptos
        impuesto = 0
        totalConcepto = 0
        precioUnitario = 0

        Dim total
        total = 0
        Dim isr
        'obtenemos el total
                 ' Response.Write("Toatl impuestos: "&totalImpuestosTrasladados)
        If truncar = "si" Then
            total = truncarAdos(totalImpuestosTrasladados + subTotal)
            'totalImpuestosTrasladados = 0
        Else
            total = redondear(totalImpuestosTrasladados + subTotal)
            '  totalImpuestosTrasladados = 0
        End If

        'validamos si hay isr
        If hayisr <> "" And hayisr = "si" Then
            isr = (subTotal * 0.15)
            total = CDbl(subTotal) - isr

            If truncar = "si" Then
                total = truncarAdos(total)
            Else
                total = redondear(total)
            End If
        End If



recordsetTotal.MoveNext()
Wend
'  Console.Write(hayretencion)
        If retCte > 0 Then

            total = subTotal
        Else

            total = (total)
        End If

total = total 
'response.Write(total)
%>
              <tr bgcolor="<%=color%>">
                <td><%=(Recordset1.Fields.Item("folio").Value)%></td>
                <td><%=Recordset1.Fields.Item("nombreCliente").Value%></td>
                <td><%=Recordset1.Fields.Item("rfcCliente").Value%></td>
                <td><%=(Recordset1.Fields.Item("documento").Value)%></td>
                <td class="vendedor"><%=(Recordset1.Fields.Item("vendedor").Value)%></td>
                <td class="vendedor"><%=(Recordset1.Fields.Item("formapago").Value)%></td>
                <td><% If Recordset1.Fields.Item("estadoComprobante").Value = 1 Then %>
                  <a href="<%=Recordset1.Fields.Item("carpetaTimbre").Value&"\"&archvipdf%>" target="_blank"><img src="imagenes/Printer_Red.png" width="16" height="16" border="0" /></a>
                  <% End If %></td>
                <td align="center"><% If (Recordset1.Fields.Item("estatus").Value)= "Facturada" AND Recordset1.Fields.Item("estadoComprobante").Value = 1 Then %>
                  <a href="correoElectronico.asp?idCliente=<%=(Recordset1.Fields.Item("idCliente").Value)%>&amp;idfactura=<%=(Recordset1.Fields.Item("idfactura").Value)%>"><img src="imagenes/mail_accept.png" width="22" height="16" border="0" /></a>
                  <% End If %></td>
                <td align="center"><a href="javascript:enviar('<%=(Recordset1.Fields.Item("folio").Value)%>','<%=total%>','<%=(saldo)%>','<%=(Recordset1.Fields.Item("estcobranza").Value)%>','<%=(Recordset1.Fields.Item("estatus").Value)%>','<%=(Recordset1.Fields.Item("documento").Value)%>','<%=(Recordset1.Fields.Item("idcliente").Value)%>','<%=(cargo) %>')"><img src="imagenes/money.png" width="16" height="16" border="0" /></a></td>
              </tr>
              <% 
  Repeat1__index=Repeat1__index+1
  Repeat1__numRows=Repeat1__numRows-1
  Recordset1.MoveNext()
Wend
%>
            </table>
            <p>&nbsp;</p>
            <table border="0" align="center">
              <tr>
                <td><% If MM_offset <> 0 Then %>
                  <a href="<%=MM_moveFirst%>"><img src="First.gif" border="0" /></a>
                <% End If ' end MM_offset <> 0 %></td>
                <td><% If MM_offset <> 0 Then %>
                  <a href="<%=MM_movePrev%>"><img src="Previous.gif" border="0" /></a>
                <% End If ' end MM_offset <> 0 %></td>
                <td><% If Not MM_atTotal Then %>
                  <a href="<%=MM_moveNext%>"><img src="Next.gif" border="0" /></a>
                <% End If ' end Not MM_atTotal %></td>
                <td><% If Not MM_atTotal Then %>
                  <a href="<%=MM_moveLast%>"><img src="Last.gif" border="0" /></a>
                <% End If ' end Not MM_atTotal %></td>
              </tr>
          </table></td>
          <td><iframe width="400" height="100%" frameborder="0"  scrolling="yes" src="caja.asp" name="caja"></iframe>&nbsp;</td>
        </tr>
      </table>
      <p>&nbsp;</p>
      <p>&nbsp;</p>
      <!-- InstanceEndEditable -->
 <p>&nbsp;</p>
      <%=footerPage%>
  
      
</body>
<!-- InstanceEnd --></html>
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
<script language="javascript1.2">
function enviar(folio,monto,pagado,estatus, estFac, comprobante, cliente, cargo)
{
    var toSaldo = parseFloat(monto) + parseFloat(cargo)

//if para validar que no sea nota de credito
if (comprobante != "Nota de Credito")
{
//if para validar que el estatus sea factura	
	if (estFac == "Facturada")
	{
        //if para validar que este pagada		
	    if (estatus != "Pagada")
	    {
            //if para validar que tiene folio		
		    if (folio != "")
		    {
			    //if para validar si esta parcialmente pagada
			    if (estatus == "Pagada parcial")
			    {
				    alert("La factura "+folio+" esta parcialmente pagada")
			    }//Pagada parcial			
                
                //variable para guardar las variable y funciones del ifram
                 var ptr=top.frames['caja'];
                //entrando a la valriables y fucniones del ifram
                 ptr.form1.folio.value = folio
                 ptr.form1.monto.value = (monto)
                 ptr.form1.saldo.value = redondear2decimales(toSaldo - pagado)
                 ptr.form1.pagos.value = pagado
                ptr.form1.cantidad_recibida.value = ""
                ptr.form1.cambio.value = ""
                ptr.form1.cantidad_recibida.focus()
                ptr.form1.idcliente.value = cliente
		    }//folio
		    else//folio
		    {
			alert("La factura no cuenta con un folio")
		    }//folio
	    }//Pagada
	    else//Pagada
	    {
		    alert("La factura ya fue paga")
	    }//Pagada
	}//Facturada
	else//Facturada
	{
		alert("El comprobante no esta facturado")
	}//Facturada
}//comprobante
else//comprobante
{
	alert("El folio " + folio + " pertenece a una Nota de Credito")
}//comprobante

}
//funcion para redondear a dos digitos en javascript
function redondear2decimales(numero)
{
var original=parseFloat(numero);
var result=Math.round(original*100)/100 ;
return result;
}
</script>
