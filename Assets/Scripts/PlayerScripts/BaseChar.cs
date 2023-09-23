using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(MovementScript))]
public class BaseChar : MonoBehaviour
{
    #region Variables and Properties
    //Private Variables
    private MovementScript movementScript;
    private float shootAngle;
    private bool holdingShoot = false;
    private float concentrationPercentage = 0f;
    private float rangedAttackCooldownTime = 0f;
    private float dashCooldownTime = 0f;
    private float meleeCowndownTime = 0f;
    private int meleeSequence;
    public bool attacking = false;


    //Serialized Private Variables
    [SerializeField] private LayerMask otherPlayerLayer;
    [SerializeField] private BoxCollider2D meleeHitbox;
    [SerializeField] private float damage = 1f;
    [SerializeField] private ContactFilter2D meleeHitboxFilter;
    [SerializeField] private float baseMeleeDamage = 10f;
    [SerializeField] private ParticleSystem rangedAttack;
    [SerializeField] private ParticleSystem meleeAttack;
    [SerializeField] private float initialShootAngle = 10f;
    [SerializeField] private readonly float concentrationTime = 1f;
    [SerializeField] private float rangedAttackCooldown = 1f;
    [SerializeField] private float dashCooldown = 1f;
    [SerializeField] private MovementScript otherMovementScript;
    private float attackSequenceTime = 0.2f;
    private float attackSequenceTimer = 0f;


    public float dashSpeed = 20f;
    public float meleeCorrectionDistance;
    public float meleeCorrectionSpeed;
    public float meleeCorrectionMinDistance;
    public float meleeCorrectionSpeedFallOff;
    public MovementScript OtherMovementScript => otherMovementScript;
    public float MeleeCowndownTime => meleeCowndownTime;


    //Public Variables

    //Properties
    public LayerMask OtherPlayerLayer => otherPlayerLayer;
    public bool dashing;

    #endregion

    void Awake()
    {
        movementScript = GetComponent<MovementScript>();

    }

    private void OnEnable()
    {
        if (name == "Player1")
        {
            InputMenager.Instance.CharacterInput.Player.Fire.started += OnRangedPerformed;
            InputMenager.Instance.CharacterInput.Player.Fire.performed += OnRangedPerformed;
            InputMenager.Instance.CharacterInput.Player.Fire.canceled += OnRangedPerformed;
            InputMenager.Instance.CharacterInput.Player.Dash.performed += Dash;
            InputMenager.Instance.CharacterInput.Player.Melee.performed += OnMeleePerformed;
        }
        else
        {
            InputMenager.Instance.CharacterInput.Player2.Fire.started += OnRangedPerformed;
            InputMenager.Instance.CharacterInput.Player2.Fire.performed += OnRangedPerformed;
            InputMenager.Instance.CharacterInput.Player2.Fire.canceled += OnRangedPerformed;
            InputMenager.Instance.CharacterInput.Player2.Dash.performed += Dash;
            InputMenager.Instance.CharacterInput.Player2.Melee.performed += OnMeleePerformed;
        }
    }



    private void OnDisable()
    {
        if (name == "Player1")
        {
            InputMenager.Instance.CharacterInput.Player2.Fire.started -= OnRangedPerformed;
            InputMenager.Instance.CharacterInput.Player2.Fire.performed -= OnRangedPerformed;
            InputMenager.Instance.CharacterInput.Player2.Fire.canceled -= OnRangedPerformed;
            InputMenager.Instance.CharacterInput.Player2.Dash.performed -= Dash;
            InputMenager.Instance.CharacterInput.Player2.Melee.performed -= OnMeleePerformed;
        }
        else
        {
            InputMenager.Instance.CharacterInput.Player2.Fire.started -= OnRangedPerformed;
            InputMenager.Instance.CharacterInput.Player2.Fire.performed -= OnRangedPerformed;
            InputMenager.Instance.CharacterInput.Player2.Fire.canceled -= OnRangedPerformed;
            InputMenager.Instance.CharacterInput.Player2.Dash.performed -= Dash;
            InputMenager.Instance.CharacterInput.Player2.Melee.performed -= OnMeleePerformed;
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (holdingShoot)
        {
            concentrationPercentage = Mathf.Clamp01(concentrationPercentage + Time.deltaTime / concentrationTime);
            movementScript.Concentration = concentrationPercentage;
        }
        else
        {
            rangedAttackCooldownTime -= Time.deltaTime;
        }

        if (attacking)
        {
            Collider2D[] hit = new Collider2D[1];
            if (meleeHitbox.OverlapCollider(meleeHitboxFilter, hit) > 0)
            {
                otherMovementScript.Stun(0.5f);
                otherMovementScript.Hit((otherMovementScript.transform.position - transform.position).normalized, damage);
                meleeSequence++;
                if (meleeSequence > 4)
                {
                    otherMovementScript.UnStun();
                    otherMovementScript.Hit((otherMovementScript.transform.position - transform.position).normalized * baseMeleeDamage, damage);
                    meleeSequence = 0;
                    meleeCowndownTime = 1f;
                    
                }

                movementScript.UnStun();
                
                attacking = false;
            }
        }

        if(attackSequenceTimer < 0f)
        {
            meleeSequence = 0;
        }

        attackSequenceTimer -= Time.deltaTime;
        dashCooldownTime -= Time.deltaTime;
        meleeCowndownTime -= Time.deltaTime;
    }

    #region Callbacks

    private void OnRangedPerformed(InputAction.CallbackContext ctx)
    {
        if (rangedAttackCooldownTime > 0f || movementScript.Stunned) return;
        if (ctx.started)
        {
            holdingShoot = true;
            shootAngle = initialShootAngle;
            concentrationPercentage = 0f;

            movementScript.Concentration = 0f;

        }
        else if (ctx.canceled)
        {
            holdingShoot = false;

            var ps = rangedAttack.shape;
            ps.angle = Mathf.Clamp(shootAngle - (initialShootAngle * concentrationPercentage), 0f, initialShootAngle);
            rangedAttack.Emit(1);

            rangedAttackCooldownTime = rangedAttackCooldown * Mathf.Clamp01(concentrationPercentage + 0.2f);

            concentrationPercentage = 0f;
            movementScript.Concentration = 0f;

        }

    }

    private void OnMeleePerformed(InputAction.CallbackContext context)
    {
        if (meleeCowndownTime > 0f || movementScript.Stunned) return;

        if ((movementScript.otherPlayer.position - transform.position).magnitude > meleeCorrectionMinDistance)
        {
            meleeCorrectionSpeedFallOff = Mathf.InverseLerp(meleeCorrectionMinDistance, meleeCorrectionDistance, (movementScript.otherPlayer.position - transform.position).magnitude);

            movementScript.ApplyForce((movementScript.otherPlayer.position - transform.position).normalized * meleeCorrectionSpeed * meleeCorrectionSpeedFallOff);
        }

        attacking = true;
        movementScript.Stun(0.38f, () => {attacking = false;});

    }

    private void Dash(InputAction.CallbackContext ctx)
    {
        if (movementScript.Concentration > 0.2f || movementScript.Stunned) return;
        if (dashCooldownTime > 0f) return;

        if (movementScript.Velocity.sqrMagnitude != 0)
        {
            var dash = movementScript.WalkingDirection * dashSpeed;
            // if (Physics2D.OverlapPoint(transform.position + (Vector3)dash * testdash))
            // {
            //     movementScript.ApplyForce(dash);
            //     dashCooldownTime = dashCooldown;
            // }
            movementScript.ApplyForce(dash);
            dashing = true;
        }

    }

    #endregion

}
