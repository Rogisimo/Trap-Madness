using TMPro;
using UnityEngine;
using UnityEngine.UI; // Include the UI namespace for working with UI elements

public class Player : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 10.0f;
    private Rigidbody2D rb;
    public bool isGrounded = true;
    private int extraJumps;
    public int extraJumpsValue = 1;
    public int health = 3; // Player's health
    private bool facingRight = true;
    private Animator animator;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask whatIsGround;
    public int score = 0;
    public Image[] hearts; // Array to store the heart UI elements
    public TextMeshProUGUI scoreText;
    public GameObject gameOverScreen;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        extraJumps = extraJumpsValue;
        animator = GetComponent<Animator>();
        UpdateHearts(); // Initialize the hearts UI
        scoreText.text = score.ToString();
        
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

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                Jump();
                animator.SetTrigger("Jump");
                extraJumps = extraJumpsValue;
            }
            else if (extraJumps > 0)
            {
                Jump();
                extraJumps--;
                animator.SetTrigger("DoubleJump");
            }
        }

        if (rb.velocity.y < 0 && !isGrounded)
        {
            animator.SetBool("Falling", true);
        }
        else if(rb.velocity.y > 0 || isGrounded){
            animator.SetBool("Falling", false);
        }
    }

    void MovePlayer()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));
        Vector2 movement = new Vector2(moveHorizontal, 0);
        rb.velocity = new Vector2(movement.x * speed, rb.velocity.y);

        animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));

        if (moveHorizontal > 0 && !facingRight)
            Flip();
        else if (moveHorizontal < 0 && facingRight)
            Flip();
    }

    void Jump()
    {
        rb.velocity = Vector2.up * jumpForce;
        FindObjectOfType<AudioManager>().Play("Jump");
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
        animator.SetTrigger("Hurt");
        FindObjectOfType<AudioManager>().Play("Hit");
        UpdateHearts(); // Update the hearts UI
        if (health <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        Debug.Log("Game Over");
        animator.SetTrigger("Death");
        FindObjectOfType<AudioManager>().Play("GameOver");
        gameOverScreen.SetActive(true);
        Time.timeScale = 0f;
        this.enabled = false;
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Spike"))
        {
            TakeDamage(1);
        }
    }

    public void UpdateScore(int scoreToAdd){
        FindObjectOfType<AudioManager>().Play("PickUp");
        score += scoreToAdd;
        scoreText.text = score.ToString();
    }

    // Updates the heart UI to match the current health
    void UpdateHearts()
    {
        // Loop through all hearts
        for (int i = 0; i < hearts.Length; i++)
        {
            // If the current index is less than the current health, heart is enabled
            if (i < health)
            {
                hearts[i].enabled = true;
            }
            else // Otherwise, disable the heart
            {
                hearts[i].enabled = false;
            }
        }
    }

}
