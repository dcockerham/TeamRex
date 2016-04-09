using UnityEngine;
using System.Collections;

public class Asteroid_Movement : MonoBehaviour {

    public float speed = 2.5f;
	public float minSize = 4f;
	public float sizeMod = 0.7f;

    public Vector3 Direction;

	// Use this for initialization
	void Start () {

        Direction.x = Random.Range(-1f, 1f);
        Direction.y = Random.Range(-1f, 1f);

        Direction.x *= speed;
        Direction.y *= speed;

        /*transform.localScale -= new Vector3(3f, 3f, 0);*/
        if (transform.localScale.x < minSize)
        {
            Destroy(this.gameObject);
        }
	
	}

    void DestroyAsteroid()
    {
		if (transform.localScale.x * sizeMod >= minSize) {
			Vector3 newScale = new Vector3 (transform.localScale.x*sizeMod, transform.localScale.y*sizeMod, transform.localScale.z);
			GameObject newAsteroid = Instantiate (this.gameObject);
			newAsteroid.transform.localScale = newScale;
			newAsteroid = Instantiate (this.gameObject);
			newAsteroid.transform.localScale = newScale;
		}

        Destroy(this.gameObject);
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
