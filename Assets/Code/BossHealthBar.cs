using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Bunny.Tools;

public class BossHealthBar : MonoBehaviour
{

    public Slider slider;
    public Image fill;
    public AIData Statistics;

    Action<BunnyMessage<float>> callback;

    private void Awake() {
        BunnyEventManager.Instance.RegisterEvent("OnBossHurt", this);
        slider = GetComponent<Slider>();
        fill = GetComponentInChildren<Image>();
        slider.maxValue = Statistics.Health;
        slider.value = Statistics.Health;
    }

    void Start()
    {
        callback = OnPlayerHurt;
        BunnyEventManager.Instance.OnEventRaised("OnPlayerHurt", callback);
    }

    public void SetMaxHealth(float health)
    {
        slider.maxValue = Statistics.Health;
        slider.value = Statistics.Health;
    }

    public void SetHealth(float health)
    {
        slider.value = health;
    }

    public void OnPlayerHurt(BunnyMessage<float> message)
    {
        SetHealth(message.payload);
    }

}