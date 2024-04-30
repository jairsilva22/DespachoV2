<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="cfinanzas.aspx.cs" Inherits="despacho.cfinanzas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts" runat="server">
    <script>
        function parametros() {
            var parametro = "";
            //verificamos si los campos están vacios
            if ($("[id*='txtOrden']").val() != "") {
                parametro += " AND o.folio = '"+ $("[id*='txtOrden']").val()+"'"
            }
            if ($("[id*='txtRem']").val() != "") {
                parametro += " AND od.folioR = '"+$("[id*='txtRem']").val()+"'"
            }
            if ($("[id*='ddlCte']").val() != "" && $("[id*='ddlCte']").val() != null) {
                parametro += " AND s.idCliente = "+$("[id*='ddlCte']").val()
            }
            if ($("[id*='ddlVendedor']").val() != "" && $("[id*='ddlVendedor']").val() != null) {
                parametro += " AND s.idVendedor = "+$("[id*='ddlVendedor']").val()
            }
            if ($("[id*='ddlEstatus']").val() != "" && $("[id*='ddlEstatus']").val() != null) {
                parametro += " AND s.estatusPago = '"+$("[id*='ddlEstatus']").val()+"'"
            }

            //asignamos el valor de la variable a un input tipo hidden
            $("[id*='hdfParametros']").val(parametro)
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
                    <h3>Cobranza Finanzas</h3>
                    <p>/ <a href="cfinanzas.aspx">Remisiones</a></p>
                </div>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <%--<ul class="right-stats" id="mini-nav-right">
                   
                    <li>
                        <a href="solicitudesAdd.aspx" class="btn btn-info">
                            <i class="icon-add-to-list"></i>Agregar
                        </a>
                        
                    </li>
                </ul>--%>
            </div>
        </div>
    </div>
    <!-- Top Bar Ends -->

    <div class="row gutter">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-blue">
                <div class="panel-heading">
                    <div class="tabbable tabs-left clearfix">
									<ul class="nav nav-tabs">
										<li class="active">
											<a href="cFinanzas.aspx" aria-expanded="true">Remisiones sin Facturar</a>
										</li>
										<li class="">
											<a href="cFinanzasRem.aspx" aria-expanded="false">Remisiones a Facturar</a>
										</li>
										
									</ul>
									
								</div>
                    <h4>Remisiones sin Facturar</h4>
                </div>
                <div class="panel-body">
                    <div class="row gutter">
                        <div class="col-md-3">
                            <asp:Label ID="lblFechaI" runat="server" Text="Fecha Inicio:" class="control-label"></asp:Label>
                            <asp:TextBox ID="txtFechaI" runat="server" class="form-control" AutoComplete="off"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="txtFechaI_CalendarExtender" runat="server" BehaviorID="txtFechaI_CalendarExtender" TargetControlID="txtFechaI" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" PopupPosition="BottomRight" />
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="lblFechaF" runat="server" Text="Fecha Fin" class="control-label"></asp:Label>
                            <asp:TextBox ID="txtFechaF" runat="server" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="txtFechaF_CalendarExtender" runat="server" BehaviorID="txtFechaF_CalendarExtender" TargetControlID="txtFechaF" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" PopupPosition="BottomRight" />
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="lblOrden" runat="server" Text="Orden" class="control-label"></asp:Label>
                            <asp:TextBox runat="server" ID="txtOrden" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="Label1" runat="server" Text="Remision" class="control-label"></asp:Label>
                            <asp:TextBox runat="server" ID="txtRem" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="Label2" runat="server" Text="Cliente" class="control-label"></asp:Label>
                            <asp:DropDownList runat="server" ID="ddlCte" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="Label3" runat="server" Text="Vendedor" class="control-label"></asp:Label>
                            <asp:DropDownList runat="server" ID="ddlVendedor" CssClass="form-control"></asp:DropDownList>
                        </div>
                         <div class="col-md-3">
                            <asp:Label ID="Label4" runat="server" Text="Estatus" class="control-label"></asp:Label>
                            <asp:DropDownList runat="server" ID="ddlEstatus" CssClass="form-control">
                                <asp:ListItem Value="">Filtrar por Estatus</asp:ListItem>
                                <asp:ListItem Value="Pagado">Pagado</asp:ListItem>
                                <asp:ListItem Value="No Pagado">No Pagado</asp:ListItem>
                                <asp:ListItem Value="Parcial">Parcial</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="Label5" runat="server" Text="Folios de pago" class="control-label"></asp:Label>
                            <asp:TextBox runat="server" ID="txtFoliosPagos" CssClass="form-control"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtFoliosPagos" ErrorMessage="Ingrese solo números" ValidationExpression="\d+"></asp:RegularExpressionValidator>
                        </div>
                        <div class="col-md-3">
                            <asp:HiddenField ID="hdfParametros" runat="server" />
                            <br />
                            <asp:Button runat="server" ID="btnBuscar" CssClass="btn btn-success" Text="Buscar" OnClick="btnBuscar_Click" OnClientClick="parametros()" />
                            <asp:Button  runat="server" ID="btnExcel" CssClass="btn btn-info" Text="Descargar Excel" OnClick="btnExcel_Click" OnClientClick="parametros()" />
                        </div>
               </div>
            </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <asp:ListView ID="lvSolicitudes" runat="server">
                            <LayoutTemplate>
                                <table id="responsiveTabl" class="table table-striped table-bordered no-margin">
                                    <thead>
                                        <tr>
                                            <th style="text-align: center" id="Facturar">Facturar</th>
                                            <th style="text-align: center" id="cotizacion">Pagos</th>
                                            <th style="text-align: center" id="foliosP">Folios de pago</th>
                                            <th style="text-align: center" id="ordenE">Actualizar Orden</th>
                                            <th style="text-align: center" id="folio">Folio Solicitud</th>
                                            <th style="text-align: center" id="orden">Orden</th>
                                            <th style="text-align: center" id="orden">Remisiones</th>
                                            <th style="text-align: center" id="fecha">Fecha</th>
                                            <th style="text-align: center" id="hora">Hora</th>
                                            <th style="text-align: center" id="claveCliente">Clave del Cliente</th>
                                            <th id="nombreCliente">Nombre del Cliente</th>
                                            <th id="vendedor">Vendedor</th>
                                           <%-- <th id="proyecto">Proyecto</th>
                                            <th id="estOrd">Estatus Orden</th>--%>
                                            <th id="fpago">Forma de Pago</th>
                                            <th style="text-align: center" id="estado">Estado</th>
                                            <th style="text-align: center" id="cant">Cant. Entregada</th>
                                            <th style="text-align: center" id="monto">Monto</th>
                                            <th style="text-align: center" id="Pagado">Saldo</th>
                                        </tr>
                                    </thead>
                                    <tr id="itemPlaceholder" runat="server"></tr>
                                    <tfoot>
                                        <tr>
                                            <th style="text-align: center">Facturar</th>
                                            <th style="text-align: center">Pagos</th>
                                            <th style="text-align: center">Folios de pago</th>
                                            <th style="text-align: center">Actualizar Orden</th>
                                            <th style="text-align: center">Folio Solicitud</th>
                                            <th style="text-align: center">Orden</th>
                                            <th style="text-align: center">Remisiones</th>
                                            <th style="text-align: center">Fecha</th>
                                            <th style="text-align: center">Hora</th>
                                            <th style="text-align: center">Clave del Cliente</th>
                                            <th>Nombre del Cliente</th>
                                            <th>Vendedor</th>
                                          <%--  <th>Proyecto</th>
                                            <th>Estatus Orden</th>--%>
                                            <th>Forma de Pago</th>
                                            <th style="text-align: center">Estado</th>
                                            <th style="text-align: center">Cant. Entregada</th>
                                            <th style="text-align: center" id="montoF">Monto</th>
                                            <th style="text-align: center" id="PagadoF">Saldo</th>
                                        </tr>
                                    </tfoot>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td style="text-align: center">
                                        <%--<asp:LinkButton ID="btnFacturar" runat="server" CommandArgument='<%# Eval("id")+ "ˇ" + Eval("idCliente") + "ˇ" + Eval("id_factura") %>' CommandName="facturar" OnCommand="btnFacturar_ItemCommand"><span class="icon-documents"></span></asp:LinkButton>--%>
                                   <%-- </td>
                                    <td style="text-align: center">--%>
                                        <%# mostrarFactura(int.Parse(Eval("id").ToString()), int.Parse(Eval("idCliente").ToString()), Eval("id_factura").ToString()) %>
                                        <%--<asp:LinkButton ID="btnVistaPrevia" runat="server" CommandArgument='<%# Eval("id_factura")+ "ˇ" + Eval("id") + "ˇ" + Eval("idCliente")%>' CommandName="vistaprevia" OnCommand="btnVistaPrevia_ItemCommand">Vista Previa</asp:LinkButton>--%>
                                    </td>
                                    <td style="text-align: center">
                                        <asp:LinkButton ID="btnPago" runat="server" CommandArgument='<%# Eval("id")+ "ˇ" + Eval("vendedor") + "ˇ" + estatusOrd(float.Parse(Eval("cTotal").ToString()), float.Parse(Eval("cantidadEntregada").ToString()), bool.Parse(Eval("eliminadod").ToString())) %>' CommandName="pagos" OnCommand="btnPago_ItemCommand"><i class="icon-coin-dollar"></i></asp:LinkButton>
                                    </td>
                                    <td style="text-align: center">
                                        <%# obtenerFoliosPagos(int.Parse(Eval("id").ToString()))%>
                                    </td>
                                    <td style="text-align: center">
                                        <asp:LinkButton ID="lbtnOrdenEntrega" runat="server" CommandArgument='<%# Eval("idO") + "ˇ" + Eval("id") + "ˇ" + Eval("idDS") %>' CommandName="ordenEntrega" OnCommand="lbtnOrdenEntrega_Command"><i class="icon-pencil2"></i></asp:LinkButton>
                                    </td>
                                    <td style="text-align: center"><%# Eval("folio") %></td>
                                    <td style="text-align: center"><%# Eval("folioOrden") %></td>
                                    <td style="text-algin: center"><%# obtenerFoliosRemisiones(int.Parse(Eval("idO").ToString())) %></td>
                                    <td style="text-align: center"><%# Eval("fecha").ToString().Substring(0, 10) %></td>
                                    <td style="text-align: center"><%# Eval("hora") %></td>
                                    <td style="text-align: center"><%# Eval("clave") %></td>
                                    <td><%# Eval("nombreCliente") %></td>
                                    <td><%# Eval("vendedor") %></td>
                                    <%--<td><%# Eval("proyecto") %></td>
                                    <td><%# estatusOrd(float.Parse(Eval("cTotal").ToString()), float.Parse(Eval("cantidadEntregada").ToString()), bool.Parse(Eval("eliminadod").ToString())) %></td>--%>
                                    <td><%# Eval("fpago") %></td>
                                    <td style="text-align: center"><%# Eval("estado") %></td>
                                    <td style="text-align: center"><%# Eval("cantidadEntregada") %> de <%# Eval("cTotal") %></td>
                                    <td style="text-align: right">$<%# montoSolicitud(int.Parse(Eval("id").ToString()), Eval("estatusPago").ToString(), float.Parse(Eval("cTotal").ToString()), float.Parse(Eval("cantidadEntregada").ToString()), bool.Parse(Eval("eliminadod").ToString())) %></td>
                                    <td style="text-align: right">$<%# saldo(int.Parse(Eval("id").ToString())) %></td>
                                    <%--<td style="text-align: center"><%# pagadoSolicitud(Eval("id")) %></td>--%>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                    <thead>
                                        <tr>
                                            <th style="text-align: center" id="Facturar">Facturar</th>
                                            <th style="text-align: center" id="eliminarS">Pagos</th>
                                            <th style="text-align: center" id="ordenEntreg">Actualizar Orden</th>
                                            <th style="text-align: center" id="folio">Folio Solicitud</th>
                                            <th style="text-align: center" id="orden">Orden</th>
                                            <th style="text-align: center" id="orden">Remisiones</th>
                                            <th style="text-align: center" id="fecha">Fecha</th>
                                            <th style="text-align: center" id="hora">Hora</th>
                                            <th style="text-align: center" id="claveCliente">Clave del Cliente</th>
                                            <th id="nombreCliente">Nombre del Cliente</th>
                                            <th id="vendedor">Vendedor</th>
                                            <%--<th id="proyecto">Proyecto</th>
                                            <th id="estOrd">Estatus Orden</th>--%>
                                            <th id="fpago">Forma de Pago</th>
                                            <th style="text-align: center" id="estadoE">Estado</th>
                                            <th style="text-align: center" id="cant">Cant. Entregada</th>
                                            <th style="text-align: center" id="montoE">Monto</th>
                                            <th style="text-align: center" id="PagadoE">Saldo</th>
                                        </tr>
                                    </thead>
                                    <tr>
                                        <td colspan="16" class="text-center">
                                            <label class="label label-danger">¡No hay Solicitudes Registrados!</label></td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                        </asp:ListView>

                       
											
							<br />			

                        <div class="text-right">
                            
                            <table style="width: 80%">
                                <thead>
                                    <tr>
                                        <td> <asp:Label runat="server" ID="lblTotalCobrar"></asp:Label></td>
                                        <td> <asp:Label runat="server" ID="lblCobrado"></asp:Label></td>
                                        <td> <asp:Label runat="server" ID="lblSaldo"></asp:Label></td>
                                    </tr>
                                </thead>
                            </table>
                            
                        </div>
                    </div>
                </div>
            </div>


        </div>
        <asp:HiddenField ID="hfId" runat="server" Value="0" />
    </div>
</asp:Content>
