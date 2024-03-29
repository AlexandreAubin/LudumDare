﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public const string PLAYER_CLASS = "Player";

    public GameObject[] obstacles;

    public Player player;
    public SpawnController spawncontroller;
    public GameObject NMEBuffDead;
    public GameObject NMEKamikazeDead;

    public GameObject[] bullets;

    public GameObject bulletPrefab;

    public GameObject[] doors;

    // Start is called before the first frame update
    void Start()
    {
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent(PLAYER_CLASS) as Player;
        doors = GameObject.FindGameObjectsWithTag("Door");

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool isThereCollisionWithPlayer(Vector2Int position,GameObject currentObject)
    {
        this.bullets = GameObject.FindGameObjectsWithTag("Projectile");
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Projectile");
        GameObject[] holes = GameObject.FindGameObjectsWithTag("Hole");

        foreach (GameObject door in doors)
        {
            //The size of the current object on the UI
            var UISize = currentObject.GetComponent<SpriteRenderer>().bounds.size;
            Rect UIobjRect = new Rect((int)Math.Round(position.x - (UISize.x / 2)), (int)Math.Round(position.y - (UISize.y / 2)), (int)Math.Round(UISize.x), (int)Math.Round(UISize.x));
            Vector2Int obstaclePoint = new Vector2Int((int)Math.Round(door.transform.position.x - (door.transform.localScale.x / 2)), (int)Math.Round(door.transform.position.y - (door.transform.localScale.y / 2)));
            Vector2Int obstacleRect = new Vector2Int((int)Math.Round(door.transform.localScale.x), (int)Math.Round(door.transform.localScale.y));
            Rect UIObstacleRect = new Rect(obstaclePoint, obstacleRect);

            if (UIobjRect.Overlaps(UIObstacleRect))
            {
                if (door.GetComponent<TransitionManager>().isOpen)
                {
                    MusicManager.GetMusicManager().PlaySound("Porte_ferme");
                    StartCoroutine(ChangeSceneAfterClip(2, door));
                    DestroyImmediate(GameObject.FindGameObjectWithTag("Player"));
                    return true;
                }
            }
        }
        
        foreach (GameObject obstacle in obstacles)
        {
            //The size of the current object on the UI
            var UISize = currentObject.GetComponent<SpriteRenderer>().bounds.size;
            Rect UIobjRect = new Rect((int)Math.Round(position.x - (UISize.x / 2)), (int)Math.Round(position.y - (UISize.y / 2)), (int)Math.Round(UISize.x), (int)Math.Round(UISize.x));
            Vector2Int obstaclePoint = new Vector2Int((int)Math.Round(obstacle.transform.position.x - (obstacle.transform.localScale.x /2)), (int)Math.Round(obstacle.transform.position.y - (obstacle.transform.localScale.y / 2)));
            Vector2Int obstacleRect = new Vector2Int((int)Math.Round(obstacle.transform.localScale.x), (int)Math.Round(obstacle.transform.localScale.y));
            Rect UIObstacleRect = new Rect(obstaclePoint, obstacleRect);

            if (UIobjRect.Overlaps(UIObstacleRect))
            {
                return true;
            }
        }

        foreach (GameObject hole in holes)
        {
            //The size of the current object on the UI
            var UISize = currentObject.GetComponent<SpriteRenderer>().bounds.size;
            Rect UIobjRect = new Rect((int)Math.Round(position.x - (UISize.x / 2)), (int)Math.Round(position.y - (UISize.y / 2)), (int)Math.Round(UISize.x), (int)Math.Round(UISize.x));
            Vector2Int obstaclePoint = new Vector2Int((int)Math.Round(hole.transform.position.x - (hole.transform.localScale.x / 2)), (int)Math.Round(hole.transform.position.y - (hole.transform.localScale.y / 2)));
            Vector2Int obstacleRect = new Vector2Int((int)Math.Round(hole.transform.localScale.x), (int)Math.Round(hole.transform.localScale.y));
            Rect UIObstacleRect = new Rect(obstaclePoint, obstacleRect);

            if (UIobjRect.Overlaps(UIObstacleRect))
            {
                return true;
            }
        }

        foreach (GameObject bullet in bullets)
        {
            //The size of the current object on the UI
            var UISize = currentObject.GetComponent<SpriteRenderer>().bounds.size;
            Rect UIobjRect = new Rect((int)Math.Round(position.x - (UISize.x / 2)), (int)Math.Round(position.y - (UISize.y / 2)), (int)Math.Round(UISize.x), (int)Math.Round(UISize.x));
            Vector2Int obstaclePoint = new Vector2Int((int)Math.Round(bullet.transform.position.x - (bullet.transform.localScale.x / 2)), (int)Math.Round(bullet.transform.position.y - (bullet.transform.localScale.y / 2)));
            Vector2Int obstacleRect = new Vector2Int((int)Math.Round(bullet.transform.localScale.x), (int)Math.Round(bullet.transform.localScale.y));
            Rect UIObstacleRect = new Rect(obstaclePoint, obstacleRect);

            if (UIobjRect.Overlaps(UIObstacleRect))
            {
                if(!bullet.GetComponent<Bullet>().Moving)
                {
                    DestroyImmediate(bullet);
                    player.CurrentHealth++;
                    player.UpdateHealth();
                    return true;
                }
            }
        }



        return false;
    }

    private IEnumerator ChangeSceneAfterClip(int seconds, GameObject door)
    {
        float pauseEndTime = Time.realtimeSinceStartup + seconds;

        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return null; // Attend un frame
        }

        door.GetComponent<TransitionManager>().LoadNextScene();
    }


    public bool BulletsHitSomething(Vector2Int position,GameObject currentObject)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (currentObject.CompareTag("Enemy Projectile"))
        {
            //The size of the current object on the UI
            var UISize = currentObject.GetComponent<SpriteRenderer>().bounds.size;
            Rect UIobjRect = new Rect((int)Math.Round(position.x - (UISize.x / 2)), (int)Math.Round(position.y - (UISize.y / 2)), (int)Math.Round(UISize.x), (int)Math.Round(UISize.y));
            Vector2Int obstaclePoint = new Vector2Int((int)Math.Round(player.transform.position.x - (player.transform.localScale.x / 2)), (int)Math.Round(player.transform.position.y - (player.transform.localScale.y / 2)));
            Vector2Int obstacleRect = new Vector2Int((int)Math.Round(player.transform.localScale.x), (int)Math.Round(player.transform.localScale.y));
            Rect UIObstacleRect = new Rect(obstaclePoint, obstacleRect);

            if (UIobjRect.Overlaps(UIObstacleRect))
            {
                if (currentObject.GetComponent<Bullet>().Moving)
                {
                    DestroyImmediate(currentObject);
                    player.TakeDamage(1);

                    return true;
                }
            }
        }

        foreach (GameObject obstacle in obstacles)
        {
            //The size of the current object on the UI
            var UISize = currentObject.GetComponent<SpriteRenderer>().bounds.size;
            Rect UIobjRect = new Rect((int)Math.Round(position.x - (UISize.x / 2)), (int)Math.Round(position.y - (UISize.y / 2)), (int)Math.Round(UISize.x), (int)Math.Round(UISize.y));
            Vector2Int obstaclePoint = new Vector2Int((int)Math.Round(obstacle.transform.position.x - (obstacle.transform.localScale.x / 2)), (int)Math.Round(obstacle.transform.position.y - (obstacle.transform.localScale.y / 2)));
            Vector2Int obstacleRect = new Vector2Int((int)Math.Round(obstacle.transform.localScale.x), (int)Math.Round(obstacle.transform.localScale.y));
            Rect UIObstacleRect = new Rect(obstaclePoint, obstacleRect);

            if (UIobjRect.Overlaps(UIObstacleRect))
            {
                if (currentObject.CompareTag("Enemy Projectile"))
                    DestroyImmediate(currentObject);

                return true;
            }
        }

        foreach (GameObject enemy in enemies)
        {
            SpriteRenderer sprite = enemy.GetComponent<SpriteRenderer>();

            //The size of the current object on the UI
            var UISize = currentObject.GetComponent<SpriteRenderer>().bounds.size;
            Rect UIobjRect = new Rect((int)Math.Round(position.x - (UISize.x / 2)), (int)Math.Round(position.y - (UISize.y / 2)), (int)Math.Round(UISize.x), (int)Math.Round(UISize.y));
            Vector2Int obstaclePoint = new Vector2Int((int)Math.Round(enemy.transform.position.x - (sprite.size.x * enemy.transform.localScale.x / 2)), (int)Math.Round(enemy.transform.position.y - (sprite.size.y * enemy.transform.localScale.y / 2)));
            Vector2Int obstacleRect = new Vector2Int((int)Math.Round(sprite.size.x * enemy.transform.localScale.x), (int)Math.Round((sprite.size.y * enemy.transform.localScale.y)));
            Rect UIObstacleRect = new Rect(obstaclePoint, obstacleRect);

            if (UIobjRect.Overlaps(UIObstacleRect))
            {
                if (currentObject.CompareTag("Projectile"))
                {
                    DestroyImmediate(currentObject);
                    enemyController temp = enemy.GetComponent<enemyController>();
                    if (temp.pointVie > 1)
                    {
                        temp.pointVie--;
                    }
                    else
                    {
                        if (temp.typeNME == "buff")
                        {
                            Instantiate(NMEBuffDead, enemy.transform.position, Quaternion.identity);
                            for(int i = 0; i < 3; i++)
                            {
                                GameObject boule = Instantiate(bulletPrefab, enemy.transform.position, Quaternion.identity);
                                boule.GetComponent<Bullet>().Moving = false;
                            }
                        }
                        else if (temp.typeNME == "kamikaze")
                        {
                            Instantiate(NMEKamikazeDead, enemy.transform.position, Quaternion.identity);
                            GameObject boule = Instantiate(bulletPrefab, enemy.transform.position, Quaternion.identity);
                            boule.GetComponent<Bullet>().Moving = false;
                            temp.ExplosionKamikaze();
                        }
                        DestroyImmediate(enemy);
                        spawncontroller.nbrNME--;
                    }
                    return true;
                }
            }
        }




        //TODO ADD COLLISION WITH THE ENNEMIES

        return false;
    }

    public void SpawnBullet(Transform transform, Vector2Int position, float facing_x, float facing_y)
    {
        GameObject bulletGO = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        bullet.SetPosition(position);
        bullet.SetDirection(facing_x, facing_y);
    }


}
