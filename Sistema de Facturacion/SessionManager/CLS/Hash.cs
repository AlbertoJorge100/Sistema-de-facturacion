using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace SessionManager.CLS
{
    public static class Hash
    {
        public enum Opcion { SHA_1, SHA_256, SHA_384, SHA_512 };//algoritmos        
        public static string generarHash(String cadena, Opcion clave)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            switch (clave)
            {
                case Opcion.SHA_1:
                    {
                        SHA1 aux = SHA1Managed.Create();
                        stream = aux.ComputeHash(encoding.GetBytes(cadena));
                        break;
                    }
                case Opcion.SHA_256:
                    {
                        SHA256 aux = SHA256Managed.Create();
                        stream = aux.ComputeHash(encoding.GetBytes(cadena));
                        break;
                    }
                case Opcion.SHA_384:
                    {
                        SHA384 aux = SHA384Managed.Create();
                        stream = aux.ComputeHash(encoding.GetBytes(cadena));
                        break;
                    }
                case Opcion.SHA_512:
                    {
                        SHA512 aux = SHA512Managed.Create();
                        stream = aux.ComputeHash(encoding.GetBytes(cadena));
                        break;
                    }
            }
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }


        
    }
}