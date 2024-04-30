using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Timers;
using System.Web.UI;

namespace despacho
{
    public partial class productosAdd : System.Web.UI.Page

    {
        private static System.Timers.Timer aTimer;
        cUtilidades cUtl = new cUtilidades();
        cProductos cProd = new cProductos();
        cUDM cUdm = new cUDM();
        cTipoProducto cTP = new cTipoProducto();
        cCategorias cCat = new cCategorias();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                cUtl.idUsuarioActivo = int.Parse(Request.Cookies["ksroc"]["id"]);
                txtCodigo_AutoCompleteExtender.ContextKey = Request.Cookies["ksroc"]["idSucursal"];
                txtDescripcion_AutoCompleteExtender.ContextKey = Request.Cookies["ksroc"]["idSucursal"];
                if (!IsPostBack)
                {
                    llenarUDM();
                    llenarTP();
                }
            }
            catch (Exception)
            {

            }
        }
        protected void llenarUDM()
        {
            ddlUnidad.Items.Clear();
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("unidad");

            dt.Rows.Add("0", "Selecciona una Unidad de Medida");

            //cUdm.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
            DataTable dt1 = cUdm.obtenerUDMActivas();
            foreach (DataRow dr in dt1.Rows)
            {
                dt.Rows.Add(dr["id"].ToString(), dr["unidad"].ToString());
            }

            ddlUnidad.DataSource = dt;
            ddlUnidad.DataValueField = "id";
            ddlUnidad.DataTextField = "unidad";
            ddlUnidad.DataBind();
        }

        protected void llenarTP()
        {
            ddlTipoProducto.Items.Clear();
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("tipo");

            dt.Rows.Add("0", "Selecciona un Tipo de Producto");

            cTP.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
            DataTable dt1 = cTP.obtenerByIdSucursal();
            foreach (DataRow dr in dt1.Rows)
            {
                dt.Rows.Add(dr["id"].ToString(), dr["tipo"].ToString());
            }

            ddlTipoProducto.DataSource = dt;
            ddlTipoProducto.DataValueField = "id";
            ddlTipoProducto.DataTextField = "tipo";
            ddlTipoProducto.DataBind();
        }
        protected void llenarCat(int idTP)
        {
            ddlCategoria.Items.Clear();
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("categoria");

            dt.Rows.Add("0", "Selecciona una Categoría");

            cCat.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
            cCat.idTipoProducto = idTP;
            DataTable dt1 = cCat.obtenerDdlByIDTP();
            try
            {
                foreach (DataRow dr in dt1.Rows)
                {
                    dt.Rows.Add(dr["id"].ToString(), dr["categoria"].ToString());
                }
            }
            catch (Exception)
            {

            }

            ddlCategoria.DataSource = dt;
            ddlCategoria.DataValueField = "id";
            ddlCategoria.DataTextField = "categoria";
            ddlCategoria.DataBind();

            if (dt.Rows.Count > 1)
                ddlCategoria.SelectedIndex = 1;
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> getDataDescripcionProducto(string prefixText, int count, string contextKey)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnx"].ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT descripcion FROM productos WHERE (idSucursal=" + contextKey + ") AND  ((LOWER(descripcion) LIKE '%" + prefixText + 
                        "%') OR (UPPER(descripcion) LIKE '%" + prefixText + "%'))", conn))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            List<String> list = new List<String>();
                            foreach (DataRow dr in dt.Rows)
                            {
                                list.Add(dr["descripcion"].ToString());
                            }
                            return list;
                        }
                    }

                }

            }
            catch (Exception)
            {
                return null;
            }
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> getDataCcodigoProducto(string prefixText, int count, string contextKey)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnx"].ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT codigo FROM productos WHERE (idSucursal=" + contextKey + ") AND  ((LOWER(codigo) LIKE '%" + prefixText + 
                        "%') OR (UPPER(codigo) LIKE '%" + prefixText + "%'))", conn))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            List<String> list = new List<String>();
                            foreach (DataRow dr in dt.Rows)
                            {
                                list.Add(dr["codigo"].ToString());// + "-" + dr["nombre"].ToString()) ;
                            }
                            return list;
                        }
                    }

                }

            }
            catch (Exception)
            {
                return null;
            }
        }

        private void cleanInfo()
        {
            txtCodigoSAT.Text = "";
            txtPrecio.Text = "";
            ddlCategoria.SelectedIndex=0;
            txtCodigo.Text = "";
            txtDescripcion.Text = "";
            ddlUnidad.SelectedIndex = 0;
            ddlTipoProducto.SelectedIndex = 0;
            lblError.Text = "";
            txtCodigo.Focus();
        }
        protected void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            cProd.codigo = txtCodigo.Text;
            cProd.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
            DataTable dt = cProd.obtenerProductoByCodigo(txtCodigo.Text);
            if (dt.Rows.Count > 0)
            {
                lblError.Text = "Ya existe el código, se llenarán los campos con la información del producto";
                txtDescripcion.Text = dt.Rows[0]["descripcion"].ToString();
                ddlUnidad.SelectedValue = dt.Rows[0]["idUDM"].ToString();
                ddlTipoProducto.SelectedValue = dt.Rows[0]["idTipoProducto"].ToString();
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {

       
            try
            {

                if (txtCodigo.Text.Equals(""))
                {
                    lblError.Text = "Favor de ingresar el código del producto";
                    return;
                }
                if (ddlTipoProducto.SelectedItem.Text.ToLower().Contains("concr"))
                {
                    if (txtCodigo.Text.ToLower().Contains("td") || txtCodigo.Text.ToLower().Contains("bba"))
                    {

                    }
                    else
                    {
                        lblError.Text = "Favor de agregar la nomenclatura TD o BBA en el código del producto";
                        return;
                    }
                }
                cProd.codigo = txtCodigo.Text;
                if (txtDescripcion.Text.Equals(""))
                {
                    lblError.Text = "Favor de ingresar la descripción del producto";
                    return;
                }
                cProd.descripcion = txtDescripcion.Text;
                if (ddlUnidad.SelectedIndex.Equals(0) || ddlUnidad.SelectedValue.Equals("0"))
                {
                    lblError.Text = "Favor de seleccionar una Unidad de Medida";
                    return;
                }
                cProd.unidad = ddlUnidad.SelectedItem.Text;
                cProd.idUDM = int.Parse(ddlUnidad.SelectedValue);
                if (ddlTipoProducto.SelectedIndex.Equals(0) || ddlTipoProducto.SelectedValue.Equals("0"))
                {
                    lblError.Text = "Favor de seleccionar un Tipo de Producto";
                    return;
                }
                cProd.idTipoProducto = int.Parse(ddlTipoProducto.SelectedValue);
                if (ddlCategoria.SelectedIndex.Equals(0) || ddlCategoria.SelectedValue.Equals("0"))
                {
                    lblError.Text = "Favor de seleccionar una categoría para el producto";
                    return;
                }
                cProd.idCategoria = int.Parse(ddlCategoria.SelectedValue);
                if (ddlCategoria.SelectedItem.Text.ToLower().Contains("concr"))
                    cProd.peso = 0;
                else
                    cProd.peso = float.Parse(txtPeso.Text);
                if (txtPrecio.Text.Equals(""))
                    txtPrecio.Text = "0";
                cProd.precio = float.Parse(txtPrecio.Text);
                if (txtIVA.Text.Equals(""))
                    txtIVA.Text = "0.16";
                cProd.carga = int.Parse(txtCarga.Text);
                cProd.iva = txtIVA.Text;
                cProd.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                cProd.codigoSAT = txtCodigoSAT.Text;
                cProd.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));

                cProd.id = cProd.obtenerIDProductoByCodigo(cProd.codigo);
                cProd.homologar(cProd.id, cProd.idSucursal, int.Parse(Request.Cookies["ksroc"]["id"]));




            }
            catch (Exception ex)
            {
                if (ex.Message == "ok")
                {

                    //script que te abre el modal 
                    string errorMessage = "Producto agregado correctamente ";
                    ScriptManager.RegisterStartupScript(this, GetType(), "showErrorModal", $"$('#errorMessage').text('{errorMessage}'); $('#errorModal .modal-header').addClass('bg-success bg-gradient'); $('#errorModal').modal('show');", true);

                    cleanInfo();
                   
                }
                else
                {
                    string errorMessage = "Error: " + ex.Message;
                    ScriptManager.RegisterStartupScript(this, GetType(), "showErrorModal", $"$('#errorMessage').text('{errorMessage}'); $('#errorModal .modal-header').addClass('bg-danger bg-gradient'); $('#errorModal').modal('show');", true);

                }


            }
        }


        //FIUNCION PARA POCICIONARSE EN LA ALERTA
       


        protected void ddlTipoProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarCat(int.Parse(ddlTipoProducto.SelectedValue));
            if (!ddlTipoProducto.SelectedItem.Text.ToLower().Contains("concreto"))
            {
                txtCarga.Text = "0";
                lblCarga.Visible = true;
                txtCarga.Visible = true;
                txtPeso.Text = "0";
                lblPeso.Visible = true;
                txtPeso.Visible = true;
            }
            else
            {
                lblCarga.Visible = false;
                txtCarga.Visible = false;
                lblPeso.Visible = false;
                txtPeso.Visible = false;
            }
        }
    }
}
