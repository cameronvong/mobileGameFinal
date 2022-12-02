using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Bunny.Tools;

public class HealthBar : MonoBehaviour
{

    public Slider slider;
    public Image fill;
    public PlayerData Statistics;

    Action<BunnyMessage<float>> callback;

    private void Awake() {
        BunnyEventManager.Instance.RegisterEvent("OnPlayerHurt", this);
        slider = GetComponent<Slider>();
        fill = GetComponentInChildren<Image>();
        slider.maxValue = Statistics.MaxHealth;
        slider.value = Statistics.MaxHealth;
    }

    void Start()
    {
        callback = OnPlayerHurt;
        BunnyEventManager.Instance.OnEventRaised("OnPlayerHurt", callback);
    }

    public void SetMaxHealth(float health)
    {
        slider.maxValue = Statistics.MaxHealth;
        slider.value = Statistics.MaxHealth;
    }

    public void SetHealth(float health)
    {
        slider.value = health;
        Debug.Log($"Slider's new value is {slider.value}");
    }

    public void OnPlayerHurt(BunnyMessage<float> message)
    {
        SetHealth(message.payload);
    }

}