using UnityEngine;

// using enemy sprites from https://kenney.nl/assets
// using explosion sound from https://pixabay.com/sound-effects/search/pop/
public class Enemy : MonoBehaviour
{
    public float health = 300f; // enemy has 300 health    
    private float birdDamageMultiplier = 15f; // damage from the bird
    private float blockDamageMultiplier = 10f; // damage from blocks
    private float otherDamageMultiplier = 5f; // damage from other objects, like fall damage
    public GameObject explosionPrefab; // animation
    public AudioClip explosionSound; // audio
    
    private GameManager gameManager;
    
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    
    void Update()
    {
        // if enemy fell off the map
        if (transform.position.y < -20f || transform.position.y > 35f || 
            transform.position.x < -36f || transform.position.x > 38f)
        {
            Die();
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        // collision force
        float collisionForce = collision.relativeVelocity.magnitude;
        
        float damage = 0f;
        
        // what hit the snake
        if (collision.gameObject.CompareTag("Player")) // bird
        {
            damage = collisionForce * birdDamageMultiplier;
        }
        else if (collision.gameObject.CompareTag("Block")) // destructible blocks
        {
            damage = collisionForce * blockDamageMultiplier;
        }
        else // other objects
        {
            damage = collisionForce * otherDamageMultiplier;
        }
        
        TakeDamage(damage);
    }
    
    public void TakeDamage(float damage)
    {
        health -= damage;
        
        if (health <= 0)
        {
            Die();
        }
    }
    
    void Die()
    {
        // play explosion sound
        AudioSource.PlayClipAtPoint(explosionSound, transform.position);

        // the explosion animation takes around 0.2 seconds to finish
        // so destroy the animation so it isn't stuck on the screen
        GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(explosion, 0.2f);

        gameManager.EnemyDestroyed();
        Destroy(gameObject);
    }

}