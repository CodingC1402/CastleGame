using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPlatform : MonoBehaviour
{
    public SpriteRenderer sprite = null;

    void OnTriggerEnter2D(Collider2D other)
    {
        sprite.color = Color.red;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        sprite.color = Color.white;
    }
}
