using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animacao : MonoBehaviour
{
    public Animator animator;
    public BaseChar baseChar;
    public MovementScript movementScript;
    //public Transform target; // O alvo que o sprite deve sempre "olhar", geralmente o ponto de origem do jogador.
    float horizontalInput;
    float verticalInput;
    Vector2 movementDirection;
    // Start is called before the first frame update
    void Start()
    {
        Animator animator = GetComponent<Animator>();
        MovementScript movementScript = GetComponent<MovementScript>();
        BaseChar baseChar = GetComponent<BaseChar>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rotatoin = transform.rotation.eulerAngles;
        rotatoin.z = 0f;
        transform.rotation = Quaternion.Euler(rotatoin);
        
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        movementDirection = new Vector2(horizontalInput, verticalInput).normalized;
        Debug.Log(movementDirection);
        //animator.SetTrigger("frente");

        if (movementDirection.sqrMagnitude > 0)
        {
            animator.SetFloat("Horizontal", movementDirection.x);
            if(movementDirection.x > 0)
            {
                animator.SetBool("Right", true);
            }else{
                animator.SetBool("Right", false);
            }
            animator.SetFloat("Vertical", movementDirection.y);
        }
        else
        {
            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Vertical", 0);
        }

        

/*
        if (baseChar.dashing)
        {
            animator.SetTrigger("dash");
        }*/
    }
}

