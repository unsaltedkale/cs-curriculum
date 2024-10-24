using System;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    public GameManager gm;
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
        CurrentEnemy = null;
    }

    // Update is called once per frame
    void Update()
    {
        playerattacking = playeranimator.GetBool("Attack");
        if (CurrentEnemy != null)
        {
            enemyattacking = enemyanimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack");
        }
        //IsAttacking = anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack");
        print(enemyattacking);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Spikes"))
        {
            gm.ChangeHealth(-5);
        }
        
        // if (other.CompareTag("Bullet or Projectile idk man"))
    }

    /*private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            CurrentEnemy = other.gameObject;
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
                
            }
        }
    }*/
}
