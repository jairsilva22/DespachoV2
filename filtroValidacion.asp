<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!--#include file="Connections/Conecta1.asp" -->
<!--#include file="config.asp"-->
<!--#include file="checklogin.asp"-->

<%
    Response.Buffer = true
Server.ScriptTimeout=50000

dim folio'Variable para guardar el folio
dim ven'Vairable para guardar el vendedor
dim vendedor'Variable para guardar el vendedor a filtrar
dim dia'Variable para guardar el mes que se va a filtrar
dim yearrep'Variable para guardar el año que se va a filtrar
dim estatus'Variable para guardar el estatus a filtrar
dim documento'Variable para guardar el tipo de documeto a filtrar
dim fpago'Variable para guardar la forma de pago
dim rfc'Variable para guardar el rfc del cliente
dim nombre'Varaible para guardar el nombre del cliente
dim ordenar'Variable para guardar el orden de la consutla
dim subtotal'Variable apra guardar el total del subtotal
dim ivarep'Variable apra guardar el total del iva
dim total'Variable apra guardar el total del total
dim cliente'Variable para guardar el cliente a filtrar
dim sucursal 'Variable para guardar sucursal a filtrar
dim almacen 'Variable para guardar almacen a filtrar
dim subtot'Variable apra guardar el subtotal
dim ivarep2'Variable apra guardar el iva
dim tot'Variable apra guardar el total
dim submostrador'Variable para guardar el subtotal de las ventas de mostrador
dim ivamostrador'Variable para guardar el iva de las ventas de mostrador
dim totmostrador'Variable para guardar el total de las ventas de mostrador
dim subcontado'Variable para guardar el subtotal de las ventas de contado
dim ivacontado'Variable para guardar el iva de las ventas de contado
dim totcontado'Variable para guardar el total de las ventas de contado
dim subcredito'Variable para guardar el subtotal de las ventas de credito
dim ivacredito'Variable para guardar el iva de las ventas de credito
dim totcredito'Variable para guardar el total de las ventas de credito

dim otrosSub 
dim otrosIva 
dim otrosTotal 
dim Sub01 
dim Iva01 
dim Total01 
dim Sub04 
dim Iva04 
dim Total04 
dim Sub28
dim Iva28
dim Total28
dim Sub03 
dim Iva03 
dim Total03 
dim Sub02 
dim Iva02 
dim Total02 

otrosSub = 0
otrosIva = 0
otrosTotal = 0
Sub01 = 0
Iva01 = 0
Total01 = 0
Sub04 = 0
Iva04 = 0
Total04 = 0
Sub28 = 0
Iva28 = 0
Total28 = 0
Sub03 = 0
Iva03 = 0
Total03 = 0
Sub02 = 0
Iva02 = 0
Total02 = 0
ven = "&nbsp;"
subcontado = 0
ivacontado = 0
totcontado = 0
subcredito = 0
ivacredito = 0
totcredito = 0
submostrador = 0
ivamostrador = 0
totmostrador = 0
subtotal = 0 
ivarep = 0
total = 0



    if Request.Form("orden") <> "" Then
    ordenar = " " & Request.Form("orden")
    orden1 = Request.Form("orden")
    elseif Request.QueryString("orden") <> "" Then
     ordenar = " " & Request.QueryString("orden")
     orden1 = Request.QueryString("orden")
    else
    ordenar = " folio"
    end if
      
'if para validar que se filtra el vendedor
if Request.Form("vendedor") <> "" then'vendedor
vendedor = "AND vendedor = '" & Request.Form("vendedor") & "'"
vendedor1 = Request.Form("vendedor")
ven = "Vendedor: "& Request.Form("vendedor")

elseif Request.QueryString("vendedor") <> "" then
    vendedor = "AND vendedor = '" & Request.QueryString("vendedor") & "'"
    vendedor1 = Request.QueryString("vendedor") 
end if'vendedor

 if Request.Form("sucursal") <> "" then'vendedor
sucursal = "AND factura.idempresa = " & Request.Form("sucursal") 
sucursal1 =  Request.Form("sucursal") 
    elseif Request.QueryString("") <> "" Then 
    sucursal = "AND factura.idempresa = " & Request.QueryString("sucursal") 
    sucursal1 =  Request.QueryString("sucursal") 
end if'vendedor

    if Request.Form("almacen") <> "" then'vendedor
