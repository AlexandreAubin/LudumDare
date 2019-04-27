﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public const string PLAYER_CLASS = "Player";

    public GameObject[] obstacles;

    public Player player;

    public GameObject[] bullets;

    public GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent(PLAYER_CLASS) as Player;


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool isThereCollision(Vector2Int position,GameObject currentObject)
    {
        bullets = GameObject.FindGameObjectsWithTag("Projectile");
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

            if (!bullet.GetComponent<Bullet>().Moving && UIobjRect.Overlaps(UIObstacleRect))
            {
                DestroyImmediate(bullet);
                player.CurrentHealth++;
                player.UpdateHealth();
                return true;
            }
        }

        return false;
    }

    public bool BulletsHitSomething(Vector2Int position,GameObject currentObject)
    {
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
                return true;
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
