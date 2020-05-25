using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using SAOD_Kursovoy.Service;
using SAOD_Kursovoy.Model.Elements;

namespace SAOD_Kursovoy.Model
{
    /// <summary>
    /// АВЛ-дерево.
    /// </summary>
    class AVLTree<T> : IEnumerable<T>, INotifyCollectionChanged
    {
        private TreeElement<T> _root;   // Корень дерева

        private uint _count = 0;
        /// <summary>
        /// Определяет количество элементов в дереве.
        /// </summary>
        public uint Count
        {
            get { return _count; }
            set { _count = value; }
        }

        /// <summary>
        /// Вращение поддерева влево.
        /// </summary>
        /// <param name="a">Ссылка на узел.</param>
        private void RotateLeft(ref TreeElement<T> a)
        {
            var b = a.Right;    // Подъем правого узла вверх
            a.Right = b.Left;
            b.Left = a;
            a.RefreshHeight();  // Обновление высот
            b.RefreshHeight();
            a = b;
        }

        /// <summary>
        /// Вращение поддерева вправо.
        /// </summary>
        /// <param name="a">Ссылка на узел.</param>
        private void RotateRight(ref TreeElement<T> a)
        {
            var b = a.Left;     // Подъем левого узла вверх
            a.Left = b.Right;
            b.Right = a;
            a.RefreshHeight();  // Обновление высот
            b.RefreshHeight();
            a = b;
        }

        /// <summary>
        /// Балансировка дерева.
        /// </summary>
        /// <param name="path">Путь пройденный от корня.</param>
        private void BalanceTree(Stack<TreeElement<T>> path)
        {
            while (path.Count != 0)
            {
                // Балансировка для текущего элемента
                var node = path.Pop();
                node.RefreshHeight(); // Обновление высоты

                //Если перекос влево
                if (node.BalanceFactor == -2)
                {
                    if (node.Left.BalanceFactor == 1)
                        RotateLeft(ref node.Left);
                    RotateRight(ref node);
                }

                //Если перекос вправо
                if (node.BalanceFactor == 2)
                {
                    if (node.Right.BalanceFactor == -1)
                        RotateRight(ref node.Right);
                    RotateLeft(ref node);
                }

                // Сохранение изменений для родительского элемента
                if (path.Count == 0)
                    _root = node;
                else if (path.Peek().Key.CompareTo(node.Key) > 0)
                    path.Peek().Left = node;
                else
                    path.Peek().Right = node;
            }
        }

        /// <summary>
        /// Возвращает путь, пройденный для поиска значения. 
        /// </summary>
        /// <param name="key">Ключевое значение узла.</param>
        private Stack<TreeElement<T>> FindPath(string key)
        {
            var path = new Stack<TreeElement<T>>();
            // Просмотр элементов
            var node = _root;
            while (node != null)
            {
                path.Push(node); // Сохранение узла

                // Поиск
                if (node.Key == key)
                    break;
                else    // Выбор нового узла
                    node = (node.Key.CompareTo(key) > 0) ? node.Left : node.Right;
            }
            return path;
        }

        /// <summary>
        /// Ищет значение узла по ключу. 
        /// </summary>
        /// <param name="key">Ключевое значение узла.</param>
        public T Find(string key)
        {
            var node = FindPath(key)?.Peek();                   // Поиск пути
            return (node != null) ? node.Value : default(T);    // Возвращение значения
        }

        /// <summary>
        /// Добавляет элемент в дерево, в случае если его там нет.
        /// </summary>
        /// <param name="key">Ключевое значение узла.</param>
        /// <param name="value">Добавляемое значение узла.</param>
        public void Add(string key, T value)
        {
            if (_root == null)
                _root = new TreeElement<T>(key, value); // Добавление корневого узла
            else
            {
                var path = FindPath(key);   // Поиск элементов
                var node = path.Peek();     // Получение текущего элемента

                // Создание нового элемента
                if (node.Key == key)
                    throw new Exception("Ошибка при добавлении! Элемент с таким ключом уже находится в дереве!");
                else if (node.Key.CompareTo(key) > 0)
                    node.Left = new TreeElement<T>(key, value);
                else
                    node.Right = new TreeElement<T>(key, value);

                BalanceTree(path); // Балансировка дерева
            }
            Log.Add($"Добавлен объект \"{value}\".\nЗначение ключа: {key}.");
            _count++;   // Увеличение количества

            // Оповещение об изменении коллекции
            OnCollectionChanged();
        }

