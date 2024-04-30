<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ComercioExtFactura.aspx.cs" Inherits="despacho.ComercioExtFactura" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script>
        function certOrig(valor) {
            if (valor == "0") {
                document.getElementById("numCertificadoOrigen").setAttribute("disabled", "true");
            }
            else {
                document.getElementById("numCertificadoOrigen").removeAttribute("disabled");
            }
        }

        function valida() {
            var form = document.getElementById("form1");

            if (form.txtNumRegIdTrib.value == "") {
                alert("Falta Registrar el NumRegIdTrib del Cliente");
                return false;
            }
            if (form.tipoOperacion.value == "") {
                alert("Falta Registrar el Tipo de Operación");
                return false;
            }
            if (form.clavePedimento.value == "") {
                alert("Falta Registrar la Clave de Pedimento");
                return false;
            }
            if (form.certificadoOrigen.value == "") {
                alert("Falta Registrar el Certificado de Origen");
                return false;
            }
            if (form.numeroExportadorConfiable.value == "") {
                alert("Falta Registrar el Número Exportador Confiable");
                return false;
            }
            if (form.incoterm.value == "") {
                alert("Falta Registrar el Incoterm");
                return false;
            }
            if (form.subdivision.value == "") {
                alert("Falta Registrar la Subdivisión");
                return false;
            }
            if (form.tipoCambio.value == "") {
                alert("Falta Registrar el Tipo de Cambio");
                return false;
            }
            if (form.totalUsd.value == "") {
                alert("Falta Registrar el Total en USD");
                return false;
            }
            else {
                return true;
            }
        }

        function numRegId(valor) {
            var form = document.getElementById("form1");

            if (form.hdfPais.value == "CAN") {
                //verificamos el patrón para el numTribIdReg
                var patron = new RegExp("^[0-9]{9}$");
                if (!patron.test(valor.trim())) {
                    alert("El patrón del NumRegIdTrib debe tener solo 9 numeros");
                    form.txtNumRegIdTrib.focus();
                }
            }

            if (form.hdfPais.value == "USA") {
                //verificamos el patrón para el numTribIdReg
                var patron = new RegExp("^[0-9]{9}$");
                if (!patron.test(valor.trim())) {
                    alert("El patrón del NumRegIdTrib debe tener solo 9 numeros");
                    form.txtNumRegIdTrib.focus();
                }
            }

            if (form.hdfPais.value == "MEX") {
                //verificamos el patrón para el numTribIdReg
                var patron = new RegExp("^[A-Z&Ñ]{3,4}[0-9]{2}(0[1-9]|1[012])(0[1-9]|[12][0-9]|3[01])[A-Z0-9]{2}[0-9A]/");
                if (!patron.test(valor.trim())) {
                    alert("El patrón del NumRegIdTrib debe tener entre 1 y 40 caracteres");
                    form.txtNumRegIdTrib.focus();
                }
            }
        }

        function esNumero(valor, control) {
            if (isNaN(valor)) {
                alert("El valor no es un numero!");
                foco(control);
            }
            else {
                if (valor != document.getElementById("hdfTotal").value.trim()) {
                    alert("El Total de los USD debe ser igual al total de la factura!");
                    foco(control);
                }
            }
        }

        function foco(idElemento) {
            document.getElementById(idElemento).focus();
        }

    </script>
