using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private GameManager gm;
    private int scoreTextScoreVariableInt;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gm = FindFirstObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreTextScoreVariableInt = gm.GetScore();
        scoreText.text = "Score: " + scoreTextScoreVariableInt.ToString();
    }
}
