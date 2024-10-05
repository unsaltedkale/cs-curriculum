using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System;

public class Mobile_Enemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject target0;
    public GameObject target1;
    public GameObject target2;
    public GameObject target3;
    public GameObject target4;
    public GameObject currentTarget;
    private List<GameObject> waypoints = new List<GameObject>(2);
    private float speed = 4f;
    private float closest_distance;
    private GameManager gm;
    
    void Start()
    {
        gm = FindFirstObjectByType<GameManager>();
        waypoints.Add(target0);
        waypoints.Add(target1);
        waypoints.Add(target2);
        waypoints.Add(target3);
        waypoints.Add(target4);
        currentTarget = target0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentTarget.transform.position, speed * Time.deltaTime);
        // Check if the position of the cube and sphere are approximately equal.
        // if (Vector3.Distance(transform.position, target.position) < 0.001f)
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (waypoints.Contains(other.gameObject));
            print ("yayyyyy!!!");
    }
}
