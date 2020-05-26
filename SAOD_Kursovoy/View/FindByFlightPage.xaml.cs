using System.Windows.Controls;

namespace SAOD_Kursovoy.View
{
    /// <summary>
    /// Логика взаимодействия для FindByFlightPage.xaml
    /// </summary>
    public partial class FindByFlightPage : Page
    {
        public FindByFlightPage()
        {
            InitializeComponent();
            DataContext = (App.Current.MainWindow as MainWindow).FlightsVM;
        }
    }
}
