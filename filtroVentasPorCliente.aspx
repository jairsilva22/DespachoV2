<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="filtroVentasPorCliente.aspx.cs" Inherits="despacho.filtroVentasPorCliente" MasterPageFile="~/Site.Master" %>



<asp:Content runat="server" ContentPlaceHolderID="scripts">
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder">
    <div class="top-bar clearfix">
        <div class="row gutter">
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <div class="page-title">
                    <h3>Filtro de Ventas por Cliente</h3>
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
    <div class="row gutter">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-blue">
                <p>&nbsp;</p>

                <p style="text-align: center; margin-bottom: 30px">Filtro de Reporte de Ventas Por cliente</p>
                <div class="form-group">
                    <div class="row gutter" style="margin-bottom: 15px">
                        <div class="col-md-2 selectContainer">
                        </div>
                        <div class="col-md-4 selectContainer text-center">
                            <label style="display: inline" class="control-label">Fecha Inicio:</label>
                            <asp:TextBox ID="txtFechaInicio" runat="server" name="fecha" TextMode="DateTime" AutoComplete="off"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="txtFechaInicio_CalendarExtender" runat="server" BehaviorID="txtFechaInicio_CalendarExtender" TargetControlID="txtFechaInicio" FirstDayOfWeek="Monday" Format="yyyy/MM/dd" PopupPosition="BottomRight" />
                        </div>
                        <div class="col-md-4 selectContainer text-center">
                            <label style="display: inline" class="control-label">Fecha Fin:</label>
                            <asp:TextBox ID="txtFechaFin" runat="server" name="fecha" TextMode="DateTime" AutoComplete="off"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="txtFechaFin_CalendarExtender" runat="server" BehaviorID="txtFechaFin_CalendarExtender" TargetControlID="txtFechaFin" FirstDayOfWeek="Monday" Format="yyyy/MM/dd" PopupPosition="BottomRight" />
                        </div>
                        <div class="col-md-2 selectContainer">
                        </div>
                    </div>
                    <%--<div class="row gutter" style="margin-bottom: 15px">
                        <div class="col-md-4 selectContainer">
                        </div>
                        <div class="col-md-4 selectContainer text-center">
                            <label style="display: inline" class="control-label">Cliente:</label>--%>
                           <%-- <ajaxToolkit:ComboBox ID="ddlClienteInicio" runat="server" AutoCompleteMode="SuggestAppend" DropDownStyle="Simple" Style="width: 75%" AutoPostBack="True"></ajaxToolkit:ComboBox>--%> 
                            <%--<asp:DropDownList ID="ddlCliente" runat="server"></asp:DropDownList>
                        </div>
                        <div class="col-md-4 selectContainer">
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
                            <%--<asp:Button ID="btnExcel" runat="server" Text="Excel" OnClick="btnExcel_Click" />--%>
                        </div>
                        <div class="col-md-2 selectContainer">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
