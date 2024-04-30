<%@  language="VBSCRIPT" %>
<!--#include file="Connections/Conecta1.asp" -->
<!--#include file="config.asp"-->
<!--#include file="checklogin.asp"-->
<!--#include file="stylo2.asp"-->
<%Server.ScriptTimeout=50000000%> 
<% 
Response.Expires = 0
Response.Expiresabsolute = Now() - 1 
Response.AddHeader "pragma","no-cache" 
Response.AddHeader "cache-control","private" 
Response.CacheControl = "no-cache" 


Dim cliente
if request.QueryString("idCliente")<> "" then
	cliente = request.QueryString("idCliente")
else
	if request.QueryString("cliente")<> "" then
		cliente = request.QueryString("cliente")
	end if
end if

Dim Recordset3
Dim Recordset3_cmd
Dim Recordset3_numRows

If Request.QueryString("val") <> "" then
  Set Recordset3_cmd = Server.CreateObject ("ADODB.Command")
  Recordset3_cmd.ActiveConnection = MM_Conecta1_STRING
  Recordset3_cmd.CommandText = "UPDATE factura SET estatus ='FormaPago' WHERE idfactura = "&Request.QueryString("idfactura") 
  Recordset3_cmd.Prepared = true
  Set Recordset3 = Recordset3_cmd.Execute

  Response.Redirect("factura.asp")
End IF

dim pagina'Variable para guardar la pagina asi donde va a regresar
pagina = "factura.asp"

if Request.QueryString("pagina") <> "" then
pagina = Request.QueryString("pagina")
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
Dim Recordset3__MMColParam
Recordset3__MMColParam = "1"
If (Request.QueryString("idCliente") <> "") Then 
  Recordset3__MMColParam = Request.QueryString("idCliente")
End If

Set Recordset3_cmd = Server.CreateObject ("ADODB.Command")
Recordset3_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset3_cmd.CommandText = "SELECT * FROM dbo.clientesFacturacion, dbo.ciudades AS ciudad, dbo.estados AS estado WHERE idCliente = ? AND ciudad.id = ciudadCliente AND estado.id = estadoCliente " 
Recordset3_cmd.Prepared = true
Recordset3_cmd.Parameters.Append Recordset3_cmd.CreateParameter("param1", 5, 1, -1, Recordset3__MMColParam) ' adDouble
    'response.Write(Recordset3_cmd.CommandText)
Set Recordset3 = Recordset3_cmd.Execute
Recordset3_numRows = 0

    entrega = ""
    if not Recordset3.EOF then
    entrega = Recordset3.Fields.Item("entrega").Value
    end if
%>
<%
dim tretencion'Vairable para guardar el iva rteneido
dim retencion'Variable para guardar la retencion
dim soloLectuta'Variable para validar que solo el usuario administadro modifique el iva
dim color'Variable para guardar el color
dim impuesto'Variable para guardar el impuesto
dim subtotal'Variable para guardar el subtotal
dim total'Variabel para guardar el total
dim comprobante'Variable para guardar el tipo de comprobante
dim pago'Variable para guardar la forma de pago
dim impuesto2'Variable para guardar la retencion
dim impv'Variable para guardar el impuesto

tretencion = 0
retencion = 0
color = cgrid2

'if para validar que el iva se pueda mover
if session("site_username")="peisa" then'peisa
soloLectuta = "readonly="&"readonly"
end if'peisa

'if para validar el inserten articulos
If (CStr(Request("MM_insert")) = "form1") Then'MM_insert
'if para validar si se aborta el insert
  If (Not MM_abortEdit) Then'MM_abortEdit

'if para validar que la suma del iva a la factura
if Request.Form("iva") <> "" then
impuesto = Request.Form("iva") + CDbl(Request.Form("impuesto2"))
else
impuesto = Request.Form("impuesto2")
end if

'if para valdiar que el usuario tenga retencion
if Recordset3.Fields.Item("retencion").Value = true then'site_retencion
tretencion = Request.Form("impuesto2")
retencion = impuesto
end if'site_retencion
    'response.Write(Recordset3.Fields.Item("retencion").Value)
'if para validar que la suma del iva a la factura
if Request.Form("retencion") <> "" then
retencion = impuesto * (Request.Form("retencion")/100)
tretencion =  CDbl(Request.Form("impuesto2")) * (Request.Form("retencion")/100)
end if

'if para validar que la suma del subtotal a la factura
if Request.Form("subtotal") <> "" then
subtotal = Request.Form("subtotal") + CDbl(Request.Form("importe"))
else
subtotal = Request.Form("importe")
end if

'if para validar que la suma del total a la factura
if Request.Form("total2") <> "" then
total = CDbl(Request.Form("total")) + CDbl(Request.Form("total2"))
else
total = impuesto + CDbl(subtotal)
end if

