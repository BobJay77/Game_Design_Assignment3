using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    private float deltaTime;
    private float previousTime;


    private void Start()
    {
        deltaTime = 0;
        previousTime = Time.time;
    }

    private void Update()
    {
        deltaTime = Time.time - previousTime;

        if(deltaTime > 2.0f)
        {
            BasicPool.Instance.AddToPool(gameObject);  
        } 

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "Player")
        {
            BasicPool.Instance.AddToPool(gameObject);
        }

    }
}
