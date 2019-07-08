using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Polygon.Utils.Extensions
{
    public static class ColorExtensions
    {
        public static string ToNostaleFormat(this Color c)
        {
            return $"{c.B:X2}{c.G:X2}{c.R:X2}{c.A:X2}";
        }
    }
}
