<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="pagosSolicitudFAdd.aspx.cs" Inherits="despacho.pagosSolicitudFAdd" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts" runat="server">

    <style type="text/css">
        .auto-style1 {
            height: 36px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    &nbsp;<!-- Top Bar Starts --><asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
    <div class="top-bar clearfix">
        <div class="row gutter">
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <div class="page-title">
                    <h3>Nuevo Pago</h3>
                    <p>/ <a href="cfinanzas.aspx">Cobranza Finanzas </a>/ Pagos de Solicitud</p>
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
                <asp:UpdatePanel ID="upForm" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="panel-body">
                            <%--<form id="defaultForm" method="post">--%>
                                <div class="form-group">
                                    <div class="row gutter">
                                        <asp:Panel ID="Panel1" runat="server">
                                            <div class="form-group">
                                                <div class="row gutter">
                                                    <div class="col-md-4 selectContainer">
                                                    </div>
                                                    <div class="col-md-4 selectContainer text-center">
                                                        <label class="control-label">Folio:</label>
                                                        <asp:Label ID="lblFolio" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row gutter">
                                                    <div class="col-md-4 selectContainer">
                                                    </div>
                                                    <div class="col-md-4 selectContainer text-center">
                                                        <label class="control-label">Monto:</label>
                                                        <asp:TextBox ID="txtMonto" runat="server" class="form-control text-center" readonly="true" ValidationGroup="agregar"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 selectContainer">
                                                    </div>
                                                </div>
                                                <div class="row gutter">
                                                    <div class="col-md-4 selectContainer">
                                                    </div>
                                                    <div class="col-md-4 selectContainer text-center">
                                                        <label class="control-label">Saldo:</label>
                                                        <asp:TextBox ID="txtSaldo" runat="server" class="form-control text-center" ReadOnly="true" required="required" ValidationGroup="agregar"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 selectContainer">
                                                    </div>
                                                </div>
                                                <div class="row gutter">
                                                    <div class="col-md-4 selectContainer">
                                                    </div>
                                                    <div class="col-md-4 selectContainer text-center">
                                                        <label class="control-label">Pago:</label>
                                                        <asp:TextBox ID="txtPago" runat="server" class="form-control text-center" required="required" ValidationGroup="agregar"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 selectContainer">
                                                    </div>
                                                </div>
                                                <div class="row gutter">
                                                    <div class="col-md-4 selectContainer">
                                                    </div>
                                                    <div class="col-md-4 selectContainer text-center">
                                                        <label class="control-label">Forma de pago:</label>
                                                        <asp:DropDownList ID="ddlFP" runat="server" class="form-control" DataTextField="nombre" DataValueField="id" AutoPostBack="true" OnSelectedIndexChanged="ddlFP_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-4 selectContainer">
                                                    </div>
                                                </div>
                                                 <div class="row gutter" runat="server" id="pago">
                                                    <div class="col-md-4 selectContainer">
                                                    </div>
                                                    <div class="col-md-4 selectContainer text-center">
                                                        <label class="control-label">Banco de pago:</label>
                                                        <asp:DropDownList ID="ddlBancoP" runat="server" class="form-control" >
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-4 selectContainer">
                                                    </div>
                                                </div>
                                                 <div class="row gutter" runat="server" id="recibo">
                                                    <div class="col-md-4 selectContainer">
                                                    </div>
                                                    <div class="col-md-4 selectContainer text-center">
                                                        <label class="control-label">Banco de Recibo:</label>
                                                        <asp:DropDownList ID="ddlBancoR" runat="server" class="form-control" >
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-4 selectContainer">
                                                    </div>
                                                </div>
                                                <div class="row gutter">
                                                    <div class="col-md-4 selectContainer">
                                                    </div>
                                                    <div class="col-md-4 selectContainer text-center">
                                                        <label class="control-label">Método de pago:</label>
                                                        <asp:DropDownList ID="ddlMP" runat="server" class="form-control" DataTextField="nombre" DataValueField="id" >
                                                            </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-4 selectContainer">
                                                    </div>
                                                </div>
                                                 <div class="row gutter">
                                                    <div class="col-md-4 selectContainer">
                                                    </div>
                                                    <div class="col-md-4 selectContainer text-center">
                                                        <label class="control-label">Observaciones:</label>
                                                        <asp:TextBox runat="server" ID="txtObservaciones" CssClass="form-control" ValidationGroup="agregar" TextMode="Multiline" Rows="5" Columns="5"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 selectContainer">
                                                    </div>
                                                </div>
                                                <div class="row gutter">
                                                    <div class="col-md-4 selectContainer">
                                                        <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" class="btn btn-info" OnClick="btnAgregar_Click" ValidationGroup="agregar"/>
                                            <a href="pagosSolicitudF.aspx?idSolicitud=<%= Request.QueryString["idSolicitud"] %>&Vendedor=<%= Request.QueryString["Vendedor"] %>&estatus=<%= Request.QueryString["estatus"] %>&factura=<%= Request.QueryString["factura"] %>&fechaIF=<%= Request.QueryString["fechaIF"] %>&fechaFF=<%= Request.QueryString["fechaFF"] %>&ordenF=<%= Request.QueryString["ordenF"] %>&remF=<%= Request.QueryString["remF"] %>&cteF=<%= Request.QueryString["cteF"] %>&vendedorF=<%= Request.QueryString["vendedorF"] %>&estatusF=<%= Request.QueryString["estatusF"] %>" class="btn btn-info text-center">
                                                Volver
                                            </a>
                                           <%-- <asp:Button ID="btnCancelar" runat="server" Text="Volver" class="btn btn-info" OnClick="btnCancelar_Click" ValidationGroup="volver"/>--%>
                                        </asp:Panel>
                                    </div>
                                </div>
                            <%--</form>--%>
                        </div>

                 <!-- Modal -->
                        <div class="modal fade" id="myModalPDF">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span></button>
                                        <h4 class="modal-title">
                                            Error
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

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!-- Row Ends -->
</asp:Content>

