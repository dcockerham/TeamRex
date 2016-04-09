using UnityEngine;
using System.Collections;

public class Boundary_left : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.transform.position -= new Vector3((float)(2 * other.gameObject.transform.position.x + .1), 0f, 0f);
    }
}