almacen = " AND idAlmacen = " & Request.Form("almacen") 
almacen1 =  Request.Form("almacen") 
    elseif Request.QueryString("almacen") <> "" then
    almacen = " AND idAlmacen = " & Request.QueryString("almacen") 
    almacen1 = Request.QueryString("almacen") 
end if'vendedor
    'response.write(almacen)
'if para validar que se filtra el documento
if Request.Form("cliente") <> "" then'documento
  cliente = " AND idcliente = " & Request.Form("cliente")
    cliente1 = Request.Form("cliente")
    elseif Request.QueryString("cliente") <> "" then'documento
  cliente = " AND idcliente = " & Request.QueryString("cliente")
    cliente1 = Request.QueryString("cliente")
end if'documento

    if Request.Form("nomCliente") <> "" Then 
    nomCliente = Request.Form("nomCliente")
    elseif Request.QueryString("nomCliente") <> "" Then 
    nomCliente = Request.QueryString("nomCliente")
    end if 
'if para validar que se filtra el documento
if Request.Form("fpago") <> "" then'documento
  fpago = " AND forma_pago = " & Request.Form("fpago")
    fpago1 = Request.Form("fpago")
    elseif Request.QueryString("fpago") <> "" then'documento
  fpago = " AND forma_pago = " & Request.QueryString("fpago")
    fpago1 = Request.QueryString("fpago")
end if'documento

    if Request.Form("estatus") <> "" then'documento
    estatus1 = Request.Form("estatus")
        if Request.Form("estatus") = "Pendiente" Then
        estatus = " AND estatusRevision is null"
        else
        estatus = " AND estatusRevision = '" & Request.Form("estatus")&"'"
        end if
    elseif Request.QueryString("estatus") <> "" then'documento
        estatus1 = Request.QueryString("estatus")
        if Request.QueryString("estatus") = "Pendiente" Then
        estatus = " AND estatusRevision is null"
        else
        estatus = " AND estatusRevision = '" & Request.QueryString("estatus")&"'"
        end if
        
    end if'documento
  
'if para validar que se filtra el mes
    diaI = Request.Form("dia")
    diaF = Request.Form("diaF")
if Request.Form("dia") <> "" AND Request.Form("diaF") <> "" then'mes
  dia = " AND CONVERT(varchar, fechasellado, 103) BETWEEN CONVERT(varchar, '" & Request.Form("dia")&"', 103) AND CONVERT(varchar, '" & Request.Form("diaF")&"', 103)"
    elseif Request.QueryString("dia") <> "" AND Request.QueryString("diaF") <> "" then'mes
  dia = " AND CONVERT(varchar, fechasellado, 103) BETWEEN CONVERT(varchar, '" & Request.QueryString("dia")&"', 103) AND CONVERT(varchar, '" & Request.QueryString("diaF")&"', 103)"
    else
     dia = " AND CONVERT(varchar, fechasellado, 103) BETWEEN CONVERT(varchar, '" & DATE() &"', 103) AND CONVERT(varchar, '" & DATE()&"', 103)"
end if'mes

Dim Recordset1
Dim Recordset1_cmd
Dim Recordset1_numRows

Set Recordset1_cmd = Server.CreateObject ("ADODB.Command")
Recordset1_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset1_cmd.CommandText = "SELECT idcliente,  folio, documento.descripcion as documento, subtotal, iva, total , formPago.descripcion as fpago, estatus, CONVERT(varchar,fechasellado,103) AS fechasellado, retencion, idfactura, metodopago, estatusRevision As estatusR, observacionRevision AS observacionR FROM dbo.factura, dbo.formPago, dbo.documento WHERE idfactura > 0 AND abreviatura = 'FA' AND estatus = 'Facturada' AND idpago = factura.forma_pago AND subtotal > 0 and folio is not NULL AND iddocumento = tipo_comprobante AND UUID IS NOT NULL AND timbre = 'NO' " & dia & fpago & cliente & vendedor & sucursal & estatus &" ORDER BY " & ordenar
Recordset1_cmd.Prepared = true
'response.Write(Recordset1_cmd.CommandText)
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

Dim Recordset4
Dim Recordset4_cmd
Dim Recordset4_numRows

