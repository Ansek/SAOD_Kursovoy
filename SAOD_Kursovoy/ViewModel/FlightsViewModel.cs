using System;
using System.Windows;
using System.ComponentModel;
using SAOD_Kursovoy.Service;
using SAOD_Kursovoy.Model;
using SAOD_Kursovoy.Model.Data;
using ResFindF = System.Tuple<SAOD_Kursovoy.Model.Data.Flight, SAOD_Kursovoy.Model.List<System.Tuple<string, string>>>;
using ResFindA = System.Tuple<string, string, string, string>;

namespace SAOD_Kursovoy.ViewModel
{
    /// <summary>
    /// Класс-посредник для авиарейсов.
    /// </summary>
    class FlightsViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Оповещает о добавлении авиарейса.
        /// </summary>
        public event Action<Flight> AddFlight;

        /// <summary>
        /// Оповещает об удалении авиарейса.
        /// </summary>
        public event Action<string> DeleteFlight;

        /// <summary>
        /// Оповещает об очистке данных авиарейсов.
        /// </summary>
        public event Action ClearFlight;

        /// <summary>
        /// Содержит данные об авиарейсах в виде дерева.
        /// </summary>
        public AVLTree<Flight> Flights { get; set; }

        /// <summary>
        /// Содержит результат поиска авиарейса по номеру авиарейса.
        /// </summary>
        public ResFindF ResultFindByFlight { get; set; }

        /// <summary>
        /// Содержит результат поиска авиарейса по аэропорту прибытия.
        /// </summary>
        public List<ResFindA> ResultFindByAirport { get; set; }

        /// <summary>
        /// Для хранения текущей страницы поиска.
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

        /// <summary>
        /// Команда поиска авиарейса по номеру авиарейса. 
        /// </summary>
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
                    list.Sort();
                    ResultFindByFlight = new ResFindF(flight, list);
                    Current = flight.Number;
                    OnPropertyChanged("Current");
                }
                else
                    ResultFindByFlight = null;
                OnPropertyChanged("ResultFindByFlight");
            }, (key) => key != "");
        }

        /// <summary>
        /// Команда поиска авиарейса по аэропорту прибытия.
        /// </summary>
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
                    ResultFindByAirport.Sort();
                }
                else
                    ResultFindByAirport = null;
                OnPropertyChanged("ResultFindByAirport");
            }, (text) => text != "");
        }

        /// <summary>
        /// Команда добавления данных об авиарейсе.
        /// </summary>
        public Command Add
        {
            get => new Command(() =>
            {
                var d = new View.InputFlight();
                if (d.ShowDialog() == true)
                {
                    var f = d.DataContext as Flight;
                    f.NumberOfSeatsFree = f.NumberOfSeatsAll;
                    Flights.Add(f.Number, f);
                    AddFlight(f);
                }
            });
        }

        /// <summary>
        /// Команда удаления данных об авиарейсе.
        /// </summary>
        public Command Remove
        {
            get => new Command(() =>
            {
                if (MessageBox.Show($"Вы действительно хотите удалить авиарейс с номером {Current}?",
                    "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    DeleteFlight(Current);
                    Flights.Delete(Current);
                }
            }, () => Current != null);
        }

        /// <summary>
        /// Команда очистки данных об авиарейсах.
        /// </summary>
        public Command Clear
        {
            get => new Command(() =>
            {
                if (MessageBox.Show($"Вы действительно хотите полностью очистить список?",
                    "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    Flights.Clear();
                    ClearFlight();
                }                    
            }, () => Flights.Count > 0);
        }

        /// <summary>
        /// Команда открытия боковой вкладки поиска.
        /// </summary>
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
