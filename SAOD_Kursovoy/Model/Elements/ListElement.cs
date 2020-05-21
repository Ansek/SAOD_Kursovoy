
namespace SAOD_Kursovoy.Model.Elements
{
    class ListElement<T>
    {
		private T _value;
		/// <summary>
		/// Значение для данного элемента списка.
		/// </summary>
		public T Value
		{
			get { return _value; }
			set { _value = value; }
		}

		private ListElement<T> _next;
		/// <summary>
		/// Ссылка на следующий элемент.
		/// </summary>
		public ref ListElement<T> Next { get => ref _next; }
	}
}
