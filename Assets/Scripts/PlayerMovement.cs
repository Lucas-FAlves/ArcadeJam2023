using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Vector2 _movementInput;
    private Vector2 _smoothenedMovimentInput, _smootheningVelocity;
    private PlayerControls input;

    public float speed = 4.5f;

    private void Awake()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        input = new PlayerControls();
    }

    private void Anda(InputAction.CallbackContext ctx)
    {   
        _movementInput = ctx.ReadValue<Vector2>();
        _smoothenedMovimentInput = Vector2.SmoothDamp(_smoothenedMovimentInput, _movementInput, ref _smootheningVelocity, 0.1f);
        _rigidbody.velocity = _smoothenedMovimentInput * speed;
    }

    private void OnEnable() 
    {
        input.Enable();
        input.Player.Move.performed += Anda;
        input.Player.Move.canceled += Anda;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.Move.canceled -= Anda;
        input.Player.Move.performed -= Anda;
    }
}
