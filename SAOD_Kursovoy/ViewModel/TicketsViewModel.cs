using SAOD_Kursovoy.Service;

namespace SAOD_Kursovoy.ViewModel
{
    class TicketsViewModel
    {
        public Command<int> Sell
        {
            get => new Command<int>((i) =>
            {
                System.Windows.MessageBox.Show("Продать.");
            });
        }

        public Command<int> Return
        {
            get => new Command<int>((i) =>
            {
                System.Windows.MessageBox.Show("Вернуть.");
            });
        }
    }
}
