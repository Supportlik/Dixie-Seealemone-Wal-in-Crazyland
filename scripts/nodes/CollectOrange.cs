using Godot;
using MasterofElements.scripts.models;
using MasterofElements.scripts.singletons;
using MasterofElements.scripts.singletons.score;

public partial class CollectOrange : Area2D
{
    [Export] public int Score = 50;
    [Export] public float JumpVelocity = -30f;

    private bool _collected = false;

    private AutoLoader _autoLoader;

    public override void _Ready()
    {
        _autoLoader = new AutoLoader(this);
    }

    public void OnAreaEntered(Area2D other)
    {
        if (!_collected && other.IsInGroup(GroupNames.Player))
        {
            _collected = true;
            _autoLoader.AudioService.PlaySfx("pickupCoin.mp3", other);
            _autoLoader.ScoreService.OnScoreEvent(Score, ScoreType.PickupCollected);

            var tween = CreateTween();
            tween.TweenProperty(this, "position", new Vector2(Position.X, Position.Y + JumpVelocity), 0.2f);
            tween.TweenProperty(this, "position", new Vector2(Position.X, Position.Y), 0.4);
            tween.TweenCallback(
                Callable.From(
                    Die
                )
            );
        }
    }

    public void Die()
    {
        QueueFree();
    }
}