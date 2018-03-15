using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalOverlapDetector
{
    class Portal
    {
        public double Dx { get; private set; }
        public double Dy { get; private set; }

        Portal(double dx, double dy)
        {
            Dx = dx;
            Dy = dy;
        }

        static List<Portal> Portals;

        public static Portal GetPortal(double dx, double dy)
        {
            foreach(var portal in Portals)
                if(portal.Dx == dx && portal.Dy == dy)
                    return portal;
            Portals.Add(new Portal(dx, dy));
            return Portals.Last();
        }
    }
}
