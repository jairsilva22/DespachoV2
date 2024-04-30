<%@  language="VBSCRIPT" %>
<!--#include file="Connections/Conecta1.asp" -->
<!--#include file="config.asp"-->
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
If (CStr(Request("MM_update")) = "form1") Then
  If (Not MM_abortEdit) Then
    ' execute the update
    Dim MM_editCmd

    Set MM_editCmd = Server.CreateObject ("ADODB.Command")
    MM_editCmd.ActiveConnection = MM_Conecta1_STRING
    MM_editCmd.CommandText = "UPDATE dbo.cadenda SET descripcion = ?, c1 = ?, c2 = ?, c3 = ?, c4 = ?, c5 = ?, c6 = ?, c7 = ?, c8 = ?, c9 = ?, c10 = ?, c11 = ?, c12 = ?, c13 = ?, c14 = ?, c15 = ?, c16 = ?, c17 = ?, c18 = ?, c19 = ?, c20 = ? WHERE idcadenda = ?" 
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
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param22", 5, 1, -1, MM_IIF(Request.Form("MM_recordId"), Request.Form("MM_recordId"), null)) ' adDouble
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
<%
' *** Delete Record: construct a sql delete statement and execute it

If (CStr(Request("MM_delete")) = "form2" And CStr(Request("MM_recordId")) <> "") Then

  If (Not MM_abortEdit) Then
    ' execute the delete
    Set MM_editCmd = Server.CreateObject ("ADODB.Command")
    MM_editCmd.ActiveConnection = MM_Conecta1_STRING
    MM_editCmd.CommandText = "DELETE FROM dbo.cadenda WHERE idcadenda = ?"
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param1", 5, 1, -1, Request.Form("MM_recordId")) ' adDouble
    MM_editCmd.Execute
    MM_editCmd.ActiveConnection.Close

    ' append the query string to the redirect URL
    'Dim MM_editRedirectUrl
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
<%
Dim Recordset1__MMColParam
Recordset1__MMColParam = "1"
If (Request.QueryString("idcadenda") <> "") Then 
  Recordset1__MMColParam = Request.QueryString("idcadenda")
End If
%>
<%
Dim Recordset1
Dim Recordset1_cmd
Dim Recordset1_numRows

Set Recordset1_cmd = Server.CreateObject ("ADODB.Command")
Recordset1_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset1_cmd.CommandText = "SELECT * FROM dbo.cadenda WHERE idcadenda = ?" 
Recordset1_cmd.Prepared = true
Recordset1_cmd.Parameters.Append Recordset1_cmd.CreateParameter("param1", 5, 1, -1, Recordset1__MMColParam) ' adDouble

