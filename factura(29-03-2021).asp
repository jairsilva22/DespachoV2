<%@  language="VBSCRIPT" %>
<!--#include file="Connections/Conecta1.asp" -->
<!--#include file="config.asp"-->
<!--#include file="checklogin.asp"-->
<!--#include file="stylo2.asp"-->
<%Server.ScriptTimeout=50000000%> 
<%
'if para llenar Cookies
if Request.Form("button") <> "" then'button
Response.Cookies("factura")("estatus") = Request.Form("estatus")
end if'button
%>
<%
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
dim vendedorForm
dim documentoForm
dim clienteForm
dim folioForm
dim estatusForm
dim ordenarForm
dim oritentacionForm
dim nombre

color = cgrid2
oritentacion = " DESC"
ordenar = " factura.idfactura"

'if para validar que el menu vendedor no venga solo
if Request.Form("vendedor") <> "" then'clientes
  vendedor = " AND vendedor = '" & Request.Form("vendedor") & "'"
  vendedorForm = Request.Form("vendedor")
ElseIf Request.Querystring("vendedor") <> "" Then
  vendedor = " AND vendedor = '" & Request.Querystring("vendedor") & "'"
  vendedorForm = Request.Querystring("vendedor")
end if'clientes

'if para validar que el menu clitene no venga solo
if Request.Form("documento") <> "" then'clientes
  documento = " AND tipo_comprobante = " & Request.Form("documento")
  documentoForm = Request.Form("documento")
ElseIf Request.Querystring("documento") <> "" Then
  documento = " AND tipo_comprobante = " & Request.Querystring("documento")
  documentoForm = Request.Querystring("documento")
end if'clientes

'if para validar que el menu clitene no venga solo
if Request.Form("clientes") <> "" then'clientes
  cliente = " AND factura.idcliente = " & Request.Form("clientes")
  clienteForm = Request.Form("clientes")
ElseIf Request.Querystring("clientes") <> "" Then
  cliente = " AND factura.idcliente = " & Request.Querystring("clientes")
  clienteForm = Request.Querystring("clientes")
end if'clientes

'if para validar que el textbox folio no venga solo
if Request.Form("folio") <> "" then'
  folio = " AND factura.folio = " & Request.Form("folio")
  folioForm = Request.Form("folio")
Elseif Request.Querystring("folio") <> "" Then
  folio = " AND factura.folio = " & Request.Querystring("folio")
  folioForm = Request.Querystring("folio")
end if'folio

'if para validar que el menu estatus no venga solo
if Request.Form("estatus") <> "" then'
  estatus = " AND factura.estatus = '" & Request.Form("estatus") & "'"
  estatusForm = Request.Form("estatus")
elseif Request.Querystring("estatus") <> "" then
  estatus = " AND factura.estatus = '" & Request.Querystring("estatus") & "'"
  estatusForm = Request.Querystring("estatus")
end if'folio

'if para validar que el menu estatus no venga solo
if Request.Form("ordenar") <> "" then'
  ordenar = " " & Request.Form("ordenar")
  ordenarForm = Request.Form("ordenar")
Elseif Request.Querystring("ordenar") <> "" Then
  ordenar = " " & Request.Querystring("ordenar")
  ordenarForm = Request.Querystring("ordenar")
end if'folio

'if para validar que el menu estatus no venga solo
if Request.Form("orientacion") <> "" then'
  oritentacion = " " & Request.Form("orientacion")
  oritentacionForm = Request.Form("orientacion")
Elseif Request.Querystring("orientacion") <> "" Then
  oritentacion = " " & Request.Querystring("orientacion")
  oritentacionForm = Request.Querystring("orientacion")
end if'folio
%>
<%
idempresa = 1
Dim Recordset1__MMColParam
Recordset1__MMColParam = "1"
If (Session("site_idSucursal") <> "") Then 
  Recordset1__MMColParam = Session("site_idSucursal")
	idempresa = Session("site_idSucursal")
End If
%>
	<%
  Dim RecordsetE
Dim RecordsetE_cmd
Dim RecordsetE_numRows

Set RecordsetE_cmd = Server.CreateObject ("ADODB.Command")
RecordsetE_cmd.ActiveConnection = MM_Conecta1_STRING
RecordsetE_cmd.CommandText = "SELECT * FROM dbo.sucursales WHERE id = "&idempresa 
RecordsetE_cmd.Prepared = true

Set RecordsetE = RecordsetE_cmd.Execute
RecordsetE_numRows = 0
   %>
		
<%
Dim Recordset1
Dim Recordset1_cmd
Dim Recordset1_numRows

