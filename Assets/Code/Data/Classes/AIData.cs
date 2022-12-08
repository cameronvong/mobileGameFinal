using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "GameData/AIData", order = 0)]
public class AIData : ScriptableObject {
    public float Health = 100f;
    public float WalkingSpeed = 5f;
    public float RunningSpeed = 10f;
    public float BaseMeleeDamage = 5f;
    public float BaseRangeDamage = 5f;
    public int MaxAttackCombo = 1;
    public float AttackSpeed = 1.5f;
    public float AttackCooldown = 1f;
    public float SpecialAttackCooldown = 30f;
    public float MeleeAttackRange = 1f;
    public float RangeAttackRange = 10f;
    public float EnragePoint = 0.5f;
    public float MeleeAttackAnimLength = 0.8f;
    public float MeleeDamageDelay = 0.5f;
}
