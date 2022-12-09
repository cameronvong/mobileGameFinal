using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bunny;
using Bunny.Tools;

public class FrameFreeze : MonoBehaviour
{
    bool waiting;
    public Action<BunnyMessage<float>> callback;

    private void Awake() {
        BunnyEventManager.Instance.RegisterEvent("OnBossHurt", this);
        callback = FeedbackFreeze;
    }

    private void Start() {
        waiting = false;
        BunnyEventManager.Instance.OnEventRaised<float>("OnBossHurt", callback);
    }

    // Start is called before the first frame update
    public void FeedbackFreeze(BunnyMessage<float> message) {
        if (waiting == false) {
            return;
        }
        Time.timeScale = 0.0f;
        StartCoroutine(WaitTime());
        
    }

    IEnumerator WaitTime() {
        waiting = true;
        yield return new WaitForSecondsRealtime(3.0f);
        Time.timeScale = 1.0f;
        waiting = false;
    }

}
