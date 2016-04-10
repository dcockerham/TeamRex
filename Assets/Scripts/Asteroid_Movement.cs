using UnityEngine;
using System.Collections;

public class Asteroid_Movement : MonoBehaviour {

    public float speed = 2.5f;
	public float sizeMod = 0.6f;
    public int size;

    public Vector3 Direction;

    private MainController mainController;

    // Use this for initialization
    void Start () {
        GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        mainController = gameControllerObject.GetComponent<MainController>();

        Direction.x = Random.Range(-1f, 1f);
        Direction.y = Random.Range(-1f, 1f);

        Direction.x *= speed;
        Direction.y *= speed;
	
	}

    void DestroyAsteroid()
    {
		if (size > 3) {
			Vector3 newScale = new Vector3 (transform.localScale.x*sizeMod, transform.localScale.y*sizeMod, transform.localScale.z);
			GameObject newAsteroid = Instantiate (this.gameObject);
			newAsteroid.transform.localScale = newScale;
            newAsteroid.GetComponent<Asteroid_Movement>().size = size - 1;

            newAsteroid = Instantiate (this.gameObject);
            newAsteroid.GetComponent<Asteroid_Movement>().size = size - 1;
			newAsteroid.transform.localScale = newScale;

            
            mainController.ScoreAsteroid(transform.localScale.x);
        }

        Destroy(gameObject);
    }

    void OnTriggerEnter2D (Collider2D col) {

        if (col.gameObject.tag == "Bullet")
        {
            Destroy(col.gameObject);
            DestroyAsteroid();
        }
    }
	
	// Update is called once per frame
	void Update () {

        transform.Translate(Direction * Time.deltaTime);
	}
}
