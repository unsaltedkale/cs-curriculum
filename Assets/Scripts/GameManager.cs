using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager gm;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bigpotionText;
    public TextMeshProUGUI smallpotionText;
    
    public int score;
    private int health;
    private int max_health = 10;
    private string currentSceneName;
    private bool keypress;
    public float iframes;
    public float maxiframes = 2;
    public GameObject player;

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
        bigpotionText.text = "Big Potions: " + bigPotion;
        smallpotionText.text = "Small Potions: " + smallPotion;
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
        if (iframes <= 0)
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

            iframes = maxiframes;

            health = gm.GetHealth();

            // Temporary Health display
            Debug.Log("Health: " + health);
            // ACTUAL Health display
            healthText.text = "Health: " + health;
        }
        
    }

    private int bigPotion;
    private int smallPotion;
        
        
    public void ChangeBigPotion(int num)
    {
        bigPotion += num;
        
        // Temporary Health display
        Debug.Log("Big Potion: " + bigPotion);
        // ACTUAL Health display
        bigpotionText.text = "Big Potion: " + bigPotion;
    }

    public void SetBigPotion(int num)
    {
        bigPotion = num;
        Debug.Log("Big Potion: " + bigPotion);
        bigpotionText.text = "Big Potion: " + bigPotion;
    }

    public int CheckBigPotion()
    {
        return bigPotion;
    }
    
    public void ChangeSmallPotion(int num)
    {
        smallPotion += num;
        Debug.Log("Small Potion: " + smallPotion);
        smallpotionText.text = "Small Potion: " + smallPotion;
    }

    public void SetSmallPotion(int num)
    {
        smallPotion = num;
        Debug.Log("Small Potion: " + smallPotion);
        smallpotionText.text = "Small Potion: " + smallPotion;
    }

    public int CheckSmallPotion()
    {
        return smallPotion;
    }

    private bool hasAxe;

    public void SetAxe(bool b)
    {
        hasAxe = b;
    }

    public bool CheckAxe()
    {
        return hasAxe;
    }
        
        

    private bool dead = false;

    public void Die()
    {
        dead = true;
        Time.timeScale = 0;
    }
    
    // Update is called once per frame
    private void Update()
    {
        
        if (iframes > 0)
        {
            iframes -= Time.deltaTime;
        }
        
        if(Input.anyKey)
        {
            keypress = true;
        }
        
        else
        {
            keypress = false;
        }

        if (dead == true)
        {
            healthText.text = "Press 1 to Restart";
            scoreText.text = "Press 2 to Respawn";
            if (keypress == true && Input.GetKey(KeyCode.Alpha1))
            {
                Debug.Log("oop we reloading now boys!! option 1");
                Time.timeScale = 1;
                string currentSceneName = SceneManager.GetActiveScene().name;
                SetHealth(max_health);
                ChangeScore(-1);
                SceneManager.LoadScene("Start");
                dead = false;
            }

            else if (keypress == true && Input.GetKey(KeyCode.Alpha2))
            {
                Debug.Log("oop we reloading now boys!! no press 1");
                Time.timeScale = 1;
                string currentSceneName = SceneManager.GetActiveScene().name;
                SetHealth(max_health);
                ChangeScore(-1);
                SceneManager.LoadScene(currentSceneName);
                dead = false;
            }
        }
        
    }
}
