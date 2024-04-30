<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="frameProyectos.aspx.cs" Inherits="despacho.frameProyectos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts" runat="server">
    <script>
        function AlertaConfirmacion() {

            "use strict";
            alert("Regsitro Exitoso, para continuar de click en el boton cerrar");
        }
    </script>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
    &nbsp;<%--<!-- Top Bar Starts -->
    <div class="top-bar clearfix">
        <div class="row gutter">
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <div class="page-title">
                    <h3>Proyectos</h3>
                    <p>/ <a href="clientes.aspx">Clientes</a></p>
                </div>
            </div>
        </div>
    </div>--%>
    <!-- Top Bar Ends -->

    <!-- Row Starts -->
    <div class="row">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="panel">
                <div class="panel-heading">
                    <h4></h4>
                </div>
              <%--  <asp:UpdatePanel ID="upForm" runat="server">
                    <ContentTemplate>--%>
                        <div class="panel-body">
                           
                                <div class="form-group">
                                    <div class="row gutter">
                                        <div class="col-md-4" style="display:none;">
                                            Cliente:
                                            <asp:Label ID="lblCliente" runat="server" class="form-control" name="cliente"></asp:Label>
                                        </div>
                                        <div class="col-md-8">
                                        </div>
                                    </div>
                                    <div class="row gutter">
                                        <div class="col-md-4">
                                            Proyecto:
                                            <asp:TextBox ID="txtProyecto" runat="server" class="form-control" name="proyecto" autocomplete="off" ></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            Código Postal:
                                            <asp:TextBox ID="txtCP" runat="server" AutoPostBack="True" class="form-control" MaxLength="5" OnTextChanged="txtCP_TextChanged" onkeypress="return onlyNumbers(event)" AutoCompleteType="None" autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row gutter">
                                        <div class="col-md-4">
                                            Estado:
                                            <asp:DropDownList ID="ddlEstados" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlEstados_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-4">
                                            Ciudad:
                                            <asp:DropDownList ID="ddlCiudades" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlCiudades_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-"4">
                                            Colonia:<br />
                                            <ajaxToolkit:ComboBox ID="cbxColonias" runat="server" AutoCompleteMode="SuggestAppend" AutoPostBack="True" DropDownStyle="Simple" name="colonia" DataTextField="asenta" DataValueField="asenta" OnSelectedIndexChanged="cbxColonias_SelectedIndexChanged" Width="400px">
                                            </ajaxToolkit:ComboBox>
                                        </div>
                                    </div>
                                    <div class="row gutter">
                                        <div class="col-md-4">
                                            Calle:
                                            <asp:TextBox ID="txtCalle" runat="server" class="form-control" name="calle" autocomplete="off"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            Número:
                                            <asp:TextBox ID="txtNumero" runat="server" class="form-control" name="numero" TextMode="Number" autocomplete="off"></asp:TextBox>
                                        </div>
                                        <div class="col-md-"4">
                                            Interior:
                                            <asp:TextBox ID="txtInterior" runat="server" class="form-control" name="numero" TextMode="Number" MaxLength="5" Width="20%" autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row gutter">
                                        <div class="col-md-12">
                                            <br />
                                            <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row gutter">
                                        <div class="col-md-12">
                                            <br />
                                            <asp:Button ID="btnGuardar" runat="server" OnClick="btnGuardar_Click" Text="Guardar" class="btn btn-info"/>
                                            <%--<asp:Button ID="btnVolver" runat="server" class="btn btn-info" PostBackUrl="~/clientes.aspx" Text="Volver" />--%>
                                        </div>
                                    </div>
                                   
                                    <div>
                                         <asp:HiddenField ID="hfIdProy" runat="server" />
                                                                    <asp:HiddenField ID="hfIdEstado" runat="server" />
                                                                    <asp:HiddenField ID="hfIdCiudad" runat="server" />
                                                                    <asp:HiddenField ID="hfIdEstadoCP" runat="server" />
                                                                    <asp:HiddenField ID="hfIdCiudadCP" runat="server" />
                                                                    <asp:HiddenField ID="hfSearchBy" runat="server" />
                                                                    <asp:HiddenField ID="hfIdCliente" runat="server" />
                                    </div>
                           
                                </div>
                            <%--</form>--%>
                        </div>
                        <!-- Button trigger modal -->
                        <button type="button" style="display: none;" id="btnShowPopup" class="btn btn-primary btn-lg"
                            data-toggle="modal" data-target="#myModal">
                            Launch demo modal
                        </button>

                        <!-- Modal -->
                        <div class="modal fade" id="myModal">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span></button>
                                        <h4 class="modal-title">
                                            <asp:Label ID="mlblTitle" runat="server" />
                                        </h4>
                                    </div>
                                    <div class="modal-body">
                                        <asp:Label ID="mlblMessage" runat="server" />
                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button ID="mbtnClose" runat="server" Text="Cerrar" class="btn btn-default" OnClick="mbtnClose_Click" />
                                        <button type="button" style="display: none;" id="btnClosePopup" class="btn btn-default" data-dismiss="modal">
                                            Close</button>
                                        <asp:Button ID="mbtnAceptar" runat="server" Text="Aceptar" class="btn btn-info" OnClick="mbtnAceptar_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- /.modal -->
                        <%--</form>--%>
                        </div>
                   <%-- </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="txtCP" EventName="TextChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlEstados" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlCiudades" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="cbxColonias" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
                        <%--<asp:AsyncPostBackTrigger ControlID="lvDetalles" EventName="ItemCommand" />
                        <asp:AsyncPostBackTrigger ControlID="lvDetalles" EventName="ItemDeleting" />
                        <asp:AsyncPostBackTrigger ControlID="lvDetalles" EventName="ItemUpdating" />--%>
                       <%-- <asp:AsyncPostBackTrigger ControlID="mbtnAceptar" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="mbtnClose" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>--%>
            </div>
        </div>
    
    <!-- Row Ends -->
</asp:Content>

