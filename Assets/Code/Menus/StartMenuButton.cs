using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuButton : MonoBehaviour
{

    public void Play(){
        Debug.Log("Clicked button");
        SceneManager.LoadScene("SkullBoss");
    }

    public void Quit(){
        Debug.Log("QUIT");
        Application.Quit();
    }
}
