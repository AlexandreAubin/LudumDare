using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public Transform spawnPoints;
    public GameObject enemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        int random = Random.Range(0, spawnPoints.childCount);
        Instantiate(enemyPrefab, spawnPoints.GetChild(random).transform.position, Quaternion.identity);

        random = Random.Range(0, spawnPoints.childCount);
        Instantiate(enemyPrefab, spawnPoints.GetChild(random).transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
