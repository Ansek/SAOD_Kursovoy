using SAOD_Kursovoy.Service;

namespace SAOD_Kursovoy.ViewModel
{
    class FlightsViewModel
    {
        public Command FindByFlight
        {
            get => new Command(() =>
            {
                System.Windows.MessageBox.Show("Поиск по <№ авиарейса>.");
            });
        }

        public Command FindByAirport
        {
            get => new Command(() =>
            {
                System.Windows.MessageBox.Show("Поиск по <Аэропорт прибытия>.");
            });
        }

        public Command Add
        {
            get => new Command(() =>
            {
                System.Windows.MessageBox.Show("Добавить.");
            });
        }

        public Command<int> Remove
        {
            get => new Command<int>((i) =>
            {
                System.Windows.MessageBox.Show("Удалить.");
            });
        }

        public Command Clear
        {
            get => new Command(() =>
            {
                System.Windows.MessageBox.Show("Очистить.");
            });
        }
    }
}
