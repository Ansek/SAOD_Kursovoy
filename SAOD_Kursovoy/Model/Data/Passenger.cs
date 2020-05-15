using System.ComponentModel;

namespace SAOD_Kursovoy.Model.Data
{
    class Passenger : INotifyPropertyChanged
    {
		private string _passport;
		/// <summary>
		/// Значение паспорта в формате NNNN-NNNNNN.
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
		/// Возвращет данные о пассажире.
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
	}
}
