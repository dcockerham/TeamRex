using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class ItemSpawner : MonoBehaviour {
    public List<GameObject> itemlist = new List<GameObject>();
    public int maxObjects = 18;
    public int initalObjects;
    public float time;
    public float itemDistance;
    public string tagObject = "Asteroid";
    public float border;

    private float timer = 0f;
    private float width;
    private float height;
    private GameObject[] currentObjects;
    private int asteroidNumber;

    // Use this for initialization
    void Start () {
        height = 2.0f * Camera.main.orthographicSize;
        width = height * Camera.main.aspect;
        InstantiateMultipleObjects(initalObjects);
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        currentObjects = GameObject.FindGameObjectsWithTag(tagObject);

        if (timer > time)
        {
            asteroidNumber = 0;
            for (int x = 0; x < currentObjects.Length; x++)
            {
                asteroidNumber += currentObjects[x].GetComponent<Asteroid_Movement>().size;
            }

            if ((asteroidNumber + 4) <= maxObjects)
            {
                if (InstantiateObject())
                {
                    timer = 0f;
                }
            }
        }
    }

    void InstantiateMultipleObjects(int numberToCreate){
        //
        for (int x = 0; x < numberToCreate; x++)
        {
            while (!InstantiateObject())
            {
                continue;
            }
        }
    }

    bool InstantiateObject(){
        //Returns true if it was able to instantiate an object, otherwise false.
        float RandomWidth = 0;
        float RandomHeight = 0;
        float adjustment = (float) 0.1;

        if (Random.value < 0.5f)
            RandomWidth = Random.Range((- border - (width / 2)), (- adjustment - (width / 2)));
        else
            RandomWidth = Random.Range(((adjustment + width) / 2 ), ((width / 2) + border));

        if (Random.value < 0.5f)
            RandomHeight = Random.Range((- border - height / 2), (-adjustment - height / 2));
        else
            RandomHeight = Random.Range((adjustment + height / 2), (height / 2 + border));

        //Debug.Log(RandomWidth);
        Debug.Log(RandomHeight);

        Vector2 itemToPut = new Vector2(RandomWidth, RandomHeight);

        //if (!(Physics.CheckSphere(itemToPut, itemDistance)))
        //{
            GameObject g = (GameObject)Instantiate(itemlist[Random.Range(0, itemlist.Count)], itemToPut, itemlist[0].transform.rotation);
            return true;
        //}
        //return false;
    }
}
