using Godot;
using MasterofElements.scripts.singletons.fileaccess;

namespace MasterofElements.scripts.singletons;

/// <summary>
/// Die AutoLoader Klasse ist verantwortlich für das Laden und Bereitstellen von Diensten.
/// Alle Singletons oder Services (auch wenn sie keine Nodes brauchen), sollten hier geladen werden.
/// </summary>
public class AutoLoader
{
    public readonly file_access_service FileAccessService;

    public AutoLoader(Node parent)
    {
        FileAccessService = parent.GetNode<fileaccess.file_access_service>("/root/FileAccessService");
    }
}