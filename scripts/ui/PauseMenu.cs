using Godot;
using MasterofElements.scripts.singletons;
using System;

public partial class PauseMenu : Control
{
    private PauseMenuController pauseMenuController;
    private SettingsMenuController settingsMenuController;

    public override void _Ready()
    {
        pauseMenuController = GetNode<PauseMenuController>("ColorRect/Panel/GridContainer/Content/PauseMenuContent");
        pauseMenuController.ClosePauseMenu += HandleClosePauseMenu;
        settingsMenuController = GetNode<SettingsMenuController>("ColorRect/Panel/GridContainer/Content/SettingsMenuContent");
        settingsMenuController.CloseSettingsMenu += HandleCloseSettingsMenu;
    }

    private void HandleCloseSettingsMenu()
    {
        pauseMenuController.Visible = true;
        settingsMenuController.Visible = false;
    }


    private void HandleClosePauseMenu()
    {
        pauseMenuController.Visible = false;
        settingsMenuController.Visible = true;
    }
}
