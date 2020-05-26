using System.Windows;
using System.ComponentModel;
using SAOD_Kursovoy.Service;
using SAOD_Kursovoy.Model;
using SAOD_Kursovoy.Model.Data;
using ResFindP = System.Tuple<SAOD_Kursovoy.Model.Data.Passenger, SAOD_Kursovoy.Model.List<string>>;
using ResFindF = System.Tuple<string, string>;

namespace SAOD_Kursovoy.ViewModel
{
    class PassengersViewModel : INotifyPropertyChanged
    {
        public HashTable<Passenger> Passengers { get; set; }
        private InvertedList _list;

        /// <summary>
        /// Содержит результат поиска пассажира по № пасспорта.
        /// </summary>
        public ResFindP ResultFindByPassport { get; set; }

        /// <summary>
        /// Содержит результат поиска пассажира по ФИО.
        /// </summary>
        public List<ResFindF> ResultFindByFIO { get; set; }

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

        public PassengersViewModel()
        {
            Passengers = new HashTable<Passenger>(Algorithm.GetHash);
            _list = new InvertedList();
        }

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
                    foreach (var el in tickets)
                        if (el.Passport == passengers.Passport)
                            list.Add(el.Flight);
                    ResultFindByPassport = new ResFindP(passengers, list);
                    Current = passengers.Passport;
                    OnPropertyChanged("Current");
                }
                else
                    ResultFindByPassport = null;
                OnPropertyChanged("ResultFindByPassport");
            }, (key) => key != "");
        }

        public Command<string> FindByFIO
        {
            get => new Command<string>((fio) =>
            {
                var result = _list.Find(fio);
                if (result != null)
                {
                    ResultFindByFIO = new List<ResFindF>();
                    foreach (var res in result)
                    {
                        var p = Passengers.Find(res);
                        // Cохранение номера пасспорта и фамилии
                        var r = new ResFindF(p.Passport, p.FIO);
                        ResultFindByFIO.Add(r);
                    }
                }
                else
                    ResultFindByFIO = null;
                OnPropertyChanged("ResultFindByFIO");
            }, (fio) => fio != "");
        }

        public Command Add
        {
            get => new Command(() =>
            {
                var d = new View.InputPassenger();
                if (d.ShowDialog() == true)
                {
                    var p = d.DataContext as Passenger;
                    Passengers.Add(p.Passport, p);
                    _list.Add(p.FIO, p.Passport);
                }
            });
        }

        public Command Remove
        {
            get => new Command(() =>
            {
                if (MessageBox.Show($"Вы действительно хотите удалить пассажира с номером паспорта {Current}?",
                    "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var pas = Passengers.Find(Current);
                    Passengers.Delete(pas.Passport);
                    _list.Delete(pas.FIO, pas.Passport);
                }
            }, () => Current != null);
        }

        public Command Clear
        {
            get => new Command(() =>
            {
                if (MessageBox.Show($"Вы действительно хотите полностью очистить список?",
                    "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    Passengers.Clear();
                    _list.Clear();
                }                    
            }, () => Passengers.Count > 0);
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
