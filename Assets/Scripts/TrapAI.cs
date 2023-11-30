using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapAI : MonoBehaviour
{
    [SerializeField] private EnemyController enemyController;
    [SerializeField] private bool inRange = false;
    [SerializeField] private float _attackBuffer;
    [SerializeField] private float _attackspeed;

    private void Awake()
    {
        enemyController = GetComponent<EnemyController>();
        if (!enemyController) Debug.LogError("missing enemy controller ref in EnemyAI.cs");
    }
    private void OnTriggerEnter(Collider other)
    {
        EnemyRevealed(other);


        if (other.gameObject.tag == "Player") inRange = true;
    }

    private void DamagePlayer(Collider other)
    {
        if (!inRange) return;
        if (other.gameObject.tag == "Player")
        {
            var player = other.GetComponent<ISlayable>();
            player.TakeDamage(AttackDamage());
            enemyController.StopMoving();
        }
    }

    private void EnemyRevealed(Collider other)
    {
        //if light source then reveal themselves
        if (other.gameObject.tag == "Light")
        {
            enemyController.RevealEnemy();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        enemyController.HideEnemy();
        if (other.gameObject.tag == "Light")
        {
            enemyController.StopMoving();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        _attackBuffer += Time.deltaTime;
        Debug.Log(other.gameObject.name);
        if (_attackBuffer > _attackspeed)
        {
            DamagePlayer(other);
            _attackBuffer = 0;
        }
    }

    public float AttackDamage()
    {
        //TODO: returns stats instead
        return 99;
    }

}
