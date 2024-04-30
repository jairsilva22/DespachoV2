<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="programacionTest.aspx.cs" Inherits="despacho.programacionTest" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts" runat="server">
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
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <%--</div>
    </div>--%>

    <!-- Top Bar Starts -->
    <%--<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>--%>

    <div class="top-bar clearfix">
        <div class="row gutter">
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <div class="page-title">
                    <h3>Programación</h3>
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
								</li>--%>
                    <li>
                        <a href="rProgra.aspx" class="btn btn-info">
                            <i class="icon-file-text2"></i>Reporte
                        </a> 
                        <a href="home.aspx" class="btn btn-info">
                            <i class="icon-bulb"></i>Ayuda
                        </a>
                        <%--<a href="javascript:void(0)" class="btn btn-info"><i class="icon-download6"></i> Export</a>--%>
                    </li>
                </ul>
            </div>
        </div>

        <asp:UpdatePanel ID="upFiltros" runat="server">
            <ContentTemplate>
                <table class="nav-justified">
                    <tr>
                        <td colspan="3" style="justify-content: center; font-size: large;">Filtros:
                            <asp:CheckBox ID="chbxTerminados" runat="server" AutoPostBack="True" Checked="false" class="radio-inline" OnCheckedChanged="chbxProgramadas_CheckedChanged" Text="Terminados" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="row gutter">
                                <div class="col-md-3 selectContainer">
                                    &nbsp;
                                </div>
                            </div>
                        </td>
                        <td>
                            <div class="row gutter">
                                <div class="col-md-3 selectContainer">
                                    Cliente:
                                </div>
                            </div>
                        </td>
                        <td>
                            <div class="row gutter">
                                <div class="col-md-3 selectContainer">
                                    Orden:
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="ddlFFiltros" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlFFiltros_SelectedIndexChanged">
                                <asp:ListItem Value="0">Anteriores</asp:ListItem>
                                <asp:ListItem Selected="True" Value="1">Hoy</asp:ListItem>
                                <asp:ListItem Value="2">Mañana</asp:ListItem>
                                <asp:ListItem Value="3" Enabled="False">Programados</asp:ListItem>
                                <asp:ListItem Value="4" Enabled="False">Sin Programar</asp:ListItem>
                                <asp:ListItem Value="5">Entre Fechas</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlFClientes" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlFClientes_SelectedIndexChanged"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlFOrdenes" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlFOrdenes_SelectedIndexChanged"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="row gutter">
                                <div class="col-md-3 selectContainer">
                                    <asp:Label ID="lblFechaI" runat="server" Text="Fecha Inicio:" class="control-label" Visible="False"></asp:Label>
                                </div>
                            </div>
                        </td>
                        <td>
                            <div class="row gutter">
                                <div class="col-md-3 selectContainer">
                                    <asp:Label ID="lblFechaF" runat="server" Text="Fecha Fin" class="control-label" Visible="False"></asp:Label>
                                </div>
                            </div>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtFechaI" runat="server" class="form-control" Visible="False"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="txtFechaI_CalendarExtender" runat="server" BehaviorID="txtFechaI_CalendarExtender" TargetControlID="txtFechaI" FirstDayOfWeek="Monday" Format="yyyy-MM-dd" PopupPosition="BottomRight" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtFechaF" runat="server" class="form-control" Visible="False"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="txtFechaF_CalendarExtender" runat="server" BehaviorID="txtFechaF_CalendarExtender" TargetControlID="txtFechaF" FirstDayOfWeek="Monday" Format="yyyy-MM-dd" PopupPosition="BottomRight" />
                        </td>
                        <td>
                            <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" class="btn btn-info" OnClick="btnFiltrar_Click" Visible="False" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlFFiltros" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="ddlFClientes" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="ddlFOrdenes" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <!-- Top Bar Ends -->

    <!-- Row Starts -->
    <asp:UpdatePanel ID="upGrid" runat="server">
        <ContentTemplate>
            <div id="ContentPlaceHolder_upGrid">
                <div class="row gutter">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="panel panel-blue">
                            <div class="panel-heading">
                                <h4></h4>
                            </div>
                            <div class="panel-body">
                                <div class="table-responsive">
                                    <asp:ListView ID="lvO" runat="server" ItemPlaceholderID="itemPlaceHolderO" OnItemDataBound="lvO_ItemDataBound" OnItemCommand="lvO_ItemCommand">
                                        <LayoutTemplate>
                                            <table class="table no-margin" id="OrderTable">
                                                <thead>
                                                    <tr>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:PlaceHolder runat="server" ID="itemPlaceHolderO"></asp:PlaceHolder>
                                                </tbody>
                                            </table>
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <%-- table para mostrar Datos de la Orden --%>
                                            <tr class="bg-primary">
                                                <td style="text-align: center;">
                                                    <asp:Label ID="lblFolio" runat="server" class="form-control"><%# "Orden: " + Eval("folio") %></asp:Label>
                                                    <%--<i class="icon-bell"></i>--%>
                                                    <asp:Label ID="lblIDOrden" runat="server" Visible="false" Text='<%# Eval("idOrden") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# "<br/>" + "Cliente: " + Eval("clave") + " - " + Eval("cliente") + "<br/>"%>'></asp:Label>
                                                    <%--<asp:Label ID="Label6" runat="server" Text='<%# "Fecha de Entrega: " + Eval("fecha").ToString().Substring(0,10) + "<br/>" + "Cliente: " + Eval("clave") + " - " + Eval("cliente") + "<br/>"%>'></asp:Label>--%>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label7" runat="server" Text='<%# "Producto: " + Eval("descripcion") + "<br/>" + "Entrega: " + Eval("calle") + " " + Eval("numero")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label8" runat="server" Text='<%# "Cantidad Entregada: " + (Eval("cantidadEntregada") == null ? "0" : Eval("cantidadEntregada")) + " de " + 
                                                            Eval("cTotal") + "<br/>" + "Colonia: " + Eval("colonia") + " CP: " + Eval("cp")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <%--<asp:LinkButton ID="lbtnAjuste" runat="server" CommandName="xAjuste" CommandArgument='<%# Eval("idOrden") + "ˇ" + Eval("idTUT") + "ˇ" + Eval("fecha") + "ˇ" + Eval("hora") + "ˇ" + Eval("codigo") + "ˇ" + Eval("descripcion") + "ˇ" + Eval("cTotal") + "ˇ" + Eval("unidad") + "ˇ" + Eval("revenimiento") + "ˇ" + Eval("cantidadEntregada") + "ˇ" + Eval("idP") + "ˇ" + Eval("idS") + "ˇ" + Eval("idDS") %>' OnCommand="lbtnAjuste_Command" ForeColor="White" Visible="True"><i class="icon-cog6"></i>Ajuste</asp:LinkButton>--%>
                                                </td>
                                            </tr>
                                            <%-- listview para mostrar datos de las ordenes de dosificación --%>
                                            <asp:ListView ID="lvOD" runat="server" ItemPlaceholderID="itemPlaceHolderOD">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td colspan="5">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td style="width: 55%;">
                                                                        <asp:Label ID="Label3" runat="server" Text='<%# "<b>Fecha: </b>" + Eval("fecha").ToString().Substring(0,10) + "<b> - Hora: </b>" + Eval("hora")+ "<b> - Cantidad: </b>" + Eval("cantidad") + " " + Eval("unidad").ToString().Trim() + "<b> - Chofer: </b>" + Eval("chofer") + "<b> Progreso: </b>" + Eval("porcentaje") + "%" %>'>
                                                                        </asp:Label>
                                                                    </td>
                                                                    <td style="text-align: center; width: 5%;">
                                                                        <%--<asp:LinkButton ID="lbtnBackDosificar" runat="server" CommandName="xBackDosificar" CommandArgument='<%# Eval("id") + "ˇ" + Eval("antIdEstado") + "ˇ" + Eval("idUTransporte") + "ˇ" + Eval("antEstado") + "ˇ" + Eval("colorBell") %>' OnCommand="lbtnBackDosificar_Command"><i class="icon-chevron-left"></i>   </asp:LinkButton>--%>
                                                                    </td>
                                                                    <td style="text-align: center; width: 5%;">
                                                                        <%--&nbsp;&nbsp;<i class="icon-dots-three-horizontal"></i>&nbsp;&nbsp;--%>
                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("estado") %>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: center; width: 5%;">
                                                                        <%--<asp:LinkButton ID="lbtnDosificar" runat="server" CommandName="xDosificar" CommandArgument='<%# Eval("id") + "ˇ" + Eval("sigIdEstado") + "ˇ" + Eval("idUTransporte") + "ˇ" + Eval("sigEstado") + "ˇ" + Eval("colorBell")  + "ˇ" + Eval("idDS")  + "ˇ" + Eval("cantidad") %>' OnCommand="lbtnDosificar_Command"><i class="icon-chevron-right"></i>   </asp:LinkButton>--%>
                                                                    </td>
                                                                    <td style="text-align: center; width: 2%;">
                                                                        <div class="progress-bar" role="progressbar" aria-valuenow="10" aria-valuemin="0" aria-valuemax="10" style="width: 15px; height: 15px; background-color: <%# Eval("colorBell") %>; border-radius: 25px 25px">
                                                                        </div>
                                                                    </td>
                                                                    <td style="text-align: center; width: 7%;">
                                                                        <%--<asp:LinkButton ID="lbtnAsignarU" runat="server" CommandName="xAssignUnit" CommandArgument='<%# Eval("id") + "ˇ" + Eval("uTransporte") %>' OnCommand="lbtnAsignarU_Command"><i class="icon-truck2"><%# Eval("uTransporte") %></i></asp:LinkButton>--%>
                                                                    </td>
                                                                    <td style="text-align: center; width: 7%;">
                                                                        <asp:LinkButton ID="lbtnImprimirRemision" runat="server" CommandName="xPrintRem" CommandArgument='<%# Eval("id") %>' OnCommand="lbtnImprimirRemision_Command"><i class="icon-printer-text"></i>Imprimir</asp:LinkButton>
                                                                    </td>
                                                                    <td style="text-align: center; width: 7%;">
                                                                        <asp:LinkButton ID="lbtnBitacora" runat="server" CommandName="xABit" CommandArgument='<%# Eval("id") %>' OnCommand="lbtnBitacora_Command"><i class="icon-file-text2"></i>Bitácora</asp:LinkButton>
                                                                    </td>
                                                                    <td style="text-align: center; width: 7%;">
                                                                        <asp:LinkButton ID="lbtnCalidad" runat="server" CommandName="xCalidad" CommandArgument='<%# Eval("id") %>' OnCommand="lbtnCalidad_Command"><i class="icon-clipboard"></i>Calidad</asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="10">
                                                                        <div style="background-color: lightgrey; height: 10px; width: 100%">
                                                                            <div class="progress-bar" role="progressbar" aria-valuenow='<%# Eval("porcentaje") %>' aria-valuemin="0" aria-valuemax="100" style="width: <%# Eval("porcentaje") %>%; background-color: <%# Eval("color") %>;">
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <%--<EmptyDataTemplate>
                                                    <table id="responsiveTable0" class="table table-striped table-bordered no-margin">
                                                        <thead>
                                                            <tr>
                                                                <th style="text-align: center"></th>
                                                            </tr>
                                                        </thead>
                                                        <tr>
                                                            <td colspan="9">
                                                                <label class="label label-danger">
                                                                    ¡No hay Remisiones programadas!</label></td>
                                                        </tr>
                                                    </table>
                                                </EmptyDataTemplate>--%>
                                            </asp:ListView>
                                        </ItemTemplate>
                                        <EmptyDataTemplate>
                                            <table id="responsiveTable0" class="table table-striped table-bordered no-margin">
                                                <thead>
                                                    <tr>
                                                        <th style="text-align: center"></th>
                                                    </tr>
                                                </thead>
                                                <tr>
                                                    <td colspan="3">
                                                        <label class="label label-danger">
                                                            ¡No hay Ordenes Registrados!</label></td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                    </asp:ListView>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:HiddenField ID="hfIdOD" runat="server" />
                </div>
                <!-- Button trigger modal -->
                <button type="button" style="display: none;" id="btnShowPopupRemPDF" class="btn btn-primary btn-lg"
                    data-toggle="modal" data-target="#myModalRemPDF">
                    Launch demo modal
                </button>

                <!-- Modal -->
                <div class="modal fade" id="myModalRemPDF">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:LinkButton ID="mBtnTittleCloseRemPDF" class="close" runat="server" OnClick="mBtnTittleCloseRemPDF_Click"><span aria-hidden="true">&times;</span></asp:LinkButton>
                                <h4 class="modal-title">
                                    <asp:Label ID="mbllTittleRemPDF" runat="server" />
                                </h4>
                            </div>
                            <div class="modal-body">
                                <asp:Panel ID="PanelRemPDF" runat="server">
                                    <div class="form-group">
                                        <div class="panel-body">
                                            <div class="row gutter">
                                                <div class="col-md-12">
                                                </div>
                                                <div class="col-md-12">
                                                    <asp:Label ID="mlblIdODRem" runat="server" Text="" Visible="false"></asp:Label>
                                                    <asp:Button ID="mBtnGenerarRemPDF" runat="server" Text="Generar Remisión" class="form-control" OnClick="mBtnGenerarRemPDF_Click" />
                                                    <asp:Button ID="mBtnEnviarCot" runat="server" Text="Enviar por Correo" class="form-control" Visible="False" />
                                                    <asp:UpdatePanel ID="upPDF" runat="server">
                                                        <ContentTemplate>
                                                            <asp:Literal ID="mlPDF" runat="server"></asp:Literal>
                                                            <asp:Label ID="mlblMensajeRemPDF" runat="server" Text="" ForeColor="Red" Font-Size="Large" Font-Bold="True"></asp:Label>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="mBtnGenerarRemPDF" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                    <asp:HiddenField ID="mhfDocumento" runat="server" />
                                                    <asp:HiddenField ID="mhfRuta" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div class="modal-footer">
                                    <asp:Button ID="mbtnCloseRemPDF" runat="server" Text="Cerrar" class="btn btn-default" OnClick="mbtnCloseRemPDF_Click" />
                                    <button type="button" style="display: none;" id="btnClosePopupRemPDF" class="btn btn-default" data-dismiss="modal">
                                        Close</button>
                                    <asp:Button ID="mbtnAceptarRemPDF" runat="server" Text="Aceptar" class="btn btn-info" OnClick="mbtnAceptarRemPDF_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- /.modal -->

                    <!-- Button trigger modal -->
                    <button type="button" style="display: none;" id="btnShowPopupAsigU" class="btn btn-primary btn-lg"
                        data-toggle="modal" data-target="#myModalAsigU">
                        Launch demo modal
                    </button>

                    <!-- Modal -->
                    <div class="modal fade" id="myModalAsigU">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <asp:LinkButton ID="mBtnTittleCloseAsigU" class="close" runat="server" OnClick="mBtnTittleCloseAsigU_Click"><span aria-hidden="true">&times;</span></asp:LinkButton>
                                    <h4 class="modal-title">
                                        <asp:Label ID="mlblTittleAsigU" runat="server" />
                                    </h4>
                                </div>
                                <div class="modal-body">
                                    <asp:Panel ID="Panel1" runat="server">
                                        <div class="form-group">
                                            <div class="panel-body">
                                                <div class="row gutter">
                                                    <div class="col-md-12">
                                                    </div>
                                                    <div class="col-md-12">
                                                        <asp:Label ID="mlblIdODAsigU" runat="server" Text="" Visible="false"></asp:Label>
                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <ContentTemplate>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                        <asp:HiddenField ID="HiddenField2" runat="server" />
                                                        <asp:HiddenField ID="HiddenField3" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <div class="modal-footer">
                                        <asp:Button ID="mbtnCloseAsigU" runat="server" Text="Cerrar" class="btn btn-default" OnClick="mbtnCloseAsigU_Click" />
                                        <button type="button" style="display: none;" id="btnClosePopupAsigU" class="btn btn-default" data-dismiss="modal">
                                            Close</button>
                                        <asp:Button ID="mbtnAceptarAsigU" runat="server" Text="Aceptar" class="btn btn-info" OnClick="mbtnAceptarAsigU_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- /.modal -->

                        <!-- Button trigger modal -->
                        <button type="button" style="display: none;" id="btnShowPopup" class="btn btn-primary btn-lg"
                            data-toggle="modal" data-target="#myModal">
                            Launch demo modal
                        </button>

                        <!-- Modal -->
                        <div class="modal fade" id="myModal">
                            <div class="modal-dialog modal-lg">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <asp:LinkButton ID="mlbtnCloseMdl" class="close" runat="server" OnClick="mlbtnCloseMdl_Click"><span aria-hidden="true">&times;</span></asp:LinkButton>
                                        <h4 class="modal-title">
                                            <asp:Label ID="mlblTitle" runat="server" />
                                        </h4>
                                    </div>
                                    <div class="modal-body">
                                        <asp:Panel ID="Panel2" runat="server">
                                            <div class="form-group">
                                                <div class="panel-body">
                                                    <div class="row gutter">
                                                        <div class="col-md-12">
                                                            <asp:Label ID="mlblMessage" runat="server" />
                                                        </div>
                                                        <div class="col-md-12">
                                                            <asp:UpdatePanel ID="upMdl" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:Label ID="lblMdlError" runat="server" Text="" ForeColor="Red" Font-Size="Large" Font-Bold="True"></asp:Label>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                            <asp:HiddenField ID="HiddenField1" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button ID="mbtnCloseMdl" runat="server" Text="Cerrar" class="btn btn-default" OnClick="mbtnCloseMdl_Click" />
                                        <button type="button" style="display: none;" id="btnClosePopup" class="btn btn-default" data-dismiss="modal">
                                            Close</button>
                                        <asp:Button ID="mbtAceptarMdl" runat="server" Text="Aceptar" class="btn btn-info" OnClick="mbtAceptarMdl_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- /.modal -->

                    </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlFFiltros" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlFClientes" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlFOrdenes" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="chbxTerminados" EventName="CheckedChanged" />
        </Triggers>
    </asp:UpdatePanel>
    <!-- Row Ends -->

    <%--</div>
    </div>--%>
</asp:Content>
