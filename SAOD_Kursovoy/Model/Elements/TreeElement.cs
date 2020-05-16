
namespace SAOD_Kursovoy.Model.Elements
{
    class TreeElement<T>
    {
        /// <summary>
        /// Определяет левый узел.
        /// </summary>
        public TreeElement<T> Left { get; set; }

        /// <summary>
        /// Определяет правый узел.
        /// </summary>
        public TreeElement<T> Right { get; set; }

        /// <summary>
        /// Ключевое значение узла.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Значение для данного узла.
        /// </summary>
        public T Value { get; set; }

        private uint _height;
        /// <summary>
        /// Возвращает высоту поддерева.
        /// </summary>
        public uint Height { get => _height; }

        /// <summary>
        /// Вычисляет баланс данного поддерева. 
        /// </summary>
        public uint BalanceFactor
        {
            get
            {
                // Получение высот левых и правых поддеревьев
                uint heightL = Left != null ? Left.Height : 0;
                uint heightR = Right != null ? Right.Height : 0;
                // Вычисление баланса
                return heightR - heightL;
            }
        }
        
        /// <summary>
        /// Обновление данных о высоте для данного поддерева.
        /// </summary>
        public void RefreshHeight()
        {
            // Получение высот левых и правых поддеревьев
            uint heightL = Left != null ? Left.Height : 0;
            uint heightR = Right != null ? Right.Height : 0;
            // Определенение новой высоты
            _height = ((heightL > heightR) ? heightL : heightR) + 1;
        }
    }
}
