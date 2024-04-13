using Godot;
using System;

public partial class Sprite2D : Godot.Sprite2D
{
    bool right = true;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(double delta)
    {
        if (right)
        {
            GlobalPosition += (Vector2.Down + Vector2.Right) * (float)delta * 500;
        }
        else
        {
            GlobalPosition += (Vector2.Up + Vector2.Left) * (float)delta * 500;
        }
    }

    public void TimerStop()
    {
        right = !right;
    }
}