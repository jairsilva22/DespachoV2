using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class ReporteCancelados : System.Web.UI.Page
    {
        cUtilidades cUtl = new cUtilidades();
        cSucursales suc;
        int idSucursal = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            //variables
            suc = new cSucursales();
            try
            {
                string parametros = string.Empty;
                cFacturaCancelada fcan = new cFacturaCancelada();

                if (Request.QueryString["folio"] != "" && Request.QueryString["folio"] != null)
                {
                    parametros += " AND A.folio = " + Request.QueryString["folio"];
                }
                if (Request.QueryString["rfc"] != "" && Request.QueryString["rfc"] != null)
                {
                    parametros += " AND C.rfcCliente = '" + Request.QueryString["rfc"] + "'";
                }
                if (Request.QueryString["mes"] != "" && Request.QueryString["mes"] != null)
                {
                    parametros += " AND Month(A.fechaSolicitud) = " + Request.QueryString["mes"];
                }
                if (Request.QueryString["anio"] != "" && Request.QueryString["anio"] != null)
                {
                    parametros += " AND YEAR(A.fechaSolicitud) = " + Request.QueryString["anio"];
                }
                if (Request.QueryString["cliente"] != "" && Request.QueryString["cliente"] != null)
                {
                    parametros += " AND B.idCliente = " + Request.QueryString["cliente"];
                }
                if (Request.QueryString["estatus"] != "" && Request.QueryString["estatus"] != null)
                {
                    //validamos el codigo de la cancelacion
                    switch (Request.QueryString["estatus"])
                    {
                        case "Pendiente":
                            parametros += " AND (A.CodigoCan = '101' OR A.CodigoCan = '205')";
                            break;
                        case "Cancelado":
                            parametros += " AND A.CodigoCan = '201' OR A.codigoCan = '202' OR A.codigoCan = 'S - Comprobante obtenido satisfactoriamente.'";
                            break;
                        case "Error":
                            parametros += " AND (A.CodigoCan = '102' OR A.CodigoCan = '103' OR A.CodigoCan = '301' OR A.CodigoCan = 'N - 602: Comprobante no encontrado.')";
                            break;
                        default:
                            break;
                    }
                }
                if (Request.QueryString["doc"] != "" && Request.QueryString["doc"] != null)
                {
                    parametros += " AND B.tipo_comprobante = " + Request.QueryString["doc"];
                }
                if (Request.QueryString["user"] != "" && Request.QueryString["user"] != null)
                {
                    parametros += " AND B.idusuario = " + Request.QueryString["user"];
                }

                if (Request.QueryString["idSucursal"] != "" && Request.QueryString["idSucursal"] != null)
                {
                    parametros += " AND B.idempresa = " + Request.QueryString["idSucursal"];
                    idSucursal = int.Parse(Request.QueryString["idSucursal"]);
                }
                else
                {
                    idSucursal = 0;
                }

                suc.id = idSucursal;

                imagen.InnerHtml = "<img src='img/pepi_logo.png' width='100' height='78'/>&nbsp;&nbsp;" + suc.nombre;
                //llenamos el listview
                ListView1.DataSource = fcan.reporteFacturasCanceladas(parametros);
                ListView1.DataBind();
                cUtl.idUsuarioActivo = int.Parse(Request.Cookies["ksroc"]["id"]);
            }
            catch (Exception)
            {
            }

            //Response.Write(parametros);


        }

        //metodo para saber el estatus de la factura
        public string estado(string codigo)
        {
            string dato = string.Empty;

            switch (codigo)
            {
                case "101":
                case "205":
                    dato = "Pendiente";
                    break;
                case "201":
                case "202":
                case "S - Comprobante obtenido satisfactoriamente.":
                    dato = "Cancelado";
                    break;
                case "102":
                case "103":
                case "301":
                case "no_cancelable - El UUID contiene CFDI relacionados":
                case "no_cancelable":
                case "708":
                case "N - 708: No se pudo conectar al SAT.":
                case "N - 602: Comprobante no encontrado.":
                case "602":
                    dato = "Error";
                    break;
                default:
                    break;
            }

            return dato;
        }


    }
}