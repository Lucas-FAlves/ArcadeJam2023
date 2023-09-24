using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacaoMylii : MonoBehaviour
{
    public Animator animator;
    public BaseChar baseChar;
    public MovementScript movementScript;
    //public Transform target; // O alvo que o sprite deve sempre "olhar", geralmente o ponto de origem do jogador.
    float horizontalInput;
    // Obtém o componente SpriteRenderer.
    SpriteRenderer spriteRenderer;
    private string currentAnimation = "ParFrenMylli";
    // Inverte a renderização horizontalmente.

    float verticalInput;
    Vector2 movementDirection;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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

        horizontalInput = movementScript._movement.x;
        verticalInput = movementScript._movement.y;

        movementDirection = new Vector2(horizontalInput, verticalInput).normalized;
        Debug.Log(movementDirection);
        //animator.SetTrigger("frente");

        if (movementDirection.sqrMagnitude > 0)
        {
            if(movementDirection.x > 0 && movementDirection.y == 0)
            {
                spriteRenderer.flipX = false;
                animator.Play("correladomylli");
                currentAnimation = "ParLadoMylli";
            }
            if(movementDirection.x < 0 && movementDirection.y == 0)
            {
                spriteRenderer.flipX = true;
                animator.Play("correladomylli");
                currentAnimation = "ParLadoMylli";
            }
            if(movementDirection.y > 0 && movementDirection.x == 0)
            {
                animator.Play("corretrasmylli");
                currentAnimation = "ParTrasMylli";
            }
            if(movementDirection.y < 0 && movementDirection.x == 0)
            {
                animator.Play("correfrentemylli");
                currentAnimation = "ParFrenMylli";
            }
            if(movementDirection.x > 0 && movementDirection.y > 0)
            {
                spriteRenderer.flipX = false;
                animator.Play("corrediagtrasmylli");
                currentAnimation = "ParDiagTrasMylli";
                
            }
            if(movementDirection.x > 0 && movementDirection.y < 0)
            {
                spriteRenderer.flipX = false;
                animator.Play("corrediagfrentemylli");
                currentAnimation = "ParDiagFrenMylli";
            }
            if(movementDirection.x < 0 && movementDirection.y > 0)
            {
                spriteRenderer.flipX = true;
                animator.Play("corrediagtrasmylli");
                currentAnimation = "ParDiagTrasMylli";
            }
            if(movementDirection.x < 0 && movementDirection.y < 0)
            {
                spriteRenderer.flipX = true;
                animator.Play("corrediagfrentemylli");
                currentAnimation = "ParDiagFrenMylli";
            }
        }else{
            animator.Play(currentAnimation);
        }
    }

}