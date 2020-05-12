using System.ComponentModel;

namespace SAOD_Kursovoy.Model.Elements
{
    class HashElements<T> : INotifyPropertyChanged
    {
		private int _hash;
		/// <summary>
		/// Хеш-значение. 
		/// </summary>
		public int Hash
		{
			get { return _hash; }
			set { _hash = value; }
		}

		private string _key;
		/// <summary>
		/// Ключевое значения для хеш-функции.
		/// </summary>
		public string Key
		{
			get { return _key; }
			set { _key = value; }
		}

		private bool _isDelete;
		/// <summary>
		/// Определяет помечен ли элемент на удалание.
		/// </summary>
		public bool IsDelete
		{
			get { return _isDelete; }
			set { _isDelete = value; }
		}

		private string _value;

		/// <summary>
		/// Определяет значение, котороые хранит элемент.
		/// </summary>
		public string Value
		{
			get { return _value; }
			set { _value = value; }
		}

		public event PropertyChangedEventHandler PropertyChanged;
		/// <summary>
		/// Оповещает об изменении значения свойства.
		/// </summary>
		/// <param name="name">Имя свойства.</param>
		public void OnPropertyChanged(string name)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
	}
}
