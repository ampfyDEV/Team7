using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightStatueController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("level Complete");
        }
    }
}
