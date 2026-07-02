using UnityEngine;

// using animal sprites from https://kenney.nl/assets
public class DisappearAfterCollision : MonoBehaviour
{
    // if the bird hits a destructible object, it should disappear after 5 seconds
    private float disappearDelay = 5f;

    // if the bird falls lower than y = -20, it should disappear
    private float fallOff = -20f; 
    
    // timer when bird hits a destructible object
    private bool timerStarted = false;
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Only start timer if bird hits a destructible block
        if (collision.gameObject.CompareTag("Block") && !timerStarted)
        {
            timerStarted = true;

            // using invoke, citing from 
            // https://docs.unity3d.com/6000.2/Documentation/ScriptReference/MonoBehaviour.Invoke.html
            Invoke("DisappearObject", disappearDelay);
        }
    }
    
    void Update()
    {
        // if bird falls lower than y = -20
        if (transform.position.y < fallOff)
        {
            DisappearObject();
        }
    }
    
    void DisappearObject()
    {
        Destroy(gameObject);
    }
}