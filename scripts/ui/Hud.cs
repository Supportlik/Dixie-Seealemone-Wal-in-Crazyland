using Godot;
using System;

public partial class Hud : CanvasLayer
{
    private HBoxContainer _heartContainerList;
    private Label _score;
    private PackedScene _heartContainer;

    public override void _Ready()
    {
        _heartContainer = GD.Load<PackedScene>("res://scenes/ui/heart_container.tscn");
        _heartContainerList = GetNode<HBoxContainer>("VBoxContainer/MarginContainer/HpContainer");
        _score = GetNode<Label>("VBoxContainer/MarginContainer/Label");
    }

    public void SetScore(int score){
        _score.Text = "Score: " + score.ToString();
    }

    public void SetHP(int hp){
        for (int i = 0; i < hp; i++)
        {
            _heartContainerList.AddChild(_heartContainer.Instantiate<TextureRect>());
        }
    }
}
