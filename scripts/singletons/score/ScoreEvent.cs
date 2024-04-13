using System;

namespace MasterofElements.scripts.singletons.score;

/// <summary>
/// Die ScoreEvent Klasse repräsentiert ein Ereignis, bei dem Punkte erzielt wurden.
/// </summary>
public class ScoreEvent
{
    /// <summary>
    /// Erstellt eine neue Instanz der ScoreEvent Klasse.
    /// </summary>
    /// <param name="score">Die Anzahl der Punkte, die bei diesem Ereignis erzielt wurden.</param>
    /// <param name="scoreType">Der Typ des Ereignisses, bei dem die Punkte erzielt wurden.</param>
    /// <param name="happenedAt">Der Zeitpunkt, zu dem das Ereignis stattgefunden hat.</param>
    public ScoreEvent(int score, ScoreType scoreType, DateTime happenedAt)
    {
        Score = score;
        ScoreType = scoreType;
        HappenedAt = happenedAt;
    }

    /// <summary>
    /// Die Anzahl der Punkte, die bei diesem Ereignis erzielt wurden.
    /// </summary>
    public int Score { get; }

    /// <summary>
    /// Der Typ des Ereignisses, bei dem die Punkte erzielt wurden.
    /// </summary>
    public ScoreType ScoreType { get; }

    /// <summary>
    /// Der Zeitpunkt, zu dem das Ereignis stattgefunden hat.
    /// </summary>
    public DateTime HappenedAt { get; }
}