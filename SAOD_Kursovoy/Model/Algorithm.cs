using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAOD_Kursovoy.Model
{
    static class Algorithm
    {
        static int GetHash(string key)
        {
            long d = long.Parse(key.Replace("-", ""));
            ushort i = (ushort)(d * 40503);
            return Convert.ToInt32(i >> 6);
        }

    }
}
