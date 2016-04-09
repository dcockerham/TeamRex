using UnityEngine;
using System.Collections;

public class Player_Movement : MonoBehaviour {

	public float movementSpeed = 5.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.W))
		{
			transform.position += Vector3.up * Time.deltaTime * movementSpeed;
		}

		if (Input.GetKey(KeyCode.A))
		{
			transform.position += Vector3.left * Time.deltaTime * movementSpeed;
		}

		if (Input.GetKey(KeyCode.S))
		{
			transform.position += Vector3.down * Time.deltaTime * movementSpeed;
		}
		if (Input.GetKey(KeyCode.D))
		{
			transform.position += Vector3.right * Time.deltaTime * movementSpeed;
		}

		//if (Input.GetMouseButton (0))
		//{
			Vector3 mouse_po = Input.mousePosition;
			Vector3 obj = Camera.main.WorldToScreenPoint (transform.position);
			Vector3 direction = mouse_po - obj;
			direction.z = 0f;
			direction = direction.normalized;
			transform.up = -direction;
		//}


	}
}
