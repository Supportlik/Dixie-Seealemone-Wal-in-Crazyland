using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

namespace MasterofElements.scripts.singletons.score;

/// <summary>
/// Die ScoreService Klasse ist verantwortlich für die Verwaltung und Berechnung der Punkte im Spiel.
/// </summary>
public partial class ScoreService : Node
{
    /// <summary>
    /// Die aktuelle Punktzahl.
    /// </summary>
    private int _score = 0;

    private AutoLoader _autoLoader;

    /// <summary>
    /// Eine Liste aller ScoreEvent-Objekte, die die erzielten Punkte und den Typ des Ereignisses enthalten.
    /// </summary>
    private readonly LinkedList<ScoreEvent> _scoreEvents = new();

    public override void _Ready()
    {
        _autoLoader = new AutoLoader(this);
    }

    /// <summary>
    /// Wird aufgerufen, wenn ein ScoreEvent auftritt. Aktualisiert die Punktzahl und fügt das Ereignis zur Liste hinzu.
    /// </summary>
    /// <param name="score">Die Anzahl der Punkte, die bei diesem Ereignis erzielt wurden.</param>
    /// <param name="scoreType">Der Typ des Ereignisses, bei dem die Punkte erzielt wurden.</param>
    public void OnScoreEvent(int score, ScoreType scoreType)
    {
        _score += score;
        _autoLoader.SignalManager.EmitSignal(signalmanager.SignalManager.SignalName.OnScoreChanged);
        var scoreEvent = new ScoreEvent(score, scoreType, DateTime.Now);
        _scoreEvents.AddLast(scoreEvent);
    }

    public int Score => _score;

    /// <summary>
    /// Summiert die Punkte für einen bestimmten ScoreType.
    /// </summary>
    /// <param name="scoreType">Der Typ des Ereignisses, für das die Punkte summiert werden sollen.</param>
    /// <returns>Die Summe der Punkte für den gegebenen ScoreType.</returns>
    public int SummarizeScoreForType(ScoreType scoreType)
    {
        return _scoreEvents
            .Where(scoreEvent => scoreEvent.ScoreType == scoreType)
            .Sum(scoreEvent => scoreEvent.Score);
    }

    /// <summary>
    /// Setzt die Punktzahl zurück und löscht alle ScoreEvent-Objekte aus der Liste.
    /// </summary>
    public void ResetScore()
    {
        _score = 0;
        _scoreEvents.Clear();
    }
}