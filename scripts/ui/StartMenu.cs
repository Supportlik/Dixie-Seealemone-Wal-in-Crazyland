using Godot;
using MasterofElements.scripts.singletons;
using System;
using System.Reflection.Metadata;

public partial class StartMenu : Control
{
    private StartMenuController startMenuController;
    private SettingsMenuController settingsMenuController;
    private AutoLoader _autoLoader;
    public override void _Ready()
    {
        _autoLoader = new AutoLoader(this);
        _autoLoader.AudioService.PlayMusic("Ludum_Dare_55_Menu_Music.mp3", this);
        startMenuController = GetNode<StartMenuController>("Control/ColorRect/Panel/GridContainer/Content/StartMenuContent");
        startMenuController.CloseStartMenu += HandleCloseStartMenu;
        settingsMenuController = GetNode<SettingsMenuController>("Control/ColorRect/Panel/GridContainer/Content/SettingsMenuContent");
        settingsMenuController.CloseSettingsMenu += HandleCloseSettingsMenu;
    }

    private void HandleCloseSettingsMenu()
    {
        startMenuController.Visible = true;
        settingsMenuController.Visible = false;
    }


    private void HandleCloseStartMenu()
    {
        startMenuController.Visible = false;
        settingsMenuController.Visible = true;
    }

}
