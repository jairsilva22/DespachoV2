<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="reporteVentasPorProductosDespacho.aspx.cs" Inherits="despacho.reporteVentasPorProductosDespacho" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Reporte de Ventas por Productos</title>
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
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/jquery.js"></script>
    <script src="js/AG-Grid/ag-grid-enterprise.js"></script>
    <link href="css/reportesAgGrid.css" rel="stylesheet" />
    <script src="js/AG-Grid/reporteVpPDespacho.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="loader-wrapper">
            <span class="loader"></span>
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div style="width: 95%; text-align: center;">
            <table width="80%" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr align="center" valign="bottom">
                    <td colspan="2" valign="middle" class="titulos">
                        <div style="display: flex; justify-content: space-between" runat="server">
                            <div id="imagen" runat="server"></div>
                            <div>
                                <div class="text-center" style="margin-bottom: 10px">
                                    <asp:Label ID="lblReporte" runat="server">Reporte de Ventas por Producto</asp:Label>
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
                </tr>

                <tr class="stylo1">
                    <td colspan="2"></td>
                </tr>
            </table>
            <div class="col-md-12">
                <input type="text" id="filter-text-box" class="form-control" placeholder="Buscar..." oninput="onFilterTextBoxChanged()" autocomplete="off" />
                <br />
                <div id="myGrid" style="height: 800px;" class="ag-theme-alpine col-md-12"></div>
            </div>
            <div class="col-md-12">
                <table class="table">
                    <tr>
                        <td colspan="5"></td>
                        <td><span class="titulos">Total General</span></td>
                        <td><asp:Label runat="server" ID="lblDescuentos" CssClass="size20" Text=""></asp:Label></td>
                        <td><asp:Label runat="server" ID="lblTotal" CssClass="size20" Text=""></asp:Label></td>
                    </tr>
                </table>
            </div>
            <div style="display: none;">
                <asp:ListView ID="lvCliente" runat="server">
                <LayoutTemplate>
                    <table width="100%" border="1" cellpadding="0" cellspacing="0" class="">
                        <tr align="center" bgcolor="#e33045" style="color: white" class="stylo1">

                            <th style="text-align: center" id="Codigo Producto">Codigo Producto</th>
                            <th style="text-align: center" id="Nombre(productos, servicios, Paquetes)">Nombre(productos, servicios, Paquetes)</th>
                            <th style="text-align: center" id="Cantidad">Cantidad</th>
                            <%--<th style="text-align: center" id="Unidad">Unidad</th>--%>
                            <%--<th style="text-align: center" id="Neto">Neto</th>--%>
                            <th style="text-align: center" id="Descuento">Descuento</th>
                            <%--<th style="text-align: center" id="Neto-Desc">Neto-Desc</th>--%>
                            <%--<th style="text-align: center" id="Impuesto">Impuesto</th>--%>
                            <th style="text-align: center" id="Total">Total</th>
                        </tr>
                        <tr id="itemPlaceholder" runat="server"></tr>
                    </table>
                </LayoutTemplate>
                <EmptyDataTemplate>
                    <table width="100%" border="1" cellpadding="0" cellspacing="0">
                        <tr align="center" bgcolor="#e33045" style="color: white" class="stylo1">

                            <th style="text-align: center" id="Codigo Producto">Codigo Producto</th>
                            <th style="text-align: center" id="Nombre(productos, servicios, Paquetes)">Nombre(productos, servicios, Paquetes)</th>
                            <th style="text-align: center" id="Cantidad">Cantidad</th>
                           <%-- <th style="text-align: center" id="Unidad">Unidad</th>--%>
                           <%-- <th style="text-align: center" id="Neto">Neto</th>--%>
                            <th style="text-align: center" id="Descuento">Descuento</th>
                            <%--<th style="text-align: center" id="Neto-Desc">Neto-Desc</th>--%>
                            <%--<th style="text-align: center" id="Impuesto">Impuesto</th>--%>
                            <th style="text-align: center" id="Total">Total</th>
                        </tr>
                        <tr>
                            <td colspan="10" align="center">Sin Resultados
                            </td>

                        </tr>
                    </table>
                </EmptyDataTemplate>
                <ItemTemplate>
                    <tr>
                        <td style="text-align: center"><%# Eval("CCODIGOPRODUCTO").ToString() %></td>
                        <td style="text-align: center"><%# Eval("CNOMBREPRODUCTO").ToString() %></td>
                        <td style="text-align: center"><%# Eval("CUnidades").ToString() %></td>
                       <%-- <td style="text-align: center"><%# Eval("CTIPOPRODUCTO").ToString() %></td>--%>
                       <%-- <td style="text-align: center"><%# Eval("CNETO").ToString() %></td>--%>
                        <td style="text-align: center"><%# Eval("CDESCUENTO1").ToString() %></td>
                        <%--<td style="text-align: center"><%# Eval("Neto-Desc").ToString() %></td>--%>
                        <%--<td style="text-align: center"><%# Eval("CIMPUESTO1").ToString() %></td>--%>
                        <td style="text-align: center"><%# Eval("CTOTAL").ToString() %></td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
            </div>
            <asp:Label runat="server" ID="labelError"></asp:Label>
        </div>
        <script>
            function onFilterTextBoxChanged() {
                gridOptions.api.setQuickFilter(
                    document.getElementById('filter-text-box').value
                );
            }

            function llenarAgGrid(res) {
                const rowDataB = res;

                $(document).ready(function () {
                    gridOptions.api.setRowData(rowDataB);
                    var alto = 42 * rowDataB.length + 70;
                    document.getElementById("myGrid").style.height = alto + "px";
                    document.getElementById("myGrid").style.maxHeight = "600px";
                    document.getElementById("myGrid").style.minHeight = "100px";

                });

            }
        </script>
        <script src="js/loader/loader.js"></script>
    </form>
</body>
</html>
