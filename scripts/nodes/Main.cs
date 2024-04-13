using Godot;
using MasterofElements.scripts.singletons;
using MasterofElements.scripts.singletons.sceneloader;

public partial class Main : Node2D
{
    private AutoLoader _autoLoader;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GD.Print("HI");
        _autoLoader = new AutoLoader(this);
        _autoLoader.AudioService.PlayMusic("Ludum_Dare_55_Game_Background_Music.mp3", this);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public void OnTimerTimeout()
    {
        GD.Print("Timer timeout");
        _autoLoader.SceneSwitcherService.SwitchScene(AllScenes.SecondScene);
    }
}