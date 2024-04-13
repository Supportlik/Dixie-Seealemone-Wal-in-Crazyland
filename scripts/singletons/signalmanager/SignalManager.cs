using Godot;
using System;

public partial class SignalManager : Node
{
	[Signal]
	public delegate void OnGameOverSignalHandler();
	
	[Signal]
	public delegate void OnLevelCompleteSignalHandler();
}
