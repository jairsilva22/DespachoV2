using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class BancosMod : System.Web.UI.Page
    {
        cBancos bco = new cBancos();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bco.id = int.Parse(Request.QueryString["id"]);
                bco.obtenerBco();

                txtBco.Text = bco.nombre;
                cbRecibo.Checked = bco.recibo;
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            lblError.Text = "";

            if (txtBco.Text == "")
            {
                lblError.Text = "Favor de Ingresar el Nombre del Banco.";
            }
            else
            {
                bco.id = int.Parse(Request.QueryString["id"]);
                bco.nombre = txtBco.Text;
                if (bco.existeBco2())
                {
                    lblError.Text = "El Banco ya existe, favor de ingresar otro.";
                }
                else
                {
                    bco.idUsuario = int.Parse(Request.Cookies["ksroc"]["id"]);
                    bco.recibo = cbRecibo.Checked;
                    bco.modificarBco();
                    Response.Redirect("Bancos.aspx");
                }
            }
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Bancos.aspx");
        }
    }
}