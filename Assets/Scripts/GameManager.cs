using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Awake()
    {
        if(GameManager != null && GameManager !=this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            GameManager = this;
            DontDestroyOnLoad(this.gameObject)
        }
    }

    public int Score;

    public int GetScore()
    {
        return Score;
    }

    private SetScore(int amount)
    {
        Score = amount;
    }

    public void AddScore(int amount)
    {
        Score += amount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
