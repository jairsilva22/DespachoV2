using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace despacho
{
    public class cUtilidades
    {
        //variables
        private string cadena;

        //propiedades
        public int idUsuarioActivo { get; set; }
        public int idSucursalActiva { get; set; }
        public string motivo { get; set; }
        public DateTime fecha { get; set; }
        public DateTime hora { get; set; }

        //Constructor
        public cUtilidades()
        {
            cadena = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;
        }

        public static string activo(bool valor)
        {
            if (valor == true)
            {
                return "icon-check";
            }
            else
            {
                return "icon-cross";
            }
        }

        //metodo para insertar
        public void generarAlerta()
        {
            try
            {
                hora = DateTime.Parse(getDateTimeFromDB());
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    string comando = "INSERT INTO alertaCambios (motivo, fecha, hora, idUsuario, idSucursal) VALUES(@motivo, @fecha, @hora, @idUsuario, @idSucursal)";
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@motivo", motivo));
                        cmd.Parameters.Add(new SqlParameter("@fecha", hora));
                        cmd.Parameters.Add(new SqlParameter("@hora", hora.AddSeconds(12)));
                        cmd.Parameters.Add(new SqlParameter("@idUsuario", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursalActiva));

                        int filas = cmd.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        //Método para obtener el último id registrado en la tabla
        public DataTable consultarAlerta()
        {
            try
            {
                hora = DateTime.Parse(getDateTimeFromDB());
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    string selectComand = "* ";
                    string fromComand = "alertaCambios ";
                    string whereComand = "(idSucursal = " + idSucursalActiva + ") AND (fecha = '" + DateTime.Now.ToString("yyyy-MM-dd") + "')  AND (hora >= '" + hora.TimeOfDay + "')";
                    string orderByComand = "hora;";


                    using (SqlCommand cmd = new SqlCommand("SELECT " + selectComand + "FROM " + fromComand + "WHERE " + whereComand + " ORDER BY " + orderByComand, conn))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //Método para obtener el último id registrado en la tabla
        public string getDateTimeFromDB()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT GETDATE() AS datetime", conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    return reader["datetime"].ToString();
                                }
                                return "";
                            }
                        }
                        return "";
                    }
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        //metodo para insertar
        public void generarBiAlertaBitacora(int idMaster, string accion)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    string comando = "INSERT INTO alertaCambiosBitacora (idMaster, idUsuario, accion, fecha, idSucursal, sistema) VALUES(@idMaster, @idUsuario, @accion, @fecha, @idSucursal, 'Despacho')";
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idMaster", idMaster));
                        cmd.Parameters.Add(new SqlParameter("@idUsuario", idUsuarioActivo));
                        cmd.Parameters.Add(new SqlParameter("@accion", accion));
                        cmd.Parameters.Add(new SqlParameter("@fecha", DateTime.Parse(getDateTimeFromDB())));
                        cmd.Parameters.Add(new SqlParameter("@idSucursal", idSucursalActiva));

                        int filas = cmd.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public bool alertaVista(int idMaster)
        {
            try
            {
                string comando = "SELECT idMaster FROM alertaCambiosBitacora WHERE idMaster = @idMaster AND sistema like 'Dosificacion' AND idUsuario=@idUsuario";
                //string comando = "SELECT idMaster FROM alertaCambiosBitacora WHERE idMaster = @idMaster AND sistema like 'Dosificacion'";
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idMaster", idMaster));
                        cmd.Parameters.Add(new SqlParameter("@idUsuario", idUsuarioActivo));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw (ex);
            }
        }

        //public string[] getHora(string sHora)
        //{
        //    string[] arr = new string[2];
        //    arr[0] = sHora.Substring(0, 2);
        //    arr[1] = sHora.Substring(3, 2);
        //    arr[2] = sHora.Substring(6, 2);
        //    return arr;
        //}

        public static void enviarCorreo(string correoCS, string msj, string cc, string archivo, string asunto, List<string> lista, List<string> listaAdj, string ruta, string alias)
        {
            try
            {
                // informacion del correo
                //ConfigMenor config = new ConfigMenor();
                //config.obtenerConfiguracion();

                //variables a utilizar
                //string credencialsmtp = config.smtp;
                //string credencialcorreo = config.correo;
                //string credencialpassword = config.password;

                //variables a utilizar
                string credencialsmtp = "mail.csi01.com";
                string credencialcorreo = "hcaballero@csi01.com";
                string credencialpassword = "Hector*2020";

                SmtpClient smtp = new SmtpClient();

                //agregamos los datos para el envio de correos
                using (MailMessage correo = new MailMessage())
                {
                    if (alias != "")
                    {
                        correo.From = new MailAddress(credencialcorreo, alias, System.Text.Encoding.UTF8);
                    }
                    else
                    {
                        correo.From = new MailAddress(credencialcorreo);
                    }
                    correo.To.Clear();
                    //correo.To.Add(correoCS);
                    char[] delimitador = new char[] { ';' };
                    correoCS = correoCS.Replace("ˇ", ";");
                    foreach (string destinos in correoCS.Split(delimitador))
                    {
                        correo.To.Add(new MailAddress(destinos));
                    }

                    if (cc != "")
                    {
                        correo.CC.Add(cc);
                    }
                    correo.Subject = asunto;
                    correo.Body = msj.Replace("\r\n", "<br />");
                    correo.IsBodyHtml = true;
                    correo.Priority = MailPriority.Normal;

                    //valida si se incluye archivo
                    if (archivo != "")
                    {
                        correo.Attachments.Add(new Attachment(archivo));
                    }

                    //valida si se incluye el archivo de equipo
                    if (lista.Count > 0)
                    {
                        for (int i = 0; i < lista.Count; i++)
                        {
                            string arch = lista.ElementAt(i);
                            if (arch != "")
                            {
                                correo.Attachments.Add(new Attachment(arch));
                            }
                        }
                    }


                    if (listaAdj.Count > 0)
                    {
                        for (int i = 0; i < listaAdj.Count; i++)
                        {
                            string arch = listaAdj.ElementAt(i);
                            if (arch != "")
                            {
                                correo.Attachments.Add(new Attachment(ruta + arch));
                            }
                        }
                    }

                    //llenamos los datos para el smtp
                    smtp.Host = credencialsmtp;
                    smtp.Port = 587;
                    smtp.Credentials = new NetworkCredential(credencialcorreo, credencialpassword);
                    smtp.EnableSsl = false;
                    smtp.Send(correo);
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public static string validacion(string cadenaValidacion)
        {
            cadenaValidacion = cadenaValidacion.Replace("°", " ");
            cadenaValidacion = cadenaValidacion.Replace("<", "");
            cadenaValidacion = cadenaValidacion.Replace(">", "");
            cadenaValidacion = cadenaValidacion.Replace("'", "");
            cadenaValidacion = cadenaValidacion.Replace("´", "");
            cadenaValidacion = cadenaValidacion.Replace("°", "");
            cadenaValidacion = cadenaValidacion.Replace("º", " ");
            cadenaValidacion = cadenaValidacion.Replace("/", "");
            cadenaValidacion = cadenaValidacion.Replace("¡", "");
            cadenaValidacion = cadenaValidacion.Replace("!", "");
            cadenaValidacion = cadenaValidacion.Replace("¿", "");
            cadenaValidacion = cadenaValidacion.Replace("?", "");
            cadenaValidacion = cadenaValidacion.Replace("ā", "a");
            cadenaValidacion = cadenaValidacion.Replace("ă", "a");
            cadenaValidacion = cadenaValidacion.Replace("á", "a");
            cadenaValidacion = cadenaValidacion.Replace("é", "e");
            cadenaValidacion = cadenaValidacion.Replace("í", "i");
            cadenaValidacion = cadenaValidacion.Replace("ó", "o");
            cadenaValidacion = cadenaValidacion.Replace("ú", "u");

            cadenaValidacion = cadenaValidacion.Replace("Á", "A");
            cadenaValidacion = cadenaValidacion.Replace("É", "E");
            cadenaValidacion = cadenaValidacion.Replace("Í", "I");
            cadenaValidacion = cadenaValidacion.Replace("Ó", "O");
            cadenaValidacion = cadenaValidacion.Replace("Ú", "U");

            cadenaValidacion = cadenaValidacion.Replace("Ñ", "N");
            cadenaValidacion = cadenaValidacion.Replace("Ç", "C");
            cadenaValidacion = cadenaValidacion.Replace("ñ", "n");
            cadenaValidacion = cadenaValidacion.Replace("ç", "c");

            cadenaValidacion = cadenaValidacion.Replace("à", "a");
            cadenaValidacion = cadenaValidacion.Replace("è", "e");
            cadenaValidacion = cadenaValidacion.Replace("ì", "i");
            cadenaValidacion = cadenaValidacion.Replace("ò", "o");
            cadenaValidacion = cadenaValidacion.Replace("ù", "u");

            cadenaValidacion = cadenaValidacion.Replace("À", "A");
            cadenaValidacion = cadenaValidacion.Replace("È", "E");
            cadenaValidacion = cadenaValidacion.Replace("Ì", "I");
            cadenaValidacion = cadenaValidacion.Replace("Ò", "O");
            cadenaValidacion = cadenaValidacion.Replace("Ù", "U");

            cadenaValidacion = cadenaValidacion.Replace("ä", "a");
            cadenaValidacion = cadenaValidacion.Replace("ë", "e");
            cadenaValidacion = cadenaValidacion.Replace("ï", "i");
            cadenaValidacion = cadenaValidacion.Replace("ö", "o");
            cadenaValidacion = cadenaValidacion.Replace("ü", "u");

            cadenaValidacion = cadenaValidacion.Replace("Ä", "A");
            cadenaValidacion = cadenaValidacion.Replace("Ë", "E");
            cadenaValidacion = cadenaValidacion.Replace("Ï", "I");
            cadenaValidacion = cadenaValidacion.Replace("Ö", "O");
            cadenaValidacion = cadenaValidacion.Replace("Ü", "U");

            cadenaValidacion = cadenaValidacion.Replace("â", "a");
            cadenaValidacion = cadenaValidacion.Replace("ê", "e");
            cadenaValidacion = cadenaValidacion.Replace("î", "i");
            cadenaValidacion = cadenaValidacion.Replace("ô", "o");
            cadenaValidacion = cadenaValidacion.Replace("û", "u");

            cadenaValidacion = cadenaValidacion.Replace("Â", "A");
            cadenaValidacion = cadenaValidacion.Replace("Ê", "E");
            cadenaValidacion = cadenaValidacion.Replace("Î", "I");
            cadenaValidacion = cadenaValidacion.Replace("Ô", "O");
            cadenaValidacion = cadenaValidacion.Replace("Û", "U");

            cadenaValidacion = cadenaValidacion.Replace("½", "1/2");
            cadenaValidacion = cadenaValidacion.Replace("¼", "1/4");
            cadenaValidacion = cadenaValidacion.Replace("Ø", "");
            cadenaValidacion = cadenaValidacion.Replace("~", "");
            cadenaValidacion = cadenaValidacion.Replace("™", "");
            cadenaValidacion = cadenaValidacion.Replace("ˇ", "");
            cadenaValidacion = cadenaValidacion.Replace(":", "");
            cadenaValidacion = cadenaValidacion.Replace(";", "");
            cadenaValidacion = cadenaValidacion.Replace("|", "");

            return cadenaValidacion;
        }
    }
}