using UnityEngine;
using System.Collections;

public class Boundary_right : MonoBehaviour {
    void Start()
    {
        transform.position = new Vector3(Camera.main.orthographicSize * Camera.main.aspect*1.1f, 0f,0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.transform.position -= new Vector3((float)(2 * other.gameObject.transform.position.x - .5), 0f, 0f);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            return;
        }
        other.gameObject.transform.position -= new Vector3((float)(2 * other.gameObject.transform.position.y - 1.5), 0f, 0f);
    }
}
