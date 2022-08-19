using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAI : MonoBehaviour
{
    [SerializeField]
    bool isEater;

    [SerializeField]
    float moveSpeed=1f;

    [SerializeField]
    int attackDamage = 5;

    [SerializeField]
    float attackTimeThreshold = 1f;

    [SerializeField]
    float eatTimeThreshold = 2f;

    [SerializeField]
    LayerMask bushMask;

    [HideInInspector]
    public bool isMoving, left;

    Artifact artifact;

    BushFruits bushFruitsTarget;

    float attackTimer;
    float eatTimer;

    bool killingBush;
    bool isAttacking;

    // Start is called before the first frame update
    void Start()
    {
        if (isEater)
        {
            SearchForTarget();
            killingBush = false;
        }
        else
        {
            isAttacking = false;
        }
        artifact = GameObject.FindGameObjectWithTag("Artifact").GetComponent<Artifact>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!artifact)
        {
            return;
        }
        if (isEater)
        {
            if (bushFruitsTarget && bushFruitsTarget.enabled && bushFruitsTarget.HasFruits() && !killingBush)
            {
                //if not close to the bush then walk. else stop n eat bush
                if (Vector2.Distance(transform.position,bushFruitsTarget.transform.position)>0.5f)
                {
                    var step = moveSpeed * Time.deltaTime;
                    transform.position = Vector2.MoveTowards(transform.position
                        ,bushFruitsTarget.transform.position, step);
                    isMoving = true;
                }
                else
                {
                    isMoving = false;
                    bushFruitsTarget.HarvestFruit();
                    eatTimer = Time.time + eatTimeThreshold;
                    killingBush = true;
                }
            }else if (killingBush)
            {
                if (Time.time > eatTimer)
                {
                    bushFruitsTarget.EatBushFruits();
                    killingBush = false;
                    SearchForTarget();
                }
            }
            else
            {
                SearchForTarget();
            }
            if (!bushFruitsTarget)
            {
                SearchForTarget();
            }
            if (bushFruitsTarget?.transform?.position.x < transform.position.x)
            {
                left = true;
            }
            else
            {
                left = false;
            }
           
        }
        else
        {
            // wolf that destroys artifact
            if (Vector2.Distance(transform.position,artifact.transform.position)>1.5f)
            {
                var step=moveSpeed*Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, artifact.transform.position, step);
                isMoving=true;
            }else if (!isAttacking)
            {
                isAttacking=true;
                attackTimer = Time.time + attackTimeThreshold;
                isMoving = false;
            }
            if (isAttacking)
            {
                if (Time.time > attackTimer)
                {
                    Attack();
                    attackTimer=Time.time + attackTimeThreshold;
                }
            }

        }
    }
    void SearchForTarget()
    {
        bushFruitsTarget = null;
        Collider2D[] hits;

        for(int i = 1; i < 50; i++)
        {
            hits=Physics2D.OverlapCircleAll(transform.position,Mathf.Exp(i),bushMask);
            foreach(var hit in hits)
            {
                if (hit && hit.GetComponent<BushFruits>().HasFruits() && hit.GetComponent<BushFruits>().enabled  )
                {
                    bushFruitsTarget=hit.GetComponent<BushFruits>();
                    break;
                }
            }
            if (bushFruitsTarget)
            {
                break;
            }

        }
    }
    void Attack()
    {
        artifact.TakeDamage(attackDamage);
    }

}
