using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Bunny;
using Bunny.Tools;

public class DefeatedBoss : MonoBehaviour
{
    
    // Start is called before the first frame update
    private void Awake() {
        BunnyEventManager.Instance.RegisterEvent("OnBossHurt", this);
        
        callback = sceneTransition;
    }

    private void Start() {
        BunnyEventManager.Instance.OnEventRaised<float>("OnBossHurt", callback);
    }

    public void sceneTransition(BunnyMessage<float> message) {
        if (message.payload <= 0) {
            StartCoroutine(deathAnimWait());
        }
    }
    
    IEnumerator deathAnimWait() {
        yield return new WaitForSecondsRealtime(3.2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
    
}
