using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public Transform spawnPoints;
    public Transform wayPoints;
    public NME_PreSpawn enemyPrefab1;
    public NME_PreSpawn enemyPrefab2;
    public int HowmanyNME;
    public int nbrNME;
    public int nbrWaveMax;
    public GameObject exitDoor;
    public Sprite sprExitDoor;

    private int nbrWave;
    private GameObject player;
    private GameObject door;

    // Start is called before the first frame update
    void Start()
    {
        nbrNME = 0;
        nbrWave = 1;
        player = GameObject.FindGameObjectWithTag("Player");
        door = GameObject.FindGameObjectWithTag("Door");
        SpawnNMESaiyen();
        /*int random = Random.Range(0, spawnPoints.childCount);
        Instantiate(enemyPrefab, spawnPoints.GetChild(random).transform.position, Quaternion.identity);

        random = Random.Range(0, spawnPoints.childCount);
        Instantiate(enemyPrefab, spawnPoints.GetChild(random).transform.position, Quaternion.identity);*/
    }

    // Update is called once per frame
    void Update()
    {
        if (nbrNME <= 0)
        {
            if (nbrWave < nbrWaveMax)
            {
                SpawnNMEMIX();
                nbrWave++;
            }
            else
            {
                TransitionManager transitionManager = door.GetComponent<TransitionManager>();
                transitionManager.SetOpen();
                exitDoor.GetComponent<SpriteRenderer>().sprite = sprExitDoor;
            }
        }
    }

    //Fonctions
    public void SpawnNMEBuff()
    {
        for (int i = 0; i < Random.Range(1, HowmanyNME); i++)
        {
            int random = Random.Range(0, spawnPoints.childCount);
            NME_PreSpawn temp = Instantiate(enemyPrefab1, spawnPoints.GetChild(random).transform.position, Quaternion.identity);
            nbrNME++;
            temp.wayPoints = wayPoints;
            //temp.player = player;
            //temp.destinations = wayPoints;
        }
    }

    public void SpawnNMESaiyen()
    {
        for (int i = 0; i < Random.Range(1, HowmanyNME); i++)
        {
            int random = Random.Range(0, spawnPoints.childCount);
            NME_PreSpawn temp = Instantiate(enemyPrefab2, spawnPoints.GetChild(random).transform.position, Quaternion.identity);
            nbrNME++;
            temp.wayPoints = wayPoints;
            //temp.player = player;
            //temp.destinations = wayPoints;
        }
    }

    public void SpawnNMEMIX()
    {
        for (int i = 0; i < Random.Range(1, HowmanyNME); i++)
        {
            if (Random.Range(0,1)==0)
            {
                int random = Random.Range(0, spawnPoints.childCount);
                NME_PreSpawn temp = Instantiate(enemyPrefab1, spawnPoints.GetChild(random).transform.position, Quaternion.identity);
                temp.wayPoints = wayPoints;
                //temp.player = player;
                //temp.destinations = wayPoints;
            }
            else
            {
                int random = Random.Range(0, spawnPoints.childCount);
                NME_PreSpawn temp = Instantiate(enemyPrefab2, spawnPoints.GetChild(random).transform.position, Quaternion.identity);
                temp.wayPoints = wayPoints;
                //temp.player = player;
                //temp.destinations = wayPoints;
            }
            nbrNME++;
        }
    }
}
