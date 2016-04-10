using UnityEngine;
using System.Collections;

public class Asteroid_Movement : MonoBehaviour {

    public float speed = 2.5f;
	public float sizeMod = 0.6f;
    public int size;

	private Quaternion baseRot;
	public float rotateSpeed;
    public Vector3 Direction;

	public GameObject breakSound;

    private MainController mainController;

    // Use this for initialization
    void Start () {
        GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        mainController = gameControllerObject.GetComponent<MainController>();
		baseRot = transform.rotation;

        Direction.x = Random.Range(-1f, 1f);
        Direction.y = Random.Range(-1f, 1f);
		rotateSpeed = Random.Range (0f, 1f);

        Direction.x *= speed;
        Direction.y *= speed;
	
	}

    void DestroyAsteroid()
    {
		if (size > 1) {
			Vector3 newScale = new Vector3 (transform.localScale.x*sizeMod, transform.localScale.y*sizeMod, transform.localScale.z);
			GameObject newAsteroid = Instantiate (this.gameObject);
			newAsteroid.transform.localScale = newScale;
            newAsteroid.GetComponent<Asteroid_Movement>().size = size - 1;

            newAsteroid = Instantiate (this.gameObject);
            newAsteroid.GetComponent<Asteroid_Movement>().size = size - 1;
            newAsteroid.transform.localScale = newScale;

            mainController.ScoreAsteroid(transform.localScale.x);
        }

		Instantiate (breakSound);
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
		Quaternion tempRot = transform.rotation;
		transform.rotation = baseRot;
		transform.Translate(Direction * Time.deltaTime);
		transform.rotation = tempRot;
		transform.Rotate (0,0,100*rotateSpeed*Time.deltaTime);
	}
}
