﻿using UnityEngine;
using System.Collections;

public class Boundary_right : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.transform.position -= new Vector3((float)(2 * other.gameObject.transform.position.x - .5), 0f, 0f);
    }
}
