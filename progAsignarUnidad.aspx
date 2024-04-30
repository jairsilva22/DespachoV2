<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="progAsignarUnidad.aspx.cs" Inherits="despacho.progAsignarUnidad" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:Panel ID="Panel1" runat="server">
                <asp:Label ID="Label1" runat="server" Text="Orden de Dosificación: "></asp:Label>
                <asp:Label ID="lblOD" runat="server"></asp:Label>
                <br />
                <div class="panel-body">
                    <div class="table-responsive">
                        <asp:Label ID="lblIDRemision" runat="server" Text=""></asp:Label>
                        <asp:ListView ID="lvUT" runat="server" ItemPlaceholderID="itemPlaceHolderTU" DataKeyNames="id" DataSourceID="SqlDataSource1">
                            <LayoutTemplate>
                                <table runat="server">
                                    <tr runat="server">
                                        <td runat="server">
                                            <table id="itemPlaceholderContainer" runat="server" border="0" style="">
                                                <tr runat="server" style="">
                                                    <th runat="server">id</th>
                                                    <th runat="server">idOrden</th>
                                                    <th runat="server">fecha</th>
                                                    <th runat="server">hora</th>
                                                    <th runat="server">codigo</th>
                                                    <th runat="server">descripcion</th>
                                                    <th runat="server">cantidad</th>
                                                    <th runat="server">unidad</th>
                                                    <th runat="server">revenimiento</th>
                                                    <th runat="server">tipo</th>
                                                    <th runat="server">folio</th>
                                                    <th runat="server">nombre</th>
                                                    <th runat="server">calle</th>
                                                    <th runat="server">numero</th>
                                                    <th runat="server">interior</th>
                                                    <th runat="server">colonia</th>
                                                    <th runat="server">cp</th>
                                                    <th runat="server">clave</th>
                                                    <th runat="server">cliente</th>
                                                    <th runat="server">idEstadoDosificacion</th>
                                                    <th runat="server">estado</th>
                                                    <th runat="server">porcentaje</th>
                                                    <th runat="server">cantidadEntregada</th>
                                                    <th runat="server">uTransporte</th>
                                                    <th runat="server">color</th>
                                                    <th runat="server">chofer</th>
                                                </tr>
                                                <tr id="itemPlaceHolderTU" runat="server">
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr runat="server">
                                        <td runat="server" style=""></td>
                                    </tr>
                                </table>
                            </LayoutTemplate>
                            <AlternatingItemTemplate>
                                <tr style="">
                                    <td>
                                        <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="idOrdenLabel" runat="server" Text='<%# Eval("idOrden") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="fechaLabel" runat="server" Text='<%# Eval("fecha") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="horaLabel" runat="server" Text='<%# Eval("hora") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="codigoLabel" runat="server" Text='<%# Eval("codigo") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="descripcionLabel" runat="server" Text='<%# Eval("descripcion") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="cantidadLabel" runat="server" Text='<%# Eval("cantidad") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="unidadLabel" runat="server" Text='<%# Eval("unidad") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="RevenimientoLabel" runat="server" Text='<%# Eval("revenimiento") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="tipoLabel" runat="server" Text='<%# Eval("tipo") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="folioLabel" runat="server" Text='<%# Eval("folio") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="nombreLabel" runat="server" Text='<%# Eval("nombre") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="calleLabel" runat="server" Text='<%# Eval("calle") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="numeroLabel" runat="server" Text='<%# Eval("numero") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="interiorLabel" runat="server" Text='<%# Eval("interior") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="coloniaLabel" runat="server" Text='<%# Eval("colonia") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="cpLabel" runat="server" Text='<%# Eval("cp") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="claveLabel" runat="server" Text='<%# Eval("clave") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="clienteLabel" runat="server" Text='<%# Eval("cliente") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="idEstadoDosificacionLabel" runat="server" Text='<%# Eval("idEstadoDosificacion") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="estadoLabel" runat="server" Text='<%# Eval("estado") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="porcentajeLabel" runat="server" Text='<%# Eval("porcentaje") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="cantidadEntregadaLabel" runat="server" Text='<%# Eval("cantidadEntregada") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="uTransporteLabel" runat="server" Text='<%# Eval("uTransporte") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="colorLabel" runat="server" Text='<%# Eval("color") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="choferLabel" runat="server" Text='<%# Eval("chofer") %>' />
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                            <EditItemTemplate>
                                <tr style="">
                                    <td>
                                        <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Actualizar" />
                                        <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancelar" />
                                    </td>
                                    <td>
                                        <asp:Label ID="idLabel1" runat="server" Text='<%# Eval("id") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="idOrdenTextBox" runat="server" Text='<%# Bind("idOrden") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="fechaTextBox" runat="server" Text='<%# Bind("fecha") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="horaTextBox" runat="server" Text='<%# Bind("hora") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="codigoTextBox" runat="server" Text='<%# Bind("codigo") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="descripcionTextBox" runat="server" Text='<%# Bind("descripcion") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="cantidadTextBox" runat="server" Text='<%# Bind("cantidad") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="unidadTextBox" runat="server" Text='<%# Bind("unidad") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="RevenimientoTextBox" runat="server" Text='<%# Bind("revenimiento") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tipoTextBox" runat="server" Text='<%# Bind("tipo") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="folioTextBox" runat="server" Text='<%# Bind("folio") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="nombreTextBox" runat="server" Text='<%# Bind("nombre") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="calleTextBox" runat="server" Text='<%# Bind("calle") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="numeroTextBox" runat="server" Text='<%# Bind("numero") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="interiorTextBox" runat="server" Text='<%# Bind("interior") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="coloniaTextBox" runat="server" Text='<%# Bind("colonia") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="cpTextBox" runat="server" Text='<%# Bind("cp") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="claveTextBox" runat="server" Text='<%# Bind("clave") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="clienteTextBox" runat="server" Text='<%# Bind("cliente") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="idEstadoDosificacionTextBox" runat="server" Text='<%# Bind("idEstadoDosificacion") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="estadoTextBox" runat="server" Text='<%# Bind("estado") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="porcentajeTextBox" runat="server" Text='<%# Bind("porcentaje") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="cantidadEntregadaTextBox" runat="server" Text='<%# Bind("cantidadEntregada") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="uTransporteTextBox" runat="server" Text='<%# Bind("uTransporte") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="colorTextBox" runat="server" Text='<%# Bind("color") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="choferTextBox" runat="server" Text='<%# Bind("chofer") %>' />
                                    </td>
                                </tr>
                            </EditItemTemplate>
                            <EmptyDataTemplate>
                                <table runat="server" style="">
                                    <tr>
                                        <td>No se han devuelto datos.</td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                            <InsertItemTemplate>
                                <tr style="">
                                    <td>
                                        <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Insertar" />
                                        <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Borrar" />
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>
                                        <asp:TextBox ID="idOrdenTextBox" runat="server" Text='<%# Bind("idOrden") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="fechaTextBox" runat="server" Text='<%# Bind("fecha") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="horaTextBox" runat="server" Text='<%# Bind("hora") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="codigoTextBox" runat="server" Text='<%# Bind("codigo") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="descripcionTextBox" runat="server" Text='<%# Bind("descripcion") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="cantidadTextBox" runat="server" Text='<%# Bind("cantidad") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="unidadTextBox" runat="server" Text='<%# Bind("unidad") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="RevenimientoTextBox" runat="server" Text='<%# Bind("revenimiento") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tipoTextBox" runat="server" Text='<%# Bind("tipo") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="folioTextBox" runat="server" Text='<%# Bind("folio") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="nombreTextBox" runat="server" Text='<%# Bind("nombre") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="calleTextBox" runat="server" Text='<%# Bind("calle") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="numeroTextBox" runat="server" Text='<%# Bind("numero") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="interiorTextBox" runat="server" Text='<%# Bind("interior") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="coloniaTextBox" runat="server" Text='<%# Bind("colonia") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="cpTextBox" runat="server" Text='<%# Bind("cp") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="claveTextBox" runat="server" Text='<%# Bind("clave") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="clienteTextBox" runat="server" Text='<%# Bind("cliente") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="idEstadoDosificacionTextBox" runat="server" Text='<%# Bind("idEstadoDosificacion") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="estadoTextBox" runat="server" Text='<%# Bind("estado") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="porcentajeTextBox" runat="server" Text='<%# Bind("porcentaje") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="cantidadEntregadaTextBox" runat="server" Text='<%# Bind("cantidadEntregada") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="uTransporteTextBox" runat="server" Text='<%# Bind("uTransporte") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="colorTextBox" runat="server" Text='<%# Bind("color") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="choferTextBox" runat="server" Text='<%# Bind("chofer") %>' />
                                    </td>
                                </tr>
                            </InsertItemTemplate>
                            <ItemTemplate>
                                <tr style="">
                                    <td>
                                        <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="idOrdenLabel" runat="server" Text='<%# Eval("idOrden") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="fechaLabel" runat="server" Text='<%# Eval("fecha") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="horaLabel" runat="server" Text='<%# Eval("hora") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="codigoLabel" runat="server" Text='<%# Eval("codigo") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="descripcionLabel" runat="server" Text='<%# Eval("descripcion") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="cantidadLabel" runat="server" Text='<%# Eval("cantidad") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="unidadLabel" runat="server" Text='<%# Eval("unidad") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="RevenimientoLabel" runat="server" Text='<%# Eval("revenimiento") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="tipoLabel" runat="server" Text='<%# Eval("tipo") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="folioLabel" runat="server" Text='<%# Eval("folio") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="nombreLabel" runat="server" Text='<%# Eval("nombre") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="calleLabel" runat="server" Text='<%# Eval("calle") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="numeroLabel" runat="server" Text='<%# Eval("numero") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="interiorLabel" runat="server" Text='<%# Eval("interior") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="coloniaLabel" runat="server" Text='<%# Eval("colonia") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="cpLabel" runat="server" Text='<%# Eval("cp") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="claveLabel" runat="server" Text='<%# Eval("clave") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="clienteLabel" runat="server" Text='<%# Eval("cliente") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="idEstadoDosificacionLabel" runat="server" Text='<%# Eval("idEstadoDosificacion") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="estadoLabel" runat="server" Text='<%# Eval("estado") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="porcentajeLabel" runat="server" Text='<%# Eval("porcentaje") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="cantidadEntregadaLabel" runat="server" Text='<%# Eval("cantidadEntregada") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="uTransporteLabel" runat="server" Text='<%# Eval("uTransporte") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="colorLabel" runat="server" Text='<%# Eval("color") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="choferLabel" runat="server" Text='<%# Eval("chofer") %>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <SelectedItemTemplate>
                                <tr style="">
                                    <td>
                                        <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="idOrdenLabel" runat="server" Text='<%# Eval("idOrden") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="fechaLabel" runat="server" Text='<%# Eval("fecha") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="horaLabel" runat="server" Text='<%# Eval("hora") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="codigoLabel" runat="server" Text='<%# Eval("codigo") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="descripcionLabel" runat="server" Text='<%# Eval("descripcion") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="cantidadLabel" runat="server" Text='<%# Eval("cantidad") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="unidadLabel" runat="server" Text='<%# Eval("unidad") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="RevenimientoLabel" runat="server" Text='<%# Eval("revenimiento") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="tipoLabel" runat="server" Text='<%# Eval("tipo") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="folioLabel" runat="server" Text='<%# Eval("folio") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="nombreLabel" runat="server" Text='<%# Eval("nombre") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="calleLabel" runat="server" Text='<%# Eval("calle") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="numeroLabel" runat="server" Text='<%# Eval("numero") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="interiorLabel" runat="server" Text='<%# Eval("interior") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="coloniaLabel" runat="server" Text='<%# Eval("colonia") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="cpLabel" runat="server" Text='<%# Eval("cp") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="claveLabel" runat="server" Text='<%# Eval("clave") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="clienteLabel" runat="server" Text='<%# Eval("cliente") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="idEstadoDosificacionLabel" runat="server" Text='<%# Eval("idEstadoDosificacion") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="estadoLabel" runat="server" Text='<%# Eval("estado") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="porcentajeLabel" runat="server" Text='<%# Eval("porcentaje") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="cantidadEntregadaLabel" runat="server" Text='<%# Eval("cantidadEntregada") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="uTransporteLabel" runat="server" Text='<%# Eval("uTransporte") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="colorLabel" runat="server" Text='<%# Eval("color") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="choferLabel" runat="server" Text='<%# Eval("chofer") %>' />
                                    </td>
                                </tr>
                            </SelectedItemTemplate>
                        </asp:ListView>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:cnx %>" SelectCommand="SELECT        od.id, od.idOrden, od.fecha, od.hora, p.codigo, p.descripcion, od.cantidad, udm.unidad, od.revenimiento, tut.tipo, s.folio, pr.nombre, pr.calle, pr.numero, pr.interior, pr.colonia, pr.cp, cl.clave, cl.nombre AS cliente, 
                         od.idEstadoDosificacion, ed.estado, ed.porcentaje, ds.cantidadEntregada, ut.nombre AS uTransporte, ut.color, ch.nombre AS chofer
