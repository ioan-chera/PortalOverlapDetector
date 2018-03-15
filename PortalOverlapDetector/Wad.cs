using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalOverlapDetector
{
    enum WadType
    {
        Iwad,
        Pwad
    }

    class Wad
    {
        WadType Type;
        List<Lump> mLumps;

        public Lump[] Lumps
        {
            get
            {
                return mLumps.ToArray();
            }
        }

        struct DirEntry
        {
            public int Address;
            public int Size;
            public string Name;
        }

        public Wad(string path)
        {
            using(var stream = new FileStream(path, FileMode.Open))
            {
                byte[] headerBlock = stream.ReadExact(4);
                Type = TypeFromBytes(headerBlock);
                int lumpCount = stream.ReadInt32();
                int infoTableOffset = stream.ReadInt32();
                stream.Seek(infoTableOffset, SeekOrigin.Begin);
                var entries = new DirEntry[lumpCount];
                for(int i = 0; i < lumpCount; ++i)
                {
                    entries[i].Address = stream.ReadInt32();
                    entries[i].Size = stream.ReadInt32();
                    entries[i].Name = stream.ReadCString(8);
                }
                mLumps = new List<Lump>(lumpCount);
                foreach(var entry in entries)
                {
                    stream.Seek(entry.Address, SeekOrigin.Begin);
                    mLumps.Add(new Lump(entry.Name, stream.ReadExact(entry.Size)));
                }
            }
        }

        static WadType TypeFromBytes(byte[] data)
        {
            string text = Encoding.UTF8.GetString(data);
            if(text == "IWAD")
                return WadType.Iwad;
            if(text == "PWAD")
                return WadType.Pwad;
            throw new WadException("Illegal wad type header");
        }
    }

    class WadException : Exception
    {
        public WadException(string message) : base(message)
        {
        }
    }
}
