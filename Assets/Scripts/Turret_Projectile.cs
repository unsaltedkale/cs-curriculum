using System;
using System.Collections;
using UnityEngine;

public class Turret_Projectile : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Vector3 heading;
    private float speed = 4f;
    public Vector3 target;
    private GameManager gm;
    
    void Start()
    {
        StartCoroutine(die_soon());
        gm = FindFirstObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //target_vector = speed * heading * Time.deltaTime;
        //transform.Translate(target_vector);
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
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
