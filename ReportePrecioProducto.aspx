<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportePrecioProducto.aspx.cs" Inherits="despacho.ReportePrecioProducto" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Reporte de Precio Producto</title>
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
                                    <asp:Label ID="lblReporte" runat="server">Reporte de Precio Producto</asp:Label>
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
                    <table width="100%" border="1" cellpadding="0" cellspacing="0" class="">
                        <tr align="center" bgcolor="#e33045" style="color: white" class="stylo1">
                            <th style="text-align: center" id="Categoria">Categoria</th>
                            <th style="text-align: center" id="SucurCódigosal">Codigo</th>
                            <th style="text-align: center" id="Item">Item Short</th>
                            <th style="text-align: center" id="Descripcion">Descripcion</th>
                            <th style="text-align: center" id="Precio">Precio Categoria</th>
                            <th style="text-align: center" id="Fecha">Fecha Efectiva</th>

                        </tr>
                        <tr id="itemPlaceholder" runat="server"></tr>
                    </table>
                </LayoutTemplate>
                <EmptyDataTemplate>
                    <table width="100%" border="1" cellpadding="0" cellspacing="0">
                        <tr align="center" bgcolor="#e33045" style="color: white" class="stylo1">
                            <th style="text-align: center" id="Categoria">Categoria</th>
                            <th style="text-align: center" id="Códigos">Codigo</th>
                            <th style="text-align: center" id="Item">Item Short</th>
                            <th style="text-align: center" id="Descripcion">Descripcion</th>
                            <th style="text-align: center" id="Precio">Precio</th>
                            <th style="text-align: center" id="Fecha">Fecha Efectiva</th>
                        </tr>
                        <tr>
                            <td colspan="8" align="center">Sin Resultados
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <ItemTemplate>
                    <tr>
                        <td style="text-align: center"><%# categoriaProducto(int.Parse(Eval("idProducto").ToString())) %></td>
                        <td style="text-align: center"><%# codigoProducto(int.Parse(Eval("idProducto").ToString())) %></td>
                        <td style="text-align: center"><%# tipoProducto(int.Parse(Eval("idProducto").ToString())) %></td>
                        <td style="text-align: center"><%# descProducto(int.Parse(Eval("idProducto").ToString())) %></td>
                        <td style="text-align: center">$<%# precioProducto(int.Parse(Eval("idProducto").ToString())) %></td>
                        <td style="text-align: center"></td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
        </div>
    </form>
</body>
</html>
