<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="reporteVentasPorVendedorDespacho.aspx.cs" Inherits="despacho.reporteVentasPorVendedorDespacho" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Reporte de Ventas por Vendedor</title>
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
    <script src="js/AG-Grid/reporteVpV.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="loader-wrapper">
            <span class="loader"></span>
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <table width="80%" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr align="center" valign="bottom">
                    <td colspan="2" valign="middle" class="titulos">
                        <div style="display: flex; justify-content: space-between" runat="server">
                            <div id="imagen" runat="server"></div>
                            <div>
                                <div class="text-center" style="margin-bottom: 10px">
                                    <asp:Label ID="lblReporte" runat="server">Reporte de Ventas por Vendedor</asp:Label>
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
        <div style="width: 95%; text-align: center;">
            <div class="col-md-12">
                <input type="text" id="filter-text-box" class="form-control" placeholder="Buscar..." oninput="onFilterTextBoxChanged()" autocomplete="off" />
                <br />
                <div id="myGrid" style="height: 800px;" class="ag-theme-alpine col-md-12"></div>
            </div>
            <div class="col-md-12">
                <table class="table">
                    <tr>
                        <td colspan="1"><span class="titulos"><strong>Total General</strong></span></td>
                        <td><asp:Label runat="server" ID="lblTotalConcreto" CssClass="size20" Text=""></asp:Label></td>
                        <td><asp:Label runat="server" ID="lblTotalBlock" CssClass="size20" Text=""></asp:Label></td>
                        <td><asp:Label runat="server" ID="lblTotalConcretoD" CssClass="size20" Text=""></asp:Label></td>
                        <td><asp:Label runat="server" ID="lblTotalBlockD" CssClass="size20" Text=""></asp:Label></td>
                    </tr>
                </table>
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
