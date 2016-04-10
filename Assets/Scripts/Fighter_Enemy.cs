using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Fighter_Enemy : MonoBehaviour {

	private Player_Controller player;
	private Rigidbody2D rigid;
	public float speed = 1f;
    public List<GameObject> Powerups = new List<GameObject>();

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
			print(newVel);
			rigid.velocity = newVel.normalized * speed;
		}
	}

	void OnTriggerEnter2D (Collider2D col) {

		if (col.gameObject.tag == "Bullet")
		{
            //int spawn_chance = Random.Range(0, 1);
            //if (spawn_chance == 1)
            //{
                int Powerup_spawn = Random.Range(0, 1);
                Instantiate(Powerups[Powerup_spawn].gameObject);
            //}

			Destroy(col.gameObject);
			Destroy(gameObject);
		}
	}
}
