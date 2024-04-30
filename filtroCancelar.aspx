<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="filtroCancelar.aspx.cs" Inherits="despacho.filtroCancelar" %>

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
                    <h3>Reporte de Facturas Canceladas</h3>
                    <p>/ <a href="filtroCancelar.aspx">Reporte de Facturas Canceladas</a></p>
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

                <p style="text-align: center">Filtro de Documentos Cancelados</p>
                <div style="align-content: center">
                    <p>&nbsp;</p>
                    <table width="60%" border="0">
                        <tr>
                            <td align="right">Folio:</td>
                            <td>
                                <asp:TextBox ID="txtFolio" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">RFC:</td>
                            <td>
                                <asp:TextBox ID="txtRfc" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">Mes de emisión: </td>
                            <td>
                                <asp:DropDownList ID="ddlMes" runat="server" class="selectColor">
                                    <asp:ListItem Value="0" Text="Todos"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Enero"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Febrero"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Marzo"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="Abril"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="Mayo"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="Junio"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="Julio"></asp:ListItem>
                                    <asp:ListItem Value="8" Text="Agosto"></asp:ListItem>
                                    <asp:ListItem Value="9" Text="Septiembre"></asp:ListItem>
                                    <asp:ListItem Value="10" Text="Octubre"></asp:ListItem>
                                    <asp:ListItem Value="11" Text="Noviembre"></asp:ListItem>
                                    <asp:ListItem Value="12" Text="Diciembre"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">Año de emisión: </td>
                            <td>
                                <asp:DropDownList ID="ddlAño" runat="server" class="selectColor"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">Cliente:</td>
                            <td class="Estilo1" width="20%">
                                <asp:DropDownList ID="ddlCliente" runat="server" AutoPostBack="true" class="selectColor"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">Empresa:</td>
                            <td class="Estilo1" width="20%">
                                <asp:DropDownList ID="ddlSucursal" runat="server" class="selectColor"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">Estatus:</td>
                            <td>
                                <asp:DropDownList ID="ddlEstado" runat="server" class="selectColor">
                                    <asp:ListItem Value="" Text="Todos"></asp:ListItem>
                                    <asp:ListItem Value="Error" Text="Error"></asp:ListItem>
                                    <asp:ListItem Value="Cancelado" Text="Cancelado"></asp:ListItem>
                                    <asp:ListItem Value="Pendiente" Text="Pendiente"></asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td align="right">Tipo de documento:</td>
                            <td>
                                <asp:DropDownList ID="ddlTipo" runat="server" class="selectColor"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">Usuario:</td>
                            <td>
                                <asp:DropDownList ID="ddlUsuario" runat="server" class="selectColor"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr align="center" style="margin-top: 10px">
                            <td colspan="2" align="right">
                                <asp:Button ID="btnEnviar" runat="server" Text="Enviar" OnClick="reporteCancelados_Click" target="_blank" />
                            </td>
                        </tr>
                    </table>
                    <p>&nbsp;</p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
