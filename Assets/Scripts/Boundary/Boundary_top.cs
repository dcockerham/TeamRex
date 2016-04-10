using UnityEngine;
using System.Collections;

public class Boundary_top : MonoBehaviour
{
    void Start()
    {
        transform.position = new Vector3(0f, Camera.main.orthographicSize*1.2f, 0f);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.transform.position -= new Vector3(0f, (float)(2 * other.gameObject.transform.position.y - .5), 0f);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Asteroid"))
        {
            other.gameObject.transform.position -= new Vector3(0f, (float)(2 * other.gameObject.transform.position.y - 1.5), 0f);
        }
    }
}
