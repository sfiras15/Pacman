using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacman : MonoBehaviour
{
    public Mouvement mouvement;
    // The renderer for the alive pacman
    public AnimatedSpriteRenderer spriteRendererAlive;
    // The renderer for the dead pacman
    public AnimatedSpriteRenderer spriteRendererDead;
    // Start is called before the first frame update
    void Awake()
    {
        mouvement = GetComponent<Mouvement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Z))
        {
            mouvement.SetDirection(Vector2.up);
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            mouvement.SetDirection(Vector2.down);
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            mouvement.SetDirection(Vector2.right);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Q))
        {
            mouvement.SetDirection(Vector2.left);
        }
        // Rotate the object to face the direction of movement
        float angle = Mathf.Atan2(mouvement.direction.y, mouvement.direction.x);
        transform.eulerAngles = new Vector3(0f, 0f, angle * Mathf.Rad2Deg);
    }
    public void ResetState()
    {
        gameObject.SetActive(true);
        // Reset the movement
        mouvement.ResetState();  
        spriteRendererAlive.enabled = true;
        spriteRendererDead.enabled = false;
    }
    public void DeathAnimation()
    {
        mouvement.enabled = false;
        spriteRendererAlive.enabled = false;
        spriteRendererDead.enabled = true;
        spriteRendererDead.Restart();
    }
}