FROM            ordenes AS o INNER JOIN
                         solicitudes AS s ON o.idSolicitud = s.id INNER JOIN
                         clientes AS cl ON s.idCliente = cl.id INNER JOIN
                         ordenDosificacion AS od ON o.id = od.idOrden INNER JOIN
                         productos AS p ON od.idProducto = p.id INNER JOIN
                         unidadesDeMedida AS udm ON od.idUDM = udm.id INNER JOIN
                         tiposUnidadTransporte AS tut ON od.idTUT = tut.id INNER JOIN
                         proyectos AS pr ON s.idProyecto = pr.id INNER JOIN
                         estadosDosificacion AS ed ON od.idEstadoDosificacion = ed.id INNER JOIN
                         detallesSolicitud AS ds ON od.idDetalleSolicitud = ds.id INNER JOIN
                         unidadesTransporte AS ut ON od.idUnidadTransporte = ut.id INNER JOIN
                         unidadesTChoferes AS utc ON od.idUnidadTransporte = utc.idUnidad INNER JOIN
                         usuarios AS ch ON utc.idChofer = ch.id
WHERE        (od.eliminado IS NULL) AND (od.idOrden = 1)
ORDER BY od.idOrden"></asp:SqlDataSource>
                        <br />
                    </div>
                </div>
            </asp:Panel>

        </div>
    </form>
</body>
</html>
