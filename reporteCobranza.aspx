<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="reporteCobranza.aspx.cs" Inherits="despacho.reporteCobranza" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="Arise Admin Panel" />
    <meta name="keywords" content="Admin, Dashboard, Bootstrap3, Sass, transform, CSS3, HTML5, Web design, UI Design, Responsive Dashboard, Responsive Admin, Admin Theme, Best Admin UI, Bootstrap Theme, Themeforest, Bootstrap, C3 Graphs, D3 Graphs, NVD3 Graphs, Admin Skin, Black Admin Dashboard, Grey Admin Dashboard, Dark Admin Dashboard, Simple Admin Dashboard, Simple Admin Theme, Simple Bootstrap Dashboard, " />
    <meta name="author" content="Ramji" />
    <link rel="shortcut icon" href="img/fav.png">
    <title>Concretos PEPI Reporte</title>

    <!-- bootstrap CSS -->
    <link href="css/bootstrap.min.css" rel="stylesheet" media="screen" />

    <!-- Main CSS -->
    <link href="css/main.css" rel="stylesheet" media="screen" />

    <!-- Ion Icons -->
    <link href="fonts/icomoon/icomoon.css" rel="stylesheet" />

    <!-- Pricing plans CSS -->
    <link rel="stylesheet" href="css/pricing.css" />

    <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
    <script src="js/jquery.js"></script>

    <!-- moment (necessary for Bootstrap's JavaScript plugins) -->
    <script src="Scripts/moment.min.js"></script>

    <!-- bootstrap -->
    <script src="js/bootstrap.min.js"></script>

    <!-- custom JS -->
    <script src="js/custom.js"></script>

    <!-- jquery ScrollUp JS -->
    <script src="js/scrollup/jquery.scrollUp.js"></script>

    <!-- BS Validator JS -->
    <script src="js/bsvalidator/bootstrapValidator.js"></script>
    <script src="js/bsvalidator/custom-validations.js"></script>

    <!-- Sparkline Graphs -->
    <script src="js/sparkline/sparkline.js"></script>
    <script src="js/sparkline/custom-sparkline.js"></script>

    <!-- Dragula Drag & Drop JS -->
    <script src="js/dragula/dragula.min.js"></script>

    <!-- Odometer JS -->
    <script src="js/odometer/odometer.min.js"></script>

    <!-- DataBars JS -->
    <script src="js/databars/jquery.databar.js"></script>
    <script src="js/databars/custom-databars.js"></script>

    <!-- Data Tables -->
    <script src="js/datatables/dataTables.min.js"></script>
    <script src="js/datatables/dataTables.bootstrap.min.js"></script>
    <script src="js/datatables/dataTables.tableTools.js"></script>
    <script src="js/datatables/autoFill.min.js"></script>
    <script src="js/datatables/autoFill.bootstrap.min.js"></script>
    <script src="js/datatables/fixedHeader.min.js"></script>
    <script src="js/datatables/custom-datatables.js"></script>

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
        <div>
            <div class="row gutter">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="panel panel-blue">
                        <div class="row gutter">
                            <div class="panel-body">
                                <div style="width: 100%; text-align: center;">
                                    <table width="80%" border="0" align="center" cellpadding="0" cellspacing="0">
                                        <tr align="center" valign="bottom">
                                            <td colspan="2" valign="middle" class="titulos">
                                                <div style="display: flex; justify-content: space-between" runat="server">
                                                    <div id="imagen" runat="server"></div>
                                                    <div>
                                                        <div class="text-center" style="margin-bottom: 10px">
                                                            <asp:Label ID="lblReporte" runat="server">Reporte de Cobranza</asp:Label>
                                                        </div>
                                                        <%-- <div class="text-center" style="margin-bottom: 10px">
                                                            Sucursal:
                                                            <asp:Label ID="lblSucursal" runat="server"></asp:Label>
                                                        </div>--%>
                                                         <div class="text-center" style="margin-bottom: 10px">
                                                            Tipo Solicitud:
                                                            <asp:Label ID="lblTS" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="text-center" style="margin-bottom: 10px">
                                                            A la Fecha:
                                                            <asp:Label ID="lblFecha" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="text-center" style="margin-bottom: 10px">
                                                            <%--<asp:Label ID="lblAnio" runat="server"><strong>Año: </strong></asp:Label>--%>
                                                            <asp:HiddenField ID="hfAnio" runat="server" />
                                                            <asp:HiddenField ID="hfFechaAIni" runat="server" />
                                                            <asp:HiddenField ID="hfFechaAFin" runat="server" />
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
                                    <div>
                                        <asp:ListView ID="lv" runat="server">
                                            <LayoutTemplate>
                                                <table id="responsiveTabl" class="table table-striped table-bordered no-margin">
                                                    <thead>
                                                        <tr>
                                                            <th style="text-align: center;">Clave</th>
                                                            <th style="text-align: center;">Nombre</th>
                                                            <th style="text-align: center;">Total vencido</th>
                                                            <th style="text-align: center;">0 - 7 días</th>
                                                            <th style="text-align: center;">8 - 14 días</th>
                                                            <th style="text-align: center;">15 - 21 días</th>
                                                            <th style="text-align: center;">22 días o más</th>
                                                            <th style="text-align: center;">Total por vencer</th>
                                                            <th style="text-align: center;">Total</th>
                                                            <th style="text-align: center;">Vendedor</th>
                                                            <th style="text-align: center;">Observaciones</th>
                                                        </tr>
                                                    </thead>
                                                    <tr id="itemPlaceholder" runat="server"></tr>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="text-align: center"><%# Eval("clave") %></td>
                                                    <td style="text-align: left"><%# Eval("cliente") %></td>
                                                    <td style="text-align: right"><%# Eval("vencido") %></td>
                                                    <td style="text-align: right"><%# Eval("07") %></td>
                                                    <td style="text-align: right"><%# Eval("814") %></td>
                                                    <td style="text-align: right"><%# Eval("1521") %></td>
                                                    <td style="text-align: right"><%# Eval("22") %></td>
                                                    <td style="text-align: right"><%# Eval("vencer") %></td>
                                                    <td style="text-align: right"><%# Eval("total") %></td>
                                                    <td style="text-align: left"><%# Eval("vendedor") %></td>
                                                    <td style="text-align: center"><%# Eval("vacio") %></td>
                                                </tr>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                                    <thead>
                                                        <tr>
                                                            <th style="text-align: center;">Clave</th>
                                                            <th style="text-align: center;">Nombre</th>
                                                            <th style="text-align: center;">Total vencido</th>
                                                            <th style="text-align: center;">0 - 7 días</th>
                                                            <th style="text-align: center;">8 - 14 días</th>
                                                            <th style="text-align: center;">15 - 21 días</th>
                                                            <th style="text-align: center;">22 días o más</th>
                                                            <th style="text-align: center;">Total por vencer</th>
                                                            <th style="text-align: center;">Total</th>
                                                            <th style="text-align: center;">Vendedor</th>
                                                            <th style="text-align: center;">Observaciones</th>
                                                        </tr>
                                                    </thead>
                                                    <tr>
                                                        <td colspan="11">
                                                            <label class="label label-danger">¡No hay Datos Registrados!</label></td>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
                                        </asp:ListView>
                                        <%--<asp:GridView ID="GridView1" runat="server">
                                        </asp:GridView>--%>
                                    </div>
                                    <div>
                                        <asp:ListView ID="lvTotales" runat="server">
                                            <LayoutTemplate>
                                                <table id="responsiveTabl" class="table table-striped table-bordered no-margin">
                                                    <thead>
                                                        <tr>
                                                            <th style="text-align: center;"></th>
                                                            <th style="text-align: center;"></th>
                                                            <th style="text-align: right;"><b>Total vencido</b></th>
                                                            <th style="text-align: right;"><b>Total 0 - 7 días</b></th>
                                                            <th style="text-align: right;"><b>Total 8 - 14 días</b></th>
                                                            <th style="text-align: right;"><b>Total 15 - 21 días</b></th>
                                                            <th style="text-align: right;"><b>Total 22 días o más</b></th>
                                                            <th style="text-align: right;"><b>Total por vencer</b></th>
                                                            <th style="text-align: right;"><b>Total</b></th>
                                                            <th style="text-align: center;"></th>
                                                            <th style="text-align: center;"></th>
                                                        </tr>
                                                    </thead>
                                                    <tr id="itemPlaceholder" runat="server"></tr>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="text-align: center"><%# Eval("clave") %></td>
                                                    <td style="text-align: right"><%# Eval("cliente") %></td>
                                                    <td style="text-align: right"><%# Eval("vencido") %></td>
                                                    <td style="text-align: right"><%# Eval("07") %></td>
                                                    <td style="text-align: right"><%# Eval("814") %></td>
                                                    <td style="text-align: right"><%# Eval("1521") %></td>
                                                    <td style="text-align: right"><%# Eval("22") %></td>
                                                    <td style="text-align: right"><%# Eval("vencer") %></td>
                                                    <td style="text-align: right"><%# Eval("total") %></td>
                                                    <td style="text-align: center"><%# Eval("vendedor") %></td>
                                                    <td style="text-align: center"><%# Eval("vacio") %></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        </div>
    </form>
</body>
</html>

