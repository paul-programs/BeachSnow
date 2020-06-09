using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour {
    public float speed = 5.0f;
    public float jumpVelocity = 5.0f;

    public LayerMask playerMask;

    private Transform myTransform;
    private Transform tagGround;
    private Rigidbody2D myRigidbody;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    public bool isGrounded = false;//hide after testing
    public bool canMoveInAir = true;

    public bool beach = true;

    public bool summerTrophy = false;//hide after testing
    public bool winterTrophy = false;//hide after testing

	// Use this for initialization
	void Start ()
    {
        myRigidbody = this.GetComponent<Rigidbody2D>();
        myTransform = this.transform;
        tagGround = GameObject.Find(this.name + "/tag_Ground").transform;
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(0.488f, 0.195f, 0.195f);
        animator = this.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        isGrounded = Physics2D.Linecast(myTransform.position, tagGround.position, playerMask);

        Move(Input.GetAxisRaw("Horizontal"));

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            SeasonChange();
        }
	}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //check for collision with trophies and destroy if season match
        if(collision.gameObject.tag == "Trophy")
        {
            if (collision.gameObject.name.ToLower().Contains("summertrophy") && beach)//if in beach form and summer trophy tigger
            {
                summerTrophy = true;
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.name.ToLower().Contains("wintertrophy") && !beach)//if not in beach form and winter tropjy trigger
            {
                winterTrophy = true;
                Destroy(collision.gameObject);
            }
        }
    }

    public void Move(float horizontalInput)
    {
        if (!canMoveInAir && !isGrounded)
            return;

        Vector2 moveVelocity = myRigidbody.velocity;
        moveVelocity.x = horizontalInput * speed;
        myRigidbody.velocity = moveVelocity;

        if(horizontalInput < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if(horizontalInput>0)
        {
            spriteRenderer.flipX = false;
        }
    }

    //tweak jump mechanics so it gives feel of jump momentum
    public void Jump()
    {
        if (isGrounded)
        {
            myRigidbody.velocity += jumpVelocity * Vector2.up;
        }
    }

    public void SeasonChange()
    {
        if (beach)
        {
            beach = false;
            spriteRenderer.color = new Color(0.195f, 0.195f, 0.488f);
        }
        else
        {
            beach = true;
            spriteRenderer.color = new Color(0.488f, 0.195f, 0.195f);
        }
    }

    public void TrophyMatch(Collider2D collision)
    {
        //method here to manage checks for trophy collision
    }

    //check for level completed
    public bool IsLevelComplete()
    {
        if (summerTrophy && winterTrophy)
            return true;
        else
            return false;
    }
}
