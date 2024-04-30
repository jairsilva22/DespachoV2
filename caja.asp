<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!--#include file="Connections/Conecta1.asp" -->
<!--#include file="config.asp"--> 
<!--#include file="checklogin.asp"-->
<!--#include file="stylo2.asp"-->
<%
dim clienteNC'Variable para guardar el cliente de la factura
dim estCredito'Variable para guardar el estatus de la nota de credito
dim saldoCredito'Variable para guardar el saldo del credito
dim documento'Vairable para guardar el tipo de comprobante
dim folio3'Variable para guardar el folio a buscar
dim msj'Variable para guardar el mensaje de error
dim idcc'Variable para guardar el id del pago
dim fecha'Variable para unir la fehca
dim folio2'Variable para guardar el folio
dim monto2'Variable para guardar el monto
dim Tipo'Variable para guardar el tipo de pago
dim cantidad_recibida'Variable para guardar la cantidad recibida
dim refecencia'Variable para guardar la referencia
dim cambio'Variable para guardar el cambio
dim observaciones'Variable para guardar las observaciones
dim pagos'Variable para guardar los que se ha pagado
dim estcobranza'Varaible para guardar el estatus de pago
dim folio'Variable para guardar el folio de la factura
dim monto'Variable para guardar el total de la factura
dim saldo'Variable para guardar el saldo de la factura
dim bandera'Variable para validar si no exoste la factura
dim pagado'Variable para guardar cuanto se ha pagado
dim banco'Variable para guardar el banco
dim truncar'Variable para saber si se trunca o no

monto = 0
pagado = 0
saldo = 0
retCte = 0
iva = 0

Dim Recordset1
Dim Recordset1_cmd
Dim Recordset1_numRows

Set Recordset1_cmd = Server.CreateObject ("ADODB.Command")
Recordset1_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset1_cmd.CommandText = "SELECT * FROM dbo.bancos ORDER BY descripcion ASC" 
Recordset1_cmd.Prepared = true

Set Recordset1 = Recordset1_cmd.Execute
Recordset1_numRows = 0

Dim Recordset2
Dim Recordset2_cmd
Dim Recordset2_numRows

Set Recordset2_cmd = Server.CreateObject ("ADODB.Command")
Recordset2_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset2_cmd.CommandText = "SELECT * FROM dbo.tipodepago ORDER BY rango ASC" 
Recordset2_cmd.Prepared = true

Set Recordset2 = Recordset2_cmd.Execute
Recordset2_numRows = 0

Dim Recordset3__MMColParam
Recordset3__MMColParam = "000000000"
If (Request.Form("folio") <> "") Then 
  Recordset3__MMColParam = Request.Form("folio")
End If

Dim Recordset3
Dim Recordset3_cmd
Dim Recordset3_numRows

Set Recordset3_cmd = Server.CreateObject ("ADODB.Command")
Recordset3_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset3_cmd.CommandText = "SELECT tasa, retencion, idcliente, folio, estcobranza, idfactura, tipo_comprobante, estatus FROM dbo.factura WHERE folio = ?" 
Recordset3_cmd.Prepared = true
Recordset3_cmd.Parameters.Append Recordset3_cmd.CreateParameter("param1", 5, 1, -1, Recordset3__MMColParam) ' adDouble

Set Recordset3 = Recordset3_cmd.Execute
Recordset3_numRows = 0

