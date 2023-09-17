using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementScript : MonoBehaviour
{
    #region Variables and Properties
    //Private Variables
    private Vector2 _velocity;
    private Vector2 _movement;
    private Vector2 _movementInput;
    private bool _stunned = false;
    private bool _slowed = false;
    private BaseChar _baseChar;

    //Serialized Private Variables
    [SerializeField] private float maxSpeed = 4.5f;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float deceleration = 10f;
    //[SerializeField] private LayerMask floorLayer;


    //Public Variables

    //Properties
    public Vector2 Velocity { get => _velocity; }
    public Vector2 WalkingDirection { get; private set; }
    public Vector2 FacingDirection { get; private set; }
    public float Concentration { get; set; }
    public float knockbackForceMultiplier = 1f;

    public Transform otherPlayer;
    public LayerMask wall;
    public LayerMask floor;

    #endregion

    void OnEnable()
    {
        if (name == "Player1")
        {
            InputMenager.Instance.CharacterInput.Player.Move.performed += OnMovePerformed;
            InputMenager.Instance.CharacterInput.Player.Move.canceled += OnMovePerformed;
        }
        else
        {
            InputMenager.Instance.CharacterInput.Player2.Move.performed += OnMovePerformed;
            InputMenager.Instance.CharacterInput.Player2.Move.canceled += OnMovePerformed;
        }
    }

    void OnDisable()
    {
        if (name == "Player2")
        {
            InputMenager.Instance.CharacterInput.Player.Move.performed -= OnMovePerformed;
            InputMenager.Instance.CharacterInput.Player.Move.canceled -= OnMovePerformed;
        }
        else
        {
            InputMenager.Instance.CharacterInput.Player2.Move.performed -= OnMovePerformed;
            InputMenager.Instance.CharacterInput.Player2.Move.canceled -= OnMovePerformed;
        }
    }

    private void Awake()
    {
        Concentration = 0f;
        _baseChar = GetComponent<BaseChar>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!Physics2D.OverlapCircle(transform.position, 0.3f, floor))
        {
            _movement = _velocity * Time.deltaTime * (1.2f - Concentration);
            transform.position += (Vector3)_movement;
            Debug.Log(_movement);
            return;
        }
        //calculate velocity
        CalculateVelocity();

        
        _movement = _velocity * Time.deltaTime * (1.2f - Concentration);
        

        //move the player
//        if(name == "Player2") Debug.Log(_slowed);
        transform.position += (Vector3)_movement;
        if (_movementInput.sqrMagnitude > 0)
        {
            WalkingDirection = _movement.normalized;
        }

        if ((Vector2)(otherPlayer.transform.position - transform.position) != FacingDirection)
        {
            FacingDirection = otherPlayer.transform.position - transform.position;
            transform.up = FacingDirection;
        }
        
        if (Physics2D.OverlapCircle(transform.position, 0.5f, _baseChar.OtherPlayerLayer))
        {
            PullPlayer(true);
        }
        int i = 0;
        if (_baseChar.dashing)
            if (Physics2D.OverlapCircle(transform.position, 0.5f, wall))
            {
                bool t;
                do
                {
                    _velocity = Vector2.zero;
                    i++;
                    if (i > 100) break;
                    t = Physics2D.OverlapCircle(transform.position, 0.5f, wall);
                } while (t);
            }

    }

    public void PullPlayer(bool first)
    {
        int i = 0;
        bool t;
            do
            {
                transform.position += -(Vector3)FacingDirection * 0.001f;
                //_velocity = Vector2.zero;
                if(first)
                    _baseChar.OtherMovementScript.PullPlayer(false);
                i++;
                if (i > 100) break;
                t = Physics2D.OverlapCircle(transform.position, 0.5f, _baseChar.OtherPlayerLayer);
            } while (t);
    }

    private void CalculateVelocity()
    {
        //Check if the player is moving
        if (_movementInput.sqrMagnitude > 0)
        {
            //calculate new velocity
            var newSpeed = Vector2.MoveTowards(_velocity, _movementInput * maxSpeed, _slowed ? acceleration / 5 : acceleration * Time.deltaTime);
            //check if the new velocity not exceed the max speed and if the player is not trying to move in the opposite direction
            if (Vector2.Dot(_velocity, _movementInput) > 0)
            {
                if (newSpeed.sqrMagnitude < maxSpeed * maxSpeed)
                {
                    _velocity = newSpeed;
                }
            }
            else
            {
                _velocity += (newSpeed - _velocity) / 2;
            }
        }

        //apply drag
        //_velocity = Vector2.Lerp(_velocity, Vector2.zero, deceleration * Time.deltaTime);
        _velocity = Vector2.MoveTowards(_velocity, Vector2.zero, deceleration * Time.deltaTime);

        if (_baseChar != null && _velocity.sqrMagnitude <= maxSpeed * maxSpeed)
        {
            if (_baseChar.dashing) _baseChar.dashing = false;
        }
    }

    public void ApplyForce(Vector2 force, bool hit = false)
    {
        //calculate velocity based on distance to move the player distance units in the time it takes to decelerate to 0
        if (hit)
            _velocity = force * knockbackForceMultiplier;
        else
            _velocity = force;
    }

    public void Hit(Vector2 force, float damage)
    {
        knockbackForceMultiplier += damage / 100;
        ApplyForce(force, true);
    }

    public void Stun(float time)
    {
        if (!_stunned)
        {
            StartCoroutine(StunCoroutine(time));
        }
    }

    public void Slow(float time)
    {
        if (!_slowed)
        {
            StartCoroutine(SlowCoroutine(time));
        }
    }

    private IEnumerator StunCoroutine(float time)
    {
        _stunned = true;
        yield return new WaitForSeconds(time);
        _stunned = false;
    }

    private IEnumerator SlowCoroutine(float time)
    {
        _slowed = true;
        yield return new WaitForSeconds(time);
        _slowed = false;
    }

    #region Callbacks

    private void OnMovePerformed(InputAction.CallbackContext ctx)
    {
        //get the movement input
        if (!_stunned)
            _movementInput = ctx.ReadValue<Vector2>();
    }


    #endregion
}
