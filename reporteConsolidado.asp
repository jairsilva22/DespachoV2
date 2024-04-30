<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!--#include file="Connections/Conecta1.asp" -->
<!--#include file="stylo2.asp"-->
<%
Session.LCID  = 2058
Server.ScriptTimeOut = 500000000
Dim Venta
Dim IVAs
Dim Total
Dim AIva
Dim Atotal

Dim TotalDet
Dim subTDet
Dim cantidad
Dim pUnitario
Dim descuento
Dim ivaDet
Dim retDet

Dim mes1
Dim mes2
Dim mes3
Dim mes4
Dim mes5
Dim mes6
Dim mes7
Dim mes8
Dim mes9
Dim mes10
Dim mes11
Dim mes12


%>
<% 'busca nombre de la empresa
Dim Recordset1E
Dim Recordset1E_cmd
Dim Recordset1E_numRows

Set Recordset1E_cmd = Server.CreateObject ("ADODB.Command")
Recordset1E_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset1E_cmd.CommandText = "SELECT * FROM dbo.sucursales WHERE id = "&Request.Cookies("login")("idSucursal")
Recordset1E_cmd.Prepared = true
'response.Write(Recordset1_cmd.CommandText)
Set Recordset1E = Recordset1E_cmd.Execute
Recordset1E_numRows = 0
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
<title>Reporte Acumulado</title>
</head>

