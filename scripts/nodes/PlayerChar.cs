using Godot;
using MasterofElements.scripts.models;
using MasterofElements.scripts.nodes;
using MasterofElements.scripts.singletons;
using MasterofElements.scripts.singletons.signalmanager;

public partial class PlayerChar : CharacterBody2D
{
    [Export] public float Speed = 20000.0f;
    [Export] public float JumpVelocity = -350.0f;
    [Export] public float HurtVelocity = -200.0f;
    [Export] public float AirElementJumpVelocity = -600.0f;

    private bool _isDead = false;

    private AutoLoader _autoLoader;


    private PackedScene _airElemental = GD.Load<PackedScene>("res://scenes/nodes/AirElemental.tscn");
    private PackedScene _fireElemental = GD.Load<PackedScene>("res://scenes/nodes/FireElemental.tscn");


    public const int PlattformCollisionMask = 7;

    public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    private Sprite2D _playerSprite2D;
    private AudioStreamPlayer2D _audioStreamPlayer2D;
    private AnimationPlayer _animationPlayer;
    private PlayerState _playerState;
    private bool _animationFinish = true;
    private Marker2D _elementalSummonerMarker;

    private Timer _invincibleTimer;
    private bool _invincible = false;


    public override void _Ready()
    {
        base._Ready();
        _autoLoader = new AutoLoader(this);
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        _playerSprite2D = GetNode<Sprite2D>("PlayerSprite2D");
        _audioStreamPlayer2D = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
        _airElemental.Instantiate();
        _fireElemental.Instantiate();
        _elementalSummonerMarker = GetNode<Marker2D>("ElementalSummonerMarker");
        _invincibleTimer = GetNode<Timer>("InvincibleTimer");
        _autoLoader.SignalManager.OnPlayerDead += PlayerDied;
    }

    public override void _ExitTree()
    {
        _autoLoader.SignalManager.OnPlayerDead -= PlayerDied;
    }

    public void PlayerDied()
    {
        if (_isDead)
            return;
        _isDead = true;
        SetPlayerState(PlayerState.Death);
    }

    public void HurtPlayer()
    {
        if (_invincible || _isDead)
            return;
        _invincible = true;
        _invincibleTimer.Start();
        _autoLoader.AudioService.PlaySfx("characterDamage2.mp3", this, "hurt");
        double time = _invincibleTimer.WaitTime;
        Velocity += new Vector2(0, HurtVelocity);
        _autoLoader.SignalManager.EmitSignal(SignalManager.SignalName
            .OnPlayerHurt);

        var tween = CreateTween();

        tween.TweenProperty(_playerSprite2D, "modulate:a", 0, (float)time / 4);
        tween.TweenProperty(_playerSprite2D, "modulate:a", 1, (float)time / 4);
        tween.TweenProperty(_playerSprite2D, "modulate:a", 0, (float)time / 4);
        tween.TweenProperty(_playerSprite2D, "modulate:a", 1, (float)time / 4);
    }

    public void OnInvincibleTimerTimeout()
    {
        _invincible = false;
    }

    public override void _PhysicsProcess(double delta)
    {
        MovePlayer(delta);
        MoveAndSlide();
        if (_isDead)
            return;
        HandlePlayerAnimation();
    }

    private void MovePlayer(double delta)
    {
        Vector2 velocity = Velocity;

        if (!IsOnFloor())
        {
            velocity.Y += gravity * (float)delta;
        }

        if (_isDead)
        {
            velocity.X = 0;
            Velocity = velocity;
            return;
        }

        if (Input.IsActionPressed("jump") && IsOnFloor() && _playerState is PlayerState.Idle or PlayerState.Walk)
        {
            velocity.Y = JumpVelocity;
        }

        if (Input.IsActionPressed("down"))
        {
            SetCollisionMaskValue(PlattformCollisionMask, false);
        }
        else
        {
            SetCollisionMaskValue(PlattformCollisionMask, true);
        }

        Vector2 direction = Vector2.Zero;

        if (!(_playerState is
                PlayerState.SummonAir
                or PlayerState.SummonEarth
                or PlayerState.SummonFire
                or PlayerState.SummonWater))
        {
            if (Input.IsActionPressed("left"))
            {
                _playerSprite2D.FlipH = true;
                direction = Vector2.Left;
            }

            if (Input.IsActionPressed("right"))
            {
                _playerSprite2D.FlipH = false;
                direction = Vector2.Right;
            }
        }


        velocity.X = direction.X * Speed * (float)delta;

        Velocity = velocity;
    }

    private void HandlePlayerAnimation()
    {
        if (_playerState == PlayerState.Hurt || (
                _playerState is PlayerState.SummonAir or PlayerState.SummonEarth or PlayerState.SummonFire
                    or PlayerState.SummonWater))
        {
            if (!_animationFinish)
                return;
        }

        if (IsOnFloor())
        {
            if (Velocity.X == 0)
            {
                SetPlayerState(PlayerState.Idle);
            }
            else
            {
                SetPlayerState(PlayerState.Walk);
            }

            if (Input.IsActionJustPressed("summon_air") && CanSummonAirElemental())
            {
                SetPlayerState(PlayerState.SummonAir);
                _animationFinish = false;
            }

            if (Input.IsActionJustPressed("summon_water") && false)
            {
                SetPlayerState(PlayerState.SummonWater);
                _animationFinish = false;
            }

            if (Input.IsActionJustPressed("summon_fire") && CanSummonFireElemental())
            {
                SetPlayerState(PlayerState.SummonFire);
                _animationFinish = false;
            }

            if (Input.IsActionJustPressed("summon_earth") && false)
            {
                SetPlayerState(PlayerState.SummonEarth);
                _animationFinish = false;
            }
        }
        else
        {
            if (Velocity.Y >= 0)
            {
                SetPlayerState(PlayerState.Fall);
            }
            else
            {
                SetPlayerState(PlayerState.Jump);
            }
        }
    }

