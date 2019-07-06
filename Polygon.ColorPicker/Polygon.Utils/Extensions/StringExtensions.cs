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
    }
}
