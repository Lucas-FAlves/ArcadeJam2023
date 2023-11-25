using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BigArrowScript : MonoBehaviour
{
    private BaseChar baseChar;
    private float shootAngle;
    private float bigArrowCooldownTime = 0f;
    private bool teste;
    //private BigArrowCollisionController bigArrowCollisionController;

    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float stunTime;
    [SerializeField] private float initialShootAngle = 10f;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private float bigArrowCooldown = 5f;

    private void OnEnable()
    {
        InputMenager.Instance.CharacterInput.Player2.BigArrow.started += OnArrowPerformed;
        InputMenager.Instance.CharacterInput.Player2.BigArrow.performed += OnArrowPerformed;
        InputMenager.Instance.CharacterInput.Player2.BigArrow.canceled += OnArrowPerformed;
    }

    private void OnDisable()
    {
        InputMenager.Instance.CharacterInput.Player2.BigArrow.started -= OnArrowPerformed;
        InputMenager.Instance.CharacterInput.Player2.BigArrow.started -= OnArrowPerformed;
        InputMenager.Instance.CharacterInput.Player2.BigArrow.started -= OnArrowPerformed;
    }
    
    private void Awake()
    {
        baseChar = GetComponent<BaseChar>();   

    }

    void Update()
    {
        if (teste)
        {

        }
        else
        {
            bigArrowCooldownTime = Time.deltaTime;
        }
    }


    public virtual void OnArrowPerformed(InputAction.CallbackContext context)
    {
        if (bigArrowCooldownTime > 0 || baseChar.MovementScript.Stunned) return;
        if(context.started)
        {

            //Shoot(shootAngle - (initialShootAngle),damage, stunTime);
        }
    }
}
