using System;
using UnityEditor.U2D.Aseprite;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.InputSystem;

public class BirdScr : MonoBehaviour
{
    public event Action OnPlayerHitObstacle;
    public Rigidbody2D Bird;
    public float DefaultGravityScale = 3.0f;
    public int JumpForce = 5;

    private bool IsAlive = false;

    public void StartLife()
    {
        IsAlive = true;
        Bird.linearVelocityY = JumpForce;
        Bird.gravityScale = DefaultGravityScale;
    }


    // Update is called once per frame
    void Update()
    {
       if (IsAlive && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Bird.linearVelocityY = JumpForce;
        };    
    }

    private void OnDeath()
    {
        OnPlayerHitObstacle.Invoke();
        IsAlive = false;
        GetComponent<Collider2D>().enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnDeath();
    }

    private void OnBecameInvisible()
    {
        OnDeath();
    }
}
