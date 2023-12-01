using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hideable : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;

    }
    private void OnTriggerEnter(Collider other)
    {
        //if light source then reveal themselves
        if (other.gameObject.tag == "Light")
        {
            sprite.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        sprite.enabled = false;
    }

}
