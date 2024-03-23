using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public int scoreToAdd;
    public GameObject destroyEffect;
    
    Player player;

    private void Start() {
        player = FindObjectOfType<Player>();
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            AddScore();
        }
    }

    void AddScore(){
        player.UpdateScore(scoreToAdd);
        Instantiate(destroyEffect, transform.position,Quaternion.identity);
        Destroy(this.gameObject);
    }
}