<body>
  <table width="90%" border="0" align="center" cellpadding="0" cellspacing="0">
      <tr align="center" valign="bottom">
        <td width="16%"><img src="img/pepi_logo.png" width="91" height="69" /></td>
        <td colspan="2" class="titulos"><%= Recordset1E.Fields.Item("nombre").Value %></td>
      </tr>
      <tr align="center">
        <td height="25" colspan="2" valign="bottom"><strong>AÑO DE BUSQUEDA: <%= Request.QueryString("anio") %></strong></td>
        <td width="31%" valign="bottom">Fecha Impresion : <%= Date() %></td>
      </tr>
      <tr class="stylo1">
        <td colspan="3"></td>
        </tr>
  </table>  
  <table border="1" width="90%" align="center" cellpadding="0" cellspacing="0">
        <tr align="center"  bgcolor="#e33045" class="stylo1">
          <td>&nbsp;  </td>
          <td>Enero</td>
          <td>Febrero</td>
          <td>Marzo</td>
          <td>Abril</td>
          <td>Mayo</td>
          <td>Junio</td>
          <td>Julio</td>
          <td>Agosto</td>
          <td>Septiembre</td>
          <td>Octubre</td>
          <td>Noviembre</td>
          <td>Diciembre</td>
        </tr>
        <tr>
          <td align="right">Venta :</td>
          <td align="right">$ <%=FormatNumber(redondear(TotalMes(1)), 2)%></td>
          <td align="right">$ <%=FormatNumber(redondear(TotalMes(2)), 2)%></td>
          <td align="right">$ <%=FormatNumber(redondear(TotalMes(3)), 2)%></td>
          <td align="right">$ <%=FormatNumber(redondear(TotalMes(4)), 2)%></td>
          <td align="right">$ <%=FormatNumber(redondear(TotalMes(5)), 2)%></td>
          <td align="right">$ <%=FormatNumber(redondear(TotalMes(6)), 2)%></td>
          <td align="right">$ <%=FormatNumber(redondear(TotalMes(7)), 2)%></td>
          <td align="right">$ <%=FormatNumber(redondear(TotalMes(8)), 2)%></td>
          <td align="right">$ <%=FormatNumber(redondear(TotalMes(9)), 2)%></td>
          <td align="right">$ <%=FormatNumber(redondear(TotalMes(10)), 2)%></td>
          <td align="right">$ <%=FormatNumber(redondear(TotalMes(11)), 2)%></td>
          <td align="right">$ <%=FormatNumber(redondear(TotalMes(12)), 2)%></td>
        </tr>
        <tr>
          <td align="right">IVA :</td>
          <td align="right">$ <%=FormatNumber(redondear(mes1 * 0.16), 2)%></td>
          <td align="right">$ <%=FormatNumber(redondear(mes2 * 0.16), 2)%></td>
          <td align="right">$ <%=FormatNumber(redondear(mes3 * 0.16), 2)%></td>
          <td align="right">$ <%=FormatNumber(redondear(mes4 * 0.16), 2)%></td>
          <td align="right">$ <%=FormatNumber(redondear(mes5 * 0.16), 2)%></td>
          <td align="right">$ <%=FormatNumber(redondear(mes6 * 0.16), 2)%></td>
          <td align="right">$ <%=FormatNumber(redondear(mes7 * 0.16), 2)%></td>
          <td align="right">$ <%=FormatNumber(redondear(mes8 * 0.16), 2)%></td>
          <td align="right">$ <%=FormatNumber(redondear(mes9 * 0.16), 2)%></td>
          <td align="right">$ <%=FormatNumber(redondear(mes10 * 0.16), 2)%></td>
          <td align="right">$ <%=FormatNumber(redondear(mes11 * 0.16), 2)%></td>
          <td align="right">$ <%=FormatNumber(redondear(mes12 * 0.16), 2)%></td>
        </tr>
        <tr bgcolor="#9FA2F3">
          <td align="right">Acumulado :</td>
          <td align="right">$ 
            <% Total = redondear(mes1)
               Response.Write(FormatNumber(redondear(Total)))
            %>
          </td>
          <td align="right">$ 
            <% Total = CDbl(Total) + CDbl(mes2)
               Response.Write(FormatNumber(redondear(Total)))
            %>
          </td>
          <td align="right">$ 
            <% Total = CDbl(Total) + CDbl(mes3)
               Response.Write(FormatNumber(redondear(Total)))
            %>
          </td>
          <td align="right">$ 
            <% Total = CDbl(Total) + CDbl(mes4)
               Response.Write(FormatNumber(redondear(Total)))
            %>
          </td>
          <td align="right">$ 
            <% Total = CDbl(Total) + CDbl(mes5)
               Response.Write(FormatNumber(redondear(Total)))
            %>
          </td>
          <td align="right">$ 
            <% Total = CDbl(Total) + CDbl(mes6)
               Response.Write(FormatNumber(redondear(Total)))
            %>
          </td>
          <td align="right">$ 
            <% Total = CDbl(Total) + CDbl(mes7)
               Response.Write(FormatNumber(redondear(Total)))
            %>
          </td>
          <td align="right">$ 
            <% Total = CDbl(Total) + CDbl(mes8)
               Response.Write(FormatNumber(redondear(Total)))
            %>
          </td>
          <td align="right">$ 
            <% Total = CDbl(Total) + CDbl(mes9)
               Response.Write(FormatNumber(redondear(Total)))
            %>
          </td>
          <td align="right">$ 
            <% Total = CDbl(Total) + CDbl(mes10)
               Response.Write(FormatNumber(redondear(Total)))
            %>
          </td>
          <td align="right">$ 
            <% Total = CDbl(Total) + CDbl(mes11)
               Response.Write(FormatNumber(redondear(Total)))
            %>
          </td>
          <td align="right">$ 
            <% Total = CDbl(Total) + CDbl(mes12)
               Response.Write(FormatNumber(redondear(Total)))
            %>
          </td>          
        </tr>
        <tr bgcolor="#BDBDC3">
          <td align="right">Acumulado IVA :</td>
          <td align="right">$ 
            <% AIva = redondear(redondear(mes1 * 0.16))
               Response.Write(FormatNumber(AIva))
            %>            
          </td>
          <td align="right">$ 
            <% AIva = redondear(CDbl(AIva) + redondear(mes2 * 0.16))
               Response.Write(FormatNumber(AIva))
            %>
          </td>
          <td align="right">$ 
            <% AIva = redondear(CDbl(AIva) + redondear(mes3 * 0.16))
               Response.Write(FormatNumber(AIva))
            %>            
          </td>
          <td align="right">$ 
            <% AIva = redondear(CDbl(AIva) + redondear(mes4 * 0.16))
               Response.Write(FormatNumber(AIva))
            %>            
          </td>
          <td align="right">$ 
            <% AIva = redondear(CDbl(AIva) + redondear(mes5 * 0.16))
               Response.Write(FormatNumber(AIva))
            %>            
          </td>
          <td align="right">$ 
            <% AIva = redondear(CDbl(AIva) + redondear(mes6 * 0.16))
               Response.Write(FormatNumber(AIva))
            %>            
          </td>
          <td align="right">$ 
            <% AIva = redondear(CDbl(AIva) + redondear(mes7 * 0.16))
               Response.Write(FormatNumber(AIva))
            %>            
          </td>
          <td align="right">$ 
            <% AIva = redondear(CDbl(AIva) + redondear(mes8 * 0.16))
               Response.Write(FormatNumber(AIva))
            %>            
          </td>
          <td align="right">$ 
            <% AIva = redondear(CDbl(AIva) + redondear(mes9 * 0.16))
               Response.Write(FormatNumber(AIva))
            %>            
          </td>
          <td align="right">$ 
            <% AIva = redondear(CDbl(AIva) + redondear(mes10 * 0.16))
               Response.Write(FormatNumber(AIva))
            %>            
          </td>
          <td align="right">$ 
            <% AIva = redondear(CDbl(AIva) + redondear(mes11 * 0.16))
               Response.Write(FormatNumber(AIva))
            %>            
          </td>
          <td align="right">$ 
            <% AIva = redondear(CDbl(AIva) + redondear(mes12 * 0.16))
               Response.Write(FormatNumber(AIva))
            %>            
          </td>          
        </tr>
        <tr bgcolor="#E0E0EE">
          <td align="right">Total Acumulado :</td>
          <td align="right">$ 
            <% Atotal = redondear(redondear(mes1 * 0.16) + CDbl(mes1))
               Response.Write(FormatNumber(Atotal))
            %>
          </td>
          <td align="right">$ 
            <% Atotal = redondear(CDbl(Atotal) + redondear(mes2 * 0.16) + CDbl(mes2))
               Response.Write(FormatNumber(Atotal))
            %>
          </td>
          <td align="right">$ 
            <% Atotal = redondear(CDbl(Atotal) + redondear(mes3 * 0.16) + CDbl(mes3))
               Response.Write(FormatNumber(Atotal))
            %>
          </td>
          <td align="right">$ 
            <% Atotal = redondear(CDbl(Atotal) + redondear(mes4 * 0.16) + CDbl(mes4))
               Response.Write(FormatNumber(Atotal))
            %>              
          </td>
          <td align="right">$ 
            <% Atotal = redondear(CDbl(Atotal) + redondear(mes5 * 0.16) + CDbl(mes5))
               Response.Write(FormatNumber(Atotal))
            %>              
          </td>
          <td align="right">$ 
            <% Atotal = redondear(CDbl(Atotal) + redondear(mes6 * 0.16) + CDbl(mes6))
               Response.Write(FormatNumber(Atotal))
            %>              
          </td>
          <td align="right">$ 
            <% Atotal = redondear(CDbl(Atotal) + redondear(mes7 * 0.16) + CDbl(mes7))
               Response.Write(FormatNumber(Atotal))
            %>              
          </td>
          <td align="right">$ 
            <% Atotal = redondear(CDbl(Atotal) + redondear(mes8 * 0.16) + CDbl(mes8))
               Response.Write(FormatNumber(Atotal))
            %>              
          </td>
          <td align="right">$ 
            <% Atotal = redondear(CDbl(Atotal) + redondear(mes9 * 0.16) + CDbl(mes9))
               Response.Write(FormatNumber(Atotal))
            %>              
          </td>
          <td align="right">$ 
            <% Atotal = redondear(CDbl(Atotal) + redondear(mes10 * 0.16) + CDbl(mes10))
               Response.Write(FormatNumber(Atotal))
            %>              
          </td>
          <td align="right">$ 
            <% Atotal = redondear(CDbl(Atotal) + redondear(mes11 * 0.16) + CDbl(mes11))
               Response.Write(FormatNumber(Atotal))
            %>              
          </td>
          <td align="right">$ 
            <% Atotal = redondear(CDbl(Atotal) + redondear(mes12 * 0.16) + CDbl(mes12))
               Response.Write(FormatNumber(Atotal))
            %>              
          </td>          
        </tr>
  </table>
