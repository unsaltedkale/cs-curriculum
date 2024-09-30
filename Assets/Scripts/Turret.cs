using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Turret : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private float cooldown;
    private float firerate = 4;
    public GameObject TurretProjectile;
    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && cooldown <= 0)
        {
            GameObject instance = Instantiate(TurretProjectile, transform.position, quaternion.identity);
            instance.GetComponent<Turret_Projectile>().target = other.transform.position;
            cooldown = firerate;
        }
    }
}
