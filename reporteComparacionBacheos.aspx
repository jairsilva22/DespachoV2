<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="reporteComparacionBacheos.aspx.cs" Inherits="despacho.reporteComparacionBacheos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Reporte de Comparación de Bacheos</title>
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
        <div style="width: 100%; text-align: center;">
            <table width="80%" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr align="center" valign="bottom">
                    <td colspan="2" valign="middle" class="titulos">
                        <div style="display: flex; justify-content: space-between" runat="server">
                            <div id="imagen" runat="server"></div>
                            <div>
                                <div class="text-center" style="margin-bottom: 10px">
                                    <asp:Label ID="lblReporte" runat="server">Reporte de Comparacion de Bacheos</asp:Label>
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
            <asp:ListView ID="lvBacheos" runat="server">
                <LayoutTemplate>
                    <table width="100%" border="1" cellpadding="0" cellspacing="0" style="font-size:80%">
                        <tr align="center" bgcolor="#e33045" style="color: white" class="stylo1">
                            <th style="text-align: center" id="Orden">Orden</th>
                            <th style="text-align: center" id="Ticket">Ticket</th>
                            <th style="text-align: center" id="Planta">Planta</th>
                            <th style="text-align: center" id="Camion">Camion</th>
                            <th style="text-align: center" id="Cliente">Cliente</th>
                            <th style="text-align: center" id="Entregar" colspan="2">Entregar en</th>
                            <th style="text-align: center" id="Producto" colspan="2">Producto</th>
                            <th style="text-align: center" id="Descripcion" colspan="3">Descripcion</th>
                            <th style="text-align: center" id="Carga">Carga@</th>
                            <th style="text-align: center" id="Slump">Slump</th>
                            <th style="text-align: center" id="Cantidad">Cantidad</th>
                            <th style="text-align: center" id="Target">Target</th>
                            <th style="text-align: center" id="Actual">Actual</th>
                            <th style="text-align: center" id="Varianza">Varianza</th>
                            <th style="text-align: center" id="Var">%Var</th>
                        </tr>
                        <tr id="itemPlaceholder" runat="server"></tr>
                    </table>
                </LayoutTemplate>
                <EmptyDataTemplate>
                    <table width="100%" border="1" cellpadding="0" cellspacing="0">
                        <tr align="center" bgcolor="#e33045" style="color: white" class="stylo1">
                            <th style="text-align: center" id="Orden">Orden</th>
                            <th style="text-align: center" id="Ticket">Ticket</th>
                            <th style="text-align: center" id="Planta">Planta</th>
                            <th style="text-align: center" id="Camion">Camion</th>
                            <th style="text-align: center" id="Cliente">Cliente</th>
                            <th style="text-align: center" id="Entregar" colspan="2">Entregar en</th>
                            <th style="text-align: center" id="Producto">Producto</th>
                            <th style="text-align: center" id="Descripcion">Descripcion</th>
                            <th style="text-align: center" id="Carga">Carga@</th>
                            <th style="text-align: center" id="Slump">Slump</th>
                            <th style="text-align: center" id="Cantidad">Cantidad</th>
                            <th style="text-align: center" id="Target">Target</th>
                            <th style="text-align: center" id="Actual">Actual</th>
                            <th style="text-align: center" id="Varianza">Varianza</th>
                            <th style="text-align: center" id="Var">%Var</th>
                        </tr>
                        <tr>
                            <td colspan="8" align="center">Sin Resultados
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <ItemTemplate>
                    <tr>
                        <td style="text-align: center"><%# Eval("idOrden")%></td>
                        <td style="text-align: center"><%# Eval("Ticket")%></td>
                        <td style="text-align: center"><%# Eval("idSucursal") %></td>
                        <td style="text-align: center"><%# nombreUnidad(int.Parse(Eval("idUnidadTransporte").ToString())) %></td>
                        <td style="text-align: center"><%# nombreCliente(int.Parse(Eval("idCliente").ToString())) %></td>
                        <td style="text-align: center" colspan="2"><%# direccionCliente(int.Parse(Eval("idCliente").ToString())) %></td>
                        <td style="text-align: center" colspan="2"><%# codigoProducto(int.Parse(Eval("idProducto").ToString())) %></td>
                        <td style="text-align: center" colspan="3"><%# descProducto(int.Parse(Eval("idProducto").ToString())) %></td>
                        <td style="text-align: center"><%# Eval("hora") %></td>
                        <td style="text-align: center"></td>
                        <td style="text-align: center"><%# medidaProducto(Eval("Cantidad").ToString(),int.Parse(Eval("idUDM").ToString())) %></td>
                        <td style="text-align: center"><%# medidaProducto(Eval("Cantidad").ToString(),int.Parse(Eval("idUDM").ToString())) %></td>
                        <td style="text-align: center"><%# medidaProducto(Eval("Cantidad").ToString(),int.Parse(Eval("idUDM").ToString())) %></td>
                        <td style="text-align: center">0</td>
                        <td style="text-align: center">0</td>
                    </tr>
                    <tr>
                        <td style="text-align: center"></td>
                        <td style="text-align: center"></td>
                        <td style="text-align: center"></td>
                        <td style="text-align: center"></td>
                        <td style="text-align: center"></td>
                        <td style="text-align: center" colspan="2"></td>
                        <td style="text-align: center" colspan="2"><%# formulacionMaterial(int.Parse(Eval("idProducto").ToString())) %></td>
                        <td style="text-align: center" colspan="3"><%# descMaterial(int.Parse(Eval("idProducto").ToString())) %></td>
                        <td style="text-align: center"></td>
                        <td style="text-align: center"></td>
                        <td style="text-align: center;"></td>
                        <td style="text-align: center"><%#  medidaMaterial(float.Parse(Eval("Cantidad").ToString()),int.Parse(Eval("idProducto").ToString()))  %></td>
                        <td style="text-align: center"><%# actualTicket(int.Parse(Eval("Ticket").ToString()), int.Parse(Eval("idProducto").ToString())) %></td>
                        <td style="text-align: center"><%# obtenerVarianza(float.Parse(Eval("Cantidad").ToString()),int.Parse(Eval("idProducto").ToString()),int.Parse(Eval("Ticket").ToString())) %></td>
                        <td style="text-align: center"><%# porcentajeVarianza(float.Parse(Eval("Cantidad").ToString()),int.Parse(Eval("idProducto").ToString()),int.Parse(Eval("Ticket").ToString())) %></td>
                    </tr>
                    <tr>
                        <td style="text-align: center" colspan="3">Codigo de planta: <%# Eval("idSucursal") %></td>
                        <td style="text-align: center"></td>
                        <td style="text-align: center"></td>
                        <td style="text-align: center" colspan="2"></td>
                        <td style="text-align: center" colspan="2"></td>
                        <td style="text-align: center" colspan="3"></td>
                        <td style="text-align: center"></td>
                        <td style="text-align: center"></td>
                        <td style="text-align: center;"></td>
                        <td style="text-align: center" colspan="4">Fecha Orden: <%#  fechaOrden(int.Parse(Eval("idOrden").ToString()))  %></td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
           <%-- <asp:ListView ID="lvTotal" runat="server">
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
                        <td style="text-align: center"><%# obtenerTotal()%></td>
                        <td style="text-align: center"></td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>--%>
        </div>
        <asp:TextBox ID="txtTotal" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtNombreCliente" runat="server" Visible="false"></asp:TextBox>
    </form>
</body>
</html>

