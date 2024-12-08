using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

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
    public GameObject Player_Projectile;
    public bool hasaxe;
    public Collider2D col;
    public Rigidbody2D rb;
    public float JumpHeight;
    public bool isGrounded;
    public float distance;
    public float yOffset;
    public int bigPotion;
    public int smallPotion;
    
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

        rb = GetComponent<Rigidbody2D>();
        gm = FindFirstObjectByType<GameManager>();
        col = GetComponent<Collider2D>();
        JumpHeight = bigJumpHeight;
        gameObject.transform.localScale = bigSize;
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
            // offset so doesn't hit player
            Vector3 vector = Input.mousePosition - new Vector3(Screen.width * 0.5f, Screen.height * 0.5f);
            vector = Vector3.Normalize(vector);
            GameObject clone = Instantiate(Player_Projectile, transform.position + vector*1f, quaternion.identity);
            Player_Projectile script = clone.GetComponent<Player_Projectile>();
            script.target = Input.mousePosition;
            script.isfromaxe = hasaxe;

            if (doorother != null && hasaxe)
            {
                doorother.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            //transform.position += new Vector3(0, JumpHeight, 0);
            rb.AddForce(Vector2.up * JumpHeight, ForceMode2D.Impulse);
            print("jump");
            isGrounded = false;
        }
        
        
        if (Physics2D.Raycast(new Vector2(transform.position.x + col.bounds.extents.x, transform.position.y - col.bounds.extents.y + yOffset), Vector2.down, distance) 
            || Physics2D.Raycast(new Vector2(transform.position.x - col.bounds.extents.x, transform.position.y - col.bounds.extents.y + yOffset), Vector2.down, distance))
        {
            isGrounded = true;
            print("hit");
        }

        else
        {
            isGrounded = false;
        }
        
        if (keypress == true && Input.GetKeyDown(KeyCode.Q) && (smallPotion > 0 || hasStaff == true) && isSmall == false)
        {
            isSmall = true;
            smallPotion -= 1;
            gameObject.transform.localScale = smallSize;
            JumpHeight = smallJumpHeight;
            xSpeed = smallxSpeed;
            SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();
            sr.color = Color.magenta;
        }

        if (keypress == true && Input.GetKeyDown(KeyCode.E) && (bigPotion > 0 || hasStaff == true) && isSmall == true)
        {
            isSmall = false;
            bigPotion -= 1;
            gameObject.transform.localScale = bigSize;
            JumpHeight = bigJumpHeight;
            xSpeed = bigxSpeed;
            SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();
            sr.color = Color.white;
        }
        
        /*while (isSmall)
        {
            SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();
            sr.color = Color.magenta;
        }*/
        //Debug.DrawRay(new Vector2(transform.position.x + col.bounds.extents.x, transform.position.y - col.bounds.extents.y + yOffset), Vector2.down * distance);
        //Debug.DrawRay(new Vector2(transform.position.x - col.bounds.extents.x, transform.position.y - col.bounds.extents.y + yOffset), Vector2.down * distance);
    }

    private bool isSmall;
    public float smallJumpHeight;
    public float bigJumpHeight;
    public Vector3 smallSize;
    public Vector3 bigSize;
    public float bigxSpeed;
    public float smallxSpeed;
    public bool hasStaff;

    public void AddSmallPotion()
    {
        smallPotion += 1;
    }
    
    public void AddBigPotion()
    {
        bigPotion += 1;
    }
    
    
    private GameObject doorother;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("BreakableDoor"))
        {
            doorother = other.gameObject;
        }
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        doorother = null;
    }
}
