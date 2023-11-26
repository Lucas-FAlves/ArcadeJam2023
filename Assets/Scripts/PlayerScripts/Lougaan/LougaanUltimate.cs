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
    [SerializeField] private float ultArrowCooldown = 5f;
    public float ultArrowCooldownTime = 0f;

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
        baseChar = GetComponent<BaseChar>();
    }


    void Update()
    {
        ultArrowCooldownTime -= Time.deltaTime;
    }


    public virtual void OnUltimatePerformed(InputAction.CallbackContext context)
    {
        if (ultArrowCooldownTime > 0 || baseChar.MovementScript.Stunned) return;
        if (context.performed)
        {
            Debug.Log("Disparou flecha");
            Quaternion rotation = Quaternion.Euler(0f, 0f, 90f);
            UltimateArrow newArrow = Instantiate(_ultArrowPrefab, _arrowFiringPoint.position, rotation);
            _shootDirection = baseChar.MovementScript.FacingDirection;
            newArrow.Shoot(_shootDirection, damage, stunTime);
            //Shoot(shootAngle - (initialShootAngle),damage, stunTime);
        }
        else if (context.canceled)
        {
            ultArrowCooldownTime = ultArrowCooldown;
        }
    }
}
