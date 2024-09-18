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
    }

    public int Score;

    public int GetScore()
    {
        return Score;
    }

    private void SetScore(int amount)
    {
        Score = amount;
    }

    public void AddScore(int amount)
    {
        Score += amount;
        // Temporary Score display
        Debug.Log("Score: " + Score);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
