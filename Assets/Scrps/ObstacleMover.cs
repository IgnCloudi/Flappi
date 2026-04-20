using System;
using UnityEngine;

public class PipeMover : MonoBehaviour, IScoreGiver
{
    public int MoveSpeed = 5;
    [field: SerializeField] public int ScoreIncOnCrossed { get; set; } = 1;
    public event Action<GameObject, int> OnObstacleCrossed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<BirdScr>(out BirdScr PlayerBird))
        {
            OnObstacleCrossed?.Invoke(gameObject, ScoreIncOnCrossed);
        }
    }

    void Update()
    {
        transform.position += Vector3.left * MoveSpeed * Time.deltaTime;
    }
}