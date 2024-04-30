<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="empleadosPercepciones.aspx.cs" Inherits="despacho.empleadosPercepciones" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts" runat="server">
    <script>
        function mostrarCarga() {
            $('#myModalCMasiva').modal('show');
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
    &nbsp;<!-- Top Bar Starts -->
    <div class="top-bar clearfix">
        <div class="row gutter">
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <div class="page-title">
                    <h3>Percepciones</h3>
                    <p>/ <a href="empleados.aspx">Empleados</a></p>
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
                                <div class="row gutter">
                                    <div class="col-md-4">
                                        Seleciona Tipo de Nomina
                                    <asp:DropDownList ID="ddlTipoNomina" runat="server" AutoPostBack="True" class="form-control">
                                        <asp:ListItem Text="Seleccione una Nomina" Value="0" />
                                        <asp:ListItem Text="Quincenal" Value="3" />
                                        <asp:ListItem Text="Semanal" Value="2" />
                                    </asp:DropDownList>

                                    </div>
                                </div>
                                <div class="row gutter">
                                    <div class="col-md-4">
                                        Seleciona El Periodo de Nomina
                                    <asp:DropDownList ID="ddlPeriodo" runat="server" AutoPostBack="True" class="form-control" name="periodo">
                                    </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row gutter">
                                    <div class="col-md-4">
                                        Selecione el Ejercicio
                                    <asp:DropDownList ID="ddlEjercicio" runat="server" AutoPostBack="True" class="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row gutter">
                                    <div class=" col-md-12">
                                        <br />
                                        <asp:LinkButton runat="server" ID="btnExportarDatos" CssClass="btn btn-info" OnClick="btnExportarDatos_Click">                                           
                                        <i class="fa fa-upload"></i> Importacion de Nominas de Compaqi a Sistema.
                                        </asp:LinkButton>
                                    </div>
                                </div>


                                <%--    <div class="text-right">
                                    <asp:LinkButton runat="server" ID="btnCarga" CssClass="btn btn-success" OnClick="btnCarga_Click">
                                        <i class="fa fa-upload"></i> Carga Masiva
                                    </asp:LinkButton>
                                </div>--%>


                                <div class="text-left">
                                    <%--<asp:Button runat="server" Text="Importacion de Datos de Compaq a Sistema." ID="btnImportarCompaqi" CssClass="btn btn-danger"  OnClick="btnImportarCompaqi_Click" />--%>

                                    <%--<i class="fa fa-upload"></i>Importacion de Datos de Compaq a Sistema--%>
                                </div>
                                <div class="form-group">
                                    <div class="row gutter">
                                        <div class="col-md-4">
                                            Empleado:
                                            <%--<asp:Label ID="lblEmpleado" runat="server" class="form-control"></asp:Label>--%>
                                            <br />
                                            <ajaxToolkit:ComboBox ID="cbxEmpleados" runat="server" AutoCompleteMode="SuggestAppend" DropDownStyle="Simple" Width="550px" AutoPostBack="True" OnSelectedIndexChanged="cbxEmpleados_SelectedIndexChanged">
                                            </ajaxToolkit:ComboBox>
                                        </div>
                                        <div class="col-md-8">
                                        </div>
                                    </div>
                                    <div class="row gutter">
                                        <div class="col-md-4">
                                            Percepción:
                                            <asp:DropDownList ID="cbxPercepciones" runat="server" AutoPostBack="True" class="form-control" name="percepciones">
                                            </asp:DropDownList>
                                            <%--<br />
                                            <ajaxToolkit:ComboBox ID="cbxPercepciones" runat="server" AutoCompleteMode="SuggestAppend" DropDownStyle="Simple" Width="550px" OnSelectedIndexChanged="cbxPercepciones_SelectedIndexChanged">
                                            </ajaxToolkit:ComboBox>--%>
                                        </div>
                                    </div>
                                    <div class="row gutter">
                                        <div class="col-md-4">
                                            Monto:
                                            <asp:TextBox ID="txtMonto" runat="server" class="form-control" name="monto" onkeypress="return onlyDotsAndNumbers(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row gutter">
                                        <div class="col-md-4">
                                            Fecha:
                                            <asp:TextBox ID="txtFecha" runat="server" class="form-control" name="fecha" TextMode="DateTime" AutoComplete="off"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="txtFecha_CalendarExtender" runat="server" BehaviorID="txtFecha_CalendarExtender" TargetControlID="txtFecha" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" PopupPosition="BottomRight" />
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
                                            <asp:Button ID="btnGuardar" runat="server" OnClick="btnGuardar_Click" Text="Guardar" class="btn btn-info" />
                                            <asp:Button ID="btnVolver" runat="server" class="btn btn-info" PostBackUrl="~/productosTipo.aspx" Text="Volver" />
                                        </div>
                                    </div>
                                    <div class="row gutter">
                                        <div class="col-md-12">
                                            <div id="ContentPlaceHolder_upGrid">
                                                <div class="row gutter">
                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                        <div class="panel panel-blue">
                                                            <div class="panel-heading">
                                                                <h4></h4>
                                                            </div>
                                                            <div class="panel-body">
                                                                <div class="table-responsive">
                                                                    <asp:ListView ID="lvDetalles" runat="server" OnItemCommand="lvDetalles_ItemCommand" OnItemDeleting="lvDetalles_ItemDeleting" OnItemUpdating="lvDetalles_ItemUpdating">
                                                                        <LayoutTemplate>
                                                                            <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                                                                <thead>
                                                                                    <tr>
                                                                                        <th style="text-align: center" id="codigo">Código</th>
                                                                                        <th style="text-align: center" id="empleado">Empleado</th>
                                                                                        <th style="text-align: center" id="percepcion">Percepcion</th>
                                                                                        <th style="text-align: center" id="monto">Monto</th>
                                                                                        <th style="text-align: center" id="fecha">Fecha</th>
                                                                                        <th style="text-align: center" id="mes">Mes</th>
                                                                                        <th style="text-align: center" id="eliminar">Eliminar</th>
                                                                                    </tr>
                                                                                </thead>
                                                                                <tr id="itemPlaceholder" runat="server"></tr>
                                                                                <tfoot>
                                                                                    <tr>
                                                                                        <th style="text-align: center">Código</th>
                                                                                        <th style="text-align: center">Empleado</th>
                                                                                        <th style="text-align: center">Percepcion</th>
                                                                                        <th style="text-align: center">Monto</th>
                                                                                        <th style="text-align: center">Fecha</th>
                                                                                        <th style="text-align: center">Mes</th>
                                                                                        <th style="text-align: center">Eliminar</th>
                                                                                    </tr>
                                                                                </tfoot>
                                                                            </table>
                                                                        </LayoutTemplate>
                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <td style="text-align: left"><%# Eval("codigo") %></td>
                                                                                <td style="text-align: left"><%# Eval("nombre") %></td>
                                                                                <td style="text-align: left"><%# Eval("percepcion") %></td>
                                                                                <td style="text-align: right"><%# Eval("monto") %></td>
                                                                                <td style="text-align: center"><%# Eval("fecha").ToString().Substring(0,10) %></td>
                                                                                <td style="text-align: left">
                                                                                    <%# obtenerMes(Eval("mes").ToString()) %>
                                                                                </td>
                                                                                <td style="text-align: center">
                                                                                    <asp:LinkButton ID="lbtnEliminar" runat="server" CommandArgument='<%# Eval("id") + "ˇ" + Eval("nombre") %>' CommandName="delete"><i class="icon-delete"></i></asp:LinkButton>
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                        <EmptyDataTemplate>
                                                                            <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                                                                <thead>
                                                                                    <tr>
                                                                                        <th style="text-align: center" id="codigo">Código</th>
                                                                                        <th style="text-align: center" id="empleado">Empleado</th>
                                                                                        <th style="text-align: center" id="percepcion">Percepcion</th>
                                                                                        <th style="text-align: center" id="monto">Monto</th>
                                                                                        <th style="text-align: center" id="fecha">Fecha</th>
                                                                                        <th style="text-align: center" id="mes">Mes</th>
                                                                                        <th style="text-align: center" id="eliminar">Eliminar</th>
                                                                                    </tr>
                                                                                </thead>
                                                                                <tr>
                                                                                    <td colspan="7">
                                                                                        <label class="label label-danger">¡El empleado no tiene Percepciones!</label></td>
                                                                                </tr>
                                                                            </table>
                                                                        </EmptyDataTemplate>
                                                                    </asp:ListView>
                                                                </div>
                                                            </div>
                                                            <asp:HiddenField ID="hfIdEP" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </form>
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
                        </form>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cbxEmpleados" EventName="SelectedIndexChanged" />
                        <%--<asp:AsyncPostBackTrigger ControlID="cbxPercepciones" EventName="SelectedIndexChanged" />--%>
                        <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
                        <%--<asp:AsyncPostBackTrigger ControlID="btnImportarCompaqi" EventName="Click" />--%>
                        <asp:AsyncPostBackTrigger ControlID="lvDetalles" EventName="ItemCommand" />
                        <asp:AsyncPostBackTrigger ControlID="mbtnAceptar" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="mbtnClose" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="lvDetalles" EventName="ItemDeleting" />
                        <asp:AsyncPostBackTrigger ControlID="lvDetalles" EventName="ItemUpdating" />
                    </Triggers>
                </asp:UpdatePanel>


                <div class="modal fade" id="myModalCMasiva">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span></button>
                                <h4 class="modal-title">
                                    <asp:Label ID="Label1" runat="server" />
                                </h4>
                            </div>
                            <div class="modal-body">
                                <div>
                                    <h5>Antes de seleccionar un archivo de Excel verificar que no esté abierto.</h5>
                                    <br />
                                    <label>Mes: </label>
                                    <asp:DropDownList runat="server" ID="ddlMes" CssClass="form-control">
                                        <asp:ListItem Value="1">1-Enero</asp:ListItem>
                                        <asp:ListItem Value="2">2-Febrero</asp:ListItem>
                                        <asp:ListItem Value="3">3-Marzo</asp:ListItem>
                                        <asp:ListItem Value="4">4-Abril</asp:ListItem>
                                        <asp:ListItem Value="5">5-Mayo</asp:ListItem>
                                        <asp:ListItem Value="6">6-Junio</asp:ListItem>
                                        <asp:ListItem Value="7">7-Julio</asp:ListItem>
                                        <asp:ListItem Value="8">8-Agosto</asp:ListItem>
                                        <asp:ListItem Value="9">9-Septiembre</asp:ListItem>
                                        <asp:ListItem Value="10">10-Octubre</asp:ListItem>
                                        <asp:ListItem Value="11">11-Noviembre</asp:ListItem>
                                        <asp:ListItem Value="15">12-Diciembre</asp:ListItem>
                                    </asp:DropDownList>
                                    <br />


                                    <asp:FileUpload runat="server" ID="fuArchivo" />

                                    <br />
                                    <asp:Label runat="server" ID="lblMensaje" CssClass="alert-danger"></asp:Label>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="Button1" runat="server" Text="Cerrar" class="btn btn-default" OnClick="mbtnClose_Click" />
                                    <button type="button" style="display: none;" id="btnClose" class="btn btn-default" data-dismiss="modal">
                                        Close</button>
                                    <asp:Button ID="btnSubir" runat="server" Text="Aceptar" class="btn btn-info" OnClick="btnSubir_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="modal fade" id="myModalCorrecto">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span></button>
                                <h4 class="modal-title">
                                    <label>Carga Masiva</label>
                                </h4>
                            </div>
                            <div class="modal-body">
                                <div>
                                    <label>Cargado correctamente</label>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="Button2" runat="server" Text="Cerrar" class="btn btn-default" OnClick="mbtnClose_Click" />
                                    <button type="button" style="display: none;" id="btnAcept" class="btn btn-default" data-dismiss="modal">
                                        Aceptar</button>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
    <!-- Row Ends -->

    <script>
         //para activar el llenado de los cuadros de calificacion

    </script>

</asp:Content>
