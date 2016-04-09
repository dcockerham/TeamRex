using UnityEngine;
using System.Collections;

public class Boundary : MonoBehaviour {

    void OnTriggerExit(Collider2D other)
    {
        Destroy(other.gameObject);
        print(other.tag.ToString());
    }
}
