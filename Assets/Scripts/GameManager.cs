using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager gm;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI scoreText;
    
    public int score;
    private int health;
    private int max_health = 10;

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
        score = 0;
        healthText.text = "Health: " + health;
        scoreText.text = "Score: " + score;
    }

   
    public int GetScore()
    {
        return score;
    }

    private void SetScore(int amount)
    {
        score = amount;
        // Temporary Score display
        Debug.Log("Score: " + score);
        // ACTUAL Score display
        scoreText.text = "Score: " + score;
    }

    public void ChangeScore(int amount)
    {
        score += amount;
        // Temporary Score display
        Debug.Log("Score: " + score);
        // ACTUAL Score display
        scoreText.text = "Score: " + score;
    }

    public int GetHealth()
    {
        return health;
    }

    private void SetHealth(int amount)
    {
        health = amount;
        // Temporary Health display
        Debug.Log("Health: " + health);
        // ACTUAL Health display
        healthText.text = "Health: " + health;
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
            SetHealth(0);
            Debug.Log("you died!!!!!! get gud");
            
        }
        
        health = gm.GetHealth();
        
        // Temporary Health display
        Debug.Log("Health: " + health);
        // ACTUAL Health display
        healthText.text = "Health: " + health;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
