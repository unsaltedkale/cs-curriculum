using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Turret : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private float cooldown;
    private float firerate = 4;
    public GameObject Turret_Projectile;
    public GameObject player;
    private bool can_shoot; 
    void Start()
    {
        cooldown = firerate;
    }


    // Update is called once per frame
    void Update()
    {
        if (can_shoot == true && cooldown < 0)
        {
            GameObject clone = Instantiate(Turret_Projectile, transform.position, quaternion.identity);
            Turret_Projectile script = clone.GetComponent<Turret_Projectile>();
            script.target = player.transform.position;
            script.player = player.gameObject;
            cooldown = firerate;
        }
        cooldown -= Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            can_shoot = true;
            Debug.Log("truee");
            player = other.gameObject;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            can_shoot = false;
            Debug.Log("fffffalse");
            player = null;
        }
    }
    
    
    
}