Set Recordset4_cmd = Server.CreateObject ("ADODB.Command")
Recordset4_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset4_cmd.CommandText = "SELECT * FROM dbo.clientesFacturacion ORDER BY nombreCliente ASC" 
Recordset4_cmd.Prepared = true

Set Recordset4 = Recordset4_cmd.Execute
Recordset4_numRows = 0
%>
<%
Dim Recordset5
Dim Recordset5_cmd
Dim Recordset5_numRows

Set Recordset5_cmd = Server.CreateObject ("ADODB.Command")
Recordset5_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset5_cmd.CommandText = "SELECT DISTINCT(vendedor) FROM dbo.Factura WHERE vendedor <> '' ORDER BY vendedor ASC" 
Recordset5_cmd.Prepared = true

Set Recordset5 = Recordset5_cmd.Execute
Recordset5_numRows = 0
%>
<%

Dim Recordset6
Dim Recordset6_cmd
Dim Recordset6_numRows

Set Recordset6_cmd = Server.CreateObject ("ADODB.Command")
Recordset6_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset6_cmd.CommandText = "SELECT * FROM dbo.sucursales ORDER BY nombre ASC" 
Recordset6_cmd.Prepared = true

Set Recordset6 = Recordset6_cmd.Execute
Recordset6_numRows = 0
%>
<%

Dim Recordset7
Dim Recordset7_cmd
Dim Recordset7_numRows

'Set Recordset7_cmd = Server.CreateObject ("ADODB.Command")
'Recordset7_cmd.ActiveConnection = MM_Conecta1_STRING
   ' if Request.Form("sucursal") = "1" Then 
   ' Recordset7_cmd.CommandText = "SELECT * FROM dbo.almacen WHERE otraEmpresa = 'NO' OR otraEmpresa is null ORDER BY almacen ASC" 
  '  elseif Request.Form("sucursal") = "2" Then 
  '  Recordset7_cmd.CommandText = "SELECT * FROM dbo.almacen WHERE otraEmpresa = 'SI' ORDER BY almacen ASC" 
   ' else
'Recordset7_cmd.CommandText = "SELECT * FROM dbo.almacen ORDER BY almacen ASC" 
   ' end if
'Recordset7_cmd.Prepared = true

'Set Recordset7 = Recordset7_cmd.Execute
'Recordset7_numRows = 0

    'Response.Write("Id Empresa: "& Request.Form("sucursal") &"<br>")
    'Response.Write("Cliente: "& Request.Form("cliente") &"<br>")
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


    ctabla = "#e33045"
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml"><!-- InstanceBegin template="/Templates/plantillacfd.dwt.asp" codeOutsideHTMLIsLocked="false" -->
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<!-- InstanceBeginEditable name="doctitle" -->
<title><%=titlePage%></title>
<link rel="stylesheet" href="css.css" type="text/css" media="screen"  />
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
     <script type="text/javascript">
        var posicion_x;
        var posicion_y;
        posicion_x = (screen.width / 2) - (500 / 2);
        posicion_y = (screen.height / 2) - (500 / 2);

        function clientes() {
            open("clienteReporte.asp", '', 'top=' + posicion_y + ', left=' + posicion_x + ', width=800, height=650')
        }

         function borrar() {
             document.getElementById("clientes").innerHTML = ""
             document.getElementById("cliente").value = ""
             document.getElementById("nomcliente").value = ""
         }
     </script>

<!-- InstanceEndEditable -->
</head>

