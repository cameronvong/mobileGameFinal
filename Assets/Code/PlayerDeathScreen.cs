using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Bunny;
using Bunny.Tools;

public class PlayerDeathScreen : MonoBehaviour
{
    public Action<BunnyMessage<float>> callback;

    // Start is called before the first frame update
    private void Awake() {
        BunnyEventManager.Instance.RegisterEvent("OnPlayerHurt", this);
        
        callback = goToGameOver;
    }

    void OnDestroy()
    {
        BunnyEventManager.Instance.Disconnect("OnPlayerHurt");
    }

    private void Start() {
        BunnyEventManager.Instance.OnEventRaised<float>("OnPlayerHurt", callback);
    }

    public void goToGameOver(BunnyMessage<float> message) {
        if (message.payload <= 0) {
            StartCoroutine(toDeathMenu());
        }
    }
    
    IEnumerator toDeathMenu() {
        yield return new WaitForSecondsRealtime(1.7f);
        SceneManager.LoadScene("Game Over");

    }
}
