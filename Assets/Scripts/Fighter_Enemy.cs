using UnityEngine;
using System.Collections;

public class Fighter_Enemy : MonoBehaviour {

	private Player_Controller player;
	private Rigidbody2D rigid;
	public float speed = 1f;

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
			Destroy(col.gameObject);
			Destroy(gameObject);
		}
	}
}
