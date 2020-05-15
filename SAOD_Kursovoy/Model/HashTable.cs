using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAOD_Kursovoy.Model
{
    /// <summary>
    /// Хеш-таблица.
    /// </summary>
    class HashTable<T>
    {
        private const ushort _sizeSegments = 1024;  // Размер сегмента
        private const byte _c = 2;  // Константы для квадратичного опробования: 
        private const byte _d = 3;  // адрес = h(x) + c·i + d·i^2

        private byte _countSegments = 1;    // Количество сегментов
        private T[] _array;                 // Массив элементов

        private readonly Func<string, int> _getHash; // Функция расчета хеш-значения

        /// <summary>
        /// Хеш-таблица.
        /// </summary>
        /// <param name="getHash">Функция для расчета хеш-значения.</param>
        public HashTable(Func<string, int> getHash)
        {
            _array = new T[_sizeSegments];
            _getHash = getHash;
        }

        private uint _count = 0;
        /// <summary>
        /// Количество элементво в таблице
        /// </summary>
        public uint Count
        {
            get { return _count; }
            set { _count = value; }
        }



    }
}