'/////////////para buscar si el cliente trunca o redondea
If Not Recordset3.EOF Then

  retCte = Recordset3.Fields.Item("retencion").Value
  iva = Recordset3.Fields.Item("tasa").Value

  Dim Recordset0
  Dim Recordset0_cmd
  Dim Recordset0_numRows

  Set Recordset0_cmd = Server.CreateObject ("ADODB.Command")
  Recordset0_cmd.ActiveConnection = MM_Conecta1_STRING
  Recordset0_cmd.CommandText = "SELECT truncar, isr FROM dbo.Clientes WHERE idCliente = "&Recordset3.Fields.Item("idcliente").Value
  Recordset0_cmd.Prepared = true
  Set Recordset0 = Recordset0_cmd.Execute

  If Not Recordset0.EOF Then
    
    truncar = Recordset0.Fields.Item("truncar").Value
    hayisr = Recordset0.Fields.Item("isr").Value

    '//////calcular total////'
    Dim recordsetTotal
    Dim recordsetTotal_cmd
    Dim recordsetTotal_numRows

    Set recordsetTotal_cmd = Server.CreateObject ("ADODB.Command")
    recordsetTotal_cmd.ActiveConnection = MM_Conecta1_STRING
    recordsetTotal_cmd.CommandText = "SELECT * FROM dbo.detfactura WHERE id_factura = '"&(Recordset3.Fields.Item("idfactura").Value)&"'"

    recordsetTotal_cmd.Prepared = true

    Set recordsetTotal = recordsetTotal_cmd.Execute
    recordsetTotal_numRows = 0

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
      precioUnitario = (recordsetTotal.Fields.item("precio_Unitario").Value)
                
      'if para validar si tiene descuento
      If recordsetTotal.Fields("descuento").Value <> 0 Then
        des = (recordsetTotal.Fields("descuento").Value)
      Else
        des = 0
      End If

      If truncar = "si" Then
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
        
        If retCte > 0 Then
          'Console.Write(totalConcepto)
          retencion = redondear(totalConcepto * (0.16))
          totalImpuestosretenidos = totalImpuestosretenidos +redondear(retencion)
        Else
          totalImpuestosretenidos = 0
        End If

        subTotal = redondear(subTotal)
      End if

      'limpiamos las variables de los conceptos
      impuesto = 0
      totalConcepto = 0
      precioUnitario = 0

      Dim total
      total = 0
      Dim isr
      'obtenemos el total
      If truncar = "si" Then
        total = truncarAdos(totalImpuestosTrasladados + subTotal)
        'totalImpuestosTrasladados = 0
      Else
        total = redondear(totalImpuestosTrasladados + subTotal)
        'totalImpuestosTrasladados = 0
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

    If retCte > 0 Then
      total = subTotal
    Else
      total = (total)
    End If
  End If
End If

If (Recordset3.CursorType > 0) Then
  Recordset3.MoveFirst
Else
  Recordset3.Requery
End If
'//////////////////////////////////////////////////////

Dim Recordset4__MMColParam
Recordset4__MMColParam = "1"
If (Request.Cookies("login")("id") <> "") Then 
  Recordset4__MMColParam = Request.Cookies("login")("id")
End If
%>
<%
Dim Recordset4
Dim Recordset4_cmd
Dim Recordset4_numRows

Set Recordset4_cmd = Server.CreateObject ("ADODB.Command")
Recordset4_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset4_cmd.CommandText = "SELECT * FROM dbo.logCXC, tipodepago WHERE idtipo = tipo_pago AND logCXC.idusuario = ? ORDER BY idcc DESC" 
Recordset4_cmd.Prepared = true
Recordset4_cmd.Parameters.Append Recordset4_cmd.CreateParameter("param1", 5, 1, -1, Recordset4__MMColParam) ' adDouble

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
  pagos = CDbl(Request.Form("pagos")) + CDbl(Request.Form("total"))
  
  'if para validar el estatus de cobranza
  if CDbl(pagos) >= CDbl(Request.Form("monto")) then'monto
  estcobranza = "Pagada"
  else'monto
  estcobranza = "Pagada parcial"
  end if'monto

