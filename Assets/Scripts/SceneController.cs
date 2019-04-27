using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public const string PLAYER_CLASS = "Player";

    public GameObject[] obstacles;

    public Player player;

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

    public bool isThereCollision(Vector2Int position)
    {
        foreach(GameObject obstacle in obstacles)
        {
            Vector2Int obstaclePoint = new Vector2Int((int)Math.Round(obstacle.transform.position.x - (obstacle.transform.localScale.x /2)), (int)Math.Round(obstacle.transform.position.y - (obstacle.transform.localScale.y / 2)));
            Vector2Int obstacleRect = new Vector2Int((int)Math.Round(obstacle.transform.localScale.x), (int)Math.Round(obstacle.transform.localScale.y));
            RectInt rect = new RectInt(obstaclePoint, obstacleRect);
            if(rect.Contains(position))
            {
                return true;
            }
            /*if((obstaclePoint.x == position.x || (obstaclePoint.x + obstacleRect.x) == position.x) && ( obstaclePoint.y == position.y || (obstaclePoint.y + obstacleRect.y) == position.y))
            {
                return true;
            }*/
        }

        return false;
    }
}
