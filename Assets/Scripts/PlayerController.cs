using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool overworld;

    // Movement variables
    public float xSpeed = 5f;
    public float ySpeed = 5f;
    private float xVector = 0f;
    private float yVector = 0f;
    private int score = 0;
    [SerializeField] private GameManager gm;

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
    }

     private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Coin"))
        {
            // Increase Score
            gm.AddScore(1); // Add 1 to score for each coin

            //Destory the coin
            Destroy(other.gameObject);
        }
    }
}