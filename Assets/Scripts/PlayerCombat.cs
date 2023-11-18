using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour, ISlayable
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private float health;

    private void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
        if (!playerStats) Debug.LogError("null Player stats");
    }

    private void Start()
    {
        health = playerStats.GetMaxHP();
    }

    public void TakeDamage(float attack) { health -= attack; }

    public float AttackDamage() { return playerStats.GetAttack(); }

    public float GetHealth() { return health; }

    public Vector3 GetPosition() { return gameObject.transform.position; }
}
