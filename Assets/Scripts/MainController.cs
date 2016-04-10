using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class MainController : MonoBehaviour {
    public int lives;
    public float baseScoreAsteroid;

    public Text livesText;
    public Text scoreText;
    public Text restartText;
    public Text gameOverText;

    public int score;

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
            restartText.text = "Press 'R' for Restart\nPress 'Q' for Quit";
            if (Input.GetKeyDown(KeyCode.R))
            {
                ReloadCurrentScene();
            }
			else if (Input.GetKeyDown(KeyCode.Q))
			{
				Application.LoadLevel(0);
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

    public void Death()
    {
        lives--;
        UpdateLives();
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    public void ScoreAsteroid(float size)
    {

        AddScore((int) (baseScoreAsteroid * size));
    }

    /*
    public IEnumerator laserspawn(float time, GameObject gem)
    {
        yield return new WaitForSeconds(time);
        gem.SetActive(true);
    }
    */

}
