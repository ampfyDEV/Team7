using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float health;
    [SerializeField] private float attack;

    public float GetMaxHP() { return maxHealth; }
    public float GetAttack() { return attack; }
}
