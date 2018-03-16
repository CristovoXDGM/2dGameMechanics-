using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class movementBehaviour : MonoBehaviour {

    public float maxSpeed;
    public float jumpForce;
    public Transform groundCheck;

    private bool isGrounded;
    private bool isJumping ;
    

    private Rigidbody2D rb2d;
    private Animator anim;
    private SpriteRenderer sprite;


    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (Input.GetButtonDown("Jump")) {
            isJumping = true;
        }

	}

    void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(move));

        rb2d.velocity = new Vector2(move * maxSpeed, rb2d.velocity.y);
        if ((move > 0 && sprite.flipX) || (move < 0 && !sprite.flipX))
        {
            Flip();
        }
        if (isJumping && isGrounded)
        {
            rb2d.velocity = Vector2.up * jumpForce;
            anim.SetTrigger("Jump");
            isJumping = false;
        }
    }

    void Flip() {
        sprite.flipX = !sprite.flipX;
    }
}
