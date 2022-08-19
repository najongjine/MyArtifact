using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artifact : MonoBehaviour
{
    public int health;
    public int maxHealth=150;

    public int bleed = 2;

    AudioSource audioSource;

    float bleedTimer;

    PlayerBackpack playerBackpack;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        playerBackpack =GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBackpack>();
        health = maxHealth;

        bleedTimer = Time.time+1f;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > bleedTimer)
        {
            health -= bleed;
            bleedTimer = Time.time+1f;
        }
        CheckHealth();
    }
    public void TakeDamage(int damageAmount)
    {
        health-=damageAmount;
    }
    void CheckHealth()
    {
        if (health<=0)
        {
            health = 0;
            // show game over UI
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (playerBackpack?.currentNumberOfStoredFruits!=0)
            {
                audioSource.Play();
            }
            health+= playerBackpack.TakeFruits();
            if (health>maxHealth)
            {
                health = maxHealth;
            }
        }
    }

}
