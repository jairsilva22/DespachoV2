<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reportes.aspx.cs" Inherits="despacho.Reportes" %>

<asp:Content runat="server" ContentPlaceHolderID="scripts"></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder">

    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
    <div class="top-bar clearfix">
        <div class="row gutter">
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <div class="page-title">
                    <h3>Reportes</h3>
                    <p>/ <a href="home.aspx">Inicio</a></p>
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
                <div class="panel-body">
                    <asp:ListView runat="server" ID="lvMenu">
                        <LayoutTemplate>
                            <div class="col-md-6" id="itemPlaceholder" runat="server">
                            </div>

                        </LayoutTemplate>
                        <EmptyDataTemplate>
                            <div class="col-md-12">
                                <p>Sin registros</p>
                            </div>
                        </EmptyDataTemplate>
                        <ItemTemplate>
                            <div class="col-md-6">
                                <div class="row panel" style="margin: 5px;">
                                    <h3 class="panel-heading" style="text-align: center; color:#4286F7"><strong><%# Eval("tipo").ToString() %></strong></h3>
                                    <div class="panel-panel-body">
                                        <%# buscarMenusTipo(Eval("tipo").ToString()) %>
                                                                                 
                                    </div>

                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:ListView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
