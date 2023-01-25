using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Timers;
using UnityEngine;

public class GhostFrightened : GhostBehaviour
{
    // Declare public variables for the sprite renderers for the body, eyes, blue, and white sprites
    public SpriteRenderer body;
    public SpriteRenderer eyes;
    public SpriteRenderer blue;
    public SpriteRenderer white;
    public bool eaten;

    public override void Enable(float duration)
    {
        base.Enable(duration);
        // Disable the body and eyes sprites, and enable the blue and white sprites
        body.enabled = false;
        eyes.enabled = false;    
        blue.enabled = true;
        white.enabled = false;
        // Once half the duration is past the ghost starts flashing blue & white
        Invoke(nameof(Flash), duration / 2f);
    }
    public override void Disable()
    {
        base.Disable();
        // Enable the body and eyes sprites, and disable the blue and white sprites
        body.enabled = true;
        eyes.enabled = true;
        blue.enabled = false;
        white.enabled = false;
    }
    private void OnEnable()
    {
        // Restart the animation of the blue form
        blue.GetComponent<AnimatedSpriteRenderer>().Restart();
        // Slow down the ghost when he's vulnerable
        ghost.mouvement.speedMultiplier = 0.5f;
        eaten = false;
    }
    private void OnDisable()
    {
        // Set the ghost's speed back to normal
        ghost.mouvement.speedMultiplier = 1f;
        eaten = false;
    }

    private void Flash()
    {
        if (!eaten)
        {
            blue.enabled = false;
            white.enabled = true;
            // Cycle through blue & white ghost sprites 
            white.GetComponent<AnimatedSpriteRenderer>().Restart();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (this.enabled)
            {
                Eat();
            }
        }
    }
    private void Eat()
    {
        eaten = true;
        // Move the ghost to its home position
        Vector3 position = ghost.home.inside.position;
        position.z = transform.position.z;

        transform.position = position;
        ghost.home.Enable(duration);
        // Only the eyes should be active when the ghost was eaten & in home
        body.enabled = false;
        eyes.enabled = true;
        blue.enabled = false;
        white.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Node node = collision.GetComponent<Node>();
        if (node != null && this.enabled)
        {
            // the ghost starts pathing away from pacman
            Vector2 path = EscapePath(node.directions);
            ghost.mouvement.SetDirection(path);
        }
    }
    private Vector2 EscapePath(List<Vector2> directions)
    {
        Vector2 longestPath = directions[0];
        for (int i = 1; i < directions.Count; i++)
        {
            if (isLongestPath(directions[i], longestPath))
            {
                longestPath = directions[i];

            }
        }
        return longestPath;
    }
    private bool isLongestPath(Vector2 direction, Vector2 longestPath)
    {
        float LongestDistance;
        float distance;
        LongestDistance = Vector2.Distance(ghost.target.position, new Vector2(longestPath.x + ghost.transform.position.x,
            longestPath.y + ghost.transform.position.y));
        distance = Vector2.Distance(ghost.target.position, new Vector2(direction.x + ghost.transform.position.x,
            direction.y + ghost.transform.position.y));
        return distance > LongestDistance;
    }
}
