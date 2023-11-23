using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, ISlayable
{
    [SerializeField] private EnemyController enemyController;

    private void Awake()
    {
        enemyController = GetComponent<EnemyController>();
        if (!enemyController) Debug.LogError("missing enemy controller ref in EnemyAI.cs");
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyRevealed(other);


        //if hit player then deal damge
        DamagePlayer(other);
    }

    private void DamagePlayer(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            var player = other.GetComponent<ISlayable>();
            player.TakeDamage(AttackDamage());
        }
    }

    private void EnemyRevealed(Collider other)
    {
        //if light source then reveal themselves
        if (other.gameObject.tag == "Light")
        {
            enemyController.RevealEnemy();
        }

        var player = other.gameObject.GetComponentInParent<ISlayable>();
        if (player == null) return;
        enemyController.StartMoving(player);
    }

    private void OnTriggerExit(Collider other)
    {
        enemyController.HideEnemy();
    }
    public float AttackDamage()
    {
        //TODO: returns stats instead
        return 5;
    }

    public float GetHealth()
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(float attack)
    {
        throw new System.NotImplementedException();
    }

    public Vector3 GetPosition()
    {
        throw new System.NotImplementedException();
    }
}
