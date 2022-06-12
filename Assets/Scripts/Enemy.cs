using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, ITargetable
{
    private float Speed;
    public int Damage = 1;
    public const float BOUND = 7f;
    bool inTrigger = false;

    public void Setup(float speed)
    {
        Speed = speed;
    }

    void FixedUpdate()
    {
        if (transform.position.x >= BOUND)
        {
            Destroy(gameObject);
        }
        transform.position += transform.TransformDirection(Vector2.right * Speed * Time.deltaTime * 10);
    }

    private void Update()
    {
        if (inTrigger)
        {
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Hitting)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().TakeDamage(Damage);
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        inTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inTrigger = false;
    }
}
