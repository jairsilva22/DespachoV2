using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class empleadosMod : System.Web.UI.Page
    {
        cUtilidades cUtl = new cUtilidades();
        cEmpleados cEmp = new cEmpleados();
        cSucursales cSuc = new cSucursales();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                cUtl.idUsuarioActivo = int.Parse(Request.Cookies["ksroc"]["id"]);
                if (!IsPostBack)
                {
                    cargarControles();
                    cargarDatos();
                }
            }
            catch (Exception)
            {

            }
        }
        private void cargarDatos()
        {
            cEmp.obtenerEmpleado(int.Parse(Request.QueryString["id"]));
            txtNombre.Text = cEmp.nombre.ToString();
            lblNombre.Text = cEmp.nombre.ToString();
            txtTelefono.Text = cEmp.telefono;
            txtEmail.Text = cEmp.email;
        }
        protected void cargarControles()
        {
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> getDataUsuario(string prefixText, int count, string contextKey)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnx"].ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT usuario FROM usuarios WHERE LOWER(usuario) LIKE '%" + prefixText + "%' OR UPPER(usuario) LIKE '%" + prefixText + "%'", conn))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            List<String> list = new List<String>();
                            foreach (DataRow dr in dt.Rows)
                            {
                                list.Add(dr["usuario"].ToString());
                            }
                            return list;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> getDataNombre(string prefixText, int count, string contextKey)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnx"].ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT nombre FROM usuarios WHERE LOWER(nombre) LIKE '%" + prefixText + "%' OR UPPER(nombre) LIKE '%" + prefixText + "%'", conn))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            List<String> list = new List<String>();
                            foreach (DataRow dr in dt.Rows)
                            {
                                list.Add(dr["nombre"].ToString());// + "-" + dr["nombre"].ToString()) ;
                            }
                            return list;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            cEmp.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                    cEmp.nombre = txtNombre.Text;
                    cEmp.telefono = txtTelefono.Text;
                    cEmp.email = txtEmail.Text;
                    cEmp.actualizar(int.Parse(Request.QueryString["id"]), int.Parse(Request.Cookies["ksroc"]["id"]));
                    Response.Redirect("empleados.aspx");
        }


        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("empleados.aspx");
        }

    }
}
