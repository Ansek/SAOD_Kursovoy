﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAOD_Kursovoy.Model
{
    static class Algorithm
    {
        /// <summary>
        /// Получает хеш-значение на основе ключа. 
        /// </summary>
        /// <param name="key">Ключ.</param>
        public static ushort GetHash(string key)
        {
            long d = long.Parse(key.Replace("-", ""));
            ushort i = (ushort)(d * 40503); 
            return Convert.ToUInt16(i >> 6);
        }

    }
}
