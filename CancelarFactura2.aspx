<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CancelarFactura2.aspx.cs" Inherits="despacho.CancelarFacturas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style>
        button {
            border-radius: 10px;
            height: 30px;
            padding: 6px 12px;
            margin-bottom: 0;
            font-size: 14px;
            font-weight: normal;
            line-height: 1.42857143;
            text-align: center;
            white-space: nowrap;
            vertical-align: middle;
            -ms-touch-action: manipulation;
            touch-action: manipulation;
            cursor: pointer;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
            background-image: none;
            border: 1px solid transparent;
            border-radius: 4px;
        }

        .aceptar {
            color: #fff;
            background-color: #5cb85c;
            border-color: #4cae4c;
        }

            .aceptar:hover {
                color: #fff;
                background-color: #449d44;
                border-color: #398439;
            }

        .cerrar {
            color: #fff;
            background-color: #d9534f;
            border-color: #d43f3a;
        }

            .cerrar:hover {
                color: #fff;
                background-color: #c9302c;
                border-color: #ac2925;
            }

        .loader {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background: url('imagenes/ajax-loader.gif') 50% 50% no-repeat rgb(249,249,249);
            opacity: .8;
        }
    </style>

    <script src="jsF/jquery.min.js" type="text/javascript"></script>
    <script>
        $(window).load(function () {
            $(".loader").fadeOut("slow");
        });

        function cerrarPopup() {
            window.parent.$("#cerrarModal").trigger("click");
            window.parent.form1.submit();
        }
    </script>
</head>
<body>
    <div class="loader"></div>
    <form id="form1" runat="server">
        <p>&nbsp;</p>
        <h2 align="center">
            <strong>Canelar Factura
            </strong>
        </h2>
        <table>
            <tr>
                <td align="right"><strong>Folio:</strong></td>
                <td>
                    <asp:Label ID="lblFolio" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td align="right"><strong>Cliente:</strong></td>
                <td>
                    <asp:Label ID="lblCliente" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td align="right"><strong>RFC:</strong></td>
                <td>
                    <asp:Label ID="lblRfc" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td align="right"><strong>UUID:</strong></td>
                <td>
                    <asp:Label ID="lblUUID" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td align="right"><strong>Total:</strong></td>
                <td>$
                    <asp:Label ID="lblTotal" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td align="right"><strong>Fecha:</strong></td>
                <td>
                    <asp:Label ID="lblFecha" runat="server" Text="Label"></asp:Label></td>
            </tr>
        </table>
        <p style="text-align: center; font-size: x-large;"><strong>Desea cancelar este CFDI?</strong></p>
        <div style="text-align: center;">
            <button id="btnAceptar" runat="server" class="aceptar" onserverclick="btnAceptar_ServerClick">SI</button>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <button id="btnCerrar" type="button" runat="server" class="cerrar" onclick="cerrarPopup();">NO</button>
        </div>
    </form>
</body>
</html>
