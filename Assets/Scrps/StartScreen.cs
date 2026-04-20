using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartScreen : MonoBehaviour
{
    public event Action OnGameStart;
    public GameManager MyManager;

    private void Start()
    {
        OnGameStart += MyManager.OnGameStart;
    }

    void Update()
    {
        if (!MyManager.GameStarted && (Keyboard.current.spaceKey.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame)) {
            OnGameStart.Invoke();
            gameObject.SetActive(false);
            //PlayerBird.StartLife();
        }       
    }
}
