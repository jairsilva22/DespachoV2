<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="solicitudes.aspx.cs" Inherits="despacho.solicitudes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts" runat="server">
    <script>
        function openCot(id) {
            var posicion_x;
            var posicion_y;
            posicion_x = (screen.width / 2) - (500 / 2);
            posicion_y = (screen.height / 2) - (400 / 2);

            open("solicitudesCot.aspx?id=" + id, '', 'top=' + posicion_y + ', left=' + posicion_x + ', width=800, height=650');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <!-- Top Bar Starts -->
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
    <div class="top-bar clearfix">
        <div class="row gutter">
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <div class="page-title">
                    <h3>Solicitudes</h3>
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
                        <a href="solicitudesAdd.aspx" class="btn btn-info">
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
                                <h4>Solicitudes</h4>
                            </div>
                            <div class="panel-body">
                                <div class="table-responsive">
                                    <asp:ListView ID="listView" runat="server" OnItemCommand="listView_ItemCommand" OnItemDeleting="listView_ItemDeleting">
                                        <LayoutTemplate>
                                            <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                                <thead>
                                                    <tr>
                                                        <th style="text-align: center" id="folio">Folio</th>
                                                        <th style="text-align: center" id="fecha">Fecha</th>
                                                        <th style="text-align: center" id="hora">Hora</th>
                                                        <th style="text-align: center" id="claveCliente">Clave del Cliente</th>
                                                        <th id="nombreCliente">Nombre del Cliente</th>
                                                        <th id="vendedor">Vendedor</th>
                                                        <th id="proyecto">Proyecto</th>
                                                        <th id="fpago">Forma de Pago</th>
                                                        <th style="text-align: center" id="estado">Estado</th>
                                                        <th style="text-align: center" id="tipo">Tipo</th>
                                                        <th style="text-align: center" id="cotizacion">Cotización</th>
                                                        <th style="text-align: center" id="orden">Aprobar</th>
                                                        <th style="text-align: center" id="modificar">Modificar</th>
                                                        <th style="text-align: center" id="eliminar">Eliminar</th>
                                                    </tr>
                                                </thead>
                                                <tr id="itemPlaceholder" runat="server"></tr>
                                                <tfoot>
                                                    <tr>
                                                        <th style="text-align: center">Folio</th>
                                                        <th style="text-align: center">Fecha</th>
                                                        <th style="text-align: center">Hora</th>
                                                        <th style="text-align: center">Clave del Cliente</th>
                                                        <th>Nombre del Cliente</th>
                                                        <th>Vendedor</th>
                                                        <th>Proyecto</th>
                                                        <th>Forma de Pago</th>
                                                        <th style="text-align: center">Estado</th>
                                                        <th style="text-align: center">Tipo</th>
                                                        <th style="text-align: center">Cotización</th>
                                                        <th style="text-align: center">Aprobar</th>
                                                        <th style="text-align: center">Modificar</th>
                                                        <th style="text-align: center">Eliminar</th>
                                                    </tr>
                                                </tfoot>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td style="text-align: center"><%# Eval("folio") %></td>
                                                <td style="text-align: center"><%# Eval("fecha").ToString().Substring(0, 10) %></td>
                                                <td style="text-align: center"><%# Eval("hora") %></td>
                                                <td style="text-align: center"><%# Eval("clave") %></td>
                                                <td><%# Eval("nombreCliente") %></td>
                                                <td><%# Eval("vendedor") %></td>
                                                <td><%# Eval("proyecto") %></td>
                                                <td><%# Eval("fpago") %></td>
                                                <td style="text-align: center"><%# Eval("estado") %></td>
                                                <td style="text-align: center">
                                                    <%# ((bool)Eval("tipo") == true) ? "Orden" : "Solicitud" %>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:LinkButton ID="lbtnCotizacion" runat="server" CommandArgument='<%# Eval("id") + "ˇ" + Eval("folio") + "ˇ" + Eval("email") %>' CommandName="Cot"><i class="icon-file-text2"></i></asp:LinkButton>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:LinkButton ID="lbtnValidarSolicitud" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="ValSol"><i class="icon-clipboard"></i></asp:LinkButton>
                                                </td>
                                                <td style="text-align: center">
                                                    <a href="solicitudesMod.aspx?id=<%# Eval("id") %>">
                                                        <i class="icon-new-message"></i>
                                                    </a>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:LinkButton ID="lbtnEliminar" runat="server" CommandArgument='<%# Eval("id") + "ˇ" + Eval("folio") %>' CommandName="delete"><i class="icon-delete"></i></asp:LinkButton>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <EmptyDataTemplate>
                                            <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                                <thead>
                                                    <tr>
                                                        <th style="text-align: center" id="folio">Folio</th>
                                                        <th style="text-align: center" id="fecha">Fecha</th>
                                                        <th style="text-align: center" id="hora">Hora</th>
                                                        <th style="text-align: center" id="claveCliente">Clave del Cliente</th>
                                                        <th id="nombreCliente">Nombre del Cliente</th>
                                                        <th id="vendedor">Vendedor</th>
                                                        <th id="proyecto">Proyecto</th>
                                                        <th id="fpago">Forma de Pago</th>
                                                        <th style="text-align: center" id="estado">Estado</th>
                                                        <th style="text-align: center" id="tipo">Tipo</th>
                                                        <th style="text-align: center" id="cotizacionS">Cotización</th>
                                                        <th style="text-align: center" id="aprobarS">Aprobar</th>
                                                        <th style="text-align: center" id="modificarS">Modificar</th>
                                                        <th style="text-align: center" id="eliminarS">Eliminar</th>
                                                    </tr>
                                                </thead>
                                                <tr>
                                                    <td colspan="14">
                                                        <label class="label label-danger">¡No hay Solicitudes Registrados!</label></td>
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
                                        <asp:HiddenField ID="hfDelete" runat="server" />
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
                                        <%--<button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span></button>--%>
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
                                                            <asp:Label ID="lblIdSol" runat="server" Text="" class="control-label" Visible="false"></asp:Label>
                                                            <asp:Label ID="Label2" runat="server" Text="Observaciones" class="control-label" Visible="false"></asp:Label>
                                                            <asp:TextBox ID="txtObservaciones" runat="server" class="form-control" TextMode="MultiLine" Visible="false"></asp:TextBox>
                                                            <asp:Button ID="btnGenerarPDF" runat="server" Text="Generar Cotización" class="btn btn-info" OnClick="btnGenerarPDF_Click" Visible="false" />
                                                        </div>
                                                    </div>
                                                    <div class="row gutter">
                                                        <div class="col-md-5">
                                                            <asp:Label ID="mlblPara" runat="server" Text="Para:" class="control-label"></asp:Label>
                                                            <asp:TextBox ID="mtxtPara" runat="server" class="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-5">
                                                            <asp:Label ID="mlblCopia" runat="server" Text="Con copia a:" class="control-label"></asp:Label>
                                                            <asp:TextBox ID="mtxtCopia" runat="server" class="form-control" TextMode="Email"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <br />
                                                            <asp:Button ID="btnEnviarCot" runat="server" Text="Enviar por Correo" class="btn btn-info" OnClick="btnEnviarCot_Click" Visible="False" />
                                                        </div>
                                                    </div>
                                                    <%--<div class="row gutter">
                                                        <div class="col-md-4">
                                                        </div>
                                                        <div class="col-md-8">
                                                        </div>
                                                    </div>--%>
                                                    <div class="row gutter">
                                                        <div class="col-md-12">
                                                        </div>
                                                    </div>
                                                    <div class="row gutter">
                                                        <div class="col-md-12">
                                                            <asp:UpdatePanel ID="upPDF" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:Literal ID="lPDF" runat="server"></asp:Literal>
                                                                    <asp:Label ID="lblMensaje" runat="server" Text="" ForeColor="Red" Font-Size="Large" Font-Bold="True"></asp:Label>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="btnGenerarPDF" EventName="Click" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                            <asp:HiddenField ID="hfDocumento" runat="server" />
                                                            <asp:HiddenField ID="hfRuta" runat="server" />
                                                            <asp:HiddenField ID="hfFolio" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button ID="mbtnCloseCot" runat="server" Text="Cerrar" class="btn btn-default" OnClick="mbtnCloseCot_Click" UseSubmitBehavior="False" />
                                        <button type="button" style="display: none;" id="btnClosePopupCot" class="btn btn-default" data-dismiss="modal">
                                            Close</button>
                                        <asp:Button ID="mbtnAceptarCot" runat="server" Text="Aceptar" class="btn btn-info" OnClick="mbtnAceptarCot_Click" Visible="False" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- /.modal -->
                    </div>
                    <asp:HiddenField ID="hfId" runat="server" Value="0" />
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="listView" EventName="ItemCommand" />
            <asp:AsyncPostBackTrigger ControlID="btnGenerarPDF" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnEnviarCot" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="mbtnAceptar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="mbtnClose" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="mbtnAceptarCot" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="mbtnCloseCot" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <!-- Row Ends -->

</asp:Content>