<body>
      <!-- InstanceBeginEditable name="EditRegion1" -->
     
 <table width="90%" border="0" cellpadding="0" cellspacing="0" align="center">
     <tr style="padding: 10px">
         <td>
                  <form action="filtroValidacion.asp" method="post" name="form1" id="form1">
        <p>&nbsp;</p>
          Buscar  Fecha Del:
            <% if Request.Form("dia") <> "" then 
                    fecha = Request.Form("dia")
                    elseif Request.QueryString("dia") <> "" then
               fecha = Request.QueryString("dia")
               else
                    fecha = Date()
                    end if
                    %>
              <input type="text" name="dia" id="dia" class="tcal" size="10" value="<%=fecha%>" readonly="readonly" />
          Hasta: 
           <% if Request.Form("diaF") <> "" then 
                    fechaF = Request.Form("diaF")
                    elseif Request.QueryString("diaF") <> "" then
               fechaF = Request.QueryString("diaF")
               else
                    fechaF = Date()
                    end if
                    %>
              <input type="text" name="diaF" id="diaF" class="tcal" size="10" value="<%=fechaF%>" readonly="readonly" />

          Cliente <a href="javascript:clientes()">
                                    <img src="imagenes/filefind.gif" width="20" height="20"></a>
                  <% if Request.Form("cliente") <> "" Then
                                    cliente1 = Request.Form("cliente")
                                    else cliente1 = clienteForm
                                    end if%>
                                <input type="hidden" name="cliente" id="cliente" value="<%=cliente1%>">
                                <input type="hidden" name="nomcliente" id="nomcliente" value="<%=nomcliente%>">
                <% if Request.Form("nomcliente") <> "" then 
                    nombreC = Request.Form("nomcliente") & "<br>"
                    elseif nombre <> "" then
                    nombreC = nombre & "<br>"
                    else
                    nombreC = ""
                    end if
                    %>
                                <div id="clientes" name="clientes" style="display: inline;"><strong><%=nombreC%></strong></div>
          
          Empresa   <select name="sucursal" id="sucursal">
                <option value="">Todos</option>
                <%While (NOT Recordset6.EOF)
                    empresa = Recordset6.Fields.Item("nombre").Value%>

                <option value="<%=(Recordset6.Fields.Item("id").Value)%>" <%If (Not isNull((sucursal1))) Then If (CStr(Recordset6.Fields.Item("id").Value) = CStr((sucursal1))) Then Response.Write("selected=""selected""") : Response.Write("")%>><%=(empresa)%>-><%=(Recordset6.Fields.Item("razon").Value)%></option>
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
         
          Forma de pago:  <select name="fpago" id="fpago">
                <option value="">Todos</option>
                <option value="1" <%If (Not isNull((Request.Form("fpago")))) Then If (CStr("1") = CStr((Request.Form("fpago")))) Then Response.Write("selected=""selected""") : Response.Write("")%>>Contado</option>
                <option value="2" <%If (Not isNull((Request.Form("fpago")))) Then If (CStr("2") = CStr((Request.Form("fpago")))) Then Response.Write("selected=""selected""") : Response.Write("")%>>Crédito</option>
              </select>
          Vendedor: <select name="vendedor" id="vendedor">
                <option value="">Todos</option>
                <%While (NOT Recordset5.EOF)%>
                <option value="<%=(Recordset5.Fields.Item("vendedor").Value)%>" <%If (Not isNull((vendedor1))) Then If (CStr(Recordset5.Fields.Item("vendedor").Value) = CStr((vendedor1))) Then Response.Write("selected=""selected""") : Response.Write("")%> ><%=(Recordset5.Fields.Item("vendedor").Value)%></option>
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
          Estatus:  <select name="estatus" id="estatus">
                <option value="">Todos</option>
                  <option value="Cancelar" <%If (Not isNull((estatus1))) Then If (CStr("Cancelar") = CStr(((estatus1)))) Then Response.Write("selected=""selected""") : Response.Write("")%> >Cancelar</option>
                  <option value="Cheque" <%If (Not isNull((estatus1))) Then If (CStr("Cheque") = CStr(((estatus1)))) Then Response.Write("selected=""selected""") : Response.Write("")%> >Cheque</option>
              <option value="Correcto" <%If (Not isNull((estatus1))) Then If (CStr("Correcto") = CStr((estatus1))) Then Response.Write("selected=""selected""") : Response.Write("")%> >Correcto</option>
              <option value="Crédito" <%If (Not isNull((estatus1))) Then If (CStr("Crédito") = CStr((estatus1))) Then Response.Write("selected=""selected""") : Response.Write("")%> >Crédito</option>
              <option value="Efectivo" <%If (Not isNull((estatus1))) Then If (CStr("Efectivo") = CStr((estatus1))) Then Response.Write("selected=""selected""") : Response.Write("")%> >Efectivo</option>
             
                  <option value="Pendiente" <%If (Not isNull((estatus1))) Then If (CStr("Pendiente") = CStr((estatus1))) Then Response.Write("selected=""selected""") : Response.Write("")%>>Pendiente</option>
               
                  <option value="Revisar" <%If (Not isNull((estatus1))) Then If (CStr("Revisar") = CStr((estatus1))) Then Response.Write("selected=""selected""") : Response.Write("")%>>Revisar</option>
              <option value="Tarjeta" <%If (Not isNull((estatus1))) Then If (CStr("Tarjeta") = CStr((estatus1))) Then Response.Write("selected=""selected""") : Response.Write("")%> >Tarjeta</option>
              <option value="Transferencia" <%If (Not isNull((estatus1))) Then If (CStr("Transferencia") = CStr((estatus1))) Then Response.Write("selected=""selected""") : Response.Write("")%> >Transferencia</option>
              </select>

          Ordenar por: 
          <select name="orden" id="orden">
              <option value="folio" <%If (Not isNull((orden1))) Then If (CStr("Folio") = CStr((orden1))) Then Response.Write("selected=""selected""") : Response.Write("")%>>Folio</option>
              <option value="documento.descripcion" <%If (Not isNull((orden1))) Then If (CStr("documento.descripcion") = CStr((orden1))) Then Response.Write("selected=""selected""") : Response.Write("")%>>Tipo de documento</option>   
              <option value="formPago.descripcion" <%If (Not isNull((orden1))) Then If (CStr("formPago.descripcion") = CStr((orden1))) Then Response.Write("selected=""selected""") : Response.Write("")%> >Forma de pago</option>                         
            </select>
         
           <input type="submit" name="button" id="button" value="Enviar" />
       
        <p>&nbsp;</p>
          <input type="hidden" id="idempresa" name="idempresa" />
         <!-- <input type="text" id="empresa" name="empresa" />-->
      </form>
         </td>
     </tr>
 </table>
      <!--<p>&nbsp;</p>-->
   

        <table align="center" width="100%" border="1" cellpadding="0" cellspacing="0">
      <tr align="center" bgcolor="<%=ctabla%>" class="stylo1" style="color: black; ">
        <td width="4%">Folio</td>
        <td width="5%">R.F.C.</td>
        <td width="40%">Cliente</td>
        <td width="10%" bgcolor="<%=ctabla%>">Tipo documento</td>
        <td width="7%">Subtotal</td>
        <td width="5%">I.V.A.</td>
        <td width="5%">Total</td>
        <td width="5%">Forma de pago</td>
        <td width="10%">Estatus</td>
        <td width="44%">Fecha</td>
        <td width="10%">Observaciones</td>
        <td width="10%">Estatus</td>
        <td width="10%">Observación</td>
        <td width="10%">Modificar</td>
      </tr>
      <% While ((NOT Recordset1.EOF)) %>
      <%
