using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedSpriteRenderer : MonoBehaviour
{
    public Sprite[] sprites;
    private SpriteRenderer spriteRenderer;
    private int currentFrame;
    public float animationTime = 0.25f;
    public bool looping;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        InvokeRepeating(nameof(Animate), animationTime, animationTime);
    }
    private void Animate()
    {
        currentFrame++;
        if (currentFrame >= sprites.Length && looping)
        {
            currentFrame = 0;
        }
        else if (currentFrame >= 0 && currentFrame < sprites.Length)
        {
            spriteRenderer.sprite = sprites[currentFrame];
        }
    }
    public void Restart()
    {
        currentFrame = -1;

        Animate();
    }
    private void OnEnable()
    {
        spriteRenderer.enabled = true;
    }
    private void OnDisable()
    {
        spriteRenderer.enabled = false;
    }
}
