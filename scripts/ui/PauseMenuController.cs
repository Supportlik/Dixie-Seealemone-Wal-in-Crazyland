using Godot;
using MasterofElements.scripts.singletons;
using MasterofElements.scripts.singletons.sceneloader;
using System;

public partial class PauseMenuController : VBoxContainer
{
    [Signal] public delegate void ClosePauseMenuEventHandler();
    [Signal] public delegate void ResumeGameEventHandler();
    private AutoLoader _autoLoader;

    public override void _Ready()
    {
        _autoLoader = new AutoLoader(this);
    }
    private void _on_menu_button_pressed(){
        GD.Print("test");
        _autoLoader.SceneSwitcherService.SwitchScene(AllScenes.StartMenu);
    }

    private void _on_option_button_pressed(){
        GD.Print("test");
        EmitSignal(SignalName.ClosePauseMenu);
    }

    private void _on_resume_button_pressed(){
        GD.Print("test");
        EmitSignal(SignalName.ResumeGame);
    }
}
