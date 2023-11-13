using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float _speed;
    [SerializeField] private Vector2 _movement;
    [SerializeField] private Vector2 _rotate;
    [SerializeField] private float orientation;

    [SerializeField] private Rigidbody _rb;

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
    }

    public void onRotate(InputAction.CallbackContext context)
    {
        _rotate = context.ReadValue<Vector2>();
    }

    private void RotatePlayer()
    {
        if (_rotate == Vector2.zero) return;
        var rotation = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + (90 * _rotate.x), transform.eulerAngles.z);
        transform.eulerAngles = rotation;

        //hacky way to ensure update only happens once
        _rotate = Vector2.zero;
    }
}
