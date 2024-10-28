using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameManager gm;
    private int score;
    public TopDown_AnimatorController anim;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gm = FindFirstObjectByType<GameManager>();
        anim = GetComponentInChildren<TopDown_AnimatorController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Coin"))
        {
            // Increase Score
            gm.ChangeScore(1); // Add 1 to score for each coin

            //Destroy the coin
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Axe"))
        {
            Destroy(other.gameObject);
            anim.SwitchToAxe();
        }
    }
}
