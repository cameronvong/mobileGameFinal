using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bunny;
using Bunny.Tools;

public enum CameraEffectType
{
    NONE,
    SHAKE
}

public class CameraEffectRequestPayload
{
    public CameraEffectType effectType = CameraEffectType.SHAKE;
    public float duration = 1f;
}

public class CameraEffects : MonoBehaviour
{
    public bool start = false;
    public AnimationCurve curve;
    public float duration;
    float elapsedTime = 1f;
    CameraEffectType currentEffectType = CameraEffectType.NONE;

    Action<BunnyMessage<CameraEffectRequestPayload>> callback;

    public IEnumerator Shake() {
        Vector3 startPosition = transform.position;
        while (elapsedTime < duration) {
            elapsedTime += Time.deltaTime; 
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position = startPosition + UnityEngine.Random.insideUnitSphere * strength;
            yield return null;
        }
        transform.position = startPosition;
        elapsedTime = duration;
    }

    public void HandleEffectRequest(BunnyMessage<CameraEffectRequestPayload> message)
    {
        duration = message.payload.duration;
        currentEffectType = CameraEffectType.SHAKE;
    }

    public void HandleEffect(CameraEffectType effType) {
        switch(effType)
        {
            case CameraEffectType.SHAKE:
                StartCoroutine(Shake());
                return;
            default:
                return;
        }
    }
    private void Awake() {
        BunnyEventManager.Instance.RegisterEvent("CameraEffectRequest", this);
        callback = HandleEffectRequest;
    }
    // Start is called before the first frame update
    void Start()
    {
        BunnyEventManager.Instance.OnEventRaised<CameraEffectRequestPayload>("CameraEffectRequest", callback);
    }

    // Update is called once per frame
    void Update()
    {
        if (start) {
            start = false;
            HandleEffect(CameraEffectType.SHAKE);
        } else if(currentEffectType != CameraEffectType.NONE) {
            elapsedTime = 0f;
            HandleEffect(currentEffectType);
            currentEffectType = CameraEffectType.NONE;
        }
    }
}
