using UnityEngine;

// using elephant sprite from https://kenney.nl/assets
public class Elephant : Bird
{
    public float radius = 5f;
    public float force = 400f;
    public GameObject explosionPrefab;
    private bool explosionTriggered = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        // hit block or enemy, then explode
        if ((collision.gameObject.CompareTag("Block") && !explosionTriggered) || collision.gameObject.CompareTag("Enemy") && !explosionTriggered)
        {
            explosionTriggered = true;
            
            // 0.75 second explosion delay
            Invoke("ActivateAbility", 0.75f);
        }
    }

    public override void ActivateAbility()
    {
        if (abilityUsed) return;

        // if elephant is clicked early
        if (IsInvoking("ActivateAbility"))
        {
            CancelInvoke("ActivateAbility");
        }

        abilityUsed = true;

        // explosion animation destroyed after 0.2 seconds
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, 0.2f);

        Explode(force, radius);

        Destroy(gameObject);
    }

    private void Explode(float force, float radius)
    {
        // gives a list of colliders in a certain radius
        // citing: https://docs.unity3d.com/ScriptReference/Physics2D.OverlapCircleAll.html
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D hit in hits)
        {
            Rigidbody2D rb2 = hit.attachedRigidbody;

            // no force to the elephant
            if (rb2 && rb2 != rb)
            {
                Vector2 direction = rb2.position - (Vector2)transform.position;

                float distance = direction.magnitude;
                float effect = 1 - (distance / radius);

                // force
                rb2.AddForce(direction.normalized * force * effect, ForceMode2D.Impulse);

                // damage enemy
                Enemy enemy = hit.GetComponent<Enemy>();
                if (enemy != null)
                {
                    // can be 500 damage
                    float damage = 500f * effect;
                    enemy.TakeDamage(damage);
                }
            }
        }
    }
}