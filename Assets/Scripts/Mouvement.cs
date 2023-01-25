using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Mouvement : MonoBehaviour
{
    public Rigidbody2D rb2D;
    public Vector2 initialDirection;
    public Vector2 direction;
    // Next direction to move in, used for buffering input
    public Vector2 nextDirection;
    private Vector3 initialPosition;
    public float speed = 5f;
    //Multiplier for speed, used for ghosts when they enter vulnerable mode
    public float speedMultiplier = 1f;
    // The layer in which we check for collision
    public LayerMask obstacleLayer;


    // Start is called before the first frame update
    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
    }
    private void Start()
    {
        ResetState();
    }
    public void ResetState()
    {
        this.enabled = true;
        speedMultiplier = 1f;
        direction = initialDirection;
        nextDirection = Vector2.zero;
        rb2D.isKinematic = false;
        transform.position = initialPosition;
    }
    public void SetDirection(Vector2 direction , bool forced = false)// forced is for forcing the direction of the ghosts once they are outside their home
    {
        if (forced || !Occupied(direction))
        {
            this.direction = direction;// Set the current direction
            nextDirection = Vector2.zero;// Reset the next direction
        }
        else
        {
            nextDirection = direction;// Set the next direction
        }
    }
    // Return whether there is a collision in the given direction
    private bool Occupied (Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.75f, 0f, direction, 1.5f,obstacleLayer);
        return hit.collider != null;
    }

    // Update is called once per frame
    void Update()
    {
        if (nextDirection != Vector2.zero)
        {
            SetDirection(nextDirection);
        }
    }
    private void FixedUpdate()
    {
        Vector2 position = rb2D.position;
        Vector2 translation = direction * speed * speedMultiplier * Time.fixedDeltaTime;
        rb2D.MovePosition(position + translation);
    }
}
