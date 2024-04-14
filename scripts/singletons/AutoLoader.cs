using Godot;
using MasterofElements.scripts.singletons.audiomanager;
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
    public readonly SignalManager SignalManager;
    public readonly FileAccessService FileAccessService;
    public readonly SceneSwitcherService SceneSwitcherService;
    public readonly ScoreService ScoreService;
    public readonly AudioService AudioService;
    public readonly PackedSceneFactory PackedSceneFactory;
    public readonly DialogManager DialogManager;
    public readonly GameManager GameManager;

    /// <summary>
    /// Lädt einfach alle Autoloaded Singletons. Convienience!
    /// </summary>
    public AutoLoader(Node parent)
    {
        SignalManager = parent.GetNode<SignalManager>("/root/SignalManager");
        FileAccessService = parent.GetNode<FileAccessService>("/root/FileAccessService");
        SceneSwitcherService = parent.GetNode<SceneSwitcherService>("/root/SceneSwitcherService");
        ScoreService = parent.GetNode<ScoreService>("/root/ScoreService");
        AudioService = parent.GetNode<AudioService>("/root/AudioService");
        PackedSceneFactory = parent.GetNode<PackedSceneFactory>("/root/PackedSceneFactory");
        DialogManager = parent.GetNode<DialogManager>("/root/DialogManager");
        GameManager = parent.GetNode<GameManager>("/root/GameManager");
    }
}