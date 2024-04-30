using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class sucursalesAdd : System.Web.UI.Page
    {
        cUtilidades cUtl = new cUtilidades();
        cSucursales cSuc = new cSucursales();
        cCiudades cCD = new cCiudades();
        cEstados cED = new cEstados();
        cRegimenFiscal cRF = new cRegimenFiscal();
        cCodigosPostales cCP = new cCodigosPostales();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                cUtl.idUsuarioActivo = int.Parse(Request.Cookies["ksroc"]["id"]);
                if (!IsPostBack)
                {
                    fillSucursales();
                    llenarEstados();

                    ddlRegimen.DataSource = cRF.llenarRegimen();
                    ddlRegimen.DataValueField = "codigoSAT";
                    ddlRegimen.DataTextField = "descripcion";
                    ddlRegimen.DataBind();
                    ddlRegimen.Items.Insert(0, new ListItem("Seleccionar", ""));

                    txtIva.Text = "16";
                    txtPath.Text = @"C:\inetpub\wwwroot\Pepi\despacho";
                    txtPathUtilerias.Text = @"C:\inetpub\wwwroot\Pepi\despacho";
                    txtCadena.Text = @"Data Source=WIN-H7CMFN1LO9U\SQLEXPRESS01;Initial Catalog=pepi;user id=voss; password=voss*2017;";
                    txtSmtp.Text = "mail.catedralsoftware.com";

                    txtAncho.Text = "130";
                    txtAlto.Text = "70";
                }
            }
            catch (Exception)
            {

            }
        }


        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text.Equals(""))
            {
                lblError.Text = "Favor de ingresar el nombre de la Sucursal";
                return;
            }
            cSuc.nombre = txtNombre.Text;
            if (txtCodigo.Text.Equals(""))
            {
                lblError.Text = "Favor de ingresar el código de la Sucursal";
                return;
            }
            cSuc.codigo = txtCodigo.Text;
            if (txtRazon.Text.Equals(""))
            {
                lblError.Text = "Favor de ingresar la Razón Social que utilizará la Sucursal en cotizaciones";
                return;
            }
            cSuc.razon = txtRazon.Text;
            if (txtRFC.Text.Equals(""))
            {
                lblError.Text = "Favor de ingresar el RFC de la Sucursal";
                return;
            }
            cSuc.rfc = txtRFC.Text;
            if(ddlRegimen.SelectedIndex.Equals(0))
            {
                lblError.Text = "Favor de seleccionar el regimen fiscal de la Sucursal";
                return;
            }
            cSuc.regimenFiscal = int.Parse(ddlRegimen.SelectedValue);
            cSuc.idPais = 1;
            if (ddlEstado.SelectedIndex.Equals(0))
            { 
                lblError.Text = "Favor de seleccionar un Estado";
                return;
            }
            cSuc.idEstado = int.Parse(ddlEstado.SelectedValue);
            if (ddlCiudades.SelectedIndex.Equals(0))
            {
                lblError.Text = "Favor de seleccionar una Ciudad";
                return;
            }
            cSuc.idCiudad = cCD.obtenerIdByIdCiudadANDIdEstado(cSuc.idEstado, int.Parse(ddlCiudades.SelectedValue));
            if (txtCP.Text.Equals(""))
            {
                lblError.Text = "Favor de ingresar el código postal de la Sucursal";
                return;
            }
            cSuc.cp = int.Parse(txtCP.Text);
            if (txtCalle.Text.Equals(""))
            {
                lblError.Text = "Favor de ingresar la calle de la Sucursal";
                return;
            }
            if (cbxColonia.SelectedValue.Equals(""))
            {
                lblError.Text = "Favor de seleccionar una Colonia";
                return;
            }
            cSuc.colonia = cbxColonia.SelectedValue;
            cSuc.calle = txtCalle.Text;
            if (txtNumero.Text.Equals(""))
            {
                lblError.Text = "Favor de ingresar el número de la Sucursal";
                return;
            }
            cSuc.numero = int.Parse(txtNumero.Text);
            cSuc.interior = txtInterior.Text;
            //if (fuCer.HasFile)
            //{
            //    cSuc.nombreCer = fuCer.FileName;
            //    fuCer.SaveAs(Path.Combine(Server.MapPath("DatosFiscales/"), cSuc.nombreCer));            }
            //else
            //{
            //    lblError.Text = "Favor de ingresar el archivo .cer";
            //    return;
            //}
            
            //if (fuKey.HasFile)
            //{
            //    cSuc.nombreKey = fuKey.FileName;
            //    fuKey.SaveAs(Server.MapPath("DatosFiscales/") + fuKey.FileName);
            //}
            //else
            //{
            //    lblError.Text = "Favor de ingresar el archivo .key";
            //    return;
            //}
            
            //if (txtContraArch.Text.Equals(""))
            //{
            //    lblError.Text = "Favor de ingresar la contraseña de los archivos";
            //    return;
            //}
            //cSuc.contraArchivos = txtContraArch.Text;
            if (fuLogo.HasFile)
            {
                cSuc.logo = fuLogo.FileName;
                fuLogo.SaveAs(Server.MapPath("DatosFiscales/") + fuLogo.FileName);
            }
            else
            {
                lblError.Text = "Favor de ingresar el logo";
                return;
            }
            if (txtAncho.Text.Equals(""))
            {
                lblError.Text = "Favor de ingresar el ancho del logo";
                return;
            }
            cSuc.ancho = txtAncho.Text;
            if (txtAlto.Text.Equals(""))
            {
                lblError.Text = "Favor de ingresar el alto del logo";
                return;
            }
            cSuc.alto = txtAlto.Text;
            //if (txtNCarpeta.Text.Equals(""))
            //{
            //    lblError.Text = "Favor de ingresar la NCarpeta";
            //    return;
            //}
            //cSuc.ncarpeta = txtNCarpeta.Text;
            //if(txtCarpetaTimbre.Text.Equals(""))
            //{
            //    lblError.Text = "Favor de ingresar la carpeta XML Timbre";
            //    return;
            //}
            //cSuc.carpetaTimbre = txtCarpetaTimbre.Text;
            if(txtIva.Text.Equals(""))
            {
                lblError.Text = "Favor de ingresar el IVA";
                return;
            }
            if (txtPath.Text.Equals(""))
            {
                lblError.Text = "Favor de ingresar el Path";
                return;
            }
            cSuc.path = txtPath.Text;
            if (txtPathUtilerias.Text.Equals(""))
            {
                lblError.Text = "Favor de ingresar el Path Utilerias";
                return;
            }
            cSuc.pathUtilerias = txtPathUtilerias.Text;
            if (txtCadena.Text.Equals(""))
            {
                lblError.Text = "Favor de ingresar la Cadena";
                return;
            }
            cSuc.cad = txtCadena.Text;
            if (txtSmtp.Text.Equals(""))
            {
                lblError.Text = "Favor de ingresar el Smtp";
                return;
            }
            cSuc.smtp = txtSmtp.Text;
            if (txtCorreo.Text.Equals(""))
            {
                lblError.Text = "Favor de ingresar el Correo";
                return;
            }
            cSuc.correo = txtCorreo.Text;
            if (txtContraCorreo.Text.Equals(""))
            {
                lblError.Text = "Favor de ingresar la Contraseña del Correo";
                return;
            }
            cSuc.pass = txtContraCorreo.Text;
            if (txtComisionD.Text.Equals(""))
            {
                lblError.Text = "Favor de ingresar la Comisión por cliente directo del vendedor";
                return;
            }
            cSuc.comisionDirecto = txtComisionD.Text;
            if (txtComisionI.Text.Equals(""))
            {
                lblError.Text = "Favor de ingresar la Comisión por cliente indirecto del vendedor";
                return;
            }
            cSuc.comisionIndirecto = txtComisionI.Text;
            cSuc.observacionesCot = txtObservaciones.Text;
            cSuc.responsabilidadRem = txtDisclaiment.Text;
            cSuc.responsabilidadRemB = "";
            cSuc.simulacion = chbxSimulacion.Checked;
            cSuc.idSucursalCompras = int.Parse(ddlSucursalCompras.SelectedValue);
            if (!cSuc.existe())
            {
                cSuc.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                cSuc.insertarConfig(cSuc.id);
                Response.Redirect("sucursales.aspx");
            }
            else
                lblError.Text = "Ya existe una Sucursal con el mismo nombre y código";
        }

        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarCiudades(int.Parse(ddlEstado.SelectedValue));
        }
        protected void llenarEstados()
        {
            ddlEstado.Items.Clear();
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("estado");

            dt.Rows.Add("0", "Selecciona un estado");

            DataTable dt1 = cED.obtenerEstados();
            foreach (DataRow dr in dt1.Rows)
            {
                dt.Rows.Add(dr["id"].ToString(), (dr["estado"].ToString()));
            }

            ddlEstado.DataSource = dt;
            ddlEstado.DataValueField = "id";
            ddlEstado.DataTextField = "estado";
            ddlEstado.DataBind();
        }

        protected void llenarCiudades(int idE)
        {
            ddlCiudades.Items.Clear();
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("ciudad");

            dt.Rows.Add("0", "Selecciona una ciudad");

            DataTable dt1 = cCD.obtenerCiudadesByIdEstado(idE);
            foreach (DataRow dr in dt1.Rows)
            {
                dt.Rows.Add(dr["id"].ToString(), (dr["ciudad"].ToString()));
            }

            ddlCiudades.DataSource = dt;
            ddlCiudades.DataValueField = "id";
            ddlCiudades.DataTextField = "ciudad";
            ddlCiudades.DataBind();
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("sucursales.aspx");
        }

        protected void txtCP_TextChanged(object sender, EventArgs e)
        {
            if (txtCP.Text.Length == 5)
            {
                fillColonias(int.Parse(txtCP.Text));
            }
        }

        private void fillColonias(int cp)
        {
            cbxColonia.Items.Clear();
            DataTable dt = cCP.getColoniasByCP(cp);
            cbxColonia.DataSource = dt;
            cbxColonia.DataValueField = "asenta";
            cbxColonia.DataTextField = "asenta";
            cbxColonia.DataBind();

            ddlEstado.SelectedValue = dt.Rows[0]["idEstado"].ToString();
            llenarCiudades(int.Parse(ddlEstado.SelectedValue));
            ddlCiudades.SelectedValue = dt.Rows[0]["idCiudad"].ToString();
        }
        private void fillSucursales()
        {
            ddlSucursalCompras.Items.Clear();
            DataTable dt = cSuc.obtenerSucursalesCompras();
            DataRow rw = dt.NewRow();
            rw["id"] = "0";
            rw["nombre"] = "Selecciona una sucursal";
            dt.Rows.Add(rw);

            ddlSucursalCompras.DataSource = dt;
            ddlSucursalCompras.DataValueField = "id";
            ddlSucursalCompras.DataTextField = "nombre";
            ddlSucursalCompras.DataBind();
            ddlSucursalCompras.SelectedValue = "0";
        }
    }
}
