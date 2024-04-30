<%@  language="VBSCRIPT" %>
<!--#include file="Connections/Conecta1.asp" -->
<!--#include file="config.asp"-->
<!--#include file="checklogin.asp"-->
<!--#include file="stylo2.asp"-->
<%
idempresa = 1
Dim Recordset1__MMColParam
Recordset1__MMColParam = "1"
If (Request.Cookies("login")("idSucursal") <> "") Then 
  Recordset1__MMColParam = Request.Cookies("login")("idSucursal")
	idempresa = Request.Cookies("login")("idSucursal")
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
dim color'Variable para guardar el color de los renglones'
dim cliente'Variable para guardar el nombre del clietne que se consulta
dim clnt

color = cgrid2
if Request.Form("cliente") <> "" then
cliente = " AND nombreCliente LIKE '%" & Request.Form("cliente") & "%'"
clnt = Trim(Request.Form("cliente"))
Else
cliente = " AND nombreCliente LIKE '%" & Request.QueryString("cliente") & "%'"
clnt = Trim(Request.QueryString("cliente"))
end if
%>
<%
Dim Recordset1
Dim oConn
Dim sql
Dim PaginaActual      ' en qué pagina estamos
Dim PaginasTotales    ' cuántas páginas tenemos
Dim TamPagina         ' cuantos registros por pagina
Dim CuantosRegistros  ' la cuenta que os he mencionado

TamPagina = 25
color = cgrid2

if Request.Querystring("pagina")="" then
   PaginaActual=1
else
   PaginaActual=CInt(Request.Querystring("pagina"))
end if   

Set Recordset1 = Server.CreateObject ("ADODB.Recordset")
Set oConn= Server.CreateObject("ADODB.Connection")
oCOnn.Open MM_conecta1_STRING
sql = "SELECT * FROM dbo.clientesFacturacion WHERE idCliente > 0 "&cliente&" ORDER BY nombreCliente asc"
Recordset1.PageSize = TamPagina
Recordset1.CacheSize = TamPagina

Recordset1.Open sql, oConn, 1, 2

PaginasTotales = Recordset1.PageCount

if PaginaActual < 1 then 
   PaginaActual = 1
end if
if PaginaActual > PaginasTotales then
   PaginaActual = PaginasTotales
end if
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<!-- InstanceBegin template="/Templates/plantillacfd.dwt.asp" codeOutsideHTMLIsLocked="false" -->
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<link rel="stylesheet" href="efectos/css/demos.css" media="screen" type="text/css">
    
	<script type="text/javascript" src="efectos/js/menu-for-applications.js"></script>
    <script language="JavaScript" src="jsF/overlib_mini.js"></script>
    
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
        -->
    </style>
    <!-- InstanceBeginEditable name="head" -->
    <!-- InstanceEndEditable -->
</head>

<body style="background-color: white">
    <div style="overflow:auto; width: 100%; height: 100%; scrollbar-arrow-color : #B2B2B2; scrollbar-face-color : #3FA5DC;
 scrollbar-track-color:#B6D0F3;">

    
    <table width="1024" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
     
        <tr>

            <td>
               
               
                <!-- InstanceBeginEditable name="EditRegion1" -->
                <p>&nbsp;</p>
                <table width="95%" border="0" align="center">
                    <tr>
                        <td width="11%">Clientes</td>
                        <td width="72%">Empresa:<strong><%=RecordsetE.Fields.Item("nombre").Value%></strong></td>
                        <td width="17%"><a href="clientesadd.asp">
                            <img src="imagenes/application_(add)_16x16.gif" alt="" width="16" height="16" border="0" />Agregar</a></td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <hr width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <form id="form1" name="form1" method="post" action="">
                                Buscar Clientes Nombre 
            <label>
                <input name="cliente" type="text" id="cliente" value="<%=Request.Form("cliente")%>" />
            </label>
                                <label>
                                    <input type="submit" name="button" id="button" value="Enviar" />
                                </label>
                            </form>
                        </td>
                    </tr>
                </table>
                <p>&nbsp;</p>
                <table border="0" align="center">
                    <tr bgcolor="#549BB1">
                        <td>Nombre de la empresa</td>
                        <td>RFC</td>
                        <td>Cons</td>
                        <td>Mod</td>
                        <td>Adenda</td>
                        <td>Adeudos</td>
                    </tr>
                    <%If PaginasTotales <> 0 Then
			Recordset1.AbsolutePage = PaginaActual
  			While ((CuantosRegistros < TamPagina) AND (NOT Recordset1.EOF)) 
			
			'if para validar el cambio del color
			if color = cgrid2 then
				color = cgrid1
			else
				color = cgrid2
			end if
                    %>
                    <tr bgcolor="<%=color%>">
                        <td><%=(Recordset1.Fields.Item("nombreCliente").Value)%></td>
                        <td><%=(Recordset1.Fields.Item("rfcCliente").Value)%></td>
                        <td><%=(Recordset1.Fields.Item("consecutivo").Value)%></td>
                        <td align="center"><a href="clientesmod.asp?idcliente=<%=(Recordset1.Fields.Item("idCliente").Value)%>">
                            <img src="imagenes/database_table_(edit)_16x16.gif" alt="" width="16" height="16" border="0" /></a></td>
                        <td align="center"><a href="cadendaadd.asp?idcliente=<%=(Recordset1.Fields.Item("idCliente").Value)%>">
                            <img src="imagenes/tarea_add.png" width="16" height="16" border="0" /></a></td>
                        <td align="center"><a href="reporteAdeudos.asp?id=<%=(Recordset1.Fields.Item("idCliente").Value)%>" target="_blank">
                            <img src="imagenes/money.png" alt="" width="20" height="16" border="0" /></a></td>
                    </tr>
                    <%CuantosRegistros = CuantosRegistros + 1
			Recordset1.MoveNext()
		Wend
	end if%>
                </table>

                <table border="0" align="center">
                    <tr>
                        <%if PaginaActual > 1 then%>
                        <td><a href="clientes.asp?pagina=1&cliente=<%=clnt%>">
                            <img src="First.gif" width="14" height="13" border="0" /></a></td>

                        <td><a href="clientes.asp?cliente=<%=clnt%>&amp;pagina=-1">
                            <img src="Previous.gif" width="14" height="13" border="0" /></a></td>
                        <%end if%>

                        <td align="center"><%Response.Write(PaginaActual)%> de <%Response.Write(PaginasTotales)%></td>

                        <%If PaginaActual < PaginasTotales Then %>
                        <td><a href="clientes.asp?cliente=<%=clnt%>&amp;pagina=<%=PaginaActual + 1%>">
                            <img src="Next.gif" width="14" height="13" border="0" /></a></td>

                        <td><a href="clientes.asp?pagina=<%=PaginasTotales%>&cliente=<%=clnt%>">
                            <img src="Last.gif" width="14" height="13" border="0" /></a></td>
                        <% End If %>
                    </tr>
                </table>
                <p>&nbsp;</p>
                <p>&nbsp;</p>
                <!-- InstanceEndEditable -->
                <p>&nbsp;</p>
                <%=footerPage%>
            </td>
        </tr>


    </table>
        </div>
</body>
<!-- InstanceEnd -->
</html>
<%
Recordset1.Close()
Set Recordset1 = Nothing
%>
