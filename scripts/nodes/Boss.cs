using Godot;
using MasterofElements.scripts.models;
using MasterofElements.scripts.singletons;
using MasterofElements.scripts.singletons.score;
using MasterofElements.scripts.singletons.signalmanager;

public partial class Boss : CharacterBody2D
{
    [Export] public int Hp = 1000;
    [Export] public int MaxFireSlimeCount = 10;
    [Export] public float Gravity = 8500.0f;
    [Export] public float MoveSpeed = 4500;

    private AutoLoader _autoLoader;
    private AnimationTree _animationTree;
    private Marker2D _newSlimeMarker;
    private Timer _attackTimer;
    private Timer _explosionTimer;
    private Timer _invincibleTimer;
    private Facing _facing = Facing.Left;
    private Sprite2D _bossSprite;
    private Area2D _hitBox;
    private Area2D _explosion;


    private bool invincible = false;
    private bool isDead = false;

    private PackedScene _fireSlimeScene = GD.Load<PackedScene>("res://scenes/nodes/slime.tscn");

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _autoLoader = new AutoLoader(this);
        _animationTree = GetNode<AnimationTree>("AnimationTree");
        _newSlimeMarker = GetNode<Marker2D>("FireBossChar/BossSprite/NewSlimeMarker");
        _fireSlimeScene.Instantiate();
        _attackTimer = GetNode<Timer>("AttackTimer");
        _explosionTimer = GetNode<Timer>("ExplosionTimer");
        _invincibleTimer = GetNode<Timer>("InvincibleTimer");
        _bossSprite = GetNode<Sprite2D>("FireBossChar/BossSprite");
        _hitBox = GetNode<Area2D>("HitBoxDamage");
        _explosion = GetNode<Area2D>("Explosion");
        _explosion.SetCollisionLayerValue(2, false);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;
        if (!IsOnFloor())
        {
            velocity.Y = Gravity * (float)delta;
        }

        if (Hp <= 0)
        {
            Die();
            return;
        }


        if (!IsExplode() && !IsAttack() && !IsDeath())
        {
            velocity.X = (int)_facing * MoveSpeed * (float)delta * -1;
        }

        Velocity = velocity;
        MoveAndSlide();

