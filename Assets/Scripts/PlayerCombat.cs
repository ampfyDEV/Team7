using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour, ISlayable
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private float health;
    [SerializeField] private HealthBarUI healthBarUI;
    [SerializeField] private float _healthDecay;
    [SerializeField] private float _decayRate = 1f;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private AttackDetection attackDetection;

    private void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
        if (!playerStats) Debug.LogError("null Player stats");
    }

    private void Start()
    {
        health = playerStats.GetMaxHP();
    }

    private void Update()
    {
        _healthDecay += Time.deltaTime;
        if (_healthDecay > _decayRate)
        {
            TakeDamage(1);
            _healthDecay = 0;
        }
    }


    public void TakeDamage(float attack)
    {
        health -= attack;
        healthBarUI.UpdatePlayerHealth(playerStats.GetMaxHP(), health);
        if (health <= 0)
        {
            //TODO: Player Death Animation
            gameManager.GameOver();
        }
    }

    public float AttackDamage() { return playerStats.GetAttack(); }

    public float GetHealth() { return playerStats.GetMaxHP(); }

    public Vector3 GetPosition() { return gameObject.transform.position; }

    public void AttackEnemy()
    {
        var target = attackDetection.getTarget();
        if (target == null) return;
        target.TakeDamage(99);
        attackDetection.EnemyDestroyed();
    }

    public void RestoreHealth(float amount)
    {
        health = amount;
    }
}
