using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    /*
    1. disable sprite renderer & eyes sprite renderer on start
    3. if collided turn on sprite renderer and eyes renderer bool
    4. if out then turn off just sprite
    5. walk to player destination
    */

    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private SpriteRenderer face_sprite;
    [SerializeField] private ISlayable target;
    [SerializeField] private bool isMoving;
    [SerializeField] private float _speed;

    private void Start()
    {
        sprite.enabled = false;
        face_sprite.enabled = false;
        isMoving = false;
    }

    private void Update()
    {
        Move();
    }

    public void RevealEnemy()
    {
        sprite.enabled = true;
        face_sprite.enabled = true;
    }

    public void HideEnemy()
    {
        sprite.enabled = false;
    }

    public void StartMoving(ISlayable player)
    {
        if (target == null) target = player;
        isMoving = true;

    }

    public void StopMoving()
    {
        isMoving = false;
    }

    private void Move()
    {
        if (!isMoving) return;
        var targetPos = new Vector3(target.GetPosition().x, transform.position.y, target.GetPosition().z);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, _speed * Time.deltaTime);
    }

}
