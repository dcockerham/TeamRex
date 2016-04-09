﻿using UnityEngine;
using System.Collections;

public class Boundary_left : MonoBehaviour {
    void Start()
    {
        transform.position = new Vector3(-Camera.main.orthographicSize * Camera.main.aspect, 0f,0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.transform.position -= new Vector3((float)(2 * other.gameObject.transform.position.x + .5), 0f, 0f);
    }
}
