using UnityEngine;
using System.Collections;

public class Boundary : MonoBehaviour {

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
        }
    }
}
