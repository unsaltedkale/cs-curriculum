using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool overworld;

    // Movement variables
    public float xSpeed = 5f;
    private float xVector = 0f;

    private void Start()
    {
        GetComponentInChildren<TopDown_AnimatorController>().enabled = overworld;
        GetComponentInChildren<Platformer_AnimatorController>().enabled = !overworld; //what do you think ! means?
        
        
        if (overworld)
        {
            GetComponent<Rigidbody2D>().gravityScale = 0f;
        }
        else
        {
            GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }

    private void Update()
    {
        // Handle input
        float xDirection = Input.GetAxis("Horizontal");
        // Calculate xVector based on input
        //if the player moves super fast and jumps off the screen look at the Helpful Resources below.
        xVector = xDirection * xSpeed * Time.deltaTime;
        transform.Translate(xVector, 0, 0);
    }

    //for organization, put other built-in Unity functions here

    private void FixedUpdate()
    {
        // Apply movement
        //fixed update makes physics engine work better with fast moving objects.
        //similar to x = x + xVector or
        //transform.position = transform.position + new Vector3(xVector)
        
    }
}

    //after all Unity functions, your own functions can go here
