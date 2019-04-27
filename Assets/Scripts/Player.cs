using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Vector2Int position = Vector2Int.zero;
    Vector2 velocity = Vector2.zero;

    float maxrun = 1;
    float acceleration = 0.6f;

    Vector2 remainder = Vector2.zero;

    public GameObject bulletPrefab;

    int facing_x = 1;
    int facing_y = 0;

    void Start()
    {
        Application.targetFrameRate = 60;
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
        float h_input = 0;
        float v_input = 0;
        if (Input.GetKey(KeyCode.UpArrow))
            v_input++;
        if (Input.GetKey(KeyCode.DownArrow))
            v_input--;
        if (Input.GetKey(KeyCode.LeftArrow))
            h_input--;
        if (Input.GetKey(KeyCode.RightArrow))
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
            SpawnBullet();
        }

        velocity.x = Approach(velocity.x, h_input * maxrun, acceleration);
        velocity.y = Approach(velocity.y, v_input * maxrun, acceleration);

        MoveX(velocity.x);
        MoveY(velocity.y);

        transform.position = new Vector3(position.x, position.y);

    }

    void SpawnBullet()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, transform);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        bullet.SetPosition(position);
        bullet.SetDirection(facing_x, facing_y);
        
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
        // This should check collision with the level.
        return false;
    }

    int Sign(float amount)
    {
        if (amount > 0.0) return 1;
        if (amount < 0.0) return -1;
        return 0;
    }
}
