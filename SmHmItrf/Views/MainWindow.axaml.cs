using Avalonia.Controls;
using Avalonia;
using Avalonia.Markup.Xaml;
using SmHmItrf.ViewModels;
using Avalonia.Interactivity;
using System.Collections.ObjectModel;

namespace SmHmItrf.Views
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Room> Rooms { get; set; }
        public Room SelectRoom = new Room();

        public MainWindow()
        {
            InitializeComponent();

            var bAddRoom = this.FindControl<Button>("bAddRoom");
            bAddRoom.Click += AddRoom_Click;

            var gDataGrid = this.FindControl<DataGrid>("gDataGrid");
            gDataGrid.SelectionChanged += mSelectionChanged;

            var bEditRoom = this.FindControl<Button>("bEditRoom");
            bEditRoom.Click += EditRoom_Click;

            var bDeleteRoom = this.FindControl<Button>("bDeleteRoom");
            bDeleteRoom.Click += DeleteRoom_Click;
        }

        private void mSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid grid = (DataGrid)sender;
            dynamic selected_row = grid.SelectedItem;
            SelectRoom = selected_row;
        }
        
        

        async private void AddRoom_Click(object? sender,RoutedEventArgs e)
        {
            AddRoom addRoom = new AddRoom();
            await addRoom.ShowDialog(this);
            if(addRoom.resultDialog == true)
            {
                var dbService = new DatabaseService();
                Rooms = dbService.GetRooms();
            }
        }

        async private void EditRoom_Click(object? sender, RoutedEventArgs e)
        {
            AddRoom addRoom = new AddRoom();
            await addRoom.ShowDialog(this);
            if (addRoom.resultDialog == true)
            {
                var dbService = new DatabaseService();
                Rooms = dbService.GetRooms();
            }
        }

        private void DeleteRoom_Click(object sender, RoutedEventArgs e)
        {
            string sql = string.Format("Delete from Rooms where " + "room_id = '{0}'", SelectRoom.ID);
            Database.Exec_SQL(sql);
            SelectRoom = null;
            var dbService = new DatabaseService();
            Rooms = dbService.GetRooms();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}