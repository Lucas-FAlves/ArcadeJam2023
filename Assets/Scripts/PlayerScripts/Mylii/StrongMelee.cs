using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
public class StrongMelee : MonoBehaviour
{
    
    private BaseChar baseChar;
    private bool holdingMelee = false;

    [SerializeField] private float damage = 20f;
    [SerializeField] private float meleeAttackCooldownTime = 0f;
    [SerializeField] private float meleeAttackCooldown = 3f;

    private void OnEnable()
    {
        InputMenager.Instance.CharacterInput.Player.StrongMelee.started += OnStrongMeleePerformed;
        InputMenager.Instance.CharacterInput.Player.StrongMelee.performed += OnStrongMeleePerformed;
        InputMenager.Instance.CharacterInput.Player.StrongMelee.canceled += OnStrongMeleePerformed;
    }

    private void OnDisable()
    {
        InputMenager.Instance.CharacterInput.Player.StrongMelee.started -= OnStrongMeleePerformed;
        InputMenager.Instance.CharacterInput.Player.StrongMelee.performed -= OnStrongMeleePerformed;
        InputMenager.Instance.CharacterInput.Player.StrongMelee.canceled -= OnStrongMeleePerformed;
    }   
    
    private void Awake()
    {
        baseChar = GetComponent<BaseChar>();
    }

    public void update()
    {
        if(holdingMelee)
        {
            
        }
        else
        {
            meleeAttackCooldownTime -= Time.deltaTime;
        }
    }

    public virtual void OnStrongMeleePerformed(InputAction.CallbackContext context)
    {
        if (meleeAttackCooldownTime > 0 || baseChar.MovementScript.Stunned) return;
        if (context.started)
        {
            holdingMelee = true;
            
        }else if(context.canceled && holdingMelee){
            holdingMelee = false;
            Debug.Log("StrongMelee");
            baseChar.MeleeAttackScript.StrongMelee(damage);

            meleeAttackCooldownTime = meleeAttackCooldown;
        }
    }
    
}
