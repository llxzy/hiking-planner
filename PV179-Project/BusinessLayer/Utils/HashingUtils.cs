using System;
using System.Collections;
using System.Security.Cryptography;
using System.Text;

namespace BusinessLayer.Utils
{
    public static class HashingUtils
    {
        public static string Encode(string input)
        {
            var inputAsBytes = Encoding.ASCII.GetBytes(input);
            var hashAlg            = new SHA512Managed();
            return Convert.ToHexString(hashAlg.ComputeHash(inputAsBytes));
        }

        public static bool Validate(string input, string expected)
        {
            var hashedInput = Encode(input);
            return String.Equals(hashedInput, expected, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}