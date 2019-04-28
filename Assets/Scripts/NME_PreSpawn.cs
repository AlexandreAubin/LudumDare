using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class NME_PreSpawn : MonoBehaviour
{
    public Transform wayPoints;
    public float timeSpawn;
    public enemyController enemyPrefab1;
    public enemyController enemyPrefab2;
    public string typeNME;
    Stopwatch timerSpawn = new Stopwatch();

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        timerSpawn.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerSpawn.ElapsedMilliseconds > timeSpawn * 1000)
        {
            if (typeNME == "buff")
            {
                enemyController temp = Instantiate(enemyPrefab1, this.gameObject.transform.position, Quaternion.identity);
                temp.player = player;
                temp.destinations = wayPoints;
            }
            else  if (typeNME == "kamikaze")
            {
                enemyController temp = Instantiate(enemyPrefab2, this.gameObject.transform.position, Quaternion.identity);
                temp.player = player;
                temp.destinations = wayPoints;
            }
            DestroyImmediate(this.gameObject);
        }
    }
}
