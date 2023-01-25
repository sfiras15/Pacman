using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostChase : GhostBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // node will generate the available directions
        Node node = collision.GetComponent<Node>();
        // Check if the collider is a Node & the ghost is not in the frightened mode
        if (node != null && this.enabled && !ghost.frightened.enabled)
        {
            Vector2 path = ShortestPath(node.directions);
            ghost.mouvement.SetDirection(path);
        }
    }
    // Returns the direction that leads to the shortest path to pacman
    private Vector2 ShortestPath(List<Vector2> directions)
    {
        Vector2 shortestPath = directions[0];
        for(int i = 1;i < directions.Count; i++)
        {
            if (isShortestPath(directions[i],shortestPath))
            {
                shortestPath = directions[i];
            }
        }
        return shortestPath;
    }
    private bool isShortestPath(Vector2 direction,Vector2 shortestPath)
    {
        // The distance between pacman and shortestPath
        float shortestDistance;
        // The distance between pacman and the direction
        float distance;
        shortestDistance = Vector2.Distance(ghost.target.position,new Vector2(shortestPath.x + ghost.transform.position.x,
            shortestPath.y + ghost.transform.position.y));
        distance = Vector2.Distance(ghost.target.position, new Vector2(direction.x + ghost.transform.position.x,
            direction.y + ghost.transform.position.y));
        return distance < shortestDistance;
    }
    private void OnDisable()
    {
        // Alternate between scatter/chase behaviour automatically when he's outside the home
        if (!ghost.home.enabled)
        {
            ghost.scatter.Enable();
        }
        
    }
}
