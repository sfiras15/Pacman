using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Passage : MonoBehaviour
{
    public Transform exit;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 translation;
        if (collision.CompareTag("Player") || collision.gameObject.layer == LayerMask.NameToLayer("Ghost"))
        {
            if (exit.position.x < 0)
            {
                translation = Vector3.right;
            }
            else
            {
                translation = Vector3.left;
            }
            collision.transform.position = exit.position + translation;
        }
    }
}
