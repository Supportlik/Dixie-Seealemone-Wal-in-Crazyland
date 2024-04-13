namespace Masterofelements.scripts.fileaccess;

/// <summary>
/// Die UserFiles Klasse enthält alle Dateien, die von unserem Spiel verwendet werden.
/// </summary>
public static class UserFiles
{
    /// <summary>
    /// Die AudioOptionsFile repräsentiert die Datei, in der die Audioeinstellungen gespeichert sind.
    /// </summary>
    public static readonly FileInfo AudioOptionsFile = new("user://audioOptions.dat");

    /// <summary>
    /// Die VideoOptionsFile repräsentiert die Datei, in der die Videoeinstellungen gespeichert sind.
    /// </summary>
    public static readonly FileInfo VideoOptionsFile = new("user://videoOptions.dat");

    // Fügen Sie hier weitere Dateien hinzu, die von Ihrem Spiel verwendet werden.
}