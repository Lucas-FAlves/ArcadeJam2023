using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Vector2 _movementInput;
    private Vector2 _smoothenedMovimentInput, _smootheningVelocity;

    public float speed = 4.5f;

    private void Awake()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _smoothenedMovimentInput = Vector2.SmoothDamp(_smoothenedMovimentInput, _movementInput, ref _smootheningVelocity, 0.1f);
        _rigidbody.velocity = _smoothenedMovimentInput * speed;
    }

    private void OnMove(InputValue inputvalue)
    {
        _movementInput = inputvalue.Get<Vector2>();
    }
}
