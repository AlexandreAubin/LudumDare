using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector2 velocity;
    Vector2 position;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        position += velocity;
        transform.position = new Vector3(position.x, position.y);
    }

    public void SetDirection(int x, int y)
    {
        velocity.x = x;
        velocity.y = y;
        velocity = speed * velocity.normalized;
    }
    public void SetPosition(Vector2Int pos)
    {
        position = pos;
    }
}
