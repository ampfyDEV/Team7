using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISlayable
{
    public void TakeDamage(float attack);
    public float AttackDamage();
    public float GetHealth();
    public Vector3 GetPosition();
}
