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
            
            Tickets.Add(new Ticket() { Number = "000111220", Flight = "ABC-001" });
            Tickets.Add(new Ticket() { Number = "000111221", Flight = "ABC-002" });
            Tickets.Add(new Ticket() { Number = "000111222", Flight = "ABC-003" });
            Tickets.Add(new Ticket() { Number = "000111223", Flight = "ABC-004" });
            Tickets.Add(new Ticket() { Number = "000111224", Flight = "ABC-005" });
        }

        public Command<int> Sell
        {
            get => new Command<int>((i) =>
            {
                //System.Windows.MessageBox.Show("Продать.");
                Tickets.Clear();
                
            });
        }

        public Command<int> Return
        {
            get => new Command<int>((i) =>
            {
                //System.Windows.MessageBox.Show("Вернуть.");
                Tickets.Delete(new Ticket() { Number = "000111220", Flight = "ABC-001" });
                Tickets.Delete(new Ticket() { Number = "000111222", Flight = "ABC-003" });
            });
        }
    }
}
