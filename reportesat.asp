<%@  language="VBSCRIPT" %>
<!--#include file="Connections/conecta1.asp" -->
<!--#include file="config.asp"-->
<!--#include file="checklogin.asp"-->
<!--#include file="stylo2.asp" -->
<%
    Dim param
Dim Recordset1
Dim Recordset1_cmd
Dim Recordset1_numRows

Set Recordset1_cmd = Server.CreateObject ("ADODB.Command")
Recordset1_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset1_cmd.CommandText = "SELECT DISTINCT YEAR(fecha_alta) as año FROM detFactura ORDER BY año DESC" 
Recordset1_cmd.Prepared = true

Set Recordset1 = Recordset1_cmd.Execute
Recordset1_numRows = 0
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<!-- InstanceBegin template="/Templates/plantillacfd.dwt.asp" codeOutsideHTMLIsLocked="false" -->
<script language="JavaScript" src="jsF/overlib_mini.js"></script>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="efectos/css/demos.css" media="screen" type="text/css">
    <link href="//maxcdn.bootstrapcdn.com/font-awesome/4.2.0/css/font-awesome.min.css" rel="stylesheet">
    <script type="text/javascript" src="efectos/js/menu-for-applications.js"></script>
    <title><%=titlePage%></title>
    <link rel="stylesheet" href="jsF/css.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="jsF/bootstrap.css" />
    <script src="jsF/jquery.min.js" type="text/javascript"></script>
    <script src="jsF/bootstrap.min.js"></script>
    <link rel="stylesheet" href="css.css" type="text/css" media="screen" />

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

        .selectColor {
            background-color: white;
            color: black;
        }
        -->
    </style>
    <!-- InstanceBeginEditable name="head" -->
    <script language="javascript1.2">
function perfil()
{
		window.open("perfil.asp","Perfil","toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=yes, width=508, height=300, top=85, left=140")
}

function ayuda()
{
	pagina='<%=mid(Request.ServerVariables("URL"),Instrrev(Request.ServerVariables("URL"),"/")+1)%>'
	
	
	window.open("ayuda.asp?pagina="+pagina,"Perfil","toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=yes, width=908, height=500, top=85, left=140")
}
function logistica(pagina)
{
		
		pagina='<%=mid(Request.ServerVariables("URL"),Instrrev(Request.ServerVariables("URL"),"/")+1)%>'
		
		window.open("logistica.asp?pagina="+pagina,"Perfil","toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=yes, width=908, height=500, top=85, left=140")
}
    </script>




    <script language="javascript1.2">
function perfil()
{
		window.open("perfil.asp","Perfil","toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=yes, width=508, height=300, top=85, left=140")
}

function ayuda(pagina)
{
		window.open("ayuda.asp?pagina="+pagina,"Perfil","toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=yes, width=908, height=500, top=85, left=140")
}
function logistica(pagina)
{
  '<%=mid(Request.ServerVariables("URL"),Instrrev(Request.ServerVariables("URL"),"/")+1)%>'
	
		window.open("logistica.asp?pagina="+pagina,"Perfil","toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=yes, width=908, height=500, top=85, left=140")
}
    </script>


    <script>
        function param(){
         var id = document.getElementById("year").value
         url = "reporteSAT.aspx?year="+id
        window.open(url, "_blank");
        }
    </script>

    <!-- InstanceEndEditable -->


</head>

<body style="background-color: white;">
    <%
dim descripcion'Variable para guardar la descripcion de la pagina
dim pagina'Variable para guardar el nombre de la pagina

'Inicializar vareiables
pagina = trim(mid(Request.ServerVariables("URL"),Instrrev(Request.ServerVariables("URL"),"/")+1))
'////////////////////////////////////


'Recordset para tomar la descripcion de la pagina
Dim RSDescripcionPagina
Dim RSDescripcionPagina_cmd
Dim RSDescripcionPagina_numRows

Set RSDescripcionPagina_cmd = Server.CreateObject ("ADODB.Command")
RSDescripcionPagina_cmd.ActiveConnection = MM_Conecta1_STRING
RSDescripcionPagina_cmd.CommandText = "SELECT * FROM dbo.paginas WHERE pagina = '"&pagina&"'" 
RSDescripcionPagina_cmd.Prepared = true

Set RSDescripcionPagina = RSDescripcionPagina_cmd.Execute
RSDescripcionPagina_numRows = 0

'if para validar que hay datos
if NOT RSDescripcionPagina.EOF then'RSDescripcionPagina
descripcion = RSDescripcionPagina.Fields.Item("descripcion").Value
end if'RSDescripcionPagina
    %>

    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" valign="top" bgcolor="#FFFFFF">
        <tr>
            <td>
                <!-- InstanceBeginEditable name="EditRegion1" -->
                &nbsp;
&nbsp;&nbsp;
&nbsp;
                <p align="center">Seleccione el año para mostrar las Claves usadas del SAT</p>
                <form action="reporteSAT.aspx" method="post" name="form1" target="_blank" id="form1">
                    <p>&nbsp;</p>
                    <table width="36%" border="0" align="center">

                        <tr>
                            <td align="right">Año: </td>
                            <td>
                                <select name="year" id="year" style="width: 300px" class="selectColor">
                                    <option value="">Todos</option>
                                    <%
While (NOT Recordset1.EOF)
                                    %>
                                    <option value="<%=(Recordset1.Fields.Item("año").Value)%>"><%=(Recordset1.Fields.Item("año").Value)%></option>
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
                            <td colspan="3" align="center">
                                <label>
                                    <input type="button" name="button" id="button" value="Enviar" onclick="param()" />
                                </label>
                            </td>
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
<!-- InstanceEnd -->
</html>
