<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="cvendedores.aspx.cs" Inherits="despacho.cvendedores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
     <!-- Top Bar Starts -->
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
    <div class="top-bar clearfix">
        <div class="row gutter">
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <div class="page-title">
                    <h3>Cobranza Vendedores</h3>
                    <p>/ <a href="cvendedores.aspx">Vendedores</a></p>
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
                                <h4>Solicitudes</h4>
                            </div>
                            <div class="panel-body">
                                <div class="table-responsive">
                                    <asp:ListView ID="lvSolicitudes" runat="server">
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
                                                        
                                                        <th style="text-align: center" id="cotizacion">Pagos</th>
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
                                                        
                                                        <th style="text-align: center">Pagos</th>
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
                                                    <asp:LinkButton ID="btnPago" runat="server" CommandArgument='<%# Eval("id")+ ";" + Eval("vendedor") %>' CommandName="pagos" OnCommand="btnPago_ItemCommand"><i class="icon-coin-dollar"></i></asp:LinkButton>
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
                                                        <th style="text-align: center">Facturar</th>
                                                        <th style="text-align: center" id="eliminarS">Pagos</th>
                                                    </tr>
                                                </thead>
                                                <tr>
                                                    <td colspan="14" class="text-center">
                                                        <label class="label label-danger">¡No hay Solicitudes Registrados!</label></td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                    </asp:ListView>
                                </div>
                            </div>
                        </div>
                       

                    </div>
                    <asp:HiddenField ID="hfId" runat="server" Value="0" />
                </div>
</asp:Content>
