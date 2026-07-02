using UnityEngine;

public class Giraffe : Bird
{
    public float speedBoostMultiplier = 2.5f;

    public override void ActivateAbility()
    {
        // clicked for ability already
        if (abilityUsed)
        {
            return;
        }

        abilityUsed = true;

        if (rb != null)
        {
            // current velocity
            Vector2 currentVelocity = rb.linearVelocity;
            
            // boost in same direction
            Vector2 boostedVelocity = currentVelocity * speedBoostMultiplier;
            
            rb.linearVelocity = boostedVelocity;
        }
    }
}