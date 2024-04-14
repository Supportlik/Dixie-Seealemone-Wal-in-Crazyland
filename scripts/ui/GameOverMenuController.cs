using Godot;
using MasterofElements.scripts.singletons;
using MasterofElements.scripts.singletons.sceneloader;
using System;

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
    }

    private void _on_menu_button_pressed(){
         _autoLoader.SceneSwitcherService.SwitchScene(AllScenes.StartMenu);
    }

    private void _on_restart_button_pressed(){
        GetTree().ReloadCurrentScene();
    }

    public void SetTitle(string title){
        _title.Text = title;
    }

    public void SetSubTitle(string subTitle){
        _subTitle.Text = subTitle;
    }

    public void SetScore(int score){
        _scoreLabel.Text = score.ToString();
    }
}
