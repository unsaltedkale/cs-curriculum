using UnityEngine;

public class Health : MonoBehaviour
{
    public GameManager gm;
    private int health;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gm = FindFirstObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        health = gm.GetHealth();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Spikes"))
        {
            gm.ChangeHealth(-5);
            health = gm.GetHealth();
            // Temporary Health display
            Debug.Log("Health: " + health);
        }
    }
}
