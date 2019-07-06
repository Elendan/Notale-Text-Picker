using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polygon.Utils.Extensions
{
    public static class ByteArrayExtensions
    {
        public static string ToHexString(this byte[] barray)
        {
            var c = new char[barray.Length * 2];
            for (var i = 0; i < barray.Length; ++i)
            {
                var b = ((byte)(barray[i] >> 4));
                c[i * 2] = (char)(b > 9 ? b + 0x37 : b + 0x30);
                b = ((byte)(barray[i] & 0xF));
                c[i * 2 + 1] = (char)(b > 9 ? b + 0x37 : b + 0x30);
            }
            return new string(c);

        }
    }
}
