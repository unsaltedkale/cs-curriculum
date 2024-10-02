using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Turret : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private float cooldown;
    private float firerate = 5;
    public GameObject TurretProjectile;
    public GameObject player;
    private bool can_shoot; 
    void Start()
    {
        cooldown = firerate;
    }


    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;
        if (can_shoot == true && cooldown < 0)
        {
            GameObject clone = Instantiate(TurretProjectile, transform.position, quaternion.identity);
            PlayerController player = GetComponent<PlayerController>();
            clone.GetComponent<Turret_Projectile>().target = player.transform.position;
            cooldown = firerate;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            can_shoot = true;
            Debug.Log("truee");
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            can_shoot = false;
            Debug.Log("fffffalse");
        }
    }
}
