<%@  language="VBSCRIPT" codepage="65001" %>
<!--#include file="Connections/Conecta1.asp" -->

<!DOCTYPE html>
<html>
<!-- InstanceBegin template="/Templates/plantillacfd.dwt.asp" codeOutsideHTMLIsLocked="false" -->
<script language="JavaScript" src="jsF/overlib_mini.js"></script>
<head>
    <script src="Scripts/jquery.min.js"></script>
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

        #clientes {
            height: 100px;
            width: 350px;
            overflow: auto;
        }
        -->
    </style>
    <script type="text/javascript">

        //funcion para cargar los clientes sin enviar toda la pagina
        function buscar() {
            var cliente = $("#cliente").val()

            $.post("oculto.asp", { elCliente: cliente }, function (data) {
                $("#clientes").html(data)
            })
        }
        function valida() {
            var form = document.getElementsByName("ok")
            var bndr = 0
            var i

            //hacermos un for para saber si algun radio fue seleccionado
            for (i = 0; i < form.length; i++) {
                if (form[i].checked) {
                    bndr++
                }
            }

            if (bndr > 0 || $("#nota").val() != "") {
                return true
            }
            else {
                alert("Datos incorrectos")
                return false
            }
        }
    </script>
    <!-- InstanceBeginEditable name="head" -->

    <style type="text/css">
        #form1 table tr td {
            font-weight: bold;
        }
    </style>
    <!-- InstanceEndEditable -->
</head>

<body style="background-color: white;">
    <table width="50%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
        <tr>
            <td>
                <!-- InstanceBeginEditable name="EditRegion1" -->
                <form id="form1" name="form1" method="post" action="reporteNotaCredito.asp">
                    <p align="center" style="margin-bottom: 15px">GENERAR REPORTE DE NOTA DE CREDITO</p>
                    <table width="100%" border="0" align="center">
                        <tr>
                            <td align="right">POR NUM DE N.C.</td>
                            <td align="center>
                                <label for="textfield"></label>
                                <input type="text" name="notaCredito" id="nota" /></td>
                        </tr>
                        <tr>
                            <td align="right">POR CLIENTE</td>
                            <td>
                                <input type="text" name="cliente" id="cliente">&nbsp;
                                <button onclick="buscar()" type="button" title="Buscar">
                                    <img src="img/search_add.png" width="20" height="20"></button>

                            </td>
                        </tr>
                        <tr align="center">
                            <td colspan="2">
                                <div id="clientes" style="width: 60%">&nbsp;</div>
                            </td>

                        </tr>
                        <tr >
                            <td>&nbsp;</td>
                            <td >
                                <input type="submit" name="button" id="button" value="Enviar" onclick="return valida()" /></td>
                        </tr>
                    </table>
                    <p>&nbsp;</p>
                    <p>&nbsp;</p>
                </form>
                <!-- InstanceEndEditable -->
                <p>&nbsp;</p>
                <%=footerPage%>
            </td>
        </tr>
    </table>

</body>
<!-- InstanceEnd -->
</html>
