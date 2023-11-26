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
    private MeleeAttackScript meleeAttackScript;
    private RangedAttackScript rangedAttackScript;
    private MovementScript otherMovementScript;
    private float dashCooldownTime = 0f;


    //Serialized Private Variables
    [SerializeField] private LayerMask otherPlayerLayer;
    

    public float dashSpeed = 20f;



    //Public Variables

    //Properties
    public MovementScript MovementScript => movementScript;
    public MovementScript OtherMovementScript => otherMovementScript;
    public MeleeAttackScript MeleeAttackScript => meleeAttackScript;
    public RangedAttackScript RangedAttackScript => rangedAttackScript;
    public LayerMask OtherPlayerLayer => otherPlayerLayer;
    public bool dashing;

    #endregion
    

    void Awake()
    {
        movementScript = GetComponent<MovementScript>();
        meleeAttackScript = GetComponent<MeleeAttackScript>();
        rangedAttackScript = GetComponent<RangedAttackScript>();

        if(name == "Player1")
        {
            otherMovementScript = GameObject.Find("Player2").GetComponent<MovementScript>();
             
        }
        else
        {
            otherMovementScript = GameObject.Find("Player1").GetComponent<MovementScript>();
        }
        GameMenagers.Instance.OnGameOver += GameOver;
    }

    private void GameOver()
    {
        // MovementScript.enabled = false;
        // MeleeAttackScript.enabled = false;
        // RangedAttackScript.enabled = false;
    }

    private void OnEnable()
    {
        if (name == "Player1")
        {
            InputMenager.Instance.CharacterInput.Player.Dash.performed += Dash;
        }
        else
        {
            InputMenager.Instance.CharacterInput.Player2.Dash.performed += Dash;
        }
    }



    private void OnDisable()
    {
        if (name == "Player1")
        {
            InputMenager.Instance.CharacterInput.Player2.Dash.performed -= Dash;
        }
        else
        {
            InputMenager.Instance.CharacterInput.Player2.Dash.performed -= Dash;
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        dashCooldownTime -= Time.deltaTime;

    }

    #region Callbacks



    public void Dash(InputAction.CallbackContext ctx)
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
