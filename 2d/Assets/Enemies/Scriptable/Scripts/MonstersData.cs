using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMonsterData", menuName = "Monster Data")]

public class MonstersData : ScriptableObject
{
    public int Health;
    public int AttackPower;
    public float AttackSpeed;
    public float SpeedMonster;
    public float BulletSpeed;
    public float SpeedRebound;
    public bool isValid;
}
