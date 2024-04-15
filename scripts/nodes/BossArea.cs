using Godot;
using MasterofElements.scripts.models;

public partial class BossArea : Area2D
{
    private Timer _platformTimer;

    private bool _showPlatform = true;

    public override void _Ready()
    {
        _platformTimer = GetNode<Timer>("PlatformTimer");
    }


    public void OnPlatformTimerTimeout()
    {
        _showPlatform = !_showPlatform;
        MovablePlatform platform = (MovablePlatform)GetTree().GetFirstNodeInGroup(GroupNames.BossPlatform);

        if (_showPlatform)
        {
            var tween = CreateTween();
            tween.TweenCallback(Callable.From(() => platform.SetCollisionLayerValue(7, true)));
            tween.TweenProperty(platform, "modulate", new Color(1, 1, 1, 1), 1f);
        }
        else
        {
            var tween = CreateTween();
            tween.TweenProperty(platform, "modulate", new Color(1, 1, 1, 0), 1f);
            tween.TweenCallback(Callable.From(() => platform.SetCollisionLayerValue(7, false)));
        }
    }

    public void BossAreaEntered(Node2D other)
    {
        GD.Print("Boss Area Entered!");
        ((Boss)GetTree().GetFirstNodeInGroup(GroupNames.Boss)).Spawn();
    }
}