Set Recordset1_cmd = Server.CreateObject ("ADODB.Command")
Recordset1_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset1_cmd.CommandText = "SELECT  ASN,idfactura,folio,timbre,generaPDF,nombreCliente,rfcCliente,estatus,factura.fechaalta as fechaalta,Ncarpeta,carpetaTimbre,factura.idcliente as idcliente, clientes.idCliente as idcliente, descripcion, estadoComprobante, vendedor, estatusCorreo, abreviatura, factura.serie FROM dbo.factura, dbo.clientesFacturacion AS clientes, dbo.sucursales, dbo.documento WHERE factura.idempresa = ? AND clientes.idCliente = factura.idcliente AND sucursales.id = ? AND tipo_comprobante = iddocumento"&cliente&folio&estatus&documento&vendedor&" ORDER BY "&ordenar&oritentacion
'Response.Write Recordset1_cmd.CommandText
Recordset1_cmd.Prepared = true
Recordset1_cmd.Parameters.Append Recordset1_cmd.CreateParameter("param1", 5, 1, -1, Recordset1__MMColParam) ' adDouble
Recordset1_cmd.Parameters.Append Recordset1_cmd.CreateParameter("param2", 5, 1, -1, Recordset1__MMColParam) ' adDouble

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
If (Session("site_empresa") <> "") Then 
  Recordset2__MMColParam = Session("site_empresa")
End If
%>
<%
Dim Recordset3__MMColParam
Recordset3__MMColParam = "1"
If (Session("site_empresa") <> "") Then 
  Recordset3__MMColParam = Session("site_empresa")
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
Dim Recordset4__MMColParam
Recordset4__MMColParam = "1"
If (Session("site_empresa") <> "") Then 
  Recordset4__MMColParam = Session("site_empresa")
End If
%>
<%
Dim Recordset4
Dim Recordset4_cmd
Dim Recordset4_numRows

Set Recordset4_cmd = Server.CreateObject ("ADODB.Command")
Recordset4_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset4_cmd.CommandText = "SELECT DISTINCT(vendedor) FROM dbo.factura WHERE vendedor IS NOT NULL AND vendedor <> '' AND abreviatura = 'FA' AND ASN <> '' ORDER BY vendedor ASC" 
Recordset4_cmd.Prepared = true
Recordset4_cmd.Parameters.Append Recordset4_cmd.CreateParameter("param1", 5, 1, -1, Recordset8__MMColParam) ' adDouble

Set Recordset4 = Recordset4_cmd.Execute
Recordset4_numRows = 0
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
<%
'-------Recordset para actualizar los estatus----------------------------------------------
Set RSFactrua_cmd = Server.CreateObject ("ADODB.Command")
RSFactrua_cmd.ActiveConnection = MM_Conecta1_STRING
RSFactrua_cmd.CommandText = "UPDATE dbo.factura SET estatus = 'Facturada' WHERE (folio IS NOT NULL) AND (estatus <> 'Facturada') AND (estatus <> 'Cancelada') AND UUID IS NOT NULL" 
RSFactrua_cmd.Prepared = true

'Set RSFactrua = RSFactrua_cmd.Execute

If clienteForm <> "" Then
  Set RSFactrua_cmd = Server.CreateObject ("ADODB.Command")
  RSFactrua_cmd.ActiveConnection = MM_Conecta1_STRING
  RSFactrua_cmd.CommandText = "SELECT nombreCliente FROM clientes WHERE idCliente = "&clienteForm 
  RSFactrua_cmd.Prepared = true

  Set RSFactrua = RSFactrua_cmd.Execute

  If Not RSFactrua.EOF Then
    nombre = RSFactrua.Fields.Item("nombreCliente").Value&"&nbsp;<a href='javascript:borrar()'><img src='imagenes/tacha.gif' title='Borrar' width='20' height='20' border='0' /></a>"
  End If
