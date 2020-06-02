using System.IO;
using SAOD_Kursovoy.Model;
using SAOD_Kursovoy.Model.Data;

namespace SAOD_Kursovoy.Service
{
    /// <summary>
    /// Для работы с файлом приложения.
    /// </summary>
    static class FileService
    {
        /// <summary>
        /// Имя файла приложения.
        /// </summary>
        private static string file_name = "авиабилеты.txt";

        /// <summary>
        /// Загрузка данных из файла приложения. 
        /// </summary>
        /// <param name="tickets">Список авиабилетов.</param>
        /// <param name="passengers">Список пассажиров.</param>
        /// <param name="flights">Список авиарейсов.</param>
        public static void Load(List<Ticket> tickets, HashTable<Passenger> passengers, AVLTree<Flight> flights)
        {
            if (File.Exists(file_name))
                using (StreamReader sr = new StreamReader(file_name))
                {
                    string s = sr.ReadLine(); // Ticket Section
                    s = sr.ReadLine();
                    while (s != "Passenger Section")
                    {
                        var array = s.Split('&');
                        var ticket = new Ticket(array[0], array[1]);
                        if (array[2] != "")
                            ticket.Passport = array[2];
                        tickets.Add(ticket);
                        s = sr.ReadLine();
                    }
                    tickets.Sort();
                    s = sr.ReadLine(); //Passenger Section
                    while (s != "Flight Section")
                    {
                        var array = s.Split('&');
                        var passenger = new Passenger()
                        {
                            Passport = array[0],
                            PlaceAndDate = array[1],
                            FIO = array[2],
                            Birthday = array[3]
                        };
                        passengers.Add(passenger.Passport, passenger);
                        s = sr.ReadLine();
                    }
                    s = sr.ReadLine(); //Flight Section
                    while (s != null)
                    {
                        var array = s.Split('&');
                        var flight = new Flight()
                        {
                            Number = array[0],
                            Airline = array[1],
                            DeparturesAirport = array[2],
                            ArrivalAirport = array[3],
                            DeparturesDate = array[4],
                            DeparturesTime = array[5],
                            NumberOfSeatsAll = uint.Parse(array[6]),
                            NumberOfSeatsFree = uint.Parse(array[7])
                        };
                        flights.Add(flight.Number, flight);
                        s = sr.ReadLine();
                    }
                }
        }

        /// <summary>
        /// Сохранение данных в файл приложения. 
        /// </summary>
        /// <param name="tickets">Список авиабилетов.</param>
        /// <param name="passengers">Список пассажиров.</param>
        /// <param name="flights">Список авиарейсов.</param>
        public static void Save(List<Ticket> tickets, HashTable<Passenger> passengers, AVLTree<Flight> flights)
        {
            using (StreamWriter sw = new StreamWriter(file_name, false))
            {
                sw.WriteLine("Ticket Section");
                foreach (var el in tickets)
                {
                    string[] ticket = new string[3];
                    ticket[0] = el.Number;
                    ticket[1] = el.Flight;
                    ticket[2] = el.Passport;
                    sw.WriteLine(string.Join("&", ticket));
                }
                sw.WriteLine("Passenger Section");
                foreach (var el in passengers)
                {
                    string[] passenger = new string[4];
                    passenger[0] = el.Passport;
                    passenger[1] = el.PlaceAndDate;
                    passenger[2] = el.FIO;
                    passenger[3] = el.Birthday;
                    sw.WriteLine(string.Join("&", passenger));
                }
                sw.WriteLine("Flight Section");
                foreach (var el in flights)
                {
                    string[] flight = new string[8];
                    flight[0] = el.Number;
                    flight[1] = el.Airline;
                    flight[2] = el.DeparturesAirport;
                    flight[3] = el.ArrivalAirport;
                    flight[4] = el.DeparturesDate;
                    flight[5] = el.DeparturesTime;
                    flight[6] = el.NumberOfSeatsAll.ToString();
                    flight[7] = el.NumberOfSeatsFree.ToString();
                    sw.WriteLine(string.Join("&", flight));
                }
            }
        }
    }
}
