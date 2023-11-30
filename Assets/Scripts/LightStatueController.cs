using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightStatueController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private int AnimationTime = 2;
    [SerializeField] private int level;
    [SerializeField] private bool hasAlreadyCompleted = false;
    [SerializeField] private SpriteRenderer sprite;

    private void Start()
    {
        sprite.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        RevealStatue(other);
        if (other.gameObject.tag == "Player" && hasAlreadyCompleted == false)
        {
            animator.SetTrigger("LevelComplete");
            var player = other.gameObject.GetComponentInChildren<ISlayable>();
            player.RestoreHealth(player.GetHealth());
            GameManager.Instance.onLevelComplete?.Invoke(true);
            GameManager.Instance.updateLevel(level);
            hasAlreadyCompleted = true;
            StartCoroutine(WaitForAnimationToFinish());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        sprite.enabled = false;
    }

    private void RevealStatue(Collider other)
    {
        //if light source then reveal themselves
        if (other.gameObject.tag == "Light")
        {
            sprite.enabled = true;
        }


    }

    private IEnumerator WaitForAnimationToFinish()
    {
        yield return new WaitForSeconds(AnimationTime);
        GameManager.Instance.onLevelComplete?.Invoke(false);
    }
}
