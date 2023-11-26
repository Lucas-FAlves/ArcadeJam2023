using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MeleeAttackScript : MonoBehaviour
{
    private BaseChar baseChar;
    public int meleeSequence = 0;
    public bool attacking = false;


    //Serialized Private Variables
    [SerializeField] private BoxCollider2D meleeHitbox;
    [SerializeField] private ContactFilter2D meleeHitboxFilter;
    [SerializeField] private float baseMeleeDamage = 10f;
    [SerializeField] private float baseMeleeDamageMult = 1.5f;
    [SerializeField] private MovementScript otherMovementScript;
    [SerializeField] private float attackSequenceTime = 0.2f;
    [SerializeField] private float attackSequenceTimer = 0f;


    public float dashSpeed = 20f;
    public float meleeCorrectionDistance;
    public float meleeCorrectionSpeed;
    public float meleeCorrectionMinDistance;
    public float meleeCorrectionSpeedFallOff;
    public MovementScript OtherMovementScript => otherMovementScript;

    private void Awake()
    {
        baseChar = GetComponentInChildren<BaseChar>();
        if(name == "Player1")
        {
            otherMovementScript = GameObject.Find("Player2").GetComponent<MovementScript>();
        }
        else
        {
            otherMovementScript = GameObject.Find("Player1").GetComponent<MovementScript>();
        }
    }

   private void OnEnable()
    {
        if (name == "Player1")
        {
            InputMenager.Instance.CharacterInput.Player.Melee.performed += OnMeleePerformed;
        }
        else
        {
            InputMenager.Instance.CharacterInput.Player2.Melee.performed += OnMeleePerformed;
        }
    }



    private void OnDisable()
    {
        if (name == "Player1")
        {
            InputMenager.Instance.CharacterInput.Player2.Melee.performed -= OnMeleePerformed;
        }
        else
        {
            InputMenager.Instance.CharacterInput.Player2.Melee.performed -= OnMeleePerformed;
        }
    }

    private void Update()
    {
        if(attacking)
        {
            Collider2D[] hit = new Collider2D[1];
            if (meleeHitbox.OverlapCollider(meleeHitboxFilter, hit) > 0)
            {
                otherMovementScript.Hit((otherMovementScript.transform.position - transform.position).normalized, baseMeleeDamage, 0.3f);
                meleeSequence++;
                if (meleeSequence > 4)
                {
                    otherMovementScript.UnStun();
                    otherMovementScript.Hit((otherMovementScript.transform.position - transform.position).normalized * baseMeleeDamage * baseMeleeDamageMult, baseMeleeDamage, 0.5f);
                    meleeSequence = 0;
                    
                }

                baseChar.MovementScript.UnStun();
                baseMeleeDamage = 10f;
                attacking = false;
            }
        }

        if(attackSequenceTimer > 0f)
        {
            attackSequenceTimer -= Time.deltaTime;
        }
        else
        {
            meleeSequence = 0;
        }

    }

    #region Callbacks

    public virtual void OnMeleePerformed(InputAction.CallbackContext context)
    {
        if(baseChar.MovementScript.Stunned) return;

        attacking = true;
        baseChar.MovementScript.Stun(0.3f);
        if ((baseChar.MovementScript.otherPlayer.position - transform.position).magnitude > meleeCorrectionMinDistance)
        {
            meleeCorrectionSpeedFallOff = Mathf.InverseLerp(meleeCorrectionMinDistance, meleeCorrectionDistance, (baseChar.MovementScript.otherPlayer.position - transform.position).magnitude);

            baseChar.MovementScript.ApplyForce((baseChar.MovementScript.otherPlayer.position - transform.position).normalized * meleeCorrectionSpeed * meleeCorrectionSpeedFallOff);
        }

        attackSequenceTimer = attackSequenceTime;

    }

    #endregion

    //fazendo o StrongMeleeAttack
    public void StrongMelee(float damage){
        if(baseChar.MovementScript.Stunned) return;

        attacking = true;
        baseChar.MovementScript.Stun(0.3f);
        if ((baseChar.MovementScript.otherPlayer.position - transform.position).magnitude > meleeCorrectionMinDistance)
        {
            meleeCorrectionSpeedFallOff = Mathf.InverseLerp(meleeCorrectionMinDistance, meleeCorrectionDistance, (baseChar.MovementScript.otherPlayer.position - transform.position).magnitude);

            baseChar.MovementScript.ApplyForce((baseChar.MovementScript.otherPlayer.position - transform.position).normalized * meleeCorrectionSpeed * meleeCorrectionSpeedFallOff);
        }
        baseMeleeDamage = damage;
        attackSequenceTimer = attackSequenceTime;
    }
}
