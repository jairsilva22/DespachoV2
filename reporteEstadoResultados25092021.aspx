<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="reporteEstadoResultados25092021.aspx.cs" Inherits="despacho.reporteEstadoResultados25092021" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="Arise Admin Panel" />
    <meta name="keywords" content="Admin, Dashboard, Bootstrap3, Sass, transform, CSS3, HTML5, Web design, UI Design, Responsive Dashboard, Responsive Admin, Admin Theme, Best Admin UI, Bootstrap Theme, Themeforest, Bootstrap, C3 Graphs, D3 Graphs, NVD3 Graphs, Admin Skin, Black Admin Dashboard, Grey Admin Dashboard, Dark Admin Dashboard, Simple Admin Dashboard, Simple Admin Theme, Simple Bootstrap Dashboard, " />
    <meta name="author" content="Ramji" />
    <link rel="shortcut icon" href="img/fav.png">
    <title>Concretos PEPI Admin Panel</title>

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
                                                            <asp:Label ID="lblReporte" runat="server">Reporte de Estado de Resultados</asp:Label>
                                                        </div>
                                                        <div class="text-center" style="margin-bottom: 10px">
                                                            <asp:Label ID="lblFolio" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="text-center" style="margin-bottom: 10px">
                                                            <asp:Label ID="lblAnio" runat="server"><strong>Año: </strong></asp:Label>
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
                                    <div class="table-responsive">
                                        <asp:ListView ID="lvIngresos" runat="server">
                                            <LayoutTemplate>
                                                <table id="responsiveTabl" class="table table-striped table-bordered no-margin">
                                                    <thead>
                                                        <tr>
                                                            <th style="text-align: center; width:120px">Ingresos</th>
                                                            <th style="text-align: center; width:120px">Enero</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Febrero</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Marzo</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Abril</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Mayo</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Junio</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Julio</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Agosto</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Septiembre</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Octubre</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Noviembre</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Diciembre</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Acumulado</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                        </tr>
                                                    </thead>
                                                    <tr id="itemPlaceholder" runat="server"></tr>
                                                    <%--<tfoot>
                                                        <tr>
                                                            <th style="text-align: center">Ventas Netas</th>
                                                            <th style="text-align: center"><%# Eval("vn1") %></th>
                                                            <th style="text-align: center"><%# Eval("vnP1") %></th>
                                                            <th style="text-align: center"><%# Eval("vn2") %></th>
                                                            <th style="text-align: center"><%# Eval("vnP2") %></th>
                                                            <th style="text-align: center"><%# Eval("vn3") %></th>
                                                            <th style="text-align: center"><%# Eval("vnP3") %></th>
                                                            <th style="text-align: center"><%# Eval("vn4") %></th>
                                                            <th style="text-align: center"><%# Eval("vnP4") %></th>
                                                            <th style="text-align: center"><%# Eval("vn5") %></th>
                                                            <th style="text-align: center"><%# Eval("vnP5") %></th>
                                                            <th style="text-align: center"><%# Eval("vn6") %></th>
                                                            <th style="text-align: center"><%# Eval("vnP6") %></th>
                                                            <th style="text-align: center"><%# Eval("vn7") %></th>
                                                            <th style="text-align: center"><%# Eval("vnP7") %></th>
                                                            <th style="text-align: center"><%# Eval("vn8") %></th>
                                                            <th style="text-align: center"><%# Eval("vnP8") %></th>
                                                            <th style="text-align: center"><%# Eval("vn9") %></th>
                                                            <th style="text-align: center"><%# Eval("vnP9") %></th>
                                                            <th style="text-align: center"><%# Eval("vn10") %></th>
                                                            <th style="text-align: center"><%# Eval("vnP10") %></th>
                                                            <th style="text-align: center"><%# Eval("vn11") %></th>
                                                            <th style="text-align: center"><%# Eval("vnP11") %></th>
                                                            <th style="text-align: center"><%# Eval("vn12") %></th>
                                                            <th style="text-align: center"><%# Eval("vnP12") %></th>
                                                            <th style="text-align: center"><%# Eval("vnAcu") %></th>
                                                            <th style="text-align: center"><%# Eval("vnPAcu") %></th>
                                                        </tr>
                                                    </tfoot>--%>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="text-align: left"><%# Eval("ingD0") %></td>
                                                    <td style="text-align: right"><%# Eval("ingD1") %></td>
                                                    <td style="text-align: center"><%# Eval("ingDP1") %></td>
                                                    <td style="text-align: right"><%# Eval("ingD2") %></td>
                                                    <td style="text-align: center"><%# Eval("ingDP2") %></td>
                                                    <td style="text-align: right"><%# Eval("ingD3") %></td>
                                                    <td style="text-align: center"><%# Eval("ingDP3") %></td>
                                                    <td style="text-align: right"><%# Eval("ingD4") %></td>
                                                    <td style="text-align: center"><%# Eval("ingDP4") %></td>
                                                    <td style="text-align: right"><%# Eval("ingD5") %></td>
                                                    <td style="text-align: center"><%# Eval("ingDP5") %></td>
                                                    <td style="text-align: right"><%# Eval("ingD6") %></td>
                                                    <td style="text-align: center"><%# Eval("ingDP6") %></td>
                                                    <td style="text-align: right"><%# Eval("ingD7") %></td>
                                                    <td style="text-align: center"><%# Eval("ingDP7") %></td>
                                                    <td style="text-align: right"><%# Eval("ingD8") %></td>
                                                    <td style="text-align: center"><%# Eval("ingDP8") %></td>
                                                    <td style="text-align: right"><%# Eval("ingD9") %></td>
                                                    <td style="text-align: center"><%# Eval("ingDP9") %></td>
                                                    <td style="text-align: right"><%# Eval("ingD10") %></td>
                                                    <td style="text-align: center"><%# Eval("ingDP10") %></td>
                                                    <td style="text-align: right"><%# Eval("ingD11") %></td>
                                                    <td style="text-align: center"><%# Eval("ingDP11") %></td>
                                                    <td style="text-align: right"><%# Eval("ingD12") %></td>
                                                    <td style="text-align: center"><%# Eval("ingDP12") %></td>
                                                    <td style="text-align: right"><%# Eval("ingDAcu") %></td>
                                                    <td style="text-align: center"><%# Eval("ingDPAcu") %></td>
                                                </tr>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                                    <thead>
                                                        <tr>
                                                            <th style="text-align: center"></th>
                                                            <th style="text-align: center">Enero</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Febrero</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Marzo</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Abril</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Mayo</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Junio</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Julio</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Agosto</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Septiembre</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Octubre</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Noviembre</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Diciembre</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Acumulado</th>
                                                            <th style="text-align: center">%</th>
                                                        </tr>
                                                    </thead>
                                                    <tr>
                                                        <td colspan="27">
                                                            <label class="label label-danger">¡No hay Datos Registrados!</label></td>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
                                        </asp:ListView>
                                    </div>
                                    <br />
                                    <div class="table-responsive">
                                        <asp:ListView ID="lvCostoVentas" runat="server">
                                            <LayoutTemplate>
                                                <table id="responsiveTabl" class="table table-striped table-bordered no-margin">
                                                    <thead>
                                                        <tr>
                                                            <th style="text-align: center; width:120px"></th>
                                                            <th style="text-align: center; width:120px">Enero</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Febrero</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Marzo</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Abril</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Mayo</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Junio</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Julio</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Agosto</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Septiembre</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Octubre</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Noviembre</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Diciembre</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Acumulado</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                        </tr>
                                                    </thead>
                                                    <tr id="itemPlaceholder" runat="server"></tr>
                                                   <%-- <tfoot>
                                                        <tr>
                                                            <th style="text-align: center">Costo de Ventas</th>
                                                            <th style="text-align: center"><%# Eval("cv1") %></th>
                                                            <th style="text-align: center"><%# Eval("cvP1") %></th>
                                                            <th style="text-align: center"><%# Eval("cv2") %></th>
                                                            <th style="text-align: center"><%# Eval("cvP2") %></th>
                                                            <th style="text-align: center"><%# Eval("cv3") %></th>
                                                            <th style="text-align: center"><%# Eval("cvP3") %></th>
                                                            <th style="text-align: center"><%# Eval("cv4") %></th>
                                                            <th style="text-align: center"><%# Eval("cvP4") %></th>
                                                            <th style="text-align: center"><%# Eval("cv5") %></th>
                                                            <th style="text-align: center"><%# Eval("cvP5") %></th>
                                                            <th style="text-align: center"><%# Eval("cv6") %></th>
                                                            <th style="text-align: center"><%# Eval("cvP6") %></th>
                                                            <th style="text-align: center"><%# Eval("cv7") %></th>
                                                            <th style="text-align: center"><%# Eval("cvP7") %></th>
                                                            <th style="text-align: center"><%# Eval("cv8") %></th>
                                                            <th style="text-align: center"><%# Eval("cvP8") %></th>
                                                            <th style="text-align: center"><%# Eval("cv9") %></th>
                                                            <th style="text-align: center"><%# Eval("cvP9") %></th>
                                                            <th style="text-align: center"><%# Eval("cv10") %></th>
                                                            <th style="text-align: center"><%# Eval("cvP10") %></th>
                                                            <th style="text-align: center"><%# Eval("cv11") %></th>
                                                            <th style="text-align: center"><%# Eval("cvP11") %></th>
                                                            <th style="text-align: center"><%# Eval("cv12") %></th>
                                                            <th style="text-align: center"><%# Eval("cvP12") %></th>
                                                            <th style="text-align: center"><%# Eval("cvAcu") %></th>
                                                            <th style="text-align: center"><%# Eval("cvPAcu") %></th>
                                                        </tr>
                                                    </tfoot>--%>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="text-align: left"><%# Eval("cvD0") %></td>
                                                    <td style="text-align: right"><%# Eval("cvD1") %></td>
                                                    <td style="text-align: center"><%# Eval("cvDP1") %></td>
                                                    <td style="text-align: right"><%# Eval("cvD2") %></td>
                                                    <td style="text-align: center"><%# Eval("cvDP2") %></td>
                                                    <td style="text-align: right"><%# Eval("cvD3") %></td>
                                                    <td style="text-align: center"><%# Eval("cvDP3") %></td>
                                                    <td style="text-align: right"><%# Eval("cvD4") %></td>
                                                    <td style="text-align: center"><%# Eval("cvDP4") %></td>
                                                    <td style="text-align: right"><%# Eval("cvD5") %></td>
                                                    <td style="text-align: center"><%# Eval("cvDP5") %></td>
                                                    <td style="text-align: right"><%# Eval("cvD6") %></td>
                                                    <td style="text-align: center"><%# Eval("cvDP6") %></td>
                                                    <td style="text-align: right"><%# Eval("cvD7") %></td>
                                                    <td style="text-align: center"><%# Eval("cvDP7") %></td>
                                                    <td style="text-align: right"><%# Eval("cvD8") %></td>
                                                    <td style="text-align: center"><%# Eval("cvDP8") %></td>
                                                    <td style="text-align: right"><%# Eval("cvD9") %></td>
                                                    <td style="text-align: center"><%# Eval("cvDP9") %></td>
                                                    <td style="text-align: right"><%# Eval("cvD10") %></td>
                                                    <td style="text-align: center"><%# Eval("cvDP10") %></td>
                                                    <td style="text-align: right"><%# Eval("cvD11") %></td>
                                                    <td style="text-align: center"><%# Eval("cvDP11") %></td>
                                                    <td style="text-align: right"><%# Eval("cvD12") %></td>
                                                    <td style="text-align: center"><%# Eval("cvDP12") %></td>
                                                    <td style="text-align: right"><%# Eval("cvDAcu") %></td>
                                                    <td style="text-align: center"><%# Eval("cvDPAcu") %></td>
                                                </tr>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                                    <thead>
                                                        <tr>
                                                            <th style="text-align: center">Costo de Ventas</th>
                                                            <th style="text-align: center">Enero</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Febrero</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Marzo</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Abril</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Mayo</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Junio</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Julio</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Agosto</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Septiembre</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Octubre</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Noviembre</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Diciembre</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Acumulado</th>
                                                            <th style="text-align: center">%</th>
                                                        </tr>
                                                    </thead>
                                                    <tr>
                                                        <td colspan="27">
                                                            <label class="label label-danger">¡No hay Datos Registrados!</label></td>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
                                        </asp:ListView>
                                    </div>
                                    <br />
                                    <div class="table-responsive">
                                        <asp:ListView ID="lvUtilidadBruta" runat="server">
                                            <LayoutTemplate>
                                                <table id="responsiveTabl" class="table table-striped table-bordered no-margin">
                                                    <thead>
                                                        <tr>
                                                            <th style="text-align: center; width:120px"></th>
                                                            <th style="text-align: center; width:120px">Enero</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Febrero</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Marzo</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Abril</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Mayo</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Junio</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Julio</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Agosto</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Septiembre</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Octubre</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Noviembre</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Diciembre</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Acumulado</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                        </tr>
                                                    </thead>
                                                    <tr id="itemPlaceholder" runat="server"></tr>
                                                   <%-- <tfoot>
                                                        <tr>
                                                            <th style="text-align: center">Utilidad Bruta</th>
                                                            <th style="text-align: center"><%# Eval("ub1") %></th>
                                                            <th style="text-align: center"><%# Eval("ubP1") %></th>
                                                            <th style="text-align: center"><%# Eval("ub2") %></th>
                                                            <th style="text-align: center"><%# Eval("ubP2") %></th>
                                                            <th style="text-align: center"><%# Eval("ub3") %></th>
                                                            <th style="text-align: center"><%# Eval("ubP3") %></th>
                                                            <th style="text-align: center"><%# Eval("ub4") %></th>
                                                            <th style="text-align: center"><%# Eval("ubP4") %></th>
                                                            <th style="text-align: center"><%# Eval("ub5") %></th>
                                                            <th style="text-align: center"><%# Eval("ubP5") %></th>
                                                            <th style="text-align: center"><%# Eval("ub6") %></th>
                                                            <th style="text-align: center"><%# Eval("ubP6") %></th>
                                                            <th style="text-align: center"><%# Eval("ub7") %></th>
                                                            <th style="text-align: center"><%# Eval("ubP7") %></th>
                                                            <th style="text-align: center"><%# Eval("ub8") %></th>
                                                            <th style="text-align: center"><%# Eval("ubP8") %></th>
                                                            <th style="text-align: center"><%# Eval("ub9") %></th>
                                                            <th style="text-align: center"><%# Eval("ubP9") %></th>
                                                            <th style="text-align: center"><%# Eval("ub10") %></th>
                                                            <th style="text-align: center"><%# Eval("ubP10") %></th>
                                                            <th style="text-align: center"><%# Eval("ub11") %></th>
                                                            <th style="text-align: center"><%# Eval("ubP11") %></th>
                                                            <th style="text-align: center"><%# Eval("ub12") %></th>
                                                            <th style="text-align: center"><%# Eval("ubP12") %></th>
                                                            <th style="text-align: center"><%# Eval("ubAcu") %></th>
                                                            <th style="text-align: center"><%# Eval("ubPAcu") %></th>
                                                        </tr>
                                                    </tfoot>--%>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="text-align: left"><%# Eval("ubD0") %></td>
                                                    <td style="text-align: right"><%# Eval("ubD1") %></td>
                                                    <td style="text-align: center"><%# Eval("ubDP1") %></td>
                                                    <td style="text-align: right"><%# Eval("ubD2") %></td>
                                                    <td style="text-align: center"><%# Eval("ubDP2") %></td>
                                                    <td style="text-align: right"><%# Eval("ubD3") %></td>
                                                    <td style="text-align: center"><%# Eval("ubDP3") %></td>
                                                    <td style="text-align: right"><%# Eval("ubD4") %></td>
                                                    <td style="text-align: center"><%# Eval("ubDP4") %></td>
                                                    <td style="text-align: right"><%# Eval("ubD5") %></td>
                                                    <td style="text-align: center"><%# Eval("ubDP5") %></td>
                                                    <td style="text-align: right"><%# Eval("ubD6") %></td>
                                                    <td style="text-align: center"><%# Eval("ubDP6") %></td>
                                                    <td style="text-align: right"><%# Eval("ubD7") %></td>
                                                    <td style="text-align: center"><%# Eval("ubDP7") %></td>
                                                    <td style="text-align: right"><%# Eval("ubD8") %></td>
                                                    <td style="text-align: center"><%# Eval("ubDP8") %></td>
                                                    <td style="text-align: right"><%# Eval("ubD9") %></td>
                                                    <td style="text-align: center"><%# Eval("ubDP9") %></td>
                                                    <td style="text-align: right"><%# Eval("ubD10") %></td>
                                                    <td style="text-align: center"><%# Eval("ubDP10") %></td>
                                                    <td style="text-align: right"><%# Eval("ubD11") %></td>
                                                    <td style="text-align: center"><%# Eval("ubDP11") %></td>
                                                    <td style="text-align: right"><%# Eval("ubD12") %></td>
                                                    <td style="text-align: center"><%# Eval("ubDP12") %></td>
                                                    <td style="text-align: right"><%# Eval("ubDAcu") %></td>
                                                    <td style="text-align: center"><%# Eval("ubDPAcu") %></td>
                                                </tr>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                                    <thead>
                                                        <tr>
                                                            <th style="text-align: center">Utilidad Bruta</th>
                                                            <th style="text-align: center">Enero</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Febrero</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Marzo</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Abril</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Mayo</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Junio</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Julio</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Agosto</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Septiembre</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Octubre</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Noviembre</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Diciembre</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Acumulado</th>
                                                            <th style="text-align: center">%</th>
                                                        </tr>
                                                    </thead>
                                                    <tr>
                                                        <td colspan="27">
                                                            <label class="label label-danger">¡No hay Datos Registrados!</label></td>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
                                        </asp:ListView>
                                    </div>
                                    <br />
                                    <div class="table-responsive">
                                        <asp:ListView ID="lvGastosVenta" runat="server">
                                            <LayoutTemplate>
                                                <table id="responsiveTabl" class="table table-striped table-bordered no-margin">
                                                    <thead>
                                                        <tr>
                                                            <th style="text-align: center; width:120px"></th>
                                                            <th style="text-align: center; width:120px">Enero</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Febrero</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Marzo</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Abril</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Mayo</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Junio</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Julio</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Agosto</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Septiembre</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Octubre</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Noviembre</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Diciembre</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Acumulado</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                        </tr>
                                                    </thead>
                                                    <tr id="itemPlaceholder" runat="server"></tr>
                                                  <%--  <tfoot>
                                                        <tr>
                                                            <th style="text-align: center">Total Gastos de Venta</th>
                                                            <th style="text-align: center"><%# Eval("gv1") %></th>
                                                            <th style="text-align: center"><%# Eval("gvP1") %></th>
                                                            <th style="text-align: center"><%# Eval("gv2") %></th>
                                                            <th style="text-align: center"><%# Eval("gvP2") %></th>
                                                            <th style="text-align: center"><%# Eval("gv3") %></th>
                                                            <th style="text-align: center"><%# Eval("gvP3") %></th>
                                                            <th style="text-align: center"><%# Eval("gv4") %></th>
                                                            <th style="text-align: center"><%# Eval("gvP4") %></th>
                                                            <th style="text-align: center"><%# Eval("gv5") %></th>
                                                            <th style="text-align: center"><%# Eval("gvP5") %></th>
                                                            <th style="text-align: center"><%# Eval("gv6") %></th>
                                                            <th style="text-align: center"><%# Eval("gvP6") %></th>
                                                            <th style="text-align: center"><%# Eval("gv7") %></th>
                                                            <th style="text-align: center"><%# Eval("gvP7") %></th>
                                                            <th style="text-align: center"><%# Eval("gv8") %></th>
                                                            <th style="text-align: center"><%# Eval("gvP8") %></th>
                                                            <th style="text-align: center"><%# Eval("gv9") %></th>
                                                            <th style="text-align: center"><%# Eval("gvP9") %></th>
                                                            <th style="text-align: center"><%# Eval("gv10") %></th>
                                                            <th style="text-align: center"><%# Eval("gvP10") %></th>
                                                            <th style="text-align: center"><%# Eval("gv11") %></th>
                                                            <th style="text-align: center"><%# Eval("gvP11") %></th>
                                                            <th style="text-align: center"><%# Eval("gv12") %></th>
                                                            <th style="text-align: center"><%# Eval("gvP12") %></th>
                                                            <th style="text-align: center"><%# Eval("gvAcu") %></th>
                                                            <th style="text-align: center"><%# Eval("gvPAcu") %></th>
                                                        </tr>
                                                    </tfoot>--%>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="text-align: left"><%# Eval("gvD0") %></td>
                                                    <td style="text-align: right"><%# Eval("gvD1") %></td>
                                                    <td style="text-align: center"><%# Eval("gvDP1") %></td>
                                                    <td style="text-align: right"><%# Eval("gvD2") %></td>
                                                    <td style="text-align: center"><%# Eval("gvDP2") %></td>
                                                    <td style="text-align: right"><%# Eval("gvD3") %></td>
                                                    <td style="text-align: center"><%# Eval("gvDP3") %></td>
                                                    <td style="text-align: right"><%# Eval("gvD4") %></td>
                                                    <td style="text-align: center"><%# Eval("gvDP4") %></td>
                                                    <td style="text-align: right"><%# Eval("gvD5") %></td>
                                                    <td style="text-align: center"><%# Eval("gvDP5") %></td>
                                                    <td style="text-align: right"><%# Eval("gvD6") %></td>
                                                    <td style="text-align: center"><%# Eval("gvDP6") %></td>
                                                    <td style="text-align: right"><%# Eval("gvD7") %></td>
                                                    <td style="text-align: center"><%# Eval("gvDP7") %></td>
                                                    <td style="text-align: right"><%# Eval("gvD8") %></td>
                                                    <td style="text-align: center"><%# Eval("gvDP8") %></td>
                                                    <td style="text-align: right"><%# Eval("gvD9") %></td>
                                                    <td style="text-align: center"><%# Eval("gvDP9") %></td>
                                                    <td style="text-align: right"><%# Eval("gvD10") %></td>
                                                    <td style="text-align: center"><%# Eval("gvDP10") %></td>
                                                    <td style="text-align: right"><%# Eval("gvD11") %></td>
                                                    <td style="text-align: center"><%# Eval("gvDP11") %></td>
                                                    <td style="text-align: right"><%# Eval("gvD12") %></td>
                                                    <td style="text-align: center"><%# Eval("gvDP12") %></td>
                                                    <td style="text-align: right"><%# Eval("gvDAcu") %></td>
                                                    <td style="text-align: center"><%# Eval("gvDPAcu") %></td>
                                                </tr>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                                    <thead>
                                                        <tr>
                                                            <th style="text-align: center">Gastos de Venta</th>
                                                            <th style="text-align: center">Enero</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Febrero</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Marzo</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Abril</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Mayo</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Junio</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Julio</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Agosto</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Septiembre</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Octubre</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Noviembre</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Diciembre</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Acumulado</th>
                                                            <th style="text-align: center">%</th>
                                                        </tr>
                                                    </thead>
                                                    <tr>
                                                        <td colspan="27">
                                                            <label class="label label-danger">¡No hay Datos Registrados!</label></td>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
                                        </asp:ListView>
                                    </div>
                                    <br />
                                    <div class="table-responsive">
                                        <asp:ListView ID="lvGastosAdministrativos" runat="server">
                                            <LayoutTemplate>
                                                <table id="responsiveTabl" class="table table-striped table-bordered no-margin">
                                                    <thead>
                                                        <tr>
                                                            <th style="text-align: center; width:120px"></th>
                                                            <th style="text-align: center; width:120px">Enero</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Febrero</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Marzo</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Abril</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Mayo</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Junio</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Julio</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Agosto</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Septiembre</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Octubre</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Noviembre</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Diciembre</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Acumulado</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                        </tr>
                                                    </thead>
                                                    <tr id="itemPlaceholder" runat="server"></tr>
                                                  <%--  <tfoot>
                                                        <tr>
                                                            <th style="text-align: center">Total Gastos Administrativos</th>
                                                            <th style="text-align: center"><%# Eval("ga1") %></th>
                                                            <th style="text-align: center"><%# Eval("gaP1") %></th>
                                                            <th style="text-align: center"><%# Eval("ga2") %></th>
                                                            <th style="text-align: center"><%# Eval("gaP2") %></th>
                                                            <th style="text-align: center"><%# Eval("ga3") %></th>
                                                            <th style="text-align: center"><%# Eval("gaP3") %></th>
                                                            <th style="text-align: center"><%# Eval("ga4") %></th>
                                                            <th style="text-align: center"><%# Eval("gaP4") %></th>
                                                            <th style="text-align: center"><%# Eval("ga5") %></th>
                                                            <th style="text-align: center"><%# Eval("gaP5") %></th>
                                                            <th style="text-align: center"><%# Eval("ga6") %></th>
                                                            <th style="text-align: center"><%# Eval("gaP6") %></th>
                                                            <th style="text-align: center"><%# Eval("ga7") %></th>
                                                            <th style="text-align: center"><%# Eval("gaP7") %></th>
                                                            <th style="text-align: center"><%# Eval("ga8") %></th>
                                                            <th style="text-align: center"><%# Eval("gaP8") %></th>
                                                            <th style="text-align: center"><%# Eval("ga9") %></th>
                                                            <th style="text-align: center"><%# Eval("gaP9") %></th>
                                                            <th style="text-align: center"><%# Eval("ga10") %></th>
                                                            <th style="text-align: center"><%# Eval("gaP10") %></th>
                                                            <th style="text-align: center"><%# Eval("ga11") %></th>
                                                            <th style="text-align: center"><%# Eval("gaP11") %></th>
                                                            <th style="text-align: center"><%# Eval("ga12") %></th>
                                                            <th style="text-align: center"><%# Eval("gaP12") %></th>
                                                            <th style="text-align: center"><%# Eval("gaAcu") %></th>
                                                            <th style="text-align: center"><%# Eval("gaPAcu") %></th>
                                                        </tr>
                                                    </tfoot>--%>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="text-align: left"><%# Eval("gaD0") %></td>
                                                    <td style="text-align: right"><%# Eval("gaD1") %></td>
                                                    <td style="text-align: center"><%# Eval("gaDP1") %></td>
                                                    <td style="text-align: right"><%# Eval("gaD2") %></td>
                                                    <td style="text-align: center"><%# Eval("gaDP2") %></td>
                                                    <td style="text-align: right"><%# Eval("gaD3") %></td>
                                                    <td style="text-align: center"><%# Eval("gaDP3") %></td>
                                                    <td style="text-align: right"><%# Eval("gaD4") %></td>
                                                    <td style="text-align: center"><%# Eval("gaDP4") %></td>
                                                    <td style="text-align: right"><%# Eval("gaD5") %></td>
                                                    <td style="text-align: center"><%# Eval("gaDP5") %></td>
                                                    <td style="text-align: right"><%# Eval("gaD6") %></td>
                                                    <td style="text-align: center"><%# Eval("gaDP6") %></td>
                                                    <td style="text-align: right"><%# Eval("gaD7") %></td>
                                                    <td style="text-align: center"><%# Eval("gaDP7") %></td>
                                                    <td style="text-align: right"><%# Eval("gaD8") %></td>
                                                    <td style="text-align: center"><%# Eval("gaDP8") %></td>
                                                    <td style="text-align: right"><%# Eval("gaD9") %></td>
                                                    <td style="text-align: center"><%# Eval("gaDP9") %></td>
                                                    <td style="text-align: right"><%# Eval("gaD10") %></td>
                                                    <td style="text-align: center"><%# Eval("gaDP10") %></td>
                                                    <td style="text-align: right"><%# Eval("gaD11") %></td>
                                                    <td style="text-align: center"><%# Eval("gaDP11") %></td>
                                                    <td style="text-align: right"><%# Eval("gaD12") %></td>
                                                    <td style="text-align: center"><%# Eval("gaDP12") %></td>
                                                    <td style="text-align: right"><%# Eval("gaDAcu") %></td>
                                                    <td style="text-align: center"><%# Eval("gaDPAcu") %></td>
                                                </tr>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                                    <thead>
                                                        <tr>
                                                            <th style="text-align: center">Gastos Administrativos</th>
                                                            <th style="text-align: center">Enero</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Febrero</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Marzo</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Abril</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Mayo</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Junio</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Julio</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Agosto</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Septiembre</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Octubre</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Noviembre</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Diciembre</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Acumulado</th>
                                                            <th style="text-align: center">%</th>
                                                        </tr>
                                                    </thead>
                                                    <tr>
                                                        <td colspan="27">
                                                            <label class="label label-danger">¡No hay Datos Registrados!</label></td>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
                                        </asp:ListView>
                                    </div>
                                    <br />
                                    <div class="table-responsive">
                                        <asp:ListView ID="lvGastosOperativos" runat="server">
                                            <LayoutTemplate>
                                                <table id="responsiveTabl" class="table table-striped table-bordered no-margin">
                                                    <thead>
                                                        <tr>
                                                            <th style="text-align: center; width:120px"></th>
                                                            <th style="text-align: center; width:120px">Enero</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Febrero</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Marzo</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Abril</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Mayo</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Junio</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Julio</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Agosto</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Septiembre</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Octubre</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Noviembre</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Diciembre</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Acumulado</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                        </tr>
                                                    </thead>
                                                    <tr id="itemPlaceholder" runat="server"></tr>
                                                   <%-- <tfoot>
                                                        <tr>
                                                            <th style="text-align: center">Total Gastos Operativos</th>
                                                            <th style="text-align: center"><%# Eval("go1") %></th>
                                                            <th style="text-align: center"><%# Eval("goP1") %></th>
                                                            <th style="text-align: center"><%# Eval("go2") %></th>
                                                            <th style="text-align: center"><%# Eval("goP2") %></th>
                                                            <th style="text-align: center"><%# Eval("go3") %></th>
                                                            <th style="text-align: center"><%# Eval("goP3") %></th>
                                                            <th style="text-align: center"><%# Eval("go4") %></th>
                                                            <th style="text-align: center"><%# Eval("goP4") %></th>
                                                            <th style="text-align: center"><%# Eval("go5") %></th>
                                                            <th style="text-align: center"><%# Eval("goP5") %></th>
                                                            <th style="text-align: center"><%# Eval("go6") %></th>
                                                            <th style="text-align: center"><%# Eval("goP6") %></th>
                                                            <th style="text-align: center"><%# Eval("go7") %></th>
                                                            <th style="text-align: center"><%# Eval("goP7") %></th>
                                                            <th style="text-align: center"><%# Eval("go8") %></th>
                                                            <th style="text-align: center"><%# Eval("goP8") %></th>
                                                            <th style="text-align: center"><%# Eval("go9") %></th>
                                                            <th style="text-align: center"><%# Eval("goP9") %></th>
                                                            <th style="text-align: center"><%# Eval("go10") %></th>
                                                            <th style="text-align: center"><%# Eval("goP10") %></th>
                                                            <th style="text-align: center"><%# Eval("go11") %></th>
                                                            <th style="text-align: center"><%# Eval("goP11") %></th>
                                                            <th style="text-align: center"><%# Eval("go12") %></th>
                                                            <th style="text-align: center"><%# Eval("goP12") %></th>
                                                            <th style="text-align: center"><%# Eval("goAcu") %></th>
                                                            <th style="text-align: center"><%# Eval("goPAcu") %></th>
                                                        </tr>
                                                    </tfoot>--%>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="text-align: left"><%# Eval("goD0") %></td>
                                                    <td style="text-align: right"><%# Eval("goD1") %></td>
                                                    <td style="text-align: center"><%# Eval("goDP1") %></td>
                                                    <td style="text-align: right"><%# Eval("goD2") %></td>
                                                    <td style="text-align: center"><%# Eval("goDP2") %></td>
                                                    <td style="text-align: right"><%# Eval("goD3") %></td>
                                                    <td style="text-align: center"><%# Eval("goDP3") %></td>
                                                    <td style="text-align: right"><%# Eval("goD4") %></td>
                                                    <td style="text-align: center"><%# Eval("goDP4") %></td>
                                                    <td style="text-align: right"><%# Eval("goD5") %></td>
                                                    <td style="text-align: center"><%# Eval("goDP5") %></td>
                                                    <td style="text-align: right"><%# Eval("goD6") %></td>
                                                    <td style="text-align: center"><%# Eval("goDP6") %></td>
                                                    <td style="text-align: right"><%# Eval("goD7") %></td>
                                                    <td style="text-align: center"><%# Eval("goDP7") %></td>
                                                    <td style="text-align: right"><%# Eval("goD8") %></td>
                                                    <td style="text-align: center"><%# Eval("goDP8") %></td>
                                                    <td style="text-align: right"><%# Eval("goD9") %></td>
                                                    <td style="text-align: center"><%# Eval("goDP9") %></td>
                                                    <td style="text-align: right"><%# Eval("goD10") %></td>
                                                    <td style="text-align: center"><%# Eval("goDP10") %></td>
                                                    <td style="text-align: right"><%# Eval("goD11") %></td>
                                                    <td style="text-align: center"><%# Eval("goDP11") %></td>
                                                    <td style="text-align: right"><%# Eval("goD12") %></td>
                                                    <td style="text-align: center"><%# Eval("goDP12") %></td>
                                                    <td style="text-align: right"><%# Eval("goDAcu") %></td>
                                                    <td style="text-align: center"><%# Eval("goDPAcu") %></td>
                                                </tr>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                                    <thead>
                                                        <tr>
                                                            <th style="text-align: center">Gastos Operativos</th>
                                                            <th style="text-align: center">Enero</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Febrero</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Marzo</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Abril</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Mayo</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Junio</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Julio</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Agosto</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Septiembre</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Octubre</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Noviembre</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Diciembre</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Acumulado</th>
                                                            <th style="text-align: center">%</th>
                                                        </tr>
                                                    </thead>
                                                    <tr>
                                                        <td colspan="27">
                                                            <label class="label label-danger">¡No hay Datos Registrados!</label></td>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
                                        </asp:ListView>
                                    </div>
                                    <br />
                                    <div class="table-responsive">
                                        <asp:ListView ID="lvUtilidadOperativa" runat="server">
                                            <LayoutTemplate>
                                                <table id="responsiveTabl" class="table table-striped table-bordered no-margin">
                                                    <thead>
                                                        <tr>
                                                            <th style="text-align: center; width:120px"></th>
                                                            <th style="text-align: center; width:120px">Enero</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Febrero</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Marzo</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Abril</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Mayo</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Junio</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Julio</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Agosto</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Septiembre</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Octubre</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Noviembre</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Diciembre</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Acumulado</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                        </tr>
                                                    </thead>
                                                    <tr id="itemPlaceholder" runat="server"></tr>
                                                 <%--   <tfoot>
                                                        <tr>
                                                            <th style="text-align: center">Total Utilidad Operativa</th>
                                                            <th style="text-align: center"><%# Eval("uo1") %></th>
                                                            <th style="text-align: center"><%# Eval("uoP1") %></th>
                                                            <th style="text-align: center"><%# Eval("uo2") %></th>
                                                            <th style="text-align: center"><%# Eval("uoP2") %></th>
                                                            <th style="text-align: center"><%# Eval("uo3") %></th>
                                                            <th style="text-align: center"><%# Eval("uoP3") %></th>
                                                            <th style="text-align: center"><%# Eval("uo4") %></th>
                                                            <th style="text-align: center"><%# Eval("uoP4") %></th>
                                                            <th style="text-align: center"><%# Eval("uo5") %></th>
                                                            <th style="text-align: center"><%# Eval("uoP5") %></th>
                                                            <th style="text-align: center"><%# Eval("uo6") %></th>
                                                            <th style="text-align: center"><%# Eval("uoP6") %></th>
                                                            <th style="text-align: center"><%# Eval("uo7") %></th>
                                                            <th style="text-align: center"><%# Eval("uoP7") %></th>
                                                            <th style="text-align: center"><%# Eval("uo8") %></th>
                                                            <th style="text-align: center"><%# Eval("uoP8") %></th>
                                                            <th style="text-align: center"><%# Eval("uo9") %></th>
                                                            <th style="text-align: center"><%# Eval("uoP9") %></th>
                                                            <th style="text-align: center"><%# Eval("uo10") %></th>
                                                            <th style="text-align: center"><%# Eval("uoP10") %></th>
                                                            <th style="text-align: center"><%# Eval("uo11") %></th>
                                                            <th style="text-align: center"><%# Eval("uoP11") %></th>
                                                            <th style="text-align: center"><%# Eval("uo12") %></th>
                                                            <th style="text-align: center"><%# Eval("uoP12") %></th>
                                                            <th style="text-align: center"><%# Eval("uoAcu") %></th>
                                                            <th style="text-align: center"><%# Eval("uoPAcu") %></th>
                                                        </tr>
                                                    </tfoot>--%>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="text-align: left"><%# Eval("uoD0") %></td>
                                                    <td style="text-align: right"><%# Eval("uoD1") %></td>
                                                    <td style="text-align: center"><%# Eval("uoDP1") %></td>
                                                    <td style="text-align: right"><%# Eval("uoD2") %></td>
                                                    <td style="text-align: center"><%# Eval("uoDP2") %></td>
                                                    <td style="text-align: right"><%# Eval("uoD3") %></td>
                                                    <td style="text-align: center"><%# Eval("uoDP3") %></td>
                                                    <td style="text-align: right"><%# Eval("uoD4") %></td>
                                                    <td style="text-align: center"><%# Eval("uoDP4") %></td>
                                                    <td style="text-align: right"><%# Eval("uoD5") %></td>
                                                    <td style="text-align: center"><%# Eval("uoDP5") %></td>
                                                    <td style="text-align: right"><%# Eval("uoD6") %></td>
                                                    <td style="text-align: center"><%# Eval("uoDP6") %></td>
                                                    <td style="text-align: right"><%# Eval("uoD7") %></td>
                                                    <td style="text-align: center"><%# Eval("uoDP7") %></td>
                                                    <td style="text-align: right"><%# Eval("uoD8") %></td>
                                                    <td style="text-align: center"><%# Eval("uoDP8") %></td>
                                                    <td style="text-align: right"><%# Eval("uoD9") %></td>
                                                    <td style="text-align: center"><%# Eval("uoDP9") %></td>
                                                    <td style="text-align: right"><%# Eval("uoD10") %></td>
                                                    <td style="text-align: center"><%# Eval("uoDP10") %></td>
                                                    <td style="text-align: right"><%# Eval("uoD11") %></td>
                                                    <td style="text-align: center"><%# Eval("uoDP11") %></td>
                                                    <td style="text-align: right"><%# Eval("uoD12") %></td>
                                                    <td style="text-align: center"><%# Eval("uoDP12") %></td>
                                                    <td style="text-align: right"><%# Eval("uoDAcu") %></td>
                                                    <td style="text-align: center"><%# Eval("uoDPAcu") %></td>
                                                </tr>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                                    <thead>
                                                        <tr>
                                                            <th style="text-align: center">Utilidad Operativa</th>
                                                            <th style="text-align: center">Enero</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Febrero</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Marzo</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Abril</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Mayo</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Junio</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Julio</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Agosto</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Septiembre</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Octubre</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Noviembre</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Diciembre</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Acumulado</th>
                                                            <th style="text-align: center">%</th>
                                                        </tr>
                                                    </thead>
                                                    <tr>
                                                        <td colspan="27">
                                                            <label class="label label-danger">¡No hay Datos Registrados!</label></td>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
                                        </asp:ListView>
                                    </div>
                                    <br />
                                    <div class="table-responsive">
                                        <asp:ListView ID="lvGastosFinancieros" runat="server">
                                            <LayoutTemplate>
                                                <table id="responsiveTabl" class="table table-striped table-bordered no-margin">
                                                    <thead>
                                                        <tr>
                                                            <th style="text-align: center; width:120px"></th>
                                                            <th style="text-align: center; width:120px">Enero</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Febrero</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Marzo</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Abril</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Mayo</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Junio</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Julio</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Agosto</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Septiembre</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Octubre</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Noviembre</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Diciembre</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Acumulado</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                        </tr>
                                                    </thead>
                                                    <tr id="itemPlaceholder" runat="server"></tr>
                                                  <%--  <tfoot>
                                                        <tr>
                                                            <th style="text-align: center">Total Gastos Financieros</th>
                                                            <th style="text-align: center"><%# Eval("gf1") %></th>
                                                            <th style="text-align: center"><%# Eval("gfP1") %></th>
                                                            <th style="text-align: center"><%# Eval("gf2") %></th>
                                                            <th style="text-align: center"><%# Eval("gfP2") %></th>
                                                            <th style="text-align: center"><%# Eval("gf3") %></th>
                                                            <th style="text-align: center"><%# Eval("gfP3") %></th>
                                                            <th style="text-align: center"><%# Eval("gf4") %></th>
                                                            <th style="text-align: center"><%# Eval("gfP4") %></th>
                                                            <th style="text-align: center"><%# Eval("gf5") %></th>
                                                            <th style="text-align: center"><%# Eval("gfP5") %></th>
                                                            <th style="text-align: center"><%# Eval("gf6") %></th>
                                                            <th style="text-align: center"><%# Eval("gfP6") %></th>
                                                            <th style="text-align: center"><%# Eval("gf7") %></th>
                                                            <th style="text-align: center"><%# Eval("gfP7") %></th>
                                                            <th style="text-align: center"><%# Eval("gf8") %></th>
                                                            <th style="text-align: center"><%# Eval("gfP8") %></th>
                                                            <th style="text-align: center"><%# Eval("gf9") %></th>
                                                            <th style="text-align: center"><%# Eval("gfP9") %></th>
                                                            <th style="text-align: center"><%# Eval("gf10") %></th>
                                                            <th style="text-align: center"><%# Eval("gfP10") %></th>
                                                            <th style="text-align: center"><%# Eval("gf11") %></th>
                                                            <th style="text-align: center"><%# Eval("gfP11") %></th>
                                                            <th style="text-align: center"><%# Eval("gf12") %></th>
                                                            <th style="text-align: center"><%# Eval("gfP12") %></th>
                                                            <th style="text-align: center"><%# Eval("gfAcu") %></th>
                                                            <th style="text-align: center"><%# Eval("gfPAcu") %></th>
                                                        </tr>
                                                    </tfoot>--%>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="text-align: left"><%# Eval("gfD0") %></td>
                                                    <td style="text-align: right"><%# Eval("gfD1") %></td>
                                                    <td style="text-align: center"><%# Eval("gfDP1") %></td>
                                                    <td style="text-align: right"><%# Eval("gfD2") %></td>
                                                    <td style="text-align: center"><%# Eval("gfDP2") %></td>
                                                    <td style="text-align: right"><%# Eval("gfD3") %></td>
                                                    <td style="text-align: center"><%# Eval("gfDP3") %></td>
                                                    <td style="text-align: right"><%# Eval("gfD4") %></td>
                                                    <td style="text-align: center"><%# Eval("gfDP4") %></td>
                                                    <td style="text-align: right"><%# Eval("gfD5") %></td>
                                                    <td style="text-align: center"><%# Eval("gfDP5") %></td>
                                                    <td style="text-align: right"><%# Eval("gfD6") %></td>
                                                    <td style="text-align: center"><%# Eval("gfDP6") %></td>
                                                    <td style="text-align: right"><%# Eval("gfD7") %></td>
                                                    <td style="text-align: center"><%# Eval("gfDP7") %></td>
                                                    <td style="text-align: right"><%# Eval("gfD8") %></td>
                                                    <td style="text-align: center"><%# Eval("gfDP8") %></td>
                                                    <td style="text-align: right"><%# Eval("gfD9") %></td>
                                                    <td style="text-align: center"><%# Eval("gfDP9") %></td>
                                                    <td style="text-align: right"><%# Eval("gfD10") %></td>
                                                    <td style="text-align: center"><%# Eval("gfDP10") %></td>
                                                    <td style="text-align: right"><%# Eval("gfD11") %></td>
                                                    <td style="text-align: center"><%# Eval("gfDP11") %></td>
                                                    <td style="text-align: right"><%# Eval("gfD12") %></td>
                                                    <td style="text-align: center"><%# Eval("gfDP12") %></td>
                                                    <td style="text-align: right"><%# Eval("gfDAcu") %></td>
                                                    <td style="text-align: center"><%# Eval("gfDPAcu") %></td>
                                                </tr>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                                    <thead>
                                                        <tr>
                                                            <th style="text-align: center">Gastos Financieros</th>
                                                            <th style="text-align: center">Enero</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Febrero</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Marzo</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Abril</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Mayo</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Junio</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Julio</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Agosto</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Septiembre</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Octubre</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Noviembre</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Diciembre</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Acumulado</th>
                                                            <th style="text-align: center">%</th>
                                                        </tr>
                                                    </thead>
                                                    <tr>
                                                        <td colspan="27">
                                                            <label class="label label-danger">¡No hay Datos Registrados!</label></td>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
                                        </asp:ListView>
                                    </div>
                                    <br />
                                    <div class="table-responsive">
                                        <asp:ListView ID="lvUtilidadAntesImpuestos" runat="server">
                                            <LayoutTemplate>
                                                <table id="responsiveTabl" class="table table-striped table-bordered no-margin">
                                                    <thead>
                                                        <tr>
                                                            <th style="text-align: center; width:120px"></th>
                                                            <th style="text-align: center; width:120px">Enero</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Febrero</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Marzo</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Abril</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Mayo</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Junio</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Julio</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Agosto</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Septiembre</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Octubre</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Noviembre</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Diciembre</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                            <th style="text-align: center; width:120px">Acumulado</th>
                                                            <th style="text-align: center; width:60px">%</th>
                                                        </tr>
                                                    </thead>
                                                    <tr id="itemPlaceholder" runat="server"></tr>
                                                 <%--   <tfoot>
                                                        <tr>
                                                            <th style="text-align: center"><%# Eval("uai0") %></th>
                                                            <th style="text-align: center"><%# Eval("uai1") %></th>
                                                            <th style="text-align: center"><%# Eval("uaiP1") %></th>
                                                            <th style="text-align: center"><%# Eval("uai2") %></th>
                                                            <th style="text-align: center"><%# Eval("uaiP2") %></th>
                                                            <th style="text-align: center"><%# Eval("uai3") %></th>
                                                            <th style="text-align: center"><%# Eval("uaiP3") %></th>
                                                            <th style="text-align: center"><%# Eval("uai4") %></th>
                                                            <th style="text-align: center"><%# Eval("uaiP4") %></th>
                                                            <th style="text-align: center"><%# Eval("uai5") %></th>
                                                            <th style="text-align: center"><%# Eval("uaiP5") %></th>
                                                            <th style="text-align: center"><%# Eval("uai6") %></th>
                                                            <th style="text-align: center"><%# Eval("uaiP6") %></th>
                                                            <th style="text-align: center"><%# Eval("uai7") %></th>
                                                            <th style="text-align: center"><%# Eval("uaiP7") %></th>
                                                            <th style="text-align: center"><%# Eval("uai8") %></th>
                                                            <th style="text-align: center"><%# Eval("uaiP8") %></th>
                                                            <th style="text-align: center"><%# Eval("uai9") %></th>
                                                            <th style="text-align: center"><%# Eval("uaiP9") %></th>
                                                            <th style="text-align: center"><%# Eval("uai10") %></th>
                                                            <th style="text-align: center"><%# Eval("uaiP10") %></th>
                                                            <th style="text-align: center"><%# Eval("uai11") %></th>
                                                            <th style="text-align: center"><%# Eval("uaiP11") %></th>
                                                            <th style="text-align: center"><%# Eval("uai12") %></th>
                                                            <th style="text-align: center"><%# Eval("uaiP12") %></th>
                                                            <th style="text-align: center"><%# Eval("uaiAcu") %></th>
                                                            <th style="text-align: center"><%# Eval("uaiPAcu") %></th>
                                                        </tr>
                                                    </tfoot>--%>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="text-align: left"><%# Eval("uaiD0") %></td>
                                                    <td style="text-align: right"><%# Eval("uaiD1") %></td>
                                                    <td style="text-align: center"><%# Eval("uaiDP1") %></td>
                                                    <td style="text-align: right"><%# Eval("uaiD2") %></td>
                                                    <td style="text-align: center"><%# Eval("uaiDP2") %></td>
                                                    <td style="text-align: right"><%# Eval("uaiD3") %></td>
                                                    <td style="text-align: center"><%# Eval("uaiDP3") %></td>
                                                    <td style="text-align: right"><%# Eval("uaiD4") %></td>
                                                    <td style="text-align: center"><%# Eval("uaiDP4") %></td>
                                                    <td style="text-align: right"><%# Eval("uaiD5") %></td>
                                                    <td style="text-align: center"><%# Eval("uaiDP5") %></td>
                                                    <td style="text-align: right"><%# Eval("uaiD6") %></td>
                                                    <td style="text-align: center"><%# Eval("uaiDP6") %></td>
                                                    <td style="text-align: right"><%# Eval("uaiD7") %></td>
                                                    <td style="text-align: center"><%# Eval("uaiDP7") %></td>
                                                    <td style="text-align: right"><%# Eval("uaiD8") %></td>
                                                    <td style="text-align: center"><%# Eval("uaiDP8") %></td>
                                                    <td style="text-align: right"><%# Eval("uaiD9") %></td>
                                                    <td style="text-align: center"><%# Eval("uaiDP9") %></td>
                                                    <td style="text-align: right"><%# Eval("uaiD10") %></td>
                                                    <td style="text-align: center"><%# Eval("uaiDP10") %></td>
                                                    <td style="text-align: right"><%# Eval("uaiD11") %></td>
                                                    <td style="text-align: center"><%# Eval("uaiDP11") %></td>
                                                    <td style="text-align: right"><%# Eval("uaiD12") %></td>
                                                    <td style="text-align: center"><%# Eval("uaiDP12") %></td>
                                                    <td style="text-align: right"><%# Eval("uaiDAcu") %></td>
                                                    <td style="text-align: center"><%# Eval("uaiDPAcu") %></td>
                                                </tr>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                                    <thead>
                                                        <tr>
                                                            <th style="text-align: center">Utilidad Antes de Impuestos</th>
                                                            <th style="text-align: center">Enero</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Febrero</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Marzo</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Abril</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Mayo</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Junio</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Julio</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Agosto</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Septiembre</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Octubre</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Noviembre</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Diciembre</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Acumulado</th>
                                                            <th style="text-align: center">%</th>
                                                        </tr>
                                                    </thead>
                                                    <tr>
                                                        <td colspan="27">
                                                            <label class="label label-danger">¡No hay Datos Registrados!</label></td>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
                                        </asp:ListView>
                                    </div>
                                    <br />
                                    <div class="table-responsive">
                                        <asp:ListView ID="lvTotalImpuestos" runat="server">
                                            <LayoutTemplate>
                                                <table id="responsiveTabl" class="table table-striped table-bordered no-margin">
                                                    <thead>
                                                        <tr>
                                                            <th style="text-align: center"></th>
                                                            <th style="text-align: center">Enero</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Febrero</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Marzo</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Abril</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Mayo</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Junio</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Julio</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Agosto</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Septiembre</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Octubre</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Noviembre</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Diciembre</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Acumulado</th>
                                                            <th style="text-align: center">%</th>
                                                        </tr>
                                                    </thead>
                                                    <tr id="itemPlaceholder" runat="server"></tr>
                                                   <%-- <tfoot>
                                                        <tr>
                                                            <th style="text-align: center"><%# Eval("ti0") %></th>
                                                            <th style="text-align: center"><%# Eval("ti1") %></th>
                                                            <th style="text-align: center"><%# Eval("tiP1") %></th>
                                                            <th style="text-align: center"><%# Eval("ti2") %></th>
                                                            <th style="text-align: center"><%# Eval("tiP2") %></th>
                                                            <th style="text-align: center"><%# Eval("ti3") %></th>
                                                            <th style="text-align: center"><%# Eval("tiP3") %></th>
                                                            <th style="text-align: center"><%# Eval("ti4") %></th>
                                                            <th style="text-align: center"><%# Eval("tiP4") %></th>
                                                            <th style="text-align: center"><%# Eval("ti5") %></th>
                                                            <th style="text-align: center"><%# Eval("tiP5") %></th>
                                                            <th style="text-align: center"><%# Eval("ti6") %></th>
                                                            <th style="text-align: center"><%# Eval("tiP6") %></th>
                                                            <th style="text-align: center"><%# Eval("ti7") %></th>
                                                            <th style="text-align: center"><%# Eval("tiP7") %></th>
                                                            <th style="text-align: center"><%# Eval("ti8") %></th>
                                                            <th style="text-align: center"><%# Eval("tiP8") %></th>
                                                            <th style="text-align: center"><%# Eval("ti9") %></th>
                                                            <th style="text-align: center"><%# Eval("tiP9") %></th>
                                                            <th style="text-align: center"><%# Eval("ti10") %></th>
                                                            <th style="text-align: center"><%# Eval("tiP10") %></th>
                                                            <th style="text-align: center"><%# Eval("ti11") %></th>
                                                            <th style="text-align: center"><%# Eval("tiP11") %></th>
                                                            <th style="text-align: center"><%# Eval("ti12") %></th>
                                                            <th style="text-align: center"><%# Eval("tiP12") %></th>
                                                            <th style="text-align: center"><%# Eval("tiAcu") %></th>
                                                            <th style="text-align: center"><%# Eval("tiPAcu") %></th>
                                                        </tr>
                                                    </tfoot>--%>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="text-align: left"><%# Eval("tiD0") %></td>
                                                    <td style="text-align: right"><%# Eval("tiD1") %></td>
                                                    <td style="text-align: center"><%# Eval("tiDP1") %></td>
                                                    <td style="text-align: right"><%# Eval("tiD2") %></td>
                                                    <td style="text-align: center"><%# Eval("tiDP2") %></td>
                                                    <td style="text-align: right"><%# Eval("tiD3") %></td>
                                                    <td style="text-align: center"><%# Eval("tiDP3") %></td>
                                                    <td style="text-align: right"><%# Eval("tiD4") %></td>
                                                    <td style="text-align: center"><%# Eval("tiDP4") %></td>
                                                    <td style="text-align: right"><%# Eval("tiD5") %></td>
                                                    <td style="text-align: center"><%# Eval("tiDP5") %></td>
                                                    <td style="text-align: right"><%# Eval("tiD6") %></td>
                                                    <td style="text-align: center"><%# Eval("tiDP6") %></td>
                                                    <td style="text-align: right"><%# Eval("tiD7") %></td>
                                                    <td style="text-align: center"><%# Eval("tiDP7") %></td>
                                                    <td style="text-align: right"><%# Eval("tiD8") %></td>
                                                    <td style="text-align: center"><%# Eval("tiDP8") %></td>
                                                    <td style="text-align: right"><%# Eval("tiD9") %></td>
                                                    <td style="text-align: center"><%# Eval("tiDP9") %></td>
                                                    <td style="text-align: right"><%# Eval("tiD10") %></td>
                                                    <td style="text-align: center"><%# Eval("tiDP10") %></td>
                                                    <td style="text-align: right"><%# Eval("tiD11") %></td>
                                                    <td style="text-align: center"><%# Eval("tiDP11") %></td>
                                                    <td style="text-align: right"><%# Eval("tiD12") %></td>
                                                    <td style="text-align: center"><%# Eval("tiDP12") %></td>
                                                    <td style="text-align: right"><%# Eval("tiDAcu") %></td>
                                                    <td style="text-align: center"><%# Eval("tiDPAcu") %></td>
                                                </tr>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                                    <thead>
                                                        <tr>
                                                            <th style="text-align: center">Total de Impuestos</th>
                                                            <th style="text-align: center">Enero</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Febrero</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Marzo</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Abril</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Mayo</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Junio</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Julio</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Agosto</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Septiembre</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Octubre</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Noviembre</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Diciembre</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Acumulado</th>
                                                            <th style="text-align: center">%</th>
                                                        </tr>
                                                    </thead>
                                                    <tr>
                                                        <td colspan="27">
                                                            <label class="label label-danger">¡No hay Datos Registrados!</label></td>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
                                        </asp:ListView>
                                    </div>
                                    <br />
                                    <div class="table-responsive">
                                        <asp:ListView ID="lvUtilidadNeta" runat="server">
                                            <LayoutTemplate>
                                                <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                                    <thead>
                                                        <tr>
                                                            <th style="text-align: center"></th>
                                                            <th style="text-align: center">Enero</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Febrero</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Marzo</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Abril</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Mayo</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Junio</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Julio</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Agosto</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Septiembre</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Octubre</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Noviembre</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Diciembre</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Acumulado</th>
                                                            <th style="text-align: center">%</th>
                                                        </tr>
                                                    </thead>
                                                    <tr id="itemPlaceholder" runat="server"></tr>
                                                    <tfoot>
                                                        <tr>
                                                            <th style="text-align: center"><%# Eval("un0") %></th>
                                                            <th style="text-align: center"><%# Eval("un1") %></th>
                                                            <th style="text-align: center"><%# Eval("unP1") %></th>
                                                            <th style="text-align: center"><%# Eval("un2") %></th>
                                                            <th style="text-align: center"><%# Eval("unP2") %></th>
                                                            <th style="text-align: center"><%# Eval("un3") %></th>
                                                            <th style="text-align: center"><%# Eval("unP3") %></th>
                                                            <th style="text-align: center"><%# Eval("un4") %></th>
                                                            <th style="text-align: center"><%# Eval("unP4") %></th>
                                                            <th style="text-align: center"><%# Eval("un5") %></th>
                                                            <th style="text-align: center"><%# Eval("unP5") %></th>
                                                            <th style="text-align: center"><%# Eval("un6") %></th>
                                                            <th style="text-align: center"><%# Eval("unP6") %></th>
                                                            <th style="text-align: center"><%# Eval("un7") %></th>
                                                            <th style="text-align: center"><%# Eval("unP7") %></th>
                                                            <th style="text-align: center"><%# Eval("un8") %></th>
                                                            <th style="text-align: center"><%# Eval("unP8") %></th>
                                                            <th style="text-align: center"><%# Eval("un9") %></th>
                                                            <th style="text-align: center"><%# Eval("unP9") %></th>
                                                            <th style="text-align: center"><%# Eval("un10") %></th>
                                                            <th style="text-align: center"><%# Eval("unP10") %></th>
                                                            <th style="text-align: center"><%# Eval("un11") %></th>
                                                            <th style="text-align: center"><%# Eval("unP11") %></th>
                                                            <th style="text-align: center"><%# Eval("un12") %></th>
                                                            <th style="text-align: center"><%# Eval("unP12") %></th>
                                                            <th style="text-align: center"><%# Eval("unAcu") %></th>
                                                            <th style="text-align: center"><%# Eval("unPAcu") %></th>
                                                        </tr>
                                                    </tfoot>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="text-align: left"><%# Eval("unD0") %></td>
                                                    <td style="text-align: right"><%# Eval("unD1") %></td>
                                                    <td style="text-align: center"><%# Eval("unDP1") %></td>
                                                    <td style="text-align: right"><%# Eval("unD2") %></td>
                                                    <td style="text-align: center"><%# Eval("unDP2") %></td>
                                                    <td style="text-align: right"><%# Eval("unD3") %></td>
                                                    <td style="text-align: center"><%# Eval("unDP3") %></td>
                                                    <td style="text-align: right"><%# Eval("unD4") %></td>
                                                    <td style="text-align: center"><%# Eval("unDP4") %></td>
                                                    <td style="text-align: right"><%# Eval("unD5") %></td>
                                                    <td style="text-align: center"><%# Eval("unDP5") %></td>
                                                    <td style="text-align: right"><%# Eval("unD6") %></td>
                                                    <td style="text-align: center"><%# Eval("unDP6") %></td>
                                                    <td style="text-align: right"><%# Eval("unD7") %></td>
                                                    <td style="text-align: center"><%# Eval("unDP7") %></td>
                                                    <td style="text-align: right"><%# Eval("unD8") %></td>
                                                    <td style="text-align: center"><%# Eval("unDP8") %></td>
                                                    <td style="text-align: right"><%# Eval("unD9") %></td>
                                                    <td style="text-align: center"><%# Eval("unDP9") %></td>
                                                    <td style="text-align: right"><%# Eval("unD10") %></td>
                                                    <td style="text-align: center"><%# Eval("unDP10") %></td>
                                                    <td style="text-align: right"><%# Eval("unD11") %></td>
                                                    <td style="text-align: center"><%# Eval("unDP11") %></td>
                                                    <td style="text-align: right"><%# Eval("unD12") %></td>
                                                    <td style="text-align: center"><%# Eval("unDP12") %></td>
                                                    <td style="text-align: right"><%# Eval("unDAcu") %></td>
                                                    <td style="text-align: center"><%# Eval("unDPAcu") %></td>
                                                </tr>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                                    <thead>
                                                        <tr>
                                                            <th style="text-align: center">Utilidad Neta</th>
                                                            <th style="text-align: center">Enero</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Febrero</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Marzo</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Abril</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Mayo</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Junio</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Julio</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Agosto</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Septiembre</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Octubre</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Noviembre</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Diciembre</th>
                                                            <th style="text-align: center">%</th>
                                                            <th style="text-align: center">Acumulado</th>
                                                            <th style="text-align: center">%</th>
                                                        </tr>
                                                    </thead>
                                                    <tr>
                                                        <td colspan="27">
                                                            <label class="label label-danger">¡No hay Datos Registrados!</label></td>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
                                        </asp:ListView>
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

