using System;
using System.Collections.Generic;
using UnityEngine;

public class GreenPlatform : MonoBehaviour
{
    public int currentTarget;
    public List<Transform> waypoints;
    private float speed = 2f;
    private GameManager gm;
    public float waitTime;
    private float maxwaitTime = 3;
    public bool OnTrigger;
    public bool Triggered;
    
    void Start()
    {
        if (OnTrigger == true)
        {
            Triggered = false;
        }
        else
        {
            Triggered = true;
        }

        currentTarget = 1;
        waitTime = maxwaitTime;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Vector2.Distance(transform.position, waypoints[currentTarget].position)) < 0.1f)
        {
            waitTime -= Time.deltaTime;
            
            if (waitTime <= 0)
            {
                
                currentTarget += 1;
                //% (modulus) take the remainder after dividing by the amount given. In this case current target = the remainder of currentTarget/waypoints.Count.
                currentTarget %= waypoints.Count;

                waitTime = maxwaitTime;
                
                if (OnTrigger == true && currentTarget == 1)
                {
                    Triggered = false;
                }
                
            }
        }

        if (Triggered == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, waypoints[currentTarget].position, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.parent = gameObject.transform;
            
            if (OnTrigger == true)
            {
                Triggered = true;
            }
            
        }
        
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.parent = null;
        }
        
    }
}
