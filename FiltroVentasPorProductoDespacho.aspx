<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FiltroVentasPorProductoDespacho.aspx.cs" Inherits="despacho.FiltroVentasPorProductoDespacho" %>
<asp:Content runat="server" ContentPlaceHolderID="scripts">
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder">
    <div class="top-bar clearfix">
        <div class="row gutter">
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <div class="page-title">
                    <h3>Filtro de Ventas por Productos</h3>
                    <p>/ <a href="home.aspx">Inicio</a></p>
                </div>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
            </div>
        </div>
    </div>
    <div class="row gutter">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-blue">
                <p>&nbsp;</p>

                <p style="text-align: center; margin-bottom: 30px">Seleccione las fechas para generar el reporte</p>
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
                        </div>
                        <div class="col-md-2 selectContainer">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
