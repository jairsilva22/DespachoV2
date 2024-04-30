<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="reporteCuentaCliente.aspx.cs" Inherits="despacho.reporteCuentaCliente" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Reporte de cuenta del cliente</title>

    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
    <!-- Ag Grid -->
    <script src="js/AG-Grid/ag-grid-enterprise.js"></script>
    <script src="Scripts/reporteECC.js"></script>
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
    <link href="css/reportesAgGrid.css" rel="stylesheet" />


</head>
<body>
    <form id="form1" runat="server">
        <div class="loader-wrapper">
            <span class="loader"></span>
        </div>

        <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
        <div style="width: 95%; text-align: center; display: none;" class="body-wrapper">
            <table width="80%" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr align="center" valign="bottom">
                    <td colspan="2" valign="middle" class="titulos">
                        <div style="display: flex; justify-content: space-between" runat="server">
                            <div id="imagen" runat="server"></div>
                            <div>
                                <div class="text-center" style="margin-bottom: 10px">
                                    <asp:Label ID="lblReporte" runat="server">Reporte de Estado de Cuenta del Cliente</asp:Label>
                                </div>
                                <div class="text-center" style="margin-bottom: 10px">
                                    <asp:Label ID="lblFechaInicio" runat="server"><strong>Fecha Inicio: </strong></asp:Label>
                                    <asp:Label ID="lblFechaFin" runat="server"><strong>Fecha Fin: </strong></asp:Label>
                                </div>
                                <div class="text-center" style="margin-bottom: 10px">
                                    <asp:Label ID="LabelNumeroCte" runat="server"><strong>Cliente: </strong></asp:Label>
                                    <asp:Label ID="LabelNombreCte" runat="server"><strong>Nombre: </strong></asp:Label>
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
            <div id="container row" style="display: none;" class="body-wrapper">

                <div id="myGrid" style="height: 200px;" class="ag-theme-alpine col-md-12"></div>
            </div>

            <asp:ListView ID="lvCliente" runat="server">
                <LayoutTemplate>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="tabla-like-aggrid">
                        <tr align="center" bgcolor="#e33045" style="color: white" class="stylo1">
                            <th style="text-align: center; display: none;" id="Fecha">Fecha</th>
                            <th style="text-align: center; display: none;" id="Serie">Serie</th>
                            <th style="text-align: center; display: none;" id="Folio">Folio</th>
                            <th style="text-align: center; display: none;" id="Concepto">Concepto</th>
                            <th style="text-align: center; display: none;" id="Cargos">Cargos</th>
                            <th style="text-align: center; display: none;" id="Abonos">Abonos</th>
                            <th style="text-align: center; display: none;" id="Documento">Documento</th>
                            <th style="text-align: center; display: none;" id="Vence">Vence</th>
                            <th style="text-align: center; display: none;" id="Ref">Referencia</th>

                        </tr>
                        <tr id="itemPlaceholder" runat="server"></tr>
                    </table>
                </LayoutTemplate>
                <EmptyDataTemplate>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr align="center" bgcolor="#e33045" style="color: white" class="stylo1">
                            <th style="text-align: center; display: none;" id="Fecha">Fecha</th>
                            <th style="text-align: center; display: none;" id="Serie">Serie</th>
                            <th style="text-align: center; display: none;" id="Folio">Folio</th>
                            <th style="text-align: center; display: none;" id="Concepto">Concepto</th>
                            <th style="text-align: center; display: none;" id="Cargos">Cargos</th>
                            <th style="text-align: center; display: none;" id="Abonos">Abonos</th>
                            <th style="text-align: center; display: none;" id="Documento">Documento</th>
                            <th style="text-align: center; display: none;" id="Vence">Vence</th>
                            <th style="text-align: center; display: none;" id="Ref">Referencia</th>
                        </tr>
                        <tr>
                            <td colspan="8" align="center">Sin Resultados
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <ItemTemplate>
                    <tr>
                        <td style="text-align: center; display: none;"><%# Eval("Fecha").ToString() %></td>
                        <td style="text-align: center; display: none;"><%# Eval("Serie").ToString() %></td>
                        <td style="text-align: center; display: none;"><%# Eval("Folio").ToString() %></td>
                        <td style="text-align: center; display: none;"><%# Eval("Concepto").ToString() %></td>
                        <td style="text-align: center; display: none;"><%# Eval("Cargos").ToString() %></td>
                        <td style="text-align: center; display: none;"><%# Eval("Abonos").ToString() %></td>
                        <td style="text-align: center; display: none;"><%# Eval("Documento").ToString() %></td>
                        <td style="text-align: center; display: none;"><%# Eval("Vence").ToString() %></td>
                        <td style="text-align: center; display: none;"><%# Eval("Ref").ToString() %></td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>

            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="text-align: center;" colspan="4">&nbsp;</td>
                    <td style="text-align: center;" class="size20"><strong>Total</strong></td>
                    <td style="text-align: center;" class="size20">
                        <asp:Label runat="server" ID="lblTotalCargos"></asp:Label></td>
                    <td style="text-align: center;" class="size20">
                        <asp:Label runat="server" ID="lblTotalAbonos"></asp:Label></td>
                    <td style="text-align: center;" colspan="3">&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: center;" colspan="4">&nbsp;</td>
                    <td style="text-align: center;" class="size20"><strong>RESUMEN</strong></td>
                    <td style="text-align: center;" class="size20">
                        <asp:Label runat="server" ID="lblResumen"></asp:Label></td>
                    <td style="text-align: center;" colspan="4">&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: center;" colspan="4">&nbsp;</td>
                    <td style="text-align: center;" class="size20">Saldo Inicial</td>
                    <td style="text-align: center;" class="size20">
                        <asp:Label runat="server" ID="lblSaldoInicial"></asp:Label></td>
                    <td style="text-align: center;" colspan="4">&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: center;" colspan="4">&nbsp;</td>
                    <td style="text-align: center;" class="size20">(+) Cargos</td>
                    <td style="text-align: center;" class="size20">
                        <asp:Label runat="server" ID="lblCargos"></asp:Label></td>
                    <td style="text-align: center;" colspan="4">&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: center;" colspan="4">&nbsp;</td>
                    <td style="text-align: center;" class="size20">(-) Abonos</td>
                    <td style="text-align: center;" class="size20">
                        <asp:Label runat="server" ID="lblAbonos"></asp:Label></td>
                    <td style="text-align: center;" colspan="4">&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: center;" colspan="4">&nbsp;</td>
                    <td style="text-align: center;" class="size20">(=) Saldo Final</td>
                    <td style="text-align: center;" class="size20">
                        <asp:Label runat="server" ID="lblSaldoFinal"></asp:Label></td>
                    <td style="text-align: center;" colspan="4" class="size20">
                        <asp:Label runat="server" ID="lblSumaAbonos"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div>
        </div>
        <script>

            function llenarAgGrid(res) {
                const rowDataB = res;
                console.log(rowDataB.length);
                $(document).ready(function () {
                    gridOptions.api.setRowData(rowDataB);
                    var alto = 42 * rowDataB.length + 70;
                    document.getElementById("myGrid").style.height = alto + "px";
                    document.getElementById("myGrid").style.maxHeight = "600px";
                    document.getElementById("myGrid").style.minHeight = "200px";
                    sizeToFit();
                });
                //gridOptions.api.setRowData(rowDataB);
            }


        </script>
        <script src="js/loader/loader.js"></script>
    </form>
</body>

</html>