'if para validar que se paga con NOTA DE CREDITO
if Request.Form("tipoPagotexto") = "NOTA DE CREDITO" or  Request.Form("tipoPagotexto") = "Nota de Cargo"  then'NOTA DE CREDITO
    '------------Recordset para consultar la nota de credito------------------------------------  
      Set RSNCredito_cmd = Server.CreateObject ("ADODB.Command")
    RSNCredito_cmd.ActiveConnection = MM_Conecta1_STRING
    RSNCredito_cmd.CommandText = "SELECT saldo, total FROM dbo.factura WHERE folio = ?" 
    RSNCredito_cmd.Prepared = true
    RSNCredito_cmd.Parameters.Append RSNCredito_cmd.CreateParameter("param3", 5, 1, -1, MM_IIF(Request.Form("refecencia"), Request.Form("refecencia"), null)) ' adDouble

    Set RSNCredito = RSNCredito_cmd.Execute 

    saldoCredito = RSNCredito.Fields.Item("saldo").Value + Request.Form("total")
    response.write(SaldoCredito)
    'saldoCredito = RSNCredito.Fields.Item("saldo").Value - Request.Form("total")
      'if para validar el estatus de cobranza
      if saldoCredito >= CDbl(RSNCredito.Fields.Item("total").Value) then'monto
      estCredito = "Cobrada"
      else'monto
      estCredito = "Cobrada parcial"
      end if'monto

    if( Request.Form("tipoPagotexto") = "NOTA DE CREDITO" ) then
        comprob = 2 
    else
        comprob = 3
    end if

    '------------Record set para actualizar el estatus de cobranza de la nota de credito------------------------------------  
      Set RSNotaCredito_cmd = Server.CreateObject ("ADODB.Command")
    RSNotaCredito_cmd.ActiveConnection = MM_Conecta1_STRING
    RSNotaCredito_cmd.CommandText = "UPDATE dbo.factura SET estcobranza = '"&estCredito&"', fechaCobrada = ?, facturaCobrada = ?, saldo = ? WHERE folio = ? AND  tipo_comprobante = " & comprob  
    RSNotaCredito_cmd.Prepared = true
    RSNotaCredito_cmd.Parameters.Append RSNotaCredito_cmd.CreateParameter("param1", 135, 1, -1, MM_IIF(Date(), Date(), null)) ' adDouble
    RSNotaCredito_cmd.Parameters.Append RSNotaCredito_cmd.CreateParameter("param2", 5, 1, -1, MM_IIF(Request.Form("folio"), Request.Form("folio"), null)) ' adDouble
    RSNotaCredito_cmd.Parameters.Append RSNotaCredito_cmd.CreateParameter("param3", 5, 1, -1, MM_IIF(saldoCredito, saldoCredito, null)) ' adDouble
    RSNotaCredito_cmd.Parameters.Append RSNotaCredito_cmd.CreateParameter("param4", 5, 1, -1, MM_IIF(Request.Form("refecencia"), Request.Form("refecencia"), null)) ' adDouble

    Set RSNotaCredito = RSNotaCredito_cmd.Execute   

end if'NOTA DE CREDITO

'------------Record set para actualizar el estatus de cobranza------------------------------------  
  Set RSFactura_cmd = Server.CreateObject ("ADODB.Command")
