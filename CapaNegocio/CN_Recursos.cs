﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Mail;
using System.Net;
using System.IO;

namespace CapaNegocio
{
    public class CN_Recursos
    {
        public static string GenerarClave()
        {
            string clave = Guid.NewGuid().ToString("N").Substring(0,4);
            return clave;
        }

        public static bool EnviarCorreo(string correo,string asunto,string mensaje)
        {
            bool resultado= false;

            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(correo);
                mail.From = new MailAddress("pruebapractica2024@gmail.com");
                mail.Subject = asunto;
                mail.Body = mensaje;
                mail.IsBodyHtml = true;

                var smtp = new SmtpClient() {

                    Credentials = new NetworkCredential("pruebapractica2024@gmail.com", "xxwifypjjmfulooi"),
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true
                };

                smtp.Send(mail); 
                resultado = true;
            }
            catch(Exception ex)
            {
                resultado = false;
            }

            return resultado;
        }

        public static string ConvertirBase64(string ruta, out bool conversion)
        {
            string textoBase64 = string.Empty;
            conversion = true;

            try
            {
                byte[] bytes = File.ReadAllBytes(ruta);
                textoBase64 = Convert.ToBase64String(bytes);
            }
            catch 
            { 
                conversion = false;
            
            }
            return textoBase64;
        }


    }
}