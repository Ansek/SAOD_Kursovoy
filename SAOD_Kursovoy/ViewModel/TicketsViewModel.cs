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
            
            Tickets.Add(new Ticket() { Number = "000111232", Flight = "ABC-005" });
            Tickets.Add(new Ticket() { Number = "000111228", Flight = "ABC-005" });
            Tickets.Add(new Ticket() { Number = "000111224", Flight = "ABC-005" });
            Tickets.Add(new Ticket() { Number = "000111222", Flight = "ABC-003" });
            Tickets.Add(new Ticket() { Number = "000111229", Flight = "ABC-005" });
            Tickets.Add(new Ticket() { Number = "000111225", Flight = "ABC-005" });
            Tickets.Add(new Ticket() { Number = "000111220", Flight = "ABC-001" });
            Tickets.Add(new Ticket() { Number = "000111223", Flight = "ABC-004" });
            Tickets.Add(new Ticket() { Number = "000111227", Flight = "ABC-005" });
            Tickets.Add(new Ticket() { Number = "000111221", Flight = "ABC-002" });
            Tickets.Add(new Ticket() { Number = "000111226", Flight = "ABC-005" });
            Tickets.Add(new Ticket() { Number = "000111218", Flight = "ABC-002" });
            Tickets.Add(new Ticket() { Number = "000111219", Flight = "ABC-002" });
            Tickets.Add(new Ticket() { Number = "000111217", Flight = "ABC-002" });
            Tickets.Add(new Ticket() { Number = "000111231", Flight = "ABC-002" });
            Tickets.Add(new Ticket() { Number = "000111234", Flight = "ABC-002" });
            Tickets.Add(new Ticket() { Number = "000111233", Flight = "ABC-002" });
            Tickets.Add(new Ticket() { Number = "000111236", Flight = "ABC-002" }); /// Проблема с последним.
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
