using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private SceneController sceneController;

    Vector2 velocity;
    Vector2 position;
    public float speed;
    public bool Moving = true;
    // Start is called before the first frame update
    void Start()
    {
        sceneController = GameObject.FindGameObjectWithTag("SceneController").GetComponent("SceneController") as SceneController;
    }

    // Update is called once per frame
    void Update()
    {
        position += velocity;
        if(Moving && !collideAt(new Vector2Int((int)position.x,(int)position.y)))
        {
            transform.position = new Vector3(position.x, position.y);
        }
        else
        {
            Moving = false;
            velocity = new Vector2(0, 0);
        }

    }

    public void SetDirection(int x, int y)
    {
        if(Moving)
        {
            velocity.x = x;
            velocity.y = y;
            velocity = speed * velocity.normalized;
        }

    }
    public void SetPosition(Vector2Int pos)
    {
        if (Moving)
        {
            position = pos;
        }
    }

    bool collideAt(Vector2Int position)
    {
        return sceneController.BulletsHitSomething(position, this.gameObject);
    }
   
}
