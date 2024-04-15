using Godot;

public partial class MovablePlatform : AnimatableBody2D
{
    [Export] public Marker2D StartMarker;
    [Export] public Marker2D EndMarker;
    [Export] public float Speed = 50;
    [Export] public Node2D Shadow;


    private Tween _tween;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        float timeToReachEnd = StartMarker.GlobalPosition.DistanceTo(EndMarker.GlobalPosition) / Speed;
        GlobalPosition = StartMarker.GlobalPosition;
        _tween = GetTree().CreateTween().SetLoops().BindNode(this);
        _tween.TweenProperty(this, "global_position", EndMarker.GlobalPosition, timeToReachEnd);
        _tween.TweenProperty(this, "global_position", StartMarker.GlobalPosition, timeToReachEnd);
        Shadow.Hide();
    }

}