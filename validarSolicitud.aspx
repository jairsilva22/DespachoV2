<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="validarSolicitud.aspx.cs" Inherits="despacho.validarSolicitud" EnableEventValidation="false" %>

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
                    <h3>Aprobación de la Solicitud</h3>
                    <p>/ <a href="solicitudes.aspx">Solicitudes</a></p>
                </div>
            </div>
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
                                    <asp:Panel ID="Panel1" runat="server">
                                        <div class="form-group">
                                            <table class="nav-justified">
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <div class="row gutter">
                                                            <div class="col-md-8">
                                                                Cliente:
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td colspan="2">
                                                        <asp:Label ID="lblClienteNombre" runat="server" class="form-control"></asp:Label>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <div class="row gutter">
                                                            <div class="col-md-8">
                                                                Folio:
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="row gutter">
                                                            <div class="col-md-8">
                                                                Fecha:
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <asp:Label ID="lblFolio" runat="server" class="form-control"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtFecha" runat="server" class="form-control"></asp:Label>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <div class="row gutter">
                                                            <div class="col-md-8">
                                                                Hora:
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <%--<asp:TextBox ID="txtFecha" runat="server" class="form-control"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="txtFecha_CalendarExtender" runat="server" BehaviorID="txtFecha_CalendarExtender" FirstDayOfWeek="Monday" Format="yyyy/MM/dd hh:mm" PopupPosition="BottomRight" TargetControlID="txtFecha" />--%>
                                                    </td>
                                                    <td style="width: 50%;">
                                                        <asp:Label ID="lblHora" runat="server" class="form-control"></asp:Label>
                                                        &nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td colspan="2">
                                                        <asp:ListView ID="lvDetalles" runat="server">
                                                            <LayoutTemplate>
                                                                <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                                                    <thead>
                                                                        <tr>
                                                                            <th id="codigo" style="text-align: center">Código</th>
                                                                            <th id="descripción">Descripción</th>
                                                                            <th id="cantOrdenada" style="text-align: center">Cantidad Ordenada</th>
                                                                            <th id="Revenimiento" style="text-align: center">Revenimiento</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tr id="itemPlaceholder" runat="server">
                                                                    </tr>
                                                                    <tfoot>
                                                                        <tr>
                                                                            <th style="text-align: center">Código</th>
                                                                            <th>Descripción</th>
                                                                            <th style="text-align: center">Cantidad Ordenada</th>
                                                                            <th style="text-align: center">Revenimiento</th>
                                                                        </tr>
                                                                    </tfoot>
                                                                </table>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <%--<td style="text-align: center"><%# Eval("id") %></td>--%>
                                                                    <td style="text-align: center"><%# Eval("codigo") %></td>
                                                                    <td><%# Eval("descripcion") %></td>
                                                                    <td style="text-align: center"><%# Eval("cantidad") + " " + Eval("unidad") %></td>
                                                                    <td style="text-align: center"><%# Eval("revenimiento") %></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <EmptyDataTemplate>
                                                                <table id="responsiveTable0" class="table table-striped table-bordered no-margin">
                                                                    <thead>
                                                                        <tr>
                                                                            <th style="text-align: center">Código</th>
                                                                            <th>Descripción</th>
                                                                            <th style="text-align: center">Cantidad Ordenada</th>
                                                                            <th style="text-align: center">Revenimiento</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tr>
                                                                        <td colspan="3">
                                                                            <label class="label label-danger">
                                                                                ¡No hay Productos Registrados!</label></td>
                                                                    </tr>
                                                                </table>
                                                            </EmptyDataTemplate>
                                                        </asp:ListView>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <div class="row gutter">
                                                            <div class="col-md-8">
                                                                Observaciones en Remisión:
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td></td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td colspan="2">
                                                        <asp:TextBox ID="txtComentarios" runat="server" TextMode="MultiLine" Width="100%" class="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <div class="row gutter">
                                                            <div class="col-md-8">
                                                                Comentarios sobre la Ubicación:
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td colspan="2">
                                                        <asp:TextBox ID="txtComentariosUbicacion" runat="server" class="form-control" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <div class="row gutter">
                                                            <div class="col-md-8">
                                                                Aprobado Por:
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlV2" runat="server" class="form-control">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style1">&nbsp;</td>
                                                    <td class="auto-style1">
                                                        <asp:Button ID="btnAgregar" runat="server" class="btn btn-info" OnClick="btnAgregar_Click" Text="Pasar de Solicitud a Orden" />
                                                        &nbsp; </td>
                                                    <td class="auto-style1"></td>
                                                    <td class="auto-style1"></td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div class="form-group">
                                            <div class="row gutter">
                                                <div class="col-md-8">
                                                    &nbsp;
                                                </div>
                                            </div>
                                            <div class="row gutter">
                                                <div class="col-md-4 selectContainer">
                                                    <asp:HiddenField ID="hfIdSolicitud" runat="server" />
                                                    <asp:HiddenField ID="hfIdV2" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <br />
                                    </asp:Panel>
                                </div>
                            </form>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlV2" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!-- Row Ends -->
</asp:Content>
