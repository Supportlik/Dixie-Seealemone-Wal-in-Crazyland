using Godot;

public partial class SignalManager : Node
{
    [Signal]
    public delegate void OnGameOverEventHandler();

    [Signal]
    public delegate void OnLevelCompleteEventHandler();

    [Signal]
    public delegate void OnPlayerHurtEventHandler();
}