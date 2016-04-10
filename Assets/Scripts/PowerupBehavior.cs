using UnityEngine;
using System.Collections;

public class PowerupBehavior : MonoBehaviour {

    public float speed = 2.5f;
    public Vector3 Direction;
    public float time = 10f;

	private Quaternion baseRot;
	public float rotateSpeed = 5f;

    // Use this for initialization
    void Start () {
        Direction.x = Random.Range(-1f, 1f);
        Direction.y = Random.Range(-1f, 1f);
		baseRot = transform.rotation;

        Direction.x *= speed;
        Direction.y *= speed;
    }
	
	// Update is called once per frame
	void Update () {
		Quaternion tempRot = transform.rotation;
		transform.rotation = baseRot;
		transform.Translate(Direction * Time.deltaTime);
		transform.rotation = tempRot;
		transform.Rotate (0,0,rotateSpeed*Time.deltaTime);

        time -= Time.deltaTime;

        if (time <= 0f)
        {
            Destroy(this.gameObject);
        }
    }
}
