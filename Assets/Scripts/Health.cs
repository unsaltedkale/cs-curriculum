using System;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    public GameManager gm;
    private int health_at_beginning_of_iframes;
    public bool playerattacking;
    public bool enemyattacking;
    public GameObject Player;
    public Animator playeranimator;
    public GameObject CurrentEnemy;
    public Animator enemyanimator;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gm = FindFirstObjectByType<GameManager>();
        
        playeranimator = Player.GetComponentInChildren<Animator>();
        enemyanimator = CurrentEnemy.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        playerattacking = playeranimator.GetBool("Attack");
        enemyattacking = enemyanimator.GetBool("Attack");

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Spikes"))
        {
            gm.ChangeHealth(-5);
        }
        
        // if (other.CompareTag("Bullet or Projectile idk man"))
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyanimator = CurrentEnemy.GetComponentInChildren<Animator>();
            
            playerattacking = playeranimator.GetBool("Attack");
            enemyattacking = enemyanimator.GetBool("Attack");

            if (playerattacking == true && enemyattacking == true)
            {
                
            }
            else if (playerattacking == false && enemyattacking == true)
            {
                gm.ChangeHealth(-3);
            }    
            else if (playerattacking == true && enemyattacking == false)
            {
            }
            else if (playerattacking == false && enemyattacking == false)
            {
                gm.ChangeHealth(-3);
            }
        }
    }
}
