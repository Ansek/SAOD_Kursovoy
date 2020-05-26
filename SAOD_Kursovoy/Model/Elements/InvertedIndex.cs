using System;

namespace SAOD_Kursovoy.Model.Elements
{
    class InvertedIndex : IComparable
    {
		private string _key;
		/// <summary>
		/// Значение вторичного ключа.
		/// </summary>
		public string Key
		{
			get { return _key; }
			set { _key = value; }
		}

		private List<string> _indexes;
		/// <summary>
		/// Содержит список индексов (первичных ключей).
		/// </summary>
		public List<string> Indexes
		{
			get { return _indexes; }
			set { _indexes = value; }
		}
		
		/// <summary>
		/// Инвертированнанный индекс.
		/// </summary>
		/// <param name="key">Вторичные ключ.</param>
		public InvertedIndex(string key)
		{
			_key = key;
			_indexes = new List<string>();
		}

		/// <summary>
		/// Инвертированнанный индекс.
		/// </summary>
		/// <param name="key">Вторичные ключ.</param>
		/// <param name="index">Первичный ключ.</param>
		public InvertedIndex(string key, string index)
		{
			_key = key;
			_indexes = new List<string>() { index };
		}

		// Для сравнения в списке
		public int CompareTo(object obj)
		{
			var i = obj as InvertedIndex;
			return _key.CompareTo(i._key);
		}

		// Переопределение функции равенства 
		public override bool Equals(object obj)
		{
			InvertedIndex a = this;
			InvertedIndex b = obj as InvertedIndex;
			if (a == null || b == null)
				return false;
			return a._key == b._key;
		}

		// Неявное преобразование строковых значений
		public static implicit operator InvertedIndex(string s) => new InvertedIndex(s);
	}
}
