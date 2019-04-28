using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    
    private SceneController sceneController;

    Vector2Int position;
    public Vector2 velocity = Vector2.zero;

    float maxrun = 1;
    float acceleration = 0.6f;

    public const int MAX_HEALTH = 30;
    public int CurrentHealth = MAX_HEALTH;

    Vector2 remainder;

    //public GameObject bulletPrefab;
    public GameObject enemy;
    public Transform SpawnPoints;

    int facing_x = 1;
    int facing_y = 0;

    private Animator anim;
    private Text healthBar;

    void Start()
    {
        position =  new Vector2Int((int)Math.Round(transform.position.x), (int)Math.Round(transform.position.y));
        remainder = new Vector2Int(0, 0);
        anim = this.GetComponent<Animator>();
        Application.targetFrameRate = 60;
        sceneController = GameObject.FindGameObjectWithTag("SceneController").GetComponent("SceneController") as SceneController;
        healthBar =  GameObject.FindGameObjectWithTag("Health").GetComponent<Text>();
    }

    float Approach(float value, float target, float amount)
    {
        if (value > target)
            return Mathf.Max(value - amount, target);
        return Mathf.Min(value + amount, target);
    }

    // Update is called once per frame
    void Update()
    {
        int h_input = 0;
        int v_input = 0;
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            v_input++;
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            v_input--;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            h_input--;
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            h_input++;
        bool fire = Input.GetMouseButtonDown(0);
        Vector3 mousev3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mouse = new Vector2(mousev3.x, mousev3.y);

        Vector3 mouseDirection = mouse - position;
        //float mouseAngle = Vector3.SignedAngle(Vector3.right, mouseDirection, Vector3.up);

        if (Vector2.Angle(mouseDirection, Vector2.right) <= 22.5)
        {
            facing_x = 1;
            facing_y = 0;
        }
        if (Vector2.Angle(mouseDirection, Vector2.right + Vector2.up) <= 22.5)
        {
            facing_x = 1;
            facing_y = 1;
        }
        if (Vector2.Angle(mouseDirection, Vector2.up) <= 22.5)
        {
            facing_x = 0;
            facing_y = 1;
        }
        if (Vector2.Angle(mouseDirection, Vector2.left + Vector2.up) <= 22.5)
        {
            facing_x = -1;
            facing_y = 1;
        }
        if (Vector2.Angle(mouseDirection, Vector2.left) <= 22.5)
        {
            facing_x = -1;
            facing_y = 0;
        }
        if (Vector2.Angle(mouseDirection, Vector2.right + Vector2.down) <= 22.5)
        {
            facing_x = 1;
            facing_y = -1;
        }
        if (Vector2.Angle(mouseDirection, Vector2.down) <= 22.5)
        {
            facing_x = 0;
            facing_y = -1;
        }
        if (Vector2.Angle(mouseDirection, Vector2.left + Vector2.down) <= 22.5)
        {
            facing_x = -1;
            facing_y = -1;
        }

        if (fire)
        {
            if(CurrentHealth > 0)
            {
                CurrentHealth--;
                UpdateHealth();
                SpawnBullet();
            }
        }

        velocity.x = Approach(velocity.x, h_input * maxrun, acceleration);
        velocity.y = Approach(velocity.y, v_input * maxrun, acceleration);

        MoveX(velocity.x);
        MoveY(velocity.y);

        transform.position = new Vector3(position.x, position.y);

        anim.SetInteger("YSpeed", v_input);
        anim.SetInteger("XSpeed", h_input);
        anim.SetInteger("Health", CurrentHealth);


    }

    void SpawnBullet()
    {
        sceneController.SpawnBullet(transform,new Vector2Int(position.x,position.y), facing_x, facing_y);
    }

    void MoveX(float amount)
    {
        remainder.x += amount;
        int move = Mathf.RoundToInt(remainder.x);
        if (move != 0)
        {
            remainder.x -= move;
            int sign = Sign(move);
            while(move != 0)
            {
                if (collideAt(position + new Vector2Int(sign, 0)))
                {
                    break;
                }
                else
                {
                    position.x += sign;
                    move -= sign;
                }

            }
        }
    }

    void MoveY(float amount)
    {
        remainder.y += amount;
        int move = Mathf.RoundToInt(remainder.y);
        if (move != 0)
        {
            remainder.y -= move;
            int sign = Sign(move);
            while (move != 0)
            {
                if (collideAt(position + new Vector2Int(0, sign)))
                {
                    break;
                }
                else
                {
                    position.y += sign;
                    move -= sign;
                }

            }
        }
    }

    bool collideAt(Vector2Int position)
    {
        return sceneController.isThereCollision(position,this.gameObject);
    }

    int Sign(float amount)
    {
        if (amount > 0.0) return 1;
        if (amount < 0.0) return -1;
        return 0;
    }

    public void UpdateHealth()
    {
        healthBar.text = "Health : " + CurrentHealth + "/" + MAX_HEALTH;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Projectile"))
        {
            print("hello");
            CurrentHealth--;
            UpdateHealth();
        }
    }
}
