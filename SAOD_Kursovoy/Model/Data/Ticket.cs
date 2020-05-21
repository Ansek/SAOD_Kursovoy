using System.ComponentModel;

namespace SAOD_Kursovoy.Model.Data
{
    class Ticket : INotifyPropertyChanged
	{
		private string _number;
		/// <summary>
		/// Значение номера билета в формате "NNNNNNNNN".
		/// </summary>
		public string Number
		{
			get { return _number; }
			set { _number = value; OnPropertyChanged("Number"); }
		}

		private string _flight;
		/// <summary>
		/// Значение номера авиарейса в формате "AAA-NNN". 
		/// </summary>
		public string Flight
		{
			get { return _flight; }
			set { _flight = value; OnPropertyChanged("Flight"); }
		}

		private string _passport;
		/// <summary>
		/// Значение паспорта в формате NNNN-NNNNNN.
		/// </summary>
		public string Passport
		{
			get { return _passport; }
			set { _passport = value; OnPropertyChanged("Passport"); }
		}

		/// <summary>
		/// Возвращет данные о билете.
		/// </summary>
		public override string ToString()
		{
			return $"№ авиабилета: {_number}; № авиарейса: {_flight}; № паспорта  {_passport}.";
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
