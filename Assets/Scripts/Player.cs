using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 10.0f;
    private Rigidbody2D rb;
    private bool isGrounded = true;
    private int extraJumps;
    public int extraJumpsValue = 1;
    public int health = 3;
    private bool facingRight = true; // For determining which way the player is currently facing.
    private Animator animator; // Animator variable

    public Transform groundCheck; // Assign in the inspector
    public float groundCheckRadius = 0.2f; // Radius of the overlap circle
    public LayerMask whatIsGround; // Assign in the inspector, set to the ground layer

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        extraJumps = extraJumpsValue;
        animator = GetComponent<Animator>(); // Get the Animator component attached to the player
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void Update()
    {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        if (health <= 0)
        {
            Death();
            return; // Stop further execution if player is dead
        }

        if(isGrounded){
            animator.SetBool("Falling", false);
        }


        // Jump if space is pressed and either on the ground or have extra jumps left
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                Jump();
                animator.SetTrigger("Jump");
                // Reset extra jumps when jumping from the ground
                extraJumps = extraJumpsValue;
            }
            else if (extraJumps > 0)
            {
                Jump();
                // Decrease extra jumps if jumping in the air
                extraJumps--;
                animator.SetTrigger("DoubleJump"); // Trigger double jump animation
            }
        }

        // Check if the player is falling
        if (rb.velocity.y < 0 && !isGrounded)
        {
            animator.SetBool("Falling", true);
        }
    }

    void MovePlayer()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));
        Vector2 movement = new Vector2(moveHorizontal, 0);
        rb.velocity = new Vector2(movement.x * speed, rb.velocity.y);

        // Set the running animation speed parameter
        animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));

        if (moveHorizontal > 0 && !facingRight)
            Flip();
        else if (moveHorizontal < 0 && facingRight)
            Flip();
    }

    void Jump()
    {
        rb.velocity = Vector2.up * jumpForce;
        isGrounded = false;
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


    public void TakeDamage(int damage)
    {
        health -= damage;
        animator.SetTrigger("Hurt"); // Trigger hit animation
        if (health <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        Debug.Log("Game Over");
        // Optionally, trigger death animation
        animator.SetTrigger("Death");
        // Disable player movement or other actions
        this.enabled = false;
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Spike"){
            TakeDamage(1);
        }
    }

}
