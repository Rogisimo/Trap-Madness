using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public int scoreToAdd;
    public GameObject destroyEff;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            AddScore();
        }
    }

    void AddScore(){
        //Add score to player
        player.UpdateScore(scoreToAdd);
        //Spawn destroy effect
        Instantiate(destroyEff, transform.position, Quaternion.identity);
        //Destroy this object
        Destroy(this.gameObject);
    }
}
