using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private float Speed;
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
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().RemoveScore(5);
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
                GameObject.FindGameObjectWithTag("AudioSystem").GetComponent<AudioSystem>().PlayDestroySound();
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().OnTargetDestroyed();
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
