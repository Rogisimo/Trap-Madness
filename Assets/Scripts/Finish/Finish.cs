using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    public string levelToLoad;
    public GameObject endScreen;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            FindObjectOfType<AudioManager>().Play("GameWin");
            if (levelToLoad == "End")
            {
                endScreen.SetActive(true);
                FindObjectOfType<AudioManager>().Stop("MainMusic");
                Time.timeScale = 0f;
            }
            else
            {
                SceneManager.LoadScene(levelToLoad);
            }
        }
    }
}