RSFactura_cmd.ActiveConnection = MM_Conecta1_STRING
RSFactura_cmd.CommandText = "UPDATE dbo.factura SET estcobranza = ? WHERE folio = ? AND  tipo_comprobante = 1 " 
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
	estatus="Pagado"

    Set MM_editCmd = Server.CreateObject ("ADODB.Command")
    MM_editCmd.ActiveConnection = MM_Conecta1_STRING
    MM_editCmd.CommandText = "INSERT INTO dbo.logCXC (folio, tipo_pago, banco, refecencia, cantidad_recibida, monto, cambio, saldo, observaciones, total, fecha_captura, anterior, idusuario, ntarjeta, fechaalta, tipo, estatus) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)" 
    MM_editCmd.Prepared = true
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param1", 5, 1, -1, MM_IIF(Request.Form("folio"), Request.Form("folio"), null)) ' adDouble
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param2", 5, 1, -1, MM_IIF(Request.Form("tipo_pago"), Request.Form("tipo_pago"), null)) ' adDouble
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param3", 5, 1, -1, MM_IIF(Request.Form("banco"), Request.Form("banco"), null)) ' adDouble
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param4", 201, 1, 255, Request.Form("refecencia")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param5", 5, 1, -1, MM_IIF(Request.Form("cantidad_recibida"), Request.Form("cantidad_recibida"), null)) ' adDouble
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param6", 5, 1, -1, MM_IIF(Request.Form("monto"), Request.Form("monto"), null)) ' adDouble
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param7", 5, 1, -1, MM_IIF(digi(Request.Form("cambio")), digi(Request.Form("cambio")), null)) ' adDouble
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param8", 5, 1, -1, MM_IIF(Request.Form("saldo"), Request.Form("saldo"), null)) ' adDouble
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param9", 201, 1, 8000, Request.Form("observaciones")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param10", 5, 1, -1, MM_IIF(Request.Form("total"), Request.Form("total"), null)) ' adDouble
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param11", 135, 1, -1, MM_IIF(fecha, fecha, null)) ' adDBTimeStamp
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param12", 201, 1, 255, MM_IIF(Request.Form("anterior"),Request.Form("anterior"), NULL)) ' adLongVarChar	
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param13", 5, 1, -1, MM_IIF(Request.Form("idusuario"), Request.Form("idusuario"), null)) ' adDouble	
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param14", 201, 1, 255, MM_IIF(Request.Form("ntarjeta"), Request.Form("ntarjeta"), null)) ' adDouble		
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param15", 135, 1, -1, MM_IIF(Request.Form("fechalata"), Request.Form("fechalata"), null)) ' adDBTimeStamp	
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param16", 201, 1, 8000, Request.Form("tipo")) ' adLongVarChar	
	MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param17", 201, 1, 8000, estatus)' adLongVarChar	
	MM_editCmd.Execute
    MM_editCmd.ActiveConnection.Close

    ' append the query string to the redirect URL
    Dim MM_editRedirectUrl
    MM_editRedirectUrl = "caja.asp?cargar=si"
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
'if para validar que hay pagos
'if para validar que existe
if NOT Recordset4.EOF then'Recordset4
idcc = Recordset4.Fields.Item("idcc").Value
folio2 = Recordset4.Fields.Item("folio").Value
monto2 =(Recordset4.Fields.Item("monto").Value)
Tipo =(Recordset4.Fields.Item("Tipo").Value)
cantidad_recibida =(Recordset4.Fields.Item("cantidad_recibida").Value)
refecencia =(Recordset4.Fields.Item("refecencia").Value)
cambio =(Recordset4.Fields.Item("cambio").Value)
observaciones ="No de tarjeta: "&(Recordset4.Fields.Item("ntarjeta").Value)&"<br /> Observaciones: "&(Recordset4.Fields.Item("observaciones").Value)
end if'Recordset4

'if para validar que existe
if NOT Recordset3.EOF then'Recordset3

'-----------Recordset para buscar el tipo de comprobante--------------------------------------
Set RSDocumento_cmd = Server.CreateObject ("ADODB.Command")
RSDocumento_cmd.ActiveConnection = MM_Conecta1_STRING
RSDocumento_cmd.CommandText = "SELECT * FROM dbo.documento WHERE iddocumento = " & Recordset3.Fields.Item("tipo_comprobante").Value
RSDocumento_cmd.Prepared = true

Set RSDocumento = RSDocumento_cmd.Execute

'if para valida que no hay datos
if NOT RSDocumento.EOF then'RSDocumento
documento = RSDocumento.Fields.Item("descripcion").Value
end if'RSDocumento

'if para validar que no sea una nota de credito
if documento <> "Nota de Credito" then'documento
'if para validar que el estatus sea facturada
if Recordset3.Fields.Item("estatus").Value = "Facturada" then'Facturada 
'if para validar que si ya esta pagada'
if (Recordset3.Fields.Item("estcobranza").Value = "Pendiente" OR Recordset3.Fields.Item("estcobranza").Value = "Pagada parcial") then'pagada
if Recordset3.Fields.Item("estcobranza").Value = "Pagada parcial" then
bandera = "no"
msj = "La factura "&Request.Form("folio")&" esta parcialmente pagada"
else
bandera = "si"
end if

