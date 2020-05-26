using System.Windows;
using SAOD_Kursovoy.ViewModel;
using SAOD_Kursovoy.Service;

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
            PassengersVM = new PassengersViewModel();
            FlightsVM = new FlightsViewModel();

            // Загрузка с файла
            FileService.Load((TicketsVM as TicketsViewModel).Tickets,
                             (PassengersVM as PassengersViewModel).Passengers,
                             (FlightsVM as FlightsViewModel).Flights);

            // Для поиска
            foreach (var p in (PassengersVM as PassengersViewModel).Passengers)
                (PassengersVM as PassengersViewModel).InvertedList.Add(p.FIO, p.Passport);

            // Связь событий с другими станицами
            (FlightsVM as FlightsViewModel).AddFlight += (TicketsVM as TicketsViewModel).OnAddFlight;
            (FlightsVM as FlightsViewModel).DeleteFlight += (TicketsVM as TicketsViewModel).OnDeleteFlight;
            (FlightsVM as FlightsViewModel).ClearFlight += (TicketsVM as TicketsViewModel).OnClearFlight;
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            // Сохранение в файл
            FileService.Save((TicketsVM as TicketsViewModel).Tickets,
                             (PassengersVM as PassengersViewModel).Passengers,
                             (FlightsVM as FlightsViewModel).Flights);
        }
    }
}
