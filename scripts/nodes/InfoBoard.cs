using Godot;
using MasterofElements.scripts.singletons;
using System;

public partial class InfoBoard : Area2D
{
    [Export]
    public string InfoText;
    private bool playerAvailable = false;
    private bool infoVisible;
    private AutoLoader _autoLoader;

    public override void _Ready()
    {
        _autoLoader = new AutoLoader(this);
    }

    private void _on_area_exited(Node2D body){
        playerAvailable = false;
        infoVisible = false;
        _autoLoader.DialogManager.HideDialog();
    }

    private void _on_area_entered(Area2D body){
        playerAvailable = true;
    }

    public override void _Process(double delta)
    {
        if(playerAvailable && !infoVisible){
            _autoLoader.DialogManager.ShowDialog(InfoText);
            infoVisible = true;
        }
    }
}
