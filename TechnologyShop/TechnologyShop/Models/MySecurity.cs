using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Helpers;

namespace TechnologyShop.Models
{
    public class MySecurity
    {
        public static string EncryptPass(string pass)
        {
            SHA256 sha = SHA256Managed.Create();
            string signdata = "";
            byte[] result = sha.ComputeHash(Encoding.Unicode.GetBytes(pass + signdata));

            return BitConverter.ToString(result).Replace("-", "").ToLower();

        }

        public static string RandomString(int length)
        {
             Random rnd = new Random();
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[rnd.Next(s.Length)]).ToArray());
        }

        public static string RandomPassword(int length)
        {
            Random rnd = new Random();
            const string chars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[rnd.Next(s.Length)]).ToArray());
        }

        public static bool SendMail(string email, string subject, string body)
        {
            try
            {
                //Configuring webMail class to send emails  
                //gmail smtp server  
                WebMail.SmtpServer = "smtp.gmail.com";
                //gmail port to send emails  
                WebMail.SmtpPort = 587;
                WebMail.SmtpUseDefaultCredentials = true;
                //sending emails with secure protocol  
                WebMail.EnableSsl = true;
                //EmailId used to send emails from application  
                WebMail.UserName = "lehoanghai22@gmail.com";
                WebMail.Password = "yeumynhju1";

                //Sender email address.  
                WebMail.From = "lehoanghai22@gmail.com";

                //Send email  
                WebMail.Send(to: email, subject: subject, body: body, cc: "", bcc: "", isBodyHtml: true);

                return true;
             
            }
            catch (Exception ex)
            {

            }
            return false;
        }

    }
}