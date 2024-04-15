using Godot;
using MasterofElements.scripts.singletons;

namespace MasterofElements.scripts.ui;

public partial class Hud : CanvasLayer
{
    private HBoxContainer _heartContainerList;
    private Label _score;
    private PackedScene _heartContainer;
    private AutoLoader _autoLoader;
    private SkillCD _airCD;
    private SkillCD _fireCD;

    public override void _Ready()
    {
        _heartContainer = GD.Load<PackedScene>("res://scenes/ui/heart_container.tscn");
        _heartContainerList = GetNode<HBoxContainer>("VBoxContainer/MarginContainer/HpContainer");
        _score = GetNode<Label>("VBoxContainer/MarginContainer/Label");
        _airCD = GetNode<SkillCD>("VBoxContainer/MarginContainer2/HBoxContainer/AirCD");
        _fireCD = GetNode<SkillCD>("VBoxContainer/MarginContainer2/HBoxContainer/FireCD");

        _autoLoader = new AutoLoader(this);
        _autoLoader.SignalManager.OnScoreChanged += UpdateScoreFromScoreService;
        _autoLoader.SignalManager.OnPlayerSetHp += SetHP;
        _autoLoader.SignalManager.OnAirUnlocked += ShowAirCd;
        _autoLoader.SignalManager.OnFireUnlocked += ShowFireCd;
        _autoLoader.SignalManager.OnAirStartCd += StartAirCd;
        _autoLoader.SignalManager.OnFireStartCd += StartFireCd;
    }


    public override void _ExitTree()
    {
        _autoLoader.SignalManager.OnScoreChanged -= UpdateScoreFromScoreService;
        _autoLoader.SignalManager.OnPlayerSetHp -= SetHP;
        _autoLoader.SignalManager.OnAirUnlocked -= ShowAirCd;
        _autoLoader.SignalManager.OnFireUnlocked -= ShowFireCd;
        _autoLoader.SignalManager.OnAirStartCd -= StartAirCd;
        _autoLoader.SignalManager.OnFireStartCd -= StartFireCd;
    }


    public void ShowAirCd()
    {
        _airCD.Show();
    }

    public void ShowFireCd()
    {
        _fireCD.Show();
    }

    public void StartAirCd()
    {
        _airCD.StartCd();
    }

    public void StartFireCd()
    {
        _fireCD.StartCd();
    }


    private void ClearHearts()
    {
        foreach (var node in _heartContainerList.GetChildren())
        {
            _heartContainerList.RemoveChild(node);
        }
    }

    private void UpdateScoreFromScoreService()
    {
        SetScore(_autoLoader.ScoreService.Score);
    }

    public void SetScore(int score)
    {
        _score.Text = "Score: " + score.ToString("D5");
    }

    public void SetHP(int hp)
    {
        ClearHearts();
        for (int i = 0; i < hp; i++)
        {
            _heartContainerList.AddChild(_heartContainer.Instantiate<TextureRect>());
        }
    }
}