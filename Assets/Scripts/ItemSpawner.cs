using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class ItemSpawner : MonoBehaviour {
    public List<GameObject> itemlist = new List<GameObject>();
    public int maxObjects = 18;
    public int initalObjects;
    public float asteroidTime;
    public float fighterTime;
    public float itemDistance;
    public string tagObject = "Asteroid";
    public float border;

    private float asteroidtimer = 0f;
    private float fightertimer = 0f;

    private int asteroidNumber;
    private float fighterNumber = 0f;

    private float width;
    private float height;
    private GameObject[] currentObjects;

    

    // Use this for initialization
    void Start () {
        height = 2.0f * Camera.main.orthographicSize;
        width = height * Camera.main.aspect;
        InstantiateMultipleObjects(initalObjects, 0);
    }
	
	// Update is called once per frame
	void Update () {
        InstantiateAsteroid();
        InstantiateFighter();


    }

    void InstantiateFighter()
    {
        fightertimer += Time.deltaTime;

        //if (asteroidtimer > asteroidTime)

    }

    void InstantiateAsteroid()
    {
        asteroidtimer += Time.deltaTime;
        currentObjects = GameObject.FindGameObjectsWithTag("Asteroid");


        if (asteroidtimer > asteroidTime)
        {
            asteroidNumber = 0;
            for (int x = 0; x < currentObjects.Length; x++)
            {
                asteroidNumber += currentObjects[x].GetComponent<Asteroid_Movement>().size;
            }

            if ((asteroidNumber + 4) <= maxObjects)
            {
                if (InstantiateObject(0))
                {
                    asteroidtimer = 0f;
                }
            }
        }
    }

    void InstantiateMultipleObjects(int numberToCreate, int enemieType)
    {
        //
        for (int x = 0; x < numberToCreate; x++)
        {
            while (!InstantiateObject(enemieType))
            {
                continue;
            }
        }
    }

    bool InstantiateObject(int enemieType){
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
        //Debug.Log(RandomHeight);

        Vector2 itemToPut = new Vector2(RandomWidth, RandomHeight);

        //if (!(Physics.CheckSphere(itemToPut, itemDistance)))
        //{
            GameObject g = (GameObject)Instantiate(itemlist[enemieType], itemToPut, itemlist[0].transform.rotation);
            return true;
        //}
        //return false;
    }
}
