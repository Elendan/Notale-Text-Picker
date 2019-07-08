using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polygon.Utils.Extensions
{
    public static class StringExtensions
    {
        public static byte[] ToByteArray(this string hex)
        {
            var charNumber = hex.Length;
            var bytes = new byte[charNumber / 2];
            for (var i = 0; i < charNumber; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;

        }

        public static IEnumerable<string> SplitInParts(this string s, int partLength)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }

            if (partLength <= 0)
            {
                throw new ArgumentException("Part length has to be positive.", nameof(partLength));
            }

            for (var i = 0; i < s.Length; i += partLength)
            {
                yield return s.Substring(i, Math.Min(partLength, s.Length - i));
            }
        }

        public static string FindDirectory(this string path)
        {
            var splittedPath = path.Split('\\');

            if (!splittedPath.Any())
            {
                return string.Empty;
            }

            string[] data = new string[splittedPath.Length - 1];
            for (int i = 0; i < splittedPath.Length - 1; i++)
            {
                data[i] = splittedPath[i];
            }

            return string.Join("\\", data) + "\\";
        }
    }
}
