using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class reporteAntiguedadSaldos2 : System.Web.UI.Page
    {
        int idSucursal = 0;
        DateTime fechaInicio = DateTime.Now;
        DateTime fechaFin = DateTime.Now;
        cContpaq cContpaq = new cContpaq();
        cSucursales cSuc = new cSucursales();

        protected void Page_Load(object sender, EventArgs e)
        {


            //DataTable dtDatosCliente = cContpaq.obtenerDatosCliente(int.Parse(Request.QueryString["CIDCLIENTEPROVEEDOR"]));
            if (!IsPostBack)
            {
                //Encabezado del reporte
                lblFechaFin.Text = Request.QueryString["FechaFin"].ToString();



                if (Request.Cookies["ksroc"]["idSucursal"] != "" && Request.Cookies["ksroc"]["idSucursal"] != null)
                {
                    idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                }
                else
                {
                    idSucursal = int.Parse(Request.QueryString["idSucursal"]);
                }

                cSuc.id = idSucursal;
                cSuc.nombre = cSuc.obtenerNombreSucursalByID(idSucursal);

                imagen.InnerHtml = "<img src='img/pepi_logo.png' width='100' height='78'/>&nbsp;&nbsp;" + cSuc.nombre;
                llenarLV();
            }
        }

        protected void llenarLV()
        {
            //Establecemos la tabla del reporte y sus columnas
            DataTable dtcuenta = new DataTable();

            dtcuenta.Columns.Add("Codigo");
            dtcuenta.Columns.Add("Nombre");
            dtcuenta.Columns.Add("TotalVencido");
            dtcuenta.Columns.Add("Vendedor");
            dtcuenta.Columns.Add("Vencimiento");


            //declaramos las fechas del reporte en base al filtro de la pag. anterior
            fechaInicio = DateTime.Parse("2010/01/01");
            fechaFin = DateTime.Parse(Request.QueryString["FechaFin"]);
            ////Obtenemos los cargos del cliente seleccionado
            //DataTable dtCTA = cContpaq.obtenerAntiguedadSaldos(Request.QueryString["FechaInicio"], Request.QueryString["FechaFin"]);
            DataTable dtClientes = cContpaq.obtenerClientesContpaq();

            float saldoVencido = 0;
            for (int i = 0; i < dtClientes.Rows.Count; i++)
            {
                //string pivote = dtCTA.Rows[0]["CCODIGOCLIENTE"].ToString();
                saldoVencido = 0;

                DataTable dtCargosCliente = cContpaq.obtenerCargosVencidosCliente(int.Parse(dtClientes.Rows[i]["CIDCLIENTEPROVEEDOR"].ToString()), Request.QueryString["FechaInicio"], Request.QueryString["FechaFin"]);

                if (dtCargosCliente.Rows.Count > 0)
                {
                    for (int j = 0; j < dtCargosCliente.Rows.Count; j++)
                    {
                        DateTime fechaVencida = DateTime.Now;
                        //if (float.Parse(dtCargosCliente.Rows[j]["CPENDIENTE"].ToString()) > 0 && DateTime.Parse(dtCargosCliente.Rows[j]["CFECHAVENCIMIENTO"].ToString()) < fechaFin)
                        //{
                        //string pivote = dtClientes.Rows[0]["CCODIGOCLIENTE"].ToString();
                        saldoVencido = saldoVencido + float.Parse(dtCargosCliente.Rows[j]["CPENDIENTE"].ToString());
                        fechaVencida = DateTime.Parse(dtCargosCliente.Rows[0]["CFECHAVENCIMIENTO"].ToString());

                        //}
                        if (j == (dtCargosCliente.Rows.Count - 1))
                        {
                            //Definimos una fila para la tabla del reporte y llenamos con informacion de nuestra consulta 
                            DataRow rw = dtcuenta.NewRow();
                            rw["Codigo"] = dtClientes.Rows[i]["CCODIGOCLIENTE"].ToString();
                            rw["Nombre"] = dtClientes.Rows[i]["CRAZONSOCIAL"].ToString();
                            rw["TotalVencido"] = "$" + saldoVencido;
                            //DataTable dtVendedor = cContpaq.obtenerAgentePorId(int.Parse(dtClientes.Rows[i]["CIDAGENTEVENTA"].ToString()));

                            rw["Vendedor"] = cContpaq.obtenerVendedor(int.Parse(dtClientes.Rows[i]["CIDAGENTEVENTA"].ToString()));

                            rw["Vencimiento"] = fechaVencida.ToString("dd-MMM-yyyy");
                            if (saldoVencido > 1)
                            {
                                //Agregamos la fila a la tabla del reporte
                                dtcuenta.Rows.Add(rw);
                            }

                        }
                    }
                }
            }

            lvCliente.DataSource = dtcuenta;
            lvCliente.DataBind();
        }


    }
}