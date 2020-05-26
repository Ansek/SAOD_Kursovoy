using System.Windows.Controls;

namespace SAOD_Kursovoy.View
{
    /// <summary>
    /// Логика взаимодействия для FindByAirportPage.xaml
    /// </summary>
    public partial class FindByAirportPage : Page
    {
        public FindByAirportPage()
        {
            InitializeComponent();
            DataContext = (App.Current.MainWindow as MainWindow).FlightsVM;
        }
    }
}
