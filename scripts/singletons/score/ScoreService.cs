using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

namespace MasterofElements.scripts.singletons.score;

public partial class ScoreService : Node
{
    private int _score = 0;
    private LinkedList<ScoreEvent> _scoreEvents = new();

    private void OnScoreEvent(int score, ScoreType scoreType)
    {
        _score += score;
        var scoreEvent = new ScoreEvent(score, scoreType, DateTime.Now);
        _scoreEvents.AddLast(scoreEvent);
    }

    private int SummerizeScoreForType(ScoreType scoreType)
    {
        return _scoreEvents
            .Where(scoreEvent => scoreEvent.ScoreType == scoreType)
            .Sum(scoreEvent => scoreEvent.Score);
    }


    private void ResetScore(ScoreEvent scoreEvent)
    {
        _score = 0;
        _scoreEvents.Clear();
    }
}