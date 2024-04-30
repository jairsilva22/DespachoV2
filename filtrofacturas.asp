<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!--#include file="Connections/Conecta1.asp" -->
<!--#include file="config.asp"-->
<!--#include file="checklogin.asp"-->
<%
Dim Recordset1
Dim Recordset1_cmd
Dim Recordset1_numRows

Set Recordset1_cmd = Server.CreateObject ("ADODB.Command")
Recordset1_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset1_cmd.CommandText = "SELECT DISTINCT Year(fechacfd) as yearemiso FROM dbo.factura ORDER BY Year(fechacfd) DESC" 
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
Recordset2_cmd.CommandText = "SELECT * FROM dbo.documento ORDER BY descripcion ASC" 
Recordset2_cmd.Prepared = true

Set Recordset2 = Recordset2_cmd.Execute
Recordset2_numRows = 0
%>
<%
Dim Recordset3
Dim Recordset3_cmd
Dim Recordset3_numRows

Set Recordset3_cmd = Server.CreateObject ("ADODB.Command")
Recordset3_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset3_cmd.CommandText = "SELECT * FROM dbo.formPago ORDER BY descripcion ASC" 
Recordset3_cmd.Prepared = true

Set Recordset3 = Recordset3_cmd.Execute
Recordset3_numRows = 0
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
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml"><!-- InstanceBegin template="/Templates/plantillacfd.dwt.asp" codeOutsideHTMLIsLocked="false" -->
    <script language="JavaScript" src="jsF/overlib_mini.js"></script>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<!-- InstanceBeginEditable name="doctitle" -->
<title><%=titlePage%></title>
<link rel="stylesheet" href="css.css" type="text/css" media="screen"  />
    <link rel="stylesheet" href="efectos/css/demos.css" media="screen" type="text/css">
    <link href="//maxcdn.bootstrapcdn.com/font-awesome/4.2.0/css/font-awesome.min.css" rel="stylesheet">
	<script type="text/javascript" src="efectos/js/menu-for-applications.js"></script>
