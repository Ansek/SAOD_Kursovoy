using System;

namespace SAOD_Kursovoy.Model.Elements
{
    /// <summary>
    /// Хранит данные об узле дерева.
    /// </summary>
    class TreeElement<T>
    {
        private TreeElement<T> _left;
        /// <summary>
        /// Определяет левый узел.
        /// </summary>
        public ref TreeElement<T> Left { get => ref _left; }

        private TreeElement<T> _right;
        /// <summary>
        /// Определяет правый узел.
        /// </summary>
        public ref TreeElement<T> Right { get => ref _right; }

        /// <summary>
        /// Ключевое значение узла.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Значение для данного узла.
        /// </summary>
        public T Value { get; set; }

        private int _height;
        /// <summary>
        /// Возвращает высоту поддерева.
        /// </summary>
        public int Height { get => _height; }

        /// <summary>
        /// Узел дерева. 
        /// </summary>
        /// <param name="key">Ключевое значение элемента.</param>
        /// <param name="value">Определяет значение, которое хранит элемент.</param>
        public TreeElement(string key, T value)
        {
            Key = key;
            Value = value;
            _height = 1;
        }

        /// <summary>
        /// Вычисляет баланс данного поддерева. 
        /// </summary>
        public int BalanceFactor
        {
            get
            {
                // Получение высот левых и правых поддеревьев
                int heightL = _left != null ? _left.Height : 0;
                int heightR = _right != null ? _right.Height : 0;
                // Вычисление баланса
                return Convert.ToInt32(heightR - heightL);
            }
        }
        
        /// <summary>
        /// Обновление данных о высоте для данного поддерева.
        /// </summary>
        public void RefreshHeight()
        {
            // Получение высот левых и правых поддеревьев
            int heightL = _left != null ? _left.Height : 0;
            int heightR = _right != null ? _right.Height : 0;
            // Определенение новой высоты
            _height = ((heightL > heightR) ? heightL : heightR) + 1;
        }
    }
}
