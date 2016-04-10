using UnityEngine;
using System.Collections;

public class StareSelect : MonoBehaviour {

	private UnityEngine.UI.Button button;
	public float stareTime;
	public float width;
	public float height;
	public Vector3 pos;

	// Use this for initialization
	void Start () {
		button = GetComponent<UnityEngine.UI.Button> ();
		stareTime = 0f;
		width = GetComponent<RectTransform>().rect.width;
		height = GetComponent<RectTransform>().rect.height;
		pos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 lastGazePoint = GetComponent<GazePointDataComponent> ().LastGazePoint.Screen;
		if (lastGazePoint.x >= pos.x - width / 2 &&
		    lastGazePoint.x <= pos.x + width / 2 &&
		    lastGazePoint.y >= pos.y - height / 2 &&
		    lastGazePoint.y <= pos.y + height / 2) 
		{
			button.Select ();
			stareTime += Time.deltaTime;
			if (stareTime >= 0.5f) {
				button.onClick.Invoke ();
			}
		} else {
			stareTime = 0f;
		}
	}
}
