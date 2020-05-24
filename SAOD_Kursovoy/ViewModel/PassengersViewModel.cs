using SAOD_Kursovoy.Service;
using SAOD_Kursovoy.Model;
using SAOD_Kursovoy.Model.Data;

namespace SAOD_Kursovoy.ViewModel
{
    class PassengersViewModel
    {
        public HashTable<Passenger> Passengers { get; set; }
        private InvertedList _list;

        public PassengersViewModel()
        {
            Passengers = new HashTable<Passenger>(Algorithm.GetHash);
            _list = new InvertedList();
        }

        public Command FindByPassport
        {
            get => new Command(() =>
            {
                System.Windows.MessageBox.Show("Поиск по <№ паспорта>.");
            });
        }

        public Command FindByFIO
        {
            get => new Command(() =>
            {
                //System.Windows.MessageBox.Show("Поиск по <ФИО>.");
                var result = _list.Find("Иван Иванович");
                foreach (var res in result)
                    System.Windows.MessageBox.Show(res);
            });
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
    }
}
