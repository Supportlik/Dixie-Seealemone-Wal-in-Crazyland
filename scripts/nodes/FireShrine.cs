using MasterofElements.scripts.singletons.signalmanager;

public partial class FireShrine : AirShrine
{
    public override string GetDialogText()
    {
        return "Found the Fire Shrine!\n Press '2' to summon me. I will help you on your journey.\n\n You can send me to your Enemies with 'left mouse click'. \n Burn it all down!";
        ;
    }
    
    public override void Unlock()
    {
        if (!_autoLoader.GameManager.FireUnlocked)
        {
            _autoLoader.SignalManager.EmitSignal(SignalManager.SignalName.OnFireUnlocked);
        }
    }
}