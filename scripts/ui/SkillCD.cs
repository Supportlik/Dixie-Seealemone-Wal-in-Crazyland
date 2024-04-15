using Godot;

namespace MasterofElements.scripts.ui;

public partial class SkillCD : Control
{
    [Export] public string Key;
    [Export] public CompressedTexture2D Icon;
    [Export] public float Cooldown;

    private Timer _timer;
    private Label _keyLabel;
    private Label _cdLabel;
    private ColorRect _cdOverlay;
    private TextureRect _icon;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _timer = GetNode<Timer>("Timer");
        _keyLabel = GetNode<Label>("MarginContainer/KeyLabel");
        _cdLabel = GetNode<Label>("Control/MarginContainer/CDLabel");
        _cdOverlay = GetNode<ColorRect>("Control/CDOverlay");
        _icon = GetNode<TextureRect>("Control/IconTextureRect");

        _keyLabel.Text = Key;
        _timer.WaitTime = Cooldown;
        _icon.Texture = Icon;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (!_timer.IsStopped())
        {
            HandleCdOverlay();
        }
    }

    public void TimedOut()
    {
        _cdLabel.Hide();
        _cdOverlay.Hide();
    }

    private void HandleCdOverlay()
    {
        _cdLabel.Text = _doubleToString(_timer.TimeLeft);
    }

    public void StartCd()
    {
        _cdLabel.Show();
        _cdOverlay.Show();
        _cdLabel.Text = _doubleToString(Cooldown);
        _cdOverlay.Modulate = new Color(1, 1, 1, 1);
        _timer.Start();
    }

    public string _doubleToString(double value)
    {
        return Mathf.Floor(value).ToString("F0");
    }
}