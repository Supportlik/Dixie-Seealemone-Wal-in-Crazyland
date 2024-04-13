using Godot;
using MasterofElements.scripts.singletons.fileaccess;
using MasterofElements.scripts.singletons.sceneloader;
using MasterofElements.scripts.singletons.score;

namespace MasterofElements.scripts.singletons;

/// <summary>
/// Die AutoLoader Klasse ist verantwortlich für das Laden und Bereitstellen von Diensten.
/// Alle Singletons oder Services (auch wenn sie keine Nodes brauchen), sollten hier geladen werden.
/// </summary>
public class AutoLoader
{
    public readonly FileAccessService FileAccessService;
    public readonly SceneSwitcherService SceneSwitcherService;
    public readonly ScoreService ScoreService;

    public AutoLoader(Node parent)
    {
        FileAccessService = parent.GetNode<FileAccessService>("/root/FileAccessService");
        SceneSwitcherService = parent.GetNode<SceneSwitcherService>("/root/SceneSwitcherService");
        ScoreService = parent.GetNode<ScoreService>("/root/ScoreService");
    }
}