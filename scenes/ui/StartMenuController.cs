using Godot;
using System;

public partial class StartMenuController : VBoxContainer
{
    private void _on_start_button_pressed(){
        GD.Print("Start");
    }

    private void _on_option_button_pressed(){
        GD.Print("Option");
    }

    private void _on_quit_button_pressed(){
        GD.Print("Quit");
    }
}
