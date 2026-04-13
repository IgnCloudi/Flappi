using UnityEngine;
using UnityEngine.InputSystem;

public class BirdScr : MonoBehaviour
{
    public Rigidbody2D Bird;
    public int JumpForce = 5;

    // Update is called once per frame
    void Update()
    {
       if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Bird.linearVelocityY = JumpForce;
        };    
    }
}
