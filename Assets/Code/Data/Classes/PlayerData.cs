using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "GameData/PlayerData", order = 0)]
public class PlayerData : ScriptableObject {
    public float Health = 100f;
    public float WalkingSpeed = 5f;
    public float RunningSpeed = 10f;
    public float BaseMeleeDamage = 5f;
    public float BaseRangeDamage = 5f;
    public float AttackSpeed = 0.5f;
    public int MaxAttackCombo = 1;
    public float MeleeAttackRange = 1f;
    public float Stamina = 100f;
    public float DashCooldownTime = 1.2f;
    public float DashTime = 0.4f;
    public float DashSpeed = 10f;
}
