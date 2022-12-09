using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Update() 
    {
        if (Input.GetButtonDown("Play")) { Play(); }
        if (Input.GetButtonDown("Quit"))  { Quit(); }
    }


    public void Play(){
        SceneManager.LoadScene("SkullBoss");
    }

    public void Quit(){
        Debug.Log("QUIT");
        Application.Quit();
    }
}