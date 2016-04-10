using UnityEngine;
using System.Collections;

public class Boundary_right : MonoBehaviour {
    private float width;
    private float height;
    public float border;

    void Start()
    {
        height = 2.0f * Camera.main.orthographicSize;
        width = height * Camera.main.aspect;
        transform.position = new Vector3(border + width / 2, 0f, 0f);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.transform.position = new Vector3(-1 - width/2, other.gameObject.transform.position.y, other.gameObject.transform.position.z);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        other.gameObject.transform.position = new Vector3(-1 -width/2, other.gameObject.transform.position.y, other.gameObject.transform.position.z);
    }
}
