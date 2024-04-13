using Godot;
using System;
using System.Reflection.Metadata;

public partial class StartMenu : Control
{
    private StartMenuController startMenuController;
    private SettingsMenuController settingsMenuController;
    public override void _Ready()
    {
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
