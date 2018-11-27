using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

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
    }
}