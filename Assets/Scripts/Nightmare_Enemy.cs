using UnityEngine;
using System.Collections;

public class Nightmare_Enemy : MonoBehaviour {

	Player_Controller player;
	public bool freezing = false;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player").GetComponent<Player_Controller>();
		freezing = false;
	}
	
	// Update is called once per frame
	void Update () {
		player = GameObject.Find("Player").GetComponent<Player_Controller>();
		if (player == null) {
			print ("NO PLAYER ARGH!");
			return;
		}

		if (!freezing && GetComponent<GazeAwareComponent> ().HasGaze) {
			player.StartCoroutine (player.SetFreeze (1.0f));
			StartCoroutine (SetFreezing(1.0f));

			//player.freeze = true;
			//player.transform.GetChild (0).gameObject.SetActive (true);
		} /*else {
			player.freeze = false;
			player.transform.GetChild (0).gameObject.SetActive (false);
		}*/
	}

	public IEnumerator SetFreezing(float num)
	{
		freezing = true;
		yield return new WaitForSeconds (num);
		freezing = false;
	}
}
