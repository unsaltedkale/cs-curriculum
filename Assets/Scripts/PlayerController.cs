using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool overworld;

    // Movement variables
    public float xSpeed = 5f;
    public float ySpeed = 5f;
    private float xVector = 0f;
    private float yVector = 0f;
    private GameManager gm;
    public float attackcooldown;
    private float attackcooldownmax = 1;
    private bool keypress;
    Animator anim;
    private float attackanimationtimer;
    private float firerate = 4;
    public GameObject Player_Projectile;
    
    private void Start()
    {
        GetComponentInChildren<TopDown_AnimatorController>().enabled = overworld;
        GetComponentInChildren<Platformer_AnimatorController>().enabled = !overworld; //what do you think ! means?
        anim = GetComponentInChildren<Animator>();
        
        if (overworld)
        {
            GetComponent<Rigidbody2D>().gravityScale = 0f;
        }
        else
        {
            GetComponent<Rigidbody2D>().gravityScale = 1;
            ySpeed = 0;
        }

        gm = FindFirstObjectByType<GameManager>();
        
    }

    private void Update()
    {
        // Handle Hori input
        float xDirection = Input.GetAxis("Horizontal");
        // Calculate xVector based on input
        //if the player moves super fast and jumps off the screen look at the Helpful Resources below.
        xVector = xDirection * xSpeed * Time.deltaTime;

        // Handle Verti input
        float yDirection = Input.GetAxis("Vertical");
        // Calculate yVector based on input
        //if the player moves super fast and jumps off the screen look at the Helpful Resources below.
        yVector = yDirection * ySpeed * Time.deltaTime;
        transform.Translate(xVector, yVector, 0);

        attackcooldown -= Time.deltaTime;
        attackanimationtimer -= Time.deltaTime;

        if (Input.anyKey)
        {
            keypress = true;
        }

        else
        {
            keypress = false;
        }

        if (keypress == true && Input.GetMouseButton(0) && attackcooldown <= 0 && attackanimationtimer <= 0)
        {
            Debug.Log("attackinggggg player attacking");
            anim.SetTrigger("Attack");
            anim.SetBool("IsWalking", false);
            attackcooldown = attackcooldownmax;
            attackanimationtimer = 0.667f;
        }

    }
}

/*
private float cooldown;
   
   
   GameObject clone = Instantiate(Turret_Projectile, transform.position, quaternion.identity);
   Turret_Projectile script = clone.GetComponent<Turret_Projectile>();
   script.target = player.transform.position;
   cooldown = firerate;
   cooldown -= Time.deltaTime;
   */