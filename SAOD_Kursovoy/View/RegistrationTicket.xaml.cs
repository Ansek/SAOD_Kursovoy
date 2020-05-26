using System;
using System.Windows;
using SAOD_Kursovoy.Service;
using SAOD_Kursovoy.ViewModel;


namespace SAOD_Kursovoy.View
{
    /// <summary>
    /// Логика взаимодействия для RegistrationTicket.xaml
    /// </summary>
    public partial class RegistrationTicket : Window
    {
        public string Result { get; set; }

        public RegistrationTicket(string ticket, string flight)
        {
            InitializeComponent();
            var mv = App.Current.MainWindow as MainWindow;
            var pas = (mv.PassengersVM as PassengersViewModel).Passengers;
            DataContext = new Tuple<string, string, object>(ticket, flight, pas);
            OK.Command = new Command<string>((text) =>
            {
                Result = text;
                DialogResult = true;
            }, (text) => text != null);
        }
    }
}
