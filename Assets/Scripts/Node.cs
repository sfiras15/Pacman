using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Node : MonoBehaviour
{
    public List<Vector2> directions;
    public LayerMask layerMask;
    private void Start()
    {
        directions = new List<Vector2>();
        // Check for all 4 directions
        CheckAvailableDirection(Vector2.up);
        CheckAvailableDirection(Vector2.down);
        CheckAvailableDirection(Vector2.right);
        CheckAvailableDirection(Vector2.left);
    }
    // Loads the direction to the list if it leads to a free cell
    private void CheckAvailableDirection(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.5f, 0f, direction, 1f, layerMask);
         if (hit.collider == null)
        {
            directions.Add(direction);
        }
    }
}
