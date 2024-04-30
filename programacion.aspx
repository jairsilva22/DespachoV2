<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="programacion.aspx.cs" Inherits="despacho.programacion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <!-- Top Bar Starts -->
    <div class="loader-wrapper" id="loader-page">
        <span class="loader"></span>
    </div>
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
            <asp:Timer ID="tmrCheckChanges" runat="server" Interval="10000" OnTick="tmrCheckChanges_Tick"></asp:Timer>
            <div class="row gutter">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="panel panel-blue">
                        <div class="row gutter">
                            <div class="panel-heading">
                                <h4>Filtrar
                                    <br />
                                    <asp:CheckBox ID="chbxTerminados" runat="server" AutoPostBack="True" Checked="false" class="radio-inline" OnCheckedChanged="chbxTerminados_CheckedChanged" Text="Terminados y Cancelados" />
                                </h4>

                                <div class="row gutter">
                                    <div class="col-md-12">
                                        <asp:Label ID="lblC" runat="server" class="control-label"></asp:Label><br />
                                        <asp:Label ID="lblBBA" runat="server" class="control-label"></asp:Label><br />
                                        <asp:Label ID="lblTD" runat="server" class="control-label"></asp:Label><br />
                                        <asp:Label ID="lblOtro" runat="server" class="control-label"></asp:Label><br />
                                        <asp:Label ID="lblBlock" runat="server" class="control-label"></asp:Label>
                                    </div>
                                </div>
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
                                        <asp:Label ID="lblFechaI" runat="server" Text="Fecha Inicio:" class="control-label" Visible="False"></asp:Label><asp:TextBox ID="txtFechaI" runat="server" class="form-control" Visible="False" AutoComplete="off"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="txtFechaI_CalendarExtender" runat="server" BehaviorID="txtFechaI_CalendarExtender" TargetControlID="txtFechaI" FirstDayOfWeek="Monday" Format="yyyy-MM-dd" PopupPosition="BottomRight" />
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label ID="lblFechaF" runat="server" Text="Fecha Fin" class="control-label" Visible="False"></asp:Label><asp:TextBox ID="txtFechaF" runat="server" class="form-control" Visible="False" AutoComplete="off"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="txtFechaF_CalendarExtender" runat="server" BehaviorID="txtFechaF_CalendarExtender" TargetControlID="txtFechaF" FirstDayOfWeek="Monday" Format="yyyy-MM-dd" PopupPosition="BottomRight" />
                                    </div>
                                    <div class="col-md-6">
                                    </div>
                                </div>
                                <div class="row gutter">
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
                                    <div class="col-md-2">
                                    </div>
                                </div>
                                <div class="row gutter">
                                    <div class="col-md-1">
                                        <br />
                                        <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" class="btn btn-info" OnClick="btnFiltrar_Click" OnClientClick="showLoader()" />
                                    </div>
                                    <div class="col-md-1">
                                        <br />
                                        <div class="loader-wrapper" id="loader-wrapper">
                                            <span class="loader"></span>
                                        </div>
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
                                                            <asp:LinkButton ID="lBtnOrden" class="form-control" runat="server" CommandName="xOrden" CommandArgument='<%# Eval("idOrden") + "ˇ" + Eval("idS") + "ˇ" + Eval("idDS") + "ˇ" + Eval("id") %>' OnCommand="lBtnOrden_Command"><%# "Orden: " + Eval("folioOrden") %></asp:LinkButton>
                                                            <%--<asp:Label ID="lblFolio" runat="server" class="form-control"><%# "Orden: " + Eval("folioOrden") %></asp:Label>--%>
                                                            <%--<i class="icon-bell"></i>--%>
                                                            <asp:Label ID="lblIDOrden" runat="server" Visible="false" Text='<%# Eval("idOrden") %>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: center;">
                                                            <asp:Label ID="Label10" runat="server" CssClass="form-control" ForeColor="Black" Text='<%# "Hora: " + Eval("hora") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label2" runat="server" Text='<%# "Cliente: " + Eval("clave") + " - " + Eval("cliente") + "<br/>" + "Vendedor: " + Eval("vendedor") + "<br/>"%>'></asp:Label>
                                                            <%--<asp:Label ID="Label6" runat="server" Text='<%# "Fecha de Entrega: " + Eval("fecha").ToString().Substring(0,10) + "<br/>" + "Cliente: " + Eval("clave") + " - " + Eval("cliente") + "<br/>"%>'></asp:Label>--%>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label7" runat="server" Text='<%# "Producto: " + Eval("descripcion") + "<br/>" + "Entrega: " + Eval("calle") + " " + Eval("numero")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label8" runat="server" Text='<%# "Cantidad Entregada: " + calcularCantE(int.Parse(Eval("idOrden").ToString()))  + " de " + 
                                                            Eval("cTotal") + "<br/>" + "Colonia: " + Eval("colonia") + " CP: " + Eval("cp")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <%--<asp:LinkButton ID="lbtnTerminarOrden" runat="server" CommandName="xTerminar" CommandArgument='<%# Eval("idOrden") + "ˇ" + Eval("idDS") + "ˇ" + Eval("cTotal") + "ˇ" + Eval("cantidadEntregada") %>' OnCommand="lbtnTerminarOrden_Command" ForeColor="White" Visible="True"><i class="icon-checkmark"></i> Terminar</asp:LinkButton>--%>
                                                            <%--<asp:LinkButton ID="lbtnAjuste" runat="server" CommandName="xAjuste" CommandArgument='<%# Eval("idOrden") + "ˇ" + Eval("idTUT") + "ˇ" + Eval("fecha") + "ˇ" + Eval("hora") + "ˇ" + Eval("codigo") + "ˇ" + Eval("descripcion") + "ˇ" + Eval("cTotal") + "ˇ" + Eval("unidad") + "ˇ" + Eval("revenimiento") + "ˇ" + Eval("cantidadEntregada") + "ˇ" + Eval("idP") + "ˇ" + Eval("idS") + "ˇ" + Eval("idDS") %>' OnCommand="lbtnAjuste_Command" ForeColor="White" Visible="True"><i class="icon-cog6"></i>Ajuste</asp:LinkButton>--%>
                                                            <br />
                                                            <div class="progress-bar" role="progressbar" aria-valuenow="10" aria-valuemin="0" aria-valuemax="10" style="width: 15px; height: 15px; background-color: <%# Eval("idEstadoSolicitud").Equals(1) ? "#00ff00" : "#FFFF00" %>; border-radius: 25px 25px">
                                                            </div>
                                                            <asp:LinkButton ID="lBtnEdoSolicitud" runat="server" CommandName="xEdoSol" CommandArgument='<%# Eval("idS") %>' OnCommand="lBtnEdoSolicitud_Command" ForeColor="White"> <%# Eval("estadoSolicitud") %></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                    <%-- listview para mostrar datos de las ordenes de dosificación --%>
                                                    <asp:ListView ID="lvOD" runat="server" ItemPlaceholderID="itemPlaceHolderOD">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td colspan="6">
                                                                    <table style="width: 100%;">
                                                                        <tr>
                                                                            <td style="width: 55%;">
                                                                                <%--<b>Remisión: </b>" + Eval("folio") + "<b> - --%>
                                                                                <asp:Label ID="Label3" runat="server" Text='<%# (string.IsNullOrEmpty((Eval("folioR").ToString())) ? "" : "<b> Remisión: </b>" + Eval("folioR") + "<b> - </b>") + "<b>Fecha: </b>" + Eval("fecha").ToString().Substring(0,10) + "<b> - Hora: </b>" + Eval("hora")+ "<b> - Cantidad: </b>" + Eval("cantidad") + " " + Eval("unidad").ToString().Trim() + "<b> - Chofer: </b>" + Eval("chofer") + "<b> - Unidad: </b>" + Eval("uTransporte") + "<b> Progreso: </b>" + Eval("porcentaje") + "%" %>'>
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
                                                                                <asp:LinkButton ID="lbtnImprimirRemision" runat="server" CommandName="xPrintRem" CommandArgument='<%# Eval("id") + "ˇ" + Eval("idEstadoDosificacion") + "ˇ" + Eval("tp") + "ˇ" + Eval("folioR") + "ˇ" + Eval("idOrden") %>' OnCommand="lbtnImprimirRemision_Command"><i class="icon-printer-text"></i>Remisión</asp:LinkButton>
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
                                                                                <div style="background-color: lightgrey; height: 20px; width: 100%">
                                                                                    <div class="progress" role="progressbar" aria-valuenow='<%# Eval("porcentaje") %>' aria-valuemin="0" aria-valuemax="100" style="width: <%# Eval("porcentaje") %>%; background-color: <%# Eval("color") %>;">
                                                                                        <asp:Label ID="Label27" runat="server" ForeColor="White" Text='<%#  getHora1(Eval("id").ToString().ToUpper()) %>'></asp:Label>
                                                                                        <asp:Label ID="Label22" runat="server" ForeColor="White" Text='<%#  getHora2(Eval("id").ToString().ToUpper()) %>'></asp:Label>
                                                                                        <asp:Label ID="Label23" runat="server" ForeColor="White" Text='<%#  getHora3(Eval("id").ToString().ToUpper()) %>'></asp:Label>
                                                                                        <asp:Label ID="Label24" runat="server" ForeColor="White" Text='<%#  getHora4(Eval("id").ToString().ToUpper()) %>'></asp:Label>
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
                                        <asp:LinkButton ID="mlbtnClose" class="close" runat="server" OnClick="mlbtnClose_Click"><span aria-hidden="true">&times;</span></asp:LinkButton>
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
                        <button type="button" style="display: none;" id="btnShowPopupEdoSol" class="btn btn-primary btn-lg"
                            data-toggle="modal" data-target="#myModalEdoSol">
                            Launch demo modal
                        </button>

                        <!-- Modal -->
                        <div class="modal fade" id="myModalEdoSol">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <asp:LinkButton ID="mlbtnEdoSolClose" class="close" runat="server" OnClick="mlbtnEdoSolClose_Click"><span aria-hidden="true">&times;</span></asp:LinkButton>
                                        <h4 class="modal-title">
                                            <asp:Label ID="mlblEdoSolTittle" runat="server" />
                                        </h4>
                                    </div>
                                    <div class="modal-body">
                                        <asp:HiddenField ID="hfEdoSolIDS" runat="server" />
                                        <asp:Label ID="Label33" runat="server" class="control-label" Text="Estado:"></asp:Label>
                                        <asp:DropDownList ID="ddlEstadoSolicitud" runat="server" class="form-control" DataSourceID="dsEstadoSolicitud" DataTextField="estado" DataValueField="id">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="dsEstadoSolicitud" runat="server" ConnectionString="<%$ ConnectionStrings:cnx %>" SelectCommand="SELECT * FROM [estadosSolicitud]"></asp:SqlDataSource>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" style="display: none;" id="btnClosePopupEdoSol" class="btn btn-default" data-dismiss="modal">
                                            Close</button>
                                        <asp:Button ID="mlbtnEdoSolAceptar" runat="server" Text="Aceptar" class="btn btn-info" OnClick="mlbtnEdoSolAceptar_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- /.modal -->

                        <!-- Button trigger modal -->
                        <button type="button" style="display: none;" id="btnShowPopupCheck" class="btn btn-primary btn-lg"
                            data-toggle="modal" data-target="#myModalCheck">
                            Launch demo modal
                        </button>

                        <!-- Modal -->
                        <div class="modal fade" id="myModalCheck">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <asp:LinkButton ID="mlbtnCheckClose" class="close" runat="server" OnClick="mlbtnCheckClose_Click"><span aria-hidden="true">&times;</span></asp:LinkButton>
                                        <h4 class="modal-title">
                                            <asp:HiddenField ID="hfCheckID" runat="server" />
                                            <asp:Label ID="mlblCheckTittle" runat="server" />
                                        </h4>
                                    </div>
                                    <div class="modal-body">
                                        <asp:Label ID="mlblCheckMessage" runat="server" />
                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button ID="mBtnCheckCerrar" runat="server" Text="Descartar" class="btn btn-default" OnClick="mBtnCheckCerrar_Click" />
                                        <button type="button" style="display: none;" id="btnClosePopupCheck" class="btn btn-default" data-dismiss="modal">
                                            Close</button>
                                        <asp:Button ID="mBtnCheckAceptar" runat="server" Text="Aceptar" class="btn btn-info" OnClick="mBtnCheckAceptar_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- /.modal -->

                        <!-- Button trigger modal -->
                        <button type="button" style="display: none;" id="btnShowPopupTC" class="btn btn-primary btn-lg"
                            data-toggle="modal" data-target="#myModalTC">
                            Launch demo modal
                        </button>

                        <!-- Modal -->
                        <div class="modal fade" id="myModalTC">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <asp:LinkButton ID="mlbtnCloseTC" class="close" runat="server" OnClick="mlbtnCloseTC_Click"><span aria-hidden="true">&times;</span></asp:LinkButton>
                                        <h4 class="modal-title">
                                            <asp:Label ID="lblAuxTerminarOrden" runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="lblAuxCancelarOrden" runat="server" Visible="false"></asp:Label>
                                            <asp:HiddenField ID="hfIdOrden" runat="server"></asp:HiddenField>
                                            <asp:HiddenField ID="hfIdDS" runat="server"></asp:HiddenField>
                                            <asp:HiddenField ID="hfCantOrd" runat="server"></asp:HiddenField>
                                            <asp:HiddenField ID="hfCantEnt" runat="server"></asp:HiddenField>
                                            <asp:Label ID="mlblTitleTC" runat="server" />
                                        </h4>
                                    </div>
                                    <div class="modal-body">
                                        <asp:Label ID="mlblMessageTC" runat="server" />
                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button ID="mbtnCancelarTC" runat="server" Text="Cerrar" class="btn btn-default" OnClick="mbtnCancelarTC_Click" />
                                        <button type="button" style="display: none;" id="btnClosePopupTC" class="btn btn-default" data-dismiss="modal">
                                            Close</button>
                                        <asp:Button ID="mbtnAceptarTC" runat="server" Text="Aceptar" class="btn btn-info" OnClick="mbtnAceptarTC_Click" />
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
                                                            <asp:UpdatePanel ID="upPDF" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:Literal ID="mlPDF" runat="server"></asp:Literal>
                                                                    <asp:Label ID="mlblMensajeRemPDF" runat="server" Text="" ForeColor="Red" Font-Size="Large" Font-Bold="True"></asp:Label>
                                                                </ContentTemplate>
                                                                <Triggers>
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

                        <asp:HiddenField ID="hfId" runat="server" Value="0" />
                        <asp:HiddenField ID="hfCTotal" runat="server" Value="0" />
                        <asp:HiddenField ID="hfTDTotal" runat="server" Value="0" />
                        <asp:HiddenField ID="hfBBATotal" runat="server" Value="0" />
                        <asp:HiddenField ID="hfBlockTotal" runat="server" Value="0" />
                        <asp:HiddenField ID="hfCDone" runat="server" Value="0" />
                        <asp:HiddenField ID="hfTDDone" runat="server" Value="0" />
                        <asp:HiddenField ID="hfBBADone" runat="server" Value="0" />
                        <asp:HiddenField ID="hfBlockDone" runat="server" Value="0" />
                        <asp:HiddenField ID="hfCPte" runat="server" Value="0" />
                        <asp:HiddenField ID="hfTDPte" runat="server" Value="0" />
                        <asp:HiddenField ID="hfBBAPte" runat="server" Value="0" />
                        <asp:HiddenField ID="hfBlockPte" runat="server" Value="0" />
                    </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="tmrCheckChanges" EventName="Tick" />
            <asp:AsyncPostBackTrigger ControlID="btnFiltrar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="cbxChoferes" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="cbxClientes" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="cbxProductos" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="cbxUnidad" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="cbxVendedores" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="chbxTerminados" EventName="CheckedChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlFFiltros" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="mBtnCheckCerrar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="mBtnCheckAceptar" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <!-- Row Ends -->
<script>
    function showLoader() {
        document.getElementById("loader-wrapper").style.display = "block";
        return true;
    }
        function hideLoader() {
        document.getElementById("loader-wrapper").style.display = "none";
    }
    function showLoaderPage() {
        document.getElementById("loader-page").style.display = "block";
        let alto = ($(document).height() / 2) - (window.innerHeight / 2);
        window.scrollTo(0, alto);
        return true;
    }
    function hideLoaderPage() {
        document.getElementById("loader-page").style.display = "none";
    }
</script>
</asp:Content>
