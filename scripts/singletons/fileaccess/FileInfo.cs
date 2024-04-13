namespace Masterofelements.scripts.fileaccess;

/// <summary>
/// Die FileInfo Klasse repräsentiert eine Datei im Spiel. Alle Dateien müssen in UserFiles definiert werden.
/// </summary>
public class FileInfo
{
    private string _path;

    /// <summary>
    /// Initialisiert eine neue Instanz der FileInfo Klasse, die als Wrapper für einen Dateipfad dient.
    /// </summary>
    /// <param name="path">Der vollqualifizierte Name der Datei oder der relative Dateiname.</param>
    public FileInfo(string path)
    {
        _path = path;
    }

    /// <summary>
    /// Ruft den String ab, der den Pfad darstellt.
    /// </summary>

    public string Path => _path;
}