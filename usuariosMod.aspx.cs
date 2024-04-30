using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class usuariosMod : System.Web.UI.Page
    {
        cUtilidades cUtl = new cUtilidades();
        cUsuarios cUsr = new cUsuarios();
        cTurnos cTur = new cTurnos();
        cPerfiles cPer = new cPerfiles();
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
            cUsr.obtenerUsuario(int.Parse(Request.QueryString["id"]));
            lblUsuario.Text = cUsr.usuario.ToString();
            txtNombre.Text = cUsr.nombre.ToString();
            txtPasswd1.Text = cUsr.password;
            txtPasswd2.Text = cUsr.password;
            chbxActivo.Checked = cUsr.activo;
            txtTelefono.Text = cUsr.telefono;
            txtEmail.Text = cUsr.email;
            hfIdPerfil.Value = cUsr.idPerfil.ToString();
            ddlPerfil.SelectedValue = hfIdPerfil.Value;
        }
        protected void cargarControles()
        {
            //llenarTurnos();
            llenarPerfiles();
            //llenarSucursales();
        }
        //protected void llenarSucursales()
        //{
        //    ddlSucursales.Items.Clear();
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("id");
        //    dt.Columns.Add("nombre");

        //    dt.Rows.Add("0", "Selecciona una Sucursal");

        //    DataTable dt1 = cSuc.obtenerSucursales();
        //    foreach (DataRow dr in dt1.Rows)
        //    {
        //        dt.Rows.Add(dr["id"].ToString(), dr["nombre"].ToString());
        //    }

        //    ddlSucursales.DataSource = dt;
        //    ddlSucursales.DataValueField = "id";
        //    ddlSucursales.DataTextField = "nombre";
        //    ddlSucursales.DataBind();
        //}

        //protected void llenarTurnos()
        //{
        //    ddlTurno.Items.Clear();
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("id");
        //    dt.Columns.Add("turno");

        //    dt.Rows.Add("0", "Selecciona un Turno");

        //    DataTable dt1 = cTur.obtenerTurnos();
        //    foreach (DataRow dr in dt1.Rows)
        //    {
        //        dt.Rows.Add(dr["id"].ToString(), dr["turno"].ToString());
        //    }

        //    ddlTurno.DataSource = dt;
        //    ddlTurno.DataValueField = "id";
        //    ddlTurno.DataTextField = "turno";
        //    ddlTurno.DataBind();
        //}
        protected void llenarPerfiles()
        {
            ddlPerfil.Items.Clear();
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("descripcion");

            dt.Rows.Add("0", "Selecciona un Perfil");

            DataTable dt1 = cPer.obtenerPerfiles();
            foreach (DataRow dr in dt1.Rows)
            {
                dt.Rows.Add(dr["id"].ToString(), dr["descripcion"].ToString());
            }

            ddlPerfil.DataSource = dt;
            ddlPerfil.DataValueField = "id";
            ddlPerfil.DataTextField = "descripcion";
            ddlPerfil.DataBind();
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
            cUsr.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
            if (cUsr.existeUsuario(lblUsuario.Text))
            {
                cUsr.password = txtPasswd1.Text;
                if (validarPwd(txtPasswd1.Text, txtPasswd2.Text))
                {
                    cUsr.usuario = lblUsuario.Text;
                    cUsr.nombre = txtNombre.Text;
                    cUsr.password = txtPasswd1.Text;
                    cUsr.activo = chbxActivo.Checked;
                    cUsr.telefono = txtTelefono.Text;
                    cUsr.email = txtEmail.Text;
                    if (ddlPerfil.SelectedIndex.Equals(0) || ddlPerfil.SelectedValue.Equals(""))
                    {
                        lblError.Text = "Favor de seleccionar un perfil";
                        return;
                    }
                    cUsr.idPerfil = int.Parse(ddlPerfil.SelectedValue);
                    cUsr.actualizar(int.Parse(Request.QueryString["id"]), int.Parse(Request.Cookies["ksroc"]["id"]));
                    Response.Redirect("usuarios.aspx");
                }
            }
        }

        protected void txtNombre_TextChanged(object sender, EventArgs e)
        {
            //cUsr.nombre = txtNombre.Text;
            //DataTable dt = cUsr.obtenerUsuarioByNombre(cUsr.nombre);
            //if (dt.Rows.Count > 0)
            //{
            //    lblError.Text = "Ya existe el Usuario, se llenarán los campos con su información";
            //    txtUsuario.Text = dt.Rows[0]["usuario"].ToString();
            //    txtTelefono.Text = dt.Rows[0]["telefono"].ToString();
            //    txtEmail.Text = dt.Rows[0]["email"].ToString();
            //    hfIdTurno.Value = dt.Rows[0]["idTurno"].ToString();
            //    ddlTurno.SelectedValue = hfIdTurno.Value;
            //    hfIdPerfil.Value = dt.Rows[0]["idPerfil"].ToString();
            //    ddlPerfil.SelectedValue = hfIdPerfil.Value;
            //}
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
            Response.Redirect("usuarios.aspx");
        }

        protected void btnVer1_Click(object sender, EventArgs e)
        {
            string val = txtPasswd1.Text;
            if (txtPasswd1.TextMode == TextBoxMode.Password)
            {
                btnVer1.Text = "Ocultar";
                txtPasswd1.TextMode = TextBoxMode.SingleLine;
                txtPasswd1.Text = val;
            }
            else
            {
                btnVer1.Text = "Ver";
                txtPasswd1.TextMode = TextBoxMode.Password;
                txtPasswd1.Text = val;
            }
        }

        protected void btnVer2_Click(object sender, EventArgs e)
        {
            string val = txtPasswd2.Text;
            if (txtPasswd2.TextMode == TextBoxMode.Password)
            {
                btnVer2.Text = "Ocultar";
                txtPasswd2.TextMode = TextBoxMode.SingleLine;
                txtPasswd2.Text = val;
            }
            else
            {
                btnVer2.Text = "Ver";
                txtPasswd2.TextMode = TextBoxMode.Password;
                txtPasswd2.Text = val;
            }
        }

        //protected void txtPasswd2_TextChanged(object sender, EventArgs e)
        //{
        //    if (validarPwd(txtPasswd1.Text, txtPasswd2.Text))
        //        hdPwd.Value = txtPasswd2.Text;
        //    else
        //    { 
        //      lblError.Text = "Las contraseñas no coinciden, favor de verificar sean iguales";
        //        txtPasswd2.Focus();
        //    }
        //}
    }
}
