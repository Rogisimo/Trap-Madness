using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float fallDelay = 0.5f; // Delay before the platform falls
    private Rigidbody2D rb2d;
    private bool isGrounded;
    Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
        rb2d = GetComponent<Rigidbody2D>();
    }

  void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && player.isGrounded) // Check if the colliding object is the player
        {
            Invoke("StartFalling", fallDelay); // Start falling after a delay
        }
    }

    void StartFalling()
    {
        rb2d.bodyType = RigidbodyType2D.Dynamic; // Change body type to dynamic to enable gravity
        // Optional: Add any additional effects here (like shaking, sound, etc.)
    }
}
