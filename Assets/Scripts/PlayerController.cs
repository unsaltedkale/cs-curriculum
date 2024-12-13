using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public Collider2D col;
    public Rigidbody2D rb;
    public float JumpHeight = 50;
    public bool isGrounded;
    public float distance = 0.1f;
    public float yOffset = 0.03f;
    
    private bool isSmall;
    private float smallJumpHeight = 20;
    private float bigJumpHeight = 70;
    private Vector3 smallSize = new Vector3(0.5f, 0.5f, 0.5f);
    private Vector3 bigSize = new Vector3(1.5f, 1.5f, 1.5f);
    private float bigxSpeed = 5;
    private float smallxSpeed = 3;
    public bool hasStaff;
    
    private GameObject doorother;
    
    private void Start()
    {
        GetComponentInChildren<TopDown_AnimatorController>().enabled = overworld;
        GetComponentInChildren<Platformer_AnimatorController>().enabled = !overworld; //what do you think ! means?
        anim = GetComponentInChildren<Animator>();
        
        if (overworld)
        {
            GetComponent<Rigidbody2D>().gravityScale = 0f;
            bigSize = new Vector3(0.75f, 0.75f, 0.75f);
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

        if (keypress == true && Input.GetMouseButton(0) && attackcooldown <= 0 && attackanimationtimer <= 0 && SceneManager.GetActiveScene().name != "Start")
        {
            anim.SetTrigger("Attack");
            anim.SetBool("IsWalking", false);
            attackcooldown = attackcooldownmax;
            attackanimationtimer = 0.667f;
            // offset so doesn't hit player
            Vector3 vector = Input.mousePosition - new Vector3(Screen.width * 0.5f, Screen.height * 0.5f);
            vector = Vector3.Normalize(vector);
            GameObject clone = Instantiate(Player_Projectile, transform.position + vector*0.5f, quaternion.identity);
            Player_Projectile script = clone.GetComponent<Player_Projectile>();
            script.target = Input.mousePosition;
            script.isfromaxe = gm.CheckAxe();

            if (doorother != null && gm.CheckAxe() == true)
            {
                doorother.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true && SceneManager.GetActiveScene().name != "Start" && SceneManager.GetActiveScene().name != "Overworld")
        {
            //transform.position += new Vector3(0, JumpHeight, 0);
            rb.AddForce(Vector2.up * JumpHeight, ForceMode2D.Impulse);
            isGrounded = false;
        }
        
        
        if (Physics2D.Raycast(new Vector2(transform.position.x + col.bounds.extents.x, transform.position.y - col.bounds.extents.y + yOffset), Vector2.down, distance) 
            || Physics2D.Raycast(new Vector2(transform.position.x - col.bounds.extents.x, transform.position.y - col.bounds.extents.y + yOffset), Vector2.down, distance))
        {
            isGrounded = true;
        }

        else
        {
            isGrounded = false;
        }
        
        if (keypress == true && Input.GetKeyDown(KeyCode.Q) && (gm.CheckSmallPotion() > 0 || hasStaff == true) && isSmall == false)
        {
            isSmall = true;
            if (hasStaff == false)
            {
                gm.ChangeSmallPotion(-1); 
            }
            gameObject.transform.localScale = smallSize;
            JumpHeight = smallJumpHeight;
            xSpeed = smallxSpeed;
            SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();
            sr.color = Color.magenta;
        }

        if (keypress == true && Input.GetKeyDown(KeyCode.E) && (gm.CheckBigPotion() > 0 || hasStaff == true) && isSmall == true)
        {
            isSmall = false;
            if (hasStaff == false)
            {
                gm.ChangeBigPotion(-1); 
            }
            gameObject.transform.localScale = bigSize;
            JumpHeight = bigJumpHeight;
            xSpeed = bigxSpeed;
            SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();
            sr.color = Color.white;
        }
        
        //Debug.DrawRay(new Vector2(transform.position.x + col.bounds.extents.x, transform.position.y - col.bounds.extents.y + yOffset), Vector2.down * distance);
        //Debug.DrawRay(new Vector2(transform.position.x - col.bounds.extents.x, transform.position.y - col.bounds.extents.y + yOffset), Vector2.down * distance);
    }

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
