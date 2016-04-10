using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player_Controller : MonoBehaviour {

	public float movementSpeed = 5.0f;
	public float timeToShoot = 0.5f;
	private float timer;


	public GameObject projectile;
	public GameObject lockOn;
	public Vector3 lastShootPos;
	public bool useMouse = false;
	public bool freeze = false;
	public float bulletForce = 500.0f;
    public Rigidbody2D rb;
    public int force = 10;
    public GameObject deathParticle;
    private MainController mainController;

    Dictionary<string, bool> Powerups = new Dictionary<string, bool>();
	Dictionary<string, float> PowerupTimes = new Dictionary<string, float>();
	Dictionary<string, float> PowerupMaxTimes = new Dictionary<string, float>();

	// Use this for initialization
	void Start () {
        GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        mainController = gameControllerObject.GetComponent<MainController>();

        timer = timeToShoot;
		lastShootPos = Vector3.up;
		freeze = false;
		lockOn = GameObject.Find("LockOn");
        rb = GetComponent<Rigidbody2D>();

        Powerups.Add("IncreaseFireRate", false);
		PowerupMaxTimes.Add("IncreaseFireRate", 10.0f);
		PowerupTimes.Add("IncreaseFireRate", 0);

        Powerups.Add("Invincibility", false);
        PowerupMaxTimes.Add("Invincibility", 10.0f);
        PowerupTimes.Add("Invincibility", 0);
	}

	// Update is called once per frame
	void Update () {
		// movement controls
		if (!freeze) {
			if (Input.GetKey (KeyCode.W)) {
                //transform.position += Vector3.up * Time.deltaTime * movementSpeed;
                rb.AddForce(Vector3.up * force);
			}
			if (Input.GetKey (KeyCode.A)) {
                //transform.position += Vector3.left * Time.deltaTime * movementSpeed;
                rb.AddForce(Vector3.left * force);
            }
			if (Input.GetKey (KeyCode.S)) {
                //transform.position += Vector3.down * Time.deltaTime * movementSpeed;
                rb.AddForce(Vector3.down * force);
            }
			if (Input.GetKey (KeyCode.D)) {
                //transform.position += Vector3.right * Time.deltaTime * movementSpeed;
                rb.AddForce(Vector3.right * force);
            }
		}

		// power up handling
		foreach (string powerup in PowerupMaxTimes.Keys)
		{
			if (Powerups[powerup] == true)
			{
				PowerupTimes[powerup] -= Time.deltaTime;
			}
			if (PowerupTimes[powerup] <= 0)
			{
				Powerups[powerup] = false;
			}
		}

		if (!Powerups["Invincibility"]) {
			transform.GetChild (1).gameObject.SetActive (true);
			transform.GetChild (2).gameObject.SetActive (false);
		} else {
			transform.GetChild (2).gameObject.SetActive (true);
			transform.GetChild (1).gameObject.SetActive (false);
		}
		if (!Powerups["IncreaseFireRate"]) {
			transform.GetChild (3).gameObject.SetActive (false);
		} else {
			transform.GetChild (3).gameObject.SetActive (true);
		}

		if (!freeze) {
			// get the target screen position
			if (!useMouse) {
				// if using the tobii-eye, get the gaze position
				EyeXGazePoint lastGazePoint = GetComponent<GazePointDataComponent> ().LastGazePoint;
				if (lastGazePoint.IsWithinScreenBounds) {
					Vector2 screenSpace = lastGazePoint.Screen;
					lastShootPos = Camera.main.ScreenToWorldPoint (new Vector3 (screenSpace.x, screenSpace.y, Camera.main.nearClipPlane));
					lastShootPos.z = 0f;
					lockOn.transform.position = lastShootPos;
					lastShootPos = new Vector3 (screenSpace.x, screenSpace.y, 0f);
				}
			} else {
				// if using the mouse, get the mouse position
				Vector3 mouse_po = Input.mousePosition;
				//Vector3 obj = Camera.main.WorldToScreenPoint (transform.position);
				lastShootPos = Camera.main.ScreenToWorldPoint (new Vector3 (mouse_po.x, mouse_po.y, Camera.main.nearClipPlane));
				lastShootPos.z = 0f;
				lockOn.transform.position = lastShootPos;
				lastShootPos = mouse_po;
			}

			// calculate the direction of the target position
			//lockOn.transform.position = lastShootPos;
			Vector3 direction = lastShootPos - Camera.main.WorldToScreenPoint (transform.position);
			direction.z = 0f;
			direction = direction.normalized;
			transform.up = -direction;

			// if it's time to shoot, shoot
			if (Powerups ["IncreaseFireRate"] == true && timer > 0.2f) {
				timer = 0.2f;
			}
			timer -= Time.deltaTime;
			if (timer <= 0.0f) {
				if (Powerups["IncreaseFireRate"] == true){
					timer += 0.2f;
				}
				else{
					timer += timeToShoot;
				}

				Quaternion q = Quaternion.FromToRotation (Vector3.up, direction);
				GameObject go = (GameObject)Instantiate (projectile, transform.position, q);
				Rigidbody2D bulletRb = go.GetComponent<Rigidbody2D> ();
				bulletRb.AddForce (go.transform.up * bulletForce);

				AudioSource audio = GetComponent<AudioSource> ();
				if (audio != null) {
					audio.Play();
				}
			}
		}
	}


	void OnTriggerEnter2D (Collider2D col)
	{
        if (Powerups["Invincibility"] == false)
        {
            if (col.gameObject.tag == "Asteroid")
            {
                mainController.Death();
                Instantiate(deathParticle, transform.position, transform.rotation);

                Vector2 itemToPut = new Vector2(100000, 100000);
                transform.position = itemToPut;
                StartCoroutine(waitFunction(3f));
            }
        }

		if (Powerups.ContainsKey(col.gameObject.tag))
		{
			Powerups[col.gameObject.tag] = true;
			PowerupTimes[col.gameObject.tag] = PowerupMaxTimes[col.gameObject.tag];
            Destroy(col.gameObject);
		}
	}

	public IEnumerator SetFreeze(float num)
	{
		freeze = true;
		transform.GetChild (0).gameObject.SetActive (true);
		yield return new WaitForSeconds (num);
		freeze = false;
		transform.GetChild (0).gameObject.SetActive (false);
	}

    public IEnumerator waitFunction(float waitTime)
    {
		Powerups["Invincibility"] = false;
		Powerups["IncreaseFireRate"] = false;
		freeze = true;
		yield return new WaitForSeconds(waitTime);
		freeze = false;

        Vector2 itemToPut;
        itemToPut = new Vector2(0, 0);
        transform.position = itemToPut;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;

		Powerups["Invincibility"] = true;
		PowerupTimes["Invincibility"] = 3.0f;
    }

}
