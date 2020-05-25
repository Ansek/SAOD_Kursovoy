using SAOD_Kursovoy.Service;
using SAOD_Kursovoy.Model;
using SAOD_Kursovoy.Model.Data;

namespace SAOD_Kursovoy.ViewModel
{
    class FlightsViewModel
    {
        public AVLTree<Flight> Flights { get; set; }

        public FlightsViewModel()
        {
            Flights = new AVLTree<Flight>();
            // Проверка сдвигов для символов не входящих в слово
            System.Console.WriteLine(Algorithm.SearchBM("ABCABD", "ABCAB1ABCABD"));
            System.Console.WriteLine(Algorithm.SearchBM("ABCABD", "ABCA2DABCABD"));
            System.Console.WriteLine(Algorithm.SearchBM("ABCABD", "ABC3BDABCABD"));
            System.Console.WriteLine(Algorithm.SearchBM("ABCABD", "AB4ABDABCABD"));
            System.Console.WriteLine(Algorithm.SearchBM("ABCABD", "A5CABDABCABD"));
            System.Console.WriteLine(Algorithm.SearchBM("ABCABD", "6BCABDABCABD"));
            // Проверка сдвигов для символов на входящих в слово
            System.Console.WriteLine(Algorithm.SearchBM("ABCABD", "ABCABAABCABD"));
            System.Console.WriteLine(Algorithm.SearchBM("ABCABD", "ABCABDABCABD"));
            System.Console.WriteLine(Algorithm.SearchBM("ABCABD", "ABCCBDABCABD"));
            System.Console.WriteLine(Algorithm.SearchBM("ABCABD", "ABDABDABCABD"));
            System.Console.WriteLine(Algorithm.SearchBM("ABCABD", "AACABDABCABD"));
            System.Console.WriteLine(Algorithm.SearchBM("ABCABD", "BBCABDABCABD"));
        }

        public Command FindByFlight
        {
            get => new Command(() =>
            {
                System.Windows.MessageBox.Show("Поиск по <№ авиарейса>.");
            });
        }

        public Command FindByAirport
        {
            get => new Command(() =>
            {
                //System.Windows.MessageBox.Show("Поиск по <Аэропорт прибытия>.");
                var result = Algorithm.FindFlight(Flights, "верь");
                foreach (var res in result)
                    System.Windows.MessageBox.Show(res);
            });
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
                    NumberOfSeatsAll = 100,
                    NumberOfSeatsFree = 50
                }; 
                switch (Flights.Count)
                {
                    case 0:
                        flight.Number = "ABC-001";
                        Flights.Add(flight.Number, flight);
                        break;
                    case 1:
                        flight.Number = "ABC-002";
                        Flights.Add(flight.Number, flight);
                        break;
                    case 2:
                        flight.Number = "ABC-003";
                        flight.ArrivalAirport = "Тверь";
                        Flights.Add(flight.Number, flight);
                        break;
                    case 3:
                        flight.Number = "ABC-004";
                        Flights.Add(flight.Number, flight);
                        break;
                    case 4:
                        flight.Number = "ABC-005";
                        Flights.Add(flight.Number, flight);
                        break;
                    case 5:
                        flight.Number = "ABC-006";
                        Flights.Add(flight.Number, flight);
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
    }
}
