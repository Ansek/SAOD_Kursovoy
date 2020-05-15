using System;
using System.Collections;
using System.Collections.Specialized;
using SAOD_Kursovoy.Service;
using SAOD_Kursovoy.Model.Elements;

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
        private HashElements<T>[] _array;   // Массив элементов

        private readonly Func<string, ushort> _getHash; // Функция расчета хеш-значения

        /// <summary>
        /// Хеш-таблица.
        /// </summary>
        /// <param name="getHash">Функция для расчета хеш-значения.</param>
        public HashTable(Func<string, ushort> getHash)
        {
            _array = new HashElements<T>[_sizeSegments];
            _getHash = getHash;
            OnCollectionChanged();
        }

        /// <summary>
        /// Возвращает индекс на элемент с заданным ключом.
        /// В случае неудачи возвращает индекс на первый null.
        /// </summary>
        /// <param name="key">Ключевое значения для хеш-функции.</param>
        private uint GetIndex(string key)
        {
            uint hash = _getHash(key);  // Получение хеш-значения
            uint _startHash = hash;     // Сохранение начального значения
            uint i = 1;                 // Отслеживание количество попыток

            // Поиск элемента по ключу
            while (_array[hash] != null && _array[hash]?.Key != key)
            {
                i++; // Увелечение значения попыток
                // Расчет по квадратичному опробованию: адрес = h(x) + c·i + d·i^2
                hash = Convert.ToUInt32(_getHash(key) + _c * i + _d * i * i);

                // Проверка на выход за диапазон
                if (hash > MaxCount)
                    hash %= MaxCount;
            }

            return hash;    // Возвращение индекса
        }

        // Увеличивает размер таблицы
        private void ResizeTable()
        {
            _countSegments++;       // Увеличение количества сегментов        
            var oldArray = _array;  // Сохранение текущего массива
            _array = new HashElements<T>[MaxCount]; // Создание нового массива

            // Копирование элементов
            for (uint i = 0; i < oldArray.Length; i++)
                if (oldArray[i] != null)
                {
                    // Получение нового индекса для старого ключа
                    uint j = GetIndex(oldArray[i].Key);
                    _array[j] = oldArray[i];    // Запись в новый массив
                    oldArray[i] = null;         // Обнуление указателя в старом массиве
                }
        }

        private uint _count = 0;
        /// <summary>
        /// Количество элементов в таблице.
        /// </summary>
        public uint Count
        {
            get { return _count; }
            set { _count = value; }
        }

        /// <summary>
        /// Максимальное количество элементов в таблице.
        /// </summary>
        public uint MaxCount
        {
            get { return (uint)_sizeSegments * _countSegments; }
        }

        /// <summary>
        /// Добавляет элемент в хеш-таблицу.
        /// </summary>
        /// <param name="key">Ключевое значения для хеш-функции.</param>
        /// <param name="value">Добавляемое значение.</param>
        public void Add(string key, T value)
        {
            // Проверка, что заполненность хеш-таблицы превысила 50%
            if (_count > MaxCount / 2)
                ResizeTable();      // Увеличить размер таблицы

            // Получение индекса на новый элемент
            uint i = GetIndex(key);
            if (_array[i] != null && !_array[i].IsDelete)
                throw new Exception("Ошибка при добавлении. Ячейка уже занята!");

            //Создание нового объекта
            _array[i] = new HashElements<T>()
            {
                Hash = _getHash(key),
                Key = key,
                IsDelete = false,
                Value = value
            };

            // Сохранение сообщения в журнале
            Log.Add($"Добавлен объект \"{value}\".\nРазмещение по индексу {i}.");
            _count++;   // Увеличение количества

            // Оповещение об добавлении
            OnCollectionChanged();
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
        public void OnCollectionChanged()
        {
            // Параметр Reset используется, чтобы сохранить порядок элементов коллекции
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }

    /// <summary>
    /// Перечеслитель для элементов хеш-таблицы.
    /// </summary>
    class HashTableEnumerator<T> : IEnumerator
    {
        private HashElements<T>[] _array; // Массив элементов
        private int i;  // Указывает позицию текущего элемента

        //Конструктор для получения массива
        public HashTableEnumerator(HashElements<T>[] array)
        {
            _array = array;
        }

        object IEnumerator.Current => _array[i].Value; // Текущий элемент

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
