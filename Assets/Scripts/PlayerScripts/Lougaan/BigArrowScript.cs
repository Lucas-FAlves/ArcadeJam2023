using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BigArrowScript : MonoBehaviour
{
    private BaseChar baseChar;
    private BigArrow bigArrow;
    private float shootAngle;
    private bool teste;
    private Vector2 _shootDirection;

    [SerializeField] private Transform _arrowFiringPoint;
    [SerializeField] private BigArrow _bigArrowPrefab;
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float stunTime;
    [SerializeField] private float initialShootAngle = 10f;
    [SerializeField] private float bigArrowCooldown = 5f;
    public float bigArrowCooldownTime = 0f;


    private void OnEnable()
    {
        InputMenager.Instance.CharacterInput.Player2.BigArrow.started += OnArrowPerformed;
        InputMenager.Instance.CharacterInput.Player2.BigArrow.performed += OnArrowPerformed;
        InputMenager.Instance.CharacterInput.Player2.BigArrow.canceled += OnArrowPerformed;
    }

    private void OnDisable()
    {
        InputMenager.Instance.CharacterInput.Player2.BigArrow.started -= OnArrowPerformed;
        InputMenager.Instance.CharacterInput.Player2.BigArrow.performed -= OnArrowPerformed;
        InputMenager.Instance.CharacterInput.Player2.BigArrow.canceled -= OnArrowPerformed;
    }
    
    private void Awake()
    {
        baseChar = GetComponent<BaseChar>();   

    }

    void Update()
    {
        bigArrowCooldownTime -= Time.deltaTime;
        //if (teste)
        //{

        //}
        //else
        //{
        //}
    }


    public virtual void OnArrowPerformed(InputAction.CallbackContext context)
    {
        if (bigArrowCooldownTime > 0 || baseChar.MovementScript.Stunned) return;
        if(context.performed)
        {
            Debug.Log("Disparou flecha");
            Quaternion rotation = Quaternion.Euler(0f,0f,90f);
            BigArrow newArrow = Instantiate(_bigArrowPrefab, _arrowFiringPoint.position, rotation);
            _shootDirection = baseChar.MovementScript.FacingDirection;
            newArrow.Shoot(_shootDirection, damage, stunTime);
            //Shoot(shootAngle - (initialShootAngle),damage, stunTime);
        }
        else if (context.canceled)
        {

        }
    }
}
