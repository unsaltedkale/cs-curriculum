using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager gm;

    void Awake()
    {
        if(gm != null && gm !=this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            gm = this;
            DontDestroyOnLoad(this.gameObject);
        }

        health = max_health;
    }

    public int Score;
    private int health;
    private int max_health = 10;

    public int GetScore()
    {
        return Score;
    }

    private void SetScore(int amount)
    {
        Score = amount;
    }

    public void ChangeScore(int amount)
    {
        Score += amount;
        // Temporary Score display
        Debug.Log("Score: " + Score);
    }

    public int GetHealth()
    {
        return health;
    }

    private void SetHealth(int amount)
    {
        health = amount;
    }
    
    public void ChangeHealth(int amount)
    {
        health += amount;
        if (health > max_health)
        {
            health = max_health;
        }

        if (health < 1)
        {
            // Die();
            // Temporary Health display
            Debug.Log("you died!!!!!! get gud");
            SetHealth(10);
            
        }
       
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
