using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Markup;

namespace SmHmItrf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<Room> Rooms { get; set; }

        public MainWindowViewModel()
        {
            var dbService = new DatabaseService();
            Rooms = dbService.GetRooms();
        }
    }
}
