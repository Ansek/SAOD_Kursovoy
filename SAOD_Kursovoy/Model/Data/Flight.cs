using System.ComponentModel;

namespace SAOD_Kursovoy.Model.Data
{
    class Flight : INotifyPropertyChanged
    {
		private string _number;
		/// <summary>
		/// Значение номера авиарейса в формате "AAA-NNN".
		/// </summary>
		public string Number
		{
			get { return _number; }
			set { _number = value; OnPropertyChanged("Number"); }
		}

		private string _airline;
		/// <summary>
		/// Значение авиакомпании. 
		/// </summary>
		public string Airline
		{
			get { return _airline; }
			set { _airline = value; OnPropertyChanged("Airline"); }
		}

		private string _departuresAirport;
		/// <summary>
		/// Значение аэропорта отправления.
		/// </summary>
		public string DeparturesAirport
		{
			get { return _departuresAirport; }
			set { _departuresAirport = value; OnPropertyChanged("DeparturesAirport"); }
		}

		private string _arrivalAirport;
		/// <summary>
		/// Значение аэропорта прибытия.
		/// </summary>
		public string ArrivalAirport
		{
			get { return _arrivalAirport; }
			set { _arrivalAirport = value; OnPropertyChanged("ArrivalAirport"); }
		}

		private string _departuresDate;
		/// <summary>
		/// Значение даты отправления.
		/// </summary>
		public string DeparturesDate
		{
			get { return _departuresDate; }
			set { _departuresDate = value; OnPropertyChanged("DeparturesDate"); }
		}

		private string _departuresTime;
		/// <summary>
		/// Значение времени отправления.
		/// </summary>
		public string DeparturesTime
		{
			get { return _departuresTime; }
			set { _departuresTime = value; OnPropertyChanged("DeparturesTime"); }
		}

		private uint _numberOfSeatsAll;
		/// <summary>
		/// Значение количества мест всего
		/// </summary>
		public uint NumberOfSeatsAll
		{
			get { return _numberOfSeatsAll; }
			set { _numberOfSeatsAll = value; OnPropertyChanged("NumberOfSeatsAll"); }
		}

		private uint _numberOfSeatsFree;
		/// <summary>
		/// Значение количества мест свободно
		/// </summary>
		public uint NumberOfSeatsFree
		{
			get { return _numberOfSeatsFree; }
			set { _numberOfSeatsFree = value; OnPropertyChanged("NumberOfSeatsFree"); }
		}
		
		/// <summary>
		/// Возвращет данные о авиарейсе.
		/// </summary>
		public override string ToString()
		{
			return $"номер: {_number}; авиакомпания: {_airline}; аэропорт отправления {_departuresAirport}; " + 
				$"аэропорт прибытия: {_arrivalAirport}; дата отправления: {_departuresDate}; " +
				$"время отправления: {_departuresTime}; мест всего: {_numberOfSeatsAll}; мест свободно: {_numberOfSeatsFree}.";
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