End If
%>
<!DOCTYPE html>
<html>
<!-- InstanceBegin template="/Templates/plantillacfd.dwt.asp" codeOutsideHTMLIsLocked="false" -->
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <!-- InstanceBeginEditable name="doctitle" -->
    <title><%=titlePage%></title>
    <link rel="stylesheet" href="css.css" type="text/css" media="screen" />

    <link rel="stylesheet" href="assets/plugins/bootstrap/css/bootstrap.css" />
    <script src="Scripts/jquery.min.js"></script>
    <script src="assets/plugins/bootstrap/js/bootstrap.min.js"></script>

    <link rel="stylesheet" href="efectos/css/demos.css" media="screen" type="text/css" />
        <link href="//maxcdn.bootstrapcdn.com/font-awesome/4.2.0/css/font-awesome.min.css" rel="stylesheet">
	<script type="text/javascript" src="efectos/js/menu-for-applications.js"></script>
    <!-- InstanceEndEditable -->
    <style type="text/css">
        body {
            background-color: #CCC;
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

        .vendedor {
            font-size: 10px;
        }

        .hj {
            font-size: 12px;
        }

        #tb1 {
            background-color: #ffffff;
        }

        .modal-dialog {
            width: 550px;
            margin: 30px auto;
        }
        .loader {
    position: fixed;
    left: 0px;
    top: 0px;
    width: 100%;
    height: 100%;
    z-index: 9999;
    background: url('imagenes/ajax-loader.gif') 50% 50% no-repeat rgb(249,249,249);
    opacity: .8;
}
    </style>
    <script type="text/javascript">
       

        var posicion_x;
        var posicion_y;
        posicion_x = (screen.width / 2) - (500 / 2);
        posicion_y = (screen.height / 2) - (500 / 2);

        function pagar(id, idCliente) {
            open("formaPago.asp?id=" + id + "&idCliente=" + idCliente, '', 'top=' + posicion_y + ', left=' + posicion_x + ', width=600, height=450')
        }

        function clientes() {
            open("clienteFactura.asp", '', 'top=' + posicion_y + ', left=' + posicion_x + ', width=800, height=650')
        }

        function borrar() {
            document.getElementById("cliente").innerHTML = ""
            document.getElementById("clientes").value = ""
        }
    </script>
    <!-- InstanceBeginEditable name="head" -->
    <!-- InstanceEndEditable -->
</head>

<body>
    
    <table width="1024" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF" id="tb1">
        <tr>
            <td width="57">&nbsp;</td>
            <td width="556"><strong>
                <img src="imagenes/viñeta-user.jpg" alt="c" width="20" height="20"></strong><span class="template1"><strong>
                    <% Response.Write(Session("site_nombre")) %>
&nbsp; /&nbsp; <strong><strong>
    <% Response.Write(Session("site_tipo")) %>
</strong></strong></strong></span></td>
            <td width="173">&nbsp;</td>
            <td width="214"><strong>
                <img src="imagenes/viñeta-clock.jpg" alt="c" width="20" height="20">
            </strong><span class="template1"><strong>
                <% Response.Write(date()) %>
