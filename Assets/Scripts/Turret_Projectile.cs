using System;
using System.Collections;
using UnityEngine;

public class Turret_Projectile : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float speed = 4f;
    public Vector3 target;
    private GameManager gm;
    public Vector3 moveVector;
    
    void Start()
    {
        StartCoroutine(die_soon());
        gm = FindFirstObjectByType<GameManager>();
        transform.LookAt(target);
    }

    // Update is called once per frame
    void Update()
    {
        //target_vector = speed * heading * Time.deltaTime;
        //transform.Translate(target_vector);
        transform.Translate(transform.forward * Time.deltaTime);
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
