<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="productosFactorMod.aspx.cs" Inherits="despacho.productosFactorMod" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    &nbsp;<!-- Top Bar Starts --><asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
    <div class="top-bar clearfix">
        <div class="row gutter">
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <div class="page-title">
                    <h3>Modificar Factor</h3>
                    <p>/ <a href="productosFactor.aspx">Factores</a></p>
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
                                                <div class="row gutter">
                                                    <div class="col-md-8">
                                                        <asp:Label ID="Label1" runat="server" class="control-label" Text="Tipo de Producto:"></asp:Label> 
                                                        <asp:Label ID="lblTP" runat="server" class="form-control" Text=""></asp:Label>                                                       
                                                    </div>
                                                </div>
                                                <div class="row gutter">
                                                    <div class="col-md-8">
                                                        <asp:Label ID="Label3" runat="server" class="control-label" Text="Factor:"></asp:Label> 
                                                        <asp:TextBox ID="txtFactor" runat="server" class="form-control" name="factor"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row gutter">
                                                    <div class="col-md-8">
                                                        <asp:Label ID="Label4" runat="server" class="control-label" Text="Porcentaje:"></asp:Label> 
                                                        <asp:TextBox ID="txtPorcentaje" runat="server" class="form-control" placeholder="0.10 Representa 10% Descuento y 1.10 Representa 10% de Aumento" onkeypress="return onlyDotsAndNumbers(event)" MaxLength="4"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row gutter">
                                                    <div class="col-md-8">
                                                        <asp:Button ID="btnAgregar" runat="server" OnClick="btnAgregar_Click" Text="Modificar" class="btn btn-info" />
                                                        &nbsp;<asp:Button ID="btnRegresar" runat="server" class="btn btn-info" PostBackUrl="~/productos.aspx" Text="Volver" OnClick="btnRegresar_Click" />
                                                    </div>
                                                </div>
                                                <div class="row gutter">
                                                    <div class="col-md-8">
                                                        <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <asp:HiddenField ID="hfIdTP" runat="server" />
                                            <asp:HiddenField ID="hfIdS" runat="server" />
                                        </asp:Panel>
                                    </div>
                                </div>
                            </form>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!-- Row Ends -->
</asp:Content>
