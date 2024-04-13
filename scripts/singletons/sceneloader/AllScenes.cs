using System.Collections.Generic;

namespace MasterofElements.scripts.singletons.sceneloader;

/// <summary>
/// Die AllScenes Klasse enthält Informationen über alle Szenen im Projekt.
/// </summary>
public class AllScenes
{
    /// <summary>
    /// Das Hauptmenü.
    /// </summary>
    public static readonly SceneInfo MainMenu = new("res://scenes/main.tscn"),
        SecondScene = new SceneInfo("res://scenes/second.tscn");

    /// <summary>
    /// Eine Liste aller Szenen. Neue Szennen sollen hierhinzugefügt werden.
    /// </summary>
    public static readonly List<SceneInfo> AllScenesList = new()
    {
        MainMenu,
        SecondScene
    };
}