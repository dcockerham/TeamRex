using UnityEngine;
using System.Collections;

public class PowerupBehavior : MonoBehaviour {

    public float speed = 2.5f;
    public Vector3 Direction;
    public float time = 10f;

    // Use this for initialization
    void Start () {
        Direction.x = Random.Range(-1f, 1f);
        Direction.y = Random.Range(-1f, 1f);

        Direction.x *= speed;
        Direction.y *= speed;
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Direction * Time.deltaTime);

        time -= Time.deltaTime;

        if (time <= 0f)
        {
            Destroy(this.gameObject);
        }
    }
}
