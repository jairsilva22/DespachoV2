using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class filtroCancelar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //variables de pagina
                cFactura fact = new cFactura();
                cClientes clnt = new cClientes();
                cDocumento doc = new cDocumento();
                cUsuarios user = new cUsuarios();
                cSucursales emp = new cSucursales();

                //llenamos los combos
                if (!IsPostBack)
                {

                    //empresa
                    ddlSucursal.DataSource = emp.obtenerSucursales();
                    ddlSucursal.DataTextField = "nombre";
                    ddlSucursal.DataValueField = "id";
                    ddlSucursal.DataBind();
                    ddlSucursal.Items.Insert(0, new ListItem("Seleccionar", "0"));

                    if (Request.QueryString["id"] != "")
                    {
                        ddlSucursal.SelectedValue = Request.QueryString["id"];
                    }
                    //Año
                    ddlAño.DataSource = fact.añoFactura();
                    ddlAño.DataTextField = "fecha";
                    ddlAño.DataValueField = "fecha";
                    ddlAño.DataBind();

                    //mes
                    ddlMes.SelectedValue = DateTime.Today.Month.ToString();

                    //cliente
                    ddlCliente.DataSource = clnt.obtenerClientesFacturacion();
                    ddlCliente.DataTextField = "nombreCliente";
                    ddlCliente.DataValueField = "idCliente";
                    ddlCliente.DataBind();
                    ddlCliente.Items.Insert(0, new ListItem("Seleccionar", "0"));

                    //tipoDocumento
                    ddlTipo.DataSource = doc.documentos();
                    ddlTipo.DataTextField = "descripcion";
                    ddlTipo.DataValueField = "iddocumento";
                    ddlTipo.DataBind();
                    ddlTipo.Items.Insert(0, new ListItem("Seleccionar", "0"));

                    //usuario
                    ddlUsuario.DataSource = user.obtenerUsuarios();
                    ddlUsuario.DataTextField = "Nombre";
                    ddlUsuario.DataValueField = "id";
                    ddlUsuario.DataBind();
                    ddlUsuario.Items.Insert(0, new ListItem("Seleccionar", "0"));
                }
            }
            catch (Exception)
            {

            }

        }

        protected void reporteCancelados_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReporteCancelados.aspx?folio=" + txtFolio.Text + "&rfc=" + txtRfc.Text + "&mes=" + ddlMes.SelectedValue + "&anio=" + ddlAño.SelectedValue + "&cliente=" + ddlCliente.SelectedValue +
                "&estatus=" + ddlEstado.SelectedValue + "&doc=" + ddlTipo.SelectedValue + "&user=" + ddlUsuario.SelectedValue + "&idSucursal=" + ddlSucursal.SelectedValue);
        }

        //propiedades para pasar los datos a la otra pagina
        public string folio
        {
            get
            {
                return txtFolio.Text;
            }
        }
        public string rfc
        {
            get
            {
                return txtRfc.Text;
            }
        }
        public int mes
        {
            get
            {
                return int.Parse(ddlMes.SelectedValue);
            }
        }
        public int año
        {
            get
            {
                return int.Parse(ddlAño.SelectedValue);
            }
        }
        public string cliente
        {
            get
            {
                return (ddlCliente.SelectedValue);
            }
        }
        public string estatus
        {
            get
            {
                return ddlEstado.SelectedValue;
            }
        }
        public int doc
        {
            get
            {
                return int.Parse(ddlTipo.SelectedValue);
            }
        }
        public int user
        {
            get
            {
                return int.Parse(ddlUsuario.SelectedValue);
            }
        }
        public int idSucursal
        {
            get
            {
                return int.Parse(ddlSucursal.SelectedValue);
            }
        }
    }
}