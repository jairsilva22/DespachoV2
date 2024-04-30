<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="generarOD.aspx.cs" Inherits="despacho.generarOD" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    &nbsp;<!-- Top Bar Starts --><asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
    <div class="top-bar clearfix">
        <div class="row gutter">
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <div class="page-title">
                    <h3>Generar Ordenes de Dosificación</h3>
                    <p>/<a href="ordenes.aspx">Ordenes</a></p>
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
                                                    <td colspan="2">
                                                        <div class="row gutter">
                                                            <div class="col-md-8">
                                                                Cliente:
                                                            </div>
                                                        </div>
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
                                                                Sucursal:
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
                                                        <asp:Label ID="lblSucursal" runat="server" class="form-control"></asp:Label>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <div class="row gutter">
                                                            <div class="col-md-8">
                                                                Fecha:
                                                            </div>
                                                        </div>
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
                                                        <asp:Label ID="lblFecha" runat="server" class="form-control"></asp:Label>
                                                        <%--<asp:TextBox ID="txtFecha" runat="server" class="form-control"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="txtFecha_CalendarExtender" runat="server" BehaviorID="txtFecha_CalendarExtender" FirstDayOfWeek="Monday" Format="yyyy/MM/dd" PopupPosition="BottomRight" TargetControlID="txtFecha" />--%>
                                                    </td>
                                                    <td style="width: 50%;">
                                                        <asp:Label ID="lblHora" runat="server" class="form-control"></asp:Label>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <div class="row gutter">
                                                            <div class="col-md-8">
                                                                Comentarios:
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
                                                    <td colspan="2">
                                                        <asp:Panel ID="pnlDetalleProductos" runat="server">
                                                            <table class="nav-justified">
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td colspan="12">

                                                                        <!-- Row Starts -->
                                                                        <div id="ContentPlaceHolder_upGrid">
                                                                            <div class="row gutter">
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                                                    <div class="panel panel-blue">
                                                                                        <div class="panel-heading">
                                                                                            <h4>Productos a generar Ordenes de entrega</h4>
                                                                                        </div>
                                                                                        <div class="panel-body">
                                                                                            <div class="table-responsive">
                                                                                                <asp:ListView ID="lvDetalles" runat="server" OnItemCommand="lvDetalles_ItemCommand">
                                                                                                    <LayoutTemplate>
                                                                                                        <table id="responsiveTabl" class="table table-striped table-bordered no-margin">
                                                                                                            <thead>
                                                                                                                <tr>
                                                                                                                    <th id="codigo" style="text-align: center">Código</th>
                                                                                                                    <th id="descripción">Descripción</th>
                                                                                                                    <th id="cantOrdenada" style="text-align: center">Cantidad a Entregar</th>
                                                                                                                    <th id="modificar" style="text-align: center">Programar</th>
                                                                                                                </tr>
                                                                                                            </thead>
                                                                                                            <tr id="itemPlaceholder" runat="server">
                                                                                                            </tr>
                                                                                                            <tfoot>
                                                                                                                <tr>
                                                                                                                    <th style="text-align: center">Código</th>
                                                                                                                    <th>Descripción</th>
                                                                                                                    <th style="text-align: center">Cantidad a Entregar</th>
                                                                                                                    <th style="text-align: center">Programar</th>
                                                                                                                </tr>
                                                                                                            </tfoot>
                                                                                                        </table>
                                                                                                    </LayoutTemplate>
                                                                                                    <ItemTemplate>
                                                                                                        <tr>
                                                                                                            <td style="text-align: center"><%# Eval("codigo") %></td>
                                                                                                            <td><%# Eval("descripcion") %></td>
                                                                                                            <td style="text-align: center"><%# Eval("cantidad") + " " + Eval("unidad") %></td>
                                                                                                            <td style="text-align: center">
                                                                                                                <asp:LinkButton ID="lbtnAccion" runat="server" CommandArgument='<%# Eval("id") + "ˇ" + Eval("idProducto") + "ˇ" + Eval("codigo") + "ˇ" + Eval("descripcion") + "ˇ" + Eval("cantidad") + "ˇ" + Eval("unidad") + "ˇ" + Eval("idUDM") + "ˇ" + Eval("revenimiento") + "ˇ" + Eval("idTipoProducto") + "ˇ" + Eval("tipo") %>' CommandName="programar"><i class="icon-edit"></i></asp:LinkButton>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </ItemTemplate>
                                                                                                    <EmptyDataTemplate>
                                                                                                        <table id="responsiveTable0" class="table table-striped table-bordered no-margin">
                                                                                                            <thead>
                                                                                                                <tr>
                                                                                                                    <th style="text-align: center">Código</th>
                                                                                                                    <th>Descripción</th>
                                                                                                                    <th style="text-align: center">Cantidad a Entregar</th>
                                                                                                                    <th style="text-align: center">Revenimiento</th>
                                                                                                                    <th style="text-align: center">Programar</th>
                                                                                                                </tr>
                                                                                                            </thead>
                                                                                                            <tr>
                                                                                                                <td colspan="5">
                                                                                                                    <label class="label label-danger">
                                                                                                                        ¡No hay Productos Registrados!</label></td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </EmptyDataTemplate>
                                                                                                </asp:ListView>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <!-- Row Ends -->
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </table>
                                    </asp:Panel>
                                    <asp:Panel ID="Panel3" runat="server">
                                        <div class="form-group">
                                            <table class="nav-justified">
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td colspan="2">
                                                        <asp:Panel ID="Panel2" runat="server">
                                                            <div class="row gutter">
                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                                    <div class="panel panel-blue">
                                                                        <div class="panel-heading">
                                                                            <h4>
                                                                                <asp:Label ID="lblMensaje" runat="server" class="control-label" ForeColor="Red" Font-Bold="True" Font-Size="Large"></asp:Label>
                                                                            </h4>
                                                                        </div>
                                                                        <div class="panel-body">
                                                                            <div class="table-responsive">
                                                                                <table class="nav-justified">
                                                                                    <tr>
                                                                                        <td colspan="14">
                                                                                            <asp:Panel ID="pnlDS" runat="server" Visible="false">
                                                                                                <table class="nav-justified">
                                                                                                    <tr>
                                                                                                        <td>&nbsp;</td>
                                                                                                        <td>
                                                                                                            <div class="row gutter">
                                                                                                                <div class="col-md-8">
                                                                                                                    Código del Producto:
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <div class="row gutter">
                                                                                                                <div class="col-md-8">
                                                                                                                    Descripción:
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <div class="row gutter">
                                                                                                                <div class="col-md-8">
                                                                                                                    Cantidad Ordenada:
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <div class="row gutter">
                                                                                                                <div class="col-md-8">
                                                                                                                    Tipo de unidad:
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <div class="row gutter">
                                                                                                                <div class="col-md-8">
                                                                                                                    <asp:Label ID="lblRevenimiento" runat="server" Text="Revenimiento:" Visible="False"></asp:Label>
                                                                                                                </div>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <div class="row gutter">
                                                                                                                <div class="col-md-8">
                                                                                                                    Cantidad máxima de carga:
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <div class="row gutter">
                                                                                                                <div class="col-md-8">
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </td>
                                                                                                        <td>&nbsp;</td>
                                                                                                        <td>&nbsp;</td>
                                                                                                        <td>&nbsp;</td>
                                                                                                        <td>&nbsp;</td>
                                                                                                        <td>&nbsp;</td>
                                                                                                        <td>&nbsp;</td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>&nbsp;</td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblCodigoProducto0" runat="server" class="form-control" name="codigoP"></asp:Label>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblDescProducto0" runat="server" class="form-control" name="descP"></asp:Label>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblCantProducto0" runat="server" class="form-control" name="descP"></asp:Label>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblUDM2" runat="server" class="form-control"></asp:Label>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblRevenimiento3" runat="server" class="form-control"></asp:Label>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblCantMaxCarga" runat="server" class="form-control" name="cantMaxCarga"></asp:Label>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Button ID="btnGenerarOD" runat="server" class="btn btn-info" OnClick="btnGenerarOD_Click" Text="Automáticamente" Visible="False" />
                                                                                                            <asp:Button ID="btnGenerarODManual" runat="server" class="btn btn-info" OnClick="btnGenerarODManual_Click" Text="Generar" Visible="False" />
                                                                                                        </td>
                                                                                                        <td>&nbsp;</td>
                                                                                                        <td>&nbsp;</td>
                                                                                                        <td>&nbsp;</td>
                                                                                                        <td>&nbsp;</td>
                                                                                                        <td>&nbsp;</td>
                                                                                                        <td>&nbsp;</td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </asp:Panel>
                                                                                        </td>
                                                                                        <tr>
                                                                                            <td colspan="14">
                                                                                                <asp:Panel ID="PanelDatosOD" runat="server" Visible="False">
                                                                                                    <table id="tableOD" class="nav-justified">
                                                                                                        <tr>
                                                                                                            <td>&nbsp;</td>
                                                                                                            <td>
                                                                                                                <div class="row gutter">
                                                                                                                    <div class="col-md-8">
                                                                                                                        Fecha de entrega:
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <div class="row gutter">
                                                                                                                    <div class="col-md-8">
                                                                                                                        Hora:
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <div class="row gutter">
                                                                                                                    <div class="col-md-8">
                                                                                                                        Código del Producto:
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <div class="row gutter">
                                                                                                                    <div class="col-md-8">
                                                                                                                        Descripción:
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <div class="row gutter">
                                                                                                                    <div class="col-md-8">
                                                                                                                        Restante:
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <div class="row gutter">
                                                                                                                    <div class="col-md-8">
                                                                                                                        Cantidad:
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <div class="row gutter">
                                                                                                                    <div class="col-md-8">
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <div class="row gutter">
                                                                                                                    <div class="col-md-8">
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </td>
                                                                                                            <td>&nbsp;</td>
                                                                                                            <td>&nbsp;</td>
                                                                                                            <td>&nbsp;</td>
                                                                                                            <td>&nbsp;</td>
                                                                                                            <td>&nbsp;</td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>&nbsp;</td>
                                                                                                            <td>
                                                                                                                <asp:TextBox ID="txtFechaDosificacion" runat="server" class="form-control" AutoComplete="off"></asp:TextBox>
                                                                                                                <ajaxToolkit:CalendarExtender ID="txtFechaDosificacion_CalendarExtender" runat="server" BehaviorID="txtFechaDosificacion_CalendarExtender" FirstDayOfWeek="Monday" Format="yyyy/MM/dd" PopupPosition="BottomRight" TargetControlID="txtFechaDosificacion" />
                                                                                                            </td>
                                                                                                            <td style="width: 20%">
                                                                                                                <div class="row gutter">
                                                                                                                    <div class="col-md-4">
                                                                                                                        <asp:DropDownList ID="cbxHoraD" runat="server" class="form-control">
                                                                                                                            <asp:ListItem>01</asp:ListItem>
                                                                                                                            <asp:ListItem>02</asp:ListItem>
                                                                                                                            <asp:ListItem>03</asp:ListItem>
                                                                                                                            <asp:ListItem>04</asp:ListItem>
                                                                                                                            <asp:ListItem>05</asp:ListItem>
                                                                                                                            <asp:ListItem>06</asp:ListItem>
                                                                                                                            <asp:ListItem Selected="True">07</asp:ListItem>
                                                                                                                            <asp:ListItem>08</asp:ListItem>
                                                                                                                            <asp:ListItem>09</asp:ListItem>
                                                                                                                            <asp:ListItem>10</asp:ListItem>
                                                                                                                            <asp:ListItem>11</asp:ListItem>
                                                                                                                            <asp:ListItem>12</asp:ListItem>
                                                                                                                            <asp:ListItem>13</asp:ListItem>
                                                                                                                            <asp:ListItem>14</asp:ListItem>
                                                                                                                            <asp:ListItem>15</asp:ListItem>
                                                                                                                            <asp:ListItem>16</asp:ListItem>
                                                                                                                            <asp:ListItem>17</asp:ListItem>
                                                                                                                            <asp:ListItem>18</asp:ListItem>
                                                                                                                            <asp:ListItem>19</asp:ListItem>
                                                                                                                            <asp:ListItem>20</asp:ListItem>
                                                                                                                            <asp:ListItem>21</asp:ListItem>
                                                                                                                            <asp:ListItem>22</asp:ListItem>
                                                                                                                            <asp:ListItem>23</asp:ListItem>
                                                                                                                        </asp:DropDownList>
                                                                                                                    </div>
                                                                                                                    <div class="col-md-4">
                                                                                                                        <asp:DropDownList ID="cbxMinutosD" runat="server" class="form-control">
                                                                                                                            <asp:ListItem Selected="True">00</asp:ListItem>
                                                                                                                            <asp:ListItem>01</asp:ListItem>
                                                                                                                            <asp:ListItem>02</asp:ListItem>
                                                                                                                            <asp:ListItem>03</asp:ListItem>
                                                                                                                            <asp:ListItem>04</asp:ListItem>
                                                                                                                            <asp:ListItem>05</asp:ListItem>
                                                                                                                            <asp:ListItem>06</asp:ListItem>
                                                                                                                            <asp:ListItem>07</asp:ListItem>
                                                                                                                            <asp:ListItem>08</asp:ListItem>
                                                                                                                            <asp:ListItem>09</asp:ListItem>
                                                                                                                            <asp:ListItem>10</asp:ListItem>
                                                                                                                            <asp:ListItem>11</asp:ListItem>
                                                                                                                            <asp:ListItem>12</asp:ListItem>
                                                                                                                            <asp:ListItem>13</asp:ListItem>
                                                                                                                            <asp:ListItem>14</asp:ListItem>
                                                                                                                            <asp:ListItem>15</asp:ListItem>
                                                                                                                            <asp:ListItem>16</asp:ListItem>
                                                                                                                            <asp:ListItem>17</asp:ListItem>
                                                                                                                            <asp:ListItem>18</asp:ListItem>
                                                                                                                            <asp:ListItem>19</asp:ListItem>
                                                                                                                            <asp:ListItem>20</asp:ListItem>
                                                                                                                            <asp:ListItem>21</asp:ListItem>
                                                                                                                            <asp:ListItem>22</asp:ListItem>
                                                                                                                            <asp:ListItem>23</asp:ListItem>
                                                                                                                            <asp:ListItem>24</asp:ListItem>
                                                                                                                            <asp:ListItem>25</asp:ListItem>
                                                                                                                            <asp:ListItem>26</asp:ListItem>
                                                                                                                            <asp:ListItem>27</asp:ListItem>
                                                                                                                            <asp:ListItem>28</asp:ListItem>
                                                                                                                            <asp:ListItem>29</asp:ListItem>
                                                                                                                            <asp:ListItem>30</asp:ListItem>
                                                                                                                            <asp:ListItem>31</asp:ListItem>
                                                                                                                            <asp:ListItem>32</asp:ListItem>
                                                                                                                            <asp:ListItem>33</asp:ListItem>
                                                                                                                            <asp:ListItem>34</asp:ListItem>
                                                                                                                            <asp:ListItem>35</asp:ListItem>
                                                                                                                            <asp:ListItem>36</asp:ListItem>
                                                                                                                            <asp:ListItem>37</asp:ListItem>
                                                                                                                            <asp:ListItem>38</asp:ListItem>
                                                                                                                            <asp:ListItem>39</asp:ListItem>
                                                                                                                            <asp:ListItem>40</asp:ListItem>
                                                                                                                            <asp:ListItem>41</asp:ListItem>
                                                                                                                            <asp:ListItem>42</asp:ListItem>
                                                                                                                            <asp:ListItem>43</asp:ListItem>
                                                                                                                            <asp:ListItem>44</asp:ListItem>
                                                                                                                            <asp:ListItem>45</asp:ListItem>
                                                                                                                            <asp:ListItem>46</asp:ListItem>
                                                                                                                            <asp:ListItem>47</asp:ListItem>
                                                                                                                            <asp:ListItem>48</asp:ListItem>
                                                                                                                            <asp:ListItem>49</asp:ListItem>
                                                                                                                            <asp:ListItem>50</asp:ListItem>
                                                                                                                            <asp:ListItem>51</asp:ListItem>
                                                                                                                            <asp:ListItem>52</asp:ListItem>
                                                                                                                            <asp:ListItem>53</asp:ListItem>
                                                                                                                            <asp:ListItem>54</asp:ListItem>
                                                                                                                            <asp:ListItem>55</asp:ListItem>
                                                                                                                            <asp:ListItem>56</asp:ListItem>
                                                                                                                            <asp:ListItem>57</asp:ListItem>
                                                                                                                            <asp:ListItem>58</asp:ListItem>
                                                                                                                            <asp:ListItem>59</asp:ListItem>
                                                                                                                        </asp:DropDownList>
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblCodigoProductoDosificacion" runat="server" class="form-control" name="codigoP"></asp:Label>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblDescProductoDosificacion" runat="server" class="form-control" name="descP"></asp:Label>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblRestante" runat="server" class="form-control"></asp:Label>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:TextBox ID="txtCantidadDosificacion" runat="server" class="form-control" name="cantPrd" onkeypress="return onlyDotsAndNumbers(event)"></asp:TextBox>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblUDM1Dosificacion" runat="server"></asp:Label>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <%--<asp:Label ID="lblRevenimientoDosificacion" runat="server" Visible="false"></asp:Label>--%>
                                                                                                            </td>
                                                                                                            <td colspan="5">
                                                                                                                <asp:Button ID="btnCrearOD" runat="server" class="btn btn-info" OnClick="btnCrearOD_Click" Text="Insertar" />
                                                                                                                <asp:Button ID="btnModOD" runat="server" class="btn btn-info" Text="Modificar" Visible="False" OnClick="btnModOD_Click" />
                                                                                                            </td>
                                                                                                            <td>&nbsp;</td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </asp:Panel>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>&nbsp;</td>
                                                                                            <td>&nbsp;</td>
                                                                                            <td>&nbsp;</td>
                                                                                            <td>&nbsp;</td>
                                                                                            <td>&nbsp;</td>
                                                                                            <td>&nbsp;</td>
                                                                                            <td>&nbsp;</td>
                                                                                            <td>&nbsp;</td>
                                                                                            <td>&nbsp;</td>
                                                                                            <td>&nbsp;</td>
                                                                                            <td>&nbsp;</td>
                                                                                            <td>&nbsp;</td>
                                                                                            <td>&nbsp;</td>
                                                                                            <td>&nbsp;</td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>&nbsp;</td>
                                                                                            <td>&nbsp;</td>
                                                                                            <td colspan="10">
                                                                                                <asp:ListView ID="lvOD" runat="server" OnItemCommand="lvOD_ItemCommand">
                                                                                                    <LayoutTemplate>
                                                                                                        <table id="responsiveTable1" class="table table-striped table-bordered no-margin">
                                                                                                            <thead>
                                                                                                                <tr>
                                                                                                                    <th id="fecha" style="text-align: center">Fecha de Entrega</th>
                                                                                                                    <th id="hora" style="text-align: center">Hora</th>
                                                                                                                    <th id="folio" style="text-align: center">Folio</th>
                                                                                                                    <th id="orden" style="text-align: center">Orden</th>
                                                                                                                    <th id="codigo" style="text-align: center">Código del Producto</th>
                                                                                                                    <th id="descripción">Descripción</th>
                                                                                                                    <th id="cantidad" style="text-align: center">Cantidad Ordenada</th>
                                                                                                                    <th id="unidad" style="text-align: center">Unidad</th>
                                                                                                                    <th id="modificar" style="text-align: center">Modificar</th>
                                                                                                                    <th id="eliminar" style="text-align: center">Eliminar</th>
                                                                                                                </tr>
                                                                                                            </thead>
                                                                                                            <tr id="itemPlaceholder" runat="server">
                                                                                                            </tr>
                                                                                                            <tfoot>
                                                                                                                <tr>
                                                                                                                    <th style="text-align: center">Fecha de Entrega</th>
                                                                                                                    <th style="text-align: center">Hora</th>
                                                                                                                    <th style="text-align: center">Folio</th>
                                                                                                                    <th style="text-align: center">Orden</th>
                                                                                                                    <th style="text-align: center">Código del Producto</th>
                                                                                                                    <th>Descripción</th>
                                                                                                                    <th style="text-align: center">Cantidad Ordenada</th>
                                                                                                                    <th style="text-align: center">Unidad</th>
                                                                                                                    <th style="text-align: center">Modificar</th>
                                                                                                                    <th style="text-align: center">Eliminar</th>
                                                                                                                </tr>
                                                                                                            </tfoot>
                                                                                                        </table>
                                                                                                    </LayoutTemplate>
                                                                                                    <ItemTemplate>
                                                                                                        <tr>
                                                                                                            <td style="text-align: center"><%# Eval("fecha").ToString().Substring(0,10) %></td>
                                                                                                            <td style="text-align: center"><%# Eval("hora") %></td>
                                                                                                            <td style="text-align: center"><%# Eval("folio") %></td>
                                                                                                            <td style="text-align: center"><%# Eval("idOrden") %></td>
                                                                                                            <td style="text-align: center"><%# Eval("codigo") %></td>
                                                                                                            <td><%# Eval("descripcion") %></td>
                                                                                                            <td style="text-align: center"><%# Eval("cantidad") %></td>
                                                                                                            <td style="text-align: center"><%# Eval("unidad") %></td>
                                                                                                            <td style="text-align: center">
                                                                                                                <asp:LinkButton ID="lbtnModOD" runat="server" CommandArgument='<%# Eval("id") + "ˇ" + Eval("fecha") + "ˇ" + Eval("hora") + "ˇ" + Eval("folio") + "ˇ" + Eval("idOrden") + "ˇ" + Eval("codigo") + "ˇ" + Eval("descripcion") + "ˇ" + Eval("cantidad") + "ˇ" + Eval("unidad") + "ˇ" + Eval("idP") + "ˇ" + Eval("idTipoProducto") + "ˇ" + Eval("tipoP") %>' CommandName="modificarOD"><i class="icon-edit"></i></asp:LinkButton>
                                                                                                            </td>
                                                                                                            <td style="text-align: center">
                                                                                                                <asp:LinkButton ID="lbtnEliminarOD" runat="server" CommandArgument='<%# Eval("id")+ "ˇ" + Eval("folio") %>' CommandName="eliminarOD"><i class="icon-delete"></i></asp:LinkButton>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </ItemTemplate>
                                                                                                    <EmptyDataTemplate>
                                                                                                        <table id="responsiveTable2" class="table table-striped table-bordered no-margin">
                                                                                                            <thead>
                                                                                                                <tr>
                                                                                                                    <th style="text-align: center">Fecha de Entrega</th>
                                                                                                                    <th style="text-align: center">Hora</th>
                                                                                                                    <th style="text-align: center">Folio</th>
                                                                                                                    <th style="text-align: center">Orden</th>
                                                                                                                    <th style="text-align: center">Código del Producto</th>
                                                                                                                    <th>Descripción</th>
                                                                                                                    <th style="text-align: center">Cantidad Ordenada</th>
                                                                                                                    <th style="text-align: center">Unidad</th>
                                                                                                                    <th style="text-align: center">Modificar</th>
                                                                                                                    <th style="text-align: center">Eliminar</th>
                                                                                                                </tr>
                                                                                                            </thead>
                                                                                                            <tr>
                                                                                                                <td colspan="10">
                                                                                                                    <label class="label label-danger">
                                                                                                                        ¡No hay Ordenes de Entrega Registradas para ésta orden!</label></td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </EmptyDataTemplate>
                                                                                                </asp:ListView>
                                                                                                <td>&nbsp;</td>
                                                                                                <td>&nbsp;</td>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>&nbsp;</td>
                                                                                            <td>&nbsp;</td>
                                                                                            <td>&nbsp;</td>
                                                                                            <td>&nbsp;</td>
                                                                                            <td>&nbsp;</td>
                                                                                            <td>&nbsp;</td>
                                                                                            <td>&nbsp;</td>
                                                                                            <td>&nbsp;</td>
                                                                                            <td>&nbsp;</td>
                                                                                            <td>&nbsp;</td>
                                                                                            <td>&nbsp;</td>
                                                                                            <td>&nbsp;</td>
                                                                                            <td>&nbsp;</td>
                                                                                            <td>&nbsp;</td>
                                                                                        </tr>
                                                                                    </tr>
                                                                                </table>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </asp:Panel>


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
                                                                        <asp:Label ID="lblAux" runat="server" />
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
                                                    <td class="progress-md"></td>
                                                    <td class="progress-md"></td>
                                                    <td class="progress-md"></td>
                                                    <td class="progress-md"></td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <asp:Button ID="btnTerminar" runat="server" class="btn btn-info" OnClick="btnTerminar_Click" Text="Terminar" />
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
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
                                                    <asp:HiddenField ID="hfIdOrden" runat="server" />
                                                    <asp:HiddenField ID="hfIdOD" runat="server" />
                                                    <asp:HiddenField ID="hfIdSucursal" runat="server" />
                                                    <asp:HiddenField ID="hfIdSolicitud" runat="server" />
                                                    <asp:HiddenField ID="hfIdDS" runat="server" />
                                                    <asp:HiddenField ID="hfDSQty" runat="server" Value="0" />
                                                    <asp:HiddenField ID="hfIdV2" runat="server" />
                                                    <asp:HiddenField ID="hfIdProducto" runat="server" />
                                                    <asp:HiddenField ID="hfIdUDM" runat="server" />
                                                    <asp:HiddenField ID="hfCapMaxUnidad" runat="server" />
                                                    <asp:HiddenField ID="hfIdES" runat="server" />
                                                    <asp:HiddenField ID="hfEnableMod" runat="server" />
                                                    <asp:HiddenField ID="hfRestan" runat="server" Value="0" />
                                                    <asp:HiddenField ID="hfCantRecomendada" runat="server" Value="0" />
                                                    <asp:HiddenField ID="hfCantMod" runat="server" Value="0" />
                                                    <asp:HiddenField ID="hfFolioOD" runat="server" />
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
                        <asp:AsyncPostBackTrigger ControlID="lvDetalles" EventName="ItemCommand" />
                        <asp:AsyncPostBackTrigger ControlID="btnCrearOD" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnModOD" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="lvOD" EventName="ItemCommand" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!-- Row Ends -->
</asp:Content>
