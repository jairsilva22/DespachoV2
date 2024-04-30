<%
	Set Con = Server.CreateObject("ADODB.Connection")
	Con.Open strCon
	sql = "Select usuario, password from dbo.usuarios WHERE  id = "& Request.Cookies("login")("id")
	set rec = Con.execute(sql)
		
		if rec.eof then
	        if  Request.Cookies("login")("id") <> "" AND Request.Cookies("login")("id") <> null Then		
		       Session("site_usuario") = Request.Cookies("login")("usuario")
			   Session("site_id") = Request.Cookies("login")("id")
			   Session("site_idPerfil") = Request.Cookies("login")("idPerfil")
			   Session("site_idSucursal") = Request.Cookies("login")("idSucursal")
            else
               response.redirect "login.asp"	
			end if	
end if	
	rec.close
	set rec = nothing
	Con.close
	set Con = nothing

%>

