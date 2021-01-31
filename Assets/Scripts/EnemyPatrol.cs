using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public Transform point1;
    public Transform point2;
    public int speed = 7;

    // Start is called before the first frame update
    void Start()
    {
        if (transform.position.x > point2.position.x)
        {
            rb.velocity = new Vector2(-speed, 0);
        } else
        {
            rb.velocity = new Vector2(speed, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= point1.position.x)
        {
            rb.velocity = new Vector2(speed, 0);
            sr.flipX = false;
            
        }

        if (transform.position.x >= point2.position.x)
        {
            rb.velocity = new Vector2(-speed, 0);
            sr.flipX = true;
        }
    }
}
