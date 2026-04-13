using System;
using UnityEngine;

public interface IScoreGiver
{
    public int ScoreIncOnCrossed { get; }
    public event Action<GameObject, int> OnObstacleCrossed;
}