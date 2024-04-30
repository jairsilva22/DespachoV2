using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class empleadosPercepciones : System.Web.UI.Page
    {
        cUtilidades cUtl = new cUtilidades();
        cEmpleados cEmp = new cEmpleados();
        cTipoPercepcion cTP = new cTipoPercepcion();
        cEmpleadosPercepciones cEP = new cEmpleadosPercepciones();
        cDepartamento dpto = new cDepartamento();
        static string NombreMes(int month)
        {
            return CultureInfo.CurrentCulture.
                DateTimeFormat.GetMonthName
                (month);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                cUtl.idUsuarioActivo = int.Parse(Request.Cookies["ksroc"]["id"]);
                if (!IsPostBack)
                {
                    ddlMes.SelectedValue = DateTime.Now.Month.ToString();
                    cargarControles();
                    cargarDatos();
                    //EnlistarMES();
                    ddlPeriodo.Items.Add("Selecione Periodo");

                    for (int i = 1; i <= 53; i++)
                    {
                        ddlPeriodo.Items.Add(i.ToString());

                    }
                    ddlEjercicio.Items.Add("Selecionar Ejercicio");

                    int ano = DateTime.Now.Year;
                    for (int i = 2021; i <= ano; i++)
                    {
                        ddlEjercicio.Items.Add(i.ToString());

                    }

                }
            }
            catch (Exception)
            {

            }
        }

        protected void cargarControles()
        {
            llenarEmpleados();
            llenarTiposPercepcion();
            mostrarDatos();
        }
        protected void EnlistarMES()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Numero");
            dt.Columns.Add("Mes");
            dt.Rows.Add("0", "Selecciona Mes");
            dt.Rows.Add("1", "Enero");
            dt.Rows.Add("2", "Ferero");
            dt.Rows.Add("3", "Marzo");
            dt.Rows.Add("4", "Abril");
            dt.Rows.Add("5", "Mayo");
            dt.Rows.Add("6", "Junio");
            dt.Rows.Add("7", "Julio");
            dt.Rows.Add("8", "Agosto");
            dt.Rows.Add("9", "Septiembre");
            dt.Rows.Add("10", "Octubre");
            dt.Rows.Add("11", "Noviembre");
            dt.Rows.Add("12", "Diciembre");
            ddlPeriodo.DataSource = dt;
            ddlPeriodo.DataValueField = "Numero";
            ddlPeriodo.DataTextField = "Mes";
            ddlPeriodo.DataBind();
            ddlPeriodo.SelectedValue = "0";
            //drop.DataSource = dt;
            //drop.DataValueField = "Numero";
            //drop.DataTextField = "Mes";
            //drop.DataBind();
            //drop.SelectedValue = "0";
        }
        protected void llenarEmpleados()
        {
            cbxEmpleados.Items.Clear();
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("nombre");

            dt.Rows.Add("0", "Selecciona un Empleado");

            DataTable dt1 = cEmp.obtenerBySucursal(int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
            foreach (DataRow dr in dt1.Rows)
            {
                dt.Rows.Add(dr["id"].ToString(), dr["nombre"].ToString());
            }

            cbxEmpleados.DataSource = dt;
            cbxEmpleados.DataValueField = "id";
            cbxEmpleados.DataTextField = "nombre";
            cbxEmpleados.DataBind();
            cbxEmpleados.SelectedValue = "0";
        }
        protected void llenarTiposPercepcion()
        {
            cbxPercepciones.Items.Clear();
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("percepcion");

            dt.Rows.Add("0", "Selecciona una percepción");

            DataTable dt1 = cTP.obtenerPercepciones();
            foreach (DataRow dr in dt1.Rows)
            {
                dt.Rows.Add(dr["id"].ToString(), dr["percepcion"].ToString());
            }

            cbxPercepciones.DataSource = dt;
            cbxPercepciones.DataValueField = "id";
            cbxPercepciones.DataTextField = "percepcion";
            cbxPercepciones.DataBind();
            cbxPercepciones.SelectedValue = "0";
        }

        private void cargarDatos()
        {
            //cEmp.obtenerEmpleado(int.Parse(Request.QueryString["id"]));
            //lblEmpleado.Text = cEmp.nombre;
            //txtFecha.Text = DateTime.Now.ToString();
            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //llenarGrid(cTP.id);

        }

        protected void llenarGrid()
        {
            cEP.idEmpleado = int.Parse(cbxEmpleados.SelectedValue);
            lvDetalles.DataSource = cEP.obtenerDatosByIdEmpleado(Request.Cookies["ksroc"]["idSucursal"].ToString());
            lvDetalles.DataBind();
        }

        protected void mostrarDatos()
        {
            lvDetalles.DataSource = cEP.obtenerDatosBySucursal(int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
            lvDetalles.DataBind();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("empleados.aspx");
        }

        protected void lvDetalles_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("update"))
                {
                    string[] arr;
                    arr = e.CommandArgument.ToString().Split('ˇ');
                    hfIdEP.Value = arr[0];
                    cbxEmpleados.SelectedValue = arr[1];
                    cbxPercepciones.SelectedValue = arr[2];
                    txtMonto.Text = arr[3];
                    txtFecha.Text = DateTime.Parse(arr[4]).ToString("dd/MM/yyyy");
                }
                if (e.CommandName.Equals("delete"))
                {
                    string[] arr;
                    arr = e.CommandArgument.ToString().Split('ˇ');
                    hfIdEP.Value = arr[0];
                    this.mlblTitle.Text = "¡¡¡CONFIRMACIÓN!!!";
                    this.mlblMessage.Text = "¿Estás seguro que deseas eliminar la percepción de " + arr[1] + "?";
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
            cEP.id = int.Parse(hfIdEP.Value);
            cEP.eliminar(cEP.id, int.Parse(Request.Cookies["ksroc"]["id"]));
            llenarGrid();
            cleanInfo();
            if (cbxEmpleados.SelectedValue == "")
            {
                mostrarDatos();
            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopup();", true);

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            cEP.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
            if (cbxEmpleados.SelectedValue.Equals(0))
            {
                lblError.Text = "Favor de seleccionar el empleado";
                return;
            }
            cEP.idEmpleado = int.Parse(cbxEmpleados.SelectedValue);
            if (cbxPercepciones.SelectedValue.Equals(0))
            {
                lblError.Text = "Favor de seleccionar la percepción";
                return;
            }
            cEP.idPercepcion = int.Parse(cbxPercepciones.SelectedValue);
            if (txtMonto.Text.Equals(""))
            {
                lblError.Text = "Favor de ingresar un monto";
                return;
            }
            cEP.monto = txtMonto.Text;
            if (txtFecha.Text.Equals(""))
            {
                lblError.Text = "Favor de seleccionar la fecha";
                return;
            }
            cEP.fecha = DateTime.Parse(txtFecha.Text);
            cEP.mes = cEP.fecha.Month;
            try
            {
                if (!hfIdEP.Value.Equals(""))
                    cEP.id = int.Parse(hfIdEP.Value);
            }
            catch (Exception)
            {

            }
            if (cEP.existe())
            {
                if (hfIdEP.Value.Equals(""))
                    cEP.id = int.Parse(cEP.obtenerIdByEmpleadoPercepcionFecha().Rows[0]["id"].ToString());
                cEP.actualizar(cEP.id, int.Parse(Request.Cookies["ksroc"]["id"]));
            }
            else
            {
                if (hfIdEP.Value.Equals("") || hfIdEP.Value.Equals("0"))
                {
                    cEP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                }
                else
                {
                    cEP.actualizar(cEP.id, int.Parse(Request.Cookies["ksroc"]["id"]));
                }
            }
            llenarGrid();
            cleanInfo();
        }

        private void cleanInfo()
        {
            hfIdEP.Value = "";
            txtMonto.Text = "";
            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        protected void lvDetalles_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {

        }

        protected void cbxEmpleados_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarGrid();
        }

        protected void cbxPercepciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMonto.Focus();
        }

        protected void btnCarga_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "mostrarCarga();", true);
        }
        protected void btnSubir_Click(object sender, EventArgs e)
        {
            if (fuArchivo.HasFile)
            {
                string cadena = "";
                string hoja = "";
                DataTable dtHoja = new DataTable();
                DataTable dt;
                string extension = Path.GetExtension(fuArchivo.FileName);

                if (!Directory.Exists(HttpContext.Current.Server.MapPath(@"Archivos")))
                {
                    //Creamos la carpeta
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(@"Archivos"));
                }
                if (extension == ".xls" || extension == ".xlsx")
                {
                    //guardamos el archivo en la carpeta de "Archivos"
                    fuArchivo.SaveAs(MapPath(@"Archivos\") + fuArchivo.FileName);
                    cadena = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + Server.MapPath(@"Archivos/" + fuArchivo.FileName) +
                        "';Extended Properties='Excel 12.0; HDR=YES; ImportMixedTypes=Text;'";
                    try
                    {
                        using (OleDbConnection conn = new OleDbConnection(cadena))
                        {
                            conn.Open();
                            //obtenemos el nombre de la primer hoja del archivo de excel 
                            dtHoja = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            foreach (DataRow row in dtHoja.Rows)
                            {
                                hoja = row["TABLE_NAME"].ToString();
                                break;
                            }

                            //generamos el DataTable para leer el archivo 
                            dt = new DataTable();

                            //hacemos consulta a la hoja
                            using (OleDbCommand cmd = new OleDbCommand("SELECT * FROM [" + hoja + "]", conn))
                            {
                                using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
                                {
                                    da.Fill(dt);
                                }
                            }
                        }

                        for (int i = 8; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i][0].ToString().Trim() != "" && dt.Rows[i][1].ToString().Trim() != "")
                            {
                                //obtenemos info
                                cEmp.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                                cEmp.codigo = dt.Rows[i][0].ToString().Trim();
                                if (cEmp.existeEmp())
                                {

                                    cEP.idEmpleado = cEmp.id;
                                }
                                else
                                {
                                    dpto.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                                    if (dt.Rows[i][2].ToString().Trim() != "")
                                    {
                                        dpto.departamento = dt.Rows[i][2].ToString().Trim();
                                        if (dpto.existeDpto())
                                        {
                                            cEmp.idDpto = dpto.idDpto;
                                        }
                                        else
                                        {
                                            dpto.idUsuario = int.Parse(Request.Cookies["ksroc"]["id"]);
                                            dpto.insertar();
                                            dpto.ultimoID();
                                            cEmp.idDpto = dpto.idDpto;
                                        }
                                    }
                                    else
                                    {
                                        cEmp.idDpto = 0;
                                    }

                                    //insertamos empleado 
                                    cEmp.codigo = dt.Rows[i][0].ToString().Trim();
                                    cEmp.nombre = dt.Rows[i][1].ToString().Trim();
                                    cEmp.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                                    cEmp.insertarEmp(int.Parse(Request.Cookies["ksroc"]["id"]));

                                    cEP.idEmpleado = cEmp.ultimoEmp();
                                }
                                cEP.mes = int.Parse(ddlMes.SelectedValue);
                                //validamos exista percepcion
                                //3 Sueldo
                                cTP.percepcion = "Sueldo";
                                if (cTP.existeP())
                                {
                                    cEP.idPercepcion = cTP.id;
                                }
                                else
                                {
                                    cTP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                    cTP.existeP();
                                    cEP.idPercepcion = cTP.id;
                                }

                                //insertamos percepcion sueldo 
                                if (dt.Rows[i][3].ToString().Trim() != "")
                                {
                                    cEP.monto = dt.Rows[i][3].ToString().Trim().Replace("$", "").Replace(",", "");
                                    cEP.fecha = DateTime.Now;
                                    cEP.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                                    cEP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                }

                                //4 séptimo dia
                                cTP.percepcion = "Séptimo día";
                                if (cTP.existeP())
                                {
                                    cEP.idPercepcion = cTP.id;
                                }
                                else
                                {
                                    cTP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                    cTP.existeP();
                                    cEP.idPercepcion = cTP.id;
                                }

                                //insertamos percepcion sueldo 
                                if (dt.Rows[i][4].ToString().Trim() != "")
                                {
                                    cEP.monto = dt.Rows[i][4].ToString().Trim().Replace("$", "").Replace(",", "");
                                    cEP.fecha = DateTime.Now;
                                    cEP.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                                    cEP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                }

                                //5 horas extras
                                cTP.percepcion = "Horas extras";
                                if (cTP.existeP())
                                {
                                    cEP.idPercepcion = cTP.id;
                                }
                                else
                                {
                                    cTP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                    cTP.existeP();
                                    cEP.idPercepcion = cTP.id;
                                }

                                //insertamos percepcion sueldo 
                                if (dt.Rows[i][5].ToString().Trim() != "")
                                {
                                    cEP.monto = dt.Rows[i][5].ToString().Trim().Replace("$", "").Replace(",", "");
                                    cEP.fecha = DateTime.Now;
                                    cEP.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                                    cEP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                }

                                //6 premios asistencia
                                cTP.percepcion = "Premios Asistencia";
                                if (cTP.existeP())
                                {
                                    cEP.idPercepcion = cTP.id;
                                }
                                else
                                {
                                    cTP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                    cTP.existeP();
                                    cEP.idPercepcion = cTP.id;
                                }

                                //insertamos percepcion sueldo 
                                if (dt.Rows[i][6].ToString().Trim() != "")
                                {
                                    cEP.monto = dt.Rows[i][6].ToString().Trim().Replace("$", "").Replace(",", "");
                                    cEP.fecha = DateTime.Now;
                                    cEP.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                                    cEP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                }

                                //7 premio puntualidad 
                                cTP.percepcion = "Premio puntualidad";
                                if (cTP.existeP())
                                {
                                    cEP.idPercepcion = cTP.id;
                                }
                                else
                                {
                                    cTP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                    cTP.existeP();
                                    cEP.idPercepcion = cTP.id;
                                }

                                //insertamos percepcion sueldo 
                                if (dt.Rows[i][7].ToString().Trim() != "")
                                {
                                    cEP.monto = dt.Rows[i][7].ToString().Trim().Replace("$", "").Replace(",", "");
                                    cEP.fecha = DateTime.Now;
                                    cEP.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                                    cEP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                }

                                //8 vacaciones a tiempo 
                                cTP.percepcion = "Vacaciones a tiempo";
                                if (cTP.existeP())
                                {
                                    cEP.idPercepcion = cTP.id;
                                }
                                else
                                {
                                    cTP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                    cTP.existeP();
                                    cEP.idPercepcion = cTP.id;
                                }

                                //insertamos percepcion sueldo 
                                if (dt.Rows[i][8].ToString().Trim() != "")
                                {
                                    cEP.monto = dt.Rows[i][8].ToString().Trim().Replace("$", "").Replace(",", "");
                                    cEP.fecha = DateTime.Now;
                                    cEP.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                                    cEP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                }

                                //9 prima de vacaciones a tiempo 
                                cTP.percepcion = "Prima de vacaciones a tiempo";
                                if (cTP.existeP())
                                {
                                    cEP.idPercepcion = cTP.id;
                                }
                                else
                                {
                                    cTP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                    cTP.existeP();
                                    cEP.idPercepcion = cTP.id;
                                }

                                //insertamos percepcion sueldo 
                                if (dt.Rows[i][9].ToString().Trim() != "")
                                {
                                    cEP.monto = dt.Rows[i][9].ToString().Trim().Replace("$", "").Replace(",", "");
                                    cEP.fecha = DateTime.Now;
                                    cEP.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                                    cEP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                }

                                //10 vacaciones reportadas 
                                cTP.percepcion = "Vacaciones reportadas $";
                                if (cTP.existeP())
                                {
                                    cEP.idPercepcion = cTP.id;
                                }
                                else
                                {
                                    cTP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                    cTP.existeP();
                                    cEP.idPercepcion = cTP.id;
                                }

                                //insertamos percepcion sueldo 
                                if (dt.Rows[i][10].ToString().Trim() != "")
                                {
                                    cEP.monto = dt.Rows[i][10].ToString().Trim().Replace("$", "").Replace(",", "");
                                    cEP.fecha = DateTime.Now;
                                    cEP.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                                    cEP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                }


                                //11 prima de vacaciones reportadas
                                cTP.percepcion = "Prima de vaciones reportada $";
                                if (cTP.existeP())
                                {
                                    cEP.idPercepcion = cTP.id;
                                }
                                else
                                {
                                    cTP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                    cTP.existeP();
                                    cEP.idPercepcion = cTP.id;
                                }

                                //insertamos percepcion sueldo 
                                if (dt.Rows[i][11].ToString().Trim() != "")
                                {
                                    cEP.monto = dt.Rows[i][11].ToString().Trim().Replace("$", "").Replace(",", "");
                                    cEP.fecha = DateTime.Now;
                                    cEP.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                                    cEP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                }


                                //12 despensa 
                                cTP.percepcion = "Despensa";
                                if (cTP.existeP())
                                {
                                    cEP.idPercepcion = cTP.id;
                                }
                                else
                                {
                                    cTP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                    cTP.existeP();
                                    cEP.idPercepcion = cTP.id;
                                }

                                //insertamos percepcion sueldo 
                                if (dt.Rows[i][12].ToString().Trim() != "")
                                {
                                    cEP.monto = dt.Rows[i][12].ToString().Trim().Replace("$", "").Replace(",", "");
                                    cEP.fecha = DateTime.Now;
                                    cEP.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                                    cEP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                }

                                //13 otras percepciones
                                cTP.percepcion = "Otras Percepciones";
                                if (cTP.existeP())
                                {
                                    cEP.idPercepcion = cTP.id;
                                }
                                else
                                {
                                    cTP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                    cTP.existeP();
                                    cEP.idPercepcion = cTP.id;
                                }

                                //insertamos percepcion sueldo 
                                if (dt.Rows[i][13].ToString().Trim() != "")
                                {
                                    cEP.monto = dt.Rows[i][13].ToString().Trim().Replace("$", "").Replace(",", "");
                                    cEP.fecha = DateTime.Now;
                                    cEP.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                                    cEP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                }

                                ////14 Ret. Inv Y Vida 
                                //cTP.percepcion = "Ret. Inv. Y Vida";
                                //if (cTP.existeP())
                                //{
                                //    cEP.idPercepcion = cTP.id;
                                //}
                                //else
                                //{
                                //    cTP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //    cTP.existeP();
                                //    cEP.idPercepcion = cTP.id;
                                //}

                                ////insertamos percepcion sueldo 
                                //if (dt.Rows[i][14].ToString().Trim() != "")
                                //{
                                //    cEP.monto = dt.Rows[i][14].ToString().Trim().Replace("$", "").Replace(",", "");
                                //    cEP.fecha = DateTime.Now;
                                //    cEP.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                                //    cEP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //}
                                ////15 Ret. Cesantia 
                                //cTP.percepcion = "Ret Cesantia";
                                //if (cTP.existeP())
                                //{
                                //    cEP.idPercepcion = cTP.id;
                                //}
                                //else
                                //{
                                //    cTP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //    cTP.existeP();
                                //    cEP.idPercepcion = cTP.id;
                                //}

                                ////insertamos percepcion sueldo 
                                //if (dt.Rows[i][15].ToString().Trim() != "")
                                //{
                                //    cEP.monto = dt.Rows[i][15].ToString().Trim().Replace("$", "").Replace(",", "");
                                //    cEP.fecha = DateTime.Now;
                                //    cEP.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                                //    cEP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //}

                                ////16 Ret. Enf. y Mat. obrero
                                //cTP.percepcion = "Ret. Enf. y Mat. obrero";
                                //if (cTP.existeP())
                                //{
                                //    cEP.idPercepcion = cTP.id;
                                //}
                                //else
                                //{
                                //    cTP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //    cTP.existeP();
                                //    cEP.idPercepcion = cTP.id;
                                //}

                                ////insertamos percepcion sueldo 
                                //if (dt.Rows[i][16].ToString().Trim() != "")
                                //{
                                //    cEP.monto = dt.Rows[i][16].ToString().Trim().Replace("$", "").Replace(",", "");
                                //    cEP.fecha = DateTime.Now;
                                //    cEP.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                                //    cEP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //}

                                ////17 Préstamo infonavit (FD)
                                //cTP.percepcion = "Préstamo Infonavit (FD)";
                                //if (cTP.existeP())
                                //{
                                //    cEP.idPercepcion = cTP.id;
                                //}
                                //else
                                //{
                                //    cTP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //    cTP.existeP();
                                //    cEP.idPercepcion = cTP.id;
                                //}

                                ////insertamos percepcion sueldo 
                                //if (dt.Rows[i][17].ToString().Trim() != "")
                                //{
                                //    cEP.monto = dt.Rows[i][17].ToString().Trim().Replace("$", "").Replace(",", "");
                                //    cEP.fecha = DateTime.Now;
                                //    cEP.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                                //    cEP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //}

                                ////18 Subs al Empleo acreditado
                                //cTP.percepcion = "Subs al Empleo acreditado";
                                //if (cTP.existeP())
                                //{
                                //    cEP.idPercepcion = cTP.id;
                                //}
                                //else
                                //{
                                //    cTP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //    cTP.existeP();
                                //    cEP.idPercepcion = cTP.id;
                                //}

                                ////insertamos percepcion sueldo 
                                //if (dt.Rows[i][18].ToString().Trim() != "")
                                //{
                                //    cEP.monto = dt.Rows[i][18].ToString().Trim().Replace("$", "").Replace(",", "");
                                //    cEP.fecha = DateTime.Now;
                                //    cEP.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                                //    cEP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //}

                                ////19 Subs al Empleo (mes)
                                //cTP.percepcion = "Subs al Empleo (mes)";
                                //if (cTP.existeP())
                                //{
                                //    cEP.idPercepcion = cTP.id;
                                //}
                                //else
                                //{
                                //    cTP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //    cTP.existeP();
                                //    cEP.idPercepcion = cTP.id;
                                //}

                                ////insertamos percepcion sueldo 
                                //if (dt.Rows[i][19].ToString().Trim() != "")
                                //{
                                //    cEP.monto = dt.Rows[i][19].ToString().Trim().Replace("$", "").Replace(",", "");
                                //    cEP.fecha = DateTime.Now;
                                //    cEP.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                                //    cEP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //}

                                ////20 I.S.R. antes de Subs al Empleo
                                //cTP.percepcion = "I.S.R. antes de Subs al Empleo";
                                //if (cTP.existeP())
                                //{
                                //    cEP.idPercepcion = cTP.id;
                                //}
                                //else
                                //{
                                //    cTP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //    cTP.existeP();
                                //    cEP.idPercepcion = cTP.id;
                                //}

                                ////insertamos percepcion sueldo 
                                //if (dt.Rows[i][20].ToString().Trim() != "")
                                //{
                                //    cEP.monto = dt.Rows[i][20].ToString().Trim().Replace("$", "").Replace(",", "");
                                //    cEP.fecha = DateTime.Now;
                                //    cEP.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                                //    cEP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //}

                                ////21 I.S.R. (mes)
                                //cTP.percepcion = "I.S.R. (mes)";
                                //if (cTP.existeP())
                                //{
                                //    cEP.idPercepcion = cTP.id;
                                //}
                                //else
                                //{
                                //    cTP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //    cTP.existeP();
                                //    cEP.idPercepcion = cTP.id;
                                //}

                                ////insertamos percepcion sueldo 
                                //if (dt.Rows[i][21].ToString().Trim() != "")
                                //{
                                //    cEP.monto = dt.Rows[i][21].ToString().Trim().Replace("$", "").Replace(",", "");
                                //    cEP.fecha = DateTime.Now;
                                //    cEP.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                                //    cEP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //}

                                ////22 Préstamo infonavit (PORC)
                                //cTP.percepcion = "Préstamos infonavit (PORC)";
                                //if (cTP.existeP())
                                //{
                                //    cEP.idPercepcion = cTP.id;
                                //}
                                //else
                                //{
                                //    cTP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //    cTP.existeP();
                                //    cEP.idPercepcion = cTP.id;
                                //}

                                ////insertamos percepcion sueldo 
                                //if (dt.Rows[i][22].ToString().Trim() != "")
                                //{
                                //    cEP.monto = dt.Rows[i][22].ToString().Trim().Replace("$", "").Replace(",", "");
                                //    cEP.fecha = DateTime.Now;
                                //    cEP.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                                //    cEP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //}

                                ////23 Préstamo FONACOT
                                //cTP.percepcion = "Préstamo FONACOT";
                                //if (cTP.existeP())
                                //{
                                //    cEP.idPercepcion = cTP.id;
                                //}
                                //else
                                //{
                                //    cTP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //    cTP.existeP();
                                //    cEP.idPercepcion = cTP.id;
                                //}

                                ////insertamos percepcion sueldo 
                                //if (dt.Rows[i][23].ToString().Trim() != "")
                                //{
                                //    cEP.monto = dt.Rows[i][23].ToString().Trim().Replace("$", "").Replace(",", "");
                                //    cEP.fecha = DateTime.Now;
                                //    cEP.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                                //    cEP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //}

                                ////24 Préstamo empresa
                                //cTP.percepcion = "Préstamo empresa";
                                //if (cTP.existeP())
                                //{
                                //    cEP.idPercepcion = cTP.id;
                                //}
                                //else
                                //{
                                //    cTP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //    cTP.existeP();
                                //    cEP.idPercepcion = cTP.id;
                                //}

                                ////insertamos percepcion sueldo 
                                //if (dt.Rows[i][24].ToString().Trim() != "")
                                //{
                                //    cEP.monto = dt.Rows[i][24].ToString().Trim().Replace("$", "").Replace(",", "");
                                //    cEP.fecha = DateTime.Now;
                                //    cEP.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                                //    cEP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //}

                                ////25 Fondo de ahorro 
                                //cTP.percepcion = "Fondo de ahorro";
                                //if (cTP.existeP())
                                //{
                                //    cEP.idPercepcion = cTP.id;
                                //}
                                //else
                                //{
                                //    cTP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //    cTP.existeP();
                                //    cEP.idPercepcion = cTP.id;
                                //}

                                ////insertamos percepcion sueldo 
                                //if (dt.Rows[i][25].ToString().Trim() != "")
                                //{
                                //    cEP.monto = dt.Rows[i][25].ToString().Trim().Replace("$", "").Replace(",", "");
                                //    cEP.fecha = DateTime.Now;
                                //    cEP.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                                //    cEP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //}

                                ////26 Ajuste en Subsidio para el empleo
                                //cTP.percepcion = "Ajuste en Subsidio para el empleo";
                                //if (cTP.existeP())
                                //{
                                //    cEP.idPercepcion = cTP.id;
                                //}
                                //else
                                //{
                                //    cTP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //    cTP.existeP();
                                //    cEP.idPercepcion = cTP.id;
                                //}

                                ////insertamos percepcion sueldo 
                                //if (dt.Rows[i][26].ToString().Trim() != "")
                                //{
                                //    cEP.monto = dt.Rows[i][26].ToString().Trim().Replace("$", "").Replace(",", "");
                                //    cEP.fecha = DateTime.Now;
                                //    cEP.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                                //    cEP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //}

                                ////27 Subs entregado que no correspondía
                                //cTP.percepcion = "Subs entregado que no correspondia";
                                //if (cTP.existeP())
                                //{
                                //    cEP.idPercepcion = cTP.id;
                                //}
                                //else
                                //{
                                //    cTP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //    cTP.existeP();
                                //    cEP.idPercepcion = cTP.id;
                                //}

                                ////insertamos percepcion sueldo 
                                //if (dt.Rows[i][27].ToString().Trim() != "")
                                //{
                                //    cEP.monto = dt.Rows[i][27].ToString().Trim().Replace("$", "").Replace(",", "");
                                //    cEP.fecha = DateTime.Now;
                                //    cEP.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                                //    cEP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //}

                                ////28 Ajuste al neto
                                //cTP.percepcion = "Ajuste al neto";
                                //if (cTP.existeP())
                                //{
                                //    cEP.idPercepcion = cTP.id;
                                //}
                                //else
                                //{
                                //    cTP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //    cTP.existeP();
                                //    cEP.idPercepcion = cTP.id;
                                //}

                                ////insertamos percepcion sueldo 
                                //if (dt.Rows[i][28].ToString().Trim() != "")
                                //{
                                //    cEP.monto = dt.Rows[i][28].ToString().Trim().Replace("$", "").Replace(",", "");
                                //    cEP.fecha = DateTime.Now;
                                //    cEP.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                                //    cEP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //}

                                ////29 ISR de ajuste mensual
                                //cTP.percepcion = "ISR de ajuste mensual";
                                //if (cTP.existeP())
                                //{
                                //    cEP.idPercepcion = cTP.id;
                                //}
                                //else
                                //{
                                //    cTP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //    cTP.existeP();
                                //    cEP.idPercepcion = cTP.id;
                                //}

                                ////insertamos percepcion sueldo 
                                //if (dt.Rows[i][29].ToString().Trim() != "")
                                //{
                                //    cEP.monto = dt.Rows[i][29].ToString().Trim().Replace("$", "").Replace(",", "");
                                //    cEP.fecha = DateTime.Now;
                                //    cEP.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                                //    cEP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //}

                                ////30 ISR ajustado por subsidio
                                //cTP.percepcion = "ISR ajustado por subsidio";
                                //if (cTP.existeP())
                                //{
                                //    cEP.idPercepcion = cTP.id;
                                //}
                                //else
                                //{
                                //    cTP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //    cTP.existeP();
                                //    cEP.idPercepcion = cTP.id;
                                //}

                                ////insertamos percepcion sueldo 
                                //if (dt.Rows[i][30].ToString().Trim() != "")
                                //{
                                //    cEP.monto = dt.Rows[i][30].ToString().Trim().Replace("$", "").Replace(",", "");
                                //    cEP.fecha = DateTime.Now;
                                //    cEP.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                                //    cEP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //}

                                ////31 Ajuste al Subsidio Causado
                                //cTP.percepcion = "Ajuste al Subsidio Causado";
                                //if (cTP.existeP())
                                //{
                                //    cEP.idPercepcion = cTP.id;
                                //}
                                //else
                                //{
                                //    cTP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //    cTP.existeP();
                                //    cEP.idPercepcion = cTP.id;
                                //}

                                ////insertamos percepcion sueldo 
                                //if (dt.Rows[i][31].ToString().Trim() != "")
                                //{
                                //    cEP.monto = dt.Rows[i][31].ToString().Trim().Replace("$", "").Replace(",", "");
                                //    cEP.fecha = DateTime.Now;
                                //    cEP.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                                //    cEP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //}

                                ////35 Invalidez y Vida 
                                //cTP.percepcion = "Invalides y Vida";
                                //if (cTP.existeP())
                                //{
                                //    cEP.idPercepcion = cTP.id;
                                //}
                                //else
                                //{
                                //    cTP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //    cTP.existeP();
                                //    cEP.idPercepcion = cTP.id;
                                //}

                                ////insertamos percepcion sueldo 
                                //if (dt.Rows[i][35].ToString().Trim() != "")
                                //{
                                //    cEP.monto = dt.Rows[i][35].ToString().Trim().Replace("$", "").Replace(",", "");
                                //    cEP.fecha = DateTime.Now;
                                //    cEP.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                                //    cEP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //}

                                ////36 Cesantia y Vejez
                                //cTP.percepcion = "Cesantia y Vejez";
                                //if (cTP.existeP())
                                //{
                                //    cEP.idPercepcion = cTP.id;
                                //}
                                //else
                                //{
                                //    cTP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //    cTP.existeP();
                                //    cEP.idPercepcion = cTP.id;
                                //}

                                ////insertamos percepcion sueldo 
                                //if (dt.Rows[i][36].ToString().Trim() != "")
                                //{
                                //    cEP.monto = dt.Rows[i][36].ToString().Trim().Replace("$", "").Replace(",", "");
                                //    cEP.fecha = DateTime.Now;
                                //    cEP.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                                //    cEP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //}

                                ////37 Enf. y Mat. Patron
                                //cTP.percepcion = "Enf. y Mat. Patron";
                                //if (cTP.existeP())
                                //{
                                //    cEP.idPercepcion = cTP.id;
                                //}
                                //else
                                //{
                                //    cTP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //    cTP.existeP();
                                //    cEP.idPercepcion = cTP.id;
                                //}

                                ////insertamos percepcion sueldo 
                                //if (dt.Rows[i][37].ToString().Trim() != "")
                                //{
                                //    cEP.monto = dt.Rows[i][37].ToString().Trim().Replace("$", "").Replace(",", "");
                                //    cEP.fecha = DateTime.Now;
                                //    cEP.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                                //    cEP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //}

                                ////38 2% Fondo retiro SAR (8)
                                //cTP.percepcion = "2% Fondo retiro SAR (8)";
                                //if (cTP.existeP())
                                //{
                                //    cEP.idPercepcion = cTP.id;
                                //}
                                //else
                                //{
                                //    cTP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //    cTP.existeP();
                                //    cEP.idPercepcion = cTP.id;
                                //}

                                ////insertamos percepcion sueldo 
                                //if (dt.Rows[i][38].ToString().Trim() != "")
                                //{
                                //    cEP.monto = dt.Rows[i][38].ToString().Trim().Replace("$", "").Replace(",", "");
                                //    cEP.fecha = DateTime.Now;
                                //    cEP.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                                //    cEP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //}

                                ////39 2% Impuesto estatal
                                //cTP.percepcion = "2% Impuesto estatal";
                                //if (cTP.existeP())
                                //{
                                //    cEP.idPercepcion = cTP.id;
                                //}
                                //else
                                //{
                                //    cTP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //    cTP.existeP();
                                //    cEP.idPercepcion = cTP.id;
                                //}

                                ////insertamos percepcion sueldo 
                                //if (dt.Rows[i][39].ToString().Trim() != "")
                                //{
                                //    cEP.monto = dt.Rows[i][39].ToString().Trim().Replace("$", "").Replace(",", "");
                                //    cEP.fecha = DateTime.Now;
                                //    cEP.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                                //    cEP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //}

                                ////40 Riesgo de trabajo (9)
                                //cTP.percepcion = "Riesgo de trabajo (9)";
                                //if (cTP.existeP())
                                //{
                                //    cEP.idPercepcion = cTP.id;
                                //}
                                //else
                                //{
                                //    cTP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //    cTP.existeP();
                                //    cEP.idPercepcion = cTP.id;
                                //}

                                ////insertamos percepcion sueldo 
                                //if (dt.Rows[i][40].ToString().Trim() != "")
                                //{
                                //    cEP.monto = dt.Rows[i][40].ToString().Trim().Replace("$", "").Replace(",", "");
                                //    cEP.fecha = DateTime.Now;
                                //    cEP.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                                //    cEP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //}

                                ////41 I.M.S.S empresa
                                //cTP.percepcion = "I.M.S.S. empresa";
                                //if (cTP.existeP())
                                //{
                                //    cEP.idPercepcion = cTP.id;
                                //}
                                //else
                                //{
                                //    cTP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //    cTP.existeP();
                                //    cEP.idPercepcion = cTP.id;
                                //}

                                ////insertamos percepcion sueldo 
                                //if (dt.Rows[i][41].ToString().Trim() != "")
                                //{
                                //    cEP.monto = dt.Rows[i][41].ToString().Trim().Replace("$", "").Replace(",", "");
                                //    cEP.fecha = DateTime.Now;
                                //    cEP.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                                //    cEP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //}

                                ////42 Infonavit empresa
                                //cTP.percepcion = "Infonavit empresa";
                                //if (cTP.existeP())
                                //{
                                //    cEP.idPercepcion = cTP.id;
                                //}
                                //else
                                //{
                                //    cTP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //    cTP.existeP();
                                //    cEP.idPercepcion = cTP.id;
                                //}

                                ////insertamos percepcion sueldo 
                                //if (dt.Rows[i][42].ToString().Trim() != "")
                                //{
                                //    cEP.monto = dt.Rows[i][42].ToString().Trim().Replace("$", "").Replace(",", "");
                                //    cEP.fecha = DateTime.Now;
                                //    cEP.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                                //    cEP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //}

                                ////43 Guarderia I.M.S.S (7)
                                //cTP.percepcion = "Guarderia I.M.S.S. (7)";
                                //if (cTP.existeP())
                                //{
                                //    cEP.idPercepcion = cTP.id;
                                //}
                                //else
                                //{
                                //    cTP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //    cTP.existeP();
                                //    cEP.idPercepcion = cTP.id;
                                //}

                                ////insertamos percepcion sueldo 
                                //if (dt.Rows[i][43].ToString().Trim() != "")
                                //{
                                //    cEP.monto = dt.Rows[i][43].ToString().Trim().Replace("$", "").Replace(",", "");
                                //    cEP.fecha = DateTime.Now;
                                //    cEP.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                                //    cEP.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                                //}
                            }

                        }

                        mostrarDatos();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "$('#myModalCorrecto').modal('show');", true);

                    }
                    catch (Exception ex)
                    {
                        lblMensaje.Text = "Error: " + ex.Message.ToString();
                        ScriptManager.RegisterStartupScript(Page, GetType(), "paquete", "$('#myModalCMasiva').modal('show');", true);
                    }
                }
                else
                {

                    //mostramos mensaje
                    lblMensaje.Text = "Solo se permiten archivos con extencion .xls y .xlsx";
                    ScriptManager.RegisterStartupScript(Page, GetType(), "paquete", "$('#myModalCMasiva').modal('show');", true);
                }
            }
            else
            {
                lblMensaje.Text = "Favor de Seleccionar un archivo";
                ScriptManager.RegisterStartupScript(Page, GetType(), "paquete", "$('#myModalCMasiva').modal('show');", true);
            }
        }

        public string obtenerMes(string mes)
        {
            string param = "";
            if (mes != "")
            {
                int num = int.Parse(mes);
                switch (num)
                {
                    case 1:
                        param = "Enero";
                        break;
                    case 2:
                        param = "Febrero";
                        break;
                    case 3:
                        param = "Marzo";
                        break;
                    case 4:
                        param = "Abril";
                        break;
                    case 5:
                        param = "Mayo";
                        break;
                    case 6:
                        param = "Junio";
                        break;
                    case 7:
                        param = "Julio";
                        break;
                    case 8:
                        param = "Agosto";
                        break;
                    case 9:
                        param = "Septiembre";
                        break;
                    case 10:
                        param = "Octubre";
                        break;
                    case 11:
                        param = "Noviembre";
                        break;
                    case 12:
                        param = "Diciembre";
                        break;
                }
            }
            return param;
        }

        cEmpleadosPercepciones per = new cEmpleadosPercepciones();

        protected void btnImportaDatosCompaq_Click(object sender, EventArgs e)
        {

            //DataTable empleados = per.ObtenerDatosPEPI("select idsucursal,idusuario,codigo from empleados");
            //per.ObtenerDatosPEPI("delete empleadospercepciones where mes='" + DateTime.Now.Month + "'");
            //DataTable compaq = per.ObtenerDatosCOMNomina("select GuidDocument,NumEmpleadoRec, fechapago from nomina where FechaPagoAnio=" + DateTime.Now.Year + " and FechaPagoMes='" + DateTime.Now.Month + "'");
            //foreach (DataRow Empleados in empleados.Rows)
            //{
            //    foreach (DataRow Nomina in compaq.Rows)
            //    {
            //        if (Empleados["codigo"].ToString() == Nomina["NumEmpleadoRec"].ToString())
            //        {
            //            DataTable NominaDetalle = per.ObtenerDatosCOMNomina("select clave, ImporteGravado,ImporteExento,Importe from nomina_detalle where GuidDocument='" + Nomina["GuidDocument"].ToString() + "' and tiponominadetalle='PER'");
            //            foreach (DataRow Detalle in NominaDetalle.Rows)
            //            {
            //                decimal ImporteGravado = decimal.Parse(Detalle["ImporteGravado"].ToString());
            //                decimal ImporteExcento = decimal.Parse(Detalle["ImporteExento"].ToString());
            //                decimal Importe = decimal.Parse(Detalle["Importe"].ToString());
            //                decimal Total = ImporteGravado + ImporteExcento + Importe;
            //                per.ObtenerDatosPEPI("insert into empleadosPercepciones (idEmpleado,idPercepcion,monto,fecha,idSucursal,idUsuario,fechaAlta,mes) values ('" + Empleados["codigo"].ToString() + "','" + Detalle["clave"].ToString() + "','" + Total + "','" + DateTime.Now.Date.ToString("yyyy/MM/dd") + "','" + Empleados["idsucursal"].ToString() + "','" + Empleados["idusuario"].ToString() + "','" + DateTime.Parse(Nomina["fechapago"].ToString()).ToString("yyyy/MM/dd HH:mmm:ss") + "','" + DateTime.Now.Month + "')");
            //            }
            //        }
            //    }
            //}
            //lblMensaje.Text = "Importacion Exitosa.";

        }

        protected void btnImportaDatosCompaq_Click1(object sender, EventArgs e)
        {
            //DataTable empleados = per.ObtenerDatosPEPI("select idsucursal,idusuario,codigo from empleados");
            //per.ObtenerDatosPEPI("delete empleadospercepciones where mes='" + DateTime.Now.Month + "'");
            //DataTable compaq = per.ObtenerDatosCOMNomina("select GuidDocument,NumEmpleadoRec, fechapago from nomina where FechaPagoAnio=" + DateTime.Now.Year + " and FechaPagoMes='" + DateTime.Now.Month + "'");
            //foreach (DataRow Empleados in empleados.Rows)
            //{
            //    foreach (DataRow Nomina in compaq.Rows)
            //    {
            //        if (Empleados["codigo"].ToString() == Nomina["NumEmpleadoRec"].ToString())
            //        {
            //            DataTable NominaDetalle = per.ObtenerDatosCOMNomina("select clave, ImporteGravado,ImporteExento,Importe from nomina_detalle where GuidDocument='" + Nomina["GuidDocument"].ToString() + "' and tiponominadetalle='PER'");
            //            foreach (DataRow Detalle in NominaDetalle.Rows)
            //            {
            //                decimal ImporteGravado = decimal.Parse(Detalle["ImporteGravado"].ToString());
            //                decimal ImporteExcento = decimal.Parse(Detalle["ImporteExento"].ToString());
            //                decimal Importe = decimal.Parse(Detalle["Importe"].ToString());
            //                decimal Total = ImporteGravado + ImporteExcento + Importe;
            //                per.ObtenerDatosPEPI("insert into empleadosPercepciones (idEmpleado,idPercepcion,monto,fecha,idSucursal,idUsuario,fechaAlta,mes) values ('" + Empleados["codigo"].ToString() + "','" + Detalle["clave"].ToString() + "','" + Total + "','" + DateTime.Now.Date.ToString("yyyy/MM/dd") + "','" + Empleados["idsucursal"].ToString() + "','" + Empleados["idusuario"].ToString() + "','" + DateTime.Parse(Nomina["fechapago"].ToString()).ToString("yyyy/MM/dd HH:mmm:ss") + "','" + DateTime.Now.Month + "')");
            //            }
            //        }
            //    }
            //}
            //lblMensaje.Text = "Importacion Exitosa.";
        }

        protected void btnImportarCompaqi_Click(object sender, EventArgs e)
        {
            //DataTable empleados = per.ObtenerDatosPEPI("select idsucursal,idusuario,codigo from empleados");
            //per.ObtenerDatosPEPI("delete empleadospercepciones where mes='" + DateTime.Now.Month + "'");
            //DataTable compaq = per.ObtenerDatosCOMNomina("select GuidDocument,NumEmpleadoRec, fechapago from nomina where FechaPagoAnio=" + DateTime.Now.Year + " and FechaPagoMes='" + 5 + "'");

            ////DataTable compaq = per.ObtenerDatosCOM("select GuidDocument,NumEmpleadoRec, fechapago from nomina where FechaPagoAnio=" + DateTime.Now.Year + " and FechaPagoMes='" + DateTime.Now.Month + "'");
            //foreach (DataRow Empleados in empleados.Rows)
            //{
            //    foreach (DataRow Nomina in compaq.Rows)
            //    {
            //        if (Empleados["codigo"].ToString() == Nomina["NumEmpleadoRec"].ToString())
            //        {
            //            DataTable NominaDetalle = per.ObtenerDatosCOMNomina("select clave, ImporteGravado,ImporteExento,Importe from nomina_detalle where GuidDocument='" + Nomina["GuidDocument"].ToString() + "' and tiponominadetalle='PER'");
            //            foreach (DataRow Detalle in NominaDetalle.Rows)
            //            {
            //                decimal ImporteGravado = decimal.Parse(Detalle["ImporteGravado"].ToString());
            //                decimal ImporteExcento = decimal.Parse(Detalle["ImporteExento"].ToString());
            //                decimal Importe = decimal.Parse(Detalle["Importe"].ToString());
            //                decimal Total = ImporteGravado + ImporteExcento + Importe;
            //                per.ObtenerDatosPEPI("insert into empleadosPercepciones (idEmpleado,idPercepcion,monto,fecha,idSucursal,idUsuario,fechaAlta,mes) values ('" + Empleados["codigo"].ToString() + "','" + Detalle["clave"].ToString() + "','" + Total + "','" + DateTime.Now.Date.ToString("yyyy/MM/dd") + "','" + Empleados["idsucursal"].ToString() + "','" + Empleados["idusuario"].ToString() + "','" + DateTime.Parse(Nomina["fechapago"].ToString()).ToString("yyyy/MM/dd HH:mmm:ss") + "','" + DateTime.Now.Month + "')");
            //            }
            //        }
            //    }
            //}
            //lblMensaje.Text = "Importacion Exitosa.";

        }

        protected void btnImporta_Click(object sender, EventArgs e)
        {
            //DataTable empleados = per.ObtenerDatosPEPI("select idsucursal,idusuario,codigo from empleados");
            //per.ObtenerDatosPEPI("delete empleadospercepciones where mes='" + DateTime.Now.Month + "'");
            //DataTable compaq = per.ObtenerDatosCOMNomina("select GuidDocument,NumEmpleadoRec, fechapago from nomina where FechaPagoAnio=" + DateTime.Now.Year + " and FechaPagoMes='" + DateTime.Now.Month + "'");
            //foreach (DataRow Empleados in empleados.Rows)
            //{
            //    foreach (DataRow Nomina in compaq.Rows)
            //    {
            //        if (Empleados["codigo"].ToString() == Nomina["NumEmpleadoRec"].ToString())
            //        {
            //            DataTable NominaDetalle = per.ObtenerDatosCOMNomina("select clave, ImporteGravado,ImporteExento,Importe from nomina_detalle where GuidDocument='" + Nomina["GuidDocument"].ToString() + "' and tiponominadetalle='PER'");
            //            foreach (DataRow Detalle in NominaDetalle.Rows)
            //            {
            //                decimal ImporteGravado = decimal.Parse(Detalle["ImporteGravado"].ToString());
            //                decimal ImporteExcento = decimal.Parse(Detalle["ImporteExento"].ToString());
            //                decimal Importe = decimal.Parse(Detalle["Importe"].ToString());
            //                decimal Total = ImporteGravado + ImporteExcento + Importe;
            //                per.ObtenerDatosPEPI("insert into empleadosPercepciones (idEmpleado,idPercepcion,monto,fecha,idSucursal,idUsuario,fechaAlta,mes) values ('" + Empleados["codigo"].ToString() + "','" + Detalle["clave"].ToString() + "','" + Total + "','" + DateTime.Now.Date.ToString("yyyy/MM/dd") + "','" + Empleados["idsucursal"].ToString() + "','" + Empleados["idusuario"].ToString() + "','" + DateTime.Parse(Nomina["fechapago"].ToString()).ToString("yyyy/MM/dd HH:mmm:ss") + "','" + DateTime.Now.Month + "')");
            //            }
            //        }
            //    }
            //}
            //lblMensaje.Text = "Importacion Exitosa.";
        }

        protected void btnPrueba_Click(object sender, EventArgs e)
        {
            //if (drop.SelectedIndex != 0)
            //{
            //    DataTable empleados = per.ObtenerDatosPEPI("select idsucursal,idusuario,codigo from empleados");
            //    per.ObtenerDatosPEPI("delete empleadospercepciones where  (DATEPART(yy, fechaalta) = " + DateTime.Now.Year + " AND DATEPART(mm, fechaalta) = " + drop.SelectedValue.ToString() + ")");
            //    //DataTable compaq = per.ObtenerDatosCOM("select GuidDocument,NumEmpleadoRec, fechapago from nomina where FechaPagoAnio=" + DateTime.Now.Year + " and FechaPagoMes='" + DateTime.Now.Month + "'");
            //    DataTable compaq = per.ObtenerDatosCOMNomina("select GuidDocument,NumEmpleadoRec, fechapago from nomina where FechaPagoAnio=" + DateTime.Now.Year + " and FechaPagoMes='" + drop.SelectedValue.ToString() + "'");
            //    foreach (DataRow Empleados in empleados.Rows)
            //    {
            //        foreach (DataRow Nomina in compaq.Rows)
            //        {
            //            if (Empleados["codigo"].ToString() == Nomina["NumEmpleadoRec"].ToString())
            //            {
            //                DataTable NominaDetalle = per.ObtenerDatosCOMNomina("select clave, ImporteGravado,ImporteExento,Importe from nomina_detalle where GuidDocument='" + Nomina["GuidDocument"].ToString() + "' and tiponominadetalle='PER'");
            //                foreach (DataRow Detalle in NominaDetalle.Rows)
            //                {
            //                    decimal ImporteGravado = decimal.Parse(Detalle["ImporteGravado"].ToString());
            //                    decimal ImporteExcento = decimal.Parse(Detalle["ImporteExento"].ToString());
            //                    decimal Importe = decimal.Parse(Detalle["Importe"].ToString());
            //                    decimal Total = ImporteGravado + ImporteExcento + Importe;
            //                    per.ObtenerDatosPEPI("insert into empleadosPercepciones (idEmpleado,idPercepcion,monto,fecha,idSucursal,idUsuario,fechaAlta,mes) values ('" + Empleados["codigo"].ToString() + "','" + Detalle["clave"].ToString() + "','" + Total + "','" + DateTime.Now.Date.ToString("yyyy/MM/dd") + "','" + Empleados["idsucursal"].ToString() + "','" + Empleados["idusuario"].ToString() + "','" + DateTime.Parse(Nomina["fechapago"].ToString()).ToString("yyyy/MM/dd HH:mmm:ss") + "','" + drop.SelectedValue.ToString() + "')");
            //                }
            //            }
            //        }
            //    }
            //    mostrarDatos();
            //    drop.SelectedIndex = 0;
            //    lblError.Text = "Importacion Existosa";
            //    lblError.BackColor = System.Drawing.Color.Red;
            //    lblError.ForeColor = System.Drawing.Color.Yellow;
            //}
            //else
            //{
            //    drop.SelectedIndex = 0;

            //    lblError.Text = "Favor de Seleccionar un Mes.";
            //    lblError.BackColor = System.Drawing.Color.Red;
            //    lblError.ForeColor = System.Drawing.Color.Yellow;
            //}



        }

        protected void cmbMes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnExportarDatos_Click(object sender, EventArgs e)
        {
            if (ddlTipoNomina.SelectedIndex == 0)
            {
                lblError.Text = "Selecionar por favor un Tipo de nomina";
                lblError.BackColor = System.Drawing.Color.Red;
                lblError.ForeColor = System.Drawing.Color.Yellow;
                return;
            }
            if (ddlPeriodo.SelectedIndex == 0)
            {
                lblError.Text = "Selecionar por favor un Periodo";
                lblError.BackColor = System.Drawing.Color.Red;
                lblError.ForeColor = System.Drawing.Color.Yellow;
                return;

            }
            if (ddlEjercicio.SelectedIndex == 0)
            {
                lblError.Text = "Selecionar por favor un Ejercicio";
                lblError.BackColor = System.Drawing.Color.Red;
                lblError.ForeColor = System.Drawing.Color.Yellow;
                return;
            }

            string sucursal = Request.Cookies["ksroc"]["idSucursal"].ToString();
            if (sucursal == "1")
            {
                per.ObtenerDatosPEPI("delete empleadospercepciones where idsucursal=1 and periodo='" + ddlPeriodo.Text + "' and ejercicio='" + ddlEjercicio.Text + "'");
                DataTable dt = per.ObtenerDatosCOMESaltilloConcretos("select nom10007.idperiodo,nom10002.numeroperiodo,nom10002.ejercicio,nom10002.mes,nom10002.idtipoperiodo,nom10002.diasdepago,nom10002.fechainicio,nom10002.fechafin,nom10002.fechaPago,nom10007.idempleado,nom10001.codigoempleado,nom10001.nombrelargo,nom10001.sueldodiario,nom10003.iddepartamento, nom10003.descripcion,nom10004.idconcepto,nom10004.descripcion,nom10004.tipoconcepto,nom10004.descripcion,importetotal,importe1,importe2,importe3,importe4 from nom10007 inner join nom10002 on nom10007.idperiodo = nom10002.idperiodo inner join nom10001 on nom10007.idempleado = nom10001.idempleado inner join nom10003 on nom10001.iddepartamento = nom10003.iddepartamento inner join nom10004 on nom10007.idconcepto = nom10004.idconcepto where nom10004.tipoconcepto = 'P' and nom10002.idtipoperiodo = " + ddlTipoNomina.SelectedValue.ToString() + " and ejercicio = '" + ddlEjercicio.Text + "' and numeroperiodo = '" + ddlPeriodo.Text + "'");
                foreach (DataRow empleados in dt.Rows)
                {
                    per.ObtenerDatosPEPI("insert into empleadospercepciones (idempleado,idpercepcion,monto,fecha,idsucursal,idusuario,fechaalta,mes,periodo,ejercicio) values ('" + empleados["codigoempleado"].ToString() + "','" + empleados["idconcepto"].ToString() + "','" + empleados["importetotal"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + sucursal + "','4045','" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + empleados["mes"].ToString() + "','" + ddlPeriodo.Text + "','" + ddlEjercicio.Text + "')");
                }
                cargarControles();
                cargarDatos();

                ddlEjercicio.SelectedIndex = 0;
                ddlTipoNomina.SelectedIndex = 0;
                ddlPeriodo.SelectedIndex = 0;
                lblError.Text = "Importacion Existosa";
                lblError.BackColor = System.Drawing.Color.Red;
                lblError.ForeColor = System.Drawing.Color.Yellow;
            }
            else if (sucursal == "3")
            {
                per.ObtenerDatosPEPI("delete empleadospercepciones where idsucursal=3 and periodo='" + ddlPeriodo.Text + "' and ejercicio='" + ddlEjercicio.Text + "'");
                DataTable dt = per.ObtenerDatosCOMSaltilloBlock("select nom10007.idperiodo,nom10002.numeroperiodo,nom10002.ejercicio,nom10002.mes,nom10002.idtipoperiodo,nom10002.diasdepago,nom10002.fechainicio,nom10002.fechafin,nom10002.fechaPago,nom10007.idempleado,nom10001.codigoempleado,nom10001.nombrelargo,nom10001.sueldodiario,nom10003.iddepartamento, nom10003.descripcion,nom10004.idconcepto,nom10004.descripcion,nom10004.tipoconcepto,nom10004.descripcion,importetotal,importe1,importe2,importe3,importe4 from nom10007 inner join nom10002 on nom10007.idperiodo = nom10002.idperiodo inner join nom10001 on nom10007.idempleado = nom10001.idempleado inner join nom10003 on nom10001.iddepartamento = nom10003.iddepartamento inner join nom10004 on nom10007.idconcepto = nom10004.idconcepto where nom10004.tipoconcepto = 'P' and nom10002.idtipoperiodo = " + ddlTipoNomina.SelectedValue.ToString() + " and ejercicio = '" + ddlEjercicio.Text + "' and numeroperiodo = '" + ddlPeriodo.Text + "'");
                foreach (DataRow empleados in dt.Rows)
                {
                    per.ObtenerDatosPEPI("insert into empleadospercepciones (idempleado,idpercepcion,monto,fecha,idsucursal,idusuario,fechaalta,mes,periodo,ejercicio) values ('" + empleados["codigoempleado"].ToString() + "','" + empleados["idconcepto"].ToString() + "','" + empleados["importetotal"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + sucursal + "','4045','" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + empleados["mes"].ToString() + "','" + ddlPeriodo.Text + "','" + ddlEjercicio.Text + "')");
                }
                cargarControles();
                cargarDatos();

                ddlEjercicio.SelectedIndex = 0;
                ddlTipoNomina.SelectedIndex = 0;
                ddlPeriodo.SelectedIndex = 0;
                lblError.Text = "Importacion Existosa";
                lblError.BackColor = System.Drawing.Color.Red;
                lblError.ForeColor = System.Drawing.Color.Yellow;

            }
            else if (sucursal == "2")
            {
                per.ObtenerDatosPEPI("delete empleadospercepciones where idsucursal=2 and periodo='" + ddlPeriodo.Text + "' and ejercicio='" + ddlEjercicio.Text + "'");
                DataTable dt = per.ObtenerDatosCOMIrapuatoConcretos("select nom10007.idperiodo,nom10002.numeroperiodo,nom10002.ejercicio,nom10002.mes,nom10002.idtipoperiodo,nom10002.diasdepago,nom10002.fechainicio,nom10002.fechafin,nom10002.fechaPago,nom10007.idempleado,nom10001.codigoempleado,nom10001.nombrelargo,nom10001.sueldodiario,nom10003.iddepartamento, nom10003.descripcion,nom10004.idconcepto,nom10004.descripcion,nom10004.tipoconcepto,nom10004.descripcion,importetotal,importe1,importe2,importe3,importe4 from nom10007 inner join nom10002 on nom10007.idperiodo = nom10002.idperiodo inner join nom10001 on nom10007.idempleado = nom10001.idempleado inner join nom10003 on nom10001.iddepartamento = nom10003.iddepartamento inner join nom10004 on nom10007.idconcepto = nom10004.idconcepto where nom10004.tipoconcepto = 'P' and nom10002.idtipoperiodo = " + ddlTipoNomina.SelectedValue.ToString() + " and ejercicio = '" + ddlEjercicio.Text + "' and numeroperiodo = '" + ddlPeriodo.Text + "'");
                foreach (DataRow empleados in dt.Rows)
                {
                    per.ObtenerDatosPEPI("insert into empleadospercepciones (idempleado,idpercepcion,monto,fecha,idsucursal,idusuario,fechaalta,mes,periodo,ejercicio) values ('" + empleados["codigoempleado"].ToString() + "','" + empleados["idconcepto"].ToString() + "','" + empleados["importetotal"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + sucursal + "','4045','" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + empleados["mes"].ToString() + "','" + ddlPeriodo.Text + "','" + ddlEjercicio.Text + "')");
                }
                cargarControles();
                cargarDatos();

                ddlEjercicio.SelectedIndex = 0;
                ddlTipoNomina.SelectedIndex = 0;
                ddlPeriodo.SelectedIndex = 0;
                lblError.Text = "Importacion Existosa";
                lblError.BackColor = System.Drawing.Color.Red;
                lblError.ForeColor = System.Drawing.Color.Yellow;
            }
            else if (sucursal == "1006")
            {
                per.ObtenerDatosPEPI("delete empleadospercepciones where idsucursal=1006 and periodo='" + ddlPeriodo.Text + "' and ejercicio='" + ddlEjercicio.Text + "'");
                DataTable dt = per.ObtenerDatosCOMIrapuatoBlock("select nom10007.idperiodo,nom10002.numeroperiodo,nom10002.ejercicio,nom10002.mes,nom10002.idtipoperiodo,nom10002.diasdepago,nom10002.fechainicio,nom10002.fechafin,nom10002.fechaPago,nom10007.idempleado,nom10001.codigoempleado,nom10001.nombrelargo,nom10001.sueldodiario,nom10003.iddepartamento, nom10003.descripcion,nom10004.idconcepto,nom10004.descripcion,nom10004.tipoconcepto,nom10004.descripcion,importetotal,importe1,importe2,importe3,importe4 from nom10007 inner join nom10002 on nom10007.idperiodo = nom10002.idperiodo inner join nom10001 on nom10007.idempleado = nom10001.idempleado inner join nom10003 on nom10001.iddepartamento = nom10003.iddepartamento inner join nom10004 on nom10007.idconcepto = nom10004.idconcepto where nom10004.tipoconcepto = 'P' and nom10002.idtipoperiodo = " + ddlTipoNomina.SelectedValue.ToString() + " and ejercicio = '" + ddlEjercicio.Text + "' and numeroperiodo = '" + ddlPeriodo.Text + "'");
                foreach (DataRow empleados in dt.Rows)
                {
                    per.ObtenerDatosPEPI("insert into empleadospercepciones (idempleado,idpercepcion,monto,fecha,idsucursal,idusuario,fechaalta,mes,periodo,ejercicio) values ('" + empleados["codigoempleado"].ToString() + "','" + empleados["idconcepto"].ToString() + "','" + empleados["importetotal"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + sucursal + "','4045','" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + empleados["mes"].ToString() + "','" + ddlPeriodo.Text + "','" + ddlEjercicio.Text + "')");
                }
                cargarControles();
                cargarDatos();

                ddlEjercicio.SelectedIndex = 0;
                ddlTipoNomina.SelectedIndex = 0;
                ddlPeriodo.SelectedIndex = 0;
                lblError.Text = "Importacion Existosa";
                lblError.BackColor = System.Drawing.Color.Red;
                lblError.ForeColor = System.Drawing.Color.Yellow;
            }
            else if (sucursal == "1010")
            {
                per.ObtenerDatosPEPI("delete empleadospercepciones where idsucursal=1010 and periodo='" + ddlPeriodo.Text + "' and ejercicio='" + ddlEjercicio.Text + "'");
                DataTable dt = per.ObtenerDatosCOMExternos("select nom10007.idperiodo,nom10002.numeroperiodo,nom10002.ejercicio,nom10002.mes,nom10002.idtipoperiodo,nom10002.diasdepago,nom10002.fechainicio,nom10002.fechafin,nom10002.fechaPago,nom10007.idempleado,nom10001.codigoempleado,nom10001.nombrelargo,nom10001.sueldodiario,nom10003.iddepartamento, nom10003.descripcion,nom10004.idconcepto,nom10004.descripcion,nom10004.tipoconcepto,nom10004.descripcion,importetotal,importe1,importe2,importe3,importe4 from nom10007 inner join nom10002 on nom10007.idperiodo = nom10002.idperiodo inner join nom10001 on nom10007.idempleado = nom10001.idempleado inner join nom10003 on nom10001.iddepartamento = nom10003.iddepartamento inner join nom10004 on nom10007.idconcepto = nom10004.idconcepto where nom10004.tipoconcepto = 'P' and nom10002.idtipoperiodo = " + ddlTipoNomina.SelectedValue.ToString() + " and ejercicio = '" + ddlEjercicio.Text + "' and numeroperiodo = '" + ddlPeriodo.Text + "'");
                foreach (DataRow empleados in dt.Rows)
                {
                    per.ObtenerDatosPEPI("insert into empleadospercepciones (idempleado,idpercepcion,monto,fecha,idsucursal,idusuario,fechaalta,mes,periodo,ejercicio) values ('" + empleados["codigoempleado"].ToString() + "','" + empleados["idconcepto"].ToString() + "','" + empleados["importetotal"].ToString() + "','" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + sucursal + "','4045','" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + empleados["mes"].ToString() + "','" + ddlPeriodo.Text + "','" + ddlEjercicio.Text + "')");
                }
                cargarControles();
                cargarDatos();

                ddlEjercicio.SelectedIndex = 0;
                ddlTipoNomina.SelectedIndex = 0;
                ddlPeriodo.SelectedIndex = 0;
                lblError.Text = "Importacion Existosa";
                lblError.BackColor = System.Drawing.Color.Red;
                lblError.ForeColor = System.Drawing.Color.Yellow;
            }
        }
    }
}
