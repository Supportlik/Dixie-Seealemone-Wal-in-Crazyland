﻿using Godot;

namespace MasterofElements.scripts.singletons;

/// <summary>
/// Die AutoLoader Klasse ist verantwortlich für das Laden und Bereitstellen von Diensten.
/// Alle Singletons oder Services (auch wenn sie keine Nodes brauchen), sollten hier geladen werden.
/// </summary>
public class AutoLoader
{
    public readonly FileAccessService FileAccessService;

    public AutoLoader(Node parent)
    {
        FileAccessService = parent.GetNode<FileAccessService>("/root/FileAccessService");
    }
}