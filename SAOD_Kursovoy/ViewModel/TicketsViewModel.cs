using System.Windows;
using SAOD_Kursovoy.Service;
using SAOD_Kursovoy.Model;
using SAOD_Kursovoy.Model.Data;

namespace SAOD_Kursovoy.ViewModel
{
    class TicketsViewModel
    {
        public List<Ticket> Tickets { get; set; }

        public TicketsViewModel()
        {
            Tickets = new List<Ticket>();
        }

        private string SumString(string str)
        {
            int sum = 0;
            foreach (var c in str)
                sum += c;
            return sum.ToString().Substring(0, 2);
        }

        public void OnAddFlight(Flight flight)
        {
            var part = SumString(flight.Airline) + 
                       SumString(flight.ArrivalAirport + flight.DeparturesDate) +
                       SumString(flight.DeparturesAirport + flight.DeparturesTime);
            for (int i = 0; i < flight.NumberOfSeatsAll; i++)
            {
                var num = part + i.ToString("000").Substring(0, 3);
                Tickets.Add(new Ticket(num, flight.Number));
            }                
        }

        public void OnDeleteFlight(string flight)
        {
            var listDelete = new List<Ticket>();
            foreach (var t in Tickets)
                if (t.Flight == flight)
                    listDelete.Add(t);
            foreach (var t in listDelete)
                Tickets.Delete(t);
        }

        public void OnClearFlight()
        {
            Tickets.Clear();
        }

        public Command<Ticket> Sell
        {
            get => new Command<Ticket>((t) =>
            {
                var mv = App.Current.MainWindow as View.MainWindow;
                if ((mv.PassengersVM as PassengersViewModel).Passengers.Count > 0)
                {
                    var d = new View.RegistrationTicket(t.Number, t.Flight);
                    if (d.ShowDialog() == true)
                    {
                        t.Passport = d.Result;

                        var flights = (mv.FlightsVM as FlightsViewModel).Flights;
                        var flight = flights.Find(t.Flight);
                        if (flight != null)
                            flight.NumberOfSeatsFree--;
                    }
                }
                else
                    MessageBox.Show("В системе не зарегстрированно ни одного пользователя.");
            }, (t) => t != null && t.Passport == null);
        }

        public Command<Ticket> Return
        {
            get => new Command<Ticket>((t) =>
            {
                if (MessageBox.Show($"Вы действительно хотите вернуть билет с номером {t.Number}?",
                    "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    t.Passport = null;
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