    public bool CanSummonAirElemental()
    {
        return _autoLoader.GameManager.AirUnlocked &&
               GetTree().GetFirstNodeInGroup(GroupNames.AirElementalRoot) == null;
    }

    public bool CanSummonFireElemental()
    {
        return _autoLoader.GameManager.FireUnlocked &&
               GetTree().GetFirstNodeInGroup(GroupNames.FireElementalRoot) == null;
    }

    public void SummonAirElemental()
    {
        if (!CanSummonAirElemental())
            return;
        SummonElemental(_airElemental, SignalManager.SignalName.OnAirStartCd);
    }

    public void SummonFireElemental()
    {
        if (!CanSummonFireElemental())
            return;
        SummonElemental(_fireElemental, SignalManager.SignalName.OnFireStartCd);
    }

    public void SummonElemental(PackedScene elemental, StringName eventName)
    {
        var spawnNode = GetTree().GetFirstNodeInGroup(GroupNames.SpawnGroup);

        if (spawnNode == null)
        {
            GD.PrintErr("SPAWN GROUP NOT FOUND!");
            return;
        }

        var summonPosition = _elementalSummonerMarker.GlobalPosition;
        if (_playerSprite2D.FlipH)
            // Offset, wenn der Charakter nach links schaut, damit es beim Stab entsteht.
            summonPosition += new Vector2(-60, 0);

        var newElemental = elemental.Instantiate<Node2D>();

        newElemental.GlobalPosition = summonPosition;
        spawnNode.AddChild(newElemental);
        _autoLoader.SignalManager.EmitSignal(eventName);
    }

    private void SetPlayerState(PlayerState newPlayerState)
    {
        if (newPlayerState == _playerState)
        {
            return;
        }

        if (_playerState == PlayerState.Fall)
        {
            if (newPlayerState == PlayerState.Idle || newPlayerState == PlayerState.Walk)
            {
                _autoLoader.AudioService.PlaySfx("PlantHit.mp3", this);
            }
        }


        _playerState = newPlayerState;

        switch (_playerState)
        {
            case PlayerState.Idle:
                _animationPlayer.Play("idle");
                break;
            case PlayerState.Walk:
                _animationPlayer.Play("walk");
                break;
            case PlayerState.Jump:
                _animationPlayer.Play("jump");
                _autoLoader.AudioService.PlaySfx("jump.mp3", this, "jump");
                break;
            case PlayerState.Fall:
                _animationPlayer.Play("fall");
                break;
            case PlayerState.Death:
                _animationPlayer.Play("death");
                _autoLoader.AudioService.PlaySfx("characterDamage2.mp3", this);
                break;
            case PlayerState.SummonEarth:
                _animationPlayer.Play("summon_earth");
                _autoLoader.AudioService.PlaySfx("Summoning2.mp3", this);
                break;
            case PlayerState.SummonAir:
                _animationPlayer.Play("summon_air");
                _autoLoader.AudioService.PlaySfx("Summoning2.mp3", this);
                break;
            case PlayerState.SummonFire:
                _animationPlayer.Play("summon_fire");
                _autoLoader.AudioService.PlaySfx("Summoning2.mp3", this);
                break;
            case PlayerState.SummonWater:
                _animationPlayer.Play("summon_water");
                _autoLoader.AudioService.PlaySfx("Summoning2.mp3", this);
                break;
            case PlayerState.Hurt:
                _autoLoader.AudioService.PlaySfx("characterDamage2.mp3", this);
                break;
        }
    }

    public PlayerState PlayerState
    {
        get => _playerState;
        set => _playerState = value;
    }

    public void PlayWalkSound()
    {
        //_autoLoader.AudioService.PlaySFX("Walk.mp3", this);
    }

    private void OnAnimationFinished(StringName stringName)
    {
        _animationFinish = true;
    }

    private void OnBodyEntered(Node2D node2D)
    {
        _handleEntered(node2D);
    }

    private void OnBodyExited(Node2D node2D)
    {
    }

    private void OnAreaEntered(Area2D area2D)
    {
        _handleEntered(area2D);
    }

    private void _handleEntered(Node2D node2D)
    {
        GD.Print("OnPlayerEntered:\n" + node2D.GetTreeStringPretty());
        if (node2D.IsInGroup(GroupNames.Enemy))
        {
            HurtPlayer();
        }

        if (node2D.IsInGroup(GroupNames.AirElemental))
        {
            AirElemental airElemental = (AirElemental)GetTree().GetFirstNodeInGroup(GroupNames.AirElementalRoot);
            if (airElemental.CanBounce() && !IsOnFloor())
            {
                airElemental.CallOnBounce();
                Velocity = new Vector2(Velocity.X, AirElementJumpVelocity);
                _autoLoader.AudioService.PlaySfx("windHurt.wav", this);
            }
        }
    }

    private void OnAreaExited(Area2D area2D)
    {
    }
}