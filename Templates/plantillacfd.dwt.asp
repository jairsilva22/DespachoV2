<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<!-- TemplateBeginEditable name="doctitle" -->
<title><%=titlePage%></title>
<!-- TemplateEndEditable -->
<style type="text/css">
<!--
body {
	background-color: #CCC;
	margin-left: 0px;
	margin-top: 0px;
	margin-right: 0px;
	margin-bottom: 0px;
}
.Estilo1 {color: #000000}
.Estilo7 {font-size: 9px}
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
	width:800px;
}
-->
</style>
<!-- TemplateBeginEditable name="head" -->

<!-- TemplateEndEditable -->
</head>

<body>
<table width="1024" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
     <tr>
    <td width="57">&nbsp;</td>
    <td width="556"><strong><img src="../imagenes/viñeta-user.jpg" alt="c" width="20" height="20"></strong><span class="template1"><strong>
        <% Response.Write(Session("site_nombre")) %>
&nbsp; /&nbsp; <strong><strong>
        <% Response.Write(Session("site_tipo")) %>
       </strong></strong></strong></span></td>
    <td width="173">&nbsp;</td>
    <td width="214"><strong><img src="../imagenes/viñeta-clock.jpg" alt="c" width="20" height="20">
              </strong><span class="template1"><strong>
              <% Response.Write(date()) %>
/
<% Response.Write(time()) %>
        <a href="logout.asp"></a></strong></span></td>
  </tr>
  <tr>
  
    <td colspan="4"><table height="120" width="1024" border="1"   bordercolor="#C8D7F6"  cellpadding="0" cellspacing="0" background="../imagenes/cfd-banner.jpg">
     
      <tr>
        <td height="90" valign="bottom">&nbsp;</td>
      </tr>
    </table>
      <% set menu=createobject("ADODB.Recordset")
sqltxt= "SELECT * FROM dbo.menu WHERE Nivel='Emision' OR Nivel='Todos' ORDER BY Numero ASC"
'rs1.CursorType=1
menu.open sqltxt,MM_conecta1_STRING %>
      <table width="95%" border="0" cellpadding="0" cellspacing="0">
        <tr>
          <td height="23"  valign="bottom"><table width="168" border="0" align="center" cellpadding="2" cellspacing="0" bordercolor="#FFFFFF">
            <tr valign="top">
              <td width="45%" height="18" valign="top"><strong><img src="../imagenes/viñeta-menu.jpg" width="70" height="20" /></strong></td>
              <%

While Not menu.EOF
if  menu("Nivel")="100" and  Session("site_tipo") <> "Administrador"  Then
menu.MoveNext
Else
%>
              <td width="11%" valign="top" bgcolor="#FFFFFF"><a href="<%= menu("Link")%>"></a><a href="<%= menu("Link")%>"><img src="../imagenes/<%= menu("imagen") %>" alt="y" width="15" height="20" border="0" align="left" /></a></td>
              <td width="32%" align="center" valign="top"><a href="<%= menu("Link")%>"> <%= menu("Descripcion") %></a></td>
              <td width="12%" valign="top">&nbsp;&nbsp;</td>
              <%          
menu.MoveNext
 end if
 Wend
%>
            </tr>
          </table></td>
        </tr>
      </table>
      <!-- TemplateBeginEditable name="EditRegion1" -->
      <p>&nbsp;</p>
      <p>&nbsp;</p>
      <p>&nbsp;</p>
      <!-- TemplateEndEditable -->
 <p>&nbsp;</p>
      <%=footerPage%>
    </td>
  </tr>
 
  
</table>
      
</body>
</html>
