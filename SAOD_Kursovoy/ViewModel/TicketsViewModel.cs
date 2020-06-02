using System.Windows;
using SAOD_Kursovoy.Service;
using SAOD_Kursovoy.Model;
using SAOD_Kursovoy.Model.Data;

namespace SAOD_Kursovoy.ViewModel
{
    /// <summary>
    /// Класс-посредник для авиабилетов. 
    /// </summary>
    class TicketsViewModel
    {
        /// <summary>
        /// Хранит данные об авиабилетах.
        /// </summary>
        public List<Ticket> Tickets { get; set; }

        public TicketsViewModel()
        {
            Tickets = new List<Ticket>();
        }

        /// <summary>
        /// Преобразует строку в число из двух символов. 
        /// </summary>
        /// <param name="str">Преобразуемая строка.</param>
        private string SumString(string str)
        {
            int sum = 0;
            foreach (var c in str)
                sum += c;
            return sum.ToString().Substring(0, 2);
        }

        /// <summary>
        /// Вызывается при добавлении авиарейса.
        /// </summary>
        /// <param name="flight">Объект авиарейса.</param>
        public void OnAddFlight(Flight flight)
        {
            // Генерации начального значение номера билета
            var part = SumString(flight.Airline) + 
                       SumString(flight.ArrivalAirport + flight.DeparturesDate) +
                       SumString(flight.DeparturesAirport + flight.DeparturesTime);
            for (int i = 0; i < flight.NumberOfSeatsAll; i++)
            {
                // Получение порядкого номера для билета
                var num = part + i.ToString("000").Substring(0, 3);
                // Добавление билета
                Tickets.Add(new Ticket(num, flight.Number));
            }
            Tickets.Sort();
        }

        /// <summary>
        /// Вызывается при удалении билета.
        /// </summary>
        /// <param name="flight">Номер билета.</param>
        public void OnDeleteFlight(string flight)
        {
            var listDelete = new List<Ticket>();
            // Создание списка удаляемых авиабилетов
            foreach (var t in Tickets)
                if (t.Flight == flight)
                    listDelete.Add(t);
            // Удаление авиабилетов
            foreach (var t in listDelete)
                Tickets.Delete(t);
        }

        /// <summary>
        /// Вызывается при очистке списка авиабилетов.
        /// </summary>
        public void OnClearFlight()
        {
            Tickets.Clear();
        }

        /// <summary>
        /// Команда продажи авиабилета.
        /// </summary>
        public Command<Ticket> Sell
        {
            get => new Command<Ticket>((t) =>
            {
                var mv = App.Current.MainWindow as View.MainWindow;
                // Проверка наличия пользователей
                if ((mv.PassengersVM as PassengersViewModel).Passengers.Count > 0)
                {
                    // Передача данных в окно
                    var d = new View.RegistrationTicket(t.Number, t.Flight);
                    if (d.ShowDialog() == true)
                    {
                        t.Passport = d.Result; // Отображение паспорта

                        // Уменьшение параметра свободных билетов
                        var flights = (mv.FlightsVM as FlightsViewModel).Flights;
                        var flight = flights.Find(t.Flight);
                        if (flight != null)
                            flight.NumberOfSeatsFree--;
                    }
                }
                else
                    MessageBox.Show("В системе не зарегистрированно ни одного пользователя.");
            }, (t) => t != null && t.Passport == null);
        }

        /// <summary>
        /// Команда возвращения авиабилета.
        /// </summary>
        public Command<Ticket> Return
        {
            get => new Command<Ticket>((t) =>
            {
                if (MessageBox.Show($"Вы действительно хотите вернуть билет с номером {t.Number}?",
                    "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    t.Passport = null; // Удаление пасспорта
                    // Увеличение параметра свободных билетов
                    var mv = App.Current.MainWindow as View.MainWindow;
                    var flights = (mv.FlightsVM as FlightsViewModel).Flights;
                    var flight = flights.Find(t.Flight);
                    if (flight != null)
                        flight.NumberOfSeatsFree++;
                }
            }, (t) => t != null && t.Passport != null);
        }
    }
}
