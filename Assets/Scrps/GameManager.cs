using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool GameStarted = false;

    public int Score;
    public Text ScoreText;
    public GameObject GameOverScreen;
    public BirdScr PlayerBird;

    private void Start()
    {
        ScoreText.text = "0";
        if (PlayerBird)
        {
            PlayerBird.OnPlayerHitObstacle += OnPlayerHitObstacle;
        }
    }

    public void OnGameStart()
    {
        GameStarted = true;
        PlayerBird.StartLife();

    }

    [ContextMenu("Add Score!")]
    public void AddScore(GameObject Caller, int Increment)
    {
        Score += Increment;
        ScoreText.text = Score.ToString();
    }

    private void OnPlayerHitObstacle()
    {
        GameOverScreen.SetActive(true);
    }

    private void OnGameOverButtonPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