Set Recordset1 = Recordset1_cmd.Execute
Recordset1_numRows = 0
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<!-- InstanceBegin template="Templates/plantillacfd.dwt.asp" codeOutsideHTMLIsLocked="false" -->
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
    <link rel="stylesheet" href="css.css" type="text/css" media="screen" />
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
                <form action="<%=MM_editAction%>" method="post" name="form1" id="form1">
                    <table align="center">
                        <tr valign="baseline">
                            <td nowrap="nowrap" align="right">Descripcion:</td>
                            <td>
                                <input type="text" name="descripcion" value="<%=(Recordset1.Fields.Item("descripcion").Value)%>" size="32" /></td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr valign="baseline">
                            <td nowrap="nowrap" align="right">C1:</td>
                            <td>
                                <input type="text" name="c1" value="<%=(Recordset1.Fields.Item("c1").Value)%>" size="32" /></td>
                            <td>C11:</td>
                            <td>
                                <input type="text" name="c11" value="<%=(Recordset1.Fields.Item("c11").Value)%>" size="32" /></td>
                        </tr>
                        <tr valign="baseline">
                            <td nowrap="nowrap" align="right">C2:</td>
                            <td>
                                <input type="text" name="c2" value="<%=(Recordset1.Fields.Item("c2").Value)%>" size="32" /></td>
                            <td>C12:</td>
                            <td>
                                <input type="text" name="c12" value="<%=(Recordset1.Fields.Item("c12").Value)%>" size="32" /></td>
                        </tr>
                        <tr valign="baseline">
                            <td nowrap="nowrap" align="right">C3:</td>
                            <td>
                                <input type="text" name="c3" value="<%=(Recordset1.Fields.Item("c3").Value)%>" size="32" /></td>
                            <td>C13:</td>
                            <td>
                                <input type="text" name="c13" value="<%=(Recordset1.Fields.Item("c13").Value)%>" size="32" /></td>
                        </tr>
                        <tr valign="baseline">
                            <td nowrap="nowrap" align="right">C4:</td>
                            <td>
                                <input type="text" name="c4" value="<%=(Recordset1.Fields.Item("c4").Value)%>" size="32" /></td>
                            <td>C14:</td>
                            <td>
                                <input type="text" name="c14" value="<%=(Recordset1.Fields.Item("c14").Value)%>" size="32" /></td>
                        </tr>
                        <tr valign="baseline">
                            <td nowrap="nowrap" align="right">C5:</td>
                            <td>
                                <input type="text" name="c5" value="<%=(Recordset1.Fields.Item("c5").Value)%>" size="32" /></td>
                            <td>C15:</td>
                            <td>
                                <input type="text" name="c15" value="<%=(Recordset1.Fields.Item("c15").Value)%>" size="32" /></td>
                        </tr>
                        <tr valign="baseline">
                            <td nowrap="nowrap" align="right">C6:</td>
                            <td>
                                <input type="text" name="c6" value="<%=(Recordset1.Fields.Item("c6").Value)%>" size="32" /></td>
                            <td>C16:</td>
                            <td>
                                <input type="text" name="c16" value="<%=(Recordset1.Fields.Item("c16").Value)%>" size="32" /></td>
                        </tr>
                        <tr valign="baseline">
                            <td nowrap="nowrap" align="right">C7:</td>
                            <td>
                                <input type="text" name="c7" value="<%=(Recordset1.Fields.Item("c7").Value)%>" size="32" /></td>
                            <td>C17:</td>
                            <td>
                                <input type="text" name="c17" value="<%=(Recordset1.Fields.Item("c17").Value)%>" size="32" /></td>
                        </tr>
                        <tr valign="baseline">
                            <td nowrap="nowrap" align="right">C8:</td>
                            <td>
                                <input type="text" name="c8" value="<%=(Recordset1.Fields.Item("c8").Value)%>" size="32" /></td>
                            <td>C18:</td>
                            <td>
                                <input type="text" name="c18" value="<%=(Recordset1.Fields.Item("c18").Value)%>" size="32" /></td>
                        </tr>
                        <tr valign="baseline">
                            <td nowrap="nowrap" align="right">C9:</td>
                            <td>
                                <input type="text" name="c9" value="<%=(Recordset1.Fields.Item("c9").Value)%>" size="32" /></td>
                            <td>C19:</td>
                            <td>
                                <input type="text" name="c19" value="<%=(Recordset1.Fields.Item("c19").Value)%>" size="32" /></td>
                        </tr>
                        <tr valign="baseline">
                            <td nowrap="nowrap" align="right">C10:</td>
                            <td>
                                <input type="text" name="c10" value="<%=(Recordset1.Fields.Item("c10").Value)%>" size="32" /></td>
                            <td>C20:</td>
                            <td>
                                <input type="text" name="c20" value="<%=(Recordset1.Fields.Item("c20").Value)%>" size="32" /></td>
                        </tr>
                        <tr valign="baseline">
                            <td nowrap="nowrap" align="right">&nbsp;</td>
                            <td>
                                <input type="submit" value="Modificar" /></td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                    <input type="hidden" name="MM_update" value="form1" />
                    <input type="hidden" name="MM_recordId" value="<%= Recordset1.Fields.Item("idcadenda").Value %>" />
                </form>
                <form id="form2" name="form2" method="POST" action="<%=MM_editAction%>">
                    <label>
                        <input type="submit" name="button" id="button" value="Eliminar" />
                    </label>
                    <input type="hidden" name="MM_delete" value="form2" />
                    <input type="hidden" name="MM_recordId" value="<%= Recordset1.Fields.Item("idcadenda").Value %>" />
                </form>
                <p>&nbsp;</p>
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
