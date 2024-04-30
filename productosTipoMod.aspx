﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="productosTipoMod.aspx.cs" Inherits="despacho.productosTipoMod" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts" runat="server">
    
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    &nbsp;<!-- Top Bar Starts --><asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
    <div class="top-bar clearfix">
        <div class="row gutter">
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <div class="page-title">
                    <h3>Modificar Tipo de Producto</h3>
                    <p> / <a href="productosTipo.aspx">Tipos de Producto</a></p>
                </div>
            </div>
            <%--<asp:DropDownList ID="ddlTipoProducto" runat="server" class="form-control" DataSourceID="dsTipoProducto" DataTextField="tipo" DataValueField="id" ></asp:DropDownList>
                                                                                            <asp:SqlDataSource ID="dsTipoProducto" runat="server" ConnectionString="<%$ ConnectionStrings:cnx %>" SelectCommand="SELECT * FROM [tiposProductos]"></asp:SqlDataSource>--%>
        </div>
    </div>
    <!-- Top Bar Ends -->

    <!-- Row Starts -->
    <div class="row">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="panel">
                <div class="panel-heading">
                    <h4></h4>
                </div>
                <asp:UpdatePanel ID="upForm" runat="server">
                    <ContentTemplate>
                        <div class="panel-body">
                            <form id="defaultForm" method="post">
                                <div class="form-group">
                                    <div class="row gutter">
                                        <asp:Panel ID="Panel1" runat="server">
                                            <div class="form-group">
                                                <div class="form-group">
                                                    <div class="row gutter">
                                                        <div class="col-md-8">
                                                            Tipo de Producto:
                                                            <asp:TextBox ID="txtTipo" runat="server" class="form-control" name="clave"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <%--<div class="row gutter">
                                                        <div class="col-md-8">
                                                            Sucursal:     
                                                            <asp:Label ID="lblSucursal" runat="server" class="form-control" Text=""></asp:Label>
                                                        </div>
                                                    </div>--%>
                                                </div>
                                                <div class="row gutter">
                                                    <div class="col-md-8">
                                                        <asp:CheckBox ID="chbxRev" runat="server" Text="Solicitar revenimiento en la solicitud" />
                                                    </div>
                                                </div>
                                                <div class="row gutter">
                                                    <div class="col-md-8">
                                                        <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row gutter">
                                                    <div class="col-md-8">
                                                        <asp:Button ID="btnAgregar" runat="server" OnClick="btnAgregar_Click" Text="Modificar" class="btn btn-info" />
                                                        &nbsp;<asp:Button ID="btnCancelar" runat="server" class="btn btn-info" OnClick="btnCancelar_Click" Text="Volver" />
                                                    </div>
                                                </div>
                                                <asp:HiddenField ID="hfIdSucursal" runat="server" />
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </form>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="txtTipo" EventName="TextChanged" />
                        <asp:AsyncPostBackTrigger ControlID="btnAgregar" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!-- Row Ends -->
</asp:Content>
