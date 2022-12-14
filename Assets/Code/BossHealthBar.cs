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
        if(BunnyEventManager.Instance.TryGetEvent("OnBossHurt", out BunnyEvent dataEvent) == false);
            BunnyEventManager.Instance.RegisterEvent("OnBossHurt", this);
        slider = GetComponent<Slider>();
        fill = GetComponentInChildren<Image>();
        slider.maxValue = Statistics.Health;
        slider.value = Statistics.Health;
    }

    void Start()
    {
        callback = OnBossHurt;
        BunnyEventManager.Instance.OnEventRaised("OnBossHurt", callback);
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

    public void OnBossHurt(BunnyMessage<float> message)
    {
        SetHealth(message.payload);
    }

}