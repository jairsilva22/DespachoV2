<!--#include file="Connections/conecta1.asp" -->
<!--#include file="config.asp"-->
<!--#include file="checklogin.asp"-->
<%
Dim Recordset3
Dim Recordset3_cmd
Dim Recordset3_numRows

Set Recordset3_cmd = Server.CreateObject ("ADODB.Command")
Recordset3_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset3_cmd.CommandText = "SELECT DISTINCT(YEAR(fechasellado)) AS fecha FROM dbo.factura WHERE idfactura > 0 AND tipo_comprobante = 2 AND fechasellado <> '' ORDER BY fecha DESC"
Recordset3_cmd.Prepared = true

Set Recordset3 = Recordset3_cmd.Execute

Set Recordset4_cmd = Server.CreateObject ("ADODB.Command")
Recordset4_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset4_cmd.CommandText = "SELECT idDocumento, descripcion FROM dbo.documento WHERE descripcion <> 'Factura' ORDER BY descripcion"
Recordset4_cmd.Prepared = true

Set Recordset4 = Recordset4_cmd.Execute
%>
<!DOCTYPE html>
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
    <link rel="stylesheet" href="css.css" type="text/css" media="screen" />
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

        .selectColor {
            background-color: white;
            color: black;
        }
        -->
    </style>
    <!-- InstanceBeginEditable name="head" -->


    <!-- InstanceEndEditable -->
</head>

<body style="background-color: white;">
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
        <tr>
            <td>
                <!-- InstanceBeginEditable name="EditRegion1" -->
                &nbsp;
&nbsp;&nbsp;
&nbsp;
                <p align="center">&nbsp;</p>
                <form method="GET" name="form1" target="_blank" action="reporteNotasDeCredito.asp">
                    <table width="10%" border="0" align="center">
                        <tr>
                            <td align="center">Tipo de documento: </td>
                            <td align="center">
                                <select name="documento" id="documento" class="selectColor">
                                    <%While(Not Recordset4.EOF)%>
                                    <option value="<%=Recordset4.Fields.Item("iddocumento").Value%>"><%=Recordset4.Fields.Item("descripcion").Value%></option>
                                    <%  Recordset4.MoveNext
          Wend%>
                                </select>
                            </td>
                        </tr>

                        <tr>
                            <td align="center">Año: </td>
                            <td align="center">
                                <select name="anio" id="anio" class="selectColor">
                                    <%While(Not Recordset3.EOF)%>
                                    <option value="<%=Recordset3.Fields.Item("fecha").Value%>"><%=Recordset3.Fields.Item("fecha").Value%></option>
                                    <%  Recordset3.MoveNext
          Wend%>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center">&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center">
                                <button type="submit" value="Enviar">Enviar</button></td>
                        </tr>
                    </table>
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