rfc = "&nbsp;"
nombre = "&nbsp;"
cgrid2 = "#ececec"
cgrid1 = "#ffffff"
'if para validar el cambio del color
if color = cgrid2 then'color
color = cgrid1
else'color
color = cgrid2
end if'color


'if para validar que tnga cliente
         ' Response.Write("Id cliente = "&Recordset1.Fields.Item("idcliente").Value&" ")
if Recordset1.Fields.Item("idcliente").Value <> "" then'idcliente
'-----------Recordset para consultar informacion del cliente-------------------------------------
Set RSCleinte_cmd = Server.CreateObject ("ADODB.Command")
RSCleinte_cmd.ActiveConnection = MM_Conecta1_STRING
RSCleinte_cmd.CommandText = "SELECT * FROM dbo.clientes WHERE  idCliente = " & Recordset1.Fields.Item("idcliente").Value
RSCleinte_cmd.Prepared = true

Set RSCleinte = RSCleinte_cmd.Execute

'if para validar que tiene datos
if NOT RSCleinte.EOF then'RSCleinte
rfc = RSCleinte.Fields.Item("rfcCliente").Value
nombre = RSCleinte.Fields.Item("nombreCliente").Value
end if'RSCleinte
end if'idcliente

'----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    total1 = 0
    subtotal1 = 0
    retencion = 0
    totalImpuesto = 0
                  '-------Recordset para redondear los precios-------------------------------
         ' Response.Write("id factura = "&Recordset1.Fields.Item("idfactura").Value)
    Set RSPrecios_cmd = Server.CreateObject ("ADODB.Command")
    RSPrecios_cmd.ActiveConnection = MM_Conecta1_STRING
    RSPrecios_cmd.CommandText = "select precio_unitario, descuento, cantidad, iva FROM dbo.detFactura WHERE id_factura = " & (Recordset1.Fields.Item("idfactura").Value) &almacen
    RSPrecios_cmd.Prepared = true

    Set RSPrecios = RSPrecios_cmd.Execute
    'Response.Write( RSPrecios_cmd.CommandText&"<br>")
          'consulta = NOT RSPrecios.EOF
    'Response.Write( "Consulta "&consulta&"<br>")
          
    while ((NOT RSPrecios.EOF))
        'mostrar el total y el descuento
        if RSPrecios.fields.item("descuento").value > 0 Then
            precioUnitario = truncarAseis(RSPrecios.Fields.Item("precio_unitario").Value * ((100 - RSPrecios.Fields.Item("descuento").Value) / 100))
        Else
            precioUnitario = truncarAseis(RSPrecios.Fields.Item("precio_unitario").Value)
	    End If
   'Response.Write("Precio Unitario = "&RSPrecios.Fields.Item("precio_unitario").Value&" Descuento = "&RSPrecios.fields.item("descuento").value&"<br>")
	    'validamos si se trunca o redondea
	    If RSCleinte.Fields.Item("truncar").Value = "si" Then
		    totalConcepto = truncarAdos(precioUnitario * RSPrecios.Fields.Item("cantidad").Value)
		    subtotal1 = subtotal1 + truncarAdos(totalConcepto)
            'Response.Write("Total concepto 1 = "&totalConcepto&"<br>")
		    If RSPrecios.Fields.Item("iva").Value <> "0" Then
			    impuesto = truncarAdos(totalConcepto * 0.16)
			    totalImpuesto = totalImpuesto + truncarAdos(impuesto)
		    Else
			    impuesto = 0
			    totalImpuesto = 0
		    End If
	    Else
		    totalConcepto = redondear(precioUnitario * RSPrecios.Fields.Item("cantidad").Value)
		    subtotal1 = redondear(CDbl(subtotal1) + CDbl(totalConcepto))
          
            'Response.Write("Total concepto 2 = "&totalConcepto&"<br>")
		    If RSPrecios.Fields.Item("iva").Value <> "0" Then
			    impuesto = redondear(totalConcepto * 0.16)
			    totalImpuesto = totalImpuesto + redondear(impuesto)
		    Else
			    impuesto = 0
			    totalImpuesto = 0
		    End If		
	    End If
          'Response.Write("Truncar = "&RSCleinte.Fields.Item("truncar").Value & "<br>")
          'Response.Write("IVA = "&RSPrecios.Fields.Item("iva").Value & "<br>")
          
      RSPrecios.MoveNext()

    Wend

    If RSCleinte.Fields.Item("truncar").Value = "si" Then
	    total1 = truncarAdos(subtotal1 + totalImpuesto)

	    If RSCleinte.Fields.Item("isr").Value = "si" Then
		    total1 = truncarAdos(subtotal1 - (subtotal1 * 0.15))
	    End If
	    'response.Write "si"
    Else
	    total1 = redondear(subtotal1 + totalImpuesto)

	    If RSCleinte.Fields.Item("isr").Value = "si" Then
		    total1 = redondear(subtotal1 - (subtotal1 * 0.15))
	    End If
	    'response.Write "no"&total
    End If


    'validar si la factura incluye rentencion 
    retCte = Recordset1.fields.item("retencion").value

    'if para valdiar que el usuario tenga retencion
    if retCte > 0  then'site_retencion
        retencion = totalImpuesto
        total1 = subtotal1
    else
        retencion = 0
        total1 = total1
    end if'site_retencion



  '----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

