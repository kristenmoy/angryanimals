using UnityEngine;

// using bird sprites from https://kenney.nl/assets
public class Bird : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected bool abilityUsed = false;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void OnLaunched()
    {
    }

    public virtual void ActivateAbility()
    {
        // default bird does nothing
    }
}
