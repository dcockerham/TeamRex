using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newPos = gameObject.transform.position;

		if (Input.GetKey (KeyCode.W)) {
			newPos.y += 0.1f;
		}
		if (Input.GetKey (KeyCode.S)) {
			newPos.y -= 0.1f;
		}
		if (Input.GetKey (KeyCode.A)) {
			newPos.x -= 0.1f;
		}
		if (Input.GetKey (KeyCode.D)) {
			newPos.x += 0.1f;
		}

		gameObject.transform.position = newPos;
	}
}
