<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="reporteProduccionTicket.aspx.cs" Inherits="despacho.reporteProduccionTicket" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Reporte de Producción por Ticket</title>
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
                                    <asp:Label ID="lblReporte" runat="server">Reporte de Produccion por Ticket</asp:Label>
                                </div>
                                <div class="text-center" style="margin-bottom: 10px">
                                    <asp:Label ID="lblFolio" runat="server"></asp:Label>
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
            <asp:ListView ID="lvTicket" runat="server">
                <LayoutTemplate>
                    <table width="100%" border="1" cellpadding="0" cellspacing="0" class="">
                        <tr align="center" bgcolor="#e33045" style="color: white" class="stylo1">
                            <th style="text-align: center" id="Ticket">Ticket</th>
                            <th style="text-align: center" id="Fecha">Fecha</th>
                            <th style="text-align: center" id="Orden">Orden</th>
                            <th style="text-align: center" id="Camion">Camion</th>
                            <th style="text-align: center" id="Planta">Planta</th>
                            <th style="text-align: center" id="Cantidad">Cantidad</th>
                            <th style="text-align: center" id="Unidad">Unidad</th>
                            <th style="text-align: center" id="Impresion">Impresion</th>
                            <th style="text-align: center" id="Cliente">Cliente</th>
                            <th style="text-align: center" id="Conductor">Conductor</th>
                            <th style="text-align: center" id="Mezcla">Mezcla</th>
                        </tr>
                        <tr id="itemPlaceholder" runat="server"></tr>
                    </table>
                </LayoutTemplate>
                <EmptyDataTemplate>
                    <table width="100%" border="1" cellpadding="0" cellspacing="0">
                        <tr align="center" bgcolor="#e33045" style="color: white" class="stylo1">
                            <th style="text-align: center" id="Ticket">Ticket</th>
                            <th style="text-align: center" id="Fecha">Fecha</th>
                            <th style="text-align: center" id="Orden">Orden</th>
                            <th style="text-align: center" id="Camion">Camion</th>
                            <th style="text-align: center" id="Planta">Planta</th>
                            <th style="text-align: center" id="Cantidad">Cantidad</th>
                            <th style="text-align: center" id="Unidad">Unidad</th>
                            <th style="text-align: center" id="Impresion">Impresion</th>
                            <th style="text-align: center" id="Cliente">Cliente</th>
                            <th style="text-align: center" id="Conductor">Conductor</th>
                            <th style="text-align: center" id="Mezcla">Mezcla</th>
                        </tr>
                        <tr>
                            <td colspan="8" align="center">Sin Resultados
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <ItemTemplate>
                    <tr>
                        <td style="text-align: center"><%# Eval("id") %></td>
                        <td style="text-align: center"><%# Eval("fecha").ToString().Substring(0, 10) %></td>
                        <td style="text-align: center"><%# Eval("idOrden") %></td>
                        <td style="text-align: center"><%# nombreUnidad(int.Parse(Eval("idUnidadTransporte").ToString())) %></td>
                        <td style="text-align: center"><%# Eval("idSucursal") %></td>
                        <td style="text-align: center"><%# Eval("Cantidad")%></td>
                        <td style="text-align: center"><%# medidaProducto(int.Parse(Eval("idUDM").ToString())) %></td>
                        <td style="text-align: center"><%# Eval("hora") %></td>
                        <td style="text-align: center"><%# nombreCliente(int.Parse(Eval("idCliente").ToString())) %></td>
                        <td style="text-align: center"><%# choferUnidad(int.Parse(Eval("idUnidadTransporte").ToString())) %></td>
                        <td style="text-align: center"><%# codigoProducto(int.Parse(Eval("idProducto").ToString())) %></td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
            <asp:ListView ID="lvTotal" runat="server">
                <LayoutTemplate>
                    <table width="100%" border="1" cellpadding="0" cellspacing="0" class="">
                        <tr align="center" bgcolor="#e33045" style="color: white" class="stylo1">
                        </tr>
                        <tr id="itemPlaceholder" runat="server"></tr>
                </LayoutTemplate>
                <EmptyDataTemplate>
                    <table width="100%" border="1" cellpadding="0" cellspacing="0" class="">
                        <tr align="center" bgcolor="#e33045" style="color: white" class="stylo1">
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <ItemTemplate>
                    <tr>
                        <td style="text-align: center">Total</td>
                        <td style="text-align: center"></td>
                        <td style="text-align: center"></td>
                        <td style="text-align: center"></td>
                        <td style="text-align: center"></td>
                        <td style="text-align: center"><%# obtenerTotal()%></td>
                        <td style="text-align: center"></td>
                        <td style="text-align: center"></td>
                        <td style="text-align: center"></td>
                        <td style="text-align: center"></td>
                        <td style="text-align: center"></td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
        </div>
        <asp:TextBox ID="txtTotal" runat="server" Visible="false"></asp:TextBox>
    </form>
</body>
</html>
