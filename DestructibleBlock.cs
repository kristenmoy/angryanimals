using UnityEngine;

// using block sprites from https://kenney.nl/assets
public class DestructibleBlock : MonoBehaviour
{
    public float health = 200f;          // block health
    private float damageMultiplier = 5f; // collide force

    void OnCollisionEnter2D(Collision2D collision)
    {
        float collisionForce = collision.relativeVelocity.magnitude;
        float damage = collisionForce * damageMultiplier;
        TakeDamage(damage);
    }

    void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            DestroyBlock();
        }
    }

    void DestroyBlock()
    {
        Destroy(gameObject);
    }
}
