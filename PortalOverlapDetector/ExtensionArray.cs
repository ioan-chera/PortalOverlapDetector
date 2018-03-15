using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalOverlapDetector
{
    static class ExtensionArray
    {
        public static T SafeAt<T>(this T[] arr, int index) where T: class
        {
            return index == -1 ? null : arr[index];
        }
    }
}
