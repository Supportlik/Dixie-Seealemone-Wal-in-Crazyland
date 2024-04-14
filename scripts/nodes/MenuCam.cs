using Godot;

public partial class MenuCam : Node2D
{
    [Export] public int CamSpeed = 200;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    public override void _Process(double delta)
    {        
        GlobalPosition = new Vector2(
            GlobalPosition.X + CamSpeed * (float)delta,
            GlobalPosition.Y);
    }
}