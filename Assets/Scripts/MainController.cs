using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class MainController : MonoBehaviour {
    public int lives;

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {

        GameOver();
   
    }

    private void GameOver()
    {
        if (lives <= 0)
        {
            gameOverText.text = "GameOver!";
            restartText.text = "Press 'R' for Restart";
            if (Input.GetKeyDown(KeyCode.R))
            {
                ReloadCurrentScene();
            }
        }
    }

    public void Death()
    {
        lives--;
    }

    private void ReloadCurrentScene()
    {
        // get the current scene name 
        string sceneName = SceneManager.GetActiveScene().name;

        // load the same scene
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }


}
