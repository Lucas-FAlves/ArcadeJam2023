using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class KnockBackResistence : MonoBehaviour
{

    BaseChar baseChar;
    public float knockbackResistenceTime = 0f;
    private float OldKnockbackResistence;
    [SerializeField] private float resisCooldown = 8f;
    public float resisCooldownTime = 0f;

    private void Awake()
    {
        baseChar = GetComponent<BaseChar>();

    }




    // Start is called before the first frame update
    private void OnEnable() {
        InputMenager.Instance.CharacterInput.Player.KnockbackResistence.started += OnKnockbackResistencePerformed;
        InputMenager.Instance.CharacterInput.Player.KnockbackResistence.performed += OnKnockbackResistencePerformed;
        InputMenager.Instance.CharacterInput.Player.KnockbackResistence.canceled += OnKnockbackResistencePerformed;
    }

    private void OnDisable()
    {
        InputMenager.Instance.CharacterInput.Player.KnockbackResistence.started -= OnKnockbackResistencePerformed;
        InputMenager.Instance.CharacterInput.Player.KnockbackResistence.performed -= OnKnockbackResistencePerformed;
        InputMenager.Instance.CharacterInput.Player.KnockbackResistence.canceled -= OnKnockbackResistencePerformed;
    }

    private void Update()
    {
        resisCooldownTime -= Time.deltaTime;
        knockbackResistenceTime -= Time.deltaTime;
    }

    public virtual void OnKnockbackResistencePerformed(InputAction.CallbackContext context)
    {
        if (resisCooldownTime > 0 || baseChar.MovementScript.Stunned) return;

        if (context.performed && knockbackResistenceTime <= 0)
        {
            //Fazendo a resistencia a knockback
            OldKnockbackResistence = baseChar.MovementScript.knockbackForceMultiplier;
            baseChar.MovementScript.knockbackForceMultiplier = 0f;
            knockbackResistenceTime = 3f;
        }
        else if(context.canceled)
        {
            if (knockbackResistenceTime > 0) return;
            resisCooldownTime = resisCooldown;
            baseChar.MovementScript.knockbackForceMultiplier = OldKnockbackResistence;
        }
    }
}
