using UnityEngine;

public class Turret_Projectile : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Vector3 heading;
    private float speed = 4f;
    public Vector3 target;
    private Vector3 target_vector;
    void Start()
    {
        heading = transform.position - target
    }

    // Update is called once per frame
    void Update()
    {
        target_vector = speed * target * Time.deltaTime;
        transform.Translate(target_vector);
    }
}
