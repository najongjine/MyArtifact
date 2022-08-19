using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHarvest : MonoBehaviour
{
    [SerializeField]
    float harvestTime = 0.4f;

    PlayerMovement playerMovement;
    PlayerBackpack backpack;

    AudioSource audioSource;

    Collider2D collidedBush;
    BushFruits hitBush;

    bool canHarvestFruits;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        backpack = GetComponent<PlayerBackpack>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space))
        {
            TryHarvestFruit();
        }
    }
    void TryHarvestFruit()
    {
        if (!canHarvestFruits)
        {
            return;
        }
        if (collidedBush)
        {
            hitBush=collidedBush.GetComponent<BushFruits>();
            if (hitBush && hitBush.HasFruits())
            {
                audioSource.Play();
                playerMovement.HarvestStopMovement(harvestTime);
                backpack.AddFruits(hitBush.HarvestFruit());
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bush")
        {
            canHarvestFruits= true;
            collidedBush = collision;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bush")
        {
            canHarvestFruits = false;
            collidedBush = null;
        }
    }

}
