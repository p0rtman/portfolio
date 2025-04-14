using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GetEmoji : Interactable
{
    public Sprite action;
    public Sprite idle;

    SpriteRenderer sr;
    bool isInteracted;

    public override void Interact()
    {
        if(isInteracted)
        {
            sr.sprite = action;
        }
        else
        {
            sr.sprite = idle;
        }
        isInteracted = !isInteracted;
    }

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = idle;
    }
}

