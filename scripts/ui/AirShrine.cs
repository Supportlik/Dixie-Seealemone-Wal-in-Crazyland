using Godot;
using MasterofElements.scripts.singletons;
using MasterofElements.scripts.singletons.signalmanager;

public partial class AirShrine : Node2D
{
    protected AutoLoader _autoLoader;
    private AnimatedSprite2D _airElemental;

    public virtual string GetDialogText()
    {
        return
            "Found the Air Shrine!\n Press '1' to summon me. I will help you on your journey.\n\n Jump on me to reach the clouds. \nYou can send me to your Enemies with 'left mouse click'. \n Unlock your limitless potential!";
    }


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _autoLoader = new AutoLoader(this);
        _airElemental = GetNode<AnimatedSprite2D>("Elemental");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public void OnBodyEntered(Node2D other)
    {
        _autoLoader.DialogManager.ShowDialog(GetDialogText());
        _autoLoader.AudioService.PlaySfx("pickupCoin.mp3", this);
        _airElemental.Show();
        _airElemental.Play("summon");
        Unlock();
    }

    public virtual void Unlock()
    {
        if (!_autoLoader.GameManager.AirUnlocked)
        {
            _autoLoader.SignalManager.EmitSignal(SignalManager.SignalName.OnAirUnlocked);
        }
    }

    public void OnBodyExited(Node2D other)
    {
        _autoLoader.DialogManager.HideDialog();
        _airElemental.Hide();
    }
}