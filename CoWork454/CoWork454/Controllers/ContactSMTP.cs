using System;
using CoWork454.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;

namespace CoWork454.Controllers
{
    public class ContactSMTPController : Controller
    {

        public class ConfigSMTP
        {
            //SMTP parameters
            public static string smtpAdress = "smtp.gmail.com";
            public static int portNumber = 587;
            public static bool enableSSL = true;
            //need it for the secured connection
            public static string from = "codemaster.team3@gmail.com";
            public static string password = "Betachad1!";
        }

        [HttpPost]
        public ActionResult SendEmail(ContactForm contactForm)
        {
            try
            {
                Response.StatusCode = 200;
                //getting useful configuration
                string smtpAddress = ConfigSMTP.smtpAdress;
                int portNumber = ConfigSMTP.portNumber;   //Smtp port
                bool enableSSL = ConfigSMTP.enableSSL;  //SSL enable
                string fromAdd = ConfigSMTP.from;
                string password = ConfigSMTP.password;

                //Get Body from ContactForm Model
                String body = "From: " + contactForm.Name + "<br>";
                body += "Company: " + contactForm.Company + "<br>";
                body += "Email: " + contactForm.Email + "<br>";
                body += "Contact Number: " + contactForm.ContactNo + "<br>";

                body += "Subject: " + contactForm.Message + "<br>";


                using (MailMessage mail = new MailMessage(new MailAddress(fromAdd), new MailAddress(fromAdd)))
                {
                    //mail.From = new MailAddress(fromAdd);
                    //mail.To = new MailAddress(fromAdd);
                    mail.Subject = "Contact Enquiry";
                    mail.Body = body;
                    mail.IsBodyHtml = true;

                    // smtp pass credentials and send mail
                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.EnableSsl = enableSSL;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Credentials = new NetworkCredential(fromAdd, password);
                        smtp.Timeout = 20000;

                        // Passing values to smtp object to send
                        smtp.Send(mail);
                    }
                }

            }
            catch (Exception e)
            {
                return BadRequest();
                //Response.StatusCode = 400;
            }
            return RedirectToAction("Index","Home");
        }
    }
}
