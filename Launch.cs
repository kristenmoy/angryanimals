using UnityEngine;
using UnityEngine.InputSystem;

public class Launch : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D birdCollider;
    private Camera mainCamera;

    private Vector2 startPos;
    private Vector2 endPos;
    private bool isDragging = false;

    public float maxDragDistance = 2f;
    public float forceMultiplier = 3f;
    
    private GameManager gameManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        birdCollider = GetComponent<Collider2D>();
        mainCamera = Camera.main;
        gameManager = FindObjectOfType<GameManager>();

        rb.bodyType = RigidbodyType2D.Kinematic;
        birdCollider.enabled = false;
    }

    void Update()
    {
        // don't process clicks if time is frozen or game is over
        if (Time.timeScale == 0f)
        {
            return;
        }

        // left click special power
        if (rb != null && rb.bodyType == RigidbodyType2D.Dynamic)
        {
            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                Bird bird = GetComponent<Bird>();
                if (bird != null)
                {
                    bird.ActivateAbility();
                }
            }
        }

        if (rb == null || rb.bodyType == RigidbodyType2D.Dynamic)
        {
            return;
        } 

        // using new input system and not old because that is what i have
        bool isPressed = Mouse.current.leftButton.isPressed;
        bool wasPressed = Mouse.current.leftButton.wasPressedThisFrame;
        bool wasReleased = Mouse.current.leftButton.wasReleasedThisFrame;
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();

        if (wasPressed)
        {
            StartDragging(mouseScreenPos);
        }
        else if (isPressed && isDragging)
        {
            Dragging(mouseScreenPos);
        }
        else if (wasReleased && isDragging)
        {
            Shoot(mouseScreenPos);
        }  
    }


    private void StartDragging(Vector2 mouseScreenPos)
    {
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, 0));

        isDragging = true;
        startPos = rb.position;
    }

    private void Dragging(Vector2 mouseScreenPos)
    {
        endPos = mainCamera.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, 0));

        // from class -- gives direction and distance
        Vector2 dragVector = startPos - endPos;

        // drag distance
        if (dragVector.magnitude > maxDragDistance)
        {
            dragVector = dragVector.normalized * maxDragDistance;
            endPos = startPos - dragVector;
        }

        transform.position = endPos;
    }

    private void Shoot(Vector2 mouseScreenPos)
    {
        isDragging = false;

        endPos = mainCamera.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, 0));

        Vector2 dragVector = startPos - endPos;

        if (dragVector.magnitude > maxDragDistance)
        {
            dragVector = dragVector.normalized * 10;
        }  

        rb.bodyType = RigidbodyType2D.Dynamic;
        birdCollider.enabled = true;
        
        rb.AddForce(dragVector * forceMultiplier, ForceMode2D.Impulse);
    }
}