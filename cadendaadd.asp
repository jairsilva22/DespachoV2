<%@  language="VBSCRIPT" %>
<!--#include file="Connections/Conecta1.asp" -->
<%
Dim MM_editAction
MM_editAction = CStr(Request.ServerVariables("SCRIPT_NAME"))
If (Request.QueryString <> "") Then
  MM_editAction = MM_editAction & "?" & Server.HTMLEncode(Request.QueryString)
End If

' boolean to abort record edit
Dim MM_abortEdit
MM_abortEdit = false
%>
<%
' IIf implementation
Function MM_IIf(condition, ifTrue, ifFalse)
  If condition = "" Then
    MM_IIf = ifFalse
  Else
    MM_IIf = ifTrue
  End If
End Function
%>
<%
If (CStr(Request("MM_insert")) = "form1") Then
  If (Not MM_abortEdit) Then
    ' execute the insert
    Dim MM_editCmd

    Set MM_editCmd = Server.CreateObject ("ADODB.Command")
    MM_editCmd.ActiveConnection = MM_Conecta1_STRING
    MM_editCmd.CommandText = "INSERT INTO dbo.cadenda (descripcion, c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12, c13, c14, c15, c16, c17, c18, c19, c20, idcliente, idempresa, idusuario, fechaalta) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)" 
    MM_editCmd.Prepared = true
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param1", 201, 1, 255, Request.Form("descripcion")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param2", 201, 1, 50, Request.Form("c1")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param3", 202, 1, 10, Request.Form("c2")) ' adVarWChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param4", 202, 1, 10, Request.Form("c3")) ' adVarWChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param5", 202, 1, 10, Request.Form("c4")) ' adVarWChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param6", 202, 1, 10, Request.Form("c5")) ' adVarWChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param7", 202, 1, 10, Request.Form("c6")) ' adVarWChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param8", 202, 1, 10, Request.Form("c7")) ' adVarWChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param9", 202, 1, 10, Request.Form("c8")) ' adVarWChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param10", 202, 1, 10, Request.Form("c9")) ' adVarWChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param11", 202, 1, 10, Request.Form("c10")) ' adVarWChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param12", 202, 1, 10, Request.Form("c11")) ' adVarWChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param13", 202, 1, 10, Request.Form("c12")) ' adVarWChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param14", 202, 1, 10, Request.Form("c13")) ' adVarWChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param15", 202, 1, 10, Request.Form("c14")) ' adVarWChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param16", 202, 1, 10, Request.Form("c15")) ' adVarWChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param17", 202, 1, 10, Request.Form("c16")) ' adVarWChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param18", 202, 1, 10, Request.Form("c17")) ' adVarWChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param19", 202, 1, 10, Request.Form("c18")) ' adVarWChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param20", 202, 1, 10, Request.Form("c19")) ' adVarWChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param21", 202, 1, 10, Request.Form("c20")) ' adVarWChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param22", 5, 1, -1, MM_IIF(Request.Form("idcliente"), Request.Form("idcliente"), null)) ' adDouble
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param23", 5, 1, -1, MM_IIF(Request.Form("idempresa"), Request.Form("idempresa"), null)) ' adDouble
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param24", 5, 1, -1, MM_IIF(Request.Form("idusuario"), Request.Form("idusuario"), null)) ' adDouble
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param25", 135, 1, -1, MM_IIF(Request.Form("fechaalta"), Request.Form("fechaalta"), null)) ' adDBTimeStamp
    MM_editCmd.Execute
    MM_editCmd.ActiveConnection.Close

    ' append the query string to the redirect URL
    Dim MM_editRedirectUrl
    MM_editRedirectUrl = "cadendaadd.asp"
    If (Request.QueryString <> "") Then
      If (InStr(1, MM_editRedirectUrl, "?", vbTextCompare) = 0) Then
        MM_editRedirectUrl = MM_editRedirectUrl & "?" & Request.QueryString
      Else
        MM_editRedirectUrl = MM_editRedirectUrl & "&" & Request.QueryString
      End If
    End If
    Response.Redirect(MM_editRedirectUrl)
  End If
End If
%>
<!--#include file="config.asp"-->
<!--#include file="checklogin.asp"-->
<!--#include file="stylo2.asp"-->
<%
Dim Recordset1__MMColParam
Recordset1__MMColParam = "1"
If (Request.QueryString("idcliente") <> "") Then 
  Recordset1__MMColParam = Request.QueryString("idcliente")
End If
%>
<%
Dim Recordset1
Dim Recordset1_cmd
Dim Recordset1_numRows

Set Recordset1_cmd = Server.CreateObject ("ADODB.Command")
Recordset1_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset1_cmd.CommandText = "SELECT * FROM dbo.cadenda WHERE idcliente = ?" 
Recordset1_cmd.Prepared = true
Recordset1_cmd.Parameters.Append Recordset1_cmd.CreateParameter("param1", 5, 1, -1, Recordset1__MMColParam) ' adDouble

