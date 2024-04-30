<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="detallesComExt.aspx.cs" Inherits="despacho.detallesComExt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script>
        function validar() {
            alert("Todos los campos son Obligatorios");
        }

        function salir() {
            var form = document.getElementById("form1");

            form.action = "factura.asp"
            //form.target = "_top";
            form.submit();
        }

        function esNumero(valor, control) {
            if (isNaN(valor)) {
                alert("El valor no es un numero!");
                foco(control);
            }
        }

        function fraccion(valor, control) {
            var form = document.getElementById("form1");
            var patron = new RegExp("^[0-9]{8}$");
            if (!patron.test(valor.trim())) {
                alert("El patrón de la Fraccion Arancelaria debe tener solo 8 numeros");
                control.focus();
            }
        }
    </script>
</head>
<body>
    <p>&nbsp;</p>
    <center><h2>Datos de Mercancía para Comercio Exterior</h2></center>
    <p>&nbsp;</p>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
                <asp:PostBackTrigger ControlID="btnGuardar" />
            </Triggers>
            <ContentTemplate>
                <asp:ListView ID="ListView1" runat="server">
                    <LayoutTemplate>
                        <table align="center" width="90%" cellpadding="5">
                            <tr align="center" bgcolor="#549BB1">
                                <td style="visibility: hidden">ID</td>
                                <td>nParte</td>
                                <td>No Identificacion</td>
                                <td>Cantidad Aduana</td>
                                <td>Unidad Aduana</td>
                                <td>Valor Unitario Aduana</td>
                                <td>Valor Dolares</td>
                                <td>Fraccion Arancelaria</td>
                            </tr>
                            <tr id="itemPlaceholder" runat="server"></tr>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td style="visibility: hidden">
                                <asp:Label ID="lblId" runat="server" Text='<%# Eval("id_detfactura") %>'></asp:Label></td>
                            <td><%# Eval("nParte") %></td>
                            <td>
                                <asp:TextBox ID="txtnoIdentificacion" runat="server"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="txtCantidad" runat="server" Text='<%# Eval("cantidad") %>' onChange="esNumero(this.value, this)"></asp:TextBox></td>
                            <td>
                                <asp:DropDownList ID="ddlUnidad" runat="server"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="txtValorUnitario" runat="server" onChange="esNumero(this.value, this)"
                                    Text='<%# Math.Round(double.Parse(Eval("precio_unitario").ToString()), 2) %>'></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="txtDls" runat="server" onChange="esNumero(this.value, this)"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="txtFracion" runat="server" onChange="fraccion(this.value, this)"></asp:TextBox></td>
                        </tr>
                    </ItemTemplate>
                </asp:ListView>
                <p>&nbsp;</p>
                <table align="center" width="center">
                    <tr>
                        <td>Total Mercancias: </td>
                        <td>$
                            <asp:Label ID="totalMerc" runat="server"></asp:Label></td>
                    </tr>
                </table>
                <table align="center" width="center">
                    <tr>
                        <td colspan="2" align="center">
                            <a href="detfacturaadd.asp?idfactura=<%= Request.QueryString["id"] %>&idempresa=<%= Request.QueryString["idempresa"] %>&idcliente=<%= Request.QueryString["idcliente"] %>"
                               >
                                <img src="imagenes/Arrow-right.png" width="32" height="32" border="0" />
                                Regresar
                            </a>
                        </td>
                        <td align="center" colspan="3">
                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />
                        </td>
                        <td align="left" colspan="3">
                            <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click">
                                Terminar <img src="imagenes/Arrow-left.png" width="32" height="32" border="0">
                            </asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:HiddenField ID="hdfTotal" runat="server" />
    </form>
</body>
</html>
