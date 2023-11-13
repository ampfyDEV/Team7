using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IsometricPlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Vector2 _movement;


    public void onMove(InputAction.CallbackContext context)
    {
        _movement = context.ReadValue<Vector2>();
    }


    private void Move()
    {
        if (_movement == Vector2.zero) return;
        var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));

        Vector3 playerMove = new Vector3(_movement.x, 0, _movement.y);
        var m_offset = matrix.MultiplyPoint3x4(playerMove);
        var r_pos = transform.position + m_offset - transform.position;
        transform.Translate(r_pos * _speed * Time.deltaTime, Space.World);
    }

    private void Look()
    {
        if (_movement == Vector2.zero) return;

        var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));

        Vector3 playerMove = new Vector3(_movement.x, 0, _movement.y);
        var m_offset = matrix.MultiplyPoint3x4(playerMove);

        var r_pos = transform.position + m_offset - transform.position;
        var rotation = Quaternion.LookRotation(r_pos, Vector3.up);
        transform.rotation = rotation;
    }

    private void Update()
    {
        Move();
        Look();

    }
}
