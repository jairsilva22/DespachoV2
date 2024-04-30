<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!--#include file="Connections/Conecta1.asp" -->
<!--#include file="config.asp"-->
<!--#include file="checklogin.asp"-->
<!--#include file="stylo2.asp"-->
<%
Dim Recordset1
Dim Recordset1_cmd
Dim Recordset1_numRows

Set Recordset1_cmd = Server.CreateObject ("ADODB.Command")
Recordset1_cmd.ActiveConnection = MM_Conecta1_STRING
'Recordset1_cmd.CommandText = "SELECT idfactura, nombreCliente, rfcCliente, folio, forma_pago, fechasellado FROM dbo.factura JOIN Clientes ON Clientes.idcliente = factura.idcliente WHERE idfactura > 0 AND vendedor = '"&Request.Form("vendedor")&"' AND MONTH(fechasellado) = "&Request.Form("mes")&" AND YEAR(fechasellado) = "&Request.Form("yearemiso")&" AND estatus = 'Facturada' AND abreviatura = 'FA' ORDER BY Folio DESC"
Recordset1_cmd.CommandText = "SELECT idfactura, nombreCliente, rfcCliente, folio, forma_pago, fechasellado FROM dbo.factura JOIN clientesFacturacion ON clientesFacturacion.idcliente = factura.idcliente WHERE idfactura > 0 AND MONTH(fechasellado) = "&Request.Form("mes")&" AND estatus = 'Facturada' AND abreviatura = 'FA' ORDER BY Folio DESC"
Recordset1_cmd.Prepared = true
'Response.Write Recordset1_cmd.CommandText
Set Recordset1 = Recordset1_cmd.Execute
Recordset1_numRows = 0

Dim Recordset2
Dim Recordset2_cmd
Dim Recordset2_numRows

Set Recordset2_cmd = Server.CreateObject ("ADODB.Command")
Recordset2_cmd.ActiveConnection = MM_Conecta1_STRING
'Recordset2_cmd.CommandText = "SELECT idfactura, nombreCliente, rfcCliente, folio, forma_pago, fechasellado FROM dbo.factura JOIN Clientes ON Clientes.idcliente = factura.idcliente WHERE idfactura > 0 AND vendedor = '"&Request.Form("vendedor")&"' AND MONTH(fechasellado) = "&Request.Form("mes")&" AND YEAR(fechasellado) = "&Request.Form("yearemiso")&" AND estatus = 'Facturada' AND abreviatura = 'CRED' ORDER BY Folio DESC"
Recordset2_cmd.CommandText = "SELECT idfactura, nombreCliente, rfcCliente, folio, forma_pago, fechasellado FROM dbo.factura JOIN clientesFacturacion ON clientesFacturacion.idcliente = factura.idcliente WHERE idfactura > 0 AND MONTH(fechasellado) = "&Request.Form("mes")&" AND estatus = 'Facturada' AND abreviatura = 'CRED' ORDER BY Folio DESC"
Recordset2_cmd.Prepared = true
'Response.Write Recordset2_cmd.CommandText
Set Recordset2 = Recordset2_cmd.Execute
Recordset2_numRows = 0

Dim Recordset3
Dim Recordset3_cmd
Dim Recordset3_numRows

Set Recordset3_cmd = Server.CreateObject ("ADODB.Command")
Recordset3_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset3_cmd.CommandText = "SELECT idfactura, nombreCliente, rfcCliente, folio, forma_pago, fechasellado FROM dbo.factura JOIN clientesFacturacion ON clientesFacturacion.idcliente = factura.idcliente WHERE idfactura > 0 AND MONTH(fechasellado) = "&Request.Form("mes")&" AND estatus = 'Cancelada' AND abreviatura = 'FA' ORDER BY Folio DESC"
Recordset3_cmd.Prepared = true
'Response.Write Recordset3_cmd.CommandText
Set Recordset3 = Recordset3_cmd.Execute
Recordset3_numRows = 0

Dim pUnit, subtot, descuento, total, fact, NC
subtot = 0
total = 0

Set RSInfo_cmd = Server.CreateObject ("ADODB.Command")
RSInfo_cmd.ActiveConnection = MM_Conecta1_STRING
RSInfo_cmd.CommandText = "SELECT * FROM dbo.confimenor WHERE idConf = 1 AND refacciones = 'True'"
RSInfo_cmd.Prepared = true

Set RSInfo = RSInfo_cmd.Execute
%>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<style type="text/css">
<!--
.titulos {
	font-size: 32px;
	font-weight: bold;
}
-->
</style>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Documento sin título</title>
</head>

