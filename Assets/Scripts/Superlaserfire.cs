using UnityEngine;
using System.Collections;

public class Superlaserfire : MonoBehaviour {
    Player_Controller player;

	// Use this for initialization
	void OnEnable () {
        player = GameObject.Find("Player").GetComponent<Player_Controller>();
        Vector3 playerpos = player.gameObject.transform.position;
        Vector3 currentpos = transform.position;

        Vector3 direction = playerpos - currentpos;
        //Vector3 direction = lastShootPos - Camera.main.WorldToScreenPoint(transform.position);
        direction.z = 0f;
        direction = direction.normalized;
        transform.right = -direction;

        transform.position = (playerpos + currentpos) / 2f;

        float x1 = playerpos.x;
        float x2 = currentpos.x;
        float y1 = playerpos.y;
        float y2 = currentpos.y;
        float dist = Mathf.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
        print(dist);
        transform.localScale = new Vector3(dist/2f, 1f, 1f);
    }
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 playerpos = player.gameObject.transform.position;
        Vector3 currentpos = transform.position;

        Vector3 direction = playerpos - currentpos;
        //Vector3 direction = lastShootPos - Camera.main.WorldToScreenPoint(transform.position);
        direction.z = 0f;
        direction = direction.normalized;
        transform.right = -direction;

        transform.position = (playerpos + currentpos) / 2f;

        float x1 = playerpos.x;
        float x2 = currentpos.x;
        float y1 = playerpos.y;
        float y2 = currentpos.y;
        float dist = Mathf.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
        print(dist);
        transform.localScale = new Vector3(dist / 2f, 1f, 1f);
    }

    void ONCollisionEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Asteroid"))
        {
            Asteroid_Movement script = other.GetComponent<Asteroid_Movement>();
            script.DestroyAsteroid();
        }
    }

    void ONCollisionStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Asteroid"))
        {
            Asteroid_Movement script = other.GetComponent<Asteroid_Movement>();
            script.DestroyAsteroid();
        }
    }
}
