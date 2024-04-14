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

    [Export] public float Speed = 500;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _autoloader = new AutoLoader(this);
        _animationTree = GetNode<AnimationTree>("AnimationTree");
        _playerClickMarker = GetNode<Marker2D>("PlayerClickMarker");
        _elementalChar = GetNode<Node2D>("ElementalChar");
        _elementalState = ElementalState.OnIdle;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        HandleInput();
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("move_summon") && Input.InputEventMouseButton)
        {
            var mousePosition = GetGlobalMousePosition();
            
            
            
            _playerClickMarker.GlobalPosition = mousePosition;
        }

        if (Input.IsActionJustPressed("event_summon"))
        {
            
        }    
    }



    public void SetState(ElementalState state)
    {
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