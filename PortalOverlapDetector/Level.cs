using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalOverlapDetector
{
    class Level
    {
        enum Special
        {
            PortalCeiling = 358,
            PortalCeilingAnchor = 360,
            PortalFloor = 359,
            PortalFloorAnchor = 361,
            PortalLine = 376,
            PortalLineAnchor = 377
        }

        static readonly Dictionary<Special, Special> PortalLinks = new Dictionary<Special, Special>()
        {
            {Special.PortalCeiling, Special.PortalCeilingAnchor },
            {Special.PortalFloor, Special.PortalFloorAnchor },
            {Special.PortalLine, Special.PortalLineAnchor },
        };

        public Linedef[] Linedefs { get; private set; }
        public Vertex[] Vertices { get; private set; }
        public Sector[] Sectors { get; private set; }

        List<Island> Islands = new List<Island>();

        public Level(Linedef[] linedefs, Vertex[] vertices, Sector[] sectors)
        {
            Linedefs = linedefs;
            Vertices = vertices;
            Sectors = sectors;
        }

        void FindIslands()
        {
            foreach(var sector in Sectors)
            {
                if(sector.Island != null)
                    continue;
                MakeIsland(sector);
            }
        }

        void MakeIsland(Sector sector)
        {
            Island island = new Island(Islands.Count);
            var queue = new Queue<Sector>();
            queue.Enqueue(sector);
            while(queue.Count > 0)
            {
                sector = queue.Dequeue();
                if(sector.Island != null)
                    continue;
                sector.Island = island;
                island.Sectors.Add(sector);
                foreach(var neigh in sector.Neighs)
                {
                    if(neigh.Island != null)
                        continue;
                    queue.Enqueue(neigh);
                }
            }
            Islands.Add(island);
        }

        void FindPortals()
        {
            var queue = new Queue<Island>();
            var island = Islands.First();
            queue.Enqueue(island);
            while(queue.Count > 0)
            {
                island = queue.Dequeue();
                island.Fixed = true;

                // Find neighs
                foreach(var sector in island.Sectors)
                {
                    foreach(var linedef in sector.Linedefs)
                    {
                        if(linedef.Type == (short)Special.PortalCeiling)
                        {
                            Sector otherSector = SectorWithTag(linedef.Tag);
                            Linedef otherLine = LinedefWithTag(linedef.Tag, Special.PortalCeilingAnchor);
                            // TODO
                        }
                    }
                }
            }
        }

        Linedef LinedefWithTag(short tag, Special special)
        {
            foreach(var linedef in Linedefs)
                if(linedef.Tag == tag && linedef.Type == (short)special)
                    return linedef;
            return null;
        }

        Sector SectorWithTag(short tag)
        {
            foreach(var sector in Sectors)
                if(sector.Tag == tag)
                    return sector;
            return null;
        }


    }
}
