using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemSpawner : MonoBehaviour {
    public List<GameObject> itemlist = new List<GameObject>();

    public float asteroidTime;
    public int asteroidMaxNumber;
    public int asteroidInitalSpawn;

    public float fighterTime;
    public float fighterPercentage;
    public int fighterScore;

    public float nightmareTime;
    public float nightmarePercentage;
    public int nightmareScore;

    public float laserPowerUpTime;
    public float laserPowerUpPercentage;
    public int laserPowerUpScore;

    private GameObject[] currentObjects;
    private MainController gameController;

    private float width;
    private float height;

    void Start () {
        GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        gameController = gameControllerObject.GetComponent<MainController>();

        height = 2.0f * Camera.main.orthographicSize;
        width = height * Camera.main.aspect;

        InstantiateMultipleObjects(asteroidInitalSpawn, 0);

        InvokeRepeating("SpawnAsteroid", asteroidTime, asteroidTime);
        InvokeRepeating("SpawnFighter", fighterTime, fighterTime);
        InvokeRepeating("SpawnNightmare", nightmareTime, nightmareTime);
        InvokeRepeating("SpawnLaserPowerUp", laserPowerUpTime, laserPowerUpTime);
    }

    Vector2 GenerateRandomPositionOutsideBoundary(){
        float RandomWidth = 0;
        float RandomHeight = 0;
        float adjustment = (float)0.1;
        float border = 0.2f;

        if (Random.value < 0.5f)
            RandomWidth = Random.Range((-border - (width / 2)), (-adjustment - (width / 2)));
        else
            RandomWidth = Random.Range(((adjustment + width) / 2), ((width / 2) + border));

        if (Random.value < 0.5f)
            RandomHeight = Random.Range((-border - height / 2), (-adjustment - height / 2));
        else
            RandomHeight = Random.Range((adjustment + height / 2), (height / 2 + border));

        return new Vector2(RandomWidth, RandomHeight);
    }

    Vector2 GenerateRandomPositionInsideBoundary(){
        return new Vector2(Random.Range(-width / 2, width / 2), Random.Range(-height / 2, height / 2));
    }

    void InstantiateObject(int enemieType){
        Instantiate(itemlist[enemieType], GenerateRandomPositionOutsideBoundary(), itemlist[0].transform.rotation);
    }

    void InstantiateMultipleObjects(int numberToCreate, int enemieType){
        for (int x = 0; x < numberToCreate; x++)
            InstantiateObject(enemieType);
    }

    void SpawnAsteroid(){
        int asteroidCurrentNumber = 0;
        currentObjects = GameObject.FindGameObjectsWithTag("Asteroid");

        for (int x = 0; x < currentObjects.Length; x++)
            asteroidCurrentNumber += currentObjects[x].GetComponent<Asteroid_Movement>().size;

        if ((asteroidCurrentNumber + 4) <= asteroidMaxNumber)
            InstantiateObject(0);
    }

    void SpawnFighter(){
        currentObjects = GameObject.FindGameObjectsWithTag("Fighter");

        if ((gameController.score / fighterScore) > currentObjects.Length && Random.value < fighterPercentage / 100)
            InstantiateObject(1);
    }

    void SpawnNightmare(){
        currentObjects = GameObject.FindGameObjectsWithTag("Nightmare");
        if (gameController.score / nightmareScore > currentObjects.Length && Random.value < nightmarePercentage / 100)
            InstantiateObject(2);
    }

    void SpawnLaserPowerUp(){
        if (gameController.score > laserPowerUpScore && Random.value < (laserPowerUpPercentage / 100))
            Instantiate(itemlist[3], GenerateRandomPositionInsideBoundary(), itemlist[3].transform.rotation);
    }

}
