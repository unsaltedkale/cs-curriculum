using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager gm;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI scoreText;
    
    public int score;
    private int health;
    private int max_health = 10;
    private string currentSceneName;
    

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
        
        if (score < 1)
        {
            SetScore(0);
        }
        
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
            SetHealth(0);
            Debug.Log("you died!!!!!! get gud");
            Die();
        }
        
        health = gm.GetHealth();
        
        // Temporary Health display
        Debug.Log("Health: " + health);
        // ACTUAL Health display
        healthText.text = "Health: " + health;
    }
    
    public void Die()
    {
        //if not keypressed {
        //make an if else statement with a bool called keyPressed
        //only able to set keypressed to true when you press one of the two keys
        //else iff keypressed and key == left {
        //bla ha
        //else if keypressed and key == right {
        //blalhalfhal
        //
        print("oop we reloading now boys!!");
        string currentSceneName = SceneManager.GetActiveScene().name;
        health = max_health;
        ChangeScore(-1);
        SceneManager.LoadScene(currentSceneName);
    }
    
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            SceneManager.LoadScene(0);
        }
        
    }
}