<!-- InstanceEndEditable -->
<style type="text/css">
<!--
body {
	background-color: #CCC;
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
.selectColor {
    background-color: white;
    color: black;
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
         }
     </script>
    <link rel="stylesheet" href="jsF/css.css" type="text/css" media="screen"  />
    <link rel="stylesheet" href="jsF/bootstrap.css" />
    <script src="jsF/jquery.min.js" type="text/javascript"></script>
    <script src="jsF/bootstrap.min.js"></script>
<!-- InstanceEndEditable -->
</head>

<body style="background-color:white;">
<table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
  <tr>
      <td>
      <!-- InstanceBeginEditable name="EditRegion1" -->
      <p>&nbsp;</p>
      <table width="50%" border="0" align="center">
        <% If Request.QueryString("term") = "1" Then %>
        <% End If %>
        <tr align="center">
          <td>Seleccione el mes y el año de emision para mostrar las facturas</td>
        </tr>
      </table>
      <form action="repfacturas.asp" method="post" name="form1" target="_blank" id="form1">
        <p>&nbsp;</p>
        <table width="56%" border="0" align="center">
          <tr>
            <td align="right">Folio:</td>
            <td><label>
              <input type="text" name="folio" id="folio" />
            </label></td>
          </tr>
          <tr>
            <td width="45%" align="right">Mes de emisión: </td>
            <td width="44%"><select name="mes" id="mes" class="selectColor">
            <option value="">Todos</option>            
              <%for i = 1 to 12%>
              <option value="<%=i%>" <%If (Not isNull(Month(Date()))) Then If (i = Month(Date())) Then Response.Write("selected=""selected""") : Response.Write("")%>><%=MonthName(i)%></option>
              <%next%>
            </select></td>
          </tr>
          <tr>
            <td align="right">Año de emisión: </td>
            <td><select name="yearemiso" id="yearemiso" class="selectColor">
              <%
While (NOT Recordset1.EOF)
%>
              <option value="<%=(Recordset1.Fields.Item("yearemiso").Value)%>"><%=(Recordset1.Fields.Item("yearemiso").Value)%></option>
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
          <tr>
            <td align="right">Cliente:</td>
            <td align="left" class="Estilo1">
                  <a href="javascript:clientes()">
                                    <img src="imagenes/filefind.gif" width="20" height="20" /></a>
                                <input type="hidden" name="cliente" id="cliente" value="<%=clienteForm%>"/>
                                <div id="clientes" style="display: inline;"><strong><%=nombre%></strong></div>
              <!--<select name="cliente" id="cliente">
                <option value="">Todos</option>
                <%
While (NOT Recordset4.EOF)
%>
                <option value="<%=(Recordset4.Fields.Item("idCliente").Value)%>"><%=(Recordset4.Fields.Item("nombreCliente").Value)%></option>
                <%
  Recordset4.MoveNext()
Wend
If (Recordset4.CursorType > 0) Then
  Recordset4.MoveFirst
Else
  Recordset4.Requery
End If
%>
              </select>-->
            </td>
          </tr>
            <tr>
            <td align="right">Empresa:</td>
            <td align="left" class="Estilo1"><label>
              <select name="sucursal" id="sucursal" class="selectColor">
                <option value="">Todos</option>
                <%
While (NOT Recordset6.EOF)
                     empresa = split(Recordset6.Fields.Item("nombre").Value, ",")

%>
               <option value="<%=(Recordset6.Fields.Item("id").Value)%>"><%=(empresa(0))%>-><%=(Recordset6.Fields.Item("nombre").Value)%></option>
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
            </label></td>
          </tr>
          <tr>
            <td align="right">Estatus:</td>
            <td align="left" class="Estilo1"><select name="estatus" id="estatus" class="selectColor">
              <option value="">Todas</option>
              <option value="Facturada">Facturada</option>
              <option value="Cancelada">Cancelada</option>
              <option value="Pendiente">Pendiente</option>
              <option value="Procesando">Procesando</option>
              <option value="Error">Error</option>
            </select></td>
          </tr>
          <tr>
            <td align="right">Tipo de documento:</td>
            <td align="left" class="Estilo1"><select name="documento" id="documento" class="selectColor">
              <%
While (NOT Recordset2.EOF)
%>
              <option value="<%=(Recordset2.Fields.Item("iddocumento").Value)%>"><%=(Recordset2.Fields.Item("descripcion").Value)%></option>
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
          <tr>
            <td align="right">Forma de pago:</td>
            <td align="left"><label>
              <select name="fpago" id="fpago" class="selectColor">
                <option value="">Todos</option>
                <option value="XAXX010101000">Venta al publico</option>
                <%
While (NOT Recordset3.EOF)
%>
                <option value="<%=(Recordset3.Fields.Item("idpago").Value)%>"><%=(Recordset3.Fields.Item("descripcion").Value)%></option>
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
            </label></td>
          </tr>
          <tr>
            <td align="right">Vendedor:</td>
            <td align="left"><label>
              <select name="vendedor" id="vendedor" class="selectColor">
                <option value="">Todos</option>
                <%
While (NOT Recordset5.EOF)
%>
                <option value="<%=(Recordset5.Fields.Item("vendedor").Value)%>"><%=(Recordset5.Fields.Item("vendedor").Value)%></option>
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
            </label></td>
          </tr>
          <tr>
            <td align="right">Ordenado por:</td>
            <td align="left"><select name="orden" id="orden" class="selectColor">
              <option value="folio">Folio</option>
              <option value="estatus">Estatus</option>
              <option value="documento.descripcion">Tipo de documento</option>   
              <option value="formPago.descripcion">Forma de pago</option>                         
            </select></td>
          </tr>
          <tr>
            <td colspan="3" align="center"><label>
              <input type="submit" name="button" id="button" value="Enviar" />
            </label></td>
          </tr>
        </table>
        <p>&nbsp;</p>
      </form>
      <p>&nbsp;</p>
      <p>&nbsp;</p>
      <!-- InstanceEndEditable -->
 <p>&nbsp;</p>
      <%=footerPage%>
    </td>
  </tr>
 
  
</table>
      
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
Recordset4.Close()
Set Recordset4 = Nothing
%>
<%
Recordset5.Close()
Set Recordset5 = Nothing
%>
