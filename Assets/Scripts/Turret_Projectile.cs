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
    private bool reachedTarget;
    
    void Start()
    {
        StartCoroutine(die_soon());
        gm = FindFirstObjectByType<GameManager>();
        moveVector = target - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!reachedTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }

        if (Vector2.Distance(transform.position, target) > 1f)
        {
            Vector3 vector = target - transform.position;
            Vector3.Normalize(vector);
            print (vector);
            Debug.DrawRay(vector);
        }
        
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
