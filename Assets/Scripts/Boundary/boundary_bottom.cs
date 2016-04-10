using UnityEngine;
using System.Collections;

public class boundary_bottom : MonoBehaviour {
    private float width;
    private float height;
    public float border;

    void Start()
    {
        height = 2.0f * Camera.main.orthographicSize;
        width = height * Camera.main.aspect;
        transform.position = new Vector3(0f, - border - height / 2, 0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.transform.position = new Vector3(other.gameObject.transform.position.x, 1+ height/2, other.gameObject.transform.position.z);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        other.gameObject.transform.position = new Vector3(other.gameObject.transform.position.x, 1+ height/2, other.gameObject.transform.position.z);
    }
}
