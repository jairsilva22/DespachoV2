using System;
using System.Data;

namespace despacho
{
    public partial class unidadesTransporteAdd : System.Web.UI.Page
    {
        cUtilidades cUtl = new cUtilidades();
        cUTransporte cUT = new cUTransporte();
        cTipoUT cTut = new cTipoUT();
        cUDM cUdm = new cUDM();
        cTipoProducto cTP = new cTipoProducto();
        cSucursales cSuc = new cSucursales();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                cUtl.idUsuarioActivo = int.Parse(Request.Cookies["ksroc"]["id"]);
                if (!IsPostBack)
                {
                    cargarControles();
                }
            }
            catch (Exception)
            {

            }
        }
        protected void cargarControles()
        {
            llenarUDM();
            llenarTUT();
        }
        protected void llenarUDM()
        {
            ddlUDM.Items.Clear();
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("unidad");

            dt.Rows.Add("0", "Selecciona una Unidad");

            DataTable dt1 = cUdm.obtenerUDMActivas();
            foreach (DataRow dr in dt1.Rows)
            {
                dt.Rows.Add(dr["id"].ToString(), dr["unidad"].ToString());
            }

            ddlUDM.DataSource = dt;
            ddlUDM.DataValueField = "id";
            ddlUDM.DataTextField = "unidad";
            ddlUDM.DataBind();
        }
        protected void llenarTUT()
        {
            ddlTU.Items.Clear();
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("tipo");

            dt.Rows.Add("0", "Selecciona un Tipo de Unidad");
            cTut.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
            DataTable dt1 = cTut.obtenerUT();
            foreach (DataRow dr in dt1.Rows)
            {
                dt.Rows.Add(dr["id"].ToString(), dr["tipo"].ToString() + " - " + dr["capacidad"].ToString() + " - " + dr["unidad"].ToString());
            }

            ddlTU.DataSource = dt;
            ddlTU.DataValueField = "id";
            ddlTU.DataTextField = "tipo";
            ddlTU.DataBind();
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            cUT.nombre = txtNombre.Text;
            cUT.matricula = txtMatricula.Text;
            if (txtCapacidad.Text.Equals(""))
            {
                lblError.Text = "Por favor de introducir la capacidad de carga de la unidad";
                return;
            }
            cUT.capacidad = float.Parse(txtCapacidad.Text);
            if (txtCapacidadMax.Text.Equals(""))
            {
                lblError.Text = "Por favor de introducir la capacidad máxima de carga de la unidad";
                return;
            }
            cUT.capacidadMax = float.Parse(txtCapacidadMax.Text);
            cUT.combustible = ddlCombustible.SelectedValue;
            cUT.color = "#" + txtColor.Text;
            if (ddlUDM.SelectedIndex.Equals(0))
            { 
                lblError.Text = "Por favor selecciona una unidad de medida";
                return;
            }
            cUT.idUDM = int.Parse(ddlUDM.SelectedValue);
            if (ddlTU.SelectedIndex.Equals(0))
            {
                lblError.Text = "Por favor selecciona un tipo de Unidad";
                return;
            }
            cUT.idTipoUT = int.Parse(ddlTU.SelectedValue);

            cUT.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);

            if (cUT.existeUT(cUT.nombre, cUT.color, cUT.idSucursal))
            {
                lblError.Text = "Ya existe el color y nombre de Unidad en la sucursal, favor de revisar la información";
            }
            else
            {
                cUT.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                Response.Redirect("unidadesTransporte.aspx");
            }
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("unidadesTransporte.aspx");
        }
    }
}