        /// <summary>
        /// Удаление элемент из дерева.
        /// </summary>
        /// <param name="key">Ключевое значение узла.</param>
        public void Delete(string key)
        {
            var path = FindPath(key);   // Поиск элементов
            // Если элемент не найден
            if (path.Count == 0)
                throw new Exception("Ошибка при удалении! Элемент с таким ключом не найден!");
            // Если элемент является корнем и больше нет узлов
            else if (path.Count == 1 && _root.Height == 1)
            {
                Log.Add($"Удален объект \"{_root.Value}\".\nЗначение ключа: {key}.");
                _root = null;
            }
            else
            {
                var node = path.Pop();
                TreeElement<T> newNode = null;

                // Если узел является поддеревом
                if (node.Height > 1)
                {
                    // Если левого узла нет
                    if (node.Left == null)
                        newNode = node.Right; // Заменяем правой частью
                    // Если у левого узла нет правого
                    else if (node.Left.Right == null)
                    {
                        newNode = node.Left;        // Заменяем левой частью
                        newNode.Right = node.Right; // Переносим правую ветвь
                    }
                    else
                    {
                        // Идем к левому узлу, а потом к крайнему правому листу
                        var temp = node.Left;
                        var parent = node.Left;
                        while (temp.Right != null)
                        {
                            parent = temp;
                            temp = temp.Right;
                        }
                        newNode = node;         // Копирование поддерева
                        newNode.Key = temp.Key; // Замена ключа
                        // Удаление листа
                        if (temp.Left == null)
                            parent.Right = null;
                        else
                            parent.Right = temp.Left;
                    }
                }

                if (node != _root) // Проверка, что у элемента есть родители
                {
                    // Заменяем новый узел в родительском элементе
                    if (path.Peek().Key.CompareTo(node.Key) > 0)
                        path.Peek().Left = newNode;
                    else
                        path.Peek().Right = newNode;
                }
                if (newNode != null)
                    path.Push(newNode); // Добавление для проверки

                BalanceTree(path); // Балансировка дерева
                Log.Add($"Удален объект \"{node.Value}\".\nЗначение ключа: {key}.");
                _count--;   // Уменьшение количества

                // Оповещение об изменении коллекции
                OnCollectionChanged();
            }
        }

        /// <summary>
        /// Очищает дерево.
        /// </summary>
        public void Clear()
        {
            //Удаляем, пока не будет пуст корень
            while (_root != null)
                Delete(_root.Key);
            _count = 0; // Обнуление количества

            // Сохранение сообщения в журнале
            Log.Add($"Список дерева очищен.");

            // Оповещение об изменении коллекции
            OnCollectionChanged();
        }
        
        /// <summary>
        /// Рекурсивный обратный обход дерева.
        /// </summary>
        /// <param name="node">Текущий узел дерева.</param>
        public IEnumerable<T> LastOrder(TreeElement<T> node)
        {
            if (node != null) // Перебор значений
            {
                foreach (var n in LastOrder(node.Left))
                    yield return n;                
                foreach (var n in LastOrder(node.Right))
                    yield return n;
                yield return node.Value;
            }
        }

        /// <summary>
        /// Возвращает перечислитель для АВЛ-дерева. 
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var node in LastOrder(_root))
                yield return node;
        }

        /// <summary>
        /// Возвращает перечислитель для АВЛ-дерева. 
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            foreach (var node in LastOrder(_root))
                yield return node;
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
}