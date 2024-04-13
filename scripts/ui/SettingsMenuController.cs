using Godot;
using System;

public partial class SettingsMenuController : VBoxContainer
{
    [Signal] public delegate void CloseSettingsMenuEventHandler();
    public VBoxContainer ContentContainer;
    public AudioBusUI audioBusUI;

    public override void _Ready()
    {
        ContentContainer =  GetNode<VBoxContainer>("Settings/MarginContainer/Content");
        audioBusUI = new AudioBusUI();
        ContentContainer.AddChild(audioBusUI);
    }
    private void _on_texture_button_pressed(){
        EmitSignal(SignalName.CloseSettingsMenu);
    }
}