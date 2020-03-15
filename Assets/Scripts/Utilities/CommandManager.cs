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
    public string LOG_ROOM = "log_room";
    public string OPEN_CONNECTING_PANEL = "open_connecting_panel";
    public string HIDE_CONNECTING_PANEL = "hide_connecting_panel";
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

    public string UPDATE_TIME_UI = "update_time_ui";
    public string UPDATE_START_TIMER = "update_start_timer";
    public string OPEN_TIME_TEXT = "open_time_text";
    public string HIDE_TIME_TEXT = "hide_time_text";
    public string OPEN_START_TIMER_TEXT = "open_start_timer_text";
    public string HIDE_START_TIMER_TEXT = "hide_start_timer_text";
    public string SET_LAST_CATCHER = "set_last_catcher";
    public string OPEN_EXIT_BUTTON = "open_exit_button";
    public string HIDE_EXIT_BUTTON = "hide_exit_button";
    public string OPEN_EXITCONF_PANEL = "open_exit_confirmation_panel";
    public string HIDE_EXITCONF_PANEL = "hide_exit_confirmation_panel";
    public string OPEN_GAMEOVER_PANEL = "open_gameover_panel";
    public string HIDE_GAMEOVER_PANEL = "hide_gameover_panel";

    public string READY_PLAYER_STATUS = "ready_player_status";
    public string READY_TO_SPAWN = "ready_to_spawn";
    public string PLAYER_POSITION = "player_position";
}
