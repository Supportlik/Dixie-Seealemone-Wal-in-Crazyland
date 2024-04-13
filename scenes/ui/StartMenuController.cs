using Godot;
using MasterofElements.scripts.singletons;
using MasterofElements.scripts.singletons.sceneloader;
using System;

public partial class StartMenuController : VBoxContainer
{
    [Signal] public delegate void CloseStartMenuEventHandler();
    private AutoLoader _autoLoader;
    public override void _Ready()
    {
        _autoLoader = new AutoLoader(this);
    }
    private void _on_start_button_pressed(){
        _autoLoader.SceneSwitcherService.SwitchScene(AllScenes.BaseLevel);
    }

    private void _on_option_button_pressed(){
        EmitSignal(SignalName.CloseStartMenu);
        GD.Print("Option");
    }

    private void _on_quit_button_pressed(){
        GetTree().Quit();
    }
}
