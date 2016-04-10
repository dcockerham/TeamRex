using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Nightmare_Enemy : MonoBehaviour {

	Player_Controller player;
	public bool freezing = false;
	public float freezeTime = 1.5f;

    public float speed = 2.5f;
    public Vector3 Direction;

    public List<GameObject> Powerups = new List<GameObject>();

    // Use this for initialization
    void Start () {

        Direction.x = Random.Range(-1f, 1f);
        Direction.y = Random.Range(-1f, 1f);

        Direction.x *= speed;
        Direction.y *= speed;

        player = GameObject.Find("Player").GetComponent<Player_Controller>();
		freezing = false;
	}

    void OnTriggerEnter2D(Collider2D col)
    {

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

            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update () {

        transform.Translate(Direction * Time.deltaTime);

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
		yield return new WaitForSeconds (num);
		freezing = false;
		transform.GetChild (0).gameObject.SetActive (false);
	}
}
