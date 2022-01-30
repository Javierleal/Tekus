using System;
using System.Text.RegularExpressions;

namespace API.Extensions
{
    public static class StringValid
    {
        /// <summary>
        /// Validar formato de un email
        /// </summary>
        /// <param name="email">Direccion de correo</param>
        /// <returns></returns>
        public static bool ValidMail(this string email)
        {
            if (email == null)
                return false;
            if (email == "")
                return false;
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Metodo para validar NIT de colombia
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool ValidNit(this string NIT)
        {
            if (NIT == null)
                return false;
            if (NIT == "")
                return false;
            NIT = NIT.Replace(".", "").Replace("-", "");
            if (NIT.Length != 10)
                return false;
            var digitos = new byte[10];
            for (int i = 0; i < NIT.Length; i++)
            {
                if (!char.IsDigit(NIT[i]))
                    return false;
                digitos[i] = byte.Parse(NIT[i].ToString());
            }
            var v = 41 * digitos[0] +
                    37 * digitos[1] +
                    29 * digitos[2] +
                    23 * digitos[3] +
                    19 * digitos[4] +
                    17 * digitos[5] +
                    13 * digitos[6] +
                    7 * digitos[7] +
                    3 * digitos[8];
            v = v % 11;
            if (v >= 2)
                v = 11 - v;
            return v == digitos[9];
        }
    }
}
