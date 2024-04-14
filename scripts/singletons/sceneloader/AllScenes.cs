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
    public static readonly SceneInfo BaseLevel = new("res://scenes/level/base_level.tscn");

    public static readonly SceneInfo StartMenu = new("res://scenes/ui/main_menu.tscn");

    /// <summary>
    /// Eine Liste aller Szenen. Neue Szennen sollen hierhinzugefügt werden.
    /// </summary>
    public static readonly List<SceneInfo> AllScenesList = new()
    {
        BaseLevel,
        StartMenu
    };
}