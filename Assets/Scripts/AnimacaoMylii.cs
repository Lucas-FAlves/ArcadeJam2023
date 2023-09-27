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

    public MeleeAttackScript meleeAttackScript;
    float verticalInput;
    Vector2 movementDirection;
    Vector2 FacingDirection;
    // Start is called before the first frame update
    void Start()
    {
        MeleeAttackScript meleeAttackScript = GetComponent<MeleeAttackScript>(); 
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

        FacingDirection = baseChar.MovementScript.FacingDirection;
        Debug.Log(meleeAttackScript.attacking);
        //animator.SetTrigger("frente");

        /*if(meleeAttackScript.attacking)
        {
            animator.Play("FrenAtackMylli");
            currentAnimation = "ParFrenMylli";
        }*/

        

        if (movementDirection.sqrMagnitude > 0)
        {
            if(movementDirection.x > 0 && movementDirection.y == 0)
            {
                spriteRenderer.flipX = false;
                if(baseChar.dashing){
                    animator.Play("LadoDashMylli");
                    //currentAnimation = "ParLadoMylli";
                }
                else{
                    animator.Play("correladomylli");
                    currentAnimation = "ParLadoMylli";
                }
            }
            if(movementDirection.x < 0 && movementDirection.y == 0)
            {
                spriteRenderer.flipX = true;
                if(baseChar.dashing){
                    animator.Play("LadoDashMylli");
                    //currentAnimation = "ParLadoMylli";
                }
                else{
                    animator.Play("correladomylli");
                    currentAnimation = "ParLadoMylli";
                }
                
            }
            if(movementDirection.y > 0 && movementDirection.x == 0)
            {
                if(baseChar.dashing){
                    animator.Play("TrasDashMylli");
                    //currentAnimation = "ParTrasMylli";
                }
                else{
                    animator.Play("corretrasmylli");
                    currentAnimation = "ParTrasMylli";
                }
                
            }
            if(movementDirection.y < 0 && movementDirection.x == 0)
            {
                if(baseChar.dashing){
                    animator.Play("FrenDashMylli");
                    //currentAnimation = "ParFrenMylli";
                }
                else{
                    animator.Play("correfrentemylli");
                    currentAnimation = "ParFrenMylli";
                }
            }
            if(movementDirection.x > 0 && movementDirection.y > 0)
            {
                spriteRenderer.flipX = false;
                if(baseChar.dashing){
                    animator.Play("DiagTrasDashMylli");
                    //currentAnimation = "ParDiagTrasMylli";
                }
                else{
                    animator.Play("corrediagtrasmylli");
                    currentAnimation = "ParDiagTrasMylli";
                }
            }
            if(movementDirection.x > 0 && movementDirection.y < 0)
            {
                spriteRenderer.flipX = false;
                if(baseChar.dashing){
                    animator.Play("DiagFrenDashMylli");
                    //currentAnimation = "ParDiagFrenMylli";
                }
                else{
                    animator.Play("corrediagfrentemylli");
                    currentAnimation = "ParDiagFrenMylli";
                }
            }
            if(movementDirection.x < 0 && movementDirection.y > 0)
            {
                spriteRenderer.flipX = true;
                if(baseChar.dashing){
                    animator.Play("DiagTrasDashMylli");
                    //currentAnimation = "ParDiagTrasMylli";
                }
                else{
                    animator.Play("corrediagtrasmylli");
                    currentAnimation = "ParDiagTrasMylli";
                }
            }
            if(movementDirection.x < 0 && movementDirection.y < 0)
            {
                spriteRenderer.flipX = true;
                if(baseChar.dashing){
                    animator.Play("DiagFrenDashMylli");
                    //currentAnimation = "ParDiagFrenMylli";
                }
                else{
                    animator.Play("corrediagfrentemylli");
                    currentAnimation = "ParDiagFrenMylli";
                }
            }
        }else{
            if(FacingDirection.x > 0 && FacingDirection.y == 0)
            {
                spriteRenderer.flipX = false;
                animator.Play("ParLadoMylli");
                
            }
            if(FacingDirection.x < 0 && FacingDirection.y == 0)
            {
                spriteRenderer.flipX = true;
                animator.Play("ParLadoMylli");
                
            }
            if(FacingDirection.y > 0 && FacingDirection.x == 0)
            {
                animator.Play("ParTrasMylli");
                
            }
            if(FacingDirection.y < 0 && FacingDirection.x == 0)
            {
                animator.Play("ParFrenMylli");
                
            }
            if(FacingDirection.x > 0 && FacingDirection.y > 0)
            {
                spriteRenderer.flipX = false;
                animator.Play("ParDiagTrasMylli");
            }
            if(FacingDirection.x > 0 && FacingDirection.y < 0)
            {
                spriteRenderer.flipX = false;
                animator.Play("ParDiagFrenMylli");
                
            }
            if(FacingDirection.x < 0 && FacingDirection.y > 0)
            {
                spriteRenderer.flipX = true;
                animator.Play("ParDiagTrasMylli");
                
            }
            if(FacingDirection.x < 0 && FacingDirection.y < 0)
            {
                spriteRenderer.flipX = true;
                animator.Play("ParDiagFrenMylli");
                
            }
        }
    }

}