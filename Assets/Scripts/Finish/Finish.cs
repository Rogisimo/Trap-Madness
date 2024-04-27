using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    public string levelToLoad;
    public GameObject endScreen;


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            if(levelToLoad == "End"){
                endScreen.SetActive(true);
                Time.timeScale = 0f;
            }
            else{
                SceneManager.LoadScene(levelToLoad);
            }
        }
    }
}