<body>
<table width="700" border="0">
  <tr>
    <td><table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
      <tr align="center" valign="bottom">
        <td width="16%"><img src="img/pepi_logo.png" width="91" height="69" /></td>
        <td colspan="2" class="titulos"><%=RSInfo.fields.item("razon_social").value %></td>
      </tr>
      <tr align="center">
        <td height="25" colspan="2" valign="bottom">Vendedor: <strong><%=Request.Form("vendedor")%></strong></td>
        <td width="31%" valign="bottom">Fecha Impresion : <%= Date() %></td>
      </tr>
      <tr class="stylo1">
        <td colspan="3"></td>
        </tr>
    </table></td>
  </tr>
  <tr>
    <td>
      <table width="100%" border="1" cellpadding="0" cellspacing="0">
        <tr>
          <td colspan="6" align="center"><h3>Facturas</h3></td>
        </tr>
        <tr align="center" bgcolor="#e33045" class="stylo1">
          <td width="4%">Folio</td>
          <td width="5%">R.F.C.</td>
          <td width="30%">Cliente</td>
          <td width="15%">Forma de pago</td>
          <td width="17%">Fecha</td>
          <td width="7%">Subtotal</td>
        </tr>
        <% While (NOT Recordset1.EOF)

            'if para validar el cambio del color
            if color = cgrid2 then'color
              color = cgrid1
            else'color
              color = cgrid2
            end if'color
        %>
        <tr bgcolor="<%=color%>" class="stylo2">
          <td><%=Recordset1.Fields.Item("folio").Value%></td>
          <td><%=Recordset1.Fields.Item("rfcCliente").Value%></td>
          <td><%=Recordset1.Fields.Item("nombreCliente").Value%></td>
          <td align="center">
            <%If (Recordset1.Fields.Item("forma_pago").Value) = 1 Then
                Response.Write("Contado")
              ElseIf Recordset1.Fields.Item("forma_pago").Value = 2 Then
                Response.Write("Credito")
              End If
            %>
          </td>
          <td align="center"><%=(Recordset1.Fields.Item("fechasellado").Value)%></td>
          <%
            Set Recordset0_cmd = Server.CreateObject ("ADODB.Command")
            Recordset0_cmd.ActiveConnection = MM_conecta1_STRING
            Recordset0_cmd.CommandText = "SELECT descuento, precio_unitario, cantidad FROM detFactura WHERE id_factura = "&Recordset1.Fields.Item("idfactura").Value
            'response.Write(Recordset2_cmd.CommandText&"<br>")
            Recordset0_cmd.Prepared = true  
            Set Recordset0 = Recordset0_cmd.Execute

            While(NOT Recordset0.EOF)
             
              'generamos los totales, impuestos y descuentos
              If Recordset0.Fields.Item("descuento").Value <> "0" Then
                pUnit = truncarAseis(Recordset0.Fields.Item("precio_unitario").Value  * ((100 - (Recordset0.Fields.Item("descuento").Value)) / 100))

                subtot = subtot + redondear(pUnit * Recordset0.Fields.Item("cantidad").Value)
              Else
                pUnit = truncarAseis(Recordset0.Fields.Item("precio_unitario").Value)
                subtot = subtot + redondear(pUnit * Recordset0.Fields.Item("cantidad").Value)
              End If
               'Response.Write("Sub = "&subtot&"<br>")
              Recordset0.MoveNext
            Wend
          %>
          <td align="right">$ <%=FormatNumber(subtot, 2)%></td>
        </tr>
          <% 
            Recordset1.MoveNext()
            total = total + subtot
            subtot = 0
          Wend
          %>
          <tr>
            <td colspan="4" align="right"><strong>Total:</strong></td>
            <td colspan="2" align="right">$ <%=FormatNumber(total ,2)%></td>
          </tr>
          <%fact = total
          total = 0
          %>
      </table>
    </td>
  </tr>
  <tr>
    <td>
      <table width="100%" border="1" cellpadding="0" cellspacing="0">
        <tr>
          <td colspan="6" align="center"><h3>Notas de Credito</h3></td>
        </tr>
        <tr align="center" bgcolor="#e33045" class="stylo1">
          <td width="4%">Folio</td>
          <td width="5%">R.F.C.</td>
          <td width="30%">Cliente</td>
          <td width="15%">Forma de pago</td>
          <td width="17%">Fecha</td>
          <td width="7%">Subtotal</td>
        </tr>
        <% While (NOT Recordset2.EOF)

            'if para validar el cambio del color
            if color = cgrid2 then'color
              color = cgrid1
            else'color
              color = cgrid2
            end if'color
            Response.Write(Recordset2.Fields.Item("folio").Value)
        %>
        <tr bgcolor="<%=color%>" class="stylo2">
          <td><%=Recordset2.Fields.Item("folio").Value%></td>
          <td><%=Recordset2.Fields.Item("rfcCliente").Value%></td>
          <td><%=Recordset2.Fields.Item("nombreCliente").Value%></td>
          <td align="center">
            <%If (Recordset2.Fields.Item("forma_pago").Value) = 1 Then
                Response.Write("Contado")
              ElseIf Recordset2.Fields.Item("forma_pago").Value = 2 Then
                Response.Write("Credito")
              End If
            %>
          </td>
          <td align="center"><%=(Recordset2.Fields.Item("fechasellado").Value)%></td>
          <%
            Set Recordset0_cmd = Server.CreateObject ("ADODB.Command")
            Recordset0_cmd.ActiveConnection = MM_conecta1_STRING
            Recordset0_cmd.CommandText = "SELECT descuento,cantidad,precio_unitario FROM detFactura WHERE id_factura = "&Recordset2.Fields.Item("idfactura").Value
            'response.Write(Recordset2_cmd.CommandText&"<br>")
            Recordset0_cmd.Prepared = true  
            Set Recordset0 = Recordset0_cmd.Execute

            While(NOT Recordset0.EOF)
              'generamos los totales, impuestos y descuentos
              If Recordset0.Fields.Item("descuento").Value <> "0" Then
                pUnit = truncarAseis(Recordset0.Fields.Item("precio_unitario").Value  * ((100 - (Recordset0.Fields.Item("descuento").Value)) / 100))

                subtot = subtot + redondear(pUnit * Recordset0.Fields.Item("cantidad").Value)
              Else
                pUnit = truncarAseis(Recordset0.Fields.Item("precio_unitario").Value)
                subtot = subtot + redondear(pUnit * Recordset0.Fields.Item("cantidad").Value)
              End If
              Recordset0.MoveNext
            Wend
          %>
          <td align="right">$ <%=FormatNumber(subtot, 2)%></td>
        </tr>
          <% 
            Recordset2.MoveNext()
            total = total + subtot
            subtot = 0
          Wend
          %>
          <tr>
            <td colspan="4" align="right"><strong>Total:</strong></td>
            <td colspan="2" align="right">$ <%=FormatNumber(total ,2)%></td>
          </tr>
          <%NC = total
          total = 0%>          
      </table>
    </td>
  </tr>
  <tr>
    <td>
      <table width="100%" border="1" cellpadding="0" cellspacing="0">
        <tr>
          <td colspan="6" align="center"><h3>Facturas Canceladas</h3></td>
        </tr>
        <tr align="center" bgcolor="#e33045" class="stylo1">
          <td width="4%">Folio</td>
          <td width="5%">R.F.C.</td>
          <td width="30%">Cliente</td>
          <td width="15%">Forma de pago</td>
          <td width="17%">Fecha</td>
          <td width="7%">Subtotal</td>
        </tr>
        <% While (NOT Recordset3.EOF)

            'if para validar el cambio del color
            if color = cgrid2 then'color
              color = cgrid1
            else'color
              color = cgrid2
            end if'color
        %>
        <tr bgcolor="<%=color%>" class="stylo2">
          <td><%=Recordset3.Fields.Item("folio").Value%></td>
          <td><%=Recordset3.Fields.Item("rfcCliente").Value%></td>
          <td><%=Recordset3.Fields.Item("nombreCliente").Value%></td>
          <td align="center">
            <%If (Recordset3.Fields.Item("forma_pago").Value) = 1 Then
                Response.Write("Contado")
              ElseIf Recordset3.Fields.Item("forma_pago").Value = 2 Then
                Response.Write("Credito")
              End If
            %>
          </td>
          <td align="center"><%=(Recordset3.Fields.Item("fechasellado").Value)%></td>
          <%
            Set Recordset0_cmd = Server.CreateObject ("ADODB.Command")
            Recordset0_cmd.ActiveConnection = MM_conecta1_STRING
            Recordset0_cmd.CommandText = "SELECT * FROM detFactura WHERE id_factura = "&Recordset3.Fields.Item("idfactura").Value
            'response.Write(Recordset2_cmd.CommandText&"<br>")
            Recordset0_cmd.Prepared = true  
            Set Recordset0 = Recordset0_cmd.Execute

            While(NOT Recordset0.EOF)
              'generamos los totales, impuestos y descuentos
              If Recordset0.Fields.Item("descuento").Value <> "0" Then
                pUnit = truncarAseis(Recordset0.Fields.Item("precio_unitario").Value  * ((100 - (Recordset0.Fields.Item("descuento").Value)) / 100))

                subtot = subtot + redondear(pUnit * Recordset0.Fields.Item("cantidad").Value)
              Else
                pUnit = truncarAseis(Recordset0.Fields.Item("precio_unitario").Value)
                subtot = subtot + redondear(pUnit * Recordset0.Fields.Item("cantidad").Value)
              End If
              Recordset0.MoveNext
            Wend
          %>
          <td align="right">$ <%=FormatNumber(subtot, 2)%></td>
        </tr>
          <% 
            Recordset3.MoveNext()
            total = total + subtot
            subtot = 0
          Wend
          %>
          <tr>
            <td colspan="4" align="right"><strong>Total:</strong></td>
            <td colspan="2" align="right">$ <%=FormatNumber(total ,2)%></td>
          </tr>
          <%total = 0%>
          <tr>
            <td colspan="3" align="right"><strong>Total Comision (Factura - Nota Credito)</strong></td>
            <td colspan="3" align="right">$ <%=FormatNumber(CDbl(fact) - CDbl(NC), 2)%></td>
          </tr>
      </table>
    </td>    
  </tr>
</table>
<p>&nbsp;</p>
</body>
</html>
<%
Recordset1.Close()
Set Recordset1 = Nothing

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