        if (IsOnFloor() && IsOnWall())
        {
            FlipDirection();
        }
    }

    private void FlipDirection()
    {
        _bossSprite.FlipH = !_bossSprite.FlipH;

        if (_facing == Facing.Left)
        {
            _facing = Facing.Right;
        }
        else
        {
            _facing = Facing.Left;
        }
    }


    public void Spawn()
    {
        GD.Print("Boss Spawn!");
        Show();
        _attackTimer.Start();
        _explosionTimer.Start();
        var spawnNode = GetTree().GetFirstNodeInGroup(GroupNames.SpawnGroup);
        if (spawnNode == null)
        {
            GD.PrintErr("SPAWN GROUP NOT FOUND!");
        }

        _autoLoader.AudioService.StopPlayBackAndQueueFree(spawnNode, "background_music_game");
        _autoLoader.AudioService.PlayMusic("Ludum_Dare_55_Game_Boss_Music.mp3", spawnNode, "background_music_game2");
    }


    public void StartAttack()
    {
        if (!IsExplode() && !IsDeath() && CanAttack())
            SetAttack(true);
    }

    public void StartExplosion()
    {
        if (!IsExplode() && !IsDeath() && CanExplode())
        {
            _autoLoader.AudioService.PlaySfx("explosion.mp3", this);
            _invincibleTimer.Start();
            invincible = true;
            SetExplode(true);
            _explosion.SetCollisionLayerValue(2, true);
        }
    }

    public void AfterExplosion()
    {
        SetExplode(false);
        _explosion.SetCollisionLayerValue(2, false);
    }

    public bool CanExplode()
    {
        return !IsExplode();
    }

    public int GetFireSlimeCount()
    {
        return GetTree().GetNodesInGroup(GroupNames.SummonSlime).Count;
    }

    public void SpawnSlime()
    {
        SetAttack(false);
        if (!CanAttack()) return;

        var newSlime = _fireSlimeScene.Instantiate<Slime>();
        newSlime.AddToGroup(GroupNames.SummonSlime);
        var spawnNode = GetTree().GetFirstNodeInGroup(GroupNames.SpawnGroup);

        if (spawnNode == null)
        {
            GD.PrintErr("SPAWN GROUP NOT FOUND!");
            return;
        }

        newSlime.GlobalPosition = _newSlimeMarker.GlobalPosition;
        spawnNode.AddChild(newSlime);
    }

    public bool CanAttack()
    {
        return GetFireSlimeCount() < MaxFireSlimeCount;
    }

    public bool IsAttack()
    {
        return (bool)_animationTree.Get("parameters/conditions/on_attack");
    }

    public void SetAttack(bool attack)
    {
        _animationTree.Set("parameters/conditions/on_attack", attack);
    }

    public bool IsDeath()
    {
        return (bool)_animationTree.Get("parameters/conditions/on_death");
    }

    public void SetDeath(bool death)
    {
        _animationTree.Set("parameters/conditions/on_death", death);
    }

    public bool IsExplode()
    {
        return (bool)_animationTree.Get("parameters/conditions/on_explode");
    }

    public void SetExplode(bool explode)
    {
        _animationTree.Set("parameters/conditions/on_explode", explode);
    }

    public void AfterInvincibleTimer()
    {
        invincible = false;
    }


    public void TakeDamage(int damage)
    {
        if (invincible) return;
        invincible = true;
        var tween1 = CreateTween();
        // Make the sprite flash
        tween1.TweenProperty(_bossSprite, "modulate", new Color(0.2f, 0.2f, 0.2f, 1), 0.1f);
        tween1.TweenProperty(_bossSprite, "modulate", new Color(1, 1, 1, 1), 0.1f);
        var tween2 = CreateTween();
        // Make the sprite jump
        tween2.TweenProperty(_bossSprite, "position",
            new Vector2(_bossSprite.Position.X, _bossSprite.Position.Y - 3), 0.2f);
        tween2.TweenProperty(_bossSprite, "position", new Vector2(_bossSprite.Position.X, _bossSprite.Position.Y),
            0.2f);

        StartExplosion();

        Hp -= damage;
        if (Hp <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;
        SetDeath(true);
        _autoLoader.ScoreService.OnScoreEvent(5000, ScoreType.GameCompleted);

        var spawnNode = GetTree().GetFirstNodeInGroup(GroupNames.SpawnGroup);

        if (spawnNode == null)
        {
            GD.PrintErr("SPAWN GROUP NOT FOUND!");
            return;
        }

        _autoLoader.AudioService.StopPlayBackAndQueueFree(spawnNode, "background_music_game2");
        _autoLoader.AudioService.PlayMusic("Ludum_Dare_55_Menu_Music.mp3", spawnNode, "background_music_game3");


        var tween = CreateTween();
        tween.TweenInterval(0.3);
        tween.TweenCallback(Callable.From(() => _autoLoader.AudioService.PlaySfx("pickupCoin.mp3", spawnNode)));
        tween.TweenInterval(0.3);
        tween.TweenCallback(Callable.From(() => _autoLoader.AudioService.PlaySfx("pickupCoin.mp3", spawnNode)));
        tween.TweenInterval(0.3);
        tween.TweenCallback(Callable.From(() => _autoLoader.AudioService.PlaySfx("pickupCoin.mp3", spawnNode)));

        tween.TweenInterval(1);
        tween.TweenCallback(Callable.From(DestroyAndSignal));
    }

    public void DestroyAndSignal()
    {
        QueueFree();
        _autoLoader.SignalManager.EmitSignal(SignalManager.SignalName.OnLevelComplete);
    }
}