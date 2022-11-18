using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public AIData BossData;
    public float Health;

    Action<BunnyMessage<float>> onBossAttacked;
    private void Awake() {
        BunnyEventManager.Instance.RegisterEvent("DamageBossRequest", this);
    }

    // Start is called before the first frame update
    void Start()
    {
      Health = BossData.Health; 
      onBossAttacked = DamageBoss;
      BunnyEventManager.Instance.OnEventRaised<float>("DamageBossRequest", onBossAttacked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DamageBoss(BunnyMessage<float> message)
    {
        Health -= message.payload;
    }
}
