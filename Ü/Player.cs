using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float walkSpeed = 5f;
    public Animator animator;
    public GameObject interactIcon;
    public float interactionRadius = 1f;


    [SerializeField] private EmoteWheelController emoteWheelController; // Reference to the EmoteWheelController

    float horizontal;
    Rigidbody2D rb;
    bool isFacingRight = true;
    bool isEmoteWheelActive = false; // State flag for the emote wheel

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        // emoteWheelController is set via the inspector
    }

    void Update()
    {
        // A key pressed to trigger the Interaction Logic
        if (Input.GetKeyDown(KeyCode.E) && !isEmoteWheelActive)
        {
            CheckInteraction();
        }

        // A key pressed to toggle the emote wheel
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleEmoteWheel();
        }

        if (!isEmoteWheelActive) // Only allow movement if the emote wheel is not active
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            animator.SetFloat("Speed", Mathf.Abs(horizontal));
            Flip();
        }
    }

    private void FixedUpdate()
    {
        if (!isEmoteWheelActive) // Only allow movement if the emote wheel is not active
        {
            rb.velocity = new Vector2(horizontal * walkSpeed, rb.velocity.y);
        }
        else
        {
            // Stop movement when emote wheel is active
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    public void OpenInteractableIcon()
    {
        interactIcon.SetActive(true);
    }

    public void CloseInteractableIcon()
    {
        interactIcon.SetActive(false);
    }

    private void CheckInteraction()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactionRadius);
        foreach (var collider in colliders)
        {
            Interactable interactable = collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                interactable.Interact();
                break;
            }
        }
    }


    private void ToggleEmoteWheel()
    {
        if (emoteWheelController != null)
        {
            isEmoteWheelActive = !isEmoteWheelActive; // Toggle the state
            emoteWheelController.ToggleVisibility(isEmoteWheelActive);
            animator.SetFloat("Speed", 0); // Stop walking animation when emote wheel is active
        }
        else
        {
            Debug.LogError("EmoteWheelController is not assigned on the Player script.");
        }
    }
}
