using UnityEngine;
using System.Collections;

public class Asteroid_Movement : MonoBehaviour {

    float x;
    float y;

    public float speed = 2.5f;

    int count = 200;

    public Vector3 Direction;

	// Use this for initialization
	void Start () {

        Direction.x = Random.Range(-1f, 1f);
        Direction.y = Random.Range(-1f, 1f);

        Direction.x *= speed;
        Direction.y *= speed;

        transform.localScale -= new Vector3(3f, 3f, 0);

        if (transform.localScale.x < 4)
        {
            Destroy(this.gameObject);
        }
	
	}

    void DestroyAsteroid()
    {
        Instantiate(this.gameObject);
        Instantiate(this.gameObject);

        Destroy(this.gameObject);
    }

    void onCollisionEnter (Collision col) {

        if (col.gameObject.tag == "bullet")
        {
            DestroyAsteroid();
        }
    }
	
	// Update is called once per frame
	void Update () {

        transform.Translate(Direction * Time.deltaTime);

        count--;

        if (count == 0)
        {
            DestroyAsteroid();
        }
	}
}
