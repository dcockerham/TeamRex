using UnityEngine;
using System.Collections;

public class Nightmare_Enemy : MonoBehaviour {

	Player_Controller player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player").GetComponent<Player_Controller>();
	}
	
	// Update is called once per frame
	void Update () {
		player = GameObject.Find("Player").GetComponent<Player_Controller>();
		if (player == null) {
			print ("NO PLAYER ARGH!");
			return;
		}

		if (GetComponent<GazeAwareComponent> ().HasGaze) {
			player.freeze = true;
		} else {
			player.freeze = false;
		}
	}
}
