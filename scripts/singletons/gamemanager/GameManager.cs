using Godot;
using MasterofElements.scripts.singletons;
using MasterofElements.scripts.singletons.signalmanager;

public partial class GameManager : Node
{
    private int _initialHearts = 5;
    private int _health = 5;
    private float _secondsAfterDeadBeforeGameOver = 1.5f;

    private bool _airUnlocked = false;
    private bool _fireUnlocked = false;

    private AutoLoader _autoLoader;


    public int InitialHearts => _initialHearts;

    public int Health => _health;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _autoLoader = new AutoLoader(this);
        _autoLoader.SignalManager.OnPlayerHurt += InflictPlayerDamge;
        _autoLoader.SignalManager.OnLevelComplete += LevelCompleted;
        _autoLoader.SignalManager.OnAirUnlocked += _unlockAir;
        _autoLoader.SignalManager.OnFireUnlocked += _unlockFire;
    }

    public override void _ExitTree()
    {
        _autoLoader.SignalManager.OnPlayerHurt -= InflictPlayerDamge;
        _autoLoader.SignalManager.OnLevelComplete -= LevelCompleted;
        _autoLoader.SignalManager.OnAirUnlocked -= _unlockAir;
        _autoLoader.SignalManager.OnFireUnlocked -= _unlockFire;
    }

    private void _unlockAir()
    {
        _airUnlocked = true;
    }

    private void _unlockFire()
    {
        _fireUnlocked = true;
    }


    public bool AirUnlocked => _airUnlocked;

    public bool FireUnlocked => _fireUnlocked;

    public void InflictPlayerDamge()
    {
        _health--;
        _autoLoader.SignalManager.EmitSignal(
            SignalManager.SignalName.OnPlayerSetHp, _health);
        if (!IsGameOver()) return;
        GameOver();
    }

    public void GameOver()
    {
        _health = 0;
        _autoLoader.SignalManager.EmitSignal(SignalManager.SignalName
            .OnPlayerDead);
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
        _airUnlocked = false;
        _fireUnlocked = false;
        _autoLoader.SignalManager.EmitSignal(
            SignalManager.SignalName.OnPlayerSetHp, _initialHearts);
    }
}