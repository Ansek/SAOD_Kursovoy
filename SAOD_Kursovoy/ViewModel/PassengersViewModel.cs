using SAOD_Kursovoy.Service;
using SAOD_Kursovoy.Model;
using SAOD_Kursovoy.Model.Data;
using ResFindP = System.Tuple<SAOD_Kursovoy.Model.Data.Passenger, SAOD_Kursovoy.Model.List<string>>;
using ResFindF = System.Tuple<string, string>;
using System.ComponentModel;

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
                //System.Windows.MessageBox.Show("Зарегистрировать.");
                switch (Passengers.Count)
                {
                    case 0:
                        Passengers.Add("4007-395943", new Passenger
                        {
                            Passport = "4007-395943",
                            PlaceAndDate = "Место 21.06.2002",
                            FIO = "Иванов Иван Иванович",
                            Birthday = "20.03.1970"
                        });
                        _list.Add("Иванов Иван Иванович", "4007-395943");
                        break;
                    case 1:
                        Passengers.Add("4009-392042", new Passenger
                        {
                            Passport = "4009-392042",
                            PlaceAndDate = "Место 22.06.2002",
                            FIO = "Сидоров Иван Иванович",
                            Birthday = "20.03.1975"
                        });
                        _list.Add("Сидоров Иван Иванович", "4009-392042");
                        break;
                    case 2:
                        Passengers.Add("4001-893939", new Passenger
                        {
                            Passport = "4001-893939",
                            PlaceAndDate = "Место 23.06.2002",
                            FIO = "Петров Иван Иванович",
                            Birthday = "20.03.1980"
                        });
                        _list.Add("Петров Иван Иванович", "4001-893939");
                        break;
                    case 3:
                        Passengers.Add("4001-893943", new Passenger
                        {
                            Passport = "4001-893943",
                            PlaceAndDate = "Место 23.06.2002",
                            FIO = "Федоров Максим Иванович",
                            Birthday = "20.03.1980"
                        });
                        _list.Add("Федоров Максим Иванович", "4001-893943");
                        break;
                }
            });
        }

        public Command<int> Remove
        {
            get => new Command<int>((i) =>
            {
                //System.Windows.MessageBox.Show("Удалить.");
                Passengers.Delete("4001-893939");
                _list.Delete("Петров Иван Иванович", "4001-893939");
            });
        }

        public Command Clear
        {
            get => new Command(() =>
            {
                //System.Windows.MessageBox.Show("Очистить.");
                Passengers.Clear();
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
