using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalOverlapDetector
{
    class Vertex
    {
        public short X { get; private set; }
        public short Y { get; private set; }

        public Vertex(short x, short y)
        {
            X = x;
            Y = y;
        }
    }
}
