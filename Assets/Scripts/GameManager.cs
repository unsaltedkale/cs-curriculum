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
    private bool keypress;
    

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

    private bool dead = false;
    private int cycle = 0;

    public void Die()
    {
        dead = true;
        Time.timeScale = 0;
        cycle = 0;
        
        if (keypress = true && Input.GetKey(KeyCode.Alpha1))
        {
            Debug.Log("oop we reloading now boys!! option 1");
            Time.timeScale = 1;
            string currentSceneName = SceneManager.GetActiveScene().name;
            health = max_health;
            ChangeScore(-1);
            SceneManager.LoadScene("Start");
            dead = false;
        }

        else
        {
            Debug.Log("oop we reloading now boys!! no press 1");
            Time.timeScale = 1;
            string currentSceneName = SceneManager.GetActiveScene().name;
            health = max_health;
            ChangeScore(-1);
            SceneManager.LoadScene(currentSceneName);
            dead = false;
        }
        
    }
    public IEnumerator Die2()
    {
        while (Time.timeScale == 0)
        {
            if (keypress = true && Input.GetKeyDown(KeyCode.Alpha1) && !Input.GetKeyDown(KeyCode.Alpha2))
            {
                Debug.Log("oop we reloading now boys!! option 1");
                Time.timeScale = 1;
                string currentSceneName = SceneManager.GetActiveScene().name;
                health = max_health;
                ChangeScore(-1);
                SceneManager.LoadScene(currentSceneName);
                dead = false;
                
                yield break;
            }

            else if (keypress = true && Input.GetKeyDown(KeyCode.Alpha2) && !Input.GetKeyDown(KeyCode.Alpha1))
            {
                Debug.Log("oop we reloading now boys!! option 2");
                Time.timeScale = 1;
                string currentSceneName = SceneManager.GetActiveScene().name;
                health = max_health;
                ChangeScore(-1);
                SceneManager.LoadScene(currentSceneName);
                dead = false;
                
                yield break;
            }

            else if (keypress = true && Input.GetKeyDown(KeyCode.Alpha2) && Input.GetKeyDown(KeyCode.Alpha1))
            {
                Debug.Log("plewase press only oneeee :pleading_face:");
            }
        }

        cycle += 1;
        Debug.Log("Number of Cycles:" + cycle);
        yield return new WaitForSecondsRealtime(1);
        
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.anyKey)
        {
            keypress = true;
        }
        
        else
        {
            keypress = false;
        }
    }
}
