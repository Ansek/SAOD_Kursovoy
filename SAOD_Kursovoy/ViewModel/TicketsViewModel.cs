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

        public Command<int> Sell
        {
            get => new Command<int>((i) =>
            {
                //System.Windows.MessageBox.Show("Продать.");
                //Tickets.Clear();
                Tickets.Find(new Ticket("645867001", "ABC-003"));
                Tickets.Current.Passport = "4007-395943";
                Tickets.Find(new Ticket("645867005", "ABC-003"));
                Tickets.Current.Passport = "4009-392042";
                Tickets.Find(new Ticket("645867003", "ABC-003"));
                Tickets.Current.Passport = "4001-893939";
                Tickets.Find(new Ticket("645867007", "ABC-003"));
                Tickets.Current.Passport = "4001-893943";
                Tickets.OnCollectionChanged();
            });
        }

        public Command<int> Return
        {
            get => new Command<int>((i) =>
            {
                //System.Windows.MessageBox.Show("Вернуть.");
                //Tickets.Delete(new Ticket() { Number = "000111220", Flight = "ABC-001" });
                //Tickets.Delete(new Ticket() { Number = "000111222", Flight = "ABC-003" });
            });
        }
    }
}