'subtot = digi(Recordset1.Fields.Item("subtotal").Value)
'ivarep2 = digi(Recordset1.Fields.Item("iva").Value)
'tot = digi(Recordset1.Fields.Item("total").Value)
         
 subtot = FormatNumber(subtotal1)
          ivarep2 = FormatNumber(totalImpuesto)
tot = FormatNumber(total1) 

    
 

%>
      <tr bgcolor="<%=color%>" class="stylo2">
           <% if subtot > 0  AND ivarep2 > 0 AND tot > 0 then  %>
                <td><% If Recordset1.Fields.Item("folio").Value <> "" Then 
		        Response.Write(Recordset1.Fields.Item("folio").Value)
		        Else Response.Write("&nbsp;")
		        End If %></td>

                <td><%=rfc%></td>
                <td><%=nombre%></td>
            <% if Recordset1.Fields.Item("documento").Value = "Factura" then
              documento = "FA"
              elseif Recordset1.Fields.Item("documento").Value = "Nota de Credito" OR Recordset1.Fields.Item("documento").Value = "Nota de Crédito" Then
              documento = "NC"
              elseif Recordset1.Fields.Item("documento").Value = "Nota de Cargo" then
              documento = "NCA"
              end if
              %>
                <td align="center"><%=(documento)%></td>
                <td><%=(subtot)%></td>
                <td><%=(ivarep2)%></td>
                <td><%=(tot)%></td>
          <%
              if Recordset1.Fields.Item("fpago").Value = "Pago en Parcialidades o Diferido" Then
              fpago = "PPD"
              else
              fpago = "PUE"
              end if
              %>
                <td align="center"><%=(fpago)%></td>
                <td align="center"><%=(Recordset1.Fields.Item("estatus").Value)%></td>
                <td align="center"><%=(Recordset1.Fields.Item("fechasellado").Value)%></td>
                <td align="center">&nbsp;</td>
           <td><%= Recordset1.Fields.Item("estatusR").Value %></td>
          <td><%= Recordset1.Fields.Item("observacionR").Value %></td>
          <td style="vertical-align: central" align="center"><a href="validacionmod.asp?idfactura=<%= Recordset1.Fields.Item("idfactura").Value%>&orden=<%= orden1%>&estatus=<%= estatus1%>&vendedor=<%= vendedor1%>&sucursal=<%= sucursal1%>&almacen=<%= almacen1%>&fpago=<%= fpago1%>&dia=<%= diaI%>&diaF=<%= diaF%>&cliente=<%= cliente1%>&nomCliente=<%= nomCliente%>"><img src="database_table_(edit)_16x16.gif" width="16" height="16" border="0"></a></td>
            <% end if %>
         
      </tr>
      <% 
  Repeat1__index=Repeat1__index+1
  Repeat1__numRows=Repeat1__numRows-1
  Recordset1.MoveNext()
  response.Flush()
