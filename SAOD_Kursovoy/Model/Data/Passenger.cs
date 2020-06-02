using System.ComponentModel;
using System.Text.RegularExpressions;

namespace SAOD_Kursovoy.Model.Data
{
	/// <summary>
	/// Для хранения данных о пассажире.
	/// </summary>
    class Passenger : INotifyPropertyChanged, IDataErrorInfo
	{
		private string _passport;
		/// <summary>
		/// Значение паспорта в формате "NNNN-NNNNNN".
		/// </summary>
		public string Passport
		{
			get { return _passport; }
			set { _passport = value; OnPropertyChanged("Passport"); }
		}

		private string _placeAndDate;
		/// <summary>
		/// Значение места и даты выдачи паспорта. 
		/// </summary>
		public string PlaceAndDate
		{
			get { return _placeAndDate; }
			set { _placeAndDate = value; OnPropertyChanged("PlaceAndDate"); }
		}

		private string _fio;
		/// <summary>
		/// Значение фамилии, имени и отчества. 
		/// </summary>
		public string FIO
		{
			get { return _fio; }
			set { _fio = value; OnPropertyChanged("FIO"); }
		}

		private string _birthday;
		/// <summary>
		/// Значение даты рождения.
		/// </summary>
		public string Birthday
		{
			get { return _birthday; }
			set { _birthday = value; OnPropertyChanged("Birthday"); }
		}

		/// <summary>
		/// Возвращает данные о пассажире.
		/// </summary>
		public override string ToString()
		{
			return $"пассажир: {_fio}; паспорт: {_passport}; выдан: {_placeAndDate}; дата рождения: {_birthday}";
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

		/// <summary>
		/// Проверяет все поля не пустые значения.
		/// </summary>
		public bool IsFieldsNotNull => _passport != null && _placeAndDate != null &&
			_fio != null && _birthday != null;

		// Возвращает сообщение об ошибке
		public string Error { get; set; }

		// Проводит проверку на наличие ошибок в данных
		public string this[string columnName]
		{
			get
			{
				Error = string.Empty;
				Regex regex;
				switch (columnName)
				{
					case "Passport":
						regex = new Regex("[0-9]{4}\\-[0-9]{6}$");
						if (!regex.IsMatch(_passport))
							Error = "Номер пасспорта должен иметь формат 'NNNN-NNNNNN'.\n" +
								"Где N - цифра.";
						break;
					case "PlaceAndDate":
						if (_placeAndDate == "")
							Error = "Поле места и даты выдачи не должно быть пустым.";
						break;
					case "FIO":
						regex = new Regex("[А-Я][a-я]+\\s[А-Я][a-я]+\\s[А-Я][a-я]+$");
						if (!regex.IsMatch(_fio))
							Error = "Поле должно соответствовать образцу 'Фамилия Имя Отчество'";
						break;
					case "Birthday":
						regex = new Regex("[0-3][0-9]\\.[0-1][0-9]\\.[0-9]{4}$");
						if (!regex.IsMatch(_birthday))
							Error = "Поле даты рождения должно быть в формате 'dd.mm.yyyy'.";
						break;
				}
				return Error;
			}
		}
	}
}