'if para validar que tiene retencion
if retencion <> 0 then'retencion
total = total - CDbl(tretencion)
end if'retencion

'-----------Recordset para modificar el iva, subtotal y total de la fatcura--------------------------------
  Set RSFactura_cmd = Server.CreateObject ("ADODB.Command")
RSFactura_cmd.ActiveConnection = MM_Conecta1_STRING
RSFactura_cmd.CommandText = "UPDATE dbo.factura SET iva = "& impuesto &", subtotal = " & subtotal & ", total = "& total &", retencion = "&retencion&" WHERE idfactura = " & Request.QueryString("idfactura")
RSFactura_cmd.Prepared = true

'Set RSFactura = RSFactura_cmd.Execute
  
  End If'MM_abortEdit
End If'MM_insert
%>
<%
If (CStr(Request("MM_insert")) = "form1") Then


  If (Not MM_abortEdit) Then
  
  ' unidad
    Dim MM_editCmd1
	Dim Recordset21
	Dim Recordset21_numRows

    Set MM_editCmd1 = Server.CreateObject ("ADODB.Command")
    MM_editCmd1.ActiveConnection = MM_Conecta1_STRING
    MM_editCmd1.CommandText = "SELECT unidadSAT AS nombre FROM dbo.unidadesDeMedida where descripcion= '"& Request.Form("unidad") &"' "
   
	MM_editCmd1.Prepared = true
   
   Set Recordset21 = MM_editCmd1.Execute
	Recordset21_numRows = 0

	if NOT Recordset21.EOF then
	cveUnidad = Recordset21.Fields.Item("nombre").Value
	end if
  
    ' execute the insert
    descripcion = Replace(Request.Form("descripcion"), "'","")
    Dim MM_editCmd

    Set MM_editCmd = Server.CreateObject ("ADODB.Command")
    MM_editCmd.ActiveConnection = MM_Conecta1_STRING
    MM_editCmd.CommandText = "INSERT INTO dbo.detFactura (cantidad, descripcion, precio_unitario, unidad, descuento, total, iva, id_factura, id_usuario, fecha_alta, impuesto, nparte, retencion, tretencion, claveProdServ, claveUnidad, impuestoP, numPedimento) VALUES (?, '"&descripcion&"', ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,'"& Request.Form("claveProdServ") &"','"& cveUnidad &"', 0, '"&request.Form("numPedimento")&"')" 
    MM_editCmd.Prepared = true
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param1", 5, 1, -1, MM_IIF(cdbl(Request.Form("cantidad")), cdbl(Request.Form("cantidad")), null)) ' adDouble
   ' MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param2", 201, 1, 255, Request.Form("descripcion")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param3", 5, 1, -1, MM_IIF(CDbl(Request.Form("precio_unitario")), CDbl(Request.Form("precio_unitario")), null)) ' adDouble
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param4", 201, 1, 255, Request.Form("unidad")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param5", 5, 1, -1, MM_IIF(Request.Form("descuento"), Request.Form("descuento"), null)) ' adDouble
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param6", 5, 1, -1, MM_IIF(CDbl(Request.Form("importe")), CDbl(Request.Form("importe")), null)) ' adDouble
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param7", 5, 1, -1, MM_IIF(Request.Form("impuesto"), Request.Form("impuesto"), null)) ' adDouble
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param8", 5, 1, -1, MM_IIF(Request.Form("id_factura"), Request.Form("id_factura"), null)) ' adDouble
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param9", 5, 1, -1, MM_IIF(Request.Form("id_usuario"), Request.Form("id_usuario"), null)) ' adDouble
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param10", 135, 1, -1, MM_IIF(Request.Form("fecha_alta"), Request.Form("fecha_alta"), null)) ' adDBTimeStamp
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param11", 5, 1, -1, MM_IIF(CDbl(Request.Form("impuesto2")), CDbl(Request.Form("impuesto2")), null)) ' adDouble
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param12", 201, 1, 255, Request.Form("nparte")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param13", 5, 1, -1, MM_IIF(Request.Form("retencion"), Request.Form("retencion"), null)) ' adDouble
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param14", 5, 1, -1, MM_IIF(CDbl(tretencion), CDbl(tretencion), null)) ' adDouble			
    MM_editCmd.Execute
    MM_editCmd.ActiveConnection.Close

    ' append the query string to the redirect URL
    Dim MM_editRedirectUrl
    MM_editRedirectUrl = "MontosFactura.asp?ter=1"
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
Recordset1_cmd.CommandText = "SELECT idfactura, tipo_comprobante, forma_pago, idcliente, estatus, iva, subtotal, total, retencion, fechaalta, ASN, embarque, vendedor, ordenCompra,  CONVERT (CHAR (12), fEmbarque, 107) as fEmbarque, VA, FOB, terminos, obsCliente, nfactura, tretencion FROM dbo.factura WHERE idfactura = ?" 
Recordset1_cmd.Prepared = true
Recordset1_cmd.Parameters.Append Recordset1_cmd.CreateParameter("param1", 5, 1, -1, Recordset1__MMColParam) ' adDouble

