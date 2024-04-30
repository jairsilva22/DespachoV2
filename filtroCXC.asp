<%@  language="VBSCRIPT" codepage="65001" %>
<!--#include file="Connections/Conecta1.asp" -->

<%
dim desactivar'Variable para desactivar el menu
dim sql'Variable para guardar el comando de la buesqueda
dim rutaf'Variable para guardar la ruta que se filtra

'Incializar variables

'/////////////////////////////////

Dim Recordset4
Dim Recordset4_cmd
Dim Recordset4_numRows

Set Recordset4_cmd = Server.CreateObject ("ADODB.Command")
Recordset4_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset4_cmd.CommandText = "SELECT DISTINCT Year(fechacfd) as yearemiso FROM dbo.factura ORDER BY Year(fechacfd) DESC"
Recordset4_cmd.Prepared = true

Set Recordset4 = Recordset4_cmd.Execute
Recordset4_numRows = 0

Set RecordsetMnda_cmd = Server.CreateObject ("ADODB.Command")
RecordsetMnda_cmd.ActiveConnection = MM_Conecta1_STRING
RecordsetMnda_cmd.CommandText = "SELECT * FROM dbo.moneda ORDER BY cMoneda"
RecordsetMnda_cmd.Prepared = true

Set RecordsetMnda = RecordsetMnda_cmd.Execute
%>
<!--#include file="config.asp"-->
<!--#include file="checklogin.asp"-->
<!--#include file="stylo2.asp" -->
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<!-- InstanceBegin template="/Templates/plantillacfd.dwt.asp" codeOutsideHTMLIsLocked="false" -->
    <script language="JavaScript" src="jsF/overlib_mini.js"></script>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="efectos/css/demos.css" media="screen" type="text/css">
    <link href="//maxcdn.bootstrapcdn.com/font-awesome/4.2.0/css/font-awesome.min.css" rel="stylesheet">
    <script type="text/javascript" src="efectos/js/menu-for-applications.js"></script>
    <!-- InstanceBeginEditable name="doctitle" -->
    <title><%=titlePage%></title>
    <link rel="stylesheet" href="jsF/css.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="jsF/bootstrap.css" />
    <script src="jsF/jquery.min.js" type="text/javascript"></script>
    <script src="jsF/bootstrap.min.js"></script>
    <!-- InstanceEndEditable -->
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
        -->
    </style>
    <script>
          var posicion_x; 
          var posicion_y; 
          posicion_x=(screen.width/2)-(500/2);
          posicion_y=(screen.height/2)-(500/2);

      function clientes()
      {
        open("clienteRC.asp", '', 'top='+posicion_y+', left='+posicion_x + ', width=800, height=650')
      }

      function borrar()
      {
        document.getElementById("cliente").innerHTML = ""
        document.getElementById("CTES").value = "TODOS"
      }
    </script>

    <!-- InstanceBeginEditable name="head" -->

    <!-- InstanceEndEditable -->
</head>
<body style="background-color:white">
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
       <tr>
           <td>
                <!-- InstanceBeginEditable name="EditRegion1" -->
                <form id="form1" name="form1" method="post" action="reporteCXC.asp">
                    <p>&nbsp;</p>
                    <p>&nbsp;</p>
                    <table width="20%" border="0" align="center">
                        <tr>
                            <td align="right">Buscar:</td>
                            <td>
                                <p>
                                    <label>
                                        <input <%If (CStr(Request.Form("RadioGroup1")) = CStr("Ruta")) Then Response.Write("checked=""checked""") : Response.Write("")%> name="RadioGroup1" type="radio" id="RadioGroup1_0" value="Ruta" checked="checked" onclick="filtro()" /> Todos</label>
                                    <label>
                                        <input <%If (CStr(Request.Form("RadioGroup1")) = CStr("Cliente")) Then Response.Write("checked=""checked""") : Response.Write("")%> type="radio" name="RadioGroup1" value="Cliente" id="RadioGroup1_1" onclick="filtro()" />
                                        Cliente</label>
                                    <br />
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">Cliente:</td>
                            <td>
                                <%
                                    if request.Form("RadioGroup1") = "Cliente" then
                                        op = "javascript:clientes()"
                                        stl = ""
                                    else
                                        op = ""
                                        stl = "style='opacity:0.5'"
                                    end if
                                    
                                %>
                                <a href="<%=op %>">
                                    <img src="imagenes/filefind.gif" width="20" height="20" alt="" <%=stl %> /></a>
                                <input type="hidden" name="CTES" id="CTES" value="TODOS" />
                                <div id="cliente" style="display: inline;"><strong><%=nombre%></strong></div>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <input type="submit" name="button" id="button" value="Enviar" /></td>
                        </tr>
                        <tr>
                            <td align="right">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                    <p>&nbsp;</p>
                    <p>&nbsp; </p>
                </form>
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
<%
Recordset4.Close()
Set Recordset4 = Nothing
%>
<script language="javascript">
    function filtro() {
        document.form1.action = "filtroCxc.asp";
        document.form1.submit();

    }
</script>
