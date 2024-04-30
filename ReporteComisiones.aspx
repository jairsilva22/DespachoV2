<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="reporteComisiones.aspx.cs" Inherits="despacho.reporteComisiones" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Reporte de Comisiones</title>
    <style type="text/css">
        <!--
        .titulos {
            font-size: 20px;
            font-weight: bold;
        }

        .colorFondo {
            background-color: rgb(218,227,243);
        }
        -->
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 95%; text-align: center;">
            <table width="80%" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr align="center" valign="bottom">
                    <td colspan="2" valign="middle" class="titulos">
                        <div style="display: flex; justify-content: space-between" runat="server">
                            <div id="imagen" runat="server"></div>
                            <div>
                                <div class="text-center" style="margin-bottom: 10px">
                                    <asp:Label ID="lblReporte" runat="server">Reporte de Comisiones</asp:Label>
                                </div>
                                <div class="text-center" style="margin-bottom: 10px">
                                    <asp:Label ID="lblFolio" runat="server"></asp:Label>
                                </div>
                                <div class="text-center" style="margin-bottom: 10px">
                                    <asp:Label ID="lblVend" runat="server"><strong>Vendedor: </strong></asp:Label>
                                </div>
                                <div class="text-center" style="margin-bottom: 10px">
                                    <asp:Label ID="lblFechaInicio" runat="server"><strong>Fecha Inicio: </strong></asp:Label>
                                    <asp:Label ID="lblFechaFin" runat="server"><strong>Fecha Fin: </strong></asp:Label>
                                </div>
                            </div>
                            <div></div>
                        </div>
                    </td>
                </tr>
                <tr align="center" valign="bottom">
                    <td colspan="2" valign="middle" class="titulos">
                        <hr color="#e33045" />
                    </td>
                </tr>

                <tr align="center">
                    <td height="25" valign="bottom">
                        <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
                    </td>
                    <%--<td width="40%" valign="bottom">Fecha Impresion : <%= DateTime.Now.ToShortDateString() %></td>--%>
                </tr>

                <tr class="stylo1">
                    <td colspan="2"></td>
                </tr>
            </table>
            <asp:ListView ID="lvComisiones" runat="server">
                <LayoutTemplate>
                    <table width="100%" border="1" cellpadding="0" cellspacing="0" class="">
                        <tr align="center" bgcolor="#e33045" style="color: white" class="stylo1">
                            <th style="text-align: center" id="Orden">Orden</th>
                            <th style="text-align: center" id="FechaVenta">Fecha Venta</th>
                            <th style="text-align: center" id="Cliente">Cliente</th>
                            <th style="text-align: center" id="Tipo">Tipo</th>
                            <th style="text-align: center" id="Monto">Monto</th>
                            <th style="text-align: center" id="FechaPago">Fecha Pago</th>
                            <th style="text-align: center" id="FormaPago">Forma Pago</th>
                            <th style="text-align: center" id="Factura">Factura</th>
                            <th style="text-align: center" id="Comisión">Comisión</th>
                            <th style="text-align: center" id="FueradeFecha">Fuera de Fecha</th>

                        </tr>
                        <tr id="itemPlaceholder" runat="server"></tr>
                    </table>
                </LayoutTemplate>
                <EmptyDataTemplate>
                    <table width="100%" border="1" cellpadding="0" cellspacing="0">
                        <tr align="center" bgcolor="#e33045" style="color: white" class="stylo1">
                            <th style="text-align: center" id="OrdenE">Orden</th>
                            <th style="text-align: center" id="FechaVentaE">Fecha Venta</th>
                            <th style="text-align: center" id="ClienteE">Cliente</th>
                            <th style="text-align: center" id="TipoE">Tipo</th>
                            <th style="text-align: center" id="MontoE">Monto</th>
                            <th style="text-align: center" id="FormaPagoE">Forma Pago</th>
                            <th style="text-align: center" id="FacturaE">Factura</th>
                            <th style="text-align: center" id="ComisiónE">Comisión</th>
                            <th style="text-align: center" id="FueradeFechaE">Fuera de Fecha</th>
                        </tr>
                        <tr>
                            <td colspan="9" align="center">Sin Resultados
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <ItemTemplate>
                    <%# llenarTabla() %>
                    <tr> 
                        <%--<td style="text-align: center"><%# Eval("id") %></td>
                        <td style="text-align: center"><%# Eval("fecha").ToString().Substring(0, 10) %></td>
                        <td style="text-align: center"><%# Eval("nombre") %></td>
                        <td style="text-align: center"><%# tipo(int.Parse(Eval("idCliente").ToString())) %></td>
                        <td style="text-align: center">$<%# monto(int.Parse(Eval("idSolicitud").ToString())) %></td>
                        <td style="text-align: center"><%# fechaPago(int.Parse(Eval("idSolicitud").ToString())) %></td>
                        <td style="text-align: center"><%# formaPago(int.Parse(Eval("idSolicitud").ToString())) %></td>
                        <td style="text-align: center"><%# factura(int.Parse(Eval("idSolicitud").ToString())) %></td>
                        <td style="text-align: center">$<%# comision(int.Parse(Eval("idCliente").ToString()), int.Parse(Eval("idSolicitud").ToString())) %></td>
                        <td style="text-align: center"><%# fueraDeFecha(int.Parse(Eval("idSolicitud").ToString()), int.Parse(Eval("id").ToString())) %> días</td>--%>

                    </tr>
                </ItemTemplate>
            </asp:ListView>
            <div style="display:flex; justify-content: center;">
                <asp:ListView ID="lvTotales" runat="server">
                    <LayoutTemplate>
                        <table width="50%" border="1" cellpadding="0" cellspacing="0" class="">
                            <tr align="center" bgcolor="#e33045" style="color: white" class="stylo1">
                            </tr>
                            <tr id="itemPlaceholder" runat="server"></tr>
                    </LayoutTemplate>
                    <EmptyDataTemplate>
                        <table width="50%" border="1" cellpadding="0" cellspacing="0" class="">
                            <tr align="center" bgcolor="#e33045" style="color: white" class="stylo1">
                                <th style="text-align: center" id="SubtotalE">Subtotal</th>
                                <th style="text-align: center" id="IVAE">IVA</th>
                                <th style="text-align: center" id="TotalE">Total</th>
                                <th style="text-align: center" id="ComisionesE">Comisiones</th>
                            </tr>
                            <tr>
                                <td colspan="9" align="center">Sin Resultados
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr>
                            <th style="text-align: center" id="Orden" >Subtotal</th>
                            <td style="text-align: center">$<%# obtenerSubtotal() %></td>
                        </tr>
                        <tr>
                            <th style="text-align: center" id="IVA">IVA</th>
                            <td style="text-align: center" id="tdIva" runat="server">$<%# obtenerIVA()%></td>
                        </tr>
                        <tr>
                            <th style="text-align: center" id="Total">Total</th>
                            <td style="text-align: center">$<%# obtenerTotal() %></td>
                        </tr>
                        <tr>
                            <th style="text-align: center" id="Comisiones">Comisiones</th>
                            <td style="text-align: center">$<%# mostrarSumaComision() %> </td>
                        </tr>
                    </ItemTemplate>
                </asp:ListView>
            </div>

            <asp:TextBox ID="sumaCom" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtComison" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtSubtotal" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtTotal" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtIva" runat="server" Visible="false"></asp:TextBox>
    </form>
</body>
</html>