/
                <% Response.Write(time()) %>
                <a href="logout.asp"></a></strong></span></td>
        </tr>
        <tr>
            <td colspan="4">
                <table height="120" width="1024" border="1" bordercolor="#C8D7F6" cellpadding="0" cellspacing="0" background="imagenes/cfd-banner.png">
                    <tr>
                        <td height="90" valign="bottom">&nbsp;</td>
                    </tr>
                </table>
                <% set menu=createobject("ADODB.Recordset")
                sqltxt= "SELECT * FROM dbo.menu WHERE (Nivel='Emision' OR Nivel='Todos') AND opcion ='facturacion'  ORDER BY Numero ASC"
                'rs1.CursorType=1
                menu.open sqltxt,MM_conecta1_STRING %>
                <table width="95%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td height="23" valign="bottom">
                            <table width="168" border="0" align="center" cellpadding="2" cellspacing="0" bordercolor="#FFFFFF">
                                <tr valign="top">
                                    <td width="45%" height="18" valign="top"><strong>
                                        <img src="imagenes/viñeta-menu.jpg" width="70" height="20" /></strong></td>
                                    <%
                                      While Not menu.EOF
                                        if  menu("Nivel")="100" and  Session("site_tipo") <> "Administrador"  Then
                                          menu.MoveNext
                                        Else
                                    %>
                                    <td width="11%" valign="top" bgcolor="#FFFFFF"><a href="<%= menu("Link")%>"></a><a href="<%= menu("Link")%>">
                                        <img src="imagenes/<%= menu("imagen") %>" alt="y" width="15" height="20" border="0" align="left" /></a></td>
                                    <td width="32%" align="center" valign="top"><a href="<%= menu("Link")%>"><%= menu("Descripcion") %></a></td>
                                    <td width="12%" valign="top">&nbsp;&nbsp;</td>
                                    <%          
                                          menu.MoveNext
                                         end if
                                       Wend
                                    %>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <!-- InstanceBeginEditable name="EditRegion1" -->
                <p>&nbsp;</p>
                <table width="95%" border="0" align="center">
                    <tr>
                        <td width="11%">Facturas</td>
						<% lugar = RecordsetE.Fields.Item("nombre")  %>
						<% codigo = RecordsetE.Fields.Item("codigo")  %>
                        <td width="52%">Empresa:<strong><%= codigo %> - <%= lugar%></strong></td>
                        <td width="17%"><a href="javascript:carga(<%= idempresa %>)">
                            <img src="imagenes/tarea_add.png" alt="Alternate Text" border="0" />Cargar Factura</a></td>
                        <td width="17%"><a href="facturaadd.asp?idempresa=<%=Session("site_empresa")%>">
                            <img src="imagenes/application_(add)_16x16.gif" width="16" height="16" border="0" />Agregar</a></td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <hr width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <form id="form1" name="form1" method="post" action="">
                                Buscar Facturas Cliente            
                                <a href="javascript:clientes()">
                                    <img src="imagenes/filefind.gif" width="20" height="20"></a>
                                <input type="hidden" name="clientes" id="clientes" value="<%=clienteForm%>">
                                <div id="cliente" style="display: inline;"><strong><%=nombre%></strong></div>
                                <label>Folio<input name="folio" type="text" id="folio" value="<%=Request.Form("folio")%>" size="5" /></label>
                                Estatus
                                <label>
                                    <select name="estatus" id="estatus">
                                        <option value="" <%If (Not isNull(Request.Cookies("factura")("estatus"))) Then If ("" = CStr(Request.Cookies("factura")("estatus"))) Then Response.Write("selected=""selected""") : Response.Write("")%>>Todos</option>
                                        <option value="Pendiente" <%If (Not isNull(Request.Cookies("factura")("estatus"))) Then If ("Pendiente" = CStr(Request.Cookies("factura")("estatus"))) Then Response.Write("selected=""selected""") : Response.Write("")%>>Pendiente</option>
                                        <option value="Facturada" <%If (Not isNull(Request.Cookies("factura")("estatus"))) Then If ("Facturada" = CStr(Request.Cookies("factura")("estatus"))) Then Response.Write("selected=""selected""") : Response.Write("")%>>Facturada</option>
                                        <option value="Cancelada" <%If (Not isNull(Request.Cookies("factura")("estatus"))) Then If ("Cancelada" = CStr(Request.Cookies("factura")("estatus"))) Then Response.Write("selected=""selected""") : Response.Write("")%>>Cancelada</option>
                                        <option value="PCancelada" <%If (Not isNull(Request.Cookies("factura")("estatus"))) Then If ("PCancelada" = CStr(Request.Cookies("factura")("estatus"))) Then Response.Write("selected=""selected""") : Response.Write("")%>>Pendiente de Cancelar</option>
                                        <option value="Error" <%If (Not isNull(Request.Cookies("factura")("estatus"))) Then If ("Error" = CStr(Request.Cookies("factura")("estatus"))) Then Response.Write("selected=""selected""") : Response.Write("")%>>Error</option>
                                        <option value="NotaCO" <%If (Not isNull(Request.Cookies("factura")("estatus"))) Then If ("NotaCO" = CStr(Request.Cookies("factura")("estatus"))) Then Response.Write("selected=""selected""") : Response.Write("")%>>NotaCO</option>
                                        <option value="Procesando" <%If (Not isNull(Request.Cookies("factura")("estatus"))) Then If ("Procesando" = CStr(Request.Cookies("factura")("estatus"))) Then Response.Write("selected=""selected""") : Response.Write("")%>>Procesando</option>
                                        <option value="FormaPago" <%If (Not isNull(Request.Cookies("factura")("estatus"))) Then If ("FormaPago" = CStr(Request.Cookies("factura")("estatus"))) Then Response.Write("selected=""selected""") : Response.Write("")%>>FormaPago</option>
                                   

 </select>
                                </label>
                                Tipo de Comprobante 
                                  <label>
                                      <select name="documento" id="documento">
                                          <option value="" <%If (Not isNull(Request.Form("documento"))) Then If ("" = CStr(Request.Form("documento"))) Then Response.Write("selected=""selected""") : Response.Write("")%>>Todos</option>
                                          <%
                                            While (NOT Recordset3.EOF)
                                          %>
                                          <option value="<%=(Recordset3.Fields.Item("iddocumento").Value)%>" <%If (Not isNull(Request.Form("documento"))) Then If (CStr(Recordset3.Fields.Item("iddocumento").Value) = CStr(Request.Form("documento"))) Then Response.Write("selected=""selected""") : Response.Write("")%>><%=(Recordset3.Fields.Item("descripcion").Value)%></option>
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
                                    Vendedor
                                    <select name="vendedor" id="vendedor">
                                        <option value="" <%If (Not isNull(Request.Form("vendedor"))) Then If ("" = CStr(Request.Form("vendedor"))) Then Response.Write("selected=""selected""") : Response.Write("")%>>Todos</option>
                                        <%
                                        While (NOT Recordset4.EOF)
                                        %>
                                        <option value="<%=(Recordset4.Fields.Item("vendedor").Value)%>" <%If (Not isNull(Request.Form("vendedor"))) Then If (CStr(Recordset4.Fields.Item("vendedor").Value) = CStr(Request.Form("vendedor"))) Then Response.Write("selected=""selected""") : Response.Write("")%>><%=(Recordset4.Fields.Item("vendedor").Value)%></option>
                                        <%
                                              Recordset4.MoveNext()
                                            Wend
                                            If (Recordset4.CursorType > 0) Then
                                              Recordset4.MoveFirst
                                            Else
                                              Recordset4.Requery
                                            End If
                                        %>
                                    </select>&nbsp;&nbsp;
                                    <input type="submit" name="button" id="button" value="Enviar" />
                                </label>


                                <div class="modal fade" id="formAgregar" tabindex="-1" role="dialog" aria-labelledby="myModalLabel1" aria-hidden="true">
                                    <div class="modal-dialog" role="document">
                                        <div class="modal-content" style="border: 3px solid #337ab7; border: 5px solid #337ab7; color: #000000;">
                                            <div class="modal-header" style="background-color: #337ab7;">
                                                <h3 class="modal-title" id="myModalLabelA">&nbsp;</h3>
                                            </div>
                                            <div class="modal-body">
                                                <label id="lblMensaje" name="lblMensaje">¿Desea modificar la factura?</label>
                                                <input type="hidden" name="idfact" class="idfact" />
                                            </div>
                                            <div class="modal-footer">
                                                <button type="button" class="btn btn-primary" id="archivoFrm" onclick="modificarFact()">
                                                    Aceptar
                                                </button>
                                                <button type="button" class="btn btn-danger" id="archivoFrm1" data-dismiss="modal">
                                                    Cancelar
                                                </button>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                  <!--Modal-->
                                <div class="modal fade" id="miModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel2" aria-hidden="true">
                                         <div class="modal-dialog" role="document">
                                        <div class="modal-content" style="border: 3px solid #337ab7; border: 5px solid #337ab7; color: #000000;">
                                            <div class="modal-header" style="background-color: #337ab7;">
                                                <h3 class="modal-title" id="myModalLabelA2">&nbsp;</h3>
                                               <div style="text-align: right">
                                                    <span onclick="cerrar()">X</span>
                                               </div>
                                            </div>
                                            <div class="modal-body">
                                                    <iframe frameborder="0" width="100%" id="frame" height="400" ></iframe>
                                            </div>
                                            <!--<div class="modal-footer">
                                                <button type="button" class="btn btn-primary" id="archivoFrm" onclick="modificarFact()">
                                                    Aceptar
                                                </button>
                                                <button type="button" class="btn btn-danger" id="archivoFrm1" data-dismiss="modal">
                                                    Cancelar
                                                </button>
                                            </div>-->
                                        </div>
                                    </div>
                                </div>

                                
                            </form>
                        </td>
                    </tr>
                </table>
                <p>&nbsp;</p>
                <table border="0" align="center">
                    <tr align="center" bgcolor="<%=ctabla%>">
                        <td>Id</td>
                        <td>Folio</td>
                        <td>Cliente</td>
                        <td>RFC</td>
                        <td>Fecha</td>
                        <td>Tipo</td>
                        <td>Comprobante</td>
                        <td>Estatus</td>
                        <td>Estado del comprobante</td>
                        <td>Vendedor</td>
                        <td>XML</td>
                        <td>PDF</td>
                        <td>Correo Electronico</td>
                        <td>Estatus Correo</td>
                        <td>Mod</td>
                        <td>Procesando</td>
                        <td>Forma de Pago</td>
                        <td>Pdte</td>
                        <td>Pagos</td>
                        <td>Observaciones</td>
                        <td>OP</td>
                        <!--<td>Canc. Factura</td>-->
                        <td>Adenda</td>
                        <td>Anticipo</td>
                    </tr>
                    <% While ((Repeat1__numRows <> 0) AND (NOT Recordset1.EOF)) %>
                    <%
                    archvixml = "&nbsp;"
                    archvipdf = "&nbsp;"

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
                    'Response.Write(RSArchivos_cmd.CommandText)
                    RSArchivos_cmd.Prepared = true

                    Set RSArchivos = RSArchivos_cmd.Execute

                    if NOT RSArchivos.EOF then
                      'If Year(Date) <> Year(Recordset1.Fields.Item("fechaalta").Value) Then
                        archvixml = RSArchivos.Fields.Item("nombre_archivo").Value
                        archvipdf = RSArchivos.Fields.Item("archivo_pdf").Value
                       '' error = RSArchivos.Fields.Item("status").Value
                        observaciones = RSArchivos.Fields.Item("observaciones").Value

                    error = Recordset1.Fields.Item("timbre").Value

                    'Response.Write(error)
                      'Else
                      '  archvixml = Year(Date) & "_" & Recordset1.Fields.Item("folio").Value & Recordset1.Fields.Item("serie").Value & "_" & Recordset1.Fields.Item("abreviatura").Value & "_" & Recordset1.Fields.Item("idfactura").Value & "CO.xml"

                      '  archvipdf =  Year(Date) & "_" & Recordset1.Fields.Item("folio").Value & Recordset1.Fields.Item("serie").Value & "_" & Recordset1.Fields.Item("abreviatura").Value & "_" & Recordset1.Fields.Item("idfactura").Value & "CO.pdf"
                     ' End If

                    end if

                    Set RSPaginas_cmd = Server.CreateObject ("ADODB.Command")
                    RSPaginas_cmd.ActiveConnection = MM_Conecta1_STRING
                    RSPaginas_cmd.CommandText = "SELECT iderror, observaciones, paginas FROM dbo.errores WHERE detalles = '" & observaciones&"'"

                    RSPaginas_cmd.Prepared = true
                    Set RSPaginas = RSPaginas_cmd.Execute
                    tipoS = ""
                    'validar el tipo de documento
                     if Recordset1.Fields.Item("ASN").Value <> "" then
                        'vlalidar si es refaccion o mantenimiento
                         Set RSMtto_cmd = Server.CreateObject ("ADODB.Command")
                         RSMtto_cmd.ActiveConnection = MM_Conecta1_STRING
                         RSMtto_cmd.CommandText = "SELECT * FROM dbo.mantenimiento WHERE nventa = '" & Recordset1.Fields.Item("ASN").Value &"'"

                         RSMtto_cmd.Prepared = true
                         Set RSMtto = RSMtto_cmd.Execute

                         if NOT RSMtto.EOF then
                            tipoS = "Mantenimiento"
                         else
                            tipoS = "Refaccion"
                         end if
                    else
                        
                        tipoS = "Facturacion"
                    end if 
                    %>
                    <tr bgcolor="<%=color%>" class="hj">
                        <td><%=(Recordset1.Fields.Item("idfactura").Value)%></td>
                        <td><%=(Recordset1.Fields.Item("folio").Value)%></td>
                        <td><%=Recordset1.Fields.Item("nombreCliente").Value%></td>
                        <td><%=Recordset1.Fields.Item("rfcCliente").Value%></td>
                        <td><%=(Recordset1.Fields.Item("fechaalta").Value)%></td>
                        <td><%=tipoS %></td>
                        <td><%=(Recordset1.Fields.Item("descripcion").Value)%></td>
                        <td><%=(Recordset1.Fields.Item("estatus").Value)%> &nbsp;&nbsp; <% If (Recordset1.Fields.Item("estatus").Value)="NotaCO"  Then %>  <%=(Recordset1.Fields.Item("ASN").Value)%><% End If %>
                        </td>
                        <td><%=estado%></td>
                        <td class="vendedor"><%=(Recordset1.Fields.Item("vendedor").Value)%></td>

                        <td><% If Ucase(Recordset1.Fields.Item("timbre").Value)= "NO" Then %><a href="download.asp?file=<%=pathconfi&Recordset1.Fields.Item("carpetaTimbre").Value&"\"&archvixml%>" target="_blank"><%=archvixml%></a><% End If %></td>

                        <td><% If not (Recordset1.Fields.Item("generaPDF").Value)  AND Recordset1.Fields.Item("timbre").Value= "NO" Then %><a href="<%=Recordset1.Fields.Item("carpetaTimbre").Value&"\"&archvipdf%>" target="_blank"><%=archvipdf%></a><% End If %><% 'If (Recordset1.Fields.Item("estatus").Value) <> "Facturada" Then %>
                            <a href="impfactura.asp?idfactura=<%=(Recordset1.Fields.Item("idfactura").Value)%>&idcliente=<%=(Recordset1.Fields.Item("idCliente").Value)%>" target="_blank">Vista Previa</a>
                            <% 'End If %></td>

                        <td align="center"><% If (Recordset1.Fields.Item("estatus").Value)= "Facturada" AND Recordset1.Fields.Item("estadoComprobante").Value = 1 AND Recordset1.Fields.Item("timbre").Value= "NO" Then %><a href="correoElectronico.asp?idCliente=<%=(Recordset1.Fields.Item("idCliente").Value)%>&idfactura=<%=(Recordset1.Fields.Item("idfactura").Value)%>"><img src="imagenes/mail_accept.png" width="22" height="16" border="0" /></a><% End If %></td>

                        <% If Recordset1.Fields.Item("estatusCorreo").Value <> "" Then %>
                        <% If Recordset1.Fields.Item("estatusCorreo").Value <> "Enviado" Then %>
                        <td style="background-color: #FF6868;"><%=Recordset1.Fields.Item("estatusCorreo").Value%></td>
                        <% Else %>
                        <td style="background-color: #82FF9E;" align="center"><%=Recordset1.Fields.Item("estatusCorreo").Value%></td>
                        <% End If %>
                        <% Else %>
                        <td>&nbsp;</td>
                        <% End If %>

                        <td align="right">
                            <%If (Recordset1.Fields.Item("estatus").Value)= "Pendiente" OR (Recordset1.Fields.Item("estatus").Value)= "NotaCO"  Then 
                            If Recordset1.Fields.Item("abreviatura").Value = "CRED" Then%>
                            <a href="detNotaCredito.asp?idfactura=<%=(Recordset1.Fields.Item("idfactura").Value)%>&idempresa=<%=Session("site_empresa")%>&idcliente=<%=(Recordset1.Fields.Item("idcliente").Value)%>">
                                <img src="database_table_(edit)_16x16.gif" width="16" height="16" border="0" /></a>
                            <%Else%>
                            <a href="detfacturaadd.asp?idfactura=<%=(Recordset1.Fields.Item("idfactura").Value)%>&idempresa=<%=Session("site_empresa")%>&idcliente=<%=(Recordset1.Fields.Item("idcliente").Value)%>">
                                <img src="database_table_(edit)_16x16.gif" width="16" height="16" border="0" /></a>
                            <%  End If 
                            End If %>
                        </td>
                        <td align="right"><% If (Recordset1.Fields.Item("estatus").Value) <> "Pendiente" AND Recordset1.Fields.Item("estatus").Value <> "Cancelada"  Then%>
                            <iframe src="carga.asp?id=<%=(Recordset1.Fields.Item("idfactura").Value)%>" width="150px" height="70px" frameborder="0"></iframe>
                            <%End If%></td>
                        <td align="center"><% If (Recordset1.Fields.Item("estatus").Value)= "FormaPago" Then%>
                            <a href="javascript:pagar(<%=Recordset1.Fields.Item("idfactura").Value%>, <%=Recordset1.Fields.Item("idcliente").Value%>)">
                                <img src="database_table_(edit)_16x16.gif" width="16" height="16" border="0" /></a>
                            <%End IF%>
                        </td>
                        <td>
                            <% If (Recordset1.Fields.Item("estatus").Value = "FormaPago")  Then
                            %>
                            <a onclick="abrirModal('<%=(Recordset1.Fields.Item("idfactura").Value)%>')">
                                <img src="imagenes/Arrow-left.png" width="16" height="16" border="0" /></a>
                            <%
                                end if
                            %>
                        </td>
                        <td align="center">
                            <% if (Recordset1.Fields.Item("timbre").Value)= "NO" then
                            %>
                            <a href="javascript:pagos(<%=(Recordset1.Fields.Item("idfactura").Value)%>)">
                                <img src="imagenes/Arrow-left.png" width="16" height="16" border="0" /></a>
                            <%end if
                            %>
                        </td>
                        <td align="center">
                            <%if (Recordset1.Fields.Item("estatus").Value)= "Facturada" and error = "Error" then %>
                            <p>
                                <% if not RSPaginas.eof then%>
                                <%=RSPaginas.fields.item("observaciones").value%><a href="<%=RSPaginas.fields.item("paginas").value%>?idfac=<%=(Recordset1.Fields.Item("idfactura").Value)%>&iderror=<%=RSPaginas.fields.item("iderror").value%>"><br>
                                    <%if RSPaginas.fields.item("observaciones").value = "Error del servidor" then
                                    %><u>Volver a procesar</u>
                                    <%
                                else
                                    %><u>Corregir</u><%
                                end if %>
                                </a>
                                <%
                            end if%>
                            </p>
                            <%
                        end if  %>
                        </td>
                        <td>
                            <%if (Recordset1.Fields.Item("estatus").Value) = "Cancelada" Or (Recordset1.Fields.Item("estatus").Value) = "Facturada" then %>
                            <a href="javascript:window.open('CancelarProd.asp?folio=<%=(Recordset1.Fields.Item("folio").Value)%>&tipo=<%=(Recordset1.Fields.Item("abreviatura").Value)%>')">
                                <img src="imagenes/Arrow-left.png" width="16" height="16" border="0" />
                            </a>
                            <%End If%>
                        </td>
                        <!-- cancelar factura -->
                        <!--<td align="center">
                            <%If (Recordset1.Fields.Item("estatus").Value = "Facturada" AND Recordset1.Fields.Item("estadoComprobante").Value = 1) Or (Recordset1.Fields.Item("estatus").Value = "PCancelada") Then  %>
                              <a href="javascript:abrirModalCancelar(<%=(Recordset1.Fields.Item("idfactura").Value)%>)">
                                  <img src="imagenes/Banned.png" width="20" height="20" />
                              </a>
                            <% End If %>
                        </td>-->
                        <td align="center"><% If (Recordset1.Fields.Item("estatus").Value)= "Facturada" AND Recordset1.Fields.Item("estadoComprobante").Value = 1 Then %><a href="javascript:addenda(<%=Recordset1.Fields.Item("idfactura").Value%>, <%=Recordset1.Fields.Item("folio").Value%>)"><img src="imagenes/tarea_add.png" width="16" height="16" /></a><% End If %></td>
                        <td align="center"><% If (Recordset1.Fields.Item("estatus").Value)= "Pendiente" OR Recordset1.Fields.Item("estatus").Value= "FormaPago" Then %><a href="javascript:Anticipos(<%=Recordset1.Fields.Item("idfactura").Value%>)"><img src="imagenes/tarea_add.png" width="16" height="16" /></a><% End If %></td>

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
                            <a href="<%=MM_moveFirst%>">
                                <img src="First.gif" border="0" /></a>
                            <% End If ' end MM_offset <> 0 %></td>
                        <td><% If MM_offset <> 0 Then %>
                            <a href="<%=MM_movePrev%>">
                                <img src="Previous.gif" border="0" /></a>
                            <% End If ' end MM_offset <> 0 %></td>
                        <td><% If Not MM_atTotal Then %>
                            <a href="<%=MM_moveNext%>">
                                <img src="Next.gif" border="0" /></a>
                            <% End If ' end Not MM_atTotal %></td>
                        <td><% If Not MM_atTotal Then %>
                            <a href="<%=MM_moveLast%>">
                                <img src="Last.gif" border="0" /></a>
                            <% End If ' end Not MM_atTotal %></td>
                    </tr>
                </table>
                <p>&nbsp;</p>
                <!-- InstanceEndEditable -->
                <p>&nbsp;</p>


                <%=footerPage%>
            </td>
        </tr>
    </table>
    <script>
       
         function pagos(id)
          {
            window.open("pagosFactura.asp?idfactura="+id,"Pagos","toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=yes, width=850, height=620, top=85, left=140")
          }

         function addenda(idfactura, folio)
          {
            window.open("tipoAdenda.aspx?id=" + idfactura + "&folio=" + folio,"Addenda","toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=yes, width=800, height=600, top=85, left=140")
    
          }

        function Anticipos(idfac){
            window.open("Anticipos.asp?id=" + idfac,"Addenda","toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=yes, width=800, height=600, top=85, left=140")
        }

         function abrirModal(idfact){
            document.form1.idfact.value = idfact; 
             $("#formAgregar").modal();
        }

        function abrirModalCancelar(id){
            $("#frame").attr("src", "CancelarFactura2.aspx?id="+id);
             $("#miModal").modal({backdrop: 'static', keyboard: false});
        }
        function modificarFact(){
            window.location="modEstatusFact.asp?idfac=" + document.form1.idfact.value;
        }
        function cerrar(){
         $("#miModal").modal('hide');
        window.parent.form1.submit();
        }

        function carga(idemp)
        {
            window.open("SubirFactura.aspx?idempresa=" + idemp,"Factura","toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=yes, width=850, height=600, top=85, left=140")
        }
    </script>
</body>
<!-- InstanceEnd -->
</html>
<%
Recordset1.Close()
Set Recordset1 = Nothing
%>
<%
Recordset3.Close()
Set Recordset3 = Nothing
%>
