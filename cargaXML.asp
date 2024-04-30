<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Untitled Document</title>
<%'response.write(request.QueryString("archivo"))%>
</head>

<body>
<form action="" method="post" enctype="multipart/form-data" name="form1" id="form1">
  <p align="center">CARGAR ARCHIVO PARA RECUPERAR FACTURA</p>
  <p align="center">
    <label for="textfield"></label>
     ARCHIVO
     
    <label for="fileField"></label>
    <input type="file" name="fileField" id="fileField" onChange="enviar(this)"/>
  </p>
  <p align="center"><a href="leerXML.asp?nombre=<%=mid(request.QueryString("archivo"),instrrev(request.QueryString("archivo"),"\")+1)%>">Enviar</a></p>
  <p>&nbsp;</p>
</form>
</body>
</html>
<script language="javascript">
function enviar(archivo)
{
	alert("el archivo ha sido cargado correctamente");
	document.form1.action = "cargaXML.asp?archivo="+archivo.value
    document.form1.submit(); 
	}
</script>