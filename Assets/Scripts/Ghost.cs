using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public int points = 200;// Points awarded for catching the ghost
    public Mouvement mouvement { get; private set; }
    
    public GhostHome home { get; private set; }// The home behavior for the ghost
    public GhostScatter scatter { get; private set; }// The scatter behavior for the ghost
    public GhostChase chase { get; private set; }// The chase behavior for the ghost
    public GhostFrightened frightened { get; private set; }// The frightened behavior for the ghost

    public GhostBehaviour initialGhostBehaviour;// Initial behavior of the ghost when the game starts

    // The target that the ghost will chase/be frightened of
    public Transform target;
    private void Awake()
    {
        mouvement = GetComponent<Mouvement>();
        home = GetComponent<GhostHome>();
        scatter = GetComponent<GhostScatter>();
        chase = GetComponent<GhostChase>();
        frightened = GetComponent<GhostFrightened>();
    }
    private void Start()
    {
        ResetState();// Reset the state of the ghost
    }
    public void ResetState()
    {
        gameObject.SetActive(true);
        mouvement.ResetState();

        scatter.Disable();
        chase.Disable();
        frightened.Disable();
        if (initialGhostBehaviour != null)
        {
            initialGhostBehaviour.Enable();
        }
        if (initialGhostBehaviour != home)
        {
            home.Disable();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Catch the ghost if he's in frightened otherwise pacman dies
            if (this.frightened.enabled)
            {
                FindObjectOfType<GameManager>().GhostEaten(this);
            }
            else
            {
                FindObjectOfType<GameManager>().PacmanEaten();
            }
        }
    }
}
