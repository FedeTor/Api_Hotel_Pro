using System;
using System.Text;

namespace Common.HashMethods
{
    public class Hashes
    {
        public static string HashearContraseña(string contraseña)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(contraseña);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            String hash = System.Text.Encoding.ASCII.GetString(data);
            return hash;
        }
        public static string HashearToken(string token)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            string hash = String.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(token));
            foreach (byte theByte in crypto)
            {
                hash += theByte.ToString("x2");
            }
            return hash;
        }
    }
}
