﻿using UnityEngine;
using System.Collections;

public class Boundary_top : MonoBehaviour
{
    void Start()
    {
        transform.position = new Vector3(0f, Camera.main.orthographicSize, 0f);
    }





    void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.transform.position -= new Vector3(0f, (float)(2 * other.gameObject.transform.position.y - .5), 0f);
        print(other.gameObject.tag);
    }
}