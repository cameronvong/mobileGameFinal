using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "GameData/AbstractProjectileData", order = 0)]
public class AbstractProjectileData : ScriptableObject {
    public float Lifetime;
    public float Damage;
    public float Speed;
}
