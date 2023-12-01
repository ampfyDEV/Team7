using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDetection : MonoBehaviour
{
    [SerializeField] private ISlayable target;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Enemy" || other.gameObject == null) return;
        target = other.gameObject.GetComponent<ISlayable>();
        Debug.Log(target);
    }

    private void OnTriggerExit(Collider other)
    {

        target = null;
    }

    public ISlayable getTarget()
    {
        return target;
    }

    public void EnemyDestroyed()
    {
        target = null;
    }
}
