using System.Windows.Controls;

namespace SAOD_Kursovoy.View
{
    /// <summary>
    /// Боковая вкладка для поиска авиарейса по аэропорту прибытия
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
