using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polygon.Utils
{
    public class HexFinder
    {
        private IEnumerable<string> _oldHexToString;
        private IEnumerable<string> _newHexToString;
        private IEnumerable<byte[]> _byteHex;

        public HexFinder()
        {
            _oldHexToString = new List<string>();
            _newHexToString = new List<string>();
            _byteHex = new List<byte[]>();
        }


    }
}
