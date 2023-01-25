using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GhostHome : GhostBehaviour
{
    public Transform inside;
    public Transform outside;
    private void OnEnable()
    {
        StopAllCoroutines();
        ghost.scatter.Disable();
        ghost.chase.Disable();
    }
    // Start the animation of the ghost exiting the home as soon as the duration of home expires
    private void OnDisable()
    {
        StartCoroutine(nameof(Animate));
        // Ghost enters scatter mode when he's out of the home
        ghost.scatter.Enable();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (enabled && collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            ghost.mouvement.SetDirection(-ghost.mouvement.direction);
        }
    }
    // Moves the ghost to the inside position of the home and then to the outside position
    private IEnumerator Animate()
    {
        ghost.mouvement.SetDirection(Vector2.up, true);
        // Set the rigidbody to kinimatic to ignore the home's collider
        ghost.mouvement.rb2D.isKinematic = true;
        ghost.mouvement.enabled = false;

        float duration = 0.5f;
        float elapsed = 0f;
        Vector3 position = transform.position;
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            Vector3 newPos = Vector2.Lerp(position, inside.position, t);
            transform.position = newPos;
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = inside.position;
        elapsed = 0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            Vector3 newPos = Vector3.Lerp(inside.position, outside.position, t);
            newPos.z = position.z;
            transform.position = newPos;
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = outside.position;

        ghost.mouvement.SetDirection(new Vector2 (Random.Range(0f,1f) < 0.5f ? 1f : -1f,0f) , true);
        ghost.mouvement.rb2D.isKinematic = false;
        ghost.mouvement.enabled = true;
    }

}
