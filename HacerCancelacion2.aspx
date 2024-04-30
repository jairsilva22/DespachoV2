<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HacerCancelacion2.aspx.cs" Inherits="despacho.HacerCancelacion2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
     <script src="jsF/jquery.min.js" type="text/javascript"></script>
    <style>
        button{
            border-radius:10px;
            height:30px;
            padding: 6px 12px;
            margin-bottom: 0;
            font-size: 14px;
            font-weight: normal;
            line-height: 1.42857143;
            text-align: center;
            white-space: nowrap;
            vertical-align: middle;
            -ms-touch-action: manipulation;
            touch-action: manipulation;
            cursor: pointer;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
            background-image: none;
            border: 1px solid transparent;
            border-radius: 4px;
        }

        .cerrar{
            color: #fff;
            background-color: #d9534f;
            border-color: #d43f3a;
        }
        .cerrar:hover{
            color: #fff;
            background-color: #c9302c;
            border-color: #ac2925;
        }

    </style>
    <script>
        function cerrarPopup() {
	    window.parent.$("#cerrarModal").trigger("click");
            window.parent.form1.submit();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
          <p>&nbsp;</p>
        <p>&nbsp;</p>
        <div style="width: 80%;">
            <p style="text-align:center;"><strong>Resultado de Cancelacion</strong></p>
            <asp:Label ID="lblMensaje" runat="server" style="text-align:left"></asp:Label>
            <p>&nbsp;</p>
            <div style="text-align: center;">
               <!-- <button type="button" class="cerrar" onclick="cerrarPopup();">Cerrar</button>-->
            </div>
        </div>
    </form>
</body>
</html>
