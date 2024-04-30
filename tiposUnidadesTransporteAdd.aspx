<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tiposUnidadesTransporteAdd.aspx.cs" Inherits="despacho.tiposUnidadesTransporteAdd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    &nbsp;<!-- Top Bar Starts --><asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
    <div class="top-bar clearfix">
        <div class="row gutter">
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <div class="page-title">
                    <h3>Nuevo tipo de Unidad</h3>
                    <p>/ <a href="tiposUnidadesTransporte.aspx">Tipode de Unidades</a></p>
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
                                                        <label class="control-label">Tipo:</label>
                                                        <asp:TextBox ID="txtTipo" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row gutter">
                                                    <div class="col-md-4 selectContainer">
                                                        <label class="control-label">Capacidad:</label>
                                                        <asp:TextBox ID="txtCapacidad" runat="server" class="form-control" onkeypress="return onlyDotsAndNumbers(event)"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 selectContainer">
                                                        <label class="control-label">Unidad de Medida:</label>
                                                        <asp:DropDownList ID="ddlUDM" runat="server" class="form-control" DataTextField="unidad" DataValueField="id"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row gutter">
                                                    <div class="col-md-4 selectContainer">
                                                        <label class="control-label">Tipo de Producto:</label>
                                                        <asp:DropDownList ID="ddlTP" runat="server" class="form-control" DataTextField="tipo" DataValueField="id"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-4 selectContainer">
                                                        <label class="control-label"></label>
                                                        <br />
                                                        <asp:CheckBox ID="chbxCarga" runat="server" class="control-label" Text="¿Se utiliza para carga de diversos productos a la vez?" />
                                                    </div>
                                                </div>
                                                <div class="row gutter">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblPeso" runat="server" Text="Capacidad de carga en KG:"></asp:Label>
                                                        <asp:TextBox ID="txtPeso" runat="server" class="form-control" name="carga" onkeypress="return onlyDotsAndNumbers(event)"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row gutter">
                                                    <div class="col-md-4 selectContainer">
                                                        <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" class="btn btn-info" OnClick="btnAgregar_Click" />

                                            <asp:Button ID="btnCancelar" runat="server" Text="Volver" class="btn btn-info" OnClick="btnCancelar_Click" />
                                        </asp:Panel>
                                    </div>
                                </div>
                            </form>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnAgregar" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!-- Row Ends -->
</asp:Content>
