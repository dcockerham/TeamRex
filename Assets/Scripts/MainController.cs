using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class MainController : MonoBehaviour {
    public int lives;

    public Text livesText;
    public Text scoreText;
    public Text restartText;
    public Text gameOverText;

    private int score;
    // Use this for initialization
    void Start () {
        score = 0;
        UpdateLives();
        UpdateScore();
    }
	
	// Update is called once per frame
	void Update () {

        
        GameOver();
   
    }

    private void GameOver()
    {
        if (lives <= 0)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Destroy(player);

            gameOverText.text = "GameOver!";
            restartText.text = "Press 'R' for Restart";
            if (Input.GetKeyDown(KeyCode.R))
            {
                ReloadCurrentScene();
            }
        }
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    void UpdateLives()
    {
        livesText.text = "Lives: " + lives;
    }

    void ReloadCurrentScene()
    {
        // get the current scene name 
        string sceneName = SceneManager.GetActiveScene().name;

        // load the same scene
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    public void Death()
    {
        lives--;
        UpdateLives();
    }

}
