<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rProgra.aspx.cs" Inherits="despacho.rProgra" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

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

    <script type="text/javascript">
        function ShowPopup() {
            $("#btnShowPopup").click();
        }

        function ClosePopup() {
            $("#btnClosePopup").click();
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }

        function ShowPopupCot() {
            $("#btnShowPopupCot").click();
        }

        function ClosePopupCot() {
            $("#btnClosePopupCot").click();
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }

        function ShowPopupAjuste() {
            $("#btnShowPopupAjuste").click();
        }

        function ClosePopupAjuste() {
            $("#btnClosePopupAjuste").click();
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }

        function ShowPopupRemPDF() {
            $("#btnShowPopupRemPDF").click();
        }

        function ClosePopupARemPDF() {
            $("#btnClosePopupRemPDF").click();
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }

        function ShowPopupAsigU() {
            $("#btnShowPopupAsigU").click();
        }

        function ClosePopupAsigU() {
            $("#btnClosePopupAsigU").click();
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }

        function onlyDotsAndNumbers(event) {
            var charCode = (event.which) ? event.which : event.keyCode
            if (charCode == 46) {
                return true;
            }
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }

        function onlyNumbers(event) {
            var charCode = (event.which) ? event.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
    </script>
    <script>
        function openPAU(idOD) {
            var posicion_x;
            var posicion_y;
            posicion_x = (screen.width / 2) - (500 / 2);
            posicion_y = (screen.height / 2) - (400 / 2);

            open("pAsignarUnidad.aspx?idOD=" + idOD, '', 'top=' + posicion_y + ', left=' + posicion_x + ', width=800, height=650');
        }
        function openPBIT(idOD) {
            var posicion_x;
            var posicion_y;
            posicion_x = (screen.width / 2) - (500 / 2);
            posicion_y = (screen.height / 2) - (400 / 2);

            open("pBitacoraInsidenciasOD.aspx?idOD=" + idOD, '', 'top=' + posicion_y + ', left=' + posicion_x + ', width=800, height=650');
        }
        function openPMAP(idOD) {
            var posicion_x;
            var posicion_y;
            posicion_x = (screen.width / 2) - (500 / 2);
            posicion_y = (screen.height / 2) - (400 / 2);

            open("pMapa.aspx?idOD=" + idOD, '', 'top=' + posicion_y + ', left=' + posicion_x + ', width=800, height=650');
        }
        function openPCALIDAD(idOD) {
            var posicion_x;
            var posicion_y;
            posicion_x = (screen.width / 2) - (500 / 2);
            posicion_y = (screen.height / 2) - (400 / 2);

            open("pCalidad.aspx?idOD=" + idOD, '', 'top=' + posicion_y + ', left=' + posicion_x + ', width=1080, height=768');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <!-- Dashboard Wrapper Start -->
            <div class="">
                <div class="top-bar clearfix">
                    <%--<div class="row gutter">
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                            <div class="page-title">
                                <h3>Reporte de Programación</h3>
                                <p><a href="home.aspx">Home</a></p>
                            </div>
                        </div>
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                            <ul class="right-stats" id="mini-nav-right">
                                <%--<li>
									<a href="javascript:void(0)" class="btn btn-danger"><span>76</span>Sales</a>
								</li>
								<li>
									<a href="tasks.html" class="btn btn-success">
										<span>18</span>Tasks</a>
								</li>
                                <li>
                                    <a href="home.aspx" class="btn btn-info">
                                        <i class="icon-bulb"></i>Ayuda
                                    </a>
                                </li>
                            </ul>
                        </div>
                    </div>--%>
                    <asp:UpdatePanel ID="upGrid" runat="server">
                        <ContentTemplate>
                            <div class="row gutter">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <div class="panel panel-blue">
                                        <div class="row gutter">
                                            <div class="panel-heading">
                                                <h4>
                                                    <br />
                                                    <asp:CheckBox ID="chbxTerminados" runat="server" AutoPostBack="True" Checked="false" class="radio-inline" OnCheckedChanged="chbxTerminados_CheckedChanged" Text="Terminados" Visible="False" />
                                                </h4>
                                            </div>
                                            <div class="panel-body">
                                                <div class="row gutter">
                                                    <div class="col-md-12">
                                                        Filtrar: 
                                                    </div>
                                                </div>
                                                <div class="row gutter">
                                                    <div class="col-md-2">
                                                        <br />
                                                        <asp:DropDownList ID="ddlFFiltros" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlFFiltros_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Ayer</asp:ListItem>
                                                            <asp:ListItem Selected="True" Value="1">Hoy</asp:ListItem>
                                                            <asp:ListItem Value="2">Mañana</asp:ListItem>
                                                            <asp:ListItem Value="3" Enabled="False">Programados</asp:ListItem>
                                                            <asp:ListItem Value="4" Enabled="False">Sin Programar</asp:ListItem>
                                                            <asp:ListItem Value="5">Entre Fechas</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:Label ID="lblFechaI" runat="server" Text="Fecha Inicio:" class="control-label" Visible="False"></asp:Label><asp:TextBox ID="txtFechaI" runat="server" class="form-control" Visible="False"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="txtFechaI_CalendarExtender" runat="server" BehaviorID="txtFechaI_CalendarExtender" TargetControlID="txtFechaI" FirstDayOfWeek="Monday" Format="yyyy-MM-dd" PopupPosition="BottomRight" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:Label ID="lblFechaF" runat="server" Text="Fecha Fin" class="control-label" Visible="False"></asp:Label><asp:TextBox ID="txtFechaF" runat="server" class="form-control" Visible="False"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="txtFechaF_CalendarExtender" runat="server" BehaviorID="txtFechaF_CalendarExtender" TargetControlID="txtFechaF" FirstDayOfWeek="Monday" Format="yyyy-MM-dd" PopupPosition="BottomRight" />
                                                    </div>
                                                    <div class="col-md-6">
                                                    </div>
                                                </div>
                                                <div class="row gutter">
                                                    <div class="col-md-2">
                                                        <asp:Label ID="Label2" runat="server" Text="Cliente:" class="control-label" Visible="True"></asp:Label>
                                                        <ajaxToolkit:ComboBox ID="cbxClientes" runat="server" AutoCompleteMode="SuggestAppend" DataTextField="cliente" DataValueField="clave" DropDownStyle="Simple" Visible="True" AutoPostBack="True" OnSelectedIndexChanged="cbxClientes_SelectedIndexChanged"></ajaxToolkit:ComboBox>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:Label ID="Label1" runat="server" Text="Vendedor:" class="control-label" Visible="True"></asp:Label>
                                                        <ajaxToolkit:ComboBox ID="cbxVendedores" runat="server" AutoCompleteMode="SuggestAppend" DataTextField="vendedor" DataValueField="idV" DropDownStyle="Simple" Visible="True" AutoPostBack="True" OnSelectedIndexChanged="cbxVendedores_SelectedIndexChanged"></ajaxToolkit:ComboBox>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:Label ID="Label4" runat="server" Text="Producto:" class="control-label" Visible="True"></asp:Label>
                                                        <ajaxToolkit:ComboBox ID="cbxProductos" runat="server" AutoCompleteMode="SuggestAppend" DataTextField="codigo" DataValueField="codigo" DropDownStyle="Simple" Visible="True" AutoPostBack="True" OnSelectedIndexChanged="cbxProductos_SelectedIndexChanged"></ajaxToolkit:ComboBox>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:Label ID="Label5" runat="server" Text="Unidad:" class="control-label" Visible="True"></asp:Label>
                                                        <ajaxToolkit:ComboBox ID="cbxUnidad" runat="server" AutoCompleteMode="SuggestAppend" DataTextField="uNombre" DataValueField="uNombre" DropDownStyle="Simple" Visible="True" AutoPostBack="True" OnSelectedIndexChanged="cbxUnidad_SelectedIndexChanged"></ajaxToolkit:ComboBox>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:Label ID="Label3" runat="server" Text="Chofer:" class="control-label" Visible="True"></asp:Label>
                                                        <ajaxToolkit:ComboBox ID="cbxChoferes" runat="server" AutoCompleteMode="SuggestAppend" DataTextField="chofer" DataValueField="idCh" DropDownStyle="Simple" Visible="True" AutoPostBack="True" OnSelectedIndexChanged="cbxChoferes_SelectedIndexChanged"></ajaxToolkit:ComboBox>
                                                    </div>
                                                    <div class="col-md-2">
                                                    </div>
                                                </div>
                                                <div class="row gutter">
                                                    <div class="col-md-1">
                                                        <br />
                                                        <asp:Button ID="btnFiltrar" runat="server" Text="Generar" class="btn btn-info" OnClick="btnFiltrar_Click" />
                                                    </div>
                                                    <div class="col-md-1">
                                                        <br />
                                                        <asp:Button ID="btnExportarExcel" runat="server" Text="Exportar a Excel" class="btn btn-info" OnClick="btnExportarExcel_Click" Visible="true" />
                                                    </div>
                                                    <div class="col-md-1">
                                                        <br />
                                                        <asp:Button ID="btnExportar" runat="server" Text="Exportar a PDF" class="btn btn-info" OnClick="btnExportar_Click" Visible="False" />
                                                        <asp:Button ID="btnVerPDF" runat="server" Text="Ver PDF" class="btn btn-info" OnClick="btnVerPDF_Click" Visible="False" />
                                                    </div>
                                                    <div class="col-md-1">
                                                        <br />
                                                    </div>
                                                    <div class="col-md-8">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- Top Bar Ends -->

                            <!-- Row Starts -->
                            <div id="ContentPlaceHolder_upGrid">
                                <div class="row gutter">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                        <div class="panel panel-blue">
                                            <div class="panel-heading">
                                                <h4></h4>
                                            </div>
                                            <div class="panel-body">
                                                <div class="table-responsive" runat="server">
                                                    <asp:GridView ID="gvOD" runat="server" AutoGenerateColumns="False" class="table table-bordered table-hover table-condensed no-margin">
                                                        <Columns>
                                                            <asp:BoundField DataField="folioOrden" HeaderText="Folio" SortExpression="folioOrden" />
                                                            <asp:BoundField DataField="fecha" HeaderText="Fecha" SortExpression="fecha" />
                                                            <asp:BoundField DataField="hora" HeaderText="Hora" SortExpression="hora" />
                                                            <asp:BoundField DataField="vendedor" HeaderText="Vendedor" SortExpression="vendedor" />
                                                            <asp:BoundField DataField="clave" HeaderText="Clave" SortExpression="clave" />
                                                            <asp:BoundField DataField="cliente" HeaderText="Cliente" SortExpression="cliente" />
                                                            <asp:BoundField DataField="calle" HeaderText="Calle" SortExpression="calle" />
                                                            <asp:BoundField DataField="numero" HeaderText="Número" SortExpression="numero" />
                                                            <asp:BoundField DataField="colonia" HeaderText="Colonia" SortExpression="colonia" />
                                                            <asp:BoundField DataField="cp" HeaderText="Código Postal" SortExpression="cp" />
                                                            <asp:BoundField DataField="codigo" HeaderText="Producto" SortExpression="codigo" />
                                                            <asp:BoundField DataField="cantOrdenada" HeaderText="Cantidad" SortExpression="cantOrdenada" />
                                                            <asp:BoundField DataField="elemento" HeaderText="Elemento" SortExpression="elemento" />
                                                            <asp:BoundField DataField="revenimiento" HeaderText="Revenimiento" SortExpression="revenimiento" />
                                                            <%--<asp:BoundField DataField="estado" HeaderText="Estatus" SortExpression="estado" />
                                                            <asp:BoundField DataField="uNombre" HeaderText="Unidad" SortExpression="uNombre" />
                                                            <asp:BoundField DataField="chofer" HeaderText="Chofer" SortExpression="chofer" />--%>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <%--<asp:ListView ID="lvOD" runat="server">
                                                        <LayoutTemplate>
                                                            <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                                                <thead>
                                                                    <tr>
                                                                        <th style="text-align: center" id="folio">Folio</th>
                                                                        <th style="text-align: center" id="fecha">Fecha</th>
                                                                        <th style="text-align: center" id="hora">Hora</th>
                                                                        <th id="vendedor">Vendedor</th>
                                                                        <th id="cliente">Cliente</th>
                                                                        <th style="text-align: center" id="direccion">Dirección de Entrega</th>
                                                                        <th style="text-align: center" id="codigo">Producto</th>
                                                                        <th style="text-align: center" id="cantidad">Cantidad</th>
                                                                        <th style="text-align: center" id="elemento">Elemento</th>
                                                                        <th style="text-align: center" id="revenimiento">Revenimiento</th>
                                                                        <th style="text-align: center" id="estatus">Estatus</th>
                                                                        <th style="text-align: center" id="unidad">Unidad</th>
                                                                        <th style="text-align: center" id="chofer">Chofer</th>
                                                                    </tr>
                                                                </thead>
                                                                <tr id="itemPlaceholder" runat="server"></tr>
                                                                <tfoot>
                                                                    <tr>
                                                                        <th style="text-align: center">Folio</th>
                                                                        <th style="text-align: center">Fecha</th>
                                                                        <th style="text-align: center">Hora</th>
                                                                        <th>Vendedor</th>
                                                                        <th>Cliente</th>
                                                                        <th style="text-align: center">Dirección de Entrega</th>
                                                                        <th style="text-align: center">Producto</th>
                                                                        <th style="text-align: center">Cantidad</th>
                                                                        <th style="text-align: center">Elemento</th>
                                                                        <th style="text-align: center">Revenimiento</th>
                                                                        <th style="text-align: center">Estatus</th>
                                                                        <th style="text-align: center">Unidad</th>
                                                                        <th style="text-align: center">Chofer</th>
                                                                    </tr>
                                                                </tfoot>
                                                            </table>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td style="text-align: center"><%# Eval("folio").ToString().Trim() %></td>
                                                                <td style="text-align: center"><%# Eval("fecha").ToString().Substring(0,10) %></td>
                                                                <td style="text-align: center"><%# Eval("hora").ToString().Trim() %></td>
                                                                <td><%# Eval("vendedor").ToString().Trim() %></td>
                                                                <td><%# Eval("clave") + " " + Eval("cliente").ToString().Trim() %></td>
                                                                <td style="text-align: center"><%# Eval("calle") + " " + Eval("numero") + " " + Eval("colonia") + " " + Eval("cp") %></td>
                                                                <td style="text-align: center"><%# Eval("codigo").ToString().Trim() %></td>
                                                                <td style="text-align: center"><%# Eval("cantidad") + " " + Eval("unidad").ToString().Trim() %></td>
                                                                <td style="text-align: center"><%# Eval("elemento").ToString().Trim() %></td>
                                                                <td style="text-align: center"><%# Eval("revenimiento").ToString().Trim() %></td>
                                                                <td style="text-align: center"><%# Eval("estado").ToString().Trim() %></td>
                                                                <td style="text-align: center"><%# Eval("uNombre").ToString().Trim() %></td>
                                                                <td style="text-align: center"><%# Eval("chofer").ToString().Trim() %></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <EmptyDataTemplate>
                                                            <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                                                <thead>
                                                                    <tr>
                                                                        <th style="text-align: center" id="folio">Folio</th>
                                                                        <th style="text-align: center" id="fecha">Fecha</th>
                                                                        <th style="text-align: center" id="hora">Hora</th>
                                                                        <th id="vendedor">Vendedor</th>
                                                                        <th id="cliente">Cliente</th>
                                                                        <th style="text-align: center" id="direccion">Dirección de Entrega</th>
                                                                        <th style="text-align: center" id="codigo">Producto</th>
                                                                        <th style="text-align: center" id="cantidad">Cantidad</th>
                                                                        <th style="text-align: center" id="elemento">Elemento</th>
                                                                        <th style="text-align: center" id="revenimiento">Revenimiento</th>
                                                                        <th style="text-align: center" id="estatus">Estatus</th>
                                                                        <th style="text-align: center" id="unidad">Unidad</th>
                                                                        <th style="text-align: center" id="chofer">Chofer</th>
                                                                    </tr>
                                                                </thead>
                                                                <tr>
                                                                    <td colspan="13">
                                                                        <label class="label label-danger">¡No hay Fórmulas Registradas!</label></td>
                                                                </tr>
                                                            </table>
                                                        </EmptyDataTemplate>
                                                    </asp:ListView>--%>
                                                    <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
                                                </div>
                                            </div>
                                            <!-- Button trigger modal -->
                                            <button type="button" style="display: none;" id="btnShowPopupCot" class="btn btn-primary btn-lg"
                                                data-toggle="modal" data-target="#myModalCot">
                                                Launch demo modal
                                            </button>

                                            <!-- Modal -->
                                            <div class="modal fade" id="myModalCot">
                                                <div class="modal-dialog modal-lg">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <asp:LinkButton ID="lbtnCloseModalCot" class="close" OnClick="lbtnCloseModalCot_Click" runat="server"><span aria-hidden="true">&times;</span></asp:LinkButton>
                                                            <h4 class="modal-title">
                                                                <asp:Label ID="mlblTitleCot" runat="server" />
                                                            </h4>
                                                        </div>
                                                        <div class="modal-body">
                                                            <asp:Panel ID="Panel1" runat="server">
                                                                <div class="form-group">
                                                                    <div class="panel-body">
                                                                        <div class="row gutter">
                                                                            <div class="col-md-12">
                                                                                <asp:TextBox ID="txtObservaciones" runat="server" class="form-control" TextMode="MultiLine"></asp:TextBox>
                                                                            </div>
                                                                            <div class="col-md-12">
                                                                                <asp:UpdatePanel ID="upPDF" runat="server">
                                                                                    <ContentTemplate>
                                                                                        <asp:Literal ID="lPDF" runat="server"></asp:Literal>
                                                                                    </ContentTemplate>
                                                                                    <Triggers>
                                                                                    </Triggers>
                                                                                </asp:UpdatePanel>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </asp:Panel>
                                                        </div>
                                                        <div class="modal-footer">
                                                            <button type="button" style="display: none;" id="btnClosePopupCot" class="btn btn-default" data-dismiss="modal">
                                                                Close</button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- /.modal -->
                                        </div>
                                    </div>
                                    <asp:HiddenField ID="hfIdOD" runat="server" />
                                </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlFFiltros" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="chbxTerminados" EventName="CheckedChanged" />
                            <asp:AsyncPostBackTrigger ControlID="btnFiltrar" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnExportar" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="cbxChoferes" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="cbxClientes" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="cbxProductos" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="cbxUnidad" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="cbxVendedores" EventName="SelectedIndexChanged" />
                            <asp:PostBackTrigger ControlID="btnExportarExcel" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
    </form>
</body>
</html>
