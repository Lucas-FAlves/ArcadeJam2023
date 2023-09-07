using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHandler : MonoBehaviour
{
    public Collider2D[] inRange;
    private float rangeRadius = 4f;
    [SerializeField] private LayerMask layerMask;
    
    private void Update() 
    { 
        inRange = Physics2D.OverlapCircleAll(gameObject.transform.position, rangeRadius, layerMask);
    }

    
}
