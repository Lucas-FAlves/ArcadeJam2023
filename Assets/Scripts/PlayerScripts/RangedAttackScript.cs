using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RangedAttackScript : MonoBehaviour
{
    private BaseChar baseChar;
    private ParticleCollisionController particleCollisionController;
    private float shootAngle;
    private bool holdingShoot = false;
    private float concentrationPercentage = 0f;
    private float rangedAttackCooldownTime = 0f;
    private float dashCooldownTime = 0f;
    private float meleeCowndownTime = 0f;


    //Serialized Private Variables
    [SerializeField] private float timeToMaxConcentration = 1f;
    [SerializeField] private float damage = 1f;
    [SerializeField] private float initialShootAngle = 10f;
    [SerializeField] private readonly float concentrationTime = 1f;
    [SerializeField] private float rangedAttackCooldown = 1f;

    public float ConcentrationPercentage => concentrationPercentage;

   private void OnEnable()
    {
        if (name == "Player1")
        {
            InputMenager.Instance.CharacterInput.Player.Fire.started += OnRangedPerformed;
            InputMenager.Instance.CharacterInput.Player.Fire.performed += OnRangedPerformed;
            InputMenager.Instance.CharacterInput.Player.Fire.canceled += OnRangedPerformed;
        }
        else
        {
            InputMenager.Instance.CharacterInput.Player2.Fire.started += OnRangedPerformed;
            InputMenager.Instance.CharacterInput.Player2.Fire.performed += OnRangedPerformed;
            InputMenager.Instance.CharacterInput.Player2.Fire.canceled += OnRangedPerformed;
        }
    }



    private void OnDisable()
    {
        if (name == "Player1")
        {
            InputMenager.Instance.CharacterInput.Player.Fire.started -= OnRangedPerformed;
            InputMenager.Instance.CharacterInput.Player.Fire.performed -= OnRangedPerformed;
            InputMenager.Instance.CharacterInput.Player.Fire.canceled -= OnRangedPerformed;
        }
        else
        {
            InputMenager.Instance.CharacterInput.Player2.Fire.started -= OnRangedPerformed;
            InputMenager.Instance.CharacterInput.Player2.Fire.performed -= OnRangedPerformed;
            InputMenager.Instance.CharacterInput.Player2.Fire.canceled -= OnRangedPerformed;
        }
    }

    private void Awake()
    {
        baseChar = GetComponent<BaseChar>();
        particleCollisionController = GetComponentInChildren<ParticleCollisionController>();
    }

    private void Update()
    {

        if(holdingShoot)
        {
            concentrationPercentage = Mathf.Clamp01(concentrationPercentage + (Time.deltaTime / concentrationTime / timeToMaxConcentration));

        }
        else
        {
            rangedAttackCooldownTime -= Time.deltaTime;
        }
    }

    #region Callbacks

    public virtual void OnRangedPerformed(InputAction.CallbackContext context)
    {
        if (rangedAttackCooldownTime > 0f || baseChar.MovementScript.Stunned) return;
        if(context.started)
        {
            holdingShoot = true;
            shootAngle = initialShootAngle;
            concentrationPercentage = 0f;

        }
        else if(context.canceled && holdingShoot && particleCollisionController != null)
        {
            holdingShoot = false;

            particleCollisionController.Shoot(shootAngle - (initialShootAngle * concentrationPercentage), Mathf.Clamp(damage * concentrationPercentage, 0.33f, damage), 0.38f);

            rangedAttackCooldownTime = rangedAttackCooldown * Mathf.Clamp01(concentrationPercentage + 0.2f);

            concentrationPercentage = 0f;
        }
    }

    #endregion
    // if (holdingShoot)
    //     {
    //         concentrationPercentage = Mathf.Clamp01(concentrationPercentage + Time.deltaTime / concentrationTime);
    //         movementScript.Concentration = concentrationPercentage;
    //     }
    //     else
    //     {
    //         rangedAttackCooldownTime -= Time.deltaTime;
    //     }

    // if (rangedAttackCooldownTime > 0f || movementScript.Stunned) return;
    //     if (ctx.started)
    //     {
    //         holdingShoot = true;
    //         shootAngle = initialShootAngle;
    //         concentrationPercentage = 0f;

    //         movementScript.Concentration = 0f;

    //     }
    //     else if (ctx.canceled)
    //     {
    //         holdingShoot = false;

    //         var ps = rangedAttack.shape;
    //         ps.angle = Mathf.Clamp(shootAngle - (initialShootAngle * concentrationPercentage), 0f, initialShootAngle);
    //         rangedAttack.Emit(1);

    //         rangedAttackCooldownTime = rangedAttackCooldown * Mathf.Clamp01(concentrationPercentage + 0.2f);

    //         concentrationPercentage = 0f;
    //         movementScript.Concentration = 0f;

    //     }
}
