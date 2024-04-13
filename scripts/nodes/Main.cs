using Godot;
using MasterofElements.scripts.singletons;

public partial class Main : Node2D
{
    private AutoLoader _autoLoader;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GD.Print("HI");
        _autoLoader = new AutoLoader(this);
        _autoLoader.AudioService.PlayMusic("Ludum_Dare_55_Game_Background_Music.mp3");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}