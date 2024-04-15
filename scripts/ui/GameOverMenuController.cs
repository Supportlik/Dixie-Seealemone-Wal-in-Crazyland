using Godot;
using MasterofElements.scripts.singletons;
using MasterofElements.scripts.singletons.sceneloader;

public partial class GameOverMenuController : VBoxContainer
{
    private AutoLoader _autoLoader;

    private Label _title;
    private Label _subTitle;

    private Label _scoreLabel;

    public override void _Ready()
    {
        _autoLoader = new AutoLoader(this);
        _title = GetNode<Label>("VBoxContainer2/GameOverTitle");
        _subTitle = GetNode<Label>("VBoxContainer2/GameOverSubTitle");
        _scoreLabel = GetNode<Label>("VBoxContainer2/ScoreLabel");
        _autoLoader.SignalManager.OnScoreChanged += OnScoreChange;
        _autoLoader.SignalManager.OnLevelComplete += SetLevelCompleted;
        OnScoreChange();
    }

    public override void _ExitTree()
    {
        _autoLoader.SignalManager.OnScoreChanged -= OnScoreChange;
        _autoLoader.SignalManager.OnLevelComplete -= SetLevelCompleted;
    }

    private void OnScoreChange()
    {
        SetScore(_autoLoader.ScoreService.Score);
    }

    private void _on_menu_button_pressed()
    {
        _autoLoader.SceneSwitcherService.SwitchScene(AllScenes.StartMenu);
    }

    private void _on_restart_button_pressed()
    {
        GetTree().ReloadCurrentScene();
    }

    public void SetLevelCompleted()
    {
        SetTitle("Level Completed");
        SetSubTitle("Congratulations! Master of Elements!");
    }

    public void SetTitle(string title)
    {
        _title.Text = title;
    }

    public void SetSubTitle(string subTitle)
    {
        _subTitle.Text = subTitle;
    }

    public void SetScore(int score)
    {
        _scoreLabel.Text = score.ToString("D5");
    }
}