using Godot;
using Godot.Collections;

namespace MasterofElements.scripts.singletons.sceneloader;

/// <summary>
/// Die SceneSwitcherService Klasse ist verantwortlich für das Laden und Wechseln von Szenen.
/// </summary>
public partial class SceneSwitcherService : Node
{
    /// <summary>
    /// Ein Wörterbuch, das alle geladenen Szenen speichert. Der Schlüssel ist der Pfad zur Szene und der Wert ist die geladene Szene.
    /// </summary>
    Dictionary<string, PackedScene> _allLoadedScenes = new();

    /// <summary>
    /// Wird aufgerufen, wenn die Node bereit ist. Lädt alle Szenen und speichert sie im _allLoadedScenes Wörterbuch.
    /// </summary>
    public override void _Ready()
    {
        foreach (var sceneInfo in AllScenes.AllScenesList)
        {
            var scene = GD.Load<PackedScene>(sceneInfo.ScenePath);
            scene.Instantiate();
            _allLoadedScenes.Add(sceneInfo.ScenePath, scene);
        }
    }

    /// <summary>
    /// Wechselt die aktuelle Szene zur angegebenen Szene.
    /// </summary>
    /// <param name="sceneInfo">Die Szene, zu der gewechselt werden soll.</param>
    public void SwitchScene(SceneInfo sceneInfo)
    {
        GetTree().ChangeSceneToPacked(_allLoadedScenes[sceneInfo.ScenePath]);
    }
}