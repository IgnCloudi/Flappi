using UnityEngine;

public class PipeDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.GetComponent<PipeMover>())
        {
            Destroy(collision.gameObject);
            Debug.Log("Enter detrected");
        }   
    }
}