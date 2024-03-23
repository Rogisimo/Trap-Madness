using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float bounceForce = 10f; // The force with which the player will be bounced upwards

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Ensure the colliding object is the player
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                // Apply an upward force to the player's Rigidbody
                playerRb.AddForce(new Vector2(0, bounceForce), ForceMode2D.Impulse);
            }
        }
    }
}
