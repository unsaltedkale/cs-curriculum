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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Spikes"))
        {
            gm.ChangeHealth(-5);
        }
        
        // if (other.CompareTag("Bullet or Projectile idk man"))
        // enemy to player damage and bullet damage dealt with in their respective scripts
    }

}
