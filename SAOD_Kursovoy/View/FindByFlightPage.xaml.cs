using System.Windows.Controls;

namespace SAOD_Kursovoy.View
{
    /// <summary>
    /// Боковая вкладка для поиска авиарейса по номеру авиарейса
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
