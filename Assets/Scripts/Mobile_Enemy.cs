using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using UnityEditor.U2D;

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
    private bool startofstate;
    private float attackingradius;
    public Animator animator;
    public float attackcooldown;
    public float attackcooldownmax;
    public TopDown_EnemyAnimator tdea;
    public BoxCollider2D hitbox;
    private Vector2 hitboxoriginal;

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
        tdea = GetComponentInChildren<TopDown_EnemyAnimator>();
        print(tdea);
        currentTarget = 0;
        waitTime = maxwaitTime;
        state = States.Patrol;
        startofstate = true;
        attackingradius = 2f;
        attackcooldownmax = 2;
        hitbox = GetComponent<BoxCollider2D>();
        hitboxoriginal = hitbox.size;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == States.Patrol)
        {
            if (startofstate == true);
            {
                //start stuff
                startofstate = false;
            }
            
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
            if (startofstate == true);
            {
                //start stuff
                startofstate = false;
            }
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position,  speed * Time.deltaTime);
        }

        if (state == States.Attacking)
        {
            if (startofstate == true);
            {
                //atttack stuff
                startofstate = false;
            }

            attackcooldown -= Time.deltaTime;
            
            if (attackcooldown <= 0)
            {
                attackcooldown = attackcooldownmax;
                print("im attacking!!!! raaaaa!!!!!!!");
                tdea.Attack(); // play the attack animation
                hitbox.size = new Vector2(2.72f, 3.14f);
                if (GetComponent<Collider2D>().IsTouching(player.GetComponent<Collider2D>()))
                {
                    gm.ChangeHealth(-3);
                    print("gotcha lol get gud");
                }

                hitbox.size = hitboxoriginal;
            }

            if (state != States.Attacking)
            {
                if ((Vector2.Distance(transform.position, player.transform.position)) < attackingradius)
                {
                    state = States.Attacking;
                    startofstate = true;
                }
            }
            
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //if (state == States.Chasing)
            //if (gameObject.CompareTag("AttackingRadius"))
            if ((Vector2.Distance(transform.position, player.transform.position)) < attackingradius)
            {
                state = States.Attacking;
                startofstate = true;
            }
            if ((Vector2.Distance(transform.position, player.transform.position)) > attackingradius)
            {
                state = States.Chasing;
                startofstate = true;
                PlayerInTrigger = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if ((Vector2.Distance(transform.position, player.transform.position)) < attackingradius)
            {
                state = States.Chasing;
                startofstate = true;
            }

            if ((Vector2.Distance(transform.position, player.transform.position)) > attackingradius)
            {
                PlayerInTrigger = false;
                StartCoroutine(ChasingStop());
            }
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
                startofstate = true;
                yield return new WaitForSeconds(1);
                
                if (PlayerInTrigger == true);
                {
                    state = States.Chasing;
                    startofstate = true;
                    yield return state;
                }
                
                if (PlayerInTrigger == false)
                {
                    state = States.Patrol;
                    startofstate = true;
                }
            }
            // if player out of trigger set to wait and then patrol
    }
}
