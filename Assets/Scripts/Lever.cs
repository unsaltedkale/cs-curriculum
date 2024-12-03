using System;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public GameObject Door;
    public SpriteRenderer sr;
    public Sprite OffSprite;
    public Sprite OnSprite;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr.sprite = OffSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        sr.sprite = OnSprite;
        Destroy(Door);
    }
}
