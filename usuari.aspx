<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="usuari.aspx.cs" Inherits="despacho.usuari" %>

<%@ Register Src="~/wuc/abcMenu.ascx" TagPrefix="uc1" TagName="abcMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts" runat="server">
    <script>
        function modificar() {
            var user = $("[id*='idUser']").val()

            if (user != "") {
                if (confirm("Deseas Modificar éste Usuario?") == true) {
                    return true
                }
                else {
                    return false
                }
            }
            else {
                return true
            }
            
        }

        function existeNombre() {
            alert("Ya existe un Usuario con ese nombre !")
            return false
        }

        function existeUsuario() {
            alert("Ya existe un Usuario con ese usuario !")
            return false
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <!--Botones para el ABC de Usuarios-->
    <div>
        <asp:LinkButton ID="agregarLbtn" runat="server" OnClick="agregarLbtn_Click" CausesValidation="false" ToolTip="Nuevo Registro">
            <%--<i class='fa fa-plus-square fa-2x'></i>--%>
            Nuevo
        </asp:LinkButton>
        &nbsp;&nbsp
        <asp:LinkButton ID="guardarLbtn" runat="server" OnClick="guardarLbtn_Click" OnClientClick="return modificar()" CausesValidation="true"  ToolTip="Guardar Registro">
            <%--<i class="fa fa-floppy-o fa-2x"></i>--%>
            Agregar
        </asp:LinkButton>
        &nbsp;&nbsp;
        <asp:LinkButton ID="eliminarLbtn" runat="server" OnClick="eliminarLbtn_Click" OnClientClick="return confirm('Eliminar Usuario?')" CausesValidation="true" ToolTip="Eliminar Registro">
            <%--<i class="fa fa-minus-square-o fa-2x"></i>--%>
            Eliminar
        </asp:LinkButton>
        &nbsp;&nbsp;
        <a href="javascript:buscar('UsuariosMod.aspx', 'Buscar Usuarios')" title="Buscar Registro">
            <%--<i class="fa fa-search fa-2x"></i>--%>
            Buscar
        </a>
    </div>
    <div>
        <uc1:abcMenu runat="server" id="abcMenu" />
    </div>
    <!--Titulo de la Página-->
    <center><h3>Usuarios</h3></center>
    <p>&nbsp;</p>
    <!--Formulario-->
    <div class="container">
        <div class="form-horizontal">
            <!--nombre-->
            <div class="form-group">
                <asp:Label ID="Label1" runat="server" Text="Nombre" CssClass="col-md-4 control-label"></asp:Label>
                <div class="col-md-4">
                    <input type="text" name="nombreTxt" id="nombreTxt" runat="server" class="form-control" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="label label-danger" runat="server" ErrorMessage="Falta el nombre del Usuario !" ControlToValidate="nombreTxt"></asp:RequiredFieldValidator>
                </div>
            </div>
            <!--usuario-->
            <div class="form-group">
                <asp:Label ID="Label2" runat="server" Text="Usuario" CssClass="col-md-4 control-label"></asp:Label>
                <div class="col-md-4">
                    <input type="text" name="usuarioTxt" id="usuarioTxt" runat="server" class="form-control" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="label label-danger" runat="server" ErrorMessage="Falta el Usuario !" ControlToValidate="usuarioTxt"></asp:RequiredFieldValidator>
                </div>
            </div>
            <!--usuario-->
            <div class="form-group">
                <asp:Label ID="Label3" runat="server" Text="Contraseña" CssClass="col-md-4 control-label"></asp:Label>
                <div class="col-md-4">
                    <input type="password" name="passTxt" id="passTxt" runat="server" class="form-control" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="label label-danger" runat="server" ErrorMessage="Falta la contraseña del Usuario !" ControlToValidate="passTxt"></asp:RequiredFieldValidator>
                </div>
            </div>
            <!--perfil-->
            <div class="form-group">
                <asp:Label ID="Label4" runat="server" Text="Perfil" CssClass="col-md-4 control-label"></asp:Label>
                <div class="col-md-4">
                    <select runat="server" id="perfilDdl" class="form-control"></select>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="label label-danger" runat="server" ErrorMessage="Falta el Perfil del Usuario !" ControlToValidate="perfilDdl"></asp:RequiredFieldValidator>
                    <input type="hidden" runat="server" id="idUser" name="idUser" />
                </div>
            </div>
            <!--activo-->
            <div>
                <div class="col-md-4"></div>
                <div class="col-md-4">
                    <asp:CheckBox ID="activoChk" runat="server" />
                    <asp:Label ID="Label5" runat="server" CssClass="control-label" Text="Activo"></asp:Label>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
