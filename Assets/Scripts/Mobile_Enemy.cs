using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System;

public class Mobile_Enemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public int currentTarget;
    public List<Transform> waypoints;
    private float speed = 3f;
    private float closest_distance;
    private GameManager gm;
    public float waitTime;
    private float maxwaitTime = 3;
    
    void Start()
    {
        gm = FindFirstObjectByType<GameManager>();
        currentTarget = 0;
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
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentTarget].position,  speed * Time.deltaTime);
        // Check if the position of the cube and sphere are approximately equal.
        // if (Vector3.Distance(transform.position, target.position) < 0.001f)
    }
}
