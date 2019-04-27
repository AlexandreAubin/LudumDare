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

    GameObject bullet;

    void Start()
    {
        //transform = GetComponent<Transform>();
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
        float fire = Input.GetAxis("Fire1");
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 mouseDirection = mouse - transform.position;
        float mouseAngle = Vector3.Angle(mouseDirection, transform.forward);

        velocity.x = Approach(velocity.x, h_input * maxrun, acceleration);
        velocity.y = Approach(velocity.y, v_input * maxrun, acceleration);
        MoveX(velocity.x);
        MoveY(velocity.y);
        transform.position = new Vector3(position.x, position.y);

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
