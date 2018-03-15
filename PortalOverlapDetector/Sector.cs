using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalOverlapDetector
{
    class Sector
    {
        public short HeightFloor { get; private set; }
        public short HeightCeiling { get; private set; }
        public string TextureFloor { get; private set; }
        public string TextureCeiling { get; private set; }
        public short Light { get; private set; }
        public short Type { get; private set; }
        public short Tag { get; private set; }

        public Island Island { get; set; }
        public List<Linedef> Linedefs { get; private set; }

        public Sector[] Neighs
        {
            get
            {
                List<Sector> neighs = new List<Sector>();
                foreach(var linedef in Linedefs)
                {
                    if(linedef.S1 == this)
                    {
                        if(linedef.S2 != null)
                            neighs.Add(linedef.S2);
                    }
                    else
                        neighs.Add(linedef.S1);
                }
                return neighs.ToArray();
            }
        }


        public Sector(short heightFloor,
            short heightCeiling,
            string textureFloor,
            string textureCeiling,
            short light,
            short type,
            short tag)
        {
            HeightFloor = heightFloor;
            HeightCeiling = heightCeiling;
            TextureFloor = textureFloor;
            TextureCeiling = textureCeiling;
            Light = light;
            Type = type;
            Tag = tag;

            Linedefs = new List<Linedef>();
        }
    }
}
