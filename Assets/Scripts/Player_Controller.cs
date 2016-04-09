using UnityEngine;
using System.Collections;

public class Player_Controller : MonoBehaviour {

	public float movementSpeed = 5.0f;
	public float timeToShoot = 0.5f;
	private float timer;
	public GameObject projectile;
	public Vector3 lastShootPos;
	public bool useMouse = false;

	// Use this for initialization
	void Start () {
		timer = timeToShoot;
		lastShootPos = Vector3.up;
	}

	// Update is called once per frame
	void Update () {
		// movement controls
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

		// get the target screen position
		if (!useMouse) {
			// if using the tobii-eye, get the gaze position
			EyeXGazePoint lastGazePoint = GetComponent<GazePointDataComponent> ().LastGazePoint;
			if (lastGazePoint.IsWithinScreenBounds) {
				Vector2 screenSpace = lastGazePoint.Screen;
				lastShootPos = Camera.main.ScreenToWorldPoint (new Vector3 (screenSpace.x, screenSpace.y, Camera.main.nearClipPlane));
				lastShootPos.z = 0f;
			}
		} else {
			// if using the mouse, get the mouse position
			Vector3 mouse_po = Input.mousePosition;
			Vector3 obj = Camera.main.WorldToScreenPoint (transform.position);
			lastShootPos = mouse_po - obj;
		}

		// calculate the direction of the target position
		Vector3 direction = lastShootPos;
		direction.z = 0f;
		direction = direction.normalized;
		transform.up = -direction;

		// if it's time to shoot, shoot
		timer -= Time.deltaTime;
		if (timer <= 0.0f) {
			timer += timeToShoot;

			Quaternion q = Quaternion.FromToRotation(Vector3.up, direction);
			GameObject go = (GameObject)Instantiate(projectile, transform.position, q);
			Rigidbody2D bulletRb = go.GetComponent<Rigidbody2D>();
			bulletRb.AddForce(go.transform.up * 1000.0f);
		}
	}
}