Set Recordset1 = Recordset1_cmd.Execute
Recordset1_numRows = 0

'buscar el tipo de documento agregado 31/10/2020
Dim Recordset3T
Dim Recordset3T_cmd
Dim Recordset3T_numRows

Set Recordset3T_cmd = Server.CreateObject ("ADODB.Command")
Recordset3T_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset3T_cmd.CommandText = "SELECT * FROM dbo.documento WHERE iddocumento = "& Recordset1.Fields.Item("tipo_comprobante") &" ORDER BY descripcion ASC" 
Recordset3T_cmd.Prepared = true

Set Recordset3T = Recordset3T_cmd.Execute
Recordset3T_numRows = 0

    tipoDocumento = Recordset3T.Fields.Item("descripcion")
%>
<%
Dim Recordset2__MMColParam
Recordset2__MMColParam = "1"
If (Request.QueryString("idfactura") <> "") Then 
  Recordset2__MMColParam = Request.QueryString("idfactura")
End If
%>
<%
Dim Recordset2
Dim Recordset2_cmd
Dim Recordset2_numRows

Set Recordset2_cmd = Server.CreateObject ("ADODB.Command")
Recordset2_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset2_cmd.CommandText = "SELECT cantidad, nparte, claveProdServ, descripcion, numpedimento as pedimento, precio_unitario, descuento, impuesto, total, id_detFactura  FROM dbo.detFactura WHERE id_factura = ?" 
Recordset2_cmd.Prepared = true
Recordset2_cmd.Parameters.Append Recordset2_cmd.CreateParameter("param1", 5, 1, -1, Recordset2__MMColParam) ' adDouble

Set Recordset2 = Recordset2_cmd.Execute
Recordset2_numRows = 0
%>
<%
Dim Recordset4__MMColParam
Recordset4__MMColParam = (Recordset1.Fields.Item("tipo_comprobante").Value)
If (Request("MM_EmptyValue") <> "") Then 
  Recordset4__MMColParam = Request("MM_EmptyValue")
End If
%>
<%
Dim Recordset4
Dim Recordset4_cmd
Dim Recordset4_numRows

Set Recordset4_cmd = Server.CreateObject ("ADODB.Command")
Recordset4_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset4_cmd.CommandText = "SELECT * FROM dbo.documento WHERE iddocumento = ?" 
Recordset4_cmd.Prepared = true
Recordset4_cmd.Parameters.Append Recordset4_cmd.CreateParameter("param1", 5, 1, -1, Recordset4__MMColParam) ' adDouble

Set Recordset4 = Recordset4_cmd.Execute
Recordset4_numRows = 0
%>
<%
Dim Recordset5__MMColParam
Recordset5__MMColParam = (Recordset1.Fields.Item("forma_pago").Value)
If (Request("MM_EmptyValue") <> "") Then 
  Recordset5__MMColParam = Request("MM_EmptyValue")
End If
%>
<%
Dim Recordset5
Dim Recordset5_cmd
Dim Recordset5_numRows

Set Recordset5_cmd = Server.CreateObject ("ADODB.Command")
Recordset5_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset5_cmd.CommandText = "SELECT * FROM dbo.formPago WHERE idpago = ?" 
Recordset5_cmd.Prepared = true
Recordset5_cmd.Parameters.Append Recordset5_cmd.CreateParameter("param1", 5, 1, -1, Recordset5__MMColParam) ' adDouble

Set Recordset5 = Recordset5_cmd.Execute
Recordset5_numRows = 0
%>
<%
Dim Recordset6__MMColParam
Recordset6__MMColParam = "1"
If (Request.Cookies("login")("idSucursal") <> "") Then 
  Recordset6__MMColParam = Request.Cookies("login")("idSucursal")
End If
%>
<%
Dim Recordset6
Dim Recordset6_cmd
Dim Recordset6_numRows

Set Recordset6_cmd = Server.CreateObject ("ADODB.Command")
Recordset6_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset6_cmd.CommandText = "SELECT * FROM dbo.unidadesDeMedida ORDER BY descripcion ASC" 
Recordset6_cmd.Prepared = true
Recordset6_cmd.Parameters.Append Recordset6_cmd.CreateParameter("param1", 5, 1, -1, Recordset6__MMColParam) ' adDouble

Set Recordset6 = Recordset6_cmd.Execute
Recordset6_numRows = 0
%>
<%
Dim Recordset7__MMColParam
Recordset7__MMColParam = "1"
If (Request.Cookies("login")("idSucursal") <> "") Then 
  Recordset7__MMColParam = Request.Cookies("login")("idSucursal")
