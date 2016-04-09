using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class ItemSpawner : MonoBehaviour {
    public List<GameObject> itemlist = new List<GameObject>();
    public int max;
    public float time;
    public float itemDistance;
    public float border;
    private float timer = 0f;
    private float width;
    private float height;
    // Use this for initialization
    void Start () {
        height = 2.0f * Camera.main.orthographicSize;
        width = height * Camera.main.aspect;
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        if (timer > time)
        {
            Vector2 itemToPut = new Vector2(Random.Range((border - width / 2), (width / 2 - border)), Random.Range((border - height / 2), (height / 2 - border)));

            if (!(Physics.CheckSphere(itemToPut, itemDistance)))
            {
                GameObject g = (GameObject)Instantiate(itemlist[Random.Range(0, itemlist.Count)], itemToPut, itemlist[0].transform.rotation);
                timer = 0f;
            }
        }
    }
}