Set Recordset1 = Recordset1_cmd.Execute
Recordset1_numRows = 0
%>
<%
Dim Repeat1__numRows
Dim Repeat1__index

Repeat1__numRows = -1
Repeat1__index = 0
Recordset1_numRows = Recordset1_numRows + Repeat1__numRows
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <script language="JavaScript" src="jsF/overlib_mini.js"></script>
<!-- InstanceBegin template="/Templates/plantillacfd.dwt.asp" codeOutsideHTMLIsLocked="false" -->
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="efectos/css/demos.css" media="screen" type="text/css">
    <link href="//maxcdn.bootstrapcdn.com/font-awesome/4.2.0/css/font-awesome.min.css" rel="stylesheet">
	<script type="text/javascript" src="efectos/js/menu-for-applications.js"></script>
    <!-- InstanceBeginEditable name="doctitle" -->
     <title><%=titlePage%></title>
    <link rel="stylesheet" href="jsF/css.css" type="text/css" media="screen"  />
    <link rel="stylesheet" href="jsF/bootstrap.css" />
    <script src="jsF/jquery.min.js" type="text/javascript"></script>
    <script src="jsF/bootstrap.min.js"></script>
    <!-- InstanceEndEditable -->
   
    <style type="text/css">
        <!--
        body {
            background-color: white;
            margin-left: 0px;
            margin-top: 0px;
            margin-right: 0px;
            margin-bottom: 0px;
        }

        .Estilo1 {
            color: #000000
        }

        .Estilo7 {
            font-size: 9px
        }

        .Estilo11 {
            font-weight: bold;
            color: #000000;
            font-size: 14px;
        }

        .template1 {
            font-size: 12px;
        }

        .inicio {
            background-position: center;
            height: auto;
            width: 800px;
        }
        -->
    </style>
    <!-- InstanceBeginEditable name="head" -->
    <!-- InstanceEndEditable -->
</head>

