using System.Windows.Controls;

namespace SAOD_Kursovoy.View
{
    /// <summary>
    /// Вкладка "Авиарейсы".
    /// </summary>
    public partial class FlightsPage : Page
    {
        public FlightsPage()
        {
            InitializeComponent();
            var mw = App.Current.MainWindow as MainWindow;
            DataContext = mw.FlightsVM;
        }
    }
}
