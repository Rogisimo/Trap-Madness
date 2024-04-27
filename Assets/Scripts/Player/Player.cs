using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public int health = 3;
    public int score = 0;
    private bool facingRight = true;
    public bool isGrounded = true;
    private int extraJumps;
    public int extraJumpsValue = 1;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    private Rigidbody2D rb;
    private Animator anim;
    public TextMeshProUGUI scoreText;
    public Image[] hearts;
    public GameObject gameOverScreen;
    GameObject startingPosition;


    // Start is called before the first frame update
    void Start()
    {
        TeleportPlayerToStartingPosition();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        extraJumps = extraJumpsValue;
        scoreText.text = score.ToString() + "X";
        UpdateHearts();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position,groundCheckRadius,whatIsGround);

        if(Input.GetButtonDown("Jump")){
            if(isGrounded){
                Jump();
                anim.SetTrigger("Jump");
                extraJumps = extraJumpsValue;
            }
            else if(extraJumps > 0){
                Jump();
                anim.SetTrigger("DoubleJump");
                extraJumps--;
            }
        }

        if(rb.velocity.y < 0 && !isGrounded){
            anim.SetBool("Falling",true);
        }
        else if(rb.velocity.y >= 0){
            anim.SetBool("Falling",false);
        }
    }

    private void FixedUpdate() {
        MovePlayer();
    }

    private void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
          if(i<health){
            hearts[i].enabled = true;
          }
          else{
            hearts[i].enabled = false;
          }
        }
    }

    public void TeleportPlayerToStartingPosition(){
         startingPosition = GameObject.Find("StartingPosition");
        transform.position = startingPosition.transform.position;
    }


    void MovePlayer(){
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        anim.SetFloat("Speed",Mathf.Abs(moveHorizontal));
        Vector2 movement = new Vector2(moveHorizontal,0);
        rb.velocity = new Vector2(movement.x * speed, rb.velocity.y);

        if(moveHorizontal > 0 && !facingRight){
            Flip();
        }
        else if(moveHorizontal < 0 && facingRight){
            Flip();
        }
    }

    void Jump(){
        rb.velocity = Vector2.up * jumpForce;
        isGrounded = false;
    }


    void Flip(){
        facingRight = !facingRight;
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void TakeDamage(int damage){
        health -= damage;
        anim.SetTrigger("Hit");
        UpdateHearts();
        TeleportPlayerToStartingPosition();
        if(health <= 0){
            Death();
        }
    }

    public void UpdateScore(int scoreToAdd){
        score += scoreToAdd;
        scoreText.text = score.ToString() + "X";
    }

    void Death(){
        gameOverScreen.SetActive(true);
        Time.timeScale = 0f;
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Spike"){
            TakeDamage(1);
        }
    }
}
