using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeldCompareExtension
{
    public static class MeldInvoker
    {
        public static void Invoke(params string[] paths)
        {
            _Invoke(paths);
        }

        public static void Invoke(IEnumerable<string> paths)
        {
            _Invoke(paths);
        }

        private static void _Invoke(IEnumerable<string> paths)
        {

        }
    }
}