Wend
%>
<%
coniva = 1 + ((iva/100))
submostrador=(submostrador/coniva)
if ivamostrador="" OR ivamostrador=0 THEN
ivamostrador = submostrador * (iva/100)
END IF
totmostrador = submostrador + ivamostrador
%>
    </table>
           <p>&nbsp;</p>

        <!-- <table border="0" align="center">
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
                </table>-->
      <!-- InstanceEndEditable -->
 <p>&nbsp;</p>
    
    
      
</body>
<!-- InstanceEnd --></html>
<script language="javascript1.2">
function cambiar()
{
  var nomcliente =  document.getElementById("clientes").innerHTML
    document.form1.nomcliente.value=nomcliente
document.form1.action="filtroDiario.asp"
document.form1.target=""
document.form1.submit()
}
</script>
<%
Recordset4.Close()
Set Recordset4 = Nothing
Recordset5.Close()
Set Recordset5 = Nothing
%>
<%
     function truncarAdos(valor)
	'if para validar si el subtotal tiene digitos
	if InsTr(valor,".") <> 0 then'valor
		digitos = Mid(valor,InsTr(valor,".")+1)
		'if para validar si tiene un solo digito
		if Len(digitos) <= 1 then'digitos
			valor = valor & "0"
		else'digitos
			valor =  Mid(valor,1,InsTr(valor,".")-1)
			valor = valor & "." & Mid(digitos,1,2)
		end if'digitos
	else'valor
		valor = valor&".00"
	end if'valor
	truncarAdos=valor
End function

function truncarAseis(valor)
	'if para validar si el subtotal tiene digitos
	if InsTr(valor,".") <> 0 then'valor
		digitos = Mid(valor,InsTr(valor,".")+1)
		'if para validar si tiene un solo digito
		if Len(digitos) <= 1 then'digitos
			valor = valor & "0"
		else'digitos
			valor =  Mid(valor,1,InsTr(valor,".")-1)
			valor = valor & "." & Mid(digitos,1,6)
		end if'digitos
	else'valor
		valor = valor&".00"
	end if'valor
	truncarAseis=valor
End function

function redondear(valor)
	'if para validar si el subtotal tiene digitos
	if InsTr(valor,".") <> 0 then'valor
		valor = Round(valor, 2)
	else'valor
		valor = valor&".00"
	end if'valor
	redondear=valor
End function

    %>