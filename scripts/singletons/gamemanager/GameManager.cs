using Godot;
using MasterofElements.scripts.singletons;

public partial class GameManager : Node
{
    private int _initialHearts = 5;
    private int _health = 5;
    private float _secondsAfterDeadBeforeGameOver = 1.5f;

    private AutoLoader _autoLoader;


    public int InitialHearts => _initialHearts;

    public int Health => _health;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _autoLoader = new AutoLoader(this);
        _autoLoader.SignalManager.OnPlayerHurt += InflictPlayerDamge;
        _autoLoader.SignalManager.OnLevelComplete += LevelCompleted;
    }

    public override void _ExitTree()
    {
        _autoLoader.SignalManager.OnPlayerHurt -= InflictPlayerDamge;
        _autoLoader.SignalManager.OnLevelComplete -= LevelCompleted;
    }

    public void InflictPlayerDamge()
    {
        _health--;
        _autoLoader.SignalManager.EmitSignal(SignalManager.SignalName.OnPlayerSetHp, _health);
        if (!IsGameOver()) return;

        _autoLoader.SignalManager.EmitSignal(SignalManager.SignalName.OnPlayerDead);
        var tween = CreateTween();
        tween.TweenInterval(_secondsAfterDeadBeforeGameOver);
        tween.TweenCallback(Callable.From(() =>
            _autoLoader.SignalManager.EmitSignal(SignalManager.SignalName.OnGameOver)));
    }

    public void LevelCompleted()
    {
        var tween = CreateTween();
        tween.TweenInterval(_secondsAfterDeadBeforeGameOver);
        tween.TweenCallback(Callable.From(() =>
            _autoLoader.SignalManager.EmitSignal(SignalManager.SignalName.OnGameOver)));
    }

    public bool IsGameOver()
    {
        return _health <= 0;
    }

    public void StartGame()
    {
        _health = _initialHearts;
        _autoLoader.ScoreService.ResetScore();
        _autoLoader.SignalManager.EmitSignal(SignalManager.SignalName.OnPlayerSetHp, _initialHearts);
    }
}