using SAOD_Kursovoy.Service;
using SAOD_Kursovoy.Model;
using SAOD_Kursovoy.Model.Data;

namespace SAOD_Kursovoy.ViewModel
{
    class PassengersViewModel
    {
        public HashTable<Passenger> Passengers { get; set; }

        public PassengersViewModel()
        {
            Passengers = new HashTable<Passenger>(Algorithm.GetHash);
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
                System.Windows.MessageBox.Show("Поиск по <ФИО>.");
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
                        Passengers.Add(new Passenger
                        {
                            Passport = "0000-000000",
                            PlaceAndDate = "Место 21.06.2002",
                            FIO = "Иванов Иван Иванович",
                            Birthday = "20.03.1970"
                        });
                        break;
                    case 1:
                        Passengers.Add(new Passenger
                        {
                            Passport = "0000-000001",
                            PlaceAndDate = "Место 22.06.2002",
                            FIO = "Сидоров Иван Иванович",
                            Birthday = "20.03.1975"
                        });
                        break;
                    case 2:
                        Passengers.Add(new Passenger
                        {
                            Passport = "0000-000002",
                            PlaceAndDate = "Место 23.06.2002",
                            FIO = "Петров Иван Иванович",
                            Birthday = "20.03.1980"
                        });
                        break;
                }
            });
        }

        public Command<int> Remove
        {
            get => new Command<int>((i) =>
            {
                System.Windows.MessageBox.Show("Удалить.");
            });
        }

        public Command Clear
        {
            get => new Command(() =>
            {
                System.Windows.MessageBox.Show("Очистить.");
            });
        }
    }
}
