using System;
using System.ComponentModel;
using SAOD_Kursovoy.Service;
using SAOD_Kursovoy.Model;
using SAOD_Kursovoy.Model.Data;
using ResFindF = System.Tuple<SAOD_Kursovoy.Model.Data.Flight, SAOD_Kursovoy.Model.List<System.Tuple<string, string>>>;
using ResFindA = System.Tuple<string, string, string, string>;

namespace SAOD_Kursovoy.ViewModel
{
    class FlightsViewModel : INotifyPropertyChanged
    {
        public event Action<Flight> AddFlight;
        public event Action<Flight> DeleteFlight;

        /// <summary>
        /// Содержит данные об авиарейсах в виде дерева.
        /// </summary>
        public AVLTree<Flight> Flights { get; set; }

        /// <summary>
        /// Содержит результат поиска авиарейса по № авиарейса.
        /// </summary>
        public ResFindF ResultFindByFlight { get; set; }

        /// <summary>
        /// Содержит результат поиска авиарейса по аэропорту прибытия.
        /// </summary>
        public List<ResFindA> ResultFindByAirport { get; set; }

        /// <summary>
        /// Для хранения текущей страницы поиска
        /// </summary>
        public object PageFind { get; set; }

        private string _current;
        /// <summary>
        /// Поле для связывания основной таблицы и результата запроса.
        /// </summary>
        public string Current
        {
            get { return _current; }
            set { _current = value; OnPropertyChanged("Current"); }
        }
        
        public FlightsViewModel()
        {
            Flights = new AVLTree<Flight>();
        }

        public Command<string> FindByFlight
        {
            get => new Command<string>((key) =>
            {
                var flight = Flights.Find(key);
                if (flight != null)
                {
                    var list = new List<Tuple<string, string>>();
                    var win = App.Current.MainWindow as View.MainWindow;
                    var tickets = (win.TicketsVM as TicketsViewModel).Tickets;
                    var passengers = (win.PassengersVM as PassengersViewModel).Passengers;
                    // Копирование информации по билетам
                    foreach (var el in tickets)
                        if (el.Passport != null && el.Flight == flight.Number)
                        {
                            var pas = passengers.Find(el.Passport);
                            if (pas != null)
                                list.Add(new Tuple<string, string>(pas.Passport, pas.FIO));
                        }
                    ResultFindByFlight = new ResFindF(flight, list);
                    Current = flight.Number;
                    OnPropertyChanged("Current");
                }
                else
                    ResultFindByFlight = null;
                OnPropertyChanged("ResultFindByFlight");
            }, (key) => key != "");
        }

        public Command<string> FindByAirport
        {
            get => new Command<string>((text) =>
            {
                var result = Algorithm.FindFlight(Flights, text);
                if (result != null)
                {
                    ResultFindByAirport = new List<ResFindA>();
                    // Просмотр результата поиска
                    foreach (var res in result)
                    {
                        var f = Flights.Find(res);
                        // Сохранение номера авиарейса, аэропорта прибытия, даты отправления, времени отправления
                        var r = new ResFindA(f.Number, f.ArrivalAirport, f.DeparturesDate, f.DeparturesTime);
                        ResultFindByAirport.Add(r);
                    }
                }
                else
                    ResultFindByAirport = null;
                OnPropertyChanged("ResultFindByAirport");
            }, (text) => text != "");
        }

        public Command Add
        {
            get => new Command(() =>
            {
                //System.Windows.MessageBox.Show("Добавить.");
                var flight = new Flight
                {
                    Number = "ABC-000", 
                    Airline = "Восток",
                    DeparturesAirport = "Москва",
                    ArrivalAirport = "Санкт-Петербург",                    
                    DeparturesDate = "01.06.2020",
                    DeparturesTime = "18:00",
                    NumberOfSeatsAll = 10,
                    NumberOfSeatsFree = 10
                }; 
                switch (Flights.Count)
                {
                    case 0:
                        flight.Number = "ABC-001";
                        Flights.Add(flight.Number, flight);
                        AddFlight(flight);
                        break;
                    case 1:
                        flight.Number = "ABC-002";
                        Flights.Add(flight.Number, flight);
                        AddFlight(flight);
                        break;
                    case 2:
                        flight.Number = "ABC-003";
                        flight.ArrivalAirport = "Тверь";
                        Flights.Add(flight.Number, flight);
                        AddFlight(flight);
                        break;
                    case 3:
                        flight.Number = "ABC-004";
                        Flights.Add(flight.Number, flight);
                        AddFlight(flight);
                        break;
                    case 4:
                        flight.Number = "ABC-005";
                        Flights.Add(flight.Number, flight);
                        AddFlight(flight);
                        break;
                    case 5:
                        flight.Number = "ABC-006";
                        Flights.Add(flight.Number, flight);
                        AddFlight(flight);
                        break;
                    case 6:
                        flight.Number = "ABC-007";
                        flight.ArrivalAirport = "Тверь";
                        Flights.Add(flight.Number, flight);
                        break;
                    case 7:
                        flight.Number = "ABC-008";
                        Flights.Add(flight.Number, flight);
                        break;
                    case 8:
                        flight.Number = "ABC-009";
                        Flights.Add(flight.Number, flight);
                        break;
                }
            });
        }

        public Command<int> Remove
        {
            get => new Command<int>((i) =>
            {
                //System.Windows.MessageBox.Show("Удалить.");
                Flights.Delete("ABC-003");
            });
        }

        public Command Clear
        {
            get => new Command(() =>
            {
                //System.Windows.MessageBox.Show("Очистить.");
                Flights.Clear();
            });
        }

        public Command<object> SetPageFind
        {
            get => new Command<object>((page) =>
            {
                PageFind = page;
                OnPropertyChanged("PageFind");
            });
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
