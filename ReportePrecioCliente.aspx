<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportePrecioCliente.aspx.cs" Inherits="despacho.ReportePrecioCliente" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Reporte de Precio Cliente</title>
    <style type="text/css">
        <!--
        .titulos {
            font-size: 20px;
            font-weight: bold;
        }

        .colorFondo {
            background-color: rgb(218,227,243);
        }

        table {
            table-layout: fixed;
            width: 100%;
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
                                    <asp:Label ID="lblReporte" runat="server">Reporte de Precio Cliente</asp:Label>
                                </div>
                                <div class="text-center" style="margin-bottom: 10px">
                                    <asp:Label ID="lblFolio" runat="server"></asp:Label>
                                </div>
                                <div class="text-center" style="margin-bottom: 10px">
                                    <asp:Label ID="lblClienteInicio" runat="server"><strong>Del Cliente: </strong></asp:Label>
                                    <asp:Label ID="lblClienteFin" runat="server"><strong>Hasta: </strong></asp:Label>
                                </div>
                                <div class="text-center" style="margin-bottom: 10px">
                                    <asp:Label ID="lblProyectoInicio" runat="server"><strong>Del Proyecto: </strong></asp:Label>
                                    <asp:Label ID="lblProyectoFin" runat="server"><strong>Hasta: </strong></asp:Label>
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
            <asp:ListView ID="lvPrecio" runat="server">
                <LayoutTemplate>
                    <table width="100%" border="1" cellpadding="0" cellspacing="0" class="" style="margin-bottom: 15px">
                        <%--<tr align="center" bgcolor="#e33045" style="color: white" class="stylo1">--%>
                        <%--<th style="text-align: center" id="Fecha">Fecha</th>
                            <th style="text-align: center" id="Sucursal">Planta</th>
                            <th style="text-align: center" id="TipoProducto">Tipo de producto</th>
                            <th style="text-align: center" id="Cantidad">Cantidad</th>
                            <th style="text-align: center" id="Unidad">Unidad</th>
                            <th style="text-align: center" id="Monto">Monto</th>
                            <th style="text-align: center" id="IVA">IVA</th>
                            <th style="text-align: center" id="MontoTotal">Monto Total</th>--%>
                        <%--</tr>--%>
                        <tr id="itemPlaceholder" runat="server"></tr>
                    </table>
                </LayoutTemplate>
                <EmptyDataTemplate>
                    <table width="100%" border="1" cellpadding="0" cellspacing="0">
                        <tr>
                            <th>Cliente:</th>
                            <td style="text-align: center">Sin Resultados</td>
                        </tr>
                        <tr>
                            <th>Metodo de pago</th>
                            <td style="text-align: center">Sin Resultados</td>
                        </tr>
                        <tr>
                            <th>Porcentaje de descuento</th>
                            <td style="text-align: center">Sin Resultados</td>
                        </tr>
                        <tr>
                            <th>Monto de descuento</th>
                            <td style="text-align: center">Sin Resultados</td>
                        </tr>
                        <tr>
                            <th>Minimo carga entrega</th>
                            <th>Cantidad entregada</th>
                            <th>Carga</th>
                        </tr>
                        <tr>
                            <td style="text-align: center">Sin Resultados</td>
                            <td style="text-align: center">Sin Resultados</td>
                            <td style="text-align: center">Sin Resultados</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <ItemTemplate>
                    <table width="100%" border="1" cellpadding="0" cellspacing="0" class="" style="margin-bottom: 15px">
                        <tr>
                            <th>Cliente:</th>
                            <td style="text-align: center"><%# nombreCliente(int.Parse(Eval("id").ToString())) %></td>
                        </tr>
                        <tr>
                            <th>Metodo de pago:</th>
                            <td style="text-align: center"><%# formaDePago(int.Parse(Eval("idFormaPago").ToString())) %></td>
                        </tr>
                        <tr>
                            <th>Porcentaje de descuento:</th>
                            <td style="text-align: center">0.0000%</td>
                        </tr>
                        <tr>
                            <th>Monto de descuento:</th>
                            <td style="text-align: center">$<%# montoDescuento(int.Parse(Eval("id").ToString())) %></td>
                        </tr>
                        <tr>
                            <th>Minimo carga entrega</th>
                            <th>Cantidad entregada</th>
                            <th>Carga</th>
                        </tr>
                        <tr style="margin-bottom: 15px">
                            <td style="text-align: center">Sin carga</td>
                            <td style="text-align: center"><%# cantidadEntregada(int.Parse(Eval("id").ToString())) %></td>
                            <td style="text-align: center"><%# cantidadCargada(int.Parse(Eval("id").ToString())) %></td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:ListView>
        </div>
    </form>
</body>
</html>
