using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public event Action OnGameStart;
    public GameManager MyManager;

    private void Start()
    {
        Debug.Log(SceneManager.GetActiveScene().name);
        OnGameStart += MyManager.OnGameStart;
    }

    void Update()
    {
        if (!MyManager.GameStarted && Keyboard.current.spaceKey.wasPressedThisFrame) {
            OnGameStart.Invoke();
            gameObject.SetActive(false);
            //PlayerBird.StartLife();
        }       
    }
}
