using System;

namespace MasterofElements.scripts.singletons.score;

public class ScoreEvent
{
    public ScoreEvent(int score, ScoreType scoreType, DateTime happenedAt)
    {
        Score = score;
        ScoreType = scoreType;
        HappenedAt = happenedAt;
    }

    public int Score { get; }

    public ScoreType ScoreType { get; }

    public DateTime HappenedAt { get; }
}