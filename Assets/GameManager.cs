using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int Score;
    public Text ScoreText;

    private void Start()
    {
        ScoreText.text = "0";
    }

    [ContextMenu("Add Score!")]
    public void AddScore(GameObject Caller, int Increment)
    {
        Score += Increment;
        ScoreText.text = Score.ToString();
    }
}
