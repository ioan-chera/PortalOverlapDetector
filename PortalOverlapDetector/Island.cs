using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalOverlapDetector
{
    /// <summary>
    /// Island of sectors, disjointed from others
    /// </summary>
    class Island
    {
        public List<Sector> Sectors { get; private set; }
        public int Id { get; private set; } // unique id

        public double x, y; // link offsets
        public bool Fixed;  // whether it's fixed in space

        public Island(int id)
        {
            Sectors = new List<Sector>();
            Id = id;
        }
    }
}
