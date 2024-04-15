using Godot;
using MasterofElements.scripts.singletons;
using MasterofElements.scripts.singletons.signalmanager;

public partial class PlayerCam : Node2D
{
    [Export] public Node2D Target;
    [Export] public int ShakeAmount = 5;

    private Marker2D _markerLeftBotttom;
    private Marker2D _markerRightTop;
    private Camera2D _camera2D;
    private Timer _shakeTimer;

    private AutoLoader _autoLoader;
    private bool _isDead = false;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _autoLoader = new AutoLoader(this);
        _markerLeftBotttom = GetNode<Marker2D>("LevelLimitLB");
        _markerRightTop = GetNode<Marker2D>("LevelLimitRT");
        _camera2D = GetNode<Camera2D>("Camera2D");
        _shakeTimer = GetNode<Timer>("ShakeTimer");
        _camera2D.LimitLeft = (int)_markerLeftBotttom.GlobalPosition.X;
        _camera2D.LimitBottom = (int)_markerLeftBotttom.GlobalPosition.Y;
        _camera2D.LimitTop = (int)_markerRightTop.GlobalPosition.Y;
        _camera2D.LimitRight = (int)_markerRightTop.GlobalPosition.X;
        _autoLoader.SignalManager.OnPlayerHurt += StartShake;
        _autoLoader.SignalManager.OnPlayerDead += StartShake;
    }

    public override void _ExitTree()
    {
        _autoLoader.SignalManager.OnPlayerHurt -= StartShake;
        _autoLoader.SignalManager.OnPlayerDead -= StartShake;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (Target != null)
        {
            GlobalPosition = Target.GlobalPosition;
            CheckPlayerOutOfScreen();
            if (!_shakeTimer.IsStopped())
            {
                Shake();
            }
        }
    }

    public void StartShake()
    {
        _shakeTimer.Start();
    }

    public void Shake()
    {
        _camera2D.Offset = new Vector2(
            GD.RandRange(-ShakeAmount, ShakeAmount),
            GD.RandRange(-ShakeAmount, ShakeAmount)
        );
    }

    public void _disableShaking()
    {
        _camera2D.Offset = Vector2.Zero;
    }

    public void CheckPlayerOutOfScreen()
    {
        if (Target is PlayerChar)
        {
            var distance = Target.GlobalPosition.Y;
            var treshhold = (GetWindow().Size.Y * 4);
            if (distance > treshhold && !_isDead)
            {
                _isDead = true;
                _autoLoader.GameManager.GameOver();
            }
        }
    }
}