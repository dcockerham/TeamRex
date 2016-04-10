using UnityEngine;
using System.Collections;

public class Superlaser : MonoBehaviour {
    Player_Controller player;
    //MainController control;
    public float laser_time;
    //public float Hit_speed;
    public float spawn_time_least;
    public float spawn_time_most;
    public float speed = 0.5f;

    protected Vector3 Direction;

    private bool on;
    private bool alive;
    private float spawn_time;

	public float time = 10f;
	public ParticleSystem parts;
	public FadeObjectInOut fader;

    void Start()
    {
        Direction.x = Random.Range(-1f, 1f);
        Direction.y = Random.Range(-1f, 1f);

        Direction.x *= speed;
        Direction.y *= speed;

        player = GameObject.Find("Player").GetComponent<Player_Controller>();
        //control = GameObject.Find("GameController").GetComponent<MainController>();
        on = false;
        alive = false;
        spawn_time = Random.Range(spawn_time_least, spawn_time_most);

		parts = GetComponent<ParticleSystem> ();
		fader = GetComponent<FadeObjectInOut> ();
    }

    void Update()
    {
		transform.Translate(Direction * Time.deltaTime);

        if (player == null)
        {
            print("NO PLAYER ARGH!");
            return;
        }

		if (GetComponent<GazeAwareComponent>().HasGaze && !player.freeze)
        {
            Direction = Vector3.zero;
            StartCoroutine(laser(laser_time));
        }

		time -= Time.deltaTime;
		if (time <= 0.5f) {
			fader.FadeOut (0.5f);
			parts.enableEmission = false;
		}
		if (time <= 0f)
		{
			Destroy(this.gameObject);
		}
    }

    void OnMouseOver()
    {
        if (player.useMouse)
        {
            Direction = Vector3.zero;
            StartCoroutine(laser(laser_time));
        }
    }

    public IEnumerator laser(float num)
    {
        on = true;
        transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(num);
        alive = false;
        transform.GetChild(0).gameObject.SetActive(false);
        gameObject.active = false;
        //control.laserspawn(spawn_time, gameObject);
    }
}