End If
%>
<%
Dim Recordset7
Dim Recordset7_cmd
Dim Recordset7_numRows

Set Recordset7_cmd = Server.CreateObject ("ADODB.Command")
Recordset7_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset7_cmd.CommandText = "SELECT * FROM dbo.sucursales WHERE id = ?" 
Recordset7_cmd.Prepared = true
Recordset7_cmd.Parameters.Append Recordset7_cmd.CreateParameter("param1", 5, 1, -1, Recordset7__MMColParam) ' adDouble

Set Recordset7 = Recordset7_cmd.Execute
Recordset7_numRows = 0
%>
<%
Dim Repeat1__numRows
Dim Repeat1__index

Repeat1__numRows = -1
Repeat1__index = 0
Recordset2_numRows = Recordset2_numRows + Repeat1__numRows
%>
<%
'if para valdiar que hay datos
if NOT Recordset4.EOF then'Recordset4
comprobante = Recordset4.Fields.Item("descripcion").Value
end if'Recordset4

'if para valdiar que hay datos
if NOT Recordset5.EOF then'Recordset4
pago = Recordset5.Fields.Item("descripcion").Value
end if'Recordset4

if Recordset1.Fields.Item("retencion").Value <> "" then
retencion = (Recordset1.Fields.Item("retencion").Value)
else
retencion ="---"
end if

if Request.QueryString("impuesto") <> "" then 
impv = Request.QueryString("impuesto") 
else 
impv = iva 
end if

   ' Request.QueryString("idcliente")

Set RecordsetCte_cmd = Server.CreateObject ("ADODB.Command")
RecordsetCte_cmd.ActiveConnection = MM_Conecta1_STRING
RecordsetCte_cmd.CommandText = "SELECT * FROM clientesFacturacion WHERE idCliente = " &request.QueryString("idcliente") 
RecordsetCte_cmd.Prepared = true

Set RecordsetCte = RecordsetCte_cmd.Execute
RecordsetCte_numRows = 0

