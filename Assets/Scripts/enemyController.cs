using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class enemyController : MonoBehaviour
{
    //public GameObject projectile;
    public Transform destinations;              //Keeps all the possible destinations in memory.
    public GameObject player;
    public int cadence;
    public int pointVie;
    public int targetingRange;
    public GameObject bulletPrefab;
    //public typeTire;
    private int randomDestination;
    private bool isPlayerInRange = false;
    private bool destinationSet = false;
    private NavMeshAgent2D nav;
    private Transform[] fixedDestinations;

    // Initiates all variables.
    void Start()
    {
        randomDestination = Random.Range(0, destinations.childCount);
        nav = GetComponent<NavMeshAgent2D>();
        fixedDestinations = new Transform[destinations.childCount];

        for (int i = 0; i < destinations.childCount; i++)
        {
            fixedDestinations[i] = destinations.GetChild(i);
            fixedDestinations[i].position = new Vector3((fixedDestinations[i].position.x * 2) + 200,
                                                        (fixedDestinations[i].position.y * 3) + 75, 0);
        }

        Vector3 w = Camera.main.ScreenToWorldPoint(fixedDestinations[randomDestination].position);
        nav.destination = w;
        destinationSet = true;

        InvokeRepeating("InstantiateEnemyFire", cadence, cadence); //Make the enemy shoot
    }

    // Update Mr. Bad Guy's destination.
    void Update()
    {
        if (!destinationSet)
        {
            if (Vector3.Distance(player.transform.position, transform.position) <= targetingRange)
            {
                isPlayerInRange = true;
                nav.SetDestination(player.transform.position);
                print("yes");
            }
            else if (isPlayerInRange)
            {
                isPlayerInRange = false;

                randomDestination = Random.Range(0, destinations.childCount);
                Vector3 w = Camera.main.ScreenToWorldPoint(fixedDestinations[randomDestination].position);
                nav.destination = w;
                print("enemy gave up");
            }

            if (Vector3.Distance(transform.position, nav.destination) <= 1 && !isPlayerInRange)
            {
                randomDestination = Random.Range(0, destinations.childCount);
                print(randomDestination);
                Vector3 w = Camera.main.ScreenToWorldPoint(fixedDestinations[randomDestination].position);
                nav.destination = w;
                print("dest: " + nav.destination);
            }
        }
        else
            destinationSet = false;
    }

    // Spanws a magic pickup.
     private void InstantiateEnemyFire()
     {
        GameObject bulletGO = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        bullet.SetPosition(new Vector2Int((int)transform.position.x, (int)transform.position.y));
        bullet.SetDirection((int)(player.transform.position.x - transform.position.x),
                            (int)(player.transform.position.y - transform.position.y));
    }
}
