using System.Collections;
using System.Collections.Generic;

using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public class BouncyEmojiMovement : MonoBehaviour
{
    public PhysicsMaterial2D bouncyMaterial;
    private Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = EmojiFallMovement.globalFallSpeed; // Use the static fall speed

        // Ensure that the emoji can bounce by setting the bounciness of the collider
        var collider = GetComponent<Collider2D>();
        collider.sharedMaterial = bouncyMaterial; // Assign a bouncy physics material here

        // Apply an initial force if needed
        ApplyInitialForce();
    }

    void ApplyInitialForce()
    {
        // This initial force could be a random value to give some variation
        float forceMagnitude = 2f; // Adjust as necessary
        Vector2 force = new Vector2(Random.Range(-1f, 1f), 0f) * forceMagnitude;
        rb.AddForce(force, ForceMode2D.Impulse);
    }

    void Update()
    {
        // You can keep the destruction logic as is from EmojiFallMovement or adjust if needed
        if (transform.position.y <= -9)
        {
            Destroy(gameObject);
        }
    }
}
