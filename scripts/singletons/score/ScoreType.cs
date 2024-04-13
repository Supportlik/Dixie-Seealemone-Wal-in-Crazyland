namespace MasterofElements.scripts.singletons.score;

/// <summary>
/// Die ScoreType Enumeration repräsentiert verschiedene Arten von Ereignissen, bei denen Punkte im Spiel erzielt werden können.
/// </summary>
public enum ScoreType
{
    /// <summary>
    /// Punkte werden erzielt, wenn ein Feind besiegt wird.
    /// </summary>
    EnemyDefeated,

    /// <summary>
    /// Punkte werden erzielt, wenn ein Level abgeschlossen wird.
    /// </summary>
    LevelCompleted,

    /// <summary>
    /// Punkte werden erzielt, wenn ein Boss besiegt wird.
    /// </summary>
    BossDefeated,

    /// <summary>
    /// Punkte werden erzielt, wenn das Spiel abgeschlossen wird.
    /// </summary>
    GameCompleted,

    /// <summary>
    /// Punkte werden erzielt, wenn ein Gegenstand aufgesammelt wird.
    /// </summary>
    PickupCollected,

    /// <summary>
    /// Punkte werden erzielt, wenn eine Münze gesammelt wird.
    /// </summary>
    CoinCollected
}