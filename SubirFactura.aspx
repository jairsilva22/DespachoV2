<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubirFactura.aspx.cs" Inherits="despacho.SubirFactura" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Subir Archivo</title>
    <script>
        function error(texto) {
            alert(texto);
        }
        function Terminar() {
            alert('Factura Cargada con Exito');
            window.opener.location.reload();
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center">
            <h1>
                <strong>Subir Archivo XML</strong>
            </h1>
            <p>&nbsp;</p>
            <p align="center">
                Seleccione un archivo .XML para agregarlo al Sistema
            </p>
            <p>&nbsp;</p>
            <asp:FileUpload ID="fuArchivo" runat="server" />
            <p>&nbsp;</p>
            <asp:Button ID="btnSubir" runat="server" Text="Subir Archivo" OnClick="btnSubir_Click" Style="height: 50px; width: 120px;" />
        </div>
    </form>
</body>
</html>
