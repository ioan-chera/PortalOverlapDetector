using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalOverlapDetector
{
    class Program
    {
        static void Main(string[] args)
        {
            Wad wad = new Wad(args[0]);
            int index = 0;
            Lump linedefs, sidedefs, sectors, vertices;
            foreach(var lump in wad.Lumps)
            {
                if(lump.Name == args[1])
                {
                    linedefs = wad.Lumps[index + 2];
                    sidedefs = wad.Lumps[index + 3];
                    vertices = wad.Lumps[index + 4];
                    sectors = wad.Lumps[index + 8];
                    MakeMap(linedefs, sidedefs, vertices, sectors);
                    return;
                }
                ++index;
            }
            Console.WriteLine("Map not found");
        }

        static void MakeMap(Lump lumpLinedefs, Lump lumpSidedefs, Lump lumpVertices, Lump lumpSectors)
        {
            Vertex[] vertices = new Vertex[lumpVertices.Data.Length / 4];
            using(var stream = new MemoryStream(lumpVertices.Data))
                for(int i = 0; i < vertices.Length; ++i)
                    vertices[i] = new Vertex(stream.ReadInt16(), stream.ReadInt16());
            Sector[] sectors = new Sector[lumpSectors.Data.Length / 26];
            using(var stream = new MemoryStream(lumpSectors.Data))
                for(int i = 0; i < sectors.Length; ++i)
                {
                    sectors[i] = new Sector(stream.ReadInt16(), stream.ReadInt16(), stream.ReadCString(8), stream.ReadCString(8), stream.ReadInt16(), stream.ReadInt16(), stream.ReadInt16());
                }
            Sector[] sectorRefs = new Sector[lumpSidedefs.Data.Length / 30];
            using(var stream = new MemoryStream(lumpSidedefs.Data))
                for(int i = 0; i < sectorRefs.Length; ++i)
                {
                    stream.Seek(28, SeekOrigin.Current);
                    sectorRefs[i] = sectors[stream.ReadInt16()];
                }
            Linedef[] linedefs = new Linedef[lumpLinedefs.Data.Length / 14];
            using(var stream = new MemoryStream(lumpLinedefs.Data))
                for(int i = 0; i < linedefs.Length; ++i)
                {
                    Vertex v1 = vertices[stream.ReadInt16()];
                    Vertex v2 = vertices[stream.ReadInt16()];
                    stream.Seek(2, SeekOrigin.Current);
                    linedefs[i] = new Linedef(v1, v2, stream.ReadInt16(), stream.ReadInt16(),
                        sectorRefs.SafeAt(stream.ReadInt16()), sectorRefs.SafeAt(stream.ReadInt16()));
                    linedefs[i].S1?.Linedefs.Add(linedefs[i]);
                    linedefs[i].S2?.Linedefs.Add(linedefs[i]);
                }
            // now we have them.
            Level level = new Level(linedefs, vertices, sectors);
        }
    }
}
