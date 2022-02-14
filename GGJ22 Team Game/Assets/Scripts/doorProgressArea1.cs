using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorProgressArea1 : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager2.instance.numArtifacts > 0)
        {
            spriteRenderer.enabled = true;
            boxCollider.enabled = true;
        }
        else
        {
            spriteRenderer.enabled = false;
            boxCollider.enabled = false;
        }
    }
}
