using System.Windows;
using SAOD_Kursovoy.ViewModel;

namespace SAOD_Kursovoy.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public object TicketsVM { get; set; }
        public object PassengersVM { get; set; }
        public object FlightsVM { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            PassengersVM = new PassengersViewModel();
            FlightsVM = new FlightsViewModel();

            // Связь событий с другими станицами
            (FlightsVM as FlightsViewModel).AddFlight += (TicketsVM as TicketsViewModel).OnAddFlight;
            (FlightsVM as FlightsViewModel).DeleteFlight += (TicketsVM as TicketsViewModel).OnDeleteFlight;
            (FlightsVM as FlightsViewModel).ClearFlight += (TicketsVM as TicketsViewModel).OnClearFlight;
        }

    }
}
