<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!--#include file="Connections/Conecta1.asp" -->
<%
dim color'Variable para guardar el color de las lineas
dim folio'Variable para guardar el folio a filtrar
dim banco'Variable para guardar el banco a filtrar
dim pago'Variable para guardar el pago a filtrar
dim usuario'Variable para guardar los usuarios a filtrar
dim fecha'Variable para guardar las fechas a filtrar
dim papel'Variable para guardar si filtra en papel

color = cgrid1
'if para validar que se filtran las fehcas
if Request.Form("papel") <> "" then'fehcas
papel = " AND anterior = '" & Request.Form("papel") & "'"
end if'fehcas

'if para validar que se filtran las fehcas
if Request.Form("finicial") <> "" AND Request.Form("ffinal") <> "" then'fehcas
fecha = " AND logCXC.fecha_captura BETWEEN CONVERT(datetime, '" & Request.Form("finicial") & "', 103) AND CONVERT(datetime, '" & Request.Form("ffinal") & "', 103)"
end if'fehcas

'if para validar que se filtra el usuario
if Request.Form("usuario") <> "" then'usuario
usuario = " AND logCXC.idusuario = " & Request.Form("usuario")
end if'usuario

'if para validar que se filtra el pago
if Request.Form("pago") <> "" then'pago
pago = " AND tipo_pago = " & Request.Form("pago")
end if'pago

'if para validar que se filtra el banco
if Request.Form("banco") <> "" then'banco
banco = " AND banco = " & Request.Form("banco")
end if'banco

'if para validar que se filtra el folio
if Request.Form("folio") <> "" then'folio
folio = " AND folio = " & Request.Form("folio")
end if'folio
%>
<%
Dim Recordset1
Dim Recordset1_cmd
Dim Recordset1_numRows

Set Recordset1_cmd = Server.CreateObject ("ADODB.Command")
Recordset1_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset1_cmd.CommandText = "SELECT folio, refecencia, cantidad_recibida, monto, observaciones, anterior, ntarjeta, fecha_captura, banco, tipodepago.Tipo as pago, usuarios.nombre as usuario, logCXC.fechaalta as fechaalta FROM dbo.logCXC, dbo.tipodepago, dbo.usuarios WHERE idtipo = tipo_pago AND usuarios.id = logCXC.idusuario "&folio&banco&pago&usuario&fecha&papel&" ORDER BY fecha_captura ASC" 
Recordset1_cmd.Prepared = true
'response.write Recordset1_cmd.CommandText
Set Recordset1 = Recordset1_cmd.Execute
Recordset1_numRows = 0
%>
<%
Dim Recordset2__MMColParam
Recordset2__MMColParam = "1"
If (Session("site_empresa") <> "") Then 
  Recordset2__MMColParam = Session("site_empresa")
End If
%>
<%
Dim Recordset2
Dim Recordset2_cmd
Dim Recordset2_numRows

Set Recordset2_cmd = Server.CreateObject ("ADODB.Command")
Recordset2_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset2_cmd.CommandText = "SELECT * FROM dbo.sucursales WHERE id = ?" 
Recordset2_cmd.Prepared = true
Recordset2_cmd.Parameters.Append Recordset2_cmd.CreateParameter("param1", 5, 1, -1, Recordset2__MMColParam) ' adDouble

Set Recordset2 = Recordset2_cmd.Execute
Recordset2_numRows = 0
%>
<%
Dim Repeat1__numRows
Dim Repeat1__index

Repeat1__numRows = -1
Repeat1__index = 0
Recordset1_numRows = Recordset1_numRows + Repeat1__numRows
%>
<!--#include file="config.asp"-->
<!--#include file="checklogin.asp"-->
<!--#include file="stylo2.asp"-->
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<style type="text/css">
<!--
.styloTitulo {
	font-size: 24px;
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
    <td><table width="100%" height="43" border="1" cellpadding="0" cellspacing="0">
      <tr valign="bottom">
        <td width="23%"><img src="img/pepi_logo.png" width="150px" height="60px" /></td>
        <td width="77%" class="styloTitulo">Reporte de captura</td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td><table width="100%" border="0">
      <tr align="center" bgcolor="#e33045" class="stylo1">
        <td>Folio</td>
        <td>Usuario</td>
        <td>Referencia</td>
        <td>Cantidad recibida</td>
        <td>Monto</td>
        <td>Observaciones</td>
        <td>Papel</td>
        <td>Fecha captura</td>
        <td>No tarjeta</td>
        <td>Banco</td>
        <td>Pago</td>
      </tr>
      <% While ((Repeat1__numRows <> 0) AND (NOT Recordset1.EOF)) %>
<%
if color = cgrid1 then
color = cgrid2
else
color = cgrid1
end if
%>      
      <tr bgcolor="<%=color%>" class="stylo2">
        <td><%=(Recordset1.Fields.Item("folio").Value)%></td>
        <td><%=(Recordset1.Fields.Item("usuario").Value)%></td>
        <td><%=(Recordset1.Fields.Item("refecencia").Value)%></td>
        <td><%=(Recordset1.Fields.Item("cantidad_recibida").Value)%></td>
        <td><%=(Recordset1.Fields.Item("monto").Value)%></td>
        <td><%=(Recordset1.Fields.Item("observaciones").Value)%></td>
        <td><%=(Recordset1.Fields.Item("anterior").Value)%></td>
        <td><%=(Recordset1.Fields.Item("fecha_captura").Value)%></td>
        <td><%=(Recordset1.Fields.Item("ntarjeta").Value)%></td>
        <%
        If Not IsNull(Recordset1.Fields.Item("banco").Value) And Recordset1.Fields.Item("banco").Value <> "" Then
          
          Set Recordsetruta2_cmd = Server.CreateObject ("ADODB.Command")
          Recordsetruta2_cmd.ActiveConnection = MM_conecta1_STRING
          Recordsetruta2_cmd.CommandText = "SELECT nombre FROM bancos WHERE id = "&Recordset1.Fields.Item("banco").Value
          'response.write Recordsetruta2_cmd.CommandText
          Recordsetruta2_cmd.Prepared = true
          Set Recordsetruta2 = Recordsetruta2_cmd.Execute
          Recordsetruta2_numRows = 0

          If Not Recordset2.EOF Then
            bancos = Recordsetruta2.Fields.Item("nombre").Value
          else  
            bancos = ""
          End If
        else
          bancos = ""
        End If
        %>
        <td><%=bancos%></td>
        <td><%=(Recordset1.Fields.Item("pago").Value)%></td>
      </tr>
      <% 
  Repeat1__index=Repeat1__index+1
  Repeat1__numRows=Repeat1__numRows-1
  Recordset1.MoveNext()
Wend
%>
    </table></td>
  </tr>
</table>
<p>&nbsp;</p>
</body>
</html>
<%
Recordset1.Close()
Set Recordset1 = Nothing
%>
<%
Recordset2.Close()
Set Recordset2 = Nothing
%>
