using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{

    GameObject player;
    Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;
    Animator animator;
    Animation animation;
    //public Text infoText;
    float jumpPower = 3;
    float playerSpeed = 1;
    bool groundCheck = true;
    int directionInput;
    public bool facingRight = true;
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); 
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        animator.SetBool("Wait", true);

    }
    void KeyboardController()
    {
        if (Input.GetKeyDown(KeyCode.A)) {Move(-1); } 
        if (Input.GetKeyUp(KeyCode.A)) {Move(0);}
        if (Input.GetKeyDown(KeyCode.D)) {Move(1);} 
        if (Input.GetKeyUp(KeyCode.D)) {Move(0);}
        if (Input.GetKeyDown(KeyCode.Space)) {Jump(true);}
    }
    public void Move(int InputAxis)
    {
        directionInput = InputAxis;
        if (directionInput != 0) 
        {
            animator.SetBool("Move", true);
            animator.SetBool("Wait", false);
        } else 
        {
            animator.SetBool("Wait", true);
            animator.SetBool("Move", false);
        }
    }
    public void Attack(bool Attack)
    {
        int ComboAttack = 0;
        if(groundCheck && Attack == true && ComboAttack == 0)
        {   
            ComboAttack++;
            animator.SetBool("Jump", false);
            animator.SetBool("isGround", false);
            animator.SetBool("Wait", false);
            animator.SetBool("Move", false); 
            animator.SetBool("Attack1", true);
        }
        if(ComboAttack == 1)
        {
            ComboAttack = 0;
            animator.SetBool("Jump", true);
            animator.SetBool("isGround", false);
            animator.SetBool("Wait", false);
            animator.SetBool("Move", false); 
            animator.SetBool("Attack1", false);
            animator.SetBool("Attack2", true);
        }
    }
    public void Jump(bool isJump)
    {
        groundCheck = isJump;
        if (groundCheck)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpPower); 
            animator.SetBool("Jump", true);
            animator.SetBool("isGround", false);
            animator.SetBool("Wait", false);
            animator.SetBool("Move", false); 
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    void FixedUpdate()
    {
        rb2d.velocity = new Vector2(playerSpeed * directionInput, rb2d.velocity.y);
    }
    void Update()
    {
        KeyboardController();
        if ((directionInput < 0) && (facingRight))
        {
            Flip();
        }

        if ((directionInput > 0) && (!facingRight))
        {
            Flip();
        }
        groundCheck = true;
    }
    void DrawText()
    {
        
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Collider")
        {
            animator.SetBool("isGround", true);
            animator.SetBool("Jump", false);
            animator.SetBool("Wait", false);
        }
    }
}
