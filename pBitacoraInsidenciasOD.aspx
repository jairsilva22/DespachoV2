<%@ Page Title="" Language="C#" MasterPageFile="~/PopUP.Master" AutoEventWireup="true" CodeBehind="pBitacoraInsidenciasOD.aspx.cs" Inherits="despacho.pBitacoraInsidenciasOD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
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
                                    <table style="width:100%">
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td colspan="2" style="width:100%">
                                                <div class="row gutter">
                                                    <asp:Panel ID="Panel1" runat="server">
                                                        <div class="form-group">
                                                            <div class="row gutter">
                                                                <div class="col-md-12">
                                                                    <asp:Panel ID="Panel2" runat="server">
                                                                        <asp:Label ID="lblOD" runat="server" Visible="false"></asp:Label>
                                                                        <br />
                                                                        <div class="panel-body">
                                                                            <div class="table-responsive">
                                                                                <asp:Label ID="lblIDRemision" runat="server" Text="" Visible="false"></asp:Label>

                                                                                <div class="row gutter">
                                                                                    <div class="col-md-3">
                                                                                        <%--<label class="control-label">ID:</label>
                                                                                        <asp:Label ID="lblID" runat="server" class="control-label" Text=""></asp:Label>--%>
                                                                                    </div>
                                                                                    <div class="col-md-3">
                                                                                        <label class="control-label">Acción:</label>
                                                                                        <asp:TextBox ID="txtAccion" runat="server" class="form-control"></asp:TextBox>
                                                                                    </div>
                                                                                    <div class="col-md-3">
                                                                                        <label class="control-label">Motivo:</label>
                                                                                        <asp:TextBox ID="txtMotivo" runat="server" class="form-control"></asp:TextBox>
                                                                                    </div>
                                                                                    <div class="col-md-3" style="align-content:center">
                                                                                        <br />
                                                                                        <asp:Button ID="btnAgregar" runat="server" Text="Agregar" class="btn btn-info" OnClick="btnAgregar_Click"/>
                                                                                    </div>
                                                                                </div>
                                                                                <br />
                                                                                <br />
                                                                                <asp:ListView ID="lvUT" runat="server" ItemPlaceholderID="itemPlaceHolderTU">
                                                                                    <LayoutTemplate>
                                                                                        <table id="OrderTable" class="table no-margin">
                                                                                            <thead>
                                                                                                <tr>
                                                                                                    <%--<th style="text-align: center">ID</th>--%>
                                                                                                    <th style="text-align: center">Acción</th>
                                                                                                    <th style="text-align: center">Motivo</th>
                                                                                                    <th style="text-align: center">Usuario</th>
                                                                                                    <th style="text-align: center">Fecha y hora</th>
                                                                                                    <%--<th style="text-align: center">Registrar</th>--%>
                                                                                                </tr>
                                                                                            </thead>
                                                                                            <tbody>
                                                                                                <asp:PlaceHolder ID="itemPlaceHolderTU" runat="server"></asp:PlaceHolder>
                                                                                            </tbody>
                                                                                        </table>
                                                                                        </div>
                                                                                    </LayoutTemplate>
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <%--<td style="text-align: center">
                                                                                                <asp:Label ID="lblIDUnidad" runat="server" Text='<%# Eval("id") %>'></asp:Label>
                                                                                            </td>--%>
                                                                                            <td style="text-align: center">
                                                                                                <asp:LinkButton ID="lbtnUnidadT" runat="server" Text='<%# Eval("accion") %>'></asp:LinkButton>
                                                                                            </td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label ID="lblTipoUnidad" runat="server" Text='<%# Eval("motivo") %>'></asp:Label>
                                                                                            </td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("usuario") %>'></asp:Label>
                                                                                            </td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("fecha") %>'></asp:Label>
                                                                                            </td>
                                                                                            <%--<td style="text-align: center">
                                                                                                <asp:LinkButton ID="lBtnAsignar" runat="server" CommandName="xAssignUnit" CommandArgument='<%# Eval("id") %>' OnCommand="lBtnAsignar_Command">Modificar</asp:LinkButton>
                                                                                            </td>--%>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:ListView>
                                                                                <br />
                                                                                <asp:Label ID="lblMensaje" runat="server" Text="" ForeColor="Red" Font-Size="Large" Font-Bold="True"></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                        <asp:HiddenField ID="hfIdBitacora" runat="server" />
                                                                    </asp:Panel>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                </div>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                    </table>
                                </div>
                            </form>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnAgregar" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!-- Row Ends -->
</asp:Content>
