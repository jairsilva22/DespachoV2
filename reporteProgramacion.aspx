<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="reporteProgramacion.aspx.cs" Inherits="despacho.reporteProgramacion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <!-- Top Bar Starts -->
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
    <div class="top-bar clearfix">
        <div class="row gutter">
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <div class="page-title">
                    <h3>Programación</h3>
                    <p><a href="home.aspx">Principal</a></p>
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
                    </li>
                </ul>
            </div>
        </div>
    </div>
    <!-- Top Bar Ends -->

    <!-- Row Starts -->
    <asp:UpdatePanel ID="upForm" runat="server">
        <ContentTemplate>
            <div class="row gutter">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="panel panel-blue">
                        <div class="row gutter">
                            <div class="panel-heading">
                                <h4>Filtrar
                                                    <br />
                                    <asp:CheckBox ID="chbxTerminados" runat="server" AutoPostBack="True" Checked="false" class="radio-inline" OnCheckedChanged="chbxTerminados_CheckedChanged" Text="Terminados" />
                                </h4>
                            </div>
                            <div class="panel-body">
                                <div class="row gutter">
                                    <div class="col-md-2">
                                        <br />
                                        <asp:DropDownList ID="ddlFFiltros" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlFFiltros_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Anteriores</asp:ListItem>
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
                                        <asp:Label ID="lblFiltroSucursal" runat="server" Text="Sucursal:" class="control-label" Visible="True"></asp:Label><br />
                                        <ajaxToolkit:ComboBox ID="cbxSucursales" runat="server" AutoCompleteMode="SuggestAppend" DataTextField="sucursal" DataValueField="idSuc" DropDownStyle="Simple" Visible="True" AutoPostBack="True" OnSelectedIndexChanged="cbxSucursales_SelectedIndexChanged"></ajaxToolkit:ComboBox>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label ID="Label4" runat="server" Text="Cliente:" class="control-label" Visible="True"></asp:Label><br />
                                        <ajaxToolkit:ComboBox ID="cbxClientes" runat="server" AutoCompleteMode="SuggestAppend" DataTextField="cliente" DataValueField="clave" DropDownStyle="Simple" Visible="True" AutoPostBack="True" OnSelectedIndexChanged="cbxClientes_SelectedIndexChanged"></ajaxToolkit:ComboBox>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label ID="Label5" runat="server" Text="Vendedor:" class="control-label" Visible="True"></asp:Label><br />
                                        <ajaxToolkit:ComboBox ID="cbxVendedores" runat="server" AutoCompleteMode="SuggestAppend" DataTextField="vendedor" DataValueField="idV" DropDownStyle="Simple" Visible="True" AutoPostBack="True" OnSelectedIndexChanged="cbxVendedores_SelectedIndexChanged"></ajaxToolkit:ComboBox>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label ID="Label6" runat="server" Text="Producto:" class="control-label" Visible="True"></asp:Label><br />
                                        <ajaxToolkit:ComboBox ID="cbxProductos" runat="server" AutoCompleteMode="SuggestAppend" DataTextField="codigo" DataValueField="codigo" DropDownStyle="Simple" Visible="True" AutoPostBack="True" OnSelectedIndexChanged="cbxProductos_SelectedIndexChanged"></ajaxToolkit:ComboBox>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label ID="Label9" runat="server" Text="Unidad:" class="control-label" Visible="True"></asp:Label><br />
                                        <ajaxToolkit:ComboBox ID="cbxUnidad" runat="server" AutoCompleteMode="SuggestAppend" DataTextField="uNombre" DataValueField="uNombre" DropDownStyle="Simple" Visible="True" AutoPostBack="True" OnSelectedIndexChanged="cbxUnidad_SelectedIndexChanged"></ajaxToolkit:ComboBox>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label ID="Label3" runat="server" Text="Chofer:" class="control-label" Visible="True"></asp:Label><br />
                                        <ajaxToolkit:ComboBox ID="cbxChoferes" runat="server" AutoCompleteMode="SuggestAppend" DataTextField="chofer" DataValueField="idCh" DropDownStyle="Simple" Visible="True" AutoPostBack="True" OnSelectedIndexChanged="cbxChoferes_SelectedIndexChanged"></ajaxToolkit:ComboBox>
                                    </div>
                                </div>
                                <div class="row gutter">
                                    <div class="col-md-1">
                                        <br />
                                        <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" class="btn btn-info" OnClick="btnFiltrar_Click" />
                                    </div>
                                    <div class="col-md-1">
                                        <br />
                                        <%--<asp:Button ID="btnExportar" runat="server" Text="Exportar a PDF" class="btn btn-info" OnClick="btnExportar_Click" Visible="False" />--%>
                                    </div>
                                    <div class="col-md-1">
                                        <br />
                                        <%--<asp:Button ID="btnVerPDF" runat="server" Text="Ver PDF" class="btn btn-info" OnClick="btnVerPDF_Click" Visible="False" />--%>
                                    </div>
                                    <div class="col-md-9">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row gutter">
                            <div class="panel-body">
                                <div class="row gutter">
                                    <div class="col-md-12">
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
                                                            <asp:Label ID="lblFolio" runat="server" class="form-control"><%# "Orden: " + Eval("folioOrden") %></asp:Label>
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
                                                                                <%--<b>Remisión: </b>" + Eval("folio") + "<b> - --%>
                                                                                <asp:Label ID="Label3" runat="server" Text='<%# "Fecha: </b>" + Eval("fecha").ToString().Substring(0,10) + "<b> - Hora: </b>" + Eval("hora")+ "<b> - Cantidad: </b>" + Eval("cantidad") + " " + Eval("unidad").ToString().Trim() + "<b> - Chofer: </b>" + Eval("chofer") + "<b> - Unidad: </b>" + Eval("uTransporte") + "<b> Progreso: </b>" + Eval("porcentaje") + "%" %>'>
                                                                                </asp:Label>
                                                                            </td>
                                                                            <td style="text-align: center; width: 5%;">
                                                                                <%--<asp:LinkButton ID="lbtnBackDosificar" runat="server" CommandName="xBackDosificar" CommandArgument='<%# Eval("id") + "ˇ" + Eval("antIdEstado") + "ˇ" + Eval("idUTransporte") + "ˇ" + Eval("antEstado") + "ˇ" + Eval("colorBell") %>' OnCommand="lbtnBackDosificar_Command"><i class="icon-chevron-left"></i>   </asp:LinkButton>--%>
                                                                            </td>
                                                                            <td style="text-align: center; width: 5%;">
                                                                                <%--&nbsp;&nbsp;<i class="icon-dots-three-horizontal"></i>&nbsp;&nbsp;--%>
                                                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("estadoD") %>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: center; width: 5%;">
                                                                                <%--<asp:LinkButton ID="lbtnDosificar" runat="server" CommandName="xDosificar" CommandArgument='<%# Eval("id") + "ˇ" + Eval("sigIdEstado") + "ˇ" + Eval("idUTransporte") + "ˇ" + Eval("sigEstado") + "ˇ" + Eval("colorBell")  + "ˇ" + Eval("idDS")  + "ˇ" + Eval("cantidad") + "ˇ" + Eval("idProducto") + "ˇ" + Eval("folio") + "ˇ" + Eval("codigo") + "ˇ" + Eval("tp") + "ˇ" + Eval("idEstadoDosificacion") %>' OnCommand="lbtnDosificar_Command"><i class="icon-chevron-right"></i>&nbsp;&nbsp;   </asp:LinkButton>--%>
                                                                            </td>
                                                                            <td style="text-align: center; width: 2%;">
                                                                                <div class="progress-bar" role="progressbar" aria-valuenow="10" aria-valuemin="0" aria-valuemax="10" style="width: 15px; height: 15px; background-color: <%# Eval("colorBell") %>; border-radius: 25px 25px">
                                                                                </div>
                                                                            </td>
                                                                            <td style="text-align: center; width: 7%;">
                                                                                <%--<asp:LinkButton ID="lbtnAsignarU" runat="server" CommandName="xAssignUnit" CommandArgument='<%# Eval("id") + "ˇ" + Eval("uTransporte") + "ˇ" + Eval("tp") + "ˇ" + Eval("idUTransporte") + "ˇ" + Eval("idEstadoDosificacion") %>' OnCommand="lbtnAsignarU_Command"><i class="icon-truck2"><%# Eval("uTransporte") %></i></asp:LinkButton>--%>
                                                                            </td>
                                                                            <td style="text-align: center; width: 7%;">
                                                                                <asp:LinkButton ID="lbtnImprimirRemision" runat="server" CommandName="xPrintRem" CommandArgument='<%# Eval("id") + "ˇ" + Eval("idEstadoDosificacion") + "ˇ" + Eval("revenimiento") + "ˇ" + Eval("idSucursal") + "ˇ" + Eval("folioR") %>' OnCommand="lbtnImprimirRemision_Command"><i class="icon-printer-text"></i>Remisión</asp:LinkButton>
                                                                            </td>
                                                                            <td style="text-align: center; width: 7%;">
                                                                                <asp:LinkButton ID="lbtnBitacora" runat="server" CommandName="xABit" CommandArgument='<%# Eval("id") %>' OnCommand="lbtnBitacora_Command"><i class="icon-file-text2"></i>Bitácora</asp:LinkButton>
                                                                            </td>
                                                                            <td style="text-align: center; width: 7%;">
                                                                                <%--<asp:LinkButton ID="lbtnCalidad" runat="server" CommandName="xCalidad" CommandArgument='<%# Eval("id") %>' OnCommand="lbtnCalidad_Command"><i class="icon-clipboard"></i>Calidad</asp:LinkButton>--%>
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
                                                                <label class="label label-danger">¡No hay Ordenes de Entrega Registrados!</label></td>
                                                        </tr>
                                                    </table>
                                                </EmptyDataTemplate>
                                            </asp:ListView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row gutter">
                            <div class="panel-heading">
                                <h4></h4>
                            </div>
                            <div class="panel-body">
                                <div class="row gutter">
                                    <div class="col-md-12">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- Button trigger modal -->
                        <button type="button" style="display: none;" id="btnShowPopup" class="btn btn-primary btn-lg"
                            data-toggle="modal" data-target="#myModal">
                            Launch demo modal
                        </button>

                        <!-- Modal -->
                        <div class="modal fade" id="myModal">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span></button>
                                        <h4 class="modal-title">
                                            <asp:Label ID="mlblTitle" runat="server" />
                                        </h4>
                                    </div>
                                    <div class="modal-body">
                                        <asp:Label ID="mlblMessage" runat="server" />
                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button ID="mbtnClose" runat="server" Text="Cerrar" class="btn btn-default" OnClick="mbtnClose_Click" />
                                        <button type="button" style="display: none;" id="btnClosePopup" class="btn btn-default" data-dismiss="modal">
                                            Close</button>
                                        <asp:Button ID="mbtnAceptar" runat="server" Text="Aceptar" class="btn btn-info" OnClick="mbtnAceptar_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- /.modal -->

                        <!-- Button trigger modal -->
                        <button type="button" style="display: none;" id="btnShowPopupBit" class="btn btn-primary btn-lg"
                            data-toggle="modal" data-target="#myModalBit">
                            Launch demo modal
                        </button>

                        <!-- Modal -->
                        <div class="modal fade" id="myModalBit">
                            <asp:UpdatePanel ID="upBitacora" runat="server">
                                <ContentTemplate>
                                    <div class="modal-dialog modal-lg">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <asp:LinkButton ID="mlbtnCloseBit" class="close" runat="server" OnClick="mlbtnCloseBit_Click"><span aria-hidden="true">&times;</span></asp:LinkButton>
                                                <h4 class="modal-title">
                                                    <asp:Label ID="mlblTitleBit" runat="server" />
                                                </h4>
                                            </div>
                                            <div class="modal-body">

                                                <div class="row gutter">
                                                    <asp:Panel ID="Panel2" runat="server">
                                                        <div class="form-group">
                                                            <div class="row gutter">
                                                                <div class="col-md-12">
                                                                    <asp:Panel ID="Panel3" runat="server">
                                                                        <asp:Label ID="lblOD" runat="server" Visible="false"></asp:Label>
                                                                        <br />
                                                                        <div class="panel-body">
                                                                            <div class="table-responsive">
                                                                                <asp:Label ID="lblIDRemision" runat="server" Text="" Visible="false"></asp:Label>

                                                                                <div class="row gutter">
                                                                                    <div class="col-md-1">
                                                                                    </div>
                                                                                    <div class="col-md-10">
                                                                                        <label class="control-label">Motivo:</label>
                                                                                        <asp:TextBox ID="txtMotivo" runat="server" class="form-control" TextMode="MultiLine" Rows="5"></asp:TextBox>
                                                                                    </div>
                                                                                    <div class="col-md-1">
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row gutter">
                                                                                    <div class="col-md-1">
                                                                                    </div>
                                                                                    <div class="col-md-10">
                                                                                        <asp:Button ID="mbtnAgregarBit" runat="server" Text="Agregar" class="btn btn-info" OnClick="mbtnAgregarBit_Click" />
                                                                                    </div>
                                                                                    <div class="col-md-1">
                                                                                    </div>
                                                                                </div>
                                                                                <br />
                                                                                <br />
                                                                                <asp:ListView ID="lvBit" runat="server" ItemPlaceholderID="itemPlaceHolderBit">
                                                                                    <LayoutTemplate>
                                                                                        <table id="OrderTable" class="table no-margin">
                                                                                            <thead>
                                                                                                <tr>
                                                                                                    <%--<th style="text-align: center">ID</th>--%>
                                                                                                    <th style="text-align: center">Observaciones</th>
                                                                                                    <th style="text-align: center">Usuario</th>
                                                                                                    <th style="text-align: center">Fecha y hora</th>
                                                                                                    <%--<th style="text-align: center">Registrar</th>--%>
                                                                                                </tr>
                                                                                            </thead>
                                                                                            <tbody>
                                                                                                <asp:PlaceHolder ID="itemPlaceHolderBit" runat="server"></asp:PlaceHolder>
                                                                                            </tbody>
                                                                                        </table>
                                                                                        </div>
                                                                                    </LayoutTemplate>
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <%--<td style="text-align: center">
                                                                                                <asp:Label ID="lblIDUnidad" runat="server" Text='<%# Eval("id") %>'></asp:Label>
                                                                                            </td>--%>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label ID="lblTipoUnidad" runat="server" Text='<%# Eval("motivo") %>'></asp:Label>
                                                                                            </td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("usuario") %>'></asp:Label>
                                                                                            </td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("fecha") %>'></asp:Label>
                                                                                            </td>
                                                                                            <%--<td style="text-align: center">
                                                                                                <asp:LinkButton ID="lBtnAsignar" runat="server" CommandName="xAssignUnit" CommandArgument='<%# Eval("id") %>' OnCommand="lBtnAsignar_Command">Modificar</asp:LinkButton>
                                                                                            </td>--%>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:ListView>
                                                                                <br />
                                                                                <asp:Label ID="lblMensaje" runat="server" Text="" ForeColor="Red" Font-Size="Large" Font-Bold="True"></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                        <asp:HiddenField ID="hfIdBitacora" runat="server" />
                                                                    </asp:Panel>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                </div>
                                                <asp:Label ID="mlblMessageBit" runat="server" />
                                            </div>
                                            <div class="modal-footer">
                                                <button type="button" style="display: none;" id="btnClosePopupBit" class="btn btn-default" data-dismiss="modal">
                                                    Close</button>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="mbtnAgregarBit" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <!-- /.modal -->

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
                                                            <asp:Button ID="mBtnGenerarRemPDF" runat="server" Text="Generar Remisión" class="form-control" OnClick="mBtnGenerarRemPDF_Click" Visible="false" />
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
                                            <%--<asp:Button ID="mbtnCloseRemPDF" runat="server" Text="Cerrar" class="btn btn-default" OnClick="mbtnCloseRemPDF_Click" />--%>
                                            <button type="button" style="display: none;" id="btnClosePopupRemPDF" class="btn btn-default" data-dismiss="modal">
                                                Close</button>
                                            <asp:Button ID="mbtnAceptarRemPDF" runat="server" Text="Aceptar" class="btn btn-info" OnClick="mbtnAceptarRemPDF_Click" Visible="false" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- /.modal -->

                        <!-- Button trigger modal -->
                        <button type="button" style="display: none;" id="btnShowPopupDosi" class="btn btn-primary btn-lg"
                            data-toggle="modal" data-target="#myModalDosi">
                            Launch demo modal
                        </button>

                        <!-- Modal -->
                        <div class="modal fade" id="myModalDosi">
                            <div class="modal-dialog modal-lg">
                                <asp:UpdatePanel ID="upModalDosi" runat="server">
                                    <ContentTemplate>
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <asp:LinkButton ID="mlbtnCloseDosi" class="close" runat="server" OnClick="mlbtnCloseDosi_Click"><span aria-hidden="true">&times;</span></asp:LinkButton>
                                                <h4 class="modal-title">
                                                    <asp:Label ID="mlblTittleDosi" runat="server" />
                                                </h4>
                                            </div>
                                            <div class="modal-body">
                                                <asp:Label ID="mlblDosiIdOD" runat="server" Visible="false" />
                                                <asp:Label ID="mlblDosiIdFormula" runat="server" Visible="false" />
                                                <asp:Label ID="mlblDosiCantidad" runat="server" Visible="false" />
                                                <asp:Label ID="mlblDosiEdpDosi" runat="server" Visible="false" />
                                                <asp:Label ID="mlblDosiBell" runat="server" Visible="false" />
                                                <asp:Label ID="mlblTP" runat="server" Visible="false" />
                                                <asp:Panel ID="mpnlDosi1" runat="server">
                                                    <div class="form-group">
                                                        <div class="panel-body">
                                                            <div class="row gutter">
                                                                <div class="col-md-4">
                                                                    <asp:Label ID="Label1" runat="server" Text="Cantidad de mezcla en camión: " class="control-label"></asp:Label>
                                                                    <asp:TextBox ID="txtMezclaEnCamion" runat="server" class="form-control" onkeypress="return onlyDotsAndNumbers(event)">0</asp:TextBox>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <asp:Label ID="Label2" runat="server" Text="Cantidad de Agua en camión: " class="control-label"></asp:Label>
                                                                    <asp:TextBox ID="txtAguaEnCamion" runat="server" class="form-control" onkeypress="return onlyDotsAndNumbers(event)">0</asp:TextBox>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <br />
                                                                    <asp:Button ID="btnCalcularMaterial" runat="server" OnClick="btnCalcularMaterial_Click" Text="Calcular total de material" class="btn btn-info" />
                                                                </div>
                                                            </div>
                                                            <div class="row gutter">
                                                                <div class="col-md-12">
                                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                                        <div class="panel panel-blue">
                                                                            <div class="panel-heading">
                                                                                <h4>Formulación Base</h4>
                                                                            </div>
                                                                            <div class="panel-body">
                                                                                <div class="table-responsive">
                                                                                    <asp:GridView ID="gvFormulaBase" runat="server" AutoGenerateColumns="False" class="table table-bordered table-hover table-condensed no-margin">
                                                                                        <Columns>
                                                                                            <asp:BoundField DataField="Material" HeaderText="Material" />
                                                                                            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                                                                                            <asp:BoundField DataField="Unidad" HeaderText="Unidad" />
                                                                                            <asp:BoundField DataField="Adicional" HeaderText="Adicional" />
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row gutter">
                                                                <div class="col-md-12">
                                                                </div>
                                                            </div>
                                                            <div class="row gutter">
                                                                <div class="col-md-12">
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                                <asp:Panel ID="mpnlDosi2" runat="server" Visible="false">
                                                    <div class="form-group">
                                                        <div class="panel-body">
                                                            <div class="row gutter">
                                                                <div class="col-md-12">
                                                                    <div class="row gutter">
                                                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                                            <div class="panel panel-blue">
                                                                                <div class="panel-heading">
                                                                                    <h4>Formulación calculada por m3 de Dosificación</h4>
                                                                                </div>
                                                                                <div class="panel-body">
                                                                                    <div class="row gutter">
                                                                                        <div class="col-md-12">
                                                                                            <div class="table-responsive">
                                                                                                <asp:GridView ID="gvTotalMaterial" runat="server" class="table table-bordered table-hover table-condensed no-margin">
                                                                                                </asp:GridView>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row gutter">
                                                                                        <div class="col-md-12">
                                                                                            <div class="table-responsive">
                                                                                                <asp:GridView ID="gvAdicionales" runat="server" class="table table-bordered table-hover table-condensed no-margin">
                                                                                                </asp:GridView>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row gutter">
                                                                <div class="col-md-12">
                                                                    <asp:Button ID="mbtnDosiDosificar" runat="server" OnClick="mbtnDosiDosificar_Click" Text="Dosificar" class="btn btn-info" />
                                                                </div>
                                                            </div>
                                                            <div class="row gutter">
                                                                <div class="col-md-12">
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                                <asp:Panel ID="mpnlDosi3" runat="server" Visible="false">
                                                    <div class="row gutter">
                                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                            <div class="panel panel-blue">
                                                                <div class="panel-heading">
                                                                    <h4>Bitácora de Dosificación</h4>
                                                                </div>
                                                                <div class="panel-body">
                                                                    <div class="table-responsive">
                                                                        <asp:ListView ID="lvBitacora" runat="server">
                                                                            <LayoutTemplate>
                                                                                <table id="basicExample" class="table table-striped table-condensed table-bordered no-margin">
                                                                                    <thead>
                                                                                        <tr>
                                                                                            <th id="Material">Material</th>
                                                                                            <th style="text-align: center" id="Cantidad">Cantidad</th>
                                                                                            <th style="text-align: center" id="Unidad">Unidad</th>
                                                                                            <th style="text-align: center" id="PorcentajeDeDosificación">Porcentaje de Dosificación</th>
                                                                                            <th style="text-align: center" id="ProgresoDeDosificacion">Progreso de Dosificación</th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tr id="itemPlaceholder" runat="server"></tr>
                                                                                    <tfoot>
                                                                                        <tr>
                                                                                            <th>Material</th>
                                                                                            <th style="text-align: center">Cantidad</th>
                                                                                            <th style="text-align: center">Unidad</th>
                                                                                            <th style="text-align: center">Porcentaje de Dosificación</th>
                                                                                            <th style="text-align: center">Progreso de Dosificación</th>
                                                                                        </tr>
                                                                                    </tfoot>
                                                                                </table>
                                                                            </LayoutTemplate>
                                                                            <ItemTemplate>
                                                                                <tr>
                                                                                    <td><%# Eval("material") %></td>
                                                                                    <td style="text-align: center"><%# Eval("cantidad")%></td>
                                                                                    <td style="text-align: center"><%# Eval("unidad") %></td>
                                                                                    <td style="text-align: center"><%# Eval("porcentaje") %></td>
                                                                                    <td style="text-align: center"><%# Eval("progreso") %></td>

                                                                                </tr>
                                                                            </ItemTemplate>
                                                                            <EmptyDataTemplate>
                                                                                <table id="basicExample" class="table table-striped table-condensed table-bordered no-margin">
                                                                                    <thead>
                                                                                        <tr>
                                                                                            <th class="span2 hidden-phone" id="Material">Material</th>
                                                                                            <th class="span2 hidden-phone" id="Cantidad">Cantidad</th>
                                                                                            <th class="span2  hidden-phone" id="Unidad">Unidad</th>
                                                                                            <th class="span2  hidden-phone" id="PorcentajeDeDosificacion">Porcentaje de Dosificación</th>
                                                                                            <th class="span2  hidden-phone" id="ProgresoDeDosificación">Progreso de Dosificación</th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tr>
                                                                                        <td colspan="5">
                                                                                            <label class="label label-danger">¡No hay Registros en la Bitacora!</label></td>
                                                                                    </tr>
                                                                                </table>
                                                                            </EmptyDataTemplate>
                                                                        </asp:ListView>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                                <div class="modal-footer">
                                                    <asp:Button ID="mlbtnCancelarDosi2" runat="server" Text="Cancelar" class="btn btn-default" OnClick="mlbtnCancelarDosi2_Click" />
                                                    <button type="button" style="display: none;" id="btnClosePopupDosi" class="btn btn-default" data-dismiss="modal">
                                                        Close</button>
                                                    <asp:Button ID="mlbtnSiguienteDosi" runat="server" Text="Terminar Dosificación" class="btn btn-info" OnClick="mlbtnSiguienteDosi_Click" Visible="false" />
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnCalcularMaterial" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="mbtnDosiDosificar" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <!-- /.modal -->

                        <!-- Button trigger modal -->
                        <button type="button" style="display: none;" id="btnShowPopupAU" class="btn btn-primary btn-lg"
                            data-toggle="modal" data-target="#myModalAU">
                            Launch demo modal
                        </button>

                        <!-- Modal -->
                        <div class="modal fade" id="myModalAU">
                            <div class="modal-dialog modal-lg">
                                <asp:UpdatePanel ID="upAU" runat="server">
                                    <ContentTemplate>
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <asp:LinkButton ID="mlbtnCloseMyModalAU" class="close" runat="server" OnClick="mlbtnCloseMyModalAU_Click"><span aria-hidden="true">&times;</span></asp:LinkButton>
                                                <h4 class="modal-title">
                                                    <asp:Label ID="Label10" runat="server" />
                                                </h4>
                                            </div>
                                            <div class="modal-body">
                                                <asp:Panel ID="mpnlAUAsignarUnidad" runat="server">
                                                    <div class="form-group">
                                                        <div class="panel-body">
                                                            <div class="row gutter">
                                                                <div class="col-md-12">
                                                                    <asp:HiddenField ID="mhfIdODAU" runat="server" />
                                                                    <asp:HiddenField ID="mhfIdUTAU" runat="server" />
                                                                    <asp:HiddenField ID="mhfIdEstadoDAU" runat="server" />
                                                                    <asp:HiddenField ID="mhfNombreUTAU" runat="server" />
                                                                    <asp:HiddenField ID="mhfTPAU" runat="server" />
                                                                    <asp:ListView ID="lvMdlAU" runat="server" ItemPlaceholderID="itemPlaceHolderTU">
                                                                        <LayoutTemplate>
                                                                            <table id="OrderTable" class="table no-margin" style="width: 100%">
                                                                                <thead>
                                                                                    <tr style="width: 100%">
                                                                                        <th style="text-align: center">ID</th>
                                                                                        <th style="text-align: center">Unidad</th>
                                                                                        <th style="text-align: center">Capacidad</th>
                                                                                        <th style="text-align: center">Tipo de Unidad</th>
                                                                                        <th style="text-align: center">Asignar</th>
                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody>
                                                                                    <asp:PlaceHolder ID="itemPlaceHolderTU" runat="server"></asp:PlaceHolder>
                                                                                </tbody>
                                                                            </table>
                                                                            </div>
                                                                        </LayoutTemplate>
                                                                        <ItemTemplate>
                                                                            <tr style="width: 100%">
                                                                                <td style="text-align: center">
                                                                                    <asp:Label ID="lblIDUnidad" runat="server" Text='<%# Eval("idUT") %>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: center"><i class="icon-truck2"></i>
                                                                                    <asp:LinkButton ID="lbtnUnidadT" runat="server" Text='<%# Eval("nombre") %>'></asp:LinkButton>
                                                                                </td>
                                                                                <td style="text-align: center">
                                                                                    <asp:Label ID="lblCapacidad" runat="server" Text='<%# Eval("capacidadUT") %>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: center">
                                                                                    <asp:Label ID="lblTipoUnidad" runat="server" Text='<%# Eval("tipo") %>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: center">
                                                                                    <asp:LinkButton ID="lbtnAsignarAU" runat="server" CommandName="xAssignUnit" CommandArgument='<%# Eval("idUT") %>' OnCommand="lbtnAsignarAU_Command">Asignar</asp:LinkButton>
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                        <EmptyDataTemplate>
                                                                            <table id="responsiveTable0" class="table table-striped table-bordered no-margin">
                                                                                <thead>
                                                                                    <tr style="width: 100%">
                                                                                        <th style="text-align: center">ID</th>
                                                                                        <th style="text-align: center">Unidad</th>
                                                                                        <th style="text-align: center">Capacidad</th>
                                                                                        <th style="text-align: center">Tipo de Unidad</th>
                                                                                        <th style="text-align: center">Asignar</th>
                                                                                    </tr>
                                                                                </thead>
                                                                                <tr>
                                                                                    <td colspan="5">
                                                                                        <label class="label label-danger">
                                                                                            ¡No hay Unidades disponibles!</label></td>
                                                                                </tr>
                                                                            </table>
                                                                        </EmptyDataTemplate>
                                                                    </asp:ListView>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                                <asp:Panel ID="mpnlAUMapa" runat="server" Visible="false">
                                                    <div class="form-group">
                                                        <div class="panel-body">
                                                            <div class="row gutter">
                                                                <div class="col-md-12">
                                                                    <div class="row gutter">
                                                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                                            <div class="panel panel-blue">
                                                                                <div class="panel-heading">
                                                                                    <h4>Formulación calculada por m3 de Dosificación</h4>
                                                                                </div>
                                                                                <div class="panel-body">
                                                                                    <div class="table-responsive">
                                                                                        <asp:GridView ID="GridView2" runat="server" class="table table-bordered table-hover table-condensed no-margin">
                                                                                        </asp:GridView>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row gutter">
                                                                <div class="col-md-12">
                                                                    <asp:Button ID="Button2" runat="server" OnClick="mbtnDosiDosificar_Click" Text="Dosificar" class="btn btn-info" />
                                                                </div>
                                                            </div>
                                                            <div class="row gutter">
                                                                <div class="col-md-12">
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                                <asp:Panel ID="mpnlAUDesasignar" runat="server" Visible="false">
                                                    <div class="form-group">
                                                        <div class="panel-body">
                                                            <div class="row gutter">
                                                                <div class="col-md-12">
                                                                    <div class="row gutter">
                                                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                                            <div class="panel panel-blue">
                                                                                <div class="panel-heading">
                                                                                    <h4>¿Deseeas desasignar la unidad <asp:Label ID="mlblAUNombreUnidad" runat="server" Text=""></asp:Label> de esta entrega?</h4>
                                                                                </div>
                                                                                <div class="panel-body" >
                                                                                    <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d34280.354346421824!2d-100.99938788807908!3d25.428954849904372!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x0%3A0x8bd059467b9764f8!2sDistribuidora%20Y%20Procesos%20Pepi%20Sa%20De%20Cv!5e0!3m2!1ses!2smx!4v1611609826813!5m2!1ses!2smx" width="600" height="450" frameborder="0" style="border:0;" allowfullscreen="" aria-hidden="false" tabindex="0"></iframe>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                                <div class="modal-footer">
                                                    <asp:Button ID="mlbtnCancelar" runat="server" Text="Cancelar" class="btn btn-default" OnClick="mlbtnCancelar_Click" Visible="false" />
                                                    <button type="button" style="display: none;" id="btnClosePopupAU" class="btn btn-default" data-dismiss="modal">
                                                        Close</button>
                                                    <asp:Button ID="mlbtnAceptarAU" runat="server" Text="Aceptar" class="btn btn-info" OnClick="mlbtnAceptarAU_Click" Visible="false" />
                                                    <asp:Button ID="mlbtnAUDCancelar" runat="server" Text="Cancelar" class="btn btn-default" OnClick="mlbtnAUDCancelar_Click" Visible="false" />
                                                    <asp:Button ID="mlbtnAUDAceptar" runat="server" Text="Aceptar" class="btn btn-info" OnClick="mlbtnAUDAceptar_Click" Visible="false" />
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <!-- /.modal -->
                        <asp:HiddenField ID="hfId" runat="server" Value="0" />
                    </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnFiltrar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="cbxChoferes" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="cbxSucursales" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="cbxClientes" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="cbxProductos" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="cbxUnidad" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="cbxVendedores" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="chbxTerminados" EventName="CheckedChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlFFiltros" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
    <!-- Row Ends -->
</asp:Content>