</head>
<body>
    <p>&nbsp;</p>
    <center><h2>Datos Generales para Comercio Exterior</h2></center>
    <p>&nbsp;</p>
    <form id="form1" runat="server">
        <table align="center" width="90%">
            <tr>
                <td>NumRegIdTrib Cliente:</td>
                <td>
                    <asp:TextBox ID="txtNumRegIdTrib" runat="server" onChange="numRegId(this.value)"></asp:TextBox>
                    <asp:HiddenField ID="hdfPais" runat="server" />
                </td>
                <td>Motivo Traslado:</td>
                <td>
                    <asp:Label ID="motivoTras" runat="server" Text=""></asp:Label>
                    <asp:HiddenField ID="hdfMotivo" runat="server" />
                </td>
            </tr>
            <tr>
                <td>Tipo Operacion:</td>
                <td>
                    <asp:DropDownList ID="tipoOperacion" runat="server">
                        <asp:ListItem Value="" Text="Seleccionar"></asp:ListItem>
                        <asp:ListItem Value="A" Text="Exportación de Servicios"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Exportación"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>Clave de Pedimento:</td>
                <td>
                    <asp:TextBox ID="clavePedimento" runat="server" Text="A1" ReadOnly="true"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Certificado Origen:</td>
                <td>
                    <asp:DropDownList ID="certificadoOrigen" runat="server" onChange="certOrig(this.value)">
                        <asp:ListItem Value="" Text="Seleccionar"></asp:ListItem>
                        <asp:ListItem Value="0" Text="No Funge como certificado de origen"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Funge como certificado de origen"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>Numero Certificado Origen</td>
                <td>
                    <asp:TextBox ID="numCertificadoOrigen" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Numero Exportador Confiable:</td>
                <td>
                    <asp:TextBox ID="numeroExportadorConfiable" runat="server"></asp:TextBox></td>
                <td>Incoterm:</td>
                <td>
                    <asp:DropDownList ID="incoterm" runat="server">
                        <asp:ListItem Value="" Text="Seleccionar"></asp:ListItem>
                        <asp:ListItem Value="CFR" Text="COSTE Y FLETE (PUERTO DE DESTINO CONVENIDO)."></asp:ListItem>
                        <asp:ListItem Value="CIF" Text="COSTE, SEGURO Y FLETE (PUERTO DE DESTINO CONVENIDO)."></asp:ListItem>
                        <asp:ListItem Value="CPT" Text="TRANSPORTE PAGADO HASTA (EL LUGAR DE DESTINO CONVENIDO)."></asp:ListItem>
                        <asp:ListItem Value="CIP" Text="TRANSPORTE Y SEGURO PAGADO HASTA (EL LUGAR DE DESTINO CONVENIDO)."></asp:ListItem>
                        <asp:ListItem Value="DAF" Text="ENTRGADA EN FRONTERA (LUGAR CONVENIDO)."></asp:ListItem>
                        <asp:ListItem Value="DAP" Text="ENTREGADA EN LUGAR."></asp:ListItem>
                        <asp:ListItem Value="DAT" Text="ENTREGADA EN TERMINAL."></asp:ListItem>
                        <asp:ListItem Value="DES" Text="ENTREGADA SOBRE BUQUE (PUERTO DE DESTINO CONVENIDO)."></asp:ListItem>
                        <asp:ListItem Value="DEQ" Text="ENTREGADA EN MUELLE (PUERTO DE DESTINO CONVENIDO)."></asp:ListItem>
                        <asp:ListItem Value="DDU" Text="ENTREGADA DERECHOS NO PAGADOS (LUGAR DE DESTINO CONVENIDO)."></asp:ListItem>
                        <asp:ListItem Value="DDP" Text="ENTREGADA DERECHOS PAGADOS (LUGAR DE DESTINO CONVENIDO)."></asp:ListItem>
                        <asp:ListItem Value="EXW" Text="EN FÁBRICA (LUGAR CONVENIDO)."></asp:ListItem>
                        <asp:ListItem Value="FCA" Text="FRANCO TRANSPORTISTA (LUGAR DESIGNADO)."></asp:ListItem>
                        <asp:ListItem Value="FAS" Text="FRANCO AL COSTADO DEL BUQUE (PUERTO DE CARGA CONVENIDO)."></asp:ListItem>
                        <asp:ListItem Value="FOB" Text="FRANCO A BORDO (PUERTO DE CARGA CONVENIDO)."></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Subdivisión:</td>
                <td>
                    <asp:TextBox ID="subdivision" runat="server" Text="0"></asp:TextBox></td>
                <td>Observaciones:</td>
                <td>
                    <asp:TextBox ID="observaciones" runat="server" Columns="50" Rows="5" TextMode="MultiLine"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Tipo Cambio USD:</td>
                <td>
                    <asp:TextBox ID="tipoCambio" runat="server" OnChange="esNumero(this.value, 'tipoCambio')"></asp:TextBox></td>
                <td>Total USD</td>
                <td>
                    <asp:TextBox ID="totalUsd" runat="server" OnChange="esNumero(this.value, 'totalUsd')"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <a href="detfacturaadd.asp?idfactura=<%= Request.QueryString["id"] %>&idempresa=<%= Request.QueryString["idempresa"] %>&idcliente=<%= Request.QueryString["idcliente"] %>"
                        >
                        <img src="imagenes/Arrow-right.png" width="32" height="32" border="0" />
                        Regresar
                    </a>
                </td>
                <td colspan="2" align="center">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click"
                        OnClientClick="return valida()" /></td>
            </tr>
        </table>
        <asp:HiddenField ID="hdfTotal" runat="server" />
    </form>
</body>
</html>
