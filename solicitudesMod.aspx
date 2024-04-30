<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="solicitudesMod.aspx.cs" Inherits="despacho.solicitudesMod" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    &nbsp;<!-- Top Bar Starts --><asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
    <div class="top-bar clearfix">
        <div class="row gutter">
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <div class="page-title">
                    <h3>Modificar Solicitud</h3>
                    <p>/ <a href="solicitudes.aspx">Solicitudes</a></p>
                </div>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <ul class="right-stats" id="mini-nav-right">
                    <%# Eval("cantidad") %>
                    <li>
                        <a href="home.aspx" class="btn btn-info">
                            <i class="icon-bulb"></i>Ayuda
                        </a>
                        <%# Eval("subtotal") %>
                    </li>
                </ul>
            </div>
            <%# Eval("iva") %>
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
                                    <div class="row gutter">
                                        <asp:Panel ID="Panel1" runat="server">
                                            <div class="form-group">
                                                <asp:Panel ID="pnlSolicitud" runat="server">
                                                    <div class="panel-heading">
                                                        <h4>Datos de la Solicitud</h4>
                                                    </div>
                                                    <div class="row gutter">
                                                        <div class="col-md-12">
                                                            <asp:Label ID="lblFoli" runat="server" class="control-label" Text="Folio:"></asp:Label>
                                                            <asp:Label ID="lblFolio" runat="server" class="form-control" name="folio"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="row gutter">
                                                        <div class="col-md-4">
                                                            <asp:Label ID="Label1" runat="server" class="control-label" Text="Fecha de Entrega:"></asp:Label>
                                                            <asp:TextBox ID="txtFecha" runat="server" class="form-control" name="fecha" TextMode="DateTime" AutoComplete="off"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="txtFecha_CalendarExtender" runat="server" BehaviorID="txtFecha_CalendarExtender" TargetControlID="txtFecha" FirstDayOfWeek="Monday" Format="yyyy/MM/dd" PopupPosition="BottomRight" />
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:Label ID="Label2" runat="server" class="control-label" Text="Hora:"></asp:Label><br />
                                                            <div class="row gutter">
                                                                <div class="col-md-4">
                                                                    <asp:DropDownList ID="cbxHora" runat="server" class="form-control">
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
                                                                    <asp:DropDownList ID="cbxMinutos" runat="server" class="form-control">
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
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:Label ID="Label3" runat="server" class="control-label" Text="Estado:"></asp:Label>
                                                            <asp:DropDownList ID="ddlEstadoSolicitud" runat="server" class="form-control" DataSourceID="dsEstadoSolicitud" DataTextField="estado" DataValueField="id">
                                                            </asp:DropDownList>
                                                            <asp:SqlDataSource ID="dsEstadoSolicitud" runat="server" ConnectionString="<%$ ConnectionStrings:cnx %>" SelectCommand="SELECT * FROM [estadosSolicitud]"></asp:SqlDataSource>
                                                        </div>
                                                         <div class="col-md-4">
                                                            <asp:Label ID="Label28" runat="server" class="control-label" Text="Requiere Factura:"></asp:Label>
                                                            <asp:DropDownList ID="ddlReqFac" runat="server" class="form-control">
                                                                <asp:ListItem Value="">Seleccionar</asp:ListItem>
                                                                <asp:ListItem Value="SI">Si</asp:ListItem>
                                                                <asp:ListItem Value="NO">No</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                                <br />
                                                <asp:Panel ID="Panel3" runat="server">
                                                    <div class="panel-heading">
                                                        <h4>Datos del Cliente</h4>
                                                    </div>
                                                    <div class="row gutter">
                                                        <div class="col-md-3">
                                                            <asp:Label ID="Label5" runat="server" class="control-label" Text="Clave:"></asp:Label>
                                                            <asp:TextBox ID="txtClaveCliente" runat="server" class="form-control" name="clave" OnTextChanged="txtClaveCliente_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                            <ajaxToolkit:AutoCompleteExtender ID="txtClaveCliente_AutoCompleteExtender" runat="server" BehaviorID="txtClaveCliente_AutoCompleteExtender" DelimiterCharacters="" ServicePath="" TargetControlID="txtClaveCliente" ServiceMethod="getDataClaveCliente" MinimumPrefixLength="1" CompletionSetCount="1" UseContextKey="True">
                                                            </ajaxToolkit:AutoCompleteExtender>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:Label ID="Label8" runat="server" class="control-label" Text="Nombre:"></asp:Label>
                                                            <asp:TextBox ID="txtNombreCliente" runat="server" class="form-control" OnTextChanged="txtNombreCliente_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                            <ajaxToolkit:AutoCompleteExtender ID="txtNombreCliente_AutoCompleteExtender" runat="server" BehaviorID="TtxtNombreCliente_AutoCompleteExtender" DelimiterCharacters="" ServicePath="" TargetControlID="txtNombreCliente" ServiceMethod="getDataNombreCliente" MinimumPrefixLength="1" CompletionSetCount="1" UseContextKey="True">
                                                            </ajaxToolkit:AutoCompleteExtender>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:Label ID="Label9" runat="server" class="control-label" Text="Forma de Pago:"></asp:Label>
                                                            <asp:DropDownList ID="ddlFP" runat="server" class="form-control" AutoPostBack="True" DataTextField="nombre" DataValueField="id" OnSelectedIndexChanged="ddlFP_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:Label ID="Label10" runat="server" class="control-label" Text="Vendedor:"></asp:Label>
                                                            <asp:DropDownList ID="ddlVendedores" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlVendedores_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                                <br />
                                                <asp:Panel ID="Panel2" runat="server">
                                                    <div class="panel-heading">
                                                        <h4>Datos del Proyecto</h4>
                                                    </div>
                                                    <div class="row gutter">
                                                        <div class="col-md-3">
                                                            <asp:Label ID="lblProyecto" runat="server" class="control-label" Text="" Visible="false"></asp:Label>
                                                            <asp:Label ID="Label4" runat="server" class="control-label" Text="Proyecto:"></asp:Label>
                                                            <asp:DropDownList ID="ddlProyectos" runat="server" class="form-control" DataTextField="tipo" DataValueField="id" OnSelectedIndexChanged="ddlProyectos_SelectedIndexChanged" AutoPostBack="True">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:Label ID="Label6" runat="server" class="control-label" Text="Código Postal:"></asp:Label>
                                                            <asp:TextBox ID="txtCP" runat="server" class="form-control" MaxLength="5" onkeypress="return onlyNumbers(event)" AutoPostBack="True" OnTextChanged="txtCP_TextChanged"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            Estado:
                                                <asp:DropDownList ID="ddlEstados" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlEstados_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                        </div>
                                                        <div class="col-md-3">
                                                            Ciudad:
                                                <asp:DropDownList ID="ddlCiudades" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlCiudades_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="row gutter">
                                                        <div class="col-md-3">
                                                            <asp:Label ID="Label7" runat="server" class="control-label" Text="Colonia:"></asp:Label><br />
                                                            <ajaxToolkit:ComboBox ID="cbxColonias" runat="server" AutoCompleteMode="SuggestAppend" AutoPostBack="True" DropDownStyle="Simple" name="colonia" DataTextField="asenta" DataValueField="asenta" OnSelectedIndexChanged="cbxColonias_SelectedIndexChanged">
                                                            </ajaxToolkit:ComboBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:Label ID="Label11" runat="server" class="control-label" Text="Calle:"></asp:Label>
                                                            <asp:TextBox ID="txtCalle" runat="server" class="form-control" name="calle"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:Label ID="Label12" runat="server" class="control-label" Text="Número:"></asp:Label>
                                                            <asp:TextBox ID="txtNumero" runat="server" class="form-control" name="numero" onkeypress="return onlyNumbers(event)" AutoComplete="off"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:Label ID="Label13" runat="server" class="control-label" Text="Interior:"></asp:Label>
                                                            <asp:TextBox ID="txtInterior" runat="server" class="form-control" name="interior" MaxLength="5" AutoComplete="off"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                                <br />
                                                <asp:Panel ID="Panel4" runat="server">
                                                    <div class="row gutter">
                                                        <div class="col-md-12">
                                                            <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="row gutter">
                                                        <div class="col-md-4">
                                                            <asp:Button ID="btnAgregarDetalle" runat="server" OnClick="btnAgregarDetalle_Click" Text="Modificar Solicitud" class="btn btn-info" />
                                                            &nbsp;<asp:Button ID="btnCancelar" runat="server" class="btn btn-info" PostBackUrl="~/solicitudes.aspx" Text="Cancelar" />
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </asp:Panel>
                                        <%--<asp:UpdatePanel ID="upGrid" runat="server">
                                            <ContentTemplate>--%>
                                        <asp:Panel ID="pnlDetalleProductos" runat="server" BorderWidth="1px" Visible="False">
                                            <div class="panel-heading">
                                                <h4></h4>
                                            </div>
                                            <div class="row gutter">
                                                <div class="col-md-1">
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:Label ID="Label19" runat="server" class="control-label" Text="Tipo:"></asp:Label>
                                                    <asp:DropDownList ID="ddlTipoProducto" runat="server" class="form-control" DataTextField="tipo" DataValueField="id" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoProducto_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:Label ID="lblElementos" runat="server" class="control-label" Text="Elemento:" Visible="False"></asp:Label>
                                                    <asp:DropDownList ID="ddlElemento" runat="server" class="form-control" DataTextField="elemento" DataValueField="id" AutoPostBack="True" OnSelectedIndexChanged="ddlElemento_SelectedIndexChanged" Visible="False"></asp:DropDownList>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:Label ID="Label17" runat="server" class="control-label" Text="Código:"></asp:Label>
                                                    <asp:TextBox ID="txtCodigoProducto" runat="server" class="form-control" name="codigoP" AutoPostBack="True" OnTextChanged="txtCodigoProducto_TextChanged"></asp:TextBox>
                                                    <ajaxToolkit:AutoCompleteExtender ID="txtCodigoProducto_AutoCompleteExtender" runat="server" BehaviorID="txtCodigoProducto_AutoCompleteExtender" DelimiterCharacters="" ContextKey="" ServiceMethod="getCodigoProductos" TargetControlID="txtCodigoProducto" MinimumPrefixLength="1" CompletionSetCount="1" UseContextKey="true">
                                                    </ajaxToolkit:AutoCompleteExtender>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:Label ID="Label18" runat="server" class="control-label" Text="Descripción:"></asp:Label>
                                                    <asp:TextBox ID="txtDescProducto" runat="server" class="form-control" name="descP"></asp:TextBox>
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:Label ID="Label25" runat="server" class="control-label" Text="Unidad:"></asp:Label><br />
                                                    <asp:Label ID="lblUDM" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:Label ID="lblRevenimiento" runat="server" class="control-label" Text="Revenimiento:" Visible="False"></asp:Label>
                                                    <asp:TextBox ID="txtRevenimiento" runat="server" class="form-control" name="Revenimiento" onkeypress="return onlyNumbers(event)" Visible="False"></asp:TextBox>
                                                </div>
                                                <div class="col-md-1">
                                                </div>
                                            </div>
                                            <div class="row gutter">
                                                <div class="col-md-1">
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:Label ID="Label22" runat="server" class="control-label" Text="Precio Unitario:"></asp:Label>
                                                    <asp:Label ID="lblPrecioU" runat="server" class="form-control" Text=""></asp:Label>
                                                    <asp:Label ID="lblPrecioU2" runat="server" class="form-control" Text="" Visible="false"></asp:Label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:Label ID="Label15" runat="server" class="control-label" Text="Factor:"></asp:Label>
                                                    <asp:DropDownList ID="ddlFactor" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlFactor_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:Label ID="Label16" runat="server" class="control-label" Text="Precio con Factor:"></asp:Label>
                                                    <asp:TextBox ID="txtPrecioFactor" runat="server" class="form-control" name="precioF" AutoPostBack="true" onkeypress="return onlyDotsAndNumbers(event)" OnTextChanged="txtPrecioFactor_TextChanged"></asp:TextBox>
                                                    <asp:Label ID="txtPrecioFactor2" runat="server" class="form-control" Text="" Visible="false"></asp:Label>
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:Label ID="Label14" runat="server" class="control-label" Text="Cantidad:"></asp:Label>
                                                    <asp:TextBox ID="txtCantOrdenada" runat="server" class="form-control" name="cantPrd" onkeypress="return onlyDotsAndNumbers(event)" AutoPostBack="True" OnTextChanged="txtCantOrdenada_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:Label ID="Label23" runat="server" class="control-label" Text="Sub-Total:"></asp:Label>
                                                    <asp:Label ID="lblSubTotal" runat="server" class="form-control" Text=""></asp:Label>
                                                    <asp:Label ID="lblSubTotal2" runat="server" class="form-control" Text="" Visible="false"></asp:Label>
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:Label ID="Label24" runat="server" class="control-label" Text="IVA:"></asp:Label>
                                                    <asp:TextBox ID="txtIVA" runat="server" class="form-control" name="iva" AutoPostBack="True" OnTextChanged="txtIVA_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:Label ID="Label20" runat="server" class="control-label" Text="Total:"></asp:Label>
                                                    <asp:Label ID="lblTotal" runat="server" class="form-control" Text=""></asp:Label>
                                                    <asp:Label ID="lblTotal2" runat="server" class="form-control" Text="" Visible="false"></asp:Label>
                                                </div>
                                                <div class="col-md-1">
                                                    <br />
                                                    <asp:Button ID="btnInsertarProducto" runat="server" Text="Insertar" class="btn btn-info" OnClick="btnInsertarProducto_Click" />
                                                    <asp:Button ID="btnModProducto" runat="server" class="btn btn-info" OnClick="btnModProducto_Click" Text="Modificar" Visible="False" />
                                                </div>
                                                <div class="col-md-1">
                                                </div>
                                            </div>
                                            <div class="row gutter">
                                                <div class="col-md-12">
                                                    <div id="ContentPlaceHolder_upGrid">
                                                        <div class="row gutter">
                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                                <div class="panel panel-blue">
                                                                    <div class="panel-heading">
                                                                        <h4>Detalle</h4>
                                                                    </div>
                                                                    <div class="panel-body">
                                                                        <div class="table-responsive">

                                                                            <asp:ListView ID="lvDetalles" runat="server" OnItemCommand="lvDetalles_ItemCommand" OnItemDeleting="lvDetalles_ItemDeleting">
                                                                                <LayoutTemplate>
                                                                                    <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                                                                        <thead>
                                                                                            <tr>
                                                                                                <th style="text-align: center" id="tipo">Tipo</th>
                                                                                                <th style="text-align: center" id="elemento">Elemento</th>
                                                                                                <th style="text-align: center" id="codigo">Código</th>
                                                                                                <th id="descripción">Descripción</th>
                                                                                                <th style="text-align: center" id="unidad">Unidad</th>
                                                                                                <th style="text-align: center" id="Revenimiento">Revenimiento</th>
                                                                                                <th style="text-align: center" id="precioU">Precio Unitario</th>
                                                                                                <th style="text-align: center" id="factor">Factor</th>
                                                                                                <th style="text-align: center" id="precioF">Precio Factor</th>
                                                                                                <th style="text-align: center" id="cantidad">Cantidad</th>
                                                                                                <th style="text-align: center" id="subTotal">Sub-Total</th>
                                                                                                <th style="text-align: center" id="iva">IVA</th>
                                                                                                <th style="text-align: center" id="total">Total</th>
                                                                                                <th style="text-align: center" id="modificar">Modificar</th>
                                                                                                <th style="text-align: center" id="eliminar">Eliminar</th>
                                                                                            </tr>
                                                                                        </thead>
                                                                                        <tr id="itemPlaceholder" runat="server"></tr>
                                                                                        <tfoot>
                                                                                            <tr>
                                                                                                <th style="text-align: center">Tipo</th>
                                                                                                <th style="text-align: center">Elemento</th>
                                                                                                <th style="text-align: center">Código</th>
                                                                                                <th>Descripción</th>
                                                                                                <th style="text-align: center">Unidad</th>
                                                                                                <th style="text-align: center">Revenimiento</th>
                                                                                                <th style="text-align: center">Precio Unitario</th>
                                                                                                <th style="text-align: center">Factor</th>
                                                                                                <th style="text-align: center">Precio Factor</th>
                                                                                                <th style="text-align: center">Cantidad</th>
                                                                                                <th style="text-align: center">Sub-Total</th>
                                                                                                <th style="text-align: center">IVA</th>
                                                                                                <th style="text-align: center">Total</th>
                                                                                                <th style="text-align: center">Modificar</th>
                                                                                                <th style="text-align: center">Eliminar</th>
                                                                                            </tr>
                                                                                        </tfoot>
                                                                                    </table>
                                                                                </LayoutTemplate>
                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                        <td style="text-align: center"><%# Eval("tipo") %></td>
                                                                                        <td style="text-align: center"><%# Eval("elemento") %></td>
                                                                                        <td style="text-align: center"><%# Eval("codigo") %></td>
                                                                                        <td><%# Eval("descripcion") %></td>
                                                                                        <td style="text-align: center"><%# Eval("unidad") %></td>
                                                                                        <td style="text-align: center"><%# Eval("revenimiento") %></td>
                                                                                        <td style="text-align: center"><%# Eval("precioU") %></td>
                                                                                        <td style="text-align: center"><%# Eval("factor") + " - " + Eval("porcentaje") + "%" %></td>
                                                                                        <td style="text-align: center"><%# Eval("precioF") %></td>
                                                                                        <td style="text-align: center"><%# Eval("cantidad") %></td>
                                                                                        <td style="text-align: center"><%# Eval("subtotal") %></td>
                                                                                        <td style="text-align: center"><%# Eval("iva") %></td>
                                                                                        <td style="text-align: center"><%# Eval("total") %></td>
                                                                                        <td style="text-align: center">
                                                                                            <asp:LinkButton ID="lbtnMod" runat="server" CommandArgument='<%# Eval("id") + "ˇ" + Eval("idTipoProducto") + "ˇ" + Eval("idElemento") + "ˇ" + Eval("codigo") + "ˇ" + Eval("descripcion") + "ˇ" + Eval("unidad") + "ˇ" + Eval("revenimiento") + "ˇ" + Eval("precioU") + "ˇ" + Eval("idFactor") + "ˇ" + Eval("precioF") + "ˇ" + Eval("cantidad") + "ˇ" + Eval("subtotal") + "ˇ" + Eval("iva") + "ˇ" + Eval("total") %>' CommandName="modificar"><i class="icon-edit"></i></asp:LinkButton>
                                                                                        </td>
                                                                                        <td style="text-align: center">
                                                                                            <asp:LinkButton ID="lbtnEliminar" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="eliminar"><i class="icon-delete"></i></asp:LinkButton>
                                                                                        </td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                                <EmptyDataTemplate>
                                                                                    <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                                                                        <thead>
                                                                                            <tr>
                                                                                                <th style="text-align: center" id="tipo">Tipo</th>
                                                                                                <th style="text-align: center" id="elemento">Elemento</th>
                                                                                                <th style="text-align: center" id="codigo">Código</th>
                                                                                                <th id="descripción">Descripción</th>
                                                                                                <th style="text-align: center" id="unidad">Unidad</th>
                                                                                                <th style="text-align: center" id="Revenimiento">Revenimiento</th>
                                                                                                <th style="text-align: center" id="precioU">Precio Unitario</th>
                                                                                                <th style="text-align: center" id="factor">Factor</th>
                                                                                                <th style="text-align: center" id="precioF">Precio Factor</th>
                                                                                                <th style="text-align: center" id="cantidad">Cantidad</th>
                                                                                                <th style="text-align: center" id="subTotal">Sub-Total</th>
                                                                                                <th style="text-align: center" id="iva">IVA</th>
                                                                                                <th style="text-align: center" id="total">Total</th>
                                                                                                <th style="text-align: center" id="modificar">Modificar</th>
                                                                                                <th style="text-align: center" id="eliminar">Eliminar</th>
                                                                                            </tr>
                                                                                        </thead>
                                                                                        <tr>
                                                                                            <td colspan="15">
                                                                                                <label class="label label-danger">¡No hay Productos Registrados!</label></td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </EmptyDataTemplate>
                                                                            </asp:ListView>
                                                                        </div>
                                                                        <asp:Button ID="btnFinalizar" runat="server" class="btn btn-info"  PostBackUrl="~/solicitudes.aspx" Text="Terminar" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <%--</ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddlTipoProducto" EventName="SelectedIndexChanged" />
                                                <asp:AsyncPostBackTrigger ControlID="ddlElemento" EventName="SelectedIndexChanged" />
                                                <asp:AsyncPostBackTrigger ControlID="txtCodigoProducto" EventName="TextChanged" />
                                                <asp:AsyncPostBackTrigger ControlID="ddlFactor" EventName="SelectedIndexChanged" />
                                                <asp:AsyncPostBackTrigger ControlID="txtPrecioFactor" EventName="TextChanged" />
                                                <asp:AsyncPostBackTrigger ControlID="txtCantOrdenada" EventName="TextChanged" />
                                                <asp:AsyncPostBackTrigger ControlID="btnInsertarProducto" EventName="Click" />
                                                <asp:AsyncPostBackTrigger ControlID="btnModProducto" EventName="Click" />
                                                <asp:AsyncPostBackTrigger ControlID="lvDetalles" EventName="ItemCommand" />
                                            </Triggers>
                                        </asp:UpdatePanel>--%>
                                    </div>
                                             <!-- Modal -->
                                                <div class="modal fade" id="errorModal" tabindex="-1" role="dialog" aria-labelledby="errorModalLabel" aria-hidden="true">
                                                  <div class="modal-dialog modal-dialog-centered" role="document">
                                                    <div class="modal-content">
                                                      <div class="modal-header bg-primary text-white">
                                                        <h5 class="modal-title" id="errorModalLabel">Mensaje:</h5>
               
                                                        </button>
                                                      </div>
                                                      <div class="modal-body display-3">
                                                        <p id="errorMessage" ></p>
                                                      </div>
                                                      <div class="modal-footer">
                                                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                                                      </div>
                                                    </div>
                                                  </div>
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
                                    <div class="form-group">
                                        <div class="row gutter">
                                            <div class="col-md-8">
                                                &nbsp;<asp:HiddenField ID="hfIdCliente" runat="server" />
                                                <asp:HiddenField ID="hfIdVendedor" runat="server" />
                                                <asp:HiddenField ID="hfIdProyecto" runat="server" />
                                                <asp:HiddenField ID="hfIdSolicitud" runat="server" />
                                                <asp:HiddenField ID="hfIdDS" runat="server" />
                                                <asp:HiddenField ID="hfIdFP" runat="server" />
                                                <asp:HiddenField ID="hfIdProducto" runat="server" />
                                                <asp:HiddenField ID="hfIdElemento" runat="server" />
                                                <asp:HiddenField ID="hfIdEstado" runat="server" />
                                                <asp:HiddenField ID="hfIdCiudad" runat="server" />
                                                <asp:HiddenField ID="hfIdEstadoCP" runat="server" />
                                                <asp:HiddenField ID="hfIdCiudadCP" runat="server" />
                                                <asp:HiddenField ID="hfSearchBy" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <br />
                                </div>
                        </div>
                        </form>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="txtClaveCliente" EventName="TextChanged" />
                        <asp:AsyncPostBackTrigger ControlID="txtNombreCliente" EventName="TextChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlFP" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlVendedores" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlProyectos" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="txtCP" EventName="TextChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlEstados" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlCiudades" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="cbxColonias" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="btnAgregarDetalle" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="ddlTipoProducto" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlElemento" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="txtCodigoProducto" EventName="TextChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlFactor" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="txtPrecioFactor" EventName="TextChanged" />
                        <asp:AsyncPostBackTrigger ControlID="txtCantOrdenada" EventName="TextChanged" />
                        <asp:AsyncPostBackTrigger ControlID="btnInsertarProducto" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnModProducto" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="lvDetalles" EventName="ItemCommand" />
                        <asp:AsyncPostBackTrigger ControlID="txtIVA" EventName="TextChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!-- Row Ends -->

</asp:Content>
