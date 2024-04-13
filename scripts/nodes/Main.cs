using Godot;
using MasterofElements.scripts.singletons;
using MasterofElements.scripts.singletons.fileaccess;

public partial class Main : Node2D
{
    private AutoLoader _autoLoader;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _autoLoader = new AutoLoader(this);
        _autoLoader.FileAccessService.ReadFile(UserFiles.AudioOptionsFile);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}