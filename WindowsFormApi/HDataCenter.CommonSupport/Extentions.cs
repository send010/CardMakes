using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDataCenter.CommonSupport
{
    public static class Extentions
    {
        public static string ToString2(this Object obj)
        {
            return obj == null ? string.Empty : obj.ToString();
        }
    }
}
