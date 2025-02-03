using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.ObjectModel;

namespace SmHmItrf;

public partial class AddRoom : Window
{
    public Room itemRoom;
    public bool resultDialog;
    //public ObservableCollection<Room> Rooms { get; set; }

    public Room SelectRoom = new Room();
    public string mode = "insert";
    public AddRoom()
    {
        InitializeComponent();

        var bSaveRoom = this.FindControl<Button>("bSaveRoom");
        bSaveRoom.Click += SaveRoom_Click;

        var bCancel = this.FindControl<Button>("bCancel");
        bCancel.Click += Cancel_Click;
    }

    private void Cancel_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        resultDialog = false;
        Close();
    }

    private void SaveRoom_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        string RoomName = this.FindControl<TextBox>("tRoomName").Text;

        int peripheralLightId = 0;
        bool saccessParsePLId = int.TryParse(this.FindControl<TextBox>("tPeripheralLightId").Text, out peripheralLightId);
        int PeripheralLightId = peripheralLightId;

        int mainLightId = 0;
        bool saccessParseMLId = int.TryParse(this.FindControl<TextBox>("tMainLightId").Text, out mainLightId);
        int MainLightId = mainLightId;

        string sql = "";
        if(mode == "insert")
        {
            sql = string.Format(@"Insert into Rooms (room_name, peripheral_light_id, main_light_id)"
                        + "Values ('{0}', '{1}', '{2}');", RoomName, PeripheralLightId, MainLightId);
        }
        else
        {
            
            sql = string.Format("Update Rooms SET room_name = '{0}'," + "peripheral_light_id = '{1}'"+ "main_light_id = '{2}'"+"Where room_id = '{3}';",
                RoomName, PeripheralLightId, MainLightId, SelectRoom.ID);
        }


        resultDialog = Database.Exec_SQL(sql);
        Close();
    }
    private void AddRoom_Activated(object? sender, System.EventArgs e)
    {
        if(mode == "edit")
        {
            if (SelectRoom != null)
            {
                this.FindControl<TextBox>("tRoomName").Text = SelectRoom.RoomName.ToString();
            }
        }
    }
}