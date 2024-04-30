<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ordenes.aspx.cs" Inherits="despacho.ordenes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <!-- Top Bar Starts -->
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
    <div class="top-bar clearfix">
        <div class="row gutter">
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <div class="page-title">
                    <h3>Ordenes</h3>
                    <p>/ <a href="home.aspx">Home</a></p>
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
                        <a href="ordenesAdd.aspx" class="btn btn-info">
                            <i class="icon-add-to-list"></i>Agregar
                        </a>
                        <%--<a href="javascript:void(0)" class="btn btn-info"><i class="icon-download6"></i> Export</a>--%>
                    </li>
                </ul>
            </div>
        </div>
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
                                <h4>Ordenes</h4>
                            </div>
                            <div class="panel-body">
                                <div class="row gutter">
                                    <div class="col-md-12">
                                        <asp:CheckBox ID="chbxProgramadas" runat="server" class="radio-inline" Text="Programadas" Checked="false" AutoPostBack="True" OnCheckedChanged="chbxProgramadas_CheckedChanged" />
                                    </div>
                                </div>
                                <div class="row gutter">
                                    <div class="col-md-2">
                                        <asp:Label ID="lblFechaI" runat="server" Text="Fecha Inicio:" class="control-label" ></asp:Label><asp:TextBox ID="txtFechaI" runat="server" class="form-control" AutoComplete="off"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="txtFechaI_CalendarExtender" runat="server" BehaviorID="txtFechaI_CalendarExtender" TargetControlID="txtFechaI" FirstDayOfWeek="Monday" Format="yyyy-MM-dd" PopupPosition="BottomRight" />
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label ID="lblFechaF" runat="server" Text="Fecha Fin" class="control-label" ></asp:Label><asp:TextBox ID="txtFechaF" runat="server" class="form-control" AutoComplete="off"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="txtFechaF_CalendarExtender" runat="server" BehaviorID="txtFechaF_CalendarExtender" TargetControlID="txtFechaF" FirstDayOfWeek="Monday" Format="yyyy-MM-dd" PopupPosition="BottomRight" />
                                    </div>
                                    <div class="col-md-8">
                                        <br />
                                        <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" class="btn btn-info" OnClick="btnFiltrar_Click" />
                                    </div>
                                </div>
                                <br />
                                <div class="table-responsive">
                                    <asp:ListView ID="listView" runat="server" OnItemCommand="listView_ItemCommand" OnItemDeleting="listView_ItemDeleting">
                                        <LayoutTemplate>
                                            <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                                <thead>
                                                    <tr>
                                                        <th style="text-align: center" id="folio">Folio</th>
                                                        <th style="text-align: center" id="fecha">Fecha</th>
                                                        <th id="nombreCliente">Nombre del Cliente</th>
                                                        <th id="vendedor">Aprobó</th>
                                                        <th id="comentario1">Comentarios</th>
                                                        <th id="comentario2">Comentarios de Ubicación</th>
                                                        <th style="text-align: center" id="orden">Programar</th>
                                                        <th style="text-align: center" id="solicitud">Regresar a Solicitud</th>
                                                        <th style="text-align: center" id="eliminar">Eliminar</th>
                                                        <%--                                                        <th style="text-align: center" id="modificar">Modificar</th>
                                                        <th style="text-align: center" id="eliminar">Eliminar</th>--%>
                                                    </tr>
                                                </thead>
                                                <tr id="itemPlaceholder" runat="server"></tr>
                                                <tfoot>
                                                    <tr>
                                                        <th style="text-align: center">Folio</th>
                                                        <th style="text-align: center">Fecha</th>
                                                        <th>Nombre del Cliente</th>
                                                        <th>Aprobó</th>
                                                        <th>Comentarios</th>
                                                        <th>Comentarios de Ubicación</th>
                                                        <th style="text-align: center">Programar</th>
                                                        <th style="text-align: center">Regresar a Solicitud</th>
                                                        <th style="text-align: center">Eliminar</th>
                                                        <%--                                                        <th style="text-align: center">Modificar</th>
                                                        <th style="text-align: center">Eliminar</th>--%>
                                                    </tr>
                                                </tfoot>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td style="text-align: center"><%# Eval("folio") %></td>
                                                <td style="text-align: center"><%# Eval("fecha").ToString().Substring(0,10) %></td>
                                                <td><%# Eval("cliente") %></td>
                                                <td><%# Eval("nombreV2") %></td>
                                                <td><%# Eval("comentarios") %></td>
                                                <td><%# Eval("ubicacion") %></td>
                                                <td style="text-align: center">
                                                    <a href="generarOD.aspx?id=<%# Eval("id") %>">
                                                        <i class="icon-calendar2"></i>
                                                    </a>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:LinkButton ID="lbtnSolicitud" runat="server" CommandArgument='<%# Eval("id") + "ˇ" + Eval("folio") + "ˇ" + Eval("idSolicitud") %>' CommandName="solicitud"><i class="icon-paper"></i></asp:LinkButton>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:LinkButton ID="lbtnEliminar" runat="server" CommandArgument='<%# Eval("id") + "ˇ" + Eval("folio") + "ˇ" + Eval("idSolicitud") %>' CommandName="delete"><i class="icon-delete"></i></asp:LinkButton>
                                                </td>
                                                <%--<td style="text-align: center">
                                                    <a href="solicitudesMod.aspx?id=<%# Eval("id") %>">
                                                        <i class="icon-new-message"></i>
                                                    </a>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:LinkButton ID="lbtnEliminar" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="delete"><i class="icon-delete"></i></asp:LinkButton>
                                                </td>--%>
                                            </tr>
                                        </ItemTemplate>
                                        <EmptyDataTemplate>
                                            <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                                <thead>
                                                    <tr>
                                                        <th style="text-align: center" id="folio">Folio</th>
                                                        <th style="text-align: center" id="fecha">Fecha</th>
                                                        <th id="nombreCliente">Nombre del Cliente</th>
                                                        <th id="vendedor">Aprobó</th>
                                                        <th id="comentario1">Comentarios</th>
                                                        <th id="comentario2">Comentarios de Ubicación</th>
                                                        <th style="text-align: center" id="aprobarS">Programar</th>
                                                        <th style="text-align: center" id="solicitudS">Regresar a Solicitud</th>
                                                        <th style="text-align: center" id="eliminarS">Eliminar</th>
                                                        <%--                                                        <th style="text-align: center" id="modificarS">Modificar</th>
                                                        <th style="text-align: center" id="eliminarS">Eliminar</th>--%>
                                                    </tr>
                                                </thead>
                                                <tr>
                                                    <td colspan="9">
                                                        <label class="label label-danger">¡No hay Ordenes Registrados!</label></td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                    </asp:ListView>
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
                                        <asp:Label ID="lblID" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblIDS" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblFolio" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblAux" runat="server" Visible="false"></asp:Label>
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
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="chbxProgramadas" EventName="CheckedChanged" />
            <asp:AsyncPostBackTrigger ControlID="listView" EventName="ItemCommand" />
        </Triggers>
    </asp:UpdatePanel>
    <!-- Row Ends -->

</asp:Content>
