using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace Polygon.Utils.Extensions
{
    public static class ColorDialogExtensions
    {
        public static Color CreateColorFromDialog(this ColorDialog dialog)
        {
            return new Color
            {
                A = dialog.Color.A,
                B = dialog.Color.B,
                G = dialog.Color.G,
                R = dialog.Color.R
            };
        }
    }
}
