using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float _speed;
    [SerializeField] private Vector2 _movement;
    [SerializeField] private Vector2 _rotate;
    [SerializeField] private float initial_fov;

    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Light _light;
    [SerializeField] private float light_speed;
    [SerializeField] private bool _lighting_up;
    [SerializeField] private float _light_per_second;

    private void Start()
    {
        initial_fov = _light.spotAngle;
    }
    public void onMove(InputAction.CallbackContext context)
    {
        _movement = context.ReadValue<Vector2>();
    }

    private void Move()
    {
        if (_movement == Vector2.zero) return;
        Vector3 playerMove;
        switch (transform.localEulerAngles.y)
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


        _rb.MovePosition(transform.position + playerMove * _movement.magnitude * _speed * Time.deltaTime);
    }

    private void Update()
    {
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
        if (!_lighting_up) _light.spotAngle = initial_fov;
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
        if (!_lighting_up) return;
        _light.spotAngle += _light_per_second * Time.deltaTime;
    }
}
