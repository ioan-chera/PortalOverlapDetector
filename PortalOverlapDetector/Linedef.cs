using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalOverlapDetector
{
    class Linedef
    {
        public Vertex V1 { get; private set; }
        public Vertex V2 { get; private set; }
        public short Type { get; private set; }
        public short Tag { get; private set; }
        public Sector S1 { get; private set; }
        public Sector S2 { get; private set; }

        public double MidX
        {
            get
            {
                return (V1.X + V2.X) / 2.0;
            }
        }

        public double MidY
        {
            get
            {
                return (V1.Y + V2.Y) / 2.0;
            }
        }

        public Linedef(Vertex v1, Vertex v2, short type, short tag, Sector s1, Sector s2)
        {
            V1 = v1;
            V2 = v2;
            Type = type;
            Tag = tag;
            S1 = s1;
            S2 = s2;
        }
    }
}
