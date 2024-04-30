using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class empleadosAdd : System.Web.UI.Page
    {
        cUtilidades cUtl = new cUtilidades();
        cEmpleados cEmp = new cEmpleados();
        cSucursales cSuc = new cSucursales();
        cDepartamento cDepartamento = new cDepartamento();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                cUtl.idUsuarioActivo = int.Parse(Request.Cookies["ksroc"]["id"]);
                if (!IsPostBack)
                {
                    cargarControles();
                    ddlDepartamentos.Items.Add("Seleccione departamento");
                }
            }
            catch (Exception)
            {

            }
        }

        protected void cargarControles()
        {
            cDepartamento.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
            DataTable dpto = cDepartamento.obtenerDptos();
            ddlDepartamentos.DataSource = dpto;
            ddlDepartamentos.DataValueField = "idDpto";
            ddlDepartamentos.DataTextField = "departamento";
            ddlDepartamentos.DataBind();
            ddlDepartamentos.SelectedIndex = 0;
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
                    using (SqlCommand cmd = new SqlCommand("SELECT usuario FROM usuarios WHERE idSucursal = " + contextKey + " AND LOWER(usuario) LIKE '%" + prefixText + "%' OR UPPER(usuario) LIKE '%" + prefixText + "%'", conn))
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
                    using (SqlCommand cmd = new SqlCommand("SELECT nombre FROM usuarios WHERE idSucursal = " + contextKey + " AND LOWER(nombre) LIKE '%" + prefixText + "%' OR UPPER(nombre) LIKE '%" + prefixText + "%'", conn))
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
            if (cEmp.existe(txtNombre.Text))
            {
                lblError.Text = "Ya existe el Empleado, no es posible dar de alta otro registro con el mismo Nombre";
                txtNombre.Focus();
            }
            else
            {
                    cEmp.nombre = txtNombre.Text;
                    cEmp.telefono = txtTelefono.Text;
                    cEmp.email = txtEmail.Text;
                    cEmp.idDpto = int.Parse(ddlDepartamentos.SelectedValue);
                    cEmp.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                    Response.Redirect("empleados.aspx");
            }
        }

        private bool validarPwd(string p1, string p2)
        {
            if (p1.Equals(p2))
                return true;
            else
                return false;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("empleados.aspx");
        }

    }
}
