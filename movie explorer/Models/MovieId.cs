using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace movie_explorer.Models
{
     public static class MovieId
    {
        public static string toHash(string title, int year) // generating hash code to use it as id for movies
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                var key = $"{Normalize(title)}_{year.ToString()}";
                // Convert the input string to a byte array and compute the hash
                var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(key));
                // Convert the byte array to a hexadecimal string
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();

            }
        }

        private static string Normalize(string title)
        {
            return title.Trim().ToLowerInvariant();
        }
    }
}
