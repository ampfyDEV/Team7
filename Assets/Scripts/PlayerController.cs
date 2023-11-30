using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float _speed;
    [SerializeField] private Vector2 _movement;
    [SerializeField] private Vector2 _rotate;
    [SerializeField] private float initial_fov;
    [SerializeField] private float initial_radius;

    [SerializeField] private Rigidbody _rb;
    [SerializeField] private SphereCollider _radius;
    [SerializeField] private Light _light;
    [SerializeField] private bool _lighting_up;
    [SerializeField] private float _light_per_second;
    [SerializeField] private float _radius_per_second;

    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject sprite;

    [SerializeField] private float lightRate;
    [SerializeField] private float lightDmg;
    [SerializeField] private float lightTimeDecay;
    [SerializeField] private PlayerCombat playerCombat;
    [SerializeField] private bool isAttacking = false;
    [SerializeField] private bool _attackCD;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private bool animationPlaying = false;

    private void OnEnable()
    {
        GameManager.Instance.onLevelComplete += onLevelComplete_playerLevelComplete;
    }

    private void OnDisable()
    {
        GameManager.Instance.onLevelComplete -= onLevelComplete_playerLevelComplete;
    }
    private void Start()
    {
        initial_fov = _light.spotAngle;
        initial_radius = _radius.radius;
    }
    public void onMove(InputAction.CallbackContext context)
    {
        _movement = context.ReadValue<Vector2>();
    }

    private void Move()
    {
        if (_movement == Vector2.zero) _animator.SetFloat("speed", -1);
        if (_movement == Vector2.zero) return;
        else _animator.SetFloat("speed", 1);

        Vector3 playerMove;
        playerMove = GetPlayerMove();

        RotateSprite();
        _rb.MovePosition(transform.position + playerMove * _movement.magnitude * _speed * Time.deltaTime);
    }

    private void RotateSprite()
    {
        var y_offset = transform.eulerAngles.y;
        if (_movement.x < 0 || _movement.y < 0)
        {
            var rotation = new Vector3(sprite.transform.eulerAngles.x, y_offset + 180, sprite.transform.eulerAngles.z);
            sprite.transform.eulerAngles = rotation;
        }
        else
        {
            var rotation = new Vector3(sprite.transform.eulerAngles.x, y_offset + 0, sprite.transform.eulerAngles.z);
            sprite.transform.eulerAngles = rotation;
        }
    }

    private Vector3 GetPlayerMove()
    {
        Vector3 playerMove;
        switch (Mathf.Abs(transform.localEulerAngles.y))
        {
            //looking right
            case 90:
                playerMove = new Vector3(_movement.y, 0, -_movement.x);
                break;

            //looking behind
            case 180:
                playerMove = new Vector3(-_movement.x, 0, -_movement.y);
                break;

            //looking left
            case 270:
                playerMove = new Vector3(-_movement.y, 0, _movement.x);
                break;

            //looking forward
            default:
                playerMove = new Vector3(_movement.x, 0, _movement.y);
                break;
        }

        return playerMove;
    }

    private void Update()
    {
        if (animationPlaying) return;
        Move();
        RotatePlayer();
        LightUpEnv();
    }

    public void onRotate(InputAction.CallbackContext context)
    {
        _rotate = context.ReadValue<Vector2>();
    }

    public void onLightUp(InputAction.CallbackContext context)
    {
        _lighting_up = context.performed;
        if (!_lighting_up)
        {
            _light.spotAngle = initial_fov;
            _radius.radius = initial_radius;
        }
    }

    public void onAttack(InputAction.CallbackContext context)
    {
        if (isAttacking == true || _attackCD == true) return;
        if (context.action.triggered)
        {
            isAttacking = true;
            _attackCD = true;

            sprite.transform.localPosition = new Vector3(0, 1, 0);


            _animator.SetBool("isAttacking", isAttacking);
            _animator.SetBool("attackCD", _attackCD);
            StartCoroutine(FinishAttacking());
            StartCoroutine(AttackOnCD());
            playerCombat.AttackEnemy();

        }
    }

    private IEnumerator FinishAttacking()
    {
        yield return new WaitForSeconds(0.08f);
        isAttacking = false;
        _animator.SetBool("isAttacking", isAttacking);
        sprite.transform.localPosition = Vector3.zero;
    }

    private IEnumerator AttackOnCD()
    {
        yield return new WaitForSeconds(_attackSpeed);
        _attackCD = false;
        _animator.SetBool("attackCD", _attackCD);
    }

    private void RotatePlayer()
    {
        if (_rotate == Vector2.zero) return;
        var rotation = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + (90 * _rotate.x), transform.eulerAngles.z);
        transform.eulerAngles = rotation;

        //hacky way to ensure update only happens once
        _rotate = Vector2.zero;
    }
    private void LightUpEnv()
    {
        if (!_lighting_up)
        {
            lightRate = 0;
        }
        else
        {
            _light.spotAngle += _light_per_second * Time.deltaTime;
            _radius.radius += _radius_per_second * Time.deltaTime;
            lightRate += Time.deltaTime;
            if (lightRate > lightTimeDecay)
            {
                playerCombat.TakeDamage(lightDmg);
                lightRate = 0;
            }
        }

    }

    private void onLevelComplete_playerLevelComplete(bool setLevel)
    {
        if (setLevel)
        {
            _animator.SetTrigger("levelComplete");
        }
        animationPlaying = setLevel;
    }
}