<p>&nbsp;</p>
</body>
</html>
<%
'funcion redondear a 2 digitos
Function redondear(val)
  If InStr(val, ".") <> 0 Then
    'redondear la funcion
    val = Round(val, 2)
  Else
    val = val & ".00"
  End If
  redondear = val
End Function

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

Function TotalMes(mes)

  TotalDet = 0
  subtotal1 = 0
  Set Recordset1_cmd = Server.CreateObject ("ADODB.Command")
  Recordset1_cmd.ActiveConnection = MM_Conecta1_STRING
  Recordset1_cmd.CommandText = "SELECT precio_unitario, cantidad, detfactura.descuento FROM dbo.detfactura JOIN factura on idfactura = id_factura WHERE idfactura > 0 AND factura.subtotal > 0 and folio is not NULL AND UUID IS NOT NULL AND timbre = 'NO' AND Month(fechasellado) = "&mes&" AND year(fechasellado) = "&Request.QueryString("anio")&" AND estatus = 'Facturada'AND tipo_comprobante = 1 ORDER BY folio "
  Recordset1_cmd.Prepared = true
  Set Recordset1 = Recordset1_cmd.Execute
  'Response.Write Recordset1_cmd.CommandText

  While Not Recordset1.EOF
    if Recordset1.fields.item("descuento").value > 0 Then
          precioUnitario = (Recordset1.Fields.Item("precio_unitario").Value * CDbl((100 - Recordset1.Fields.Item("descuento").Value) / 100))
    Else
      precioUnitario = CDbl(Recordset1.Fields.Item("precio_unitario").Value)
    End If
     'Response.Write(Recordset1.Fields.Item("cantidad").Value & " * " &Recordset1.Fields.Item("precio_unitario").Value & "=" & precioUnitario & "<br/>")
    totalConcepto = redondear(precioUnitario * Recordset1.Fields.Item("cantidad").Value)
    subtotal1 = redondear(CDbl(subtotal1) + CDbl(totalConcepto))   

    TotalDet = TotalDet + totalConcepto

    Response.Flush
    Recordset1.MoveNext
  Wend

  If mes = 1 Then
    mes1 = TotalDet
  ElseIf mes = 2 Then
    mes2 = TotalDet
  ElseIf mes = 3 Then
    mes3 = TotalDet
  ElseIf mes = 4 Then
    mes4 = TotalDet
  ElseIf mes = 5 Then
    mes5 = TotalDet
  ElseIf mes = 6 Then
    mes6 = TotalDet
  ElseIf mes = 7 Then
    mes7 = TotalDet
  ElseIf mes = 8 Then
    mes8 = TotalDet
  ElseIf mes = 9 Then
    mes9 = TotalDet
  ElseIf mes = 10 Then
    mes10 = TotalDet
  ElseIf mes =11 Then
    mes11 = TotalDet
  ElseIf mes = 12 Then
    mes12 = TotalDet
  End If
 
  TotalMes = TotalDet
End Function
%>