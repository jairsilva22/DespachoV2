<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="filtroEstatusTicket.aspx.cs" Inherits="despacho.filtroEstatusTicket" %>
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
                    <h3>Filtro de Estatus de Ticket</h3>
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
                <p>&nbsp;</p>

                <p style="text-align: center; margin-bottom: 30px">Seleccione las fechas para generar el reporte</p>
                <asp:Panel ID="Panel1" runat="server">
                    <div class="form-group">
                        <div class="row gutter" style="margin-bottom: 15px">
                            <div class="col-md-2 selectContainer">
                            </div>
                            <div class="col-md-4 selectContainer text-center">
                                <label style="display: inline" class="control-label">Fecha Inicio:</label>
                                <asp:TextBox ID="txtFechaInicio" runat="server" name="fecha" TextMode="DateTime" autocomplete="off"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="txtFechaInicio_CalendarExtender" runat="server" BehaviorID="txtFechaInicio_CalendarExtender" TargetControlID="txtFechaInicio" FirstDayOfWeek="Monday" Format="yyyy/MM/dd" PopupPosition="BottomRight" />
                            </div>
                            <div class="col-md-4 selectContainer text-center">
                                <label style="display: inline" class="control-label">Fecha Fin:</label>
                                <asp:TextBox ID="txtFechaFin" runat="server" name="fecha" TextMode="DateTime" autocomplete="off"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="txtFechaFin_CalendarExtender" runat="server" BehaviorID="txtFechaFin_CalendarExtender" TargetControlID="txtFechaFin" FirstDayOfWeek="Monday" Format="yyyy/MM/dd" PopupPosition="BottomRight" />
                            </div>
                            <div class="col-md-2 selectContainer">
                            </div>
                        </div>
                        <div class="row gutter" style="margin-bottom: 15px">
                            <div class="col-md-2 selectContainer">
                            </div>
                            <div class="col-md-4 selectContainer text-center">
                                <label style="display: inline" class="control-label">Cliente: </label>
                                <asp:DropDownList ID="ddlClienteI" runat="server" Style="width: 75%"></asp:DropDownList>
                            </div>
                            <div class="col-md-4 selectContainer">
                                <label style="display: inline" class="control-label">Hasta: </label>
                                <asp:DropDownList ID="ddlClienteF" runat="server" Style="width: 75%"></asp:DropDownList>
                            </div>
                            <div class="col-md-2 selectContainer">
                            </div>
                        </div>
                        <div class="row gutter" style="margin-bottom: 15px">
                            <div class="col-md-2 selectContainer">
                            </div>
                            <div class="col-md-4 selectContainer text-center">
                                <label style="display: inline" class="control-label">Orden: </label>
                                <asp:DropDownList ID="ddlOrdenI" runat="server" Style="width: 75%"></asp:DropDownList>
                            </div>
                            <div class="col-md-4 selectContainer">
                                <label style="display: inline" class="control-label">Hasta: </label>
                                <asp:DropDownList ID="ddlOrdenF" runat="server" Style="width: 75%"></asp:DropDownList>
                            </div>
                            <div class="col-md-2 selectContainer">
                            </div>
                        </div>
                        <%--<div class="row gutter" style="margin-bottom: 15px">
                            <div class="col-md-2 selectContainer">
                            </div>
                            <div class="col-md-4 selectContainer text-center">
                                <label style="display: inline" class="control-label">Ordenado por: </label>
                                <asp:DropDownList ID="ddlOrdenado" runat="server" Style="width: 65%"></asp:DropDownList>
                            </div>
                            <div class="col-md-4 selectContainer">
                            </div>
                            <div class="col-md-2 selectContainer">
                            </div>
                        </div>--%>
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
                            <div class="col-md-2 selectContainer">
                            </div>
                            <div class="col-md-4 selectContainer text-center">
                                <asp:Button ID="btnEnviar" runat="server" OnClick="btnEnviar_Click" Text="Enviar" />
                            </div>
                            <div class="col-md-4 selectContainer text-center">
                                <asp:Button ID="btnExcel" runat="server" Text="Excel" OnClick="btnExcel_Click" />
                            </div>
                            <div class="col-md-2 selectContainer">
                            </div>
                        </div>

                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>

