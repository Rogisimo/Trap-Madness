using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float bounceForce = 10f;
    private Animator anim;

    private void Start() {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Player"){
            Rigidbody2D playerRb = other.gameObject.GetComponent<Rigidbody2D>();
            playerRb.AddForce(new Vector2(0,bounceForce), ForceMode2D.Impulse);
            anim.SetTrigger("Jump");
        }
    }
}
