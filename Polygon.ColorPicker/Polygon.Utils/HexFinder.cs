﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polygon.Utils.Extensions;

namespace Polygon.Utils
{
    public class HexFinder
    {
        private readonly string _nostalePath;
        private readonly string _newColor;

        private readonly List<string> _oldHexToString;
        private readonly List<string> _newHexToString;
        private readonly List<byte[]> _byteHex;

        public HexFinder(string nostalePath, string newColor)
        {
            _nostalePath = nostalePath;
            _newColor = newColor;

            _oldHexToString = new List<string>();
            _newHexToString = new List<string>();
            _byteHex = new List<byte[]>();
        }

        public bool ReplaceColorPattern(string colorPattern, int substringIndex)
        {
            var byteData = Deserialization.ReadFully(new FileStream(_nostalePath, FileMode.Open));
            _oldHexToString.Add(byteData.ToHexString());

            var currentInfo = _oldHexToString.FirstOrDefault(s => s.Contains(colorPattern));
            if (currentInfo == null)
            {
                return false;
            }

            using (var writer = new BinaryWriter(File.Open(_nostalePath, FileMode.Open)))
            {
                for (var i = 0; i < _oldHexToString.Count; i++)
                {
                    _oldHexToString[i] = _oldHexToString[i].Replace(colorPattern, $"{colorPattern.Substring(0, substringIndex)}{_newColor}");
                    _newHexToString.Add(_oldHexToString[i]);
                }

                foreach (var currentString in _newHexToString)
                {
                    var byteArray = currentString.ToByteArray();
                    _byteHex.Add(byteArray);
                }

                foreach (var hexBytes in _byteHex)
                {
                    writer.Write(hexBytes);
                }
            }

            return true;
        }
    }
}
