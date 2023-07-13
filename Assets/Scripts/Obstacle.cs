using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log($"я коснулся {collision.gameObject.name}");
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            //Debug.Log($"я коснулся {Hero.Instance.gameObject.name}");
            Hero.Instance.GetDamage();
        }
    }
}
