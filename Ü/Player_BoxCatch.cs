using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_BoxCatch : MonoBehaviour
{
    [Header("Movement Range")]
    [SerializeField]
    [Range(1f, 10f)]
    private float moveRange = 6f; // This can still be adjusted in the inspector if needed

    void Update()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        // Use the player speed from the GameMngr's GameSettings
        float horiOffset = horizontalMove * GameMngr.Instance.gameSettings.playerSpeed * Time.deltaTime;
        float rawHoriPos = transform.position.x + horiOffset;
        float clampedHoriMove = Mathf.Clamp(rawHoriPos, -moveRange, moveRange);
        transform.position = new Vector2(clampedHoriMove, transform.position.y);
    }
}
