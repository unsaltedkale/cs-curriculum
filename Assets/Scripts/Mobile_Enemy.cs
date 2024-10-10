using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
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
    public GameObject player;
    private Vector3 playercurrentposition;
    public States state;
    private bool PlayerInTrigger;

    public enum States
    {
        Attacking,
        Patrol,
        Chasing,
        Wait,
        Dead,
        BeratingCreator
    }
    void Start()
    {
        gm = FindFirstObjectByType<GameManager>();
        currentTarget = 0;
        waitTime = maxwaitTime;
        state = States.Patrol;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == States.Patrol)
        {
            // Check if the position of the enemy and target are approximately equal.
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

        }

        if (state == States.Chasing)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position,  speed * Time.deltaTime);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            state = States.Chasing;
            PlayerInTrigger = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            PlayerInTrigger = false;
            StartCoroutine(ChasingStop());
        }
    }

    private IEnumerator ChasingStop()
    {
        yield return new WaitForSeconds(2);
            if (PlayerInTrigger == true);
            {
                state = States.Chasing;
                yield return state;
            }

            if (PlayerInTrigger == false);
            {
                state = States.Wait;
                yield return new WaitForSeconds(1);
                state = States.Patrol;
            }
            // if player out of trigger set to wait and then patrol
    }
}
