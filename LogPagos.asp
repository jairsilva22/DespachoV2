<%@  language="VBSCRIPT" codepage="65001" %>
<!--#include file="Connections/Conecta1.asp" -->
<!--#include file="stylo2.asp"-->
<%
Dim pagototal
pagototal=0
Dim numpago
numpago=1
Dim fecha
fecha=date
Dim usuario
usuario=1

Dim Recordset1
Dim Recordset1_cmd
Dim Recordset1_numRows

Set Recordset1_cmd = Server.CreateObject ("ADODB.Command")
Recordset1_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset1_cmd.CommandText = "SELECT * FROM dbo.logCXC, dbo.tipodepago WHERE tipodepago.idtipo=logCXC.tipo_pago AND logCXC.folio="&request.QueryString("folio") 
'response.Write(Recordset1_cmd.CommandText)
Recordset1_cmd.Prepared = true

Set Recordset1 = Recordset1_cmd.Execute
Recordset1_numRows = 0
%>
<%
Dim Repeat1__numRows
Dim Repeat1__index

Repeat1__numRows = 10
Repeat1__index = 0
Recordset1_numRows = Recordset1_numRows + Repeat1__numRows
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Documento sin título</title>
    <style type="text/css">
        <!--
        .STILO1 {
            font-size: 12px;
            font-weight: bold;
        }

        .STILO1 {
            text-align: center;
        }

        .stilo2 {
            font-size: 12px;
        }
        -->
    </style>
</head>

<body>
    <p align="center">&nbsp;</p>
    <p align="center">
        RECIBO DE PAGOS
    </p>
    <table width="99%" border="0">
        <tr>
            <td colspan="3" align="right">FECHA ACTUAL</td>
            <td><%=fecha%>&nbsp;</td>
        </tr>
        <tr>
            <td bgcolor="#CAFFFF">NUMERO DE FACTURA : <%=request.QueryString("folio")%></td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td width="9%">&nbsp;</td>
        </tr>
        <tr>
            <td>CLIENTE : <%=request.QueryString("cliente")%></td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td bgcolor="#CAFFFF">RFC : <%=request.QueryString("RFC")%></td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td colspan="4">&nbsp;
      <table width="100%" border="0">
          <tr class="STILO1" bgcolor="#6DB6B6">
              <td>NUMERO DE PAGO</td>
              <td>USUARIO</td>
              <td>FECHA</td>
              <td>PAGO RECIBIDO</td>
              <td>TIPO</td>
              <td>MONTO</td>
              <td>ESTATUS</td>
              <td>OBSERVACIONES</td>
              <td>TOTAL</td>
          </tr>
          <% While ((Repeat1__numRows <> 0) AND (NOT Recordset1.EOF)) 
		
		'if para validar el cambio del color
if color = cgrid2 then'color
color = cgrid1
else'color
color = cgrid2
end if'color
          %>
          <tr class="stilo2" bgcolor="<%=color%>">
              <td align="center"><%=numpago%>&nbsp;</td>
              <td align="center"><%=usuario%>&nbsp;</td>
              <td align="center"><%=(Recordset1.Fields.Item("fecha_captura").Value)%>&nbsp;</td>
              <td align="right"><%=(Recordset1.Fields.Item("cantidad_recibida").Value)%>&nbsp;</td>
              <td align="center"><%=(Recordset1.Fields.Item("Tipo").Value)%>&nbsp;</td>
              <td align="right"><%=formatnumber(Recordset1.Fields.Item("monto").Value, 2)%>&nbsp;</td>
              <td><%=(Recordset1.Fields.Item("estatus").Value)%></td>
              <td><%=(Recordset1.Fields.Item("observaciones").Value)%>&nbsp;</td>
              <td align="right"><%=(Recordset1.Fields.Item("total").Value)%>&nbsp;</td>
          </tr>
          <% 
		  montototal=Recordset1.Fields.Item("monto").Value
		
		 if (Recordset1.Fields.Item("estatus").Value)<>"Cancelado" then
		  pagototal=pagototal+Recordset1.Fields.Item("cantidad_recibida").Value
          end if
  Repeat1__index=Repeat1__index+1
  Repeat1__numRows=Repeat1__numRows-1
  Recordset1.MoveNext()
  numpago=numpago+1
Wend

          %>
      </table>
            </td>
        </tr>
        <tr>
            <td width="37%">&nbsp;</td>
            <td width="40%">&nbsp;</td>
            <td width="14%" align="right" bgcolor="#8CC6C6">MONTO TOTAL</td>
            <td align="right" bgcolor="#8CC6C6"><%=formatnumber(montototal, 2)%>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td align="right">TOTAL RECIBIDO</td>
            <%IF pagototal>montototal THEN
	     pagototal=montototal
		 END IF%>
            <td align="right"><%=formatnumber(pagototal, 2)%>&nbsp;</td>
        </tr>
        <%saldototal=montototal-pagototal%>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td align="right" bgcolor="#8CC6C6">SALDO ACTUAL</td>
            <%IF saldototal<0 THEN
	     saldototal=0
		 END IF%>
            <td align="right" bgcolor="#8CC6C6"><%=formatnumber(saldototal, 2)%>&nbsp;</td>
        </tr>
    </table>
    <p>&nbsp;</p>
</body>
</html>
<%
Recordset1.Close()
Set Recordset1 = Nothing
%>
