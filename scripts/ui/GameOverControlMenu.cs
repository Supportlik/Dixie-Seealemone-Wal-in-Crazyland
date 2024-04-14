using Godot;
using MasterofElements.scripts.singletons;

public partial class GameOverControlMenu : Control
{
    private AutoLoader _autoLoader;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _autoLoader = new AutoLoader(this);
        _autoLoader.SignalManager.OnGameOver += ShowMenu;
    }

    public override void _ExitTree()
    {
        _autoLoader.SignalManager.OnGameOver -= ShowMenu;
    }

    public void ShowMenu()
    {
        Show();
    }
}