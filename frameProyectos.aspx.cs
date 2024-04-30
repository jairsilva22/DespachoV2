using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class frameProyectos : System.Web.UI.Page
    {
        cUtilidades cUtl = new cUtilidades();
        cProyectos cProy = new cProyectos();
        cCodigosPostales cCP = new cCodigosPostales();
        cClientes cClt = new cClientes();
        cEstados cEst = new cEstados();
        cCiudades cCd = new cCiudades();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                cUtl.idUsuarioActivo = int.Parse(Request.Cookies["ksroc"]["id"]);
                if (!IsPostBack)
                {
                    hfIdCliente.Value = Request.QueryString["id"];
                    //hfIdCliente.Value = Eval("id").ToString();
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
            cClt.id = int.Parse(Request.QueryString["id"]);
            cClt.obtenerClienteByID(cClt.id);
            lblCliente.Text = cClt.nombre;
            //llenarGrid(cClt.id);

        }
        protected void cargarControles()
        {
            llenarEstados();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("clientes.aspx");
        }

        protected void lvDetalles_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("update"))
                {
                    string[] arr;
                    arr = e.CommandArgument.ToString().Split('ˇ');
                    hfIdProy.Value = arr[0];
                    txtProyecto.Text = arr[1];
                    txtCP.Text = arr[2];
                    if (txtCP.Text.Equals("") || txtCP.Text.Equals("0"))
                    {
                        hfIdEstado.Value = arr[7].ToString();
                        ddlEstados.SelectedValue = hfIdEstado.Value;
                        llenarCiudades(int.Parse(ddlEstados.SelectedValue));
                        hfIdCiudad.Value = arr[8].ToString();
                        ddlCiudades.SelectedValue = hfIdCiudad.Value;
                        hfSearchBy.Value = "cd";
                        cbxColonias.Items.Clear();
                        ListItem li = new ListItem();
                        li.Value = arr[3];
                        li.Text = arr[3];
                        cbxColonias.Items.Add(li);

                    }
                    else
                    {
                        fillColonias(int.Parse(txtCP.Text), true, 0);
                    }
                    try
                    {
                        cbxColonias.SelectedValue = arr[3];
                    }
                    catch (Exception ex)
                    {
                        ListItem li = new ListItem();
                        li.Text = arr[3];
                        li.Value = arr[3];
                        cbxColonias.Items.Add(li);
                        cbxColonias.SelectedValue = arr[3];
                    }

                    txtCalle.Text = arr[4];
                    txtNumero.Text = arr[5];
                    txtInterior.Text = arr[6];
                }
                if (e.CommandName.Equals("delete"))
                {
                    string[] arr;
                    arr = e.CommandArgument.ToString().Split('ˇ');
                    hfIdProy.Value = arr[0];
                    this.mlblTitle.Text = "¡¡¡CONFIRMACIÓN!!!";
                    this.mlblMessage.Text = "¿Estás seguro que deseas eliminar el proyecto " + arr[1] + "?";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ShowPopup();", true);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void lvDetalles_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {

        }

        protected void mbtnClose_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopup();", true);

        }

        protected void mbtnAceptar_Click(object sender, EventArgs e)
        {
            cProy.id = int.Parse(hfIdProy.Value);
            cProy.eliminar(int.Parse(Request.Cookies["ksroc"]["id"]));
            //llenarGrid(int.Parse(Request.QueryString["id"]));
            cleanInfo();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopup();", true);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            cProy.idCliente = int.Parse(Request.QueryString["id"]);
            if (cProy.idCliente.Equals(0))
                if (!hfIdCliente.Value.Equals(""))
                    cProy.idCliente = int.Parse(hfIdCliente.Value);
                else
                {
                    lblError.Text = "Favor de regresar a la página anterior e iniciar el proceso de alta o actualización nuevamente";
                    return;
                }

            if (txtProyecto.Text.Equals(""))
            {
                lblError.Text = "Favor de introducir el nombre del proyecto";
                return;
            }
            cProy.nombre = txtProyecto.Text;
            if (!txtCP.Text.Equals(""))
            {
                cProy.cp = int.Parse(txtCP.Text);
                cCP.cp = cProy.cp;
            }
            hfIdEstado.Value = ddlEstados.SelectedValue;
            if (hfIdEstado.Value.Equals(""))
            {
                lblError.Text = "Favor de seleccionar el Estado del país al que pertenece el proyecto";
                return;
            }
            cProy.idEstado = int.Parse(hfIdEstado.Value);
            hfIdEstado.Value = ddlCiudades.SelectedValue;
            if (hfIdCiudad.Value.Equals(""))
            {
                lblError.Text = "Favor de seleccionar la ciudad al que pertenece el proyecto";
                return;
            }
            cProy.idCiudad = int.Parse(hfIdCiudad.Value);
            if (cbxColonias.Text.Equals(""))
            {
                lblError.Text = "Favor de seleccionar una colonia o ingresar el nombre de la colonia para agregarla al sistema";
                return;
            }
            cCP.asenta = cbxColonias.SelectedItem.Text;
            if (!cCP.existe() && !cCP.cp.Equals(0))
            {
                cCP.idEstado = int.Parse(hfIdEstado.Value);
                cCP.idCiudad = int.Parse(hfIdCiudad.Value);
                cCP.insertar();
            }
            cProy.colonia = cbxColonias.SelectedItem.Text;
            if (txtCalle.Text.Equals(""))
            {
                lblError.Text = "Favor de introducir la calle del proyecto";
                return;
            }
            cProy.calle = txtCalle.Text;
            if (txtNumero.Text.Equals(""))
            {
                lblError.Text = "Favor de introducir el número del proyecto";
                return;
            }
            cProy.numero = int.Parse(txtNumero.Text);
            cProy.interior = txtInterior.Text;
            if (hfIdProy.Value != "")
            {
                cProy.id = int.Parse(hfIdProy.Value);
            }
            if (cProy.existeEnCliente())
            {
                cProy.actualizar(int.Parse(Request.Cookies["ksroc"]["id"]));
            }
            else
            {
                if (hfIdProy.Value.Equals("") || hfIdProy.Value.Equals("0"))
                {
                    cProy.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alerta", "AlertaConfirmacion();", true);

                }
                else
                {
                    cProy.actualizar(int.Parse(Request.Cookies["ksroc"]["id"]));
                }
            }
            //llenarGrid2(cProy.idCliente);
            cleanInfo();
        }

        protected void llenarGrid2(int id)
        {
            cProy.obtenerProyectosByIdCliente(id);

        }

        private void cleanInfo()
        {
            try
            {
                hfIdProy.Value = "";
                hfIdEstado.Value = "";
                hfIdCiudad.Value = "";
                hfIdEstadoCP.Value = "";
                hfIdCiudadCP.Value = "";
                txtProyecto.Text = "";
                txtCP.Text = "";
                ddlEstados.SelectedIndex = 0;
                ddlCiudades.Items.Clear();
                ddlCiudades.SelectedIndex = 0;
                cbxColonias.Items.Clear();
                //cbxColonias.SelectedIndex = 0;
                //cbxColonias.Text = "";
                txtCalle.Text = "";
                txtNumero.Text = "";
                txtInterior.Text = "";
            }
            catch (Exception)
            {

            }
        }
        protected void txtCP_TextChanged(object sender, EventArgs e)
        {
            if (!cbxColonias.Text.Length.Equals(0))
                if (hfSearchBy.Value.Equals("cd"))
                {
                    lblError.Text = "Favor de limpiar el campo de Colonia si desea buscar por Código Postal, por lo contrario se utilizará el Código Postal escrito para dar de alta la Colonia";
                    return;
                }
            hfSearchBy.Value = "cp";
            if (txtCP.Text.Length == 5)
            {
                fillColonias(int.Parse(txtCP.Text), true, 0);
            }
        }

        private void fillColonias(int value, bool hasCP, int idE)
        {
            cbxColonias.Items.Clear();
            if (hasCP)
            {
                DataTable dt = cCP.getColoniasByCP(value);
                cbxColonias.DataSource = dt;
                cbxColonias.DataValueField = "asenta";
                cbxColonias.DataTextField = "asenta";
                cbxColonias.DataBind();

                hfIdEstadoCP.Value = dt.Rows[0]["idEstado"].ToString();
                hfIdEstado.Value = hfIdEstadoCP.Value;
                ddlEstados.SelectedValue = hfIdEstadoCP.Value;
                llenarCiudades(int.Parse(ddlEstados.SelectedValue));
                hfIdCiudadCP.Value = dt.Rows[0]["idCiudad"].ToString();
                ddlCiudades.SelectedValue = hfIdCiudadCP.Value;
                hfIdCiudad.Value = hfIdCiudadCP.Value;
            }
            else
            {
                DataTable dt = cCP.getColoniasByIdC(value, idE);
                cbxColonias.DataSource = dt;
                cbxColonias.DataValueField = "asenta";
                cbxColonias.DataTextField = "asenta";
                cbxColonias.DataBind();
            }
            cbxColonias.Focus();
        }
        protected void llenarCiudades(int idEst)
        {
            ddlCiudades.Items.Clear();
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("ciudad");

            //dt.Rows.Add("0", "Selecciona un Estado");

            DataTable dt1 = cCd.obtenerCiudadesByIdEstado(idEst);
            foreach (DataRow dr in dt1.Rows)
            {
                dt.Rows.Add(dr["id"].ToString(), dr["ciudad"].ToString());
            }

            ddlCiudades.DataSource = dt;
            ddlCiudades.DataValueField = "id";
            ddlCiudades.DataTextField = "ciudad";
            ddlCiudades.DataBind();
        }
        protected void llenarEstados()
        {
            ddlEstados.Items.Clear();
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("estado");

            dt.Rows.Add("0", "Selecciona un Estado");

            DataTable dt1 = cEst.obtenerEstados();
            foreach (DataRow dr in dt1.Rows)
            {
                dt.Rows.Add(dr["id"].ToString(), dr["estado"].ToString());
            }

            ddlEstados.DataSource = dt;
            ddlEstados.DataValueField = "id";
            ddlEstados.DataTextField = "estado";
            ddlEstados.DataBind();
        }

        protected void lvDetalles_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {

        }

        protected void ddlEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            hfIdEstado.Value = ddlEstados.SelectedValue;
            llenarCiudades(int.Parse(hfIdEstado.Value));
        }

        protected void ddlCiudades_SelectedIndexChanged(object sender, EventArgs e)
        {
            hfIdCiudad.Value = ddlCiudades.SelectedValue;
            hfSearchBy.Value = "cd";
            fillColonias(int.Parse(hfIdCiudad.Value), false, int.Parse(hfIdEstado.Value));
        }

        protected void cbxColonias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (hfSearchBy.Value.Equals("cd"))
            {
                cCP.idEstado = int.Parse(hfIdEstado.Value);
                cCP.idCiudad = int.Parse(hfIdCiudad.Value);
                cCP.asenta = cbxColonias.SelectedItem.Text;
                txtCP.Text = cCP.obtenerCPByAsentaAndIdC();
                txtCalle.Focus();
            }

        }
    }
}