using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public Transform spawnPoints;
    public Transform wayPoints;
    public enemyController enemyPrefab1;
    public enemyController enemyPrefab2;
    public int HowmanyNME;
    public int nbrNME;
    public int nbrWaveMax;

    public int nbrWave;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        SpawnNMEBuff();
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
                //OPEN DOOR
                print("OPEN DOOR");
            }
        }
    }

    //Fonctions
    public void SpawnNMEBuff()
    {
        for (int i = 0; i < Random.Range(1, HowmanyNME); i++)
        {
            int random = Random.Range(0, spawnPoints.childCount);
            enemyController temp = Instantiate(enemyPrefab1, spawnPoints.GetChild(random).transform.position, Quaternion.identity);
            nbrNME++;
            temp.player = player;
            temp.destinations = wayPoints;
        }
    }

    public void SpawnNMESaiyen()
    {
        for (int i = 0; i < Random.Range(1, HowmanyNME); i++)
        {
            int random = Random.Range(0, spawnPoints.childCount);
            enemyController temp = Instantiate(enemyPrefab2, spawnPoints.GetChild(random).transform.position, Quaternion.identity);
            nbrNME++;
            temp.player = player;
            temp.destinations = wayPoints;
        }
    }

    public void SpawnNMEMIX()
    {
        for (int i = 0; i < Random.Range(1, HowmanyNME); i++)
        {
            if (Random.Range(0,1)==0)
            {
                int random = Random.Range(0, spawnPoints.childCount);
                enemyController temp = Instantiate(enemyPrefab1, spawnPoints.GetChild(random).transform.position, Quaternion.identity);
                temp.player = player;
                temp.destinations = wayPoints;
            }
            else
            {
                int random = Random.Range(0, spawnPoints.childCount);
                enemyController temp = Instantiate(enemyPrefab2, spawnPoints.GetChild(random).transform.position, Quaternion.identity);
                temp.player = player;
                temp.destinations = wayPoints;
            }
            nbrNME++;
        }
    }
}
