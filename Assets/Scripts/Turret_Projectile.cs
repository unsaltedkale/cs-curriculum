using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class Turret_Projectile : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float speed = 4f;
    public Vector3 target;
    private GameManager gm;
    public Vector3 moveVector;
    public GameObject player;
    
    void Start()
    {
        StartCoroutine(die_soon());
        gm = FindFirstObjectByType<GameManager>();
        moveVector = target - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, moveVector * 5, speed * Time.deltaTime);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gm.ChangeHealth(-2);
            Destroy(gameObject);
        }
        
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator die_soon()
    {
        yield return new WaitForSeconds(6);
        Destroy(gameObject);
    }
}
