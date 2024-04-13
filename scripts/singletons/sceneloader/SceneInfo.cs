namespace MasterofElements.scripts.singletons.sceneloader;

/// <summary>
/// Die SceneInfo Klasse speichert den Pfad zu einer bestimmten Szene in Ihrem Projekt.
/// </summary>
public class SceneInfo
{
    /// <summary>
    /// Erstellt eine neue Instanz der SceneInfo Klasse.
    /// </summary>
    /// <param name="scenePath">Der Pfad zur Szene.</param>
    private readonly string _scenePath;

    public SceneInfo(string scenePath)
    {
        _scenePath = scenePath;
    }

    /// <summary>
    /// Gibt den Pfad zur Szene zurück.
    /// </summary>
    public string ScenePath => _scenePath;
}