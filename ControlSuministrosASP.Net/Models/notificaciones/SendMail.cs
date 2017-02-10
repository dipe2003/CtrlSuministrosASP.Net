using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Web;

namespace ControlSuministrosASP.Net.Models.notificaciones {
    
    public class SendMail {
        public int SenMailId { get; set; }
        public String Host { get; set; }
        public int Port { get; set; }
        public String FromAddress { get; set; }
        public String FromPassword { get; set; }
        public bool UsarSSL { get; set; }

        public SendMail() {

        }

        /// <summary>
        /// Envia el mensaje esepcificado.
        /// </summary>
        /// <param name="mensaje"></param>
        /// <param name="destinatario"></param>
        /// <param name="asunto"></param>
        /// <returns></returns>
        public bool EnviarMail(String mensaje, String destinatario, String asunto) {
            // smtp settings
            var smtp = new System.Net.Mail.SmtpClient();
            {
                smtp.Host = Host;
                smtp.Port = Port;
                smtp.EnableSsl = UsarSSL;
                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(FromAddress, FromPassword);
                smtp.Timeout = 20000;
            }
            try {
                smtp.Send(FromAddress, destinatario, asunto, mensaje);
            }catch(Exception ex) {
                System.Console.Write("No se pudo enviar correo: " + ex.Message);
                return false;
            }

            System.Console.Write("El mensaje fue enviado.");
            return true;
            
        }

    }
}