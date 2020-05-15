using System;
using System.Collections;
using System.Collections.Specialized;

namespace SAOD_Kursovoy.Model
{
    /// <summary>
    /// Хеш-таблица.
    /// </summary>
    class HashTable<T> : IEnumerable, INotifyCollectionChanged
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

        public void Add(T value)
        {
            _array[_count] = value;
            _count++;
            OnCollectionChanged(NotifyCollectionChangedAction.Add, value);
        }

        /// <summary>
        /// Возвращает перечислитель для хеш-таблицы. 
        /// </summary>]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new HashTableEnumerator<T>(_array);
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;
        /// <summary>
        /// Оповещает об изменении в коллекции.
        /// </summary>
        /// <param name="action">Произошедшее действие.</param>
        public void OnCollectionChanged(NotifyCollectionChangedAction action, T element)
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action, element));
        }
    }

    /// <summary>
    /// Перечеслитель для элементов хеш-таблицы.
    /// </summary>
    class HashTableEnumerator<T> : IEnumerator
    {
        private T[] _array; // Массив элементов
        private int i;      // Указывает позицию текущего элемента

        //Конструктор для получения массива
        public HashTableEnumerator(T[] array)
        {
            _array = array;
        }

        object IEnumerator.Current => _array[i]; // Текущий элемент

        // Перемещает перечислитель к следующему элементу коллекции
        public bool MoveNext()
        {
            i++; // Увеличиваем индекс
            // Пока элемент пустой 
            while (i < _array.Length - 1 && _array[i] == null)
                i++; // Увеличиваем индекс
            
            return i < _array.Length - 1; // Проверка, что не вышли за границу
        }

        // Установка перечислителя в начальное положение
        public void Reset()
        {
            i = -1;
        }

        // Освобождает ресурсы
        public void Dispose() { }
    }
}
