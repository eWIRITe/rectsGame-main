using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorToRandom : MonoBehaviour
{
    public SpriteRenderer sprite;

    // Start is called before the first frame update
    void Awake()
    {
        sprite.color = Random.ColorHSV();
    }
}
