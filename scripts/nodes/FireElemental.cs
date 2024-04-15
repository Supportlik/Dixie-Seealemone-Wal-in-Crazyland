using System.Collections.Generic;
using Godot;
using MasterofElements.scripts.models;
using MasterofElements.scripts.singletons;

public partial class FireElemental : Node2D
{
    private AutoLoader _autoloader;
    private AnimationTree _animationTree;
    private Marker2D _playerClickMarker;
    private CharacterBody2D _elementalChar;
    private Sprite2D _sprite;
    [Export] public int DamageOnAttack = 150;

    [Export] public float Speed = 3000;
    [Export] public float Gravity = 8500.0f;

    private List<HitBoxDamageInterface> _hitBoxDamageInterfaces = new();

    public override void _Ready()
    {
        _autoloader = new AutoLoader(this);
        _animationTree = GetNode<AnimationTree>("AnimationTree");
        _playerClickMarker = GetNode<Marker2D>("PlayerClickMarker");
        _elementalChar = GetNode<CharacterBody2D>("ElementalChar");
        _sprite = GetNode<Sprite2D>("ElementalChar/Sprites/Sprite2DFireElemental");
    }

    public override void _PhysicsProcess(double delta)
    {
        HandleMovement(delta);
        _elementalChar.MoveAndSlide();
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("move_summon") && !IsExploding())
        {
            var mousePosition = GetGlobalMousePosition();
            _playerClickMarker.GlobalPosition = mousePosition;
            CallOnWalk(true);
        }

        if (Input.IsActionJustPressed("event_summon") && !IsExploding())
        {
            StartExplosion();
        }
    }

    public void StartExplosion()
    {
        _elementalChar.Velocity = Vector2.Zero;
        CallOnWalk(false);
        CallOnExplode(true);
    }

    public void HandleMovement(double delta)
    {
        var velocity = _elementalChar.Velocity;
        if (IsWalking())
        {
            var direction = (_playerClickMarker.GlobalPosition - _elementalChar.GlobalPosition).Normalized() *
                            new Vector2(1, 0);
            if (direction.X >= 0)
            {
                _sprite.FlipH = false;
            }
            else
            {
                _sprite.FlipH = true;
            }

            velocity.X = direction.X * Speed * (float)delta;

            var elementalXPosition = new Vector2(_elementalChar.GlobalPosition.X, 0);
            var playerClickMarkerXPosition = new Vector2(_playerClickMarker.GlobalPosition.X, 0);

            if (elementalXPosition.DistanceTo(playerClickMarkerXPosition) < 5)
            {
                velocity.X = 0;
                CallOnWalk(false);
            }
        }

        if (!_elementalChar.IsOnFloor())
        {
            velocity.Y = Gravity * (float)delta;
        }

        _elementalChar.Velocity = velocity;
    }

    public void Explode()
    {
        _autoloader.AudioService.PlaySfx("PlantDeath.mp3", this);

        var queuedForDeletion = _hitBoxDamageInterfaces.FindAll(hitBoxDamageInterface =>
            hitBoxDamageInterface == null || hitBoxDamageInterface.IsQueuedForDeletion());

        queuedForDeletion.ForEach(hitBoxDamageInterface =>
            _hitBoxDamageInterfaces.Remove(hitBoxDamageInterface));

        _hitBoxDamageInterfaces.ForEach(hitBoxDamageInterface => hitBoxDamageInterface.OnDamage(DamageOnAttack));
        _hitBoxDamageInterfaces.Clear();
    }

    public void CallOnWalk(bool walk)
    {
        _animationTree.Set("parameters/conditions/on_walk", walk);
    }

    public void CallOnExplode(bool explode)
    {
        _animationTree.Set("parameters/conditions/on_explode", explode);
    }

    public bool IsWalking()
    {
        return (bool)_animationTree.Get("parameters/conditions/on_walk");
    }

    public bool IsExploding()
    {
        return (bool)_animationTree.Get("parameters/conditions/on_explode");
    }

    public void OnDieTimerTimeout()
    {
        if(!IsExploding())
            StartExplosion();
        QueueFree();
    }

    public void HideAfterExplosion()
    {
        Hide();
    }

    public void OnEnemyEntered(Area2D other)
    {
        GD.Print("OnAirElementalEntered:\n" + other.GetTreeStringPretty());
        if (other is HitBoxDamageInterface hitBoxDamageInterface && other.IsInGroup(GroupNames.Enemy))
        {
            _hitBoxDamageInterfaces.Add(hitBoxDamageInterface);
        }
    }

    public void OnEnemyExited(Area2D other)
    {
        GD.Print("OnAirElementalExited:\n" + other.GetTreeStringPretty());
        if (other is HitBoxDamageInterface hitBoxDamageInterface && other.IsInGroup(GroupNames.Enemy))
        {
            _hitBoxDamageInterfaces.Remove(hitBoxDamageInterface);
        }
    }
}