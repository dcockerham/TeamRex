using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Nightmare_Enemy : MonoBehaviour {

	Player_Controller player;
	public bool freezing = false;
	public float freezeTime = 1.5f;

	private Quaternion baseRot;
	public float rotateSpeed = -5f;

    public float speed = 2.5f;
    public Vector3 Direction;

    public List<GameObject> Powerups = new List<GameObject>();

    int life = 3;

    // Use this for initialization
    void Start () {

        Direction.x = Random.Range(-1f, 1f);
        Direction.y = Random.Range(-1f, 1f);
		baseRot = transform.rotation;

        Direction.x *= speed;
        Direction.y *= speed;

        player = GameObject.Find("Player").GetComponent<Player_Controller>();
		freezing = false;
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            life--;
            Destroy(col.gameObject);

			if (life <= 0) {
				float spawn_chance = Random.Range (-1f, 1f);
				if (spawn_chance > 0) {
					spawn_chance = Random.Range (-1f, 1f);

					int Powerup_spawn;
					if (spawn_chance < 0) {
						Powerup_spawn = 0;
					} else {
						Powerup_spawn = 1;
					}
					Instantiate (Powerups [Powerup_spawn].gameObject, transform.position, Quaternion.identity);
				}
				StartCoroutine (DeathBurst(0.4f));
			} else {
				StartCoroutine (DamageFlash(0.4f));
			}
        }
    }

    // Update is called once per frame
    void Update () {
		Quaternion tempRot = transform.rotation;
		transform.rotation = baseRot;
		transform.Translate(Direction * Time.deltaTime);
		transform.rotation = tempRot;
		transform.Rotate (0,0,rotateSpeed*Time.deltaTime);

        if (player == null) {
			print ("NO PLAYER ARGH!");
			return;
		}

		if (!freezing && GetComponent<GazeAwareComponent> ().HasGaze) {
			player.StartCoroutine (player.SetFreeze (freezeTime));
			StartCoroutine (SetFreezing(freezeTime));
		}
	}

	void OnMouseOver()
	{
		if (!freezing && player.useMouse) {
			player.StartCoroutine (player.SetFreeze (freezeTime));
			StartCoroutine (SetFreezing(freezeTime));
		}
	}

	public IEnumerator SetFreezing(float num)
	{
		freezing = true;
		transform.GetChild (0).gameObject.SetActive (true);
		transform.GetChild (1).gameObject.SetActive (false);
		transform.GetChild (2).gameObject.SetActive (false);
		yield return new WaitForSeconds (num);
		freezing = false;
		transform.GetChild (0).gameObject.SetActive (false);
		transform.GetChild (1).gameObject.SetActive (true);
		transform.GetChild (2).gameObject.SetActive (true);
	}

	public IEnumerator DamageFlash(float num)
	{
		transform.GetChild (3).gameObject.SetActive (true);
		yield return new WaitForSeconds (num);
		transform.GetChild (3).gameObject.SetActive (false);
	}

	public IEnumerator DeathBurst(float num)
	{
		transform.GetChild (0).gameObject.SetActive (false);
		transform.GetChild (1).gameObject.SetActive (false);
		transform.GetChild (2).gameObject.SetActive (false);
		transform.GetChild (3).gameObject.SetActive (false);
		transform.GetChild (4).gameObject.SetActive (false);
		GetComponent<SpriteRenderer> ().enabled = false;
		//transform.GetChild (6).gameObject.SetActive (false);
		transform.GetChild (5).gameObject.SetActive (true);
		yield return new WaitForSeconds (num);
		Destroy (gameObject);
	}
}
