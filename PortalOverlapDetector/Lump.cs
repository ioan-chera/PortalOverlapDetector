using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalOverlapDetector
{
    class Lump
    {
        public string Name { get; private set; }
        public byte[] Data { get; private set; }

        public Lump(string name, byte[] data)
        {
            Name = name;
            Data = data;
        }
    }
}
