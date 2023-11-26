using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LougaanUltimate : MonoBehaviour
{
    private BaseChar baseChar;
    private Vector2 _shootDirection;

    [SerializeField] private float _shootSpeed;
    [SerializeField] private Transform _arrowFiringPoint;
    [SerializeField] private UltimateArrow _ultArrowPrefab;
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float stunTime;
    [SerializeField] private float initialShootAngle = 10f;
    [SerializeField] private float bigArrowCooldown = 5f;
    public float bigArrowCooldownTime = 0f;

    private void OnEnable()
    {
        InputMenager.Instance.CharacterInput.Player2.Ultimate.started += OnUltimatePerformed;
        InputMenager.Instance.CharacterInput.Player2.Ultimate.performed += OnUltimatePerformed;
        InputMenager.Instance.CharacterInput.Player2.Ultimate.canceled += OnUltimatePerformed;
    }

    private void OnDisable()
    {
        InputMenager.Instance.CharacterInput.Player2.Ultimate.started -= OnUltimatePerformed;
        InputMenager.Instance.CharacterInput.Player2.Ultimate.performed -= OnUltimatePerformed;
        InputMenager.Instance.CharacterInput.Player2.Ultimate.canceled -= OnUltimatePerformed;
    }

    private void Awake()
    {

    }


    void Update()
    {

    }


    public virtual void OnUltimatePerformed(InputAction.CallbackContext context)
    {

    }
}
