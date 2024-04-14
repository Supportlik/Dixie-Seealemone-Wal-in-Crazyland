using Godot;
using MasterofElements.scripts.models;
using MasterofElements.scripts.singletons;

namespace MasterofElements.scripts.nodes;

public partial class AirElemental : Node2D
{
    private AutoLoader _autoloader;
    private AnimationTree _animationTree;
    private Marker2D _playerClickMarker;

    private ElementalState _elementalState;
    private Node2D _elementalChar;
    private Sprite2D _sprite;

    [Export] public float Speed = 500;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _autoloader = new AutoLoader(this);
        _animationTree = GetNode<AnimationTree>("AnimationTree");
        _playerClickMarker = GetNode<Marker2D>("PlayerClickMarker");
        _elementalChar = GetNode<Node2D>("ElementalChar");
        _sprite = GetNode<Sprite2D>("ElementalChar/Sprites/Sprite2DWolke");
        _elementalState = ElementalState.OnIdle;
    }

    public override void _PhysicsProcess(double delta)
    {
        HandleMovement(delta);
    }

    public void HandleMovement(double delta)
    {
        if (_elementalState == ElementalState.OnFly)
        {
            var direction = (_playerClickMarker.GlobalPosition - _elementalChar.GlobalPosition).Normalized();
            if (direction.X >= 0)
            {
                _sprite.FlipH = true;
            }
            else
            {
                _sprite.FlipH = false;
            }

            _elementalChar.GlobalPosition += direction * Speed * (float)delta;

            if (_elementalChar.GlobalPosition.DistanceTo(_playerClickMarker.GlobalPosition) < 5)
            {
                SetState(ElementalState.OnIdle);
                CallOnFly(false);
            }
        }
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("move_summon"))
        {
            var mousePosition = GetGlobalMousePosition();
            _playerClickMarker.GlobalPosition = mousePosition;
            CallOnFly(true);
        }

        if (Input.IsActionJustPressed("event_summon"))
        {
        }
    }


    public void SetState(ElementalState state)
    {
        var oldState = _elementalState;
        var newState = state;
        _elementalState = state;

        switch (_elementalState)
        {
            // muss bei überschneidung gesetzt werden.
            case ElementalState.OnAttack:
                break;
            // muss bei jump auf das elementar gesetzt werden.
            case ElementalState.OnBounce:
                break;
            // nicht benötigt animation player
            case ElementalState.OnDisapear:
                break;
            // muss von animation gesetzt werden, wenn man fliegt und muss als check genutzt werden, um zu sehen, ob das elementar ankommt.
            case ElementalState.OnFly:
                break;
            // muss gesetzt werden, wenn das elementar ankommt.
            case ElementalState.OnIdle:
            default:
                break;
        }
    }

    public void CallOnFly(bool fly)
    {
        _animationTree.Set("parameters/conditions/on_fly", fly);
    }

    public void CallOnAttack(bool attack)
    {
        _animationTree.Set("parameters/conditions/on_attack", attack);
    }

    public void CallDisapear()
    {
        _animationTree.Set("parameters/conditions/on_disapear", true);
        SetState(ElementalState.OnDisapear);
    }

    public void CallOnBounce()
    {
        _animationTree.Set("parameters/conditions/on_bounce", true);
    }

    public void CallOnBounceComplete()
    {
        _animationTree.Set("parameters/conditions/on_bounce", false);
    }


    public void Die()
    {
        QueueFree();
    }
}