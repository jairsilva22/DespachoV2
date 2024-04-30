<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="filtroCobranza.aspx.cs" Inherits="despacho.filtroCobranza" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="scripts" runat="Server">
    <style>
        .selectColor {
            background-color: white;
            color: black;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">

    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
    <div class="top-bar clearfix">
        <div class="row gutter">
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <div class="page-title">
                    <h3>Filtro de Reporte de Cobranza</h3>
                    <p>/ <a href="cfinanzas.aspx">Cobranza</a></p>
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
                <p>&nbsp;</p>

                <p style="text-align: center; margin-bottom: 30px">Filtro de Reporte de Cobranza</p>
                <asp:Panel ID="Panel1" runat="server">
                    <div class="form-group">
                        <div class="row gutter" style="margin-bottom: 15px">
                            <div class="col-md-4 selectContainer">
                            </div>
                            <div class="col-md-4 selectContainer text-center ">
                                <label style="display: inline" class="control-label">Vendedor:</label>
                                <asp:DropDownList ID="ddlVendedores" runat="server"></asp:DropDownList>
                            </div>
                            <div class="col-md-4 selectContainer">
                            </div>
                        </div>
                        <div class="row gutter" style="margin-bottom: 15px">
                            <div class="col-md-4 selectContainer">
                            </div>
                            <div class="col-md-4 selectContainer text-center ">
                                <%--<label style="display: inline" class="control-label">Tipo Orden:</label>--%>
                                <asp:RadioButtonList ID="rbtnFacturada" runat="server" style="display: inline">
                                    <asp:ListItem Value="0" Selected="True">Todos</asp:ListItem>
                                    <asp:ListItem Value="1">Facturado</asp:ListItem>
                                    <asp:ListItem Value="2">Sin Facturar</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <div class="col-md-4 selectContainer">
                            </div>
                        </div>
                        <div class="row gutter" style="margin-bottom: 15px">
                            <div class="col-md-4 selectContainer">
                            </div>
                            <div class="col-md-4 selectContainer text-center">
                                <asp:Label ID="lblError" runat="server" Font-Size="Large" ForeColor="Red"></asp:Label>
                            </div>
                            <div class="col-md-4 selectContainer">
                            </div>
                        </div>
                        <div class="row gutter">
                            <div class="col-md-3 selectContainer">
                            </div>
                            <div class="col-md-3 selectContainer text-right">
                                <asp:Button ID="btnEnviar" runat="server" OnClick="btnEnviar_Click" Text="Enviar" class="btn btn-info" />
                            </div>
                            <div class="col-md-3 selectContainer">
                                <asp:Button ID="btnEnviarExcel" runat="server" OnClick="btnEnviarExcel_Click" Text="Excel" class="btn btn-success" />
                            </div>
                            <div class="col-md-3 selectContainer">
                            </div>
                        </div>

                        <%--</form>--%>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
