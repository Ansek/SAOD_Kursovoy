using System.Windows;
using System.ComponentModel;
using SAOD_Kursovoy.Service;
using SAOD_Kursovoy.Model;
using SAOD_Kursovoy.Model.Data;
using ResFindP = System.Tuple<SAOD_Kursovoy.Model.Data.Passenger, SAOD_Kursovoy.Model.List<string>>;
using ResFindF = System.Tuple<string, string>;

namespace SAOD_Kursovoy.ViewModel
{
    /// <summary>
    /// Класс-посредник для пассажиров.
    /// </summary>
    class PassengersViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Хранит данные о пассажирах.
        /// </summary>
        public HashTable<Passenger> Passengers { get; set; }

        /// <summary>
        /// Хранит для каждого вторичного ключа список первичных.
        /// </summary>
        public InvertedList InvertedList { get; set; }

        /// <summary>
        /// Содержит результат поиска пассажира по номеру паспорта.
        /// </summary>
        public ResFindP ResultFindByPassport { get; set; }

        /// <summary>
        /// Содержит результат поиска пассажира по ФИО.
        /// </summary>
        public List<ResFindF> ResultFindByFIO { get; set; }

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

        public PassengersViewModel()
        {
            Passengers = new HashTable<Passenger>(Algorithm.GetHash);
            InvertedList = new InvertedList();
        }

        /// <summary>
        /// Команда поиска пассажира по номеру паспорта.
        /// </summary>
        public Command<string> FindByPassport
        {
            get => new Command<string>((key) =>
            {
                var passengers = Passengers.Find(key);
                if (passengers != null)
                {
                    var list = new List<string>();
                    var win = App.Current.MainWindow as View.MainWindow;
                    var tickets = (win.TicketsVM as TicketsViewModel).Tickets;
                    // Получение авиариейсов на которые пассажир купил билеты
                    foreach (var el in tickets)
                        if (el.Passport == passengers.Passport)
                            list.Add(el.Flight);
                    list.Sort();
                    ResultFindByPassport = new ResFindP(passengers, list);
                    Current = passengers.Passport;
                    OnPropertyChanged("Current");
                }
                else
                    ResultFindByPassport = null;
                OnPropertyChanged("ResultFindByPassport");
            }, (key) => key != "");
        }

        /// <summary>
        /// Команда поиска пассажира по ФИО.
        /// </summary>
        public Command<string> FindByFIO
        {
            get => new Command<string>((fio) =>
            {
                var result = InvertedList.Find(fio);
                if (result != null)
                {
                    ResultFindByFIO = new List<ResFindF>();
                    foreach (var res in result)
                    {
                        var p = Passengers.Find(res);
                        // Cохранение номера паспорта и фамилии
                        var r = new ResFindF(p.Passport, p.FIO);
                        ResultFindByFIO.Add(r);
                    }
                    ResultFindByFIO.Sort();
                }
                else
                    ResultFindByFIO = null;
                OnPropertyChanged("ResultFindByFIO");
            }, (fio) => fio != "");
        }

        /// <summary>
        /// Команда добавления данных о пассажире.
        /// </summary>
        public Command Add
        {
            get => new Command(() =>
            {
                var d = new View.InputPassenger();
                if (d.ShowDialog() == true)
                {
                    var p = d.DataContext as Passenger;
                    Passengers.Add(p.Passport, p);
                    InvertedList.Add(p.FIO, p.Passport);
                }
            });
        }

        /// <summary>
        /// Команда удаления данных о пассажире.
        /// </summary>
        public Command Remove
        {
            get => new Command(() =>
            {
                if (MessageBox.Show($"Вы действительно хотите удалить пассажира с номером паспорта {Current}?",
                    "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var pas = Passengers.Find(Current);
                    Passengers.Delete(pas.Passport);
                    InvertedList.Delete(pas.FIO, pas.Passport);
                }
            }, () => Current != null);
        }

        /// <summary>
        /// Команда очистки данных о пассажире.
        /// </summary>
        public Command Clear
        {
            get => new Command(() =>
            {
                if (MessageBox.Show($"Вы действительно хотите полностью очистить список?",
                    "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    Passengers.Clear();
                    InvertedList.Clear();
                }                    
            }, () => Passengers.Count > 0);
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