%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <script language="JavaScript" src="jsF/overlib_mini.js"></script>
<!-- InstanceBegin template="/Templates/plantillacfd.dwt.asp" codeOutsideHTMLIsLocked="false" -->
<head>
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<link rel="stylesheet" href="efectos/css/demos.css" media="screen" type="text/css">
    <link href="//maxcdn.bootstrapcdn.com/font-awesome/4.2.0/css/font-awesome.min.css" rel="stylesheet">
	<script type="text/javascript" src="efectos/js/menu-for-applications.js"></script>
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
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
        <tr>

            <td>
                <div   style="overflow:auto; width: 100%; height: 100%; scrollbar-arrow-color : #B2B2B2; scrollbar-face-color : #3FA5DC;
 scrollbar-track-color:#B6D0F3;"  >
                <!-- InstanceBeginEditable name="EditRegion1" -->
                <body onload="document.forms[0].elements[0].focus()">
                    <p>&nbsp;</p>
                    <table width="90%" border="0" align="center">
                        <tr align="center" valign="top">
                            <td>
                                <table width="43%" border="0" align="center">
                                    <tr align="center">
                                        <td><strong>Agregar los articulos de la factura</strong></td>
                                    </tr>
                                    <tr>
                                        <td align="right">&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td>
                                <table width="80%" border="0" align="center">
                                    <tr>
                                        <td>Factura Interna <strong><%=(Recordset1.Fields.Item("nfactura").Value)%></strong></td>
                                        <td align="right">Modificar datos
                <% If (Recordset1.Fields.Item("estatus").Value)= "Pendiente" OR (Recordset1.Fields.Item("estatus").Value)= "NotaCO"  Then %>
                                            <a href="facturamod.asp?idfactura=<%=(Recordset1.Fields.Item("idfactura").Value)%>&amp;idempresa=<%=Request.Cookies("login")("idSucursal")%>&amp;idcliente=<%=Request.QueryString("idcliente")%>&amp;nfactura=<%=(Recordset1.Fields.Item("nfactura").Value)%>">
                                                <img src="imagenes/database_table_(edit)_16x16.gif" width="16" height="16" border="0" /></a>
                                            <% End If %></td>
                                    </tr>
                                    <tr>
                                        <td><%=(Recordset7.Fields.Item("nombre").Value)%></td>
                                        <td><strong><%=comprobante%></strong></td>
                                    </tr>
                                    <tr>
                                        <td><%=(Recordset7.Fields.Item("rfc").Value)%></td>
                                        <td>Fecha:<%=(Recordset1.Fields.Item("fechaalta").Value)%></td>
                                    </tr>
                                    <tr>
                                        <td><%=(Recordset7.Fields.Item("calle").Value)%></td>
                                        <td>ASN:<%=(Recordset1.Fields.Item("ASN").Value)%></td>
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
                                    <tr align="center" bgcolor="#A8A8A8">
                                        <td colspan="3"><strong>Cliente:</strong></td>
                                    </tr>
                                    <tr>
                                         <td align="right" width="10%"></td>
                                        <td width="15%">Nombre fiscal:</td>
                                        <td><%=RecordsetCte.fields.item("nombreempresa").value %></td>
                                    </tr>
                                    <tr>
                                         <td align="right"></td>
                                        <td>RFC:</td>
                                        <td><%=RecordsetCte.fields.item("rfccliente").value %></td>
                                    </tr>
                                    <tr> <td align="right"></td>
                                        <td>Dirección</td>
                                        <td><%=RecordsetCte.fields.item("callecliente").value %>  # <%=RecordsetCte.fields.item("noExterior") %> Col. <%=RecordsetCte.fields.item("colonia").value %></td>
                                    </tr>
                                    <tr> <td align="right"></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%" border="0">
                                    <tr align="center" bgcolor="#A8A8A8">
                                        <td><strong>Bill To:</strong></td>
                                        <td><strong>Ship To:</strong></td>
                                    </tr>
                                    <tr>
                                        <td><%=(Recordset1.Fields.Item("embarque").Value)%></td>
                                        <td><%=(entrega)%></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%" border="0" align="center" cellpadding="0">
                                    <tr align="center" bgcolor="<%=ctabla%>" class="stylo2">
                                        <td>Vendedor</td>
                                        <td>Orden de Compra</td>
                                        <td>Fecha de Embarque</td>
                                        <td>V/A</td>
                                        <td>Terminos</td>
                                    </tr>
                                    <tr>
                                        <td><%=(Recordset1.Fields.Item("vendedor").Value)%></td>
                                        <td><%=(Recordset1.Fields.Item("ordenCompra").Value)%></td>
                                        <td><%=(Recordset1.Fields.Item("fEmbarque").Value)%></td>
                                        <td><%=(Recordset1.Fields.Item("VA").Value)%></td>
                                        <td><%=(Recordset1.Fields.Item("terminos").Value)%></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr bgcolor="#CBDFF5">
                            <td bgcolor="#CBDFF5">
                                <form action="<%=MM_editAction%>" method="post" name="form1" id="form1" onsubmit="return val(this)">
                                    <% If (Recordset1.Fields.Item("estatus").Value)= "Pendiente" Then %>
                                    <table width="79%" border="0" align="center">
                                        <tr bgcolor="<%=ctabla%>">
                                            <td>Cant:</td>
                                            <td>N° Parte:</td>
                                            <td>Clave SAT:</td>
                                            <td>Descripcion:</td>
                                            <td>Número de pedimento:</td>
                                            <td>Precio unitario:</td>
                                            <td>Unidad:</td>
                                            <td>Desc:</td>
                                            <td>Total:</td>
                                            <td>Impuesto:</td>
                                        </tr>
                                        <tr valign="top">
                                            <td>
                                                <input type="text" name="cantidad" value="<%=Request.QueryString("cantidad")%>" size="10" onchange="tot()" onkeypress="return SoloNumerosDec(event);" /></td>
                                            <td>
                                                <label>
                                                    <input name="nparte" type="text" id="nparte" value="<%=Request.QueryString("nparte")%>" size="2" />
                                                </label>
                                            </td>
                                            <td>
                                                <label for="claveProdServ"></label>
                                                <input type="text" name="claveProdServ" id="claveProdServ" size="10" /></td>
                                            <td>
                                                <textarea name="descripcion" cols="50" rows="2"><%=Request.QueryString("descripcion")%></textarea></td>
                                            <td>
                                                 <input name="numPedimento" type="text" id="numPedimento" value="<%=Request.QueryString("numPedimento")%>" size="10" />
                                            </td>
                                            <td>
                                                <input type="text" name="precio_unitario" value="<%=Replace(Request.QueryString("precio"),"*",".")%>" size="10" onchange="tot()" onkeypress="return SoloNumerosDec(event);" /></td>
                                            <td>
                                                <label>
                                                    <select name="unidad" id="unidad">
                                                        <option value="" <%If (Not isNull(Request.QueryString("unidad"))) Then If ("" = CStr(Request.QueryString("unidad"))) Then Response.Write("selected=""selected""") : Response.Write("")%>>Seleccionar</option>
                                                        <%
                                                        While (NOT Recordset6.EOF)
                                                        %>
                                                        <option value="<%=(Recordset6.Fields.Item("descripcion").Value)%>" <%If (Not isNull(Request.QueryString("unidad"))) Then If (CStr(Recordset6.Fields.Item("descripcion").Value) = CStr(Request.QueryString("unidad"))) Then Response.Write("selected=""selected""") : Response.Write("")%>><%=(Recordset6.Fields.Item("unidad").Value)%></option>
                                                        <%
                                                          Recordset6.MoveNext()
                                                        Wend
                                                        If (Recordset6.CursorType > 0) Then
                                                          Recordset6.MoveFirst
                                                        Else
                                                          Recordset6.Requery
                                                        End If
                                                        %>
                                                    </select>
                                                </label>
                                            </td>
                                            <td>
                                                <input type="text" name="descuento" value="<%=Request.QueryString("descuento")%>" size="2" onchange="desc()" onkeypress="return SoloNumerosDec(event);" /></td>
                                            <td>
                                                <input name="importe" type="text" id="importe" size="15" value="<%=Request.QueryString("importe")%>" onkeypress="return SoloNumerosDec(event);" /></td>
                                            <td>
                                                <input name="impuesto" type="text" onchange="imp()" value="<%=impv%>" size="1" <%=soloLectuta%> /></td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>
                                                <input type="submit" value="Guardar" onsubmit="true" /></td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>
                                    <input type="hidden" name="id_factura" value="<%=Request.QueryString("idfactura")%>" />
                                    <input type="hidden" name="id_usuario" value="<%=Request.Cookies("login")("id")%>" />
                                    <input type="hidden" name="fecha_alta" value="<%=datef2(Date())%>" />
                                    <input type="hidden" name="MM_insert" value="form1" />
                                    <input name="iva" type="hidden" id="iva" value="<%=(Recordset1.Fields.Item("iva").Value)%>" />
                                    <input name="subtotal" type="hidden" id="subtotal" value="<%=(Recordset1.Fields.Item("subtotal").Value)%>" />
                                    <input name="total2" type="hidden" id="total2" value="<%=(Recordset1.Fields.Item("total").Value)%>" />
                                    <input name="impuesto2" type="hidden" id="impuesto2" value="<%=Request.QueryString("impuesto2")%>" />
                                    <input type="hidden" name="total" value="<%=Request.QueryString("total2")%>" size="2" />
                                    <input name="retencion" type="hidden" id="retencion" value="<%=(Recordset1.Fields.Item("tretencion").Value)%>" />
                                    <% End If %>
                                </form>
                            </td>
                        </tr>
                        <tr bgcolor="#CBDFF5">
                            <td>&nbsp;</td>
                        </tr>
                        <tr bgcolor="#CBDFF5">
                            <td>
                                <table width="100%" border="0" align="center">
                                    <tr bgcolor="<%=ctabla%>">
                                        <td bgcolor="<%=ctabla%>">Cantidad</td>
                                        <td>N° Parte</td>
                                        <td>Clave SAT</td>
                                        <td>Descripcion</td>
                                        <td>Número de pedimento</td>
                                        <td>Precio Unitario</td>
                                        <td>Descuento</td>
                                        <td>Impuesto</td>
                                        <td>Total</td>
                                        <td>Mod</td>
                                    </tr>
                                    <% While ((Repeat1__numRows <> 0) AND (NOT Recordset2.EOF)) %>
                                    <%
                                    'if para validar el cambio de color
                                    if color = cgrid2 then'color
                                    color = cgrid1
                                    else'color
                                    color = cgrid2
                                    end if'color
                                    %>
                                    <tr bgcolor="<%=color%>">
                                        <td><%=(Recordset2.Fields.Item("cantidad").Value)%></td>
                                        <td><%=(Recordset2.Fields.Item("nparte").Value)%></td>
                                        <td><%=(Recordset2.Fields.Item("claveProdServ").Value)%></td>
                                        <td><%=(Recordset2.Fields.Item("descripcion").Value)%></td>
                                        <td><%= recordset2.fields.item("pedimento").value %></td>
                                        <td><%=FormatNumber(Recordset2.Fields.Item("precio_unitario").Value)%></td>
                                        <td><%=(Recordset2.Fields.Item("descuento").Value)%></td>
                                        <td><%=FormatNumber(Recordset2.Fields.Item("impuesto").Value)%></td>
                                        <td><%=FormatNumber(Recordset2.Fields.Item("total").Value)%></td>
                                        <td><% If  (Recordset1.Fields.Item("estatus").Value)= "Pendiente"Then %>
                                            <a href="javascript:eliminar('<%=(Recordset2.Fields.Item("id_detFactura").Value)%>','<%=Request.QueryString("idfactura")%>','<%=Request.QueryString("idcliente")%>')">
                                                <img src="imagenes/database_table_(edit)_16x16.gif" width="16" height="16" border="0" /></a></td>
                                        <% End If %>
                                    </tr>
                                    <% 
                                      Repeat1__index=Repeat1__index+1
                                      Repeat1__numRows=Repeat1__numRows-1
                                      Recordset2.MoveNext()
                                    Wend
                                    %>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td align="right">&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%" border="0">
                                    <tr align="center" bgcolor="<%=ctabla%>">
                                        <td colspan="9"><strong>OBSERVACIONES</strong></td>
                                    </tr>
                                    <tr>
                                        <td colspan="9"><%=(Recordset1.Fields.Item("obsCliente").Value)%></td>
                                    </tr>
                                    <tr>
                                        <td width="2%">&nbsp;</td>
                                        <td width="3%">&nbsp;</td>
                                        <td width="3%">&nbsp;</td>
                                        <td width="3%">&nbsp;</td>
                                        <td width="3%">&nbsp;</td>
                                        <td width="3%" align="right">&nbsp;</td>
                                        <td width="3%" align="center">&nbsp;</td>
                                        <td width="47%">&nbsp;</td>
                                        <td width="33%">
                                            <table width="100%" border="0" align="right" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td style="border-bottom: 1px solid #ccc;">SUB-TOTAL:</td>
                                                    <td style="border-bottom: 1px solid #ccc;" align="center">$</td>
                                                    <td style="border-bottom: 1px solid #ccc;" align="right"><%=FormatNumber(Recordset1.Fields.Item("subtotal").Value)%></td>
                                                </tr>
                                                <tr>
                                                    <td style="border-bottom: 1px solid #ccc;">IVA:</td>
                                                    <td style="border-bottom: 1px solid #ccc;" align="center">$</td>
                                                    <td style="border-bottom: 1px solid #ccc;" align="right"><%=FormatNumber(Recordset1.Fields.Item("iva").Value)%></td>
                                                </tr>
                                                <tr>
                                                    <td style="border-bottom: 1px solid #ccc;">Retencion IVA:</td>
                                                    <td style="border-bottom: 1px solid #ccc;" align="center">$</td>
                                                    <td style="border-bottom: 1px solid #ccc;" align="right"><%=FormatNumber(retencion)%></td>
                                                </tr>
                                                <tr>
                                                    <td style="border-bottom: 1px solid #ccc;">TOTAL:</td>
                                                    <td style="border-bottom: 1px solid #ccc;" align="center">$</td>
                                                    <td style="border-bottom: 1px solid #ccc;" align="right"><%=FormatNumber(Recordset1.Fields.Item("total").Value)%></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td><% If (Recordset1.Fields.Item("estatus").Value)= "Pendiente" OR(Recordset1.Fields.Item("estatus").Value)= "NotaCO" Then %>
                                <table width="50%" border="0" align="center">
                                    <tr>
                                        <td><a href="<%=pagina%>">
                                            <img src="imagenes/Arrow-right.png" width="32" height="32" border="0" />Pendiente</a></td>
                                        <td><%If Recordset1.Fields.Item("total").Value <> 0 And Recordset1.Fields.Item("total").Value <> "" Then%>
                                            <a href="comercioExterior.asp?id=<%=Request.QueryString("idfactura")%>&idempresa=<%=Request.QueryString("idempresa")%>&idcliente=<%=cliente%>&pagina=fac">
                                                <img src="imagenes/comext.png" width="32" height="32">Comercio Exterior</a>
                                            <% End If %>
                                        </td>
                                        <td>
                                            <% If tipoDocumento = "Nota de Credito" OR tipoDocumento = "Nota de Crédito" Then %>
                                            <a href="facturaCte.asp?idfactura=<%=Request.QueryString("idfactura")%>&idcliente=<%=Request.QueryString("idcliente")%>&idempresa=<%=Request.QueryString("idempresa")%>&val=si" onclick="return terminar()">
                                            <img src="imagenes/Arrow-left.png" width="32" height="32" border="0" />Terminar</a>
                                            <%else %>
                                            <a href="detfacturaadd.asp?idfactura=<%=Request.QueryString("idfactura")%>&idcliente=<%=Request.QueryString("idcliente")%>&idempresa=<%=Request.QueryString("idempresa")%>&val=si" onclick="return terminar()">
                                            <img src="imagenes/Arrow-left.png" width="32" height="32" border="0" />Terminar</a>
                                            <% End If %>
                                        </td>
                                        <td><a href="cancelarfactura.asp?idfactura=<%=Request.QueryString("idfactura")%>">
                                            <img src="imagenes/cancel.gif" width="32" height="32" border="0" />Cancelar</a></td>
                                        <td><a href="impfactura.asp?idfactura=<%=Request.QueryString("idfactura")%>&amp;idcliente=<%=Request.QueryString("idcliente")%>" target="_blank">
                                            <img src="imagenes/page_accept.png" width="32" height="32" border="0" />Vista Previa</a></td>
                                    </tr>
                                </table>
                                <% End If %></td>
                        </tr>
                    </table>
                    <p>&nbsp;</p>
                    <p>&nbsp;</p>
                    <!-- InstanceEndEditable -->
                    <p>&nbsp;</p>
                    <%=footerPage%>
          </div>  </td>
        </tr>
    </table>
    <script>
        function terminar(){
            var message = "\n";
            var campo = document.getElementById("total2").value;
            if(campo <= 0){
                message = message + "No se han ingresado articulos.\n\n";
                alert (message)
	            return false;
            }else{
                return true;
            }
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
<%
Recordset6.Close()
Set Recordset6 = Nothing
%>
<%
Recordset7.Close()
Set Recordset7 = Nothing
%>
<script language="javascript1.2">
var descuentoglobal = "";
var totaloglobal;

if ('<%=Request.QueryString("ter")%>' != "")
{
    document.form1.MM_insert.value="0000"
    document.form1.action="detfacturaadd.asp?idfactura="+'<%=Request.QueryString("idfactura")%>'+"&idempresa="+'<%=Request.QueryString("idempresa")%>'+"&idcliente="+'<%=Request.QueryString("idcliente")%>'+"&pagina="+'<%=Request.QueryString("pagina")%>'	
    document.form1.submit()
}

//funcion para calcular el total del articulo
function tot()
{
    document.form1.total.value = document.form1.cantidad.value * document.form1.precio_unitario.value
    document.form1.importe.value = document.form1.cantidad.value * document.form1.precio_unitario.value
    imp()//llamar ala funcion descuento
}
//funcion para redondear a dos digitos en javascript
function redondear2decimales(numero)
{
    var original=parseFloat(numero);
    var result=Math.round(original*100)/100 ;
    return result;
}
//funcion para hacer descuento
function desc()
{
    imp();
    var descuento;//variable ara guardar el descuento
    if (document.form1.descuento.value != "" && document.form1.descuento.value != 0)
    {
        descuentoglobal = document.form1.descuento.value
        desceunto = totaloglobal * (document.form1.descuento.value/100)
        document.form1.total.value = totaloglobal - desceunto
    }
    else if (descuentoglobal != "")
    {
        desceunto = totaloglobal * (descuentoglobal/100)
        document.form1.total.value = parseFloat(document.form1.total.value) + desceunto
    }
}
//funcion para calcular el impuesto
function imp()
{
    var impuesto = document.form1.total.value * (document.form1.impuesto.value/100)
    var total;
    document.form1.impuesto2.value = impuesto
    impuesto = impuesto + parseFloat(document.form1.total.value)
    //if para validar que el texbox total no este solo
    if (document.form1.total.value != 0 && document.form1.total.value != impuesto)
    {
        document.form1.total.value = impuesto
        totaloglobal = document.form1.total.value
    }
}

//funcion para abrir el popup
function adenda()
{
    alert('<%=Request.Cookies("login")("idSucursal")%>')
    window.open("filtroadenda.asp?idempresa="+'<%=Request.Cookies("login")("idSucursal")%>'+"&idcliente="+<%=Request.QueryString("idcliente")%>,"Adenda","width=508, height=600, scrollbars=yes")
}

function eliminar(iddetFactura,idfactura,idcliente)
{
    var message = "- Presione Aceptar para modificar.\n\n";
    message = message + "- Presione Cancelar para eliminar.\n\n";
    if (confirm(message))
    {
        //document.location="detfacturamod.asp?iddetFactura="+iddetFactura+"&idfactura="+idfactura+"&idcliente="+idcliente+"&pagina="+'<%=Request.QueryString("pagina")%>'
    }
    else
    {
        document.location="detfacturamod.asp?iddetFactura="+iddetFactura+"&idfactura="+idfactura+"&idcliente="+idcliente+"&eliminar=1&pagina="+'<%=Request.QueryString("pagina")%>'	
    }
}

function val(f)
{
    var message = "Falta:.\n\n";
    if (f.cantidad.value == "")
    {
          message = message + "- Cantidad.\n\n";
          alert (message)
	      return false;
    }
    if (f.descripcion.value == "")
    {
          message = message + "- Descripcion.\n\n";
          alert (message)
	      return false;
    }
    if (f.precio_unitario.value == "")
    {
          message = message + "- Precio unitario.\n\n";
          alert (message)
	      return false;
    }
    if (f.unidad.value == "")
    {
          message = message + "- Unidad.\n\n";
          alert (message)
	      return false;
    }
    if (f.impuesto.value == "")
    {
          message = message + "- Impuesto.\n\n";
          alert (message)
	      return false;
    }
    if (f.importe.value == "")
    {
          tot()
	      return true;
    }
    else
    {
        return true
    }
}
</script>


<!--<a href="facturaXml.asp?facturaXml=<%=Request.QueryString("idfactura")%>&idcliente=<%=Request.QueryString("idcliente")%>"><img src="imagenes/Arrow-left.png" width="32" height="32" border="0" />Terminar</a>-->
