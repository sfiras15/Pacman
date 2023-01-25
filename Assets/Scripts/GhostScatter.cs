using UnityEngine;
[RequireComponent(typeof(Mouvement))]
public class GhostScatter : GhostBehaviour
{
    // In this state the ghost will roam the map randomly
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Node node = collision.GetComponent<Node>(); 
        if (node != null && this.enabled && !ghost.frightened.enabled)
        {
            int index = Random.Range(0, node.directions.Count);
            // This is to prevent the ghost from backtracking and retaking the same direction he came from
            if (node.directions[index] == -ghost.mouvement.direction && node.directions.Count >1)
            {
                index++;
                if (index >= node.directions.Count)
                {
                    index = 0;
                }
            }
            ghost.mouvement.SetDirection(node.directions[index]);
        }
    }
    private void OnDisable()
    {
        if (!ghost.home.enabled)
        {
            ghost.chase.Enable();
        }
    }
}
