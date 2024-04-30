<%@ Page Title="" Language="C#" MasterPageFile="~/PopUP.Master" AutoEventWireup="true" CodeBehind="pCalidad.aspx.cs" Inherits="despacho.pCalidad" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

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
                                    <div class="row gutter">
                                        <asp:Panel ID="Panel1" runat="server">
                                            <div class="form-group">
                                                <div class="panel-body">
                                                    <%--<div class="row gutter">
                                                        <div class="col-md-12">
                                                            <div class="container">
                                                               <div class="row">
                                                                  <div class='col-sm-6'>
                                                                     <div class="form-group">
                                                                        <div class='input-group date' id='datetimepicker3'>
                                                                           <input type='text' class="form-control" />
                                                                           <span class="input-group-addon">
                                                                           <span class="glyphicon glyphicon-time"></span>
                                                                           </span>
                                                                        </div>
                                                                     </div>
                                                                  </div>
                                                                  <script type="text/javascript">
                                                                      $(function () {
                                                                          $('#datetimepicker3').datetimepicker({
                                                                              format: 'LT'
                                                                          });
                                                                      });
                                                                  </script>
                                                               </div>
                                                            </div>
                                                            <div class="container">
                                                               <div class="row">
                                                                  <div class='col-sm-6'>
                                                                     <input type='text' class="form-control" id='datetimepicker4' />
                                                                  </div>
                                                                  <script type="text/javascript">
                                                                      $(function () {
                                                                          $('#datetimepicker4').datetimepicker();
                                                                      });
                                                                  </script>
                                                               </div>
                                                            </div>
                                                        </div>
                                                    </div>--%>
                                                    <div class="row gutter">
                                                        <div class="col-md-3">
                                                            <label class="control-label">Cilindro:</label>
                                                            <asp:TextBox ID="txtCilindro" runat="server" class="form-control" onkeypress="return onlyNumbers(event)"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <label class="control-label">Fecha de colado:</label>
                                                            <asp:TextBox ID="txtFechaColado" runat="server" class="form-control"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="txtFechaColado_CalendarExtender" runat="server" BehaviorID="txtFechaColado_CalendarExtender" Format="yyyy/MM/dd" TargetControlID="txtFechaColado" />
                                                        </div>
                                                        <div class="col-md-3">
                                                            <label class="control-label">Edad de ensaye:</label>
                                                            <asp:TextBox ID="txtEdadEnsaye" runat="server" class="form-control" onkeypress="return onlyNumbers(event)" OnTextChanged="txtEdadEnsaye_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <label class="control-label">Fecha de ensaye:</label>
                                                            <asp:TextBox ID="txtFechaEnsaye" runat="server" class="form-control"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="txtFechaEnsaye_CalendarExtender" runat="server" BehaviorID="txtFechaEnsaye_CalendarExtender" Format="yyyy/MM/dd" TargetControlID="txtFechaEnsaye" />
                                                        </div>
                                                    </div>
                                                    <div class="row gutter">
                                                        <div class="col-md-3">
                                                            <label class="control-label">Resistencia KG/cm2:</label>
                                                            <asp:TextBox ID="txtResistencia" runat="server" class="form-control" onkeypress="return onlyNumbers(event)" OnTextChanged="txtResistencia_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <label class="control-label">Carga KG:</label>
                                                            <asp:TextBox ID="txtCargaKG" runat="server" class="form-control" onkeypress="return onlyNumbers(event)" OnTextChanged="txtCargaKG_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <label class="control-label">Área Cm2:</label>
                                                            <asp:TextBox ID="txtAreaCM" runat="server" class="form-control" onkeypress="return onlyDotsAndNumbers(event)" OnTextChanged="txtAreaCM_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <label class="control-label">Esfuerzo Kg/cm2:</label>
                                                            <asp:TextBox ID="txtEsfuerzoKg" runat="server" class="form-control" onkeypress="return onlyNumbers(event)"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="row gutter">
                                                        <div class="col-md-3">
                                                            <label class="control-label">Resistencia en %:</label>
                                                            <asp:TextBox ID="txtResistenciaP" runat="server" class="form-control" onkeypress="return onlyNumbers(event)"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <label class="control-label">Tam Max:</label>
                                                            <asp:TextBox ID="txtTamMax" runat="server" class="form-control" onkeypress="return onlyNumbers(event)"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <label class="control-label">REvCM:</label>
                                                            <asp:TextBox ID="txtRevCM" runat="server" class="form-control" onkeypress="return onlyNumbers(event)"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <label class="control-label">Tipo de concreto:</label>
                                                            <asp:TextBox ID="txtTipoConcreto" runat="server" class="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="row gutter">
                                                        <div class="col-md-3">
                                                            <label class="control-label">Temperatura del ambiente:</label>
                                                            <asp:TextBox ID="txtTempAmb" runat="server" class="form-control" onkeypress="return onlyNumbers(event)"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <label class="control-label">Temperatura del concreto:</label>
                                                            <asp:TextBox ID="txtTempConc" runat="server" class="form-control" onkeypress="return onlyNumbers(event)"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <label class="control-label">Aditivo:</label>
                                                            <asp:TextBox ID="txtAditivo" runat="server" class="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <label class="control-label">Elemento Colado:</label>
                                                            <asp:TextBox ID="txtElemento" runat="server" class="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="row gutter">
                                                        <div class="col-md-12">
                                                            <asp:Label ID="lblMensaje" runat="server" Text="" ForeColor="Red" Font-Size="Large" Font-Bold="True"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row gutter">
                                                        <div class="col-md-12">
                                                            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" class="btn btn-info" OnClick="btnAgregar_Click" Visible="False" />
                                                            <asp:Button ID="btnModificar" runat="server" Text="Modificar" class="btn btn-info" OnClick="btnModificar_Click" Visible="False" />
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row gutter">
                                                        <div class="col-md-12">
                                                            <asp:Panel ID="Panel2" runat="server" ScrollBars="Horizontal">
                                                                <asp:ListView ID="listView" runat="server" OnItemCommand="listView_ItemCommand" OnItemDeleting="listView_ItemDeleting" OnItemUpdating="listView_ItemUpdating">
                                                                <LayoutTemplate>
                                                                    <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                                                        <thead>
                                                                            <tr>
                                                                                <th style="text-align: center" id="numCilindro">Cilindro</th>
                                                                                <th style="text-align: center" id="fechaColado">Fecha de Colado</th>
                                                                                <th style="text-align: center" id="edadEnsaye">Edad de Ensaye</th>
                                                                                <th style="text-align: center" id="fechaEnsaye">Fecha de Ensaye</th>
                                                                                <th style="text-align: center" id="resistenciaKG">Resistencia KG/cm2</th>
                                                                                <th style="text-align: center" id="cargaKG">Carga KG</th>
                                                                                <th style="text-align: center" id="areaCM">Area cm2</th>
                                                                                <th style="text-align: center" id="esfuerzoKG">Esfuerzo KG/cm2</th>
                                                                                <th style="text-align: center" id="resistenciaP">Resistencia %</th>
                                                                                <th style="text-align: center" id="tamMax">TAM MAX</th>
                                                                                <th style="text-align: center" id="revCM">REV CM</th>
                                                                                <th style="text-align: center" id="tipoConcreto">Tipo Concreto</th>
                                                                                <th style="text-align: center" id="cliente">Cliente</th>
                                                                                <th style="text-align: center" id="proyecto">Proyecto</th>
                                                                                <th style="text-align: center" id="unidad">Unidad</th>
                                                                                <th style="text-align: center" id="tempAmb">Tem Amb</th>
                                                                                <th style="text-align: center" id="tempConc">Tem Concreto</th>
                                                                                <th style="text-align: center" id="remision">Remisión</th>
                                                                                <th style="text-align: center" id="aditivo">Aditivo</th>
                                                                                <th style="text-align: center" id="elemento">Elemento</th>
                                                                                <th style="text-align: center" id="modificar">Modificar</th>
                                                                                <th style="text-align: center" id="eliminar">Eliminar</th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tr id="itemPlaceholder" runat="server"></tr>
                                                                        <tfoot>
                                                                            <tr>
                                                                                <th style="text-align: center">Cilindro</th>
                                                                                <th style="text-align: center">Fecha de Colado</th>
                                                                                <th style="text-align: center">Edad de Ensaye</th>
                                                                                <th style="text-align: center">Fecha de Ensaye</th>
                                                                                <th style="text-align: center">Resistencia KG/cm2</th>
                                                                                <th style="text-align: center">Carga KG</th>
                                                                                <th style="text-align: center">Area cm2</th>
                                                                                <th style="text-align: center">Esfuerzo KG/cm2</th>
                                                                                <th style="text-align: center">Resistencia %</th>
                                                                                <th style="text-align: center">TAM MAX</th>
                                                                                <th style="text-align: center">REV CM</th>
                                                                                <th style="text-align: center">Tipo Concreto</th>
                                                                                <th style="text-align: center">Cliente</th>
                                                                                <th style="text-align: center">Proyecto</th>
                                                                                <th style="text-align: center">Unidad</th>
                                                                                <th style="text-align: center">Tem Amb</th>
                                                                                <th style="text-align: center">Tem Concreto</th>
                                                                                <th style="text-align: center">Remisión</th>
                                                                                <th style="text-align: center">Aditivo</th>
                                                                                <th style="text-align: center">Elemento</th>
                                                                                <th style="text-align: center">Modificar</th>
                                                                                <th style="text-align: center">Eliminar</th>
                                                                            </tr>
                                                                        </tfoot>
                                                                    </table>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td style="text-align: center"><%# Eval("numCilindro") %></td>
                                                                        <td style="text-align: center"><%# Eval("fechaColado").ToString().Substring(0,10) %></td>
                                                                        <td style="text-align: center"><%# Eval("edadEnsaye") %></td>
                                                                        <td style="text-align: center"><%# Eval("fechaEnsayeT").ToString().Substring(0,10) %></td>
                                                                        <td style="text-align: center"><%# Eval("resistenciaKG") %></td>
                                                                        <td style="text-align: center"><%# Eval("cargaKG") %></td>
                                                                        <td style="text-align: center"><%# Eval("areaCM") %></td>
                                                                        <td style="text-align: center"><%# Eval("esfuerzoKG") %></td>
                                                                        <td style="text-align: center"><%# Eval("resistenciaP") %></td>
                                                                        <td style="text-align: center"><%# Eval("tamMax") %></td>
                                                                        <td style="text-align: center"><%# Eval("revCM") %></td>
                                                                        <td style="text-align: center"><%# Eval("tipoConcreto") %></td>
                                                                        <td style="text-align: center"><%# Eval("nombre") %></td>
                                                                        <td style="text-align: center"><%# Eval("proyecto") %></td>
                                                                        <td style="text-align: center"><%# Eval("unidadT") %></td>
                                                                        <td style="text-align: center"><%# Eval("tempAmb") %></td>
                                                                        <td style="text-align: center"><%# Eval("tempConc") %></td>
                                                                        <td style="text-align: center"><%# Eval("folio") + "-" + Eval("id") %></td>
                                                                        <td style="text-align: center"><%# Eval("aditivo") %></td>
                                                                        <td style="text-align: center"><%# Eval("elemento") %></td>
                                                                        <td style="text-align: center">
                                                                            <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="update"><i class="icon-new-message"></i></asp:LinkButton>
                                                                        </td>
                                                                        <td style="text-align: center">
                                                                            <asp:LinkButton ID="lbtnEliminar" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="delete"><i class="icon-delete"></i></asp:LinkButton>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <EmptyDataTemplate>
                                                                    <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                                                        <thead>
                                                                            <tr>
                                                                                <th style="text-align: center" id="numCilindro">Cilindro</th>
                                                                                <th style="text-align: center" id="fechaColado">Fecha de Colado</th>
                                                                                <th style="text-align: center" id="edadEnsaye">Edad de Ensaye</th>
                                                                                <th style="text-align: center" id="fechaEnsaye">Fecha de Ensaye</th>
                                                                                <th style="text-align: center" id="resistenciaKG">Resistencia KG/cm2</th>
                                                                                <th style="text-align: center" id="cargaKG">Carga KG</th>
                                                                                <th style="text-align: center" id="areaCM">Area cm2</th>
                                                                                <th style="text-align: center" id="esfuerzoKG">Esfuerzo KG/cm2</th>
                                                                                <th style="text-align: center" id="resistenciaP">Resistencia %</th>
                                                                                <th style="text-align: center" id="tamMax">TAM MAX</th>
                                                                                <th style="text-align: center" id="revCM">REV CM</th>
                                                                                <th style="text-align: center" id="tipoConcreto">Tipo Concreto</th>
                                                                                <th style="text-align: center" id="cliente">Cliente</th>
                                                                                <th style="text-align: center" id="proyecto">Proyecto</th>
                                                                                <th style="text-align: center" id="unidad">Unidad</th>
                                                                                <th style="text-align: center" id="tempAmb">Tem Amb</th>
                                                                                <th style="text-align: center" id="tempConc">Tem Concreto</th>
                                                                                <th style="text-align: center" id="remision">Remisión</th>
                                                                                <th style="text-align: center" id="aditivo">Aditivo</th>
                                                                                <th style="text-align: center" id="elemento">Elemento</th>
                                                                                <th style="text-align: center" id="modificar">Modificar</th>
                                                                                <th style="text-align: center" id="eliminar">Eliminar</th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tr>
                                                                            <td colspan="22">
                                                                                <label class="label label-danger">¡No hay Datos Registrados!</label></td>
                                                                        </tr>
                                                                    </table>
                                                                </EmptyDataTemplate>
                                                            </asp:ListView>
                                                            </asp:Panel>
                                                        </div>
                                                    </div>
                                                </div>
                                        </asp:Panel>
                                        <asp:HiddenField ID="hfIdCalidad" runat="server" />
                                    </div>
                                </div>
                            </form>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnAgregar" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnModificar" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="listView" EventName="ItemCommand" />
                        <asp:AsyncPostBackTrigger ControlID="listView" EventName="ItemDeleting" />
                        <asp:AsyncPostBackTrigger ControlID="listView" EventName="ItemUpdating" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!-- Row Ends -->
</asp:Content>
