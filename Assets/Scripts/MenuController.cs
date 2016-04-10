using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

	public GameObject credits;
	public GameObject lockOn;
	public GameObject startButton;
	public Vector3 startLoc;
	public Vector3 lastShootPos = new Vector3(0, 0, 200);
	public Vector3 gazePoint;

	// Use this for initialization
	void Start () {
		//startLoc = Camera.main.ScreenToWorldPoint (new Vector3 (startButton.transform.position.x, startButton.transform.position.y, 8f));
		startLoc = startButton.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		EyeXGazePoint lastGazePoint = GetComponent<GazePointDataComponent> ().LastGazePoint;
		gazePoint = lastGazePoint.Screen;
		if (lastGazePoint.IsWithinScreenBounds) {
			Vector2 screenSpace = lastGazePoint.Screen;
			lastShootPos = Camera.main.ScreenToWorldPoint (new Vector3 (screenSpace.x, screenSpace.y, 8f));
			lastShootPos.z = 0f;
		}

		/*Vector3 mouse_po = Input.mousePosition;
		lastShootPos = Camera.main.ScreenToWorldPoint (new Vector3 (mouse_po.x, mouse_po.y, 8f));
		lastShootPos.z = 0f;
		gazePoint = mouse_po;*/

		lockOn.transform.position = lastShootPos;
	}

	public void StartButton() {
		Application.LoadLevel(1);
	}

	public void MouseStartButton() {
		Application.LoadLevel(2);
	}

	public void LastResort() {
		Application.Quit();
	}

	public void DisplayCredits() {
		credits.SetActive(!credits.activeSelf);
	}
}
