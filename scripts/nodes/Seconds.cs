using Godot;
using MasterofElements.scripts.singletons;

namespace MasterofElements.scripts.nodes;

public partial class Seconds : Node2D
{
    private AutoLoader _autoLoader;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GD.Print("HI2");
        _autoLoader = new AutoLoader(this);
        _autoLoader.AudioService.PlayMusic("Ludum_Dare_55_Menu_Music.mp3", this);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
    
    
}