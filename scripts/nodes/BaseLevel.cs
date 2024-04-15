using Godot;
using MasterofElements.scripts.models;
using MasterofElements.scripts.singletons;

public partial class BaseLevel : Node2D
{
    private AutoLoader _autoLoader;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _autoLoader = new AutoLoader(this);
        _autoLoader.GameManager.StartGame();
        
        var spawnNode = GetTree().GetFirstNodeInGroup(GroupNames.SpawnGroup);

        if (spawnNode == null)
        {
            GD.PrintErr("SPAWN GROUP NOT FOUND!");
        }
        
        _autoLoader.AudioService.PlayMusic("Ludum_Dare_55_Game_Background_Music.mp3", spawnNode, "background_music_game");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}