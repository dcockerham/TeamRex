using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class ItemSpawner : MonoBehaviour {
    public List<GameObject> itemlist = new List<GameObject>();
    public int maxObjects = 18;
    public int initalObjects;
    public float border;

    public float asteroidTime;
    public float fighterTime;
    public float fighterPercentage;
    public int fighterScore;

    public float nightmareTime;
    public float nightmarePercentage;
    public int nightmareScore;

    public float laserPowerUpTime;
    public float laserPowerUpPercentage;
    public int laserPowerUpScore;

    private float asteroidtimer = 0f;
    private float fightertimer = 0f;
    private float nightmaretimer = 0f;
    private float laserPowerUpTimer = 0f;

    private int asteroidNumber;


    private float width;
    private float height;
    private GameObject[] currentObjects;
    private MainController mainController;



    // Use this for initialization
    void Start () {
        GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        mainController = gameControllerObject.GetComponent<MainController>();

        height = 2.0f * Camera.main.orthographicSize;
        width = height * Camera.main.aspect;
        InstantiateMultipleObjects(initalObjects, 0);
    }
	
	// Update is called once per frame
	void Update () {
        InstantiateAsteroid();
        InstantiateFighter();
        InstantiateNightmare();
        InstantiateLaserPowerUp();
    }

    void InstantiateLaserPowerUp()
    {
        laserPowerUpTimer += Time.deltaTime;
        currentObjects = GameObject.FindGameObjectsWithTag("Nightmare");
        //Debug.Log("laserPowerUpTimer");
        //Debug.Log(laserPowerUpTimer);
        //Debug.Log(laserPowerUpTime);
        if (laserPowerUpTimer > laserPowerUpTime)
        {
            laserPowerUpTimer = 0f;
            float random = Random.value;
            //Debug.Log("percentage");
            //Debug.Log(random);
            //Debug.Log((laserPowerUpPercentage / 100));
            //Debug.Log(random < (laserPowerUpPercentage / 100));
            if (random < (laserPowerUpPercentage / 100))
            {
                //Debug.Log("TRUE");
                if (mainController.score > laserPowerUpScore)
                {
                    Vector2 itemToPut = new Vector2(Random.Range(-width / 2, width / 2), Random.Range(-height / 2, height / 2));
                    GameObject g = (GameObject)Instantiate(itemlist[3], itemToPut, itemlist[3].transform.rotation);
                    
                }
            }

        }

    }
    void InstantiateNightmare()
    {
        nightmaretimer += Time.deltaTime;
        currentObjects = GameObject.FindGameObjectsWithTag("Nightmare");


        if (nightmaretimer > nightmareTime)
        {
            nightmaretimer = 0f;

            if (Random.value < nightmarePercentage / 100)
            {
                if (mainController.score / nightmareScore > currentObjects.Length)
                {
                    if (InstantiateObject(2))
                    {
                    }

                }
            }
        }
    }

    void InstantiateFighter()
    {
        fightertimer += Time.deltaTime;
        currentObjects = GameObject.FindGameObjectsWithTag("Fighter");


        if (fightertimer > fighterTime)
        {
            fightertimer = 0f;

            if (Random.value < fighterPercentage / 100)
            {
                if (mainController.score / fighterScore > currentObjects.Length)
                {
                    if (InstantiateObject(1))
                    {
                    }

                }
            }
        }
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
