using Godot;

public partial class BaseLevel : Node2D
{
    private Hud _hud;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _hud = GetNode<Hud>("HUD");
        _hud.SetScore(9999);
        _hud.SetHP(5);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}