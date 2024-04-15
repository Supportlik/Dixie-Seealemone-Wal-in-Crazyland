using Godot;

namespace MasterofElements.scripts.singletons.signalmanager;

public partial class SignalManager : Node
{
    [Signal]
    public delegate void OnGameOverEventHandler();

    [Signal]
    public delegate void OnLevelCompleteEventHandler();

    [Signal]
    public delegate void OnPlayerHurtEventHandler();

    [Signal]
    public delegate void OnPlayerSetHpEventHandler(int hearts);

    [Signal]
    public delegate void OnPlayerDeadEventHandler();

    [Signal]
    public delegate void OnScoreChangedEventHandler();
    
    [Signal]
    public delegate void OnAirUnlockedEventHandler();
    
    [Signal]
    public delegate void OnFireUnlockedEventHandler();
    
    [Signal]
    public delegate void OnAirStartCdEventHandler();
    
    [Signal]
    public delegate void OnFireStartCdEventHandler();
    
    
}