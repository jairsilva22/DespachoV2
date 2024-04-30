<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="reporteVentasGeneralesCobranzaDespacho.aspx.cs" Inherits="despacho.reporteVentasGeneralesCobranzaDespacho" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Reporte de Antiguedad de Saldos</title>
    <!-- bootstrap CSS -->
    <link href="css/bootstrap.min.css" rel="stylesheet" media="screen" />

    <!-- Main CSS -->
    <link href="css/main.css" rel="stylesheet" media="screen" />

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
    <!-- Ag Grid -->
    <!-- Include the JS for AG Grid -->
    <script src="js/AG-Grid/ag-grid-enterprise.js"></script>
    <script src="js/AG-Grid/reporteVentasGeneralesCobranza.js"></script>

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
        body {
            background: white;
        }
    </style>
    <link href="css/reportesAgGrid.css" rel="stylesheet" />
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
                                    <asp:Label ID="lblReporte" runat="server">Saldos de Cobranza Despacho</asp:Label>
                                </div>
                                <div class="text-center" style="margin-bottom: 10px">
                                    <asp:Label ID="lblFechaFin" runat="server"><strong>Fecha de Corte: </strong></asp:Label>
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
                <div id="myGrid" style="height: 200px;" class="ag-theme-alpine col-md-12"></div>
            </div>
            <div class="col-md-12">
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="2"></td>
                        <td><span class="titulos"><strong>Total General</strong></span></td>
                        <td>
                            <asp:Label runat="server" ID="lblTotalVencido" CssClass="size20" Text=""></asp:Label></td>
                        <td>
                            <asp:Label runat="server" ID="lblTotalVencer" CssClass="size20" Text=""></asp:Label></td>
                        <td>
                            <asp:Label runat="server" ID="lblTotal" CssClass="size20" Text=""></asp:Label></td>
                    </tr>
                </table>
            </div>
            <asp:ListView ID="lvCliente" runat="server">
                <LayoutTemplate>
                    <table width="100%" border="1" cellpadding="0" cellspacing="0" class="">
                        <tr align="center" bgcolor="#e33045" style="color: white" class="stylo1">
                            <th style="text-align: center" id="Codigo">Codigo</th>
                            <th style="text-align: center" id="Nombre">Nombre(Cliente)</th>
                            <th style="text-align: center" id="TotalVencido">Nombre(Agente)</th>
                            <th style="text-align: center" id="Vendedor">Total Vencido</th>
                            <th style="text-align: center" id="Vencimiento">Total por Vencer</th>
                            <th style="text-align: center" id="Total">Total</th>
                        </tr>
                        <tr id="itemPlaceholder" runat="server"></tr>
                    </table>
                </LayoutTemplate>
                <EmptyDataTemplate>
                    <table width="100%" border="1" cellpadding="0" cellspacing="0">
                        <tr align="center" bgcolor="#e33045" style="color: white" class="stylo1">
                            <th style="text-align: center" id="Codigo">Codigo</th>
                            <th style="text-align: center" id="Nombre">Nombre(Cliente)</th>
                            <th style="text-align: center" id="TotalVencido">Nombre(Agente)</th>
                            <th style="text-align: center" id="Vendedor">Total Vencido</th>
                            <th style="text-align: center" id="Vencimiento">Total por Vencer</th>
                            <th style="text-align: center" id="Total">Total</th>
                        </tr>
                        <tr>
                            <td colspan="5" align="center">Sin Resultados
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <ItemTemplate>
                    <tr>
                        <td style="text-align: center"><%# Eval("Codigo").ToString() %></td>
                        <td style="text-align: center"><%# Eval("Nombre").ToString() %></td>
                        <td style="text-align: center"><%# Eval("Agente").ToString() %></td>
                        <td style="text-align: center"><%# Eval("TotalVencido").ToString() %></td>
                        <td style="text-align: center"><%# Eval("TotalporVencer").ToString() %></td>
                        <td style="text-align: center"><%# Eval("Total").ToString() %></td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
        </div>
        <div>
        </div>
        <script>
            function onFilterTextBoxChanged() {
                gridOptions.api.setQuickFilter(
                    document.getElementById('filter-text-box').value
                );
            }

            function llenarAgGrid(res) {
                const rowDataB = res;
                //console.log(rowDataB.length);
                $(document).ready(function () {
                    gridOptions.api.setRowData(rowDataB);
                    var alto = 42 * rowDataB.length + 70;
                    document.getElementById("myGrid").style.height = alto + "px";
                    document.getElementById("myGrid").style.maxHeight = "600px";
                    //let jsonString = JSON.stringify(rowDataB);
                    //console.log(jsonString);
                });
                //gridOptions.api.setRowData(rowDataB);
            }
        </script>
        <script src="js/loader/loader.js"></script>
    </form>
</body>
</html>