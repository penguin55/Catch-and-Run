using UnityEngine;

public class CommandManager : MonoBehaviour
{
    public static Commands UI;
    public static Commands PROPS;

    private void Awake()
    {
        UI = new Commands();
        PROPS = new Commands();
    }
}

public class Commands
{
    public string OPEN_MENU_PANEL = "open_menu_panel";
    public string HIDE_MENU_PANEL = "hide_menu_panel";
    public string OPEN_TUTORIAL_PANEL = "open_tutorial_panel";
    public string HIDE_TUTORIAL_PANEL = "hide_tutorial_panel";
    public string OPEN_ROOM_PANEL = "open_room_panel";
    public string HIDE_ROOM_PANEL = "hide_room_panel";
    public string OPEN_ROOMLIST_PANEL = "open_roomlist_panel";
    public string HIDE_ROOMLIST_PANEL = "hide_roomlist_panel";
    public string OPEN_CREATE_ROOM_PANEL = "open_createroom_panel";
    public string HIDE_CREATE_ROOM_PANEL = "hide_createroom_panel";
    public string OPEN_START_BUTTON = "open_start_button";
    public string HIDE_START_BUTTON = "hide_start_button";
    public string OPEN_READY_BUTTON = "open_ready_button";
    public string HIDE_READY_BUTTON = "hide_ready_button";

    public string READY_PLAYER_STATUS = "ready_player_status";
}
