using System.Windows.Controls;

namespace SAOD_Kursovoy.View
{
    /// <summary>
    /// Боковая вкладка для поиска пассажира по ФИО
    /// </summary>
    public partial class FindByFIOPage : Page
    {
        public FindByFIOPage()
        {
            InitializeComponent();
            DataContext = (App.Current.MainWindow as MainWindow).PassengersVM;
        }
    }
}
