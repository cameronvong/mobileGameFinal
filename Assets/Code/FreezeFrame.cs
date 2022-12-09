using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bunny;
using Bunny.Tools;

public class FrameFreeze : MonoBehaviour
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
    public void FeedbackFreeze() {
        if (!waiting) {
            Time.timeScale = 0.0f;
            StartCoroutine(WaitTime(0.01f));
        }
    }

    IEnumerator WaitTime() {
        waiting = true;
        yield return new WaitForSecondsRealtime(0.01f);
        Time.timeScale = 1.0f;
        waiting = false;
    }

}