clienteNC = (Recordset3.Fields.Item("idcliente").Value)
folio3 = Request.Form("folio")
folio = (Recordset3.Fields.Item("folio").Value)
monto = Round(total, 2)

'-----------------Recordset para consultar cuanto ha pagado---------------------------------
Set RSMonto_cmd = Server.CreateObject ("ADODB.Command")
RSMonto_cmd.ActiveConnection = MM_Conecta1_STRING
RSMonto_cmd.CommandText = "SELECT SUM(total) as pagado FROM dbo.logCXC INNER JOIN tipodepago ON idtipo = tipo_pago WHERE estatus <> 'Cancelado' AND folio =" & folio & " AND (tipodepago.tipo <> 'Nota de Cargo')"
'response.Write(RSMonto_cmd.CommandText)
RSMonto_cmd.Prepared = true

Set RSMonto = RSMonto_cmd.Execute

'----------------------consultar si existe recargo
Set RSMontoCargo_cmd = Server.CreateObject ("ADODB.Command")
RSMontoCargo_cmd.ActiveConnection = MM_Conecta1_STRING
RSMontoCargo_cmd.CommandText = "SELECT isnull(SUM(total),0) as cargo FROM dbo.logCXC INNER JOIN tipodepago ON idtipo = tipo_pago WHERE estatus <> 'Cancelado' AND folio =" & folio & " AND (tipodepago.tipo = 'Nota de Cargo')"
'response.Write(RSMontoCargo_cmd.CommandText)
RSMontoCargo_cmd.Prepared = true

Set RSMontoCargo = RSMontoCargo_cmd.Execute
    NCargo = RSMontoCargo.fields.item("cargo").value

'if para validar que hay pagos
if RSMonto.Fields.Item("pagado").Value <> "" then'pagado
saldo =  RSMonto.Fields.Item("pagado").Value
pagado = RSMonto.Fields.Item("pagado").Value
end if'pagado

saldo = Round(monto - saldo,2)

    saldo = Round(saldo + NCargo,2)
else
bandera = "no"
folio3 = ""
msj = "La factura "&Request.Form("folio")&" ya fue pagada"
end if'pagada
else'Facturada
bandera = "no"
folio3 = ""
msj = "El comprobante no esta facturado"
end if'Facturada
else'documento
bandera = "no"
folio3 = ""
msj = "El folio " & Request.Form("folio") & " pertenece a una Nota de Credito"
end if'documento
elseif Request.Form("folio") <> "" then'Recordset3
bandera = "no"
msj = "El folio "&Request.Form("folio")&" no  existe"
folio3 = ""
end if'Recordset3

%>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
        <link href="css/main.css" rel="stylesheet" media="screen" />
<!--<link rel="stylesheet" href="css.css" type="text/css" media="screen"  />-->
     <!-- bootstrap CSS -->
   

  

  
<style type="text/css">
<!--
.styloencabezado {
	font-size: 18px;
	font-weight: bold;
}
-->
</style>
   
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Documento sin título</title>
</head>

<body style="background-color: white">
<table width="200" border="0" align="center">
  <tr align="center">
    <td>Caja</td>
  </tr>
</table>
<% If bandera = "no" Then %>
<table width="256" border="0" align="center">
  <tr align="center" class="styloencabezado">
    <td><%=msj%></td>
  </tr>
