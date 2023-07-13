using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WalkingMonster : Entity
{
    private float speed = 0.3f;
    private Vector3 dir;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    private void Start()
    {
        dir = transform.right;
    }

    // Update is called once per frame
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        var position = transform.position + transform.right * dir.x * 0.5f;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.01f);
        //Debug.Log($"There are {colliders.Length} colliders");
        //Debug.Log($"The posiion is {position}");
        if (colliders.Length > 0 && !colliders[0].gameObject.CompareTag("Player"))
        {
            //Debug.Log($"There are {colliders[0].name} colliders");
            dir *= -1f;
        }

        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            Hero.Instance.GetDamage();
        }
    }
} 