<body>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
        
        <tr>
            <td>
                <!-- InstanceBeginEditable name="EditRegion1" -->
                <p>&nbsp;</p>
                <p>&nbsp;</p>
                <p align="center">Adenda</p>
                <form action="<%=MM_editAction%>" method="POST" name="form1" id="form1">
                    <table align="center">
                        <tr valign="baseline">
                            <td align="right" valign="baseline" nowrap="nowrap">Descripcion:</td>
                            <td colspan="3">
                                <label>
                                    <input name="descripcion" type="text" id="descripcion" size="40" />
                                </label>
                            </td>
                        </tr>
                        <tr valign="baseline">
                            <td nowrap="nowrap" align="right">C1:</td>
                            <td>
                                <input type="text" name="c1" value="" size="32" /></td>
                            <td>C11:</td>
                            <td>
                                <input type="text" name="c11" value="" size="32" /></td>
                        </tr>
                        <tr valign="baseline">
                            <td nowrap="nowrap" align="right">C2:</td>
                            <td>
                                <input type="text" name="c2" value="" size="32" /></td>
                            <td>C12:</td>
                            <td>
                                <input type="text" name="c12" value="" size="32" /></td>
                        </tr>
                        <tr valign="baseline">
                            <td nowrap="nowrap" align="right">C3:</td>
                            <td>
                                <input type="text" name="c3" value="" size="32" /></td>
                            <td>C13:</td>
                            <td>
                                <input type="text" name="c13" value="" size="32" /></td>
                        </tr>
                        <tr valign="baseline">
                            <td nowrap="nowrap" align="right">C4:</td>
                            <td>
                                <input type="text" name="c4" value="" size="32" /></td>
                            <td>C14:</td>
                            <td>
                                <input type="text" name="c14" value="" size="32" /></td>
                        </tr>
                        <tr valign="baseline">
                            <td nowrap="nowrap" align="right">C5:</td>
                            <td>
                                <input type="text" name="c5" value="" size="32" /></td>
                            <td>C15:</td>
                            <td>
                                <input type="text" name="c15" value="" size="32" /></td>
                        </tr>
                        <tr valign="baseline">
                            <td nowrap="nowrap" align="right">C6:</td>
                            <td>
                                <input type="text" name="c6" value="" size="32" /></td>
                            <td>C16:</td>
                            <td>
                                <input type="text" name="c16" value="" size="32" /></td>
                        </tr>
                        <tr valign="baseline">
                            <td nowrap="nowrap" align="right">C7:</td>
                            <td>
                                <input type="text" name="c7" value="" size="32" /></td>
                            <td>C17:</td>
                            <td>
                                <input type="text" name="c17" value="" size="32" /></td>
                        </tr>
                        <tr valign="baseline">
                            <td nowrap="nowrap" align="right">C8:</td>
                            <td>
                                <input type="text" name="c8" value="" size="32" /></td>
                            <td>C18:</td>
                            <td>
                                <input type="text" name="c18" value="" size="32" /></td>
                        </tr>
                        <tr valign="baseline">
                            <td nowrap="nowrap" align="right">C9:</td>
                            <td>
                                <input type="text" name="c9" value="" size="32" /></td>
                            <td>C19:</td>
                            <td>
                                <input type="text" name="c19" value="" size="32" /></td>
                        </tr>
                        <tr valign="baseline">
                            <td nowrap="nowrap" align="right">C10:</td>
                            <td>
                                <input type="text" name="c10" value="" size="32" /></td>
                            <td>C20:</td>
                            <td>
                                <input type="text" name="c20" value="" size="32" /></td>
                        </tr>
                        <tr valign="baseline">
                            <td nowrap="nowrap" align="right">&nbsp;</td>
                            <td>
                                <input type="submit" value="Guardar" /></td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                    <input type="hidden" name="idcliente" value="<%=Request.QueryString("idcliente")%>" />
                    <input type="hidden" name="idempresa" value="<%=Session("site_empres")%>" />
                    <input type="hidden" name="idusuario" value="<%=Session("sitr_id")%>" />
                    <input type="hidden" name="fechaalta" value="<%=datef2(Date())%>" />
                    <input type="hidden" name="MM_insert" value="form1" />
                </form>
                <p>&nbsp;</p>
                <table border="1" align="center">
                    <tr>
                        <td>Descripcion</td>
                        <td>c1</td>
                        <td>c2</td>
                        <td>c3</td>
                        <td>c4</td>
                        <td>c5</td>
                        <td>c6</td>
                        <td>c7</td>
                        <td>c8</td>
                        <td>c9</td>
                        <td>c10</td>
                        <td>c11</td>
                        <td>c12</td>
                        <td>c13</td>
                        <td>c14</td>
                        <td>c15</td>
                        <td>c16</td>
                        <td>c17</td>
                        <td>c18</td>
                        <td>c19</td>
                        <td>c20</td>
                        <td>Mod</td>
                    </tr>
                    <% While ((Repeat1__numRows <> 0) AND (NOT Recordset1.EOF)) %>
                    <tr>
                        <td><%=(Recordset1.Fields.Item("descripcion").Value)%></td>
                        <td><%=(Recordset1.Fields.Item("c1").Value)%></td>
                        <td><%=(Recordset1.Fields.Item("c2").Value)%></td>
                        <td><%=(Recordset1.Fields.Item("c3").Value)%></td>
                        <td><%=(Recordset1.Fields.Item("c4").Value)%></td>
                        <td><%=(Recordset1.Fields.Item("c5").Value)%></td>
                        <td><%=(Recordset1.Fields.Item("c6").Value)%></td>
                        <td><%=(Recordset1.Fields.Item("c7").Value)%></td>
                        <td><%=(Recordset1.Fields.Item("c8").Value)%></td>
                        <td><%=(Recordset1.Fields.Item("c9").Value)%></td>
                        <td><%=(Recordset1.Fields.Item("c10").Value)%></td>
                        <td><%=(Recordset1.Fields.Item("c11").Value)%></td>
                        <td><%=(Recordset1.Fields.Item("c12").Value)%></td>
                        <td><%=(Recordset1.Fields.Item("c13").Value)%></td>
                        <td><%=(Recordset1.Fields.Item("c14").Value)%></td>
                        <td><%=(Recordset1.Fields.Item("c15").Value)%></td>
                        <td><%=(Recordset1.Fields.Item("c16").Value)%></td>
                        <td><%=(Recordset1.Fields.Item("c17").Value)%></td>
                        <td><%=(Recordset1.Fields.Item("c18").Value)%></td>
                        <td><%=(Recordset1.Fields.Item("c19").Value)%></td>
                        <td><%=(Recordset1.Fields.Item("c20").Value)%></td>
                        <td><a href="cadendamod.asp?idcadenda=<%=(Recordset1.Fields.Item("idcadenda").Value)%>">
                            <img src="imagenes/database_table_(edit)_16x16.gif" width="16" height="16" border="0" /></a></td>
                    </tr>
                    <% 
  Repeat1__index=Repeat1__index+1
  Repeat1__numRows=Repeat1__numRows-1
  Recordset1.MoveNext()
Wend
                    %>
                </table>
                <p>&nbsp;</p>
                <!-- InstanceEndEditable -->
                <p>&nbsp;</p>
                <%=footerPage%>
            </td>
        </tr>


    </table>

</body>
<!-- InstanceEnd -->
</html>
<%
Recordset1.Close()
Set Recordset1 = Nothing
%>
