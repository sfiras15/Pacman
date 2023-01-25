using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI Variables")]
    public Image[] livesUI;
    public TextMeshProUGUI up;
    public TextMeshProUGUI player;
    public TextMeshProUGUI ready;
    public TextMeshProUGUI PlayerText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    [Header("Gameplay Variables")]
    public Ghost[] ghosts;
    public Pacman pacman;
    public Transform pellets;
    public int score { get; private set; }
    public int highScore { get; private set; }
    // current ghost multiplier for calculating score when a ghost is eaten
    public int ghostMultiplier { get; private set; } = 1;
    public int lives { get; private set; }

    // Initializes a new game when the script is first loaded
    private void Start()
    {
        StartCoroutine(nameof(Flashing));
        NewGame();
    }
    private IEnumerator Flashing()
    {
        while (enabled)
        {
            up.color = Color.clear;
            yield return new WaitForSeconds(0.5f);
            up.color = Color.white;
            yield return new WaitForSeconds(0.5f);
        }
    }
    private void StartGame()
    {
        PlayerText.gameObject.SetActive(false);
        foreach (Ghost ghost in ghosts)
        {
            ghost.gameObject.SetActive(true);
        }
        pacman.gameObject.SetActive(true);
        Time.timeScale = 1f;    
    }
    private void Update()
    {
        if (lives <= 0)
        {
            NewGame();
        }
    }
    // Resets the score, lives, and starts a new round
    public void NewGame()
    {
        if (Input.anyKeyDown)
        {
            player.gameObject.SetActive(false);
            ready.gameObject.SetActive(false);
            foreach (Image live in livesUI)
            {
                live.gameObject.SetActive(true);
            }
            SetScore(0);
            SetLives(3);
            NewRound();
        }   
    }
    // Sets the current score
    private void SetScore(int score)
    {
        this.score = score;
        if (score >= highScore)
        {
            highScore = score;
        }
        scoreText.text = score.ToString();
        highScoreText.text = highScore.ToString();
    }
    // Sets the current number of lives
    private void SetLives(int lives)
    {
        this.lives = lives;
    }
    // Initializes a new round by activating all pellets and resetting the game state
    private void NewRound()
    {
        foreach(Transform pellet in pellets)
        {
            pellet.gameObject.SetActive(true);
        }
        ResetState();
    }
    // Resets the game state by resetting the ghost multiplier and the state of the ghosts and pacman
    private void ResetState()
    {
        ResetGhostMultiplier();
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].ResetState();
        }
        pacman.ResetState();
    }
    // Ends the game and disables all ghosts and pacman objects
    private void GameOver()
    {
        player.gameObject.SetActive(true);
        ready.gameObject.SetActive(true);
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].gameObject.SetActive(false);
        }
        pacman.gameObject.SetActive(false);
    }
    // Increases the score and ghost multiplier when a ghost is eaten
    public void GhostEaten(Ghost ghost)
    {
        SetScore(score + (ghost.points * this.ghostMultiplier));
        this.ghostMultiplier++;
    }
    // Decreases the number of lives and either resets the game state or ends the game if the player has no more lives
    public void PacmanEaten()
    {
        //pacman.gameObject.SetActive(false);
        livesUI[lives - 1].gameObject.SetActive(false);
        SetLives(lives - 1);
        foreach(Ghost ghost in ghosts)
        {
            ghost.gameObject.SetActive(false);
        }
        pacman.DeathAnimation();
        if (this.lives > 0)
        {
            Invoke(nameof(ResetState), 4f);
        }
        else
        {
            Invoke(nameof(GameOver), 4f);
        }
    }
    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);
        SetScore(score + pellet.points);
        if (!HasRemainingPellets())
        {
            pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound),3f);
        }
    }
    public void PowerPelletEaten(PowerPellet pellet)
    {
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].frightened.Enable(pellet.duration);
        }
        // This is so we can build up a big multiplier if we eat multiple powerPellets in a row
        CancelInvoke(nameof(ResetGhostMultiplier));
        Invoke(nameof(ResetGhostMultiplier), pellet.duration);
        PelletEaten(pellet);


    }
    private bool HasRemainingPellets()
    {
        foreach (Transform pellet in pellets)
        {
            // if a pellet is still active, return true
            if (pellet.gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }
    private void ResetGhostMultiplier()
    {
        this.ghostMultiplier = 1;
    }


}