</table>
<% End If %>
<form action="<%=MM_editAction%>" method="POST" name="form1" id="form1" >
  <table align="center">
    <tr valign="baseline">
      <td nowrap="nowrap" align="right">Folio:</td>
      <td><input name="folio" id="folio" type="text" value="<%=folio3%>" size="32" class="focusNext" tabindex="1" onkeyup="saltar(event, 'dia')" /></td>
    </tr>
    <tr valign="baseline">
      <td nowrap="nowrap" align="right">Fecha de pago:</td>
      <td><label>
        <input name="dia" type="text" id="dia" value="<%=Day(Date())%>" size="5" tabindex="2"  class="focusNext" onkeyup="saltar(event, 'mes')"/>
        /
        <input name="mes" type="text" id="mes" value="<%=Month(Date())%>" size="5" tabindex="3" onkeyup="saltar(event,'yearcxc')" />
        /
        <input name="yearcxc" type="text" id="yearcxc" value="<%=year(Date())%>" size="5" tabindex="4" onkeypress="saltar(event,'tipo_pago')" />
      </label></td>
    </tr>
    <tr valign="baseline">
      <td nowrap="nowrap" align="right">Factura en papel:</td>
      <td><label>
        <input name="anterior" type="checkbox" id="anterior" value="si" tabindex="5"/>
      </label></td>
    </tr>
    <tr valign="baseline">
      <td nowrap="nowrap" align="right">Tipo pago:</td>
      <td><select name="tipo_pago" id="tipo_pago" onChange="desactivar(this)" tabindex="6" onkeyup="saltar(event, 'banco')">
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
      <td nowrap="nowrap" align="right">Banco:</td>
      <td><select name="banco" id="banco" onkeyup="saltar(event,'referencia')">
        <option value="" tabindex="7">Seleccionar</option>
        <%
While (NOT Recordset1.EOF)
%>
        <option value="<%=(Recordset1.Fields.Item("idbanco").Value)%>"><%=(Recordset1.Fields.Item("descripcion").Value)%></option>
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
      <td nowrap="nowrap" align="right">Referencia:</td>
      <td><input name="refecencia" id="referencia" type="text" value="" size="32" onkeyup="saltar(event,'cantidad_recibida')"/></td>
    </tr>
    <tr valign="baseline" bgcolor="#CCCCCC">
      <td align="right" nowrap="nowrap">Monto:</td>
      <td><input name="monto" id="monto" type="text" value="<%=monto%>" size="32" readonly="readonly" /></td>
    </tr>
    <tr valign="baseline" bgcolor="#CBDFF5" class="stylo49">
      <td align="right" nowrap="nowrap">&nbsp;</td>
      <td>&nbsp;</td>
    </tr>
    <tr valign="baseline" bgcolor="#CBDFF5">
      <td align="right" nowrap="nowrap">Cantidad recibida:</td>
      <td><input type="text" name="cantidad_recibida" id="cantidad_recibida" value="" size="32" onChange="valor()" onkeyup="saltar(event,'ntarjeta')"/></td>
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
      <td align="right" nowrap="nowrap">Numero de tarjeta:</td>
      <td><label>
        <input name="ntarjeta" type="text" id="ntarjeta" size="32" onkeyup="saltar(event,'observaciones')" />
      </label></td>
    </tr>
    <tr valign="top">
      <td align="right" nowrap="nowrap">Observaciones:</td>
      <td><label>
        <textarea name="observaciones" id="observaciones" cols="28" rows="5" onkeyup="saltar(event,'observaciones')"></textarea>
      </label></td>
    </tr>
    <tr valign="baseline">
      <td nowrap="nowrap" align="right">&nbsp;</td>
      <td><input type="button" value="Guardar" onclick="sub(this)"  />
          <!--<input type="submit" style="display:block" id="submit" value="Guardar"  />-->
      </td>
      
     
    </tr>
  </table>
  <input type="hidden" name="MM_insert" id="MM_insert" value="form1" />
  <input type="hidden" name="total" id="total" />
  <label>
    <input name="pagos" type="hidden" id="pagos" value="<%=pagado%>" />
  </label>
  <input name="idusuario" type="hidden" id="idusuario" value="<%=Request.Cookies("login")("id")%>" />
  <input name="fechalata" type="hidden" id="fechalata" value="<%=Date()&" "&FormatDateTime(now, 4)%>" />
  <input name="tipo" type="hidden" id="tipo" value="caja" />
  <input type="hidden" name="tipoPagotexto" id="tipoPagotexto" />
  <input name="idcliente" type="hidden" id="idcliente" value="<%=clienteNC%>" />
