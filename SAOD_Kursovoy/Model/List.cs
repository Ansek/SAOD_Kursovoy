using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using SAOD_Kursovoy.Service;
using SAOD_Kursovoy.Model.Elements;

namespace SAOD_Kursovoy.Model
{
    class List<T> : IEnumerable<T>, INotifyCollectionChanged
	{
		private ListElement<T> _current; // Текущий просматриваемый элемент

		/// <summary>
		/// Возвращает текущее просматриваемое значение.
		/// </summary>
		public T Current => (_current != null) ? _current.Value : default(T);

		private uint _count = 0;
		/// <summary>
		/// Определяет количество элементов в списке.
		/// </summary>
		public uint Count
		{
			get { return _count; }
			set { _count = value; }
		}

		/// <summary>
		/// Пролистывает список в поисках записи. Возвращает true - если элемент найден.
		/// Останавливает _current перед искомым элементом.
		/// </summary>
		/// <param name="value">Искомое значение.</param>
		private bool PrevFind(T value)
		{
			if (_current != null)
			{
				var p = _current.Value; // Сохранение места начала поиска
				do
				{   // Сообщаем если следующий элемент является искомым
					if (value.Equals(_current.Next.Value))
						return true;
					_current = _current.Next; // Переход к следующей записи
				}
				while (!p.Equals(_current.Next.Value)); // Проходим, пока не вернемся к началу
				// Сообщаем если следующий элемент является искомым
				if (value.Equals(_current.Next.Value))
				return true;
			}
			return false;
		}

		/// <summary>
		/// Пролистывает список в поисках записи. Возвращает true - если элемент найден. 
		/// Останавливает _current на искомом элементе.
		/// </summary>
		/// <param name="value">Искомое значение.</param>
		public bool Find(T value)
		{
			bool res = false;
			if (_current != null)
			{
				if (value.Equals(_current.Value)) // Если текущий элемент является искомым 
					res = true;
				else
				{
					res = PrevFind(value);		// Поиск элемента
					_current = _current.Next;	// Сделать найденный элемент текущим
				}
			}
			return res;
		}

		/// <summary>
		/// Добавляет элемент в список. 
		/// </summary>
		/// <param name="value">Добавляемое значение.</param>
		public void Add(T value)
		{
			// Если элемент найден
			if (Find(value))
				throw new Exception("Ошибка при добавлении! Такой элемент уже есть в списке!");

			// Если список пуст
			if (_current == null)
			{
				_current = new ListElement<T>();
				_current.Value = value;
				_current.Next = _current; // Зацикливание списка
			}
			else // Иначе создаем элемент после текущего
			{
				// Создание нового элемента
				var p = _current.Next;
				_current.Next = new ListElement<T>();
				_current = _current.Next;
				// Заполнение 
				_current.Value = value;
				_current.Next = p; 
			}

			Log.Add($"Добавлен объект \"{value}\".");
			_count++;   // Увеличение количества

			// Оповещение об изменении коллекции
			OnCollectionChanged();
		}

		/// <summary>
		/// Удаляет элемент из списка. 
		/// </summary>
		/// <param name="value">Удаляемое значение.</param>
		public void Delete(T value)
		{
			//Если элемент не найден.
			if (!PrevFind(value))
				throw new Exception("Ошибка при удалении! Элемент с таким значением не найден!");

			// Если элемент является единственными в списке
			if (_current.Value.Equals(_current.Next.Value))
			{
				_current.Value = default(T); // Очистка значений
				_current.Next = null;
				_current = null;
			}
			else
			{
				var p = _current.Next;	// Искомый элемент
				_current.Next = p.Next; // Зацикливание с другим элементом
				p.Value = default(T);   // Очистка значений
				p.Next = null;				
			}

			Log.Add($"Удален объект \"{value}\".");
			_count--;   // Уменьшение количества

			// Оповещение об изменении коллекции
			OnCollectionChanged();
		}

		/// <summary>
		/// Очистка содержимого списка.
		/// </summary>
		public void Clear()
		{
			while (_current != null)
			{
				var p = _current.Next;
				_current.Value = default(T); // Очистка значений
				_current.Next = null;
				_current = p;	// Переход к следующей записи
			}
			_count = 0; // Обнуление количества

			// Сохранение сообщения в журнале
			Log.Add($"Список очищен.");

			// Оповещение об изменении коллекции
			OnCollectionChanged();
		}

		// Просмотреть элементы и найти максимум
		// Поменять его с последним из неотсортированным
		public void Sort()
		{
			if (_current != null)
			{
				if (!(_current.Value is IComparable))
					throw new Exception("Ошибка при сортировке! Элемент не реализует IComparable!");

				var end = _current;
				var max = _current;
				while (_current.Next != end)
				{
					var p = _current.Next;

					// Поиск максимального значения
					IComparable c;
					while (p.Next != end)
					{
						c = p.Value as IComparable;
						if (c.CompareTo(max.Value) > 0)
							max = p;
						p = p.Next; 
					}
					// Дополнительная проверка для предпоследнего элемента
					c = p.Value as IComparable;
					if (c.CompareTo(max.Value) > 0)
						max = p;
					// Перестановка элементов
					var temp = max.Value;
					max.Value = end.Value;
					end.Value = temp;
					// Сужение поиска
					end = p;
					max = p;
				}
				_current = end; // Установка наименьшего значения как текущего
			}
		}

		/// <summary>
		/// Возвращает перечислитель для списка. 
		/// </summary>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new ListEnumerator<T>(_current);
		}

		/// <summary>
		/// Возвращает перечислитель для списка. 
		/// </summary>
		public IEnumerator<T> GetEnumerator()
		{
			return new ListEnumerator<T>(_current);
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
	/// Перечеслитель для элементов списка.
	/// </summary>
	class ListEnumerator<T> : IEnumerator<T>
	{
		private ListElement<T> _current; // Текущий просматриваемый элемент
		private T _p;	// Первое значение, для остановки просмотра списка
		bool is_begin = false; // Флаг начала перебора

		// Конструктор для получения ссылки на текущий элемент
		public ListEnumerator(ListElement<T> current)
		{
			_current = current;
		}

		public T Current => _current.Value; // Текущий элемент

		object IEnumerator.Current => _current.Value; // Текущий элемент

		// Перемещает перечислитель к следующему элементу коллекции
		public bool MoveNext()
		{
			if (_current != null)
			{
				if (is_begin)
				{
					_current = _current.Next; // Переход к следующему элементу
					if (_p.Equals(_current.Value))
						return false;	// Завершаем перебор, если достигли текущего
				}
				else
				{
					is_begin = true; // Чтобы список начинался с текущего элемента
					_p = _current.Value; // Сохраняем первый для остановки цикла
				}
				return true;
			}
			return false;
		}

		// Установка перечислителя в начальное положение
		public void Reset()
		{
			is_begin = false;
		}

		public void Dispose()
		{
		}
	}
}
