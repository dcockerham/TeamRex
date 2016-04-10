using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Fighter_Enemy : MonoBehaviour {

	private Player_Controller player;
	private Rigidbody2D rigid;
	public float speed = 1f;
    public List<GameObject> Powerups = new List<GameObject>();
	public GameObject deathParticle;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player").GetComponent<Player_Controller>();
		rigid = gameObject.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (player != null) {
			Vector3 direction = player.transform.position - transform.position;
			direction.z = 0f;
			direction = direction.normalized;
			transform.up = -direction;

			Vector2 newVel = rigid.velocity - new Vector2 (transform.up.x, transform.up.y);
			rigid.velocity = newVel.normalized * speed;
		}
	}

	void OnTriggerEnter2D (Collider2D col) {

		if (col.gameObject.tag == "Bullet")
		{
            float spawn_chance = Random.Range(-1f, 1f);
            if (spawn_chance > 0)
            {
                spawn_chance = Random.Range(-1f, 1f);

                int Powerup_spawn;
                if (spawn_chance < 0)
                {
                    Powerup_spawn = 0;
                }
                else
                {
                    Powerup_spawn = 1;
                }
                Instantiate(Powerups[Powerup_spawn].gameObject, transform.position, Quaternion.identity);
            }

			Instantiate(deathParticle, transform.position, transform.rotation);
			Destroy(col.gameObject);
			Destroy(gameObject);
		}
	}
}
