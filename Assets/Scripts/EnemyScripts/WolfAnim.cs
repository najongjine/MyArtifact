using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAnim : MonoBehaviour
{
    [SerializeField]
    Sprite[] wolfAnimSprites;

    [SerializeField]
    float animTimeThreshold = 0.15f;

    WolfAI wolfAI;

    SpriteRenderer sr;

    int state = 0;
    float animTimer;

    private void Awake()
    {
        wolfAI = GetComponent<WolfAI>();
        sr = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (wolfAI.isMoving)
        {
            if (Time.time > animTimer)
            {
                sr.sprite = wolfAnimSprites[state % wolfAnimSprites.Length];
                state++;
                if (state == wolfAnimSprites.Length)
                {
                    state = 0;
                }
                animTimer = Time.time + animTimeThreshold;
            }
        }
        else
        {
            sr.sprite=wolfAnimSprites[0];
        }
        sr.flipX = wolfAI.left;

    }



}
