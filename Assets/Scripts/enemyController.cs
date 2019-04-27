using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class enemyController : MonoBehaviour
{
    //public GameObject projectile;
    public Transform destinations;              //Keeps all the possible destinations in memory.
    //public Transform spanwPoints;
    //public NavMeshSurface surface;              //Allows to dynamicly bake the nav mesh.
    //public GameObject player;
    public int vitesseMouvement;
    public int cadence;
    public int pointVie;
    public int targetingRange;
    //public typeTire;
    //private NavMeshAgent nav;                   //Keeps the nav Mesh agent in memory to allow Mr. Bad Guy to move.
    private int randomDestination;
    private bool isPlayerInRange = false;
    private NavMeshAgent2D nav;

    // Initiates all variables.
    void Start()
    {
        randomDestination = Random.Range(0, destinations.childCount);
        //surface.BuildNavMesh();
        nav = GetComponent<NavMeshAgent2D>();
        /*nav = GetComponent<NavMeshAgent>();
        nav.updateRotation = false;*/
        //Vector3 w = Camera.main.ScreenToWorldPoint(destinations.GetChild(randomDestination).position);
        //nav.destination = w;
        // InvokeRepeating("InstantiateEnemyFire", cadence, cadence); //Make the enemy shoot
    }

    // Update Mr. Bad Guy's destination.
    void Update()
    {
        //nav.stoppingDistance = 0;
        /* int positionPlayer = (int)(Mathf.Sqrt((Mathf.Pow(player.transform.position.x, 2) +
                                                Mathf.Pow(player.transform.position.y, 2))));
         int positionEnemy = (int)(Mathf.Sqrt((Mathf.Pow(transform.position.x, 2) +
                                               Mathf.Pow(transform.position.y, 2))));

         if (positionPlayer - positionEnemy >= targetingRange)
         {
             isPlayerInRange = true;
             nav.SetDestination(player.transform.position);
         }
         else
         {
             isPlayerInRange = false;
         }*/

        /*if (Input.GetMouseButton(0))
        {
            Vector3 w = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GetComponent<NavMeshAgent2D>().destination = w;
            print(Input.mousePosition);
        }*/

        if (nav.remainingDistance <= 1 && !isPlayerInRange)
        {
            
            randomDestination = Random.Range(0, destinations.childCount);
            print(randomDestination);
            Vector3 w = Camera.main.ScreenToWorldPoint(destinations.GetChild(randomDestination).position);
            nav.destination = w;
        }

        //print(destinations.GetChild(0).position);
    }

    // Spanws a magic pickup.
   /* private void InstantiateEnemyFire()
    {
        GameObject mpu = Instantiate(projectile, transform.position, Quaternion.identity);
        mpu.transform.Translate(Vector3.forward * 3.0f * Time.deltaTime);
    }*/
}
