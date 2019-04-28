using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class enemyController : MonoBehaviour
{
    //public GameObject projectile;
    public Transform destinations;              //Keeps all the possible destinations in memory.
    public GameObject player;
    public float cadence;
    public int pointVie;
    public int targetingRange;
    public GameObject bulletPrefab;
    public GameObject bulletPrefabAmmo;
    public string typeNME;
    public float kamikazeSpeed;
    public float kamikazeAcc;
    //public typeTire;
    private int randomDestination;
    private bool isPlayerInRange = false;
    private bool destinationSet = false;
    private NavMeshAgent2D nav;
    private Transform[] fixedDestinations;
    private bool isShooting;
    private bool isLock;
    private Transform targetKamikaze;
    private SceneController sceneController;
    private GameObject kamikazeCorpse;
    Stopwatch timerChase = new Stopwatch();

    // Initiates all variables.
    void Start()
    {
        targetKamikaze = player.transform;
        sceneController = GameObject.FindGameObjectWithTag("SceneController").GetComponent("SceneController") as SceneController;
        isLock = false;
        isShooting = false;
        randomDestination = UnityEngine.Random.Range(0, destinations.childCount);
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
    }

    // Update Mr. Bad Guy's destination.
    void Update()
    {
        if (typeNME == "buff")
        {
            if (!destinationSet)
            {
                if (Vector3.Distance(player.transform.position, transform.position) <= targetingRange)
                {
                    isPlayerInRange = true;
                    nav.SetDestination(player.transform.position);
                    if (!isShooting)
                    {
                        isShooting = true;
                        InvokeRepeating("InstantiateEnemyFire", cadence, cadence); //Make the enemy shoot
                    }
                }
                else if (isPlayerInRange)
                {
                    isPlayerInRange = false;

                    randomDestination = UnityEngine.Random.Range(0, destinations.childCount);
                    Vector3 w = Camera.main.ScreenToWorldPoint(fixedDestinations[randomDestination].position);
                    nav.destination = w;
                    isShooting = false;
                    CancelInvoke("InstantiateEnemyFire");
                }

                if (Vector3.Distance(transform.position, nav.destination) <= 1 && !isPlayerInRange)
                {
                    randomDestination = UnityEngine.Random.Range(0, destinations.childCount);
                    print(randomDestination);
                    Vector3 w = Camera.main.ScreenToWorldPoint(fixedDestinations[randomDestination].position);
                    nav.destination = w;
                }
            }
            else
                destinationSet = false;
        }
        else if (typeNME == "kamikaze")
        {
            if (!destinationSet)
            {
                if (Vector3.Distance(player.transform.position, transform.position) <= targetingRange)
                {
                    isPlayerInRange = true;
                    nav.SetDestination(player.transform.position);
                    if (!isLock)
                    {
                        timerChase.Start();
                        isLock = true;
                        nav.speed = kamikazeSpeed;
                        nav.acceleration = kamikazeAcc;
                    }
                }

                if (isLock && timerChase.ElapsedMilliseconds > cadence * 1000)
                {
                    ExplosionKamikaze();
                    kamikazeCorpse = Instantiate(sceneController.NMEKamikazeDead, transform.position, Quaternion.identity);
                    GameObject boule = Instantiate(bulletPrefabAmmo, transform.position, Quaternion.identity);
                    boule.GetComponent<Bullet>().Moving = false;
                    DestroyImmediate(gameObject);
                    sceneController.spawncontroller.nbrNME--;
                }

                if (!isLock && Vector3.Distance(transform.position, nav.destination) <= 1 && !isPlayerInRange)
                {
                    randomDestination = UnityEngine.Random.Range(0, destinations.childCount);
                    print(randomDestination);
                    Vector3 w = Camera.main.ScreenToWorldPoint(fixedDestinations[randomDestination].position);
                    nav.destination = w;
                }
            }
            else
                destinationSet = false;
        }
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

    public void ExplosionKamikaze()
    {
        GameObject bulletGOG = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Bullet bulletG = bulletGOG.GetComponent<Bullet>();
        bulletG.SetPosition(new Vector2Int((int)transform.position.x, (int)transform.position.y));
        bulletG.SetDirection(-1, 0);

        GameObject bulletGOD = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Bullet bulletD = bulletGOD.GetComponent<Bullet>();
        bulletD.SetPosition(new Vector2Int((int)transform.position.x, (int)transform.position.y));
        bulletD.SetDirection(1, 0);

        GameObject bulletGOH = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Bullet bulletH = bulletGOH.GetComponent<Bullet>();
        bulletH.SetPosition(new Vector2Int((int)transform.position.x, (int)transform.position.y));
        bulletH.SetDirection(0, 1);

        GameObject bulletGOB = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Bullet bulletB = bulletGOB.GetComponent<Bullet>();
        bulletB.SetPosition(new Vector2Int((int)transform.position.x, (int)transform.position.y));
        bulletB.SetDirection(0, -1);
    }
}