</form>
<table width="363" border="0" align="center">
  <tr>
    <td colspan="4" align="center" class="stylo1"><table width="100%" border="0">
        <tr>
          <td class="stylo1">Ultimo pago registrado</td>
          <td><% If idcc <> "" Then %><a href="cajamod.asp?idcc=<%=idcc%>"><img src="imagenes/database_table_(edit)_16x16.gif" width="16" height="16" border="0" /></a><% End If %></td>
        </tr>
    </table></td>
  </tr>
  <tr>
    <td width="100" align="right" class="stylo1">Folio:</td>
    <td width="88" class="stylo2"><%=folio2%></td>
    <td width="70" align="right" class="stylo1">Monto:</td>
    <td width="87" class="stylo2"><%=monto2%></td>
  </tr>
  <tr>
    <td align="right" class="stylo1">Tipo pago:</td>
    <td class="stylo2"><%=(Tipo)%></td>
    <td align="right" class="stylo1">Recibido:</td>
    <td class="stylo2"><%=cantidad_recibida%></td>
  </tr>
  <tr>
    <td align="right" class="stylo1">Referencia:</td>
    <td class="stylo2"><%=refecencia%></td>
    <td align="right" class="stylo1">Cambio:</td>
    <td class="stylo2"><%=cambio%></td>
  </tr>
  <tr valign="top">
    <td align="right" class="stylo1"><p>Observaciones:</p></td>
    <td colspan="3" class="stylo2"><%=observaciones%></td>
  </tr>
</table>
    
</body>
</html><%
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
<!--<script language="JavaScript">
    document.onkeydown = checkKeycode//llamar la fucnion checkKeycode(eliminar tecla suprimir)
    //fucnion para eliminaro con el tecla suprimir
    function checkKeycode(e) {
        var keycode;//variable para guardar la tecla
        //if para valdiar si se presiono una tecla
        if (window.event) keycode = window.event.keyCode;
        else if (e) keycode = e.which;
        //if para valdiar el enter
        if (keycode == 13) {
            document.form1.MM_insert.value = "00000000"
            document.form1.submit();
        }
    }
</script>-->
<script>
    function saltar(e,id)
    {
    document.getElementById("MM_insert").value = "000000000";
	    // Obtenemos la tecla pulsada
	    (e.keyCode)?k=e.keyCode:k=e.which;
	    // Si la tecla pulsada es enter (codigo ascii 13)
    
	    if(k==13)
	    {
		    // Si la variable id contiene "submit" enviamos el formulario
		    if(id=="submit")
		    {
			    //document.forms[0].submit();
                document.getElementById("MM_insert").value = "000000000";
		    }else{
			    // nos posicionamos en el siguiente input
			   
			    document.getElementById(id).focus();
		    }
          
	    }
    }
    function sub(){
       var f = document.getElementById("form1").this;
       var re =  val(document.getElementById("form1"));
       
        if(re){
            document.getElementById("form1").onSubmit = "return true";
            document.form1.submit();
        }

    }
</script>
<script language="javascript1.2">
 

