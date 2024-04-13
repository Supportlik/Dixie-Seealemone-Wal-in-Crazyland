using System.Collections.Generic;
using Godot;

public partial class PackedSceneFactory : Node
{
    // private PackedScene _scene = GD.Load<PackedScene>("res://scenes/scene.tscn");

    private List<PackedScene> _scenes = new()
    {
    };

    public override void _Ready()
    {
        // _scenes.Add(_scene);
        _scenes.ForEach(scene => scene.Instantiate());
    }

    public void AddSceneAtPosition(PackedScene packedScene, Vector2 globalPosition, Node parent)
    {
        var sceneInstance = packedScene.Instantiate<Node2D>();
        sceneInstance.GlobalPosition = globalPosition;
        parent.AddChild(sceneInstance);
    }
}