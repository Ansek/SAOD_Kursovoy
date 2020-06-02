using SAOD_Kursovoy.Model.Elements;

namespace SAOD_Kursovoy.Model
{
    /// <summary>
    /// Хранит список инвертированных индексов.
    /// </summary>
    class InvertedList
    {
        private List<InvertedIndex> _invertedList; // Хранит список с инвертированными индексами

        public InvertedList()
        {
            _invertedList = new List<InvertedIndex>();
        }

        /// <summary>
        /// Добавляет первичный ключ для каждой части вторичного ключа, 
        /// разделенного символом пробела. 
        /// </summary>
        /// <param name="foreign">Вторичный ключ.</param>
        /// <param name="primary">Первичный ключ.</param>
        public void Add(string foreign, string primary)
        {
            // Разбиваем слово по пробелам
            foreach (var s in foreign.Split(' '))
                if (_invertedList.Find(s)) // Проверяем наличие ключа
                {
                    if (!_invertedList.Current.Indexes.Find(primary))
                        _invertedList.Current.Indexes.Add(primary);         // Добавляем еще индекс
                }
                else
                    _invertedList.Add(new InvertedIndex(s, primary));   // Добавляем, если нет
        }

        /// <summary>
        /// Удаляет первичный ключ для каждой части вторичного ключа, 
        /// разделенного символом пробела. При отсутствии первичных
        /// ключей удаляет сам вторичный ключ.
        /// </summary>
        /// <param name="foreign">Вторичный ключ.</param>
        /// <param name="primary">Первичный ключ.</param>
        public void Delete(string foreign, string primary)
        {
            // Разбиваем слово по пробела
            foreach (var s in foreign.Split(' '))
                if (_invertedList.Find(s)) // Проверяем наличие ключа
                {
                    _invertedList.Current.Indexes.Delete(primary);      // Удаляем связанный первичный ключ
                    if (_invertedList.Current.Indexes.Count == 0)   // Если список стал пустым
                        _invertedList.Delete(s);                    // Удаляем вторичный ключ
                }
        }

        /// <summary>
        /// Осуществляет поиск первичных ключей по вторичному ключу,
        /// разделив последний на части по символу пробела.
        /// Возвращает список первичных ключей.
        /// </summary>
        /// <param name="foreign">Вторичный ключ.</param>
        public List<string> Find(string foreign)
        {
            var list = new List<string>();          // Для вывод результата
            var indexs = new List<InvertedIndex>(); // Для хранения подходящих вторичных индексов
            string[] arr = foreign.Split(' ');          // Разбиение по словам
            InvertedIndex first;

            // Поиск первого вторичного ключа
            if (_invertedList.Find(arr[0]))
                first = _invertedList.Current; // Сохранение первого набора ключей
            else
                return null;    // Если не найден

            // Поиск оставшихся вторичных ключей
            foreach (var s in arr)
                if (_invertedList.Find(s))
                    indexs.Add(_invertedList.Current); // Копирование подходящих наборов ключей
                else
                    return null; // Если хотя бы один вторичный ключ не найден

            // Сравнение наличия ключей первого набора с остальными
            foreach (var f in first.Indexes)
            {
                bool l = true;  // Флаг о наличии ключа в других наборах
                // Просмотр всех наборов
                foreach (var i in indexs)
                    if (!i.Indexes.Find(f))
                    {
                        l = false;  // Если хотя в одном наборе не найден вторичный ключ
                        break;
                    }
                if (l)  // Добавляем только ключи найденные во всех наборах
                    list.Add(f);
            }

            return (list.Count > 0) ? list : null;
        }

        /// <summary>
        /// Очищает весь список инвертированных индексов.
        /// </summary>
        public void Clear()
        {
            foreach (var l in _invertedList)
                l.Indexes.Clear();
            _invertedList.Clear();
        }
    }
}
