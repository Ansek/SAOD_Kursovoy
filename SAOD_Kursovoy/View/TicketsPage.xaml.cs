using System.Windows.Controls;

namespace SAOD_Kursovoy.View
{
    /// <summary>
    /// Вкладка «Авиабилеты».
    /// </summary>
    public partial class TicketsPage : Page
    {
        public TicketsPage()
        {
            InitializeComponent();
            var mw = App.Current.MainWindow as MainWindow;
            DataContext = mw.TicketsVM = new ViewModel.TicketsViewModel();
        }
    }
}
