using System.Windows;
using SAOD_Kursovoy.Model.Data;
using SAOD_Kursovoy.Service;

namespace SAOD_Kursovoy.View
{
    /// <summary>
    /// Окно добавления данных о пассажире.
    /// </summary>
    public partial class InputPassenger : Window
    {
        public InputPassenger()
        {
            InitializeComponent();
            DataContext = new Passenger();
            OK.Command = new Command(() =>
            {
                DialogResult = true;
            }, () => (DataContext as Passenger).Error == string.Empty &&
                (DataContext as Passenger).IsFieldsNotNull);
        }
    }
}
