<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PagosSolicitudF.aspx.cs" Inherits="despacho.PagosSolicitud" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts" runat="server">
    <script>


        // funcion a cambiar en caso de ser necesario 
        function abrirPDF(url) {
            console.log(url);
            window.open("/Pagos/" + url, "PDF");
        }

        function abrirPDFRem(url) {
            console.log(url);
            window.open(url,"PDFRem");
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
                    <h3>Pagos de Solicitud</h3>
                    <p>/ <a href="cFinanzas.aspx">Cobranza Finanzas</a> /Pagos de Solicitud</p>
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
                        <a href="PagosSolicitudFAdd.aspx?idSolicitud=<%= Request.QueryString["idSolicitud"] %>&Vendedor=<%= Request.QueryString["Vendedor"] %>&estatus=<%= Request.QueryString["estatus"] %>&factura=<%= Request.QueryString["factura"] %>&fechaIF=<%= Request.QueryString["fechaIF"] %>&fechaFF=<%= Request.QueryString["fechaFF"] %>&ordenF=<%= Request.QueryString["ordenF"] %>&remF=<%= Request.QueryString["remF"] %>&cteF=<%= Request.QueryString["cteF"] %>&vendedorF=<%= Request.QueryString["vendedorF"] %>&estatusF=<%= Request.QueryString["estatusF"] %>" class="btn btn-info">
                            <i class="icon-add-to-list"></i>Agregar
                        </a>
                        <%--<a href="javascript:void(0)" class="btn btn-info"><i class="icon-download6"></i> Export</a>--%>
                    </li>
                </ul>
            </div>
        </div>
    </div>
    <!-- Top Bar Ends -->

    <div class="row gutter">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-blue">
                <div class="panel-heading">
                    <a href="cfinanzas.aspx" runat="server" id="regresar" class="btn btn-warning">Regresar</a>
                    <asp:Button Text="Extraer Pagos de Contpaqi" CssClass="btn btn-info" runat="server" ID="btnActualizar" OnClick="ExtraerPago" />
                    <asp:Label ID="lblInfo" runat="server" Text="" Visible="false" ></asp:Label>
                    <h4>Pagos de Solicitud</h4>
                </div>

                <div class="panel-body">
                    <div class="table-responsive">
                        <div class="text-center" style="margin-bottom: 10px">
                            <asp:Label ID="lblFolio" runat="server"></asp:Label>
                        </div>
                        <div class="text-center" style="margin-bottom: 10px">
                            <asp:Label ID="lblFinanzas" runat="server"><strong>Finanzas</strong></asp:Label>
                        </div>
                        <asp:ListView ID="lvPagos" runat="server">
                            <LayoutTemplate>
                                <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                    <thead>
                                        <tr>
                                            <th style="text-align: center" id="folio">Folio</th>
                                            <th style="text-align: center" id="Fecha">Fecha</th>
                                            <th style="text-align: center" id="Monto">Monto</th>
                                            <%--<th style="text-align: center" id="Saldo">Saldo</th>--%>
                                            <th style="text-align: center" id="Pago">Pago</th>
                                            <%--<th style="text-align: center" id="Estatus">Estatus</th>--%>
                                            <th style="text-align: center" id="Cancelar">Cancelar</th>
                                            <th style="text-align: center" id="pdf">PDF</th>
                                        </tr>
                                    </thead>
                                    <tr id="itemPlaceholder" runat="server"></tr>
                                    <tfoot>
                                        <tr>
                                            <th style="text-align: center" id="folioF">Folio</th>
                                            <th style="text-align: center" id="FechaF">Fecha</th>
                                            <th style="text-align: center" id="MontoF">Monto</th>
                                             <%--<th style="text-align: center" id="SaldoF">Saldo</th>--%>
                                            <th style="text-align: center" id="PagoF">Pago</th>
                                            <%--<th style="text-align: center" id="EstatusF">Estatus</th>--%>
                                            <th style="text-align: center" id="CancelarF">Cancelar</th>
                                            <th style="text-align: center" id="">PDF</th>
                                                 <a href="filtroPagos.aspx">filtroPagos.aspx</a>
                                        </tr>
                                    </tfoot>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td style="text-align: center"><%# Eval("folio") %></td>
                                    <td style="text-align: center"><%# Eval("fechaPago").ToString().Substring(0, 10) %></td>
                                    <td style="text-align: center"><%# convertir(Eval("monto").ToString()) %></td>
                                    <%--   <td style="text-align: center"><%# convertir(Eval("saldo").ToString()) %></td>--%>
                                    <td style="text-align: center"><%# convertir(Eval("pago").ToString()) %></td>
                                    <%--<td style="text-align: center"><%# Eval("estatus") %></td>--%>
                                    <td style="text-align: center">
                                        <div <%# mostrarCancelar(Eval("estatus").ToString()) %>>
                                            <asp:LinkButton runat="server" ID="lkCancelar" CommandArgument='<%# Eval("id") %>' OnCommand="lkCancelar_ItemCommand">
                                            <span class="icon-cancel2" style="font-size: 20px"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </td>
                                    <td style="text-align: center">
                                        <asp:LinkButton runat="server" ID="lkPDF" CommandName="pdf" CommandArgument='<%#  Eval("id") + ";" + Eval("pdfPago") %>' OnCommand="lkCancelar_ItemCommand">
                                            <span class="icon-printer-text" style="font-size: 20px"></span>
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                    <thead>
                                        <tr>
                                            <th style="text-align: center" id="folio">Folio</th>
                                            <th style="text-align: center" id="Fecha">Fecha</th>
                                            <th style="text-align: center" id="Monto">Monto</th>
                                            <%-- <th style="text-align: center" id="Saldo">Saldo</th>
                                            <th style="text-align: center" id="Pago">Pago</th>
                                            <th style="text-align: center" id="EstatusF">Estatus</th>--%>
                                            <th style="text-align: center" id="CancelarF">Cancelar</th>
                                            <th style="text-align: center" id="pdf">PDF</th>
                                        </tr>
                                    </thead>
                                    <tr>
                                        <td colspan="14" class="text-center">
                                            <label class="label label-danger">¡No hay Pagos Registrados!</label></td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                        </asp:ListView>
                        <div class="text-center" style="margin-bottom: 10px; margin-top: 10px;">
                            <asp:Label ID="lblVendedor" runat="server"></asp:Label>
                        </div>
                        <asp:ListView ID="LvPagosVendedor" runat="server">
                            <LayoutTemplate>
                                <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                    <thead>
                                        <tr>
                                            <th style="text-align: center" id="folioV">Folio</th>
                                            <th style="text-align: center" id="FechaV">Fecha</th>
                                            <th style="text-align: center" id="MontoV">Monto</th>
                                            <%--   <th style="text-align: center" id="SaldoV">Saldo</th>
                                            <th style="text-align: center" id="PagoV">Pago</th>
                                            <th style="text-align: center" id="estatus">Estatus</th>--%>
                                        </tr>
                                    </thead>
                                    <tr id="itemPlaceholder" runat="server"></tr>
                                    <tfoot>
                                        <tr>
                                            <th style="text-align: center" id="folioVF">Folio</th>
                                            <th style="text-align: center" id="FechaVF">Fecha</th>
                                            <th style="text-align: center" id="MontoVF">Monto</th>
                                            <%--  <th style="text-align: center" id="SaldoVF">Saldo</th>
                                            <th style="text-align: center" id="PagoVF">Pago</th>
                                            <th style="text-align: center">Estatus</th>--%>
                                        </tr>
                                    </tfoot>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td style="text-align: center"><%# Eval("folio") %></td>
                                    <td style="text-align: center"><%# Eval("fechaPago").ToString().Substring(0, 10) %></td>
                                    <td style="text-align: center"><%# convertir(Eval("monto").ToString()) %></td>
                                    <%--   <td style="text-align: center"><%# convertir(Eval("saldo").ToString()) %></td>
                                    <td style="text-align: center"><%# convertir(Eval("pago").ToString()) %></td>
                                    <td style="text-align: center"><%# Eval("estatus") %></td>--%>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                    <thead>
                                        <tr>
                                            <th style="text-align: center" id="folio">Folio</th>
                                            <th style="text-align: center" id="Fecha">Fecha</th>
                                            <th style="text-align: center" id="Monto">Monto</th>
                                            <%--                   <th style="text-align: center" id="Saldo">Saldo</th>
                                            <th style="text-align: center" id="Pago">Pago</th>
                                            <th style="text-align: center" id="estatus">Estatus</th>--%>
                                        </tr>
                                    </thead>
                                    <tr>
                                        <td colspan="14" class="text-center">
                                            <label class="label label-danger">¡No hay Pagos Registrados!</label></td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                        </asp:ListView>
                        <br />
                        <asp:Label ID="lblSaldo" BackColor="Yellow" ForeColor="Black" Font-Size="Large" Font-Bold="true" runat="server" class="label">Saldo:0</asp:Label>
                        <asp:Label ID="lblPago" BackColor="Yellow" ForeColor="Black" Font-Size="Large" Font-Bold="true" runat="server" class="label">Pagado:0</asp:Label>
                        <asp:Label ID="lblStatus" BackColor="Yellow" ForeColor="Black" Font-Size="Large" Font-Bold="true" runat="server" class="label">Status:Pendiente</asp:Label>
                        <div class="text-center" style="margin-bottom: 10px; margin-top: 10px;">
                            <strong>Remisiones</strong>
                        </div>

                        <asp:ListView ID="lvRemision" runat="server">
                            <LayoutTemplate>
                                <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                    <thead>
                                        <tr>
                                            <th style="text-align: center">Folio</th>
                                            <th style="text-align: center">Estatus</th>
                                            <th style="text-align: center">Ver</th>
                                        </tr>
                                    </thead>
                                    <tr id="itemPlaceholder" runat="server"></tr>
                                    <tfoot>
                                        <tr>
                                            <th style="text-align: center">Folio</th>
                                            <th style="text-align: center">Estatus</th>
                                            <th style="text-align: center">Ver</th>
                                        </tr>
                                    </tfoot>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td style="text-align: center"><%# Eval("folioR") %></td>
                                    <td style="text-align: center">
                                        <%# obtenerEstatus(Eval("eliminado").ToString()) %>
                                    </td>
                                    <td style="text-align: center">
                                        <div <%# mostrarPDF(Eval("folioR").ToString(), int.Parse(Eval("idSucursal").ToString())) %>>
                                            <a href="../Dosificacion/Remisiones/<%# Eval("idSucursal").ToString() %>/Remision<%# Eval("folioR").ToString() %>.pdf" target="_blank">
                                                <span class="icon-printer-text" style="font-size: 20px"></span>
                                            </a>
                                        </div>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                    <thead>
                                        <tr>
                                            <th style="text-align: center">Folio</th>
                                            <th style="text-align: center">Estatus</th>
                                            <th style="text-align: center">Ver</th>
                                        </tr>
                                    </thead>
                                    <tr>
                                        <td colspan="14" class="text-center">
                                            <label class="label label-danger">¡No hay Remisiones Registradas!</label></td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                        </asp:ListView>

                        <asp:HiddenField runat="server" ID="hdIdPago" />

                        <!-- Modal -->
                        <div class="modal fade" id="myModal">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span></button>
                                        <h4 class="modal-title">Cancelar Pago
                                        </h4>
                                    </div>
                                    <div class="modal-body">
                                        <p>¿Seguro(a) que desea cancelar el Pago?</p>
                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button ID="mbtnClose" runat="server" Text="Cerrar" class="btn btn-default" />
                                        <button type="button" style="display: none;" id="btnClosePopup" class="btn btn-default" data-dismiss="modal">
                                            Close</button>
                                        <asp:Button ID="mbtnAceptar" runat="server" Text="Aceptar" class="btn btn-info" OnClick="mbtnAceptar_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- /.modal -->
                        <!-- Modal -->
                        <div class="modal fade" id="myModalPDF">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span></button>
                                        <h4 class="modal-title">Error
                                        </h4>
                                    </div>
                                    <div class="modal-body">
                                        <asp:Label runat="server" ID="lblErrorPDF"></asp:Label>
                                    </div>
                                    <div class="modal-footer">
                                        <a href="pagosSolicitudF.aspx?idSolicitud=<%= Request.QueryString["idSolicitud"] %>&Vendedor=<%= Request.QueryString["Vendedor"] %>&estatus=<%= Request.QueryString["estatus"] %>" class="btn btn-default">Aceptar</a>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- /.modal -->
                        <!-- Modal Avisos Contpaq-->
                        <div class="modal fade" id="myModalContpaq">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span></button>
                                        <h4 class="modal-title">Aviso
                                        </h4>
                                    </div>
                                    <div class="modal-body">
                                        <%--<p>No se han encontrado pagos para esta Remisión en Contpaq</p>--%>
                                        <asp:Label runat="server" ID="LabelTerminaContpaq"></asp:Label>
                                    </div>
                                    <div class="modal-footer">
                                        <%--<asp:Button ID="Button1" runat="server" Text="Cerrar" class="btn btn-default" />
                                        <button type="button" style="display: none;" id="btnClosePopupContpaq" class="btn btn-default" data-dismiss="modal">
                                            Close</button>
                                        <asp:Button ID="Button2" runat="server" Text="Aceptar" class="btn btn-info" OnClick="mbtnAceptar_Click" />--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- /.modal -->
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
