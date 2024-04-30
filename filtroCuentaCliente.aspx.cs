using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Services;
using System.Data;
using System.Web.Script.Services;
using System.Web.Script.Serialization;

namespace despacho
{
    public partial class filtroCuentaCliente : System.Web.UI.Page
    {
        cClientes cCliente = new cClientes();
        cContpaq cContpaq = new cContpaq();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (Request.Cookies.Count >0) {
                    cCliente.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                    txtNombreCliente_AutoCompleteExtender.ContextKey = Request.Cookies["ksroc"]["idSucursal"];
                    //llenar Clientes de despacho
                    //ddlCliente.DataSource = cCliente.obtenerClientesActivos();
                    //llenar clientes de contpaq
                    ddlCliente.DataSource = cContpaq.obtenerClientesContpaq();
                    ddlCliente.DataValueField = "CIDCLIENTEPROVEEDOR";
                    ddlCliente.DataTextField = "CRAZONSOCIAL";
                    ddlCliente.DataBind();
                    ddlCliente.Items.Insert(0, new ListItem("Seleccionar", ""));

                    buscarClientesContpaq();
                }
                else {
                    Response.Redirect(ResolveUrl("login.aspx"));
                }
                
            }
            txtFechaFin.Attributes.Add("oninput", "quitarError();");
            txtFechaInicio.Attributes.Add("oninput", "quitarError();");
        }

        //Obtener la cadena de la sucursal según el ID que le pasen
        public string[,] cadenaBD(int sucursal) {
            //Arreglo debido a que puede haber dos conexiones a la base de datos
            string[,] conexiones;

            switch (sucursal) {
                //Saltillo concretos
                case 1:
                    conexiones = new string[2, 2];
                    conexiones[0, 0] = cContpaq.BDContpaq();
                    conexiones[1, 0] = cContpaq.BDConcrSalVG();
                    conexiones[0, 1] = "Concretos Saltillo Facturable";
                    conexiones[1, 1] = "Concretos Saltillo Ventas General";
                    break;
                //Irapuato concretos
                case 2:
                    conexiones = new string[2, 2];
                    conexiones[0, 0] = cContpaq.BDIraConcretos();
                    conexiones[0, 1] = "Concretos Irapuato Facturable";
                    conexiones[1, 0] = cContpaq.BDIraConcretosVG();
                    conexiones[1, 1] = "Concretos Irapuato Ventas General";
                    break;
                //Saltillo Block
                case 3:
                    conexiones = new string[2, 2];
                    conexiones[0, 0] = cContpaq.BDBlockSalFac();
                    conexiones[1, 0] = cContpaq.BDBlockSalVG();
                    conexiones[0, 1] = "Block Saltillo Facturable";
                    conexiones[1, 1] = "Block Saltillo Ventas General";
                    break;
                //Irapuato Block
                case 1006:
                    conexiones = new string[2, 2];
                    conexiones[0, 0] = cContpaq.BDBlockIra();
                    conexiones[0, 1] = "Block Irapuato Facturable";
                    conexiones[1, 0] = cContpaq.BDBlockIraVG();
                    conexiones[1, 1] = "Block Irapuato Ventas General";
                    break;
                //Default: saltillo concretos
                default:
                    conexiones = new string[2, 2];
                    conexiones[0, 0] = cContpaq.BDContpaq();
                    conexiones[1, 0] = cContpaq.BDConcrSalVG();
                    conexiones[0, 1] = "Concretos Saltillo Facturable";
                    conexiones[1, 1] = "Concretos Saltillo Ventas General";
                    break;
            }

            return conexiones;
        }

        public void buscarClientesContpaq() {
            string cadena;
            DataTable dtClientes;
            var reporteECC = new List<clientesContpaq>();
            int sucursal = cCliente.idSucursal;

            //Validar perfil
            string perfil = Request.Cookies["login"]["idPerfil"];
            string[,] conexiones = cadenaBD(sucursal);
            if (perfil != "1" && perfil != "1007") {
                //lblError.Text = "No debe ver todo" + perfil;
                for (int j = 0; j < conexiones.GetLength(0); j++) {
                    cadena = conexiones[j, 0];
                    dtClientes = cContpaq.obtenerDatosCliente(cadena);
                    if (dtClientes != null) {
                        for (int i = 0; i < dtClientes.Rows.Count; i++) {
                            //Añadir movimiento a la lista
                            reporteECC.Add(new clientesContpaq() { seleccionar = int.Parse(dtClientes.Rows[i]["CIDCLIENTEPROVEEDOR"].ToString()), nombre = dtClientes.Rows[i]["CRAZONSOCIAL"].ToString(), sucursal = conexiones[j, 1], numcliente = dtClientes.Rows[i]["CCODIGOCLIENTE"].ToString() });

                        }
                    }
                }
                
            }
            else {
                //lblError.Text = "Sí debe ver todo" + perfil;

                //Buscar clientes en cada sucursal
                DataTable dtConexiones = cContpaq.obtenerConexiones();

                foreach (DataRow row in dtConexiones.Rows) {
                    cadena = row[5].ToString();
                    string nombreSucursal = row[1].ToString();
                    dtClientes = cContpaq.obtenerDatosCliente(cadena);
                    if (dtClientes != null) {
                        for (int i = 0; i < dtClientes.Rows.Count; i++) {
                            //Añadir movimiento a la lista
                            reporteECC.Add(new clientesContpaq() { seleccionar = int.Parse(dtClientes.Rows[i]["CIDCLIENTEPROVEEDOR"].ToString()), nombre = dtClientes.Rows[i]["CRAZONSOCIAL"].ToString(), sucursal = nombreSucursal, numcliente = dtClientes.Rows[i]["CCODIGOCLIENTE"].ToString() });

                        }
                    }
                    
                }

                //Traer clientes de Concretos Saltillo Facturable
                //cadena = cContpaq.BDContpaq();
                //dtClientes = cContpaq.obtenerDatosCliente(cadena);
                //for (int i = 0; i < dtClientes.Rows.Count; i++) {
                //    //Añadir movimiento a la lista
                //    reporteECC.Add(new clientesContpaq() { seleccionar = int.Parse(dtClientes.Rows[i]["CIDCLIENTEPROVEEDOR"].ToString()), nombre = dtClientes.Rows[i]["CRAZONSOCIAL"].ToString(), sucursal = "Concretos Saltillo Facturable", numcliente = dtClientes.Rows[i]["CCODIGOCLIENTE"].ToString() });

                //}

                ////Traer clientes de Concretos Saltillo Ventas General
                //cadena = cContpaq.BDConcrSalVG();
                //dtClientes = cContpaq.obtenerDatosCliente(cadena);
                //for (int i = 0; i < dtClientes.Rows.Count; i++) {
                //    //Añadir movimiento a la lista
                //    reporteECC.Add(new clientesContpaq() { seleccionar = int.Parse(dtClientes.Rows[i]["CIDCLIENTEPROVEEDOR"].ToString()), nombre = dtClientes.Rows[i]["CRAZONSOCIAL"].ToString(), sucursal = "Concretos Saltillo Venta General", numcliente = dtClientes.Rows[i]["CCODIGOCLIENTE"].ToString() });

                //}

                ////Traer clientes de Block Saltillo Facturable
                //cadena = cContpaq.BDBlockSalFac();
                //dtClientes = cContpaq.obtenerDatosCliente(cadena);
                //for (int i = 0; i < dtClientes.Rows.Count; i++) {
                //    //Añadir movimiento a la lista
                //    reporteECC.Add(new clientesContpaq() { seleccionar = int.Parse(dtClientes.Rows[i]["CIDCLIENTEPROVEEDOR"].ToString()), nombre = dtClientes.Rows[i]["CRAZONSOCIAL"].ToString(), sucursal = "Block Saltillo Facturable", numcliente = dtClientes.Rows[i]["CCODIGOCLIENTE"].ToString() });

                //}

                ////Traer clientes de Block Saltillo Ventas General
                //cadena = cContpaq.BDBlockSalVG();
                //dtClientes = cContpaq.obtenerDatosCliente(cadena);
                //for (int i = 0; i < dtClientes.Rows.Count; i++) {
                //    //Añadir movimiento a la lista
                //    reporteECC.Add(new clientesContpaq() { seleccionar = int.Parse(dtClientes.Rows[i]["CIDCLIENTEPROVEEDOR"].ToString()), nombre = dtClientes.Rows[i]["CRAZONSOCIAL"].ToString(), sucursal = "Block Saltillo Ventas General", numcliente = dtClientes.Rows[i]["CCODIGOCLIENTE"].ToString() });

                //}

                ////Traer clientes de Block Irapuato
                //cadena = cContpaq.BDBlockIra();
                //dtClientes = cContpaq.obtenerDatosCliente(cadena);
                //for (int i = 0; i < dtClientes.Rows.Count; i++) {
                //    //Añadir movimiento a la lista
                //    reporteECC.Add(new clientesContpaq() { seleccionar = int.Parse(dtClientes.Rows[i]["CIDCLIENTEPROVEEDOR"].ToString()), nombre = dtClientes.Rows[i]["CRAZONSOCIAL"].ToString(), sucursal = "Block Irapuato", numcliente = dtClientes.Rows[i]["CCODIGOCLIENTE"].ToString() });

                //}

                ////Traer clientes de Irapuato Concretos
                //cadena = cContpaq.BDIraConcretos();
                //dtClientes = cContpaq.obtenerDatosCliente(cadena);
                //for (int i = 0; i < dtClientes.Rows.Count; i++) {
                //    //Añadir movimiento a la lista
                //    reporteECC.Add(new clientesContpaq() { seleccionar = int.Parse(dtClientes.Rows[i]["CIDCLIENTEPROVEEDOR"].ToString()), nombre = dtClientes.Rows[i]["CRAZONSOCIAL"].ToString(), sucursal = "Irapuato Concretos", numcliente = dtClientes.Rows[i]["CCODIGOCLIENTE"].ToString() });

                //}
            }

            var serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = 2147483647;
            var serializedResult = serializer.Serialize(reporteECC);

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", "llenarAgGrid(" + serializedResult + ");", true);

        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            if (txtFechaInicio.Text.Equals(""))
            {
                lblError.Text = "Favor de seleccionar fecha de inicio";
                return;
            }
            else if (txtFechaFin.Text.Equals(""))
            {
                lblError.Text = "Favor de seleccionar fecha de fin";
                return;
            }
            else if (ddlCliente.SelectedValue.Equals("0"))
            {
                lblError.Text = "Favor de seleccionar un cliente";
                return;
            }
            else if (txtNombreCliente.Text.Equals(""))
            {
                lblError.Text = "Favor de seleccionar un cliente";
                return;
            }
            else if (txtNombreCliente.Text.Equals(null))
            {
                lblError.Text = "Favor de seleccionar un cliente";
                return;
            }
            else {
                string conexion = "";
                string cadena2 = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;

                //consulta a BD de pepi 
                try {
                    DataTable dtContpaq = new DataTable();
                    using (SqlConnection conn = new SqlConnection(cadena2)) {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("SELECT top(1) conexionBD FROM configContpaq WHERE idConfigContpaq = 1", conn)) {
                            using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                                sda.Fill(dtContpaq);
                                conexion = dtContpaq.Rows[0]["conexionBD"].ToString();
                            }
                        }
                    }
                }
                catch (Exception ex) {
                    throw (ex);
                }

                DataTable dt = new DataTable();
                string id = "";
                using (SqlConnection conn = new SqlConnection(conexion)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT CIDCLIENTEPROVEEDOR FROM dbo.admClientes WHERE CRAZONSOCIAL LIKE '" + txtNombreCliente.Text+"'", conn)) {
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            if (!dt.Rows.Count.Equals(0)) {
                                
                                foreach (DataRow dr in dt.Rows) {
                                    id = dr["CIDCLIENTEPROVEEDOR"].ToString();
                                }

                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sendClient", "sendReport(" + int.Parse(Request.Cookies["ksroc"]["idSucursal"]) + ", '" + txtFechaInicio.Text + "', '" + txtFechaFin.Text + "', '" + id + "');", true);

                            }
                            else {
                                lblError.Text = "El cliente no existe";
                                return;
                            }
                        }
                    }

                }

                
            }
            
            //Response.Write("<script>window.open('ReporteCuentaCliente.aspx?idSucursal=" + int.Parse(Request.Cookies["ksroc"]["idSucursal"]) + "&FechaInicio=" +
                //txtFechaInicio.Text + "&FechaFin=" + txtFechaFin.Text + "&CIDCLIENTEPROVEEDOR=" + txtNombreCliente.Text+ "' ,'_blank');</script>");

        }
        protected void btnExcel_Click(object sender, EventArgs e)
        {
            if (txtFechaInicio.Text.Equals(""))
            {
                lblError.Text = "Favor de seleccionar fecha de inicio";
                return;
            }
            else if (txtFechaFin.Text.Equals(""))
            {
                lblError.Text = "Favor de seleccionar fecha de fin";
                return;
            }
            else if (txtNombreCliente.Text.Equals(""))
            {
                lblError.Text = "Favor de seleccionar un cliente";
            }
            Response.Write("<script>window.open('ReporteCuentaCliente.aspx?idSucursal=" + int.Parse(Request.Cookies["ksroc"]["idSucursal"]) + "&FechaInicio=" +
                txtFechaInicio.Text + "&FechaFin=" + txtFechaFin.Text + "&CIDCLIENTEPROVEEDOR=" + txtNombreCliente.Text + "&Excel=1" + "' ,'_blank');</script>");

        }

        

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> getDataNombreCliente(string prefixText) {

            string conexion = "";
            string cadena2 = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;
            
            //consulta a BD de pepi 
            try {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena2)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT top(1) conexionBD FROM configContpaq WHERE idConfigContpaq = 1", conn)) {
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            conexion = dt.Rows[0]["conexionBD"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex) {
                throw (ex);
            }

            //consulta a clientes de contpaqi
            try {
                List<string> equis = new List<string>();
                string cadena = conexion;
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena)) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT CIDCLIENTEPROVEEDOR, CRAZONSOCIAL FROM dbo.admClientes WHERE ((LOWER(CRAZONSOCIAL) LIKE '%" + prefixText + "%') OR (UPPER(CRAZONSOCIAL) LIKE '%" + prefixText + "%'))", conn)) {
                        //cmd.Parameters.Add(new SqlParameter("@idInicial", IdInicial));
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) {
                            sda.Fill(dt);
                            if (!dt.Rows.Count.Equals(0)) {
                                List<String> list = new List<String>();
                                foreach (DataRow dr in dt.Rows) {
                                    list.Add(dr["CRAZONSOCIAL"].ToString());
                                }
                                int abc = list.Count;
                                return list;
                            }
                            else {
                                equis.Add("No existe el cliente que coincida con lo introducido, favor de registrarlo");
                                return equis;
                            }
                        }
                    }
                }

            }
            catch (Exception ex) {
                throw (ex);
            }


        }

        protected void txtFechaFin_TextChanged(object sender, EventArgs e) {
            lblError.Text = "";
        }
    }
}

public class clientesContpaq {
    public int seleccionar { get; set; }
    public string nombre { get; set; }
    public string sucursal { get; set; }
    public string numcliente { get; set; }
}