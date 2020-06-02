using System.Windows.Controls;

namespace SAOD_Kursovoy.View
{
    /// <summary>
    /// Боковая вкладка для поиска пассажира по номеру паспорта
    /// </summary>
    public partial class FindByPassportPage : Page
    {
        public FindByPassportPage()
        {
            InitializeComponent();
            DataContext = (App.Current.MainWindow as MainWindow).PassengersVM;
        }
    }
}
