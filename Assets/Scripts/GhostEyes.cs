using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class GhostEyes : MonoBehaviour
{
    public Sprite up;// Sprite for when the ghost is facing up
    public Sprite down;// sprite for when the ghost is facing down
    public Sprite right;// sprite for when the ghost is facing right
    public Sprite left;//sprite for when the ghost is facing left
    private SpriteRenderer spriterRenderer;
    private Mouvement mouvement;
    private void Awake()
    {
        spriterRenderer = GetComponent<SpriteRenderer>();
        mouvement = GetComponentInParent<Mouvement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mouvement.direction == Vector2.up)
        {
            spriterRenderer.sprite = up;
        }
        else if (mouvement.direction == Vector2.down)
        {
            spriterRenderer.sprite = down;
        }
        else if (mouvement.direction == Vector2.right)
        {
            spriterRenderer.sprite = right;
        }
        else if (mouvement.direction == Vector2.left)
        {
            spriterRenderer.sprite = left;
        }
    }
}