//if para recargar la pagian principal
if ('<%=Request.QueryString("cargar")%>' == "si")//cargar
{
	parent.location.reload()
}//cargar
//Funcion para avisar que no tiene notas de credito
function SinNC()
{
	alert("El cliente no cuenta con notas de credito")	
	document.form1.tipo_pago[0].selected = true
    document.form1.tipoPagotexto.value=""
}
function SinNotasCargo()
{
	alert("El cliente no cuenta con notas de cargo")	
	document.form1.tipo_pago[0].selected = true
    document.form1.tipoPagotexto.value=""
}
///////////////////////////////////////////////
//funcion para desactivar la referencia
function desactivar(menu)
{
	//if para validar que se paga con nota de credito
	if (menu.options[menu.selectedIndex].text == "NOTA DE CREDITO")//NOTA DE CREDITO
	{
		document.form1.refecencia.readOnly=true
		document.form1.tipoPagotexto.value="NOTA DE CREDITO"
		window.open("NotaCredito.asp?idcliente="+document.form1.idcliente.value,"displayWindow",'width=' + 600 + ',height=' + 700 + ',resizable=no,scrollbars=yes,menubar=no,status=no' )
	}//NOTA DE CREDITO
    else if (menu.options[menu.selectedIndex].text == "Nota de Cargo"){
        //alert("seleccioamp nota de cargo");
        document.form1.refecencia.readOnly=true
		document.form1.tipoPagotexto.value="Nota de Cargo"
		window.open("NotaCargo.asp?idcliente="+document.form1.idcliente.value,"displayWindow",'width=' + 600 + ',height=' + 700 + ',resizable=no,scrollbars=yes,menubar=no,status=no' )

    }
	else //NOTA DE CREDITO
	{
		//alert("ATENCION ESTA CAMBIANDO FORMA DE PAGO")
		document.form1.refecencia.value = ""
		document.form1.observaciones.value = ""
		document.form1.cantidad_recibida.value = ""
		document.form1.cambio.value = ""
		document.form1.tipoPagotexto.value=""
		document.form1.cambio.readOnly=false
		document.form1.refecencia.readOnly=false

    if(menu.options[menu.selectedIndex].text == "EFECTIVO")
    {
      document.form1.banco.value = ""
    }
	}//NOTA DE CREDITO
}
    function nextFocus(inputF, inputS) {
  document.getElementById(inputF).addEventListener('keydown', function(event) {
    if (event.keyCode == 13) {
      document.getElementById(inputS).focus();
    }
  });
}

//funcion para validar el formulario
function val(f)
{
	var message = "Falta:.\n\n";
	if (f.tipo_pago.options[f.tipo_pago.selectedIndex].text == "NOTA DE CREDITO")
	{  //alert(f.monto.value)
	  if (parseFloat(f.cantidad_recibida.value) > parseFloat(f.monto.value))
	  {
			  
		   alert("EL MONTO A PAGAR CON UNA NOTA DE CREDITO NO PUEDE SER MAYOR AL MONTO DE LA FACTURA")
		   return false;
	  }
  }

  if(f.tipo_pago.options[f.tipo_pago.selectedIndex].text == "TARJETA CREDITO" || f.tipo_pago.options[f.tipo_pago.selectedIndex].text == "TARJETA DEBITO" || f.tipo_pago.options[f.tipo_pago.selectedIndex].text == "TRANSFERENCIA BANCARIA" || f.tipo_pago.options[f.tipo_pago.selectedIndex].text == "TARJETA" )
  {
    if(f.banco.value == "")
    {
      alert("FALTA INDICAR EL BANCO")
      return false;
    }
  }

    if(f.cantidad_recibida.value == "")
    {
      alert("FALTA INDICAR LA CANTIDAD RECIBIDA")
      return false;
    }

    if(f.cantidad_recibida.value == 0 || f.cantidad_recibida.value < 0)
    {
    alert("LA CANTIDAD RECIBIDA DEBE SER MAYOR A CERO")
      return false;
    }

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
        document.getElementById("MM_insert").value = "form1";
		return true;
	}
}
/////////////////////////////////////////////////////////
//funcion para calcular los montos
function valor()
{
  var saldo
  saldo = document.form1.cantidad_recibida.value - document.form1.saldo.value   
  if (saldo >=0)
  {
    document.form1.total.value = document.form1.cantidad_recibida.value - saldo
    document.form1.cambio.value = redondear2decimales(saldo)
    
  }
  else
  {
    document.form1.total.value = document.form1.cantidad_recibida.value
    document.form1.cambio.value = 0
  }
  
}

/////////////////////////////////////////////////////////////////
//funcion para redondear a dos digitos en javascript
function redondear2decimales(numero)
{
  var original=parseFloat(numero);
  var result=Math.round(original*100)/100 ;
  return result;
}

////////////////////////////////////////////////////////////
</script>

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
%>