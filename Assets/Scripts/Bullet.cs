using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private SceneController sceneController;
    private Animator anim;

    Vector2 velocity;
    Vector2 position;
    public float speed;
    public bool Moving = true;
    // Start is called before the first frame update
    void Start()
    {
        sceneController = GameObject.FindGameObjectWithTag("SceneController").GetComponent("SceneController") as SceneController;
        anim = this.GetComponent<Animator>();
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
            if(Moving)
            {
                MusicManager.GetMusicManager().PlayHitWallSound();
            }

            if (anim != null)
            {
                anim.SetBool("isMoving", false);
                Moving = false;
                velocity = new Vector2(0, 0);
            }

        }

    }

    public void SetDirection(float x, float y)
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
