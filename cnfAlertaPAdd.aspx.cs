﻿using System;
using System.Data;

namespace despacho
{
    public partial class cnfAlertaPAdd : System.Web.UI.Page
    {
        cUtilidades cUtl = new cUtilidades();
        cAlertaP cAP = new cAlertaP();
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
            llenarSucursales();
        }
        protected void llenarSucursales()
        {
            ddlSucursal.Items.Clear();
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("nombre");

            dt.Rows.Add("0", "Selecciona la Sucursal");

            DataTable dt1 = cSuc.obtenerSucursales();
            foreach (DataRow dr in dt1.Rows)
            {
                dt.Rows.Add(dr["id"].ToString(), dr["nombre"].ToString());
            }

            ddlSucursal.DataSource = dt;
            ddlSucursal.DataValueField = "id";
            ddlSucursal.DataTextField = "nombre";
            ddlSucursal.DataBind();
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (ddlColor.SelectedIndex.Equals(0))
            {
                lblError.Text = "Por favor de seleccionar el color";
                return;
            }
            cAP.colorNombre = ddlColor.SelectedItem.Text;
            cAP.color = ddlColor.SelectedValue;
            if (txtTiempo.Text.Equals(""))
            {
                lblError.Text = "Por favor de introducir el tiempo";
                return;
            }
            cAP.tiempo = int.Parse(txtTiempo.Text);
            if (ddlSucursal.SelectedIndex.Equals(0))
            {
                lblError.Text = "Por favor selecciona una Sucursal";
                return;
            }
            cAP.idSucursal = int.Parse(ddlSucursal.SelectedValue);

            if (cAP.existe(cAP.colorNombre, cAP.idSucursal))
            {
                lblError.Text = "Ya existe el color " + ddlColor.SelectedItem.Text + " en la sucursal " + ddlSucursal.SelectedItem.Text + ", favor de revisar la información";
            }
            else
            {
                cAP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                Response.Redirect("cnfAlertaP.aspx");
            }
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("cnfAlertaP.aspx");
        }

        protected void ddlColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
