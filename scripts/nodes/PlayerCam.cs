using Godot;

public partial class PlayerCam : Node2D
{
    [Export] public Node2D Target;

    private Marker2D _markerLeftBotttom;
    private Marker2D _markerRightTop;
    private Camera2D _camera2D;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _markerLeftBotttom = GetNode<Marker2D>("LevelLimitLB");
        _markerRightTop = GetNode<Marker2D>("LevelLimitRT");
        _camera2D = GetNode<Camera2D>("Camera2D");
        _camera2D.LimitLeft = (int)_markerLeftBotttom.GlobalPosition.X;
        _camera2D.LimitBottom = (int)_markerLeftBotttom.GlobalPosition.Y;
        _camera2D.LimitTop = (int)_markerRightTop.GlobalPosition.Y;
        _camera2D.LimitRight = (int)_markerRightTop.GlobalPosition.X;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (Target != null)
        {
            GlobalPosition = Target.GlobalPosition;
        }
    }
}