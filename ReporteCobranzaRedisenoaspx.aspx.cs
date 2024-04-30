using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class ReporteCobranzaRedisenoaspx : System.Web.UI.Page
    {
        int idSucursal = 0;
        cSucursales cSuc = new cSucursales();
        cContpaq cContpaq = new cContpaq();
        protected void Page_Load(object sender, EventArgs e)
        {
            lblFechaInicio.Text += Request.QueryString["FechaInicio"];

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
        public string ToFormat24h(DateTime dt)
        {
            return dt.ToString("yyyy/MM/dd HH:mm:ss");
        }
        protected void llenarLV()
        {
            //Establecemos la tabla del reporte y sus columnas
            DataTable dtcuenta = new DataTable();
            dtcuenta.Columns.Add("Codigo");
            dtcuenta.Columns.Add("Nombre");
            dtcuenta.Columns.Add("TotalVencido");
            //dtcuenta.Columns.Add("TotalPorVencer");
            dtcuenta.Columns.Add("Dia45");
            dtcuenta.Columns.Add("Dia4690");
            dtcuenta.Columns.Add("Dia9135");
            dtcuenta.Columns.Add("Dia136Mas");
            dtcuenta.Columns.Add("xvencer");
            dtcuenta.Columns.Add("Dia45PorVencer");
            dtcuenta.Columns.Add("Total", typeof(decimal));
            decimal TOTALVENCIDO = 0;
            decimal TOTALDIA45 = 0;
            decimal TOTALDIA4690 = 0;
            decimal TOTALDIA9135 = 0;
            decimal TOTALDIA136MAS = 0;
            decimal TOTAL45PORVENCER = 0;
            decimal TOTALTOTAL = 0;
            decimal TotalVencido = 0;
            decimal Dias45 = 0;
            decimal Dias4690 = 0;
            decimal Dias91135 = 0;
            decimal Dias136Mas = 0;
            decimal PorVencer = 0;
            decimal PorVencermenos45 = 0;
            decimal PorVencerTotal = 0;

            //declaramos las fechas del reporte en base al filtro de la pag. anterior
            DateTime FinalFecha = DateTime.Parse(Request.QueryString["FechaInicio"]);
            //fechaFin = DateTime.Parse(Request.QueryString["FechaFin"]);
            //Obtenemos los cargos del cliente seleccionado
            //Request.QueryString["FechaInicio"], Request.QueryString["FechaFin"]
            //  DataTable dtClientes = cContpaq.ObtenerDatosCompaqi("select top (50) CIDCLIENTEPROVEEDOR,CCODIGOCLIENTE,CRAZONSOCIAL,CNOMBREAGENTE from admClientes inner join admAgentes on admClientes.CIDAGENTEVENTA = admAgentes.CIDAGENTE");
            DataTable dtClientes = cContpaq.ObtenerDatosCompaqi("select  CIDCLIENTEPROVEEDOR,CCODIGOCLIENTE,CRAZONSOCIAL from admClientes");
            foreach (DataRow cliente in dtClientes.Rows)
            {
                string CodigoCliente = cliente["CIDCLIENTEPROVEEDOR"].ToString();
                string Codigo = cliente["CCODIGOCLIENTE"].ToString();
                string Nombre = cliente["CRAZONSOCIAL"].ToString();
                if (Codigo == "1030376")
                {
                    int avisa = 0;
                }
                DataTable dtDatos = cContpaq.ObtenerDatosCompaqi("SELECT  cpendiente,CFECHAVENCIMIENTO, DATEDIFF(DAY, CFECHAVENCIMIENTO, '" + ToFormat24h(FinalFecha) + "') as Dias FROM admDocumentos WHERE(CIDCLIENTEPROVEEDOR = '" + CodigoCliente + "') AND(CIDCONCEPTODOCUMENTO = 3008 OR CIDCONCEPTODOCUMENTO = 3014) and ccancelado = 0 and cfecha BETWEEN '2010-03-19' AND '" + ToFormat24h(FinalFecha) + "' ; ");
                DataTable dtDatosSumados = cContpaq.ObtenerDatosCompaqi("SELECT sum(cpendiente) as pendiente FROM admDocumentos WHERE(CIDCLIENTEPROVEEDOR = '" + CodigoCliente + "') AND(CIDCONCEPTODOCUMENTO = 3008 OR CIDCONCEPTODOCUMENTO = 3014) and ccancelado = 0 and cfecha BETWEEN '2010-03-19' AND '" + ToFormat24h(FinalFecha) + "' ; ");
                if (dtDatos.Rows.Count > 0)
                {
                    if (decimal.Parse(dtDatosSumados.Rows[0][0].ToString()) > 0)
                    {
                        foreach (DataRow datos in dtDatos.Rows)
                        {
                            decimal pendiente = decimal.Parse(datos["cpendiente"].ToString());
                            if (pendiente > 0)
                            {
                                if (double.Parse(pendiente.ToString()) >= 0.01 && double.Parse(pendiente.ToString()) <= 0.99)
                                {
                                    continue;
                                }
                                int dias = int.Parse(datos["Dias"].ToString());
                                if (dias >= 0 && dias <= 45)
                                {
                                    Dias45 += pendiente; TotalVencido += pendiente; TOTALVENCIDO += TotalVencido; TOTALDIA45 += Dias45;
                                }
                                else if (dias >= 46 && dias <= 90)
                                {
                                    Dias4690 += pendiente; TotalVencido += pendiente; TOTALVENCIDO += TotalVencido; TOTALDIA4690 += Dias4690;
                                }
                                else if (dias >= 91 && dias <= 135)
                                {
                                    Dias91135 += pendiente; TotalVencido += pendiente; TOTALVENCIDO += TotalVencido; TOTALDIA9135 += Dias91135;
                                }
                                else if (dias >= 136)
                                {
                                    Dias136Mas += pendiente; TotalVencido += pendiente; TOTALVENCIDO += TotalVencido; TOTALDIA136MAS += Dias136Mas;
                                }
                                else if (dias > -45 && dias < 0)
                                {
                                    PorVencermenos45 += pendiente; PorVencerTotal += pendiente; ; TOTAL45PORVENCER += pendiente;
                                }
                                else if (dias < 0)
                                {
                                    PorVencer += pendiente; TotalVencido += pendiente; ;
                                }

                            }
                        }
                        DataRow row = dtcuenta.NewRow();
                        row["Codigo"] = Codigo;
                        row["Nombre"] = Nombre;
                        row["TotalVencido"] = TotalVencido;
                        //row["TotalPorVencer"] = PorVencerTotal;
                        row["Dia45"] = Dias45;
                        row["Dia4690"] = Dias4690;
                        row["Dia9135"] = Dias91135;
                        row["Dia136Mas"] = Dias136Mas;
                        row["xvencer"] = PorVencermenos45;
                        row["Dia45PorVencer"] = PorVencermenos45;
                        row["Total"] = PorVencermenos45 + Dias45 + Dias4690 + Dias91135 + Dias136Mas;
                        decimal totales = PorVencermenos45 + Dias45 + Dias4690 + Dias91135 + Dias136Mas;
                        TOTALTOTAL += PorVencermenos45 + TOTALVENCIDO;

                        if (totales != 0)
                        {
                            dtcuenta.Rows.Add(row);
                        }


                        TotalVencido = 0;
                        PorVencerTotal = 0;
                        Dias45 = 0;
                        Dias4690 = 0;
                        Dias91135 = 0;
                        Dias136Mas = 0;
                        PorVencer = 0;
                        PorVencermenos45 = 0;
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    continue;
                }
            }
            DataRow row2 = dtcuenta.NewRow();
            row2["TotalVencido"] = "<strong> $" + TOTALVENCIDO.ToString("#,##0.00") + "</strong> ";
            //row["TotalPorVencer"] = PorVencerTotal;
            row2["Dia45"] = "<strong> $" + TOTALDIA45.ToString("#,##0.00") + "</strong> ";
            row2["Dia4690"] = "<strong> $" + TOTALDIA4690.ToString("#,##0.00") + "</strong> ";
            row2["Dia9135"] = "<strong> $" + TOTALDIA9135.ToString("#,##0.00") + "</strong> ";
            row2["Dia136Mas"] = "<strong> $" + TOTALDIA136MAS.ToString("#,##0.00") + "</strong> ";
            row2["xvencer"] = "<strong> $" + TOTAL45PORVENCER.ToString("#,##0.00") + "</strong> $";
            row2["Dia45PorVencer"] = "<strong> $" + TOTAL45PORVENCER.ToString("#,##0.00") + "</strong> ";
            row2["Total"] = TOTALTOTAL;
            dtcuenta.Rows.Add(row2);
            TOTALVENCIDO = 0;
            TOTALDIA45 = 0;
            TOTALDIA4690 = 0;
            TOTALDIA9135 = 0;
            TOTALDIA136MAS = 0;
            TOTAL45PORVENCER = 0;
            TOTALTOTAL = 0;

            //string pivote = dtCTA.Rows[0]["CCODIGOCLIENTE"].ToString();
            //float tTotal = 0;
            //float tCantidad = 0;
            //float tNeto = 0;
            //float tDescuento = 0;
            //float tNetoDesc = 0;
            //float tImpuesto = 0;

            //for (int i = 0; i < (dtCTA.Rows.Count); i++)
            //{
            //    //Definimos una fila para la tabla del reporte y llenamos con informacion de nuestra consulta del cargo
            //    DataRow rw = dtcuenta.NewRow();

            //    //rw["CIDDOCUMENTO"] = dtCTA.Rows[i]["CIDDOCUMENTO"].ToString();
            //    rw["CCODIGOCLIENTE"] = dtCTA.Rows[i]["CCODIGOCLIENTE"].ToString();
            //    rw["CRAZONSOCIAL"] = dtCTA.Rows[i]["CRAZONSOCIAL"].ToString();
            //    //rw["CIDPRODUCTO"] = dtCTA.Rows[i]["CIDPRODUCTO"].ToString();
            //    rw["CCODIGOPRODUCTO"] = dtCTA.Rows[i]["CCODIGOPRODUCTO"].ToString();
            //    rw["CNOMBREPRODUCTO"] = dtCTA.Rows[i]["CNOMBREPRODUCTO"].ToString();
            //    rw["CUnidades"] = dtCTA.Rows[i]["CUnidades"].ToString();
            //    float rwNETO = float.Parse(dtCTA.Rows[i]["CNETO"].ToString());
            //    rw["CNETO"] = "$" + rwNETO.ToString("#,##0.00");
            //    float rwCDESCUENTO1 = float.Parse(dtCTA.Rows[i]["CDESCUENTO1"].ToString());
            //    rw["CDESCUENTO1"] = "$" + rwCDESCUENTO1.ToString("#,##0.00");
            //    float rwNETODESC = float.Parse(dtCTA.Rows[i]["Neto-Desc"].ToString());
            //    rw["Neto-Desc"] = "$" + rwNETODESC.ToString("#,##0.00");
            //    float rwCIMPUESTO1 = float.Parse(dtCTA.Rows[i]["CIMPUESTO1"].ToString());
            //    rw["CIMPUESTO1"] = "$" + rwCIMPUESTO1.ToString("#,##0.00");
            //    float rwCTOTAL = float.Parse(dtCTA.Rows[i]["CTOTAL"].ToString());
            //    rw["CTOTAL"] = "$" + rwCTOTAL.ToString("#,##0.00");




            //    if (pivote == dtCTA.Rows[i]["CCODIGOCLIENTE"].ToString())
            //    {
            //        //Agregamos la fila a la tabla del reporte
            //        dtcuenta.Rows.Add(rw);
            //        //
            //        tTotal = tTotal + float.Parse(dtCTA.Rows[i]["CTOTAL"].ToString());
            //        tCantidad = tCantidad + float.Parse(dtCTA.Rows[i]["CUnidades"].ToString());
            //        tNeto = tNeto + float.Parse(dtCTA.Rows[i]["CNETO"].ToString());
            //        tDescuento = tDescuento + float.Parse(dtCTA.Rows[i]["CDESCUENTO1"].ToString());
            //        tNetoDesc = tNetoDesc + float.Parse(dtCTA.Rows[i]["Neto-Desc"].ToString());
            //        tImpuesto = tImpuesto + float.Parse(dtCTA.Rows[i]["CIMPUESTO1"].ToString());

            //        if (i == dtCTA.Rows.Count - 1)
            //        {

            //            DataRow rw2 = dtcuenta.NewRow();
            //            rw2["CTOTAL"] = "<strong> $" + tTotal.ToString("#,##0.00") + "</strong> $";
            //            rw2["CUnidades"] = "<strong>" + tCantidad + "</strong>";
            //            rw2["CNETO"] = "<strong> $" + tNeto.ToString("#,##0.00") + "</strong> $";
            //            rw2["CDESCUENTO1"] = "<strong> $" + tDescuento.ToString("#,##0.00") + "</strong> $";
            //            rw2["Neto-Desc"] = "<strong> $" + tNetoDesc.ToString("#,##0.00") + "</strong> $";
            //            rw2["CIMPUESTO1"] = "<strong> $" + tImpuesto.ToString("#,##0.00") + "</strong> $";
            //            rw2["CNOMBREPRODUCTO"] = "<strong>TOTAL</strong>";
            //            dtcuenta.Rows.Add(rw2);
            //        }

            //    }
            //    else
            //    {
            //        DataRow rw2 = dtcuenta.NewRow();
            //        rw2["CTOTAL"] = "<strong> $" + tTotal.ToString("#,##0.00") + "</strong>";
            //        rw2["CUnidades"] = "<strong> " + tCantidad + "</strong>";
            //        rw2["CNETO"] = "<strong> $" + tNeto.ToString("#,##0.00") + "</strong>";
            //        rw2["CDESCUENTO1"] = "<strong> $" + tDescuento.ToString("#,##0.00") + "</strong>";
            //        rw2["Neto-Desc"] = "<strong> $" + tNetoDesc.ToString("#,##0.00") + "</strong>";
            //        rw2["CIMPUESTO1"] = "<strong> $" + tImpuesto.ToString("#,##0.00") + "</strong>";
            //        rw2["CNOMBREPRODUCTO"] = "<strong>TOTAL</strong>";

            //        dtcuenta.Rows.Add(rw2);

            //        //Agregamos la fila a la tabla del reporte
            //        dtcuenta.Rows.Add(rw);
            //        //
            //        tTotal = 0;
            //        tCantidad = 0;
            //        tNeto = 0;
            //        tDescuento = 0;
            //        tNetoDesc = 0;
            //        tImpuesto = 0;
            //        pivote = dtCTA.Rows[i]["CCODIGOCLIENTE"].ToString();

            //        tTotal = tTotal + float.Parse(dtCTA.Rows[i]["CTOTAL"].ToString());
            //        tCantidad = tCantidad + float.Parse(dtCTA.Rows[i]["CUnidades"].ToString());
            //        tNeto = tNeto + float.Parse(dtCTA.Rows[i]["CNETO"].ToString());
            //        tDescuento = tDescuento + float.Parse(dtCTA.Rows[i]["CDESCUENTO1"].ToString());
            //        tNetoDesc = tNetoDesc + float.Parse(dtCTA.Rows[i]["Neto-Desc"].ToString());
            //        tImpuesto = tImpuesto + float.Parse(dtCTA.Rows[i]["CIMPUESTO1"].ToString());

            //        if (i == dtCTA.Rows.Count - 1)
            //        {

            //            DataRow rw3 = dtcuenta.NewRow();
            //            rw3["CTOTAL"] = "<strong> $" + tTotal.ToString("#,##0.00") + "</strong>";
            //            rw3["CUnidades"] = "<strong>" + tCantidad + "</strong>";
            //            rw3["CNETO"] = "<strong> $" + tNeto.ToString("#,##0.00") + "</strong>";
            //            rw3["CDESCUENTO1"] = "<strong> $" + tDescuento.ToString("#,##0.00") + "</strong>";
            //            rw3["Neto-Desc"] = "<strong> $" + tNetoDesc.ToString("#,##0.00") + "</strong>";
            //            rw3["CIMPUESTO1"] = "<strong> $" + tImpuesto.ToString("#,##0.00") + "</strong>";
            //            rw3["CNOMBREPRODUCTO"] = "<strong>TOTAL</strong>";

            //            dtcuenta.Rows.Add(rw3);
            //        }

            //    }
            //}


            DataView dv = dtcuenta.DefaultView;
            dv.Sort = "Total desc";
            DataTable dtShowSort = dv.ToTable();
            lvCliente.DataSource = dtShowSort;
            lvCliente.DataBind();
        }
    }
}