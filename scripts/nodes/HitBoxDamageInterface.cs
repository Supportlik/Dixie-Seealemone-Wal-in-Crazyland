using Godot;

public partial class HitBoxDamageInterface : Area2D
{
    [Signal]
    public delegate void OnDamageReceivedEventHandler(int damage);

    public void OnDamage(int damage)
    {
        EmitSignal(SignalName.OnDamageReceived, damage);
    }
}