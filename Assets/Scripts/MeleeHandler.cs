using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MeleeHandler : MonoBehaviour
{
    public Collider2D[] inRange;
    [SerializeField] private float rangeRadius = 4f;
    [SerializeField] private LayerMask layerMask;
    private PlayerControls input;

    private Material material;

public void OnMelee(InputAction.CallbackContext ctx)
    {
        inRange = Physics2D.OverlapCircleAll(gameObject.transform.position, rangeRadius, layerMask);
        if (inRange.Length >=1 )
        {
            Debug.Log("Inimigo detectado.");
            material = inRange[0].gameObject.GetComponent<SpriteRenderer>().material;
            material.color = Color.red;
        }
        //Debug.Log("Inimigo detectado.");
    }
    
    private void Awake() 
    {
        input = new PlayerControls();
    }

    private void OnEnable() 
    {
        input.Enable();
        input.Player.Melee.started += OnMelee;  
    }

    private void OnDisable() 
    {
        input.Disable();
        input.Player.Melee.started -= OnMelee;
    }

    

    
}
