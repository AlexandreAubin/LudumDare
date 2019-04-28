using System;
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

    public bool isThereCollision(Vector2Int position,GameObject currentObject)
    {
        this.bullets = GameObject.FindGameObjectsWithTag("Projectile");

        foreach (GameObject door in doors)
        {
            //The size of the current object on the UI
            var UISize = currentObject.GetComponent<SpriteRenderer>().bounds.size;
            Rect UIobjRect = new Rect((int)Math.Round(position.x - (UISize.x / 2)), (int)Math.Round(position.y - (UISize.y / 2)), (int)Math.Round(UISize.x), (int)Math.Round(UISize.y));
            Vector2Int obstaclePoint = new Vector2Int((int)Math.Round(door.transform.position.x - (door.transform.localScale.x / 2)), (int)Math.Round(door.transform.position.y - (door.transform.localScale.y / 2)));
            Vector2Int obstacleRect = new Vector2Int((int)Math.Round(door.transform.localScale.x), (int)Math.Round(door.transform.localScale.y));
            Rect UIObstacleRect = new Rect(obstaclePoint, obstacleRect);

            if (UIobjRect.Overlaps(UIObstacleRect))
            {
                door.GetComponent<TransitionManager>().LoadNextScene();
                return true;
            }
        }
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Projectile");


        foreach (GameObject obstacle in obstacles)
        {
            //The size of the current object on the UI
            var UISize = currentObject.GetComponent<SpriteRenderer>().bounds.size;
            Rect UIobjRect = new Rect((int)Math.Round(position.x - (UISize.x / 2)), (int)Math.Round(position.y - (UISize.y / 2)), (int)Math.Round(UISize.x), (int)Math.Round(UISize.y));
            Vector2Int obstaclePoint = new Vector2Int((int)Math.Round(obstacle.transform.position.x - (obstacle.transform.localScale.x /2)), (int)Math.Round(obstacle.transform.position.y - (obstacle.transform.localScale.y / 2)));
            Vector2Int obstacleRect = new Vector2Int((int)Math.Round(obstacle.transform.localScale.x), (int)Math.Round(obstacle.transform.localScale.y));
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
            Rect UIobjRect = new Rect((int)Math.Round(position.x - (UISize.x / 2)), (int)Math.Round(position.y - (UISize.y / 2)), (int)Math.Round(UISize.x), (int)Math.Round(UISize.y));
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
                    player.CurrentHealth--;
                    player.UpdateHealth();

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
            //The size of the current object on the UI
            var UISize = currentObject.GetComponent<SpriteRenderer>().bounds.size;
            Rect UIobjRect = new Rect((int)Math.Round(position.x - (UISize.x / 2)), (int)Math.Round(position.y - (UISize.y / 2)), (int)Math.Round(UISize.x), (int)Math.Round(UISize.y));
            Vector2Int obstaclePoint = new Vector2Int((int)Math.Round(enemy.transform.position.x - (enemy.transform.localScale.x / 2)), (int)Math.Round(enemy.transform.position.y - (enemy.transform.localScale.y / 2)));
            Vector2Int obstacleRect = new Vector2Int((int)Math.Round(enemy.transform.localScale.x), (int)Math.Round(enemy.transform.localScale.y));
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

    public void SpawnBullet(Transform transform, Vector2Int position, int facing_x, int facing_y)
    {
        GameObject bulletGO = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        bullet.SetPosition(position);
        bullet.SetDirection(facing_x, facing_y);
    }


}
