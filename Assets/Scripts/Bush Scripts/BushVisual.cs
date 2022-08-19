using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushVisual : MonoBehaviour
{
    [SerializeField]
    Sprite[] bushSprites, fruitSprites, drySprites;

    [SerializeField]
    SpriteRenderer[] fruitsRenderers;

    public enum BushVariant { Green,Cyan,Yellow}
    BushVariant bushVariant;

    public float hideTimePerFruit = 0.2f;
    SpriteRenderer sr;

    // Start is called before the first frame update
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        bushVariant = (BushVariant)Random.Range(0, bushSprites.Length);
        sr.sprite = bushSprites[(int)bushVariant];

        if (Random.Range(0, 2) == 1)
        {
            sr.flipX = true;
        }
        for(int i = 0; i < fruitsRenderers.Length; i++)
        {
            fruitsRenderers[i].sprite = fruitSprites[(int)bushVariant];
            fruitsRenderers[i].enabled = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public BushVariant GetBushVariant()
    {
        return bushVariant;
    }
    public void SetToDry()
    {
        sr.sprite=drySprites[(int)bushVariant];
    }
    IEnumerator _HideFruits(float time,int index)
    {
        yield return new WaitForSeconds(time);
        fruitsRenderers[index].enabled = false;
    }
    public void HideFruits()
    {
        var waitTimeForFruit = hideTimePerFruit;
        for(int i = 0; i < fruitsRenderers.Length; i++)
        {
            StartCoroutine(_HideFruits(waitTimeForFruit,i));
            waitTimeForFruit += hideTimePerFruit;
        }

    }
    public void ShowFruits()
    {
        for (int i = 0; i < fruitsRenderers.Length; i++)
        {
            fruitsRenderers[i].enabled = true;
        }
    }

}
