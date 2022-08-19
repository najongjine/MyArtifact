using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 3f;

    Rigidbody2D myBody;
    Vector2 moveVector;
    SpriteRenderer sr;
    Animator anim;

    float harvestTimer;
    bool isHarvesting;
    GameObject artifact;

    string MOVEMENT_AXIS_X = "Horizontal";
    string MOVEMENT_AXIS_Y = "Vertical";

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim=GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time>harvestTimer)
        {
            isHarvesting = false;
        }
        FlipSprite();
    }
    private void FixedUpdate()
    {
        if (isHarvesting)
        {
            myBody.velocity = Vector2.zero;
        }
        else
        {
            moveVector = new Vector2(Input.GetAxis(MOVEMENT_AXIS_X), Input.GetAxis(MOVEMENT_AXIS_Y));
            if (moveVector.sqrMagnitude>1)
            {
                moveVector=moveVector.normalized;
            }
            if (moveVector.sqrMagnitude > 0)
            {
                anim.SetBool("Walk", true);
            }
            else
            {
                anim.SetBool("Walk", false);
            }
            myBody.velocity = new Vector2(moveVector.x * movementSpeed, moveVector.y * movementSpeed);

        }

    }
    void FlipSprite()
    {
        if (Input.GetAxisRaw(MOVEMENT_AXIS_X) ==1)
        {
            sr.flipX = false;
        }
        else if (Input.GetAxisRaw(MOVEMENT_AXIS_X) == -1)
        {
            sr.flipX = true;
        }

    }
    
    public void HarvestStopMovement(float time)
    {
        isHarvesting = true;
        harvestTimer = Time.time + time;
    }
    public bool IsHarvesting()
    {
        return isHarvesting;
    }

}
