using System;
using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Projectile : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Vector3 CenterRef = new Vector3 (Screen.width*0.5f, Screen.height*0.5f);
    private float speed = 3f;
    public Vector3 target; // current mouse position where bottom left of screen is 0,0 and
    // The top-right of the screen or window is at (Screen.width, Screen.height).
    private Vector3 mousePos;
    private GameManager gm;
    public GameObject axe;
    public bool isfromaxe;
    
    void Start()
    {
        StartCoroutine(die_soon());
        gm = FindFirstObjectByType<GameManager>();
        mousePos = target - CenterRef;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, mousePos, speed * Time.deltaTime);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Instantiate(axe, other.transform.position, quaternion.identity);
            Destroy(gameObject);
        }
        
        /*else if (other.gameObject.CompareTag("BreakableDoor") && isfromaxe == true)
        {
            other.gameObject.SetActive(false);
            Destroy(gameObject);
        }*/
        
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator die_soon()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
