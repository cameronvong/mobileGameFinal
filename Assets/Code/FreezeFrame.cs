using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bunny;
using Bunny.Tools;

public class FreezeFrame : MonoBehaviour
{
    bool waiting = false;
    public Action<BunnyMessage<float>> callback;

    private void Awake() {
        BunnyEventManager.Instance.RegisterEvent("OnBossHurt", this);
        callback = FeedbackFreeze;
    }

    private void Start() {
        BunnyEventManager.Instance.OnEventRaised<float>("OnBossHurt", callback);
    }

    // Start is called before the first frame update
    public void FeedbackFreeze(BunnyMessage<float> message) {
        if (!waiting) {
            StartCoroutine(WaitTime());
        }
    }

    IEnumerator WaitTime() {
        waiting = true;
        yield return new WaitForSecondsRealtime(0.6f);
        Time.timeScale = 0.0f;
        yield return new WaitForSecondsRealtime(0.2f);
        Time.timeScale = 1.0f;
        waiting = false;
    }


}
