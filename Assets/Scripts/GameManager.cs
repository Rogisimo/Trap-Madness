using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void PlayAgain(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level1");
    }
}
