using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalOverlapDetector
{
    /// <summary>
    /// Extensions for Stream
    /// </summary>
    static class ExtensionStream
    {
        public static byte[] ReadExact(this Stream stream, int count)
        {
            int readSoFar = 0;
            byte[] buffer = new byte[count];
            while(readSoFar < count)
            {
                int amount = stream.Read(buffer, readSoFar, count - readSoFar);
                if(amount == 0)
                    throw new IOException("EOF");
                readSoFar += amount;
            }
            return buffer;
        }

        public static int ReadInt32(this Stream stream)
        {
            byte[] buffer = stream.ReadExact(4);
            return buffer[0] | buffer[1] << 8 | buffer[2] << 16 | buffer[3] << 24;
        }

        public static short ReadInt16(this Stream stream)
        {
            byte[] buffer = stream.ReadExact(2);
            return (short)(buffer[0] | buffer[1] << 8);
        }

        /// <summary>
        /// Reads a null-terminated C string of up to N bytes.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="byteCount"></param>
        /// <returns></returns>
        public static string ReadCString(this Stream stream, int byteCount)
        {
            byte[] buffer = stream.ReadExact(byteCount);
            int i;
            for(i = 0; i < buffer.Length; ++i)
                if(buffer[i] == 0)
                    break;
            if(i == 0)
                return "";
            return Encoding.UTF8.GetString(buffer, 0, i);
        }
    }
}
