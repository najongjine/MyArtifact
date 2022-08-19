using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashAnim : MonoBehaviour
{
    [SerializeField]
    Sprite[] slashSprites;

    [SerializeField]
    float timeTreshold = 0.06f;

    [SerializeField]
    int damage = 35;

    float timer;
    int state = 0;

    SpriteRenderer sr;

    // Start is called before the first frame update
    void Awake()
    {
        sr=GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > timer)
        {
            if (state == slashSprites.Length)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                sr.sprite=slashSprites[state];
                state++;
                timer = Time.time + timeTreshold;
            }

        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wolf")
        {

        }
    }

}
