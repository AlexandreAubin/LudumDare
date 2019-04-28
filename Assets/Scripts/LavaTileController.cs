using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaTileController : MonoBehaviour
{
    public GameObject playerCharacter;

    private GameObject[] lava;
    private Player player;
    private float timeLeft;

    // Start is called before the first frame update
    void Start()
    {
        player = player = GameObject.FindGameObjectWithTag("Player").GetComponent("Player") as Player;
        lava = GameObject.FindGameObjectsWithTag("lava");
        timeLeft = 1;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject tile in lava)
        {
            //The size of the current object on the UI
            var UISize = gameObject.GetComponent<SpriteRenderer>().bounds.size;
            Rect playerRect = new Rect((int)Math.Round(playerCharacter.transform.position.x - (UISize.x / 2)),
                                       (int)Math.Round(playerCharacter.transform.position.y - (UISize.y / 2)),
                                       (int)Math.Round(UISize.x), (int)Math.Round(UISize.y));
            Vector2Int lavaTilePoint = new Vector2Int((int)Math.Round(tile.transform.position.x - (tile.transform.localScale.x / 2)), (int)Math.Round(tile.transform.position.y - (tile.transform.localScale.y / 2)));
            Vector2Int lavaTileRect = new Vector2Int((int)Math.Round(tile.transform.localScale.x), (int)Math.Round(tile.transform.localScale.y));
            Rect lavaRect = new Rect(lavaTilePoint, lavaTileRect);

            if (playerRect.Overlaps(lavaRect))
            {
                timeLeft -= Time.deltaTime;

                if (timeLeft <= 0)
                {
                    player.CurrentHealth -= 3;
                    timeLeft = 1;
                }
            }
        }
    }

    private IEnumerator DamageOverTime(int seconds)
    {
        float pauseEndTime = Time.realtimeSinceStartup + seconds;

        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return null; // Attend un frame
        }


    }

}
