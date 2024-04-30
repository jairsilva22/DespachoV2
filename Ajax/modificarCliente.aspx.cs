using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho.Ajax {

    public partial class modificarCliente : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            cClientes clientes = new cClientes();
            if (!IsPostBack) {
                if (Request.Form.Count == 4) {
                    string cadena = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;

                    int idCliente = int.Parse(Request.Form["idCliente"]);

                    string clave = Request.Form["clave"].Trim();
                    string nombre = Request.Form["nombre"].Trim();
                    string fp = Request.Form["fp"].Trim();
                    int formaPago = idFormaPago(cadena, fp);

                    try {
                        if (idCliente == 0) {
                            string msg = "";

                            if (nombre == "") {
                                msg += "- Falta Ingresar el Nombre del Cliente para un nuevo registro.";
                            }

                            if (nombre != "" && nombre != null) //insertamos al cliente
                            {

                                //validamos si existe el cliente 
                                clientes.nombre = nombre;
                                clientes.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);

                                if (!clientes.existe()) {
                                    string clave1 = clientes.generarClave();
                                    insertar(cadena, clave1, nombre, formaPago);
                                }
                                else {
                                    msg = "El cliente ya existe.";
                                }


                            }
                            Response.Write(msg);
                        }
                        else {
                            if (idCliente > 0) {
                                using (SqlConnection conn = new SqlConnection(cadena)) {
                                    conn.Open();
                                    using (SqlCommand cmd = new SqlCommand("UPDATE clientes SET nombre = @nombre, " +
                                        "idUsuarioMod = @idUsuarioMod, idFormaPago = @fp, fechaMod = GETDATE() WHERE id = @id", conn)) {
                                        cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                                        cmd.Parameters.Add(new SqlParameter("@idUsuarioMod", int.Parse(Request.Cookies["ksroc"]["id"])));
                                        cmd.Parameters.Add(new SqlParameter("@id", idCliente));
                                        cmd.Parameters.Add(new SqlParameter("@fp", formaPago));

                                        int filasAfectadas = cmd.ExecuteNonQuery();
                                        if (filasAfectadas > 0) {
                                            Response.Write("El Cliente " + nombre + " ha sido modificado con éxito.");
                                        }
                                        else {
                                            Response.Write("El Cliente " + nombre + " no puede ser modificado.");
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex) {
                        Response.Write(ex.ToString());
                    }
                }
                else {
                    Response.Write("El Cliente no puede ser modificado.");
                }
            }
        }

        public int idFormaPago(string cadena, string fp) {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(cadena)) {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("select * from dbo.formasPago where nombre LIKE @fp", conn)) {
                    cmd.Parameters.Add(new SqlParameter("@fp", fp));
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                        sda.Fill(dt);
                        return int.Parse(dt.Rows[0]["id"].ToString());
                    }

                }
            }
        }

        private void insertar(string cadena, string clave, string nombre, int formaPago) {
            using (SqlConnection conn = new SqlConnection(cadena)) {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT clientes(nombre, clave, idSucursal, idUsuario, fechaAlta, idFormaPago) VALUES(@nombre, @cve, @idS, @idU, GETDATE(), @fp) ", conn)) {
                    cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                    cmd.Parameters.Add(new SqlParameter("@cve", clave));
                    cmd.Parameters.Add(new SqlParameter("@idS", int.Parse(Request.Cookies["ksroc"]["idSucursal"])));
                    cmd.Parameters.Add(new SqlParameter("@idU", int.Parse(Request.Cookies["ksroc"]["id"])));
                    cmd.Parameters.Add(new SqlParameter("@fp", formaPago));

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0) {
                        Response.Write("El Cliente " + nombre + " ha sido agregado con éxito.");
                    }
                    else {
                        Response.Write("El Cliente " + nombre + " no puede ser agregado.");
                    }
                }
            }
        }
    }
}