using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace despacho.Ajax
{
    public partial class buscarClientes : System.Web.UI.Page
    {
        cClientes cClt;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            cClt = new cClientes();
            if(!IsPostBack)
            {
                cClt.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
                DataTable dt = cClt.buscarClientes();
                string[] param = new string[dt.Rows.Count +1];
                
               // param = "[ {fp:'', nombre: '', clave: '', mod: 0}";
                if (dt.Rows.Count > 0)
                {
                    param[0] = "[ {fp:\"\", nombre: \"\", clave: \"\", mod: 0: select: 0},";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //if (i == 0)
                        //{
                        //    param += ", ";
                        //}
                        //int cont = i + 1;

                        if (i == dt.Rows.Count - 1)
                        {
                            param[i+1] = "{fp: \"" + dt.Rows[i]["formaPago"].ToString() + "\", nombre: \"" + dt.Rows[i]["nombre"].ToString() + "\", clave: \"" + dt.Rows[i]["clave"].ToString() + "\", mod: " + dt.Rows[i]["id"].ToString() + ", select: "+ dt.Rows[i]["id"].ToString() + "}]";
                           // param += "{fp: '" + dt.Rows[i]["formaPago"].ToString() + "', nombre: '" + dt.Rows[i]["nombre"].ToString() + "', clave: '" + dt.Rows[i]["clave"].ToString() + "', mod: " + dt.Rows[i]["id"].ToString() + "}";
                        }
                        else
                        {
                            param[i+1] = "{fp: \"" + dt.Rows[i]["formaPago"].ToString() + "\", nombre: \"" + dt.Rows[i]["nombre"].ToString() + "\", clave: \"" + dt.Rows[i]["clave"].ToString() + "\", mod: " + dt.Rows[i]["id"].ToString() + ", select: " + dt.Rows[i]["id"].ToString() + "}, ";
                           // param += "{fp: '" + dt.Rows[i]["formaPago"].ToString() + "', nombre: '" + dt.Rows[i]["nombre"].ToString() + "', clave: '" + dt.Rows[i]["clave"].ToString() + "', mod: " + dt.Rows[i]["id"].ToString() + "}, ";
                        }
                    }
                }
                else
                {
                    param[0] = "[{fp:\"\", nombre: \"\", clave: \"\", mod: 0: select: 0}]";
                }
                //param += "]";

                Response.Write(param);
            }
        }
    }
}