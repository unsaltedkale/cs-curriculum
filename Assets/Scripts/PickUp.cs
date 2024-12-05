using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameManager gm;
    private int score;
    public TopDown_AnimatorController anim;
    public PlayerController player;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gm = FindFirstObjectByType<GameManager>();
        anim = GetComponentInChildren<TopDown_AnimatorController>();
        player = FindFirstObjectByType<PlayerController>();
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

        if (other.CompareTag("BigPotion"))
        {
            player.AddBigPotion();
            Destroy(other.gameObject);
        }
        
        if (other.CompareTag("SmallPotion"))
        {
            player.AddSmallPotion();
            Destroy(other.gameObject);
        }
        
    }
}
