using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class empleados : System.Web.UI.Page
    {
        cUtilidades cUtl = new cUtilidades();
        cEmpleados cEmp = new cEmpleados();
        cEmpleadosPercepciones per = new cEmpleadosPercepciones();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                cUtl.idUsuarioActivo = int.Parse(Request.Cookies["ksroc"]["id"]);
                if (!IsPostBack)
                {
                    llenarGrid();
                }
            }
            catch (Exception)
            {

            }
        }

        protected void llenarGrid()
        {
            listView.DataSource = cEmp.obtenerBySucursal(int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
            listView.DataBind();
        }

        protected void listView_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("delete"))
                {
                    string[] arr;
                    arr = e.CommandArgument.ToString().Split('ˇ');
                    hfId.Value = arr[0];
                    this.mlblTitle.Text = "¡¡¡CONFIRMACIÓN!!!";
                    this.mlblMessage.Text = "¿Estás seguro que deseas eliminar el empleado " + arr[1] + " del sistema?";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ShowPopup();", true);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void listView_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void mbtnAceptar_Click(object sender, EventArgs e)
        {
            cEmp.eliminar(int.Parse(hfId.Value), int.Parse(Request.Cookies["ksroc"]["id"]));
            llenarGrid();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopup();", true);
        }

        protected void mbtnClose_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopup();", true);
        }

        protected void btnEmpleados_Click(object sender, EventArgs e)
        {
            string sucursal = Request.Cookies["ksroc"]["idSucursal"].ToString();
            if (sucursal == "1")
            {
                per.ObtenerDatosPEPI("delete empleados where idsucursal=1");
                DataTable dt = per.ObtenerDatosCOMESaltilloConcretos("select nombrelargo,codigoempleado, nom10001.iddepartamento from nom10001 inner join nom10003 on nom10001.iddepartamento = nom10003.iddepartamento ");
                foreach (DataRow empleados in dt.Rows)
                {
                    per.ObtenerDatosPEPI("insert into empleados (nombre,idsucursal,idusuario,fechaalta,codigo,dpto) values ('" + empleados["nombrelargo"].ToString() + "','" + sucursal + "','4025','" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + empleados["codigoempleado"].ToString() + "','" + empleados["iddepartamento"].ToString() + "')");
                }
                llenarGrid();
                lblError.Text = "Importacion Existosa";
                lblError.BackColor = System.Drawing.Color.Red;
                lblError.ForeColor = System.Drawing.Color.Yellow;
            }
            else if (sucursal == "3")
            {
                per.ObtenerDatosPEPI("delete empleados where idsucursal=3");
                DataTable dt = per.ObtenerDatosCOMSaltilloBlock("select nombrelargo,codigoempleado, nom10001.iddepartamento from nom10001 inner join nom10003 on nom10001.iddepartamento = nom10003.iddepartamento ");
                foreach (DataRow empleados in dt.Rows)
                {
                    per.ObtenerDatosPEPI("insert into empleados (nombre,idsucursal,idusuario,fechaalta,codigo,dpto) values ('" + empleados["nombrelargo"].ToString() + "','" + sucursal + "','4025','" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + empleados["codigoempleado"].ToString() + "','" + empleados["iddepartamento"].ToString() + "')");
                }
                llenarGrid();
                lblError.Text = "Importacion Existosa";
                lblError.BackColor = System.Drawing.Color.Red;
                lblError.ForeColor = System.Drawing.Color.Yellow;

            }
            else if (sucursal == "2")
            {
                per.ObtenerDatosPEPI("delete empleados where idsucursal=2");
                DataTable dt = per.ObtenerDatosCOMIrapuatoConcretos("select nombrelargo,codigoempleado, nom10001.iddepartamento from nom10001 inner join nom10003 on nom10001.iddepartamento = nom10003.iddepartamento  ");
                foreach (DataRow empleados in dt.Rows)
                {
                    per.ObtenerDatosPEPI("insert into empleados (nombre,idsucursal,idusuario,fechaalta,codigo,dpto) values ('" + empleados["nombrelargo"].ToString() + "','" + sucursal + "','4025','" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + empleados["codigoempleado"].ToString() + "','" + empleados["iddepartamento"].ToString() + "')");
                }
                llenarGrid();
                lblError.Text = "Importacion Existosa";
                lblError.BackColor = System.Drawing.Color.Red;
                lblError.ForeColor = System.Drawing.Color.Yellow;
            }
            else if (sucursal == "1006")
            {
                per.ObtenerDatosPEPI("delete empleados where idsucursal=1006");
                DataTable dt = per.ObtenerDatosCOMIrapuatoBlock("select nombrelargo,codigoempleado, nom10001.iddepartamento from nom10001 inner join nom10003 on nom10001.iddepartamento = nom10003.iddepartamento ");
                foreach (DataRow empleados in dt.Rows)
                {
                    per.ObtenerDatosPEPI("insert into empleados (nombre,idsucursal,idusuario,fechaalta,codigo,dpto) values ('" + empleados["nombrelargo"].ToString() + "','" + sucursal + "','4025','" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + empleados["codigoempleado"].ToString() + "','" + empleados["iddepartamento"].ToString() + "')");
                }
                llenarGrid();
                lblError.Text = "Importacion Existosa";
                lblError.BackColor = System.Drawing.Color.Red;
                lblError.ForeColor = System.Drawing.Color.Yellow;
            }
            else if (sucursal == "1010")
            {
                per.ObtenerDatosPEPI("delete empleados where idsucursal=1010");
                DataTable dt = per.ObtenerDatosCOMExternos("select nombrelargo,codigoempleado, nom10001.iddepartamento from nom10001 inner join nom10003 on nom10001.iddepartamento = nom10003.iddepartamento  ");
                foreach (DataRow empleados in dt.Rows)
                {
                    per.ObtenerDatosPEPI("insert into empleados (nombre,idsucursal,idusuario,fechaalta,codigo,dpto) values ('" + empleados["nombrelargo"].ToString() + "','" + sucursal + "','4025','" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + empleados["codigoempleado"].ToString() + "','" + empleados["iddepartamento"].ToString() + "')");
                }
                llenarGrid();
                lblError.Text = "Importacion Existosa";
                lblError.BackColor = System.Drawing.Color.Red;
                lblError.ForeColor = System.Drawing.Color.Yellow;
            }
        }
    }
}